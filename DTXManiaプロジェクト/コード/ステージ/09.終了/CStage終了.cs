using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SharpDX;
using FDK;
using SlimDXKey = SlimDX.DirectInput.Key;

namespace DTXMania
{
	internal class CStage終了 : CStage
	{
		// コンストラクタ

		public CStage終了()
		{
			base.eステージID = CStage.Eステージ.終了;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.b活性化してない = true;
		}


		// CStage 実装

		public override void On活性化()
		{
			Trace.TraceInformation( "終了ステージを活性化します。" );
			Trace.Indent();
			try
			{
				this.ct時間稼ぎ = new CCounter();
				base.On活性化();
			}
			finally
			{
				Trace.TraceInformation( "終了ステージの活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void On非活性化()
		{
			Trace.TraceInformation( "終了ステージを非活性化します。" );
			Trace.Indent();
			try
			{
				base.On非活性化();
			}
			finally
			{
				Trace.TraceInformation( "終了ステージの非活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.tx文字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\9_text.png" ) );
                this.tx文字2 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\9_text.png" ) );
                this.tx文字3 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\9_text.png" ) );
				this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\9_background.jpg" ), false );
                this.tx白 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ), false );
                this.ds背景 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する( CSkin.Path( @"Graphics\9_background.mp4" ), CDTXMania.app.WindowHandle, true );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx背景 );
                CDTXMania.tテクスチャの解放( ref this.tx文字 );
                CDTXMania.tテクスチャの解放( ref this.tx文字2 );
                CDTXMania.tテクスチャの解放( ref this.tx文字3 );
                CDTXMania.tテクスチャの解放( ref this.tx白 );
                CDTXMania.t安全にDisposeする( ref this.ds背景 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
            if( this.ds背景 != null )
            {
                this.ds背景.t再生開始();
                
                this.ds背景.t現時点における最新のスナップイメージをTextureに転写する( this.tx背景 );
            }
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
					CDTXMania.Skin.soundゲーム終了音.t再生する();
					this.ct時間稼ぎ.t開始( 0, 5000, 1, CDTXMania.Timer );
                    base.b初めての進行描画 = false;
				}


				this.ct時間稼ぎ.t進行();

				if( this.tx背景 != null )
				{
                    if( this.ds背景 != null )
                    {
                        if( this.ds背景.b上下反転 )
                            this.tx背景.t2D上下反転描画( CDTXMania.app.Device, 0, 0 );
                        else
                            this.tx背景.t2D描画( CDTXMania.app.Device, 0, 0 );
                    }
                    else
					    this.tx背景.t2D描画( CDTXMania.app.Device, 0, 0 );
				}

                //this.ct時間稼ぎ.n現在の値 = offset;
                //offset = this.ct時間稼ぎ.n現在の値;

                if( this.ct時間稼ぎ.n現在の値 < 2000 )
                {
                    if( this.tx文字 != null )
                    {
                        // 2019.04.06 kairera0467 作り直し。Z座標完全無視の荒業
                        Matrix mat = Matrix.Identity;
                        Matrix mat2 = Matrix.Identity;

                        mat *= Matrix.Scaling( 3.0f, 3.5f, 1.0f );
                        mat *= Matrix.RotationX( C変換.DegreeToRadian( -5 ) );
                        mat *= Matrix.RotationY( C変換.DegreeToRadian( 5 ) );
                        mat *= Matrix.RotationZ( C変換.DegreeToRadian( -56 ) );
                        mat *= Matrix.Translation( 360 - ( 1.40f * (this.ct時間稼ぎ.n現在の値 - 300)), -800 + ( 2.05f * (this.ct時間稼ぎ.n現在の値 - 300)), 0 );

                        mat2 *= Matrix.Scaling( 2.0f, 2.0f, 1.0f );
                        mat2 *= Matrix.RotationX( C変換.DegreeToRadian( 20 ) );
                        mat2 *= Matrix.RotationZ( C変換.DegreeToRadian( -72 + ( -50f * ( (this.ct時間稼ぎ.n現在の値 - 300) / 1280.0f ) ) ) );
                        mat2 *= Matrix.Translation( -790 + ( 1.135f * this.ct時間稼ぎ.n現在の値 - 300 ), -920 + ( 1.44f * this.ct時間稼ぎ.n現在の値 - 300 ), 0 );

                        this.tx文字2.t3D描画( CDTXMania.app.Device, mat, new System.Drawing.Rectangle( 0, 0, 620, 92 ) );
                        this.tx文字3.t3D描画( CDTXMania.app.Device, mat2, new System.Drawing.Rectangle( 0, 92, 620, 94 ) );                   

                        //this.tx文字2.fZ軸中心回転 = -0.8f;
                        //this.tx文字2.fZ軸中心回転 = rot;
                        //this.tx文字3.fZ軸中心回転 = ( -1.6f * ( this.ct時間稼ぎ.n現在の値 / 1280.0f ) ) >= -1.6f ? ( -1.6f * ( this.ct時間稼ぎ.n現在の値 / 1280.0f ) ) : -1.6f ;
                        //this.tx文字2.vc拡大縮小倍率 = new Vector3( fScaleX, fScaleX, 1.0f );
                        //this.tx文字3.vc拡大縮小倍率 = new Vector3( 4.0f, 4.0f, 1.0f );

                        //this.tx文字2.t2D描画( CDTXMania.app.Device, (int)fX - ( 1.30f * this.ct時間稼ぎ.n現在の値), fY - ( 1.6f * this.ct時間稼ぎ.n現在の値), new System.Drawing.Rectangle( 0, 0, 620, 92 )  );
                        //this.tx文字3.t2D描画( CDTXMania.app.Device, -250 + ( 1.10f * this.ct時間稼ぎ.n現在の値), 1600 - ( 1.6f * this.ct時間稼ぎ.n現在の値), new System.Drawing.Rectangle( 0, 92, 620, 94 )  );
                    }
                }
                else
                {

                    if( this.tx文字 != null )
                    {
                        this.tx文字2.fZ軸中心回転 = 0f;
                        this.tx文字3.fZ軸中心回転 = 0f;
                        this.tx文字2.vc拡大縮小倍率 = new Vector3( 1.3f, 1.3f, 1.0f );
                        this.tx文字3.vc拡大縮小倍率 = new Vector3( 1.3f, 1.3f, 1.0f );

                        this.tx文字2.t2D描画( CDTXMania.app.Device, 480, 376, new System.Drawing.Rectangle( 0, 0, 620, 92 )  );
                        this.tx文字3.t2D描画( CDTXMania.app.Device, 500, 486, new System.Drawing.Rectangle( 0, 92, 620, 95 )  );
                        this.tx文字.t2D描画( CDTXMania.app.Device, 662, 613, new System.Drawing.Rectangle( 0, 187, 620, 44 )  );
                    }

                    if( this.tx白 != null )
			        {
				        this.tx白.n透明度 = ( 2255 + 300 ) - ( this.ct時間稼ぎ.n現在の値 );
				        for( int i = 0; i <= ( SampleFramework.GameWindowSize.Width / 64 ); i++ )
				        {
					        for( int j = 0; j <= ( SampleFramework.GameWindowSize.Height / 64 ); j++ )
					        {
                                this.tx白.t2D描画( CDTXMania.app.Device, i * 64, j * 64 );
					        }
				        }
			        }
                }

                if( this.ct時間稼ぎ.b終了値に達した && !CDTXMania.Skin.soundゲーム終了音.b再生中 )
				{
					return 1;
				}


#if DEBUG
                //CDTXMania.act文字コンソール.tPrint(0, 720 - 16, C文字コンソール.Eフォント種別.白, "F1,F2 X1  F3,F4 Y1  F6,F7 rot  F8,F9 ScaleX0.1  F10,F11 ScaleX1.0  D1,D2 ScaleY0.1  D3,D4 ScaleY1.0  D7,D8 X10  D9,D10 Y10");
                //CDTXMania.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, "RotZ:" + rot.ToString());
                //CDTXMania.act文字コンソール.tPrint(0, 16, C文字コンソール.Eフォント種別.白, "PanelX:" + fX.ToString());
                //CDTXMania.act文字コンソール.tPrint(0, 32, C文字コンソール.Eフォント種別.白, "PanelY:" + fY.ToString());
                //CDTXMania.act文字コンソール.tPrint(0, 48, C文字コンソール.Eフォント種別.白, "ScaleX:" + fScaleX.ToString());
                //CDTXMania.act文字コンソール.tPrint( 0, 64, C文字コンソール.Eフォント種別.白, "ScaleY:" + fScaleY.ToString() );
                //CDTXMania.act文字コンソール.tPrint( 0, 80, C文字コンソール.Eフォント種別.白, "OFFSET:" + offset.ToString() );
                ////CDTXMania.act文字コンソール.tPrint( 0, 96, C文字コンソール.Eフォント種別.白, "PanelZ:" + fZ.ToString() );

                //if ( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F1 ) )
                //{
                //    fX--;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F2 ) )
                //{
                //    fX++;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F3 ) )
                //{
                //    fY--;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F4 ) )
                //{
                //    fY++;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F6 ) )
                //{
                //    rot--;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F7 ) )
                //{
                //    rot++;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F8 ) )
                //{
                //    fScaleX -= 0.1f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F9 ) )
                //{
                //    fScaleX += 0.1f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F10 ) )
                //{
                //    fScaleX -= 1.0f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F11 ) )
                //{
                //    fScaleX += 1.0f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D1 ) )
                //{
                //    fScaleY -= 0.1f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D2 ) )
                //{
                //    fScaleY += 0.1f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D3 ) )
                //{
                //    fScaleY -= 1f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D4 ) )
                //{
                //    fScaleY += 1f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D5 ) )
                //{
                //    offset -= 50;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D6 ) )
                //{
                //    offset += 50;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D7 ) )
                //{
                //    fX -= 10;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D8 ) )
                //{
                //    fX += 10;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D9 ) )
                //{
                //    fY -= 10;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D0 ) )
                //{
                //    fY += 10;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.Q ) )
                //{
                //    fZ -= 1;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.W ) )
                //{
                //    fZ += 1;
                //}
                //if ( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.E ) )
                //{
                //    fZ -= 10;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.R ) )
                //{
                //    fZ += 10;
                //}
#endif
            }
            return 0;
		}


		// その他

#region [ private ]
		//-----------------
		private CCounter ct時間稼ぎ;
		private CTexture tx背景;
        private CTexture tx文字;
        private CTexture tx文字2;
        private CTexture tx文字3;
        private CDirectShow ds背景;
        private CTexture tx白;

#if DEBUG
        // 座標テスト用
        private float fX;
        private float fY;
        private float fZ;
        private int rot;
        private float fScaleX;
        private float fScaleY;
        private int offset;
#endif
        //-----------------
#endregion
    }
}