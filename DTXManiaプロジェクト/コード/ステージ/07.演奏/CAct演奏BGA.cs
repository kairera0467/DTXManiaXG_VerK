using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using SharpDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CAct演奏BGA : CActivity
	{
		// コンストラクタ

		public CAct演奏BGA()
		{
			base.b活性化してない = true;
		}
		
		
		// メソッド

		public void ChangeScope( int nチャンネル, CDTX.CBMP bmp, CDTX.CBMPTEX bmptex )
		{
			for( int i = 0; i < 8; i++ )
			{
				if( nチャンネル == this.nChannel[ i ] )
				{
					this.stLayer[ i ].rBMP = bmp;
					this.stLayer[ i ].rBMPTEX = bmptex;
					return;
				}
			}
		}
		public void Start( int nチャンネル, CDTX.CBMP bmp, CDTX.CBMPTEX bmptex, int n開始サイズW, int n開始サイズH, int n終了サイズW, int n終了サイズH, int n画像側開始位置X, int n画像側開始位置Y, int n画像側終了位置X, int n画像側終了位置Y, int n表示側開始位置X, int n表示側開始位置Y, int n表示側終了位置X, int n表示側終了位置Y, int n総移動時間ms )
		{
			this.Start( nチャンネル, bmp, bmptex, n開始サイズW, n開始サイズH, n終了サイズW, n終了サイズH, n画像側開始位置X, n画像側開始位置Y, n画像側終了位置X, n画像側終了位置Y, n表示側開始位置X, n表示側開始位置Y, n表示側終了位置X, n表示側終了位置Y, n総移動時間ms, -1 );
		}
		public void Start( int nチャンネル, CDTX.CBMP bmp, CDTX.CBMPTEX bmptex, int n開始サイズW, int n開始サイズH, int n終了サイズW, int n終了サイズH, int n画像側開始位置X, int n画像側開始位置Y, int n画像側終了位置X, int n画像側終了位置Y, int n表示側開始位置X, int n表示側開始位置Y, int n表示側終了位置X, int n表示側終了位置Y, int n総移動時間ms, int n移動開始時刻ms )
		{
			for( int i = 0; i < 8; i++ )
			{
				if( nチャンネル == this.nChannel[ i ] )
				{
					this.stLayer[ i ].rBMP = bmp;
					this.stLayer[ i ].rBMPTEX = bmptex;
					this.stLayer[ i ].sz開始サイズ.Width = n開始サイズW;
					this.stLayer[ i ].sz開始サイズ.Height = n開始サイズH;
					this.stLayer[ i ].sz終了サイズ.Width = n終了サイズW;
					this.stLayer[ i ].sz終了サイズ.Height = n終了サイズH;
					this.stLayer[ i ].pt画像側開始位置.X = n画像側開始位置X;
					this.stLayer[ i ].pt画像側開始位置.Y = n画像側開始位置Y;
					this.stLayer[ i ].pt画像側終了位置.X = n画像側終了位置X;
					this.stLayer[ i ].pt画像側終了位置.Y = n画像側終了位置Y;
					this.stLayer[ i ].pt表示側開始位置.X = n表示側開始位置X * 2;
					this.stLayer[ i ].pt表示側開始位置.Y = n表示側開始位置Y * 2;
					this.stLayer[ i ].pt表示側終了位置.X = n表示側終了位置X * 2;
					this.stLayer[ i ].pt表示側終了位置.Y = n表示側終了位置Y * 2;
					this.stLayer[ i ].n総移動時間ms = n総移動時間ms;
					this.stLayer[ i ].n移動開始時刻ms = ( n移動開始時刻ms != -1 ) ? n移動開始時刻ms : CDTXMania.Timer.n現在時刻;
				}
			}
		}
		public void SkipStart( int n移動開始時刻ms )
		{
			for( int i = 0; i < CDTXMania.DTX.listChip.Count; i++ )
			{
				CDTX.CChip chip = CDTXMania.DTX.listChip[ i ];
				if( chip.n発声時刻ms > n移動開始時刻ms )
				{
					return;
				}
				switch( chip.eBGA種別 )
				{
					case EBGA種別.BMP:
						if( ( chip.rBMP != null ) && ( chip.rBMP.tx画像 != null ) )
						{
							this.Start( chip.nチャンネル番号, chip.rBMP, null, chip.rBMP.n幅, chip.rBMP.n高さ, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chip.n発声時刻ms );
						}
						break;

					case EBGA種別.BMPTEX:
						if( ( chip.rBMPTEX != null ) && ( chip.rBMPTEX.tx画像 != null ) )
						{
							this.Start( chip.nチャンネル番号, null, chip.rBMPTEX, chip.rBMPTEX.tx画像.sz画像サイズ.Width, chip.rBMPTEX.tx画像.sz画像サイズ.Height, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chip.n発声時刻ms );
						}
						break;

					case EBGA種別.BGA:
						if( chip.rBGA != null )
						{
							this.Start( chip.nチャンネル番号, chip.rBMP, chip.rBMPTEX, chip.rBGA.pt画像側右下座標.X - chip.rBGA.pt画像側左上座標.X, chip.rBGA.pt画像側右下座標.Y - chip.rBGA.pt画像側左上座標.Y, 0, 0, chip.rBGA.pt画像側左上座標.X, chip.rBGA.pt画像側左上座標.Y, 0, 0, chip.rBGA.pt表示座標.X, chip.rBGA.pt表示座標.Y, 0, 0, 0, chip.n発声時刻ms );
						}
						break;

					case EBGA種別.BGAPAN:
						if( chip.rBGAPan != null )
						{
							this.Start( chip.nチャンネル番号, chip.rBMP, chip.rBMPTEX, chip.rBGAPan.sz開始サイズ.Width, chip.rBGAPan.sz開始サイズ.Height, chip.rBGAPan.sz終了サイズ.Width, chip.rBGAPan.sz終了サイズ.Height, chip.rBGAPan.pt画像側開始位置.X, chip.rBGAPan.pt画像側開始位置.Y, chip.rBGAPan.pt画像側終了位置.X, chip.rBGAPan.pt画像側終了位置.Y, chip.rBGAPan.pt表示側開始位置.X, chip.rBGAPan.pt表示側開始位置.Y, chip.rBGAPan.pt表示側終了位置.X, chip.rBGAPan.pt表示側終了位置.Y, chip.n総移動時間, chip.n発声時刻ms );
						}
						break;
				}
			}
		}
		public void Stop()
		{
			for( int i = 0; i < 8; i++ )
			{
				this.stLayer[ i ].n移動開始時刻ms = -1;
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			for( int i = 0; i < 8; i++ )
			{
				STLAYER stlayer2 = new STLAYER();
				STLAYER stlayer = stlayer2;
				stlayer.rBMP = null;
				stlayer.rBMPTEX = null;
				stlayer.n移動開始時刻ms = -1;
				this.stLayer[ i ] = stlayer;
			}
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				using( Surface surface = CDTXMania.app.Device.GetBackBuffer( 0, 0 ) )
				{
					try
					{
						this.sfBackBuffer = Surface.CreateOffscreenPlain( CDTXMania.app.Device, surface.Description.Width, surface.Description.Height, surface.Description.Format, Pool.SystemMemory );
					}
					catch ( Exception e )
					{
						Trace.TraceError( "CAct演奏BGA: Error: ( " + e.Message + " )" );
					}
				}
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				if( this.sfBackBuffer != null )
				{
                    CDTXMania.t安全にDisposeする( ref this.sfBackBuffer );
					this.sfBackBuffer = null;
				}
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(x,y)のほうを使用してください。" );
		}
		public int t進行描画( int x, int y )
		{
			if( !base.b活性化してない )
			{
				for( int i = 0; i < 8; i++ )
				{
					if( ( ( this.stLayer[ i ].n移動開始時刻ms != -1 ) && ( ( this.stLayer[ i ].rBMP != null ) || ( this.stLayer[ i ].rBMPTEX != null ) ) ) && ( ( ( this.stLayer[ i ].rBMP == null ) || ( this.stLayer[ i ].rBMP.bUse && ( this.stLayer[ i ].rBMP.tx画像 != null ) ) ) && ( ( this.stLayer[ i ].rBMPTEX == null ) || ( this.stLayer[ i ].rBMPTEX.bUse && ( this.stLayer[ i ].rBMPTEX.tx画像 != null ) ) ) ) )
					{
						Size sizeStart = this.stLayer[ i ].sz開始サイズ;
						Size sizeEnd = this.stLayer[ i ].sz終了サイズ;
						Point ptImgStart = this.stLayer[ i ].pt画像側開始位置;
						Point ptImgEnd = this.stLayer[ i ].pt画像側終了位置;
						Point ptDispStart = this.stLayer[ i ].pt表示側開始位置;
						Point ptDispEnd = this.stLayer[ i ].pt表示側終了位置;
						long timeTotal = this.stLayer[ i ].n総移動時間ms;
						long timeMoveStart = this.stLayer[ i ].n移動開始時刻ms;
						
						if( CDTXMania.Timer.n現在時刻 < timeMoveStart )
						{
							timeMoveStart = CDTXMania.Timer.n現在時刻;
						}
						// Size size3 = new Size( 0x116, 0x163 );
						Size size表示域 = new Size( 278, 355 );
						// chnrm0 : #34192
						// 表示域を２倍に変更した。
						// x,yについては次のように変更した。
						// 338,57 => 1014+139,128 (Dr.) 139は278の半分で、GR領域の中央によせるためにすこし右側にずらした。
						// 181,50 => 682, 112 (Gt.)
						Size sizeBMP = new Size(
							( this.stLayer[ i ].rBMP != null ) ? this.stLayer[ i ].rBMP.n幅 : this.stLayer[ i ].rBMPTEX.tx画像.sz画像サイズ.Width,
							( this.stLayer[ i ].rBMP != null ) ? this.stLayer[ i ].rBMP.n高さ : this.stLayer[ i ].rBMPTEX.tx画像.sz画像サイズ.Height );

						int n再生位置 = (int) ( ( CDTXMania.Timer.n現在時刻 - timeMoveStart ) * ( ( (double) CDTXMania.ConfigIni.n演奏速度 ) / 20.0 ) );
						
						if( ( timeTotal != 0 ) && ( timeTotal < n再生位置 ) )
						{
							this.stLayer[ i ].pt画像側開始位置 = ptImgStart = ptImgEnd;
							this.stLayer[ i ].pt表示側開始位置 = ptDispStart = ptDispEnd;
							this.stLayer[ i ].sz開始サイズ = sizeStart = sizeEnd;
							this.stLayer[ i ].n総移動時間ms = timeTotal = 0;
						}
						
						Rectangle rect画像側 = new Rectangle();
						Rectangle rect表示側 = new Rectangle();
						
						if( timeTotal == 0 )
						{
							rect画像側.X = ptImgStart.X;
							rect画像側.Y = ptImgStart.Y;
							rect画像側.Width = sizeStart.Width;
							rect画像側.Height = sizeStart.Height;
							rect表示側.X = ptDispStart.X;
							rect表示側.Y = ptDispStart.Y;
							rect表示側.Width = sizeStart.Width;
							rect表示側.Height = sizeStart.Height;
						}
						else
						{
							double coefSizeWhileMoving = ( (double) n再生位置 ) / ( (double) timeTotal );
							Size sizeWhileMoving = new Size( sizeStart.Width + ( (int) ( ( sizeEnd.Width - sizeStart.Width ) * coefSizeWhileMoving ) ), sizeStart.Height + ( (int) ( ( sizeEnd.Height - sizeStart.Height ) * coefSizeWhileMoving ) ) );
							rect画像側.X = ptImgStart.X + ( (int) ( ( ptImgEnd.X - ptImgStart.X ) * coefSizeWhileMoving ) );
							rect画像側.Y = ptImgStart.Y + ( (int) ( ( ptImgEnd.Y - ptImgStart.Y ) * coefSizeWhileMoving ) );
							rect画像側.Width = sizeWhileMoving.Width;
							rect画像側.Height = sizeWhileMoving.Height;
							rect表示側.X = ptDispStart.X + ( (int) ( ( ptDispEnd.X - ptDispStart.X ) * coefSizeWhileMoving ) );
							rect表示側.Y = ptDispStart.Y + ( (int) ( ( ptDispEnd.Y - ptDispStart.Y ) * coefSizeWhileMoving ) );
							rect表示側.Width = sizeWhileMoving.Width * 2;
							rect表示側.Height = sizeWhileMoving.Height * 2;
						}
						if(
							( rect画像側.Right > 0 ) &&
							( rect画像側.Bottom > 0 ) &&
							( rect画像側.Left < sizeBMP.Width ) &&
							( rect画像側.Top < sizeBMP.Height ) && 
							( rect表示側.Right > 0 ) &&
							( rect表示側.Bottom > 0 ) &&
							( rect表示側.Left < size表示域.Width ) &&
							( rect表示側.Top < size表示域.Height )
							)
						{
							// 画像側の表示指定が画像の境界をまたいでいる場合補正
							if( rect画像側.X < 0 )
							{
								rect表示側.Width -= -rect画像側.X;
								rect表示側.X += -rect画像側.X;
								rect画像側.Width -= -rect画像側.X;
								rect画像側.X = 0;
							}
							if( rect画像側.Y < 0 )
							{
								rect表示側.Height -= -rect画像側.Y;
								rect表示側.Y += -rect画像側.Y;
								rect画像側.Height -= -rect画像側.Y;
								rect画像側.Y = 0;
							}
							if( rect画像側.Right > sizeBMP.Width )
							{
								rect表示側.Width -= rect画像側.Right - sizeBMP.Width;
								rect画像側.Width -= rect画像側.Right - sizeBMP.Width;
							}
							if( rect画像側.Bottom > sizeBMP.Height )
							{
								rect表示側.Height -= rect画像側.Bottom - sizeBMP.Height;
								rect画像側.Height -= rect画像側.Bottom - sizeBMP.Height;
							}

							// 表示側の表示指定が表示域の境界をまたいでいる場合補正
							if( rect表示側.X < 0 )
							{
								rect画像側.Width -= -rect表示側.X;
								rect画像側.X += -rect表示側.X;
								rect表示側.Width -= rect表示側.X;
								rect表示側.X = 0;
							}
							if( rect表示側.Y < 0 )
							{
								rect画像側.Height -= -rect表示側.Y;
								rect画像側.Y += -rect表示側.Y;
								rect表示側.Height -= -rect表示側.Y;
								rect表示側.Y = 0;
							}
							if( rect表示側.Right > size表示域.Width )
							{
								rect画像側.Width -= ( rect表示側.Right - size表示域.Width ) / 2;
								rect表示側.Width -= rect表示側.Right - size表示域.Width;
							}
							if( rect表示側.Bottom > size表示域.Height )
							{
								rect画像側.Height -= ( rect表示側.Bottom - size表示域.Height ) / 2;
								rect表示側.Height -= rect表示側.Bottom - size表示域.Height;
							}
							
							if(
								( rect画像側.Width > 0 ) &&
								( rect画像側.Height > 0 ) &&
								( rect表示側.Width > 0 ) && 
								( rect表示側.Height > 0 ) &&

								( rect画像側.Right >= 0 ) &&
								( rect画像側.Bottom >= 0 ) &&
								( rect画像側.Left <= sizeBMP.Width ) &&
								( rect画像側.Top <= sizeBMP.Height ) &&
								( rect表示側.Right >= 0 ) &&
								( rect表示側.Bottom >= 0 ) &&
								( rect表示側.Left <= size表示域.Width ) &&
								( rect表示側.Top <= size表示域.Height )
								)
							{
								if ( ( this.stLayer[ i ].rBMP != null ) && ( this.stLayer[ i ].rBMP.tx画像 != null ) )
								{
									if ( CDTXMania.DTX != null && !CDTXMania.DTX.bUse556x710BGAAVI )
									{
                                        //this.stLayer[i].rBMP.tx画像.vc拡大縮小倍率.X = 2.0f;
                                        //this.stLayer[i].rBMP.tx画像.vc拡大縮小倍率.Y = 2.0f;
										this.stLayer[i].rBMP.tx画像.vc拡大縮小倍率.Z = 1.0f;
									}
									this.stLayer[ i ].rBMP.tx画像.t2D描画(
										CDTXMania.app.Device,
										(x + rect表示側.X),
										(y + rect表示側.Y),
										rect画像側 );
								}
								else if( ( this.stLayer[ i ].rBMPTEX != null ) && ( this.stLayer[ i ].rBMPTEX.tx画像 != null ) )
								{
									if ( CDTXMania.DTX != null && !CDTXMania.DTX.bUse556x710BGAAVI )
									{
                                        //this.stLayer[i].rBMPTEX.tx画像.vc拡大縮小倍率.X = 2.0f;
                                        //this.stLayer[i].rBMPTEX.tx画像.vc拡大縮小倍率.Y = 2.0f;
										this.stLayer[i].rBMPTEX.tx画像.vc拡大縮小倍率.Z = 1.0f;
									}
									this.stLayer[ i ].rBMPTEX.tx画像.t2D描画(
										CDTXMania.app.Device,
										(x + rect表示側.X),
										(y + rect表示側.Y),
										rect画像側 );
								}
							}
						}
					}
				}
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct STLAYER
		{
			public CDTX.CBMP rBMP;
			public CDTX.CBMPTEX rBMPTEX;
			public Size sz開始サイズ;
			public Size sz終了サイズ;
			public Point pt画像側開始位置;
			public Point pt画像側終了位置;
			public Point pt表示側開始位置;
			public Point pt表示側終了位置;
			public long n総移動時間ms;
			public long n移動開始時刻ms;
		}

		private readonly int[] nChannel = new int[] { 4, 7, 0x55, 0x56, 0x57, 0x58, 0x59, 0x60 };
		private Surface sfBackBuffer;
		private STLAYER[] stLayer = new STLAYER[ 8 ];
		//-----------------
		#endregion
	}
}
