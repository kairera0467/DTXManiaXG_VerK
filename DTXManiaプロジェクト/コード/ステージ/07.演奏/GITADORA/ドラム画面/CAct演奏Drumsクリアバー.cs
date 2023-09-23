using System.Drawing;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsクリアバーGD : CAct演奏クリアバー共通
    {
        public CAct演奏DrumsクリアバーGD()
        {
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            base.On活性化();
        }

        public override void On非活性化()
        {
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            if( !this.b活性化してない )
            {
                #region[ ゲージ粒 ]
                // 横 現在:7px 記録:3px
                // 縦 8pxの64分割で合計512px
                Bitmap canvas = new Bitmap( 7, 8 );
                Graphics g = Graphics.FromImage(canvas);

                SolidBrush sbOK = new SolidBrush( Color.FromArgb( 225, 225, 60 ) );
                SolidBrush sbNG  = new SolidBrush( Color.FromArgb( 110, 140, 230 ) );

                // OK色
                g.FillRectangle( sbOK, 0, 0, 7, 8 );
                this.txClearGauge_OK = CDTXMania.tテクスチャの生成( canvas );
            
                // NG色
                g.FillRectangle( sbNG, 0, 0, 7, 8 );
                this.txClearGauge_NG = CDTXMania.tテクスチャの生成( canvas );


                g.Dispose();
                sbOK.Dispose();
                sbNG.Dispose();
                canvas.Dispose();
                #endregion
                this.txSongBar_Cursor = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Songbar Cursor.png" ) );

                base.OnManagedリソースの作成();
            }
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txClearGauge_NG );
            CDTXMania.tテクスチャの解放( ref this.txClearGauge_OK );
            CDTXMania.tテクスチャの解放( ref this.txSongBar_Cursor );
            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( this.b初めての進行描画 )
            {
                base.b初めての進行描画 = false;
            }
            this.dbNowPos = ( ( double ) CDTXMania.Timer.n現在時刻 ) / 1000.0;
            this.db現在の曲進行割合 = dbNowPos / dbEndPos;

            for( int j = n現在の区間; j < SECTION_COUNT; j++ )
            {
                if( this.dbNowPos >= db区間位置[ j ] )
                {
                    base.tクリアゲージ判定();
                    this.n現在の区間++;
                    break;
                }
            }

            for( int i = 0; i < SECTION_COUNT; i++ )
            {
                if( i < this.n現在の区間 - 1 )
                {
                    if( base.bClearBar.Drums[ i ] ) {
                        if( this.txClearGauge_OK != null ) {
                            this.txClearGauge_OK.t2D描画( CDTXMania.app.Device, 904, 575 - ( i * 8 ) );
                        }
                    } else {
                        if( this.txClearGauge_NG != null ) {
                            this.txClearGauge_NG.t2D描画( CDTXMania.app.Device, 904, 575 - ( i * 8 ) );
                        }
                    }
                }
            }
            this.txSongBar_Cursor.t2D描画( CDTXMania.app.Device, 853, db現在の曲進行割合 <= 1.0 ? 570 - (int)( 512.0 * db現在の曲進行割合 ) : 58 );
            return 0;
        }

        #region[ private ]
        //-----------------
        private CTexture txClearGauge_OK;
        private CTexture txClearGauge_NG;
        private CTexture txSongBar_Cursor;
        //-----------------
        #endregion
    }
}
