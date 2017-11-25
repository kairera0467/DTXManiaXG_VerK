using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActSelect演奏履歴パネル : CActivity
	{
		// メソッド

		public CActSelect演奏履歴パネル()
		{
			base.b活性化してない = true;
		}
		public void t選択曲が変更された()
		{
			Cスコア cスコア;
            bool bスクロール中 = false;
            if( CDTXMania.bXGRelease ) { cスコア = CDTXMania.stage選曲XG.r現在選択中のスコア; bスクロール中 = CDTXMania.stage選曲XG.bスクロール中; }
            else { cスコア = CDTXMania.stage選曲GITADORA.r現在選択中のスコア; bスクロール中 = CDTXMania.stage選曲GITADORA.bスクロール中; }

			if( ( cスコア != null ) && !bスクロール中 )
			{
				try
				{
					Bitmap image = new Bitmap( 800, 240 );
					Graphics graphics = Graphics.FromImage( image );
					graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
					for ( int i = 0; i < 5; i++ )
					{
						if( ( cスコア.譜面情報.演奏履歴[ i ] != null ) && ( cスコア.譜面情報.演奏履歴[ i ].Length > 0 ) )
						{
							graphics.DrawString( cスコア.譜面情報.演奏履歴[ i ], this.ft表示用フォント, Brushes.Yellow, (float) 0f, (float) ( i * 36f ) );
						}
					}
					graphics.Dispose();
					if( this.tx文字列パネル != null )
					{
						this.tx文字列パネル.Dispose();
					}
					this.tx文字列パネル = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );
					this.tx文字列パネル.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
					image.Dispose();
				}
				catch( CTextureCreateFailedException )
				{
					Trace.TraceError( "演奏履歴文字列テクスチャの作成に失敗しました。" );
					this.tx文字列パネル = null;
				}
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n本体X = 810;
			this.n本体Y = 558;
			this.ft表示用フォント = new Font( "Arial", 30f, FontStyle.Bold, GraphicsUnit.Pixel );
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ft表示用フォント != null )
			{
				this.ft表示用フォント.Dispose();
				this.ft表示用フォント = null;
			}
			this.ct登場アニメ用 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_play history panel.png" ), true );
				this.t選択曲が変更された();
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.tx文字列パネル );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
					this.ct登場アニメ用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
					base.b初めての進行描画 = false;
				}
				this.ct登場アニメ用.t進行();
				if( this.ct登場アニメ用.b終了値に達した || ( this.txパネル本体 == null ) )
				{
					this.n本体X = 810;
					this.n本体Y = 558;
				}
				else
				{
					double num = ( (double) this.ct登場アニメ用.n現在の値 ) / 100.0;
					double num2 = Math.Cos( ( 1.5 + ( 0.5 * num ) ) * Math.PI );
					this.n本体X = 810;
					this.n本体Y = 558 + ( (int) ( this.txパネル本体.sz画像サイズ.Height * ( 1.0 - ( num2 * num2 ) ) ) );
				}
                if( CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_決定演出 || CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト )
                {
                    if( CDTXMania.bXGRelease )
                    {
                        this.n本体Y = 558 + ( CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 <= 250 && CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 >= 0 ? (int)( 70 * ( ( CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 ) / 250.0 ) ) : 0 );
                        this.txパネル本体.n透明度 = (int)( 255 - ( 255 * ( ( CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 ) / 250.0 ) ) );
                        this.tx文字列パネル.n透明度 = (int)( 255 - ( 255 * ( ( CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 ) / 250.0 ) ) );
                        if( CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 > 250 )
                        {
                            this.n本体Y = 558 + 70;
                            this.txパネル本体.n透明度 = 0;
                            this.tx文字列パネル.n透明度 = 0;
                        }
                    }

                }
				if( this.txパネル本体 != null )
				{
					this.txパネル本体.t2D描画( CDTXMania.app.Device, this.n本体X, this.n本体Y );
				}
				if( this.tx文字列パネル != null )
				{
					this.tx文字列パネル.t2D描画( CDTXMania.app.Device, this.n本体X + 28, this.n本体Y + 26 );
				}
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
		private CCounter ct登場アニメ用;
		private Font ft表示用フォント;
		private int n本体X;
		private int n本体Y;
		private CTexture txパネル本体;
		private CTexture tx文字列パネル;
		//-----------------
		#endregion
	}
}
