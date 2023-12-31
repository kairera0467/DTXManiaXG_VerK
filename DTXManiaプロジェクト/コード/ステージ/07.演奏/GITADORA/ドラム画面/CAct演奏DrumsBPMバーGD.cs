﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
	internal class CAct演奏DrumsBPMバーGD : CAct演奏BPMバー共通
	{
		// CActivity 実装（共通クラスからの差分のみ）

		public override int On進行描画()
		{
            if( !base.b活性化してない )
            {
                base.ctBPMバー.t進行Loop();

                int num1 = base.ctBPMバー.n現在の値;
                if( ( base.txBPMバー != null ) )
                {
                    //if( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A )
                    {
                        base.txBPMバー.n透明度 = 255;
                        base.txBPMバー.t2D描画( CDTXMania.app.Device, 900 + (float)( 6 * Math.Sin( Math.PI * num1 / 14 ) ), 54, new Rectangle( 38, 0, 10, 600 ) );

                        if( base.bサビ区間 )
                        {
                            base.txBPMバー.n透明度 = 255 - (int)( 255 * num1 / 14 );
                            base.txBPMバー.t2D描画( CDTXMania.app.Device, 896 + (float)( 6 * Math.Sin( Math.PI * num1 / 14 ) ), 54, new Rectangle( 80, 0, 32, 600 ) );
                        }
                    }
                    //if( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A || CDTXMania.ConfigIni.eBPMbar == Eタイプ.B )
                    {
                        base.txBPMバー.n透明度 = 255;
                        base.txBPMバー.t2D描画( CDTXMania.app.Device, 240 - (float)( 6 * Math.Sin( Math.PI * num1 / 14 ) ), 54, new Rectangle( 28, 0, 10, 600 ) );

                        if( base.bサビ区間 )
                        {
                            base.txBPMバー.n透明度 = 255 - (int)( 255 * num1 / 14 );
                            base.txBPMバー.t2D描画( CDTXMania.app.Device, 220 - (float)( 6 * Math.Sin( Math.PI * num1 / 14 ) ), 54, new Rectangle( 48, 0, 32, 600 ) );
                        }
                    }
                }
            }
			return 0;
		}
	}
}
