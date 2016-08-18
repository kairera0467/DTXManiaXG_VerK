using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
	internal class CAct演奏DrumsRGB : CAct演奏RGB共通
	{
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
					int x = ( CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Left ) ? 1527 : 1456;
					for( int i = 0; i < 3; i++ )
					{
						int index = CDTXMania.ConfigIni.bLeft.Guitar ? ( 2 - i ) : i;
						Rectangle rc = new Rectangle(
							index * 72,
							0,
							72,
							72
						);
						if ( base.b押下状態[ index ] )
						{
							rc.Y += 72;
						}
						if( base.txRGB != null )
						{
							//	int y = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.GUITAR, true, CDTXMania.ConfigIni.bReverse.Guitar, false, false );
							//base.txRGB.t2D描画( CDTXMania.app.Device, 0x1fd + ( j * 0x1a ), 0x39, rectangle );
							int y = 演奏判定ライン座標.n演奏RGBボタンY座標( E楽器パート.GUITAR, false, CDTXMania.ConfigIni.bReverse.Guitar );
							base.txRGB.t2D描画(
								CDTXMania.app.Device,
								x + ( i * 26 * 3 ),
								y,
								rc
							);
						}
					}
				}
				if( CDTXMania.DTX.bチップがある.Bass )
				{
					for( int i = 0; i < 3; i++ )
					{
						int x = ( CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Left ) ? 1200 : 206;
						int index = CDTXMania.ConfigIni.bLeft.Bass ? ( 2 - i ) : i;
						Rectangle rc = new Rectangle(
							index * 72,
							0,
							72,
							72
						);
						if( base.b押下状態[ index + 3 ] )
						{
							rc.Y += 72;
						}
						if( base.txRGB != null )
						{
							int y = 演奏判定ライン座標.n演奏RGBボタンY座標( E楽器パート.BASS, false, CDTXMania.ConfigIni.bReverse.Bass );
							base.txRGB.t2D描画(
								CDTXMania.app.Device,
								x + ( i * 26 * 3 ),
								y,
								rc
							);
						}
					}
				}
				for( int i = 0; i < 6; i++ )
				{
					base.b押下状態[ i ] = false;
				}
			}
			return 0;
		}
	}
}
