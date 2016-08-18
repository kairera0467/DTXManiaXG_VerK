using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActResultSongBar : CActivity
	{
		// コンストラクタ

		public CActResultSongBar()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			//this.ct登場用.n現在の値 = this.ct登場用.n終了値;
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.ft曲名用フォント = new Font( "MS PGothic", 44f * Scale.Y, FontStyle.Bold, GraphicsUnit.Pixel );
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ft曲名用フォント != null )
			{
				this.ft曲名用フォント.Dispose();
				this.ft曲名用フォント = null;
			}
			if( this.ct登場用 != null )
			{
				this.ct登場用 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txバー = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\ScreenResult song bar.png" ), false );
				try
				{
					Bitmap image = new Bitmap( (int)(0x3a8 * Scale.X), (int)(0x36 * Scale.Y) );
					Graphics graphics = Graphics.FromImage( image );
					graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
					graphics.DrawString( CDTXMania.DTX.TITLE, this.ft曲名用フォント, Brushes.White, ( float ) 8f * Scale.X, ( float ) 0f );
					this.tx曲名 = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );
					this.tx曲名.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
					graphics.Dispose();
					image.Dispose();
				}
				catch( CTextureCreateFailedException )
				{
					Trace.TraceError( "曲名テクスチャの生成に失敗しました。" );
					this.tx曲名 = null;
				}
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txバー );
				CDTXMania.tテクスチャの解放( ref this.tx曲名 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				this.ct登場用 = new CCounter( 0, 270, 4, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct登場用.t進行();
			if( !this.ct登場用.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter ct登場用;
		private Font ft曲名用フォント;
		private CTextureAf txバー;
		private CTexture tx曲名;
		//-----------------
		#endregion
	}
}
