using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
	internal class CAct演奏GuitarRGB : CAct演奏RGB共通
	{
		// コンストラクタ

		public CAct演奏GuitarRGB()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装（共通クラスからの差分のみ）

		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(C演奏判定ライン座標共通 演奏判定ライン共通 ) のほうを使用してください。" );
		}
		public override int t進行描画( C演奏判定ライン座標共通 演奏判定ライン座標 )
		{
			if( !base.b活性化してない )
			{
				if( !CDTXMania.ConfigIni.bGuitar有効 )
				{
					return 0;
				}
				if( CDTXMania.DTX.bチップがある.Guitar )
				{
					if( base.txRGB != null )
					{
                        int nRectY = !CDTXMania.ConfigIni.bLeft.Guitar ? 0 : 64;
						base.txRGB.t2D描画( CDTXMania.app.Device, 80, 44, new Rectangle( 13, nRectY, 210, 62 ) );
                        base.txRGB.t2D描画( CDTXMania.app.Device, 67, 669, new Rectangle( 0, 127, 277, 51 ) );
					}
				}
				if( CDTXMania.DTX.bチップがある.Bass )
				{
                    if( base.txRGB != null )
                    {
                        int nRectY = !CDTXMania.ConfigIni.bLeft.Bass ? 0 : 64;
						base.txRGB.t2D描画( CDTXMania.app.Device, 948, 44, new Rectangle( 13, nRectY, 210, 62 ) );
                        base.txRGB.t2D描画( CDTXMania.app.Device, 935, 669, new Rectangle( 0, 127, 277, 51 ) );
                    }
				}
			}
			return 0;
		}
	}
}
