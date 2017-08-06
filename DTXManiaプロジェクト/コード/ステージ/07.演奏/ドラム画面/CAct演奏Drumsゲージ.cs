using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Drumsゲージ : CAct演奏ゲージ共通
	{
		// プロパティ
		
		// コンストラクタ

		public CAct演奏Drumsゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
			// CAct演奏ゲージ共通.Init()に移動
			// this.dbゲージ値 = ( CDTXMania.ConfigIni.nRisky > 0 ) ? 1.0 : 0.66666666666666663;
            this.ctマスクFIFO = new CCounter( 0, 1500, 2, CDTXMania.Timer );
            this.stGaugeAddAnime = new STGaugeAddAnime[ 32 ];
            for( int i = 0; i < 32; i++ )
            {
                this.stGaugeAddAnime[ i ] = new STGaugeAddAnime();
            }
			base.On活性化();
		}
		public override void On非活性化()
		{
            for( int i = 0; i < 32; i++ )
            {
                base.stGaugeAddAnime = null;
            }
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                base.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Gauge.png" ) );

                this.nゲージX = (int)( (float)( 592 - 504 ) / 2.0f );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref base.txゲージ );

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if ( !base.b活性化してない )
			{
                this.ctマスクFIFO.t進行Loop();
                if( base.txゲージ != null )
                {
                    base.txゲージ.b加算合成 = false;
                    base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                    base.txゲージ.n透明度 = 255;
                    base.txゲージ.t2D描画( CDTXMania.app.Device, 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655 ), new Rectangle( 2, 2, base.txゲージ.sz画像サイズ.Width, 45 ) );

                    if( base.db現在のゲージ値.Drums == 1.0 )
                    {
                        base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                        base.txゲージ.t2D描画( CDTXMania.app.Device, 1 + this.nゲージX + 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665 ), new Rectangle( 2, 146, 504, 26 ) );
                    }
                    else if( base.db現在のゲージ値.Drums >= 0.0 )
                    {
                        base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Drums;
                        base.txゲージ.t2D描画( CDTXMania.app.Device, 1 + this.nゲージX + 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665 ), new Rectangle( 2, 146, 504, 26 ) );
                    }

                    base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                    base.txゲージ.t2D描画( CDTXMania.app.Device, 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655 ), new Rectangle( 2, 97, base.txゲージ.sz画像サイズ.Width, 45 ) );
                

                    if( base.IsDanger( E楽器パート.DRUMS ) && base.db現在のゲージ値.Drums >= 0.0 )
                    {
                        base.txゲージ.t2D描画( CDTXMania.app.Device, 259, ( CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652 ), new Rectangle( 2, 260, 592, 52 ));
                    }
                    if( base.db現在のゲージ値.Drums == 1.0 )
                    {
                        base.txゲージ.n透明度 = ( this.ctマスクFIFO.n現在の値 <= 750 ? (int)( this.ctマスクFIFO.n現在の値 / 2.94 ) : 500 - (int)(( this.ctマスクFIFO.n現在の値) / 2.94 ) );
                        base.txゲージ.t2D描画(CDTXMania.app.Device, 259, (CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652), new Rectangle( 2, 206, 592, 52 ));
                    }


                    //「ゲージがMAXでなければアニメーション」なので、ここでゲージ量による分岐をしないこと。
 				    for( int i = 0; i < 32; i++ )
				    {
                        if( this.stGaugeAddAnime[ i ].ePart == E楽器パート.DRUMS )
                        {
					        if( this.stGaugeAddAnime[ i ].bUsing )
					        {
						        this.stGaugeAddAnime[ i ].ctAnimeCounter.t進行();
						        if( this.stGaugeAddAnime[ i ].ctAnimeCounter.b終了値に達した )
						        {
							        this.stGaugeAddAnime[ i ].ctAnimeCounter.t停止();
							        this.stGaugeAddAnime[ i ].bUsing = false;
						        }
                        
                                this.txゲージ.b加算合成 = true;
                                float n幅 = 504.0f * (float)base.db現在のゲージ値.Drums;
                                if( (42 * this.stGaugeAddAnime[i].ctAnimeCounter.n現在の値) + 64 < n幅 )
                                {
                                    this.txゲージ.t2D描画( CDTXMania.app.Device, (1 + this.nゲージX + 258) + (42 * this.stGaugeAddAnime[i].ctAnimeCounter.n現在の値), 665, new Rectangle( 2, 410, 64, 26 ) );
                                }
					        }
                        }
				    }
                }
                //if( CDTXMania.Input管理.Keyboard.bキーが押された(  (int) SlimDX.DirectInput.Key.F8 ) )
                //{
                //    this.tGaugeAddAnime( E楽器パート.DRUMS );
                //}
				#region [ Risky残りMiss回数表示 ]
				if ( this.bRisky && this.actLVLNFont != null )		// #23599 2011.7.30 yyagi Risky残りMiss回数表示
				{
					CActLVLNFont.EFontColor efc = this.IsDanger( E楽器パート.DRUMS ) ?
						CActLVLNFont.EFontColor.Red : CActLVLNFont.EFontColor.Yellow;
					actLVLNFont.t文字列描画( 262, 668, nRiskyTimes.ToString(), efc, CActLVLNFont.EFontAlign.Right );
				}
				#endregion
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
        private int nゲージX;
		//-----------------
		#endregion
	}
}
