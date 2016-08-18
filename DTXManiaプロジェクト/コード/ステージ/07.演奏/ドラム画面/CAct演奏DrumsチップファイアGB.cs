using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace DTXMania
{
	internal class CAct演奏DrumsチップファイアGB : CAct演奏チップファイアGB
	{
		// メソッド

		public override void Start( int nLane, C演奏判定ライン座標共通 演奏判定ライン座標 )
		{

		}


		// その他

		#region [ private ]
		//-----------------
		private readonly Point[] pt中央 = new Point[] {
			new Point( 519 * 3, (int) (95 * 2.25) ),	// GtR
			new Point( 545 * 3, (int) (95 * 2.25) ),	// GtG
			new Point( 571 * 3, (int) (95 * 2.25) ),	// GtB
			new Point( 410 * 3, (int) (95 * 2.25) ),	// BsR
			new Point( 436 * 3, (int) (95 * 2.25) ),	// BsG
			new Point( 462 * 3, (int) (95 * 2.25) )		// BsB
		};
		//-----------------
		#endregion
	}
}
