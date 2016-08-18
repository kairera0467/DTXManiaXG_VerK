using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏DrumsレーンフラッシュGB : CAct演奏レーンフラッシュGB共通
	{
		// CActivity 実装（共通クラスからの差分のみ）

		public override int On進行描画()
		{
			if ( !base.b活性化してない )
			{
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private readonly int[ , ] nRGBのX座標 = new int[ , ] { { 2, 0x1c, 0x36, 2, 0x1c, 0x36 }, { 0x36, 0x1c, 2, 0x36, 0x1c, 2 } };
		//-----------------
		#endregion
	}
}
