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
				this.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Gauge_Guitar_bar.png" ) );
				this.txゲージ背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Gauge_Guitar.png" ) );

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
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                if( base.txゲージ背景 != null & base.txゲージ != null )
                {
                    int[] nゲージ本体 = new int[] { 0, 0 };
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ) nゲージ本体 = new int[]{ 65, 861 };
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B ) nゲージ本体 = new int[]{ -2, 931 };
                    for( int i = 0; i < 2; i++ )
                    {
                        if( CDTXMania.DTX.bチップがある[ i + 1 ] )
                        {
                            base.txゲージ背景.t2D描画( CDTXMania.app.Device, nゲージ本体[ i ], 0, new Rectangle( 0, 0, base.txゲージ背景.sz画像サイズ.Width, 42 ) );

                            if( base.db現在のゲージ値[ i + 1 ] == 1.0 )
                            {
                                base.txゲージ.vc拡大縮小倍率.X = 1.0f;
                                base.txゲージ.t2D描画( CDTXMania.app.Device, 1 + this.nゲージX + nゲージ本体[ i ], 8, new Rectangle( 0, 0, 320, 26 ) );
                            }
                            else if( base.db現在のゲージ値[ i + 1 ] >= 0.0 )
                            {
                                base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値[ i + 1 ];
                                base.txゲージ.t2D描画( CDTXMania.app.Device, 1 + this.nゲージX + nゲージ本体[ i ], 8, new Rectangle( 0, 0, 320, 26 ) );
                            }

                            //base.txゲージ背景.t2D描画( CDTXMania.app.Device, 258, ( CDTXMania.ConfigIni.bReverse.Drums ? 20 : 655 ), new Rectangle( 0, 45, base.txゲージ背景.sz画像サイズ.Width, 45 ) );
                        }

                    }
  
                }
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
