using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DTXMania
{
	#region [ Ech定義 ]
	public enum Ech定義
	{
		BGM					= 0x01,
		BarLength			= 0x02,
		BPM					= 0x03,
		BGALayer1			= 0x04,
		//ExObj_nouse			= 0x05,
		//MissAnimation_nouse	= 0x06,
		BGALayer2			= 0x07,
		BPMEx				= 0x08,
		//BMS_reserved_09		= 0x09,
		//BMS_reserved_0A		= 0x0A,
		//BMS_reserved_0B		= 0x0B,
		//BMS_reserved_0C		= 0x0C,
		//BMS_reserved_0D		= 0x0D,
		//BMS_reserved_0E		= 0x0E,
		//BMS_reserved_0F		= 0x0F,
		//BMS_reserved_10		= 0x10,
		HiHatClose			= 0x11,
		Snare				= 0x12,
		BassDrum			= 0x13,
		HighTom				= 0x14,
		LowTom				= 0x15,
		Cymbal				= 0x16,
		FloorTom			= 0x17,
		HiHatOpen			= 0x18,
		RideCymbal			= 0x19,
		LeftCymbal			= 0x1A,
		LeftPedal			= 0x1B,
		LeftBassDrum		= 0x1C,
		//nouse_1d			= 0x1D,
		//nouse_1e			= 0x1E,
		//nouse_1f			= 0x1F,
		Guitar_Open			= 0x20,
		Guitar_xxB			= 0x21,
		Guitar_xGx			= 0x22,
		Guitar_xGB			= 0x23,
		Guitar_Rxx			= 0x24,
		Guitar_RxB			= 0x25,
		Guitar_RGx			= 0x26,
		Guitar_RGB			= 0x27,
		Guitar_Wailing		= 0x28,
		//flowspeed_gt_nouse	= 0x29,
		//nouse_2a			= 0x2A,
		//nouse_2b			= 0x2B,
		//nouse_2c			= 0x2C,
		//nouse_2d			= 0x2D,
		//nouse_2e			= 0x2E,
		Guitar_WailingSound	= 0x2F,
		//flowspeed_dr_nouse	= 0x30,
		HiHatClose_Hidden	= 0x31,
		Snare_Hidden		= 0x32,
		BassDrum_Hidden		= 0x33,
		HighTom_Hidden		= 0x34,
		LowTom_Hidden		= 0x35,
		Cymbal_Hidden		= 0x36,
		FloorTom_Hidden		= 0x37,
		HiHatOpen_Hidden	= 0x38,
		RideCymbal_Hidden	= 0x39,
		LeftCymbal_Hidden	= 0x3A,
		LeftPedal_Hidden	= 0x3B,
		LeftBassDrum_Hidden	= 0x3C,
		//nouse_3d			= 0x3D,
		//nouse_3e			= 0x3E,
		//nouse_3f			= 0x3F,
		//BMS_reserved_40		= 0x40,
		//HiddenObject_2P_41	= 0x41,
		//HiddenObject_2P_42	= 0x42,
		//HiddenObject_2P_43	= 0x43,
		//HiddenObject_2P_44	= 0x44,
		//HiddenObject_2P_45	= 0x45,
		//HiddenObject_2P_46	= 0x46,
		//BMS_reserved_47		= 0x47,
		//BMS_reserved_48		= 0x48,
		//BMS_reserved_49		= 0x49,
		//nouse_4a			= 0x4A,
		//nouse_4b			= 0x4B,
		Bonus1			    = 0x4C,
		Bonus2		    	= 0x4D,
		Bonus3		    	= 0x4E,
		Bonus4	    		= 0x4F,
		BarLine				= 0x50,
		BeatLine			= 0x51,
		MIDIChorus			= 0x52,
		FillIn				= 0x53,
		Movie				= 0x54,
		BGALayer3			= 0x55,
		BGALayer4			= 0x56,
		BGALayer5			= 0x57,
		BGALayer6			= 0x58,
		BGALayer7			= 0x59,
		MovieFull			= 0x5A,
		//nouse_5b			= 0x5B,
		//nouse_5c			= 0x5C,
		//nouse_5d			= 0x5D,
		//nouse_5e			= 0x5E,
		//nouse_5f			= 0x5F,
		BGALayer8			= 0x60,
		SE01				= 0x61,
		SE02				= 0x62,
		SE03				= 0x63,
		SE04				= 0x64,
		SE05				= 0x65,
		SE06				= 0x66,
		SE07				= 0x67,
		SE08				= 0x68,
		SE09				= 0x69,
		//nouse_6a			= 0x6A,
		//nouse_6b			= 0x6B,
		//nouse_6c			= 0x6C,
		//nouse_6d			= 0x6D,
		//nouse_6e			= 0x6E,
		//nouse_6f			= 0x6F,
		SE10				= 0x70,
		SE11				= 0x71,
		SE12				= 0x72,
		SE13				= 0x73,
		SE14				= 0x74,
		SE15				= 0x75,
		SE16				= 0x76,
		SE17				= 0x77,
		SE18				= 0x78,
		SE19				= 0x79,
		//nouse_7a			= 0x7A,
		//nouse_7b			= 0x7B,
		//nouse_7c			= 0x7C,
		//nouse_7d			= 0x7D,
		//nouse_7e			= 0x7E,
		//nouse_7f			= 0x7F,
		SE20				= 0x80,
		SE21				= 0x81,
		SE22				= 0x82,
		SE23				= 0x83,
		SE24				= 0x84,
		SE25				= 0x85,
		SE26				= 0x86,
		SE27				= 0x87,
		SE28				= 0x88,
		SE29				= 0x89,
		//nouse_8a			= 0x8A,
		//nouse_8b			= 0x8B,
		//nouse_8c			= 0x8C,
		//nouse_8d			= 0x8D,
		//nouse_8e			= 0x8E,
		//nouse_8f			= 0x8F,
		SE30				= 0x90,
		SE31				= 0x91,
		SE32				= 0x92,
		//nouse_90			= 0x90,
		//nouse_91			= 0x91,
		//nouse_92			= 0x92,
		Guitar_xxxYx		= 0x93,
		Guitar_xxBYx		= 0x94,
		Guitar_xGxYx		= 0x95,
		Guitar_xGBYx		= 0x96,
		Guitar_RxxYx		= 0x97,
		Guitar_RxBYx		= 0x98,
		Guitar_RGxYx		= 0x99,
		Guitar_RGBYx		= 0x9A,
		Guitar_xxxxP		= 0x9B,
		Guitar_xxBxP		= 0x9C,
		Guitar_xGxxP		= 0x9D,
		Guitar_xGBxP		= 0x9E,
		Guitar_RxxxP		= 0x9F,
		Bass_Open			= 0xA0,
		Bass_xxB			= 0xA1,
		Bass_xGx			= 0xA2,
		Bass_xGB			= 0xA3,
		Bass_Rxx			= 0xA4,
		Bass_RxB			= 0xA5,
		Bass_RGx			= 0xA6,
		Bass_RGB			= 0xA7,
		Bass_Wailing		= 0xA8,
		Guitar_RxBxP		= 0xA9,
		Guitar_RGxxP		= 0xAA,
		Guitar_RGBxP		= 0xAB,
		Guitar_xxxYP		= 0xAC,
		Guitar_xxBYP		= 0xAD,
		Guitar_xGxYP		= 0xAE,
		Guitar_xGBYP        = 0xAF, //Bass_WailingSound	= 0xAF,
		//nouse_b0			= 0xB0,
		HiHatClose_NoChip	= 0xB1,
		Snare_NoChip		= 0xB2,
		BassDrum_NoChip		= 0xB3,
		HighTom_NoChip		= 0xB4,
		LowTom_NoChip		= 0xB5,
		Cymbal_NoChip		= 0xB6,
		FloorTom_NoChip		= 0xB7,
		HiHatOpen_NoChip	= 0xB8,
		RideCymbal_NoChip	= 0xB9,
		Guitar_NoChip		= 0xBA,
		Bass_NoChip			= 0xBB,
		LeftCymbal_NoChip	= 0xBC,
		LeftPedal_NoChip	= 0xBD,
		LeftBassDrum_NoChip	= 0xBE,
		//nouse_bf			= 0xBF,
		//nouse_c0			= 0xC0,
		BeatLineShift		= 0xC1,
		BeatLineDisplay		= 0xC2,
		//nouse_c3			= 0xC3,
		BGALayer1_Swap		= 0xC4,
		//nouse_c5			= 0xC5,
		//nouse_c6			= 0xC6,
		BGALayer2_Swap		= 0xC7,
		//nouse_c8			= 0xC8,
		//nouse_c9			= 0xC9,
		//nouse_ca			= 0xCA,
		//nouse_cb			= 0xCB,
		//nouse_cc			= 0xCC,
		//nouse_cd			= 0xCD,
		//nouse_ce			= 0xCE,
		//nouse_cf			= 0xCF,
		//nouse_d0			= 0xD0,
		//nouse_d1			= 0xD1,
		//nouse_d2			= 0xD2,
		//nouse_d3			= 0xD3,
		//nouse_d4			= 0xD4,
		BGALayer3_Swap		= 0xD5,
		BGALayer4_Swap		= 0xD6,
		BGALayer5_Swap		= 0xD7,
		BGALayer6_Swap		= 0xD8,
		BGALayer7_Swap		= 0xD9,
		MixerAdd			= 0xDA,
		MixerRemove			= 0xDB,
		//nouse_dc			= 0xDC,
		//nouse_dd			= 0xDD,
		//nouse_de			= 0xDE,
		//nouse_df			= 0xDF,
		BGALayer8_Swap		= 0xE0,
		//nouse_e1			= 0xE1,
		//nouse_e2			= 0xE2,
		//nouse_e3			= 0xE3,
		//nouse_e4			= 0xE4,
		//nouse_e5			= 0xE5,
		//nouse_e6			= 0xE6,
		//nouse_e7			= 0xE7,
		//nouse_e8			= 0xE8,
		//nouse_e9			= 0xE9,
		//nouse_ea			= 0xEA,
		//nouse_eb			= 0xEB,
		//nouse_ec			= 0xEC,
		//nouse_ed			= 0xED,
		//nouse_ee			= 0xEE,
		//nouse_ef			= 0xEF,
		//nouse_f0			= 0xF0,
		//nouse_f1			= 0xF1,
		//nouse_f2			= 0xF2,
		//nouse_f3			= 0xF3,
		//nouse_f4			= 0xF4,
		//nouse_f5			= 0xF5,
		//nouse_f6			= 0xF6,
		//nouse_f7			= 0xF7,
		//nouse_f8			= 0xF8,
		//nouse_f9			= 0xF9,
		//nouse_fa			= 0xFA,
		//nouse_fb			= 0xFB,
		//nouse_fc			= 0xFC,
		//nouse_fd			= 0xFD,
		//nouse_fe			= 0xFE,
		//nouse_ff			= 0xFF,
	}
	#endregion
    public enum ERelease
    {
        XG,
        GITADORA,
        GDReEvolve,
        GDmatixx
    }
    public enum EMovieClipMode
    {
        OFF = 0,
        FullScreen = 1,
        Window = 2,
        Both = 3
    }
	public enum ECYGroup
	{
		打ち分ける,
		共通
	}
	public enum EFTGroup
	{
		打ち分ける,
		共通
	}
	public enum EHHGroup
	{
		全部打ち分ける,
		ハイハットのみ打ち分ける,
		左シンバルのみ打ち分ける,
		全部共通
	}
	public enum EBDGroup		// #27029 2012.1.4 from add
	{
        打ち分ける,
        BDとLPで打ち分ける,
        左右ペダルのみ打ち分ける,
        どっちもBD
	}
    public enum ERDPosition
    {
        RDRC,
        RCRD
    }
    public enum EJust
    {
        OFF,
        JUST,
        GREAT,
    }
    public enum Eタイプ
    {
        A,
        B,
        C,
        D,
        E
    }
	public enum Eダークモード
	{
		OFF,
		HALF,
		FULL
	}
	public enum Eダメージレベル
	{
		少ない	= 0,
		普通	= 1,
		大きい	= 2
	}
	public enum Eパッド			// 演奏用のenum。ここを修正するときは、次に出てくる EKeyConfigPad と EパッドFlag もセットで修正すること。
	{
		HH		= 0,
		R		= 0,
		SD		= 1,
		G		= 1,
		BD		= 2,
		B		= 2,
		HT		= 3,
		Pick	= 3,
		LT		= 4,
		Wail	= 4,
		FT		= 5,
		Cancel	= 5,
		CY		= 6,
		Decide	= 6,
		HHO		= 7,
        Y       = 7,
		RD		= 8,
		LC		= 9,
        P       = 9,
		//HP		= 10,	// #27029 2012.1.4 from
        LP      = 10,
        WailW   = 10,
        LBD     = 11,
        WailD   = 11,
		MAX,			// 門番用として定義
		UNKNOWN = 99
	}
	public enum EKeyConfigPad		// #24609 キーコンフィグで使うenum。capture要素あり。
	{
		HH		= Eパッド.HH,
		R		= Eパッド.R,
		SD		= Eパッド.SD,
		G		= Eパッド.G,
		BD		= Eパッド.BD,
		B		= Eパッド.B,
		HT		= Eパッド.HT,
		Pick	= Eパッド.Pick,
		LT		= Eパッド.LT,
		Wail	= Eパッド.Wail,
		FT		= Eパッド.FT,
		Cancel	= Eパッド.Cancel,
		CY		= Eパッド.CY,
		Decide	= Eパッド.Decide,
		HHO		= Eパッド.HHO,
        Y       = Eパッド.Y,
		RD		= Eパッド.RD,
        P       = Eパッド.P,
		LC		= Eパッド.LC,
        WailW   = Eパッド.WailW,
		//HP		= Eパッド.HP,		// #27029 2012.1.4 from
        LP      = Eパッド.LP,
        WailD   = Eパッド.WailD,
        LBD     = Eパッド.LBD,
		Capture,
		UNKNOWN = Eパッド.UNKNOWN
	}
	[Flags]
	public enum EパッドFlag		// #24063 2011.1.16 yyagi コマンド入力用 パッド入力のフラグ化
	{
		None	= 0,
		HH		= 1,
		R		= 1,
		SD		= 2,
		G		= 2,
		B		= 4,
		BD		= 4,
		HT		= 8,
		Pick	= 8,
		LT		= 16,
		Wail	= 16,
		FT		= 32,
		Help	= 32,
		CY		= 64,
		Decide	= 128,
		HHO		= 128,
		RD		= 256,
        Y       = 256,
		LC		= 512,
        P       = 512,
        LP      = 1024,
        WailW   = 1024,
        LBD     = 2048,
        WailD   = 2048,
		UNKNOWN = 4096
	}
	public enum Eランダムモード
	{
        OFF,
        MIRROR,
        RANDOM,
        SUPERRANDOM,
        HYPERRANDOM,
        MASTERRANDOM,
        ANOTHERRANDOM
	}
	public enum ESoundChipType
	{
		Drums,
		Guitar,
		Bass,
		SE,
		BGM,
		UNKNOWN = 99
	}

	public enum E楽器パート		// ここを修正するときは、セットで次の EKeyConfigPart も修正すること。
	{
		DRUMS	= 0,
		GUITAR	= 1,
		BASS	= 2,
		UNKNOWN	= 99
	}
	public enum EKeyConfigPart	// : E楽器パート
	{
		DRUMS	= E楽器パート.DRUMS,
		GUITAR	= E楽器パート.GUITAR,
		BASS	= E楽器パート.BASS,
		SYSTEM,
		UNKNOWN	= E楽器パート.UNKNOWN
	}

	public enum E打ち分け時の再生の優先順位
	{
		ChipがPadより優先,
		PadがChipより優先
	}
	internal enum E入力デバイス
	{
		キーボード		= 0,
		MIDI入力		= 1,
		ジョイパッド	= 2,
		マウス			= 3,
		不明			= -1
	}
	public enum E判定
	{
		Perfect	= 0,
		Great	= 1,
		Good	= 2,
		Poor	= 3,
		Miss	= 4,
		Bad		= 5,
        XPerfect = 6,
		Auto
	}
	internal enum E判定文字表示位置
	{
		表示OFF,
		レーン上,
		判定ライン上,
		コンボ下
	}
	internal enum E判定位置
	{
		標準	= 0,
		Lower,
		MAX
	}
	internal enum E判定表示優先度
	{
		Chipより下,
		Chipより上
	}
	internal enum Eドラムレーン表示位置
	{
		Left = 0,
		Center = 1
	}
	internal enum EAVI種別
	{
		Unknown,
		AVI,
		AVIPAN
	}
	internal enum EBGA種別
	{
		Unknown,
		BMP,
		BMPTEX,
		BGA,
		BGAPAN
	}
	internal enum EFIFOモード
	{
		フェードイン,
		フェードアウト
	}
	internal enum Eドラムコンボ文字の表示位置
	{
		LEFT,
		CENTER,
		RIGHT,
		OFF
	}
	internal enum Eレーン
	{
        LC = 0,
        HH,
        SD,
        BD,
        HT,
        LT,
        FT,
        CY,
        LP,
        RD,		// 将来の独立レーン化/独立AUTO設定を見越して追加
        LBD = 10,
        Guitar,	// AUTOレーン判定を容易にするため、便宜上定義しておく(未使用)
        Bass,	// (未使用)
        GtR,
        GtG,
        GtB,
        GtY,
        GtP,
        GtPick,
        GtW,
        BsR,
        BsG,
        BsB,
        BsY,
        BsP,
        BsPick,
        BsW,
        MAX,	// 要素数取得のための定義 ("BGM"は使わない前提で)
        BGM
	}
	internal enum Eレーン数
	{
		物理 = 8,	// LC, HH,     SD, BD, HT, LT, FT, CY
		論理 = 10	// LC, HO, HC, SD, BD, HT, LT, FT, RC, RD
	}
	internal enum Eログ出力
	{
		OFF,
		ON通常,
		ON詳細あり
	}
	internal enum E演奏画面の戻り値
	{
		継続,
		演奏中断,
		ステージ失敗,
		ステージクリア,
		再読込_再演奏,
		再演奏
	}
	internal enum E曲読込画面の戻り値
	{
		継続 = 0,
		読込完了,
		読込中止
	}
	/// <summary>
	/// 入力ラグ表示タイプ
	/// </summary>
	internal enum EShowLagType
	{
		OFF,			// 全く表示しない
		ON,				// 判定に依らず全て表示する
		GREAT_POOR		// GREAT-MISSの時のみ表示する(PERFECT時は表示しない)
	}

	/// <summary>
	/// 透明チップの種類
	/// </summary>
	public enum EInvisible
	{
		OFF,		// チップを透明化しない
		SEMI,		// Poor/Miss時だけ、一時的に透明解除する
		FULL		// チップを常に透明化する
	}

    /// <summary>
    /// 使用するAUTOゴーストデータの種類 (#35411 chnmr0)
    /// </summary>
    public enum EAutoGhostData
    {
        PERFECT = 0, // 従来のAUTO
        LAST_PLAY = 1, // (.score.ini) の LastPlay ゴースト
        HI_SKILL = 2, // (.score.ini) の HiSkill ゴースト
        HI_SCORE = 3, // (.score.ini) の HiScore ゴースト
        ONLINE = 4 // オンラインゴースト (DTXMOS からプラグインが取得、本体のみでは指定しても無効)
    }

    /// <summary>
    /// 使用するターゲットゴーストデータの種類 (#35411 chnmr0)
    /// ここでNONE以外を指定してかつ、ゴーストが利用可能な場合グラフの目標値に描画される
    /// NONE の場合従来の動作
    /// </summary>
    public enum ETargetGhostData
    {
        NONE = 0,
        PERFECT = 1,
        LAST_PLAY = 2, // (.score.ini) の LastPlay ゴースト
        HI_SKILL = 3, // (.score.ini) の HiSkill ゴースト
        HI_SCORE = 4, // (.score.ini) の HiScore ゴースト
        ONLINE = 5 // オンラインゴースト (DTXMOS からプラグインが取得、本体のみでは指定しても無効)
    }

    public enum ESkillType
    {
        DTXMania,
        XG
    }

	/// <summary>
	/// Drum/Guitar/Bass の値を扱う汎用の構造体。
	/// </summary>
	/// <typeparam name="T">値の型。</typeparam>
	[Serializable]
	[StructLayout( LayoutKind.Sequential )]
	public struct STDGBVALUE<T>			// indexはE楽器パートと一致させること
	{
		public T Drums;
		public T Guitar;
		public T Bass;
		public T Unknown;
		public T this[ int index ]
		{
			get
			{
				switch( index )
				{
					case (int) E楽器パート.DRUMS:
						return this.Drums;

					case (int) E楽器パート.GUITAR:
						return this.Guitar;

					case (int) E楽器パート.BASS:
						return this.Bass;

					case (int) E楽器パート.UNKNOWN:
						return this.Unknown;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				switch( index )
				{
					case (int) E楽器パート.DRUMS:
						this.Drums = value;
						return;

					case (int) E楽器パート.GUITAR:
						this.Guitar = value;
						return;

					case (int) E楽器パート.BASS:
						this.Bass = value;
						return;

					case (int) E楽器パート.UNKNOWN:
						this.Unknown = value;
						return;
				}
				throw new IndexOutOfRangeException();
			}
		}
	}

	/// <summary>
	/// レーンの値を扱う汎用の構造体。列挙型"Eドラムレーン"に準拠。
	/// </summary>
	/// <typeparam name="T">値の型。</typeparam>
	[StructLayout( LayoutKind.Sequential )]
	public struct STLANEVALUE<T>
	{
        public T LC;
        public T HH;
        public T SD;
        public T BD;
        public T HT;
        public T LT;
        public T FT;
        public T CY;
        public T LP;
        public T RD;
        public T LBD;
        public T Guitar;
        public T Bass;
        public T GtR;
        public T GtG;
        public T GtB;
        public T GtY;
        public T GtP;
        public T GtPick;
        public T GtW;
        public T BsR;
        public T BsG;
        public T BsB;
        public T BsY;
        public T BsP;
        public T BsPick;
        public T BsW;
        public T BGM;

        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case (int)Eレーン.LC:
                        return this.LC;
                    case (int)Eレーン.HH:
                        return this.HH;
                    case (int)Eレーン.SD:
                        return this.SD;
                    case (int)Eレーン.BD:
                        return this.BD;
                    case (int)Eレーン.HT:
                        return this.HT;
                    case (int)Eレーン.LT:
                        return this.LT;
                    case (int)Eレーン.FT:
                        return this.FT;
                    case (int)Eレーン.CY:
                        return this.CY;
                    case (int)Eレーン.LP:
                        return this.LP;
                    case (int)Eレーン.RD:
                        return this.RD;
                    case (int)Eレーン.LBD:
                        return this.LBD;
                    case (int)Eレーン.Guitar:
                        return this.Guitar;
                    case (int)Eレーン.Bass:
                        return this.Bass;
                    case (int)Eレーン.GtR:
                        return this.GtR;
                    case (int)Eレーン.GtG:
                        return this.GtG;
                    case (int)Eレーン.GtB:
                        return this.GtB;
                    case (int)Eレーン.GtY:
                        return this.GtY;
                    case (int)Eレーン.GtP:
                        return this.GtP;
                    case (int)Eレーン.GtPick:
                        return this.GtPick;
                    case (int)Eレーン.GtW:
                        return this.GtW;
                    case (int)Eレーン.BsR:
                        return this.BsR;
                    case (int)Eレーン.BsG:
                        return this.BsG;
                    case (int)Eレーン.BsB:
                        return this.BsB;
                    case (int)Eレーン.BsY:
                        return this.BsY;
                    case (int)Eレーン.BsP:
                        return this.BsP;
                    case (int)Eレーン.BsPick:
                        return this.BsPick;
                    case (int)Eレーン.BsW:
                        return this.BsW;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (index)
                {
                    case (int)Eレーン.LC:
                        this.LC = value;
                        return;
                    case (int)Eレーン.HH:
                        this.HH = value;
                        return;
                    case (int)Eレーン.SD:
                        this.SD = value;
                        return;
                    case (int)Eレーン.BD:
                        this.BD = value;
                        return;
                    case (int)Eレーン.HT:
                        this.HT = value;
                        return;
                    case (int)Eレーン.LT:
                        this.LT = value;
                        return;
                    case (int)Eレーン.FT:
                        this.FT = value;
                        return;
                    case (int)Eレーン.CY:
                        this.CY = value;
                        return;
                    case (int)Eレーン.LP:
                        this.LP = value;
                        return;
                    case (int)Eレーン.RD:
                        this.RD = value;
                        return;
                    case (int)Eレーン.LBD:
                        this.LBD = value;
                        return;
                    case (int)Eレーン.Guitar:
                        this.Guitar = value;
                        return;
                    case (int)Eレーン.Bass:
                        this.Bass = value;
                        return;
                    case (int)Eレーン.GtR:
                        this.GtR = value;
                        return;
                    case (int)Eレーン.GtG:
                        this.GtG = value;
                        return;
                    case (int)Eレーン.GtB:
                        this.GtB = value;
                        return;
                    case (int)Eレーン.GtY:
                        this.GtY = value;
                        return;
                    case (int)Eレーン.GtP:
                        this.GtP = value;
                        return;
                    case (int)Eレーン.GtPick:
                        this.GtPick = value;
                        return;
                    case (int)Eレーン.GtW:
                        this.GtW = value;
                        return;
                    case (int)Eレーン.BsR:
                        this.BsR = value;
                        return;
                    case (int)Eレーン.BsG:
                        this.BsG = value;
                        return;
                    case (int)Eレーン.BsB:
                        this.BsB = value;
                        return;
                    case (int)Eレーン.BsY:
                        this.BsY = value;
                        return;
                    case (int)Eレーン.BsP:
                        this.BsP = value;
                        return;
                    case (int)Eレーン.BsPick:
                        this.BsPick = value;
                        return;
                    case (int)Eレーン.BsW:
                        this.BsW = value;
                        return;
                }
                throw new IndexOutOfRangeException();
			}
		}
	}


	[StructLayout( LayoutKind.Sequential )]
	public struct STAUTOPLAY								// Eレーンとindexを一致させること
	{
		public bool LC;			// 0
		public bool HH;			// 1
		public bool SD;			// 2
		public bool BD;			// 3
		public bool HT;			// 4
		public bool LT;			// 5
		public bool FT;			// 6
		public bool CY;			// 7
        public bool LP;
		public bool RD;			// 8
        public bool LBD;
		public bool Guitar;		// 9	(not used)
		public bool Bass;		// 10	(not used)
		public bool GtR;		// 11
		public bool GtG;		// 12
		public bool GtB;		// 13
        public bool GtY;
        public bool GtP;
		public bool GtPick;		// 14
		public bool GtW;		// 15
		public bool BsR;		// 16
		public bool BsG;		// 17
		public bool BsB;		// 18
        public bool BsY;
        public bool BsP;
		public bool BsPick;		// 19
		public bool BsW;		// 20
		public bool this[ int index ]
		{
			get
			{
				switch ( index )
				{
					case (int) Eレーン.LC:
						return this.LC;
					case (int) Eレーン.HH:
						return this.HH;
					case (int) Eレーン.SD:
						return this.SD;
					case (int) Eレーン.BD:
						return this.BD;
					case (int) Eレーン.HT:
						return this.HT;
					case (int) Eレーン.LT:
						return this.LT;
					case (int) Eレーン.FT:
						return this.FT;
					case (int) Eレーン.CY:
						return this.CY;
                    case (int) Eレーン.LP:
                        return this.LP;
					case (int) Eレーン.RD:
						return this.RD;
                    case (int) Eレーン.LBD:
                        return this.LBD;
                    case (int)Eレーン.Guitar:
                        if (!this.GtR) return false;
                        if (!this.GtG) return false;
                        if (!this.GtB) return false;
                        if (!this.GtY) return false;
                        if (!this.GtP) return false;
                        if (!this.GtPick) return false;
                        if (!this.GtW) return false;
                        return true;
                    case (int)Eレーン.Bass:
                        if (!this.BsR) return false;
                        if (!this.BsG) return false;
                        if (!this.BsB) return false;
                        if (!this.BsY) return false;
                        if (!this.BsP) return false;
                        if (!this.BsPick) return false;
                        if (!this.BsW) return false;
                        return true;
					case (int) Eレーン.GtR:
						return this.GtR;
					case (int) Eレーン.GtG:
						return this.GtG;
					case (int) Eレーン.GtB:
						return this.GtB;
                    case (int) Eレーン.GtY:
                        return this.GtY;
                    case (int) Eレーン.GtP:
                        return this.GtP;
					case (int) Eレーン.GtPick:
						return this.GtPick;
					case (int) Eレーン.GtW:
						return this.GtW;
					case (int) Eレーン.BsR:
						return this.BsR;
					case (int) Eレーン.BsG:
						return this.BsG;
					case (int) Eレーン.BsB:
						return this.BsB;
                    case (int) Eレーン.BsY:
                        return this.BsY;
                    case (int) Eレーン.BsP:
                        return this.BsP;
					case (int) Eレーン.BsPick:
						return this.BsPick;
					case (int) Eレーン.BsW:
						return this.BsW;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				switch ( index )
				{
					case (int) Eレーン.LC:
						this.LC = value;
						return;
					case (int) Eレーン.HH:
						this.HH = value;
						return;
					case (int) Eレーン.SD:
						this.SD = value;
						return;
					case (int) Eレーン.BD:
						this.BD = value;
						return;
					case (int) Eレーン.HT:
						this.HT = value;
						return;
					case (int) Eレーン.LT:
						this.LT = value;
						return;
					case (int) Eレーン.FT:
						this.FT = value;
						return;
					case (int) Eレーン.CY:
						this.CY = value;
						return;
                    case (int) Eレーン.LP:
                        this.LP = value;
                        return;
					case (int) Eレーン.RD:
						this.RD = value;
						return;
                    case (int) Eレーン.LBD:
                        this.LBD = value;
                        return;
                    case (int)Eレーン.Guitar:
                        this.GtR = this.GtG = this.GtB = this.GtY = this.GtP = this.GtPick = this.GtW = value;
                        //this.GtR = this.GtG = this.GtB = this.GtPick = this.GtW = value;
                        return;
                    case (int)Eレーン.Bass:
                        this.BsR = this.BsG = this.BsB = this.BsY = this.BsP = this.BsPick = this.BsW = value;
                        //this.BsR = this.BsG = this.BsB = this.BsPick = this.BsW = value;
                        return;
					case (int) Eレーン.GtR:
						this.GtR = value;
						return;
					case (int) Eレーン.GtG:
						this.GtG = value;
						return;
					case (int) Eレーン.GtB:
						this.GtB = value;
						return;
                    case (int) Eレーン.GtY:
                        this.GtY = value;
                        return;
                    case (int) Eレーン.GtP:
                        this.GtP = value;
                        return;
					case (int) Eレーン.GtPick:
						this.GtPick = value;
						return;
					case (int) Eレーン.GtW:
						this.GtW = value;
						return;
					case (int) Eレーン.BsR:
						this.BsR = value;
						return;
					case (int) Eレーン.BsG:
						this.BsG = value;
						return;
					case (int) Eレーン.BsB:
						this.BsB = value;
						return;
                    case (int) Eレーン.BsY:
                        this.BsY = value;
                        return;
                    case (int) Eレーン.BsP:
                        this.BsP = value;
                        return;
					case (int) Eレーン.BsPick:
						this.BsPick = value;
						return;
					case (int) Eレーン.BsW:
						this.BsW = value;
						return;
				}
				throw new IndexOutOfRangeException();
			}
		}
    }

    internal class C定数
	{
		public const int BGA_H = 0x163;
		public const int BGA_W = 0x116;
		public const int HIDDEN_POS = 100;
		public const int MAX_AVI_LAYER = 1;
		public const int MAX_WAILING = 4;
		public const int PANEL_H = 0x1a;
		public const int PANEL_W = 0x116;
		public const int PREVIEW_H = 0x10d;
		public const int PREVIEW_W = 0xcc;
		public const int SCORE_H = 0x18;
		public const int SCORE_W = 12;
		public const int SUDDEN_POS = 200;

        public static int[] nチップ幅テーブルDB = new int[] { 6, 6, 6, 6, 6, 7, 7, 9, 10, 12, 16, 19, 24 }; // 2019.9.10 kairera0467

        public class Drums
		{
			public const int BAR_Y = 0x1a6;
			public const int BAR_Y_REV = 0x38;
			public const int BASS_BAR_Y = 0x5f;
			public const int BASS_BAR_Y_REV = 0x176;
			public const int BASS_H = 0x163;
			public const int BASS_W = 0x6d;
			public const int BASS_X = 0x18e;
			public const int BASS_Y = 0x39;
			public const int BGA_X = 0x152;
			public const int BGA_Y = 0x39;
			public const int GAUGE_H = 0x160;
			public const int GAUGE_W = 0x10;
			public const int GAUGE_X = 6;
			public const int GAUGE_Y = 0x35;
			public const int GUITAR_BAR_Y = 0x5f;
			public const int GUITAR_BAR_Y_REV = 0x176;
			public const int GUITAR_H = 0x163;
			public const int GUITAR_W = 0x6d;
			public const int GUITAR_X = 0x1fb;
			public const int GUITAR_Y = 0x39;
			public const int PANEL_X = 0x150;
			public const int PANEL_Y = 0x1ab;
			public const int SCORE_X = 0x164;
			public const int SCORE_Y = 14;
		}
		public class Guitar
		{
			public const int BAR_Y = 40;
			public const int BAR_Y_REV = 0x171;
			public const int BASS_H = 0x199;
			public const int BASS_W = 140;
			public const int BASS_X = 480;
			public const int BASS_Y = 0;
			public const int BGA_X = 0xb5;
			public const int BGA_Y = 50;
			public const int GAUGE_H = 0x10;
			public const int GAUGE_W = 0x80;
			public const int GAUGE_X_BASS = 0x14f;
			public const int GAUGE_X_GUITAR = 0xb2;
			public const int GAUGE_Y_BASS = 8;
			public const int GAUGE_Y_GUITAR = 8;
			public const int GUITAR_H = 0x199;
			public const int GUITAR_W = 140;
			public const int GUITAR_X = 0x1a;
			public const int GUITAR_Y = 0;
			public const int PANEL_X = 0xb5;
			public const int PANEL_Y = 430;
		}
	}
}
