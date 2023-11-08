using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.Threading;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActConfigList共通 : CActivity
	{
		// プロパティ

		public bool bIsKeyAssignSelected		// #24525 2011.3.15 yyagi
		{
			get
			{
				Eメニュー種別 e = this.eメニュー種別;
				if ( e == Eメニュー種別.KeyAssignBass || e == Eメニュー種別.KeyAssignDrums ||
					e == Eメニュー種別.KeyAssignGuitar || e == Eメニュー種別.KeyAssignSystem )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public bool bIsFocusingParameter		// #32059 2013.9.17 yyagi
		{
			get
			{
				return b要素値にフォーカス中;
			}
		}
		public bool b現在選択されている項目はReturnToMenuである
		{
			get
			{
				CItemBase currentItem = this.list項目リスト[ this.n現在の選択項目 ];
				if ( currentItem == this.iSystemReturnToMenu || currentItem == this.iDrumsReturnToMenu ||
					currentItem == this.iGuitarReturnToMenu || currentItem == this.iBassReturnToMenu )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public CItemBase ib現在の選択項目
		{
			get
			{
				return this.list項目リスト[ this.n現在の選択項目 ];
			}
		}
		public int n現在の選択項目;


		// メソッド
		#region [ t項目リストの設定_System() ]
		public void t項目リストの設定_System()
		{
			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iSystemReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iSystemReturnToMenu );

			this.iSystemReloadDTX = new CItemBase( "Reload Songs", CItemBase.Eパネル種別.通常,
				"曲データの一覧情報を取得し直します。",
				"Reload song data." );
			this.list項目リスト.Add( this.iSystemReloadDTX );

            //this.iCommonDark = new CItemList( "Dark", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eDark,
            //    "HALF: 背景、レーン、ゲージが表示されなくなります。\n" +
            //    "FULL: さらに小節線、拍線、判定ライン、パッドも表示されなくなります。",
            //    "OFF: all display parts are shown.\n" +
            //    "HALF: wallpaper, lanes and gauge are disappeared.\n" +
            //    "FULL: additionaly to HALF, bar/beat lines, hit bar, pads are disappeared.",
            //    new string[] { "OFF", "HALF", "FULL" } );
            //this.list項目リスト.Add( this.iCommonDark );

            //this.iSystemRisky = new CItemInteger( "Risky", 0, 10, CDTXMania.ConfigIni.nRisky,
            //    "Riskyモードの設定:\n" +
            //    "1以上の値にすると、その回数分のPoor/Missで\n" + 
            //    "FAILEDとなります。\n" +
            //    "0にすると無効になり、DamageLevelに従った\n" + 
            //    "ゲージ増減となります。\n" +
            //    "\n" +
            //    "なお、この設定は、StageFailedの設定と併用できます。",
            //    "Risky mode:\n" +
            //    "It means the Poor/Miss times to be FAILED.\n" +
            //    "Set 0 to disable Risky mode." );
            //this.list項目リスト.Add( this.iSystemRisky );

            int nDGmode = ( CDTXMania.ConfigIni.bGuitar有効 ? 1 : 1 ) + ( CDTXMania.ConfigIni.bDrums有効 ? 0 : 1 ) - 1;
			this.iSystemGRmode = new CItemList("Drums & GR", CItemBase.Eパネル種別.通常, nDGmode,
				"使用楽器の選択：\n" + 
                "DrOnly: ドラムを有効にします。\n" + 
                "GROnly: ギター/ベースを有効にします。",
				"DrOnly: Drums is available.\n" + 
                "GROnly: Guitar/Bass are available.\n",
				new string[] { "DrOnly", "GROnly" });
			this.list項目リスト.Add( this.iSystemGRmode );

			this.iCommonPlaySpeed = new CItemInteger( "PlaySpeed", 5, 40, CDTXMania.ConfigIni.n演奏速度,
				"曲の演奏速度を、速くしたり遅くしたりすることが\n" + 
                "できます。" +
				"（※一部のサウンドカードでは正しく再生\n" + 
                "できない可能性があります。）\n" +
				"\n" +
				"TimeStretchがONのとき、x0.850以下にすると、\n" + 
                "チップのズレが大きくなります。",
				"It changes the song speed.\n" +
				"For example, you can play in half speed by setting PlaySpeed = 0.500 for your practice.\n" +
				"\n" +
				"Note: It also changes the songs' pitch. In case TimeStretch=ON and slower than x0.900, some sound lag occurs." );
			this.list項目リスト.Add( this.iCommonPlaySpeed );

			this.iSystemTimeStretch = new CItemToggle( "TimeStretch", CDTXMania.ConfigIni.bTimeStretch,
				"演奏速度の変更方式:\n" +
				"ONにすると、演奏速度の変更を周波数変更ではなく\n"+
                "タイムストレッチで行います。\n" +
				"\n" +
				"タイムストレッチ使用時は、サウンド処理により\n" +
                "多くのCPU性能を使用します。\n" +
				"また、演奏速度をx0.850以下にすると、\n" +
                "チップのズレが大きくなります。",
				"The way to change the playing speed:\n" +
				"Turn ON to use time stretch to change the play speed." +
				"\n" +
				"TimeStretch uses more CPU power. And some sound lag occurs if PlaySpeed is slower than x0.900." );
			this.list項目リスト.Add( this.iSystemTimeStretch );

			this.iSystemFullscreen = new CItemToggle( "Fullscreen", CDTXMania.ConfigIni.b全画面モード,
				"画面モード設定：\n" +
				"ONで全画面モード、OFFでウィンドウモードになります。",
				"Fullscreen mode or window mode." );
			this.list項目リスト.Add( this.iSystemFullscreen );

			this.iSystemStageFailed = new CItemToggle( "StageFailed", CDTXMania.ConfigIni.bSTAGEFAILED有効,
				"STAGE FAILED 有効：\n" +
				"ON にすると、ゲージがなくなった時に \n" +
                "STAGE FAILED となり演奏が中断されます。\n" +
				"OFF の場合は、ゲージがなくなっても\n" +
                "最後まで演奏できます。",
				"Turn OFF if you don't want to encount GAME OVER." );
			this.list項目リスト.Add( this.iSystemStageFailed );

			this.iSystemRandomFromSubBox = new CItemToggle( "RandSubBox", CDTXMania.ConfigIni.bランダムセレクトで子BOXを検索対象とする,
				"子BOXをRANDOMの対象とする：\n" +
				"ON にすると、RANDOM SELECT 時に子BOXも\n" +
                "選択対象とします。",
				"Turn ON to use child BOX (subfolders) at RANDOM SELECT." );
			this.list項目リスト.Add( this.iSystemRandomFromSubBox );


			this.iSystemAdjustWaves = new CItemToggle( "AdjustWaves", CDTXMania.ConfigIni.bWave再生位置自動調整機能有効,
				"サウンド再生位置自動補正：\n" +
				"ハードウェアやOSに起因するサウンドのずれを\n"+
                "強制的に補正します。\n" +
				"BGM のように再生時間の長い音声データが\n" +
                "使用されている曲で効果があります。\n" +
				"\n" +
				"※ SoundTypeでDSound(DirectSound)を指定している\n" +
                "場合にのみ有効です。\n" +
                "WASAPI/ASIO使用時は効果がありません。",
				"Automatic wave playing position adjustment feature.\n" +
				"\n" +
				"If you turn it ON, it decrease the lag which comes from the difference of hardware/OS.\n" +
				"Usually, you should turn it ON.\n" +
				"\n" +
				"Note: This setting is effetive only when DSound(DirectSound) in SoundType is used." );
			this.list項目リスト.Add( this.iSystemAdjustWaves );


			this.iSystemVSyncWait = new CItemToggle( "VSyncWait", CDTXMania.ConfigIni.b垂直帰線待ちを行う,
				"垂直帰線同期：\n" +
				"画面の描画をディスプレイの垂直帰線中に行なう場合\n" +
                "には ON を指定します。\n" +
				"ON にすると、ガタつきのない滑らかな画面描画が\n" +
                "実現されます。",
				"Turn ON to wait VSync (Vertical Synchronizing signal) at every drawings. (so FPS becomes 60)\n" +
				"\n" +
				"If you have enough CPU/GPU power, the scroll would become smooth." );
			this.list項目リスト.Add( this.iSystemVSyncWait );
			this.iSystemAVI = new CItemToggle( "AVI", CDTXMania.ConfigIni.bAVI有効,
				"AVIの使用：\n" +
				"演奏中に動画(AVI)を再生する場合にON にします。\n" +
				"解像度が高い動画の場合、それなりのマシンパワーが\n" +
                "必要とされます。",
				"To use AVI playback or not." );
			this.list項目リスト.Add( this.iSystemAVI );
			this.iSystemWindowClipDisp = new CItemToggle( "MovieWindow", CDTXMania.ConfigIni.bWindowClipMode,
				"ムービークリップの小窓表示を追加します。" +
                "",
				"" );
			this.list項目リスト.Add( this.iSystemWindowClipDisp );
			this.iSystemForceAVIFullscreen = new CItemToggle( "FullAVI", CDTXMania.ConfigIni.bForceAVIFullscreen,
				"旧AVIの全画面表示：\n" +
				"旧仕様の動画(AVI)の表示を強制的に全画面化します。\n" +
				"BGAと併用している場合は、表示がおかしくなります。",
				"Forcely show the legacy-spec AVI to fullscreen.\n" +
				"If the data contains both AVI and BGA, the screen will corrupt." );
			this.list項目リスト.Add( this.iSystemForceAVIFullscreen );
			this.iSystemBGA = new CItemToggle( "BGA", CDTXMania.ConfigIni.bBGA有効,
				"BGAの使用：\n" +
				"演奏中に画像(BGA)を表示する場合にON にします。\n" +
				"BGA の再生には、それなりのマシンパワーが\n" +
                "必要とされます。",
				"To draw BGA (back ground animations) or not." );
			this.list項目リスト.Add( this.iSystemBGA );
			this.iSystemPreviewSoundWait = new CItemInteger( "PreSoundWait", 0, 0x2710, CDTXMania.ConfigIni.n曲が選択されてからプレビュー音が鳴るまでのウェイトms,
				"プレビュー音演奏までの時間：\n" +
				"曲にカーソルが合わされてからプレビュー音が\n" +
                "鳴り始めるまでの時間を指定します。\n" +
				"0 ～ 10000 [ms] が指定可能です。",
				"Delay time(ms) to start playing preview sound in SELECT MUSIC screen.\n" +
				"You can specify from 0ms to 10000ms." );
			this.list項目リスト.Add( this.iSystemPreviewSoundWait );
            //this.iSystemPreviewImageWait = new CItemInteger( "PreImageWait", 0, 0x2710, CDTXMania.ConfigIni.n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms,
            //    "プレビュー画像表示までの時間：\n" +
            //    "曲にカーソルが合わされてからプレビュー画像が\n" +
            //    "表示されるまでの時間を指定します。\n" +
            //    "0 ～ 10000 [ms] が指定可能です。",
            //    "Delay time(ms) to show preview image in SELECT MUSIC screen.\n" +
            //    "You can specify from 0ms to 10000ms." );
            //this.list項目リスト.Add( this.iSystemPreviewImageWait );
			this.iSystemDebugInfo = new CItemToggle( "Debug Info", CDTXMania.ConfigIni.b演奏情報を表示する,
				"演奏情報の表示：\n" +
				"演奏中、BGA領域の下部に演奏情報\n" +
                "（FPS、BPM、演奏時間など）を表示します。\n" +
				"また、小節線の横に小節番号が表示されるように\n" +
                "なります。",
				"To show song informations on playing BGA area. (FPS, BPM, total time etc)\n" +
				"You can ON/OFF the indications by pushing [Del] while playing drums, guitar or bass." );
			this.list項目リスト.Add( this.iSystemDebugInfo );
            //this.iSystemBGAlpha = new CItemInteger( "BG Alpha", 0, 0xff, CDTXMania.ConfigIni.n背景の透過度,
            //    "背景画像の透明割合：\n" +
            //    "背景画像をDTXManiaのフレーム画像と合成する際の、\n" +
            //    "背景画像の透明度を指定します。\n" +
            //    "0 が完全不透明で、255 が完全透明となります。",
            //    "The degree for transparing playing screen and wallpaper.\n" +
            //    "\n" +
            //    "0=no transparent, 255=completely transparency." );
            //this.list項目リスト.Add( this.iSystemBGAlpha );
			this.iSystemBGMSound = new CItemToggle( "BGM Sound", CDTXMania.ConfigIni.bBGM音を発声する,
				"BGMの再生：\n" +
				"これをOFFにすると、BGM を再生しなくなります。",
				"Turn OFF if you don't want to play BGM." );
			this.list項目リスト.Add( this.iSystemBGMSound );
			this.iSystemAudienceSound = new CItemToggle( "Audience", CDTXMania.ConfigIni.b歓声を発声する,
				"歓声の再生：\n" +
				"これをOFFにすると、歓声を再生しなくなります。",
				"Turn ON if you want to be cheered at the end of fill-in zone." );
			this.list項目リスト.Add( this.iSystemAudienceSound );
			this.iSystemDamageLevel = new CItemList( "DamageLevel", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eダメージレベル,
				"ゲージ減少割合：\n" +
				"Miss ヒット時のゲージの減少度合いを指定します。\n" +
				"Riskyが1以上の場合は無効となります。",
				"Damage level at missing (and recovering level) at playing.\n" +
				"This setting is ignored when Risky >= 1.",
				new string[] { "Small", "Normal", "Large" } );
			this.list項目リスト.Add( this.iSystemDamageLevel );
			this.iSystemSaveScore = new CItemToggle( "SaveScore", CDTXMania.ConfigIni.bScoreIniを出力する,
				"演奏記録の保存：\n" +
				"これをONにすると、演奏記録を ～.score.ini \n" +
                "ファイルに保存します。",
				"To save high-scores/skills, turn it ON.\n" +
				"Turn OFF in case your song data are in read-only media (CD-ROM etc).\n" +
				"Note that the score files also contain 'BGM Adjust' parameter.\n" +
				"So if you want to keep adjusting parameter, you need to set SaveScore=ON." );
			this.list項目リスト.Add( this.iSystemSaveScore );


			this.iSystemChipVolume = new CItemInteger( "ChipVolume", 0, 100, CDTXMania.ConfigIni.n手動再生音量,
				"打音の音量：\n" +
				"入力に反応して再生されるチップの音量を指定します。\n" +
				"0 ～ 100 % の値が指定可能です。",
				"The volumes for chips you hit.\n" +
				"You can specify from 0 to 100%." );
			this.list項目リスト.Add( this.iSystemChipVolume );
			this.iSystemAutoChipVolume = new CItemInteger( "AutoVolume", 0, 100, CDTXMania.ConfigIni.n自動再生音量,
				"自動再生音の音量：\n" +
				"自動的に再生されるチップの音量を指定します。\n" +
				"0 ～ 100 % の値が指定可能です。",
				"The volumes for AUTO chips.\n" +
				"You can specify from 0 to 100%." );
			this.list項目リスト.Add( this.iSystemAutoChipVolume );
            //this.iSystemStoicMode = new CItemToggle( "StoicMode", CDTXMania.ConfigIni.bストイックモード,
            //    "ストイック（禁欲）モード：\n" +
            //    "以下をまとめて表示ON/OFFします。\n" +
            //    "・プレビュー画像/動画\n" +
            //    "・リザルト画像/動画\n" +
            //    "・NowLoading画像\n" +
            //    "・演奏画面の背景画像\n" +
            //    "・BGA 画像 / AVI 動画\n" +
            //    "・グラフ画像",
            //    "Turn ON to disable drawing\n" +
            //    " * preview image / movie\n" +
            //    " * result image / movie\n" +
            //    " * nowloading image\n" +
            //    " * wallpaper (in playing screen)\n" +
            //    " * BGA / AVI (in playing screen)\n" +
            //    " * Graph bar" );
            //this.list項目リスト.Add( this.iSystemStoicMode );
			this.iSystemShowLag = new CItemList( "ShowLagTime", CItemBase.Eパネル種別.通常, CDTXMania.ConfigIni.nShowLagType,
				"ズレ時間表示：\n" +
				"ジャストタイミングからのズレ時間(ms)を表示します。\n" +
				" OFF: ズレ時間を表示しません。\n" +
				" ON: ズレ時間を表示します。\n" +
				" GREAT-: PERFECT以外の時のみ表示します。",
				"About displaying the lag from the \"just timing\".\n" +
				" OFF: Don't show it.\n" +
				" ON: Show it.\n" +
				" GREAT-: Show it except you've gotten PERFECT.",
				new string[] { "OFF", "ON", "GREAT-" } );
			this.list項目リスト.Add( this.iSystemShowLag );
			this.iSystemAutoResultCapture = new CItemToggle( "AutoSaveResult", CDTXMania.ConfigIni.bIsAutoResultCapture,
				"リザルト画像自動保存機能：\n" +
				"ONにすると、ハイスコア/ハイスキル時に、\n" +
                "自動的にリザルト画像を曲データと同じフォルダに\n" +
                "保存します。",
				"AutoSaveResult:\n" +
				"Turn ON to save your result screen image automatically when you get hiscore/hiskill." );
			this.list項目リスト.Add( this.iSystemAutoResultCapture );


			this.iSystemJudgeDispPriority = new CItemList( "JudgePriority", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.e判定表示優先度,
				"判定文字列とコンボ表示の優先順位を指定します。\n" +
				"\n" +
				" Under: チップの下に表示します。\n" +
				" Over:  チップの上に表示します。",
				"The display prioity between chips and judge mark/combo.\n" +
				"\n" +
				" Under: Show them under the chips.\n" +
				" Over:  Show them over the chips.",
				new string[] { "Under", "Over" } );
			this.list項目リスト.Add( this.iSystemJudgeDispPriority );

			this.iSystemBufferedInput = new CItemToggle( "BufferedInput", CDTXMania.ConfigIni.bバッファ入力を行う,
				"バッファ入力モード：\n" +
				"ON にすると、FPS を超える入力解像度を実現します。\n" +
				"OFF にすると、入力解像度は FPS に等しくなります。",
				"To select joystick input method.\n" +
				"\n" +
				"ON to use buffer input. No lost/lags.\n" +
				"OFF to use realtime input. It may causes lost/lags for input.\n" +
				"Moreover, input frequency is synchronized with FPS." );
			this.list項目リスト.Add( this.iSystemBufferedInput );
			this.iLogOutputLog = new CItemToggle( "TraceLog", CDTXMania.ConfigIni.bログ出力,
				"Traceログ出力：\n" +
				"DTXManiaLog.txt にログを出力します。\n" +
				"この設定の変更は、DTXManiaの再起動後に\n" +
                "有効となります。",
				"Turn ON to put debug log to DTXManiaLog.txt.\n" +
				"To take it effective, you need to re-open DTXMania." );
			this.list項目リスト.Add( this.iLogOutputLog );

            this.iSystemSkillMode = new CItemList( "SkillMode", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eSkillMode,
                "スキルモード：\n" +
                "達成率、ランク、スコアの計算方法を設定します。\n" +
                " DTXMania: DTXManiaの計算方法です。\n" +
                " XG:          XG以降の計算方法です。\n",
                new string[] { "DTXMania", "XG" } );
            this.list項目リスト.Add( this.iSystemSkillMode );

            #region [ XGオプション ]
            //this.iSystemSpeaker = new CItemToggle("Speaker", CDTXMania.ConfigIni.bSpeaker,
            //    "スピーカーの表示を設定します。",
            //    "");
            //this.list項目リスト.Add(this.iSystemSpeaker);

            //this.iDrumsMoveDrumSet = new CItemList("DrumSetMove", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eドラムセットを動かす,
            //    "ドラムセットが動くかを設定します。\n" +
            //    "ON: ドラムセットを動かします。\n" +
            //    "OFF: ドラムセットを動かしません。\n"+
            //    "NONE: ドラムセットを表示しません。",
            //    "Set up a DrumSet Moves.",
            //    new string[] { "ON", "OFF", "NONE" });
            //this.list項目リスト.Add(this.iDrumsMoveDrumSet);

            //this.iSystemLivePoint = new CItemToggle("LivePoint", CDTXMania.ConfigIni.bLivePoint,
            //    "LivePointゲージの表示を設定します。\n",
            //    "\n" +
            //    "");
            //this.list項目リスト.Add(this.iSystemLivePoint);

            //this.iSystemBPMbar = new CItemList("BPMBar", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eBPMbar,
            //    "左右のBPMに連動して動くバーの表示\n" +
            //    "位置を変更します。\n" +
            //    "ON: 左右両方表示します。\n" +
            //    "L Only: 左のみ表示します。\n" +
            //    "OFF: 動くバーを表示しません。\n"+
            //    "NONE: BPMバーを表示しません。",
            //    "To change the displaying position of\n" +
            //    "the ride cymbal.",
            //    new string[] { "ON", "L Only", "OFF", "NONE" });
            //this.list項目リスト.Add(this.iSystemBPMbar);

            this.iSystemNamePlateType = new CItemList("NamePlateType", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eNamePlateType,
                "演奏画面の構成を変更します。\n" +
                " Type-A: XG2風の表示です。\n" +
                " Type-B: XG1風の表示です。\n",
                "Change the configuration playing screen\n" +
                " Type-A: XG2\n" +
                " Type-B: XG1\n",
                new string[] { "Type-A", "Type-B" });
            this.list項目リスト.Add(this.iSystemNamePlateType);

			this.iSystemJudgeCountDisp = new CItemToggle( "JudgeCountDisp", CDTXMania.ConfigIni.bJudgeCountDisp,
				"判定数表示：\n" +
				"ONにするとSkillMater上に現在の判定数を\n" +
                "表示します。\n" +
                "SkillMaterが無効の場合は表示されません。",
				"JudgeCountDisplayMode." );
			this.list項目リスト.Add( this.iSystemJudgeCountDisp );
            #endregion

			#region [ WASAPI / ASIO ]
			// #24820 2013.1.3 yyagi
			this.iSystemSoundType = new CItemList( "SoundType", CItemList.Eパネル種別.通常, CDTXMania.ConfigIni.nSoundDeviceType,
				"サウンドの出力方式:\n" +
				"WASAPI, ASIO, DSound(DirectSound)の中から\n" +
                "サウンド出力方式を選択します。\n" +
				"WASAPIはVista以降でのみ使用可能です。\n" +
                "ASIOはXPでも使用可能ですが、対応機器でのみ\n" +
                "使用できます。\n" +
				"WASAPIかASIOを指定することで、\n" +
                "遅延の少ない演奏を楽しむことができます。\n" +
				"\n" +
				"※ 設定はCONFIGURATION画面の終了時に\n" +
                "有効になります。",
				"Sound output type:\n" +
				"You can choose WASAPI, ASIO or DShow(DirectShow).\n" +
				"WASAPI can use only after Vista.\n" +
				"ASIO can use on XP or later OSs, but you need \"ASIO-supported\" sound device.\n" +
				"\n" +
				"You should use WASAPI or ASIO to decrease the sound lag.\n" +
				"\n" +
				"Note: Exit CONFIGURATION to make the setting take effect.",
				new string[] { "DSound", "ASIO", "WASAPI排他", "WASAPI共有" } );
			this.list項目リスト.Add( this.iSystemSoundType );

			// #24820 2013.1.15 yyagi
			this.iSystemWASAPIBufferSizeMs = new CItemInteger( "WASAPI BufferSize", 0, 99999, CDTXMania.ConfigIni.nWASAPIBufferSizeMs,
			    "WASAPI使用時のバッファサイズ:\n" +
			  //  "0～99999ms を指定可能です。\n" +
			    "0を指定すると、設定可能な最小のバッファサイズを\n" +
                "自動設定します。\n" +
				"1以上を指定すると、その値以上で指定可能な\n" +
                "最小のバッファサイズを自動設定します。\n" +
			    "値を小さくするほど発音遅延が減少しますが、\n" +
                "音割れや性能低下などの問題が発生する場合が\n" +
                "あります。\n" +
				"タブレットなど性能の低いPCを使う場合は、\n" +
                "手動で大きめの値を指定してください。\n" +
				"\n" +
			    "※ 設定はCONFIGURATION画面の終了時に\n" +
				"　有効になります。",
			    "Sound buffer size for WASAPI:\n" +
			    "You can set from 0 to 99999ms.\n" +
			    "Set 0 to use a minimum buffer size automatically.\n" +
			    "Smaller value makes smaller lag, but it may cause sound troubles. " +
				"So if you use poor CPU PC (tablet etc), please specify a little bigger value.\n" +
			    "\n" +
			    "Note: Exit CONFIGURATION to make the setting take effect." );
			this.list項目リスト.Add( this.iSystemWASAPIBufferSizeMs );

			// #24820 2013.1.17 yyagi
			string[] asiodevs = CEnumerateAllAsioDevices.GetAllASIODevices();
			this.iSystemASIODevice = new CItemList( "ASIO device", CItemList.Eパネル種別.通常, CDTXMania.ConfigIni.nASIODevice,
				"ASIOデバイス:\n" +
				"ASIO使用時のサウンドデバイスを選択します。\n" +
				"\n" +
				"※ 設定はCONFIGURATION画面の終了時に\n" +
                "有効になります。",
				"ASIO Sound Device:\n" +
				"Select the sound device to use under ASIO mode.\n" +
				"\n" +
				"Note: Exit CONFIGURATION to make the setting take effect.",
				asiodevs );
			this.list項目リスト.Add( this.iSystemASIODevice );

			// #24820 2013.1.3 yyagi
			//this.iSystemASIOBufferSizeMs = new CItemInteger("ASIOBuffSize", 0, 99999, CDTXMania.ConfigIni.nASIOBufferSizeMs,
			//    "ASIO使用時のバッファサイズ:\n" +
			//    "0～99999ms を指定可能です。\n" +
			//    "推奨値は0で、サウンドデバイスでの\n" +
			//    "設定値をそのまま使用します。\n" +
			//    "(サウンドデバイスのASIO設定は、\n" +
			//    " ASIO capsなどで行います)\n" +
			//    "値を小さくするほど発音ラグが\n" +
			//    "減少しますが、音割れや異常動作を\n" +
			//    "引き起こす場合があります。\n" +
			//    "\n" +
			//    "※ 設定はCONFIGURATION画面の\n" +
			//    "　終了時に有効になります。",
			//    "Sound buffer size for ASIO:\n" +
			//    "You can set from 0 to 99999ms.\n" +
			//    "You should set it to 0, to use\n" +
			//    "a default value specified to\n" +
			//    "the sound device.\n" +
			//    "Smaller value makes smaller lag,\n" +
			//    "but it may cause sound troubles.\n" +
			//    "\n" +
			//    "Note: Exit CONFIGURATION to make\n" +
			//    "     the setting take effect." );
			//this.list項目リスト.Add( this.iSystemASIOBufferSizeMs );

			// #33689 2014.6.17 yyagi
			this.iSystemSoundTimerType = new CItemToggle( "UseOSTimer", CDTXMania.ConfigIni.bUseOSTimer,
				"OSタイマーを使用するかどうか:\n" +
				"演奏タイマーとして、DTXMania独自のタイマーを使うか\n" +
                "OS標準のタイマーを使うかを選択します。\n" +
				"OS標準タイマーを使うとスクロールが滑らかに\n" +
                "なりますが、演奏で音ズレが発生することが\n" +
                "あります。\n" +
                "(そのためAdjustWavesの効果が適用されます。)\n" +
				"\n" +
				"この指定はWASAPI/ASIO使用時のみ有効です。\n",
				"Use OS Timer or not:\n" +
				"If this settings is ON, DTXMania uses OS Standard timer. It brings smooth scroll, but may cause some sound lag.\n" +
				"(so AdjustWaves is also avilable)\n" +
				"\n" +
				"If OFF, DTXMania uses its original timer and the effect is vice versa.\n" +
				"\n" +
				"This settings is avilable only when you use WASAPI/ASIO.\n"
			);
			this.list項目リスト.Add( this.iSystemSoundTimerType );
			#endregion
			// #33700 2013.1.3 yyagi
			this.iSystemMasterVolume = new CItemInteger( "MasterVolume", 0, 100, CDTXMania.ConfigIni.nMasterVolume,
				"マスターボリュームの設定:\n" +
				"全体の音量を設定します。\n" +
				"0が無音で、100が最大値です。\n" +
				"(WASAPI/ASIO時のみ有効です)",
				"Master Volume:\n" +
				"You can set 0 - 100.\n" +
				"\n" +
				"Note:\n" +
				"Only for WASAPI/ASIO mode." );
			this.list項目リスト.Add( this.iSystemMasterVolume );

            this.iSystemWASAPIEventDriven = new CItemToggle("WASAPIEventDriven", CDTXMania.ConfigIni.bEventDrivenWASAPI,
                "WASAPIをEvent Drivenモードで使用します。\n" +
                "これを使うと、サウンド出力の遅延をより小さくできますが、システム負荷は上昇します。",
                "Use WASAPI Event Driven mode.\n" +
                "It reduce sound output lag, but it also decreases system performance.");
            this.list項目リスト.Add( this.iSystemWASAPIEventDriven );

			this.iSystemSkinSubfolder = new CItemList( "Skin (General)", CItemBase.Eパネル種別.通常, nSkinIndex,
				"スキン切替：スキンを切り替えます。\n",
				//"CONFIGURATIONを抜けると、設定した\n" +
				//"スキンに変更されます。",
				"Skin:\n" +
				"Change skin.",
				skinNames );
			this.list項目リスト.Add( this.iSystemSkinSubfolder );
			this.iSystemUseBoxDefSkin = new CItemToggle( "Skin (Box)", CDTXMania.ConfigIni.bUseBoxDefSkin,
				"Music boxスキンの利用：\n" +
				"特別なスキンが設定されたMusic boxに出入りしたとき\n" +
                "に、自動でスキンを切り替えるかどうかを設定します。\n",
				//"\n" +
				//"(Music Boxスキンは、box.defファイル\n" +
				//" で指定できます)\n",
				"Box skin:\n" +
				"Automatically change skin specified in box.def file." );
			this.list項目リスト.Add( this.iSystemUseBoxDefSkin );

            // #36372 2016.06.19 kairera0467
			this.iSystemBGMAdjust = new CItemInteger( "BGMAdjust", -99, 99, CDTXMania.ConfigIni.nCommonBGMAdjustMs,
				"BGMの再生タイミングの微調整を行います。\n" +
				"-99 ～ 99ms まで指定可能です。\n" +
                "値を指定してください。\n",
				"To adjust the BGM play timing.\n" +
				"You can set from -99 to 0ms.\n" );
			this.list項目リスト.Add( this.iSystemBGMAdjust );

			this.iSystemGoToKeyAssign = new CItemBase( "System Keys", CItemBase.Eパネル種別.通常,
			"システムのキー入力に関する項目を設定します。",
			"Settings for the system key/pad inputs." );
			this.list項目リスト.Add( this.iSystemGoToKeyAssign );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.System;
		}

		#endregion
		#region [ t項目リストの設定_Drums() ]
		public void t項目リストの設定_Drums()
		{
			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iDrumsReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iDrumsReturnToMenu );

			#region [ AutoPlay ]
			this.iDrumsAutoPlayAll = new CItemThreeState( "AutoPlay (All)", CItemThreeState.E状態.不定,
				"全パッドの自動演奏の ON/OFF を\n" +
                "まとめて切り替えます。",
				"You can change whether Auto or not for all drums lanes at once." );
			this.list項目リスト.Add( this.iDrumsAutoPlayAll );

			this.iDrumsLeftCymbal = new CItemToggle( "    LeftCymbal", CDTXMania.ConfigIni.bAutoPlay.LC,
				"左シンバルを自動で演奏します。",
				"To play LeftCymbal automatically." );
			this.list項目リスト.Add( this.iDrumsLeftCymbal );

			this.iDrumsHiHat = new CItemToggle( "    HiHat", CDTXMania.ConfigIni.bAutoPlay.HH,
				"ハイハットを自動で演奏します。\n" +
				"（クローズ、オープンとも）",
				"To play HiHat automatically.\n" +
				"(It effects to both HH-close and HH-open)" );
			this.list項目リスト.Add( this.iDrumsHiHat );

			this.iDrumsSnare = new CItemToggle( "    Snare", CDTXMania.ConfigIni.bAutoPlay.SD,
				"スネアを自動で演奏します。",
				"To play Snare automatically." );
			this.list項目リスト.Add( this.iDrumsSnare );

			this.iDrumsBass = new CItemToggle( "    BassDrum", CDTXMania.ConfigIni.bAutoPlay.BD,
				"バスドラムを自動で演奏します。",
				"To play Bass Drum automatically." );
			this.list項目リスト.Add( this.iDrumsBass );

			this.iDrumsHighTom = new CItemToggle( "    HighTom", CDTXMania.ConfigIni.bAutoPlay.HT,
				"ハイタムを自動で演奏します。",
				"To play High Tom automatically." );
			this.list項目リスト.Add( this.iDrumsHighTom );

			this.iDrumsLowTom = new CItemToggle( "    LowTom", CDTXMania.ConfigIni.bAutoPlay.LT,
				"ロータムを自動で演奏します。",
				"To play Low Tom automatically." );
			this.list項目リスト.Add( this.iDrumsLowTom );

			this.iDrumsFloorTom = new CItemToggle( "    FloorTom", CDTXMania.ConfigIni.bAutoPlay.FT,
				"フロアタムを自動で演奏します。",
				"To play Floor Tom automatically." );
			this.list項目リスト.Add( this.iDrumsFloorTom );

			this.iDrumsCymbalRide = new CItemToggle( "    Cym/Ride", CDTXMania.ConfigIni.bAutoPlay.CY,
				"右シンバルとライドシンバルを自動で演奏します。",
				"To play both right- and Ride-Cymbal automatically." );
			this.list項目リスト.Add( this.iDrumsCymbalRide );

			this.iDrumsLeftPedal = new CItemToggle( "    LeftPedal", CDTXMania.ConfigIni.bAutoPlay.LP,
				"ハイハットペダルを自動で演奏します。",
				"To play LeftPedal automatically." );
			this.list項目リスト.Add( this.iDrumsLeftPedal );

			this.iDrumsLeftBassDrum = new CItemToggle( "    LeftBassDrum", CDTXMania.ConfigIni.bAutoPlay.LBD,
				"左バスドラムを自動で演奏します。",
				"To play LeftBassDrum automatically." );
			this.list項目リスト.Add( this.iDrumsLeftBassDrum );
			#endregion


			this.iDrumsScrollSpeed = new CItemInteger( "ScrollSpeed", 0, 0x7cf, CDTXMania.ConfigIni.n譜面スクロール速度.Drums,
				"演奏時のドラム譜面のスクロールの速度を指定します。\n" +
				"x0.5 ～ x1000.0 を指定可能です。",
				"To change the scroll speed for the drums lanes.\n" +
				"You can set it from x0.5 to x1000.0.\n" +
				"(ScrollSpeed=x0.5 means half speed)" );
			this.list項目リスト.Add( this.iDrumsScrollSpeed );

			#region [ SudHid ]
			this.iDrumsSudHid = new CItemList( "Sud+Hid", CItemBase.Eパネル種別.通常, getDefaultSudHidValue( E楽器パート.DRUMS ),
				"ドラムチップの表示方式:\n" +
				"OFF:　　チップを常に表示します。\n" +
				"Sudden: チップがヒットバー付近に来るまで表示\n" +
				"　　　　されなくなります。\n" +
				"Hidden: チップがヒットバー付近で表示されなく\n" +
				"　　　　なります。\n" +
				"Sud+Hid: SuddenとHiddenの効果を同時にかけます。\n" +
				"S(emi)-Invisible:\n" +
				"　　　　通常はチップを透明にしますが、Poor /\n" +
				"　　　　Miss時にはしばらく表示します。\n" +
				"F(ull)-Invisible:\n" +
				"　　　　チップを常に透明にします。暗譜での練習\n" +
				"　　　　にお使いください。",
				"Drums chips display type:\n" +
				"\n" +
				"OFF:    Always show chips.\n" +
				"Sudden: The chips are disappered until they\n" +
				"        come near the hit bar, and suddenly\n" +
				"        appears.\n" +
				"Hidden: The chips are hidden by approaching to\n" +
				"        the hit bar.\n" +
				"Sud+Hid: Both Sudden and Hidden.\n" +
				"S(emi)-Invisible:\n" +
				"        Usually you can't see the chips except\n" +
				"        you've gotten Poor/Miss.\n" +
				"F(ull)-Invisible:\n" +
				"        You can't see the chips at all.",
				new string[] { "OFF", "Sudden", "Hidden", "Sud+Hid", "S-Invisible", "F-Invisible" } );
			this.list項目リスト.Add( this.iDrumsSudHid );
			#endregion

            this.iDrumsDark = new CItemList("       Dark", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eDark,
                "レーン表示のオプションをまとめて切り替えます。\n"+
                "HALF: レーンが表示されなくなります。\n"+
                "FULL: さらに小節線、拍線、判定ラインも\n"+
                "表示されなくなります。",
                "OFF: all display parts are shown.\nHALF: lanes and gauge are\n disappeared.\nFULL: additionaly to HALF, bar/beat\n lines, hit bar are disappeared.",
                new string[] { "OFF", "HALF", "FULL" });
            this.list項目リスト.Add(this.iDrumsDark);

            this.iDrumsLaneDispType = new CItemList("LaneDisp", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.nLaneDispType.Drums,
                "レーンの縦線と小節線の表示を切り替えます。\n" +
                "ALL  ON :レーン背景、小節線を表示します。\n" +
                "LANE OFF:レーン背景を表示しません。\n" +
                "LINE OFF:小節線を表示しません。\n"+
                "ALL  OFF:レーン背景、小節線を表示しません。",
                "",
                new string[] { "ALL ON", "LANE OFF", "LINE OFF", "ALL OFF"});
            this.list項目リスト.Add(this.iDrumsLaneDispType);

            this.iDrumsJudgeLineDisp = new CItemToggle("JudgeLineDisp", CDTXMania.ConfigIni.bJudgeLineDisp.Drums,
                "判定ラインの表示 / 非表示を切り替えます。",
                "Toggle JudgeLine");
            this.list項目リスト.Add(this.iDrumsJudgeLineDisp);

            this.iDrumsLaneFlush = new CItemToggle("LaneFlush", CDTXMania.ConfigIni.bLaneFlush.Drums,
                "レーンフラッシュの表示 / 非表示を\n" +
                 "切り替えます。",
                "Toggle LaneFlush");
            this.list項目リスト.Add(this.iDrumsLaneFlush);

            if( !CDTXMania.bXGRelease )
            {
                this.iDrumsMatixxFrameDisp = new CItemToggle( "FrameDisp", CDTXMania.ConfigIni.bフレームを表示する,
                    "フレームの表示 / 非表示を\n" +
                     "切り替えます。",
                    "Toggle FrameDisp" );
                this.list項目リスト.Add( this.iDrumsMatixxFrameDisp );
            }

			this.iDrumsReverse = new CItemToggle( "Reverse", CDTXMania.ConfigIni.bReverse.Drums,
				"ドラムチップが譜面の下から上に流れるようになります。",
				"The scroll way is reversed. Drums chips flow from the bottom to the top." );
			this.list項目リスト.Add( this.iDrumsReverse );

			this.iSystemRisky = new CItemInteger( "Risky", 0, 10, CDTXMania.ConfigIni.nRisky,
				"Riskyモードの設定:\n" +
				"1以上の値にすると、その回数分のPoor/Missで\n" +
                "FAILEDとなります。\n" +
				"0にすると無効になり、DamageLevelに従った\n" +
                "ゲージ増減となります。\n" +
				"\n" +
				"なお、この設定は、StageFailedの設定と併用できます。",
				"Risky mode:\n" +
				"It means the Poor/Miss times to be FAILED.\n" +
				"Set 0 to disable Risky mode." );
			this.list項目リスト.Add( this.iSystemRisky );

			this.iDrumsTight = new CItemToggle( "Tight", CDTXMania.ConfigIni.bTight,
				"ドラムチップのないところでパッドを叩くとミスになります",
				"It becomes MISS to hit pad without chip." );
			this.list項目リスト.Add( this.iDrumsTight );

            this.iDrumsJust = new CItemList( "JUST", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eJUST.Drums,
                "ON   :PERFECT以外の判定を全てミス扱いにします。\n" +
                "GREAT:GOOD以下の判定を全てミス扱いにします。\n",
                "",
                new string[] { "OFF", "ON", "GREAT" });
            this.list項目リスト.Add( this.iDrumsJust );

			#region [ Position ]
            #region[ LaneType ]
            this.iDrumsLaneType = new CItemList("LaneType", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eLaneType,
                "ドラムのレーンの配置を変更します。\n" +
                "Type-A 通常の設定です。\n"+
                "Type-B 2ペダルとタムをそれぞれま\n"+
                "とめた表示です。\n"+
                "Type-C 3タムのみをまとめた表示です。\n"+
                "Type-D 左右完全対象の表示です。",
                "To change the displaying position of\n" +
                "Drum Lanes.\n"+
                "Type-A default\n" +
                "Type-B Summarized 2 pedals and Toms.\n"+
                "Type-C Summarized 3 Toms only.\n"+
                "Type-D Work In Progress....",
                new string[] { "Type-A", "Type-B", "Type-C", "Type-D"});
            this.list項目リスト.Add(this.iDrumsLaneType);
            #endregion
			#region [ DrumsLanePosition ]
            //this.iDrumsLanePosition = new CItemList( "LanePosition", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eドラムレーン表示位置,
            //    "ドラムレーンの位置を指定します。\n" + 
            //    "\n" +
            //    "Left:   画面の左側にドラムレーンを表示します。\n" +
            //    "Center: 画面の中央にドラムレーンを表示します。\n",
            //    "The display position for Drums Lane." +
            //    "\n" +
            //    " Left:   Drums lanes are shown in the left of screen.\n" +
            //    " Center: Drums lanes are shown in the center of screen.",
            //    //"Note that it doesn't take effect at Autoplay ([Left] is forcely used).",
            //    new string[] { "Left", "Center" } );
            //this.list項目リスト.Add( this.iDrumsLanePosition );
			#endregion
			#region [ ComboPosition ]
            //this.iDrumsComboPosition = new CItemList( "ComboPosition", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.ドラムコンボ文字の表示位置,
            //    "演奏時のドラムコンボ文字列の位置を指定します。",
            //    "The display position for Drums Combo.",
            //    //"Note that it doesn't take effect at Autoplay ([Left] is forcely used).",
            //    new string[] { "Left", "Center", "Right", "OFF" } );
            //this.list項目リスト.Add( this.iDrumsComboPosition );
			#endregion
			#region [ Position ]
			this.iDrumsPosition = new CItemList( "Position", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.判定文字表示位置.Drums,
				"ドラムの判定文字の表示位置を指定します。\n" +
				"  P-A: レーン上\n" +
				"  P-B: 判定ライン下\n" +
				"  OFF: 表示しない",
				"The position to show judgement mark.\n" +
				"(Perfect, Great, ...)\n" +
				"\n" +
				" P-A: on the lanes.\n" +
				" P-B: under the hit bar.\n" +
				" OFF: no judgement mark.",
				new string[] { "OFF", "P-A", "P-B" } );
			this.list項目リスト.Add( this.iDrumsPosition );
			#endregion

            this.iDrumsRDPosition = new CItemList("RDPosition", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eRDPosition,
                "ライドシンバルレーンの表示\n" +
                "位置を変更します。\n"+
                "RD RC:最右端はRCレーンになります\n"+
                "RC RD: 最右端はRDレーンになります",
                "To change the displaying position of\n" +
                "the ride cymbal.",
                new string[] { "RD RC", "RC RD" });
            this.list項目リスト.Add(this.iDrumsRDPosition);
            #endregion
			#region [ Group ]
			this.iSystemHHGroup = new CItemList( "HH Group", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eHHGroup,
				"ハイハットレーン打ち分け設定：\n" +
				"左シンバル、ハイハットオープン、\n" +
                "ハイハットクローズの打ち分け方法を指定します。\n" +
				"  HH-0 ... LC | HHC | HHO\n" +
				"  HH-1 ... LC & ( HHC | HHO )\n" +
				"  HH-2 ... LC | ( HHC & HHO )\n" +
				"  HH-3 ... LC & HHC & HHO\n" +
				"\n",
				"HH-0: LC|HC|HO;\n" +
				" all are separated.\n" +
				"HH-1: LC&(HC|HO);\n" +
				" HC and HO are separted.\n" +
				" LC is grouped with HC and HHO.\n" +
				"HH-2: LC|(HC&HO);\n" +
				" LC and HHs are separated.\n" +
				" HC and HO are grouped.\n" +
				"HH-3: LC&HC&HO; all are grouped.\n" +
				"\n",
				new string[] { "HH-0", "HH-1", "HH-2", "HH-3" } );
			this.list項目リスト.Add( this.iSystemHHGroup );

			this.iSystemFTGroup = new CItemList( "FT Group", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eFTGroup,
				"フロアタム打ち分け設定：\n" +
				"ロータムとフロアタムの打ち分け方法を指定します。\n" +
				"  FT-0 ... LT | FT\n" +
				"  FT-1 ... LT & FT\n",
				"FT-0: LT|FT\n" +
				" LT and FT are separated.\n" +
				"FT-1: LT&FT\n" +
				" LT and FT are grouped.",
				new string[] { "FT-0", "FT-1" } );
			this.list項目リスト.Add( this.iSystemFTGroup );

			this.iSystemCYGroup = new CItemList( "CY Group", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eCYGroup,
				"シンバルレーン打ち分け設定：\n" +
				"右シンバルとライドシンバルの打ち分け方法を\n" +
                "指定します。\n" +
				"  CY-0 ... CY | RD\n" +
				"  CY-1 ... CY & RD\n",
				"CY-0: CY|RD\n" +
				" CY and RD are separated.\n" +
				"CY-1: CY&RD\n" +
				" CY and RD are grouped.",
				new string[] { "CY-0", "CY-1" } );
			this.list項目リスト.Add( this.iSystemCYGroup );

            this.iSystemBDGroup = new CItemList("BD Group", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eBDGroup,		// #27029 2012.1.4 from
                "フットペダル打ち分け設定：\n" +
                "左ペダル、左バスドラ、右バスドラの打ち分け\n" +
                "方法を指定します。\n" +
                "  BD-0 ... LP | LBD | BD\n" +
                "  BD-1 ... LP | LBD & BD\n" +
                "  BD-2 ... LP & LBD | BD\n" +
                "  BD-3 ... LP & LBD & BD\n",
                new string[] { "BD-0", "BD-1", "BD-2", "BD-3" });
            this.list項目リスト.Add(this.iSystemBDGroup);
			#endregion

			this.iSystemCymbalFree = new CItemToggle( "CymbalFree", CDTXMania.ConfigIni.bシンバルフリー,
				"シンバルフリーモード：\n" +
				"左シンバル・右シンバルの区別をなくします。\n" +
                "ライドシンバルまで区別をなくすか否かは、\n" +
                "CYGroup に従います。\n",
				"Turn ON to group LC (left cymbal) and CY (right cymbal).\n" +
				"Whether RD (ride cymbal) is also grouped or not depends on the 'CY Group' setting." );
			this.list項目リスト.Add( this.iSystemCymbalFree );

			#region [ SoundPriority ]
			this.iSystemHitSoundPriorityHH = new CItemList( "HH Priority", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eHitSoundPriorityHH,
				"発声音決定の優先順位：\n" +
				"ハイハットレーン打ち分け有効時に、チップの発声音を\n" +
                "どのように決定するかを指定します。\n" +
				"  C > P ... チップの音が優先\n" +
				"  P > C ... 叩いたパッドの音が優先\n" +
				"\n" +
				"※BD Group が BD-1 である場合、\n" +
                "この項目は変更できません。\n",
				"To specify playing sound in case you're using HH-0,1 and 2.\n" +
				"\n" +
				"C>P:\n" +
				" Chip sound is prior to the pad sound.\n" +
				"P>C:\n" +
				" Pad sound is prior to the chip sound.\n" +
				"\n" +
				"* This value cannot be changed while BD Group is set as BD-1.",
				new string[] { "C>P", "P>C" } );
			this.list項目リスト.Add( this.iSystemHitSoundPriorityHH );

			this.iSystemHitSoundPriorityFT = new CItemList( "FT Priority", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eHitSoundPriorityFT,
				"発声音決定の優先順位：\n" +
				"フロアタム打ち分け有効時に、チップの発声音を\n" +
                "どのように決定するかを指定します。\n" +
				"  C > P ... チップの音が優先\n" +
				"  P > C ... 叩いたパッドの音が優先",
				"To specify playing sound in case you're using FT-0.\n" +
				"\n" +
				"C>P:\n" +
				" Chip sound is prior to the pad sound.\n" +
				"P>C:\n" +
				" Pad sound is prior to the chip sound.",
				new string[] { "C>P", "P>C" } );
			this.list項目リスト.Add( this.iSystemHitSoundPriorityFT );

			this.iSystemHitSoundPriorityCY = new CItemList( "CY Priority", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eHitSoundPriorityCY,
				"発声音決定の優先順位：\n" +
				"シンバルレーン打ち分け有効時に、チップの発声音を\n" +
                "どのように決定するかを指定します。\n" +
				"  C > P ... チップの音が優先\n" +
				"  P > C ... 叩いたパッドの音が優先",
				"To specify playing sound in case you're using CY-0.\n" +
				"\n" +
				"C>P:\n" +
				" Chip sound is prior to the pad sound.\n" +
				"P>C:\n" +
				" Pad sound is prior to the chip sound.",
				new string[] { "C>P", "P>C" } );
			this.list項目リスト.Add( this.iSystemHitSoundPriorityCY );
			#endregion

			this.iSystemFillIn = new CItemToggle( "FillIn", CDTXMania.ConfigIni.bフィルイン有効,
				"フィルインエフェクトの使用：\n" +
				"フィルイン区間の爆発パターンに特別なエフェクトを\n" +
                "使用します。",
				"To show bursting effects at the fill-in zone." );
			this.list項目リスト.Add( this.iSystemFillIn );

            this.iDrumsShutterImage = new CItemList( "ShutterImage", CItemBase.Eパネル種別.通常, this.nCurrentShutterImage.Drums,
                "シャッター画像の絵柄を選択できます。",
                this.shutterNames
              );
            this.list項目リスト.Add( this.iDrumsShutterImage );

			this.iSystemHitSound = new CItemToggle( "HitSound", CDTXMania.ConfigIni.bドラム打音を発声する,
				"打撃音の再生：\n" +
				"これをOFFにすると、パッドを叩いたときの音を\n" +
                "再生しなくなります（ドラムのみ）。\n" +
				"電子ドラム本来の音色で演奏したい場合などに\n" +
                "OFFにします。\n" +
				"\n" +
				"注意：BD Group が BD-1 である場合は\n" +
                "不具合が生じます。\n",
				"Turn OFF if you don't want to play hitting chip sound.\n" +
				"It is useful to play with real/electric drums kit.\n" +
				"\n" +
				"Warning: You should not use BD Group BD-1 with HitSound OFF.\n" );
			this.list項目リスト.Add( this.iSystemHitSound );

			this.iSystemSoundMonitorDrums = new CItemToggle( "DrumsMonitor", CDTXMania.ConfigIni.b演奏音を強調する.Drums,
				"ドラム音モニタ：\n" +
				"ドラム音を他の音より大きめの音量で発声します。\n" +
				"ただし、オートプレイの場合は通常音量で発声されます",
				"To enhance the drums chip sound (except autoplay)." );
			this.list項目リスト.Add( this.iSystemSoundMonitorDrums );

			this.iSystemMinComboDrums = new CItemInteger( "D-MinCombo", 1, 0x1869f, CDTXMania.ConfigIni.n表示可能な最小コンボ数.Drums,
				"表示可能な最小コンボ数（ドラム）：\n" +
				"画面に表示されるコンボの最小の数を指定します。\n" +
				"1 ～ 99999 の値が指定可能です。",
				"Initial number to show the combo for the drums.\n" +
				"You can specify from 1 to 99999." );
			this.list項目リスト.Add( this.iSystemMinComboDrums );

            this.iDrumsJudgeLinePos = new CItemInteger("JudgeLinePos", 0, 100, CDTXMania.ConfigIni.nJudgeLine.Drums,
                "演奏時の判定ラインの高さを変更します。\n" +
                "0～100の間で指定できます。",
                "To change the judgeLinePosition for the\n" +
                "You can set it from 0 to 100." );
            this.list項目リスト.Add(this.iDrumsJudgeLinePos);

            this.iDrumsJudgeLineOffset = new CItemInteger("JudgeLineOffset", -99, 99, CDTXMania.ConfigIni.nJudgeLinePosOffset.Drums,
                "演奏時の判定ラインのY座標の位置を変更します。\n" +
                "-99 ～ 99pxの間で指定できます。",
                "To change the judgeLinePosition for the\n" +
                "You can set it from 0 to 100." );
            this.list項目リスト.Add( this.iDrumsJudgeLineOffset );

            this.iDrumsShutterInPos = new CItemInteger( "ShutterInPos", 0, 100, CDTXMania.ConfigIni.nShutterInSide.Drums,
                "演奏時のノーツが現れる側のシャッターの\n" +
                "位置を変更します。",
                "To change the InsideShutter for the\n" +
                "You can set it from 0 to 100." );
            this.list項目リスト.Add( this.iDrumsShutterInPos );

            this.iDrumsShutterOutPos = new CItemInteger( "ShutterOutPos", 0, 100, CDTXMania.ConfigIni.nShutterOutSide.Drums,
                "演奏時のノーツが消える側のシャッターの\n" +
                "位置を変更します。",
                "To change the OutsideShutter for the\n" +
                "You can set it from 0 to 100." );
            this.list項目リスト.Add( this.iDrumsShutterOutPos );

			// #23580 2011.1.3 yyagi
			this.iDrumsInputAdjustTimeMs = new CItemInteger( "InputAdjust", -99, 99, CDTXMania.ConfigIni.nInputAdjustTimeMs.Drums,
				"ドラムの入力タイミングの微調整を行います。\n" +
				"-99 ～ 99ms まで指定可能です。\n" +
				"入力ラグを軽減するためには、\n" +
                "負の値を指定してください。",
				"To adjust the drums input timing.\n" +
				"You can set from -99 to 99ms.\n" +
				"To decrease input lag, set minus value." );
			this.list項目リスト.Add( this.iDrumsInputAdjustTimeMs );

            // #39397 2019.07.19 kairera0467
            this.iDrumsPedalJudgeRangeDelta = new CItemInteger( "PedalRangeAdj", 0, 200, CDTXMania.ConfigIni.nPedalJudgeRangeDelta,
                "ペダルレーンの判定範囲の微調整を行います。\n" +
                "0 ～ 200ms(暫定仕様)まで指定可能です。\n" + 
                "この数値は通常の判定範囲に加算されます。");
            this.list項目リスト.Add( this.iDrumsPedalJudgeRangeDelta );

            this.iDrumsAssignToLBD = new CItemToggle( "AssignToLBD", CDTXMania.ConfigIni.bAssignToLBD.Drums,
                "旧仕様のドコドコチップをLBDレーンに\n"+
                "適当に振り分けます。\n"+
                "LP、LBDがある譜面では無効になります。",
                "To move some of BassDrum chips to\n"+
                "LBD lane moderately.\n"+
                "(for old-style 2-bass DTX scores\n"+
                "without LP & LBD chips)");
            this.list項目リスト.Add( this.iDrumsAssignToLBD );

            this.iDrumsDkdkType = new CItemList( "DkdkType", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eDkdkType.Drums,
                "ツーバス譜面の仕様を変更する。\n"+
                "OFF: デフォルト\n"+
                "R L: 始動足変更\n"+
                "R Only: dkdk1レーン化",
                "To change the style of double-bass-\n"+
                "concerned chips.\n"+
                "L R: default\n"+
                "R L: changes the beginning foot\n"+
                "R Only: puts bass chips into single\n"+
                "lane",
                new string[] { "OFF", "R L", "R Only" } );
            this.list項目リスト.Add( this.iDrumsDkdkType );
            
            this.iDrumsNumOfLanes = new CItemList( "NumOfLanes", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eNumOfLanes.Drums,
                "10レーン譜面の仕様を変更する。\n"+
                "A: デフォルト10レーン\n"+
                "B: XG仕様9レーン\n"+
                "C: CLASSIC仕様6レーン",

                "To change the number of lanes.\n"+
                "10: default 10 lanes\n"+
                "9: XG style 9 lanes\n"+
                "6: classic style 6 lanes", 
                new string[]{ "10", "9", "6" } );
            this.list項目リスト.Add( this.iDrumsNumOfLanes );
            
            this.iDrumsRandomPad = new CItemList( "RandomPad", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eRandom.Drums,
                "ドラムのパッドチップがランダムに\n"+
                "降ってきます。\n"+
                "Mirror: ミラーをかけます\n"+
                "Part: レーン単位で交換\n"+
                "Super: 小節単位で交換\n"+
                "Hyper: 四分の一小節単位で交換\n"+
                "Master: 死ぬがよい\n"+
                "Another: チップを丁度良くバラける",
                "Drums chips (pads) come randomly.\n"+
                "Mirror: \n"+
                "Part: swapping lanes randomly\n"+
                "Super: swapping for each measure\n"+
                "Hyper: swapping for each 1/4 measure\n"+
                "Master: game over...\n"+
                "Another: moderately swapping each\n"+
                "chip randomly",
                new string[] { "OFF", "Mirror", "Part", "Super", "Hyper", "Master", "Another" } );
            this.list項目リスト.Add( this.iDrumsRandomPad );

            this.iDrumsRandomPedal = new CItemList( "RandomPedal", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eRandomPedal.Drums,
                "ドラムの足チップがランダムに\n降ってきます。\n"+
                "Mirror: ミラーをかけます\n"+
                "Part: レーン単位で交換\n"+
                "Super: 小節単位で交換\n"+
                "Hyper: 四分の一小節単位で交換\n"+
                "Master: 死ぬがよい\n"+
                "Another: チップを丁度良くバラける",
                "Drums chips (pedals) come randomly.\n"+
                "Part: swapping lanes randomly\n"+
                "Super: swapping for each measure\n"+
                "Hyper: swapping for each 1/4 measure\n"+
                "Master: game over...\n"+
                "Another: moderately swapping each\n"+
                "chip randomly",
                new string[] { "OFF", "Mirror", "Part", "Super", "Hyper", "Master", "Another" });
            this.list項目リスト.Add( this.iDrumsRandomPedal );

			// #24074 2011.01.23 add ikanick
			this.iDrumsGraph = new CItemToggle( "Graph", CDTXMania.ConfigIni.bGraph.Drums,
				"最高スキルと比較できるグラフを表示します。\n" +
				"オートプレイだと表示されません。",
				"To draw Graph or not." );
			this.list項目リスト.Add( this.iDrumsGraph );

			this.iDrumsGoToKeyAssign = new CItemBase( "Drums Keys", CItemBase.Eパネル種別.通常,
				"ドラムのキー入力に関する項目を設定します。",
				"Settings for the drums key/pad inputs." );
			this.list項目リスト.Add( this.iDrumsGoToKeyAssign );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.Drums;
		}
		#endregion
		#region [ t項目リストの設定_Guitar() ]
		public void t項目リストの設定_Guitar()
		{
			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iGuitarReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iGuitarReturnToMenu );

			#region [ AutoPlay ]
			this.iGuitarAutoPlayAll = new CItemThreeState( "AutoPlay (All)", CItemThreeState.E状態.不定,
				"全ネック/ピックの自動演奏の ON/OFF を\n" +
                "まとめて切り替えます。",
				"You can change whether Auto or not for all guitar neck/pick at once." );
			this.list項目リスト.Add( this.iGuitarAutoPlayAll );
			this.iGuitarR = new CItemToggle( "    R", CDTXMania.ConfigIni.bAutoPlay.GtR,
				"Rネックを自動で演奏します。",
				"To play R neck automatically." );
			this.list項目リスト.Add( this.iGuitarR );
			this.iGuitarG = new CItemToggle( "    G", CDTXMania.ConfigIni.bAutoPlay.GtG,
				"Gネックを自動で演奏します。",
				"To play G neck automatically." );
			this.list項目リスト.Add( this.iGuitarG );
			this.iGuitarB = new CItemToggle( "    B", CDTXMania.ConfigIni.bAutoPlay.GtB,
				"Bネックを自動で演奏します。",
				"To play B neck automatically." );
			this.list項目リスト.Add( this.iGuitarB );
			this.iGuitarY = new CItemToggle( "    Y", CDTXMania.ConfigIni.bAutoPlay.GtY,
				"Yネックを自動で演奏します。",
				"To play Y neck automatically." );
			this.list項目リスト.Add( this.iGuitarY );
			this.iGuitarP = new CItemToggle( "    P", CDTXMania.ConfigIni.bAutoPlay.GtP,
				"Pネックを自動で演奏します。",
				"To play P neck automatically." );
			this.list項目リスト.Add( this.iGuitarP );
			this.iGuitarPick = new CItemToggle( "    Pick", CDTXMania.ConfigIni.bAutoPlay.GtPick,
				"ピックを自動で演奏します。",
				"To play Pick automatically." );
			this.list項目リスト.Add( this.iGuitarPick );
			this.iGuitarW = new CItemToggle( "    Wailing", CDTXMania.ConfigIni.bAutoPlay.GtW,
				"ウェイリングを自動で演奏します。",
				"To play wailing automatically." );
			this.list項目リスト.Add( this.iGuitarW );
			#endregion
			this.iGuitarScrollSpeed = new CItemInteger( "ScrollSpeed", 0, 0x7cf, CDTXMania.ConfigIni.n譜面スクロール速度.Guitar,
				"演奏時のドラム譜面のスクロールの速度を指定します。\n" +
				"x0.5 ～ x1000.0 を指定可能です。",
				"To change the scroll speed for the drums lanes.\n" +
				"You can set it from x0.5 to x1000.0.\n" +
				"(ScrollSpeed=x0.5 means half speed)" );
			this.list項目リスト.Add( this.iGuitarScrollSpeed );

			this.iGuitarSudHid = new CItemList( "Sud+Hid", CItemBase.Eパネル種別.通常, getDefaultSudHidValue( E楽器パート.GUITAR ),
				"ギターチップの表示方式:\n" +
				"OFF:　　チップを常に表示します。\n" +
				"Sudden: チップがヒットバー付近に来るまで表示\n" +
				"　　　　されなくなります。\n" +
				"Hidden: チップがヒットバー付近で表示されなく\n" +
				"　　　　なります。\n" +
				"Sud+Hid: SuddenとHiddenの効果を同時にかけます。\n" +
				"S(emi)-Invisible:\n" +
				"　　　　通常はチップを透明にしますが、Bad時\n" +
				"　　　　にはしばらく表示します。\n" +
				"F(ull)-Invisible:\n" +
				"　　　　チップを常に透明にします。暗譜での練習\n" +
				"　　　　にお使いください。",
				"Guitar chips display type:\n" +
				"\n" +
				"OFF:    Always show chips.\n" +
				"Sudden: The chips are disappered until they\n" +
				"        come near the hit bar, and suddenly\n" +
				"        appears.\n" +
				"Hidden: The chips are hidden by approaching to\n" +
				"        the hit bar.\n" +
				"Sud+Hid: Both Sudden and Hidden.\n" +
				"S(emi)-Invisible:\n" +
				"        Usually you can't see the chips except\n" +
				"        you've gotten Bad.\n" +
				"F(ull)-Invisible:\n" +
				"        You can't see the chips at all.",
				new string[] { "OFF", "Sudden", "Hidden", "Sud+Hid", "S-Invisible", "F-Invisible" } );
			this.list項目リスト.Add( this.iGuitarSudHid );

			//this.iGuitarSudden = new CItemToggle( "Sudden", CDTXMania.ConfigIni.bSudden.Guitar,
			//    "ギターチップがヒットバー付近にくる\nまで表示されなくなります。",
			//    "Guitar chips are disappered until they\ncome near the hit bar, and suddenly\nappears." );
			//this.list項目リスト.Add( this.iGuitarSudden );
			//this.iGuitarHidden = new CItemToggle( "Hidden", CDTXMania.ConfigIni.bHidden.Guitar,
			//    "ギターチップがヒットバー付近で表示\nされなくなります。",
			//    "Guitar chips are hidden by approaching\nto the hit bar. " );
			//this.list項目リスト.Add( this.iGuitarHidden );

			//this.iGuitarInvisible = new CItemList( "Invisible", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eInvisible.Guitar,
			//    "ギターのチップを全く表示しなくなりま\n" +
			//    "す。暗譜での練習にお使いください。\n" +
			//    "これをONにすると、SuddenとHiddenの\n" +
			//    "効果は無効になります。",
			//    "If you set Blindfold=ON, you can't\n" +
			//    "see the chips at all.",
			//    new string[] { "OFF", "HALF", "ON" } );
			//this.list項目リスト.Add( this.iGuitarInvisible );

            this.iGuitarDark = new CItemList("       Dark", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eDark,
                 "レーン表示のオプションをまとめて切り替えます。\n" +
                 "HALF: レーンが表示されなくなります。\n" +
                 "FULL: さらに小節線、拍線、判定ラインも\n" +
                 "表示されなくなります。",
                 "OFF: all display parts are shown.\nHALF: lanes and gauge are\n disappeared.\nFULL: additionaly to HALF, bar/beat\n lines, hit bar are disappeared.",
                 new string[] { "OFF", "HALF", "FULL" });
            this.list項目リスト.Add(this.iGuitarDark);

            this.iGuitarLaneDispType = new CItemList("LaneDisp", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.nLaneDispType.Guitar,
                "レーンの縦線と小節線の表示を切り替えます。\n" +
                "ALL  ON :レーン背景、小節線を表示します。\n" +
                "LANE OFF:レーン背景を表示しません。\n" +
                "LINE OFF:小節線を表示しません。\n" +
                "ALL  OFF:レーン背景、小節線を表示しません。",
                "",
                new string[] { "ALL ON", "LANE OFF", "LINE OFF", "ALL OFF" });
            this.list項目リスト.Add(this.iGuitarLaneDispType);

            this.iGuitarJudgeLineDisp = new CItemToggle( "JudgeLineDisp", CDTXMania.ConfigIni.bJudgeLineDisp.Guitar,
                "判定ラインの表示 / 非表示を切り替えます。",
                "Toggle JudgeLine");
            this.list項目リスト.Add( this.iGuitarJudgeLineDisp );

            this.iGuitarLaneFlush = new CItemToggle( "LaneFlush", CDTXMania.ConfigIni.bLaneFlush.Guitar,
                "レーンフラッシュの表示の表示 / 非表示を\n" +
                 "切り替えます。",
                "Toggle LaneFlush" );
            this.list項目リスト.Add( this.iGuitarLaneFlush );

			this.iGuitarReverse = new CItemToggle( "Reverse", CDTXMania.ConfigIni.bReverse.Guitar,
				"ギターチップが譜面の上から下に流れるようになります",
				"The scroll way is reversed. Guitar chips flow from the top to the bottom." );
			this.list項目リスト.Add( this.iGuitarReverse );

			this.iSystemJudgePosGuitar = new CItemList( "JudgePos", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.e判定位置.Guitar,
				"判定ライン表示位置:\n" +
				"判定ラインとRGBボタンが、少し下側に表示される\n" +
                "ようになります。",
				"Judge Line position:\n" +
				"The judge line and RGB buttons will be displayed lower position.",
				new string[] { "Normal", "Lower" } );
			this.list項目リスト.Add( this.iSystemJudgePosGuitar );

			this.iGuitarPosition = new CItemList( "Position", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.判定文字表示位置.Guitar,
				"ギターの判定文字の表示位置を指定します。\n" +
				" P-A: レーン上\n" +
				" P-B: 判定ラインの上\n" +
				" P-C: COMBO の下\n" +
				" OFF: 表示しない",
				"The position to show judgement mark.\n" +
				"(Perfect, Great, ...)\n" +
				"\n" +
				" P-A: on the lanes.\n" +
				" P-B: over the hit bar.\n" +
				" P-C: under the COMBO indication.\n" +
				" OFF: no judgement mark.",
				new string[] { "OFF", "P-A", "P-B", "P-C" } );
			this.list項目リスト.Add( this.iGuitarPosition );

			//this.iGuitarJudgeDispPriority = new CItemList( "JudgePriority", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.e判定表示優先度.Guitar,
			//    "判定文字列とコンボ表示の優先順位を\n" +
			//    "指定します。\n" +
			//    "\n" +
			//    " Under: チップの下に表示します。\n" +
			//    " Over:  チップの上に表示します。",
			//    "The display prioity between chips\n" +
			//    " and judge mark/combo.\n" +
			//    "\n" +
			//    " Under: Show them under the chips.\n" +
			//    " Over:  Show them over the chips.",
			//    new string[] { "Under", "Over" } );
			//this.list項目リスト.Add( this.iGuitarJudgeDispPriority );

			this.iGuitarRandom = new CItemList( "Random", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eRandom.Guitar,
				"ギターのチップがランダムに降ってきます。\n" +
				"  Part: 小節・レーン単位で交換\n" +
				"  Super: チップ単位で交換\n" +
				"  Hyper: 全部完全に変更",
				"Guitar chips come randomly.\n" +
				"\n" +
				"  Part: swapping lanes randomly for each measures.\n" +
				"  Super: swapping chip randomly\n" +
				"  Hyper: swapping randomly (number of lanes also changes)",
				new string[] { "OFF", "Part", "Super", "Hyper" } );
			this.list項目リスト.Add( this.iGuitarRandom );

			this.iGuitarLight = new CItemToggle( "Light", CDTXMania.ConfigIni.bLight.Guitar,
				"ギターチップのないところでピッキングしても BAD に\n" +
                "なりません。",
				"Even if you pick without any chips, it doesn't become BAD." );
			this.list項目リスト.Add( this.iGuitarLight );
            this.iGuitarJust = new CItemList( "JUST", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eJUST.Guitar,
                "ON   :PERFECT以外の判定を全てミス扱いにします。\n" +
                "GREAT:GOOD以下の判定を全てミス扱いにします。\n",
                "",
                new string[] { "OFF", "ON", "GREAT" });
            this.list項目リスト.Add( this.iGuitarJust );

            this.iGuitarShutterImage = new CItemList( "ShutterImage", CItemBase.Eパネル種別.通常, this.nCurrentShutterImage.Guitar,
                "シャッター画像の絵柄を選択できます。",
                this.shutterNames
              );
            this.list項目リスト.Add( this.iGuitarShutterImage );

			this.iGuitarLeft = new CItemToggle( "Left", CDTXMania.ConfigIni.bLeft.Guitar,
				"ギターの RGB の並びが左右反転します。\n" +
                "（左利きモード）",
				"Lane order 'R-G-B' becomes 'B-G-R' for lefty." );
			this.list項目リスト.Add( this.iGuitarLeft );

			this.iSystemSoundMonitorGuitar = new CItemToggle( "GuitarMonitor", CDTXMania.ConfigIni.b演奏音を強調する.Guitar,
			"ギター音モニタ：\n" +
			"ギター音を他の音より大きめの音量で発声します。\n" +
			"ただし、オートプレイの場合は通常音量で\n" +
            "発声されます。",
			"To enhance the guitar chip sound (except autoplay)." );
			this.list項目リスト.Add( this.iSystemSoundMonitorGuitar );
			this.iSystemMinComboGuitar = new CItemInteger( "G-MinCombo", 1, 0x1869f, CDTXMania.ConfigIni.n表示可能な最小コンボ数.Guitar,
				"表示可能な最小コンボ数（ギター）：\n" +
				"画面に表示されるコンボの最小の数を指定します。\n" +
				"1 ～ 99999 の値が指定可能です。",
				"Initial number to show the combo for the guitar.\n" +
				"You can specify from 1 to 99999." );
			this.list項目リスト.Add( this.iSystemMinComboGuitar );


			// #23580 2011.1.3 yyagi
			this.iGuitarInputAdjustTimeMs = new CItemInteger( "InputAdjust", -99, 99, CDTXMania.ConfigIni.nInputAdjustTimeMs.Guitar,
				"ギターの入力タイミングの微調整を行います。\n" +
				"-99 ～ 99ms まで指定可能です。\n" +
				"入力ラグを軽減するためには、\n" +
                "負の値を指定してください。",
				"To adjust the guitar input timing.\n" +
				"You can set from -99 to 99ms.\n" +
				"To decrease input lag, set minus value." );
			this.list項目リスト.Add( this.iGuitarInputAdjustTimeMs );

			this.iGuitarGraph = new CItemToggle( "Graph", CDTXMania.ConfigIni.bGraph.Guitar,
				"最高スキルと比較できるグラフを表示します。\n" +
				"オートプレイだと表示されません。\n" +
                "この項目を有効にすると、ベースパートのグラフは\n" +
                "無効になります。",
				"To draw Graph or not." );
			this.list項目リスト.Add( this.iGuitarGraph );

			this.iGuitarGoToKeyAssign = new CItemBase( "Guitar Keys", CItemBase.Eパネル種別.通常,
				"ギターのキー入力に関する項目を設定します。",
				"Settings for the guitar key/pad inputs." );
			this.list項目リスト.Add( this.iGuitarGoToKeyAssign );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.Guitar;
		}
		#endregion
		#region [ t項目リストの設定_Bass() ]
		public void t項目リストの設定_Bass()
		{
			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iBassReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iBassReturnToMenu );

			#region [ AutoPlay ]
			this.iBassAutoPlayAll = new CItemThreeState( "AutoPlay (All)", CItemThreeState.E状態.不定,
				"全ネック/ピックの自動演奏の ON/OFF を\n" +
                "まとめて切り替えます。",
				"You can change whether Auto or not for all bass neck/pick at once." );
			this.list項目リスト.Add( this.iBassAutoPlayAll );
			this.iBassR = new CItemToggle( "    R", CDTXMania.ConfigIni.bAutoPlay.BsR,
				"Rネックを自動で演奏します。",
				"To play R neck automatically." );
			this.list項目リスト.Add( this.iBassR );
			this.iBassG = new CItemToggle( "    G", CDTXMania.ConfigIni.bAutoPlay.BsG,
				"Gネックを自動で演奏します。",
				"To play G neck automatically." );
			this.list項目リスト.Add( this.iBassG );
			this.iBassB = new CItemToggle( "    B", CDTXMania.ConfigIni.bAutoPlay.BsB,
				"Bネックを自動で演奏します。",
				"To play B neck automatically." );
			this.list項目リスト.Add( this.iBassB );
			this.iBassY = new CItemToggle( "    Y", CDTXMania.ConfigIni.bAutoPlay.BsY,
				"Yネックを自動で演奏します。",
				"To play Y neck automatically." );
			this.list項目リスト.Add( this.iBassY );
			this.iBassP = new CItemToggle( "    P", CDTXMania.ConfigIni.bAutoPlay.BsP,
				"Pネックを自動で演奏します。",
				"To play P neck automatically." );
			this.list項目リスト.Add( this.iBassP );
			this.iBassPick = new CItemToggle( "    Pick", CDTXMania.ConfigIni.bAutoPlay.BsPick,
				"ピックを自動で演奏します。",
				"To play Pick automatically." );
			this.list項目リスト.Add( this.iBassPick );
			this.iBassW = new CItemToggle( "    Wailing", CDTXMania.ConfigIni.bAutoPlay.BsW,
				"ウェイリングを自動で演奏します。",
				"To play wailing automatically." );
			this.list項目リスト.Add( this.iBassW );
			#endregion

			this.iBassScrollSpeed = new CItemInteger( "ScrollSpeed", 0, 0x7cf, CDTXMania.ConfigIni.n譜面スクロール速度.Bass,
				"演奏時のベース譜面のスクロールの速度を指定します\n" +
				"x0.5 ～ x1000.0 までを指定可能です。",
				"To change the scroll speed for the bass lanes.\n" +
				"You can set it from x0.5 to x1000.0.\n" +
				"(ScrollSpeed=x0.5 means half speed)" );
			this.list項目リスト.Add( this.iBassScrollSpeed );

			this.iBassSudHid = new CItemList( "Sud+Hid", CItemBase.Eパネル種別.通常, getDefaultSudHidValue( E楽器パート.BASS ),
				"ベースチップの表示方式:\n" +
				"OFF:　　チップを常に表示します。\n" +
				"Sudden: チップがヒットバー付近に来るまで表示\n" +
				"　　　　されなくなります。\n" +
				"Hidden: チップがヒットバー付近で表示されなく\n" +
				"　　　　なります。\n" +
				"Sud+Hid: SuddenとHiddenの効果を同時にかけます。\n" +
				"S(emi)-Invisible:\n" +
				"　　　　通常はチップを透明にしますが、Bad時\n" +
				"　　　　にはしばらく表示します。\n" +
				"F(ull)-Invisible:\n" +
				"　　　　チップを常に透明にします。暗譜での練習\n" +
				"　　　　にお使いください。",
				"Bass chips display type:\n" +
				"\n" +
				"OFF:    Always show chips.\n" +
				"Sudden: The chips are disappered until they\n" +
				"        come near the hit bar, and suddenly\n" +
				"        appears.\n" +
				"Hidden: The chips are hidden by approaching to\n" +
				"        the hit bar.\n" +
				"Sud+Hid: Both Sudden and Hidden.\n" +
				"S(emi)-Invisible:\n" +
				"        Usually you can't see the chips except\n" +
				"        you've gotten Bad.\n" +
				"F(ull)-Invisible:\n" +
				"        You can't see the chips at all.",
				new string[] { "OFF", "Sudden", "Hidden", "Sud+Hid", "S-Invisible", "F-Invisible" } );
			this.list項目リスト.Add( this.iBassSudHid );

			//this.iBassSudden = new CItemToggle( "Sudden", CDTXMania.ConfigIni.bSudden.Bass,
			//    "ベースチップがヒットバー付近にくる\nまで表示されなくなります。",
			//    "Bass chips are disappered until they\ncome near the hit bar, and suddenly\nappears." );
			//this.list項目リスト.Add( this.iBassSudden );
			//this.iBassHidden = new CItemToggle( "Hidden", CDTXMania.ConfigIni.bHidden.Bass,
			//    "ベースチップがヒットバー付近で表示\nされなくなります。",
			//    "Bass chips are hidden by approaching\nto the hit bar." );
			//this.list項目リスト.Add( this.iBassHidden );

			//this.iBassInvisible = new CItemList( "InvisibleBlindfold", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.eInvisible.Bass,
			//    "ベースのチップを全く表示しなくなりま\n" +
			//    "す。暗譜での練習にお使いください。\n" +
			//    "これをONにすると、SuddenとHiddenの\n" +
			//    "効果は無効になります。",
			//    "If you set Blindfold=ON, you can't\n" +
			//    "see the chips at all.",
			//    new string[] { "OFF", "HALF", "ON"} );
			//this.list項目リスト.Add( this.iBassInvisible );

            this.iBassDark = new CItemList("       Dark", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eDark,
                 "レーン表示のオプションをまとめて切り替えます。\n" +
                 "HALF: レーンが表示されなくなります。\n" +
                 "FULL: さらに小節線、拍線、判定ラインも\n" +
                 "表示されなくなります。",
                 "OFF: all display parts are shown.\nHALF: lanes and gauge are\n disappeared.\nFULL: additionaly to HALF, bar/beat\n lines, hit bar are disappeared.",
                 new string[] { "OFF", "HALF", "FULL" });
            this.list項目リスト.Add(this.iBassDark);

            this.iBassLaneDispType = new CItemList( "LaneDisp", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.nLaneDispType.Bass,
                "レーンの縦線と小節線の表示を切り替えます。\n" +
                "ALL  ON :レーン背景、小節線を表示します。\n" +
                "LANE OFF:レーン背景を表示しません。\n" +
                "LINE OFF:小節線を表示しません。\n" +
                "ALL  OFF:レーン背景、小節線を表示しません。",
                "",
                new string[] { "ALL ON", "LANE OFF", "LINE OFF", "ALL OFF" });
            this.list項目リスト.Add(this.iBassLaneDispType);

            this.iBassJudgeLineDisp = new CItemToggle("JudgeLineDisp", CDTXMania.ConfigIni.bJudgeLineDisp.Bass,
                "判定ラインの表示 / 非表示を切り替えます。",
                "Toggle JudgeLine");
            this.list項目リスト.Add(this.iBassJudgeLineDisp);

            this.iBassLaneFlush = new CItemToggle( "LaneFlush", CDTXMania.ConfigIni.bLaneFlush.Bass,
                "レーンフラッシュの表示 / 非表示を\n" +
                 "切り替えます。",
                "Toggle LaneFlush" );
            this.list項目リスト.Add( this.iBassLaneFlush );

			this.iBassReverse = new CItemToggle( "Reverse", CDTXMania.ConfigIni.bReverse.Bass,
				"ベースチップが譜面の上から下に流れるようになります",
				"The scroll way is reversed. Bass chips flow from the top to the bottom." );
			this.list項目リスト.Add( this.iBassReverse );

			this.iSystemJudgePosBass = new CItemList( "JudgePos", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.e判定位置.Bass,
				"判定ライン表示位置:\n" +
				"判定ラインとRGBボタンが、少し下側に表示される\n" +
                "ようになります。",
				"Judge Line position:\n" +
				"The judge line and RGB buttons will be displayed lower position.",
				new string[] { "Normal", "Lower" } );
			this.list項目リスト.Add( this.iSystemJudgePosBass );
			
			this.iBassPosition = new CItemList( "Position", CItemBase.Eパネル種別.通常,
				(int) CDTXMania.ConfigIni.判定文字表示位置.Bass,
				"ベースの判定文字の表示位置を指定します。\n" +
				" P-A: レーン上\n" +
				" P-B: 判定ラインの上\n" +
				" P-C: COMBO の下\n" +
				" OFF: 表示しない",
				"The position to show judgement mark.\n" +
				"(Perfect, Great, ...)\n" +
				"\n" +
				" P-A: on the lanes.\n" +
				" P-B: over the hit bar.\n" +
				" P-C: under the COMBO indication.\n" +
				" OFF: no judgement mark.",
				new string[] { "OFF", "P-A", "P-B", "P-C" } );
			this.list項目リスト.Add( this.iBassPosition );

			//this.iBassJudgeDispPriority = new CItemList( "JudgePriority", CItemBase.Eパネル種別.通常, (int) CDTXMania.ConfigIni.e判定表示優先度.Bass,
			//"判定文字列とコンボ表示の優先順位を\n" +
			//"指定します。\n" +
			//"\n" +
			//" Under: チップの下に表示します。\n" +
			//" Over:  チップの上に表示します。",
			//"The display prioity between chips\n" +
			//" and judge mark/combo.\n" +
			//"\n" +
			//" Under: Show them under the chips.\n" +
			//" Over:  Show them over the chips.",
			//new string[] { "Under", "Over" } );
			//this.list項目リスト.Add( this.iBassJudgeDispPriority );

			this.iBassRandom = new CItemList( "Random", CItemBase.Eパネル種別.通常,
				(int) CDTXMania.ConfigIni.eRandom.Bass,
				"ベースのチップがランダムに降ってきます。\n" +
				"  Part: 小節・レーン単位で交換\n" +
				"  Super: チップ単位で交換\n" +
				"  Hyper: 全部完全に変更",
				"Bass chips come randomly.\n" +
				"\n" +
				"  Part: swapping lanes randomly for each measures.\n" +
				"  Super: swapping chip randomly\n" +
				"  Hyper: swapping randomly (number of lanes also changes)",
				new string[] { "OFF", "Part", "Super", "Hyper" } );
			this.list項目リスト.Add( this.iBassRandom );
			this.iBassLight = new CItemToggle( "Light", CDTXMania.ConfigIni.bLight.Bass,
				"ベースチップのないところでピッキングしても BAD に\n" +
                "なりません。",
				"Even if you pick without any chips, it doesn't become BAD." );
			this.list項目リスト.Add( this.iBassLight );
            this.iBassJust = new CItemList( "JUST", CItemBase.Eパネル種別.通常, (int)CDTXMania.ConfigIni.eJUST.Bass,
                "ON   :PERFECT以外の判定を全てミス扱いにします。\n" +
                "GREAT:GOOD以下の判定を全てミス扱いにします。\n",
                "",
                new string[] { "OFF", "ON", "GREAT" });
            this.list項目リスト.Add( this.iBassJust );

            this.iBassShutterImage = new CItemList("ShutterImage", CItemBase.Eパネル種別.通常, this.nCurrentShutterImage.Bass,
                "シャッター画像の絵柄を選択できます。",
                this.shutterNames
              );
            this.list項目リスト.Add( this.iBassShutterImage );

			this.iBassLeft = new CItemToggle( "Left", CDTXMania.ConfigIni.bLeft.Bass,
				"ベースの RGB の並びが左右反転します。\n" +
                "（左利きモード）",
				"Lane order 'R-G-B' becomes 'B-G-R' for lefty." );
			this.list項目リスト.Add( this.iBassLeft );

			this.iSystemSoundMonitorBass = new CItemToggle( "BassMonitor", CDTXMania.ConfigIni.b演奏音を強調する.Bass,
			"ベース音モニタ：\n" +
			"ベース音を他の音より大きめの音量で発声します。\n" +
			"ただし、オートプレイの場合は通常音量で\n" +
            "発声されます。",
			"To enhance the bass chip sound (except autoplay)." );
			this.list項目リスト.Add( this.iSystemSoundMonitorBass );

			this.iSystemMinComboBass = new CItemInteger( "B-MinCombo", 1, 0x1869f, CDTXMania.ConfigIni.n表示可能な最小コンボ数.Bass,
				"表示可能な最小コンボ数（ベース）：\n" +
				"画面に表示されるコンボの最小の数を指定します。\n" +
				"1 ～ 99999 の値が指定可能です。",
				"Initial number to show the combo for the bass.\n" +
				"You can specify from 1 to 99999." );
			this.list項目リスト.Add( this.iSystemMinComboBass );

			this.iBassGraph = new CItemToggle( "Graph", CDTXMania.ConfigIni.bGraph.Bass,
				"最高スキルと比較できるグラフを表示します。\n" +
				"オートプレイだと表示されません。\n" +
                "この項目を有効にすると、ギターパートのグラフは\n" +
                "無効になります。",
				"To draw Graph or not." );
			this.list項目リスト.Add( this.iBassGraph );

			// #23580 2011.1.3 yyagi
			this.iBassInputAdjustTimeMs = new CItemInteger( "InputAdjust", -99, 99, CDTXMania.ConfigIni.nInputAdjustTimeMs.Bass,
				"ベースの入力タイミングの微調整を行います。\n" +
				"-99 ～ 99ms まで指定可能です。\n" +
				"入力ラグを軽減するためには、負の値を\n" +
                "指定してください。",
				"To adjust the bass input timing.\n" +
				"You can set from -99 to 99ms.\n" +
				"To decrease input lag, set minus value." );
			this.list項目リスト.Add( this.iBassInputAdjustTimeMs );

			this.iBassGoToKeyAssign = new CItemBase( "Bass Keys", CItemBase.Eパネル種別.通常,
				"ベースのキー入力に関する項目を設定します。",
				"Settings for the bass key/pad inputs." );
			this.list項目リスト.Add( this.iBassGoToKeyAssign );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.Bass;
		}
		#endregion

		/// <summary>Sud+Hidの初期値を返す</summary>
		/// <param name="eInst"></param>
		/// <returns>
		/// 0: None
		/// 1: Sudden
		/// 2: Hidden
		/// 3: Sud+Hid
		/// 4: Semi-Invisible
		/// 5: Full-Invisible
		/// </returns>
		protected int getDefaultSudHidValue( E楽器パート eInst )
		{
			int defvar;
			int nInst = (int) eInst;
			if ( CDTXMania.ConfigIni.eInvisible[ nInst ] != EInvisible.OFF )
			{
				defvar = (int) CDTXMania.ConfigIni.eInvisible[ nInst ] + 3;
			}
			else
			{
				defvar = ( CDTXMania.ConfigIni.bSudden[ nInst ] ? 1 : 0 ) +
						 ( CDTXMania.ConfigIni.bHidden[ nInst ] ? 2 : 0 );
			}
			return defvar;
		}

		/// <summary>
		/// ESC押下時の右メニュー描画
		/// </summary>
		public void tEsc押下()
		{
			if ( this.b要素値にフォーカス中 )		// #32059 2013.9.17 add yyagi
			{
				this.b要素値にフォーカス中 = false;
			}

			if ( this.eメニュー種別 == Eメニュー種別.KeyAssignSystem )
			{
				t項目リストの設定_System();
			}
			else if ( this.eメニュー種別 == Eメニュー種別.KeyAssignDrums )
			{
				t項目リストの設定_Drums();
			}
			else if ( this.eメニュー種別 == Eメニュー種別.KeyAssignGuitar )
			{
				t項目リストの設定_Guitar();
			}
			else if ( this.eメニュー種別 == Eメニュー種別.KeyAssignBass )
			{
				t項目リストの設定_Bass();
			}
			// これ以外なら何もしない
		}
		public void tEnter押下()
		{
			CDTXMania.Skin.sound決定音.t再生する();
			if( this.b要素値にフォーカス中 )
			{
				this.b要素値にフォーカス中 = false;
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ].e種別 == CItemBase.E種別.整数 )
			{
				this.b要素値にフォーカス中 = true;
			}
			else if( this.b現在選択されている項目はReturnToMenuである )
			{
				//this.tConfigIniへ記録する();
				//CONFIG中にスキン変化が発生すると面倒なので、一旦マスクした。
			}
			#region [ 個々のキーアサイン ]
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsLC )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.LC );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsHHC )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.HH );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsHHO )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.HHO );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsSD )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.SD );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsBD )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.BD );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsHT )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.HT );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsLT )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.LT );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsFT )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.FT );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsCY )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.CY );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsRD )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.RD );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsLP )			// #27029 2012.1.4 from
			{																							//
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.LP );	//
			}																							//
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsLBD )			// #27029 2012.1.4 from
			{																							//
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.DRUMS, EKeyConfigPad.LBD );	//
			}																							//
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarR )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.GUITAR, EKeyConfigPad.R );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarG )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.GUITAR, EKeyConfigPad.G );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarB )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.GUITAR, EKeyConfigPad.B );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarPick )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.GUITAR, EKeyConfigPad.Pick );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarWail )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.GUITAR, EKeyConfigPad.Wail );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarDecide )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.GUITAR, EKeyConfigPad.Decide );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarCancel )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.GUITAR, EKeyConfigPad.Cancel );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassR )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.BASS, EKeyConfigPad.R );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassG )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.BASS, EKeyConfigPad.G );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassB )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.BASS, EKeyConfigPad.B );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassPick )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.BASS, EKeyConfigPad.Pick );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassWail )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.BASS, EKeyConfigPad.Wail );
			}
			else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassDecide )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.BASS, EKeyConfigPad.Decide );
			}
			else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassCancel )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.BASS, EKeyConfigPad.Cancel );
			}
			else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignSystemCapture )
			{
				CDTXMania.stageコンフィグ.tパッド選択通知( EKeyConfigPart.SYSTEM, EKeyConfigPad.Capture);
			}
			#endregion
			else
			{
		 		// #27029 2012.1.5 from
				if( ( this.iSystemBDGroup.n現在選択されている項目番号 == (int) EBDGroup.どっちもBD ) &&
					( ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemHHGroup ) || ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemHitSoundPriorityHH ) ) )
				{
					// 変更禁止（何もしない）
				}
				else
				{
					// 変更許可
					this.list項目リスト[ this.n現在の選択項目 ].tEnter押下();
				}


				// Enter押下後の後処理

				if( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemFullscreen )
				{
					CDTXMania.app.b次のタイミングで全画面_ウィンドウ切り替えを行う = true;
				}
				else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemVSyncWait )
				{
					CDTXMania.ConfigIni.b垂直帰線待ちを行う = this.iSystemVSyncWait.bON;
					CDTXMania.app.b次のタイミングで垂直帰線同期切り替えを行う = true;
				}
				#region [ AutoPlay #23886 2012.5.8 yyagi ]
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iDrumsAutoPlayAll )
				{
					this.t全部のドラムパッドのAutoを切り替える( this.iDrumsAutoPlayAll.e現在の状態 == CItemThreeState.E状態.ON );
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iGuitarAutoPlayAll )
				{
					this.t全部のギターパッドのAutoを切り替える( this.iGuitarAutoPlayAll.e現在の状態 == CItemThreeState.E状態.ON );
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iBassAutoPlayAll )
				{
					this.t全部のベースパッドのAutoを切り替える( this.iBassAutoPlayAll.e現在の状態 == CItemThreeState.E状態.ON );
				}
				#endregion
				#region [ キーアサインへの遷移と脱出 ]
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemGoToKeyAssign )			// #24609 2011.4.12 yyagi
				{
					t項目リストの設定_KeyAssignSystem();
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignSystemReturnToMenu )	// #24609 2011.4.12 yyagi
				{
					t項目リストの設定_System();
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iDrumsGoToKeyAssign )				// #24525 2011.3.15 yyagi
				{
					t項目リストの設定_KeyAssignDrums();
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignDrumsReturnToMenu )		// #24525 2011.3.15 yyagi
				{
					t項目リストの設定_Drums();
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iGuitarGoToKeyAssign )			// #24525 2011.3.15 yyagi
				{
					t項目リストの設定_KeyAssignGuitar();
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignGuitarReturnToMenu )	// #24525 2011.3.15 yyagi
				{
					t項目リストの設定_Guitar();
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iBassGoToKeyAssign )				// #24525 2011.3.15 yyagi
				{
					t項目リストの設定_KeyAssignBass();
				}
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iKeyAssignBassReturnToMenu )		// #24525 2011.3.15 yyagi
				{
					t項目リストの設定_Bass();
				}
				#endregion
                #region [ ダーク ]
                else if (this.list項目リスト[this.n現在の選択項目] == this.iDrumsDark)					// #27029 2012.1.4 from
                {
                    if (this.iDrumsDark.n現在選択されている項目番号 == (int)Eダークモード.FULL)
                    {
                        this.iDrumsLaneDispType.n現在選択されている項目番号 = 3;
                        this.iDrumsJudgeLineDisp.bON = false;
                        this.iDrumsLaneFlush.bON = false;
                    }
                    else if (this.iDrumsDark.n現在選択されている項目番号 == (int)Eダークモード.HALF)
                    {
                        this.iDrumsLaneDispType.n現在選択されている項目番号 = 1;
                        this.iDrumsJudgeLineDisp.bON = true;
                        this.iDrumsLaneFlush.bON = true;
                    }
                    else
                    {
                        this.iDrumsLaneDispType.n現在選択されている項目番号 = 0;
                        this.iDrumsJudgeLineDisp.bON = true;
                        this.iDrumsLaneFlush.bON = true;
                    }
                }
                else if (this.list項目リスト[this.n現在の選択項目] == this.iGuitarDark)					// #27029 2012.1.4 from
                {
                    if (this.iGuitarDark.n現在選択されている項目番号 == (int)Eダークモード.FULL)
                    {
                        this.iGuitarLaneDispType.n現在選択されている項目番号 = 3;
                        this.iGuitarJudgeLineDisp.bON = false;
                        this.iGuitarLaneFlush.bON = false;
                    }
                    else if (this.iGuitarDark.n現在選択されている項目番号 == (int)Eダークモード.HALF)
                    {
                        this.iGuitarLaneDispType.n現在選択されている項目番号 = 1;
                        this.iGuitarJudgeLineDisp.bON = true;
                        this.iGuitarLaneFlush.bON = true;
                    }
                    else
                    {
                        this.iGuitarLaneDispType.n現在選択されている項目番号 = 0;
                        this.iGuitarJudgeLineDisp.bON = true;
                        this.iGuitarLaneFlush.bON = true;
                    }
                }
                else if (this.list項目リスト[this.n現在の選択項目] == this.iBassDark)					// #27029 2012.1.4 from
                {
                    if (this.iBassDark.n現在選択されている項目番号 == (int)Eダークモード.FULL)
                    {
                        this.iBassLaneDispType.n現在選択されている項目番号 = 3;
                        this.iBassJudgeLineDisp.bON = false;
                        this.iBassLaneFlush.bON = false;
                    }
                    else if (this.iBassDark.n現在選択されている項目番号 == (int)Eダークモード.HALF)
                    {
                        this.iBassLaneDispType.n現在選択されている項目番号 = 1;
                        this.iBassJudgeLineDisp.bON = true;
                        this.iBassLaneFlush.bON = true;
                    }
                    else
                    {
                        this.iBassLaneDispType.n現在選択されている項目番号 = 0;
                        this.iBassJudgeLineDisp.bON = true;
                        this.iBassLaneFlush.bON = true;
                    }
                }
                #endregion
                #region[ ギター・ベースグラフ ]
                else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iGuitarGraph )
                {
                    if( this.iGuitarGraph.bON == true )
                    {
                        CDTXMania.ConfigIni.bGraph.Bass = false;
                        this.iBassGraph.bON = false;
                    }
                }
                else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iBassGraph )
                {
                    if( this.iBassGraph.bON == true )
                    {
                        CDTXMania.ConfigIni.bGraph.Guitar = false;
                        this.iGuitarGraph.bON = false;
                        
                    }
                }
                #endregion
                #region [ BDGroup #27029 2012.1.4 from ]
                else if( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemBDGroup )					// #27029 2012.1.4 from
				{
					if( this.iSystemBDGroup.n現在選択されている項目番号 == (int) EBDGroup.どっちもBD )
					{
						// #27029 2012.1.5 from: 変更前の状態をバックアップする。
                        //CDTXMania.ConfigIni.BackupOf1BD = new CConfigIni.CBackupOf1BD() {
                        //    eHHGroup = (EHHGroup) this.iSystemHHGroup.n現在選択されている項目番号,
                        //    eHitSoundPriorityHH = (E打ち分け時の再生の優先順位) this.iSystemHitSoundPriorityHH.n現在選択されている項目番号,
                        //};

                        //// HH Group ... HH-0 → HH-2 / HH-1 → HH-3 / HH-2 → 変更なし / HH-3 → 変更なし
                        //if( this.iSystemHHGroup.n現在選択されている項目番号 == (int) EHHGroup.全部打ち分ける )
                        //    this.iSystemHHGroup.n現在選択されている項目番号 = (int) EHHGroup.左シンバルのみ打ち分ける;
                        //if( this.iSystemHHGroup.n現在選択されている項目番号 == (int) EHHGroup.ハイハットのみ打ち分ける )
                        //    this.iSystemHHGroup.n現在選択されている項目番号 = (int) EHHGroup.全部共通;

                        //// HH Priority ... C>P → 変更なし / P>C → C>P
                        //if( this.iSystemHitSoundPriorityHH.n現在選択されている項目番号 == (int) E打ち分け時の再生の優先順位.PadがChipより優先 )
                        //    this.iSystemHitSoundPriorityHH.n現在選択されている項目番号 = (int) E打ち分け時の再生の優先順位.ChipがPadより優先;
					}
					else
					{
						// #27029 2012.1.5 from: 変更前の状態に戻す。
						//this.iSystemHHGroup.n現在選択されている項目番号 = (int) CDTXMania.ConfigIni.BackupOf1BD.eHHGroup;
						//this.iSystemHitSoundPriorityHH.n現在選択されている項目番号 = (int) CDTXMania.ConfigIni.BackupOf1BD.eHitSoundPriorityHH;
						
						//CDTXMania.ConfigIni.BackupOf1BD = null;
					}
				}
				#endregion
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemUseBoxDefSkin )			// #28195 2012.5.6 yyagi
				{
					CSkin.bUseBoxDefSkin = this.iSystemUseBoxDefSkin.bON;
				}
				#region [ スキン項目でEnterを押下した場合に限り、スキンの縮小サンプルを生成する。]
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemSkinSubfolder )			// #28195 2012.5.2 yyagi
				{
					tGenerateSkinSample();
				}
				#endregion
				#region [ 曲データ一覧の再読み込み ]
				else if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemReloadDTX )				// #32081 2013.10.21 yyagi
				{
					if ( CDTXMania.EnumSongs.IsEnumerating )
					{
						// Debug.WriteLine( "バックグラウンドでEnumeratingSongs中だったので、一旦中断します。" );
						CDTXMania.EnumSongs.Abort();
						CDTXMania.actEnumSongs.On非活性化();
					}

					CDTXMania.EnumSongs.StartEnumFromDisk();
					CDTXMania.EnumSongs.ChangeEnumeratePriority( ThreadPriority.Normal );
					CDTXMania.actEnumSongs.bコマンドでの曲データ取得 = true;
					CDTXMania.actEnumSongs.On活性化();
				}
				#endregion
			}
		}

		protected void tGenerateSkinSample()
		{
			nSkinIndex = ( ( CItemList ) this.list項目リスト[ this.n現在の選択項目 ] ).n現在選択されている項目番号;
			if ( nSkinSampleIndex != nSkinIndex )
			{
				string path = skinSubFolders[ nSkinIndex ];
				path = System.IO.Path.Combine( path, @"Graphics\1_background.png" );
				Bitmap bmSrc = new Bitmap( path );
				Bitmap bmDest = new Bitmap( bmSrc.Width / 4, bmSrc.Height / 4 );
				Graphics g = Graphics.FromImage( bmDest );
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.DrawImage( bmSrc, new Rectangle( 0, 0, bmSrc.Width / 4, bmSrc.Height / 4 ),
					0, 0, bmSrc.Width, bmSrc.Height, GraphicsUnit.Pixel );
				if ( txSkinSample1 != null )
				{
					CDTXMania.t安全にDisposeする( ref txSkinSample1 );
				}
				txSkinSample1 = CDTXMania.tテクスチャの生成( bmDest, false );
				g.Dispose();
				bmDest.Dispose();
				bmSrc.Dispose();
				nSkinSampleIndex = nSkinIndex;
			}
		}

		#region [ 項目リストの設定 ( Exit, KeyAssignSystem/Drums/Guitar/Bass) ]
		public void t項目リストの設定_Exit()
		{
			this.tConfigIniへ記録する();
			this.eメニュー種別 = Eメニュー種別.Unknown;
		}
		public void t項目リストの設定_KeyAssignSystem()
		{
			//this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iKeyAssignSystemReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iKeyAssignSystemReturnToMenu );
			this.iKeyAssignSystemCapture = new CItemBase( "Capture",
				"キャプチャキー設定：\n" +
				"画面キャプチャのキーの割り当てを設定します。",
				"Capture key assign:\n" +
				"To assign key for screen capture.\n" +
				"(You can assign keyboard only. You can't use pads to capture screenshot.)" );
			this.list項目リスト.Add( this.iKeyAssignSystemCapture );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.KeyAssignSystem;
		}
		public void t項目リストの設定_KeyAssignDrums()
		{
			//			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iKeyAssignDrumsReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iKeyAssignDrumsReturnToMenu );
			this.iKeyAssignDrumsLC = new CItemBase( "LeftCymbal",
				"ドラムのキー設定：\n" +
				"左シンバルへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for LeftCymbal button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsLC );
			this.iKeyAssignDrumsHHC = new CItemBase( "HiHat(Close)",
				"ドラムのキー設定：\n" +
				"ハイハット（クローズ）へのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for HiHat(Close) button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsHHC );
			this.iKeyAssignDrumsHHO = new CItemBase( "HiHat(Open)",
				"ドラムのキー設定：\n" +
				"ハイハット（オープン）へのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for HiHat(Open) button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsHHO );
			this.iKeyAssignDrumsSD = new CItemBase( "Snare",
				"ドラムのキー設定：\n" +
				"スネアへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for Snare button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsSD );
			this.iKeyAssignDrumsBD = new CItemBase( "Bass",
				"ドラムのキー設定：\n" +
				"バスドラムへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for Bass button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsBD );
			this.iKeyAssignDrumsHT = new CItemBase( "HighTom",
				"ドラムのキー設定：\n" +
				"ハイタムへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for HighTom button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsHT );
			this.iKeyAssignDrumsLT = new CItemBase( "LowTom",
				"ドラムのキー設定：\n" +
				"ロータムへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for LowTom button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsLT );
			this.iKeyAssignDrumsFT = new CItemBase( "FloorTom",
				"ドラムのキー設定：\n" +
				"フロアタムへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for FloorTom button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsFT );
			this.iKeyAssignDrumsCY = new CItemBase( "RightCymbal",
				"ドラムのキー設定：\n" +
				"右シンバルへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for RightCymbal button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsCY );
			this.iKeyAssignDrumsRD = new CItemBase( "RideCymbal",
				"ドラムのキー設定：\n" +
				"ライドシンバルへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for RideCymbal button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsRD );
			this.iKeyAssignDrumsLP = new CItemBase( "HiHatPedal",									// #27029 2012.1.4 from
				"ドラムのキー設定：\n" +															//
				"ハイハットのフットペダルへのキーの割り当てを設定します。",							//
				"Drums key assign:\n" +																//
				"To assign key/pads for HiHatPedal button." );										//
			this.list項目リスト.Add( this.iKeyAssignDrumsLP );										//
			this.iKeyAssignDrumsLBD = new CItemBase( "LeftBass",
				"ドラムのキー設定：\n" +
				"ハイハットのフットペダルへのキーの割り当てを設定します。",
				"Drums key assign:\n" +
				"To assign key/pads for LeftBass button." );
			this.list項目リスト.Add( this.iKeyAssignDrumsLBD );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.KeyAssignDrums;
		}
		public void t項目リストの設定_KeyAssignGuitar()
		{
			//			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iKeyAssignGuitarReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iKeyAssignGuitarReturnToMenu );
			this.iKeyAssignGuitarR = new CItemBase( "R",
				"ギターのキー設定：\n" +
				"Rボタンへのキーの割り当てを設定します。",
				"Guitar key assign:\n" +
				"To assign key/pads for R button." );
			this.list項目リスト.Add( this.iKeyAssignGuitarR );
			this.iKeyAssignGuitarG = new CItemBase( "G",
				"ギターのキー設定：\n" +
				"Gボタンへのキーの割り当てを設定します。",
				"Guitar key assign:\n" +
				"To assign key/pads for G button." );
			this.list項目リスト.Add( this.iKeyAssignGuitarG );
			this.iKeyAssignGuitarB = new CItemBase( "B",
				"ギターのキー設定：\n" +
				"Bボタンへのキーの割り当てを設定します。",
				"Guitar key assign:\n" +
				"To assign key/pads for B button." );
			this.list項目リスト.Add( this.iKeyAssignGuitarB );
			this.iKeyAssignGuitarPick = new CItemBase( "Pick",
				"ギターのキー設定：\n" +
				"ピックボタンへのキーの割り当てを設定します。",
				"Guitar key assign:\n" +
				"To assign key/pads for Pick button." );
			this.list項目リスト.Add( this.iKeyAssignGuitarPick );
			this.iKeyAssignGuitarWail = new CItemBase( "Wailing",
				"ギターのキー設定：\n" +
				"Wailingボタンへのキーの割り当てを設定します。",
				"Guitar key assign:\nTo assign key/pads for Wailing button." );
			this.list項目リスト.Add( this.iKeyAssignGuitarWail );
			this.iKeyAssignGuitarDecide = new CItemBase( "Decide",
				"ギターのキー設定：\n" +
				"決定ボタンへのキーの割り当てを設定します。",
				"Guitar key assign:\nTo assign key/pads for Decide button." );
			this.list項目リスト.Add( this.iKeyAssignGuitarDecide );
			this.iKeyAssignGuitarCancel = new CItemBase( "Cancel",
				"ギターのキー設定：\n" +
				"キャンセルボタンへのキーの割り当てを設定します。",
				"Guitar key assign:\n" +
				"To assign key/pads for Cancel button." );
			this.list項目リスト.Add( this.iKeyAssignGuitarCancel );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.KeyAssignGuitar;
		}
		public void t項目リストの設定_KeyAssignBass()
		{
			//			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();

			// #27029 2012.1.5 from: 説明文は最大9行→13行に変更。

			this.iKeyAssignBassReturnToMenu = new CItemBase( "<< Return To Menu", CItemBase.Eパネル種別.その他,
				"左側のメニューに戻ります。",
				"Return to left menu." );
			this.list項目リスト.Add( this.iKeyAssignBassReturnToMenu );
			this.iKeyAssignBassR = new CItemBase( "R",
				"ベースのキー設定：\n" +
				"Rボタンへのキーの割り当てを設定します。",
				"Bass key assign:\n" +
				"To assign key/pads for R button." );
			this.list項目リスト.Add( this.iKeyAssignBassR );
			this.iKeyAssignBassG = new CItemBase( "G",
				"ベースのキー設定：\n" +
				"Gボタンへのキーの割り当てを設定します。",
				"Bass key assign:\n" +
				"To assign key/pads for G button." );
			this.list項目リスト.Add( this.iKeyAssignBassG );
			this.iKeyAssignBassB = new CItemBase( "B",
				"ベースのキー設定：\n" +
				"Bボタンへのキーの割り当てを設定します。",
				"Bass key assign:\n" +
				"To assign key/pads for B button." );
			this.list項目リスト.Add( this.iKeyAssignBassB );
			this.iKeyAssignBassPick = new CItemBase( "Pick",
				"ベースのキー設定：\n" +
				"ピックボタンへのキーの割り当てを設定します。",
				"Bass key assign:\n" +
				"To assign key/pads for Pick button." );
			this.list項目リスト.Add( this.iKeyAssignBassPick );
			this.iKeyAssignBassWail = new CItemBase( "Wailing",
				"ベースのキー設定：\n" +
				"Wailingボタンへのキーの割り当てを設定します。",
				"Bass key assign:\n" +
				"To assign key/pads for Wailing button." );
			this.list項目リスト.Add( this.iKeyAssignBassWail );
			this.iKeyAssignBassDecide = new CItemBase( "Decide",
				"ベースのキー設定：\n" +
				"決定ボタンへのキーの割り当てを設定します。",
				"Bass key assign:\n" +
				"To assign key/pads for Decide button." );
			this.list項目リスト.Add( this.iKeyAssignBassDecide );
			this.iKeyAssignBassCancel = new CItemBase( "Cancel",
				"ベースのキー設定：\n" +
				"キャンセルボタンへのキーの割り当てを設定します。",
				"Bass key assign:\n" +
				"To assign key/pads for Cancel button." );
			this.list項目リスト.Add( this.iKeyAssignBassCancel );

			OnListMenuの初期化();
			this.n現在の選択項目 = 0;
			this.eメニュー種別 = Eメニュー種別.KeyAssignBass;
		}
		#endregion
		public void t次に移動()
		{
			CDTXMania.Skin.soundカーソル移動音.t再生する();
			if( this.b要素値にフォーカス中 )
			{
				this.list項目リスト[ this.n現在の選択項目 ].t項目値を前へ移動();
				t要素値を上下に変更中の処理();
			}
			else
			{
				this.n目標のスクロールカウンタ += 100;
			}
		}
		public void t前に移動()
		{
			CDTXMania.Skin.soundカーソル移動音.t再生する();
			if( this.b要素値にフォーカス中 )
			{
				this.list項目リスト[ this.n現在の選択項目 ].t項目値を次へ移動();
				t要素値を上下に変更中の処理();
			}
			else
			{
				this.n目標のスクロールカウンタ -= 100;
			}
		}
		private void t要素値を上下に変更中の処理()
		{
			if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemMasterVolume )				// #33700 2014.4.26 yyagi
			{
				CDTXMania.Sound管理.nMasterVolume = this.iSystemMasterVolume.n現在の値;
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			if( this.b活性化してる )
				return;

			this.list項目リスト = new List<CItemBase>();
			this.eメニュー種別 = Eメニュー種別.Unknown;

			#region [ スキン選択肢と、現在選択中のスキン(index)の準備 #28195 2012.5.2 yyagi ]
			int ns = ( CDTXMania.Skin.strSystemSkinSubfolders == null ) ? 0 : CDTXMania.Skin.strSystemSkinSubfolders.Length;
			int nb = ( CDTXMania.Skin.strBoxDefSkinSubfolders == null ) ? 0 : CDTXMania.Skin.strBoxDefSkinSubfolders.Length;
			skinSubFolders = new string[ ns + nb ];
			for ( int i = 0; i < ns; i++ )
			{
				skinSubFolders[ i ] = CDTXMania.Skin.strSystemSkinSubfolders[ i ];
			}
			for ( int i = 0; i < nb; i++ )
			{
				skinSubFolders[ ns + i ] = CDTXMania.Skin.strBoxDefSkinSubfolders[ i ];
			}
			skinSubFolder_org = CDTXMania.Skin.GetCurrentSkinSubfolderFullName( true );
			Array.Sort( skinSubFolders );
			skinNames = CSkin.GetSkinName( skinSubFolders );
			nSkinIndex = Array.BinarySearch( skinSubFolders, skinSubFolder_org );

            CDTXMania.Skin.CreateShutterList();
            shutterNames = CDTXMania.Skin.arGetShutterName();
            for( int i = 0; i < 3; i++ ) {
                nCurrentShutterImage[ i ] = Array.BinarySearch( shutterNames, CDTXMania.ConfigIni.strShutterImageName[ i ] );
                if( nCurrentShutterImage[ i ] < 0 )
                    nCurrentShutterImage[ i ] = 0;
            }

			if ( nSkinIndex < 0 )	// 念のため
			{
				nSkinIndex = 0;
			}
			nSkinSampleIndex = -1;
			#endregion

            // 2017.10.11 kairera0467 子クラスで初期化
//          this.prvFont = new CPrivateFastFont( CSkin.Path( @"Graphics\fonts\mplus-1p-heavy.ttf" ), 20 );	// t項目リストの設定 の前に必要
//			this.listMenu = new List<stMenuItemRight>();

			this.t項目リストの設定_Bass();		// #27795 2012.3.11 yyagi; System設定の中でDrumsの設定を参照しているため、
			this.t項目リストの設定_Guitar();	// 活性化の時点でDrumsの設定も入れ込んでおかないと、System設定中に例外発生することがある。
			this.t項目リストの設定_Drums();	// 
			this.t項目リストの設定_System();	// 順番として、最後にSystemを持ってくること。設定一覧の初期位置がSystemのため。
			this.b要素値にフォーカス中 = false;
			this.n目標のスクロールカウンタ = 0;
			this.n現在のスクロールカウンタ = 0;
			this.nスクロール用タイマ値 = -1;
			this.ct三角矢印アニメ = new CCounter();

			this.iSystemSoundType_initial			= this.iSystemSoundType.n現在選択されている項目番号;	// CONFIGに入ったときの値を保持しておく
			this.iSystemWASAPIBufferSizeMs_initial	= this.iSystemWASAPIBufferSizeMs.n現在の値;				// CONFIG脱出時にこの値から変更されているようなら
			// this.iSystemASIOBufferSizeMs_initial	= this.iSystemASIOBufferSizeMs.n現在の値;				// サウンドデバイスを再構築する
			this.iSystemASIODevice_initial			= this.iSystemASIODevice.n現在選択されている項目番号;	//
			this.iSystemSoundTimerType_initial      = this.iSystemSoundTimerType.GetIndex();				//
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.b活性化してない )
				return;
			if( this.ftフォント != null )													// 以下OPTIONと共通
			{
				this.ftフォント.Dispose();
				this.ftフォント = null;
			}

			this.tConfigIniへ記録する();
			this.list項目リスト.Clear();
			this.ct三角矢印アニメ = null;

			OnListMenuの解放();
			prvFont.Dispose();

			base.On非活性化();
			#region [ Skin変更 ]
			if ( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( true ) != this.skinSubFolder_org )
			{
				CDTXMania.stageChangeSkin.tChangeSkinMain();	// #28195 2012.6.11 yyagi CONFIG脱出時にSkin更新
			}
			#endregion

			// #24820 2013.1.22 yyagi CONFIGでWASAPI/ASIO/DirectSound関連の設定を変更した場合、サウンドデバイスを再構築する。
			// #33689 2014.6.17 yyagi CONFIGでSoundTimerTypeの設定を変更した場合も、サウンドデバイスを再構築する。
			#region [ サウンドデバイス変更 ]
			if ( this.iSystemSoundType_initial != this.iSystemSoundType.n現在選択されている項目番号 ||
				this.iSystemWASAPIBufferSizeMs_initial != this.iSystemWASAPIBufferSizeMs.n現在の値 ||
				// this.iSystemASIOBufferSizeMs_initial != this.iSystemASIOBufferSizeMs.n現在の値 ||
				this.iSystemASIODevice_initial != this.iSystemASIODevice.n現在選択されている項目番号 ||
				this.iSystemSoundTimerType_initial != this.iSystemSoundTimerType.GetIndex() )
			{
				ESoundDeviceType soundDeviceType;
				switch ( this.iSystemSoundType.n現在選択されている項目番号 )
				{
					case 0:
						soundDeviceType = ESoundDeviceType.DirectSound;
						break;
					case 1:
						soundDeviceType = ESoundDeviceType.ASIO;
						break;
					case 2:
						soundDeviceType = ESoundDeviceType.ExclusiveWASAPI;
						break;
                    case 3:
                        soundDeviceType = ESoundDeviceType.SharedWASAPI;
                        break;
					default:
						soundDeviceType = ESoundDeviceType.Unknown;
						break;
				}

				CDTXMania.Sound管理.t初期化( soundDeviceType,
										this.iSystemWASAPIBufferSizeMs.n現在の値,
                                        false,
										0,
										// this.iSystemASIOBufferSizeMs.n現在の値,
										this.iSystemASIODevice.n現在選択されている項目番号,
										this.iSystemSoundTimerType.bON );
				CDTXMania.app.ShowWindowTitleWithSoundType();
			}
			#endregion
			#region [ サウンドのタイムストレッチモード変更 ]
			FDK.CSound管理.bIsTimeStretch = this.iSystemTimeStretch.bON;
			#endregion
		}
		public override void OnManagedリソースの作成()
		{
			if( this.b活性化してない )
				return;

			this.tx通常項目行パネル = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\4_itembox.png" ), false );
			this.txその他項目行パネル = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\4_itembox other.png" ), false );
			this.tx三角矢印 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\4_triangle arrow.png" ), false );
			this.txSkinSample1 = null;		// スキン選択時に動的に設定するため、ここでは初期化しない

			if( CDTXMania.stageコンフィグ.bメニューにフォーカス中 )
			{
				this.t説明文パネルに現在選択されているメニューの説明を描画する( CDTXMania.stageコンフィグ.n現在のメニュー番号 );
			}
			else
			{
				this.t説明文パネルに現在選択されている項目の説明を描画する();
			}
			base.OnManagedリソースの作成();
		}
		public override void OnManagedリソースの解放()
		{
			if( this.b活性化してない )
				return;

			CDTXMania.tテクスチャの解放( ref this.txSkinSample1 );
			CDTXMania.tテクスチャの解放( ref this.tx通常項目行パネル );
			CDTXMania.tテクスチャの解放( ref this.txその他項目行パネル );
			CDTXMania.tテクスチャの解放( ref this.tx三角矢印 );
		
			base.OnManagedリソースの解放();
		}

		private void OnListMenuの初期化()
		{
			OnListMenuの解放();
			this.listMenu = new stMenuItemRight[ this.list項目リスト.Count ];
		}

		/// <summary>
		/// 事前にレンダリングしておいたテクスチャを解放する。
		/// </summary>
		private void OnListMenuの解放()
		{
			if ( listMenu != null )
			{
				for ( int i = 0; i < listMenu.Length; i++ )
				{
					if ( listMenu[ i ].txParam != null )
					{
						listMenu[ i ].txParam.Dispose();
					}
					if ( listMenu[ i ].txMenuItemRight != null )
					{
						listMenu[ i ].txMenuItemRight.Dispose();
					}
				}
				this.listMenu = null;
			}
		}
		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(bool)のほうを使用してください。" );
		}
		public virtual int t進行描画( bool b項目リスト側にフォーカスがある )
		{
			if( this.b活性化してない )
				return 0;

			// 進行
            /*
			#region [ 初めての進行描画 ]
			//-----------------
			if( base.b初めての進行描画 )
			{
                this.nスクロール用タイマ値 = CSound管理.rc演奏用タイマ.n現在時刻;
				this.ct三角矢印アニメ.t開始( 0, 9, 50, CDTXMania.Timer );
			
				base.b初めての進行描画 = false;
			}
			//-----------------
			#endregion

			this.b項目リスト側にフォーカスがある = b項目リスト側にフォーカスがある;		// 記憶

			#region [ 項目スクロールの進行 ]
			//-----------------
			long n現在時刻 = CDTXMania.Timer.n現在時刻;
			if( n現在時刻 < this.nスクロール用タイマ値 ) this.nスクロール用タイマ値 = n現在時刻;

			const int INTERVAL = 2;	// [ms]
			while( ( n現在時刻 - this.nスクロール用タイマ値 ) >= INTERVAL )
			{
				int n目標項目までのスクロール量 = Math.Abs( (int) ( this.n目標のスクロールカウンタ - this.n現在のスクロールカウンタ ) );
				int n加速度 = 0;

				#region [ n加速度の決定；目標まで遠いほど加速する。]
				//-----------------
				if( n目標項目までのスクロール量 <= 100 )
				{
					n加速度 = 2;
				}
				else if( n目標項目までのスクロール量 <= 300 )
				{
					n加速度 = 3;
				}
				else if( n目標項目までのスクロール量 <= 500 )
				{
					n加速度 = 4;
				}
				else
				{
					n加速度 = 8;
				}
				//-----------------
				#endregion
				#region [ this.n現在のスクロールカウンタに n加速度 を加減算。]
				//-----------------
				if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )
				{
					this.n現在のスクロールカウンタ += n加速度;
					if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )
					{
						// 目標を超えたら目標値で停止。
						this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;
					}
				}
				else if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )
				{
					this.n現在のスクロールカウンタ -= n加速度;
					if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )
					{
						// 目標を超えたら目標値で停止。
						this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;
					}
				}
				//-----------------
				#endregion
				#region [ 行超え処理、ならびに目標位置に到達したらスクロールを停止して項目変更通知を発行。]
				//-----------------
				if( this.n現在のスクロールカウンタ >= 100 )
				{
					this.n現在の選択項目 = this.t次の項目( this.n現在の選択項目 );
					this.n現在のスクロールカウンタ -= 100;
					this.n目標のスクロールカウンタ -= 100;
					if( this.n目標のスクロールカウンタ == 0 )
					{
						CDTXMania.stageコンフィグ.t項目変更通知();
					}
				}
				else if( this.n現在のスクロールカウンタ <= -100 )
				{
					this.n現在の選択項目 = this.t前の項目( this.n現在の選択項目 );
					this.n現在のスクロールカウンタ += 100;
					this.n目標のスクロールカウンタ += 100;
					if( this.n目標のスクロールカウンタ == 0 )
					{
						CDTXMania.stageコンフィグ.t項目変更通知();
					}
				}
				//-----------------
				#endregion

				this.nスクロール用タイマ値 += INTERVAL;
			}
			//-----------------
			#endregion
			
			#region [ ▲印アニメの進行 ]
			//-----------------
			if( this.b項目リスト側にフォーカスがある && ( this.n目標のスクロールカウンタ == 0 ) )
				this.ct三角矢印アニメ.t進行Loop();
			//-----------------
			#endregion


			// 描画

			this.ptパネルの基本座標[ 4 ].X = this.b項目リスト側にフォーカスがある ? 276 : 301;		// メニューにフォーカスがあるなら、項目リストの中央は頭を出さない。

			#region [ 計11個の項目パネルを描画する。]
			//-----------------
			int nItem = this.n現在の選択項目;
			for( int i = 0; i < 4; i++ )
				nItem = this.t前の項目( nItem );

			for( int n行番号 = -4; n行番号 < 6; n行番号++ )		// n行番号 == 0 がフォーカスされている項目パネル。
			{
				#region [ 今まさに画面外に飛びだそうとしている項目パネルは描画しない。]
				//-----------------
				if( ( ( n行番号 == -4 ) && ( this.n現在のスクロールカウンタ > 0 ) ) ||		// 上に飛び出そうとしている
					( ( n行番号 == +5 ) && ( this.n現在のスクロールカウンタ < 0 ) ) )		// 下に飛び出そうとしている
				{
					nItem = this.t次の項目( nItem );
					continue;
				}
				//-----------------
				#endregion

				int n移動元の行の基本位置 = n行番号 + 4;
				int n移動先の行の基本位置 = ( this.n現在のスクロールカウンタ <= 0 ) ? ( ( n移動元の行の基本位置 + 1 ) % 10 ) : ( ( ( n移動元の行の基本位置 - 1 ) + 10 ) % 10 );
				int x = this.ptパネルの基本座標[ n移動元の行の基本位置 ].X + ( (int) ( ( this.ptパネルの基本座標[ n移動先の行の基本位置 ].X - this.ptパネルの基本座標[ n移動元の行の基本位置 ].X ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );
				int y = this.ptパネルの基本座標[ n移動元の行の基本位置 ].Y + ( (int) ( ( this.ptパネルの基本座標[ n移動先の行の基本位置 ].Y - this.ptパネルの基本座標[ n移動元の行の基本位置 ].Y ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );

				#region [ 現在の行の項目パネル枠を描画。]
				//-----------------
				switch ( this.list項目リスト[ nItem ].eパネル種別 )
				{
					case CItemBase.Eパネル種別.通常:
						if ( this.tx通常項目行パネル != null )
							this.tx通常項目行パネル.t2D描画( CDTXMania.app.Device, x * Scale.X, y * Scale.Y );
						break;

					case CItemBase.Eパネル種別.その他:
						if ( this.txその他項目行パネル != null )
							this.txその他項目行パネル.t2D描画( CDTXMania.app.Device, x * Scale.X, y * Scale.Y );
						break;
				}
				//-----------------
				#endregion
				#region [ 現在の行の項目名を描画。]
				//-----------------
				if ( listMenu[ nItem ].txMenuItemRight != null )	// 自前のキャッシュに含まれているようなら、再レンダリングせずキャッシュを使用
				{
					listMenu[ nItem ].txMenuItemRight.t2D描画( CDTXMania.app.Device, x + 337, (int)( ( y + 18 ) * 1.5 - 20 ) );
				}
				else
				{
					Bitmap bmpItem = prvFont.DrawPrivateFont( this.list項目リスト[ nItem ].str項目名, Color.White, Color.Black );
					listMenu[ nItem ].txMenuItemRight = CDTXMania.tテクスチャの生成( bmpItem );
					//					ctItem.t2D描画( CDTXMania.app.Device, ( x + 0x12 ) * Scale.X, ( y + 12 ) * Scale.Y - 20 );
					//					CDTXMania.tテクスチャの解放( ref ctItem );
					CDTXMania.t安全にDisposeする( ref bmpItem );
				}
				//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 0x12, y + 12, this.list項目リスト[ nItem ].str項目名 );
				//-----------------
				#endregion
				#region [ 現在の行の項目の要素を描画。]
				//-----------------
				string strParam = null;
				bool b強調 = false;
				switch ( this.list項目リスト[ nItem ].e種別 )
				{
					case CItemBase.E種別.ONorOFFトグル:
						#region [ *** ]
						//-----------------
						//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, ( (CItemToggle) this.list項目リスト[ nItem ] ).bON ? "ON" : "OFF" );
						strParam = ( (CItemToggle) this.list項目リスト[ nItem ] ).bON ? "ON" : "OFF";
						break;
					//-----------------
						#endregion

					case CItemBase.E種別.ONorOFFor不定スリーステート:
						#region [ *** ]
						//-----------------
						switch ( ( (CItemThreeState) this.list項目リスト[ nItem ] ).e現在の状態 )
						{
							case CItemThreeState.E状態.ON:
								strParam = "ON";
								break;

							case CItemThreeState.E状態.不定:
								strParam = "- -";
								break;

							default:
								strParam = "OFF";
								break;
						}
						//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, "ON" );
						break;
					//-----------------
						#endregion

					case CItemBase.E種別.整数:		// #24789 2011.4.8 yyagi: add PlaySpeed supports (copied them from OPTION)
						#region [ *** ]
						//-----------------
						if ( this.list項目リスト[ nItem ] == this.iCommonPlaySpeed )
						{
							double d = ( (double) ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値 ) / 20.0;
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, d.ToString( "0.000" ), ( n行番号 == 0 ) && this.b要素値にフォーカス中 );
							strParam = d.ToString( "0.000" );
						}
						else if ( this.list項目リスト[ nItem ] == this.iDrumsScrollSpeed || this.list項目リスト[ nItem ] == this.iGuitarScrollSpeed || this.list項目リスト[ nItem ] == this.iBassScrollSpeed )
						{
							float f = ( ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値 + 1 ) * 0.5f;
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, f.ToString( "x0.0" ), ( n行番号 == 0 ) && this.b要素値にフォーカス中 );
							strParam = f.ToString( "x0.0" );
						}
						else
						{
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値.ToString(), ( n行番号 == 0 ) && this.b要素値にフォーカス中 );
							strParam = ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値.ToString();
						}
						b強調 = ( n行番号 == 0 ) && this.b要素値にフォーカス中;
						break;
					//-----------------
						#endregion

					case CItemBase.E種別.リスト:	// #28195 2012.5.2 yyagi: add Skin supports
						#region [ *** ]
						//-----------------
						{
							CItemList list = (CItemList) this.list項目リスト[ nItem ];
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, list.list項目値[ list.n現在選択されている項目番号 ] );
							strParam = list.list項目値[ list.n現在選択されている項目番号 ];

							#region [ 必要な場合に、Skinのサンプルを生成・描画する。#28195 2012.5.2 yyagi ]
							if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemSkinSubfolder )
							{
								tGenerateSkinSample();		// 最初にSkinの選択肢にきたとき(Enterを押す前)に限り、サンプル生成が発生する。
								if ( txSkinSample1 != null )
								{
									txSkinSample1.t2D描画( CDTXMania.app.Device, 124, 409 );
								}
							}
							#endregion
							break;
						}
					//-----------------
						#endregion
				}
				if ( b強調 )
				{
					Bitmap bmpStr = b強調 ?
						prvFont.DrawPrivateFont( strParam, Color.White, Color.Black, Color.Yellow, Color.OrangeRed ) :
						prvFont.DrawPrivateFont( strParam, Color.White, Color.Black );
					CTexture txStr = CDTXMania.tテクスチャの生成( bmpStr, false );
					txStr.t2D描画( CDTXMania.app.Device, ( x + 210 ) * Scale.X, (int)( ( y + 18 ) * 1.5 - 20 ) );
					CDTXMania.tテクスチャの解放( ref txStr );
					CDTXMania.t安全にDisposeする( ref bmpStr );
				}
				else
				{
					int nIndex = this.list項目リスト[ nItem ].GetIndex();
					if ( listMenu[ nItem ].nParam != nIndex || listMenu[ nItem ].txParam == null )
					{
						stMenuItemRight stm = listMenu[ nItem ];
						stm.nParam = nIndex;
						object o = this.list項目リスト[ nItem ].obj現在値();
						stm.strParam = ( o == null ) ? "" : o.ToString();

						Bitmap bmpStr =
							prvFont.DrawPrivateFont( strParam, Color.White, Color.Black );
						stm.txParam = CDTXMania.tテクスチャの生成( bmpStr, false );
						CDTXMania.t安全にDisposeする( ref bmpStr );

						listMenu[ nItem ] = stm;
					}
					listMenu[ nItem ].txParam.t2D描画( CDTXMania.app.Device, ( x + 210 ) * Scale.X, (int)( ( y + 18 ) * 1.5 - 20 ) );
				}
				//-----------------
				#endregion
				
				nItem = this.t次の項目( nItem );
			}
			//-----------------
			#endregion
			
			#region [ 項目リストにフォーカスがあって、かつスクロールが停止しているなら、パネルの上下に▲印を描画する。]
			//-----------------
			if( this.b項目リスト側にフォーカスがある && ( this.n目標のスクロールカウンタ == 0 ) )
			{
				int x;
				int y_upper;
				int y_lower;
			
				// 位置決定。

				if( this.b要素値にフォーカス中 )
				{
					x = 528;	// 要素値の上下あたり。
					y_upper = 198 - this.ct三角矢印アニメ.n現在の値;
					y_lower = 242 + this.ct三角矢印アニメ.n現在の値;
				}
				else
				{
					x = 276;	// 項目名の上下あたり。
					y_upper = 186 - this.ct三角矢印アニメ.n現在の値;
					y_lower = 254 + this.ct三角矢印アニメ.n現在の値;
				}

				// 描画。

				if ( this.tx三角矢印 != null )
				{
					this.tx三角矢印.t2D描画( CDTXMania.app.Device, x * Scale.X, y_upper * Scale.Y, new Rectangle( 0, 0, (int) ( 32 * Scale.X ), (int) ( 16 * Scale.Y ) ) );
					this.tx三角矢印.t2D描画( CDTXMania.app.Device, x * Scale.X, y_lower * Scale.Y, new Rectangle( 0, (int) ( 16 * Scale.Y ), (int) ( 32 * Scale.X ), (int) ( 16 * Scale.Y ) ) );
				}
			}
			//-----------------
			#endregion
            */
			return 0;
		}
	

		// その他

		#region [ private ]
		//-----------------
		private enum Eメニュー種別
		{
			System,
			Drums,
			Guitar,
			Bass,
			KeyAssignSystem,		// #24609 2011.4.12 yyagi: 画面キャプチャキーのアサイン
			KeyAssignDrums,
			KeyAssignGuitar,
			KeyAssignBass,
			Unknown

		}

		protected bool b項目リスト側にフォーカスがある;
		protected bool b要素値にフォーカス中;
		protected CCounter ct三角矢印アニメ;
		private Eメニュー種別 eメニュー種別;
		#region [ キーコンフィグ ]
		private CItemBase iKeyAssignSystemCapture;			// #24609
		private CItemBase iKeyAssignSystemReturnToMenu;		// #24609
		private CItemBase iKeyAssignBassB;
		private CItemBase iKeyAssignBassCancel;
		private CItemBase iKeyAssignBassDecide;
		private CItemBase iKeyAssignBassG;
		private CItemBase iKeyAssignBassPick;
		private CItemBase iKeyAssignBassR;
		private CItemBase iKeyAssignBassReturnToMenu;
		private CItemBase iKeyAssignBassWail;
		private CItemBase iKeyAssignDrumsBD;
		private CItemBase iKeyAssignDrumsCY;
		private CItemBase iKeyAssignDrumsFT;
		private CItemBase iKeyAssignDrumsHHC;
		private CItemBase iKeyAssignDrumsHHO;
		private CItemBase iKeyAssignDrumsHT;
		private CItemBase iKeyAssignDrumsLC;
		private CItemBase iKeyAssignDrumsLT;
		private CItemBase iKeyAssignDrumsRD;
		private CItemBase iKeyAssignDrumsReturnToMenu;
		private CItemBase iKeyAssignDrumsSD;
		private CItemBase iKeyAssignDrumsLP;	// #27029 2012.1.4 from
        private CItemBase iKeyAssignDrumsLBD;
		private CItemBase iKeyAssignGuitarB;
		private CItemBase iKeyAssignGuitarCancel;
		private CItemBase iKeyAssignGuitarDecide;
		private CItemBase iKeyAssignGuitarG;
		private CItemBase iKeyAssignGuitarPick;
		private CItemBase iKeyAssignGuitarR;
		private CItemBase iKeyAssignGuitarReturnToMenu;
		private CItemBase iKeyAssignGuitarWail;
		#endregion
		private CItemToggle iLogOutputLog;
		private CItemToggle iSystemAdjustWaves;
		private CItemToggle iSystemAudienceSound;
		private CItemInteger iSystemAutoChipVolume;
		private CItemToggle iSystemAVI;
		private CItemToggle iSystemForceAVIFullscreen;
		private CItemToggle iSystemBGA;
//		private CItemToggle iSystemGraph; #24074 2011.01.23 comment-out ikanick オプション(Drums)へ移行
		private CItemToggle iSystemBGMSound;
		private CItemInteger iSystemChipVolume;
		private CItemList iSystemCYGroup;
		private CItemToggle iSystemCymbalFree;
		private CItemList iSystemDamageLevel;
		private CItemToggle iSystemDebugInfo;
//		private CItemToggle iSystemDrums;
		private CItemToggle iSystemFillIn;
		private CItemList iSystemFTGroup;
		private CItemToggle iSystemFullscreen;
//		private CItemToggle iSystemGuitar;
		private CItemList iSystemHHGroup;
		private CItemList iSystemBDGroup;		// #27029 2012.1.4 from
		private CItemToggle iSystemHitSound;
		private CItemList iSystemHitSoundPriorityCY;
		private CItemList iSystemHitSoundPriorityFT;
		private CItemList iSystemHitSoundPriorityHH;
		private CItemInteger iSystemMinComboBass;
		private CItemInteger iSystemMinComboDrums;
		private CItemInteger iSystemMinComboGuitar;
		private CItemInteger iSystemPreviewSoundWait;
		private CItemToggle iSystemRandomFromSubBox;
		private CItemBase iSystemReturnToMenu;
		private CItemToggle iSystemSaveScore;
        private CItemList iSystemSkillMode;
		private CItemToggle iSystemSoundMonitorBass;
		private CItemToggle iSystemSoundMonitorDrums;
		private CItemToggle iSystemSoundMonitorGuitar;
		private CItemToggle iSystemStageFailed;
		private CItemToggle iSystemVSyncWait;
		private CItemList	iSystemShowLag;					// #25370 2011.6.3 yyagi
		private CItemToggle iSystemAutoResultCapture;		// #25399 2011.6.9 yyagi
		private CItemToggle iSystemBufferedInput;
		private CItemInteger iSystemRisky;					// #23559 2011.7.27 yyagi
		private CItemList iSystemSoundType;					// #24820 2013.1.3 yyagi
		private CItemInteger iSystemWASAPIBufferSizeMs;		// #24820 2013.1.15 yyagi
//		private CItemInteger iSystemASIOBufferSizeMs;		// #24820 2013.1.3 yyagi
		private CItemList	iSystemASIODevice;				// #24820 2013.1.17 yyagi
        private CItemInteger iSystemBGMAdjust;              // #36372 2016.06.19 kairera0467

		private int iSystemSoundType_initial;
		private int iSystemWASAPIBufferSizeMs_initial;
//		private int iSystemASIOBufferSizeMs_initial;
		private int iSystemASIODevice_initial;
		private CItemToggle iSystemSoundTimerType;			// #33689 2014.6.17 yyagi
		private int iSystemSoundTimerType_initial;			// #33689 2014.6.17 yyagi
        private CItemToggle iSystemWASAPIEventDriven;

		private CItemToggle iSystemTimeStretch;				// #23664 2013.2.24 yyagi
		private CItemList iSystemJudgePosGuitar;			// #33891 2014.6.26 yyagi
		private CItemList iSystemJudgePosBass;				// #33891 2014.6.26 yyagi

		//private CItemList iDrumsJudgeDispPriority;	//
		//private CItemList iGuitarJudgeDispPriority;	//
		//private CItemList iBassJudgeDispPriority;		//
		private CItemList iSystemJudgeDispPriority;

        #region[ XG ]
        private CItemList iSystemNamePlateType;
        private CItemToggle iSystemJudgeCountDisp;
        private CItemToggle iSystemWindowClipDisp;
        #endregion

        protected List<CItemBase> list項目リスト;
		protected long nスクロール用タイマ値;
		protected int n現在のスクロールカウンタ;
		protected int n目標のスクロールカウンタ;
		protected CTextureAf txその他項目行パネル;
		protected CTexture tx三角矢印;
		protected CTextureAf tx通常項目行パネル;
        protected CTexture tx説明文;
        protected CTexture tx説明文パネル;
        protected Font ftフォント;

        protected CPrivateFastFont prvFont;
		//private List<string> list項目リスト_str最終描画名;
		protected struct stMenuItemRight
		{
			//	public string strMenuItem;
			public CTexture txMenuItemRight;
			public int nParam;
			public string strParam;
			public CTexture txParam;
		}
		protected stMenuItemRight[] listMenu;

		protected CTexture txSkinSample1;				// #28195 2012.5.2 yyagi
		private string[] skinSubFolders;			//
		private string[] skinNames;					//
		private string skinSubFolder_org;			//
		private int nSkinSampleIndex;				//
		private int nSkinIndex;						//
        private string[] shutterNames;              // #36144 kairera0467
        private STDGBVALUE<int> nCurrentShutterImage;

		private CItemBase iDrumsGoToKeyAssign;
		private CItemBase iGuitarGoToKeyAssign;
		private CItemBase iBassGoToKeyAssign;
		private CItemBase iSystemGoToKeyAssign;		// #24609

		private CItemList iSystemGRmode;

		//private CItemToggle iBassAutoPlay;
		private CItemThreeState iBassAutoPlayAll;			// #23886 2012.5.8 yyagi
		private CItemToggle iBassR;							//
		private CItemToggle iBassG;							//
		private CItemToggle iBassB;                         //
		private CItemToggle iBassY;                         // 2023.11.8 kairera0467
		private CItemToggle iBassP;                         //
		private CItemToggle iBassPick;						//
		private CItemToggle iBassW;							//

        private CItemList iBassJust;
		private CItemToggle iBassLeft;
		private CItemToggle iBassLight;
		private CItemList iBassPosition;
		private CItemList iBassRandom;
		private CItemBase iBassReturnToMenu;
		private CItemToggle iBassReverse;
		protected CItemInteger iBassScrollSpeed;
        private CItemList iBassDark;
        private CItemList iBassLaneDispType;
        private CItemToggle iBassJudgeLineDisp;
        private CItemToggle iBassLaneFlush;
        private CItemToggle iBassGraph;
        private CItemList iBassShutterImage;
        
		protected CItemInteger iCommonPlaySpeed;
//		private CItemBase iCommonReturnToMenu;

		private CItemThreeState iDrumsAutoPlayAll;
		private CItemToggle iDrumsBass;
		private CItemToggle iDrumsCymbalRide;
		private CItemToggle iDrumsFloorTom;
		//private CItemToggle iDrumsHidden;
		private CItemToggle iDrumsHighTom;
		private CItemToggle iDrumsHiHat;
        private CItemInteger iDrumsJudgeLinePos;
        private CItemInteger iDrumsJudgeLineOffset;
        private CItemList iDrumsJust;
        private CItemList iDrumsLaneType;
		private CItemToggle iDrumsLeftBassDrum;
		private CItemToggle iDrumsLeftCymbal;
		private CItemToggle iDrumsLeftPedal;
		private CItemToggle iDrumsLowTom;
        private CItemList iDrumsRDPosition;
		private CItemList iDrumsPosition;
		private CItemBase iDrumsReturnToMenu;
		private CItemToggle iDrumsReverse;
		protected CItemInteger iDrumsScrollSpeed;
        private CItemInteger iDrumsShutterInPos;
        private CItemInteger iDrumsShutterOutPos;
		private CItemToggle iDrumsSnare;
		//private CItemToggle iDrumsSudden;
		private CItemToggle iDrumsTight;
		private CItemToggle iDrumsGraph;        // #24074 2011.01.23 add ikanick
        private CItemList iDrumsDark;
        private CItemList iDrumsLaneDispType;
        private CItemToggle iDrumsJudgeLineDisp;
        private CItemToggle iDrumsLaneFlush;
        private CItemList iDrumsShutterImage;

        private CItemToggle iDrumsAssignToLBD;
        private CItemList iDrumsRandomPad;
        private CItemList iDrumsRandomPedal;
        private CItemList iDrumsNumOfLanes;
        private CItemList iDrumsDkdkType;

        //private CItemToggle iGuitarAutoPlay;
        private CItemThreeState iGuitarAutoPlayAll;			// #23886 2012.5.8 yyagi
		private CItemToggle iGuitarR;						//
		private CItemToggle iGuitarG;						//
		private CItemToggle iGuitarB;                       //
		private CItemToggle iGuitarY;						// 2023.11.8 kairera0467
		private CItemToggle iGuitarP;						//
		private CItemToggle iGuitarPick;					//
		private CItemToggle iGuitarW;						//

        private CItemList iGuitarJust;
		private CItemToggle iGuitarLeft;
		private CItemToggle iGuitarLight;
		private CItemList iGuitarPosition;
		private CItemList iGuitarRandom;
		private CItemBase iGuitarReturnToMenu;
		private CItemToggle iGuitarReverse;
		protected CItemInteger iGuitarScrollSpeed;
        private CItemList iGuitarDark;
        private CItemList iGuitarLaneDispType;
        private CItemToggle iGuitarJudgeLineDisp;
        private CItemToggle iGuitarLaneFlush;
        private CItemToggle iGuitarGraph;
        private CItemList iGuitarShutterImage;

		private CItemInteger iDrumsInputAdjustTimeMs;		// #23580 2011.1.3 yyagi
		private CItemInteger iGuitarInputAdjustTimeMs;		//
		private CItemInteger iBassInputAdjustTimeMs;		//
		protected CItemList iSystemSkinSubfolder;				// #28195 2012.5.2 yyagi
		private CItemToggle iSystemUseBoxDefSkin;			// #28195 2012.5.6 yyagi
		private CItemList iDrumsSudHid;						// #32072 2013.9.20 yyagi
		private CItemList iGuitarSudHid;					// #32072 2013.9.20 yyagi
		private CItemList iBassSudHid;						// #32072 2013.9.20 yyagi
		private CItemBase iSystemReloadDTX;					// #32081 2013.10.21 yyagi
		private CItemInteger iSystemMasterVolume;			// #33700 2014.4.26 yyagi
        private CItemInteger iDrumsPedalJudgeRangeDelta;    // #39397 2019.07.19 kairera0467
		private CItemToggle iDrumsMatixxFrameDisp;			// 2019.09.07 kairera0467

		protected int t前の項目( int nItem )
		{
			if( --nItem < 0 )
			{
				nItem = this.list項目リスト.Count - 1;
			}
			return nItem;
		}
		protected int t次の項目( int nItem )
		{
			if( ++nItem >= this.list項目リスト.Count )
			{
				nItem = 0;
			}
			return nItem;
		}
		private void t全部のドラムパッドのAutoを切り替える( bool bAutoON )
		{
			this.iDrumsLeftCymbal.bON = this.iDrumsHiHat.bON = this.iDrumsSnare.bON = this.iDrumsBass.bON = this.iDrumsHighTom.bON = this.iDrumsLowTom.bON = this.iDrumsFloorTom.bON = this.iDrumsCymbalRide.bON = this.iDrumsLeftPedal.bON = this.iDrumsLeftBassDrum.bON = bAutoON;
		}
		private void t全部のギターパッドのAutoを切り替える( bool bAutoON )
		{
			this.iGuitarR.bON = this.iGuitarG.bON = this.iGuitarB.bON = this.iGuitarY.bON = this.iGuitarP.bON = this.iGuitarPick.bON = this.iGuitarW.bON = bAutoON;
		}
		private void t全部のベースパッドのAutoを切り替える( bool bAutoON )
		{
			this.iBassR.bON = this.iBassG.bON = this.iBassB.bON = this.iBassY.bON = this.iBassP.bON = this.iBassPick.bON = this.iBassW.bON = bAutoON;
		}
		private void tConfigIniへ記録する()
		{
			switch( this.eメニュー種別 )
			{
				case Eメニュー種別.System:
					this.tConfigIniへ記録する_System();
					this.tConfigIniへ記録する_KeyAssignSystem();
					return;

				case Eメニュー種別.Drums:
					this.tConfigIniへ記録する_Drums();
					this.tConfigIniへ記録する_KeyAssignDrums();
					return;

				case Eメニュー種別.Guitar:
					this.tConfigIniへ記録する_Guitar();
					this.tConfigIniへ記録する_KeyAssignGuitar();
					return;

				case Eメニュー種別.Bass:
					this.tConfigIniへ記録する_Bass();
					this.tConfigIniへ記録する_KeyAssignBass();
					return;
			}
		}
		private void tConfigIniへ記録する_KeyAssignBass()
		{
		}
		private void tConfigIniへ記録する_KeyAssignDrums()
		{
		}
		private void tConfigIniへ記録する_KeyAssignGuitar()
		{
		}
		private void tConfigIniへ記録する_KeyAssignSystem()
		{
		}
		private void tConfigIniへ記録する_System()
		{
			//CDTXMania.ConfigIni.eDark = (Eダークモード) this.iCommonDark.n現在選択されている項目番号;
			CDTXMania.ConfigIni.n演奏速度 = this.iCommonPlaySpeed.n現在の値;

			CDTXMania.ConfigIni.bGuitar有効 = ( ( ( this.iSystemGRmode.n現在選択されている項目番号 + 1 ) / 2 ) == 1 );
				//this.iSystemGuitar.bON;
			CDTXMania.ConfigIni.bDrums有効 = ( ( ( this.iSystemGRmode.n現在選択されている項目番号 + 1 ) % 2 ) == 1 );
				//this.iSystemDrums.bON;

			CDTXMania.ConfigIni.b全画面モード = this.iSystemFullscreen.bON;
			CDTXMania.ConfigIni.bSTAGEFAILED有効 = this.iSystemStageFailed.bON;
			CDTXMania.ConfigIni.bランダムセレクトで子BOXを検索対象とする = this.iSystemRandomFromSubBox.bON;

			CDTXMania.ConfigIni.bWave再生位置自動調整機能有効 = this.iSystemAdjustWaves.bON;
			CDTXMania.ConfigIni.b垂直帰線待ちを行う = this.iSystemVSyncWait.bON;
			CDTXMania.ConfigIni.bバッファ入力を行う = this.iSystemBufferedInput.bON;
			CDTXMania.ConfigIni.bAVI有効 = this.iSystemAVI.bON;
			CDTXMania.ConfigIni.bForceAVIFullscreen = this.iSystemForceAVIFullscreen.bON;
			CDTXMania.ConfigIni.bBGA有効 = this.iSystemBGA.bON;
//			CDTXMania.ConfigIni.bGraph有効 = this.iSystemGraph.bON;#24074 2011.01.23 comment-out ikanick オプション(Drums)へ移行
			CDTXMania.ConfigIni.n曲が選択されてからプレビュー音が鳴るまでのウェイトms = this.iSystemPreviewSoundWait.n現在の値;
            //CDTXMania.ConfigIni.n曲が選択されてからプレビュー画像が表示開始されるまでのウェイトms = this.iSystemPreviewImageWait.n現在の値;
			CDTXMania.ConfigIni.b演奏情報を表示する = this.iSystemDebugInfo.bON;
			//CDTXMania.ConfigIni.n背景の透過度 = this.iSystemBGAlpha.n現在の値;
			CDTXMania.ConfigIni.bBGM音を発声する = this.iSystemBGMSound.bON;
			CDTXMania.ConfigIni.b歓声を発声する = this.iSystemAudienceSound.bON;
			CDTXMania.ConfigIni.eダメージレベル = (Eダメージレベル) this.iSystemDamageLevel.n現在選択されている項目番号;
			CDTXMania.ConfigIni.bScoreIniを出力する = this.iSystemSaveScore.bON;

			CDTXMania.ConfigIni.bログ出力 = this.iLogOutputLog.bON;
			CDTXMania.ConfigIni.n手動再生音量 = this.iSystemChipVolume.n現在の値;
			CDTXMania.ConfigIni.n自動再生音量 = this.iSystemAutoChipVolume.n現在の値;
			//CDTXMania.ConfigIni.bストイックモード = this.iSystemStoicMode.bON;

			CDTXMania.ConfigIni.nShowLagType = this.iSystemShowLag.n現在選択されている項目番号;				// #25370 2011.6.3 yyagi
			CDTXMania.ConfigIni.bIsAutoResultCapture = this.iSystemAutoResultCapture.bON;					// #25399 2011.6.9 yyagi

			//CDTXMania.ConfigIni.nRisky = this.iSystemRisky.n現在の値;										// #23559 2011.7.27 yyagi

			CDTXMania.ConfigIni.strSystemSkinSubfolderFullName = skinSubFolders[ nSkinIndex ];				// #28195 2012.5.2 yyagi
			CDTXMania.Skin.SetCurrentSkinSubfolderFullName( CDTXMania.ConfigIni.strSystemSkinSubfolderFullName, true );
			CDTXMania.ConfigIni.bUseBoxDefSkin = this.iSystemUseBoxDefSkin.bON;								// #28195 2012.5.6 yyagi

			CDTXMania.ConfigIni.nSoundDeviceType = this.iSystemSoundType.n現在選択されている項目番号;		// #24820 2013.1.3 yyagi
			CDTXMania.ConfigIni.nWASAPIBufferSizeMs = this.iSystemWASAPIBufferSizeMs.n現在の値;				// #24820 2013.1.15 yyagi
//			CDTXMania.ConfigIni.nASIOBufferSizeMs = this.iSystemASIOBufferSizeMs.n現在の値;					// #24820 2013.1.3 yyagi
			CDTXMania.ConfigIni.nASIODevice = this.iSystemASIODevice.n現在選択されている項目番号;			// #24820 2013.1.17 yyagi
			CDTXMania.ConfigIni.bUseOSTimer = this.iSystemSoundTimerType.bON;								// #33689 2014.6.17 yyagi

			CDTXMania.ConfigIni.bTimeStretch = this.iSystemTimeStretch.bON;									// #23664 2013.2.24 yyagi
//Trace.TraceInformation( "saved" );
//Trace.TraceInformation( "Skin現在Current : " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName(true) );
//Trace.TraceInformation( "Skin現在System  : " + CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在BoxDef  : " + CSkin.strBoxDefSkinSubfolderFullName );
			CDTXMania.ConfigIni.nMasterVolume = this.iSystemMasterVolume.n現在の値;							// #33700 2014.4.26 yyagi
			CDTXMania.ConfigIni.e判定表示優先度 = (E判定表示優先度) this.iSystemJudgeDispPriority.n現在選択されている項目番号;

            CDTXMania.ConfigIni.eSkillMode = (ESkillType)this.iSystemSkillMode.n現在選択されている項目番号;
            CDTXMania.ConfigIni.eNamePlateType = (Eタイプ)this.iSystemNamePlateType.n現在選択されている項目番号;
            CDTXMania.ConfigIni.bJudgeCountDisp = this.iSystemJudgeCountDisp.bON;
            CDTXMania.ConfigIni.nCommonBGMAdjustMs = this.iSystemBGMAdjust.n現在の値;                       // #36372 2016.06.19 kairera0467
            CDTXMania.ConfigIni.bWindowClipMode = this.iSystemWindowClipDisp.bON;                           // #37846 2017.12.29 kairera0467
		}
		private void tConfigIniへ記録する_Bass()
		{
			//CDTXMania.ConfigIni.bAutoPlay.Bass = this.iBassAutoPlay.bON;
			CDTXMania.ConfigIni.bAutoPlay.BsR = this.iBassR.bON;
			CDTXMania.ConfigIni.bAutoPlay.BsG = this.iBassG.bON;
			CDTXMania.ConfigIni.bAutoPlay.BsB = this.iBassB.bON;
			CDTXMania.ConfigIni.bAutoPlay.BsY = this.iBassY.bON;
			CDTXMania.ConfigIni.bAutoPlay.BsP = this.iBassP.bON;
			CDTXMania.ConfigIni.bAutoPlay.BsPick = this.iBassPick.bON;
			CDTXMania.ConfigIni.bAutoPlay.BsW = this.iBassW.bON;
			CDTXMania.ConfigIni.n譜面スクロール速度.Bass = this.iBassScrollSpeed.n現在の値;
												// "Sudden" || "Sud+Hid"
			CDTXMania.ConfigIni.bSudden.Bass = ( this.iBassSudHid.n現在選択されている項目番号 == 1 || this.iBassSudHid.n現在選択されている項目番号 == 3 ) ? true : false;
												// "Hidden" || "Sud+Hid"
			CDTXMania.ConfigIni.bHidden.Bass = ( this.iBassSudHid.n現在選択されている項目番号 == 2 || this.iBassSudHid.n現在選択されている項目番号 == 3 ) ? true : false;
			if      ( this.iBassSudHid.n現在選択されている項目番号 == 4 ) CDTXMania.ConfigIni.eInvisible.Bass = EInvisible.SEMI;	// "S-Invisible"
			else if ( this.iBassSudHid.n現在選択されている項目番号 == 5 ) CDTXMania.ConfigIni.eInvisible.Bass = EInvisible.FULL;	// "F-Invisible"
			else                                                          CDTXMania.ConfigIni.eInvisible.Bass = EInvisible.OFF;
			CDTXMania.ConfigIni.bReverse.Bass = this.iBassReverse.bON;
			CDTXMania.ConfigIni.判定文字表示位置.Bass = (E判定文字表示位置) this.iBassPosition.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eRandom.Bass = (Eランダムモード) this.iBassRandom.n現在選択されている項目番号;
			CDTXMania.ConfigIni.bLight.Bass = this.iBassLight.bON;
			CDTXMania.ConfigIni.bLeft.Bass = this.iBassLeft.bON;
			CDTXMania.ConfigIni.nInputAdjustTimeMs.Bass = this.iBassInputAdjustTimeMs.n現在の値;		// #23580 2011.1.3 yyagi

			CDTXMania.ConfigIni.b演奏音を強調する.Bass = this.iSystemSoundMonitorBass.bON;
			CDTXMania.ConfigIni.n表示可能な最小コンボ数.Bass = this.iSystemMinComboBass.n現在の値;
			CDTXMania.ConfigIni.e判定位置.Bass = (E判定位置) this.iSystemJudgePosBass.n現在選択されている項目番号;					// #33891 2014.6.26 yyagi
			//CDTXMania.ConfigIni.e判定表示優先度.Bass = (E判定表示優先度) this.iBassJudgeDispPriority.n現在選択されている項目番号;
            CDTXMania.ConfigIni.bLaneFlush.Bass = this.iBassLaneFlush.bON;
            CDTXMania.ConfigIni.nLaneDispType.Bass = this.iBassLaneDispType.n現在選択されている項目番号;
            CDTXMania.ConfigIni.bJudgeLineDisp.Bass = this.iBassJudgeLineDisp.bON;
            CDTXMania.ConfigIni.bGraph.Bass = this.iBassGraph.bON;
            CDTXMania.ConfigIni.eJUST.Bass = (EJust)this.iBassJust.n現在選択されている項目番号;
            CDTXMania.ConfigIni.strShutterImageName.Bass = this.iBassShutterImage.list項目値[ this.iBassShutterImage.n現在選択されている項目番号 ];
		}
		private void tConfigIniへ記録する_Drums()
		{
			CDTXMania.ConfigIni.bAutoPlay.LC = this.iDrumsLeftCymbal.bON;
			CDTXMania.ConfigIni.bAutoPlay.HH = this.iDrumsHiHat.bON;
			CDTXMania.ConfigIni.bAutoPlay.SD = this.iDrumsSnare.bON;
			CDTXMania.ConfigIni.bAutoPlay.BD = this.iDrumsBass.bON;
			CDTXMania.ConfigIni.bAutoPlay.HT = this.iDrumsHighTom.bON;
			CDTXMania.ConfigIni.bAutoPlay.LT = this.iDrumsLowTom.bON;
			CDTXMania.ConfigIni.bAutoPlay.FT = this.iDrumsFloorTom.bON;
			CDTXMania.ConfigIni.bAutoPlay.CY = this.iDrumsCymbalRide.bON;
            CDTXMania.ConfigIni.bAutoPlay.RD = this.iDrumsCymbalRide.bON;
            CDTXMania.ConfigIni.bAutoPlay.LP = this.iDrumsLeftPedal.bON;
            CDTXMania.ConfigIni.bAutoPlay.LBD = this.iDrumsLeftBassDrum.bON;
			CDTXMania.ConfigIni.n譜面スクロール速度.Drums = this.iDrumsScrollSpeed.n現在の値;
            //CDTXMania.ConfigIni.ドラムコンボ文字の表示位置 = (Eドラムコンボ文字の表示位置) this.iDrumsComboPosition.n現在選択されている項目番号;
												// "Sudden" || "Sud+Hid"
			CDTXMania.ConfigIni.bSudden.Drums = ( this.iDrumsSudHid.n現在選択されている項目番号 == 1 || this.iDrumsSudHid.n現在選択されている項目番号 == 3 ) ? true : false;
												// "Hidden" || "Sud+Hid"
			CDTXMania.ConfigIni.bHidden.Drums = ( this.iDrumsSudHid.n現在選択されている項目番号 == 2 || this.iDrumsSudHid.n現在選択されている項目番号 == 3 ) ? true : false;
			if      ( this.iDrumsSudHid.n現在選択されている項目番号 == 4 ) CDTXMania.ConfigIni.eInvisible.Drums = EInvisible.SEMI;	// "S-Invisible"
			else if ( this.iDrumsSudHid.n現在選択されている項目番号 == 5 ) CDTXMania.ConfigIni.eInvisible.Drums = EInvisible.FULL;	// "F-Invisible"
			else                                                           CDTXMania.ConfigIni.eInvisible.Drums = EInvisible.OFF;
			CDTXMania.ConfigIni.bReverse.Drums = this.iDrumsReverse.bON;
			CDTXMania.ConfigIni.判定文字表示位置.Drums = (E判定文字表示位置) this.iDrumsPosition.n現在選択されている項目番号;
			CDTXMania.ConfigIni.bTight = this.iDrumsTight.bON;
			CDTXMania.ConfigIni.nInputAdjustTimeMs.Drums = this.iDrumsInputAdjustTimeMs.n現在の値;		// #23580 2011.1.3 yyagi
			CDTXMania.ConfigIni.bGraph.Drums = this.iDrumsGraph.bON;// #24074 2011.01.23 add ikanick

			CDTXMania.ConfigIni.eHHGroup = (EHHGroup) this.iSystemHHGroup.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eFTGroup = (EFTGroup) this.iSystemFTGroup.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eCYGroup = (ECYGroup) this.iSystemCYGroup.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eBDGroup = (EBDGroup) this.iSystemBDGroup.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eHitSoundPriorityHH = (E打ち分け時の再生の優先順位) this.iSystemHitSoundPriorityHH.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eHitSoundPriorityFT = (E打ち分け時の再生の優先順位) this.iSystemHitSoundPriorityFT.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eHitSoundPriorityCY = (E打ち分け時の再生の優先順位) this.iSystemHitSoundPriorityCY.n現在選択されている項目番号;
			CDTXMania.ConfigIni.bフィルイン有効 = this.iSystemFillIn.bON;
			CDTXMania.ConfigIni.b演奏音を強調する.Drums = this.iSystemSoundMonitorDrums.bON;
			CDTXMania.ConfigIni.bドラム打音を発声する = this.iSystemHitSound.bON;
			CDTXMania.ConfigIni.n表示可能な最小コンボ数.Drums = this.iSystemMinComboDrums.n現在の値;
			CDTXMania.ConfigIni.bシンバルフリー = this.iSystemCymbalFree.bON;

			//CDTXMania.ConfigIni.eDark = (Eダークモード)this.iCommonDark.n現在選択されている項目番号;
			CDTXMania.ConfigIni.nRisky = this.iSystemRisky.n現在の値;						// #23559 2911.7.27 yyagi
            CDTXMania.ConfigIni.nJudgeLine.Drums = this.iDrumsJudgeLinePos.n現在の値;
            CDTXMania.ConfigIni.nJudgeLinePosOffset.Drums = this.iDrumsJudgeLineOffset.n現在の値;
            //CDTXMania.ConfigIni.eドラムレーン表示位置 = (Eドラムレーン表示位置) this.iDrumsLanePosition.n現在選択されている項目番号;
            CDTXMania.ConfigIni.eRDPosition = (ERDPosition)this.iDrumsRDPosition.n現在選択されている項目番号;
            CDTXMania.ConfigIni.eLaneType = ( Eタイプ )this.iDrumsLaneType.n現在選択されている項目番号;

            CDTXMania.ConfigIni.nLaneDispType.Drums = this.iDrumsLaneDispType.n現在選択されている項目番号;
            CDTXMania.ConfigIni.bJudgeLineDisp.Drums = this.iDrumsJudgeLineDisp.bON;
            CDTXMania.ConfigIni.bLaneFlush.Drums = this.iDrumsLaneFlush.bON;
            CDTXMania.ConfigIni.eJUST.Drums = (EJust)this.iDrumsJust.n現在選択されている項目番号;
            CDTXMania.ConfigIni.nShutterInSide.Drums = this.iDrumsShutterInPos.n現在の値;
            CDTXMania.ConfigIni.nShutterOutSide.Drums = this.iDrumsShutterOutPos.n現在の値;
            CDTXMania.ConfigIni.strShutterImageName.Drums = this.iDrumsShutterImage.list項目値[ this.iDrumsShutterImage.n現在選択されている項目番号 ];
            CDTXMania.ConfigIni.bAssignToLBD.Drums = this.iDrumsAssignToLBD.bON;
            CDTXMania.ConfigIni.eRandom.Drums = (Eランダムモード)this.iDrumsRandomPad.n現在選択されている項目番号;
            CDTXMania.ConfigIni.eRandomPedal.Drums = (Eランダムモード)this.iDrumsRandomPedal.n現在選択されている項目番号;
            CDTXMania.ConfigIni.eNumOfLanes.Drums = (Eタイプ)this.iDrumsNumOfLanes.n現在選択されている項目番号;

            CDTXMania.ConfigIni.nPedalJudgeRangeDelta = this.iDrumsPedalJudgeRangeDelta.n現在の値;

            // 2019.9.7 kairera0467 試験的に設置。XGまたはmatixxで設定項目を制限するテスト。そのうちメソッドごとに分離するかもしれない。
            if( CDTXMania.bXGRelease )
            {

            }
            else
            {
                CDTXMania.ConfigIni.bフレームを表示する = this.iDrumsMatixxFrameDisp.bON;
            }
		}
		private void tConfigIniへ記録する_Guitar()
		{
			//CDTXMania.ConfigIni.bAutoPlay.Guitar = this.iGuitarAutoPlay.bON;
			CDTXMania.ConfigIni.bAutoPlay.GtR = this.iGuitarR.bON;
			CDTXMania.ConfigIni.bAutoPlay.GtG = this.iGuitarG.bON;
			CDTXMania.ConfigIni.bAutoPlay.GtB = this.iGuitarB.bON;
			CDTXMania.ConfigIni.bAutoPlay.GtY = this.iGuitarY.bON;
			CDTXMania.ConfigIni.bAutoPlay.GtP = this.iGuitarP.bON;
			CDTXMania.ConfigIni.bAutoPlay.GtPick = this.iGuitarPick.bON;
			CDTXMania.ConfigIni.bAutoPlay.GtW = this.iGuitarW.bON;
			CDTXMania.ConfigIni.n譜面スクロール速度.Guitar = this.iGuitarScrollSpeed.n現在の値;
												// "Sudden" || "Sud+Hid"
			CDTXMania.ConfigIni.bSudden.Guitar = ( this.iGuitarSudHid.n現在選択されている項目番号 == 1 || this.iGuitarSudHid.n現在選択されている項目番号 == 3 ) ? true : false;
												// "Hidden" || "Sud+Hid"
			CDTXMania.ConfigIni.bHidden.Guitar = ( this.iGuitarSudHid.n現在選択されている項目番号 == 2 || this.iGuitarSudHid.n現在選択されている項目番号 == 3 ) ? true : false;
			if      ( this.iGuitarSudHid.n現在選択されている項目番号 == 4 ) CDTXMania.ConfigIni.eInvisible.Guitar = EInvisible.SEMI;	// "S-Invisible"
			else if ( this.iGuitarSudHid.n現在選択されている項目番号 == 5 ) CDTXMania.ConfigIni.eInvisible.Guitar = EInvisible.FULL;	// "F-Invisible"
			else                                                            CDTXMania.ConfigIni.eInvisible.Guitar = EInvisible.OFF;
			CDTXMania.ConfigIni.bReverse.Guitar = this.iGuitarReverse.bON;
			CDTXMania.ConfigIni.判定文字表示位置.Guitar = (E判定文字表示位置) this.iGuitarPosition.n現在選択されている項目番号;
			CDTXMania.ConfigIni.eRandom.Guitar = (Eランダムモード) this.iGuitarRandom.n現在選択されている項目番号;
			CDTXMania.ConfigIni.bLight.Guitar = this.iGuitarLight.bON;
			CDTXMania.ConfigIni.bLeft.Guitar = this.iGuitarLeft.bON;
			CDTXMania.ConfigIni.nInputAdjustTimeMs.Guitar = this.iGuitarInputAdjustTimeMs.n現在の値;	// #23580 2011.1.3 yyagi

			CDTXMania.ConfigIni.n表示可能な最小コンボ数.Guitar = this.iSystemMinComboGuitar.n現在の値;
			CDTXMania.ConfigIni.b演奏音を強調する.Guitar = this.iSystemSoundMonitorGuitar.bON;
			CDTXMania.ConfigIni.e判定位置.Guitar = (E判定位置) this.iSystemJudgePosGuitar.n現在選択されている項目番号;					// #33891 2014.6.26 yyagi
			//CDTXMania.ConfigIni.e判定表示優先度.Guitar = (E判定表示優先度) this.iGuitarJudgeDispPriority.n現在選択されている項目番号;
            CDTXMania.ConfigIni.bLaneFlush.Guitar = this.iGuitarLaneFlush.bON;
            CDTXMania.ConfigIni.nLaneDispType.Guitar = this.iGuitarLaneDispType.n現在選択されている項目番号;
            CDTXMania.ConfigIni.bJudgeLineDisp.Guitar = this.iGuitarJudgeLineDisp.bON;
            CDTXMania.ConfigIni.bGraph.Guitar = this.iGuitarGraph.bON;
            CDTXMania.ConfigIni.eJUST.Guitar = (EJust)this.iGuitarJust.n現在選択されている項目番号;
            CDTXMania.ConfigIni.strShutterImageName.Guitar = this.iGuitarShutterImage.list項目値[ this.iGuitarShutterImage.n現在選択されている項目番号 ];
		}
		public virtual void t説明文パネルに現在選択されているメニューの説明を描画する( int n現在のメニュー番号 ){
        }
        public virtual void t説明文パネルに現在選択されている項目の説明を描画する() {
        }
		//-----------------
		#endregion
	}
}
