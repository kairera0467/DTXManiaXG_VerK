using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;
using SlimDX.Direct3D9;

namespace DTXMania
{
    internal class CAct演奏Drumsレーン : CActivity
    {
        /// <summary>
        /// レーンを描画するクラス。
        /// レーンの他にもクリップのウィンドウ表示も兼ねる。要はクラス作成が面倒だっただけ。
        /// 
        /// 課題
        /// ・レーンの透明度対応
        /// </summary>
        public CAct演奏Drumsレーン()
        {
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            this.n振動X座標 = 0;
            for( int i = 0; i < 10; i++ )
            {
                this.ct進行[ i ] = new CCounter();
            }
            this.ct振動進行 = new CCounter();
            base.On活性化();
        }

        public override void On非活性化()
        {
            for( int i = 0; i < 10; i++ )
            { 
                this.ct進行[ i ] = null;
            }
            this.ct振動進行 = null;
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            for( int i = 0; i < 10; i++ )
            {
                if( CDTXMania.ConfigIni.nLaneDispType.Drums == 0 || CDTXMania.ConfigIni.nLaneDispType.Drums == 2 )
                    this.txLane[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Paret.png" ) );
                else
                    this.txLane[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile black 64x64.png" ) );
            }
            this.txLaneShadow = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_Shadow.png" ) );
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            for( int i = 0; i < 10; i++ )
            { 
                CDTXMania.tテクスチャの解放( ref this.txLane[ i ] );
            }
            CDTXMania.tテクスチャの解放( ref this.txLaneShadow );
            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( CDTXMania.bXGRelease && !( CDTXMania.ConfigIni.nLaneDispType.Drums == 1 || CDTXMania.ConfigIni.nLaneDispType.Drums == 3 ) )
            {
			    for( int i = 0; i < 10; i++ )
    			{
	    			if( !this.ct進行[ i ].b停止中 )
		    		{
			    		this.ct進行[ i ].t進行();
				    	if ( this.ct進行[ i ].b終了値に達した )
					    {
						    this.ct進行[ i ].t停止();
    					}
	    			}
	            }
                if( !this.ct振動進行.b停止中 )
                {
                    this.ct振動進行.t進行();
                    if( this.ct振動進行.b終了値に達した )
                    {
                        this.ct振動進行.t停止();
                    }
                }

                //CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.灰, this.n振動X座標.ToString() );
                //CDTXMania.act文字コンソール.tPrint( 0, 16, C文字コンソール.Eフォント種別.灰, this.ct振動進行.n現在の値.ToString() );
                if( this.ct振動進行.b進行中 )
                    this.n振動X座標 = CDTXMania.Random.Next( 12 ) - 6;
                else
                    this.n振動X座標 = 0;
            }

            if( this.txLaneShadow != null )
            {
                this.txLaneShadow.t2D描画( CDTXMania.app.Device, 0, 0 );
            }

            if( this.txLane[ 0 ] != null ) //LC
            {
                float[] f拡大率 = new float[] { 1.0f, 0.97f, 0.93f, 0.90f, 0.86f, 0.90f, 0.93f, 0.97f, 1.0f }; //BD
                this.txLane[ 0 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 0 ].n現在の値 ];
                this.txLane[ 0 ].t2D描画( CDTXMania.app.Device, 367 - (int)( 72 * f拡大率[ this.ct進行[ 0 ].n現在の値 ] ) + this.n振動X座標, 0, new Rectangle( 0, 0, 72, 720 ) );
            }
            if( this.txLane[ 1 ] != null ) //HH
            {
                float[] f拡大率 = new float[] { 1.0f, 0.97f, 0.93f, 0.90f, 0.86f, 0.90f, 0.93f, 0.97f, 1.0f }; //BD
                this.txLane[ 1 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 1 ].n現在の値 ];
                this.txLane[ 1 ].t2D描画( CDTXMania.app.Device, 416 - (int)( 49 * f拡大率[ this.ct進行[ 1 ].n現在の値 ] ) + this.n振動X座標, 0, new Rectangle( 72, 0, 49, 720 ) );

                this.txLane[ 1 ].vc拡大縮小倍率.X = 1.0f;
                this.txLane[ 1 ].t2D描画( CDTXMania.app.Device, 416 + this.n振動X座標, 0, new Rectangle( 121, 0, 3, 720 ) );
            }
            if( this.txLane[ 2 ] != null ) //SD
            {
                float[] f拡大率 = new float[] { 1.0f, 0.94f, 0.89f, 0.87f, 0.81f, 0.87f, 0.89f, 0.94f, 1.0f }; //BDのものを使いまわし
                int[] nX座標補正 = new int[] { 0, 1, 1, 2, 2, 1, 1, 0, 0 };

                switch( CDTXMania.ConfigIni.eLaneType )
                {
                    case Eタイプ.A:
                    case Eタイプ.C:
                        this.txLane[ 2 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 2 ].n現在の値 ];
                        this.txLane[ 2 ].t2D描画( CDTXMania.app.Device, 497 - (int)( ( 60 / 2 ) * f拡大率[ this.ct進行[ 2 ].n現在の値 ] ) + nX座標補正[ this.ct進行[ 2 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 172, 0, 60, 720 ) );
                        break;
                    case Eタイプ.B:
                    case Eタイプ.D:
                        this.txLane[ 2 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 2 ].n現在の値 ];
                        this.txLane[ 2 ].t2D描画( CDTXMania.app.Device, 446 - (int)( ( 60 / 2 ) * f拡大率[ this.ct進行[ 2 ].n現在の値 ] ) + nX座標補正[ this.ct進行[ 2 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 172, 0, 60, 720 ) );
                        break;
                }
            }
            if( this.txLane[ 3 ] != null ) //BD
            {
                float[] f拡大率 = new float[] { 1.0f, 0.94f, 0.89f, 0.87f, 0.81f, 0.87f, 0.89f, 0.94f, 1.0f }; //BD
                int[] nX座標補正 = new int[] { 0, 1, 1, 2, 4, 2, 1, 1, 0 };
                switch( CDTXMania.ConfigIni.eLaneType )
                {
                    case Eタイプ.A:
                    case Eタイプ.D:
                        this.txLane[ 3 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 3 ].n現在の値 ];
                        this.txLane[ 3 ].t2D描画( CDTXMania.app.Device, 612 - (int)( ( 66 / 2 ) * f拡大率[ this.ct進行[ 3 ].n現在の値 ] ) - nX座標補正[ this.ct進行[ 3 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 284, 0, 66, 720 ) );
                        break;
                    case Eタイプ.B:
                    case Eタイプ.C:
                        this.txLane[ 3 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 3 ].n現在の値 ];
                        this.txLane[ 3 ].t2D描画( CDTXMania.app.Device, 563 - (int)( ( 66 / 2 ) * f拡大率[ this.ct進行[ 3 ].n現在の値 ] ) - nX座標補正[ this.ct進行[ 3 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 284, 0, 66, 720 ) );
                        break;
                }
            }
            if( this.txLane[ 4 ] != null ) //HT
            {
                float[] f拡大率 = new float[] { 1.0f, 0.98f, 0.91f, 0.87f, 0.78f, 0.87f, 0.91f, 0.98f, 1.0f }; //HT
                int[] nX座標補正 = new int[] { 0, 1, 1, 1, 2, 1, 1, 1, 0 };

                switch( CDTXMania.ConfigIni.eLaneType )
                {
                    case Eタイプ.A:
                        this.txLane[ 4 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 4 ].n現在の値 ];
                        this.txLane[ 4 ].t2D描画( CDTXMania.app.Device, 550 - (int)( ( 52 / 2 ) * f拡大率[ this.ct進行[ 4 ].n現在の値 ] ) + nX座標補正[ this.ct進行[ 4 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 229, 0, 52, 720 ) );
                        this.txLane[ 4 ].vc拡大縮小倍率.X = 1.0f;
                        this.txLane[ 4 ].t2D描画( CDTXMania.app.Device, 576 + this.n振動X座標, 0, new Rectangle( 281, 0, 3, 720 ) );
                        break;
                    case Eタイプ.B:
                    case Eタイプ.C:
                        nX座標補正 = new int[] { 0, 1, 1, 1, 2, 1, 1, 1, 0 };
                        f拡大率 = new float[] { 1.0f, 0.96f, 0.94f, 0.92f, 0.88f, 0.92f, 0.94f, 0.96f, 1.0f };
                        this.txLane[ 4 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 4 ].n現在の値 ];
                        this.txLane[ 4 ].t2D描画( CDTXMania.app.Device, 593 - nX座標補正[ this.ct進行[ 4 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 229, 0, 52, 720 ) );
                        this.txLane[ 4 ].vc拡大縮小倍率.X = 1.0f;
                        this.txLane[ 4 ].t2D描画( CDTXMania.app.Device, 527 + this.n振動X座標, 0, new Rectangle( 281, 0, 3, 720 ) );
                        break;
                    case Eタイプ.D:
                        this.txLane[ 4 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 4 ].n現在の値 ];
                        this.txLane[ 4 ].t2D描画( CDTXMania.app.Device, 501 - (int)( ( 52 / 2 ) * f拡大率[ this.ct進行[ 4 ].n現在の値 ] ) + nX座標補正[ this.ct進行[ 4 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 229, 0, 52, 720 ) );
                        break;
                }

            }
            if( this.txLane[ 5 ] != null ) //LT
            {
                float[] f拡大率 = new float[] { 1.0f, 0.96f, 0.94f, 0.92f, 0.88f, 0.92f, 0.94f, 0.96f, 1.0f }; //LT
                int[] nX座標補正 = new int[] { 0, 1, 1, 1, 2, 1, 1, 1, 0 };
                this.txLane[ 5 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 5 ].n現在の値 ];
                this.txLane[ 5 ].t2D描画( CDTXMania.app.Device, 642 - nX座標補正[ this.ct進行[ 5 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 347, 0, 52, 720 ) );
            }
            if( this.txLane[ 6 ] != null ) //FT
            {
                float[] f拡大率 = new float[] { 1.0f, 0.96f, 0.94f, 0.92f, 0.88f, 0.92f, 0.94f, 0.96f, 1.0f }; //LTの使い回し
                int[] nX座標補正 = new int[] { 0, 1, 1, 1, 2, 1, 1, 1, 0 };
                this.txLane[ 6 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 6 ].n現在の値 ];
                this.txLane[ 6 ].t2D描画( CDTXMania.app.Device, 691 - nX座標補正[ this.ct進行[ 6 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 396, 0, 52, 720 ) );

                this.txLane[ 6 ].vc拡大縮小倍率.X = 1.0f;
                this.txLane[ 6 ].t2D描画( CDTXMania.app.Device, 743 + this.n振動X座標, 0, new Rectangle( 448, 0, 2, 720 ) );
            }
            if( this.txLane[ 9 ] != null ) //RD
            {
                float[] f拡大率 = new float[] { 1.0f, 0.96f, 0.94f, 0.92f, 0.88f, 0.92f, 0.94f, 0.96f, 1.0f }; //HTの使い回し
                int[] nX座標補正 = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 0 };
                this.txLane[ 9 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 9 ].n現在の値 ];

