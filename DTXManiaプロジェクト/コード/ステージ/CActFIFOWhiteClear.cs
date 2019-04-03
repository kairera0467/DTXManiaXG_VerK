using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using SharpDX;
using FDK;

using Rectangle = System.Drawing.Rectangle;
namespace DTXMania
{
	internal class CActFIFOWhiteClear : CActivity
	{
		// メソッド

		public void tフェードアウト開始()
		{
			this.mode = EFIFOモード.フェードアウト;
			this.counter = new CCounter( 0, 400, 5, CDTXMania.Timer );
            this.ctFCEXC = new CCounter( 0, 255, 2, CDTXMania.Timer );
            this.ctFCEXCText = new CCounter( 0, 70, 1, CDTXMania.Timer );
		}
		public void tフェードイン開始()
		{
			this.mode = EFIFOモード.フェードイン;
			this.counter = new CCounter( 0, 400, 5, CDTXMania.Timer );
		}
		public void tフェードイン完了()		// #25406 2011.6.9 yyagi
		{
			this.counter.n現在の値 = this.counter.n終了値;
		}
        public void Start()
        {
            if ((this.txボーナス花火 != null))
            {
                for (int i = 0; i < 256; i++)
                {
                    for (int j = 0; j < 256; j++)
                    {
                        if (!this.st青い星[j].b使用中)
                        {
                            this.st青い星[j].b使用中 = true;
                            int n回転初期値 = CDTXMania.Random.Next(360);
                            double num7 = 2.5 + (((double)CDTXMania.Random.Next(40)) / 100.0);
                            this.st青い星[j].ct進行 = new CCounter(0, 100, 20, CDTXMania.Timer);
                            this.st青い星[j].fX = 600; //X座標

                            this.st青い星[j].fY = 350; //Y座標
                            this.st青い星[j].f加速度X = (float)(num7 * Math.Cos((Math.PI * 2 * n回転初期値) / 360.0));
                            this.st青い星[j].f加速度Y = (float)(num7 * (Math.Sin((Math.PI * 2 * n回転初期値) / 360.0) - 0.2));
                            this.st青い星[j].f加速度の加速度X = 0.995f;
                            this.st青い星[j].f加速度の加速度Y = 0.995f;
                            this.st青い星[j].f重力加速度 = 0.00355f;
                            this.st青い星[j].f半径 = (float)(0.5 + (((double)CDTXMania.Random.Next(30)) / 100.0));
                            break;
                        }
                    }
                }
            }
        }

