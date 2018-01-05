using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Threading;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	/// <summary>
	/// 演奏画面の共通クラス (ドラム演奏画面, ギター演奏画面の継承元)
	/// </summary>
	internal abstract class CStage演奏画面共通 : CStage
	{
		// プロパティ

		public bool bAUTOでないチップが１つでもバーを通過した	// 誰も参照していない？？
		{
			get;
			protected set;
		}

		// メソッド

		#region [ t演奏結果を格納する_ドラム() ]
		public void t演奏結果を格納する_ドラム( out CScoreIni.C演奏記録 Drums )
		{
			Drums = new CScoreIni.C演奏記録();

			if ( CDTXMania.DTX.bチップがある.Drums && !CDTXMania.ConfigIni.bギタレボモード )
			{
				Drums.nスコア = (long) this.actScore.Get( E楽器パート.DRUMS );
                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                {
				    Drums.dbゲーム型スキル値 = CScoreIni.tゲーム型スキルを計算して返す( CDTXMania.DTX.LEVEL.Drums, CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数_Auto含まない.Drums.Perfect, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay );
				    Drums.db演奏型スキル値 = CScoreIni.t演奏型スキルを計算して返す( CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数_Auto含まない.Drums.Perfect, this.nヒット数_Auto含まない.Drums.Great, this.nヒット数_Auto含まない.Drums.Good, this.nヒット数_Auto含まない.Drums.Poor, this.nヒット数_Auto含まない.Drums.Miss, E楽器パート.DRUMS, bIsAutoPlay );
                }
                else
                {
				    Drums.dbゲーム型スキル値 = CScoreIni.tXG曲別スキルを計算して返す( CDTXMania.DTX.LEVEL.Drums, CDTXMania.DTX.LEVELDEC.Drums, CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数_Auto含まない.Drums.Perfect, this.nヒット数_Auto含まない.Drums.Great, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay );
                    Drums.db演奏型スキル値 = CScoreIni.tXG演奏型スキルを計算して返す( CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数_Auto含まない.Drums.Perfect, this.nヒット数_Auto含まない.Drums.Great, this.nヒット数_Auto含まない.Drums.Good, this.nヒット数_Auto含まない.Drums.Poor, this.nヒット数_Auto含まない.Drums.Miss, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay );
                    // Drums.db演奏型スキル値 = CScoreIni.tXGSkillCal( CDTXMania.DTX.n可視チップ数.Drums, this.nヒット数_Auto含まない.Drums.Perfect, this.nヒット数_Auto含まない.Drums.Great, this.nヒット数_Auto含まない.Drums.Good, this.nヒット数_Auto含まない.Drums.Poor, this.nヒット数_Auto含まない.Drums.Miss, 0, this.actCombo.n現在のコンボ数.Drums最高値, E楽器パート.DRUMS, bIsAutoPlay, 0.0 ).dbGame;
                }
				Drums.nPerfect数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数_Auto含む.Drums.Perfect : this.nヒット数_Auto含まない.Drums.Perfect;
				Drums.nGreat数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数_Auto含む.Drums.Great : this.nヒット数_Auto含まない.Drums.Great;
				Drums.nGood数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数_Auto含む.Drums.Good : this.nヒット数_Auto含まない.Drums.Good;
				Drums.nPoor数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数_Auto含む.Drums.Poor : this.nヒット数_Auto含まない.Drums.Poor;
				Drums.nMiss数 = CDTXMania.ConfigIni.bドラムが全部オートプレイである ? this.nヒット数_Auto含む.Drums.Miss : this.nヒット数_Auto含まない.Drums.Miss;

				Drums.nPerfect数_Auto含まない = this.nヒット数_Auto含まない.Drums.Perfect;
				Drums.nGreat数_Auto含まない = this.nヒット数_Auto含まない.Drums.Great;
				Drums.nGood数_Auto含まない = this.nヒット数_Auto含まない.Drums.Good;
				Drums.nPoor数_Auto含まない = this.nヒット数_Auto含まない.Drums.Poor;
				Drums.nMiss数_Auto含まない = this.nヒット数_Auto含まない.Drums.Miss;
				Drums.n最大コンボ数 = this.actCombo.n現在のコンボ数.Drums最高値;
				Drums.n全チップ数 = CDTXMania.DTX.n可視チップ数.Drums;
				for ( int i = 0; i < (int) Eレーン.MAX;  i++ )
				{
					Drums.bAutoPlay[ i ] = bIsAutoPlay[ i ];
				}
				Drums.bTight = CDTXMania.ConfigIni.bTight;
				for ( int i = 0; i < 3; i++ )
				{
					Drums.bSudden[ i ] = CDTXMania.ConfigIni.bSudden[ i ];
					Drums.bHidden[ i ] = CDTXMania.ConfigIni.bHidden[ i ];
					Drums.eInvisible[ i ] = CDTXMania.ConfigIni.eInvisible[ i ];
					Drums.bReverse[ i ] = CDTXMania.ConfigIni.bReverse[ i ];
					Drums.eRandom[ i ] = CDTXMania.ConfigIni.eRandom[ i ];
					Drums.bLight[ i ] = CDTXMania.ConfigIni.bLight[ i ];
					Drums.bLeft[ i ] = CDTXMania.ConfigIni.bLeft[ i ];
					Drums.f譜面スクロール速度[ i ] = ( (float) ( CDTXMania.ConfigIni.n譜面スクロール速度[ i ] + 1 ) ) * 0.5f;
				}
				Drums.eDark = CDTXMania.ConfigIni.eDark;
				Drums.n演奏速度分子 = CDTXMania.ConfigIni.n演奏速度;
				Drums.n演奏速度分母 = 20;
				Drums.eHHGroup = CDTXMania.ConfigIni.eHHGroup;
				Drums.eFTGroup = CDTXMania.ConfigIni.eFTGroup;
				Drums.eCYGroup = CDTXMania.ConfigIni.eCYGroup;
				Drums.eHitSoundPriorityHH = CDTXMania.ConfigIni.eHitSoundPriorityHH;
				Drums.eHitSoundPriorityFT = CDTXMania.ConfigIni.eHitSoundPriorityFT;
				Drums.eHitSoundPriorityCY = CDTXMania.ConfigIni.eHitSoundPriorityCY;
				Drums.bGuitar有効 = CDTXMania.ConfigIni.bGuitar有効;
				Drums.bDrums有効 = CDTXMania.ConfigIni.bDrums有効;
				Drums.bSTAGEFAILED有効 = CDTXMania.ConfigIni.bSTAGEFAILED有効;
				Drums.eダメージレベル = CDTXMania.ConfigIni.eダメージレベル;
				Drums.b演奏にキーボードを使用した = this.b演奏にキーボードを使った.Drums;
				Drums.b演奏にMIDI入力を使用した = this.b演奏にMIDI入力を使った.Drums;
				Drums.b演奏にジョイパッドを使用した = this.b演奏にジョイパッドを使った.Drums;
				Drums.b演奏にマウスを使用した = this.b演奏にマウスを使った.Drums;
				Drums.nPerfectになる範囲ms = CDTXMania.nPerfect範囲ms;
				Drums.nGreatになる範囲ms = CDTXMania.nGreat範囲ms;
				Drums.nGoodになる範囲ms = CDTXMania.nGood範囲ms;
				Drums.nPoorになる範囲ms = CDTXMania.nPoor範囲ms;
				Drums.strDTXManiaのバージョン = CDTXMania.VERSION;
				Drums.最終更新日時 = DateTime.Now.ToString();
				Drums.Hash = CScoreIni.t演奏セクションのMD5を求めて返す( Drums );
				Drums.nRisky = CDTXMania.ConfigIni.nRisky; // #35461 chnmr0 add
				Drums.bギターとベースを入れ替えた = CDTXMania.ConfigIni.bIsSwappedGuitarBass; // #35417 chnmr0 add
			}
		}
		#endregion
		#region [ t演奏結果を格納する_ギター() ]
		public void t演奏結果を格納する_ギター( out CScoreIni.C演奏記録 Guitar )
		{
			Guitar = new CScoreIni.C演奏記録();

			if ( CDTXMania.DTX.bチップがある.Guitar )
			{
				Guitar.nスコア = (long) this.actScore.Get( E楽器パート.GUITAR );
                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                {
	    			Guitar.dbゲーム型スキル値 = CScoreIni.tゲーム型スキルを計算して返す( CDTXMania.DTX.LEVEL.Guitar, CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数_Auto含まない.Guitar.Perfect, this.actCombo.n現在のコンボ数.Guitar最高値, E楽器パート.GUITAR, bIsAutoPlay );
    				Guitar.db演奏型スキル値 = CScoreIni.t演奏型スキルを計算して返す( CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数_Auto含まない.Guitar.Perfect, this.nヒット数_Auto含まない.Guitar.Great, this.nヒット数_Auto含まない.Guitar.Good, this.nヒット数_Auto含まない.Guitar.Poor, this.nヒット数_Auto含まない.Guitar.Miss, E楽器パート.GUITAR, bIsAutoPlay );
                }
                else
                {
	    			Guitar.dbゲーム型スキル値 = CScoreIni.tXG曲別スキルを計算して返す( CDTXMania.DTX.LEVEL.Guitar, CDTXMania.DTX.LEVELDEC.Guitar, CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数_Auto含まない.Guitar.Perfect, this.nヒット数_Auto含まない.Guitar.Great, this.actCombo.n現在のコンボ数.Guitar最高値, E楽器パート.GUITAR, bIsAutoPlay );
    				Guitar.db演奏型スキル値 = CScoreIni.tXG演奏型スキルを計算して返す( CDTXMania.DTX.n可視チップ数.Guitar, this.nヒット数_Auto含まない.Guitar.Perfect, this.nヒット数_Auto含まない.Guitar.Great, this.nヒット数_Auto含まない.Guitar.Good, this.nヒット数_Auto含まない.Guitar.Poor, this.nヒット数_Auto含まない.Guitar.Miss, this.actCombo.n現在のコンボ数.Guitar最高値, E楽器パート.GUITAR, bIsAutoPlay );
                }
				Guitar.nPerfect数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数_Auto含む.Guitar.Perfect : this.nヒット数_Auto含まない.Guitar.Perfect;
				Guitar.nGreat数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数_Auto含む.Guitar.Great : this.nヒット数_Auto含まない.Guitar.Great;
				Guitar.nGood数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数_Auto含む.Guitar.Good : this.nヒット数_Auto含まない.Guitar.Good;
				Guitar.nPoor数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数_Auto含む.Guitar.Poor : this.nヒット数_Auto含まない.Guitar.Poor;
				Guitar.nMiss数 = CDTXMania.ConfigIni.bギターが全部オートプレイである ? this.nヒット数_Auto含む.Guitar.Miss : this.nヒット数_Auto含まない.Guitar.Miss;
				Guitar.nPerfect数_Auto含まない = this.nヒット数_Auto含まない.Guitar.Perfect;
				Guitar.nGreat数_Auto含まない = this.nヒット数_Auto含まない.Guitar.Great;
				Guitar.nGood数_Auto含まない = this.nヒット数_Auto含まない.Guitar.Good;
				Guitar.nPoor数_Auto含まない = this.nヒット数_Auto含まない.Guitar.Poor;
				Guitar.nMiss数_Auto含まない = this.nヒット数_Auto含まない.Guitar.Miss;
				Guitar.n最大コンボ数 = this.actCombo.n現在のコンボ数.Guitar最高値;
				Guitar.n全チップ数 = CDTXMania.DTX.n可視チップ数.Guitar;
				for ( int i = 0; i < (int) Eレーン.MAX; i++ )
				{
					Guitar.bAutoPlay[ i ] = bIsAutoPlay[ i ];
				}
				Guitar.bTight = CDTXMania.ConfigIni.bTight;
				for ( int i = 0; i < 3; i++ )
				{
					Guitar.bSudden[ i ] = CDTXMania.ConfigIni.bSudden[ i ];
					Guitar.bHidden[ i ] = CDTXMania.ConfigIni.bHidden[ i ];
					Guitar.eInvisible[ i ] = CDTXMania.ConfigIni.eInvisible[ i ];
					Guitar.bReverse[ i ] = CDTXMania.ConfigIni.bReverse[ i ];
					Guitar.eRandom[ i ] = CDTXMania.ConfigIni.eRandom[ i ];
					Guitar.bLight[ i ] = CDTXMania.ConfigIni.bLight[ i ];
					Guitar.bLeft[ i ] = CDTXMania.ConfigIni.bLeft[ i ];
					Guitar.f譜面スクロール速度[ i ] = ( (float) ( CDTXMania.ConfigIni.n譜面スクロール速度[ i ] + 1 ) ) * 0.5f;
				}
				Guitar.eDark = CDTXMania.ConfigIni.eDark;
				Guitar.n演奏速度分子 = CDTXMania.ConfigIni.n演奏速度;
				Guitar.n演奏速度分母 = 20;
				Guitar.eHHGroup = CDTXMania.ConfigIni.eHHGroup;
				Guitar.eFTGroup = CDTXMania.ConfigIni.eFTGroup;
				Guitar.eCYGroup = CDTXMania.ConfigIni.eCYGroup;
				Guitar.eHitSoundPriorityHH = CDTXMania.ConfigIni.eHitSoundPriorityHH;
				Guitar.eHitSoundPriorityFT = CDTXMania.ConfigIni.eHitSoundPriorityFT;
				Guitar.eHitSoundPriorityCY = CDTXMania.ConfigIni.eHitSoundPriorityCY;
				Guitar.bGuitar有効 = CDTXMania.ConfigIni.bGuitar有効;
				Guitar.bDrums有効 = CDTXMania.ConfigIni.bDrums有効;
				Guitar.bSTAGEFAILED有効 = CDTXMania.ConfigIni.bSTAGEFAILED有効;
				Guitar.eダメージレベル = CDTXMania.ConfigIni.eダメージレベル;
				Guitar.b演奏にキーボードを使用した = this.b演奏にキーボードを使った.Guitar;
				Guitar.b演奏にMIDI入力を使用した = this.b演奏にMIDI入力を使った.Guitar;
				Guitar.b演奏にジョイパッドを使用した = this.b演奏にジョイパッドを使った.Guitar;
				Guitar.b演奏にマウスを使用した = this.b演奏にマウスを使った.Guitar;
				Guitar.nPerfectになる範囲ms = CDTXMania.nPerfect範囲ms;
				Guitar.nGreatになる範囲ms = CDTXMania.nGreat範囲ms;
				Guitar.nGoodになる範囲ms = CDTXMania.nGood範囲ms;
				Guitar.nPoorになる範囲ms = CDTXMania.nPoor範囲ms;
				Guitar.strDTXManiaのバージョン = CDTXMania.VERSION;
				Guitar.最終更新日時 = DateTime.Now.ToString();
				Guitar.Hash = CScoreIni.t演奏セクションのMD5を求めて返す( Guitar );
				Guitar.bギターとベースを入れ替えた = CDTXMania.ConfigIni.bIsSwappedGuitarBass; // #35417 chnmr0 add
			}
		}
		#endregion
		#region [ t演奏結果を格納する_ベース() ]
		public void t演奏結果を格納する_ベース( out CScoreIni.C演奏記録 Bass )
		{
			Bass = new CScoreIni.C演奏記録();

			if ( CDTXMania.DTX.bチップがある.Bass )
			{
				Bass.nスコア = (long) this.actScore.Get( E楽器パート.BASS );
                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                {
				    Bass.dbゲーム型スキル値 = CScoreIni.tゲーム型スキルを計算して返す( CDTXMania.DTX.LEVEL.Bass, CDTXMania.DTX.n可視チップ数.Bass, this.nヒット数_Auto含まない.Bass.Perfect, this.actCombo.n現在のコンボ数.Bass最高値, E楽器パート.BASS, bIsAutoPlay );
				    Bass.db演奏型スキル値 = CScoreIni.t演奏型スキルを計算して返す( CDTXMania.DTX.n可視チップ数.Bass, this.nヒット数_Auto含まない.Bass.Perfect, this.nヒット数_Auto含まない.Bass.Great, this.nヒット数_Auto含まない.Bass.Good, this.nヒット数_Auto含まない.Bass.Poor, this.nヒット数_Auto含まない.Bass.Miss, E楽器パート.BASS, bIsAutoPlay );
                }
                else
                {
				    Bass.dbゲーム型スキル値 = CScoreIni.tXG曲別スキルを計算して返す( CDTXMania.DTX.LEVEL.Bass, CDTXMania.DTX.LEVELDEC.Bass, CDTXMania.DTX.n可視チップ数.Bass, this.nヒット数_Auto含まない.Bass.Perfect, this.nヒット数_Auto含まない.Bass.Great, this.actCombo.n現在のコンボ数.Bass最高値, E楽器パート.BASS, bIsAutoPlay );
				    Bass.db演奏型スキル値 = CScoreIni.tXG演奏型スキルを計算して返す( CDTXMania.DTX.n可視チップ数.Bass, this.nヒット数_Auto含まない.Bass.Perfect, this.nヒット数_Auto含まない.Bass.Great, this.nヒット数_Auto含まない.Bass.Good, this.nヒット数_Auto含まない.Bass.Poor, this.nヒット数_Auto含まない.Bass.Miss, this.actCombo.n現在のコンボ数.Bass最高値, E楽器パート.BASS, bIsAutoPlay );
                }
				Bass.nPerfect数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数_Auto含む.Bass.Perfect : this.nヒット数_Auto含まない.Bass.Perfect;
				Bass.nGreat数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数_Auto含む.Bass.Great : this.nヒット数_Auto含まない.Bass.Great;
				Bass.nGood数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数_Auto含む.Bass.Good : this.nヒット数_Auto含まない.Bass.Good;
				Bass.nPoor数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数_Auto含む.Bass.Poor : this.nヒット数_Auto含まない.Bass.Poor;
				Bass.nMiss数 = CDTXMania.ConfigIni.bベースが全部オートプレイである ? this.nヒット数_Auto含む.Bass.Miss : this.nヒット数_Auto含まない.Bass.Miss;
				Bass.nPerfect数_Auto含まない = this.nヒット数_Auto含まない.Bass.Perfect;
				Bass.nGreat数_Auto含まない = this.nヒット数_Auto含まない.Bass.Great;
				Bass.nGood数_Auto含まない = this.nヒット数_Auto含まない.Bass.Good;
				Bass.nPoor数_Auto含まない = this.nヒット数_Auto含まない.Bass.Poor;
				Bass.nMiss数_Auto含まない = this.nヒット数_Auto含まない.Bass.Miss;
				Bass.n最大コンボ数 = this.actCombo.n現在のコンボ数.Bass最高値;
				Bass.n全チップ数 = CDTXMania.DTX.n可視チップ数.Bass;
				for ( int i = 0; i < (int) Eレーン.MAX; i++ )
				{
					Bass.bAutoPlay[ i ] = bIsAutoPlay[ i ];
				}
				Bass.bTight = CDTXMania.ConfigIni.bTight;
				for ( int i = 0; i < 3; i++ )
				{
					Bass.bSudden[ i ] = CDTXMania.ConfigIni.bSudden[ i ];
					Bass.bHidden[ i ] = CDTXMania.ConfigIni.bHidden[ i ];
					Bass.eInvisible[ i ] = CDTXMania.ConfigIni.eInvisible[ i ];
					Bass.bReverse[ i ] = CDTXMania.ConfigIni.bReverse[ i ];
					Bass.eRandom[ i ] = CDTXMania.ConfigIni.eRandom[ i ];
					Bass.bLight[ i ] = CDTXMania.ConfigIni.bLight[ i ];
					Bass.bLeft[ i ] = CDTXMania.ConfigIni.bLeft[ i ];
					Bass.f譜面スクロール速度[ i ] = ( (float) ( CDTXMania.ConfigIni.n譜面スクロール速度[ i ] + 1 ) ) * 0.5f;
				}
				Bass.eDark = CDTXMania.ConfigIni.eDark;
				Bass.n演奏速度分子 = CDTXMania.ConfigIni.n演奏速度;
				Bass.n演奏速度分母 = 20;
				Bass.eHHGroup = CDTXMania.ConfigIni.eHHGroup;
				Bass.eFTGroup = CDTXMania.ConfigIni.eFTGroup;
				Bass.eCYGroup = CDTXMania.ConfigIni.eCYGroup;
				Bass.eHitSoundPriorityHH = CDTXMania.ConfigIni.eHitSoundPriorityHH;
				Bass.eHitSoundPriorityFT = CDTXMania.ConfigIni.eHitSoundPriorityFT;
				Bass.eHitSoundPriorityCY = CDTXMania.ConfigIni.eHitSoundPriorityCY;
				Bass.bGuitar有効 = CDTXMania.ConfigIni.bGuitar有効;
				Bass.bDrums有効 = CDTXMania.ConfigIni.bDrums有効;
				Bass.bSTAGEFAILED有効 = CDTXMania.ConfigIni.bSTAGEFAILED有効;
				Bass.eダメージレベル = CDTXMania.ConfigIni.eダメージレベル;
				Bass.b演奏にキーボードを使用した = this.b演奏にキーボードを使った.Bass;			// #24280 2011.1.29 yyagi
				Bass.b演奏にMIDI入力を使用した = this.b演奏にMIDI入力を使った.Bass;				//
				Bass.b演奏にジョイパッドを使用した = this.b演奏にジョイパッドを使った.Bass;		//
				Bass.b演奏にマウスを使用した = this.b演奏にマウスを使った.Bass;					//
				Bass.nPerfectになる範囲ms = CDTXMania.nPerfect範囲ms;
				Bass.nGreatになる範囲ms = CDTXMania.nGreat範囲ms;
				Bass.nGoodになる範囲ms = CDTXMania.nGood範囲ms;
				Bass.nPoorになる範囲ms = CDTXMania.nPoor範囲ms;
				Bass.strDTXManiaのバージョン = CDTXMania.VERSION;
				Bass.最終更新日時 = DateTime.Now.ToString();
				Bass.Hash = CScoreIni.t演奏セクションのMD5を求めて返す( Bass );
				Bass.bギターとベースを入れ替えた = CDTXMania.ConfigIni.bIsSwappedGuitarBass; // #35417 chnmr0 add
			}
		}
		#endregion

		// CStage 実装

		public override void On活性化()
		{
			listChip = CDTXMania.DTX.listChip;
			listWAV = CDTXMania.DTX.listWAV;

			this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.継続;
			this.n現在のトップChip = ( listChip.Count > 0 ) ? 0 : -1;
			this.L最後に再生したHHの実WAV番号 = new List<int>( 16 );
			this.n最後に再生したHHのチャンネル番号 = 0;
			this.n最後に再生した実WAV番号.Guitar = -1;
			this.n最後に再生した実WAV番号.Bass = -1;
			for ( int i = 0; i < 50; i++ )
			{
				this.n最後に再生したBGMの実WAV番号[ i ] = -1;
			}
			this.r次にくるギターChip = null;
			this.r次にくるベースChip = null;
			for ( int j = 0; j < 10; j++ )
			{
				this.r現在の空うちドラムChip[ j ] = null;
			}
			this.r現在の空うちギターChip = null;
			this.r現在の空うちベースChip = null;
			cInvisibleChip = new CInvisibleChip( CDTXMania.ConfigIni.nDisplayTimesMs, CDTXMania.ConfigIni.nFadeoutTimeMs );
			this.演奏判定ライン座標 = new C演奏判定ライン座標共通();
            this.n最大コンボ数_TargetGhost = new STDGBVALUE<int>(); // #35411 2015.08.21 chnmr0 add
			for ( int k = 0; k < 3; k++ )
			{
				//for ( int n = 0; n < 5; n++ )
				//{
					this.nヒット数_Auto含まない[ k ] = new CHITCOUNTOFRANK();
					this.nヒット数_Auto含む[ k ] = new CHITCOUNTOFRANK();
                    this.nヒット数_TargetGhost[k] = new CHITCOUNTOFRANK(); // #35411 2015.08.21 chnmr0 add
				//}
				this.queWailing[ k ] = new Queue<CDTX.CChip>();
				this.r現在の歓声Chip[ k ] = null;
				cInvisibleChip.eInvisibleMode[ k ] = CDTXMania.ConfigIni.eInvisible[ k ];
				if ( CDTXMania.DTXVmode.Enabled )
				{
					CDTXMania.ConfigIni.n譜面スクロール速度[ k ] = CDTXMania.ConfigIni.nViewerScrollSpeed[ k ];
				}

				this.nInputAdjustTimeMs[ k ] = CDTXMania.ConfigIni.nInputAdjustTimeMs[ k ];			// #23580 2011.1.3 yyagi
																									//        2011.1.7 ikanick 修正
				//this.nJudgeLinePosY_delta[ k ] = CDTXMania.ConfigIni.nJudgeLinePosOffset[ k ];		// #31602 2013.6.23 yyagi

				this.演奏判定ライン座標.n判定位置[ k ] = CDTXMania.ConfigIni.e判定位置[ k ];
                this.演奏判定ライン座標.nJudgeLinePosY[ k ] = CDTXMania.ConfigIni.nJudgeLine[ k ];
				this.演奏判定ライン座標.nJudgeLinePosY_delta[ k ] = CDTXMania.ConfigIni.nJudgeLinePosOffset[ k ];
				this.bReverse[ k ]             = CDTXMania.ConfigIni.bReverse[ k ];					//
			}
			actCombo.演奏判定ライン座標 = 演奏判定ライン座標;
			for ( int i = 0; i < 3; i++ )
			{
				this.b演奏にキーボードを使った[ i ] = false;
				this.b演奏にジョイパッドを使った[ i ] = false;
				this.b演奏にMIDI入力を使った[ i ] = false;
				this.b演奏にマウスを使った[ i ] = false;
			}
			this.bAUTOでないチップが１つでもバーを通過した = false;
			cInvisibleChip.Reset();
			this.tステータスパネルの選択();
			base.On活性化();
			this.tパネル文字列の設定();
			//this.演奏判定ライン座標();

			this.bIsAutoPlay = CDTXMania.ConfigIni.bAutoPlay;									// #24239 2011.1.23 yyagi

			
			//this.bIsAutoPlay.Guitar = CDTXMania.ConfigIni.bギターが全部オートプレイである;
			//this.bIsAutoPlay.Bass = CDTXMania.ConfigIni.bベースが全部オートプレイである;
//			this.nRisky = CDTXMania.ConfigIni.nRisky;											// #23559 2011.7.28 yyagi
			actGauge.Init( CDTXMania.ConfigIni.nRisky );									// #23559 2011.7.28 yyagi
			this.nPolyphonicSounds = CDTXMania.ConfigIni.nPoliphonicSounds;
			e判定表示優先度 = CDTXMania.ConfigIni.e判定表示優先度;

			CDTXMania.Skin.tRemoveMixerAll();	// 効果音のストリームをミキサーから解除しておく

			queueMixerSound = new Queue<stmixer>( 64 );
			bIsDirectSound = ( CDTXMania.Sound管理.GetCurrentSoundDeviceType() == ESoundDeviceType.DirectSound );
			bUseOSTimer = CDTXMania.ConfigIni.bUseOSTimer;
			this.bPAUSE = false;
			if ( CDTXMania.DTXVmode.Enabled )
			{
				db再生速度 = CDTXMania.DTX.dbDTXVPlaySpeed;
				CDTXMania.ConfigIni.n演奏速度 = (int) (CDTXMania.DTX.dbDTXVPlaySpeed * 20 + 0.5 );
			}
			else
			{
				db再生速度 = ( (double) CDTXMania.ConfigIni.n演奏速度 ) / 20.0;
			}
			bValidScore = ( CDTXMania.DTXVmode.Enabled ) ? false : true;

			cWailingChip = new CWailingChip共通[ 3 ];	// 0:未使用, 1:Gutiar, 2:Bass
			if ( CDTXMania.ConfigIni.bDrums有効 )
			{
				cWailingChip[ 1 ] = new CWailngChip_Guitar_Drum画面( ref 演奏判定ライン座標 );
				cWailingChip[ 2 ] = new CWailngChip_Bass_Drum画面( ref 演奏判定ライン座標 );
			}
			else
			{
				cWailingChip[ 1 ] = new CWailngChip_Guitar_GR画面( ref 演奏判定ライン座標 );
				cWailingChip[ 2 ] = new CWailngChip_Bass_GR画面( ref 演奏判定ライン座標 );
			}

			#region [ 演奏開始前にmixer登録しておくべきサウンド(開幕してすぐに鳴らすことになるチップ音)を登録しておく ]
			foreach ( CDTX.CChip pChip in listChip )
			{
//				Debug.WriteLine( "CH=" + pChip.nチャンネル番号.ToString( "x2" ) + ", 整数値=" + pChip.n整数値 +  ", time=" + pChip.n発声時刻ms );
				if ( pChip.n発声時刻ms <= 0 )
				{
					if ( pChip.nチャンネル番号 == 0xDA )
					{
						pChip.bHit = true;
//						Trace.TraceInformation( "first [DA] BAR=" + pChip.n発声位置 / 384 + " ch=" + pChip.nチャンネル番号.ToString( "x2" ) + ", wav=" + pChip.n整数値 + ", time=" + pChip.n発声時刻ms );
						if ( listWAV.ContainsKey( pChip.n整数値_内部番号 ) )
						{
							CDTX.CWAV wc = listWAV[ pChip.n整数値_内部番号 ];
							for ( int i = 0; i < nPolyphonicSounds; i++ )
							{
								if ( wc.rSound[ i ] != null )
								{
									CDTXMania.Sound管理.AddMixer( wc.rSound[ i ], db再生速度, pChip.b演奏終了後も再生が続くチップである );
									//AddMixer( wc.rSound[ i ] );		// 最初はqueueを介さず直接ミキサー登録する
								}
							}
						}
					}
				}
				else
				{
					break;
				}
			}
			#endregion

			if ( CDTXMania.ConfigIni.bIsSwappedGuitarBass )	// #24063 2011.1.24 yyagi Gt/Bsの譜面情報入れ替え
			{
				CDTXMania.DTX.SwapGuitarBassInfos();
			}
			this.sw = new Stopwatch();
#if DEBUG
			this.sw2 = new Stopwatch();
#endif
//			this.gclatencymode = GCSettings.LatencyMode;
//			GCSettings.LatencyMode = GCLatencyMode.Batch;	// 演奏画面中はGCを抑止する
		}
		public override void On非活性化()
		{
			this.L最後に再生したHHの実WAV番号.Clear();	// #23921 2011.1.4 yyagi
			this.L最後に再生したHHの実WAV番号 = null;	//
			for ( int i = 0; i < 3; i++ )
			{
				this.queWailing[ i ].Clear();
				this.queWailing[ i ] = null;
			}
			this.ctWailingチップ模様アニメ = null;
			this.ctチップ模様アニメ.Drums = null;
			this.ctチップ模様アニメ.Guitar = null;
			this.ctチップ模様アニメ.Bass = null;
			//listWAV.Clear();
			listWAV = null;
			listChip = null;
			queueMixerSound.Clear();
			queueMixerSound = null;
			cInvisibleChip.Dispose();
			cInvisibleChip = null;
//			GCSettings.LatencyMode = this.gclatencymode;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if ( !base.b活性化してない )
			{
				this.t背景テクスチャの生成();

				this.txWailing枠 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Wailing cursor.png" ) );

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if ( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx背景 );

				CDTXMania.tテクスチャの解放( ref this.txWailing枠 );
				base.OnManagedリソースの解放();
			}
		}

		// その他

		#region [ protected ]
		//-----------------
		public class CHITCOUNTOFRANK
		{
			// Fields
			public int Good;
			public int Great;
			public int Miss;
			public int Perfect;
			public int Poor;
            public int XPerfect;

			// Properties
			public int this[ int index ]
			{
				get
				{
					switch ( index )
					{
						case 0:
							return this.Perfect;

						case 1:
							return this.Great;

						case 2:
							return this.Good;

						case 3:
							return this.Poor;

						case 4:
							return this.Miss;

                        case 6:
                            if( false )
                                return this.XPerfect;
                            return this.Perfect;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch ( index )
					{
						case 0:
							this.Perfect = value;
							return;

						case 1:
							this.Great = value;
							return;

						case 2:
							this.Good = value;
							return;

						case 3:
							this.Poor = value;
							return;

						case 4:
							this.Miss = value;
							return;

						case 6:
                            if( false )
							    this.XPerfect = value;
                            else
                                this.Perfect = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}

		[StructLayout( LayoutKind.Sequential )]
		protected struct STKARAUCHI
		{
			public CDTX.CChip HH;
			public CDTX.CChip SD;
			public CDTX.CChip BD;
			public CDTX.CChip HT;
			public CDTX.CChip LT;
			public CDTX.CChip FT;
			public CDTX.CChip CY;
			public CDTX.CChip HHO;
			public CDTX.CChip RD;
			public CDTX.CChip LC;
            public CDTX.CChip LP;
            public CDTX.CChip LBD;
			public CDTX.CChip this[ int index ]
			{
				get
				{
					switch ( index )
					{
						case 0:
							return this.HH;

						case 1:
							return this.SD;

						case 2:
							return this.BD;

						case 3:
							return this.HT;

						case 4:
							return this.LT;

						case 5:
							return this.FT;

						case 6:
							return this.CY;

						case 7:
							return this.HHO;

						case 8:
							return this.RD;

						case 9:
							return this.LC;

                        case 10:
                            return this.LP;

                        case 11:
                            return this.LBD;

					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch ( index )
					{
						case 0:
							this.HH = value;
							return;

						case 1:
							this.SD = value;
							return;

						case 2:
							this.BD = value;
							return;

						case 3:
							this.HT = value;
							return;

						case 4:
							this.LT = value;
							return;

						case 5:
							this.FT = value;
							return;

						case 6:
							this.CY = value;
							return;

						case 7:
							this.HHO = value;
							return;

						case 8:
							this.RD = value;
							return;

						case 9:
							this.LC = value;
							return;

                        case 10:
                            this.LP = value;
                            return;

                        case 11:
                            this.LBD = value;
                            return;

					}
					throw new IndexOutOfRangeException();
				}
			}
		}

		protected struct stmixer
		{
			internal bool bIsAdd;
			internal CSound csound;
			internal bool b演奏終了後も再生が続くチップである;
		};

		protected CAct演奏AVI actAVI;
		protected CAct演奏BGA actBGA;

		protected CAct演奏チップファイアGB actChipFireGB;
		protected CAct演奏Combo共通 actCombo;
		protected CAct演奏Danger共通 actDANGER;
		protected CActFIFOBlack actFI;
		protected CActFIFOBlack actFO;
		protected CActFIFOWhite actFOClear;
		public CAct演奏ゲージ共通 actGauge;
        public CAct演奏BPMバー共通 actBPMBar;
        protected CAct演奏グラフ actGraph;
		protected CAct演奏判定文字列共通 actJudgeString;
		protected CAct演奏DrumsレーンフラッシュD actLaneFlushD;
		protected CAct演奏レーンフラッシュGB共通 actLaneFlushGB;
		protected CAct演奏パネル文字列 actPanel;
		protected CAct演奏演奏情報 actPlayInfo;
		protected CAct演奏RGB共通 actRGB;
		public CAct演奏スコア共通 actScore;
        protected CAct演奏シャッター actShutter;
		protected CAct演奏ステージ失敗 actStageFailed;
		protected CAct演奏ステータスパネル共通 actStatusPanels;
		protected CAct演奏WailingBonus共通 actWailingBonus;
        protected CAct演奏クリアバー共通 actClearBar;
		protected CAct演奏スクロール速度 act譜面スクロール速度;
		public    C演奏判定ライン座標共通 演奏判定ライン座標;
		protected bool bPAUSE;
		protected STDGBVALUE<bool> b演奏にMIDI入力を使った;
		protected STDGBVALUE<bool> b演奏にキーボードを使った;
		protected STDGBVALUE<bool> b演奏にジョイパッドを使った;
		protected STDGBVALUE<bool> b演奏にマウスを使った;
		protected CCounter ctWailingチップ模様アニメ;
		protected STDGBVALUE<CCounter> ctチップ模様アニメ;

		protected E演奏画面の戻り値 eフェードアウト完了時の戻り値;
		protected readonly int[,] nBGAスコープチャンネルマップ = new int[ , ] { { 0xc4, 0xc7, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xe0 }, { 4, 7, 0x55, 0x56, 0x57, 0x58, 0x59, 0x60 } };
        protected readonly int[] nチャンネル0Atoパッド08 = new int[] { 1, 2, 3, 4, 5, 7, 6, 1, 8, 0, 9, 9 };
        protected readonly int[] nチャンネル0Atoレーン07 = new int[] { 1, 2, 3, 4, 5, 7, 6, 1, 9, 0, 8, 8 };
                                                                  //                         RD LC  LP  RD
		protected readonly int[] nパッド0Atoチャンネル0A = new int[] { 0x11, 0x12, 0x13, 0x14, 0x15, 0x17, 0x16, 0x18, 0x19, 0x1a, 0x1b, 0x1c };
        protected readonly int[] nパッド0Atoパッド08 = new int[] { 1, 2, 3, 4, 5, 6, 7, 1, 8, 0, 9, 9 };// パッド画像のヒット処理用
                                                             //   HH SD BD HT LT FT CY HHO RD LC LP LBD
        protected readonly int[] nパッド0Atoレーン07 = new int[] { 1, 2, 3, 4, 5, 6, 7, 1, 9, 0, 8, 8 };
		public STDGBVALUE<CHITCOUNTOFRANK> nヒット数_Auto含まない;
		protected STDGBVALUE<CHITCOUNTOFRANK> nヒット数_Auto含む;
        protected STDGBVALUE<CHITCOUNTOFRANK> nヒット数_TargetGhost; // #35411 2015.08.21 chnmr0 add
        protected STDGBVALUE<int> nコンボ数_TargetGhost;
        protected STDGBVALUE<int> n最大コンボ数_TargetGhost;
		protected int n現在のトップChip = -1;
		protected int[] n最後に再生したBGMの実WAV番号 = new int[ 50 ];
		protected int n最後に再生したHHのチャンネル番号;
		protected List<int> L最後に再生したHHの実WAV番号;		// #23921 2011.1.4 yyagi: change "int" to "List<int>", for recording multiple wav No.
		protected STLANEVALUE<int> n最後に再生した実WAV番号;	// #26388 2011.11.8 yyagi: change "n最後に再生した実WAV番号.GUITAR" and "n最後に再生した実WAV番号.BASS"
																//							into "n最後に再生した実WAV番号";
//		protected int n最後に再生した実WAV番号.GUITAR;
//		protected int n最後に再生した実WAV番号.BASS;

		protected volatile Queue<stmixer> queueMixerSound;		// #24820 2013.1.21 yyagi まずは単純にAdd/Removeを1個のキューでまとめて管理するやり方で設計する
		protected DateTime dtLastQueueOperation;				//
		protected bool bIsDirectSound;							//
		protected double db再生速度;
		protected bool bValidScore;
//		protected bool bDTXVmode;
//		protected STDGBVALUE<int> nJudgeLinePosY_delta;			// #31602 2013.6.23 yyagi 表示遅延対策として、判定ラインの表示位置をずらす機能を追加する
		protected STDGBVALUE<bool> bReverse;

		protected STDGBVALUE<Queue<CDTX.CChip>> queWailing;
		protected STDGBVALUE<CDTX.CChip> r現在の歓声Chip;
		protected CDTX.CChip r現在の空うちギターChip;
		protected STKARAUCHI r現在の空うちドラムChip;
		protected CDTX.CChip r現在の空うちベースChip;
		protected CDTX.CChip r次にくるギターChip;
		protected CDTX.CChip r次にくるベースChip;
		protected CTexture txWailing枠;
		protected CTexture txチップ;
		protected CTexture txヒットバー;

		protected CTexture tx背景;

		protected STDGBVALUE<int> nInputAdjustTimeMs;		// #23580 2011.1.3 yyagi
		protected STAUTOPLAY bIsAutoPlay;		// #24239 2011.1.23 yyagi
//		protected int nRisky_InitialVar, nRiskyTime;		// #23559 2011.7.28 yyagi → CAct演奏ゲージ共通クラスに隠蔽
		protected int nPolyphonicSounds;
		protected List<CDTX.CChip> listChip;
		protected Dictionary<int, CDTX.CWAV> listWAV;
		protected CInvisibleChip cInvisibleChip;
		protected bool bUseOSTimer;
		protected E判定表示優先度 e判定表示優先度;
		protected CWailingChip共通[] cWailingChip;
        
		protected Stopwatch sw;		// 2011.6.13 最適化検討用のストップウォッチ
#if DEBUG
		protected Stopwatch sw2;
#endif
//		protected GCLatencyMode gclatencymode;

        public CCounter ctコンボ動作タイマ;

		public void AddMixer( CSound cs, bool _b演奏終了後も再生が続くチップである )
		{
			stmixer stm = new stmixer()
			{
				bIsAdd = true,
				csound = cs,
				b演奏終了後も再生が続くチップである = _b演奏終了後も再生が続くチップである
			};
			queueMixerSound.Enqueue( stm );
//		Debug.WriteLine( "★Queue: add " + Path.GetFileName( stm.csound.strファイル名 ));
		}
		public void RemoveMixer( CSound cs )
		{
			stmixer stm = new stmixer()
			{
				bIsAdd = false,
				csound = cs,
				b演奏終了後も再生が続くチップである = false
			};
			queueMixerSound.Enqueue( stm );
//		Debug.WriteLine( "★Queue: remove " + Path.GetFileName( stm.csound.strファイル名 ));
		}
		public void ManageMixerQueue()
		{
			// もしサウンドの登録/削除が必要なら、実行する
			if ( queueMixerSound.Count > 0 )
			{
				//Debug.WriteLine( "☆queueLength=" + queueMixerSound.Count );
				DateTime dtnow = DateTime.Now;
				TimeSpan ts = dtnow - dtLastQueueOperation;
				if ( ts.Milliseconds > 7 )
				{
					for ( int i = 0; i < 2 && queueMixerSound.Count > 0; i++ )
					{
						dtLastQueueOperation = dtnow;
						stmixer stm = queueMixerSound.Dequeue();
						if ( stm.bIsAdd )
						{
							CDTXMania.Sound管理.AddMixer( stm.csound, db再生速度, stm.b演奏終了後も再生が続くチップである );
						}
						else
						{
							CDTXMania.Sound管理.RemoveMixer( stm.csound );
						}
					}
				}
			}
		}

		/// <summary>
		/// 演奏開始前に適切なサイズのAVIテクスチャを作成しておくことで、AVI再生開始時のもたつきをなくす
		/// </summary>
		protected void PrepareAVITexture()
		{
			if ( CDTXMania.ConfigIni.bAVI有効 )
			{
				foreach ( CDTX.CChip pChip in listChip )
				{
					if ( pChip.nチャンネル番号 == (int) Ech定義.Movie || pChip.nチャンネル番号 == (int) Ech定義.MovieFull )
					{
						// 最初に再生するAVIチップに合わせて、テクスチャを準備しておく
						if (pChip.rAVI != null )
						{
							this.actAVI.PrepareProperSizeTexture( (int) pChip.rAVI.avi.nフレーム幅, (int) pChip.rAVI.avi.nフレーム高さ );
						}
						break;
					}
				}
			}
		}

		protected E判定 e指定時刻からChipのJUDGEを返す( long nTime, CDTX.CChip pChip, int nInputAdjustTime, bool saveLag = true )
		{
			if ( pChip != null )
			{
                // #35411 2015.08.22 chnmr0 modified add check save lag flag for ghost
                int lag = (int)(nTime + nInputAdjustTime - pChip.n発声時刻ms);
                if (saveLag)
                {
                    pChip.nLag = lag;       // #23580 2011.1.3 yyagi: add "nInputAdjustTime" to add input timing adjust feature
					if (pChip.e楽器パート != E楽器パート.UNKNOWN)
					{
						pChip.nCurrentComboForGhost = this.actCombo.n現在のコンボ数[(int)pChip.e楽器パート];
					}
                }
                // #35411 modify end

				int nDeltaTime = Math.Abs( lag );
				//Debug.WriteLine("nAbsTime=" + (nTime - pChip.n発声時刻ms) + ", nDeltaTime=" + (nTime + nInputAdjustTime - pChip.n発声時刻ms));
                if( ( nDeltaTime <= CDTXMania.nPerfect範囲ms / 2 ) && CDTXMania.ConfigIni.bXPerfect判定を有効にする )
                {
                    return E判定.XPerfect;
                }
				if ( nDeltaTime <= CDTXMania.nPerfect範囲ms )
				{
					return E判定.Perfect;
				}
				if ( nDeltaTime <= CDTXMania.nGreat範囲ms )
				{
					return E判定.Great;
				}
				if ( nDeltaTime <= CDTXMania.nGood範囲ms )
				{
					return E判定.Good;
				}
				if ( nDeltaTime <= CDTXMania.nPoor範囲ms )
				{
					return E判定.Poor;
				}
			}
			return E判定.Miss;
		}
		protected CDTX.CChip r空うちChip( E楽器パート part, Eパッド pad )
		{
			switch ( part )
			{
				case E楽器パート.DRUMS:
					switch ( pad )
					{
						case Eパッド.HH:
							if ( this.r現在の空うちドラムChip.HH != null )
							{
								return this.r現在の空うちドラムChip.HH;
							}
							if ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.ハイハットのみ打ち分ける )
							{
								if ( CDTXMania.ConfigIni.eHHGroup == EHHGroup.左シンバルのみ打ち分ける )
								{
									return this.r現在の空うちドラムChip.HHO;
								}
								if ( this.r現在の空うちドラムChip.HHO != null )
								{
									return this.r現在の空うちドラムChip.HHO;
								}
							}
							return this.r現在の空うちドラムChip.LC;

						case Eパッド.SD:
							return this.r現在の空うちドラムChip.SD;

						case Eパッド.BD:
							return this.r現在の空うちドラムChip.BD;

						case Eパッド.HT:
							return this.r現在の空うちドラムChip.HT;

						case Eパッド.LT:
							if ( this.r現在の空うちドラムChip.LT != null )
							{
								return this.r現在の空うちドラムChip.LT;
							}
							if ( CDTXMania.ConfigIni.eFTGroup == EFTGroup.共通 )
							{
								return this.r現在の空うちドラムChip.FT;
							}
							return null;

						case Eパッド.FT:
							if ( this.r現在の空うちドラムChip.FT != null )
							{
								return this.r現在の空うちドラムChip.FT;
							}
							if ( CDTXMania.ConfigIni.eFTGroup == EFTGroup.共通 )
							{
								return this.r現在の空うちドラムChip.LT;
							}
							return null;

						case Eパッド.CY:
							if ( this.r現在の空うちドラムChip.CY != null )
							{
								return this.r現在の空うちドラムChip.CY;
							}
							if ( CDTXMania.ConfigIni.eCYGroup == ECYGroup.共通 )
							{
								return this.r現在の空うちドラムChip.RD;
							}
							return null;

						case Eパッド.HHO:
							if ( this.r現在の空うちドラムChip.HHO != null )
							{
								return this.r現在の空うちドラムChip.HHO;
							}
							if ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.ハイハットのみ打ち分ける )
							{
								if ( CDTXMania.ConfigIni.eHHGroup == EHHGroup.左シンバルのみ打ち分ける )
								{
									return this.r現在の空うちドラムChip.HH;
								}
								if ( this.r現在の空うちドラムChip.HH != null )
								{
									return this.r現在の空うちドラムChip.HH;
								}
							}
							return this.r現在の空うちドラムChip.LC;

						case Eパッド.RD:
							if ( this.r現在の空うちドラムChip.RD != null )
							{
								return this.r現在の空うちドラムChip.RD;
							}
							if ( CDTXMania.ConfigIni.eCYGroup == ECYGroup.共通 )
							{
								return this.r現在の空うちドラムChip.CY;
							}
							return null;

						case Eパッド.LC:
							if ( this.r現在の空うちドラムChip.LC != null )
							{
								return this.r現在の空うちドラムChip.LC;
							}
							if ( ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.ハイハットのみ打ち分ける ) && ( CDTXMania.ConfigIni.eHHGroup != EHHGroup.全部共通 ) )
							{
								return null;
							}
							if ( this.r現在の空うちドラムChip.HH != null )
							{
								return this.r現在の空うちドラムChip.HH;
							}
							return this.r現在の空うちドラムChip.HHO;

                        case Eパッド.LP:
                            if ( this.r現在の空うちドラムChip.LP != null )
                            {
                                return this.r現在の空うちドラムChip.LP;
                            }
                            if( CDTXMania.ConfigIni.eBDGroup != EBDGroup.左右ペダルのみ打ち分ける )
                            {
                                if( this.r現在の空うちドラムChip.LBD != null )
                                {
                                    return this.r現在の空うちドラムChip.LBD;
                                }
                            }
                            return this.r現在の空うちドラムChip.LP;

                        case Eパッド.LBD:
                            if( this.r現在の空うちドラムChip.LBD != null )
                            {
                                return this.r現在の空うちドラムChip.LBD;
                            }
                            if( CDTXMania.ConfigIni.eBDGroup != EBDGroup.左右ペダルのみ打ち分ける )
                            {
                                if (this.r現在の空うちドラムChip.LP != null)
                                {
                                    return this.r現在の空うちドラムChip.LP;
                                }
                            }
                            return this.r現在の空うちドラムChip.LBD;
					}
					break;

				case E楽器パート.GUITAR:
					return this.r現在の空うちギターChip;

				case E楽器パート.BASS:
					return this.r現在の空うちベースChip;
			}
			return null;
		}
		protected CDTX.CChip r指定時刻に一番近いChip_ヒット未済問わず不可視考慮( long nTime, int nChannel, int nInputAdjustTime )
		{
#if DEBUG
			sw2.Start();
#endif
//Trace.TraceInformation( "NTime={0}, nChannel={1:x2}", nTime, nChannel );
			nTime += nInputAdjustTime;						// #24239 2011.1.23 yyagi InputAdjust

			int nIndex_InitialPositionSearchingToPast;
			if ( this.n現在のトップChip == -1 )				// 演奏データとして1個もチップがない場合は
			{
#if DEBUG
				sw2.Stop();
#endif
				return null;
			}
			int count = listChip.Count;
			int nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToPast = this.n現在のトップChip;
			if ( this.n現在のトップChip >= count )			// その時点で演奏すべきチップが既に全部無くなっていたら
			{
				nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToPast = count - 1;
			}
			//int nIndex_NearestChip_Future;	// = nIndex_InitialPositionSearchingToFuture;
			//while ( nIndex_NearestChip_Future < count )		// 未来方向への検索
			for ( ; nIndex_NearestChip_Future < count; nIndex_NearestChip_Future++)
			{
				CDTX.CChip chip = listChip[ nIndex_NearestChip_Future ];
				if ( chip.b空打ちチップである )
				{
					continue;
				}
				if ( ( ( 0x11 <= nChannel ) && ( nChannel <= 0x1C ) ) )
				{
					if ( ( chip.nチャンネル番号 == nChannel ) || ( chip.nチャンネル番号 == ( nChannel + 0x20 ) ) )
					{
						if ( chip.n発声時刻ms > nTime )
						{
							break;
						}
						nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
					}
					continue;	// ほんの僅かながら高速化
				}
				else if ( ( ( nChannel == 0x2f ) && ( chip.e楽器パート == E楽器パート.GUITAR ) ) || ( ( ( 0x20 <= nChannel ) && ( nChannel <= 0x28 ) ) && ( chip.nチャンネル番号 == nChannel ) ) )
				{
					if ( chip.n発声時刻ms > nTime )
					{
						break;
					}
					nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
				}
				else if ( ( ( nChannel == 0xaf ) && ( chip.e楽器パート == E楽器パート.BASS ) ) || ( ( ( 0xa0 <= nChannel ) && ( nChannel <= 0xa8 ) ) && ( chip.nチャンネル番号 == nChannel ) ) )
				{
					if ( chip.n発声時刻ms > nTime )
					{
						break;
					}
					nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
				}
				// nIndex_NearestChip_Future++;
			}
			int nIndex_NearestChip_Past = nIndex_InitialPositionSearchingToPast;
			//while ( nIndex_NearestChip_Past >= 0 )			// 過去方向への検索
			for ( ; nIndex_NearestChip_Past >= 0; nIndex_NearestChip_Past-- )
			{
				CDTX.CChip chip = listChip[ nIndex_NearestChip_Past ];
				if ( chip.b空打ちチップである )
				{
					continue;
				}
				if ( ( 0x11 <= nChannel ) && ( nChannel <= 0x1C ) )
				{
					if ( ( chip.nチャンネル番号 == nChannel ) || ( chip.nチャンネル番号 == ( nChannel + 0x20 ) ) )
					{
						break;
					}
				}
				else if ( ( ( nChannel == 0x2f ) && ( chip.e楽器パート == E楽器パート.GUITAR ) ) || ( ( ( 0x20 <= nChannel ) && ( nChannel <= 0x28 ) ) && ( chip.nチャンネル番号 == nChannel ) ) )
				{
					if ( ( 0x20 <= chip.nチャンネル番号 ) && ( chip.nチャンネル番号 <= 0x28 ) )
					{
						break;
					}
				}
				else if ( ( ( ( nChannel == 0xaf ) && ( chip.e楽器パート == E楽器パート.BASS ) ) || ( ( ( 0xa0 <= nChannel ) && ( nChannel <= 0xa8 ) ) && ( chip.nチャンネル番号 == nChannel ) ) )
					&& ( ( 0xa0 <= chip.nチャンネル番号 ) && ( chip.nチャンネル番号 <= 0xa8 ) ) )
				{
					break;
				}
				// nIndex_NearestChip_Past--;
			}

			if ( nIndex_NearestChip_Future >= count )
			{
				if ( nIndex_NearestChip_Past < 0 )	// 検索対象が過去未来どちらにも見つからなかった場合
				{
					return null;
				}
				else 								// 検索対象が未来方向には見つからなかった(しかし過去方向には見つかった)場合
				{
#if DEBUG
					sw2.Stop();
#endif
					return listChip[ nIndex_NearestChip_Past ];
				}
			}
			else if ( nIndex_NearestChip_Past < 0 )	// 検索対象が過去方向には見つからなかった(しかし未来方向には見つかった)場合
			{
#if DEBUG
				sw2.Stop();
#endif
				return listChip[ nIndex_NearestChip_Future ];
			}
													// 検索対象が過去未来の双方に見つかったなら、より近い方を採用する
			CDTX.CChip nearestChip_Future = listChip[ nIndex_NearestChip_Future ];
			CDTX.CChip nearestChip_Past   = listChip[ nIndex_NearestChip_Past ];
			int nDiffTime_Future = Math.Abs( (int) ( nTime - nearestChip_Future.n発声時刻ms ) );
			int nDiffTime_Past   = Math.Abs( (int) ( nTime - nearestChip_Past.n発声時刻ms ) );
			if ( nDiffTime_Future >= nDiffTime_Past )
			{
#if DEBUG
				sw2.Stop();
#endif
				return nearestChip_Past;
			}
#if DEBUG
			sw2.Stop();
#endif
			return nearestChip_Future;
		}
		protected void tサウンド再生( CDTX.CChip rChip, long n再生開始システム時刻ms, E楽器パート part )
		{
			this.tサウンド再生( rChip, n再生開始システム時刻ms, part, CDTXMania.ConfigIni.n手動再生音量, false, false );
		}
		protected void tサウンド再生( CDTX.CChip rChip, long n再生開始システム時刻ms, E楽器パート part, int n音量 )
		{
			this.tサウンド再生( rChip, n再生開始システム時刻ms, part, n音量, false, false );
		}
		protected void tサウンド再生( CDTX.CChip rChip, long n再生開始システム時刻ms, E楽器パート part, int n音量, bool bモニタ )
		{
			this.tサウンド再生( rChip, n再生開始システム時刻ms, part, n音量, bモニタ, false );
		}
		protected void tサウンド再生( CDTX.CChip pChip, long n再生開始システム時刻ms, E楽器パート part, int n音量, bool bモニタ, bool b音程をずらして再生 )
		{
			// mute sound (auto)
			// 4A: HH
			// 4B: CY
			// 4C: RD
			// 4D: LC
			// 2A: Gt
			// AA: Bs
			//

			if ( pChip != null )
			{
				bool overwrite = false;
				switch ( part )
				{
					case E楽器パート.DRUMS:
					#region [ DRUMS ]
						{
							int index = pChip.nチャンネル番号;
							if ( ( 0x11 <= index ) && ( index <= 0x1C ) )
							{
								index -= 0x11;
							}
							else if ( ( 0x31 <= index ) && ( index <= 0x3a ) )
							{
								index -= 0x31;
							}
							// mute sound (auto)
							// 4A: 84: HH (HO/HC)
							// 4B: 85: CY
							// 4C: 86: RD
							// 4D: 87: LC
							// 2A: 88: Gt
							// AA: 89: Bs
							else if ( 0x84 == index )	// 仮に今だけ追加 HHは消音処理があるので overwriteフラグ系の処理は改めて不要
							{
								index = 0;
							}
							else if ( ( 0x85 <= index ) && ( index <= 0x87 ) )	// 仮に今だけ追加
							{
								//            CY    RD    LC
								int[] ch = { 0x16, 0x19, 0x1A };
								pChip.nチャンネル番号 = ch[ pChip.nチャンネル番号 - 0x85 ];
								index = pChip.nチャンネル番号 - 0x11;
								overwrite = true;
							}
							else
							{
								return;
							}
							int nLane = this.nチャンネル0Atoレーン07[ index ];
							if ( ( nLane == 1 &&	// 今回演奏するのがHC or HO
                               index == 0 ) && (this.n最後に再生したHHのチャンネル番号 != 0x18 && this.n最後に再生したHHのチャンネル番号 != 0x38) || (((nLane == 8) && ((index == 10) && (this.n最後に再生したHHのチャンネル番号 != 0x18))) && (this.n最後に再生したHHのチャンネル番号 != 0x38))
								// HCを演奏するか、またはHO演奏＆以前HO演奏でない＆以前不可視HO演奏でない
							)
							// #24772 2011.4.4 yyagi
							// == HH mute condition == 
							//			current HH		So, the mute logics are:
							//				HC	HO		1) All played HC/HOs should be queueing
							// last HH	HC  Yes	Yes		2) If you aren't in "both current/last HH are HO", queued HH should be muted.
							//			HO	Yes	No
							{
								// #23921 2011.1.4 yyagi: 2種類以上のオープンハイハットが発音済みだと、最後のHHOしか消せない問題に対応。
#if TEST_NOTEOFFMODE	// 2011.1.1 yyagi test
								if (CDTXMania.DTX.b演奏で直前の音を消音する.HH)
								{
#endif
								for ( int i = 0; i < this.L最後に再生したHHの実WAV番号.Count; i++ )		// #23921 2011.1.4 yyagi
								{
									// CDTXMania.DTX.tWavの再生停止(this.L最後に再生したHHの実WAV番号);
									CDTXMania.DTX.tWavの再生停止( this.L最後に再生したHHの実WAV番号[ i ] );	// #23921 yyagi ストック分全て消音する
								}
								this.L最後に再生したHHの実WAV番号.Clear();
#if TEST_NOTEOFFMODE	// 2011.1.1 yyagi test
								}
#endif
								//this.n最後に再生したHHの実WAV番号 = pChip.n整数値_内部番号;
								this.n最後に再生したHHのチャンネル番号 = pChip.nチャンネル番号;
							}
#if TEST_NOTEOFFMODE	// 2011.1.4 yyagi test
							if (CDTXMania.DTX.b演奏で直前の音を消音する.HH)
							{
#endif
							if ( index == 0 || index == 7 || index == 0x20 || index == 0x27 )			// #23921 HOまたは不可視HO演奏時はそのチップ番号をストックしておく
							{																			// #24772 HC, 不可視HCも消音キューに追加
								if ( this.L最後に再生したHHの実WAV番号.Count >= 16 )	// #23921 ただしストック数が16以上になるようなら、頭の1個を削って常に16未満に抑える
								{													// (ストックが増えてList<>のrealloc()が発生するのを予防する)
									this.L最後に再生したHHの実WAV番号.RemoveAt( 0 );
								}
								if ( !this.L最後に再生したHHの実WAV番号.Contains( pChip.n整数値_内部番号 ) )	// チップ音がまだストックされてなければ
								{
									this.L最後に再生したHHの実WAV番号.Add( pChip.n整数値_内部番号 );			// ストックする
								}
							}
#if TEST_NOTEOFFMODE	// 2011.1.4 yyagi test
							}
#endif
							if ( overwrite )
							{
								CDTXMania.DTX.tWavの再生停止( this.n最後に再生した実WAV番号[index] );
							}
							CDTXMania.DTX.tチップの再生( pChip, n再生開始システム時刻ms, nLane, n音量, bモニタ );
							this.n最後に再生した実WAV番号[ nLane ] = pChip.n整数値_内部番号;		// nLaneでなくindexにすると、LC(1A-11=09)とギター(enumで09)がかぶってLC音が消されるので注意
							return;
						}
					#endregion
					case E楽器パート.GUITAR:
					#region [ GUITAR ]
#if TEST_NOTEOFFMODE	// 2011.1.1 yyagi test
						if (CDTXMania.DTX.b演奏で直前の音を消音する.Guitar) {
#endif
						CDTXMania.DTX.tWavの再生停止( this.n最後に再生した実WAV番号.Guitar );
#if TEST_NOTEOFFMODE
						}
#endif
						CDTXMania.DTX.tチップの再生( pChip, n再生開始システム時刻ms, (int) Eレーン.Guitar, n音量, bモニタ, b音程をずらして再生 );
						this.n最後に再生した実WAV番号.Guitar = pChip.n整数値_内部番号;
						return;
					#endregion
					case E楽器パート.BASS:
					#region [ BASS ]
#if TEST_NOTEOFFMODE
						if (CDTXMania.DTX.b演奏で直前の音を消音する.Bass) {
#endif
						CDTXMania.DTX.tWavの再生停止( this.n最後に再生した実WAV番号.Bass );
#if TEST_NOTEOFFMODE
						}
#endif
						CDTXMania.DTX.tチップの再生( pChip, n再生開始システム時刻ms, (int) Eレーン.Bass, n音量, bモニタ, b音程をずらして再生 );
						this.n最後に再生した実WAV番号.Bass = pChip.n整数値_内部番号;
						return;
					#endregion

					default:
						break;
				}
			}
		}
        protected void tステータスパネルの選択()
        {
            if( CDTXMania.bXGRelease )
            {
                if( CDTXMania.bコンパクトモード )
                {
                    this.actStatusPanels.tスクリプトから難易度ラベルを取得する( "DTX" );
                }
                else if( CDTXMania.stage選曲XG.r確定された曲 != null )
                {
                    this.actStatusPanels.tスクリプトから難易度ラベルを取得する( CDTXMania.stage選曲XG.r確定された曲.ar難易度ラベル[ CDTXMania.stage選曲XG.n確定された曲の難易度 ] );
                }
            }
            else
            {
                if( CDTXMania.bコンパクトモード )
                {
                    this.actStatusPanels.tスクリプトから難易度ラベルを取得する( "DTX" );
                }
                else if( CDTXMania.stage選曲GITADORA.r確定された曲 != null )
                {
                    this.actStatusPanels.tスクリプトから難易度ラベルを取得する( CDTXMania.stage選曲GITADORA.r確定された曲.ar難易度ラベル[ CDTXMania.stage選曲GITADORA.n確定された曲の難易度 ] );
                }
            }

        }
		protected E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip )
		{
			return tチップのヒット処理( nHitTime, pChip, true );
		}
		protected abstract E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip, bool bCorrectLane );
		protected E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip, E楽器パート screenmode )		// E楽器パート screenmode
		{
			return tチップのヒット処理( nHitTime, pChip, screenmode, true );
		}
		protected E判定 tチップのヒット処理( long nHitTime, CDTX.CChip pChip, E楽器パート screenmode, bool bCorrectLane )
		{
			pChip.bHit = true;
#region [メソッド化する前の記述(注釈化)]
//            bool bPChipIsAutoPlay = false;
//            bool bGtBsR = ( ( pChip.nチャンネル番号 & 4 ) > 0 );
//            bool bGtBsG = ( ( pChip.nチャンネル番号 & 2 ) > 0 );
//            bool bGtBsB = ( ( pChip.nチャンネル番号 & 1 ) > 0 );
//            bool bGtBsW = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x08 );
//            bool bGtBsO = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x00 );
//            if ( pChip.e楽器パート == E楽器パート.DRUMS )
//            {
//                if ( bIsAutoPlay[ this.nチャンネル0Atoレーン07[ pChip.nチャンネル番号 - 0x11 ] ] )
//                {
//                    bPChipIsAutoPlay = true;
//                }
//            }
//            else if ( pChip.e楽器パート == E楽器パート.GUITAR )
//            {
////Trace.TraceInformation( "chip:{0}{1}{2} ", bGtBsR, bGtBsG, bGtBsB );
////Trace.TraceInformation( "auto:{0}{1}{2} ", bIsAutoPlay[ (int) Eレーン.GtR ], bIsAutoPlay[ (int) Eレーン.GtG ], bIsAutoPlay[ (int) Eレーン.GtB ]);
//                bPChipIsAutoPlay = true;
//                if ( !bIsAutoPlay[ (int) Eレーン.GtPick ] ) bPChipIsAutoPlay = false;
//                else
//                {
//                    if ( bGtBsR  && !bIsAutoPlay[ (int) Eレーン.GtR ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsG && !bIsAutoPlay[ (int) Eレーン.GtG ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsB && !bIsAutoPlay[ (int) Eレーン.GtB ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsW && !bIsAutoPlay[ (int) Eレーン.GtW ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsO &&
//                        ( !bIsAutoPlay[ (int) Eレーン.GtR] || !bIsAutoPlay[ (int) Eレーン.GtG] || !bIsAutoPlay[ (int) Eレーン.GtB] ) )
//                        bPChipIsAutoPlay = false;
//                }
//            }
//            else if ( pChip.e楽器パート == E楽器パート.BASS )
//            {
//                bPChipIsAutoPlay = true;
//                if ( !bIsAutoPlay[ (int) Eレーン.BsPick ] ) bPChipIsAutoPlay = false;
//                else
//                {
//                    if ( bGtBsR && !bIsAutoPlay[ (int) Eレーン.BsR ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsG && bIsAutoPlay[ (int) Eレーン.BsG ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsB && bIsAutoPlay[ (int) Eレーン.BsB ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsW && bIsAutoPlay[ (int) Eレーン.BsW ] ) bPChipIsAutoPlay = false;
//                    else if ( bGtBsO &&
//                        ( !bIsAutoPlay[ (int) Eレーン.BsR ] || !bIsAutoPlay[ (int) Eレーン.BsG ] || !bIsAutoPlay[ (int) Eレーン.BsB ] ) )
//                        bPChipIsAutoPlay = false;
//                }
//            }
//            else
//            {
//                this.bAUTOでないチップが１つでもバーを通過した = true;
//            }
////Trace.TraceInformation( "ch={0:x2}, flag={1}",  pChip.nチャンネル番号, bPChipIsAutoPlay.ToString() );
#endregion
			if ( pChip.e楽器パート == E楽器パート.UNKNOWN )
			{
				this.bAUTOでないチップが１つでもバーを通過した = true;
			}
			else
			{
				cInvisibleChip.StartSemiInvisible( pChip.e楽器パート );
			}
			bool bPChipIsAutoPlay = bCheckAutoPlay( pChip );

			pChip.bIsAutoPlayed = bPChipIsAutoPlay;			// 2011.6.10 yyagi
			E判定 eJudgeResult = E判定.Auto;

            // #35411 2015.08.20 chnmr0 modified (begin)
            bool bIsPerfectGhost = CDTXMania.ConfigIni.eAutoGhost[(int)pChip.e楽器パート] == EAutoGhostData.PERFECT ||
                CDTXMania.listAutoGhostLag[(int)pChip.e楽器パート] == null;
            int nInputAdjustTime = bPChipIsAutoPlay && bIsPerfectGhost ? 0 : this.nInputAdjustTimeMs[(int)pChip.e楽器パート];
            eJudgeResult = (bCorrectLane) ? this.e指定時刻からChipのJUDGEを返す(nHitTime, pChip, nInputAdjustTime) : E判定.Miss;

            // 2017.01.10 kairera0467 #36776
            for( int i = 0; i < 3; i++ )
            {
                if( CDTXMania.ConfigIni.eJUST[ i ] == EJust.JUST )
                {
                    if( !( eJudgeResult == E判定.Perfect || eJudgeResult == E判定.XPerfect ) )
                        eJudgeResult = E判定.Miss;
                }
                else if( CDTXMania.ConfigIni.eJUST[ i ] == EJust.GREAT )
                {
                    if( !( eJudgeResult == E判定.Perfect || eJudgeResult == E判定.Great || eJudgeResult == E判定.XPerfect ) )
                        eJudgeResult = E判定.Miss;
                }
            }

            if( pChip.e楽器パート != E楽器パート.UNKNOWN )
            {
                int nChannel = -1;
                switch( pChip.e楽器パート )
                {
                    case E楽器パート.DRUMS:
                        nChannel = this.nチャンネル0Atoレーン07[pChip.nチャンネル番号 - 0x11];
                        break;
                    case E楽器パート.GUITAR:
                        nChannel = 13;
                        break;
                    case E楽器パート.BASS:
                        nChannel = 14;
                        break;
                }
                this.actJudgeString.Start(nChannel, bPChipIsAutoPlay && bIsPerfectGhost ? E判定.Auto : eJudgeResult, pChip.nLag);
            }
            // #35411 end

			if ( !bPChipIsAutoPlay && ( pChip.e楽器パート != E楽器パート.UNKNOWN ) )
			{
				// this.t判定にあわせてゲージを増減する( screenmode, pChip.e楽器パート, eJudgeResult );
				actGauge.Damage( screenmode, pChip.e楽器パート, eJudgeResult );
			}
			if ( eJudgeResult == E判定.Poor || eJudgeResult == E判定.Miss || eJudgeResult == E判定.Bad )
			{
				cInvisibleChip.ShowChipTemporally( pChip.e楽器パート );
			}
			switch ( pChip.e楽器パート )
			{
				case E楽器パート.DRUMS:
					switch ( eJudgeResult )
					{
						case E判定.Miss:
						case E判定.Bad:
							this.nヒット数_Auto含む.Drums.Miss++;
							if ( !bPChipIsAutoPlay )
							{
								this.nヒット数_Auto含まない.Drums.Miss++;
							}
                            if( !CDTXMania.bXGRelease ) this.actClearBar.t区間内ミス通知( pChip.e楽器パート );
							break;
						default:
							this.nヒット数_Auto含む.Drums[ (int) eJudgeResult ]++;
							if ( !bPChipIsAutoPlay )
							{
								this.nヒット数_Auto含まない.Drums[ (int) eJudgeResult ]++;
							}
							break;
					}

					if ( CDTXMania.ConfigIni.bドラムが全部オートプレイである || !bPChipIsAutoPlay )
					{
						switch ( eJudgeResult )
						{
                            case E判定.XPerfect:
							case E判定.Perfect:
							case E判定.Great:
							case E判定.Good:
                                this.actCombo.tComboAnime( pChip.e楽器パート );
								this.actCombo.n現在のコンボ数.Drums++;
								break;

							default:
								this.actCombo.n現在のコンボ数.Drums = 0;
								break;
						}
					}
                    if( pChip.bボーナスチップ )
                    {
                        if( eJudgeResult == E判定.XPerfect || eJudgeResult == E判定.Great || eJudgeResult == E判定.Perfect || eJudgeResult == E判定.Auto )
                        {
                            if( CDTXMania.bXGRelease )
                                CDTXMania.stage演奏ドラム画面.tボーナスチップのヒット処理( CDTXMania.ConfigIni, CDTXMania.DTX, pChip );
                            else
                                CDTXMania.stage演奏ドラム画面GITADORA.tボーナスチップのヒット処理( CDTXMania.ConfigIni, CDTXMania.DTX, pChip );
                        }
                    }
					break;

				case E楽器パート.GUITAR:
				case E楽器パート.BASS:
					int indexInst = (int) pChip.e楽器パート;
					switch ( eJudgeResult )
					{
						case E判定.Miss:
						case E判定.Bad:
							this.nヒット数_Auto含む[ indexInst ].Miss++;
							if ( !bPChipIsAutoPlay )
							{
								this.nヒット数_Auto含まない[ indexInst ].Miss++;
							}
                            if( !CDTXMania.bXGRelease ) this.actClearBar.t区間内ミス通知( pChip.e楽器パート );
							break;
						default:	// #24068 2011.1.10 ikanick changed
							// #24167 2011.1.16 yyagi changed
							this.nヒット数_Auto含む[ indexInst ][ (int) eJudgeResult ]++;
							if ( !bPChipIsAutoPlay )
							{
								this.nヒット数_Auto含まない[ indexInst ][ (int) eJudgeResult ]++;
							}
							break;
					}
					switch ( eJudgeResult )
					{
                        case E判定.XPerfect:
						case E判定.Perfect:
						case E判定.Great:
						case E判定.Good:
                            this.actCombo.tComboAnime( pChip.e楽器パート );
							this.actCombo.n現在のコンボ数[ indexInst ]++;
							break;

						default:
							this.actCombo.n現在のコンボ数[ indexInst ] = 0;
							break;
					}
					break;

				default:
					break;
			}
			if ( ( !bPChipIsAutoPlay && ( pChip.e楽器パート != E楽器パート.UNKNOWN ) ) && ( eJudgeResult != E判定.Miss ) && ( eJudgeResult != E判定.Bad ) )
			{
                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                {
				    int nCombos = this.actCombo.n現在のコンボ数[ (int) pChip.e楽器パート ];
    				long nScoreDelta = 0;
	    			long[] nComboScoreDelta = new long[] { 350L, 200L, 50L, 0L, 0L, 0L, 350L };
		    		if ( ( nCombos <= 500 ) || ( eJudgeResult == E判定.Good ) )
			    	{
				    	nScoreDelta = nComboScoreDelta[ (int) eJudgeResult ] * nCombos;
    				}
	    			else if ( ( eJudgeResult == E判定.XPerfect ) || ( eJudgeResult == E判定.Perfect ) || ( eJudgeResult == E判定.Great ) )
		    		{
			    		nScoreDelta = nComboScoreDelta[ (int) eJudgeResult ] * 500L;
				    }
    				this.actScore.Add( pChip.e楽器パート, bIsAutoPlay, nScoreDelta );
                }
                else
                {
                    this.actScore.Add( pChip.e楽器パート, bIsAutoPlay, this.tXG加算するスコアを計算する( eJudgeResult, pChip.e楽器パート ) );
                }
			}
            //デバッグ用
            if( ( bPChipIsAutoPlay && ( pChip.e楽器パート != E楽器パート.UNKNOWN ) ) && ( eJudgeResult != E判定.Miss ) && ( eJudgeResult != E判定.Bad ) )
            {
                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                {
				    int nCombos = this.actCombo.n現在のコンボ数[ (int) pChip.e楽器パート ];
    				long nScoreDelta = 0;
	    			long[] nComboScoreDelta = new long[] { 350L, 200L, 50L, 0L, 0L, 0L, 350L };
		    		if ( ( nCombos <= 500 ) || ( eJudgeResult == E判定.Good ) )
			    	{
				    	nScoreDelta = nComboScoreDelta[ (int) eJudgeResult ] * nCombos;
    				}
	    			else if ( ( eJudgeResult == E判定.XPerfect ) || ( eJudgeResult == E判定.Perfect ) || ( eJudgeResult == E判定.Great ) )
		    		{
			    		nScoreDelta = nComboScoreDelta[ (int) eJudgeResult ] * 500L;
				    }
    				this.actScore.Add( pChip.e楽器パート, bIsAutoPlay, nScoreDelta );
                }
                else
                {
                    this.actScore.Add( pChip.e楽器パート, bIsAutoPlay, this.tXG加算するスコアを計算する( eJudgeResult, pChip.e楽器パート ) );
                }
            }
			return eJudgeResult;
		}
        private long tXG加算するスコアを計算する( E判定 eJudge, E楽器パート e楽器パート )
        {
            // BEMANIWikiより
            int nTotalNotes = 0;
            long Delta = 0;
            int nエクセ点数までに必要な点数 = ( int )(1000000 - this.actScore.n現在の本当のスコア.Drums);

            double db判定補正 = 1.0;
            int n現在コンボ = 0;
            //e楽器パート = E楽器パート.DRUMS;
            #region[ 判定補正 ]
            switch( eJudge )
            {
                case E判定.XPerfect:
                case E判定.Perfect:
                    break;
                case E判定.Great:
                    db判定補正 = 0.5;
                    break;
                case E判定.Good:
                    db判定補正 = 0.2;
                    break;
                default:
                    db判定補正 = 0.0;
                    break;
            }
            #endregion
            #region[ コンボ補正 ]
            switch( e楽器パート )
            {
                case E楽器パート.DRUMS:
                    n現在コンボ = this.actCombo.n現在のコンボ数.Drums >= 50 ? 50 : this.actCombo.n現在のコンボ数.Drums;
                    break;
                case E楽器パート.GUITAR:
                    n現在コンボ = this.actCombo.n現在のコンボ数.Guitar >= 50 ? 50 : this.actCombo.n現在のコンボ数.Guitar;
                    break;
                case E楽器パート.BASS:
                    n現在コンボ = this.actCombo.n現在のコンボ数.Bass >= 50 ? 50 : this.actCombo.n現在のコンボ数.Bass;
                    break;
            }
            #endregion
            #region[ スコア ]
            switch( e楽器パート )
            {
                case E楽器パート.DRUMS:
                    {
                        int nBonusNotes = CDTXMania.DTX.nボーナスチップ数;
                        nTotalNotes = CDTXMania.DTX.n可視チップ数.Drums;
                        int n基礎点 = (int)( ( 1000000 - 500 * nBonusNotes ) / ( 1275 + 50 * ( nTotalNotes - 50 ) ) );
                        //DM:(100万-500xボーナスノーツ数)/{1275+50×(総ノーツ数-50)}
                        if( ( nTotalNotes - 1 ) == this.nヒット数_Auto含む.Drums.Perfect )
                        {
                            //100万-500×ボーナスノーツ数-基礎点×{1275+50×(総ノーツ数-51)}
                            //エクセ時はコンボ補正無し。
                            n基礎点 = (int)( 1000000.0 - 500.0 * nBonusNotes - n基礎点 * ( 1275 + 50 * ( nTotalNotes - 51 ) ) );
                            n現在コンボ = 1;
                        }

                        Delta = n基礎点;
                    }
                    break;
                case E楽器パート.GUITAR:
                    {
                        //GF:1000000÷50÷(その曲のMAXCOMBO-24.5)
                        nTotalNotes = CDTXMania.DTX.n可視チップ数.Guitar;
                        int n基礎点 = (int)( 1000000.0 / 50.0 / ( nTotalNotes - 24.5 ) );

                        if( nTotalNotes == this.nヒット数_Auto含む.Guitar.Perfect )
                        {
                            //1000000-PERFECT基準値×50×(その曲のMAXCOMBO-25.5)
                            //エクセ時はコンボ補正無し。
                            n基礎点 = (int)( 1000000.0 - n基礎点 * 50.0 * ( nTotalNotes - 25.5 ) );
                            n現在コンボ = 1;
                        }

                        Delta = n基礎点;
                    }
                    break;
                case E楽器パート.BASS:
                    {
                        //GF:1000000÷50÷(その曲のMAXCOMBO-24.5)
                        nTotalNotes = CDTXMania.DTX.n可視チップ数.Bass;
                        int n基礎点 = (int)( 1000000.0 / 50.0 / ( nTotalNotes - 24.5 ) );

                        if( nTotalNotes == this.nヒット数_Auto含む.Bass.Perfect )
                        {
                            //1000000-PERFECT基準値×50×(その曲のMAXCOMBO-25.5)
                            //エクセ時はコンボ補正無し。
                            n基礎点 = (int)( 1000000.0 - n基礎点 * 50.0 * ( nTotalNotes - 25.5 ) );
                            n現在コンボ = 1;
                        }

                        Delta = n基礎点;
                    }
                    break;
            }
            #endregion

            Delta = ( long )( ( Delta  * n現在コンボ ) * db判定補正 );
            return Delta;
        }


		protected abstract void tチップのヒット処理_BadならびにTight時のMiss( E楽器パート part );
		protected abstract void tチップのヒット処理_BadならびにTight時のMiss( E楽器パート part, int nLane );
		protected void tチップのヒット処理_BadならびにTight時のMiss( E楽器パート part, E楽器パート screenmode )
		{
			this.tチップのヒット処理_BadならびにTight時のMiss( part, 0, screenmode );
		}
		protected void tチップのヒット処理_BadならびにTight時のMiss( E楽器パート part, int nLane, E楽器パート screenmode )
		{
			this.bAUTOでないチップが１つでもバーを通過した = true;
			cInvisibleChip.StartSemiInvisible( part );
			cInvisibleChip.ShowChipTemporally( part );
			//this.t判定にあわせてゲージを増減する( screenmode, part, E判定.Miss );
			actGauge.Damage( screenmode, part, E判定.Miss );
			switch ( part )
			{
				case E楽器パート.DRUMS:
					if ( ( nLane >= 0 ) && ( nLane <= 10 ) )
					{
						this.actJudgeString.Start( nLane, bIsAutoPlay[ nLane ] ? E判定.Auto : E判定.Miss, 999 );
					}
					this.actCombo.n現在のコンボ数.Drums = 0;
					return;

				case E楽器パート.GUITAR:
					this.actJudgeString.Start( 13, E判定.Bad, 999 );
					this.actCombo.n現在のコンボ数.Guitar = 0;
					return;

				case E楽器パート.BASS:
					this.actJudgeString.Start( 14, E判定.Bad, 999 );
					this.actCombo.n現在のコンボ数.Bass = 0;
					break;

				default:
					return;
			}
		}
	
		protected CDTX.CChip r指定時刻に一番近い未ヒットChip( long nTime, int nChannelFlag, int nInputAdjustTime )
		{
			return this.r指定時刻に一番近い未ヒットChip( nTime, nChannelFlag, nInputAdjustTime, 0 );
		}
		protected CDTX.CChip r指定時刻に一番近い未ヒットChip( long nTime, int nChannel, int nInputAdjustTime, int n検索範囲時間ms )
		{
#if DEBUG
			sw2.Start();
#endif
//Trace.TraceInformation( "nTime={0}, nChannel={1:x2}, 現在のTop={2}", nTime, nChannel,CDTXMania.DTX.listChip[ this.n現在のトップChip ].n発声時刻ms );
			nTime += nInputAdjustTime;

			int nIndex_InitialPositionSearchingToPast;
			int nTimeDiff;
			if ( this.n現在のトップChip == -1 )			// 演奏データとして1個もチップがない場合は
			{
#if DEBUG
				sw2.Stop();
#endif
				return null;
			}
			int count = listChip.Count;
			int nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToPast = this.n現在のトップChip;
			if ( this.n現在のトップChip >= count )		// その時点で演奏すべきチップが既に全部無くなっていたら
			{
				nIndex_NearestChip_Future  = nIndex_InitialPositionSearchingToPast = count - 1;
			}
			// int nIndex_NearestChip_Future = nIndex_InitialPositionSearchingToFuture;
//			while ( nIndex_NearestChip_Future < count )	// 未来方向への検索
			for ( ; nIndex_NearestChip_Future < count; nIndex_NearestChip_Future++ )
			{
				CDTX.CChip chip = listChip[ nIndex_NearestChip_Future ];
				if ( !chip.bHit )
				{
					if ( chip.b空打ちチップである )
					{
						continue;
					}
					if ( ( 0x11 <= nChannel ) && ( nChannel <= 0x1c ) )
					{
						if ( ( chip.nチャンネル番号 == nChannel ) || ( chip.nチャンネル番号 == ( nChannel + 0x20 ) ) )
						{
							if ( chip.n発声時刻ms > nTime )
							{
								break;
							}
							nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
						}
						continue;
					}
					else if ( ( ( ( nChannel == 0x2f ) && ( chip.e楽器パート == E楽器パート.GUITAR ) ) || ( ( ( 0x20 <= nChannel ) && ( nChannel <= 0x28 ) ) && ( chip.nチャンネル番号 == nChannel ) ) ) )
					{
						if ( chip.n発声時刻ms > nTime )
						{
							break;
						}
						nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
					}
					else if ( ( ( ( nChannel == 0xaf ) && ( chip.e楽器パート == E楽器パート.BASS ) ) || ( ( ( 0xa0 <= nChannel ) && ( nChannel <= 0xa8 ) ) && ( chip.nチャンネル番号 == nChannel ) ) ) )
					{
						if ( chip.n発声時刻ms > nTime )
						{
							break;
						}
						nIndex_InitialPositionSearchingToPast = nIndex_NearestChip_Future;
					}
				}
//				nIndex_NearestChip_Future++;
			}
			int nIndex_NearestChip_Past = nIndex_InitialPositionSearchingToPast;
//			while ( nIndex_NearestChip_Past >= 0 )		// 過去方向への検索
			for ( ; nIndex_NearestChip_Past >= 0; nIndex_NearestChip_Past-- )
			{
				CDTX.CChip chip = listChip[ nIndex_NearestChip_Past ];
				if ( chip.b空打ちチップである )
				{
					continue;
				}
				if ( ( !chip.bHit ) &&
						(
							( ( nChannel >= 0x11 ) && ( nChannel <= 0x1c ) &&
								( ( chip.nチャンネル番号 == nChannel ) || ( chip.nチャンネル番号 == ( nChannel + 0x20 ) ) )
							)
							||
							(
								( ( nChannel == 0x2f ) && ( chip.e楽器パート == E楽器パート.GUITAR ) ) ||
								( ( ( nChannel >= 0x20 ) && ( nChannel <= 0x28 ) ) && ( chip.nチャンネル番号 == nChannel ) )
							)
							||
							(
								( ( nChannel == 0xaf ) && ( chip.e楽器パート == E楽器パート.BASS ) ) ||
								( ( ( nChannel >= 0xA0 ) && ( nChannel <= 0xa8 ) ) && ( chip.nチャンネル番号 == nChannel ) )
							)
						)
					)
					{
						break;
					}
//				nIndex_NearestChip_Past--;
			}
			if ( ( nIndex_NearestChip_Future >= count ) && ( nIndex_NearestChip_Past < 0 ) )	// 検索対象が過去未来どちらにも見つからなかった場合
			{
#if DEBUG
				sw2.Stop();
#endif
				return null;
			}
			CDTX.CChip nearestChip;	// = null;	// 以下のifブロックのいずれかで必ずnearestChipには非nullが代入されるので、null初期化を削除
			if ( nIndex_NearestChip_Future >= count )											// 検索対象が未来方向には見つからなかった(しかし過去方向には見つかった)場合
			{
				nearestChip = listChip[ nIndex_NearestChip_Past ];
//				nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
			}
			else if ( nIndex_NearestChip_Past < 0 )												// 検索対象が過去方向には見つからなかった(しかし未来方向には見つかった)場合
			{
				nearestChip = listChip[ nIndex_NearestChip_Future ];
//				nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
			}
			else
			{
				int nTimeDiff_Future = Math.Abs( (int) ( nTime - listChip[ nIndex_NearestChip_Future ].n発声時刻ms ) );
				int nTimeDiff_Past   = Math.Abs( (int) ( nTime - listChip[ nIndex_NearestChip_Past   ].n発声時刻ms ) );
				if ( nTimeDiff_Future < nTimeDiff_Past )
				{
					nearestChip = listChip[ nIndex_NearestChip_Future ];
//					nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
				}
				else
				{
					nearestChip = listChip[ nIndex_NearestChip_Past ];
//					nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
				}
			}
			nTimeDiff = Math.Abs( (int) ( nTime - nearestChip.n発声時刻ms ) );
			if ( ( n検索範囲時間ms > 0 ) && ( nTimeDiff > n検索範囲時間ms ) )					// チップは見つかったが、検索範囲時間外だった場合
			{
#if DEBUG
				sw2.Stop();
#endif
				return null;
			}
#if DEBUG
			sw2.Stop();
#endif
			return nearestChip;
		}

		protected CDTX.CChip r次に来る指定楽器Chipを更新して返す( E楽器パート inst )
		{
			switch ( (int) inst )
			{
				case (int)E楽器パート.GUITAR:
					return r次にくるギターChipを更新して返す();
				case (int)E楽器パート.BASS:
					return r次にくるベースChipを更新して返す();
				default:
					return null;
			}
		}
		protected CDTX.CChip r次にくるギターChipを更新して返す()
		{
			int nInputAdjustTime = this.bIsAutoPlay.GtPick ? 0 : this.nInputAdjustTimeMs.Guitar;
			this.r次にくるギターChip = this.r指定時刻に一番近い未ヒットChip( CSound管理.rc演奏用タイマ.n現在時刻, 0x2f, nInputAdjustTime, 500 );
			return this.r次にくるギターChip;
		}
		protected CDTX.CChip r次にくるベースChipを更新して返す()
		{
			int nInputAdjustTime = this.bIsAutoPlay.BsPick ? 0 : this.nInputAdjustTimeMs.Bass;
			this.r次にくるベースChip = this.r指定時刻に一番近い未ヒットChip( CSound管理.rc演奏用タイマ.n現在時刻, 0xaf, nInputAdjustTime, 500 );
			return this.r次にくるベースChip;
		}

		protected void ChangeInputAdjustTimeInPlaying( IInputDevice keyboard, int plusminus )		// #23580 2011.1.16 yyagi UI for InputAdjustTime in playing screen.
		{
			int part, offset = plusminus;
			if ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) )	// Guitar InputAdjustTime
			{
				part = (int) E楽器パート.GUITAR;
			}
			else if ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftAlt ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightAlt ) )	// Bass InputAdjustTime
			{
				part = (int) E楽器パート.BASS;
			}
			else	// Drums InputAdjustTime
			{
				part = (int) E楽器パート.DRUMS;
			}
			if ( !keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftControl ) && !keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightControl ) )
			{
				offset *= 10;
			}

			this.nInputAdjustTimeMs[ part ] += offset;
			if ( this.nInputAdjustTimeMs[ part ] > 99 )
			{
				this.nInputAdjustTimeMs[ part ] = 99;
			}
			else if ( this.nInputAdjustTimeMs[ part ] < -99 )
			{
				this.nInputAdjustTimeMs[ part ] = -99;
			}
			CDTXMania.ConfigIni.nInputAdjustTimeMs[ part ] = this.nInputAdjustTimeMs[ part ];
		}

		protected abstract void t入力処理_ドラム();
		protected abstract void ドラムスクロール速度アップ();
		protected abstract void ドラムスクロール速度ダウン();
		protected void tキー入力()
		{
			IInputDevice keyboard = CDTXMania.Input管理.Keyboard;
			if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.F1 ) &&
				( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) ) )
			{	// shift+f1 (pause)
				this.bPAUSE = !this.bPAUSE;
				if ( this.bPAUSE )
				{
                    CSound管理.rc演奏用タイマ.t一時停止();
					CDTXMania.Timer.t一時停止();
					CDTXMania.DTX.t全チップの再生一時停止();
					CDTXMania.DTX.t全AVIの一時停止();
				}
				else
				{
                    CSound管理.rc演奏用タイマ.t再開();
					CDTXMania.Timer.t再開();
					CDTXMania.DTX.t全チップの再生再開();
					CDTXMania.DTX.t全AVIの再生再開();
				}
			}
			if ( ( !this.bPAUSE && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				this.t入力処理_ドラム();
				this.t入力処理_ギターベース( E楽器パート.GUITAR );
				this.t入力処理_ギターベース( E楽器パート.BASS );
				if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.UpArrow ) && ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) ) )
				{	// shift (+ctrl) + UpArrow (BGMAdjust)
					CDTXMania.DTX.t各自動再生音チップの再生時刻を変更する( ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftControl ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightControl ) ) ? 1 : 10 );
					CDTXMania.DTX.tWave再生位置自動補正();
				}
				else if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.DownArrow ) && ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) ) )
				{	// shift + DownArrow (BGMAdjust)
					CDTXMania.DTX.t各自動再生音チップの再生時刻を変更する( ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftControl ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightControl ) ) ? -1 : -10 );
					CDTXMania.DTX.tWave再生位置自動補正();
				}
                else if (!this.bPAUSE && keyboard.bキーが押された((int)SlimDX.DirectInput.Key.UpArrow) && (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightAlt) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftAlt)))
                {	// alt + UpArrow (CommonBGMAdjust)
                    CDTXMania.DTX.t各自動再生音チップの再生時刻を変更する((keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftControl) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightControl)) ? 1 : 10, false, true);
                    CDTXMania.DTX.tWave再生位置自動補正();
                }
                else if (!this.bPAUSE && keyboard.bキーが押された((int)SlimDX.DirectInput.Key.DownArrow) && (keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightAlt) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftAlt)))
                {	// alt + DownArrow (CommonBGMAdjust)
                    CDTXMania.DTX.t各自動再生音チップの再生時刻を変更する((keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.LeftControl) || keyboard.bキーが押されている((int)SlimDX.DirectInput.Key.RightControl)) ? -1 : -10, false, true);
                    CDTXMania.DTX.tWave再生位置自動補正();
                }
				else if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.UpArrow ) )
				{	// UpArrow(scrollspeed up)
					ドラムスクロール速度アップ();
				}
				else if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.DownArrow ) )
				{	// DownArrow (scrollspeed down)
					ドラムスクロール速度ダウン();
				}
				else if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.Delete ) )
				{	// del (debug info)
					CDTXMania.ConfigIni.b演奏情報を表示する = !CDTXMania.ConfigIni.b演奏情報を表示する;
				}
				else if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.LeftArrow ) )		// #24243 2011.1.16 yyagi UI for InputAdjustTime in playing screen.
				{
					ChangeInputAdjustTimeInPlaying( keyboard, -1 );
				}
				else if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.RightArrow ) )		// #24243 2011.1.16 yyagi UI for InputAdjustTime in playing screen.
				{
					ChangeInputAdjustTimeInPlaying( keyboard, +1 );
				}
				else if ( ( base.eフェーズID == CStage.Eフェーズ.共通_通常状態 ) && ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.Escape ) || CDTXMania.Pad.b押されたGB( Eパッド.FT ) ) )
				{	// escape (exit)
					this.actFO.tフェードアウト開始();
					base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
					this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.演奏中断;
				}
			}
            //if(  )
            //{
            //    if( CDTXMania.ConfigIni.eMovieClipMode == EMovieClipMode.OFF )
            //        CDTXMania.ConfigIni.eMovieClipMode = EMovieClipMode.FullScreen;
            //    else if( CDTXMania.ConfigIni.eMovieClipMode == EMovieClipMode.FullScreen )
            //        CDTXMania.ConfigIni.eMovieClipMode = EMovieClipMode.Window;
            //    else if( CDTXMania.ConfigIni.eMovieClipMode == EMovieClipMode.Window )
            //        CDTXMania.ConfigIni.eMovieClipMode = EMovieClipMode.Both;
            //    else if( CDTXMania.ConfigIni.eMovieClipMode == EMovieClipMode.Both )
            //        CDTXMania.ConfigIni.eMovieClipMode = EMovieClipMode.OFF;
            //}
            if( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.F5 ) ) // 2017.12.29 kairera0467 #37846
            {
                CDTXMania.ConfigIni.bWindowClipMode = !CDTXMania.ConfigIni.bWindowClipMode;
            }

            if( CDTXMania.ConfigIni.bDrums有効 ) //2017.08.06 kairera0467 ギターは未実装
                this.actShutter.tShutterMove( keyboard );
		}

		protected void t入力メソッド記憶( E楽器パート part )
		{
			if ( CDTXMania.Pad.st検知したデバイス.Keyboard )
			{
				this.b演奏にキーボードを使った[ (int) part ] = true;
			}
			if ( CDTXMania.Pad.st検知したデバイス.Joypad )
			{
				this.b演奏にジョイパッドを使った[ (int) part ] = true;
			}
			if ( CDTXMania.Pad.st検知したデバイス.MIDIIN )
			{
				this.b演奏にMIDI入力を使った[ (int) part ] = true;
			}
			if ( CDTXMania.Pad.st検知したデバイス.Mouse )
			{
				this.b演奏にマウスを使った[ (int) part ] = true;
			}
		}
		protected abstract void t進行描画_AVI();
		protected void t進行描画_AVI(int x, int y)
		{
			if ( ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) && ( !CDTXMania.ConfigIni.bストイックモード && CDTXMania.ConfigIni.bAVI有効 ) )
			{
                this.actAVI.t進行描画(x, y, 556, 710);
			}
		}
		protected abstract void t進行描画_BGA();
		protected void t進行描画_BGA(int x, int y)
		{
			if ( ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) && ( !CDTXMania.ConfigIni.bストイックモード && CDTXMania.ConfigIni.bBGA有効 ) )
			{
				this.actBGA.t進行描画( x, y );
			}
		}
		protected abstract void t進行描画_DANGER();
		protected void t進行描画_MIDIBGM()
		{
			if ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED )
			{
				CStage.Eフェーズ eフェーズid1 = base.eフェーズID;
			}
		}
		protected void t進行描画_RGBボタン()
		{
			if ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL )
			{
				this.actRGB.t進行描画( 演奏判定ライン座標 );
			}
		}
		protected void t進行描画_STAGEFAILED()
		{
			if ( ( ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED ) || ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) && ( ( this.actStageFailed.On進行描画() != 0 ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) )
			{
				this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.ステージ失敗;
				base.eフェーズID = CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト;
				this.actFO.tフェードアウト開始();
			}
		}
		protected void t進行描画_WailingBonus()
		{
			if ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				this.actWailingBonus.On進行描画();
			}
		}
		protected abstract void t進行描画_Wailing枠();
		protected void t進行描画_Wailing枠(int GtWailingFrameX, int BsWailingFrameX, int GtWailingFrameY, int BsWailingFrameY)
		{
			if ( ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL ) && CDTXMania.ConfigIni.bGuitar有効 )
			{
				if ( this.txWailing枠 != null )
				{
					if ( CDTXMania.DTX.bチップがある.Guitar )
					{
						this.txWailing枠.t2D描画( CDTXMania.app.Device, GtWailingFrameX, GtWailingFrameY );
					}
					if ( CDTXMania.DTX.bチップがある.Bass )
					{
						this.txWailing枠.t2D描画( CDTXMania.app.Device, BsWailingFrameX, BsWailingFrameY );
					}
				}
			}
		}


		protected void t進行描画_チップファイアGB()
		{
			this.actChipFireGB.On進行描画();
		}
		protected abstract void t進行描画_パネル文字列();
		protected void t進行描画_パネル文字列(int x, int y)
		{
			if ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				this.actPanel.t進行描画( x, y );
			}
		}
		protected void tパネル文字列の設定()
		{
			this.actPanel.SetPanelString( string.IsNullOrEmpty( CDTXMania.DTX.PANEL ) ? CDTXMania.DTX.TITLE : CDTXMania.DTX.PANEL );
		}


		protected void t進行描画_ゲージ()
		{
			if ( ( ( CDTXMania.ConfigIni.eDark != Eダークモード.HALF ) && ( CDTXMania.ConfigIni.eDark != Eダークモード.FULL ) ) && ( ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) ) )
			{
				this.actGauge.On進行描画();
			}
		}
		protected void t進行描画_コンボ()
		{
			this.actCombo.On進行描画();
		}
		protected void t進行描画_スコア()
		{
			this.actScore.On進行描画();
		}
		protected void t進行描画_ステータスパネル()
		{
			this.actStatusPanels.On進行描画();
		}
		protected bool t進行描画_チップ( E楽器パート ePlayMode )
		{
            if ( ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED ) || ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
            {
                return true;
            }
            if ( ( this.n現在のトップChip == -1 ) || ( this.n現在のトップChip >= listChip.Count ) )
            {
                return true;
            }
            if ( this.n現在のトップChip == -1 )
            {
                return true;
            }

			//double speed = 264.0;	// BPM150の時の1小節の長さ[dot]
			const double speed = 324.0;	// BPM150の時の1小節の長さ[dot]

			double ScrollSpeedDrums =  ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums  + 1.0 ) * 0.5       * 37.5 * speed / 60000.0;
			double ScrollSpeedGuitar = ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Guitar + 1.0 ) * 0.5 * 0.5 * 37.5 * speed / 60000.0;
			double ScrollSpeedBass =   ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Bass   + 1.0 ) * 0.5 * 0.5 * 37.5 * speed / 60000.0;

			CDTX dTX = CDTXMania.DTX;
			CConfigIni configIni = CDTXMania.ConfigIni;
			for ( int nCurrentTopChip = this.n現在のトップChip; nCurrentTopChip < dTX.listChip.Count; nCurrentTopChip++ )
			{
				CDTX.CChip pChip = dTX.listChip[ nCurrentTopChip ];
//Debug.WriteLine( "nCurrentTopChip=" + nCurrentTopChip + ", ch=" + pChip.nチャンネル番号.ToString("x2") + ", 発音位置=" + pChip.n発声位置 + ", 発声時刻ms=" + pChip.n発声時刻ms );
				pChip.nバーからの距離dot.Drums = (int) ( ( pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻 ) * ScrollSpeedDrums );
				pChip.nバーからの距離dot.Guitar = (int) ( ( pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻 ) * ScrollSpeedGuitar );
				pChip.nバーからの距離dot.Bass = (int) ( ( pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻 ) * ScrollSpeedBass );
				if ( Math.Min( Math.Min( pChip.nバーからの距離dot.Drums, pChip.nバーからの距離dot.Guitar ), pChip.nバーからの距離dot.Bass ) > 600 )
				{
					break;
				}
//				if ( ( ( nCurrentTopChip == this.n現在のトップChip ) && ( pChip.nバーからの距離dot.Drums < -65 ) ) && pChip.bHit )
				// #28026 2012.4.5 yyagi; 信心ワールドエンドの曲終了後リザルトになかなか行かない問題の修正

				if (( dTX.listChip[ this.n現在のトップChip ].nバーからの距離dot.Drums < -65 * Scale.Y ) &&	// 小節線の消失処理などに影響するため、
					( dTX.listChip[ this.n現在のトップChip ].nバーからの距離dot.Guitar < -65 * Scale.Y ) &&	// Drumsのスクロールスピードだけには依存させない。
					( dTX.listChip[ this.n現在のトップChip ].nバーからの距離dot.Bass < -65 * Scale.Y ) && 
					dTX.listChip[ this.n現在のトップChip ].bHit )
				{
					//					nCurrentTopChip = ++this.n現在のトップChip;
					++this.n現在のトップChip;
					continue;
				}
				bool bPChipIsAutoPlay = bCheckAutoPlay( pChip );

				int nInputAdjustTime = ( bPChipIsAutoPlay || (pChip.e楽器パート == E楽器パート.UNKNOWN) )? 0 : this.nInputAdjustTimeMs[ (int) pChip.e楽器パート ];

				int instIndex = (int) pChip.e楽器パート;
				if ( ( ( pChip.e楽器パート != E楽器パート.UNKNOWN ) && !pChip.bHit ) &&
				    ( ( pChip.nバーからの距離dot[ instIndex ] < -40 * Scale.Y ) && ( this.e指定時刻からChipのJUDGEを返す( CSound管理.rc演奏用タイマ.n現在時刻, pChip, nInputAdjustTime ) == E判定.Miss ) ) )
				{
				    this.tチップのヒット処理( CSound管理.rc演奏用タイマ.n現在時刻, pChip );
				}

                // #35411 chnmr0 add (ターゲットゴースト)
                if ( CDTXMania.ConfigIni.eTargetGhost[instIndex] != ETargetGhostData.NONE &&
                     CDTXMania.listTargetGhsotLag[instIndex] != null &&
                     pChip.e楽器パート != E楽器パート.UNKNOWN &&
                     pChip.nバーからの距離dot[instIndex] < 0 )
                {
                    if ( !pChip.bTargetGhost判定済み )
                    {
                        pChip.bTargetGhost判定済み = true;

						int ghostLag = 128;
						if( 0 <= pChip.n楽器パートでの出現順 && pChip.n楽器パートでの出現順 < CDTXMania.listTargetGhsotLag[instIndex].Count )
                        {
                            ghostLag = CDTXMania.listTargetGhsotLag[instIndex][pChip.n楽器パートでの出現順];
							// 上位８ビットが１ならコンボが途切れている（ギターBAD空打ちでコンボ数を再現するための措置）
							if( ghostLag > 255 )
							{
								this.nコンボ数_TargetGhost[instIndex] = 0;
							}
							ghostLag = (ghostLag & 255) - 128;
						}
                        else if( CDTXMania.ConfigIni.eTargetGhost[instIndex] == ETargetGhostData.PERFECT )
                        {
                            ghostLag = 0;
                        }
                        
                        if ( ghostLag <= 127 )
                        {
                            E判定 eJudge = this.e指定時刻からChipのJUDGEを返す(pChip.n発声時刻ms + ghostLag , pChip, 0, false);
                            this.nヒット数_TargetGhost[instIndex][(int)eJudge]++;
                            if (eJudge == E判定.Miss || eJudge == E判定.Poor)
                            {
                                this.n最大コンボ数_TargetGhost[instIndex] = Math.Max(this.n最大コンボ数_TargetGhost[instIndex], this.nコンボ数_TargetGhost[instIndex]);
                                this.nコンボ数_TargetGhost[instIndex] = 0;
                            }
                            else
                            {
                                this.n最大コンボ数_TargetGhost[instIndex] = Math.Max(this.n最大コンボ数_TargetGhost[instIndex], this.nコンボ数_TargetGhost[instIndex]);
                                this.nコンボ数_TargetGhost[instIndex]++;
                            }
                        }
                    }
                }
 
				switch ( pChip.nチャンネル番号 )
				{
					#region [ 01: BGM ]
					case 0x01:	// BGM
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( configIni.bBGM音を発声する )
							{
//long t = CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms;
//Trace.TraceInformation( "BGM再生開始: 演奏タイマのn前回リセットしたときのシステム時刻=" + CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + ", pChip.n発生時刻ms=" + pChip.n発声時刻ms + ", 合計=" + t );
								dTX.tチップの再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, (int) Eレーン.BGM, dTX.nモニタを考慮した音量( E楽器パート.UNKNOWN ) );
							}
						}
						break;
					#endregion
					#region [ 03: BPM変更 ]
					case 0x03:	// BPM変更
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							this.actPlayInfo.dbBPM = ( pChip.n整数値 * ( ( (double) configIni.n演奏速度 ) / 20.0 ) ) + dTX.BASEBPM;

                            if( CDTXMania.ConfigIni.bDrums有効 )
                            {
                                //CDTXMania.stage演奏ドラム画面.UnitTime = ((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 16.0 ));
                                this.actBPMBar.ctBPMバー = new CCounter(1, 16, (int)((60.0 / (this.actPlayInfo.dbBPM) / 16.0 ) * 1000.0 ), CDTXMania.Timer);
                                //CDTXMania.stage演奏ドラム画面.ctコンボ動作タイマ = new CCounter(1.0, 16.0, ((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 16.0)), CSound管理.rc演奏用タイマ);
                                if( CDTXMania.bXGRelease )
                                    this.actCombo.ctコンボ動作タイマ = new CCounter( 1, 16, (int)( ( 60.0 / ( this.actPlayInfo.dbBPM ) / 16.0 ) * 1000.0 ), CDTXMania.Timer );
                                else
                                    this.actCombo.ctコンボ動作タイマ = new CCounter( 1, 16, (int)( ( 60.0 / ( this.actPlayInfo.dbBPM ) / 16.0 ) * 1000.0 ), CDTXMania.Timer );
                            }
                            else if( CDTXMania.ConfigIni.bGuitar有効 )
                            {
                                //CDTXMania.stage演奏ギター画面.UnitTime = ((60.0 / (CDTXMania.stage演奏ギター画面.actPlayInfo.dbBPM) / 16.0 ));
                                this.actBPMBar.ctBPMバー = new CCounter(1, 16, (int)((60.0 / (CDTXMania.stage演奏ギター画面.actPlayInfo.dbBPM) / 16.0 ) * 1000.0 ), CDTXMania.Timer);
                                //CDTXMania.stage演奏ギター画面.ctコンボ動作タイマ = new CCounter(1.0, 16.0, ((60.0 / (CDTXMania.stage演奏ギター画面.actPlayInfo.dbBPM) / 16.0)), CSound管理.rc演奏用タイマ);
                            }
						}
						break;
					#endregion
					#region [ 04, 07, 55, 56,57, 58, 59, 60:レイヤーBGA ]
					case 0x04:	// レイヤーBGA
					case 0x07:
					case 0x55:
					case 0x56:
					case 0x57:
					case 0x58:
					case 0x59:
					case 0x60:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( configIni.bBGA有効 )
							{
								switch ( pChip.eBGA種別 )
								{
									case EBGA種別.BMPTEX:
										if ( pChip.rBMPTEX != null )
										{
											this.actBGA.Start( pChip.nチャンネル番号, null, pChip.rBMPTEX, pChip.rBMPTEX.tx画像.sz画像サイズ.Width, pChip.rBMPTEX.tx画像.sz画像サイズ.Height, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 );
										}
										break;

									case EBGA種別.BGA:
										if ( ( pChip.rBGA != null ) && ( ( pChip.rBMP != null ) || ( pChip.rBMPTEX != null ) ) )
										{
											this.actBGA.Start( pChip.nチャンネル番号, pChip.rBMP, pChip.rBMPTEX, pChip.rBGA.pt画像側右下座標.X - pChip.rBGA.pt画像側左上座標.X, pChip.rBGA.pt画像側右下座標.Y - pChip.rBGA.pt画像側左上座標.Y, 0, 0, pChip.rBGA.pt画像側左上座標.X, pChip.rBGA.pt画像側左上座標.Y, 0, 0, pChip.rBGA.pt表示座標.X, pChip.rBGA.pt表示座標.Y, 0, 0, 0 );
										}
										break;

									case EBGA種別.BGAPAN:
										if ( ( pChip.rBGAPan != null ) && ( ( pChip.rBMP != null ) || ( pChip.rBMPTEX != null ) ) )
										{
											this.actBGA.Start( pChip.nチャンネル番号, pChip.rBMP, pChip.rBMPTEX, pChip.rBGAPan.sz開始サイズ.Width, pChip.rBGAPan.sz開始サイズ.Height, pChip.rBGAPan.sz終了サイズ.Width, pChip.rBGAPan.sz終了サイズ.Height, pChip.rBGAPan.pt画像側開始位置.X, pChip.rBGAPan.pt画像側開始位置.Y, pChip.rBGAPan.pt画像側終了位置.X, pChip.rBGAPan.pt画像側終了位置.Y, pChip.rBGAPan.pt表示側開始位置.X, pChip.rBGAPan.pt表示側開始位置.Y, pChip.rBGAPan.pt表示側終了位置.X, pChip.rBGAPan.pt表示側終了位置.Y, pChip.n総移動時間 );
										}
										break;

									default:
										if ( pChip.rBMP != null )
										{
											this.actBGA.Start( pChip.nチャンネル番号, pChip.rBMP, null, pChip.rBMP.n幅, pChip.rBMP.n高さ, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 );
										}
										break;
								}
							}
						}
						break;
					#endregion
					#region [ 08: BPM変更(拡張) ]
					case 0x08:	// BPM変更(拡張)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( dTX.listBPM.ContainsKey( pChip.n整数値_内部番号 ) )
							{
								this.actPlayInfo.dbBPM = ( dTX.listBPM[ pChip.n整数値_内部番号 ].dbBPM値 * ( ( (double) configIni.n演奏速度 ) / 20.0 ) ) + dTX.BASEBPM;
                                if( CDTXMania.ConfigIni.bDrums有効 )
                                {
                                    //CDTXMania.stage演奏ドラム画面.UnitTime = ((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 16.0 ));
                                    this.actBPMBar.ctBPMバー = new CCounter(1, 16, (int)((60.0 / this.actPlayInfo.dbBPM / 16.0 ) * 1000 ), CDTXMania.Timer);
                                    //CDTXMania.stage演奏ドラム画面.ctコンボ動作タイマ = new CCounter(1.0, 16.0, ((60.0 / (CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM) / 16.0)), CSound管理.rc演奏用タイマ);
                                    if( CDTXMania.bXGRelease )
                                        this.actCombo.ctコンボ動作タイマ = new CCounter( 1, 16, (int)( ( 60.0 / ( CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM ) / 16.0 ) * 1000.0 ), CDTXMania.Timer );
                                    else
                                        this.actCombo.ctコンボ動作タイマ = new CCounter( 1, 16, (int)( ( 60.0 / ( this.actPlayInfo.dbBPM ) / 16.0 ) * 1000.0 ), CDTXMania.Timer );
                                }
                                else if( CDTXMania.ConfigIni.bGuitar有効 )
                                {
                                    //CDTXMania.stage演奏ギター画面.UnitTime = ((60.0 / (CDTXMania.stage演奏ギター画面.actPlayInfo.dbBPM) / 16.0 ));
                                    this.actBPMBar.ctBPMバー = new CCounter(1, 16, (int)((60.0 / this.actPlayInfo.dbBPM / 16.0 ) * 1000 ), CDTXMania.Timer);
                                    //CDTXMania.stage演奏ギター画面.ctコンボ動作タイマ = new CCounter(1.0, 16.0, ((60.0 / (CDTXMania.stage演奏ギター画面.actPlayInfo.dbBPM) / 16.0)), CSound管理.rc演奏用タイマ);
                                }
							}
						}
						break;
					#endregion
					#region [ 11-1a: ドラム演奏 ]
					case 0x11:	// ドラム演奏
					case 0x12:
					case 0x13:
					case 0x14:
					case 0x15:
					case 0x16:
					case 0x17:
					case 0x18:
					case 0x19:
					case 0x1a:
                    case 0x1b:
                    case 0x1c:
						if ( pChip.b空打ちチップである )
						{
							this.t進行描画_チップ_空打ち音設定_ドラム( configIni, ref dTX, ref pChip );
						}
						else
						{
							this.t進行描画_チップ_ドラムス( configIni, ref dTX, ref pChip );
						}
						break;
					#endregion
					#region [ 1f: フィルインサウンド(ドラム) ]
					case 0x1f:	// フィルインサウンド(ドラム)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の歓声Chip.Drums = pChip;
						}
						break;
					#endregion
					#region [ 20-27: ギター演奏 ]
					case 0x20:	// ギター演奏
					case 0x21:
					case 0x22:
					case 0x23:
					case 0x24:
					case 0x25:
					case 0x26:
					case 0x27:
						this.t進行描画_チップ_ギターベース( configIni, ref dTX, ref pChip, E楽器パート.GUITAR );
						break;
					#endregion
					#region [ 28: ウェイリング(ギター) ]
					case 0x28:	// ウェイリング(ギター)
						this.t進行描画_チップ_ギター_ウェイリング( configIni, ref dTX, ref pChip, !CDTXMania.ConfigIni.bDrums有効 );
						break;
					#endregion
					#region [ 2f: ウェイリングサウンド(ギター) ]
					case 0x2f:	// ウェイリングサウンド(ギター)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Guitar < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の歓声Chip.Guitar = pChip;
						}
						break;
					#endregion
					#region [ 31-3a: 不可視チップ配置(ドラム) ]
					case 0x31:	// 不可視チップ配置(ドラム)
					case 0x32:
					case 0x33:
					case 0x34:
					case 0x35:
					case 0x36:
					case 0x37:
					case 0x38:
					case 0x39:
					case 0x3a:
                    case 0x3b:
                    case 0x3c:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
						}
						break;
					#endregion
					#region [ 50: 小節線 ]
					case 0x50:	// 小節線
						{
							this.t進行描画_チップ_小節線( configIni, ref dTX, ref pChip );
							break;
						}
					#endregion
					#region [ 51: 拍線 ]
					case 0x51:	// 拍線
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
						}
						if ( ( ePlayMode == E楽器パート.DRUMS ) && ( configIni.nLaneDispType.Drums < 2 ) && pChip.b可視 && ( this.txチップ != null ) )
						{
                            int nJPos = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );
							this.txチップ.t2D描画( CDTXMania.app.Device,
						        295,
						        configIni.bReverse.Drums ?
							        159 + pChip.nバーからの距離dot.Drums : 561 - pChip.nバーからの距離dot.Drums,
								new Rectangle( 0, 772, 558, 2 )
							);
						}
						break;
					#endregion
					#region [ 52: MIDIコーラス ]
					case 0x52:	// MIDIコーラス
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
						}
						break;
					#endregion
					#region [ 53: フィルイン ]
					case 0x53:	// フィルイン
						this.t進行描画_チップ_フィルイン( configIni, ref dTX, ref pChip );
						break;
					#endregion
					#region [ 54: 動画再生(BGA領域), 5A: 動画再生(全画面) ]
					case (int) Ech定義.Movie:		// 動画再生 (BGA領域)
					case (int) Ech定義.MovieFull:	// 動画再生 (全画面)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( configIni.bAVI有効 )
							{
								if ( CDTXMania.DTX.bチップがある.BGA )
								{
									this.actAVI.bHasBGA = true;
								}
								if ( pChip.nチャンネル番号 == (int) Ech定義.MovieFull || CDTXMania.ConfigIni.bForceAVIFullscreen )
								{
									this.actAVI.bFullScreenMovie = true;
								}
								switch ( pChip.eAVI種別 )
								{
									case EAVI種別.AVI:
										//if ( pChip.rAVI != null )
										{
											//int startWidth = ( CDTXMania.DTX.bチップがある.BGA ) ? 278 : SampleFramework.GameWindowSize.Width;
											//int startHeight = ( CDTXMania.DTX.bチップがある.BGA ) ? 355 : SampleFramework.GameWindowSize.Height;
											int startWidth  = !this.actAVI.bFullScreenMovie ? 278 : SampleFramework.GameWindowSize.Width;
											int startHeight = !this.actAVI.bFullScreenMovie ? 355 : SampleFramework.GameWindowSize.Height;
											this.actAVI.Start( pChip.nチャンネル番号, pChip.rAVI, startWidth, startHeight, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, pChip.n発声時刻ms );
										}
										break;

									case EAVI種別.AVIPAN:
										if ( pChip.rAVIPan != null )
										{
											this.actAVI.Start( pChip.nチャンネル番号, pChip.rAVI, pChip.rAVIPan.sz開始サイズ.Width, pChip.rAVIPan.sz開始サイズ.Height, pChip.rAVIPan.sz終了サイズ.Width, pChip.rAVIPan.sz終了サイズ.Height, 0, 0, 0, 0, 0, 0, 0, 0, pChip.n総移動時間, pChip.n発声時刻ms );
										}
										break;
								}
							}
						}
						break;
					#endregion
					#region [ 61-92: 自動再生(BGM, SE) ]
					case 0x61:
					case 0x62:
					case 0x63:
					case 0x64:	// 自動再生(BGM, SE)
					case 0x65:
					case 0x66:
					case 0x67:
					case 0x68:
					case 0x69:
					case 0x70:
					case 0x71:
					case 0x72:
					case 0x73:
					case 0x74:
					case 0x75:
					case 0x76:
					case 0x77:
					case 0x78:
					case 0x79:
					case 0x80:
					case 0x81:
					case 0x82:
					case 0x83:
					case 0x90:
					case 0x91:
					case 0x92:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( configIni.bBGM音を発声する )
							{
								dTX.tWavの再生停止( this.n最後に再生したBGMの実WAV番号[ pChip.nチャンネル番号 - 0x61 ] );
								dTX.tチップの再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, ( int ) Eレーン.BGM, dTX.nモニタを考慮した音量( E楽器パート.UNKNOWN ) );
								this.n最後に再生したBGMの実WAV番号[ pChip.nチャンネル番号 - 0x61 ] = pChip.n整数値_内部番号;
							}
						}
						break;
					#endregion


					#region [ 84-89: 仮: override sound ]	// #26338 2011.11.8 yyagi
					case 0x84:	// HH (HO/HC)
					case 0x85:	// CY
					case 0x86:	// RD
					case 0x87:	// LC
					case 0x88:	// Guitar
					case 0x89:	// Bass
					// mute sound (auto)
					// 4A: 84: HH (HO/HC)
					// 4B: 85: CY
					// 4C: 86: RD
					// 4D: 87: LC
					// 2A: 88: Gt
					// AA: 89: Bs

					//	CDTXMania.DTX.tWavの再生停止( this.n最後に再生した実WAV番号.Guitar );
					//	CDTXMania.DTX.tチップの再生( pChip, n再生開始システム時刻ms, 8, n音量, bモニタ, b音程をずらして再生 );
					//	this.n最後に再生した実WAV番号.Guitar = pChip.n整数値_内部番号;

					//	protected void tサウンド再生( CDTX.CChip pChip, long n再生開始システム時刻ms, E楽器パート part, int n音量, bool bモニタ, bool b音程をずらして再生 )
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							E楽器パート[] p = { E楽器パート.DRUMS, E楽器パート.DRUMS, E楽器パート.DRUMS, E楽器パート.DRUMS, E楽器パート.GUITAR, E楽器パート.BASS };

							E楽器パート pp =  p[ pChip.nチャンネル番号 - 0x84 ];
							
