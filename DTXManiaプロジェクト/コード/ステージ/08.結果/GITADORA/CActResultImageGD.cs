using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using SharpDX;
using FDK;

using Color = System.Drawing.Color;
namespace DTXMania
{
	internal class CActResultImageGD : CActResultImage共通
	{
		// コンストラクタ

		public CActResultImageGD()
		{
			base.b活性化してない = true;
		}

        
		// CActivity 実装

		public override void On活性化()
		{
            if( CDTXMania.bXGRelease ) return;
			base.On活性化();
		}
		public override void On非活性化()
		{
            if( CDTXMania.bXGRelease ) return;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
            if( CDTXMania.bXGRelease ) return;
			if( !base.b活性化してない )
			{
                this.tx背景パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_back panel.png" ) );
                this.txジャケットパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_jacket panel.png" ) );
                this.tx曲名パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Songpanel.png" ) );
                
                this.pfSongTitleFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 20, FontStyle.Regular );
                this.pfSongArtistFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 14, FontStyle.Regular );

                this.tx曲名 = this.t指定された文字テクスチャを生成する( CDTXMania.stage選曲GITADORA.r確定された曲.strタイトル );
                this.txアーティスト名 = this.t指定された文字テクスチャを生成する_小( CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.アーティスト名 );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
            if( CDTXMania.bXGRelease ) return;
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.tx背景パネル );
                CDTXMania.tテクスチャの解放( ref this.txジャケットパネル );
                CDTXMania.tテクスチャの解放( ref this.tx曲名パネル );

                CDTXMania.tテクスチャの解放( ref this.tx曲名 );
                CDTXMania.tテクスチャの解放( ref this.txアーティスト名 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
            if( CDTXMania.bXGRelease ) return 1;
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
                if( !this.tプレビュー画像の指定があれば構築する() )
                {
                    this.r表示するリザルト画像 = this.txリザルト画像がないときの画像;
                }
				base.b初めての進行描画 = false;
			}



            if( this.tx背景パネル != null )
            {
                this.tx背景パネル.t2D描画( CDTXMania.app.Device, 0, 0 );
            }
            if( this.txジャケットパネル != null )
            {
                this.txジャケットパネル.t2D描画( CDTXMania.app.Device, 442, 127 ); //発展性を考えたら中心基準描画にするべきか...
            }
            if( this.tx曲名パネル != null )
            {
                this.tx曲名パネル.t2D描画( CDTXMania.app.Device, 440, 532 );
            }
            if( this.r表示するリザルト画像 != null )
            {
                int width = this.r表示するリザルト画像.szテクスチャサイズ.Width;
                int height = this.r表示するリザルト画像.szテクスチャサイズ.Height;

                this.r表示するリザルト画像.vc拡大縮小倍率 = new Vector3( 382.0f / width, 382.0f / height, 1.0f );

                this.r表示するリザルト画像.t2D描画( CDTXMania.app.Device, 446, 131 );
            }
            if( this.tx曲名 != null )
            {
                this.tx曲名.t2D描画( CDTXMania.app.Device, 448, 540 );
            }
            if( this.txアーティスト名 != null )
            {
                this.txアーティスト名.t2D描画( CDTXMania.app.Device, 450, 584 );
            }

			return 1;
		}


        // その他

        #region [ private ]
        //-----------------
        private CTexture tx背景パネル;
        private CTexture txジャケットパネル;
        private CTexture tx曲名パネル;
        private CTexture tx曲名;
        private CTexture txアーティスト名;
        private CPrivateFastFont pfSongArtistFont;
        private CPrivateFastFont pfSongTitleFont;

        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            if( this.pfSongTitleFont == null ) return null;
            Bitmap bmp;
            bmp = this.pfSongTitleFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }
        private CTexture t指定された文字テクスチャを生成する_小( string str文字 )
        {
            if( this.pfSongArtistFont == null ) return null;
            Bitmap bmp;
            bmp = this.pfSongArtistFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }
        //-----------------
        #endregion
    }
}
