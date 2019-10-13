using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Web;
using FDK;

using SlimDXKey = SlimDX.DirectInput.Key;

namespace DTXMania
{
	internal class CConfigIni
	{
		// クラス

		#region [ CKeyAssign ]
		public class CKeyAssign
		{
			public class CKeyAssignPad
			{
				public CConfigIni.CKeyAssign.STKEYASSIGN[] HH
				{
					get
					{
						return this.padHH_R;
					}
					set
					{
						this.padHH_R = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] R
				{
					get
					{
						return this.padHH_R;
					}
					set
					{
						this.padHH_R = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] SD
				{
					get
					{
						return this.padSD_G;
					}
					set
					{
						this.padSD_G = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] G
				{
					get
					{
						return this.padSD_G;
					}
					set
					{
						this.padSD_G = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] BD
				{
					get
					{
						return this.padBD_B;
					}
					set
					{
						this.padBD_B = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] B
				{
					get
					{
						return this.padBD_B;
					}
					set
					{
						this.padBD_B = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] HT
				{
					get
					{
						return this.padHT_Pick;
					}
					set
					{
						this.padHT_Pick = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] Pick
				{
					get
					{
						return this.padHT_Pick;
					}
					set
					{
						this.padHT_Pick = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] LT
				{
					get
					{
						return this.padLT_Wail;
					}
					set
					{
						this.padLT_Wail = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] Wail
				{
					get
					{
						return this.padLT_Wail;
					}
					set
					{
						this.padLT_Wail = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] FT
				{
					get
					{
						return this.padFT_Cancel;
					}
					set
					{
						this.padFT_Cancel = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] Cancel
				{
					get
					{
						return this.padFT_Cancel;
					}
					set
					{
						this.padFT_Cancel = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] CY
				{
					get
					{
						return this.padCY_Decide;
					}
					set
					{
						this.padCY_Decide = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] Decide
				{
					get
					{
						return this.padCY_Decide;
					}
					set
					{
						this.padCY_Decide = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] HHO
				{
					get
					{
						return this.padHHO;
					}
					set
					{
						this.padHHO = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] RD
				{
					get
					{
						return this.padRD;
					}
					set
					{
						this.padRD = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] LC
				{
					get
					{
						return this.padLC;
					}
					set
					{
						this.padLC = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] LP
				{
					get
					{
						return this.padLP;
					}
					set
					{
						this.padLP = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] LBD
				{
					get
					{
						return this.padLBD;
					}
					set
					{
						this.padLBD = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] Capture
				{
					get
					{
						return this.padCapture;
					}
					set
					{
						this.padCapture = value;
					}
				}
				public CConfigIni.CKeyAssign.STKEYASSIGN[] this[ int index ]
				{
					get
					{
						switch ( index )
						{
							case (int) EKeyConfigPad.HH:
								return this.padHH_R;

							case (int) EKeyConfigPad.SD:
								return this.padSD_G;

							case (int) EKeyConfigPad.BD:
								return this.padBD_B;

							case (int) EKeyConfigPad.HT:
								return this.padHT_Pick;

							case (int) EKeyConfigPad.LT:
								return this.padLT_Wail;

							case (int) EKeyConfigPad.FT:
								return this.padFT_Cancel;

							case (int) EKeyConfigPad.CY:
								return this.padCY_Decide;

							case (int) EKeyConfigPad.HHO:
								return this.padHHO;

							case (int) EKeyConfigPad.RD:
								return this.padRD;

							case (int) EKeyConfigPad.LC:
								return this.padLC;

							case (int) EKeyConfigPad.LP:	// #27029 2012.1.4 from
								return this.padLP;			//

							case (int) EKeyConfigPad.LBD:
								return this.padLBD;

							case (int) EKeyConfigPad.Capture:
								return this.padCapture;
						}
						throw new IndexOutOfRangeException();
					}
					set
					{
						switch ( index )
						{
							case (int) EKeyConfigPad.HH:
								this.padHH_R = value;
								return;

							case (int) EKeyConfigPad.SD:
								this.padSD_G = value;
								return;

							case (int) EKeyConfigPad.BD:
								this.padBD_B = value;
								return;

							case (int) EKeyConfigPad.Pick:
								this.padHT_Pick = value;
								return;

							case (int) EKeyConfigPad.LT:
								this.padLT_Wail = value;
								return;

							case (int) EKeyConfigPad.FT:
								this.padFT_Cancel = value;
								return;

							case (int) EKeyConfigPad.CY:
								this.padCY_Decide = value;
								return;

							case (int) EKeyConfigPad.HHO:
								this.padHHO = value;
								return;

							case (int) EKeyConfigPad.RD:
								this.padRD = value;
								return;

							case (int) EKeyConfigPad.LC:
								this.padLC = value;
								return;

							case (int) EKeyConfigPad.LP:
								this.padLP = value;
								return;

							case (int) EKeyConfigPad.LBD:
								this.padLBD = value;
								return;

							case (int) EKeyConfigPad.Capture:
								this.padCapture = value;
								return;
						}
						throw new IndexOutOfRangeException();
					}
				}

				#region [ private ]
				//-----------------
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padBD_B;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padCY_Decide;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padFT_Cancel;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padHH_R;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padHHO;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padHT_Pick;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padLC;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padLT_Wail;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padRD;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padSD_G;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padCapture;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padLP;
				private CConfigIni.CKeyAssign.STKEYASSIGN[] padLBD;
				//-----------------
				#endregion
			}

			[StructLayout( LayoutKind.Sequential )]
			public struct STKEYASSIGN
			{
				public E入力デバイス 入力デバイス;
				public int ID;
				public int コード;
				public STKEYASSIGN( E入力デバイス DeviceType, int nID, int nCode )
				{
					this.入力デバイス = DeviceType;
					this.ID = nID;
					this.コード = nCode;
				}
			}

			public CKeyAssignPad Bass = new CKeyAssignPad();
			public CKeyAssignPad Drums = new CKeyAssignPad();
			public CKeyAssignPad Guitar = new CKeyAssignPad();
			public CKeyAssignPad System = new CKeyAssignPad();
			public CKeyAssignPad this[ int index ]
			{
				get
				{
					switch( index )
					{
						case (int) EKeyConfigPart.DRUMS:
							return this.Drums;

						case (int) EKeyConfigPart.GUITAR:
							return this.Guitar;

						case (int) EKeyConfigPart.BASS:
							return this.Bass;

						case (int) EKeyConfigPart.SYSTEM:
							return this.System;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case (int) EKeyConfigPart.DRUMS:
							this.Drums = value;
							return;

						case (int) EKeyConfigPart.GUITAR:
							this.Guitar = value;
							return;

						case (int) EKeyConfigPart.BASS:
							this.Bass = value;
							return;

						case (int) EKeyConfigPart.SYSTEM:
							this.System = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}
		#endregion

		//
		public enum ESoundDeviceTypeForConfig
		{
			ACM = 0,
			// DirectSound,
			ASIO,
			WASAPI,
            WASAPI_Share,
			Unknown=99
		}
		// プロパティ

#if false		// #23625 2011.1.11 Config.iniからダメージ/回復値の定数変更を行う場合はここを有効にする 087リリースに合わせ機能無効化
		//----------------------------------------
		public float[,] fGaugeFactor = new float[5,2];
		public float[] fDamageLevelFactor = new float[3];
		//----------------------------------------
#endif
		public int nBGAlpha;
		public bool bForceAVIFullscreen;
		public bool bAVI有効;
		public bool bBGA有効;
		public bool bBGM音を発声する;
		public STDGBVALUE<bool> bHidden;
		public STDGBVALUE<bool> bLeft;
		public STDGBVALUE<bool> bLight;
		public bool bLogDTX詳細ログ出力;
		public bool bLog曲検索ログ出力;
		public bool bLog作成解放ログ出力;
		public STDGBVALUE<bool> bReverse;
		//public STDGBVALUE<E判定表示優先度> e判定表示優先度;
		public E判定表示優先度 e判定表示優先度;
		public STDGBVALUE<E判定位置> e判定位置;			// #33891 2014.6.26 yyagi
		public Eドラムレーン表示位置 eドラムレーン表示位置;
		public bool bScoreIniを出力する;
		public bool bSTAGEFAILED有効;
		public STDGBVALUE<bool> bSudden;
		public bool bTight;
		public STDGBVALUE<bool> bGraph;     // #24074 2011.01.23 add ikanick
		public bool bWave再生位置自動調整機能有効;
		public bool bシンバルフリー;
		public bool bストイックモード;
		public bool bドラム打音を発声する;
		public bool bフィルイン有効;
		public bool bランダムセレクトで子BOXを検索対象とする;
		public bool bログ出力;
		public STDGBVALUE<bool> b演奏音を強調する;
		public bool b演奏情報を表示する;
		public bool b歓声を発声する;
		public bool b垂直帰線待ちを行う;
		public bool b選曲リストフォントを斜体にする;
		public bool b選曲リストフォントを太字にする;
		public bool b全画面モード;
		public int n初期ウィンドウ開始位置X; // #30675 2013.02.04 ikanick add
		public int n初期ウィンドウ開始位置Y;  
		public int nウインドウwidth;				// #23510 2010.10.31 yyagi add
		public int nウインドウheight;				// #23510 2010.10.31 yyagi add
		public Dictionary<int, string> dicJoystick;
		public ECYGroup eCYGroup;
		public Eダークモード eDark;
		public EFTGroup eFTGroup;
		public EHHGroup eHHGroup;
		public EBDGroup eBDGroup;					// #27029 2012.1.4 from add
		public E打ち分け時の再生の優先順位 eHitSoundPriorityCY;
		public E打ち分け時の再生の優先順位 eHitSoundPriorityFT;
		public E打ち分け時の再生の優先順位 eHitSoundPriorityHH;
		public STDGBVALUE<Eランダムモード> eRandom;
		public Eダメージレベル eダメージレベル;
        public CKeyAssign KeyAssign;
		public int n非フォーカス時スリープms;       // #23568 2010.11.04 ikanick add
		public int nフレーム毎スリープms;			// #xxxxx 2011.11.27 yyagi add
		public int n演奏速度;
		public int n曲が選択されてからプレビュー音が鳴るまでのウェイトms;
		public int n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms;
		public int n自動再生音量;
		public int n手動再生音量;
		public int n選曲リストフォントのサイズdot;
		public STDGBVALUE<int> n表示可能な最小コンボ数;
		public STDGBVALUE<int> n譜面スクロール速度;
		public string strDTXManiaのバージョン;
		public string str曲データ検索パス;
		public string str選曲リストフォント;
		public Eドラムコンボ文字の表示位置 ドラムコンボ文字の表示位置;
		public STDGBVALUE<E判定文字表示位置> 判定文字表示位置;
//		public int nハイハット切り捨て下限Velocity;
//		public int n切り捨て下限Velocity;			// #23857 2010.12.12 yyagi VelocityMin
		public STDGBVALUE<int> nInputAdjustTimeMs;	// #23580 2011.1.3 yyagi タイミングアジャスト機能
        public int nCommonBGMAdjustMs;              // #36372 2016.06.19 kairera0467 全曲共通のBGMオフセット
		public STDGBVALUE<int> nJudgeLinePosOffset;	// #31602 2013.6.23 yyagi 判定ライン表示位置のオフセット
		public int	nShowLagType;					// #25370 2011.6.5 yyagi ズレ時間表示機能
		public bool bIsAutoResultCapture;			// #25399 2011.6.9 yyagi リザルト画像自動保存機能のON/OFF制御
		public int nPoliphonicSounds;				// #28228 2012.5.1 yyagi レーン毎の最大同時発音数
		public bool bバッファ入力を行う;
		public bool bIsEnabledSystemMenu;			// #28200 2012.5.1 yyagi System Menuの使用可否切替
		public string strSystemSkinSubfolderFullName;	// #28195 2012.5.2 yyagi Skin切替用 System/以下のサブフォルダ名
		public bool bUseBoxDefSkin;						// #28195 2012.5.6 yyagi Skin切替用 box.defによるスキン変更機能を使用するか否か
        public STDGBVALUE<EAutoGhostData> eAutoGhost;               // #35411 2015.8.18 chnmr0 プレー時使用ゴーストデータ種別
        public STDGBVALUE<ETargetGhostData> eTargetGhost;               // #35411 2015.8.18 chnmr0 ゴーストデータ再生方法
        public bool bWarnMIDI20USB;                 // #37961 2019.1.21 add yyagi USBケーブル「MIDI2.0-USB」を使用しているときの警告表示有無
        public bool bWarnSoundDeviceOnUSB;          // #38358 2019.2.1 add yyagi USB接続のサウンドデバイスを使用しているときの警告表示有無

        #region[ Ver.K追加 ]
        public bool bCLASSIC譜面判別を有効にする;
        public bool b曲名表示をdefのものにする;
        public bool bJudgeCountDisp;
        public bool bSkillModeを自動切替えする;
        public bool bXPerfect判定を有効にする;
        public bool bWindowClipMode;

        public EMovieClipMode eMovieClipMode;
        public ESkillType eSkillMode;
        public ERDPosition eRDPosition;
        public STDGBVALUE<EJust> eJUST;
        public Eタイプ eHHOGraphics;
        public Eタイプ eJudgeAnimeType;
        public Eタイプ eLaneType;
        public Eタイプ eLBDGraphics;
        public Eタイプ eNamePlateType;
        public int nJudgeFrames;
        public int nJudgeInterval;
        public int nJudgeWidth;
        public int nJudgeHeight;
        public int[] nJudgeStringBarX;
        public int[] nJudgeStringBarY;
        public int[] nJudgeStringBarWidth;
        public int[] nJudgeStringBarHeight;
        public int nExplosionFrames;
        public int nExplosionInterval;
        public int nExplosionWidth;
        public int nExplosionHeight;
        public STDGBVALUE<bool> bJudgeLineDisp;
        public STDGBVALUE<bool> bLaneFlush;
        public STDGBVALUE<int> nJudgeLine;
        public STDGBVALUE<int> nLaneDispType;
        public STDGBVALUE<int> nNameColor;
        public STDGBVALUE<int> nShutterInSide;
        public STDGBVALUE<int> nShutterOutSide;
        private STDGBVALUE<string> strCardName;
        public STDGBVALUE<string> strGroupName;
        public string strResultSongNameFont;
        public STDGBVALUE<string> strShutterImageName;      // #36144 kairera0467 シャッター画像のパスではなくcsvに登録した名前を格納する。
        public STDGBVALUE<Eタイプ> eNumOfLanes;
        public STDGBVALUE<Eタイプ> eDkdkType;
        public STDGBVALUE<Eランダムモード> eRandomPedal;
        public STDGBVALUE<bool> bAssignToLBD;
        public int nPedalJudgeRangeDelta;                   // #39397 2019.07.19 kairera0467 ペダルレーンの補正値(通常の判定範囲に加算)
        public bool bフレームを表示する;                  // 2019.09.07 kairera0467 フレーム表示をする(今の所matixxのみ)

        #endregion
        #region[ Ver.K 追加取得処理 ]
        /// <summary>
        /// Config.iniからプレイヤー名を取得する。
        /// Config.iniが空だった場合は「GUEST」が返される
        /// </summary>
        /// <param name="epart">取得する楽器パート</param>
        /// <returns>プレイヤー名</returns>
        public string strGetCardName( E楽器パート epart )
        {
            return String.IsNullOrEmpty( this.strCardName[ (int)epart ] ) ? "GUEST" : this.strCardName[ (int)epart ];
        }
        #endregion

        public bool bConfigIniがないかDTXManiaのバージョンが異なる
		{
			get
			{
				return ( !this.bConfigIniが存在している || !CDTXMania.VERSION.Equals( this.strDTXManiaのバージョン ) );
			}
		}
		public bool bDrums有効
		{
			get
			{
				return this._bDrums有効;
			}
			set
			{
				this._bDrums有効 = value;
				if( !this._bGuitar有効 && !this._bDrums有効 )
				{
					this._bGuitar有効 = true;
				}
			}
		}
		public bool bEnterがキー割り当てのどこにも使用されていない
		{
			get
			{
				for( int i = 0; i <= (int)EKeyConfigPart.SYSTEM; i++ )
				{
					for( int j = 0; j <= (int)EKeyConfigPad.Capture; j++ )
					{
						for( int k = 0; k < 0x10; k++ )
						{
							if( ( this.KeyAssign[ i ][ j ][ k ].入力デバイス == E入力デバイス.キーボード ) && ( this.KeyAssign[ i ][ j ][ k ].コード == (int) SlimDXKey.Return ) )
							{
								return false;
							}
						}
					}
				}
				return true;
			}
		}
		public bool bGuitar有効
		{
			get
			{
				return this._bGuitar有効;
			}
			set
			{
				this._bGuitar有効 = value;
				if( !this._bGuitar有効 && !this._bDrums有効 )
				{
					this._bDrums有効 = true;
				}
			}
		}
		public bool bウィンドウモード
		{
			get
			{
				return !this.b全画面モード;
			}
			set
			{
				this.b全画面モード = !value;
			}
		}
		public bool bギタレボモード
		{
			get
			{
				return ( !this.bDrums有効 && this.bGuitar有効 );
			}
		}
		public bool bドラムが全部オートプレイである
		{
			get
			{
				for ( int i = (int) Eレーン.LC; i <= (int) Eレーン.CY; i++ )
				{
					if( !this.bAutoPlay[ i ] )
					{
						return false;
					}
				}
				return true;
			}
		}
		public bool bギターが全部オートプレイである
		{
			get
			{
				for ( int i = (int) Eレーン.GtR; i <= (int) Eレーン.GtW; i++ )
				{
					if ( !this.bAutoPlay[ i ] )
					{
						return false;
					}
				}
				return true;
			}
		}
		public bool bベースが全部オートプレイである
		{
			get
			{
				for ( int i = (int) Eレーン.BsR; i <= (int) Eレーン.BsW; i++ )
				{
					if ( !this.bAutoPlay[ i ] )
					{
						return false;
					}
				}
				return true;
			}
		}
		public bool b演奏情報を表示しない
		{
			get
			{
				return !this.b演奏情報を表示する;
			}
			set
			{
				this.b演奏情報を表示する = !value;
			}
		}
		public int n背景の透過度
		{
			get
			{
				return this.nBGAlpha;
			}
			set
			{
				if( value < 0 )
				{
					this.nBGAlpha = 0;
				}
				else if( value > 0xff )
				{
					this.nBGAlpha = 0xff;
				}
				else
				{
					this.nBGAlpha = value;
				}
			}
		}
		public int nRisky;						// #23559 2011.6.20 yyagi Riskyでの残ミス数。0で閉店
		public bool bIsAllowedDoubleClickFullscreen;	// #26752 2011.11.27 yyagi ダブルクリックしてもフルスクリーンに移行しない
		public bool bIsSwappedGuitarBass			// #24063 2011.1.16 yyagi ギターとベースの切り替え中か否か
		{
			get;
			set;
		}
		public bool bIsSwappedGuitarBass_AutoFlagsAreSwapped	// #24415 2011.2.21 yyagi FLIP中にalt-f4終了で、AUTOフラグがswapした状態でconfig.iniが出力されてしまうことを避けるためのフラグ
		{
		    get;
		    set;
		}
		public bool bIsSwappedGuitarBass_PlaySettingsAreSwapped	// #35417 2015.8.18 yyagi FLIP中にalt-f4終了で、演奏設定がswapした状態でconfig.iniが出力されてしまうことを避けるためのフラグ
		{
		    get;
		    set;
		}

		public STAUTOPLAY bAutoPlay;
		public int nSoundDeviceType;				// #24820 2012.12.23 yyagi 出力サウンドデバイス(0=ACM(にしたいが設計がきつそうならDirectShow), 1=ASIO, 2=WASAPI)
		public int nWASAPIBufferSizeMs;				// #24820 2013.1.15 yyagi WASAPIのバッファサイズ
//		public int nASIOBufferSizeMs;				// #24820 2012.12.28 yyagi ASIOのバッファサイズ
		public int nASIODevice;						// #24820 2013.1.17 yyagi ASIOデバイス
        public bool bEventDrivenWASAPI;              // #36261 2016.4.27 yyagi WASAPI動作をevent drivenにするかどうか
		public bool bUseOSTimer;					// #33689 2014.6.6 yyagi 演奏タイマーの種類
		public bool bDynamicBassMixerManagement;	// #24820
		public bool bTimeStretch;					// #23664 2013.2.24 yyagi ピッチ変更無しで再生速度を変更するかどうか
		public STDGBVALUE<EInvisible> eInvisible;	// #32072 2013.9.20 yyagi チップを非表示にする
		public int nDisplayTimesMs, nFadeoutTimeMs;

		public STDGBVALUE<int> nViewerScrollSpeed;
		public bool bViewerVSyncWait;
		public bool bViewerShowDebugStatus;
		public bool bViewerTimeStretch;
		public bool bViewerDrums有効, bViewerGuitar有効;
		public int  nViewer初期ウィンドウ開始位置X;
		public int  nViewer初期ウィンドウ開始位置Y;
		public int  nViewerウインドウwidth;
		public int  nViewerウインドウheight;
		//public bool bNoMP3Streaming;				// 2014.4.14 yyagi; mp3のシーク位置がおかしくなる場合は、これをtrueにすることで、wavにデコードしてからオンメモリ再生する
		public int nMasterVolume;
#if false
		[StructLayout( LayoutKind.Sequential )]
		public struct STAUTOPLAY								// C定数のEレーンとindexを一致させること
		{
			public bool LC;			// 0
			public bool HH;			// 1
			public bool SD;			// 2
			public bool BD;			// 3
			public bool HT;			// 4
			public bool LT;			// 5
			public bool FT;			// 6
			public bool CY;			// 7
			public bool RD;			// 8
			public bool Guitar;		// 9	(not used)
			public bool Bass;		// 10	(not used)
			public bool GtR;		// 11
			public bool GtG;		// 12
			public bool GtB;		// 13
			public bool GtPick;		// 14
			public bool GtW;		// 15
			public bool BsR;		// 16
			public bool BsG;		// 17
			public bool BsB;		// 18
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
						case (int) Eレーン.RD:
							return this.RD;
						case (int) Eレーン.Guitar:
							return this.Guitar;
						case (int) Eレーン.Bass:
							return this.Bass;
						case (int) Eレーン.GtR:
							return this.GtR;
						case (int) Eレーン.GtG:
							return this.GtG;
						case (int) Eレーン.GtB:
							return this.GtB;
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
						case (int) Eレーン.RD:
							this.RD = value;
							return;
						case (int) Eレーン.Guitar:
							this.Guitar = value;
							return;
						case (int) Eレーン.Bass:
							this.Bass = value;
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
#endif
		#region [ STRANGE ]
		public STRANGE nヒット範囲ms;
		[StructLayout( LayoutKind.Sequential )]
		public struct STRANGE
		{
			public int Perfect;
			public int Great;
			public int Good;
			public int Poor;
			public int this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Perfect;

						case 1:
							return this.Great;

						case 2:
							return this.Good;

						case 3:
							return this.Poor;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
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
					}
					throw new IndexOutOfRangeException();
				}
			}
		}
		#endregion
		#region [ STLANEVALUE ]
		public STLANEVALUE nVelocityMin;
		[StructLayout( LayoutKind.Sequential )]
		public struct STLANEVALUE
		{
			public int LC;
			public int HH;
			public int SD;
			public int BD;
			public int HT;
			public int LT;
			public int FT;
			public int CY;
			public int RD;
            public int LP;
            public int LBD;
			public int Guitar;
			public int Bass;
			public int this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.LC;

						case 1:
							return this.HH;

						case 2:
							return this.SD;

						case 3:
							return this.BD;

						case 4:
							return this.HT;

						case 5:
							return this.LT;

						case 6:
							return this.FT;

						case 7:
							return this.CY;

						case 8:
							return this.RD;

                        case 9:
                            return this.LP;

                        case 10:
                            return this.LBD;

						case 11:
							return this.Guitar;

						case 12:
							return this.Bass;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.LC = value;
							return;

						case 1:
							this.HH = value;
							return;

						case 2:
							this.SD = value;
							return;

						case 3:
							this.BD = value;
							return;

						case 4:
							this.HT = value;
							return;

						case 5:
							this.LT = value;
							return;

						case 6:
							this.FT = value;
							return;

						case 7:
							this.CY = value;
							return;

						case 8:
							this.RD = value;
							return;

                        case 9:
                            this.LP = value;
                            return;

                        case 10:
                            this.LBD = value;
                            return;

						case 11:
							this.Guitar = value;
							return;

						case 12:
							this.Bass = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}
		#endregion

		// #27029 2012.1.5 from:
		// BDGroup が FP|BD→FP&BD に変化した際に自動変化するパラメータの値のバックアップ。FP&BD→FP|BD の時に元に戻す。
		// これらのバックアップ値は、BDGroup が FP&BD 状態の時にのみ Config.ini に出力され、BDGroup が FP|BD に戻ったら Config.ini から消える。
		public class CBackupOf1BD
		{
			public EHHGroup eHHGroup = EHHGroup.全部打ち分ける;
			public E打ち分け時の再生の優先順位 eHitSoundPriorityHH = E打ち分け時の再生の優先順位.ChipがPadより優先;
		}
		public CBackupOf1BD BackupOf1BD = null;

		public void SwapGuitarBassInfos_AutoFlags()
		{
			//bool ts = CDTXMania.ConfigIni.bAutoPlay.Bass;			// #24415 2011.2.21 yyagi: FLIP時のリザルトにAUTOの記録が混ざらないよう、AUTOのフラグもswapする
			//CDTXMania.ConfigIni.bAutoPlay.Bass = CDTXMania.ConfigIni.bAutoPlay.Guitar;
			//CDTXMania.ConfigIni.bAutoPlay.Guitar = ts;

			int looptime = (int) Eレーン.GtW - (int) Eレーン.GtR + 1;		// #29390 2013.1.25 yyagi ギターのAutoLane/AutoPick対応に伴い、FLIPもこれに対応
			for ( int i = 0; i < looptime; i++ )							// こんなに離れたところを独立して修正しなければならない設計ではいけませんね・・・
			{
				bool b = CDTXMania.ConfigIni.bAutoPlay[ i + (int) Eレーン.BsR ];
				CDTXMania.ConfigIni.bAutoPlay[ i + (int) Eレーン.BsR ] = CDTXMania.ConfigIni.bAutoPlay[ i + (int) Eレーン.GtR ];
				CDTXMania.ConfigIni.bAutoPlay[ i + (int) Eレーン.GtR ] = b;
			}

			CDTXMania.ConfigIni.bIsSwappedGuitarBass_AutoFlagsAreSwapped = !CDTXMania.ConfigIni.bIsSwappedGuitarBass_AutoFlagsAreSwapped;
		}
		public void SwapGuitarBassInfos_PlaySettings()			// #35417 2015.8.18 yyagi: 演奏設定のFLIP機能を追加
		{
			bool b;
			b = bHidden.Bass;	bHidden.Bass = bHidden.Guitar;	bHidden.Guitar = b;
			b = bLeft.Bass;		bLeft.Bass = bLeft.Guitar;		bLeft.Guitar = b;
			b = bLight.Bass;	bLight.Bass = bLight.Guitar;	bLight.Guitar = b;
			b = bReverse.Bass;	bReverse.Bass = bReverse.Guitar;	bReverse.Guitar = b;
			b = bSudden.Bass;	bSudden.Bass = bSudden.Guitar;	bSudden.Guitar = b;
            b = bGraph.Bass;    bGraph.Bass = bGraph.Guitar;    bGraph.Guitar = b;

			EInvisible ei;
			ei = eInvisible.Bass;	eInvisible.Bass = eInvisible.Guitar;	eInvisible.Guitar = ei;
			Eランダムモード er;
			er = eRandom.Bass;	eRandom.Bass = eRandom.Guitar; eRandom.Guitar = er;
			E判定文字表示位置 ej;
			ej = 判定文字表示位置.Bass; 判定文字表示位置.Bass = 判定文字表示位置.Guitar;	判定文字表示位置.Guitar = ej;
			int n;
			n = n表示可能な最小コンボ数.Bass; n表示可能な最小コンボ数.Bass = n表示可能な最小コンボ数.Guitar; n表示可能な最小コンボ数.Guitar = n;

			// 譜面スクロール速度の変更だけは、On活性化()で行うこと。そうしないと、演奏開始直後にスクロール速度が変化して見苦しい。
			n = n譜面スクロール速度.Bass; n譜面スクロール速度.Bass = n譜面スクロール速度.Guitar; n譜面スクロール速度.Guitar = n;

			CDTXMania.ConfigIni.bIsSwappedGuitarBass_PlaySettingsAreSwapped = !CDTXMania.ConfigIni.bIsSwappedGuitarBass_PlaySettingsAreSwapped;

		}
		// コンストラクタ

		public CConfigIni()
		{
#if false		// #23625 2011.1.11 Config.iniからダメージ/回復値の定数変更を行う場合はここを有効にする 087リリースに合わせ機能無効化
			//----------------------------------------
			this.fGaugeFactor[0,0] =  0.004f;
			this.fGaugeFactor[0,1] =  0.006f;
			this.fGaugeFactor[1,0] =  0.002f;
			this.fGaugeFactor[1,1] =  0.003f;
			this.fGaugeFactor[2,0] =  0.000f;
			this.fGaugeFactor[2,1] =  0.000f;
			this.fGaugeFactor[3,0] = -0.020f;
			this.fGaugeFactor[3,1] = -0.030f;
			this.fGaugeFactor[4,0] = -0.050f;
			this.fGaugeFactor[4,1] = -0.050f;

			this.fDamageLevelFactor[0] = 0.5f;
			this.fDamageLevelFactor[1] = 1.0f;
			this.fDamageLevelFactor[2] = 1.5f;
			//----------------------------------------
#endif
			this.strDTXManiaのバージョン = "Unknown";
			this.str曲データ検索パス = @".\";
			this.b全画面モード = false;
			this.b垂直帰線待ちを行う = true;
			this.n初期ウィンドウ開始位置X = 0; // #30675 2013.02.04 ikanick add
			this.n初期ウィンドウ開始位置Y = 0;
			this.nウインドウwidth = 0;			// #34069 2014.7.23 yyagi 初回起動時のwindow sizeは、CDTXMania側で設定する(-> 1280x720にする)
			this.nウインドウheight = 0;			//
			this.nフレーム毎スリープms = -1;			// #xxxxx 2011.11.27 yyagi add
			this.n非フォーカス時スリープms = 1;			// #23568 2010.11.04 ikanick add
			this._bGuitar有効 = true;
			this._bDrums有効 = true;
			this.nBGAlpha = 200;
			this.eダメージレベル = Eダメージレベル.普通;
			this.bSTAGEFAILED有効 = true;
			this.bForceAVIFullscreen = false;
			this.bAVI有効 = true;
			this.bBGA有効 = true;
			this.bフィルイン有効 = true;
			this.n曲が選択されてからプレビュー音が鳴るまでのウェイトms = 1000;
			this.n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms = 100;
			this.bWave再生位置自動調整機能有効 = true;
			this.bBGM音を発声する = true;
			this.bドラム打音を発声する = true;
			this.b歓声を発声する = true;
			this.bScoreIniを出力する = true;
			this.bランダムセレクトで子BOXを検索対象とする = true;
			this.n表示可能な最小コンボ数 = new STDGBVALUE<int>();
			this.n表示可能な最小コンボ数.Drums = 11;
			this.n表示可能な最小コンボ数.Guitar = 2;
			this.n表示可能な最小コンボ数.Bass = 2;
			this.str選曲リストフォント = "MS PGothic";
			this.n選曲リストフォントのサイズdot = 20;
			this.b選曲リストフォントを太字にする = true;
			this.n自動再生音量 = 80;
			this.n手動再生音量 = 100;
			this.bログ出力 = true;
			this.b演奏音を強調する = new STDGBVALUE<bool>();
			this.bSudden = new STDGBVALUE<bool>();
			this.bHidden = new STDGBVALUE<bool>();
			this.bReverse = new STDGBVALUE<bool>();
			this.eRandom = new STDGBVALUE<Eランダムモード>();
			this.bLight = new STDGBVALUE<bool>();
			this.bLeft = new STDGBVALUE<bool>();
			this.e判定位置 = new STDGBVALUE<E判定位置>();		// #33891 2014.6.26 yyagi
			this.判定文字表示位置 = new STDGBVALUE<E判定文字表示位置>();
			this.eドラムレーン表示位置 = new Eドラムレーン表示位置();
			this.n譜面スクロール速度 = new STDGBVALUE<int>();
			this.nInputAdjustTimeMs = new STDGBVALUE<int>();	// #23580 2011.1.3 yyagi
            this.nCommonBGMAdjustMs = 0;                        // #36372 2016.06.19 kairera0467
			this.nJudgeLinePosOffset = new STDGBVALUE<int>();	// #31602 2013.6.23 yyagi
			this.e判定表示優先度 = E判定表示優先度.Chipより上;
			for ( int i = 0; i < 3; i++ )
			{
				this.b演奏音を強調する[ i ] = true;
				this.bSudden[ i ] = false;
				this.bHidden[ i ] = false;
				this.bReverse[ i ] = false;
				this.eRandom[ i ] = Eランダムモード.OFF;
				this.bLight[ i ] = false;
				this.bLeft[ i ] = false;
				this.判定文字表示位置[ i ] = E判定文字表示位置.レーン上;
				this.n譜面スクロール速度[ i ] = 1;
				this.nInputAdjustTimeMs[ i ] = 0;
				this.nJudgeLinePosOffset[ i ] = 0;
				this.eInvisible[ i ] = EInvisible.OFF;
				this.nViewerScrollSpeed[ i ] = 1;
				this.e判定位置[ i ] = E判定位置.標準;
				//this.e判定表示優先度[ i ] = E判定表示優先度.Chipより下;
			}
			this.n演奏速度 = 20;
			#region [ AutoPlay ]
			this.bAutoPlay = new STAUTOPLAY();
			this.bAutoPlay.HH = false;
			this.bAutoPlay.SD = false;
			this.bAutoPlay.BD = false;
			this.bAutoPlay.HT = false;
			this.bAutoPlay.LT = false;
			this.bAutoPlay.FT = false;
			this.bAutoPlay.CY = false;
			this.bAutoPlay.LC = false;
			//this.bAutoPlay.Guitar = true;
			//this.bAutoPlay.Bass = true;
			this.bAutoPlay.GtR = true;
			this.bAutoPlay.GtG = true;
			this.bAutoPlay.GtB = true;
			this.bAutoPlay.GtPick = true;
			this.bAutoPlay.GtW = true;
			this.bAutoPlay.BsR = true;
			this.bAutoPlay.BsG = true;
			this.bAutoPlay.BsB = true;
			this.bAutoPlay.BsPick = true;
			this.bAutoPlay.BsW = true;
			#endregion
			this.nヒット範囲ms = new STRANGE();
			this.nヒット範囲ms.Perfect = 34;
			this.nヒット範囲ms.Great = 67;
			this.nヒット範囲ms.Good = 84;
			this.nヒット範囲ms.Poor = 117;
			this.ConfigIniファイル名 = "";
			this.dicJoystick = new Dictionary<int, string>( 10 );
			this.tデフォルトのキーアサインに設定する();
			#region [ velocityMin ]
			this.nVelocityMin.LC = 0;					// #23857 2011.1.31 yyagi VelocityMin
			this.nVelocityMin.HH = 20;
			this.nVelocityMin.SD = 0;
			this.nVelocityMin.BD = 0;
			this.nVelocityMin.HT = 0;
			this.nVelocityMin.LT = 0;
			this.nVelocityMin.FT = 0;
			this.nVelocityMin.CY = 0;
			this.nVelocityMin.RD = 0;
            this.nVelocityMin.LP = 0;
            this.nVelocityMin.LBD = 0;
			#endregion
			this.nRisky = 0;							// #23539 2011.7.26 yyagi RISKYモード
			this.nShowLagType = (int) EShowLagType.OFF;	// #25370 2011.6.3 yyagi ズレ時間表示
			this.bIsAutoResultCapture = false;			// #25399 2011.6.9 yyagi リザルト画像自動保存機能ON/OFF

			this.bバッファ入力を行う = true;
			this.bIsSwappedGuitarBass = false;			// #24063 2011.1.16 yyagi ギターとベースの切り替え
			this.bIsAllowedDoubleClickFullscreen = true;	// #26752 2011.11.26 ダブルクリックでのフルスクリーンモード移行を許可
			this.eBDGroup = EBDGroup.打ち分ける;		// #27029 2012.1.4 from HHPedalとBassPedalのグルーピング
			this.nPoliphonicSounds = 4;					// #28228 2012.5.1 yyagi レーン毎の最大同時発音数
														// #24820 2013.1.15 yyagi 初期値を4から2に変更。BASS.net使用時の負荷軽減のため。
														// #24820 2013.1.17 yyagi 初期値を4に戻した。動的なミキサー制御がうまく動作しているため。
			this.bIsEnabledSystemMenu = true;			// #28200 2012.5.1 yyagi System Menuの利用可否切替(使用可)
			this.strSystemSkinSubfolderFullName = "";	// #28195 2012.5.2 yyagi 使用中のSkinサブフォルダ名
			this.bUseBoxDefSkin = true;					// #28195 2012.5.6 yyagi box.defによるスキン切替機能を使用するか否か
			this.bTight = false;                        // #29500 2012.9.11 kairera0467 TIGHTモード
			#region [ WASAPI/ASIO ]
			this.nSoundDeviceType = FDK.COS.bIsVistaOrLater() ?
				(int) ESoundDeviceTypeForConfig.WASAPI : (int) ESoundDeviceTypeForConfig.ACM;	// #24820 2012.12.23 yyagi 初期値はACM | #31927 2013.8.25 yyagi OSにより初期値変更
			this.nWASAPIBufferSizeMs = 50;				// #24820 2013.1.15 yyagi 初期値は50(0で自動設定)
			this.nASIODevice = 0;						// #24820 2013.1.17 yyagi
//			this.nASIOBufferSizeMs = 0;					// #24820 2012.12.25 yyagi 初期値は0(自動設定)
            this.bEventDrivenWASAPI = false;
			#endregion

			this.bUseOSTimer = false;;					// #33689 2014.6.6 yyagi 初期値はfalse (FDKのタイマー。ＦＲＯＭ氏考案の独自タイマー)
			this.bDynamicBassMixerManagement = true;	//
			this.bTimeStretch = false;					// #23664 2013.2.24 yyagi 初期値はfalse (再生速度変更を、ピッチ変更にて行う)
			this.nDisplayTimesMs = 3000;				// #32072 2013.10.24 yyagi Semi-Invisibleでの、チップ再表示期間
			this.nFadeoutTimeMs = 2000;					// #32072 2013.10.24 yyagi Semi-Invisibleでの、チップフェードアウト時間

			this.bViewerVSyncWait = true;
			this.bViewerShowDebugStatus = true;
			this.bViewerTimeStretch = false;
			this.bViewerDrums有効 = true;
			this.bViewerGuitar有効 = true;
			this.nViewer初期ウィンドウ開始位置X = 0;
			this.nViewer初期ウィンドウ開始位置Y = 0;
			this.nViewerウインドウwidth = 640;
			this.nViewerウインドウheight = 360;

            #region[ Ver.K追加 ]
            this.bCLASSIC譜面判別を有効にする = true;
            this.bJudgeCountDisp = false;
            this.bSkillModeを自動切替えする = true;
            this.bXPerfect判定を有効にする = false;
            this.bWindowClipMode = false;
            this.b曲名表示をdefのものにする = true;
            this.eHHOGraphics = Eタイプ.A;
            this.eJudgeAnimeType = Eタイプ.C;
            this.eLaneType = Eタイプ.A;
            this.eLBDGraphics = Eタイプ.A;
            this.eMovieClipMode = EMovieClipMode.FullScreen;
            this.eNamePlateType = Eタイプ.A;
            this.eRDPosition = ERDPosition.RCRD;
            this.eSkillMode = ESkillType.DTXMania;
            for( int i = 0; i < 3; i++ )
            {
                this.eJUST[ i ] = EJust.OFF;
            }

            this.nExplosionFrames = 1;
            this.nExplosionInterval = 50;
            this.nExplosionWidth = 0;
            this.nExplosionHeight = 0;
            this.nJudgeFrames = 24;
            this.nJudgeInterval = 14;
            this.nJudgeWidth = 250;
            this.nJudgeHeight = 170;
            this.nJudgeStringBarX = new int[] { 0, 0, 0 };
            this.nJudgeStringBarY = new int[] { 112, 134, 156 };
            this.nJudgeStringBarWidth = new int[] { 210, 210, 210 };
            this.nJudgeStringBarHeight = new int[] { 20, 20, 20 };
            this.nNameColor = new STDGBVALUE<int>();
            this.nShutterInSide = new STDGBVALUE<int>();
            this.nShutterOutSide = new STDGBVALUE<int>();
            this.strCardName = new STDGBVALUE<string>();
            this.strGroupName = new STDGBVALUE<string>();
            this.strResultSongNameFont = "MS PGothic";

            for( int i = 0; i < 3; i++ )
            {
                this.bJudgeLineDisp[ i ] = true;
                this.bLaneFlush[ i ] = true;
                this.nLaneDispType[ i ] = 0;
                this.nShutterInSide[ i ] = 0;
                this.nShutterOutSide[ i ] = 0;
                this.strShutterImageName[ i ] = "";
            }

            this.nPedalJudgeRangeDelta = 20;
            this.bフレームを表示する = true;
            #endregion

            //this.bNoMP3Streaming = false;
			this.nMasterVolume = 100;					// #33700 2014.4.26 yyagi マスターボリュームの設定(WASAPI/ASIO用)

            this.bWarnMIDI20USB = true;
            this.bWarnSoundDeviceOnUSB = true;
		}
		public CConfigIni( string iniファイル名 )
			: this()
		{
			this.tファイルから読み込み( iniファイル名 );
		}


		// メソッド

		public void t指定した入力が既にアサイン済みである場合はそれを全削除する( E入力デバイス DeviceType, int nID, int nCode )
		{
			for( int i = 0; i <= (int)EKeyConfigPart.SYSTEM; i++ )
			{
				for( int j = 0; j <= (int)EKeyConfigPad.Capture; j++ )
				{
					for( int k = 0; k < 0x10; k++ )
					{
						if( ( ( this.KeyAssign[ i ][ j ][ k ].入力デバイス == DeviceType ) && ( this.KeyAssign[ i ][ j ][ k ].ID == nID ) ) && ( this.KeyAssign[ i ][ j ][ k ].コード == nCode ) )
						{
							for( int m = k; m < 15; m++ )
							{
								this.KeyAssign[ i ][ j ][ m ] = this.KeyAssign[ i ][ j ][ m + 1 ];
							}
							this.KeyAssign[ i ][ j ][ 15 ].入力デバイス = E入力デバイス.不明;
							this.KeyAssign[ i ][ j ][ 15 ].ID = 0;
							this.KeyAssign[ i ][ j ][ 15 ].コード = 0;
							k--;
						}
					}
				}
			}
		}
		public void t書き出し( string iniファイル名 )
		{
			StreamWriter sw = new StreamWriter( iniファイル名, false, Encoding.GetEncoding( "utf-16" ) );
			sw.WriteLine( ";-------------------" );
			
			#region [ System ]
			sw.WriteLine( "[System]" );
			sw.WriteLine();

#if false		// #23625 2011.1.11 Config.iniからダメージ/回復値の定数変更を行う場合はここを有効にする 087リリースに合わせ機能無効化
	//------------------------------
			sw.WriteLine("; ライフゲージのパラメータ調整(調整完了後削除予定)");
			sw.WriteLine("; GaugeFactorD: ドラムのPerfect, Great,... の回復量(ライフMAXを1.0としたときの値を指定)");
			sw.WriteLine("; GaugeFactorG:  Gt/BsのPerfect, Great,... の回復量(ライフMAXを1.0としたときの値を指定)");
			sw.WriteLine("; DamageFactorD: DamageLevelがSmall, Normal, Largeの時に対するダメージ係数");
			sw.WriteLine("GaugeFactorD={0}, {1}, {2}, {3}, {4}", this.fGaugeFactor[0, 0], this.fGaugeFactor[1, 0], this.fGaugeFactor[2, 0], this.fGaugeFactor[3, 0], this.fGaugeFactor[4, 0]);
			sw.WriteLine("GaugeFactorG={0}, {1}, {2}, {3}, {4}", this.fGaugeFactor[0, 1], this.fGaugeFactor[1, 1], this.fGaugeFactor[2, 1], this.fGaugeFactor[3, 1], this.fGaugeFactor[4, 1]);
			sw.WriteLine("DamageFactor={0}, {1}, {2}", this.fDamageLevelFactor[0], this.fDamageLevelFactor[1], fDamageLevelFactor[2]);
			sw.WriteLine();
	//------------------------------
#endif
			#region [ Version ]
			sw.WriteLine( "; リリースバージョン" );
			sw.WriteLine( "; Release Version." );
			sw.WriteLine( "Version={0}", CDTXMania.VERSION );
			sw.WriteLine();
			#endregion
			#region [ DTXPath ]
			sw.WriteLine( "; 演奏データの格納されているフォルダへのパス。" );
			sw.WriteLine( @"; セミコロン(;)で区切ることにより複数のパスを指定できます。（例: d:\DTXFiles1\;e:\DTXFiles2\）" );
			sw.WriteLine( "; Pathes for DTX data." );
			sw.WriteLine( @"; You can specify many pathes separated with semicolon(;). (e.g. d:\DTXFiles1\;e:\DTXFiles2\)" );
			sw.WriteLine( "DTXPath={0}", this.str曲データ検索パス );
			sw.WriteLine();
			#endregion
            #region[ プレイヤー名 ]
            sw.WriteLine( "; プレイヤーネーム。" );
            sw.WriteLine( @"; 演奏中のネームプレートに表示される名前を設定できます。" );
            sw.WriteLine( "; 英字、数字の他、ひらがな、カタカナ、半角カナ、漢字なども入力できます。" );
            sw.WriteLine( "; 入力されていない場合は「GUEST」と表示されます。" );
            sw.WriteLine( "CardNameDrums={0}", this.strCardName[ 0 ] );
            sw.WriteLine( "CardNameGuitar={0}", this.strCardName[ 1 ] );
            sw.WriteLine( "CardNameBass={0}", this.strCardName[ 2 ] );
            sw.WriteLine();
            sw.WriteLine( "; グループ名" );
            sw.WriteLine( @"; 演奏中のネームプレートに表示されるグループ名を設定できます。" );
            sw.WriteLine( "; 英字、数字の他、ひらがな、カタカナ、半角カナ、漢字なども入力できます。" );
            sw.WriteLine( "; 入力されていない場合は何も表示されません。" );
            sw.WriteLine( "GroupNameDrums={0}", this.strGroupName[ 0 ] );
            sw.WriteLine( "GroupNameGuitar={0}", this.strGroupName[ 1 ] );
            sw.WriteLine( "GroupNameBass={0}", this.strGroupName[ 2 ] );
            sw.WriteLine();
            sw.WriteLine( "; ネームカラー" );
            sw.WriteLine( "; 0=白, 1=薄黄色, 2=黄色, 3=緑, 4=青, 5=紫, 6=赤, 7=銅, 8=銀, 9=金, 11～16=各色のグラデーション" );
            sw.WriteLine( "NameColorDrums={0}", this.nNameColor[ 0 ] );
            sw.WriteLine( "NameColorGuitar={0}", this.nNameColor[ 1 ] );
            sw.WriteLine( "NameColorBass={0}", this.nNameColor[ 2 ] );
            sw.WriteLine();
            #endregion
            #region [ スキン関連 ]
            #region [ Skinパスの絶対パス→相対パス変換 ]
            Uri uriRoot = new Uri( System.IO.Path.Combine( CDTXMania.strEXEのあるフォルダ, "System" + System.IO.Path.DirectorySeparatorChar ) );
			if ( strSystemSkinSubfolderFullName != null && strSystemSkinSubfolderFullName.Length == 0 )
			{
				// Config.iniが空の状態でDTXManiaをViewerとして起動・終了すると、strSystemSkinSubfolderFullName が空の状態でここに来る。
				// → 初期値として Default/ を設定する。
				strSystemSkinSubfolderFullName = System.IO.Path.Combine( CDTXMania.strEXEのあるフォルダ, "System" + System.IO.Path.DirectorySeparatorChar + "Default" + System.IO.Path.DirectorySeparatorChar );
			}
			Uri uriPath = new Uri( System.IO.Path.Combine( this.strSystemSkinSubfolderFullName, "." + System.IO.Path.DirectorySeparatorChar ) );
			string relPath = uriRoot.MakeRelativeUri( uriPath ).ToString();				// 相対パスを取得
			relPath = System.Web.HttpUtility.UrlDecode( relPath );						// デコードする
			relPath = relPath.Replace( '/', System.IO.Path.DirectorySeparatorChar );	// 区切り文字が\ではなく/なので置換する
			#endregion
			sw.WriteLine( "; 使用するSkinのフォルダ名。" );
			sw.WriteLine( "; 例えば System\\Default\\Graphics\\... などの場合は、SkinPath=.\\Default\\ を指定します。" );
			sw.WriteLine( "; Skin folder path." );
			sw.WriteLine( "; e.g. System\\Default\\Graphics\\... -> Set SkinPath=.\\Default\\" );
			sw.WriteLine( "SkinPath={0}", relPath );
			sw.WriteLine();
			sw.WriteLine( "; box.defが指定するSkinに自動で切り替えるかどうか (0=切り替えない、1=切り替える)" );
			sw.WriteLine( "; Automatically change skin specified in box.def. (0=No 1=Yes)" );
			sw.WriteLine( "SkinChangeByBoxDef={0}", this.bUseBoxDefSkin? 1 : 0 );
			sw.WriteLine();
			#endregion
			#region [ Window関連 ]
			sw.WriteLine( "; 画面モード(0:ウィンドウ, 1:全画面)" );
			sw.WriteLine( "; Screen mode. (0:Window, 1:Fullscreen)" );
			sw.WriteLine( "FullScreen={0}", this.b全画面モード ? 1 : 0 );
            sw.WriteLine();
			sw.WriteLine("; ウインドウモード時の画面幅");				// #23510 2010.10.31 yyagi add
			sw.WriteLine("; A width size in the window mode.");			//
			sw.WriteLine("WindowWidth={0}", this.nウインドウwidth);		//
			sw.WriteLine();												//
			sw.WriteLine("; ウインドウモード時の画面高さ");				//
			sw.WriteLine("; A height size in the window mode.");		//
			sw.WriteLine("WindowHeight={0}", this.nウインドウheight);	//
			sw.WriteLine();												//
			sw.WriteLine( "; ウィンドウモード時の位置X" );				            // #30675 2013.02.04 ikanick add
			sw.WriteLine( "; X position in the window mode." );			            //
			sw.WriteLine( "WindowX={0}", this.n初期ウィンドウ開始位置X );			//
			sw.WriteLine();											            	//
			sw.WriteLine( "; ウィンドウモード時の位置Y" );			            	//
			sw.WriteLine( "; Y position in the window mode." );	            	    //
			sw.WriteLine( "WindowY={0}", this.n初期ウィンドウ開始位置Y );   		//
			sw.WriteLine();												            //

			sw.WriteLine( "; ウインドウをダブルクリックした時にフルスクリーンに移行するか(0:移行しない,1:移行する)" );	// #26752 2011.11.27 yyagi
			sw.WriteLine( "; Whether double click to go full screen mode or not.(0:No, 1:Yes)" );		//
			sw.WriteLine( "DoubleClickFullScreen={0}", this.bIsAllowedDoubleClickFullscreen? 1 : 0);	//
			sw.WriteLine();																				//
			sw.WriteLine( "; ALT+SPACEのメニュー表示を抑制するかどうか(0:抑制する 1:抑制しない)" );		// #28200 2012.5.1 yyagi
			sw.WriteLine( "; Whether ALT+SPACE menu would be masked or not.(0=masked 1=not masked)" );	//
			sw.WriteLine( "EnableSystemMenu={0}", this.bIsEnabledSystemMenu? 1 : 0 );					//
			sw.WriteLine();																				//
			sw.WriteLine( "; 非フォーカス時のsleep値[ms]" );	    			    // #23568 2011.11.04 ikanick add
			sw.WriteLine( "; A sleep time[ms] while the window is inactive." );	//
			sw.WriteLine( "BackSleep={0}", this.n非フォーカス時スリープms );		// そのまま引用（苦笑）
			sw.WriteLine();											        			//
			#endregion
			#region [ フレーム処理関連(VSync, フレーム毎のsleep) ]
			sw.WriteLine("; 垂直帰線同期(0:OFF,1:ON)");
			sw.WriteLine( "VSyncWait={0}", this.b垂直帰線待ちを行う ? 1 : 0 );
            sw.WriteLine();
			sw.WriteLine( "; フレーム毎のsleep値[ms] (-1でスリープ無し, 0以上で毎フレームスリープ。動画キャプチャ等で活用下さい)" );	// #xxxxx 2011.11.27 yyagi add
			sw.WriteLine( "; A sleep time[ms] per frame." );							//
			sw.WriteLine( "SleepTimePerFrame={0}", this.nフレーム毎スリープms );		//
			sw.WriteLine();											        			//
			#endregion
			#region [ WASAPI/ASIO関連 ]
			sw.WriteLine( "; サウンド出力方式(0=ACM(って今はまだDirectSoundですが), 1=ASIO, 2=WASAPI)" );
			sw.WriteLine( "; WASAPIはVista以降のOSで使用可能。推奨方式はWASAPI。" );
			sw.WriteLine( "; なお、WASAPIが使用不可ならASIOを、ASIOが使用不可ならACMを使用します。" );
			sw.WriteLine( "; Sound device type(0=ACM, 1=ASIO, 2=WASAPI)" );
			sw.WriteLine( "; WASAPI can use on Vista or later OSs." );
			sw.WriteLine( "; If WASAPI is not available, DTXMania try to use ASIO. If ASIO can't be used, ACM is used." );
			sw.WriteLine( "SoundDeviceType={0}", (int) this.nSoundDeviceType );
			sw.WriteLine();

			sw.WriteLine( "; WASAPI使用時のサウンドバッファサイズ" );
			sw.WriteLine( "; (0=デバイスに設定可能な最小値を自動設定, 1～9999=バッファサイズ(単位:ms)の手動指定" );
			sw.WriteLine( "; WASAPI Sound Buffer Size." );
			sw.WriteLine( "; (0=set minimum buffer size automaticcaly, 1-9999=specify the buffer size(ms) by yourself)" );
			sw.WriteLine( "WASAPIBufferSizeMs={0}", (int) this.nWASAPIBufferSizeMs );
			sw.WriteLine();
				
			sw.WriteLine( "; ASIO使用時のサウンドデバイス" );
			sw.WriteLine( "; 存在しないデバイスを指定すると、DTXManiaが起動しないことがあります。" );
			sw.WriteLine( "; Sound device used by ASIO." );
			sw.WriteLine( "; Don't specify unconnected device, as the DTXMania may not bootup." );
			string[] asiodev = CEnumerateAllAsioDevices.GetAllASIODevices();
			for ( int i = 0; i < asiodev.Length; i++ )
			{
				sw.WriteLine( "; {0}: {1}", i, asiodev[ i ] );
			}
			sw.WriteLine( "ASIODevice={0}", (int) this.nASIODevice );
			sw.WriteLine();

			//sw.WriteLine( "; ASIO使用時のサウンドバッファサイズ" );
			//sw.WriteLine( "; (0=デバイスに設定されている値を使用, 1～9999=バッファサイズ(単位:ms)の手動指定" );
			//sw.WriteLine( "; ASIO Sound Buffer Size." );
			//sw.WriteLine( "; (0=Use the value specified to the device, 1-9999=specify the buffer size(ms) by yourself)" );
			//sw.WriteLine( "ASIOBufferSizeMs={0}", (int) this.nASIOBufferSizeMs );
			//sw.WriteLine();

			//sw.WriteLine( "; Bass.Mixの制御を動的に行うか否か。" );
			//sw.WriteLine( "; ONにすると、ギター曲などチップ音の多い曲も再生できますが、画面が少しがたつきます。" );
			//sw.WriteLine( "; (0=行わない, 1=行う)" );
			//sw.WriteLine( "DynamicBassMixerManagement={0}", this.bDynamicBassMixerManagement ? 1 : 0 );
			//sw.WriteLine();

			sw.WriteLine( "; WASAPI/ASIO時に使用する演奏タイマーの種類" );
			sw.WriteLine( "; Playback timer used for WASAPI/ASIO" );
			sw.WriteLine( "; (0=FDK Timer, 1=System Timer)" );
			sw.WriteLine( "SoundTimerType={0}", this.bUseOSTimer ? 1 : 0 );
			sw.WriteLine();

			sw.WriteLine( "; WASAPI使用時にEventDrivenモードを使う" );
			sw.WriteLine( "EventDrivenWASAPI={0}", this.bEventDrivenWASAPI ? 1 : 0 );
			sw.WriteLine();

			sw.WriteLine( "; 全体ボリュームの設定" );
			sw.WriteLine( "; (0=無音 ～ 100=最大。WASAPI/ASIO時のみ有効)" );
			sw.WriteLine( "; Master volume settings" );
			sw.WriteLine( "; (0=Silent - 100=Max)" );
			sw.WriteLine( "MasterVolume={0}", this.nMasterVolume );
			sw.WriteLine();

			#endregion
			#region [ ギター/ベース/ドラム 有効/無効 ]
			sw.WriteLine( "; ギター/ベース有効(0:OFF,1:ON)" );
			sw.WriteLine( "; Enable Guitar/Bass or not.(0:OFF,1:ON)" );
			sw.WriteLine( "Guitar={0}", this.bGuitar有効 ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ドラム有効(0:OFF,1:ON)" );
			sw.WriteLine( "; Enable Drums or not.(0:OFF,1:ON)" );
			sw.WriteLine( "Drums={0}", this.bDrums有効 ? 1 : 0 );
			sw.WriteLine();
			#endregion
			sw.WriteLine( "; 背景画像の半透明割合(0:不透明～255:透明)" );
			sw.WriteLine( "; Transparency for background image in playing screen.(0:no tranaparent - 255:transparent)" );
			sw.WriteLine( "BGAlpha={0}", this.nBGAlpha );
			sw.WriteLine();
			sw.WriteLine( "; Missヒット時のゲージ減少割合(0:少, 1:普通, 2:大)" );
			sw.WriteLine( "DamageLevel={0}", (int) this.eダメージレベル );
			sw.WriteLine();
			sw.WriteLine( "; ゲージゼロでSTAGE FAILED (0:OFF, 1:ON)" );
			sw.WriteLine( "StageFailed={0}", this.bSTAGEFAILED有効 ? 1 : 0 );
			sw.WriteLine();
			#region [ 打ち分け関連 ]
			sw.WriteLine( "; LC/HHC/HHO 打ち分けモード (0:LC|HHC|HHO, 1:LC&(HHC|HHO), 2:LC|(HHC&HHO), 3:LC&HHC&HHO)" );
			sw.WriteLine( "; LC/HHC/HHO Grouping       (0:LC|HHC|HHO, 1:LC&(HHC|HHO), 2:LC|(HHC&HHO), 3:LC&HHC&HHO)" );
			sw.WriteLine( "HHGroup={0}", (int) this.eHHGroup );
			sw.WriteLine();
			sw.WriteLine( "; LT/FT 打ち分けモード (0:LT|FT, 1:LT&FT)" );
			sw.WriteLine( "; LT/FT Grouping       (0:LT|FT, 1:LT&FT)" );
			sw.WriteLine( "FTGroup={0}", (int) this.eFTGroup );
			sw.WriteLine();
			sw.WriteLine( "; CY/RD 打ち分けモード (0:CY|RD, 1:CY&RD)" );
			sw.WriteLine( "; CY/RD Grouping       (0:CY|RD, 1:CY&RD)" );
			sw.WriteLine( "CYGroup={0}", (int) this.eCYGroup );
			sw.WriteLine();
			sw.WriteLine( "; LP/LBD/BD 打ち分けモード(0:LP|LBD|BD, 1:LP|(LBD&BD), 2:LP&(LBD|BD), 3:LP&LBD&BD)" );		// #27029 2012.1.4 from
            sw.WriteLine( "; LP/LBD/BD Grouping     (0:LP|LBD|BD, 1:LP(LBD&BD), 2:LP&(LBD|BD), 3:LP&LBD&BD)");
			sw.WriteLine( "BDGroup={0}", (int) this.eBDGroup );
			sw.WriteLine();
			sw.WriteLine( "; 打ち分け時の再生音の優先順位(HHGroup)(0:Chip>Pad, 1:Pad>Chip)" );
			sw.WriteLine( "; Grouping sound priority(HHGroup)(0:Chip>Pad, 1:Pad>Chip)" );
			sw.WriteLine( "HitSoundPriorityHH={0}", (int) this.eHitSoundPriorityHH );
			sw.WriteLine();
			sw.WriteLine( "; 打ち分け時の再生音の優先順位(FTGroup)(0:Chip>Pad, 1:Pad>Chip)" );
			sw.WriteLine( "; Grouping sound priority(FTGroup)(0:Chip>Pad, 1:Pad>Chip)" );
			sw.WriteLine( "HitSoundPriorityFT={0}", (int) this.eHitSoundPriorityFT );
			sw.WriteLine();
			sw.WriteLine( "; 打ち分け時の再生音の優先順位(CYGroup)(0:Chip>Pad, 1:Pad>Chip)" );
			sw.WriteLine( "; Grouping sound priority(CYGroup)(0:Chip>Pad, 1:Pad>Chip)" );
			sw.WriteLine( "HitSoundPriorityCY={0}", (int) this.eHitSoundPriorityCY );
			sw.WriteLine();
			sw.WriteLine( "; シンバルフリーモード(0:OFF, 1:ON)" );
			sw.WriteLine( "; Grouping CY and LC (0:OFF, 1:ON)" );
			sw.WriteLine( "CymbalFree={0}", this.bシンバルフリー ? 1 : 0 );
			sw.WriteLine();
			#endregion
			#region [ AVI/BGA ]
			sw.WriteLine( "; AVIの表示(0:OFF, 1:ON)" );
			sw.WriteLine( "AVI={0}", this.bAVI有効 ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; 旧サイズのAVI表示を強制的に全画面化する(0:OFF, 1:ON)" );
			sw.WriteLine( "; Forcely show BGA-sized movie as Fullscreen." );
			sw.WriteLine( "ForceAVIFullscreen={0}", this.bForceAVIFullscreen ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; BGAの表示(0:OFF, 1:ON)" );
			sw.WriteLine( "BGA={0}", this.bBGA有効 ? 1 : 0 );
			sw.WriteLine();
			//sw.WriteLine( "; クリップの表示位置(0:OFF, 1:FullScreen, 2:Window, 3:FullScreen + Window)" );
			//sw.WriteLine( "MovieClipMode={0}", (int)this.eMovieClipMode );
			//sw.WriteLine();
			sw.WriteLine( "; クリップのウィンドウ表示(0:OFF, 1:ON)" );
			sw.WriteLine( "WindowClipDisp={0}", this.bWindowClipMode ? 1 : 0 );
			sw.WriteLine();
			#endregion
			#region [ フィルイン ]
			sw.WriteLine( "; フィルイン効果(0:OFF, 1:ON)" );
			sw.WriteLine( "FillInEffect={0}", this.bフィルイン有効 ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; フィルイン達成時の歓声の再生(0:OFF, 1:ON)" );
			sw.WriteLine( "AudienceSound={0}", this.b歓声を発声する ? 1 : 0 );
			sw.WriteLine();
			#endregion
			#region [ プレビュー音 ]
			sw.WriteLine( "; 曲選択からプレビュー音の再生までのウェイト[ms]" );
			sw.WriteLine( "PreviewSoundWait={0}", this.n曲が選択されてからプレビュー音が鳴るまでのウェイトms );
			sw.WriteLine();
            //sw.WriteLine( "; 曲選択からプレビュー画像表示までのウェイト[ms]" );
            //sw.WriteLine( "PreviewImageWait={0}", this.n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms );
            //sw.WriteLine();
			#endregion
			sw.WriteLine( "; Waveの再生位置自動補正(0:OFF, 1:ON)" );
			sw.WriteLine( "AdjustWaves={0}", this.bWave再生位置自動調整機能有効 ? 1 : 0 );
			sw.WriteLine();
			#region [ BGM/ドラムヒット音の再生 ]
			sw.WriteLine( "; BGM の再生(0:OFF, 1:ON)" );
			sw.WriteLine( "BGMSound={0}", this.bBGM音を発声する ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ドラム打音の再生(0:OFF, 1:ON)" );
			sw.WriteLine( "HitSound={0}", this.bドラム打音を発声する ? 1 : 0 );
			sw.WriteLine();
			#endregion
			sw.WriteLine( "; 演奏記録（～.score.ini）の出力 (0:OFF, 1:ON)" );
			sw.WriteLine( "SaveScoreIni={0}", this.bScoreIniを出力する ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; RANDOM SELECT で子BOXを検索対象に含める (0:OFF, 1:ON)" );
			sw.WriteLine( "RandomFromSubBox={0}", this.bランダムセレクトで子BOXを検索対象とする ? 1 : 0 );
			sw.WriteLine();
			#region [ モニターサウンド(ヒット音の再生音量アップ) ]
			sw.WriteLine( "; ドラム演奏時にドラム音を強調する (0:OFF, 1:ON)" );
			sw.WriteLine( "SoundMonitorDrums={0}", this.b演奏音を強調する.Drums ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ギター演奏時にギター音を強調する (0:OFF, 1:ON)" );
			sw.WriteLine( "SoundMonitorGuitar={0}", this.b演奏音を強調する.Guitar ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ベース演奏時にベース音を強調する (0:OFF, 1:ON)" );
			sw.WriteLine( "SoundMonitorBass={0}", this.b演奏音を強調する.Bass ? 1 : 0 );
			sw.WriteLine();
			#endregion
			#region [ コンボ表示数 ]
			sw.WriteLine( "; ドラムの表示可能な最小コンボ数(1～99999)" );
			sw.WriteLine( "MinComboDrums={0}", this.n表示可能な最小コンボ数.Drums );
			sw.WriteLine();
			sw.WriteLine( "; ギターの表示可能な最小コンボ数(1～99999)" );
			sw.WriteLine( "MinComboGuitar={0}", this.n表示可能な最小コンボ数.Guitar );
			sw.WriteLine();
			sw.WriteLine( "; ベースの表示可能な最小コンボ数(1～99999)" );
			sw.WriteLine( "MinComboBass={0}", this.n表示可能な最小コンボ数.Bass );
			sw.WriteLine();
			#endregion
			sw.WriteLine( "; 演奏情報を表示する (0:OFF, 1:ON)" );
			sw.WriteLine( "; Showing playing info on the playing screen. (0:OFF, 1:ON)" );
			sw.WriteLine( "ShowDebugStatus={0}", this.b演奏情報を表示する ? 1 : 0 );
			sw.WriteLine();
			#region [ 選曲リストのフォント ]
			sw.WriteLine( "; 選曲リストのフォント名" );
			sw.WriteLine( "; Font name for select song item." );
			sw.WriteLine( "SelectListFontName={0}", this.str選曲リストフォント );
			sw.WriteLine();
			sw.WriteLine( "; 選曲リストのフォントのサイズ[dot]" );
			sw.WriteLine( "; Font size[dot] for select song item." );
			sw.WriteLine( "SelectListFontSize={0}", this.n選曲リストフォントのサイズdot );
			sw.WriteLine();
			sw.WriteLine( "; 選曲リストのフォントを斜体にする (0:OFF, 1:ON)" );
			sw.WriteLine( "; Using italic font style select song list. (0:OFF, 1:ON)" );
			sw.WriteLine( "SelectListFontItalic={0}", this.b選曲リストフォントを斜体にする ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; 選曲リストのフォントを太字にする (0:OFF, 1:ON)" );
			sw.WriteLine( "; Using bold font style select song list. (0:OFF, 1:ON)" );
			sw.WriteLine( "SelectListFontBold={0}", this.b選曲リストフォントを太字にする ? 1 : 0 );
			sw.WriteLine();
			#endregion
			sw.WriteLine( "; 打音の音量(0～100%)" );
			sw.WriteLine( "; Sound volume (you're playing) (0-100%)" );
			sw.WriteLine( "ChipVolume={0}", this.n手動再生音量 );
			sw.WriteLine();
			sw.WriteLine( "; 自動再生音の音量(0～100%)" );
			sw.WriteLine( "; Sound volume (auto playing) (0-100%)" );
			sw.WriteLine( "AutoChipVolume={0}", this.n自動再生音量 );
			sw.WriteLine();
			sw.WriteLine( "; ストイックモード(0:OFF, 1:ON)" );
			sw.WriteLine( "; Stoic mode. (0:OFF, 1:ON)" );
			sw.WriteLine( "StoicMode={0}", this.bストイックモード ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; バッファ入力モード(0:OFF, 1:ON)" );
			sw.WriteLine( "; Using Buffered input (0:OFF, 1:ON)" );
			sw.WriteLine( "BufferedInput={0}", this.bバッファ入力を行う ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; レーン毎の最大同時発音数(1～8)" );
			sw.WriteLine( "; Number of polyphonic sounds per lane. (1-8)" );
			sw.WriteLine( "PolyphonicSounds={0}", this.nPoliphonicSounds );
			sw.WriteLine();
			sw.WriteLine( "; 判定ズレ時間表示(0:OFF, 1:ON, 2=GREAT-POOR)" );				// #25370 2011.6.3 yyagi
			sw.WriteLine( "; Whether displaying the lag times from the just timing or not." );	//
			sw.WriteLine( "ShowLagTime={0}", this.nShowLagType );							//
			sw.WriteLine();
			sw.WriteLine( "; 判定・コンボ表示優先度(0:チップの下, 1:チップの上)" );
			sw.WriteLine( "; judgement/combo display priority (0:under chips, 1:over chips)" );
			sw.WriteLine( "JudgeDispPriority={0}" , (int) this.e判定表示優先度 );
			sw.WriteLine();
			sw.WriteLine( "; ドラムのレーン表示位置(0:左側, 1:中央)" );
			sw.WriteLine( "; drums lane position (0:LEFT, 1:CENTER)" );
			sw.WriteLine( "DrumsLanePosition={0}", (int) this.eドラムレーン表示位置 );
			sw.WriteLine();


			sw.WriteLine( "; リザルト画像自動保存機能(0:OFF, 1:ON)" );						// #25399 2011.6.9 yyagi
			sw.WriteLine( "; Set \"1\" if you'd like to save result screen image automatically");	//
			sw.WriteLine( "; when you get hiscore/hiskill.");								//
			sw.WriteLine( "AutoResultCapture={0}", this.bIsAutoResultCapture? 1 : 0 );		//
			sw.WriteLine();
			sw.WriteLine( "; 再生速度変更を、ピッチ変更で行うかどうか(0:ピッチ変更, 1:タイムストレッチ" );	// #23664 2013.2.24 yyagi
			sw.WriteLine( "; (WASAPI/ASIO使用時のみ有効) " );
			sw.WriteLine( "; Set \"0\" if you'd like to use pitch shift with PlaySpeed." );	//
			sw.WriteLine( "; Set \"1\" for time stretch." );								//
			sw.WriteLine( "; (Only available when you're using using WASAPI or ASIO)" );	//
			sw.WriteLine( "TimeStretch={0}", this.bTimeStretch ? 1 : 0 );					//
			sw.WriteLine();
			//sw.WriteLine( "; WASAPI/ASIO使用時に、MP3をストリーム再生するかどうか(0:ストリーム再生する, 1:しない)" );			//
			//sw.WriteLine( "; (mp3のシークがおかしくなる場合は、これを1にしてください) " );	//
			//sw.WriteLine( "; Set \"0\" if you'd like to use mp3 streaming playback on WASAPI/ASIO." );		//
			//sw.WriteLine( "; Set \"1\" not to use streaming playback for mp3." );			//
			//sw.WriteLine( "; (If you feel illegal seek with mp3, please set it to 1.)" );	//
			//sw.WriteLine( "NoMP3Streaming={0}", this.bNoMP3Streaming ? 1 : 0 );				//
			//sw.WriteLine();
			#region [ Adjust ]
			sw.WriteLine( "; 判定タイミング調整(ドラム, ギター, ベース)(-99～99)[ms]" );		// #23580 2011.1.3 yyagi
			sw.WriteLine("; Revision value to adjust judgement timing for the drums, guitar and bass.");	//
			sw.WriteLine("InputAdjustTimeDrums={0}", this.nInputAdjustTimeMs.Drums);		//
			sw.WriteLine("InputAdjustTimeGuitar={0}", this.nInputAdjustTimeMs.Guitar);		//
			sw.WriteLine("InputAdjustTimeBass={0}", this.nInputAdjustTimeMs.Bass);			//
			sw.WriteLine();

            sw.WriteLine( "; BGMタイミング調整(-99～99)[ms]" );                              // #36372 2016.06.19 kairera0467
            sw.WriteLine( "; Revision value to adjust judgement timing for BGM." );	        //
            sw.WriteLine( "BGMAdjustTime={0}", this.nCommonBGMAdjustMs );		            //
            sw.WriteLine();

			sw.WriteLine( "; 判定ラインの表示位置調整(ドラム, ギター, ベース)(-99～99)[px]" );	// #31602 2013.6.23 yyagi 判定ラインの表示位置オフセット
			sw.WriteLine( "; Offset value to adjust displaying judgement line for the drums, guitar and bass." );	//
			sw.WriteLine( "JudgeLinePosOffsetDrums={0}",  this.nJudgeLinePosOffset.Drums );		//
			sw.WriteLine( "JudgeLinePosOffsetGuitar={0}", this.nJudgeLinePosOffset.Guitar );	//
			sw.WriteLine( "JudgeLinePosOffsetBass={0}",   this.nJudgeLinePosOffset.Bass );		//
            
			sw.WriteLine( "; 判定ラインの表示位置(ギター, ベース)" );	// #33891 2014.6.26 yyagi
			sw.WriteLine( "; 0=Normal, 1=Lower" );
			sw.WriteLine( "; Position of the Judgement line and RGB button; Vseries compatible(1) or not(0)." );	//
			sw.WriteLine( "JudgeLinePosModeGuitar={0}", (int) this.e判定位置.Guitar );	//
			sw.WriteLine( "JudgeLinePosModeBass={0}  ", (int) this.e判定位置.Bass );	//
			
			sw.WriteLine();
			#endregion
			#region [ VelocityMin ]
			sw.WriteLine( "; LC, HH, SD,...の入力切り捨て下限Velocity値(0～127)" );			// #23857 2011.1.31 yyagi
			sw.WriteLine( "; Minimum velocity value for LC, HH, SD, ... to accept." );		//
			sw.WriteLine( "LCVelocityMin={0}", this.nVelocityMin.LC );						//
			sw.WriteLine("HHVelocityMin={0}", this.nVelocityMin.HH );						//
//			sw.WriteLine("; ハイハット以外の入力切り捨て下限Velocity値(0～127)");			// #23857 2010.12.12 yyagi
//			sw.WriteLine("; Minimum velocity value to accept. (except HiHat)");				//
//			sw.WriteLine("VelocityMin={0}", this.n切り捨て下限Velocity);					//
//			sw.WriteLine();																	//
			sw.WriteLine( "SDVelocityMin={0}", this.nVelocityMin.SD );						//
			sw.WriteLine( "BDVelocityMin={0}", this.nVelocityMin.BD );						//
			sw.WriteLine( "HTVelocityMin={0}", this.nVelocityMin.HT );						//
			sw.WriteLine( "LTVelocityMin={0}", this.nVelocityMin.LT );						//
			sw.WriteLine( "FTVelocityMin={0}", this.nVelocityMin.FT );						//
			sw.WriteLine( "CYVelocityMin={0}", this.nVelocityMin.CY );						//
			sw.WriteLine( "RDVelocityMin={0}", this.nVelocityMin.RD );						//
            sw.WriteLine( "LPVelocityMin={0}", this.nVelocityMin.LP );						//
			sw.WriteLine( "LBDVelocityMin={0}", this.nVelocityMin.LBD);						//
			sw.WriteLine();																	//
			#endregion

			sw.WriteLine( ";-------------------" );
			#endregion
			#region [ Log ]
			sw.WriteLine( "[Log]" );
			sw.WriteLine();
			sw.WriteLine( "; Log出力(0:OFF, 1:ON)" );
			sw.WriteLine( "OutputLog={0}", this.bログ出力 ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; 曲データ検索に関するLog出力(0:OFF, 1:ON)" );
			sw.WriteLine( "TraceSongSearch={0}", this.bLog曲検索ログ出力 ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; 画像やサウンドの作成・解放に関するLog出力(0:OFF, 1:ON)" );
			sw.WriteLine( "TraceCreatedDisposed={0}", this.bLog作成解放ログ出力 ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; DTX読み込み詳細に関するLog出力(0:OFF, 1:ON)" );
			sw.WriteLine( "TraceDTXDetails={0}", this.bLogDTX詳細ログ出力 ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( ";-------------------" );
			#endregion

			#region [ PlayOption ]
			sw.WriteLine( "[PlayOption]" );
			sw.WriteLine();
			sw.WriteLine( "; DARKモード(0:OFF, 1:HALF, 2:FULL)" );
			sw.WriteLine( "Dark={0}", (int) this.eDark );
			sw.WriteLine();
			#region [ SUDDEN ]
			sw.WriteLine( "; ドラムSUDDENモード(0:OFF, 1:ON)" );
			sw.WriteLine( "DrumsSudden={0}", this.bSudden.Drums ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ギターSUDDENモード(0:OFF, 1:ON)" );
			sw.WriteLine( "GuitarSudden={0}", this.bSudden.Guitar ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ベースSUDDENモード(0:OFF, 1:ON)" );
			sw.WriteLine( "BassSudden={0}", this.bSudden.Bass ? 1 : 0 );
			sw.WriteLine();
			#endregion
			#region [ HIDDEN ]
			sw.WriteLine( "; ドラムHIDDENモード(0:OFF, 1:ON)" );
			sw.WriteLine( "DrumsHidden={0}", this.bHidden.Drums ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ギターHIDDENモード(0:OFF, 1:ON)" );
			sw.WriteLine( "GuitarHidden={0}", this.bHidden.Guitar ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ベースHIDDENモード(0:OFF, 1:ON)" );
			sw.WriteLine( "BassHidden={0}", this.bHidden.Bass ? 1 : 0 );
			sw.WriteLine();
			#endregion
			#region [ Invisible ]
			sw.WriteLine( "; ドラムチップ非表示モード (0:OFF, 1=SEMI, 2:FULL)" );
			sw.WriteLine( "; Drums chip invisible mode" );
			sw.WriteLine( "DrumsInvisible={0}", (int) this.eInvisible.Drums );
			sw.WriteLine();
			sw.WriteLine( "; ギターチップ非表示モード (0:OFF, 1=SEMI, 2:FULL)" );
			sw.WriteLine( "; Guitar chip invisible mode" );
			sw.WriteLine( "GuitarInvisible={0}", (int) this.eInvisible.Guitar );
			sw.WriteLine();
			sw.WriteLine( "; ベースチップ非表示モード (0:OFF, 1=SEMI, 2:FULL)" );
			sw.WriteLine( "; Bbass chip invisible mode" );
			sw.WriteLine( "BassInvisible={0}", (int) this.eInvisible.Bass );
			sw.WriteLine();
			//sw.WriteLine( "; Semi-InvisibleでMissった時のチップ再表示時間(ms)" );
			//sw.WriteLine( "InvisibleDisplayTimeMs={0}", (int) this.nDisplayTimesMs );
			//sw.WriteLine();
			//sw.WriteLine( "; Semi-InvisibleでMissってチップ再表示時間終了後のフェードアウト時間(ms)" );
			//sw.WriteLine( "InvisibleFadeoutTimeMs={0}", (int) this.nFadeoutTimeMs );
			//sw.WriteLine();
			#endregion
			sw.WriteLine( "; ドラムREVERSEモード(0:OFF, 1:ON)" );
			sw.WriteLine( "DrumsReverse={0}", this.bReverse.Drums ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ギターREVERSEモード(0:OFF, 1:ON)" );
			sw.WriteLine( "GuitarReverse={0}", this.bReverse.Guitar ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ベースREVERSEモード(0:OFF, 1:ON)" );
			sw.WriteLine( "BassReverse={0}", this.bReverse.Bass ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ギターRANDOMモード(0:OFF, 1:Random, 2:SuperRandom, 3:HyperRandom)" );
			sw.WriteLine( "GuitarRandom={0}", (int) this.eRandom.Guitar );
			sw.WriteLine();
			sw.WriteLine( "; ベースRANDOMモード(0:OFF, 1:Random, 2:SuperRandom, 3:HyperRandom)" );
			sw.WriteLine( "BassRandom={0}", (int) this.eRandom.Bass );
			sw.WriteLine();
			sw.WriteLine( "; ギターLIGHTモード(0:OFF, 1:ON)" );
			sw.WriteLine( "GuitarLight={0}", this.bLight.Guitar ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ベースLIGHTモード(0:OFF, 1:ON)" );
			sw.WriteLine( "BassLight={0}", this.bLight.Bass ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ギターLEFTモード(0:OFF, 1:ON)" );
			sw.WriteLine( "GuitarLeft={0}", this.bLeft.Guitar ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ベースLEFTモード(0:OFF, 1:ON)" );
			sw.WriteLine( "BassLeft={0}", this.bLeft.Bass ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; RISKYモード(0:OFF, 1-10)" );									// #23559 2011.6.23 yyagi
			sw.WriteLine( "; RISKY mode. 0=OFF, 1-10 is the times of misses to be Failed." );	//
			sw.WriteLine( "Risky={0}", this.nRisky );			//
			sw.WriteLine();
			sw.WriteLine( "; TIGHTモード(0:OFF, 1:ON)" );									// #29500 2012.9.11 kairera0467
			sw.WriteLine( "; TIGHT mode. 0=OFF, 1=ON " );
			sw.WriteLine( "DrumsTight={0}", this.bTight ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; ドラム判定文字表示位置(0:表示OFF, 1:レーン上, 2:判定ライン上)" );
			sw.WriteLine( "; Drums Judgement display position (0:OFF, 1:on the lane, 2:over the judge line)" );
			sw.WriteLine( "DrumsPosition={0}", (int) this.判定文字表示位置.Drums );
			sw.WriteLine();
			sw.WriteLine( "; ギター判定文字表示位置(0:表示IFF, 1:レーン上, 2:判定ライン上, 3:コンボ下)" );
			sw.WriteLine( "; Guitar Judgement display position (0:OFF, 1:on the lane, 2:over the judge line, 3:under combo)" );
			sw.WriteLine( "GuitarPosition={0}", (int) this.判定文字表示位置.Guitar );
			sw.WriteLine();
			sw.WriteLine( "; ベース判定文字表示位置(0:表示OFF, 1:レーン上, 2:判定ライン上, 3:コンボ下)" );
			sw.WriteLine( "; Bass Judgement display position (0:OFF, 1:on the lane, 2:over the judge line, 3:under combo)" );
			sw.WriteLine( "BassPosition={0}", (int) this.判定文字表示位置.Bass );
			sw.WriteLine();
			sw.WriteLine( "; ドラム譜面スクロール速度(0:x0.5, 1:x1.0, 2:x1.5,…,1999:x1000.0)" );
			sw.WriteLine( "DrumsScrollSpeed={0}", this.n譜面スクロール速度.Drums );
			sw.WriteLine();
			sw.WriteLine( "; ギター譜面スクロール速度(0:x0.5, 1:x1.0, 2:x1.5,…,1999:x1000.0)" );
			sw.WriteLine( "GuitarScrollSpeed={0}", this.n譜面スクロール速度.Guitar );
			sw.WriteLine();
			sw.WriteLine( "; ベース譜面スクロール速度(0:x0.5, 1:x1.0, 2:x1.5,…,1999:x1000.0)" );
			sw.WriteLine( "BassScrollSpeed={0}", this.n譜面スクロール速度.Bass );
			sw.WriteLine();
			sw.WriteLine( "; 演奏速度(5～40)(→x5/20～x40/20)" );
			sw.WriteLine( "PlaySpeed={0}", this.n演奏速度 );
			sw.WriteLine();
			sw.WriteLine( "; ドラムCOMBO文字表示位置(0:左, 1:中, 2:右, 3:OFF)" );
			sw.WriteLine( "ComboPosition={0}", (int) this.ドラムコンボ文字の表示位置 );
			sw.WriteLine();
			//sw.WriteLine( "; 判定・コンボ表示優先度(0:チップの下, 1:チップの上)" );
			//sw.WriteLine( "JudgeDispPriorityDrums={0}" , (int) this.e判定表示優先度.Drums );
			//sw.WriteLine( "JudgeDispPriorityGuitar={0}", (int) this.e判定表示優先度.Guitar );
			//sw.WriteLine( "JudgeDispPriorityBass={0}"  , (int) this.e判定表示優先度.Bass );
			//sw.WriteLine();

            // #24074 2011.01.23 add ikanick
			sw.WriteLine( "; グラフ表示(0:OFF, 1:ON)" );
			sw.WriteLine( "DrumGraph={0}", this.bGraph.Drums ? 1 : 0 );
			sw.WriteLine( "GuitarGraph={0}", this.bGraph.Guitar ? 1 : 0 );
			sw.WriteLine( "BassGraph={0}", this.bGraph.Bass ? 1 : 0 );
			sw.WriteLine();

            // #35411 2015.8.18 chnmr0 add
            sw.WriteLine("; AUTOゴースト種別 (0:PERFECT, 1:LAST_PLAY, 2:HI_SKILL, 3:HI_SCORE, 4:ONLINE)" );
            sw.WriteLine("DrumAutoGhost={0}", (int)eAutoGhost.Drums);
            sw.WriteLine("GuitarAutoGhost={0}", (int)eAutoGhost.Guitar);
            sw.WriteLine("BassAutoGhost={0}", (int)eAutoGhost.Bass);
            sw.WriteLine();
            sw.WriteLine("; ターゲットゴースト種別 (0:NONE, 1:PERFECT, 2:LAST_PLAY, 3:HI_SKILL, 4:HI_SCORE, 5:ONLINE)");
            sw.WriteLine("DrumTargetGhost={0}", (int)eTargetGhost.Drums);
            sw.WriteLine("GuitarTargetGhost={0}", (int)eTargetGhost.Guitar);
            sw.WriteLine("BassTargetGhost={0}", (int)eTargetGhost.Bass);
            sw.WriteLine();

            #region[DTXManiaXG追加オプション]
            sw.WriteLine( "; 譜面仕様変更(0:デフォルト10レーン, 1:XG9レーン, 2:CLASSIC6レーン)" );
            sw.WriteLine( "NumOfLanes={0}", (int)this.eNumOfLanes.Drums );
            sw.WriteLine();
            sw.WriteLine( "; dkdk仕様変更(0:デフォルト, 1:始動足変更, 2:dkdk1レーン化)" );
            sw.WriteLine( "DkdkType={0}", (int)this.eDkdkType.Drums );
            sw.WriteLine();
            sw.WriteLine( "; バスをLBDに振り分け(0:OFF, 1:ON)" );
            sw.WriteLine( "AssignToLBD={0}", this.bAssignToLBD.Drums ? 1 : 0 );
            sw.WriteLine();
            sw.WriteLine( "; ドラムパッドRANDOMモード(0:OFF, 1:Mirror, 2:Random, 3:SuperRandom, 4:HyperRandom, 5:MasterRandom, 6:AnotherRandom)" );
            sw.WriteLine( "DrumsRandomPad={0}", (int)this.eRandom.Drums );
            sw.WriteLine();
            sw.WriteLine( "; ドラム足RANDOMモード(0:OFF, 1:Mirror, 2:Random, 3:SuperRandom, 4:HyperRandom, 5:MasterRandom, 6:AnotherRandom)" );
            sw.WriteLine( "DrumsRandomPedal={0}", (int)this.eRandomPedal.Drums );
            sw.WriteLine();
            //sw.WriteLine("; LP消音機能(0:OFF, 1:ON)");
            //sw.WriteLine("MutingLP={0}", this.bMutingLP ? 1 : 0);
            //sw.WriteLine();
            //sw.WriteLine("; オープンハイハットの表示画像(0:DTXMania仕様, 1:○なし, 2:クローズハットと同じ)");
            //sw.WriteLine("HHOGraphics={0}", (int)this.eHHOGraphics.Drums);
            //sw.WriteLine();
            //sw.WriteLine("; 左バスペダルの表示画像(0:バス寄り, 1:LPと同じ)");
            //sw.WriteLine("LBDGraphics={0}", (int)this.eLBDGraphics.Drums);
            //sw.WriteLine();
            sw.WriteLine( "; ライドシンバルレーンの表示位置(0:...RD RC, 1:...RC RD)" );
            sw.WriteLine( "RDPosition={0}", (int)this.eRDPosition );
            sw.WriteLine();
            #endregion
            #region[ DTXHD追加オプション ]
            //sw.WriteLine("; 判定ライン(0～100)" );
            //sw.WriteLine("DrumsJudgeLine={0}", (int)this.nJudgeLine.Drums);
            //sw.WriteLine("GuitarJudgeLine={0}", (int)this.nJudgeLine.Guitar);
            //sw.WriteLine("BassJudgeLine={0}", (int)this.nJudgeLine.Bass);
            //sw.WriteLine();
            #endregion
            #region[ ver.K追加オプション ]
            #region [ XGオプション ]
            sw.WriteLine( "; ネームプレートタイプ" );
            sw.WriteLine( "; 0:タイプA XG2風の表示がされます。" );
            sw.WriteLine( "; 1:タイプB XG風の表示がされます。このタイプでは7_NamePlate_XG.png、7_Difficlty_XG.pngが読み込まれます。" );
            sw.WriteLine( "NamePlateType={0}", (int)this.eNamePlateType );
            sw.WriteLine();
            //sw.WriteLine("; 動くドラムセット(0:ON, 1:OFF, 2:NONE)");
            //sw.WriteLine("DrumSetMoves={0}", (int)this.eドラムセットを動かす);
            //sw.WriteLine();
            //sw.WriteLine("; BPMバーの表示(0:表示する, 1:左のみ表示, 2:動くバーを表示しない, 3:表示しない)");
            //sw.WriteLine("BPMBar={0}", (int)this.eBPMbar); ;
            //sw.WriteLine();
            //sw.WriteLine("; LivePointの表示(0:OFF, 1:ON)");
            //sw.WriteLine("LivePoint={0}", this.bLivePoint ? 1 : 0);
            //sw.WriteLine();
            //sw.WriteLine("; スピーカーの表示(0:OFF, 1:ON)");
            //sw.WriteLine("Speaker={0}", this.bSpeaker ? 1 : 0);
            //sw.WriteLine();
            #endregion
            sw.WriteLine( "; XPerfect判定を有効にする(0:OFF, 1:ON)" );
            sw.WriteLine( "XPerfectJudgeMode={0}", this.bXPerfect判定を有効にする ? 1 : 0 );
            sw.WriteLine();
            sw.WriteLine( "; シャッターINSIDE(0～100)" );
            sw.WriteLine( "DrumsShutterIn={0}", (int)this.nShutterInSide.Drums );
            sw.WriteLine( "GuitarShutterIn={0}", (int)this.nShutterInSide.Guitar );
            sw.WriteLine( "BassShutterIn={0}", (int)this.nShutterInSide.Bass );
            sw.WriteLine();
            sw.WriteLine( "; シャッターOUTSIDE(0～100)" );
            sw.WriteLine( "DrumsShutterOut={0}", (int)this.nShutterOutSide.Drums );
            sw.WriteLine( "GuitarShutterOut={0}", (int)this.nShutterOutSide.Guitar );
            sw.WriteLine( "BassShutterOut={0}", (int)this.nShutterOutSide.Bass );
            sw.WriteLine();
            //sw.WriteLine( "; ボーナス演出の表示(0:表示しない, 1:表示する)");
            //sw.WriteLine("DrumsStageEffect={0}", this.ボーナス演出を表示する ? 1 : 0);
            //sw.WriteLine();
            sw.WriteLine( "; ドラムレーンタイプ(0:A, 1:B, 2:C, 3:D )" );
            sw.WriteLine( "DrumsLaneType={0}", (int)this.eLaneType );
            sw.WriteLine();
            sw.WriteLine( "; CLASSIC譜面判別" );
            sw.WriteLine( "CLASSIC={0}", this.bCLASSIC譜面判別を有効にする ? 1 : 0 );
            sw.WriteLine();
            sw.WriteLine( "; スキルモード(0:旧仕様, 1:XG仕様)" );
            sw.WriteLine( "SkillMode={0}", (int)this.eSkillMode );
            sw.WriteLine();
            sw.WriteLine( "; スキルモードの自動切換え(0:OFF, 1:ON)" );
            sw.WriteLine( "SwitchSkillMode={0}", this.bSkillModeを自動切替えする ? 1 : 0 );
            sw.WriteLine();
            //sw.WriteLine("; ドラム アタックエフェクトタイプ");
            //sw.WriteLine("; 0:ALL 粉と爆発エフェクトを表示します。");
            //sw.WriteLine("; 1:ChipOFF チップのエフェクトを消します。");
            //sw.WriteLine("; 2:EffectOnly 粉を消します。");
            //sw.WriteLine("; 3:ALLOFF すべて消します。");
            //sw.WriteLine("DrumsAttackEffect={0}", (int)this.eAttackEffect.Drums);
            //sw.WriteLine();
            //sw.WriteLine("; ギター / ベース アタックエフェクトタイプ (0:OFF, 1:ON)");
            //sw.WriteLine("GuitarAttackEffect={0}", (int)this.eAttackEffect.Guitar);
            //sw.WriteLine("BassAttackEffect={0}", (int)this.eAttackEffect.Bass);
            //sw.WriteLine();
            sw.WriteLine( "; レーン表示" );
            sw.WriteLine( "; 0:ALL ON レーン背景、小節線を表示します。" );
            sw.WriteLine( "; 1:LANE FF レーン背景を消します。" );
            sw.WriteLine( "; 2:LINE OFF 小節線を消します。" );
            sw.WriteLine( "; 3:ALL OFF すべて消します。" );
            sw.WriteLine( "DrumsLaneDisp={0}", (int)this.nLaneDispType.Drums );
            sw.WriteLine( "GuitarLaneDisp={0}", (int)this.nLaneDispType.Guitar );
            sw.WriteLine( "BassLaneDisp={0}", (int)this.nLaneDispType.Bass );
            sw.WriteLine();
            sw.WriteLine( "; 判定ライン表示" );
            sw.WriteLine( "DrumsJudgeLineDisp={0}", this.bJudgeLineDisp.Drums ? 1 : 0 );
            sw.WriteLine( "GuitarJudgeLineDisp={0}", this.bJudgeLineDisp.Guitar ? 1 : 0 );
            sw.WriteLine( "BassJudgeLineDisp={0}", this.bJudgeLineDisp.Bass ? 1 : 0 );
            sw.WriteLine();
            sw.WriteLine( "; レーンフラッシュ表示");
            sw.WriteLine( "DrumsLaneFlush={0}", this.bLaneFlush.Drums ? 1 : 0 );
            sw.WriteLine( "GuitarLaneFlush={0}", this.bLaneFlush.Guitar ? 1 : 0 );
            sw.WriteLine( "BassLaneFlush={0}", this.bLaneFlush.Bass ? 1 : 0 );
            sw.WriteLine();
            sw.WriteLine( "; 判定画像のアニメーション方式" );
            sw.WriteLine( ";(0:旧DTXMania方式 1:コマ方式 2:擬似XG方式)" );
            sw.WriteLine( "JudgeAnimeType={0}", (int)this.eJudgeAnimeType );
            sw.WriteLine();
            sw.WriteLine( "; 判定数の表示(0:表示しない, 1:表示する)");
            sw.WriteLine("JudgeCountDisp={0}", this.bJudgeCountDisp ? 1 : 0);
            sw.WriteLine();
            sw.WriteLine( "; JUST" );
            sw.WriteLine( "DrumsJust={0}", (int)this.eJUST.Drums );
            sw.WriteLine( "GuitarJust={0}", (int)this.eJUST.Guitar );
            sw.WriteLine( "BassJust={0}", (int)this.eJUST.Bass );
            sw.WriteLine();
            sw.WriteLine( "; シャッター画像" );
            sw.WriteLine( "ShutterImageDrums={0}", this.strShutterImageName.Drums );
            sw.WriteLine( "ShutterImageGuitar={0}", this.strShutterImageName.Guitar );
            sw.WriteLine( "ShutterImageBass={0}", this.strShutterImageName.Bass );
            sw.WriteLine();
            sw.WriteLine( "; フレームの表示(0:表示しない, 1:表示する)");
            sw.WriteLine( "MatixxFrameDisp={0}", this.bフレームを表示する ? 1 : 0);

            #endregion
			sw.WriteLine( ";-------------------" );
			#endregion

			#region [ ViewerOption ]
			sw.WriteLine( "[ViewerOption]" );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 ドラム譜面スクロール速度(0:x0.5, 1:x1.0, 2:x1.5,…,1999:x1000.0)" );
			sw.WriteLine( "; for viewer mode; Drums Scroll Speed" );
			sw.WriteLine( "ViewerDrumsScrollSpeed={0}", this.nViewerScrollSpeed.Drums );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 ギター譜面スクロール速度(0:x0.5, 1:x1.0, 2:x1.5,…,1999:x1000.0)");
			sw.WriteLine( "; for viewer mode; Guitar Scroll Speed" );
			sw.WriteLine( "ViewerGuitarScrollSpeed={0}", this.nViewerScrollSpeed.Guitar );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 ベース譜面スクロール速度(0:x0.5, 1:x1.0, 2:x1.5,…,1999:x1000.0)");
			sw.WriteLine( "; for viewer mode; Bass Scroll Speed" );
			sw.WriteLine( "ViewerBassScrollSpeed={0}", this.nViewerScrollSpeed.Bass );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 垂直帰線同期(0:OFF,1:ON)" );
			sw.WriteLine( "; for viewer mode; Use whether Vertical Sync or not." );
			sw.WriteLine( "ViewerVSyncWait={0}", this.bViewerVSyncWait ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 演奏情報を表示する (0:OFF, 1:ON) ");
			sw.WriteLine( "; for viewer mode;" );
			sw.WriteLine( "; Showing playing info on the playing screen. (0:OFF, 1:ON) " );
			sw.WriteLine( "ViewerShowDebugStatus={0}", this.bViewerShowDebugStatus? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 再生速度変更を、ピッチ変更で行うかどうか(0:ピッチ変更, 1:タイムストレッチ ");
			sw.WriteLine( "; (WASAPI/ASIO使用時のみ有効)  ");
			sw.WriteLine( "; for viewer mode;" );
			sw.WriteLine( "; Set \"0\" if you'd like to use pitch shift with PlaySpeed. " );
			sw.WriteLine( "; Set \"1\" for time stretch. " );
			sw.WriteLine( "; (Only available when you're using using WASAPI or ASIO) ");
			sw.WriteLine( "ViewerTimeStretch={0}", this.bViewerTimeStretch? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 ギター/ベース有効(0:OFF,1:ON) ");
			sw.WriteLine( "; for viewer mode;" );
			sw.WriteLine( "; Enable Guitar/Bass or not.(0:OFF,1:ON) " );
			sw.WriteLine( "ViewerGuitar={0}", this.bViewerGuitar有効? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード時専用 ドラム有効(0:OFF,1:ON) ");
			sw.WriteLine( "; for viewer mode;" );
			sw.WriteLine( "; Enable Drums or not.(0:OFF,1:ON) " );
			sw.WriteLine( "ViewerDrums={0}", this.bViewerDrums有効? 1 : 0 );
			sw.WriteLine();

			sw.WriteLine( "; Viewerモード専用 ウインドウモード時の画面幅" );
			sw.WriteLine( "; A width size in the window mode, for viewer mode." );
			sw.WriteLine( "ViewerWindowWidth={0}", this.nViewerウインドウwidth );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード専用 ウインドウモード時の画面高さ" );
			sw.WriteLine( "; A height size in the window mode, for viewer mode." );
			sw.WriteLine( "ViewerWindowHeight={0}", this.nViewerウインドウheight );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード専用 ウィンドウモード時の位置X" );
			sw.WriteLine( "; X position in the window mode, for viewer mode." );
			sw.WriteLine( "ViewerWindowX={0}", this.nViewer初期ウィンドウ開始位置X );
			sw.WriteLine();
			sw.WriteLine( "; Viewerモード専用 ウィンドウモード時の位置Y" );
			sw.WriteLine( "; Y position in the window mode, for viewer mode." );
			sw.WriteLine( "ViewerWindowY={0}", this.nViewer初期ウィンドウ開始位置Y );
			sw.WriteLine();
			sw.WriteLine( ";-------------------" );
			#endregion

			#region [ AutoPlay ]
			sw.WriteLine( "[AutoPlay]" );
			sw.WriteLine();
			sw.WriteLine( "; 自動演奏(0:OFF, 1:ON)" );
			sw.WriteLine();
			sw.WriteLine( "; Drums" );
			sw.WriteLine( "LC={0}", this.bAutoPlay.LC ? 1 : 0 );
			sw.WriteLine( "HH={0}", this.bAutoPlay.HH ? 1 : 0 );
			sw.WriteLine( "SD={0}", this.bAutoPlay.SD ? 1 : 0 );
			sw.WriteLine( "BD={0}", this.bAutoPlay.BD ? 1 : 0 );
			sw.WriteLine( "HT={0}", this.bAutoPlay.HT ? 1 : 0 );
			sw.WriteLine( "LT={0}", this.bAutoPlay.LT ? 1 : 0 );
			sw.WriteLine( "FT={0}", this.bAutoPlay.FT ? 1 : 0 );
			sw.WriteLine( "CY={0}", this.bAutoPlay.CY ? 1 : 0 );
			sw.WriteLine( "LP={0}", this.bAutoPlay.LP ? 1 : 0 );
			sw.WriteLine( "LBD={0}", this.bAutoPlay.LBD ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; Guitar" );
			//sw.WriteLine( "Guitar={0}", this.bAutoPlay.Guitar ? 1 : 0 );
			sw.WriteLine( "GuitarR={0}", this.bAutoPlay.GtR ? 1 : 0 );
			sw.WriteLine( "GuitarG={0}", this.bAutoPlay.GtG ? 1 : 0 );
			sw.WriteLine( "GuitarB={0}", this.bAutoPlay.GtB ? 1 : 0 );
			sw.WriteLine( "GuitarPick={0}", this.bAutoPlay.GtPick ? 1 : 0 );
			sw.WriteLine( "GuitarWailing={0}", this.bAutoPlay.GtW ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( "; Bass" );
			// sw.WriteLine( "Bass={0}", this.bAutoPlay.Bass ? 1 : 0 );
			sw.WriteLine( "BassR={0}", this.bAutoPlay.BsR ? 1 : 0 );
			sw.WriteLine( "BassG={0}", this.bAutoPlay.BsG ? 1 : 0 );
			sw.WriteLine( "BassB={0}", this.bAutoPlay.BsB ? 1 : 0 );
			sw.WriteLine( "BassPick={0}", this.bAutoPlay.BsPick ? 1 : 0 );
			sw.WriteLine( "BassWailing={0}", this.bAutoPlay.BsW ? 1 : 0 );
			sw.WriteLine();
			sw.WriteLine( ";-------------------" );
			#endregion

			#region [ HitRange ]
			sw.WriteLine( "[HitRange]" );
			sw.WriteLine();
			sw.WriteLine( "; Perfect～Poor とみなされる範囲[ms]" );
			sw.WriteLine( "Perfect={0}", this.nヒット範囲ms.Perfect );
			sw.WriteLine( "Great={0}", this.nヒット範囲ms.Great );
			sw.WriteLine( "Good={0}", this.nヒット範囲ms.Good );
			sw.WriteLine( "Poor={0}", this.nヒット範囲ms.Poor );
			sw.WriteLine();
            sw.WriteLine( "; ペダルレーンの判定補正値[ms]" );
            sw.WriteLine( "; 各判定のヒット範囲に加算されます。0～200msで指定できます。(暫定仕様)" );
            sw.WriteLine( "PedalJudgeRangeDelta={0}", this.nPedalJudgeRangeDelta );
			sw.WriteLine( ";-------------------" );
			#endregion
			#region [ GUID ]
			sw.WriteLine( "[GUID]" );
			sw.WriteLine();
			foreach( KeyValuePair<int, string> pair in this.dicJoystick )
			{
				sw.WriteLine( "JoystickID={0},{1}", pair.Key, pair.Value );
			}
			#endregion
			#region [ DrumsKeyAssign ]
			sw.WriteLine();
			sw.WriteLine( ";-------------------" );
			sw.WriteLine( "; キーアサイン" );
			sw.WriteLine( ";   項　目：Keyboard → 'K'＋'0'＋キーコード(10進数)" );
			sw.WriteLine( ";           Mouse    → 'N'＋'0'＋ボタン番号(0～7)" );
			sw.WriteLine( ";           MIDI In  → 'M'＋デバイス番号1桁(0～9,A～Z)＋ノート番号(10進数)" );
			sw.WriteLine( ";           Joystick → 'J'＋デバイス番号1桁(0～9,A～Z)＋ 0 ...... Ｘ減少(左)ボタン" );
			sw.WriteLine( ";                                                         1 ...... Ｘ増加(右)ボタン" );
			sw.WriteLine( ";                                                         2 ...... Ｙ減少(上)ボタン" );
			sw.WriteLine( ";                                                         3 ...... Ｙ増加(下)ボタン" );
			sw.WriteLine( ";                                                         4 ...... Ｚ減少(前)ボタン" );
			sw.WriteLine( ";                                                         5 ...... Ｚ増加(後)ボタン" );
			sw.WriteLine( ";                                                         6～133.. ボタン1～128" );
			sw.WriteLine( ";           これらの項目を 16 個まで指定可能(',' で区切って記述）。" );
			sw.WriteLine( ";" );
			sw.WriteLine( ";   表記例：HH=K044,M042,J16" );
			sw.WriteLine( ";           → HiHat を Keyboard の 44 ('Z'), MidiIn#0 の 42, JoyPad#1 の 6(ボタン1) に割当て" );
			sw.WriteLine( ";" );
			sw.WriteLine( ";   ※Joystick のデバイス番号とデバイスとの関係は [GUID] セクションに記してあるものが有効。" );
			sw.WriteLine( ";" );
			sw.WriteLine();
			sw.WriteLine( "[DrumsKeyAssign]" );
			sw.WriteLine();
			sw.Write( "HH=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.HH );
			sw.WriteLine();
			sw.Write( "SD=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.SD );
			sw.WriteLine();
			sw.Write( "BD=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.BD );
			sw.WriteLine();
			sw.Write( "HT=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.HT );
			sw.WriteLine();
			sw.Write( "LT=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.LT );
			sw.WriteLine();
			sw.Write( "FT=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.FT );
			sw.WriteLine();
			sw.Write( "CY=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.CY );
			sw.WriteLine();
			sw.Write( "HO=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.HHO );
			sw.WriteLine();
			sw.Write( "RD=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.RD );
			sw.WriteLine();
			sw.Write( "LC=" );
			this.tキーの書き出し( sw, this.KeyAssign.Drums.LC );
			sw.WriteLine();
			sw.Write( "LP=" );										// #27029 2012.1.4 from
			this.tキーの書き出し( sw, this.KeyAssign.Drums.LP );	//
			sw.WriteLine();											//
			sw.Write( "LBD=" );										// 2016.02.21 kairera0467
			this.tキーの書き出し( sw, this.KeyAssign.Drums.LBD );	//
			sw.WriteLine();											//
			sw.WriteLine();
			#endregion
			#region [ GuitarKeyAssign ]
			sw.WriteLine( "[GuitarKeyAssign]" );
			sw.WriteLine();
			sw.Write( "R=" );
			this.tキーの書き出し( sw, this.KeyAssign.Guitar.R );
			sw.WriteLine();
			sw.Write( "G=" );
			this.tキーの書き出し( sw, this.KeyAssign.Guitar.G );
			sw.WriteLine();
			sw.Write( "B=" );
			this.tキーの書き出し( sw, this.KeyAssign.Guitar.B );
			sw.WriteLine();
			sw.Write( "Pick=" );
			this.tキーの書き出し( sw, this.KeyAssign.Guitar.Pick );
			sw.WriteLine();
			sw.Write( "Wail=" );
			this.tキーの書き出し( sw, this.KeyAssign.Guitar.Wail );
			sw.WriteLine();
			sw.Write( "Decide=" );
			this.tキーの書き出し( sw, this.KeyAssign.Guitar.Decide );
			sw.WriteLine();
			sw.Write( "Cancel=" );
			this.tキーの書き出し( sw, this.KeyAssign.Guitar.Cancel );
			sw.WriteLine();
			sw.WriteLine();
			#endregion
			#region [ BassKeyAssign ]
			sw.WriteLine( "[BassKeyAssign]" );
			sw.WriteLine();
			sw.Write( "R=" );
			this.tキーの書き出し( sw, this.KeyAssign.Bass.R );
			sw.WriteLine();
			sw.Write( "G=" );
			this.tキーの書き出し( sw, this.KeyAssign.Bass.G );
			sw.WriteLine();
			sw.Write( "B=" );
			this.tキーの書き出し( sw, this.KeyAssign.Bass.B );
			sw.WriteLine();
			sw.Write( "Pick=" );
			this.tキーの書き出し( sw, this.KeyAssign.Bass.Pick );
			sw.WriteLine();
			sw.Write( "Wail=" );
			this.tキーの書き出し( sw, this.KeyAssign.Bass.Wail );
			sw.WriteLine();
			sw.Write( "Decide=" );
			this.tキーの書き出し( sw, this.KeyAssign.Bass.Decide );
			sw.WriteLine();
			sw.Write( "Cancel=" );
			this.tキーの書き出し( sw, this.KeyAssign.Bass.Cancel );
			sw.WriteLine();
			sw.WriteLine();
			#endregion
			#region [ SystemkeyAssign ]
			sw.WriteLine( "[SystemKeyAssign]" );
			sw.WriteLine();
			sw.Write( "Capture=" );
			this.tキーの書き出し( sw, this.KeyAssign.System.Capture );
			sw.WriteLine();
			sw.WriteLine();
			#endregion

			#region [ Temp ]	- 2012.1.5 from add
			sw.WriteLine( "[Temp]" );
			sw.WriteLine();
			if( ( this.eBDGroup == EBDGroup.どっちもBD ) && ( this.BackupOf1BD != null ) )		// #27029 2012.1.5 from: 有効な場合にのみ出力する。
			{
				sw.WriteLine( "; BDGroup が FP|BD→FP&BD に変わった時の LC/HHC/HHO 打ち分けモード(0:LC|HHC|HHO, 1:LC&(HHC|HHO), 2:LC&HHC&HHO)" );
				sw.WriteLine( "BackupOf1BD_HHGroup={0}", (int) this.BackupOf1BD.eHHGroup );
				sw.WriteLine();
				sw.WriteLine( ";BDGropu が FP|BD→FP&BD に変わった時の打ち分け時の再生音の優先順位(HHGroup)(0:Chip>Pad, 1:Pad>Chip)" );
				sw.WriteLine( "BackupOf1BD_HitSoundPriorityHH={0}", (int) this.BackupOf1BD.eHitSoundPriorityHH );
				sw.WriteLine();
			}
			sw.WriteLine( ";-------------------" );
			#endregion
			
			sw.Close();
		}
		public void tファイルから読み込み( string iniファイル名 )
		{
			this.ConfigIniファイル名 = iniファイル名;
			this.bConfigIniが存在している = File.Exists( this.ConfigIniファイル名 );
			if( this.bConfigIniが存在している )
			{
				string str;
				this.tキーアサインを全部クリアする();
				using ( StreamReader reader = new StreamReader( this.ConfigIniファイル名, Encoding.GetEncoding( "Shift_JIS" ) ) )
				{
					str = reader.ReadToEnd();
				}
				t文字列から読み込み( str );
                
                //2016.03.29 kairera0467 ギター・ベース両方ともグラフが有効になっている場合は両方とも無効にする。
                if( this.bGraph.Guitar && this.bGraph.Bass )
                    this.bGraph.Guitar = false; this.bGraph.Bass = false;
			}
		}

		private void t文字列から読み込み( string strAllSettings )	// 2011.4.13 yyagi; refactored to make initial KeyConfig easier.
		{
			Eセクション種別 unknown = Eセクション種別.Unknown;
			string[] delimiter = { "\n" };
			string[] strSingleLine = strAllSettings.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );
			foreach ( string s in strSingleLine )
			{
				string str = s.Replace( '\t', ' ' ).TrimStart( new char[] { '\t', ' ' } );
				if ( ( str.Length != 0 ) && ( str[ 0 ] != ';' ) )
				{
					try
					{
						string str3;
						string str4;
						if ( str[ 0 ] == '[' )
						{
							#region [ セクションの変更 ]
							//-----------------------------
							StringBuilder builder = new StringBuilder( 0x20 );
							int num = 1;
							while ( ( num < str.Length ) && ( str[ num ] != ']' ) )
							{
								builder.Append( str[ num++ ] );
							}
							string str2 = builder.ToString();
							if ( str2.Equals( "System" ) )
							{
								unknown = Eセクション種別.System;
							}
							else if ( str2.Equals( "Log" ) )
							{
								unknown = Eセクション種別.Log;
							}
							else if ( str2.Equals( "PlayOption" ) )
							{
								unknown = Eセクション種別.PlayOption;
							}
							else if ( str2.Equals( "ViewerOption" ) )
							{
								unknown = Eセクション種別.ViewerOption;
							}
							else if ( str2.Equals( "AutoPlay" ) )
							{
								unknown = Eセクション種別.AutoPlay;
							}
							else if ( str2.Equals( "HitRange" ) )
							{
								unknown = Eセクション種別.HitRange;
							}
							else if ( str2.Equals( "GUID" ) )
							{
								unknown = Eセクション種別.GUID;
							}
							else if ( str2.Equals( "DrumsKeyAssign" ) )
							{
								unknown = Eセクション種別.DrumsKeyAssign;
							}
							else if ( str2.Equals( "GuitarKeyAssign" ) )
							{
								unknown = Eセクション種別.GuitarKeyAssign;
							}
							else if ( str2.Equals( "BassKeyAssign" ) )
							{
								unknown = Eセクション種別.BassKeyAssign;
							}
							else if ( str2.Equals( "SystemKeyAssign" ) )
							{
								unknown = Eセクション種別.SystemKeyAssign;
							}
							else if( str2.Equals( "Temp" ) )
							{
								unknown = Eセクション種別.Temp;
							}
							else
							{
								unknown = Eセクション種別.Unknown;
							}
							//-----------------------------
							#endregion
						}
						else
						{
							string[] strArray = str.Split( new char[] { '=' } );
							if( strArray.Length == 2 )
							{
								str3 = strArray[ 0 ].Trim();
								str4 = strArray[ 1 ].Trim();
								switch( unknown )
								{
									#region [ [System] ]
									//-----------------------------
									case Eセクション種別.System:
										{
#if false		// #23625 2011.1.11 Config.iniからダメージ/回復値の定数変更を行う場合はここを有効にする 087リリースに合わせ機能無効化
										//----------------------------------------
												if (str3.Equals("GaugeFactorD"))
												{
													int p = 0;
													string[] splittedFactor = str4.Split(',');
													foreach (string s in splittedFactor) {
														this.fGaugeFactor[p++, 0] = Convert.ToSingle(s);
													}
												} else
												if (str3.Equals("GaugeFactorG"))
												{
													int p = 0;
													string[] splittedFactor = str4.Split(',');
													foreach (string s in splittedFactor)
													{
														this.fGaugeFactor[p++, 1] = Convert.ToSingle(s);
													}
												}
												else
												if (str3.Equals("DamageFactor"))
												{
													int p = 0;
													string[] splittedFactor = str4.Split(',');
													foreach (string s in splittedFactor)
													{
														this.fDamageLevelFactor[p++] = Convert.ToSingle(s);
													}
												}
												else
										//----------------------------------------
#endif
											#region [ Version ]
											if ( str3.Equals( "Version" ) )
											{
												this.strDTXManiaのバージョン = str4;
											}
											#endregion
											#region [ DTXPath ]
											else if( str3.Equals( "DTXPath" ) )
											{
												this.str曲データ検索パス = str4;
											}
											#endregion
                                            #region[ プレイヤーデータ ]
                                            else if( str3.Equals( "CardNameDrums" ) )
                                            {
                                                this.strCardName[ 0 ] = str4;
                                            }
                                            else if( str3.Equals( "CardNameGuitar" ) )
                                            {
                                                this.strCardName[ 1 ] = str4;
                                            }
                                            else if( str3.Equals( "CardNameBass" ) )
                                            {
                                                this.strCardName[ 2 ] = str4;
                                            }
                                            else if( str3.Equals( "GroupNameDrums" ) )
                                            {
                                                this.strGroupName[ 0 ] = str4;
                                            }
                                            else if( str3.Equals( "GroupNameGuitar" ) )
                                            {
                                                this.strGroupName[ 1 ] = str4;
                                            }
                                            else if( str3.Equals( "GroupNameBass" ) )
                                            {
                                                this.strGroupName[ 2 ] = str4;
                                            }
                                            else if( str3.Equals( "NameColorDrums" ) )
                                            {
                                                this.nNameColor[ 0 ] = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 19, 0 );
                                            }
                                            else if( str3.Equals( "NameColorGuitar" ) )
                                            {
                                                this.nNameColor[ 1 ] = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 19, 0 );
                                            }
                                            else if( str3.Equals( "NameColorBass" ) )
                                            {
                                                this.nNameColor[ 2 ] = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 19, 0 );
                                            }
                                            #endregion
                                            #region [ skin関係 ]
                                            else if ( str3.Equals( "SkinPath" ) )
											{
												string absSkinPath = str4;
												if ( !System.IO.Path.IsPathRooted( str4 ) )
												{
													absSkinPath = System.IO.Path.Combine( CDTXMania.strEXEのあるフォルダ, "System" );
													absSkinPath = System.IO.Path.Combine( absSkinPath, str4 );
													Uri u = new Uri( absSkinPath );
													absSkinPath = u.AbsolutePath.ToString();	// str4内に相対パスがある場合に備える
													absSkinPath = System.Web.HttpUtility.UrlDecode( absSkinPath );						// デコードする
													absSkinPath = absSkinPath.Replace( '/', System.IO.Path.DirectorySeparatorChar );	// 区切り文字が\ではなく/なので置換する
												}
												if ( absSkinPath[ absSkinPath.Length - 1 ] != System.IO.Path.DirectorySeparatorChar )	// フォルダ名末尾に\を必ずつけて、CSkin側と表記を統一する
												{
													absSkinPath += System.IO.Path.DirectorySeparatorChar;
												}
												this.strSystemSkinSubfolderFullName = absSkinPath;
											}
											else if ( str3.Equals( "SkinChangeByBoxDef" ) )
											{
												this.bUseBoxDefSkin = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											#region [ Window関係 ]
											else if ( str3.Equals( "FullScreen" ) )
											{
												this.b全画面モード = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "WindowX" ) )		// #30675 2013.02.04 ikanick add
											{
												this.n初期ウィンドウ開始位置X = C変換.n値を文字列から取得して範囲内に丸めて返す(
                                                    str4, 0,  System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 1 , this.n初期ウィンドウ開始位置X );
											}
											else if ( str3.Equals( "WindowY" ) )		// #30675 2013.02.04 ikanick add
											{
												this.n初期ウィンドウ開始位置Y = C変換.n値を文字列から取得して範囲内に丸めて返す(
                                                    str4, 0,  System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 1 , this.n初期ウィンドウ開始位置Y );
											}
											else if ( str3.Equals( "WindowWidth" ) )		// #23510 2010.10.31 yyagi add
											{
												this.nウインドウwidth = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 1, 65535, this.nウインドウwidth );
												if( this.nウインドウwidth <= 0 )
												{
													this.nウインドウwidth = SampleFramework.GameWindowSize.Width;
												}
											}
											else if( str3.Equals( "WindowHeight" ) )		// #23510 2010.10.31 yyagi add
											{
												this.nウインドウheight = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 1, 65535, this.nウインドウheight );
												if( this.nウインドウheight <= 0 )
												{
													this.nウインドウheight = SampleFramework.GameWindowSize.Height;
												}
											}
											else if ( str3.Equals( "DoubleClickFullScreen" ) )	// #26752 2011.11.27 yyagi
											{
												this.bIsAllowedDoubleClickFullscreen = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "EnableSystemMenu" ) )		// #28200 2012.5.1 yyagi
											{
												this.bIsEnabledSystemMenu = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "BackSleep" ) )				// #23568 2010.11.04 ikanick add
											{
												this.n非フォーカス時スリープms = C変換.n値を文字列から取得して範囲内にちゃんと丸めて返す( str4, 0, 50, this.n非フォーカス時スリープms );
											}
											#endregion
											#region [ WASAPI/ASIO関係 ]
											else if ( str3.Equals( "SoundDeviceType" ) )
											{
												this.nSoundDeviceType = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, this.nSoundDeviceType );
											}
											else if ( str3.Equals( "WASAPIBufferSizeMs" ) )
											{
												this.nWASAPIBufferSizeMs = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 9999, this.nWASAPIBufferSizeMs );
											}
											else if ( str3.Equals( "ASIODevice" ) )
											{
												string[] asiodev = CEnumerateAllAsioDevices.GetAllASIODevices();
												this.nASIODevice = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, asiodev.Length - 1, this.nASIODevice );
											}
											//else if ( str3.Equals( "ASIOBufferSizeMs" ) )
											//{
											//    this.nASIOBufferSizeMs = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 9999, this.nASIOBufferSizeMs );
											//}
											//else if ( str3.Equals( "DynamicBassMixerManagement" ) )
											//{
											//    this.bDynamicBassMixerManagement = C変換.bONorOFF( str4[ 0 ] );
											//}
											else if ( str3.Equals( "SoundTimerType" ) )			// #33689 2014.6.6 yyagi
											{
												this.bUseOSTimer = C変換.bONorOFF( str4[ 0 ] );
											}
                                            else if ( str3.Equals( "EventDrivenWASAPI" ) )
                                            {
                                                this.bEventDrivenWASAPI = C変換.bONorOFF( str4[ 0 ] );
                                            }
											else if ( str3.Equals( "MasterVolume" ) )
											{
											    this.nMasterVolume = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 100, this.nMasterVolume );
											}
											#endregion
											else if ( str3.Equals( "VSyncWait" ) )
											{
												this.b垂直帰線待ちを行う = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "SleepTimePerFrame" ) )		// #23568 2011.11.27 yyagi
											{
												this.nフレーム毎スリープms = C変換.n値を文字列から取得して範囲内にちゃんと丸めて返す( str4, -1, 50, this.nフレーム毎スリープms );
											}
											#region [ ギター/ベース/ドラム 有効/無効 ]
											else if( str3.Equals( "Guitar" ) )
											{
												this.bGuitar有効 = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "Drums" ) )
											{
												this.bDrums有効 = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											else if( str3.Equals( "BGAlpha" ) )
											{
												this.n背景の透過度 = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0xff, this.n背景の透過度 );
											}
											else if( str3.Equals( "DamageLevel" ) )
											{
												this.eダメージレベル = (Eダメージレベル) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eダメージレベル );
											}
											else if ( str3.Equals( "StageFailed" ) )
											{
												this.bSTAGEFAILED有効 = C変換.bONorOFF( str4[ 0 ] );
											}
											#region [ 打ち分け関連 ]
											else if( str3.Equals( "HHGroup" ) )
											{
												this.eHHGroup = (EHHGroup) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.eHHGroup );
											}
											else if( str3.Equals( "FTGroup" ) )
											{
												this.eFTGroup = (EFTGroup) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eFTGroup );
											}
											else if( str3.Equals( "CYGroup" ) )
											{
												this.eCYGroup = (ECYGroup) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eCYGroup );
											}
											else if( str3.Equals( "BDGroup" ) )		// #27029 2012.1.4 from
											{
												this.eBDGroup = (EBDGroup) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eBDGroup );
											}
											else if( str3.Equals( "HitSoundPriorityHH" ) )
											{
												this.eHitSoundPriorityHH = (E打ち分け時の再生の優先順位) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.eHitSoundPriorityHH );
											}
											else if( str3.Equals( "HitSoundPriorityFT" ) )
											{
												this.eHitSoundPriorityFT = (E打ち分け時の再生の優先順位) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.eHitSoundPriorityFT );
											}
											else if( str3.Equals( "HitSoundPriorityCY" ) )
											{
												this.eHitSoundPriorityCY = (E打ち分け時の再生の優先順位) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.eHitSoundPriorityCY );
											}
											else if ( str3.Equals( "CymbalFree" ) )
											{
												this.bシンバルフリー = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											#region [ AVI/BGA ]
											else if( str3.Equals( "AVI" ) )
											{
												this.bAVI有効 = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "ForceAVIFullscreen" ) )
											{
												this.bForceAVIFullscreen = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "BGA" ) )
											{
												this.bBGA有効 = C変換.bONorOFF( str4[ 0 ] );
											}
                                            //else if( str3.Equals( "MovieClipMode" ) )
                                            //{
                                            //    this.eMovieClipMode = (EMovieClipMode) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.eMovieClipMode );
                                            //}
											else if ( str3.Equals( "WindowClipDisp" ) )
											{
												this.bWindowClipMode = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											#region [ フィルイン関係 ]
											else if ( str3.Equals( "FillInEffect" ) )
											{
												this.bフィルイン有効 = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "AudienceSound" ) )
											{
												this.b歓声を発声する = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											#region [ プレビュー音 ]
											else if( str3.Equals( "PreviewSoundWait" ) )
											{
												this.n曲が選択されてからプレビュー音が鳴るまでのウェイトms = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x5f5e0ff, this.n曲が選択されてからプレビュー音が鳴るまでのウェイトms );
											}
                                            //else if( str3.Equals( "PreviewImageWait" ) )
                                            //{
                                            //    this.n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x5f5e0ff, this.n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms );
                                            //}
											#endregion
											else if( str3.Equals( "AdjustWaves" ) )
											{
												this.bWave再生位置自動調整機能有効 = C変換.bONorOFF( str4[ 0 ] );
											}
											#region [ BGM/ドラムのヒット音 ]
											else if( str3.Equals( "BGMSound" ) )
											{
												this.bBGM音を発声する = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "HitSound" ) )
											{
												this.bドラム打音を発声する = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											else if( str3.Equals( "SaveScoreIni" ) )
											{
												this.bScoreIniを出力する = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "RandomFromSubBox" ) )
											{
												this.bランダムセレクトで子BOXを検索対象とする = C変換.bONorOFF( str4[ 0 ] );
											}
											#region [ SoundMonitor ]
											else if( str3.Equals( "SoundMonitorDrums" ) )
											{
												this.b演奏音を強調する.Drums = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "SoundMonitorGuitar" ) )
											{
												this.b演奏音を強調する.Guitar = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "SoundMonitorBass" ) )
											{
												this.b演奏音を強調する.Bass = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											#region [ コンボ数 ]
											else if( str3.Equals( "MinComboDrums" ) )
											{
												this.n表示可能な最小コンボ数.Drums = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 1, 0x1869f, this.n表示可能な最小コンボ数.Drums );
											}
											else if( str3.Equals( "MinComboGuitar" ) )
											{
												this.n表示可能な最小コンボ数.Guitar = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 1, 0x1869f, this.n表示可能な最小コンボ数.Guitar );
											}
											else if( str3.Equals( "MinComboBass" ) )
											{
												this.n表示可能な最小コンボ数.Bass = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 1, 0x1869f, this.n表示可能な最小コンボ数.Bass );
											}
											#endregion
											else if( str3.Equals( "ShowDebugStatus" ) )
											{
												this.b演奏情報を表示する = C変換.bONorOFF( str4[ 0 ] );
											}
											#region [ 選曲リストフォント ]
											else if( str3.Equals( "SelectListFontName" ) )
											{
												this.str選曲リストフォント = str4;
											}
											else if( str3.Equals( "SelectListFontSize" ) )
											{
												this.n選曲リストフォントのサイズdot = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 1, 0x3e7, this.n選曲リストフォントのサイズdot );
											}
											else if( str3.Equals( "SelectListFontItalic" ) )
											{
												this.b選曲リストフォントを斜体にする = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "SelectListFontBold" ) )
											{
												this.b選曲リストフォントを太字にする = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											else if( str3.Equals( "ChipVolume" ) )
											{
												this.n手動再生音量 = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 100, this.n手動再生音量 );
											}
											else if( str3.Equals( "AutoChipVolume" ) )
											{
												this.n自動再生音量 = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 100, this.n自動再生音量 );
											}
											else if( str3.Equals( "StoicMode" ) )
											{
												this.bストイックモード = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "ShowLagTime" ) )				// #25370 2011.6.3 yyagi
											{
												this.nShowLagType = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, this.nShowLagType );
											}
											else if ( str3.Equals( "JudgeDispPriority" ) )
											{
												this.e判定表示優先度 = (E判定表示優先度) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.e判定表示優先度 );
											}
											else if ( str3.Equals( "DrumsLanePosition" ) )
											{
											    this.eドラムレーン表示位置 = (Eドラムレーン表示位置) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.eドラムレーン表示位置 );
											}
											else if ( str3.Equals( "AutoResultCapture" ) )			// #25399 2011.6.9 yyagi
											{
												this.bIsAutoResultCapture = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "TimeStretch" ) )				// #23664 2013.2.24 yyagi
											{
												this.bTimeStretch = C変換.bONorOFF( str4[ 0 ] );
											}
											#region [ AdjustTime ]
											else if ( str3.Equals( "InputAdjustTimeDrums" ) )		// #23580 2011.1.3 yyagi
											{
												this.nInputAdjustTimeMs.Drums = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -99, 99, this.nInputAdjustTimeMs.Drums );
											}
											else if ( str3.Equals( "InputAdjustTimeGuitar" ) )	// #23580 2011.1.3 yyagi
											{
												this.nInputAdjustTimeMs.Guitar = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -99, 99, this.nInputAdjustTimeMs.Guitar );
											}
											else if ( str3.Equals( "InputAdjustTimeBass" ) )		// #23580 2011.1.3 yyagi
											{
												this.nInputAdjustTimeMs.Bass = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -99, 99, this.nInputAdjustTimeMs.Bass );
											}
                                            else if ( str3.Equals( "BGMAdjustTime" ) )              // #36372 2016.06.19 kairera0467
                                            {
                                                this.nCommonBGMAdjustMs = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -99, 99, this.nCommonBGMAdjustMs );
                                            }
											else if ( str3.Equals( "JudgeLinePosOffsetDrums" ) )		// #31602 2013.6.23 yyagi
											{
												this.nJudgeLinePosOffset.Drums = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -99, 99, this.nJudgeLinePosOffset.Drums );
											}
											else if ( str3.Equals( "JudgeLinePosOffsetGuitar" ) )		// #31602 2013.6.23 yyagi
											{
												this.nJudgeLinePosOffset.Guitar = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -99, 99, this.nJudgeLinePosOffset.Guitar );
											}
											else if ( str3.Equals( "JudgeLinePosOffsetBass" ) )			// #31602 2013.6.23 yyagi
											{
												this.nJudgeLinePosOffset.Bass = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -99, 99, this.nJudgeLinePosOffset.Bass );
											}
											else if ( str3.Equals( "JudgeLinePosModeGuitar" ) )	// #33891 2014.6.26 yyagi
											{
												this.e判定位置.Guitar = (E判定位置) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.e判定位置.Guitar );
											}
											else if ( str3.Equals( "JudgeLinePosModeBass" ) )		// #33891 2014.6.26 yyagi
											{
												this.e判定位置.Bass = (E判定位置) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.e判定位置.Bass );
											}
											#endregion
											else if ( str3.Equals( "BufferedInput" ) )
											{
												this.bバッファ入力を行う = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "PolyphonicSounds" ) )		// #28228 2012.5.1 yyagi
											{
												this.nPoliphonicSounds = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 1, 8, this.nPoliphonicSounds );
											}
											#region [ VelocityMin ]
											else if ( str3.Equals( "LCVelocityMin" ) )			// #23857 2010.12.12 yyagi
											{
												this.nVelocityMin.LC = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.LC );
											}
											else if ( str3.Equals( "HHVelocityMin" ) )
											{
												this.nVelocityMin.HH = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.HH );
											}
											else if ( str3.Equals( "SDVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.SD = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.SD );
											}
											else if ( str3.Equals( "BDVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.BD = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.BD );
											}
											else if ( str3.Equals( "HTVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.HT = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.HT );
											}
											else if ( str3.Equals( "LTVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.LT = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.LT );
											}
											else if ( str3.Equals( "FTVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.FT = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.FT );
											}
											else if ( str3.Equals( "CYVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.CY = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.CY );
											}
											else if ( str3.Equals( "RDVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.RD = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.RD );
											}
                                            else if ( str3.Equals( "LPVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.LP = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.LP );
											}
											else if ( str3.Equals( "LBDVelocityMin" ) )			// #23857 2011.1.31 yyagi
											{
												this.nVelocityMin.LBD = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 127, this.nVelocityMin.LBD );
											}
											#endregion
											//else if ( str3.Equals( "NoMP3Streaming" ) )
											//{
											//    this.bNoMP3Streaming = C変換.bONorOFF( str4[ 0 ] );
											//}
											continue;
										}
									//-----------------------------
									#endregion

									#region [ [Log] ]
									//-----------------------------
									case Eセクション種別.Log:
										{
											if( str3.Equals( "OutputLog" ) )
											{
												this.bログ出力 = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "TraceCreatedDisposed" ) )
											{
												this.bLog作成解放ログ出力 = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "TraceDTXDetails" ) )
											{
												this.bLogDTX詳細ログ出力 = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "TraceSongSearch" ) )
											{
												this.bLog曲検索ログ出力 = C変換.bONorOFF( str4[ 0 ] );
											}
											continue;
										}
									//-----------------------------
									#endregion

									#region [ [PlayOption] ]
									//-----------------------------
									case Eセクション種別.PlayOption:
										{
											if( str3.Equals( "Dark" ) )
											{
												this.eDark = (Eダークモード) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eDark );
											}
											else if( str3.Equals( "DrumGraph" ) )  // #24074 2011.01.23 addikanick
											{
												this.bGraph.Drums = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "GuitarGraph" ) )  // #24074 2011.01.23 addikanick
											{
												this.bGraph.Guitar = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "BassGraph" ) )  // #24074 2011.01.23 addikanick
											{
												this.bGraph.Bass = C変換.bONorOFF( str4[ 0 ] );
											}
                                            else if (str3.Equals("DrumAutoGhost")) // #35411 2015.08.18 chnmr0 add
                                            {
                                                this.eAutoGhost.Drums = (EAutoGhostData)C変換.n値を文字列から取得して返す(str4, 0);
                                            }
                                            else if (str3.Equals("GuitarAutoGhost")) // #35411 2015.08.18 chnmr0 add
                                            {
                                                this.eAutoGhost.Guitar = (EAutoGhostData)C変換.n値を文字列から取得して返す(str4, 0);
                                            }
                                            else if (str3.Equals("BassAutoGhost")) // #35411 2015.08.18 chnmr0 add
                                            {
                                                this.eAutoGhost.Bass = (EAutoGhostData)C変換.n値を文字列から取得して返す(str4, 0);
                                            }
                                            else if (str3.Equals("DrumTargetGhost")) // #35411 2015.08.18 chnmr0 add
                                            {
                                                this.eTargetGhost.Drums = (ETargetGhostData)C変換.n値を文字列から取得して返す(str4, 0);
                                            }
                                            else if (str3.Equals("GuitarTargetGhost")) // #35411 2015.08.18 chnmr0 add
                                            {
                                                this.eTargetGhost.Guitar = (ETargetGhostData)C変換.n値を文字列から取得して返す(str4, 0);
                                            }
                                            else if (str3.Equals("BassTargetGhost")) // #35411 2015.08.18 chnmr0 add
                                            {
                                                this.eTargetGhost.Bass = (ETargetGhostData)C変換.n値を文字列から取得して返す(str4, 0);
                                            }

											#region [ Sudden ]
											else if( str3.Equals( "DrumsSudden" ) )
											{
												this.bSudden.Drums = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "GuitarSudden" ) )
											{
												this.bSudden.Guitar = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "BassSudden" ) )
											{
												this.bSudden.Bass = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											#region [ Hidden ]
											else if( str3.Equals( "DrumsHidden" ) )
											{
												this.bHidden.Drums = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "GuitarHidden" ) )
											{
												this.bHidden.Guitar = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "BassHidden" ) )
											{
												this.bHidden.Bass = C変換.bONorOFF( str4[ 0 ] );
											}
											#endregion
											#region [ Invisible ]
											else if ( str3.Equals( "DrumsInvisible" ) )
											{
												this.eInvisible.Drums = (EInvisible) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eInvisible.Drums );
											}
											else if ( str3.Equals( "GuitarInvisible" ) )
											{
												this.eInvisible.Guitar = (EInvisible) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eInvisible.Guitar ); 
											}
											else if ( str3.Equals( "BassInvisible" ) )
											{
												this.eInvisible.Bass = (EInvisible) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.eInvisible.Bass );
											}
											//else if ( str3.Equals( "InvisibleDisplayTimeMs" ) )
											//{
											//    this.nDisplayTimesMs = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 9999999, (int) this.nDisplayTimesMs );
											//}
											//else if ( str3.Equals( "InvisibleFadeoutTimeMs" ) )
											//{
											//    this.nFadeoutTimeMs = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 9999999, (int) this.nFadeoutTimeMs );
											//}
											#endregion
											else if ( str3.Equals( "DrumsReverse" ) )
											{
												this.bReverse.Drums = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "GuitarReverse" ) )
											{
												this.bReverse.Guitar = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "BassReverse" ) )
											{
												this.bReverse.Bass = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "GuitarRandom" ) )
											{
												this.eRandom.Guitar = (Eランダムモード) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.eRandom.Guitar );
											}
											else if( str3.Equals( "BassRandom" ) )
											{
												this.eRandom.Bass = (Eランダムモード) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.eRandom.Bass );
											}
                                            #region[  ]
                                            else if( str3.Equals( "NumOfLanes" ) )
                                            {
                                                this.eNumOfLanes.Drums = ( Eタイプ )C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 2, (int)this.eNumOfLanes.Drums);
                                            }
                                            else if( str3.Equals( "DkdkType" ) )
                                            {
                                                this.eDkdkType.Drums = ( Eタイプ )C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 2, (int)this.eDkdkType.Drums);
                                            }
                                            else if( str3.Equals( "DrumsRandomPad" ) )
                                            {
                                                this.eRandom.Drums = ( Eランダムモード )C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 6, (int)this.eRandom.Drums);
                                            }
                                            else if( str3.Equals( "DrumsRandomPedal" ) )
                                            {
                                                this.eRandomPedal.Drums = ( Eランダムモード )C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 6, (int)this.eRandomPedal.Drums);
                                            }
                                            #endregion
                                            else if( str3.Equals( "GuitarLight" ) )
											{
												this.bLight.Guitar = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "BassLight" ) )
											{
												this.bLight.Bass = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "GuitarLeft" ) )
											{
												this.bLeft.Guitar = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "BassLeft" ) )
											{
												this.bLeft.Bass = C変換.bONorOFF( str4[ 0 ] );
											}
											else if( str3.Equals( "DrumsPosition" ) )
											{
												this.判定文字表示位置.Drums = (E判定文字表示位置) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int) this.判定文字表示位置.Drums );
											}
											else if( str3.Equals( "GuitarPosition" ) )
											{
												this.判定文字表示位置.Guitar = (E判定文字表示位置) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.判定文字表示位置.Guitar );
											}
											else if( str3.Equals( "BassPosition" ) )
											{
												this.判定文字表示位置.Bass = (E判定文字表示位置) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.判定文字表示位置.Bass );
											}
											else if( str3.Equals( "DrumsScrollSpeed" ) )
											{
												this.n譜面スクロール速度.Drums = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x7cf, this.n譜面スクロール速度.Drums );
											}
											else if( str3.Equals( "GuitarScrollSpeed" ) )
											{
												this.n譜面スクロール速度.Guitar = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x7cf, this.n譜面スクロール速度.Guitar );
											}
											else if( str3.Equals( "BassScrollSpeed" ) )
											{
												this.n譜面スクロール速度.Bass = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x7cf, this.n譜面スクロール速度.Bass );
											}
											else if( str3.Equals( "PlaySpeed" ) )
											{
												this.n演奏速度 = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 5, 40, this.n演奏速度 );
											}
											else if( str3.Equals( "ComboPosition" ) )
											{
												this.ドラムコンボ文字の表示位置 = (Eドラムコンボ文字の表示位置) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.ドラムコンボ文字の表示位置 );
											}
											//else if ( str3.Equals( "JudgeDispPriorityDrums" ) )
											//{
											//    this.e判定表示優先度.Drums = (E判定表示優先度) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.e判定表示優先度.Drums );
											//}
											//else if ( str3.Equals( "JudgeDispPriorityGuitar" ) )
											//{
											//    this.e判定表示優先度.Guitar = (E判定表示優先度) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.e判定表示優先度.Guitar );
											//}
											//else if ( str3.Equals( "JudgeDispPriorityBass" ) )
											//{
											//    this.e判定表示優先度.Bass = (E判定表示優先度) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int) this.e判定表示優先度.Bass );
											//}
											else if ( str3.Equals( "Risky" ) )					// #23559 2011.6.23  yyagi
											{
												this.nRisky = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 10, this.nRisky );
											}
											else if ( str3.Equals( "DrumsTight" ) )				// #29500 2012.9.11 kairera0467
											{
												this.bTight = C変換.bONorOFF( str4[ 0 ] );
											}
                                            else if( str3.Equals( "DrumsShutterIn" ) )
                                            {
                                                this.nShutterInSide.Drums = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 100, this.nShutterInSide.Drums );
                                            }
                                            else if( str3.Equals( "DrumsShutterOut" ) )
                                            {
                                                this.nShutterOutSide.Drums = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -100, 100, this.nShutterOutSide.Drums );
                                            }
                                            else if( str3.Equals( "GuitarShutterIn" ) )
                                            {
                                                this.nShutterInSide.Guitar = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 100, this.nShutterInSide.Guitar );
                                            }
                                            else if( str3.Equals( "GuitarShutterOut" ) )
                                            {
                                                this.nShutterOutSide.Guitar = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -100, 100, this.nShutterOutSide.Guitar );
                                            }
                                            else if( str3.Equals( "BassShutterIn" ) )
                                            {
                                                this.nShutterInSide.Bass = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 100, this.nShutterInSide.Bass );
                                            }
                                            else if( str3.Equals( "BassShutterOut" ) )
                                            {
                                                this.nShutterOutSide.Bass = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, -100, 100, this.nShutterOutSide.Guitar );
                                            }
                                            else if( str3.Equals( "DrumsLaneType" ) )
                                            {
                                                this.eLaneType = ( Eタイプ ) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int)this.eLaneType );
                                            }
                                            else if( str3.Equals( "RDPosition" ))
                                            {
                                                this.eRDPosition = ( ERDPosition )C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int)this.eRDPosition );
                                            }
                                            #region [ XGオプション ]
                                            else if( str3.Equals( "NamePlateType" ) )
                                            {
                                                this.eNamePlateType = (Eタイプ)C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int)this.eNamePlateType );
                                            }
                                            //else if (str3.Equals("DrumSetMoves"))
                                            //{
                                            //    this.eドラムセットを動かす = (Eタイプ)C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 2, (int)this.eドラムセットを動かす);
                                            //}
                                            //else if (str3.Equals("BPMBar"))
                                            //{
                                            //    this.eBPMbar = ( Eタイプ )C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 3, (int)this.eBPMbar);
                                            //}
                                            //else if (str3.Equals("LivePoint"))
                                            //{
                                            //    this.bLivePoint = C変換.bONorOFF(str4[0]);
                                            //}
                                            //else if (str3.Equals("Speaker"))
                                            //{
                                            //    this.bSpeaker = C変換.bONorOFF(str4[0]);
                                            //}
                                            #endregion
                                            //else if( str3.Equals( "DrumsStageEffect" ) )
                                            //{
                                            //    this.ボーナス演出を表示する = C変換.bONorOFF( str4[0] );
                                            //}
                                            else if( str3.Equals( "CLASSIC" ) )
                                            {
                                                this.bCLASSIC譜面判別を有効にする = C変換.bONorOFF( str4[0] );
                                            }
                                            //else if( str3.Equals( "MutingLP" ) )
                                            //{
                                            //    this.bMutingLP = C変換.bONorOFF(str4[0]);
                                            //}
                                            else if( str3.Equals( "SkillMode" ) )
                                            {
                                                this.eSkillMode = ( ESkillType )C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, (int)this.eSkillMode );
                                            }
                                            else if( str3.Equals( "SwitchSkillMode" ) )
                                            {
                                                this.bSkillModeを自動切替えする = C変換.bONorOFF(str4[0]);
                                            }
                                            else if( str3.Equals( "JudgeAnimeType" ) )
                                            {
                                                this.eJudgeAnimeType = ( Eタイプ )C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int)this.eJudgeAnimeType );
                                            }
                                            else if( str3.Equals( "XPerfectJudgeMode" ) )
                                            {
                                                this.bXPerfect判定を有効にする = C変換.bONorOFF(str4[0]);
                                            }
                                            else if( str3.Equals( "JudgeCountDisp" ) )
                                            {
                                                this.bJudgeCountDisp = C変換.bONorOFF( str4[ 0 ] );
                                            }
                                            else if( str3.Equals( "DrumsJust" ) )
                                            {
                                                this.eJUST.Drums = (EJust) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int)this.eJUST.Drums );
                                            }
                                            else if( str3.Equals( "GuitarJust" ) )
                                            {
                                                this.eJUST.Guitar = (EJust) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int)this.eJUST.Guitar );
                                            }
                                            else if( str3.Equals( "BassJust" ) )
                                            {
                                                this.eJUST.Bass = (EJust) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 2, (int)this.eJUST.Bass );
                                            }
                                            else if( str3.Equals( "ShutterImageDrums" ) )
                                            {
                                                this.strShutterImageName.Drums = str4;
                                            }
                                            else if( str3.Equals( "ShutterImageGuitar" ) )
                                            {
                                                this.strShutterImageName.Guitar = str4;
                                            }
                                            else if( str3.Equals( "ShutterImageBass" ) )
                                            {
                                                this.strShutterImageName.Bass = str4;
                                            }
                                            else if( str3.Equals( "MatixxFrameDisp" ) )
                                            {
                                                this.bフレームを表示する = C変換.bONorOFF( str4[ 0 ] );
                                            }
											continue;
										}
									//-----------------------------
									#endregion

									#region [ [ViewerOption] ]
									//-----------------------------
									case Eセクション種別.ViewerOption:
										{
											if ( str3.Equals( "ViewerDrumsScrollSpeed" ) )
											{
												this.nViewerScrollSpeed.Drums = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1999, this.nViewerScrollSpeed.Drums );
											}
											else if ( str3.Equals( "ViewerGuitarScrollSpeed" ) )
											{
												this.nViewerScrollSpeed.Guitar = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1999, this.nViewerScrollSpeed.Guitar );
											}
											else if ( str3.Equals( "ViewerBassScrollSpeed" ) )
											{
												this.nViewerScrollSpeed.Bass = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1999, this.nViewerScrollSpeed.Bass );
											}
											else if ( str3.Equals( "ViewerVSyncWait" ) )
											{
												this.bViewerVSyncWait = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "ViewerShowDebugStatus" ) )
											{
												this.bViewerShowDebugStatus = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "ViewerTimeStretch" ) )
											{
												this.bViewerTimeStretch = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "ViewerGuitar" ) )
											{
												this.bViewerGuitar有効 = C変換.bONorOFF( str4[ 0 ] );
											}
											else if ( str3.Equals( "ViewerDrums" ) )
											{
												this.bViewerDrums有効 = C変換.bONorOFF( str4[ 0 ] );
											}

											else if ( str3.Equals( "ViewerWindowWidth" ) )
											{
												this.nViewerウインドウwidth = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 65535, this.nViewerウインドウwidth );
											}
											else if ( str3.Equals( "ViewerWindowHeight" ) )
											{
												this.nViewerウインドウheight = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 65535, this.nViewerウインドウheight );
											}
											else if ( str3.Equals( "ViewerWindowX" ) )
											{
												this.nViewer初期ウィンドウ開始位置X = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 65535, this.nViewer初期ウィンドウ開始位置X );
											}
											else if ( str3.Equals( "ViewerWindowY" ) )
											{
												this.nViewer初期ウィンドウ開始位置Y = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 65535, this.nViewer初期ウィンドウ開始位置Y );
											}
											continue;
										}
									//-----------------------------
									#endregion

									#region [ [AutoPlay] ]
									//-----------------------------
									case Eセクション種別.AutoPlay:
										if( str3.Equals( "LC" ) )
										{
											this.bAutoPlay.LC = C変換.bONorOFF( str4[ 0 ] );
										}
										if( str3.Equals( "HH" ) )
										{
										this.bAutoPlay.HH = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "SD" ) )
										{
											this.bAutoPlay.SD = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "BD" ) )
										{
											this.bAutoPlay.BD = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "HT" ) )
										{
											this.bAutoPlay.HT = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "LT" ) )
										{
											this.bAutoPlay.LT = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "FT" ) )
										{
										    this.bAutoPlay.FT = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "CY" ) )
										{
											this.bAutoPlay.CY = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "LP" ) )
										{
										    this.bAutoPlay.LP = C変換.bONorOFF( str4[ 0 ] );
										}
										else if( str3.Equals( "LBD" ) )
										{
											this.bAutoPlay.LBD = C変換.bONorOFF( str4[ 0 ] );
										}										//else if( str3.Equals( "Guitar" ) )
										//{
										//    this.bAutoPlay.Guitar = C変換.bONorOFF( str4[ 0 ] );
										//}
										else if ( str3.Equals( "GuitarR" ) )
										{
											this.bAutoPlay.GtR = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "GuitarG" ) )
										{
											this.bAutoPlay.GtG = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "GuitarB" ) )
										{
											this.bAutoPlay.GtB = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "GuitarPick" ) )
										{
											this.bAutoPlay.GtPick = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "GuitarWailing" ) )
										{
											this.bAutoPlay.GtW = C変換.bONorOFF( str4[ 0 ] );
										}
										//else if ( str3.Equals( "Bass" ) )
										//{
										//    this.bAutoPlay.Bass = C変換.bONorOFF( str4[ 0 ] );
										//}
										else if ( str3.Equals( "BassR" ) )
										{
											this.bAutoPlay.BsR = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "BassG" ) )
										{
											this.bAutoPlay.BsG = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "BassB" ) )
										{
											this.bAutoPlay.BsB = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "BassPick" ) )
										{
											this.bAutoPlay.BsPick = C変換.bONorOFF( str4[ 0 ] );
										}
										else if ( str3.Equals( "BassWailing" ) )
										{
											this.bAutoPlay.BsW = C変換.bONorOFF( str4[ 0 ] );
										}
										continue;
									//-----------------------------
									#endregion

									#region [ [HitRange] ]
									//-----------------------------
									case Eセクション種別.HitRange:
										if( str3.Equals( "Perfect" ) )
										{
											this.nヒット範囲ms.Perfect = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x3e7, this.nヒット範囲ms.Perfect );
										}
										else if( str3.Equals( "Great" ) )
										{
											this.nヒット範囲ms.Great = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x3e7, this.nヒット範囲ms.Great );
										}
										else if( str3.Equals( "Good" ) )
										{
											this.nヒット範囲ms.Good = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x3e7, this.nヒット範囲ms.Good );
										}
										else if( str3.Equals( "Poor" ) )
										{
											this.nヒット範囲ms.Poor = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 0x3e7, this.nヒット範囲ms.Poor );
										}
                                        else if( str3.Equals( "PedalHitRangeDelta" ) )
                                        {
                                            this.nPedalJudgeRangeDelta = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 200, this.nPedalJudgeRangeDelta );
                                        }
										continue;
									//-----------------------------
									#endregion

									#region [ [GUID] ]
									//-----------------------------
									case Eセクション種別.GUID:
										if( str3.Equals( "JoystickID" ) )
										{
											this.tJoystickIDの取得( str4 );
										}
										continue;
									//-----------------------------
									#endregion

									#region [ [DrumsKeyAssign] ]
									//-----------------------------
									case Eセクション種別.DrumsKeyAssign:
										{
											if( str3.Equals( "HH" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.HH );
											}
											else if( str3.Equals( "SD" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.SD );
											}
											else if( str3.Equals( "BD" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.BD );
											}
											else if( str3.Equals( "HT" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.HT );
											}
											else if( str3.Equals( "LT" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.LT );
											}
											else if( str3.Equals( "FT" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.FT );
											}
											else if( str3.Equals( "CY" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.CY );
											}
											else if( str3.Equals( "HO" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.HHO );
											}
											else if( str3.Equals( "RD" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.RD );
											}
											else if( str3.Equals( "LC" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.LC );
											}
											else if( str3.Equals( "LP" ) )										// #27029 2012.1.4 from
											{																	//
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.LP );	//
											}																	//
											else if( str3.Equals( "LBD" ) )										// 2016.02.21 kairera0467
											{																	//
												this.tキーの読み出しと設定( str4, this.KeyAssign.Drums.LBD );	//
											}																	//
											continue;
										}
									//-----------------------------
									#endregion

									#region [ [GuitarKeyAssign] ]
									//-----------------------------
									case Eセクション種別.GuitarKeyAssign:
										{
											if( str3.Equals( "R" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Guitar.R );
											}
											else if( str3.Equals( "G" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Guitar.G );
											}
											else if( str3.Equals( "B" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Guitar.B );
											}
											else if( str3.Equals( "Pick" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Guitar.Pick );
											}
											else if( str3.Equals( "Wail" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Guitar.Wail );
											}
											else if( str3.Equals( "Decide" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Guitar.Decide );
											}
											else if( str3.Equals( "Cancel" ) )
											{
												this.tキーの読み出しと設定( str4, this.KeyAssign.Guitar.Cancel );
											}
											continue;
										}
									//-----------------------------
									#endregion

									#region [ [BassKeyAssign] ]
									//-----------------------------
									case Eセクション種別.BassKeyAssign:
										if( str3.Equals( "R" ) )
										{
											this.tキーの読み出しと設定( str4, this.KeyAssign.Bass.R );
										}
										else if( str3.Equals( "G" ) )
										{
										this.tキーの読み出しと設定( str4, this.KeyAssign.Bass.G );
										}
										else if( str3.Equals( "B" ) )
										{
											this.tキーの読み出しと設定( str4, this.KeyAssign.Bass.B );
										}
										else if( str3.Equals( "Pick" ) )
										{
											this.tキーの読み出しと設定( str4, this.KeyAssign.Bass.Pick );
										}
										else if( str3.Equals( "Wail" ) )
										{
											this.tキーの読み出しと設定( str4, this.KeyAssign.Bass.Wail );
										}
										else if( str3.Equals( "Decide" ) )
										{
											this.tキーの読み出しと設定( str4, this.KeyAssign.Bass.Decide );
										}
										else if( str3.Equals( "Cancel" ) )
										{
											this.tキーの読み出しと設定( str4, this.KeyAssign.Bass.Cancel );
										}
										continue;
									//-----------------------------
									#endregion

									#region [ [SystemKeyAssign] ]
									//-----------------------------
									case Eセクション種別.SystemKeyAssign:
										if( str3.Equals( "Capture" ) )
										{
											this.tキーの読み出しと設定( str4, this.KeyAssign.System.Capture );
										}
										continue;
									//-----------------------------
									#endregion

									#region [ [Temp] ]
									//-----------------------------
									case Eセクション種別.Temp:	// #27029 2012.1.5 from
										if( str3.Equals( "BackupOf1BD_HHGroup" ) )
										{
											if( this.BackupOf1BD == null ) this.BackupOf1BD = new CBackupOf1BD();
											this.BackupOf1BD.eHHGroup = (EHHGroup) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.BackupOf1BD.eHHGroup );
										}
										else if( str3.Equals( "BackupOf1BD_HitSoundPriorityHH" ) )
										{
											if( this.BackupOf1BD == null ) this.BackupOf1BD = new CBackupOf1BD();
											this.BackupOf1BD.eHitSoundPriorityHH = (E打ち分け時の再生の優先順位) C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int) this.BackupOf1BD.eHitSoundPriorityHH );
										}
										continue;
									//-----------------------------
									#endregion
								}
							}
						}
						continue;
					}
					catch ( Exception exception )
					{
						Trace.TraceError( exception.Message );
						continue;
					}
				}
			}
		}

		/// <summary>
		/// ギターとベースのキーアサイン入れ替え
		/// </summary>
		//public void SwapGuitarBassKeyAssign()		// #24063 2011.1.16 yyagi
		//{
		//    for ( int j = 0; j <= (int)EKeyConfigPad.Capture; j++ )
		//    {
		//        CKeyAssign.STKEYASSIGN t; //= new CConfigIni.CKeyAssign.STKEYASSIGN();
		//        for ( int k = 0; k < 16; k++ )
		//        {
		//            t = this.KeyAssign[ (int)EKeyConfigPart.GUITAR ][ j ][ k ];
		//            this.KeyAssign[ (int)EKeyConfigPart.GUITAR ][ j ][ k ] = this.KeyAssign[ (int)EKeyConfigPart.BASS ][ j ][ k ];
		//            this.KeyAssign[ (int)EKeyConfigPart.BASS ][ j ][ k ] = t;
		//        }
		//    }
		//    this.bIsSwappedGuitarBass = !bIsSwappedGuitarBass;
		//}


		// その他

		#region [ private ]
		//-----------------
		private enum Eセクション種別
		{
			Unknown,
			System,
			Log,
			PlayOption,
			ViewerOption,
			AutoPlay,
			HitRange,
			GUID,
			DrumsKeyAssign,
			GuitarKeyAssign,
			BassKeyAssign,
			SystemKeyAssign,
			Temp,
		}

		private bool _bDrums有効;
		private bool _bGuitar有効;
		private bool bConfigIniが存在している;
		private string ConfigIniファイル名;

		private void tJoystickIDの取得( string strキー記述 )
		{
			string[] strArray = strキー記述.Split( new char[] { ',' } );
			if( strArray.Length >= 2 )
			{
				int result = 0;
				if( ( int.TryParse( strArray[ 0 ], out result ) && ( result >= 0 ) ) && ( result <= 9 ) )
				{
					if( this.dicJoystick.ContainsKey( result ) )
					{
						this.dicJoystick.Remove( result );
					}
					this.dicJoystick.Add( result, strArray[ 1 ] );
				}
			}
		}
		private void tキーアサインを全部クリアする()
		{
			this.KeyAssign = new CKeyAssign();
			for( int i = 0; i <= (int)EKeyConfigPart.SYSTEM; i++ )
			{
				for( int j = 0; j <= (int)EKeyConfigPad.Capture; j++ )
				{
					this.KeyAssign[ i ][ j ] = new CKeyAssign.STKEYASSIGN[ 16 ];
					for( int k = 0; k < 16; k++ )
					{
						this.KeyAssign[ i ][ j ][ k ] = new CKeyAssign.STKEYASSIGN( E入力デバイス.不明, 0, 0 );
					}
				}
			}
		}
		private void tキーの書き出し( StreamWriter sw, CKeyAssign.STKEYASSIGN[] assign )
		{
			bool flag = true;
			for( int i = 0; i < 0x10; i++ )
			{
				if( assign[ i ].入力デバイス == E入力デバイス.不明 )
				{
					continue;
				}
				if( !flag )
				{
					sw.Write( ',' );
				}
				flag = false;
				switch( assign[ i ].入力デバイス )
				{
					case E入力デバイス.キーボード:
						sw.Write( 'K' );
						break;

					case E入力デバイス.MIDI入力:
						sw.Write( 'M' );
						break;

					case E入力デバイス.ジョイパッド:
						sw.Write( 'J' );
						break;

					case E入力デバイス.マウス:
						sw.Write( 'N' );
						break;
				}
				sw.Write( "{0}{1}", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring( assign[ i ].ID, 1 ), assign[ i ].コード );	// #24166 2011.1.15 yyagi: to support ID > 10, change 2nd character from Decimal to 36-numeral system. (e.g. J1023 -> JA23)
			}
		}
		private void tキーの読み出しと設定( string strキー記述, CKeyAssign.STKEYASSIGN[] assign )
		{
			string[] strArray = strキー記述.Split( new char[] { ',' } );
			for( int i = 0; ( i < strArray.Length ) && ( i < 0x10 ); i++ )
			{
				E入力デバイス e入力デバイス;
				int id;
				int code;
				string str = strArray[ i ].Trim().ToUpper();
				if ( str.Length >= 3 )
				{
					e入力デバイス = E入力デバイス.不明;
					switch ( str[ 0 ] )
					{
						case 'J':
							e入力デバイス = E入力デバイス.ジョイパッド;
							break;

						case 'K':
							e入力デバイス = E入力デバイス.キーボード;
							break;

						case 'L':
							continue;

						case 'M':
							e入力デバイス = E入力デバイス.MIDI入力;
							break;

						case 'N':
							e入力デバイス = E入力デバイス.マウス;
							break;
					}
				}
				else
				{
					continue;
				}
				id = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf( str[ 1 ] );	// #24166 2011.1.15 yyagi: to support ID > 10, change 2nd character from Decimal to 36-numeral system. (e.g. J1023 -> JA23)
				if( ( ( id >= 0 ) && int.TryParse( str.Substring( 2 ), out code ) ) && ( ( code >= 0 ) && ( code <= 0xff ) ) )
				{
					this.t指定した入力が既にアサイン済みである場合はそれを全削除する( e入力デバイス, id, code );
					assign[ i ].入力デバイス = e入力デバイス;
					assign[ i ].ID = id;
					assign[ i ].コード = code;
				}
			}
		}
		private void tデフォルトのキーアサインに設定する()
		{
			this.tキーアサインを全部クリアする();

			string strDefaultKeyAssign = @"
[DrumsKeyAssign]

HH=K035,M042,M093
SD=K033,M025,M026,M027,M028,M029,M031,M032,M034,M037,M038,M040,M0113
BD=K012,K0126,M033,M035,M036,M0112
HT=K031,M048,M050
LT=K011,M047
FT=K023,M041,M043,M045
CY=K022,M049,M052,M055,M057,M091
HO=K010,M046,M092
RD=K020,M051,M053,M059,M089
LC=K026
HP=M044

[GuitarKeyAssign]

R=K055
G=K056,J012
B=K057
Pick=K0115,K046,J06
Wail=K0116
Decide=K060
Cancel=K061

[BassKeyAssign]

R=K090
G=K091,J013
B=K092
Pick=K0103,K0100,J08
Wail=K089
Decide=K096
Cancel=K097

[SystemKeyAssign]
Capture=K065
";
			t文字列から読み込み( strDefaultKeyAssign );
		}
		//-----------------
		#endregion
    }
}