                switch( CDTXMania.ConfigIni.eRDPosition )
                {
                    case ERDPosition.RCRD:
                        this.txLane[ 9 ].t2D描画( CDTXMania.app.Device, 815 - nX座標補正[ this.ct進行[ 9 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 520, 0, 38, 720 ) );
                        this.txLane[ 9 ].vc拡大縮小倍率.X = 1.0f;
                        this.txLane[ 9 ].t2D描画( CDTXMania.app.Device, 812 + this.n振動X座標, 0, new Rectangle( 517, 0, 3, 720 ) );
                        break;
                    case ERDPosition.RDRC:
                        this.txLane[ 9 ].t2D描画( CDTXMania.app.Device, 744 - nX座標補正[ this.ct進行[ 9 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 520, 0, 38, 720 ) );
                        this.txLane[ 9 ].vc拡大縮小倍率.X = 1.0f;
                        this.txLane[ 9 ].t2D描画( CDTXMania.app.Device, 783 + this.n振動X座標, 0, new Rectangle( 517, 0, 3, 720 ) );
                        break;
                }

            }
            if( this.txLane[ 7 ] != null ) //CY
            {
                float[] f拡大率 = new float[] { 1.0f, 0.96f, 0.94f, 0.92f, 0.88f, 0.92f, 0.94f, 0.96f, 1.0f }; //LCの使い回し
                int[] nX座標補正 = new int[] { 0, 2, 3, 4, 6, 4, 3, 2, 0 };
                this.txLane[ 7 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 7 ].n現在の値 ];
                this.txLane[ 7 ].t2D描画( CDTXMania.app.Device, 745 - nX座標補正[ this.ct進行[ 7 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 450, 0, 70, 720 ) );
                switch( CDTXMania.ConfigIni.eRDPosition )
                {
                    case ERDPosition.RCRD:
                        this.txLane[ 7 ].t2D描画( CDTXMania.app.Device, 745 - nX座標補正[ this.ct進行[ 7 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 450, 0, 70, 720 ) );
                        break;
                    case ERDPosition.RDRC:
                        this.txLane[ 7 ].t2D描画( CDTXMania.app.Device, 783 - nX座標補正[ this.ct進行[ 7 ].n現在の値 ] + this.n振動X座標, 0, new Rectangle( 450, 0, 70, 720 ) );
                        break;
                }
            }
            if( this.txLane[ 8 ] != null ) //LP LBD
            {
                float[] f拡大率 = new float[] { 1.0f, 0.96f, 0.92f, 0.90f, 0.86f, 0.90f, 0.92f, 0.96f, 1.0f }; //LP LBD
                this.txLane[ 8 ].vc拡大縮小倍率.X = f拡大率[ this.ct進行[ 8 ].n現在の値 ];

                switch( CDTXMania.ConfigIni.eLaneType )
                {
                    case Eタイプ.A:
                        this.txLane[ 8 ].t2D描画( CDTXMania.app.Device, 467 - (int)( 51 * f拡大率[ this.ct進行[ 8 ].n現在の値 ] ) + this.n振動X座標, 0, new Rectangle( 121, 0, 51, 720 ) );
                        break;
                    case Eタイプ.C:
                        this.txLane[ 8 ].t2D描画( CDTXMania.app.Device, 467 - (int)( 51 * f拡大率[ this.ct進行[ 8 ].n現在の値 ] ) + this.n振動X座標, 0, new Rectangle( 121, 0, 51, 720 ) );
                        this.txLane[ 8 ].vc拡大縮小倍率.X = 1.0f;
                        this.txLane[ 8 ].t2D描画( CDTXMania.app.Device, 524 + this.n振動X座標, 0, new Rectangle( 281, 0, 3, 720 ) );
                        break;
                    case Eタイプ.B:
                        this.txLane[ 8 ].t2D描画( CDTXMania.app.Device, 524 - (int)( 51 * f拡大率[ this.ct進行[ 8 ].n現在の値 ] ) + this.n振動X座標, 0, new Rectangle( 121, 0, 51, 720 ) );
                        this.txLane[ 8 ].vc拡大縮小倍率.X = 1.0f;
                        this.txLane[ 8 ].t2D描画( CDTXMania.app.Device, 524 + this.n振動X座標, 0, new Rectangle( 281, 0, 3, 720 ) );
                        break;
                    case Eタイプ.D:
                        this.txLane[ 8 ].t2D描画( CDTXMania.app.Device, 575 - (int)( 51 * f拡大率[ this.ct進行[ 8 ].n現在の値 ] ) + this.n振動X座標, 0, new Rectangle( 121, 0, 51, 720 ) );
                        break;
                }
            }

            return base.On進行描画();
        }

		public void Start( Eレーン lane )
		{
			this.ct進行[ (int) lane ] = new CCounter( 0, 8, 12, CDTXMania.Timer );
		}

        public void tVibration()
        {
			this.ct振動進行 = new CCounter( 0, 30, 20, CDTXMania.Timer );
        }

        #region[ private ]
        //-----------------
        private CTexture[] txLane = new CTexture[ 10 ];
        private CCounter[] ct進行 = new CCounter[ 10 ];
        private CCounter ct振動進行;
        private CTexture txLaneShadow;
        private int n振動X座標;
        //-----------------
        #endregion
    }
}