//							if ( pp == E楽器パート.DRUMS ) {			// pChip.nチャンネル番号= ..... HHとか、ドラムの場合は変える。
//								//            HC    CY    RD    LC
//								int[] ch = { 0x11, 0x16, 0x19, 0x1A };
//								pChip.nチャンネル番号 = ch[ pChip.nチャンネル番号 - 0x84 ]; 
//							}
							this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, pp, dTX.nモニタを考慮した音量( pp ) );
						}
						break;
					#endregion

					#region [ a0-a7: ベース演奏 ]
					case 0xa0:	// ベース演奏
					case 0xa1:
					case 0xa2:
					case 0xa3:
					case 0xa4:
					case 0xa5:
					case 0xa6:
					case 0xa7:
						this.t進行描画_チップ_ギターベース( configIni, ref dTX, ref pChip, E楽器パート.BASS );
						break;
					#endregion
					#region [ a8: ウェイリング(ベース) ]
					case 0xa8:	// ウェイリング(ベース)
						this.t進行描画_チップ_ベース_ウェイリング( configIni, ref dTX, ref pChip, !CDTXMania.ConfigIni.bDrums有効 );
						break;
					#endregion
					#region [ af: ウェイリングサウンド(ベース) ]
					case 0xaf:	// ウェイリングサウンド(ベース)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Bass < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の歓声Chip.Bass = pChip;
						}
						break;
					#endregion
					#region [ b1-b9, bc: 空打ち音設定(ドラム) ]
					case 0xb1:	// 空打ち音設定(ドラム)
					case 0xb2:
					case 0xb3:
					case 0xb4:
					case 0xb5:
					case 0xb6:
					case 0xb7:
					case 0xb8:
					case 0xb9:
					case 0xbc:
						// ここには来なくなったはずだが、一応残しておく
						this.t進行描画_チップ_空打ち音設定_ドラム( configIni, ref dTX, ref pChip );
						break;
					#endregion
					#region [ ba: 空打ち音設定(ギター) ]
					case 0xba:	// 空打ち音設定(ギター)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Guitar < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の空うちギターChip = pChip;
							pChip.nチャンネル番号 = 0x20;
						}
						break;
					#endregion
					#region [ bb: 空打ち音設定(ベース) ]
					case 0xbb:	// 空打ち音設定(ベース)
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Bass < 0 ) )
						{
							pChip.bHit = true;
							this.r現在の空うちベースChip = pChip;
							pChip.nチャンネル番号 = 0xA0;
						}
						break;
					#endregion
					#region [ c4, c7, d5-d9, e0: BGA画像入れ替え ]
					case 0xc4:
					case 0xc7:
					case 0xd5:
					case 0xd6:	// BGA画像入れ替え
					case 0xd7:
					case 0xd8:
					case 0xd9:
					case 0xe0:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
							if ( ( configIni.bBGA有効 && ( pChip.eBGA種別 == EBGA種別.BMP ) ) || ( pChip.eBGA種別 == EBGA種別.BMPTEX ) )
							{
								for ( int i = 0; i < 8; i++ )
								{
									if ( this.nBGAスコープチャンネルマップ[ 0, i ] == pChip.nチャンネル番号 )
									{
										this.actBGA.ChangeScope( this.nBGAスコープチャンネルマップ[ 1, i ], pChip.rBMP, pChip.rBMPTEX );
									}
								}
							}
						}
						break;
					#endregion
					#region [ da: ミキサーへチップ音追加 ]
					case 0xDA:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
