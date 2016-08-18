using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using FDK;

namespace DTXMania
{
	/// <summary>
	/// Wailingチップの座標計算と描画を行う基底クラス
	/// </summary>
	public class CWailingChip共通
	{
		protected E楽器パート eInst;
		protected bool bGRmode;

		protected int[] y_base;
		protected int offset;
		protected const int	WailingWidth  = (int) ( 20 * Scale.X );			// ウェイリングチップ画像の幅: 4種全て同じ値
		protected const int WailingHeight = (int) 120;	//( 50 * Scale.Y );			// ウェイリングチップ画像の高さ: 4種全て同じ値
		protected int baseTextureOffsetX, baseTextureOffsetY;

		protected int drawX;
		protected int numA, numB, numC;
		protected int showRangeY1;

	
		/// <summary>
		/// コンストラクタ
		/// </summary>
		protected CWailingChip共通()
		{
			y_base = new int[ 2 ];
		}

		/// <summary>
		/// 描画処理 (引数が多いのは追々何とかする)
		/// </summary>
		/// <param name="configIni"></param>
		/// <param name="dTX"></param>
		/// <param name="pChip"></param>
		/// <param name="txチップ"></param>
		/// <param name="演奏判定ライン座標"></param>
		/// <param name="ctWailingチップ模様アニメ"></param>
		internal void t進行描画_チップ_ギターベース_ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip,
			ref CTexture txチップ, ref C演奏判定ライン座標共通 演奏判定ライン座標, ref CCounter ctWailingチップ模様アニメ )
		{
			int indexInst = (int) eInst;
			if ( configIni.bGuitar有効 )
			{
				if ( !pChip.bHit && pChip.b可視 )
				{
					if ( txチップ != null )
					{
						txチップ.n透明度 = pChip.n透明度;
					}
					int y = configIni.bReverse.Guitar ?
						( y_base[ 1 ] - (int) ( pChip.nバーからの距離dot.Guitar ) ) :
						( y_base[ 0 ] + (int) ( pChip.nバーからの距離dot.Guitar ) );
					numB = y - offset;				// 4種全て同じ定義
					numC = 0;						// 4種全て同じ初期値
					if ( ( numB < ( showRangeY1 + numA ) ) && ( numB > -numA ) )	// 以下のロジックは4種全て同じ
					{
						int c = ctWailingチップ模様アニメ.n現在の値;
						Rectangle rect = new Rectangle(
							0,
							22,
							54,
							68
						);
						if ( numB < numA )
						{
							rect.Y += numA - numB;
							rect.Height -= numA - numB;
							numC = numA - numB;
						}
						if ( numB > ( showRangeY1 - numA ) )
						{
							rect.Height -= numB - ( showRangeY1 - numA );
						}
						if ( ( rect.Bottom > rect.Top ) && ( txチップ != null ) )
						{
							txチップ.t2D描画(
								CDTXMania.app.Device,
								drawX + 6,
								( ( y - numA ) + numC + 23 ),
								rect
							);
						}
					}
				}
				return;
			}
			pChip.bHit = true;
		}
	}




	/// <summary>
	/// Drums画面 Guitar Wailingチップ描画関連
	/// </summary>
	public class CWailngChip_Guitar_Drum画面 : CWailingChip共通
	{
		internal CWailngChip_Guitar_Drum画面( ref C演奏判定ライン座標共通 演奏判定ライン座標 ) : base()
		{
			eInst = E楽器パート.GUITAR;
			bGRmode = false;

			base.y_base[ 0 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, false );		// 95
			base.y_base[ 1 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, true  );		// 374
																								// 判定バーのY座標:ドラム画面かギター画面かで変わる値
			offset = (int) ( 0x39 * Scale.Y );												// ドラム画面かギター画面かで変わる値

			baseTextureOffsetX  = 0;	// テクスチャ画像中のウェイリングチップ画像の位置X: ドラム画面かギター画面かで変わる値
			baseTextureOffsetY  = 22;	// テクスチャ画像中のウェイリングチップ画像の位置Y: ドラム画面かギター画面かで変わる値

			drawX = (CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Left)? 1764 : 1764 - 71;	// ウェイリングチップ描画位置X座標: 4種全て異なる値

			numA = (int) ( 26 * Scale.Y );			// ドラム画面かギター画面かで変わる値
			showRangeY1 = (int) ( 355 * Scale.Y );	// ドラム画面かギター画面かで変わる値
		}
	}

	/// <summary>
	/// Drums画面 Bass Wailingチップ描画関連
	/// </summary>
	public class CWailngChip_Bass_Drum画面 : CWailingChip共通
	{
		internal CWailngChip_Bass_Drum画面( ref C演奏判定ライン座標共通 演奏判定ライン座標 ) : base()
		{
			eInst = E楽器パート.BASS;
			bGRmode = false;

			base.y_base[ 0 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, false );	// 95
			base.y_base[ 1 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, true );		// 374
			// 判定バーのY座標:ドラム画面かギター画面かで変わる値
			offset = (int) ( 0x39 * Scale.Y );												// ドラム画面かギター画面かで変わる値

			baseTextureOffsetX = 804;				// テクスチャ画像中のウェイリングチップ画像の位置X: ドラム画面かギター画面かで変わる値
			baseTextureOffsetY = 392;				// テクスチャ画像中のウェイリングチップ画像の位置Y: ドラム画面かギター画面かで変わる値

			drawX = ( CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Left ) ? 1437 : 1437 - 994;	// ウェイリングチップ描画位置X座標: 4種全て異なる値

			numA = (int) ( 26 * Scale.Y );			// ドラム画面かギター画面かで変わる値
			showRangeY1 = (int) ( 355 * Scale.Y );	// ドラム画面かギター画面かで変わる値
		}
	}

	/// <summary>
	/// GR画面 Guitar Wailingチップ描画関連
	/// </summary>
	public class CWailngChip_Guitar_GR画面 : CWailingChip共通
	{
		internal CWailngChip_Guitar_GR画面( ref C演奏判定ライン座標共通 演奏判定ライン座標 ) : base()
		{
			eInst = E楽器パート.GUITAR;
			bGRmode = true;

			base.y_base[ 0 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, false );	// 95
			base.y_base[ 1 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, true );		// 374
													// 判定バーのY座標:ドラム画面かギター画面かで変わる値
			offset = (int) ( 0 * Scale.Y );			// ドラム画面かギター画面かで変わる値
			baseTextureOffsetX = 0;	// テクスチャ画像中のウェイリングチップ画像の位置X: ドラム画面かギター画面かで変わる値
			baseTextureOffsetY = 22;	// テクスチャ画像中のウェイリングチップ画像の位置Y: ドラム画面かギター画面かで変わる値

			drawX = (int) ( 140 * Scale.X );		// ウェイリングチップ描画位置X座標: 4種全て異なる値

			numA = (int) ( 29 * Scale.Y );			// ドラム画面かギター画面かで変わる値
			showRangeY1 = (int) ( 409 * Scale.Y );	// ドラム画面かギター画面かで変わる値
		}
	}

	/// <summary>
	/// GR画面 Bass Wailingチップ描画関連
	/// </summary>
	public class CWailngChip_Bass_GR画面 : CWailingChip共通
	{
		internal CWailngChip_Bass_GR画面( ref C演奏判定ライン座標共通 演奏判定ライン座標 ) : base()
		{
			eInst = E楽器パート.BASS;
			bGRmode = true;

			base.y_base[ 0 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, false );	// 95
			base.y_base[ 1 ] = 演奏判定ライン座標.n判定ラインY座標( eInst, bGRmode, true );		// 374
																		// 判定バーのY座標:ドラム画面かギター画面かで変わる値
			offset = (int) ( 0 * Scale.Y );							// ドラム画面かギター画面かで変わる値

			baseTextureOffsetX = (int) ( 96 * Scale.X );				// テクスチャ画像中のウェイリングチップ画像の位置X: ドラム画面かギター画面かで変わる値
			baseTextureOffsetY = (int) (  0 * Scale.Y );				// テクスチャ画像中のウェイリングチップ画像の位置Y: ドラム画面かギター画面かで変わる値

			drawX = (int) ( 594 * Scale.X );							// ウェイリングチップ描画位置X座標: 4種全て異なる値

			numA = (int) ( 29 * Scale.Y );			// ドラム画面かギター画面かで変わる値
			showRangeY1 = (int) ( 409 * Scale.Y );	// ドラム画面かギター画面かで変わる値
		}
	}
}
