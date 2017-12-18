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
	internal class CAct演奏DrumsゲージGD : CAct演奏ゲージ共通
	{
		// プロパティ
		
		// コンストラクタ

		public CAct演奏DrumsゲージGD()
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
                if( base.txゲージ != null )
                {
                    if( base.db現在のゲージ値.Drums == 1.0 )
                    {
                        base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                        base.txゲージ.t2D描画( CDTXMania.app.Device, 371, ( CDTXMania.ConfigIni.bReverse.Drums ? 648 : 648 ), new Rectangle( 0, 18, 420, 18 ) );
                    }
                    else if( base.db現在のゲージ値.Drums >= 0.0 )
                    {
                        base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Drums;
                        if( base.IsDanger( E楽器パート.DRUMS ) && base.db現在のゲージ値.Drums >= 0.0 )
                        {
                            base.txゲージ.t2D描画( CDTXMania.app.Device, 371, ( CDTXMania.ConfigIni.bReverse.Drums ? 648 : 648 ), new Rectangle( 0, 36, 420, 18 ));
                        }
                        else
                        {
                            base.txゲージ.t2D描画( CDTXMania.app.Device, 371, ( CDTXMania.ConfigIni.bReverse.Drums ? 648 : 648 ), new Rectangle( 0, 0, 420, 18 ) );
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
