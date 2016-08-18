using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CActSelectInformation : CActivity
	{
		// コンストラクタ

		public CActSelectInformation()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n画像Index上 = -1;
			this.n画像Index下 = 0;
			base.On活性化();
		}
		public override void On非活性化()
		{
			this.ctスクロール用 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				string[,] infofiles = {		// #25381 2011.6.4 yyagi
				   { @"Graphics\ScreenSelect information 1.png", @"Graphics\ScreenSelect information 2.png" },
				   { @"Graphics\ScreenSelect information 1e.png", @"Graphics\ScreenSelect information 2e.png" }
				};
				int c = ( CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja" ) ? 0 : 1; 
				this.txInfo[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( infofiles[ c, 0 ] ), false );
				this.txInfo[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( infofiles[ c, 1 ] ), false );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txInfo[ 0 ] );
				CDTXMania.tテクスチャの解放( ref this.txInfo[ 1 ] );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
					this.ctスクロール用 = new CCounter( 0, 6000, 1, CDTXMania.Timer );
					base.b初めての進行描画 = false;
				}
				this.ctスクロール用.t進行();
				if( this.ctスクロール用.b終了値に達した )
				{
					this.n画像Index上 = this.n画像Index下;
					this.n画像Index下 = ( this.n画像Index下 + 1 ) % stInfo.GetLength( 0 );		//8;
					this.ctスクロール用.n現在の値 = 0;
				}
				int n現在の値 = this.ctスクロール用.n現在の値;
				if( n現在の値 <= 250 )
				{
					double n現在の割合 = ( (double) n現在の値 ) / 250.0;
					if( this.n画像Index上 >= 0 )
					{
						STINFO stinfo = this.stInfo[ this.n画像Index上 ];
						Rectangle rectangle = new Rectangle(
							stinfo.pt左上座標.X,
							stinfo.pt左上座標.Y + ( (int) ( (int)(45.0 * Scale.Y) * n現在の割合 ) ),
							(int)(221 * Scale.X),
							Convert.ToInt32((int)(45.0 * Scale.Y) * (1.0 - n現在の割合))
						);
						if( this.txInfo[ stinfo.nTexture番号 ] != null )
						{
							this.txInfo[ stinfo.nTexture番号 ].t2D描画(
								CDTXMania.app.Device,
								115 * Scale.X,
								6 * Scale.Y,
								rectangle
							);
						}
					}
					if( this.n画像Index下 >= 0 )
					{
						STINFO stinfo = this.stInfo[ this.n画像Index下 ];
						Rectangle rectangle = new Rectangle(
							stinfo.pt左上座標.X,
							stinfo.pt左上座標.Y,
							(int)(221 * Scale.X),
							(int) ( 45.0 * Scale.Y * n現在の割合 )
						);
						if( this.txInfo[ stinfo.nTexture番号 ] != null )
						{
							this.txInfo[ stinfo.nTexture番号 ].t2D描画(
								CDTXMania.app.Device,
								115 * Scale.X,
								6 * Scale.Y + ( (int) ( 45.0 * Scale.Y * ( 1.0 - n現在の割合 ) ) ),
								rectangle
							);
						}
					}
				}
				else
				{
					STINFO stinfo = this.stInfo[ this.n画像Index下 ];
					Rectangle rectangle = new Rectangle(
						stinfo.pt左上座標.X,
						stinfo.pt左上座標.Y,
						(int)(221 * Scale.X),
						(int)(45 * Scale.Y)
					);
					if( this.txInfo[ stinfo.nTexture番号 ] != null )
					{
						this.txInfo[ stinfo.nTexture番号 ].t2D描画(
							CDTXMania.app.Device,
							115 * Scale.X,
							6 * Scale.Y,
							rectangle
						);
					}
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct STINFO
		{
			public int nTexture番号;
			public Point pt左上座標;
			public STINFO( int nTexture番号, int x, int y )
			{
				this.nTexture番号 = nTexture番号;
				this.pt左上座標 = new Point( x, y );
			}
		}

		private CCounter ctスクロール用;
		private int n画像Index下;
		private int n画像Index上;
		private readonly STINFO[] stInfo = new STINFO[] {
			new STINFO( 0, 0, 0 ),
			new STINFO( 0, 0, (int)(49 * Scale.Y) ),
			new STINFO( 0, 0, (int)(97 * Scale.Y) ),
			new STINFO( 0, 0, (int)(147 * Scale.Y) ),
			new STINFO( 0, 0, (int)(196 * Scale.Y) ),
			new STINFO( 1, 0, (int)(0 * Scale.Y) ),
			new STINFO( 1, 0, (int)(49 * Scale.Y) ),
			new STINFO( 1, 0, (int)(97 * Scale.Y) ),
			new STINFO( 1, 0, (int)(147 * Scale.Y) )
		};
		private CTexture[] txInfo = new CTexture[ 2 ];
		//-----------------
		#endregion
	}
}
