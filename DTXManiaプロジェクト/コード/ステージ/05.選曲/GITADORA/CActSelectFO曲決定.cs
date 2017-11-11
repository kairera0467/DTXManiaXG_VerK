using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CActSelectFO曲決定 : CActivity
	{
		// メソッド

		public void tフェードアウト開始()
		{
			this.mode = EFIFOモード.フェードアウト;
			this.counter = new CCounter( 0, 100, 5, CDTXMania.Timer );
		}
		public void tフェードイン開始()
		{
			this.mode = EFIFOモード.フェードイン;
			this.counter = new CCounter( 0, 100, 5, CDTXMania.Timer );
		}
		public void tフェードイン完了()		// #25406 2011.6.9 yyagi
		{
			this.counter.n現在の値 = this.counter.n終了値;
		}

		// CActivity 実装

		public override void On非活性化()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx白タイル64x64 );

                CDTXMania.tテクスチャの解放( ref this.tx水色 );
                CDTXMania.tテクスチャの解放( ref this.tx黒 );
                CDTXMania.tテクスチャの解放( ref this.tx青色 );
                CDTXMania.tテクスチャの解放( ref this.tx群青 );
				base.On非活性化();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.tx白タイル64x64 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ), false );

                this.tx水色 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile lightblue.png" ) );
                this.tx黒 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile black.png" ) );
                this.tx青色 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile blue.png" ) );
                this.tx群青 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile darkblue.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない || ( this.counter == null ) )
			{
				return 0;
			}
			//this.counter.t進行();
            
			if (this.tx白タイル64x64 != null)
			{
				this.tx白タイル64x64.n透明度 = ( this.mode == EFIFOモード.フェードイン ) ? ( ( ( 100 - this.counter.n現在の値 ) * 0xff ) / 100 ) : ( ( this.counter.n現在の値 * 0xff ) / 100 );
				for (int i = 0; i <= (SampleFramework.GameWindowSize.Width / 64); i++)		// #23510 2010.10.31 yyagi: change "clientSize.Width" to "640" to fix FIFO drawing size
				{
					for (int j = 0; j <= (SampleFramework.GameWindowSize.Height / 64); j++)	// #23510 2010.10.31 yyagi: change "clientSize.Height" to "480" to fix FIFO drawing size
					{
						//this.tx白タイル64x64.t2D描画( CDTXMania.app.Device, i * 64, j * 64 );
					}
				}
			}


			if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.F3 ) )
			{
                this.counter.n現在の値--;
			}
			if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.F4 ) )
			{
                this.counter.n現在の値++;
			}
			if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.F5 ) )
			{
                this.counter.n現在の値 = 0;
			}
			if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.F6 ) )
			{
                this.counter.n現在の値 = 50;
			}
            
            if( this.tx水色 != null && this.tx群青 != null && this.tx青色 != null && this.tx黒 != null )
            {
                //横方向(90):304、斜め方向(45 or -45):427
                if( this.mode == EFIFOモード.フェードアウト )
                {
                    //14 群青(右)
                    this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx群青.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 88 ? ( 703 - ( ( ( this.counter.n現在の値 - 68 ) * 302 ) / 20 ) ) : 401, 206 ); //最終:401

                    //13 青(左)
                    this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx青色.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 86 ? ( -223 + ( ( ( this.counter.n現在の値 - 66 ) * 302 ) / 20 ) ) : 79, 206 ); //最終:79

                    //12 水色(左上)
                    this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx水色.t2D描画( CDTXMania.app.Device, 0, this.counter.n現在の値 <= 84 ? ( -256 + ( ( ( this.counter.n現在の値 - 64 ) * 304 ) / 20 ) ) : 48 ); //最終:48

                    //11 黒(左下)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, -10, this.counter.n現在の値 <= 80 ? ( 726 - ( ( ( this.counter.n現在の値 - 60 ) * 304 ) / 20 ) ) : 422 ); //最終:422

                    //10 青(上)
                    this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 0 );
                    this.tx青色.t2D描画( CDTXMania.app.Device, 0, this.counter.n現在の値 <= 69 ? ( -528 + ( ( ( this.counter.n現在の値 - 49 ) * 304 ) / 20 ) ) : -224 ); //最終:-224

                    //9 黒(下)
                    this.tx黒.fZ軸中心回転 = 0;
                    this.tx黒.t2D描画( CDTXMania.app.Device, 300, this.counter.n現在の値 <= 68 ? ( 913 - ( ( ( this.counter.n現在の値 - 48 ) * 304 ) / 20 ) ) : 609 ); //最終:609

                    //8 黒(右上)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 66 ? ( 892 - ( ( ( this.counter.n現在の値 - 46 ) * 427 ) / 20 ) ) : 465, -23 ); //最終:465

                    //7 水色(右下)
                    this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx水色.t2D描画( CDTXMania.app.Device, 646, this.counter.n現在の値 <= 61 ? ( 843 - ( ( ( this.counter.n現在の値 - 41 ) * 427 ) / 20 ) ) : 416 ); //最終:416

                    //6 黒(右)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 55 ? ( 1124 - ( ( ( this.counter.n現在の値 - 35 ) * 304 ) / 20 ) ) : 820, 206 ); //1079 最終:820

                    //5 群青(左)
                    this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx群青.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 48 ? ( -527 + ( ( ( this.counter.n現在の値 - 28 ) * 304 ) / 20 ) ) : -223, 196 ); //-527 最終:-223

                    //4 青(右下)
                    this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx青色.t2D描画( CDTXMania.app.Device, 836, this.counter.n現在の値 <= 41 ? ( 866 - ( ( ( this.counter.n現在の値 - 21 ) * 427 ) / 20 ) ) : 422 ); //最終座標:422

                    //3 黒(左上)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, -309, this.counter.n現在の値 <= 34 ? ( -457 + ( ( ( this.counter.n現在の値 - 14 ) * 427 ) / 20 ) ) : -23 ); //最終座標:-23

                    //2 群青(右上)
                    this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx群青.t2D描画( CDTXMania.app.Device, 854, this.counter.n現在の値 <= 27 ? ( -457 + ( ( (this.counter.n現在の値 - 7 ) * 427 ) / 20 ) ) : -24 ); //-457 最終座標:-24
                
                    //1 水色(左下)
                    this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx水色.t2D描画( CDTXMania.app.Device, -306, this.counter.n現在の値 <= 20 ? ( 843 - ( ( this.counter.n現在の値 * 427 ) / 20 ) ) : 416 ); //427 最終座標:416
                }
                else
                {
                    //14 群青(右)
                    this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx群青.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 88 ? ( 703 - ( ( ( this.counter.n現在の値 - 68 ) * 302 ) / 20 ) ) : 401, 206 ); //最終:401

                    //13 青(左)
                    this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx青色.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 86 ? ( -223 + ( ( ( this.counter.n現在の値 - 66 ) * 302 ) / 20 ) ) : 79, 206 ); //最終:79

                    //12 水色(左上)
                    this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx水色.t2D描画( CDTXMania.app.Device, 0, this.counter.n現在の値 <= 84 ? ( -256 + ( ( ( this.counter.n現在の値 - 64 ) * 304 ) / 20 ) ) : 48 ); //最終:48

                    //11 黒(左下)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, -10, this.counter.n現在の値 <= 80 ? ( 726 - ( ( ( this.counter.n現在の値 - 60 ) * 304 ) / 20 ) ) : 422 ); //最終:422

                    //10 青(上)
                    this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 0 );
                    this.tx青色.t2D描画( CDTXMania.app.Device, 0, this.counter.n現在の値 <= 69 ? ( -528 + ( ( ( this.counter.n現在の値 - 49 ) * 304 ) / 20 ) ) : -224 ); //最終:-224

                    //9 黒(下)
                    this.tx黒.fZ軸中心回転 = 0;
                    this.tx黒.t2D描画( CDTXMania.app.Device, 300, this.counter.n現在の値 <= 68 ? ( 913 - ( ( ( this.counter.n現在の値 - 48 ) * 304 ) / 20 ) ) : 609 ); //最終:609

                    //8 黒(右上)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 66 ? ( 892 - ( ( ( this.counter.n現在の値 - 46 ) * 427 ) / 20 ) ) : 465, -23 ); //最終:465

                    //7 水色(右下)
                    this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx水色.t2D描画( CDTXMania.app.Device, 646, this.counter.n現在の値 <= 61 ? ( 843 - ( ( ( this.counter.n現在の値 - 41 ) * 427 ) / 20 ) ) : 416 ); //最終:416

                    //6 黒(右)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 55 ? ( 1124 - ( ( ( this.counter.n現在の値 - 35 ) * 304 ) / 20 ) ) : 820, 206 ); //1079 最終:820

                    //5 群青(左)
                    this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                    this.tx群青.t2D描画( CDTXMania.app.Device, this.counter.n現在の値 <= 48 ? ( -527 + ( ( ( this.counter.n現在の値 - 28 ) * 304 ) / 20 ) ) : -223, 196 ); //-527 最終:-223

                    //4 青(右下)
                    this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx青色.t2D描画( CDTXMania.app.Device, 836, this.counter.n現在の値 <= 41 ? ( 866 - ( ( ( this.counter.n現在の値 - 21 ) * 427 ) / 20 ) ) : 422 ); //最終座標:422

                    //3 黒(左上)
                    this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                    this.tx黒.t2D描画( CDTXMania.app.Device, -309, this.counter.n現在の値 <= 34 ? ( -457 + ( ( ( this.counter.n現在の値 - 14 ) * 427 ) / 20 ) ) : -23 ); //最終座標:-23

                    //2 群青(右上)
                    this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx群青.t2D描画( CDTXMania.app.Device, 854, this.counter.n現在の値 <= 27 ? ( -457 + ( ( (this.counter.n現在の値 - 7 ) * 427 ) / 20 ) ) : -24 ); //-457 最終座標:-24
                
                    //1 水色(左下)
                    this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                    this.tx水色.t2D描画( CDTXMania.app.Device, -306, this.counter.n現在の値 <= 20 ? ( 843 - ( ( this.counter.n現在の値 * 427 ) / 20 ) ) : 416 ); //427 最終座標:416
                }

            }
            this.counter.t進行();

            CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.白, this.counter.n現在の値.ToString() );

            if ( this.counter.n現在の値 != 100 )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter counter;
		private EFIFOモード mode;
		private CTexture tx白タイル64x64;
        private CTexture txロゴ;
        //-----------------
        #endregion

        #region[ 図形描画用 ]
        CTexture tx水色;
        CTexture tx青色;
        CTexture tx群青;
        CTexture tx黒;
        #endregion
    }
}
