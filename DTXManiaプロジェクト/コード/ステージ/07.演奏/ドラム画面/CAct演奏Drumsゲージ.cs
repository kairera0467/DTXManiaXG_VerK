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
                this.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_gauge_bar.png" ) );
                this.txゲージ背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Gauge.png" ) );

                this.txマスクF = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask.png"));
                this.txマスクD = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask2.png"));

                this.nゲージX = (int)( (float)( base.txゲージ背景.sz画像サイズ.Width - base.txゲージ.sz画像サイズ.Width ) / 2f );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txゲージ背景 );
				CDTXMania.tテクスチャの解放( ref this.txマスクD );
                CDTXMania.tテクスチャの解放( ref this.txマスクF );

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if ( !base.b活性化してない )
			{
                this.ctマスクFIFO.t進行Loop();
                if( base.txゲージ背景 != null & base.txゲージ != null )
                {
                    //A～C
                    base.txゲージ背景.t2D描画( CDTXMania.app.Device, 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655 ), new Rectangle( 0, 0, base.txゲージ背景.sz画像サイズ.Width, 45 ) );

                    if( base.db現在のゲージ値.Drums == 1.0 )
                    {
                        base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                        base.txゲージ.t2D描画( CDTXMania.app.Device, 1 + this.nゲージX + 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665 ), new Rectangle( 0, 0, 504, 26 ) );
                    }
                    else if( base.db現在のゲージ値.Drums >= 0.0 )
                    {
                        base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Drums;
                        base.txゲージ.t2D描画( CDTXMania.app.Device, 1 + this.nゲージX + 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 30 : 665 ), new Rectangle( 0, 0, 504, 26 ) );
                    }

                    base.txゲージ背景.t2D描画( CDTXMania.app.Device, 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655 ), new Rectangle( 0, 45, base.txゲージ背景.sz画像サイズ.Width, 45 ) );
                }

                if( base.IsDanger( E楽器パート.DRUMS ) && base.db現在のゲージ値.Drums >= 0.0 && this.txマスクD != null )
                {
                    this.txマスクD.t2D描画( CDTXMania.app.Device, 259, ( CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652 ));
                }
                if( base.db現在のゲージ値.Drums == 1.0 && base.txマスクF != null )
                {
                    this.txマスクF.t2D描画(CDTXMania.app.Device, 259, (CDTXMania.ConfigIni.bReverse.Drums ? 17 : 652));
                    this.txマスクF.n透明度 = ( this.ct本体移動.n現在の値 <= 750 ? (int)( this.ct本体移動.n現在の値 / 2.94 ) : 500 - (int)(( this.ct本体移動.n現在の値) / 2.94 ) );
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
