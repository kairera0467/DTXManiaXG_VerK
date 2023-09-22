using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarゲージ : CAct演奏ゲージ共通
	{
		// プロパティ

//		public STDGBVALUE<double> db現在のゲージ値;


		// コンストラクタ

		public CAct演奏Guitarゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
			// CAct演奏ゲージ共通.Init()に移動
//			this.db現在のゲージ値.Guitar = ( CDTXMania.ConfigIni.nRisky > 0 ) ? 1.0 : 0.66666666666666663;
//			this.db現在のゲージ値.Bass   = ( CDTXMania.ConfigIni.nRisky > 0 ) ? 1.0 : 0.66666666666666663;
            this.ctマスクFIFO = new CCounter( 0, 1500, 2, CDTXMania.Timer );
			base.On活性化();
		}
		public override void On非活性化()
		{
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				base.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Gauge.png" ) );

                this.nゲージX = (int)( (float)( 352 - 320 ) / 2f );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txゲージ背景 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                this.ctマスクFIFO.t進行Loop();
                if( base.txゲージ != null )
                {
                    int[] nゲージ本体 = new int[] { 0, 0 };
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ) nゲージ本体 = new int[]{ 65, 861 };
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B ) nゲージ本体 = new int[]{ -2, 931 };
                    for( int i = 0; i < 2; i++ )
                    {
                        if( CDTXMania.DTX.bチップがある[ i + 1 ] )
                        {
                            base.txゲージ.b加算合成 = false;
                            base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                            base.txゲージ.n透明度 = 255;

                            base.txゲージ.t2D描画( CDTXMania.app.Device, nゲージ本体[ i ], 0, new Rectangle( 2, 51, 352, 42 ) );

                            if( base.db現在のゲージ値[ i + 1 ] == 1.0 )
                            {
                                base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                                base.txゲージ.t2D描画( CDTXMania.app.Device, this.nゲージX + nゲージ本体[ i ], 8, new Rectangle( 2, 176, 320, 26 ) );
                            }
                            else if( base.db現在のゲージ値[ i + 1 ] >= 0.0 )
                            {
                                base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値[ i + 1 ];
                                base.txゲージ.t2D描画( CDTXMania.app.Device, this.nゲージX + nゲージ本体[ i ], 8, new Rectangle( 2, 176, 320, 26 ) );
                            }
                            base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                            base.txゲージ.t2D描画( CDTXMania.app.Device, nゲージ本体[ i ] - 31, -1, new Rectangle( 2, 97, base.txゲージ.sz画像サイズ.Width, 45 ) );

                            if( base.IsDanger( (E楽器パート)i + 1 ) && base.db現在のゲージ値[ i + 1 ] >= 0.0 )
                            {
                                base.txゲージ.t2D描画( CDTXMania.app.Device, nゲージ本体[ i ], 0, new Rectangle( 2, 364, 354, 42 ));
                            }
                            if( base.db現在のゲージ値[ i + 1 ] == 1.0 )
                            {
                                base.txゲージ.n透明度 = ( this.ctマスクFIFO.n現在の値 <= 750 ? (int)( this.ctマスクFIFO.n現在の値 / 2.94 ) : 500 - (int)(( this.ctマスクFIFO.n現在の値) / 2.94 ) );
                                base.txゲージ.t2D描画(CDTXMania.app.Device, nゲージ本体[ i ], 0, new Rectangle( 2, 318, 354, 42 ));
                            } 
                        }

                        //「ゲージがMAXでなければアニメーション」なので、ここでゲージ量による分岐をしないこと。
 				        for( int j = 0; j < 32; j++ )
				        {
                            if( this.stGaugeAddAnime[ j ].ePart == (E楽器パート)i + 1 )
                            {
					            if( this.stGaugeAddAnime[ j ].bUsing )
					            {
						            this.stGaugeAddAnime[ j ].ctAnimeCounter.t進行();
						            if( this.stGaugeAddAnime[ j ].ctAnimeCounter.b終了値に達した )
						            {
							            this.stGaugeAddAnime[ j ].ctAnimeCounter.t停止();
							            this.stGaugeAddAnime[ j ].bUsing = false;
						            }
                        
                                    this.txゲージ.b加算合成 = true;
                                    float n幅 = 320.0f * (float)base.db現在のゲージ値[ i + 1 ];
                                    if( ( 26 * this.stGaugeAddAnime[ j ].ctAnimeCounter.n現在の値 ) + 64 < n幅 )
                                    {
                                        this.txゲージ.t2D描画( CDTXMania.app.Device, nゲージ本体[ i ] + (26 * this.stGaugeAddAnime[ j ].ctAnimeCounter.n現在の値), 8, new Rectangle( 2, 410, 64, 26 ) );
                                    }
					            }
                            }
				        }

				        #region [ Risky残りMiss回数表示 ]
				        if ( this.bRisky && this.actLVLNFont != null )		// #23599 2011.7.30 yyagi Risky残りMiss回数表示
				        {
					        CActLVLNFont.EFontColor efc = this.IsDanger( E楽器パート.DRUMS ) ?
						        CActLVLNFont.EFontColor.Red : CActLVLNFont.EFontColor.Yellow;
					        actLVLNFont.t文字列描画( 262, 668, nRiskyTimes.ToString(), efc, CActLVLNFont.EFontAlign.Right );
				        }
				        #endregion
                    }
                    
                }
                //if( CDTXMania.Input管理.Keyboard.bキーが押された(  (int) SlimDXKey.F8 ) )
                //{
                //    this.tGaugeAddAnime( E楽器パート.GUITAR );
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された(  (int) SlimDXKey.F9 ) )
                //{
                //    this.tGaugeAddAnime( E楽器パート.BASS );
                //}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//private CCounter ct本体移動;
		//private CCounter ct本体振動;
		//private CTexture txゲージ;
        private int nゲージX;
		//-----------------
		#endregion
	}
}