//Debug.WriteLine( "[DA(AddMixer)] BAR=" + pChip.n発声位置 / 384 + " ch=" + pChip.nチャンネル番号.ToString( "x2" ) + ", wav=" + pChip.n整数値.ToString( "x2" ) + ", time=" + pChip.n発声時刻ms );
							pChip.bHit = true;
							if ( listWAV.ContainsKey( pChip.n整数値_内部番号 ) )	// 参照が遠いので後日最適化する
							{
								CDTX.CWAV wc = listWAV[ pChip.n整数値_内部番号 ];
//Debug.Write( "[AddMixer] BAR=" + pChip.n発声位置 / 384 + ", wav=" + Path.GetFileName( wc.strファイル名 ) + ", time=" + pChip.n発声時刻ms );

								for ( int i = 0; i < nPolyphonicSounds; i++ )
								{
									if ( wc.rSound[ i ] != null )
									{
										//CDTXMania.Sound管理.AddMixer( wc.rSound[ i ] );
										AddMixer( wc.rSound[ i ], pChip.b演奏終了後も再生が続くチップである );
									}
									//else
									//{
									//    Debug.WriteLine( ", nPoly=" + i + ", Mix=" + CDTXMania.Sound管理.GetMixingStreams() );
									//    break;
									//}
									//if ( i == nPolyphonicSounds - 1 )
									//{
									//    Debug.WriteLine( ", nPoly=" + nPolyphonicSounds + ", Mix=" + CDTXMania.Sound管理.GetMixingStreams() );
									//}
								}
							}
						}
						break;
					#endregion
					#region [ db: ミキサーからチップ音削除 ]
					case 0xDB:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
