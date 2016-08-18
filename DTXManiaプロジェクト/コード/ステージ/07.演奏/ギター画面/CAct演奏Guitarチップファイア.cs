using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
	internal class CAct演奏Guitarチップファイア : CAct演奏チップファイアGB
	{
		// コンストラクタ

		public CAct演奏Guitarチップファイア()
		{
			base.b活性化してない = true;
		}
		
		
		// メソッド

		public override void Start( int nLane, C演奏判定ライン座標共通 演奏判定ライン座標 )
		{
			if( ( nLane < 0 ) && ( nLane > 9 ) )
			{
				throw new IndexOutOfRangeException();
			}
			E楽器パート e楽器パート = ( nLane < 5 ) ? E楽器パート.GUITAR : E楽器パート.BASS;
			int index = nLane < 5 ? nLane : nLane - 5;
			if( CDTXMania.ConfigIni.bLeft[ (int) e楽器パート ] )
			{
				index = ( ( index / 5 ) * 5 ) + ( 4 - ( index % 5 ) );
			}
            int[] n本体X = new int[] { 80, 948 };
			int x = n本体X[ e楽器パート == E楽器パート.GUITAR ? 0 : 1 ] + this.nレーンの中央X座標[ index ];
			int y = 演奏判定ライン座標.n判定ラインY座標( e楽器パート, true, CDTXMania.ConfigIni.bReverse[ (int) e楽器パート ] );

			base.Start( nLane, x, y, 演奏判定ライン座標 );
		}


		// その他

		#region [ private ]
		//-----------------
		private readonly Point[] pt中央 = new Point[] {
			new Point(  42, 40 ),	// GtR
			new Point(  78, 40 ),	// GtG
			new Point( 114, 40 ),	// GtB
			new Point( 496, 40 ),	// BsR
			new Point( 532, 40 ),	// BsG
			new Point( 568, 40 )	// BsB
		};

        private int[] nレーンの中央X座標 = new int[] { 27, 66, 105, 144, 183 }; //レーン左端からの相対座標
		//-----------------
		#endregion
	}
}