		// CActivity 実装
        public override void On活性化()
        {
            this.ds背景動画 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する(CSkin.Path(@"Graphics\7_StageClear.mp4"), CDTXMania.app.WindowHandle, true);
            if(this.ds背景動画 != null)
                Trace.TraceInformation("StageClear動画をDirectShowで生成しました。");
            base.On活性化();
        }
		public override void On非活性化()
		{
			if( !base.b活性化してない )
			{
                for (int i = 0; i < 256; i++)
                {
                    this.st青い星[i].ct進行 = null;
                }
				base.On非活性化();
			}
		}

		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                
				this.tx白タイル64x64 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ), false );
                this.txリザルト画像 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\8_background.jpg"), false );
                this.txFullCombo = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_FullCombo.png"));
                this.txExcellent = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Excellent.png"));
                this.tx黒幕 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\Tile black 64x64.png"), false );
                this.txFCEXCtext = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\7_FCEXC_text.png" ) ); //EXC:410px、FC:386px
                this.txZoomEffect = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\7_FCEXC_zoom.png" ) );

                this.txボーナス花火 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenPlayDrums chip star.png"));
                if (this.txボーナス花火 != null)
                {
                    this.txボーナス花火.b加算合成 = true;
                }
                if(this.ds背景動画 != null)
                    this.tx描画用 = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, SharpDX.Direct3D9.Pool.Managed );
                //this.tx背景動画 = new CTexture(CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);

                for (int i = 0; i < 256; i++)
                {
                    this.st青い星[i] = new ST青い星();
                    this.st青い星[i].b使用中 = false;
                    this.st青い星[i].ct進行 = new CCounter();
                }

				base.OnManagedリソースの作成();
			}
		}
        
        
        public override void OnManagedリソースの解放()
        {
            if (this.b活性化してない)
                return;
            if (this.tx描画用 != null)
            {
                this.tx描画用.Dispose();
                this.tx描画用 = null;
            }
            if( this.ds背景動画 != null )
            {
                this.ds背景動画.Dispose();
                this.ds背景動画 = null;
            }
            CDTXMania.tテクスチャの解放( ref this.txボーナス花火 );
            CDTXMania.tテクスチャの解放( ref this.tx白タイル64x64 );
            CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
            CDTXMania.tテクスチャの解放( ref this.txFullCombo );
            CDTXMania.tテクスチャの解放( ref this.txExcellent );
            CDTXMania.tテクスチャの解放( ref this.tx黒幕 );
            CDTXMania.tテクスチャの解放( ref this.txFCEXCtext );
            CDTXMania.tテクスチャの解放( ref this.txZoomEffect );

            base.OnManagedリソースの解放();
        }
        
		public override unsafe int On進行描画()
        {
            if (base.b活性化してない || (this.counter == null))
            {
                return 0;
            }
            this.counter.t進行();
            this.ctFCEXC.t進行();
            this.ctFCEXCText.t進行();
            bool bドラムフルコン = false;
            bool bドラムエクセ = false;

            if( CDTXMania.ConfigIni.bDrums有効 == true )
            {
                if( CDTXMania.ConfigIni.bドラムが全部オートプレイである )
                {
                    if( CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Poor == 0 )
                        bドラムエクセ = true;
                }
                else
                {
                    if( CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Poor == 0 )
                        bドラムフルコン = true;
                    if( CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect == CDTXMania.DTX.n可視チップ数.Drums )
                        bドラムエクセ = true;
                }



                if( bドラムエクセ )
                {
                    this.tx黒幕.n透明度 = 200;
                    for( int i = 0; i <= ( SampleFramework.GameWindowSize.Width / 64 ); i++ )
                    {
                        for( int j = 0; j <= ( SampleFramework.GameWindowSize.Height / 64 ); j++ )
                        {
                            this.tx黒幕.t2D描画( CDTXMania.app.Device, i * 64, j * 64 );
                        }
                    }
                    this.txZoomEffect.b加算合成 = true;
                    this.txZoomEffect.t2D描画( CDTXMania.app.Device, 126, 233, new Rectangle( 0, 0, 1024, 256 ) );
                    this.txZoomEffect.n透明度 = 255 - ctFCEXC.n現在の値;
                    #region[ 粉エフェクト ]
                    for (int i = 0; i < 256; i++)
                    {
                        if (this.st青い星[i].b使用中)
                        {
                            this.st青い星[i].n前回のValue = this.st青い星[i].ct進行.n現在の値;
                            this.st青い星[i].ct進行.t進行();
                            if (this.st青い星[i].ct進行.b終了値に達した)
                                    {
                                        this.st青い星[ i ].b使用中 = false;
                                        this.st青い星[i].ct進行.t停止();
                                    }
                                    for (int n = this.st青い星[i].n前回のValue; n < this.st青い星[i].ct進行.n現在の値; n++)
                                    {
                                        this.st青い星[i].fX += this.st青い星[i].f加速度X;
                                        this.st青い星[i].fY -= this.st青い星[i].f加速度Y;
                                        this.st青い星[i].f加速度X *= this.st青い星[i].f加速度の加速度X;
                                        this.st青い星[i].f加速度Y *= this.st青い星[i].f加速度の加速度Y;
                                        //this.st青い星[i].f加速度Y -= this.st青い星[i].f重力加速度;
                                    }
                                    Matrix mat = Matrix.Identity;

                                    float x = (float)(this.st青い星[i].f半径 * Math.Cos((Math.PI / 2 * this.st青い星[i].ct進行.n現在の値) / 100.0));
                                    mat *= Matrix.Scaling(x, x, 1f);
                                    mat *= Matrix.Translation(this.st青い星[i].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st青い星[i].fY - SampleFramework.GameWindowSize.Height / 2), 0f);

                                    if (this.txボーナス花火 != null)
                                    {
                                        this.txボーナス花火.t3D描画(CDTXMania.app.Device, mat);
                                    }
                                }
                    }
                    this.Start();
                    #endregion
                    Matrix matExc = Matrix.Identity;

                    int num = this.ctFCEXCText.n現在の値;
                    float num1 = 3.0f - ( ( float )( 2.0f * ( num / 70.0f ) ) );

                    matExc *= Matrix.Scaling( num1, num1, num1 );
                    matExc *= Matrix.Translation( 0f, 0f, 0f );
                    this.txFCEXCtext.t3D描画( CDTXMania.app.Device, matExc, new Rectangle( 0, 0, 410, 73 ) );
                }
                else if( bドラムフルコン )
                {
                    this.tx黒幕.n透明度 = 200;
                    for( int i = 0; i <= ( SampleFramework.GameWindowSize.Width / 64 ); i++ )
                    {
                        for( int j = 0; j <= ( SampleFramework.GameWindowSize.Height / 64 ); j++ )
                        {
                            this.tx黒幕.t2D描画( CDTXMania.app.Device, i * 64, j * 64 );
                        }
                    }
                    this.txZoomEffect.t2D描画( CDTXMania.app.Device, 126, 233, new Rectangle( 0, 256, 1024, 256 ) );
                    this.txZoomEffect.b加算合成 = true;
                    this.txZoomEffect.n透明度 = 255 - ctFCEXC.n現在の値;
                    #region[ 粉エフェクト ]
                    for (int i = 0; i < 240; i++)
                            {
                                if (this.st青い星[i].b使用中)
                                {
                                    this.st青い星[i].n前回のValue = this.st青い星[i].ct進行.n現在の値;
                                    this.st青い星[i].ct進行.t進行();
                                    if (this.st青い星[i].ct進行.b終了値に達した)
                                    {
                                        this.st青い星[ i ].b使用中 = false;
                                        this.st青い星[i].ct進行.t停止();
                                    }
                                    for (int n = this.st青い星[i].n前回のValue; n < this.st青い星[i].ct進行.n現在の値; n++)
                                    {
                                        this.st青い星[i].fX += this.st青い星[i].f加速度X;
                                        this.st青い星[i].fY -= this.st青い星[i].f加速度Y;
                                        this.st青い星[i].f加速度X *= this.st青い星[i].f加速度の加速度X;
                                        this.st青い星[i].f加速度Y *= this.st青い星[i].f加速度の加速度Y;
                                        //this.st青い星[i].f加速度Y -= this.st青い星[i].f重力加速度;
                                    }
                                    Matrix mat = Matrix.Identity;

                                    float x = (float)(this.st青い星[i].f半径 * Math.Cos((Math.PI / 2 * this.st青い星[i].ct進行.n現在の値) / 100.0));
                                    mat *= Matrix.Scaling(x, x, 1f);
                                    mat *= Matrix.Translation(this.st青い星[i].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st青い星[i].fY - SampleFramework.GameWindowSize.Height / 2), 0f);

                                    if (this.txボーナス花火 != null)
                                    {
                                        this.txボーナス花火.t3D描画(CDTXMania.app.Device, mat);
                                    }
                                }
                            }
                            this.Start();
                    #endregion
                    Matrix matFc = Matrix.Identity;

                    int num = this.ctFCEXCText.n現在の値;
                    float num1 = 3.0f - ( ( float )( 2.0f * ( num / 70.0f ) ) );

                    matFc *= Matrix.Scaling( num1, num1, num1 );
                    matFc *= Matrix.Translation( 0f, 0f, 0f );
                    this.txFCEXCtext.t3D描画( CDTXMania.app.Device, matFc, new Rectangle( 0, 73, 410, 73 ) );
                }
            }
            if (this.counter.n現在の値 >= 300)
            {
                if (this.ds背景動画 != null)
                {
                    this.ds背景動画.bループ再生 = false;
                    this.ds背景動画.t再生開始();
                }
                int x = 0;
                int y = 0;

                if( this.ds背景動画 != null )
                {
                    this.ds背景動画.t現時点における最新のスナップイメージをTextureに転写する(this.tx描画用);
                    //if (this.ds背景動画.b上下反転)
                    //    this.tx描画用.t2D上下反転描画(CDTXMania.app.Device, 0, 0);
                    //else
                    //    this.tx描画用.t2D描画(CDTXMania.app.Device, 0, 0);
                    this.tx描画用.bFlipY = this.ds背景動画.b上下反転;
                    this.tx描画用.t2D描画(CDTXMania.app.Device, 0, 0);

                    if( this.counter.n現在の値 != 400 || !this.ds背景動画.b再生が完了した() )
                    {
                        return 0;
                    }
                }

                if( this.ds背景動画 == null)
                {
                    if (this.tx白タイル64x64 != null)
                    {
                        if (this.counter.n現在の値 <= 300)
                        {
                            this.tx白タイル64x64.n透明度 = 0;
                        }
                        else
                        {
                            this.tx白タイル64x64.n透明度 = (this.mode == EFIFOモード.フェードイン) ? (((100 - (this.counter.n現在の値 - 300)) * 0xff) / 100) : (((this.counter.n現在の値 - 300) * 255) / 100);
                        }
                        for (int i = 0; i <= (SampleFramework.GameWindowSize.Width / 64); i++)		// #23510 2010.10.31 yyagi: change "clientSize.Width" to "640" to fix FIFO drawing size
                        {
                            for (int j = 0; j <= (SampleFramework.GameWindowSize.Height / 64); j++)	// #23510 2010.10.31 yyagi: change "clientSize.Height" to "480" to fix FIFO drawing size
                            {
                                this.tx白タイル64x64.t2D描画(CDTXMania.app.Device, i * 64, j * 64);
                            }
                        }
                    }
                    if (this.counter.n現在の値 != 400)
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }
        


		// その他

		#region [ private ]
		//-----------------
		public CCounter counter;
        public CCounter ctFCEXC;
        public CCounter ctFCEXCText;
		private EFIFOモード mode;
		private CTexture tx白タイル64x64;
        private CTexture txFullCombo;
        private CTexture txExcellent;
        private CTexture txFCEXCtext;
        private CTexture txZoomEffect;
        private CTexture tx黒幕;
        private CTexture tx描画用;
        private CTexture txボーナス花火;
        private CTexture txリザルト画像;
        protected volatile CDirectShow ds背景動画 = null;

        [StructLayout(LayoutKind.Sequential)]
        private struct ST青い星
        {
            public int nLane;
            public bool b使用中;
            public CCounter ct進行;
            public int n前回のValue;
            public float fX;
            public float fY;
            public float f加速度X;
            public float f加速度Y;
            public float f加速度の加速度X;
            public float f加速度の加速度Y;
            public float f重力加速度;
            public float f半径;
        }
        private ST青い星[] st青い星 = new ST青い星[256];
        #endregion
	}
}