//Debug.WriteLine( "[DB(RemoveMixer)] BAR=" + pChip.n発声位置 / 384 + " ch=" + pChip.nチャンネル番号.ToString( "x2" ) + ", wav=" + pChip.n整数値.ToString( "x2" ) + ", time=" + pChip.n発声時刻ms );
							pChip.bHit = true;
							if ( listWAV.ContainsKey( pChip.n整数値_内部番号) )	// 参照が遠いので後日最適化する
							{
							    CDTX.CWAV wc = listWAV[ pChip.n整数値_内部番号];
//Debug.Write( "[DelMixer] BAR=" + pChip.n発声位置 / 384 +  ", wav=" + Path.GetFileName( wc.strファイル名 ) + ", time=" + pChip.n発声時刻ms );
								for ( int i = 0; i < nPolyphonicSounds; i++ )
							    {
									if ( wc.rSound[ i ] != null )
									{
										//CDTXMania.Sound管理.RemoveMixer( wc.rSound[ i ] );
										if ( !wc.rSound[ i ].b演奏終了後も再生が続くチップである )	// #32248 2013.10.16 yyagi
										{															// DTX終了後も再生が続くチップの0xDB登録をなくすことはできず。
											RemoveMixer( wc.rSound[ i ] );							// (ミキサー解除のタイミングが遅延する場合の対応が面倒なので。)
										}															// そこで、代わりにフラグをチェックしてミキサー削除ロジックへの遷移をカットする。
									}
									//else
									//{
									//    Debug.WriteLine( ", nPoly=" + i + ", Mix=" + CDTXMania.Sound管理.GetMixingStreams() );
									//    break;
									//}
									//if ( i == nPolyphonicSounds - 1 )
									//{
									//    Debug.WriteLine( ", nPoly=" + nPolyphonicSounds + ", Mix=" + CDTXMania.Sound管理.GetMixingStreams() );
									//}
								}
							}
						}
						break;
					#endregion
					#region [ その他(未定義) ]
					default:
						if ( !pChip.bHit && ( pChip.nバーからの距離dot.Drums < 0 ) )
						{
							pChip.bHit = true;
						}
						break;
					#endregion 
				}
			}
			return false;
		}
		protected void t進行描画_チップ_模様( E楽器パート ePlayMode )
		{
            //if ( ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED ) || ( base.eフェーズID == CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
            //{
            //    return true;
            //}
            //if ( ( this.n現在のトップChip == -1 ) || ( this.n現在のトップChip >= listChip.Count ) )
            //{
            //    return true;
            //}
            //if ( this.n現在のトップChip == -1 )
            //{
            //    return true;
            //}

			//double speed = 264.0;	// BPM150の時の1小節の長さ[dot]
			const double speed = 324.0;	// BPM150の時の1小節の長さ[dot]

			double ScrollSpeedDrums =  ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Drums  + 1.0 ) * 0.5       * 37.5 * speed / 60000.0;
			double ScrollSpeedGuitar = ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Guitar + 1.0 ) * 0.5 * 0.5 * 37.5 * speed / 60000.0;
			double ScrollSpeedBass =   ( this.act譜面スクロール速度.db現在の譜面スクロール速度.Bass   + 1.0 ) * 0.5 * 0.5 * 37.5 * speed / 60000.0;

			CDTX dTX = CDTXMania.DTX;
			CConfigIni configIni = CDTXMania.ConfigIni;
			for ( int nCurrentTopChip = this.n現在のトップChip; nCurrentTopChip < dTX.listChip.Count; nCurrentTopChip++ )
			{
				CDTX.CChip pChip = dTX.listChip[ nCurrentTopChip ];
//Debug.WriteLine( "nCurrentTopChip=" + nCurrentTopChip + ", ch=" + pChip.nチャンネル番号.ToString("x2") + ", 発音位置=" + pChip.n発声位置 + ", 発声時刻ms=" + pChip.n発声時刻ms );
				pChip.nバーからの距離dot.Drums = (int) ( ( pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻 ) * ScrollSpeedDrums );
				pChip.nバーからの距離dot.Guitar = (int) ( ( pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻 ) * ScrollSpeedGuitar );
				pChip.nバーからの距離dot.Bass = (int) ( ( pChip.n発声時刻ms - CSound管理.rc演奏用タイマ.n現在時刻 ) * ScrollSpeedBass );
				if ( Math.Min( Math.Min( pChip.nバーからの距離dot.Drums, pChip.nバーからの距離dot.Guitar ), pChip.nバーからの距離dot.Bass ) > 600 )
				{
					break;
				}

                switch ( pChip.nチャンネル番号 )
				{
					#region [ 11-1a: ドラム演奏 ]
					case 0x11:	// ドラム演奏
					case 0x12:
					case 0x13:
					case 0x14:
					case 0x15:
					case 0x16:
					case 0x17:
					case 0x18:
					case 0x19:
					case 0x1a:
                    case 0x1b:
                    case 0x1c:
						if ( !pChip.b空打ちチップである )
						{
							this.t進行描画_チップ_ドラムス模様( configIni, ref dTX, ref pChip );
						}
						break;
					#endregion
					#region [ その他 ]
					default:
						break;
					#endregion 
				}
			}
			//return false;
            return;
		}

		public void t再読込()
		{
			CDTXMania.DTX.t全チップの再生停止とミキサーからの削除();
			this.eフェードアウト完了時の戻り値 = E演奏画面の戻り値.再読込_再演奏;
			base.eフェーズID = CStage.Eフェーズ.演奏_再読込;
			this.bPAUSE = false;

			// #34048 2014.7.16 yyagi
			#region [ 読み込み画面に遷移する前に、設定変更した可能性があるパラメータをConfigIniクラスに書き戻す ]
			for ( int i = 0; i < 3; i++ )
			{
				CDTXMania.ConfigIni.nViewerScrollSpeed[ i ] = CDTXMania.ConfigIni.n譜面スクロール速度[ i ];
			}
			CDTXMania.ConfigIni.b演奏情報を表示する = CDTXMania.ConfigIni.bViewerShowDebugStatus;
			#endregion
		}

		public void t停止()
		{
			CDTXMania.DTX.t全チップの再生停止とミキサーからの削除();
			this.actAVI.Stop();
			this.actBGA.Stop();
			this.actPanel.Stop();				// PANEL表示停止
			CDTXMania.Timer.t一時停止();		// 再生時刻カウンタ停止

			this.n現在のトップChip = CDTXMania.DTX.listChip.Count - 1;    // 終端にシーク

            // 自分自身のOn活性化()相当の処理もすべき。

            this.actScore.Reset();
		}

		public void t演奏位置の変更( int nStartBar )
		{
			// まず全サウンドオフにする
			CDTXMania.DTX.t全チップの再生停止();
			this.actAVI.Stop();
			this.actBGA.Stop();

            this.actScore.Reset();

			#region [ 再生開始小節の変更 ]
			nStartBar++;									// +1が必要

			#region [ 演奏済みフラグのついたChipをリセットする ]
			for ( int i = 0; i < CDTXMania.DTX.listChip.Count; i++ )
			{
				CDTX.CChip pChip = CDTXMania.DTX.listChip[ i ];
				if ( pChip.bHit )
				{
					CDTX.CChip p = (CDTX.CChip) pChip.Clone();
					p.bHit = false;
					CDTXMania.DTX.listChip[ i ] = p;
				}
			}
			#endregion

			#region [ 処理を開始するチップの特定 ]
			//for ( int i = this.n現在のトップChip; i < CDTXMania.DTX.listChip.Count; i++ )
			bool bSuccessSeek = false;
			for ( int i = 0; i < CDTXMania.DTX.listChip.Count; i++ )
			{
				CDTX.CChip pChip = CDTXMania.DTX.listChip[ i ];
				if ( pChip.n発声位置 < 384 * nStartBar )
				{
					continue;
				}
				else
				{
					bSuccessSeek = true;
					this.n現在のトップChip = i;
					break;
				}
			}
			if ( !bSuccessSeek )
			{
				// this.n現在のトップChip = CDTXMania.DTX.listChip.Count - 1;
				this.n現在のトップChip = 0;		// 対象小節が存在しないなら、最初から再生
			}
			#endregion

			#region [ 演奏開始の発声時刻msを取得し、タイマに設定 ]
			int nStartTime = CDTXMania.DTX.listChip[ this.n現在のトップChip ].n発声時刻ms;

			CSound管理.rc演奏用タイマ.tリセット();	// これでPAUSE解除されるので、次のPAUSEチェックは不要
			//if ( !this.bPAUSE )
			//{
				CSound管理.rc演奏用タイマ.t一時停止();
			//}
			CSound管理.rc演奏用タイマ.n現在時刻 = nStartTime;
			#endregion

			List<CSound> pausedCSound = new List<CSound>();

			#region [ BGMやギターなど、演奏開始のタイミングで再生がかかっているサウンドのの途中再生開始 ] // (CDTXのt入力・行解析・チップ配置()で小節番号が+1されているのを削っておくこと)
			for ( int i = this.n現在のトップChip; i >= 0; i-- )
			{
				CDTX.CChip pChip = CDTXMania.DTX.listChip[ i ];
				int nDuration = pChip.GetDuration();

				if ( ( pChip.n発声時刻ms + nDuration > 0 ) && ( pChip.n発声時刻ms <= nStartTime ) && ( nStartTime <= pChip.n発声時刻ms + nDuration ) )
				{
					if ( pChip.bWAVを使うチャンネルである && !pChip.b空打ちチップである )	// wav系チャンネル、且つ、空打ちチップではない
					{
						CDTX.CWAV wc;
						bool b = CDTXMania.DTX.listWAV.TryGetValue( pChip.n整数値_内部番号, out wc );
						if ( !b ) continue;

						if ( ( wc.bIsBGMSound && CDTXMania.ConfigIni.bBGM音を発声する ) || ( !wc.bIsBGMSound ) )
						{
							CDTXMania.DTX.tチップの再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, (int) Eレーン.BGM, CDTXMania.DTX.nモニタを考慮した音量( E楽器パート.UNKNOWN ) );
							#region [ PAUSEする ]
							int j = wc.n現在再生中のサウンド番号;
							if ( wc.rSound[ j ] != null )
							{
							    wc.rSound[ j ].t再生を一時停止する();
							    wc.rSound[ j ].t再生位置を変更する( nStartTime - pChip.n発声時刻ms );
							    pausedCSound.Add( wc.rSound[ j ] );
							}
							#endregion
						}
					}
				}
			}
			#endregion
			#region [ 演奏開始時点で既に表示されているBGAとAVIの、シークと再生 ]
			this.actBGA.SkipStart( nStartTime );
			this.actAVI.SkipStart( nStartTime );
			#endregion
			#region [ PAUSEしていたサウンドを一斉に再生再開する(ただしタイマを止めているので、ここではまだ再生開始しない) ]
			foreach ( CSound cs in pausedCSound )
			{
				cs.tサウンドを再生する();
			}
			pausedCSound.Clear();
			pausedCSound = null;
			#endregion
			#region [ タイマを再開して、PAUSEから復帰する ]
			CSound管理.rc演奏用タイマ.n現在時刻 = nStartTime;
			CDTXMania.Timer.tリセット();						// これでPAUSE解除されるので、3行先の再開()は不要
			CDTXMania.Timer.n現在時刻 = nStartTime;				// Debug表示のTime: 表記を正しくするために必要
			CSound管理.rc演奏用タイマ.t再開();
			//CDTXMania.Timer.t再開();
			this.bPAUSE = false;								// システムがPAUSE状態だったら、強制解除
			this.actPanel.Start();
			#endregion
			#endregion
		}


		/// <summary>
		/// DTXV用の設定をする。(全AUTOなど)
		/// 元の設定のバックアップなどはしないので、あとでConfig.iniを上書き保存しないこと。
		/// </summary>
		protected void tDTXV用の設定()
		{
			CDTXMania.ConfigIni.bAutoPlay.HH = true;
			CDTXMania.ConfigIni.bAutoPlay.SD = true;
			CDTXMania.ConfigIni.bAutoPlay.BD = true;
			CDTXMania.ConfigIni.bAutoPlay.HT = true;
			CDTXMania.ConfigIni.bAutoPlay.LT = true;
			CDTXMania.ConfigIni.bAutoPlay.CY = true;
			CDTXMania.ConfigIni.bAutoPlay.FT = true;
			CDTXMania.ConfigIni.bAutoPlay.RD = true;
			CDTXMania.ConfigIni.bAutoPlay.LC = true;
            CDTXMania.ConfigIni.bAutoPlay.LP = true;
            CDTXMania.ConfigIni.bAutoPlay.LBD = true;
			CDTXMania.ConfigIni.bAutoPlay.GtR = true;
			CDTXMania.ConfigIni.bAutoPlay.GtG = true;
			CDTXMania.ConfigIni.bAutoPlay.GtB = true;
			CDTXMania.ConfigIni.bAutoPlay.GtPick = true;
			CDTXMania.ConfigIni.bAutoPlay.GtW = true;
			CDTXMania.ConfigIni.bAutoPlay.BsR = true;
			CDTXMania.ConfigIni.bAutoPlay.BsG = true;
			CDTXMania.ConfigIni.bAutoPlay.BsB = true;
			CDTXMania.ConfigIni.bAutoPlay.BsPick = true;
			CDTXMania.ConfigIni.bAutoPlay.BsW = true;

			this.bIsAutoPlay = CDTXMania.ConfigIni.bAutoPlay;

			CDTXMania.ConfigIni.bAVI有効 = true;
			CDTXMania.ConfigIni.bBGA有効 = true;
			for ( int i = 0; i < 3; i++ )
			{
				CDTXMania.ConfigIni.bHidden[ i ] = false;
				CDTXMania.ConfigIni.bLeft[ i ] = false;
				CDTXMania.ConfigIni.bLight[ i ] = false;
				CDTXMania.ConfigIni.bReverse[ i ] = false;
				CDTXMania.ConfigIni.bSudden[ i ] = false;
				CDTXMania.ConfigIni.eInvisible[ i ] = EInvisible.OFF;
				CDTXMania.ConfigIni.eRandom[ i ] = Eランダムモード.OFF;
				CDTXMania.ConfigIni.n表示可能な最小コンボ数[ i ] = 65535;
				CDTXMania.ConfigIni.判定文字表示位置[ i ] = E判定文字表示位置.表示OFF;
				// CDTXMania.ConfigIni.n譜面スクロール速度[ i ] = CDTXMania.ConfigIni.nViewerScrollSpeed[ i ];	// これだけはOn活性化()で行うこと。
																												// そうしないと、演奏開始直後にスクロール速度が変化して見苦しい。
                CDTXMania.ConfigIni.bGraph[ i ] = false;
			}

			CDTXMania.ConfigIni.eDark = Eダークモード.OFF;

			CDTXMania.ConfigIni.b演奏情報を表示する = CDTXMania.ConfigIni.bViewerShowDebugStatus;
			CDTXMania.ConfigIni.bフィルイン有効 = true;
			CDTXMania.ConfigIni.bScoreIniを出力する = false;
			CDTXMania.ConfigIni.bSTAGEFAILED有効 = false;
			CDTXMania.ConfigIni.bTight = false;
			CDTXMania.ConfigIni.bストイックモード = false;
			CDTXMania.ConfigIni.bドラム打音を発声する = true;
			CDTXMania.ConfigIni.bBGM音を発声する = true;

			CDTXMania.ConfigIni.nRisky = 0;
			CDTXMania.ConfigIni.nShowLagType = 0;
			CDTXMania.ConfigIni.ドラムコンボ文字の表示位置 = Eドラムコンボ文字の表示位置.OFF;
		}


		private bool bCheckAutoPlay( CDTX.CChip pChip )
		{
			bool bPChipIsAutoPlay = false;
			bool bGtBsR = ( ( pChip.nチャンネル番号 & 4 ) > 0 );
			bool bGtBsG = ( ( pChip.nチャンネル番号 & 2 ) > 0 );
			bool bGtBsB = ( ( pChip.nチャンネル番号 & 1 ) > 0 );
			bool bGtBsW = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x08 );
			bool bGtBsO = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x00 );
			//if ( (
			//        ( ( pChip.e楽器パート == E楽器パート.DRUMS ) && bIsAutoPlay[ this.nチャンネル0Atoレーン07[ pChip.nチャンネル番号 - 0x11 ] ] ) ||
			//        ( ( pChip.e楽器パート == E楽器パート.GUITAR ) && bIsAutoPlay.Guitar ) ) ||
			//        ( ( pChip.e楽器パート == E楽器パート.BASS ) && bIsAutoPlay.Bass )
			//  )
			////				if ((pChip.e楽器パート == E楽器パート.DRUMS) && bIsAutoPlay[this.nチャンネル0Atoレーン07[pChip.nチャンネル番号 - 0x11]])
			//{
			//    bPChipIsAutoPlay = true;
			//}
			if ( pChip.e楽器パート == E楽器パート.DRUMS )
			{
				if ( bIsAutoPlay[ this.nチャンネル0Atoレーン07[ pChip.nチャンネル番号 - 0x11 ] ] )
				{
					bPChipIsAutoPlay = true;
				}
			}
			else if ( pChip.e楽器パート == E楽器パート.GUITAR )
			{
				//Trace.TraceInformation( "chip:{0}{1}{2} ", bGtBsR, bGtBsG, bGtBsB );
				//Trace.TraceInformation( "auto:{0}{1}{2} ", bIsAutoPlay[ (int) Eレーン.GtR ], bIsAutoPlay[ (int) Eレーン.GtG ], bIsAutoPlay[ (int) Eレーン.GtB ]);
				bPChipIsAutoPlay = true;
				if ( bIsAutoPlay[ (int) Eレーン.GtPick ] == false ) bPChipIsAutoPlay = false;
				else
				{
					if ( bGtBsR == true && bIsAutoPlay[ (int) Eレーン.GtR ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsG == true && bIsAutoPlay[ (int) Eレーン.GtG ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsB == true && bIsAutoPlay[ (int) Eレーン.GtB ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsW == true && bIsAutoPlay[ (int) Eレーン.GtW ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsO == true &&
						( bIsAutoPlay[ (int) Eレーン.GtR ] == false || bIsAutoPlay[ (int) Eレーン.GtG ] == false || bIsAutoPlay[ (int) Eレーン.GtB ] == false ) )
						bPChipIsAutoPlay = false;
				}
				//Trace.TraceInformation( "{0:x2}: {1}", pChip.nチャンネル番号, bPChipIsAutoPlay.ToString() );
			}
			else if ( pChip.e楽器パート == E楽器パート.BASS )
			{
				bPChipIsAutoPlay = true;
				if ( bIsAutoPlay[ (int) Eレーン.BsPick ] == false ) bPChipIsAutoPlay = false;
				else
				{
					if ( bGtBsR == true && bIsAutoPlay[ (int) Eレーン.BsR ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsG == true && bIsAutoPlay[ (int) Eレーン.BsG ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsB == true && bIsAutoPlay[ (int) Eレーン.BsB ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsW == true && bIsAutoPlay[ (int) Eレーン.BsW ] == false ) bPChipIsAutoPlay = false;
					else if ( bGtBsO == true &&
						( bIsAutoPlay[ (int) Eレーン.BsR ] == false || bIsAutoPlay[ (int) Eレーン.BsG ] == false || bIsAutoPlay[ (int) Eレーン.BsB ] == false ) )
						bPChipIsAutoPlay = false;
				}
			}
			return bPChipIsAutoPlay;
		}

		protected abstract void t進行描画_チップ_ドラムス模様( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected abstract void t進行描画_チップ_ドラムス( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		//protected abstract void t進行描画_チップ_ギター( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected abstract void t進行描画_チップ_ギターベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst );
		/// <summary>
		/// ギター・ベースのチップ表示
		/// </summary>
		/// <param name="configIni"></param>
		/// <param name="dTX"></param>
		/// <param name="pChip">描画するチップ</param>
		/// <param name="inst">楽器種別</param>
		/// <param name="barYNormal">Normal時判定ライン表示Y座標</param>
		/// <param name="barYReverse">Reverse時判定ライン表示Y座標</param>
		/// <param name="showRangeY0">チップ表示Y座標範囲(最小値)</param>
		/// <param name="showRangeY1">チップ表示Y座標範囲(最大値)</param>
		/// <param name="openXg">オープンチップの表示X座標(ギター用)</param>
		/// <param name="openXb">オープンチップの表示X座標(ベース用)</param>
		/// <param name="rectOpenOffsetX">テクスチャ内のオープンチップregionのx座標</param>
		/// <param name="rectOpenOffsetY">テクスチャ内のオープンチップregionのy座標</param>
		/// <param name="openChipWidth">テクスチャ内のオープンチップregionのwidth</param>
		/// <param name="chipHeight">テクスチャ内のチップのheight</param>
		/// <param name="chipWidth">テクスチャ内のチップのwidth</param>
		/// <param name="guitarNormalX">ギターチップ描画のx座標(Normal)</param>
		/// <param name="guitarLeftyX">ギターチップ描画のx座標(Lefty)</param>
		/// <param name="bassNormalX">ベースチップ描画のx座標(Normal)</param>
		/// <param name="bassLeftyX">ベースチップ描画のx座標(Lefty)</param>
		/// <param name="drawDeltaX">描画のX座標間隔(R,G,B...)</param>
		/// <param name="chipTexDeltaX">テクスチャののX座標間隔(R,G,B...)</param>
		protected void t進行描画_チップ_ギターベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst,
			int barYNormal, int barYReverse,
			int showRangeY0, int showRangeY1, int openXg, int openXb,
			int rectOpenOffsetX, int rectOpenOffsetY, int openChipWidth, int chipHeight, int chipWidth,
			int guitarNormalX, int guitarLeftyX, int bassNormalX, int bassLeftyX, int drawDeltaX, int chipTexDeltaX )
		{
			int instIndex = (int) inst;
			if ( configIni.bGuitar有効 )
			{
				#region [ Invisible処理 ]
				if ( configIni.eInvisible[ instIndex ] != EInvisible.OFF )
				{
					cInvisibleChip.SetInvisibleStatus( ref pChip );
				}
				#endregion
				else
				{
					#region [ Hidden/Sudden処理 ]
					if ( configIni.bSudden[ instIndex ] )
					{
						pChip.b可視 = ( pChip.nバーからの距離dot[ instIndex ] < 200 * Scale.Y );
					}
					if ( configIni.bHidden[ instIndex ] && ( pChip.nバーからの距離dot[ instIndex ] < 100 * Scale.Y ) )
					{
						pChip.b可視 = false;
					}
					#endregion
				}

                //このスパゲッティコードクソすぎだから5レーン化の時に大幅改良したい。
                //bool bChipHasR = ( ( pChip.nチャンネル番号 & 4 ) > 0 );
                //bool bChipHasG = ( ( pChip.nチャンネル番号 & 2 ) > 0 );
                //bool bChipHasB = ( ( pChip.nチャンネル番号 & 1 ) > 0 );
                bool bChipHasR = false;
                bool bChipHasG = false;
                bool bChipHasB = false;
                bool bChipHasY = false;
                bool bChipHasP = false;
				bool bChipHasW = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x08 );
				bool bChipIsO = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x00 );

                CDTXMania.DTX.tチャンネル番号からチップフラグを返す( pChip.nチャンネル番号, ref bChipHasR, ref bChipHasG, ref bChipHasB, ref bChipHasY, ref bChipHasP );

				#region [ chip描画 ]
				int OPEN = ( inst == E楽器パート.GUITAR ) ? 0x20 : 0xA0;
				if ( !pChip.bHit && pChip.b可視 )
				{
					if ( this.txチップ != null )
					{
						this.txチップ.n透明度 = pChip.n透明度;
					}
					int y = configIni.bReverse[ instIndex ] ?
						(int) ( barYReverse - pChip.nバーからの距離dot[ instIndex ] ) :
						(int) ( barYNormal  + pChip.nバーからの距離dot[ instIndex ] );
					int n小節線消失距離dot = configIni.bReverse[ instIndex ] ?
						(int) ( -100 * Scale.Y ) :
						( configIni.e判定位置[ instIndex ] == E判定位置.標準 ) ? (int) ( -36 * Scale.Y ) : (int) ( -25 * Scale.Y );
					if ( ( 104 < y ) && ( y < 720 ) )
					{
						if ( this.txチップ != null )
						{
							#region [ RGBチップのX座標初期化 ]
							int x;
                            int[] nChipX = new int[] { 6, 45, 84, 123, 162 };
							if ( inst == E楽器パート.GUITAR )
							{
								//x = ( configIni.bLeft.Guitar ) ? 162 : 86;
                                x = 83;
                                nChipX = ( !configIni.bLeft.Guitar ) ? new int[] { 6, 45, 84, 123, 162 } : new int[] { 162, 123, 84, 45, 6 };
							}
							else
							{
								//x = ( configIni.bLeft.Bass ) ? 552 : 954;
                                x = 951;
                                nChipX = ( !configIni.bLeft.Bass ) ? new int[] { 6, 45, 84, 123, 162 } : new int[] { 162, 123, 84, 45, 6 };
							}
                            #region [ OPENチップの描画 ]
							if ( pChip.nチャンネル番号 == OPEN )
			                {
								this.txチップ.t2D描画( CDTXMania.app.Device, x, y - 4, new Rectangle( 0, 11, 196, 9 ) );
							}
							#endregion
							#endregion
							//Trace.TraceInformation( "chip={0:x2}, E楽器パート={1}, x={2}", pChip.nチャンネル番号, inst, x );
							#region [ Rチップ描画 ]
							if( bChipHasR )
							{
								//this.txチップ.t2D描画( CDTXMania.app.Device, x + 3, y - 4, new Rectangle( 1, 1, 35, 8 ) );
								this.txチップ.t2D描画( CDTXMania.app.Device, x + nChipX[ 0 ], y - 4, new Rectangle( 1, 1, 35, 8 ) );
							}
							#endregion
							#region [ Gチップ描画 ]
							//x += 39;
							if( bChipHasG )
							{
								//this.txチップ.t2D描画( CDTXMania.app.Device, x + 3, y - 4, new Rectangle( 39, 1, 35, 8 ) );
								this.txチップ.t2D描画( CDTXMania.app.Device, x + nChipX[ 1 ], y - 4, new Rectangle( 39, 1, 35, 8 ) );
							}
							#endregion
							#region [ Bチップ描画 ]
                            //x += 39;
							if( bChipHasB )
							{
								//this.txチップ.t2D描画( CDTXMania.app.Device, x + 3, y - 4, new Rectangle( 77, 1, 35, 8 ) );
								this.txチップ.t2D描画( CDTXMania.app.Device, x + nChipX[ 2 ], y - 4, new Rectangle( 77, 1, 35, 8 ) );
							}
							#endregion
						}
					}
				}
				#endregion
				//if ( ( configIni.bAutoPlay.Guitar && !pChip.bHit ) && ( pChip.nバーからの距離dot.Guitar < 0 ) )


                // #35411 2015.08.20 chnmr0 modified
                // 従来のAUTO処理に加えてプレーヤーゴーストの再生機能を追加
                bool autoPlayCondition = (!pChip.bHit) && (pChip.nバーからの距離dot[instIndex] < 0);
				if ( autoPlayCondition )
                {
                    cInvisibleChip.StartSemiInvisible( inst );
                }

                bool autoPick = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtPick : bIsAutoPlay.BsPick;
                autoPlayCondition = !pChip.bHit && autoPick;
                long ghostLag = 0;
                bool bUsePerfectGhost = true;

                if ( (pChip.e楽器パート == E楽器パート.GUITAR || pChip.e楽器パート == E楽器パート.BASS ) &&
                    CDTXMania.ConfigIni.eAutoGhost[(int)(pChip.e楽器パート)] != EAutoGhostData.PERFECT &&
                    CDTXMania.listAutoGhostLag[(int)pChip.e楽器パート] != null &&
                    0 <= pChip.n楽器パートでの出現順 &&
                    pChip.n楽器パートでの出現順 < CDTXMania.listAutoGhostLag[(int)pChip.e楽器パート].Count)
                {
                	// #35411 (mod) Ghost data が有効なので 従来のAUTOではなくゴーストのラグを利用
                	// 発生時刻と現在時刻からこのタイミングで演奏するかどうかを決定
					ghostLag = CDTXMania.listAutoGhostLag[(int)pChip.e楽器パート][pChip.n楽器パートでの出現順];
					bool resetCombo = ghostLag > 255;
					ghostLag = (ghostLag & 255) - 128;
					ghostLag -= (pChip.e楽器パート == E楽器パート.GUITAR ? nInputAdjustTimeMs.Guitar : nInputAdjustTimeMs.Bass);
                    autoPlayCondition &= (pChip.n発声時刻ms + ghostLag <= CSound管理.rc演奏用タイマ.n現在時刻ms);
					if (resetCombo && autoPlayCondition )
					{
						this.actCombo.n現在のコンボ数[(int)pChip.e楽器パート] = 0;
					}
					bUsePerfectGhost = false;
                }
                
                if( bUsePerfectGhost )
                {
                	// 従来のAUTOを使用する場合
                    autoPlayCondition &= (pChip.nバーからの距離dot[instIndex] < 0);
                }

                if ( autoPlayCondition )
                {
					int lo = ( inst == E楽器パート.GUITAR ) ? 0 : 5;	// lane offset
					bool autoR = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtR : bIsAutoPlay.BsR;
					bool autoG = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtG : bIsAutoPlay.BsG;
					bool autoB = ( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtB : bIsAutoPlay.BsB;
					bool pushingR = CDTXMania.Pad.b押されている( inst, Eパッド.R );
					bool pushingG = CDTXMania.Pad.b押されている( inst, Eパッド.G );
					bool pushingB = CDTXMania.Pad.b押されている( inst, Eパッド.B );

					#region [ Chip Fire effects (auto時用) ]
                    // autoPickでない時の処理は、 t入力処理・ギターベース(E楽器パート) で行う
                    bool bSuccessOPEN = bChipIsO && (autoR || !pushingR) && (autoG || !pushingG) && (autoB || !pushingB);
					{
						if ( ( bChipHasR && ( autoR || pushingR ) ) || bSuccessOPEN )
						{
							this.actChipFireGB.Start( 0 + lo, 演奏判定ライン座標 );
						}
						if ( ( bChipHasG && ( autoG || pushingG ) ) || bSuccessOPEN )
						{
							this.actChipFireGB.Start( 1 + lo, 演奏判定ライン座標 );
						}
						if ( ( bChipHasB && ( autoB || pushingB ) ) || bSuccessOPEN )
						{
							this.actChipFireGB.Start( 2 + lo, 演奏判定ライン座標 );
						}
						if ( bSuccessOPEN )
						{
							this.actChipFireGB.Start( 3 + lo, 演奏判定ライン座標 );
						}
						if ( bSuccessOPEN )
						{
							this.actChipFireGB.Start( 4 + lo, 演奏判定ライン座標 );
						}
					}
					#endregion
					#region [ autopick ]
					{
						bool bMiss = true;
						if ( bChipHasR == autoR && bChipHasG == autoG && bChipHasB == autoB )		// autoレーンとチップレーン一致時はOK
						{																			// この条件を加えないと、同時に非autoレーンを押下している時にNGとなってしまう。
							bMiss = false;
						}
						else if ( ( autoR || ( bChipHasR == pushingR ) ) && ( autoG || ( bChipHasG == pushingG ) ) && ( autoB || ( bChipHasB == pushingB ) ) )
							// ( bChipHasR == ( pushingR | autoR ) ) && ( bChipHasG == ( pushingG | autoG ) ) && ( bChipHasB == ( pushingB | autoB ) ) )
						{
							bMiss = false;
						}
						else if ( ( ( bChipIsO == true ) && ( !pushingR | autoR ) && ( !pushingG | autoG ) && ( !pushingB | autoB ) ) )	// OPEN時
						{
							bMiss = false;
						}
						pChip.bHit = true;
						this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms + ghostLag, inst, dTX.nモニタを考慮した音量( inst ), false, bMiss );
						this.r次にくるギターChip = null;
						if ( !bMiss )
						{
							this.tチップのヒット処理(pChip.n発声時刻ms + ghostLag, pChip);
						}
						else
						{
							pChip.nLag = 0;		// tチップのヒット処理()の引数最後がfalseの時はpChip.nLagを計算しないため、ここでAutoPickかつMissのLag=0を代入
							this.tチップのヒット処理(pChip.n発声時刻ms + ghostLag, pChip, false);
						}
						int chWailingChip = ( inst == E楽器パート.GUITAR ) ? 0x28 : 0xA8;
						CDTX.CChip item = this.r指定時刻に一番近い未ヒットChip(pChip.n発声時刻ms + ghostLag, chWailingChip, this.nInputAdjustTimeMs[instIndex], 140);
						if ( item != null && !bMiss )
						{
							this.queWailing[ instIndex ].Enqueue( item );
						}
					}
					#endregion
					// #35411 modify end
				}

                if( pChip.e楽器パート == E楽器パート.GUITAR && CDTXMania.ConfigIni.bGraph.Guitar )
                {
                    #region[ ギターゴースト ]
                    if (CDTXMania.ConfigIni.eTargetGhost.Guitar != ETargetGhostData.NONE &&
                        CDTXMania.listTargetGhsotLag.Guitar != null)
                    {
                        double val = 0;
                        if (CDTXMania.ConfigIni.eTargetGhost.Guitar == ETargetGhostData.ONLINE)
                        {
                            if (CDTXMania.DTX.n可視チップ数.Guitar > 0)
                            {
                            	// Online Stats の計算式
                                val = 100 *
                                    (this.nヒット数_TargetGhost.Guitar.Perfect * 17 +
                                     this.nヒット数_TargetGhost.Guitar.Great * 7 +
                                     this.n最大コンボ数_TargetGhost.Guitar * 3) / (20.0 * CDTXMania.DTX.n可視チップ数.Guitar);
                            }
                        }
                        else
                        {
                            if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                            {
                                val = CScoreIni.t演奏型スキルを計算して返す(
                                    CDTXMania.DTX.n可視チップ数.Guitar,
                                    this.nヒット数_TargetGhost.Guitar.Perfect,
                                    this.nヒット数_TargetGhost.Guitar.Great,
                                    this.nヒット数_TargetGhost.Guitar.Good,
                                    this.nヒット数_TargetGhost.Guitar.Poor,
                                    this.nヒット数_TargetGhost.Guitar.Miss,
                                    E楽器パート.GUITAR, new STAUTOPLAY());
                            }
                            else
                            {
                                val = CScoreIni.tXG演奏型スキルを計算して返す(
                                    CDTXMania.DTX.n可視チップ数.Guitar,
                                    this.nヒット数_TargetGhost.Guitar.Perfect,
                                    this.nヒット数_TargetGhost.Guitar.Great,
                                    this.nヒット数_TargetGhost.Guitar.Good,
                                    this.nヒット数_TargetGhost.Guitar.Poor,
                                    this.nヒット数_TargetGhost.Guitar.Miss,
                                    this.actCombo.n現在のコンボ数Ghost.Guitar最高値,
                                    E楽器パート.GUITAR, new STAUTOPLAY());
                            }

                        }
                        if (val < 0) val = 0;
                        if (val > 100) val = 100;
                        this.actGraph.dbグラフ値目標_渡 = val;
                    }
                    #endregion
                }
                else if( pChip.e楽器パート == E楽器パート.BASS && CDTXMania.ConfigIni.bGraph.Bass )
                {
                    #region[ ベースゴースト ]
                    if (CDTXMania.ConfigIni.eTargetGhost.Bass != ETargetGhostData.NONE &&
                        CDTXMania.listTargetGhsotLag.Bass != null)
                    {
                        double val = 0;
                        if (CDTXMania.ConfigIni.eTargetGhost.Bass == ETargetGhostData.ONLINE)
                        {
                            if (CDTXMania.DTX.n可視チップ数.Bass > 0)
                            {
                            	// Online Stats の計算式
                                val = 100 *
                                    (this.nヒット数_TargetGhost.Bass.Perfect * 17 +
                                     this.nヒット数_TargetGhost.Bass.Great * 7 +
                                     this.n最大コンボ数_TargetGhost.Bass * 3) / (20.0 * CDTXMania.DTX.n可視チップ数.Bass);
                            }
                        }
                        else
                        {
                            if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                            {
                                val = CScoreIni.t演奏型スキルを計算して返す(
                                    CDTXMania.DTX.n可視チップ数.Bass,
                                    this.nヒット数_TargetGhost.Bass.Perfect,
                                    this.nヒット数_TargetGhost.Bass.Great,
                                    this.nヒット数_TargetGhost.Bass.Good,
                                    this.nヒット数_TargetGhost.Bass.Poor,
                                    this.nヒット数_TargetGhost.Bass.Miss,
                                    E楽器パート.BASS, new STAUTOPLAY());
                            }
                            else
                            {
                                val = CScoreIni.tXG演奏型スキルを計算して返す(
                                    CDTXMania.DTX.n可視チップ数.Bass,
                                    this.nヒット数_TargetGhost.Bass.Perfect,
                                    this.nヒット数_TargetGhost.Bass.Great,
                                    this.nヒット数_TargetGhost.Bass.Good,
                                    this.nヒット数_TargetGhost.Bass.Poor,
                                    this.nヒット数_TargetGhost.Bass.Miss,
                                    this.actCombo.n現在のコンボ数Ghost.Bass最高値,
                                    E楽器パート.BASS, new STAUTOPLAY());
                            }

                        }
                        if (val < 0) val = 0;
                        if (val > 100) val = 100;
                        this.actGraph.dbグラフ値目標_渡 = val;
                    }
                    #endregion
                }


				return;
			}	// end of "if configIni.bGuitar有効"
			if ( !pChip.bHit && ( pChip.nバーからの距離dot[ instIndex ] < 0 ) )	// Guitar/Bass無効の場合は、自動演奏する
			{
				pChip.bHit = true;
				this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻 + pChip.n発声時刻ms, inst, dTX.nモニタを考慮した音量( inst ) );
			}
		}

		
		protected virtual void t進行描画_チップ_ギターベース_ウェイリング(
			CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, E楽器パート inst, bool bGRmode )
		{
			int indexInst = (int) inst;
			if ( configIni.bGuitar有効 )
			{
				#region [ Invisible処理 ]
				if ( configIni.eInvisible[ indexInst ] != EInvisible.OFF )
				{
					cInvisibleChip.SetInvisibleStatus( ref pChip );
				}
				#endregion
				#region [ Sudden/Hidden処理 ]
				if ( configIni.bSudden[indexInst] )
				{
					pChip.b可視 = ( pChip.nバーからの距離dot[indexInst] < 200 * Scale.Y );
				}
				if ( configIni.bHidden[indexInst] && ( pChip.nバーからの距離dot[indexInst] < 100 * Scale.Y ) )
				{
					pChip.b可視 = false;
				}
				#endregion

				cWailingChip[ indexInst ].t進行描画_チップ_ギターベース_ウェイリング(
					configIni, ref dTX, ref pChip,
					ref txチップ, ref 演奏判定ライン座標, ref ctWailingチップ模様アニメ
				);

				if ( !pChip.bHit && ( pChip.nバーからの距離dot[indexInst] < 0 ) )
				{
					if ( pChip.nバーからの距離dot[indexInst] < -234 * Scale.Y )	// #25253 2011.5.29 yyagi: Don't set pChip.bHit=true for wailing at once. It need to 1sec-delay (234pix per 1sec). 
					{
						pChip.bHit = true;
					}
					bool autoW = ( inst == E楽器パート.GUITAR ) ? configIni.bAutoPlay.GtW : configIni.bAutoPlay.BsW;
					//if ( configIni.bAutoPlay[ ((int) Eレーン.Guitar - 1) + indexInst ] )	// このような、バグの入りやすい書き方(GT/BSのindex値が他と異なる)はいずれ見直したい
					if ( autoW )
					{
					//    pChip.bHit = true;								// #25253 2011.5.29 yyagi: Set pChip.bHit=true if autoplay.
					//    this.actWailingBonus.Start( inst, this.r現在の歓声Chip[indexInst] );
					// #23886 2012.5.22 yyagi; To support auto Wailing; Don't do wailing for ALL wailing chips. Do wailing for queued wailing chip.
					// wailing chips are queued when 1) manually wailing and not missed at that time 2) AutoWailing=ON and not missed at that time
						long nTimeStamp_Wailed = pChip.n発声時刻ms + CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻;
						DoWailingFromQueue( inst, nTimeStamp_Wailed, autoW );
					}
					cInvisibleChip.StartSemiInvisible( inst );
				}
				return;
			}
			pChip.bHit = true;
		}
		protected virtual void t進行描画_チップ_ギター_ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, bool bGRmode )
		{
			t進行描画_チップ_ギターベース_ウェイリング( configIni, ref dTX, ref pChip, E楽器パート.GUITAR, bGRmode );
		}
		protected abstract void t進行描画_チップ_フィルイン( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected abstract void t進行描画_チップ_小節線( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		//protected abstract void t進行描画_チップ_ベース( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected virtual void t進行描画_チップ_ベース_ウェイリング( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip, bool bGRmode )
		{
			t進行描画_チップ_ギターベース_ウェイリング( configIni, ref dTX, ref pChip, E楽器パート.BASS, bGRmode );
		}

	
		
		protected abstract void t進行描画_チップ_空打ち音設定_ドラム( CConfigIni configIni, ref CDTX dTX, ref CDTX.CChip pChip );
		protected void t進行描画_チップアニメ()
		{
			for ( int i = 0; i < 3; i++ )			// 0=drums, 1=guitar, 2=bass
			{
				if ( this.ctチップ模様アニメ[ i ] != null )
				{
					this.ctチップ模様アニメ[ i ].t進行Loop();
				}
			}
			if ( this.ctWailingチップ模様アニメ != null )
			{
				this.ctWailingチップ模様アニメ.t進行Loop();
			}
            if( this.actCombo.ctコンボ動作タイマ != null )
            {
                this.actCombo.ctコンボ動作タイマ.t進行Loop();
            }
		}

		protected bool t進行描画_フェードイン_アウト()
		{
			switch ( base.eフェーズID )
			{
				case CStage.Eフェーズ.共通_フェードイン:
					if ( this.actFI.On進行描画() != 0 )
					{
						base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
					}
					break;

				case CStage.Eフェーズ.共通_フェードアウト:
				case CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト:
					if ( this.actFO.On進行描画() != 0 )
					{
						return true;
					}
					break;

				case CStage.Eフェーズ.演奏_STAGE_CLEAR_フェードアウト:
					if ( this.actFOClear.On進行描画() == 0 )
					{
						break;
					}
					return true;
		
			}
			return false;
		}
		protected void t進行描画_レーンフラッシュD()
		{
			if ( ( CDTXMania.ConfigIni.eDark == Eダークモード.OFF ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED ) && ( base.eフェーズID != CStage.Eフェーズ.演奏_STAGE_FAILED_フェードアウト ) )
			{
				this.actLaneFlushD.On進行描画();
			}
		}
		protected void t進行描画_レーンフラッシュGB()
		{
			if ( ( CDTXMania.ConfigIni.eDark == Eダークモード.OFF ) && CDTXMania.ConfigIni.bGuitar有効 )
			{
				this.actLaneFlushGB.On進行描画();
			}
		}
		protected abstract void t進行描画_演奏情報();
		protected void t進行描画_演奏情報(int x, int y)
		{
			if ( !CDTXMania.ConfigIni.b演奏情報を表示しない )
			{
				this.actPlayInfo.t進行描画( x, y );
			}
		}
		protected void t進行描画_背景()
		{
			if ( CDTXMania.ConfigIni.eDark == Eダークモード.OFF )
			{
				if ( this.tx背景 != null )
				{
					this.tx背景.t2D描画( CDTXMania.app.Device, 0, 0 );
				}
			}
			else
			{
				// FullHD版では、背景描画のさらに奥でAVI描画をするため、
				// Dark!=OFF時下記の画面クリアをすると、AVI描画がクリアされてしまう
				// CDTXMania.app.Device.Clear( ClearFlags.ZBuffer | ClearFlags.Target, Color.Black, 0f, 0 );
			}
		}

		protected void t進行描画_判定ライン()
		{
            if( CDTXMania.ConfigIni.bDrums有効 )
            {
                int y = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );
                //if (CDTXMania.ConfigIni.bJudgeLineDisp.Drums)
                {
                    // #31602 2013.6.23 yyagi 描画遅延対策として、判定ラインの表示位置をオフセット調整できるようにする
                    this.txヒットバー.t2D描画( CDTXMania.app.Device, 295, y, new Rectangle( 0, 0, 558, 6 ) );
                }
                //if (CDTXMania.ConfigIni.b演奏情報を表示する)
                    //this.actLVFont.t文字列描画(295, (CDTXMania.ConfigIni.bReverse.Drums ? y - 20 : y + 8), CDTXMania.ConfigIni.nJudgeLine.Drums.ToString());
            }
		}
		protected void t進行描画_判定文字列()
		{
			this.actJudgeString.t進行描画( 演奏判定ライン座標 );
		}
		protected void t進行描画_判定文字列1_通常位置指定の場合()
		{
			if ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Drums ) != E判定文字表示位置.コンボ下 )	// 判定ライン上または横
			{
				this.actJudgeString.t進行描画( 演奏判定ライン座標 );
			}
		}
		protected void t進行描画_判定文字列2_判定ライン上指定の場合()
		{
			if ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Drums ) == E判定文字表示位置.コンボ下 )	// 判定ライン上または横
			{
				this.actJudgeString.t進行描画( 演奏判定ライン座標 );
			}
		}

		protected void t進行描画_譜面スクロール速度()
		{
			this.act譜面スクロール速度.On進行描画();
		}

		protected abstract void t背景テクスチャの生成();
		protected void t背景テクスチャの生成( string DefaultBgFilename, Rectangle bgrect, string bgfilename )
		{
			Bitmap image = null;
			bool bSuccessLoadDTXbgfile = false;
			


            //2016.06.18 kairera0467
            //--ムービークリップが使用されない曲の場合、デフォルトの背景画像を表示させる。
            //--ムービークリップが使用される曲の場合、黒背景を表示させる。(tx背景を初期化したままにしなければ、ムービーを描画できない。)
            //--BGAの場合、デフォルトの背景画像の上に、BGAの大きさの黒背景を表示させる。
            if( File.Exists( CSkin.Path( DefaultBgFilename ) ) && CDTXMania.DTX.bチップがある.Movie == false )
            {
                this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( DefaultBgFilename ) );
                if( CDTXMania.DTX.listBGA.Count != 0 )
                {

                }
            }
            else
            {
                image = new Bitmap( 1280, 720 );
                this.tx背景 = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );
            }
		}

		protected virtual void t入力処理_ギター()
		{
			t入力処理_ギターベース( E楽器パート.GUITAR );
		}
		protected virtual void t入力処理_ベース()
		{
			t入力処理_ギターベース( E楽器パート.BASS );
		}


		protected virtual void t入力処理_ギターベース(E楽器パート inst)
		{
			int indexInst = (int) inst;
			#region [ スクロール速度変更 ]
			if ( CDTXMania.Pad.b押されている( inst, Eパッド.Decide ) && CDTXMania.Pad.b押された( inst, Eパッド.B ) )
			{
				CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] = Math.Min( CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] + 1, 0x7cf );
			}
			if ( CDTXMania.Pad.b押されている( inst, Eパッド.Decide ) && CDTXMania.Pad.b押された( inst, Eパッド.R ) )
			{
				CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] = Math.Max( CDTXMania.ConfigIni.n譜面スクロール速度[indexInst] - 1, 0 );
			}
			#endregion

			if ( !CDTXMania.ConfigIni.bGuitar有効 || !CDTXMania.DTX.bチップがある[indexInst] )
			{
				return;
			}

			int R = ( inst == E楽器パート.GUITAR ) ? 0 : 5;
			int G = R + 1;
			int B = R + 2;
            int Y = R + 3;
            int P = R + 4;
			bool autoW =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtW : bIsAutoPlay.BsW;
			bool autoR =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtR : bIsAutoPlay.BsR;
			bool autoG =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtG : bIsAutoPlay.BsG;
			bool autoB =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtB : bIsAutoPlay.BsB;
			bool autoPick =	( inst == E楽器パート.GUITAR ) ? bIsAutoPlay.GtPick : bIsAutoPlay.BsPick;
			int nAutoW = ( autoW ) ? 8 : 0;
			int nAutoR = ( autoR ) ? 4 : 0;
			int nAutoG = ( autoG ) ? 2 : 0;
			int nAutoB = ( autoB ) ? 1 : 0;
			int nAutoMask = nAutoW | nAutoR | nAutoG | nAutoB;

