using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using SharpDX;
using FDK;

using Color = System.Drawing.Color;
using Rectangle = System.Drawing.Rectangle;
using Point = System.Drawing.Point;
namespace DTXMania
{
	internal class CAct曲読み込みメイン画面GD
	{
		// メソッド

		public CAct曲読み込みメイン画面GD()
		{

		}

		// CActivity 実装

		public void On活性化()
		{
            // TODO: 基準座標の設定
		}
		public void On非活性化()
		{

		}
		public void OnManagedリソースの作成()
		{
            this.txLabelName = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_Difficulty.png" ) );
            this.txDifficultyNumber = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_Difficulty_Number.png" ) );
            this.rectLabelName = new Rectangle( 0, 0, 0, 0 );
            this.pfTitleName = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント), 40, FontStyle.Regular );
            this.pfArtistName = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 30, FontStyle.Regular );
		}
		public void OnManagedリソースの解放()
		{
            CDTXMania.tテクスチャの解放( ref this.txJacket );
            CDTXMania.tテクスチャの解放( ref this.txTitle );
            CDTXMania.tテクスチャの解放( ref this.txArtist );
            CDTXMania.tテクスチャの解放( ref this.txDiffPanel );
            CDTXMania.tテクスチャの解放( ref this.txLabelName );
            CDTXMania.tテクスチャの解放( ref this.txDifficultyNumber );

            CDTXMania.t安全にDisposeする( ref this.pfTitleName );
            CDTXMania.t安全にDisposeする( ref this.pfArtistName );
		}
		public int On進行描画()
		{
            if( this.txJacket != null )
            {
                //とりあえず400x400(1:1)前提で
                this.txJacket.vc拡大縮小倍率 = new Vector3( 384.0f / this.txJacket.sz画像サイズ.Width, 384.0f / this.txJacket.sz画像サイズ.Height, 1.0f );
                this.txJacket.t2D描画( CDTXMania.app.Device, 100, 77 );
            }

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                if( this.txDiffPanel != null )
                {
                    this.txDiffPanel.t2D描画( CDTXMania.app.Device, 520, 77 );
                    if( this.txLabelName != null )
                    {
                        this.txLabelName.t2D描画( CDTXMania.app.Device, 616, 88, this.rectLabelName );
                    }
                    this.t大文字表示( 538, 138, CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.strレベル小数点含.Drums );
                }
            }
            else
            {
                // TODO: ギター2人用レイアウト(画像縮小) 
                int[] x = new int[] { 520, 917 };

                for (int i = 0; i <= 1; i++)
                {
                    if (!CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.b譜面がある[ i + 1 ])
                        continue;

                    if( this.txDiffPanel != null )
                    {
                        this.txDiffPanel.t2D描画( CDTXMania.app.Device, x[i], 77 );
                        if( this.txLabelName != null )
                        {
                            this.txLabelName.t2D描画( CDTXMania.app.Device, x[i] + 96, 88, this.rectLabelName );
                        }
                        this.t大文字表示( x[i] + 18, 138, CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.strレベル小数点含[ i + 1 ] );
                    }
                }
            }

            if( this.txTitle != null )
            {
                if (this.txTitle.sz画像サイズ.Width >= 625)
                    this.txTitle.vc拡大縮小倍率.X = 625f / this.txTitle.sz画像サイズ.Width;

                this.txTitle.t2D描画(CDTXMania.app.Device, 500, 275);
            }

            if( this.txArtist != null )
            {
                if( this.txArtist.sz画像サイズ.Width >= 625 )
                    this.txArtist.vc拡大縮小倍率.X = 625f / this.txArtist.sz画像サイズ.Width;

                this.txArtist.t2D描画(CDTXMania.app.Device, 500, 350);
            }
			return 0;
		}

        /// <summary>
        /// ジャケット画像を受け渡しする
        /// </summary>
        public void t指定されたパスからジャケット画像を生成する( string path )
        {
            if( this.txJacket == null )
            {
                if( File.Exists(path) )
                {
                    this.txJacket = CDTXMania.tテクスチャの生成( path );
                }
                else
                {
                    this.txJacket = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                }
            }
        }

        public void t難易度パネルの描画( int level )
        {
            //しばらくは短形描画で実装
            Bitmap canvas = new Bitmap( 278, 188 ); //42
            Graphics g = Graphics.FromImage( canvas );

            Color colorLabel = Color.White;
            switch( level )
            {
                case 0:
                    colorLabel = Color.FromArgb( 70, 140, 255 );
                    this.rectLabelName = new Rectangle( 0, 0, 180, 28 );
                    break;
                case 1:
                    colorLabel = Color.FromArgb( 236, 161, 0 );
                    this.rectLabelName = new Rectangle( 0, 28, 180, 28 );
                    break;
                case 2:
                    colorLabel = Color.FromArgb( 255, 107, 119 );
                    this.rectLabelName = new Rectangle( 0, 54, 180, 28 );
                    break;
                case 3:
                    colorLabel = Color.FromArgb( 188, 104, 225 );
                    this.rectLabelName = new Rectangle( 0, 84, 180, 28 );
                    break;
                case 4:
                    colorLabel = Color.FromArgb( 128, 128, 128 );
                    this.rectLabelName = new Rectangle( 0, 112, 180, 28 );
                    break;
            }

            SolidBrush sbBack = new SolidBrush( Color.FromArgb( 50, 50, 50 ) );
            SolidBrush sbLabel = new SolidBrush( colorLabel ); //NOVICE

            g.FillRectangle( sbBack, 0, 0, 278, 188 );
            g.FillRectangle( sbLabel, 0, 0, 278, 42 );

            this.txDiffPanel = CDTXMania.tテクスチャの生成( canvas );
            g.Dispose();
            sbBack.Dispose();
            sbLabel.Dispose();
            canvas.Dispose();
        }

        public void t曲名アーティスト名テクスチャの生成( string str曲タイトル, string strアーティスト名 )
        {
            try
            {
                #region[ 曲名、アーティスト名テクスチャの生成 ]
                if( ( str曲タイトル != null ) && ( str曲タイトル.Length > 0 ) )
                {
                    Bitmap bmpSongName = pfTitleName.DrawPrivateFont( str曲タイトル, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
                    this.txTitle = CDTXMania.tテクスチャの生成( bmpSongName, false );
                    bmpSongName.Dispose();
                }
                else
                {
                    this.txTitle = null;
                }

                if( ( strアーティスト名 != null) && ( strアーティスト名.Length > 0 ) )
                {
                    Bitmap bmpArtistName = pfArtistName.DrawPrivateFont( strアーティスト名, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
                    this.txArtist = CDTXMania.tテクスチャの生成(bmpArtistName, false);
                    bmpArtistName.Dispose();
                }
                else
                {
                    this.txArtist = null;
                }
                #endregion
            }
            catch( CTextureCreateFailedException )
            {
                Trace.TraceError( "テクスチャの生成に失敗しました。" );
                this.txTitle = null;
                this.txArtist = null;
            }
        }

        // その他

        #region [ private ]
        //-----------------
        private CTexture txJacket;
        private CTexture txDiffPanel;
        private CTexture txTitle;
        private CTexture txArtist;
        private CTexture txLabelName;
        private CTexture txDifficultyNumber;

        private CPrivateFastFont pfTitleName;
        private CPrivateFastFont pfArtistName;

        private Rectangle rectLabelName;

        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
            public ST文字位置(char ch, Point pt)
            {
                this.ch = ch;
                this.pt = pt;
            }
        }
        private ST文字位置[] st大文字位置 = new ST文字位置[] {
            new ST文字位置( '.', new Point( 780, 0 ) ),
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 78, 0 ) ),
            new ST文字位置( '2', new Point( 156, 0 ) ),
            new ST文字位置( '3', new Point( 234, 0 ) ),
            new ST文字位置( '4', new Point( 312, 0 ) ),
            new ST文字位置( '5', new Point( 390, 0 ) ),
            new ST文字位置( '6', new Point( 468, 0 ) ),
            new ST文字位置( '7', new Point( 546, 0 ) ),
            new ST文字位置( '8', new Point( 624, 0 ) ),
            new ST文字位置( '9', new Point( 702, 0 ) )
        };
        //-----------------
        private void t大文字表示(int x, int y, string str)
        {
            for( int i = 0; i < str.Length; i++ )
            {
                char c = str[ i ];
                for( int j = 0; j < this.st大文字位置.Length; j++ )
                {
                    if( this.st大文字位置[ j ].ch == c )
                    {
                        Rectangle rc画像内の描画領域 = new Rectangle( this.st大文字位置[ j ].pt.X, this.st大文字位置[ j ].pt.Y, 78, 116 );
                        if( c == '.' )
                        {
                            rc画像内の描画領域.Width -= 65;
                        }
                        if( this.txDifficultyNumber != null )
                        {
                            this.txDifficultyNumber.t2D描画( CDTXMania.app.Device, x, y, rc画像内の描画領域 );
                        }
                        break;
                    }
                }
                if( c == '.' ) x += 16;
                else x += 74;
            }
        }
        #endregion
    }
}
