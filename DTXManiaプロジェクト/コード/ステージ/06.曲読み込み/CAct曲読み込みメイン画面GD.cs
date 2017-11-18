using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using SlimDX;
using FDK;

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

		}
		public void On非活性化()
		{

		}
		public void OnManagedリソースの作成()
		{
            
		}
		public void OnManagedリソースの解放()
		{
            CDTXMania.tテクスチャの解放( ref this.txJacket );
		}
		public int On進行描画()
		{
            if( this.txJacket != null )
            {
                //とりあえず400x400(1:1)前提で
                this.txJacket.vc拡大縮小倍率 = new Vector3( 384.0f / this.txJacket.sz画像サイズ.Width, 384.0f / this.txJacket.sz画像サイズ.Height, 1.0f );
                this.txJacket.t2D描画( CDTXMania.app.Device, 100, 77 );
            }
            if( this.txDiffPanel != null )
            {
                this.txDiffPanel.t2D描画( CDTXMania.app.Device, 520, 77 );
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
                this.txJacket = CDTXMania.tテクスチャの生成( path );
            }
        }

        public void t難易度パネルの描画( int level )
        {
            //しばらくは短形描画で実装
            Bitmap canvas = new Bitmap( 278, 188 ); //42
            Graphics g = Graphics.FromImage( canvas );

            SolidBrush sbBack = new SolidBrush( Color.FromArgb( 50, 50, 50 ) );
            SolidBrush sbLabel = new SolidBrush( Color.FromArgb( 70, 140, 255 ) ); //NOVICE

            g.FillRectangle( sbBack, 0, 0, 278, 188 );
            g.FillRectangle( sbLabel, 0, 0, 278, 42 );

            this.txDiffPanel = CDTXMania.tテクスチャの生成( canvas );
            g.Dispose();
            sbBack.Dispose();
            sbLabel.Dispose();
            canvas.Dispose();
        }

        // その他

        #region [ private ]
        //-----------------
        private CTexture txJacket;
        private CTexture txDiffPanel;
		//-----------------
		#endregion
	}
}