//			if ( bIsAutoPlay[ (int) Eレーン.Guitar - 1 + indexInst ] )	// このような、バグの入りやすい書き方(GT/BSのindex値が他と異なる)はいずれ見直したい
//			{
			CDTX.CChip chip = this.r次に来る指定楽器Chipを更新して返す(inst);
			if ( chip != null )	
			{
				if ( ( chip.nチャンネル番号 & 4 ) != 0 && autoR )
				{
					this.actLaneFlushGB.Start( R );
					this.actRGB.Push( R );
				}
				if ( ( chip.nチャンネル番号 & 2 ) != 0 && autoG )
				{
					this.actLaneFlushGB.Start( G );
					this.actRGB.Push( G );
				}
				if ( ( chip.nチャンネル番号 & 1 ) != 0 && autoB )
				{
					this.actLaneFlushGB.Start( B );
					this.actRGB.Push( B );
				}
//			}

			}
//			else
			{
				int pushingR = CDTXMania.Pad.b押されている( inst, Eパッド.R ) ? 4 : 0;
				this.t入力メソッド記憶( inst );
				int pushingG = CDTXMania.Pad.b押されている( inst, Eパッド.G ) ? 2 : 0;
				this.t入力メソッド記憶( inst );
				int pushingB = CDTXMania.Pad.b押されている( inst, Eパッド.B ) ? 1 : 0;
				this.t入力メソッド記憶( inst );
				int flagRGB = pushingR | pushingG | pushingB;
				if ( pushingR != 0 )
				{
					this.actLaneFlushGB.Start( R );
					this.actRGB.Push( R );
				}
				if ( pushingG != 0 )
				{
					this.actLaneFlushGB.Start( G );
					this.actRGB.Push( G );
				}
				if ( pushingB != 0 )
				{
					this.actLaneFlushGB.Start( B );
					this.actRGB.Push( B );
				}
				// auto pickだとここから先に行かないので注意
				List<STInputEvent> events = CDTXMania.Pad.GetEvents( inst, Eパッド.Pick );
				if ( ( events != null ) && ( events.Count > 0 ) )
				{
					foreach ( STInputEvent eventPick in events )
					{
						if ( !eventPick.b押された )
						{
							continue;
						}
						this.t入力メソッド記憶( inst );
						long nTime = eventPick.nTimeStamp - CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻;
						int chWailingSound = ( inst == E楽器パート.GUITAR ) ? 0x2F : 0xAF;
						CDTX.CChip pChip = this.r指定時刻に一番近い未ヒットChip( nTime, chWailingSound, this.nInputAdjustTimeMs[indexInst] );	// E楽器パート.GUITARなチップ全てにヒットする
						E判定 e判定 = this.e指定時刻からChipのJUDGEを返す( nTime, pChip, this.nInputAdjustTimeMs[indexInst] );
//Trace.TraceInformation("ch={0:x2}, mask1={1:x1}, mask2={2:x2}", pChip.nチャンネル番号,  ( pChip.nチャンネル番号 & ~nAutoMask ) & 0x0F, ( flagRGB & ~nAutoMask) & 0x0F );
						if ( ( pChip != null ) && ( ( ( pChip.nチャンネル番号 & ~nAutoMask ) & 0x0F ) == ( ( flagRGB & ~nAutoMask) & 0x0F ) ) && ( e判定 != E判定.Miss ) )
						{
							bool bChipHasR = ( ( pChip.nチャンネル番号 & 4 ) > 0 );
							bool bChipHasG = ( ( pChip.nチャンネル番号 & 2 ) > 0 );
							bool bChipHasB = ( ( pChip.nチャンネル番号 & 1 ) > 0 );
							bool bChipHasW = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x08 );
							bool bChipIsO = ( ( pChip.nチャンネル番号 & 0x0F ) == 0x00 );
							bool bSuccessOPEN = bChipIsO && ( autoR || pushingR == 0 ) && ( autoG || pushingG == 0 ) && ( autoB || pushingB == 0 );
							if ( ( bChipHasR && ( autoR || pushingR != 0 ) ) || bSuccessOPEN )
							//if ( ( pushingR != 0 ) || autoR || ( flagRGB == 0 ) )
							{
								this.actChipFireGB.Start( R, 演奏判定ライン座標 );
							}
							if ( ( bChipHasG && ( autoG || pushingG != 0 ) ) || bSuccessOPEN )
							//if ( ( pushingG != 0 ) || autoG || ( flagRGB == 0 ) )
							{
								this.actChipFireGB.Start( G, 演奏判定ライン座標 );
							}
							if ( ( bChipHasB && ( autoB || pushingB != 0 ) ) || bSuccessOPEN )
							//if ( ( pushingB != 0 ) || autoB || ( flagRGB == 0 ) )
							{
								this.actChipFireGB.Start( B, 演奏判定ライン座標 );
							}
							this.tチップのヒット処理( nTime, pChip );
							this.tサウンド再生( pChip, CSound管理.rc演奏用タイマ.nシステム時刻, inst, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する[indexInst], e判定 == E判定.Poor );
							int chWailingChip = ( inst == E楽器パート.GUITAR ) ? 0x28 : 0xA8;
							CDTX.CChip item = this.r指定時刻に一番近い未ヒットChip( nTime, chWailingChip, this.nInputAdjustTimeMs[ indexInst ], 140 );
							if ( item != null )
							{
								this.queWailing[indexInst].Enqueue( item );
							}
							continue;
						}

						// 以下、間違いレーンでのピック時
						CDTX.CChip NoChipPicked = ( inst == E楽器パート.GUITAR ) ? this.r現在の空うちギターChip : this.r現在の空うちベースChip;
						if ( ( NoChipPicked != null ) || ( ( NoChipPicked = this.r指定時刻に一番近いChip_ヒット未済問わず不可視考慮( nTime, chWailingSound, this.nInputAdjustTimeMs[indexInst] ) ) != null ) )
						{
							this.tサウンド再生( NoChipPicked, CSound管理.rc演奏用タイマ.nシステム時刻, inst, CDTXMania.ConfigIni.n手動再生音量, CDTXMania.ConfigIni.b演奏音を強調する[indexInst], true );
						}
						if ( !CDTXMania.ConfigIni.bLight[indexInst] )
						{
							this.tチップのヒット処理_BadならびにTight時のMiss( inst );
						}
					}
				}
				List<STInputEvent> list = CDTXMania.Pad.GetEvents(inst, Eパッド.Wail );
				if ( ( list != null ) && ( list.Count > 0 ) )
				{
					foreach ( STInputEvent eventWailed in list )
					{
						if ( !eventWailed.b押された )
						{
							continue;
						}
						DoWailingFromQueue( inst, eventWailed.nTimeStamp,  autoW );
					}
				}
			}
		}

		private void DoWailingFromQueue( E楽器パート inst, long nTimeStamp_Wailed,  bool autoW )
		{
			int indexInst = (int) inst;
			long nTimeWailed = nTimeStamp_Wailed - CSound管理.rc演奏用タイマ.n前回リセットした時のシステム時刻;
			CDTX.CChip chipWailing;
			while ( ( this.queWailing[ indexInst ].Count > 0 ) && ( ( chipWailing = this.queWailing[ indexInst ].Dequeue() ) != null ) )
			{
				if ( ( nTimeWailed - chipWailing.n発声時刻ms ) <= 1000 )		// #24245 2011.1.26 yyagi: 800 -> 1000
				{
					chipWailing.bHit = true;
					this.actWailingBonus.Start( inst, this.r現在の歓声Chip[ indexInst ] );
					//if ( !bIsAutoPlay[indexInst] )
					if ( !autoW )
					{
						int nCombo = ( this.actCombo.n現在のコンボ数[ indexInst ] < 500 ) ? this.actCombo.n現在のコンボ数[ indexInst ] : 500;
						this.actScore.Add( inst, bIsAutoPlay, nCombo * 3000L );		// #24245 2011.1.26 yyagi changed DRUMS->BASS, add nCombo conditions
					}
				}
			}
		}
        #endregion
	}
}
