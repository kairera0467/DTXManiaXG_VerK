using System;
using System.Collections.Generic;
using System.Text;

namespace DTXMania
{
	internal class CAct演奏GuitarコンボGD : CAct演奏Combo共通
	{
		// CAct演奏Combo共通 実装

		protected override void tコンボ表示_ギター( int nCombo値, int nジャンプインデックス )
		{
			int x = (int) ( 230 * Scale.X );
			int y = CDTXMania.ConfigIni.bReverse.Guitar ? 0x103 : 150;
			y = (int) ( y * Scale.Y );
			//int y = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.GUITAR, false, CDTXMania.ConfigIni.bReverse.Guitar );
			//y += CDTXMania.ConfigIni.bReverse.Guitar ? -134 : +174;
			if ( base.txCOMBOギター != null )
			{
				base.txCOMBOギター.n透明度 = 0xff;
			}
			base.tコンボ表示_ギター( nCombo値, x, y, nジャンプインデックス );
		}
		protected override void tコンボ表示_ドラム( int nCombo値, int nジャンプインデックス )
		{
		}
		protected override void tコンボ表示_ベース( int nCombo値, int nジャンプインデックス )
		{
			int x = (int) ( 410 * Scale.X );
			int y = CDTXMania.ConfigIni.bReverse.Bass ? 0x103 : 150;
			y = (int) ( y * Scale.Y );
			//int y = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.BASS, false, CDTXMania.ConfigIni.bReverse.Bass );
			//y += CDTXMania.ConfigIni.bReverse.Bass ? -134 : +174;
			if ( base.txCOMBOギター != null )
			{
				base.txCOMBOギター.n透明度 = 0xff;
			}
			base.tコンボ表示_ベース( nCombo値, x, y, nジャンプインデックス );
		}
	}
}
