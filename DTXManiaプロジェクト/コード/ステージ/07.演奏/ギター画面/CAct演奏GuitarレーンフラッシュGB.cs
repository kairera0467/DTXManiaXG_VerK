using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏GuitarレーンフラッシュGB : CAct演奏レーンフラッシュGB共通
	{
		// コンストラクタ

		public CAct演奏GuitarレーンフラッシュGB()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装（共通クラスからの差分のみ）

		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                for ( int i = 0; i < 10; i++ )
				{
					if( !base.ct進行[ i ].b停止中 )
					{
	                    base.ct進行[ i ].t進行();
						if( base.ct進行[ i ].b終了値に達した )
						{
							base.ct進行[ i ].t停止();
						}
					}
				}

                for( int i = 0; i < 10; i++ )
                {
                    E楽器パート e楽器パート = ( i < 5 ) ? E楽器パート.GUITAR : E楽器パート.BASS;
                    int nLeftFlag = CDTXMania.ConfigIni.bLeft[ (int) e楽器パート ] ? 1 : 0;
                    int nLanenum = ( i < 5 ? i : i - 5 );
                    int x = ( (i < 5) ? 80 : 948 ) + this.nレーンフラッシュのX座標[ nLeftFlag, nLanenum ] + 2;
		            int y = CDTXMania.ConfigIni.bReverse[ (int) e楽器パート ] ? 414 : 100;
				    if( base.txFlush != null && !base.ct進行[ i ].b停止中 && CDTXMania.ConfigIni.bLaneFlush[ (int)e楽器パート ] )
					{
                        if( !CDTXMania.ConfigIni.bReverse[ (int) e楽器パート ] )
                            base.txFlush.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 37 * nLanenum, 0, 37, 256 ) );
                        else
                            base.txFlush.t2D上下反転描画( CDTXMania.app.Device, x, y, new Rectangle( 37 * nLanenum, 0, 37, 256 ) );
				    }

                    if( !base.ct進行[ i ].b停止中  && CDTXMania.ConfigIni.bLaneFlush[ (int)e楽器パート ] )
                        this.txFlushLine.t2D描画( CDTXMania.app.Device, ( e楽器パート == E楽器パート.GUITAR ? 80 : 948 ) + this.nレーンフラッシュのX座標[ nLeftFlag, ( i < 5 ? i : i - 5 ) ], 104, new Rectangle( nRectX[ ( i < 5 ? i : i - 5 ) ], 0, 41, 566 ) );
                }
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
        private int[,] nレーンフラッシュのX座標 = new int[ , ] { { 6, 45, 84, 123, 162 }, { 162, 123, 84, 45, 6 } }; //レーン左端からの相対座標
        private int[] nRectX = new int[] { 0, 39, 78, 117, 156 };
		//-----------------
		#endregion
	}
}
