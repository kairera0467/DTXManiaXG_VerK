﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using SharpDX;
using SharpDX.Direct3D9;
using FDK;
using SampleFramework;

using Point = System.Drawing.Point;
namespace DTXMania
{
	internal class CDTXMania : Game
	{
		// プロパティ
		#region [ properties ]
		public static readonly string VERSION = "Ver4.12(231107)";
		public static readonly string SLIMDXDLL = "c_net20x86_Jun2010";
		public static readonly string D3DXDLL = "d3dx9_43.dll";		// June 2010
        //public static readonly string D3DXDLL = "d3dx9_42.dll";	// February 2010
        //public static readonly string D3DXDLL = "d3dx9_41.dll";	// March 2009
        public static bool bXGRelease = true;

		public static CDTXMania app
		{
			get;
			private set;
		}
		public static C文字コンソール act文字コンソール
		{ 
			get;
			private set;
		}
		public static bool bコンパクトモード
		{
			get;
			private set;
		}
		public static CConfigIni ConfigIni
		{
			get; 
			private set;
		}
		public static CDTX DTX
		{
			get
			{
				return dtx;
			}
			set
			{
				if( ( dtx != null ) && ( app != null ) )
				{
					dtx.On非活性化();
					app.listトップレベルActivities.Remove( dtx );
				}
				dtx = value;
				if( ( dtx != null ) && ( app != null ) )
				{
					app.listトップレベルActivities.Add( dtx );
				}
			}
		}
		public static CFPS FPS
		{ 
			get; 
			private set;
		}
		public static CInput管理 Input管理 
		{
			get;
			private set;
		}
		#region [ 入力範囲ms ]
		public static int nPerfect範囲ms
		{
			get
			{
				if( stage選曲.r確定された曲 != null )
				{
					C曲リストノード c曲リストノード = stage選曲.r確定された曲.r親ノード;
					if( ( ( c曲リストノード != null ) && ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.BOX ) ) && ( c曲リストノード.nPerfect範囲ms >= 0 ) )
					{
						return c曲リストノード.nPerfect範囲ms;
					}
				}
				return ConfigIni.nヒット範囲ms.Perfect;
			}
		}
		public static int nGreat範囲ms
		{
			get
			{
				if( stage選曲.r確定された曲 != null )
				{
					C曲リストノード c曲リストノード = stage選曲.r確定された曲.r親ノード;
					if( ( ( c曲リストノード != null ) && ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.BOX ) ) && ( c曲リストノード.nGreat範囲ms >= 0 ) )
					{
						return c曲リストノード.nGreat範囲ms;
					}
				}
				return ConfigIni.nヒット範囲ms.Great;
			}
		}
		public static int nGood範囲ms
		{
			get
			{
				if( stage選曲.r確定された曲 != null )
				{
					C曲リストノード c曲リストノード = stage選曲.r確定された曲.r親ノード;
					if( ( ( c曲リストノード != null ) && ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.BOX ) ) && ( c曲リストノード.nGood範囲ms >= 0 ) )
					{
						return c曲リストノード.nGood範囲ms;
					}
				}
				return ConfigIni.nヒット範囲ms.Good;
			}
		}
		public static int nPoor範囲ms
		{
			get
			{
				if( stage選曲.r確定された曲 != null )
				{
					C曲リストノード c曲リストノード = stage選曲.r確定された曲.r親ノード;
					if( ( ( c曲リストノード != null ) && ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.BOX ) ) && ( c曲リストノード.nPoor範囲ms >= 0 ) )
					{
						return c曲リストノード.nPoor範囲ms;
					}
				}
				return ConfigIni.nヒット範囲ms.Poor;
			}
		}
		#endregion
		public static CPad Pad 
		{
			get;
			private set;
		}
		public static Random Random
		{
			get;
			private set;
		}
		public static CSkin Skin
		{
			get; 
			private set;
		}
		public static CSongs管理 Songs管理 
		{
			get;
			set;	// 2012.1.26 yyagi private解除 CStage起動でのdesirialize読み込みのため
		}
		public static CEnumSongs EnumSongs
		{
			get;
			private set;
		}
		public static CActEnumSongs actEnumSongs
		{
			get;
			private set;
		}
		public static CActFlushGPU actFlushGPU
		{
			get;
			private set;
		}

		public static CSound管理 Sound管理
		{
			get;
			private set;
		}
		public static CStage起動 stage起動 
		{
			get; 
			private set;
		}
		public static CStageタイトル stageタイトル
		{
			get;
			private set;
		}
//		public static CStageオプション stageオプション
//		{ 
//			get;
//			private set;
//		}
		public static CStageコンフィグ stageコンフィグ 
		{ 
			get; 
			private set;
		}
		public static CStage選曲 stage選曲
		{
			get;
			private set;
		}
		public static CStage選曲XG stage選曲XG
		{
			get;
			private set;
		}
        public static CStage選曲GITADORA stage選曲GITADORA
		{
			get;
			private set;
		}
		public static CStage曲読み込み stage曲読み込み
		{
			get;
			private set;
		}
		public static CStage演奏ギター画面 stage演奏ギター画面
		{
			get;
			private set;
		}
		public static CStage演奏ドラム画面 stage演奏ドラム画面
		{
			get;
			private set;
		}
		public static CStage演奏ギター画面GD stage演奏ギター画面GITADORA
		{
			get;
			private set;
		}
		public static CStage演奏ドラム画面GD stage演奏ドラム画面GITADORA
		{
			get;
			private set;
		}
		public static CStage結果 stage結果
		{
			get;
			private set;
		}
		public static CStageChangeSkin stageChangeSkin
		{
			get;
			private set;
		}
		public static CStage終了 stage終了
		{
			get;
			private set;
		}
		public static CStage r現在のステージ = null;
		public static CStage r直前のステージ = null;
		public static string strEXEのあるフォルダ 
		{
			get;
			private set;
		}
		public static string strコンパクトモードファイル
		{ 
			get; 
			private set;
		}
		public static CTimer Timer
		{
			get;
			private set;
		}
		public static Format TextureFormat = Format.A8R8G8B8;
		internal static IPluginActivity act現在入力を占有中のプラグイン = null;
		public bool bApplicationActive
		{
			get; 
			private set;
		}
		public bool b次のタイミングで垂直帰線同期切り替えを行う
		{
			get; 
			set;
		}
		public bool b次のタイミングで全画面_ウィンドウ切り替えを行う
		{
			get;
			set;
		}
		public Device Device
		{
			get { return base.GraphicsDeviceManager.Direct3D9.Device; }
		}
		public CPluginHost PluginHost
		{
			get;
			private set;
		}
		public List<STPlugin> listプラグイン = new List<STPlugin>();
		public struct STPlugin
		{
			public IPluginActivity plugin;
			public string strプラグインフォルダ;
			public string strアセンブリ簡易名;
			public Version Version;
		}
		private static Size currentClientSize		// #23510 2010.10.27 add yyagi to keep current window size
		{
			get;
			set;
		}
		//		public static CTimer ct;
		public IntPtr WindowHandle					// 2012.10.24 yyagi; to add ASIO support
		{
			get { return base.Window.Handle; }
		}
		public static CDTXVmode DTXVmode			// #28821 2014.1.23 yyagi
		{
			get;
			set;
		}
        public static CAnimationManager AnimationManager
        {
            get;
            set;
        }
        public static STDGBVALUE< List<int> > listAutoGhostLag = new STDGBVALUE<List<int>>();
        public static STDGBVALUE< List<int> > listTargetGhsotLag = new STDGBVALUE<List<int>>();
        public static STDGBVALUE< CScoreIni.C演奏記録 > listTargetGhostScoreData = new STDGBVALUE< CScoreIni.C演奏記録 >();
		#endregion

		// コンストラクタ

		public CDTXMania()
		{
			CDTXMania.app = this;
			this.t起動処理();
		}


		// メソッド

		public void t全画面_ウィンドウモード切り替え()
		{
#if WindowedFullscreen
			if ( ConfigIni != null )
#else
			DeviceSettings settings = base.GraphicsDeviceManager.CurrentSettings.Clone();
			if ( ( ConfigIni != null ) && ( ConfigIni.bウィンドウモード != settings.Windowed ) )
#endif
			{
#if !WindowedFullscreen
				settings.Windowed = ConfigIni.bウィンドウモード;
#endif
				if ( ConfigIni.bウィンドウモード == false )	// #23510 2010.10.27 yyagi: backup current window size before going fullscreen mode
				{
					currentClientSize = this.Window.ClientSize;
					ConfigIni.nウインドウwidth = this.Window.ClientSize.Width;
					ConfigIni.nウインドウheight = this.Window.ClientSize.Height;
//					FDK.CTaskBar.ShowTaskBar( false );
				}
#if !WindowedFullscreen
				base.GraphicsDeviceManager.ChangeDevice( settings );
#endif
				if ( ConfigIni.bウィンドウモード == true )	// #23510 2010.10.27 yyagi: to resume window size from backuped value
				{
#if WindowedFullscreen
															// #30666 2013.2.2 yyagi Don't use Fullscreen mode becasue NVIDIA GeForce is
															// tend to delay drawing on Fullscreen mode. So DTXMania uses Maximized window
															// in spite of using fullscreen mode.
					app.Window.WindowState = FormWindowState.Normal;
					app.Window.FormBorderStyle = FormBorderStyle.Sizable;
					app.Window.WindowState = FormWindowState.Normal;
#endif
					base.Window.ClientSize =
						new Size( currentClientSize.Width, currentClientSize.Height );
                    app.Window.ClientSize = new Size( currentClientSize.Width, currentClientSize.Height ); //2016.02.24 kairera0467 フルスクリーン後にサイズがウィンドウモード時のサイズに戻らないので応急処置。
//					FDK.CTaskBar.ShowTaskBar( true );
				}
#if WindowedFullscreen
				else 
				{
					app.Window.WindowState = FormWindowState.Normal;
					app.Window.FormBorderStyle = FormBorderStyle.None;
					app.Window.WindowState = FormWindowState.Maximized;
                    
				}
				if ( ConfigIni.bウィンドウモード )
				{
				    if ( !this.bマウスカーソル表示中 )
				    {
				        Cursor.Show();
				        this.bマウスカーソル表示中 = true;
				    }
				}
				else if ( this.bマウスカーソル表示中 )
				{
				    Cursor.Hide();
				    this.bマウスカーソル表示中 = false;
				}
#endif
			}
		}

		#region [ #24609 リザルト画像をpngで保存する ]		// #24609 2011.3.14 yyagi; to save result screen in case BestRank or HiSkill.
		/// <summary>
		/// リザルト画像のキャプチャと保存。
		/// </summary>
		/// <param name="strFilename">保存するファイル名(フルパス)</param>
		public bool SaveResultScreen( string strFullPath )
		{
			string strSavePath = Path.GetDirectoryName( strFullPath );
			if ( !Directory.Exists( strSavePath ) )
			{
				try
				{
					Directory.CreateDirectory( strSavePath );
				}
				catch
				{
					return false;
				}
			}

			// http://www.gamedev.net/topic/594369-dx9slimdxati-incorrect-saving-surface-to-file/
			using ( Surface pSurface = CDTXMania.app.Device.GetRenderTarget( 0 ) )
			{
				Surface.ToFile( pSurface, strFullPath, ImageFileFormat.Png );
			}
			return true;
		}
		#endregion

		// Game 実装

		protected override void Initialize()
		{
//			new GCBeep();
			//sw.Start();
			//swlist1 = new List<int>( 8192 );
			//swlist2 = new List<int>( 8192 );
			//swlist3 = new List<int>( 8192 );
			//swlist4 = new List<int>( 8192 );
			//swlist5 = new List<int>( 8192 );
			if ( this.listトップレベルActivities != null )
			{
				foreach( CActivity activity in this.listトップレベルActivities )
					activity.OnManagedリソースの作成();
			}

			foreach( STPlugin st in this.listプラグイン )
			{
				Directory.SetCurrentDirectory( st.strプラグインフォルダ );
				st.plugin.OnManagedリソースの作成();
				Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
			}
#if GPUFlushAfterPresent
			FrameEnd += dtxmania_FrameEnd;
#endif
		}
#if GPUFlushAfterPresent
		void dtxmania_FrameEnd( object sender, EventArgs e )	// GraphicsDeviceManager.game_FrameEnd()後に実行される
		{														// → Present()直後にGPUをFlushする
																// → 画面のカクツキが頻発したため、ここでのFlushは行わない
			actFlushGPU.On進行描画();		// Flush GPU
		}
#endif
		protected override void LoadContent()
		{
			if ( ConfigIni.bウィンドウモード )
			{
				if( !this.bマウスカーソル表示中 )
				{
					Cursor.Show();
					this.bマウスカーソル表示中 = true;
				}
			}
			else if( this.bマウスカーソル表示中 )
			{
				Cursor.Hide();
				this.bマウスカーソル表示中 = false;
			}
			this.Device.SetTransform(TransformState.View, Matrix.LookAtLH(new Vector3(0f, 0f, (float)(-SampleFramework.GameWindowSize.Height / 2 * Math.Sqrt(3.0))), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f)));
			this.Device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(C変換.DegreeToRadian((float)60f), ((float)this.Device.Viewport.Width) / ((float)this.Device.Viewport.Height), -100f, 100f));
			this.Device.SetRenderState(RenderState.Lighting, false);
			this.Device.SetRenderState( RenderState.ZEnable, false );
			this.Device.SetRenderState( RenderState.AntialiasedLineEnable, false );
			this.Device.SetRenderState( RenderState.AlphaTestEnable, true );
			this.Device.SetRenderState( RenderState.AlphaRef, 10 );

			this.Device.SetRenderState( RenderState.MultisampleAntialias, true );
			this.Device.SetSamplerState( 0, SamplerState.MinFilter, TextureFilter.Linear );
			this.Device.SetSamplerState( 0, SamplerState.MagFilter, TextureFilter.Linear );

			this.Device.SetRenderState<Compare>( RenderState.AlphaFunc, Compare.Greater );
			this.Device.SetRenderState( RenderState.AlphaBlendEnable, true );
			this.Device.SetRenderState<Blend>( RenderState.SourceBlend, Blend.SourceAlpha );
			this.Device.SetRenderState<Blend>( RenderState.DestinationBlend, Blend.InverseSourceAlpha );
			this.Device.SetTextureStageState( 0, TextureStage.AlphaOperation, TextureOperation.Modulate );
			this.Device.SetTextureStageState( 0, TextureStage.AlphaArg1, 2 );
			this.Device.SetTextureStageState( 0, TextureStage.AlphaArg2, 1 );

			if( this.listトップレベルActivities != null )
			{
				foreach( CActivity activity in this.listトップレベルActivities )
					activity.OnUnmanagedリソースの作成();
			}

			foreach( STPlugin st in this.listプラグイン )
			{
				Directory.SetCurrentDirectory( st.strプラグインフォルダ );
				st.plugin.OnUnmanagedリソースの作成();
				Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
			}
		}
		protected override void UnloadContent()
		{
			if( this.listトップレベルActivities != null )
			{
				foreach( CActivity activity in this.listトップレベルActivities )
					activity.OnUnmanagedリソースの解放();
			}

			foreach( STPlugin st in this.listプラグイン )
			{
				Directory.SetCurrentDirectory( st.strプラグインフォルダ );
				st.plugin.OnUnmanagedリソースの解放();
				Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
			}
		}
		protected override void OnExiting( EventArgs e )
		{
			CPowerManagement.tEnableMonitorSuspend();		// スリープ抑止状態を解除
			this.t終了処理();
			base.OnExiting( e );
		}
		protected override void Update( GameTime gameTime )
		{
			if( !this.b起動完了済み )
				return;
		}
		protected override void Draw( GameTime gameTime )
		{
			if( !this.b起動完了済み )
				return;

			Sound管理.t再生中の処理をする();

			if( Timer != null )
				Timer.t更新();
            if (CSound管理.rc演奏用タイマ != null)
                CSound管理.rc演奏用タイマ.t更新();

			if( Input管理 != null )
				Input管理.tポーリング( this.bApplicationActive, CDTXMania.ConfigIni.bバッファ入力を行う );

			if( FPS != null )
				FPS.tカウンタ更新();

			//if( Pad != null )					ポーリング時にクリアしたらダメ！曲の開始時に1回だけクリアする。(2010.9.11)
			//	Pad.st検知したデバイス.Clear();

			if( this.Device == null )
				return;

			if ( this.bApplicationActive )	// DTXMania本体起動中の本体/モニタの省電力モード移行を抑止
				CPowerManagement.tDisableMonitorSuspend();

            if( AnimationManager == null )
                AnimationManager = new CAnimationManager();

			// #xxxxx 2013.4.8 yyagi; sleepの挿入位置を、EndScnene～Present間から、BeginScene前に移動。描画遅延を小さくするため。
			#region [ スリープ ]
			if ( ConfigIni.nフレーム毎スリープms >= 0 )			// #xxxxx 2011.11.27 yyagi
			{
				Thread.Sleep( ConfigIni.nフレーム毎スリープms );
			}
			#endregion

			#region [ DTXCreatorからの指示 ]
			if ( this.Window.IsReceivedMessage )	// ウインドウメッセージで、
			{
				string strMes = this.Window.strMessage;
				this.Window.IsReceivedMessage = false;

				if ( strMes != null )
				{
					DTXVmode.ParseArguments( strMes );

					if ( DTXVmode.Enabled )
					{
						bコンパクトモード = true;
						strコンパクトモードファイル = DTXVmode.filename;
						if ( DTXVmode.Command == CDTXVmode.ECommand.Preview )
						{
							// preview soundの再生
							string strPreviewFilename = DTXVmode.previewFilename;
//Trace.TraceInformation( "Preview Filename=" + DTXVmode.previewFilename );
							try
							{
								if ( this.previewSound != null )
								{
									this.previewSound.tサウンドを停止する();
									this.previewSound.Dispose();
									this.previewSound = null;
								}
								this.previewSound = CDTXMania.Sound管理.tサウンドを生成する( strPreviewFilename );
								this.previewSound.n音量 = DTXVmode.previewVolume;
								this.previewSound.n位置 = DTXVmode.previewPan;
								this.previewSound.t再生を開始する();
								Trace.TraceInformation( "DTXCからの指示で、サウンドを生成しました。({0})", strPreviewFilename );
							}
							catch
							{
								Trace.TraceError( "DTXCからの指示での、サウンドの生成に失敗しました。({0})", strPreviewFilename );
								if ( this.previewSound != null )
								{
									this.previewSound.Dispose();
								}
								this.previewSound = null;
							}
						}
					}
				}
			}
			#endregion

			this.Device.BeginScene();
			this.Device.Clear( ClearFlags.ZBuffer | ClearFlags.Target, SharpDX.Color.Black, 1f, 0 );

            if( AnimationManager != null )
                AnimationManager.t進行();

			if( r現在のステージ != null )
			{
				this.n進行描画の戻り値 = ( r現在のステージ != null ) ? r現在のステージ.On進行描画() : 0;

				#region [ プラグインの進行描画 ]
				//---------------------
				foreach( STPlugin sp in this.listプラグイン )
				{
					Directory.SetCurrentDirectory( sp.strプラグインフォルダ );

					if( CDTXMania.act現在入力を占有中のプラグイン == null || CDTXMania.act現在入力を占有中のプラグイン == sp.plugin )
						sp.plugin.On進行描画( CDTXMania.Pad, CDTXMania.Input管理.Keyboard );
					else
						sp.plugin.On進行描画( null, null );

					Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
				}
				//---------------------
				#endregion


				CScoreIni scoreIni = null;

				if ( Control.IsKeyLocked( Keys.CapsLock ) )				// #30925 2013.3.11 yyagi; capslock=ON時は、EnumSongsしないようにして、起動負荷とASIOの音切れの関係を確認する
				{														// → songs.db等の書き込み時だと音切れするっぽい
					actEnumSongs.On非活性化();
					EnumSongs.SongListEnumCompletelyDone();
                    if( CDTXMania.bXGRelease ) {
					    CDTXMania.stage選曲XG.bIsEnumeratingSongs = false;
                    } else {
					    CDTXMania.stage選曲GITADORA.bIsEnumeratingSongs = false;
                    }

				}
				#region [ 曲検索スレッドの起動/終了 ]					// ここに"Enumerating Songs..."表示を集約
				if ( !CDTXMania.bコンパクトモード )
				{
					actEnumSongs.On進行描画();							// "Enumerating Songs..."アイコンの描画
				}
				switch ( r現在のステージ.eステージID )
				{
					case CStage.Eステージ.タイトル:
					case CStage.Eステージ.コンフィグ:
					case CStage.Eステージ.選曲:
					case CStage.Eステージ.曲読み込み:
						if ( EnumSongs != null )
						{
							#region [ (特定条件時) 曲検索スレッドの起動・開始 ]
							if ( r現在のステージ.eステージID == CStage.Eステージ.タイトル &&
								 r直前のステージ.eステージID == CStage.Eステージ.起動 &&
								 this.n進行描画の戻り値 == (int) CStageタイトル.E戻り値.継続 &&
								 !EnumSongs.IsSongListEnumStarted )
							{
								actEnumSongs.On活性化();
                                if( CDTXMania.bXGRelease ) CDTXMania.stage選曲XG.bIsEnumeratingSongs = true;
								else CDTXMania.stage選曲GITADORA.bIsEnumeratingSongs = true;
								EnumSongs.Init( CDTXMania.Songs管理.listSongsDB, CDTXMania.Songs管理.nSongsDBから取得できたスコア数 );	// songs.db情報と、取得した曲数を、新インスタンスにも与える
								EnumSongs.StartEnumFromDisk();		// 曲検索スレッドの起動・開始
								if ( CDTXMania.Songs管理.nSongsDBから取得できたスコア数 == 0 )	// もし初回起動なら、検索スレッドのプライオリティをLowestでなくNormalにする
								{
									EnumSongs.ChangeEnumeratePriority( ThreadPriority.Normal );
								}
							}
							#endregion
							
							#region [ 曲検索の中断と再開 ]
							if ( r現在のステージ.eステージID == CStage.Eステージ.選曲 && !EnumSongs.IsSongListEnumCompletelyDone )
							{
								switch ( this.n進行描画の戻り値 )
								{
									case 0:		// 何もない
										EnumSongs.Resume();						// #27060 2012.2.6 yyagi 中止していたバックグランド曲検索を再開
										EnumSongs.IsSlowdown = false;
										actEnumSongs.On活性化();
										break;

									case 2:		// 曲決定
										EnumSongs.Suspend();						// #27060 バックグラウンドの曲検索を一時停止
										actEnumSongs.On非活性化();
										break;
								}
							}
							#endregion

							#region [ 曲探索中断待ち待機 ]
							if ( r現在のステージ.eステージID == CStage.Eステージ.曲読み込み && !EnumSongs.IsSongListEnumCompletelyDone &&
								EnumSongs.thDTXFileEnumerate != null )							// #28700 2012.6.12 yyagi; at Compact mode, enumerating thread does not exist.
							{
								EnumSongs.WaitUntilSuspended();									// 念のため、曲検索が一時中断されるまで待機
							}
							#endregion

							#region [ 曲検索が完了したら、実際の曲リストに反映する ]
							// CStage選曲.On活性化() に回した方がいいかな？
							if ( EnumSongs.IsSongListEnumerated )
							{
								actEnumSongs.On非活性化();
                                if( CDTXMania.bXGRelease ) CDTXMania.stage選曲XG.bIsEnumeratingSongs = false;
								else CDTXMania.stage選曲GITADORA.bIsEnumeratingSongs = false;

								bool bRemakeSongTitleBar = ( r現在のステージ.eステージID == CStage.Eステージ.選曲 ) ? true : false;
                                if( CDTXMania.bXGRelease ) CDTXMania.stage選曲XG.Refresh( EnumSongs.Songs管理, bRemakeSongTitleBar );
								else CDTXMania.stage選曲GITADORA.Refresh( EnumSongs.Songs管理, bRemakeSongTitleBar );
								EnumSongs.SongListEnumCompletelyDone();
							}
							#endregion
						}
						break;
				}
				#endregion

				switch ( r現在のステージ.eステージID )
				{
					case CStage.Eステージ.何もしない:
						break;

					case CStage.Eステージ.起動:
						#region [ *** ]
						//-----------------------------
						if( this.n進行描画の戻り値 != 0 )
						{
							if( !bコンパクトモード )
							{
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ タイトル" );
								stageタイトル.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stageタイトル;
							}
							else
							{
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 曲読み込み" );
								stage曲読み込み.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stage曲読み込み;

							}
							foreach( STPlugin pg in this.listプラグイン )
							{
								Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
								pg.plugin.Onステージ変更();
								Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
							}

							this.tガベージコレクションを実行する();
						}
						//-----------------------------
						#endregion
						break;

					case CStage.Eステージ.タイトル:
						#region [ *** ]
						//-----------------------------
						switch( this.n進行描画の戻り値 )
						{
							case (int)CStageタイトル.E戻り値.GAMESTART:
								#region [ 選曲処理へ ]
								//-----------------------------
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 選曲" );
                                //stage選曲.On活性化(); //2017.10.01 kairera0467 完全移行まで
                                if( bXGRelease )
                                {
								    stage選曲XG.On活性化();
								    r直前のステージ = r現在のステージ;
								    r現在のステージ = stage選曲XG;
                                }
                                else
                                {
								    stage選曲GITADORA.On活性化();
								    r直前のステージ = r現在のステージ;
								    r現在のステージ = stage選曲GITADORA;
                                }
								//-----------------------------
								#endregion
								break;

							#region [ OPTION: 廃止済 ]
//							case 2:									// #24525 OPTIONとCONFIGの統合に伴い、OPTIONは廃止
//								#region [ *** ]
//								//-----------------------------
//								r現在のステージ.On非活性化();
//								Trace.TraceInformation( "----------------------" );
//								Trace.TraceInformation( "■ オプション" );
//								stageオプション.On活性化();
//								r直前のステージ = r現在のステージ;
//								r現在のステージ = stageオプション;
//								//-----------------------------
//								#endregion
							//								break;
							#endregion

							case (int)CStageタイトル.E戻り値.CONFIG:
								#region [ *** ]
								//-----------------------------
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ コンフィグ" );
								stageコンフィグ.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stageコンフィグ;
								//-----------------------------
								#endregion
								break;

							case (int)CStageタイトル.E戻り値.EXIT:
								#region [ *** ]
								//-----------------------------
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 終了" );
								stage終了.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stage終了;
								//-----------------------------
								#endregion
								break;
						}

						foreach( STPlugin pg in this.listプラグイン )
						{
							Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
							pg.plugin.Onステージ変更();
							Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
						}

						//this.tガベージコレクションを実行する();		// #31980 2013.9.3 yyagi タイトル画面でだけ、毎フレームGCを実行して重くなっていた問題の修正
						//-----------------------------
						#endregion
						break;

//					case CStage.Eステージ.オプション:
						#region [ *** ]
//						//-----------------------------
//						if( this.n進行描画の戻り値 != 0 )
//						{
//							switch( r直前のステージ.eステージID )
//							{
//								case CStage.Eステージ.タイトル:
//									#region [ *** ]
//									//-----------------------------
//									r現在のステージ.On非活性化();
//									Trace.TraceInformation( "----------------------" );
//									Trace.TraceInformation( "■ タイトル" );
//									stageタイトル.On活性化();
//									r直前のステージ = r現在のステージ;
//									r現在のステージ = stageタイトル;
//						
//									foreach( STPlugin pg in this.listプラグイン )
//									{
//										Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
//										pg.plugin.Onステージ変更();
//										Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
//									}
//						
//									this.tガベージコレクションを実行する();
//									break;
//								//-----------------------------
//									#endregion
//
//								case CStage.Eステージ.選曲:
//									#region [ *** ]
//									//-----------------------------
//									r現在のステージ.On非活性化();
//									Trace.TraceInformation( "----------------------" );
//									Trace.TraceInformation( "■ 選曲" );
//									stage選曲.On活性化();
//									r直前のステージ = r現在のステージ;
//									r現在のステージ = stage選曲;
//
//									foreach( STPlugin pg in this.listプラグイン )
//									{
//										Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
//										pg.plugin.Onステージ変更();
//										Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
//									}
//
//									this.tガベージコレクションを実行する();
//									break;
//								//-----------------------------
//									#endregion
//							}
//						}
//						//-----------------------------
						#endregion
//						break;

					case CStage.Eステージ.コンフィグ:
						#region [ *** ]
						//-----------------------------
						if( this.n進行描画の戻り値 != 0 )
						{
							switch( r直前のステージ.eステージID )
							{
								case CStage.Eステージ.タイトル:
									#region [ *** ]
									//-----------------------------
									r現在のステージ.On非活性化();
									Trace.TraceInformation( "----------------------" );
									Trace.TraceInformation( "■ タイトル" );
									stageタイトル.On活性化();
									r直前のステージ = r現在のステージ;
									r現在のステージ = stageタイトル;

									foreach( STPlugin pg in this.listプラグイン )
									{
										Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
										pg.plugin.Onステージ変更();
										Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
									}

									this.tガベージコレクションを実行する();
									break;
								//-----------------------------
									#endregion

								case CStage.Eステージ.選曲:
									#region [ *** ]
									//-----------------------------
									r現在のステージ.On非活性化();
									Trace.TraceInformation( "----------------------" );
									Trace.TraceInformation( "■ 選曲" );
                                    if( bXGRelease )
                                    {
									    stage選曲XG.On活性化();
									    r直前のステージ = r現在のステージ;
									    r現在のステージ = stage選曲XG;
                                    }
                                    else
                                    {
									    stage選曲GITADORA.On活性化();
									    r直前のステージ = r現在のステージ;
									    r現在のステージ = stage選曲GITADORA;
                                    }
                                    
									foreach( STPlugin pg in this.listプラグイン )
									{
										Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
										pg.plugin.Onステージ変更();
										Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
									}

									this.tガベージコレクションを実行する();
									break;
								//-----------------------------
									#endregion
							}
						}
						//-----------------------------
						#endregion
						break;

					case CStage.Eステージ.選曲:
						#region [ *** ]
						//-----------------------------
						switch( this.n進行描画の戻り値 )
						{
							case (int) CStage選曲.E戻り値.タイトルに戻る:
								#region [ *** ]
								//-----------------------------
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ タイトル" );
								stageタイトル.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stageタイトル;

								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.Onステージ変更();
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}

								this.tガベージコレクションを実行する();
								break;
							//-----------------------------
								#endregion

							case (int) CStage選曲.E戻り値.選曲した:
								#region [ *** ]
								//-----------------------------
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 曲読み込み" );
								stage曲読み込み.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stage曲読み込み;

								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.Onステージ変更();
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}

								this.tガベージコレクションを実行する();
								break;
							//-----------------------------
								#endregion

//							case (int) CStage選曲.E戻り値.オプション呼び出し:
								#region [ *** ]
//								//-----------------------------
//								r現在のステージ.On非活性化();
//								Trace.TraceInformation( "----------------------" );
//								Trace.TraceInformation( "■ オプション" );
//								stageオプション.On活性化();
//								r直前のステージ = r現在のステージ;
//								r現在のステージ = stageオプション;
//
//								foreach( STPlugin pg in this.listプラグイン )
//								{
//									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
//									pg.plugin.Onステージ変更();
//									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
//								}
//
//								this.tガベージコレクションを実行する();
//								break;
//							//-----------------------------
								#endregion

							case (int) CStage選曲.E戻り値.コンフィグ呼び出し:
								#region [ *** ]
								//-----------------------------
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ コンフィグ" );
								stageコンフィグ.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stageコンフィグ;

								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.Onステージ変更();
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}

								this.tガベージコレクションを実行する();
								break;
							//-----------------------------
								#endregion

							case (int) CStage選曲.E戻り値.スキン変更:

								#region [ *** ]
								//-----------------------------
								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ スキン切り替え" );
								stageChangeSkin.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stageChangeSkin;
								break;
							//-----------------------------
								#endregion
						}
						//-----------------------------
						#endregion
						break;

					case CStage.Eステージ.曲読み込み:
						#region [ *** ]
						//-----------------------------
						DTXVmode.Refreshed = false;		// 曲のリロード中に発生した再リロードは、無視する。
						if( this.n進行描画の戻り値 != 0 )
						{
							CDTXMania.Pad.st検知したデバイス.Clear();	// 入力デバイスフラグクリア(2010.9.11)
							r現在のステージ.On非活性化();
							#region [ ESC押下時は、曲の読み込みを中止して選曲画面に戻る ]
							if ( this.n進行描画の戻り値 == (int) E曲読込画面の戻り値.読込中止 )
							{
								//DTX.t全チップの再生停止();
								if( DTX != null ) DTX.On非活性化();
								Trace.TraceInformation( "曲の読み込みを中止しました。" );
								this.tガベージコレクションを実行する();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 選曲" );
                                if( bXGRelease )
                                {
								    stage選曲XG.On活性化();
								    r直前のステージ = r現在のステージ;
								    r現在のステージ = stage選曲XG;
                                }
                                else
                                {
								    stage選曲GITADORA.On活性化();
								    r直前のステージ = r現在のステージ;
								    r現在のステージ = stage選曲GITADORA;
                                }

								foreach ( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.Onステージ変更();
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}
								break;
							}
							#endregion

							if( !ConfigIni.bギタレボモード )
							{
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 演奏（ドラム画面）" );
#if false		// #23625 2011.1.11 Config.iniからダメージ/回復値の定数変更を行う場合はここを有効にする 087リリースに合わせ機能無効化
for (int i = 0; i < 5; i++)
{
	for (int j = 0; j < 2; j++)
	{
		stage演奏ドラム画面.fDamageGaugeDelta[i, j] = ConfigIni.fGaugeFactor[i, j];
	}
}
for (int i = 0; i < 3; i++) {
	stage演奏ドラム画面.fDamageLevelFactor[i] = ConfigIni.fDamageLevelFactor[i];
}		
#endif
								r直前のステージ = r現在のステージ;
                                if( CDTXMania.bXGRelease )
								    r現在のステージ = stage演奏ドラム画面;
                                else
                                    r現在のステージ = stage演奏ドラム画面GITADORA;
							}
							else
							{
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 演奏（ギター画面）" );
#if false		// #23625 2011.1.11 Config.iniからダメージ/回復値の定数変更を行う場合はここを有効にする 087リリースに合わせ機能無効化
for (int i = 0; i < 5; i++)
{
	for (int j = 0; j < 2; j++)
	{
		stage演奏ギター画面.fDamageGaugeDelta[i, j] = ConfigIni.fGaugeFactor[i, j];
	}
}
for (int i = 0; i < 3; i++) {
	stage演奏ギター画面.fDamageLevelFactor[i] = ConfigIni.fDamageLevelFactor[i];
}		
#endif
								r直前のステージ = r現在のステージ;
                                if( CDTXMania.bXGRelease )
								    r現在のステージ = stage演奏ギター画面;
                                else
                                    r現在のステージ = stage演奏ギター画面GITADORA;
							}

							foreach( STPlugin pg in this.listプラグイン )
							{
								Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
								pg.plugin.Onステージ変更();
								Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
							}

							this.tガベージコレクションを実行する();
						}
						//-----------------------------
						#endregion
						break;

					case CStage.Eステージ.演奏:
						#region [ *** ]
						//-----------------------------
						//long n1 = FDK.CSound管理.rc演奏用タイマ.nシステム時刻ms;
						//long n2 = FDK.CSound管理.SoundDevice.n経過時間ms;
						//long n3 = FDK.CSound管理.SoundDevice.tmシステムタイマ.nシステム時刻ms;
						//long n4 = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
						//long n5 = FDK.CSound管理.SoundDevice.n経過時間を更新したシステム時刻ms;

						//swlist1.Add( Convert.ToInt32(n1) );
						//swlist2.Add( Convert.ToInt32(n2) );
						//swlist3.Add( Convert.ToInt32( n3 ) );
						//swlist4.Add( Convert.ToInt32( n4 ) );
						//swlist5.Add( Convert.ToInt32( n5 ) );

						#region [ DTXVモード中にDTXCreatorから指示を受けた場合の処理 ]
						if ( DTXVmode.Enabled && DTXVmode.Refreshed )
						{
							DTXVmode.Refreshed = false;

							if ( DTXVmode.Command == CDTXVmode.ECommand.Stop )
							{
								if ( !ConfigIni.bギタレボモード )
								{
                                    if( CDTXMania.bXGRelease )
									    CDTXMania.stage演奏ドラム画面.t停止();
                                    else
                                        CDTXMania.stage演奏ドラム画面GITADORA.t停止();
								}
								else
								{
                                    if( CDTXMania.bXGRelease )
									    CDTXMania.stage演奏ギター画面.t停止();
                                    else
                                        CDTXMania.stage演奏ギター画面GITADORA.t停止();
								}
								if ( previewSound != null )
								{
									this.previewSound.tサウンドを停止する();
									this.previewSound.Dispose();
									this.previewSound = null;
								}
								//{
								//    int lastd = 0;
								//    int f = 0;
								//    for ( int i = 0; i < swlist1.Count; i++ )
								//    {
								//        int d1 = swlist1[ i ];
								//        int d2 = swlist2[ i ];
								//        int d3 = swlist3[ i ];
								//        int d4 = swlist4[ i ];
								//        int d5 = swlist5[ i ];

								//        int dif = d1 - lastd;
								//        string s = "";
								//        if ( 16 <= dif && dif <= 17 )
								//        {
								//        }
								//        else
								//        {
								//            s = "★";
								//        }
								//        Trace.TraceInformation( "frame {0:D4}: {1:D3} ( {2:D3}, {3:D3} - {7:D3}, {4:D3} ) {5}, n現在時刻={6}", f, dif, d1, d2, d3, s, d4, d5 );
								//        lastd = d1;
								//        f++;
								//    }
								//    swlist1.Clear();
								//    swlist2.Clear();
								//    swlist3.Clear();
								//    swlist4.Clear();
								//    swlist5.Clear();

								//}
							}
							else if ( DTXVmode.Command == CDTXVmode.ECommand.Play )
							{
								if ( DTXVmode.NeedReload )
								{
                                    if( CDTXMania.bXGRelease )
                                    {
									    if ( !ConfigIni.bギタレボモード )
									    {
										    CDTXMania.stage演奏ドラム画面.t再読込();
									    }
									    else
									    {
										    CDTXMania.stage演奏ギター画面.t再読込();
									    }
                                    }
                                    else
                                    {
									    if ( !ConfigIni.bギタレボモード )
									    {
										    CDTXMania.stage演奏ドラム画面GITADORA.t再読込();
									    }
									    else
									    {
										    CDTXMania.stage演奏ギター画面GITADORA.t再読込();
									    }
                                    }
									CDTXMania.ConfigIni.bDrums有効 = !DTXVmode.GRmode;
									CDTXMania.ConfigIni.bGuitar有効 = true;
									CDTXMania.ConfigIni.bTimeStretch = DTXVmode.TimeStretch;
									CSound管理.bIsTimeStretch = DTXVmode.TimeStretch;
									if ( CDTXMania.ConfigIni.b垂直帰線待ちを行う != DTXVmode.VSyncWait )
									{
										CDTXMania.ConfigIni.b垂直帰線待ちを行う = DTXVmode.VSyncWait;
										CDTXMania.app.b次のタイミングで垂直帰線同期切り替えを行う = true;
									}
								}
								else
								{
                                    if( CDTXMania.bXGRelease )
                                    {
									    if ( !ConfigIni.bギタレボモード )
									    {
										    CDTXMania.stage演奏ドラム画面.t演奏位置の変更( CDTXMania.DTXVmode.nStartBar );
									    }
									    else
									    {
										    CDTXMania.stage演奏ギター画面.t演奏位置の変更( CDTXMania.DTXVmode.nStartBar );
									    }
                                    }
                                    else
                                    {
									    if ( !ConfigIni.bギタレボモード )
									    {
										    CDTXMania.stage演奏ドラム画面GITADORA.t演奏位置の変更( CDTXMania.DTXVmode.nStartBar );
									    }
									    else
									    {
										    CDTXMania.stage演奏ギター画面GITADORA.t演奏位置の変更( CDTXMania.DTXVmode.nStartBar );
									    }
                                    }
								}
							}
						}
						#endregion

						switch( this.n進行描画の戻り値 )
						{
							case (int) E演奏画面の戻り値.再読込_再演奏:
								#region [ DTXファイルを再読み込みして、再演奏 ]
								DTX.t全チップの再生停止();
								DTX.On非活性化();
								r現在のステージ.On非活性化();
								stage曲読み込み.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stage曲読み込み;
								this.tガベージコレクションを実行する();
								break;
								#endregion

							//case (int) E演奏画面の戻り値.再演奏:
							#region [ 再読み込み無しで、再演奏 ]
							#endregion
							//	break;

							case (int) E演奏画面の戻り値.継続:
								break;

							case (int) E演奏画面の戻り値.演奏中断:
								#region [ 演奏キャンセル ]
								//-----------------------------
								scoreIni = this.tScoreIniへBGMAdjustとHistoryとPlayCountを更新( "Play canceled" );
								if ( CDTXMania.ConfigIni.bIsSwappedGuitarBass )		// #35417 2015.8.18 yyagi Gt/Bsを入れ替えていたなら、演奏設定を元に戻す
								{
									//CDTXMania.DTX.SwapGuitarBassInfos();						// 譜面情報も元に戻す (現在は再演奏機能なしのため、元に戻す必要はない)
								}

								//int lastd = 0;
								//int f = 0;
								//for (int i = 0; i < swlist1.Count; i++)
								//{
								//    int d1 = swlist1[ i ];
								//    int d2 = swlist2[ i ];
								//    int d3 = swlist3[ i ];
								//    int d4 = swlist4[ i ];

								//    int dif = d1 - lastd;
								//    string s = "";
								//    if ( 16 <= dif && dif <= 17 )
								//    {
								//    }
								//    else
								//    {
								//        s = "★";
								//    }
								//    Trace.TraceInformation( "frame {0:D4}: {1:D3} ( {2:D3}, {3:D3}, {4:D3} ) {5}, n現在時刻={6}", f, dif, d1, d2, d3, s, d4 );
								//    lastd = d1;
								//    f++;
								//}
								//swlist1.Clear();
								//swlist2.Clear();
								//swlist3.Clear();
								//swlist4.Clear();
		
								#region [ プラグイン On演奏キャンセル() の呼び出し ]
								//---------------------
								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.On演奏キャンセル( scoreIni );
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}
								//---------------------
								#endregion

								DTX.t全チップの再生停止();
								DTX.On非活性化();
								r現在のステージ.On非活性化();
								if( bコンパクトモード )
								{
									base.Window.Close();
								}
								else
								{
									Trace.TraceInformation( "----------------------" );
									Trace.TraceInformation( "■ 選曲" );
                                    if( bXGRelease )
                                    {
									    stage選曲XG.On活性化();
									    r直前のステージ = r現在のステージ;
									    r現在のステージ = stage選曲XG;
                                    }
                                    else
                                    {
									    stage選曲GITADORA.On活性化();
									    r直前のステージ = r現在のステージ;
									    r現在のステージ = stage選曲GITADORA;
                                    }


									#region [ プラグイン Onステージ変更() の呼び出し ]
									//---------------------
									foreach( STPlugin pg in this.listプラグイン )
									{
										Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
										pg.plugin.Onステージ変更();
										Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
									}
									//---------------------
									#endregion

									this.tガベージコレクションを実行する();
								}
								break;
								//-----------------------------
								#endregion

							case (int) E演奏画面の戻り値.ステージ失敗:
								#region [ 演奏失敗(StageFailed) ]
								//-----------------------------
								scoreIni = this.tScoreIniへBGMAdjustとHistoryとPlayCountを更新( "Stage failed" );

								#region [ プラグイン On演奏失敗() の呼び出し ]
								//---------------------
								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.On演奏失敗( scoreIni );
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}
								//---------------------
								#endregion

								DTX.t全チップの再生停止();
								DTX.On非活性化();
								r現在のステージ.On非活性化();
								if( bコンパクトモード )
								{
									base.Window.Close();
								}
								else
								{
									Trace.TraceInformation( "----------------------" );
									Trace.TraceInformation( "■ 選曲" );
                                    if( bXGRelease )
                                    {
									    stage選曲XG.On活性化();
									    r直前のステージ = r現在のステージ;
									    r現在のステージ = stage選曲XG;
                                    }
                                    else
                                    {
									    stage選曲GITADORA.On活性化();
									    r直前のステージ = r現在のステージ;
									    r現在のステージ = stage選曲GITADORA;
                                    }

									#region [ プラグイン Onステージ変更() の呼び出し ]
									//---------------------
									foreach( STPlugin pg in this.listプラグイン )
									{
										Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
										pg.plugin.Onステージ変更();
										Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
									}
									//---------------------
									#endregion

									this.tガベージコレクションを実行する();
								}
								break;
								//-----------------------------
								#endregion

							case (int) E演奏画面の戻り値.ステージクリア:
								#region [ 演奏クリア ]
								//-----------------------------
								CScoreIni.C演奏記録 c演奏記録_Drums, c演奏記録_Guitar, c演奏記録_Bass;
								CDTX.CChip[] chipArray = new CDTX.CChip[ 11 ];
                                if( CDTXMania.bXGRelease )
                                {
								    if( ConfigIni.bギタレボモード ) {
									    stage演奏ギター画面.t演奏結果を格納する( out c演奏記録_Drums, out c演奏記録_Guitar, out c演奏記録_Bass );
								    } else {
									    stage演奏ドラム画面.t演奏結果を格納する( out c演奏記録_Drums, out c演奏記録_Guitar, out c演奏記録_Bass, out chipArray );
								    }
                                }
                                else
                                {
								    if( ConfigIni.bギタレボモード ) {
									    stage演奏ギター画面GITADORA.t演奏結果を格納する( out c演奏記録_Drums, out c演奏記録_Guitar, out c演奏記録_Bass );
								    } else {
									    stage演奏ドラム画面GITADORA.t演奏結果を格納する( out c演奏記録_Drums, out c演奏記録_Guitar, out c演奏記録_Bass, out chipArray );
								    }
                                }


								if ( CDTXMania.ConfigIni.bIsSwappedGuitarBass )		// #24063 2011.1.24 yyagi Gt/Bsを入れ替えていたなら、演奏結果も入れ替える
								{
									CScoreIni.C演奏記録 t;
									t = c演奏記録_Guitar;
									c演奏記録_Guitar = c演奏記録_Bass;
									c演奏記録_Bass = t;

									CDTXMania.DTX.SwapGuitarBassInfos();			// 譜面情報も元に戻す
									// #35417 2015.08.30 changed フラグにアクセスしている箇所が見つかったため有効化
									// #35417 2015.8.18 yyagi: AUTO系のフラグ入れ替えは削除可能!?。以後AUTOフラグに全くアクセスしておらず、意味がないため。
									// (直下でb全AUTOである にアクセスしているが、既に計算済みのクラスへのアクセスであり、ここでの交換対象ではない)
									CDTXMania.ConfigIni.SwapGuitarBassInfos_AutoFlags();
																					// #24415 2011.2.27 yyagi
																					// リザルト集計時のみ、Auto系のフラグも元に戻す。
																					// これを戻すのは、リザルト集計後。
								}													// "case CStage.Eステージ.結果:"のところ。

								double ps = 0.0, gs = 0.0;
								if ( !c演奏記録_Drums.b全AUTOである && c演奏記録_Drums.n全チップ数 > 0) {
									ps = c演奏記録_Drums.db演奏型スキル値;
									gs = c演奏記録_Drums.dbゲーム型スキル値;
								}
								else if ( !c演奏記録_Guitar.b全AUTOである && c演奏記録_Guitar.n全チップ数 > 0) {
									ps = c演奏記録_Guitar.db演奏型スキル値;
									gs = c演奏記録_Guitar.dbゲーム型スキル値;
								}
								else
								{
									ps = c演奏記録_Bass.db演奏型スキル値;
									gs = c演奏記録_Bass.dbゲーム型スキル値;
								}
								string str = "Cleared";
								switch( CScoreIni.t総合ランク値を計算して返す( c演奏記録_Drums, c演奏記録_Guitar, c演奏記録_Bass ) )
								{
									case (int)CScoreIni.ERANK.SS:
										str = string.Format( "Cleared (SS: {0:F2})", ps );
										break;

									case (int) CScoreIni.ERANK.S:
										str = string.Format( "Cleared (S: {0:F2})", ps );
										break;

									case (int) CScoreIni.ERANK.A:
										str = string.Format( "Cleared (A: {0:F2})", ps );
										break;

									case (int) CScoreIni.ERANK.B:
										str = string.Format( "Cleared (B: {0:F2})", ps );
										break;

									case (int) CScoreIni.ERANK.C:
										str = string.Format( "Cleared (C: {0:F2})", ps );
										break;

									case (int) CScoreIni.ERANK.D:
										str = string.Format( "Cleared (D: {0:F2})", ps );
										break;

									case (int) CScoreIni.ERANK.E:
										str = string.Format( "Cleared (E: {0:F2})", ps );
										break;

									case (int)CScoreIni.ERANK.UNKNOWN:	// #23534 2010.10.28 yyagi add: 演奏チップが0個のとき
										str = "Cleared (No chips)";
										break;
								}

								scoreIni = this.tScoreIniへBGMAdjustとHistoryとPlayCountを更新( str );

								#region [ プラグイン On演奏クリア() の呼び出し ]
								//---------------------
								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.On演奏クリア( scoreIni );
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}
								//---------------------
								#endregion

								r現在のステージ.On非活性化();
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 結果" );
								stage結果.st演奏記録.Drums = c演奏記録_Drums;
								stage結果.st演奏記録.Guitar = c演奏記録_Guitar;
								stage結果.st演奏記録.Bass = c演奏記録_Bass;
								stage結果.r空うちドラムチップ = chipArray;
								stage結果.On活性化();
								r直前のステージ = r現在のステージ;
								r現在のステージ = stage結果;

								#region [ プラグイン Onステージ変更() の呼び出し ]
								//---------------------
								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.Onステージ変更();
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}
								//---------------------
								#endregion

								break;
								//-----------------------------
								#endregion
						}
						//-----------------------------
						#endregion
						break;

					case CStage.Eステージ.結果:
						#region [ *** ]
						//-----------------------------
						if( this.n進行描画の戻り値 != 0 )
						{
							// #35417 2015.08.30 chnmr0 changed : ステージクリア処理で入れ替えるため元に戻した
							// #35417 2015.8.18 yyagi: AUTO系のフラグ入れ替えは削除可能!?。以後AUTOフラグに全くアクセスしておらず、意味がないため。
							if ( CDTXMania.ConfigIni.bIsSwappedGuitarBass )		// #24415 2011.2.27 yyagi Gt/Bsを入れ替えていたなら、Auto状態をリザルト画面終了後に元に戻す
							{
								CDTXMania.ConfigIni.SwapGuitarBassInfos_AutoFlags();	// Auto入れ替え
							}

							DTX.t全チップの再生一時停止();
							DTX.On非活性化();
							r現在のステージ.On非活性化();
							if( !bコンパクトモード )
							{
								Trace.TraceInformation( "----------------------" );
								Trace.TraceInformation( "■ 選曲" );
                                if( bXGRelease )
                                {
								    stage選曲XG.On活性化();
								    r直前のステージ = r現在のステージ;
								    r現在のステージ = stage選曲XG;
                                }
                                else
                                {
								    stage選曲GITADORA.On活性化();
								    r直前のステージ = r現在のステージ;
								    r現在のステージ = stage選曲GITADORA;
                                }

								foreach( STPlugin pg in this.listプラグイン )
								{
									Directory.SetCurrentDirectory( pg.strプラグインフォルダ );
									pg.plugin.Onステージ変更();
									Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
								}

								this.tガベージコレクションを実行する();
							}
							else
							{
								base.Window.Close();
							}
						}
						//-----------------------------
						#endregion
						break;

					case CStage.Eステージ.ChangeSkin:
						#region [ *** ]
						//-----------------------------
						if ( this.n進行描画の戻り値 != 0 )
						{
							r現在のステージ.On非活性化();
							Trace.TraceInformation( "----------------------" );
							Trace.TraceInformation( "■ 選曲" );
							stage選曲.On活性化();
							r直前のステージ = r現在のステージ;
							r現在のステージ = stage選曲;
							this.tガベージコレクションを実行する();
						}
						//-----------------------------
						#endregion
						break;

					case CStage.Eステージ.終了:
						#region [ *** ]
						//-----------------------------
						if( this.n進行描画の戻り値 != 0 )
						{
							base.Exit();
						}
						//-----------------------------
						#endregion
						break;
				}
			}
			this.Device.EndScene();			// Present()は game.csのOnFrameEnd()に登録された、GraphicsDeviceManager.game_FrameEnd() 内で実行されるので不要
											// (つまり、Present()は、Draw()完了後に実行される)
#if !GPUFlushAfterPresent
			actFlushGPU.On進行描画();		// Flush GPU	// EndScene()～Present()間 (つまりVSync前) でFlush実行
#endif
			if ( Sound管理.GetCurrentSoundDeviceType() != "DirectSound" )
			{
				Sound管理.t再生中の処理をする();	// サウンドバッファの更新; 画面描画と同期させることで、スクロールをスムーズにする
			}


			#region [ 全画面・ウインドウ切り替え ]
			if ( this.b次のタイミングで全画面_ウィンドウ切り替えを行う)
			{
				ConfigIni.b全画面モード = !ConfigIni.b全画面モード;
				app.t全画面_ウィンドウモード切り替え();
				this.b次のタイミングで全画面_ウィンドウ切り替えを行う = false;
			}
			#endregion
			#region [ 垂直基線同期切り替え ]
			if ( this.b次のタイミングで垂直帰線同期切り替えを行う )
			{
				bool bIsMaximized = this.Window.IsMaximized;											// #23510 2010.11.3 yyagi: to backup current window mode before changing VSyncWait
				currentClientSize = this.Window.ClientSize;												// #23510 2010.11.3 yyagi: to backup current window size before changing VSyncWait
				DeviceSettings currentSettings = app.GraphicsDeviceManager.CurrentSettings;
				currentSettings.EnableVSync = ConfigIni.b垂直帰線待ちを行う;
				app.GraphicsDeviceManager.ChangeDevice( currentSettings );
				this.b次のタイミングで垂直帰線同期切り替えを行う = false;
				base.Window.ClientSize = new Size(currentClientSize.Width, currentClientSize.Height);	// #23510 2010.11.3 yyagi: to resume window size after changing VSyncWait
				if (bIsMaximized)
				{
					this.Window.WindowState = FormWindowState.Maximized;								// #23510 2010.11.3 yyagi: to resume window mode after changing VSyncWait
				}
			}
			#endregion
		}

		// その他

		#region [ 汎用ヘルパー ]
		//-----------------
		#region [ tテクスチャの生成 ]
		public static CTexture tテクスチャの生成( string fileName )
		{
			return tテクスチャの生成( fileName, false );
		}
		public static CTexture tテクスチャの生成( string fileName, bool b黒を透過する )
		{
			if ( app == null )
			{
				return null;
			}
            if( File.Exists( fileName ) )
            {
			    try
    			{
	    			return new CTexture( app.Device, fileName, TextureFormat, b黒を透過する );
		    	}
			    catch ( CTextureCreateFailedException )
    			{
	    			Trace.TraceError( "テクスチャの生成に失敗しました。({0})", fileName );
		    		return null;
			    }
    			catch ( FileNotFoundException )
	    		{
		    		Trace.TraceError( "テクスチャファイルが見つかりませんでした。({0})", fileName );
			    	return null;
			    }
            }
			return null;
		}
		public static CTextureAf tテクスチャの生成Af( string fileName )
		{
			return tテクスチャの生成Af( fileName, false );
		}
		public static CTextureAf tテクスチャの生成Af( string fileName, bool b黒を透過する )
		{
			if ( app == null )
			{
				return null;
			}
            if( File.Exists( fileName ) )
            {
			    try
    			{
	    			return new CTextureAf( app.Device, fileName, TextureFormat, b黒を透過する );
		    	}
			    catch ( CTextureCreateFailedException )
    			{
	    			Trace.TraceError( "テクスチャの生成に失敗しました。({0})", fileName );
		    		return null;
			    }
    			catch ( FileNotFoundException )
	    		{
		    		Trace.TraceError( "テクスチャファイルが見つかりませんでした。({0})", fileName );
			    	return null;
			    }
            }
            return null;
		}
		public static void tテクスチャの解放( ref CTexture tx )
		{
			CDTXMania.t安全にDisposeする( ref tx );
		}
		public static void tテクスチャの解放( ref CTextureAf tx )
		{
			CDTXMania.t安全にDisposeする( ref tx );
		}
		public static CTexture tテクスチャの生成( byte[] txData )
		{
			return tテクスチャの生成( txData, false );
		}
		public static CTexture tテクスチャの生成( byte[] txData, bool b黒を透過する )
		{
			if ( app == null )
			{
				return null;
			}
			if ( txData == null )
			{
				Trace.TraceError( "テクスチャの生成に失敗しました。(txData==null)" );
				return null;
			}
			try
			{
				return new CTexture( app.Device, txData, TextureFormat, b黒を透過する );
			}
			catch ( CTextureCreateFailedException )
			{
				Trace.TraceError( "テクスチャの生成に失敗しました。(txData)" );
				return null;
			}
		}

		public static CTexture tテクスチャの生成( Bitmap bitmap )
		{
			return tテクスチャの生成( bitmap, false );
		}
		public static CTexture tテクスチャの生成( Bitmap bitmap, bool b黒を透過する )
		{
			if ( app == null )
			{
				return null;
			}
			if ( bitmap == null )
			{
				Trace.TraceError( "テクスチャの生成に失敗しました。(bitmap==null)" );
				return null;
			}
			try
			{
				return new CTexture( app.Device, bitmap, TextureFormat, b黒を透過する );
			}
			catch ( CTextureCreateFailedException )
			{
				Trace.TraceError( "テクスチャの生成に失敗しました。(bitmap)" );
				return null;
			}
		}
		#endregion
        public static CDirectShow t失敗してもスキップ可能なDirectShowを生成する( string fileName, IntPtr hWnd, bool bオーディオレンダラなし )
        {
            CDirectShow ds = null;
            if( File.Exists( fileName ) )
            {
                try
                {
                    ds = new CDirectShow( fileName, hWnd, bオーディオレンダラなし );
                }
                catch( FileNotFoundException )
                {
                    Trace.TraceError( "動画ファイルが見つかりませんでした。({0})", fileName );
                    ds = null;      // Dispose はコンストラクタ内で実施済み
                }
                catch
                {
                    Trace.TraceError( "DirectShow の生成に失敗しました。[{0}]", fileName );
                    ds = null;      // Dispose はコンストラクタ内で実施済み
                }
            }
            else
            {
                Trace.TraceError( "動画ファイルが見つかりませんでした。({0})", fileName );
                return null;
            }

            return ds;
        }
		/// <summary>プロパティ、インデクサには ref は使用できないので注意。</summary>
		public static void t安全にDisposeする<T>( ref T obj )
		{
			if ( obj == null )
				return;

			var d = obj as IDisposable;

			if ( d != null )
				d.Dispose();

			obj = default( T );
		}
        public static Image tテクスチャをImageで読み込む( string fileName )
        {
			if ( app == null )
			{
				return null;
			}
            if( File.Exists( fileName ) )
            {
			    try
    			{
	    			return Image.FromFile( fileName );
		    	}
			    catch ( FileNotFoundException )
    			{
	    			Trace.TraceError( "テクスチャファイルが見つからなかったため、空のテクスチャを返しました。({0})", fileName );
		    		return (Image)new Bitmap( 1, 1 );
			    }
            }
            else
            {
	    	    Trace.TraceError( "テクスチャファイルが見つからなかったため、空のテクスチャを返しました。({0})", fileName );
            }
		    return (Image)new Bitmap( 1, 1 );
        }
        /// <summary>XMLノードの値を安全に取り出す。</summary>
        public static string strXMLInnerText( XmlNode xmlNode, string strXmlNodePath )
        {
            if( xmlNode.SelectSingleNode( strXmlNodePath ) != null )
            {
                return xmlNode.SelectSingleNode( strXmlNodePath ).InnerText;
            }

            Trace.TraceError( "指定されたXMLノードは存在しません。({0})", strXmlNodePath );
            return "";
        }
		//-----------------
		#endregion

		#region [ private ]
		//-----------------
		private bool bマウスカーソル表示中 = true;
		private bool b終了処理完了済み;
		private static CDTX dtx;
		private List<CActivity> listトップレベルActivities;
		private int n進行描画の戻り値;
		private MouseButtons mb = System.Windows.Forms.MouseButtons.Left;
		private string strWindowTitle
		{
			get
			{
                string strCPUmode = ( Environment.Is64BitProcess ) ? " [x64]" : " [x86]";

				if ( DTXVmode.Enabled )
				{
					return "DTXViewer release " + VERSION + strCPUmode;
				}
				else
				{
					return "DTXMania .NET style release " + VERSION + strCPUmode;
				}
			}
		}
		private CSound previewSound;
		private bool b起動完了済み = false;

		private void t起動処理()
		{
			#region [ strEXEのあるフォルダを決定する ]
			//-----------------
			// BEGIN #23629 2010.11.13 from: デバッグ時は Application.ExecutablePath が ($SolutionDir)/bin/x86/Debug/ などになり System/ の読み込みに失敗するので、カレントディレクトリを採用する。（プロジェクトのプロパティ→デバッグ→作業ディレクトリが有効になる）
#if DEBUG
			strEXEのあるフォルダ = Environment.CurrentDirectory + @"\";
			//strEXEのあるフォルダ = Path.GetDirectoryName( Environment.GetCommandLineArgs()[ 0 ] ) + @"\";
#else
			strEXEのあるフォルダ = Path.GetDirectoryName( Application.ExecutablePath ) + @"\";	// #23629 2010.11.9 yyagi: set correct pathname where DTXManiaGR.exe is.
#endif
			// END #23629 2010.11.13 from
			//-----------------
			#endregion

			#region [ Config.ini の読込み ]
			//---------------------
			ConfigIni = new CConfigIni();
			string path = strEXEのあるフォルダ + "Config.ini";
			if (File.Exists(path))
			{
				try
				{
					ConfigIni.tファイルから読み込み(path);
				}
				catch
				{
					//ConfigIni = new CConfigIni();	// 存在してなければ新規生成
				}
			}
			this.Window.EnableSystemMenu = CDTXMania.ConfigIni.bIsEnabledSystemMenu;	// #28200 2011.5.1 yyagi
			// 2012.8.22 Config.iniが無いときに初期値が適用されるよう、この設定行をifブロック外に移動

			//---------------------
			#endregion
			#region [ ログ出力開始 ]
			//---------------------
			Trace.AutoFlush = true;
			if (ConfigIni.bログ出力)
			{
				try
				{
					Trace.Listeners.Add(new CTraceLogListener(new StreamWriter(System.IO.Path.Combine(strEXEのあるフォルダ, "DTXManiaLog.txt"), false, Encoding.GetEncoding("utf-16"))));
				}
				catch (System.UnauthorizedAccessException)			// #24481 2011.2.20 yyagi
				{
					int c = (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja") ? 0 : 1;
					string[] mes_writeErr = {
						"DTXManiaLog.txtへの書き込みができませんでした。書き込みできるようにしてから、再度起動してください。",
						"Failed to write DTXManiaLog.txt. Please set it writable and try again."
					};
					MessageBox.Show(mes_writeErr[c], "DTXMania boot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Environment.Exit(1);
				}
			}
			Trace.WriteLine("");
			Trace.WriteLine("DTXMania powered by YAMAHA Silent Session Drums");
			Trace.WriteLine(string.Format("Release: {0} {1} mode.", VERSION, (Environment.Is64BitProcess)? "x64":"x86" ));
			Trace.WriteLine("");
			Trace.TraceInformation("----------------------");
			Trace.TraceInformation("■ アプリケーションの初期化");
			Trace.TraceInformation("OS Version: " + Environment.OSVersion);
			Trace.TraceInformation("ProcessorCount: " + Environment.ProcessorCount.ToString());
			Trace.TraceInformation("CLR Version: " + Environment.Version.ToString());
			//---------------------
			#endregion
			#region [ DTXVmodeクラス の初期化 ]
			//---------------------
			//Trace.TraceInformation( "DTXVモードの初期化を行います。" );
			//Trace.Indent();
			try
			{
				DTXVmode = new CDTXVmode();
				DTXVmode.Enabled = false;
				//Trace.TraceInformation( "DTXVモードの初期化を完了しました。" );
			}
			finally
			{
				//Trace.Unindent();
			}
			//---------------------
			#endregion
			#region [ コンパクトモードスイッチの有無、もしくは、DTXViewerとしての起動 ]
			//---------------------
			bコンパクトモード = false;
			strコンパクトモードファイル = "";
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			if ((commandLineArgs != null) && (commandLineArgs.Length > 1))
			{
				bコンパクトモード = true;
				string arg = "";

				for (int i = 1; i < commandLineArgs.Length; i++)
				{
					if (i != 1)
					{
						arg += " " + "\"" + commandLineArgs[i] + "\"";
					}
					else
					{
						arg += commandLineArgs[i];
					}
				}
				DTXVmode.ParseArguments(arg);
				if (DTXVmode.Enabled)
				{
					DTXVmode.Refreshed = false;								// 初回起動時は再読み込みに走らせない
					strコンパクトモードファイル = DTXVmode.filename;
					switch (DTXVmode.soundDeviceType)						// サウンド再生方式の設定
					{
						case ESoundDeviceType.DirectSound:
							ConfigIni.nSoundDeviceType = 0;
							break;
						case ESoundDeviceType.ExclusiveWASAPI:
							ConfigIni.nSoundDeviceType = 2;
							break;
						case ESoundDeviceType.ASIO:
							ConfigIni.nSoundDeviceType = 1;
							ConfigIni.nASIODevice = DTXVmode.nASIOdevice;
							break;
					}

					CDTXMania.ConfigIni.b垂直帰線待ちを行う = DTXVmode.VSyncWait;
					CDTXMania.ConfigIni.bTimeStretch = DTXVmode.TimeStretch;
					CDTXMania.ConfigIni.bDrums有効 = !DTXVmode.GRmode;
					CDTXMania.ConfigIni.bGuitar有効 = true;

					CDTXMania.ConfigIni.b全画面モード = false;
					CDTXMania.ConfigIni.nウインドウwidth = CDTXMania.ConfigIni.nViewerウインドウwidth;
					CDTXMania.ConfigIni.nウインドウheight = CDTXMania.ConfigIni.nViewerウインドウheight;
					CDTXMania.ConfigIni.n初期ウィンドウ開始位置X = CDTXMania.ConfigIni.nViewer初期ウィンドウ開始位置X;
					CDTXMania.ConfigIni.n初期ウィンドウ開始位置Y = CDTXMania.ConfigIni.nViewer初期ウィンドウ開始位置Y;
				}
				else														// 通常のコンパクトモード
				{
					strコンパクトモードファイル = commandLineArgs[1];
				}

				if (!File.Exists(strコンパクトモードファイル))		// #32985 2014.1.23 yyagi 
				{
					Trace.TraceError("コンパクトモードで指定されたファイルが見つかりません。DTXManiaを終了します。[{0}]", strコンパクトモードファイル);
#if DEBUG
					Environment.Exit(-1);
#else
					if ( strコンパクトモードファイル == "" )	// DTXMania未起動状態で、DTXCで再生停止ボタンを押した場合は、何もせず終了
					{
						Environment.Exit( -1 );
					}
					else
					{
						throw new FileNotFoundException( "コンパクトモードで指定されたファイルが見つかりません。DTXManiaを終了します。", strコンパクトモードファイル );
					}
#endif
				}
				if (DTXVmode.Enabled)
				{
					Trace.TraceInformation("DTXVモードで起動します。[{0}]", strコンパクトモードファイル);
				}
				else
				{
					Trace.TraceInformation("コンパクトモードで起動します。[{0}]", strコンパクトモードファイル);
				}
			}
			//---------------------
			#endregion


			#region [ Input管理 の初期化 ]
			//---------------------
			Trace.TraceInformation("DirectInput, MIDI入力の初期化を行います。");
			Trace.Indent();
			try
			{
				bool bUseMIDIIn = !DTXVmode.Enabled;
				Input管理 = new CInput管理(base.Window.Handle, bUseMIDIIn);
				foreach (IInputDevice device in Input管理.list入力デバイス)
				{
					if ((device.e入力デバイス種別 == E入力デバイス種別.Joystick) && !ConfigIni.dicJoystick.ContainsValue(device.GUID))
					{
						int key = 0;
						while (ConfigIni.dicJoystick.ContainsKey(key))
						{
							key++;
						}
						ConfigIni.dicJoystick.Add(key, device.GUID);
					}
				}
				foreach (IInputDevice device2 in Input管理.list入力デバイス)
				{
					if (device2.e入力デバイス種別 == E入力デバイス種別.Joystick)
					{
						foreach (KeyValuePair<int, string> pair in ConfigIni.dicJoystick)
						{
							if (device2.GUID.Equals(pair.Value))
							{
								((CInputJoystick)device2).SetID(pair.Key);
								break;
							}
						}
						continue;
					}
				}
				Trace.TraceInformation("DirectInput の初期化を完了しました。");
			}
			catch (Exception exception2)
			{
				Trace.TraceError(exception2.Message);
				Trace.TraceError("DirectInput, MIDI入力の初期化に失敗しました。");

				int c = (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja") ? 0 : 1;
				string[] mes_writeErr = {
						"DirectInputまたはMIDI入力の初期化に失敗しました。DTXManiaGRを終了します。",
						"Failed to initialize DirectInput (or MIDI-IN)."
				};
				MessageBox.Show(mes_writeErr[c], "DTXMania boot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion

			#region [ ウィンドウ初期化 ]
			//---------------------
			base.Window.StartPosition = FormStartPosition.Manual;                                                       // #30675 2013.02.04 ikanick add
			base.Window.Location = new Point(ConfigIni.n初期ウィンドウ開始位置X, ConfigIni.n初期ウィンドウ開始位置Y);   // #30675 2013.02.04 ikanick add

			base.Window.Text = this.strWindowTitle;		// 事前にDTXVmodeの実体を作っておくこと

			base.Window.StartPosition = FormStartPosition.Manual;                                                       // #30675 2013.02.04 ikanick add
			base.Window.Location = new Point(ConfigIni.n初期ウィンドウ開始位置X, ConfigIni.n初期ウィンドウ開始位置Y);   // #30675 2013.02.04 ikanick add

			if (ConfigIni.nウインドウwidth <= 0 && ConfigIni.nウインドウheight <= 0)		// #34069 2014.7.24 yyagi 初回起動時は1280x720にする
			{
				ConfigIni.nウインドウwidth = 1280;
				ConfigIni.nウインドウheight = 720;
				//w = this.Window.Screen.WorkingArea.Width;
				//h = this.Window.Screen.WorkingArea.Height;
				//w = base.Window.Screen.Bounds.Width;
				//h = base.Window.Screen.Bounds.Height;
				//ConfigIni.nウインドウwidth = SampleFramework.GameWindowSize.Width;
				//ConfigIni.nウインドウheight = SampleFramework.GameWindowSize.Height;
			}

			base.Window.ClientSize = new Size(ConfigIni.nウインドウwidth, ConfigIni.nウインドウheight);	// #34510 yyagi 2010.10.31 to change window size got from Config.ini
#if !WindowedFullscreen
			if (!ConfigIni.bウィンドウモード)						// #23510 2010.11.02 yyagi: add; to recover window size in case bootup with fullscreen mode
			{														// #30666 2013.02.02 yyagi: currentClientSize should be always made
#endif
			currentClientSize = new Size(ConfigIni.nウインドウwidth, ConfigIni.nウインドウheight);
#if !WindowedFullscreen
			}
#endif
			base.Window.MaximizeBox = true;							// #23510 2010.11.04 yyagi: to support maximizing window
			base.Window.FormBorderStyle = FormBorderStyle.Sizable;	// #23510 2010.10.27 yyagi: changed from FixedDialog to Sizable, to support window resize
			// #30666 2013.02.02 yyagi: moved the code to t全画面・ウインドウモード切り替え()
			base.Window.ShowIcon = true;
			base.Window.Icon = Properties.Resources.dtx;
			base.Window.KeyDown += new KeyEventHandler(this.Window_KeyDown);
			base.Window.MouseUp += new MouseEventHandler(this.Window_MouseUp);
			base.Window.MouseDoubleClick += new MouseEventHandler(this.Window_MouseDoubleClick);	// #23510 2010.11.13 yyagi: to go fullscreen mode
			base.Window.ResizeEnd += new EventHandler(this.Window_ResizeEnd);						// #23510 2010.11.20 yyagi: to set resized window size in Config.ini
			base.Window.ApplicationActivated += new EventHandler(this.Window_ApplicationActivated);
			base.Window.ApplicationDeactivated += new EventHandler(this.Window_ApplicationDeactivated);
			//---------------------
			#endregion
			#region [ Direct3D9Exを使うかどうか判定 ]
			#endregion
			#region [ Direct3D9 デバイスの生成 ]
			//---------------------
			DeviceSettings settings = new DeviceSettings();
#if WindowedFullscreen
			settings.Windowed = true;								// #30666 2013.2.2 yyagi: Fullscreenmode is "Maximized window" mode
#else
			settings.Windowed = ConfigIni.bウィンドウモード;
#endif
			settings.BackBufferWidth = SampleFramework.GameWindowSize.Width;
			settings.BackBufferHeight = SampleFramework.GameWindowSize.Height;
			//			settings.BackBufferCount = 3;
			settings.EnableVSync = ConfigIni.b垂直帰線待ちを行う;
			//			settings.BackBufferFormat = Format.A8R8G8B8;
			//			settings.MultisampleType = MultisampleType.FourSamples;
			//			settings.MultisampleQuality = 4;
			//			settings.MultisampleType = MultisampleType.None;
			//			settings.MultisampleQuality = 0;

			try
			{
				base.GraphicsDeviceManager.ChangeDevice(settings);
			}
			catch (DeviceCreationException e)
			{
				Trace.TraceError(e.ToString());
				MessageBox.Show(e.Message + e.ToString(), "DTXMania failed to boot: DirectX9 Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(-1);
			}

			base.IsFixedTimeStep = false;
			//			base.TargetElapsedTime = TimeSpan.FromTicks( 10000000 / 75 );
			base.Window.ClientSize = new Size(ConfigIni.nウインドウwidth, ConfigIni.nウインドウheight);	// #23510 2010.10.31 yyagi: to recover window size. width and height are able to get from Config.ini.
			base.InactiveSleepTime = TimeSpan.FromMilliseconds((float)(ConfigIni.n非フォーカス時スリープms));	// #23568 2010.11.3 yyagi: to support valiable sleep value when !IsActive
			// #23568 2010.11.4 ikanick changed ( 1 -> ConfigIni )
#if WindowedFullscreen
			this.t全画面_ウィンドウモード切り替え();				// #30666 2013.2.2 yyagi: finalize settings for "Maximized window mode"
#endif
			actFlushGPU = new CActFlushGPU();
			//---------------------
			#endregion

			DTX = null;

			#region [ Skin の初期化 ]
			//---------------------
			Trace.TraceInformation("スキンの初期化を行います。");
			Trace.Indent();
			try
			{
				Skin = new CSkin(CDTXMania.ConfigIni.strSystemSkinSubfolderFullName, CDTXMania.ConfigIni.bUseBoxDefSkin);
				CDTXMania.ConfigIni.strSystemSkinSubfolderFullName = CDTXMania.Skin.GetCurrentSkinSubfolderFullName(true);	// 旧指定のSkinフォルダが消滅していた場合に備える
				Trace.TraceInformation("スキンの初期化を完了しました。");
			}
			catch
			{
				Trace.TraceInformation("スキンの初期化に失敗しました。");
				throw;
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion
			//-----------
			#region [ Timer の初期化 ]
			//---------------------
			Trace.TraceInformation("タイマの初期化を行います。");
			Trace.Indent();
			try
			{
				Timer = new CTimer(CTimer.E種別.MultiMedia);
				Trace.TraceInformation("タイマの初期化を完了しました。");
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion
			//-----------

			#region [ FPS カウンタの初期化 ]
			//---------------------
			Trace.TraceInformation("FPSカウンタの初期化を行います。");
			Trace.Indent();
			try
			{
				FPS = new CFPS();
				Trace.TraceInformation("FPSカウンタを生成しました。");
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion
			#region [ act文字コンソールの初期化 ]
			//---------------------
			Trace.TraceInformation("文字コンソールの初期化を行います。");
			Trace.Indent();
			try
			{
				act文字コンソール = new C文字コンソール();
				Trace.TraceInformation("文字コンソールを生成しました。");
				act文字コンソール.On活性化();
				Trace.TraceInformation("文字コンソールを活性化しました。");
				Trace.TraceInformation("文字コンソールの初期化を完了しました。");
			}
			catch (Exception exception)
			{
				Trace.TraceError(exception.Message);
				Trace.TraceError("文字コンソールの初期化に失敗しました。");
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion
			#region [ Pad の初期化 ]
			//---------------------
			Trace.TraceInformation("パッドの初期化を行います。");
			Trace.Indent();
			try
			{
				Pad = new CPad(ConfigIni, Input管理);
				Trace.TraceInformation("パッドの初期化を完了しました。");
			}
			catch (Exception exception3)
			{
				Trace.TraceError(exception3.Message);
				Trace.TraceError("パッドの初期化に失敗しました。");
			}
			finally
			{
				Trace.Unindent();
			}
			 //---------------------
			#endregion
			#region [ Sound管理 の初期化 ]
			//---------------------
			Trace.TraceInformation("サウンドデバイスの初期化を行います。");
			Trace.Indent();
			try
			{
				ESoundDeviceType soundDeviceType;
				switch (CDTXMania.ConfigIni.nSoundDeviceType)
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
				Sound管理 = new CSound管理(base.Window.Handle,
											soundDeviceType,
											CDTXMania.ConfigIni.nWASAPIBufferSizeMs,
                                            CDTXMania.ConfigIni.bEventDrivenWASAPI,
											0,
											CDTXMania.ConfigIni.nASIODevice,
											CDTXMania.ConfigIni.bUseOSTimer
				);
				//Sound管理 = FDK.CSound管理.Instance;
				//Sound管理.t初期化( soundDeviceType, 0, 0, CDTXMania.ConfigIni.nASIODevice, base.Window.Handle );
                

				ShowWindowTitleWithSoundType();
				FDK.CSound管理.bIsTimeStretch = CDTXMania.ConfigIni.bTimeStretch;
				Sound管理.nMasterVolume = CDTXMania.ConfigIni.nMasterVolume;
				//FDK.CSound管理.bIsMP3DecodeByWindowsCodec = CDTXMania.ConfigIni.bNoMP3Streaming;


				string strDefaultSoundDeviceBusType = CSound管理.strDefaultDeviceBusType;
				Trace.TraceInformation($"Bus type of the default sound device = {strDefaultSoundDeviceBusType}");

				if (strDefaultSoundDeviceBusType.ToUpper().Equals("USB"))
				{
					if (CDTXMania.ConfigIni.bWarnSoundDeviceOnUSB)
					{
						string strWarnMes = "あなたがPCとMIDI機器を接続するためにお使いのケーブル(MIDI2.0-USB)は、MIDI入力が不安定なことが多く報告されているため、DTXManiaでのご利用はお勧めしません。\n\n"+
                        "OKをクリックすると、続行します。(以後このメッセージを表示しません)\n" +
                        "キャンセルをクリックすると、続行します。";
						var ret = MessageBox.Show(strWarnMes, "DTXMania Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
						if (ret == DialogResult.OK)
						{
							CDTXMania.ConfigIni.bWarnSoundDeviceOnUSB = false;
						}
					}
				}

				Trace.TraceInformation("サウンドデバイスの初期化を完了しました。");
			}
			catch (Exception e)
			{
				Trace.TraceError(e.Message);
				throw;
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion
			#region [ Songs管理 の初期化 ]
			//---------------------
			Trace.TraceInformation("曲リストの初期化を行います。");
			Trace.Indent();
			try
			{
				Songs管理 = new CSongs管理();
				//				Songs管理_裏読 = new CSongs管理();
				EnumSongs = new CEnumSongs();
				actEnumSongs = new CActEnumSongs();
				Trace.TraceInformation("曲リストの初期化を完了しました。");
			}
			catch (Exception e)
			{
				Trace.TraceError(e.Message);
				Trace.TraceError("曲リストの初期化に失敗しました。");
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion
			#region [ CAvi の初期化 ]
			//---------------------
			CAvi.t初期化();
			//---------------------
			#endregion
			#region [ Random の初期化 ]
			//---------------------
			Random = new Random((int)Timer.nシステム時刻);
			//---------------------
			#endregion
			#region [ ステージの初期化 ]
			//---------------------
			r現在のステージ = null;
			r直前のステージ = null;
			stage起動 = new CStage起動();
			stageタイトル = new CStageタイトル();
			//			stageオプション = new CStageオプション();
			stageコンフィグ = new CStageコンフィグ();
			stage選曲 = new CStage選曲();
            stage選曲XG = new CStage選曲XG();
            stage選曲GITADORA = new CStage選曲GITADORA();
			stage曲読み込み = new CStage曲読み込み();
			stage演奏ドラム画面 = new CStage演奏ドラム画面();
			stage演奏ギター画面 = new CStage演奏ギター画面();
            stage演奏ドラム画面GITADORA = new CStage演奏ドラム画面GD();
            stage演奏ギター画面GITADORA = new CStage演奏ギター画面GD();
			stage結果 = new CStage結果();
			stageChangeSkin = new CStageChangeSkin();
			stage終了 = new CStage終了();
			this.listトップレベルActivities = new List<CActivity>();
			this.listトップレベルActivities.Add(actEnumSongs);
			this.listトップレベルActivities.Add(act文字コンソール);
			this.listトップレベルActivities.Add(stage起動);
			this.listトップレベルActivities.Add(stageタイトル);
			//			this.listトップレベルActivities.Add( stageオプション );
			this.listトップレベルActivities.Add(stageコンフィグ);
			this.listトップレベルActivities.Add(stage選曲);
			this.listトップレベルActivities.Add(stage曲読み込み);
			this.listトップレベルActivities.Add(stage演奏ドラム画面);
			this.listトップレベルActivities.Add(stage演奏ギター画面);
            if( CDTXMania.bXGRelease )
            {
                this.listトップレベルActivities.Add( stage演奏ドラム画面GITADORA );
                this.listトップレベルActivities.Add( stage演奏ギター画面GITADORA );
            }
			this.listトップレベルActivities.Add(stage結果);
			this.listトップレベルActivities.Add(stageChangeSkin);
			this.listトップレベルActivities.Add(stage終了);
			this.listトップレベルActivities.Add(actFlushGPU);
			//---------------------
			#endregion
			#region [ プラグインの検索と生成 ]
			//---------------------
			PluginHost = new CPluginHost();

			Trace.TraceInformation("プラグインの検索と生成を行います。");
			Trace.Indent();
			try
			{
				this.tプラグイン検索と生成();
				Trace.TraceInformation("プラグインの検索と生成を完了しました。");
			}
			finally
			{
				Trace.Unindent();
			}
			//---------------------
			#endregion
			#region [ プラグインの初期化 ]
			//---------------------
			if (this.listプラグイン != null && this.listプラグイン.Count > 0)
			{
				Trace.TraceInformation("プラグインの初期化を行います。");
				Trace.Indent();
				try
				{
					foreach (STPlugin st in this.listプラグイン)
					{
						Directory.SetCurrentDirectory(st.strプラグインフォルダ);
						st.plugin.On初期化(this.PluginHost);
						st.plugin.OnManagedリソースの作成();
						st.plugin.OnUnmanagedリソースの作成();
						Directory.SetCurrentDirectory(CDTXMania.strEXEのあるフォルダ);
					}
					Trace.TraceInformation("すべてのプラグインの初期化を完了しました。");
				}
				catch
				{
					Trace.TraceError("プラグインのどれかの初期化に失敗しました。");
					throw;
				}
				finally
				{
					Trace.Unindent();
				}
			}

			//---------------------
			#endregion

			Trace.TraceInformation("アプリケーションの初期化を完了しました。");

			#region [ 最初のステージの起動 ]
			//---------------------
			Trace.TraceInformation("----------------------");
			Trace.TraceInformation("■ 起動");

			if (CDTXMania.bコンパクトモード)
			{
				r現在のステージ = stage曲読み込み;
			}
			else
			{
				r現在のステージ = stage起動;
			}
			r現在のステージ.On活性化();
			//---------------------
			#endregion

			this.b起動完了済み = true;
		}

		public void ShowWindowTitleWithSoundType()
		{
			string delay = "";
			if ( Sound管理.GetCurrentSoundDeviceType() != "DirectSound" )
			{
				delay = "(" + Sound管理.GetSoundDelay() + "ms)";
			}
			base.Window.Text = strWindowTitle + " (" + Sound管理.GetCurrentSoundDeviceType().ToString() + delay + ")";
		}

		private void t終了処理()
		{
			if( !this.b終了処理完了済み )
			{
				Trace.TraceInformation( "----------------------" );
				Trace.TraceInformation( "■ アプリケーションの終了" );
				#region [ 曲検索の終了処理 ]
				//---------------------
				if ( actEnumSongs != null )
				{
					Trace.TraceInformation( "曲検索actの終了処理を行います。" );
					Trace.Indent();
					try
					{
						actEnumSongs.On非活性化();
						actEnumSongs= null;
						Trace.TraceInformation( "曲検索actの終了処理を完了しました。" );
					}
					catch ( Exception e )
					{
						Trace.TraceError( e.Message );
						Trace.TraceError( "曲検索actの終了処理に失敗しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ 現在のステージの終了処理 ]
				//---------------------
				if( CDTXMania.r現在のステージ != null && CDTXMania.r現在のステージ.b活性化してる )		// #25398 2011.06.07 MODIFY FROM
				{
					Trace.TraceInformation( "現在のステージを終了します。" );
					Trace.Indent();
					try
					{
						r現在のステージ.On非活性化();
						Trace.TraceInformation( "現在のステージの終了処理を完了しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ プラグインの終了処理 ]
				//---------------------
				if (this.listプラグイン != null && this.listプラグイン.Count > 0)
				{
					Trace.TraceInformation( "すべてのプラグインを終了します。" );
					Trace.Indent();
					try
					{
						foreach( STPlugin st in this.listプラグイン )
						{
							Directory.SetCurrentDirectory( st.strプラグインフォルダ );
							st.plugin.OnUnmanagedリソースの解放();
							st.plugin.OnManagedリソースの解放();
							st.plugin.On終了();
							Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
						}
						PluginHost = null;
						Trace.TraceInformation( "すべてのプラグインの終了処理を完了しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ 曲リストの終了処理 ]
				//---------------------
				if (Songs管理 != null)
				{
					Trace.TraceInformation( "曲リストの終了処理を行います。" );
					Trace.Indent();
					try
					{
						Songs管理 = null;
						Trace.TraceInformation( "曲リストの終了処理を完了しました。" );
					}
					catch( Exception exception )
					{
						Trace.TraceError( exception.Message );
						Trace.TraceError( "曲リストの終了処理に失敗しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				CAvi.t終了();
				//---------------------
				#endregion
				#region [ スキンの終了処理 ]
				//---------------------
				if (Skin != null)
				{
					Trace.TraceInformation( "スキンの終了処理を行います。" );
					Trace.Indent();
					try
					{
						Skin.Dispose();
						Skin = null;
						Trace.TraceInformation( "スキンの終了処理を完了しました。" );
					}
					catch( Exception exception2 )
					{
						Trace.TraceError( exception2.Message );
						Trace.TraceError( "スキンの終了処理に失敗しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ DirectSoundの終了処理 ]
				//---------------------
				if (Sound管理 != null)
				{
					Trace.TraceInformation( "DirectSound の終了処理を行います。" );
					Trace.Indent();
					try
					{
						Sound管理.Dispose();
						Sound管理 = null;
						Trace.TraceInformation( "DirectSound の終了処理を完了しました。" );
					}
					catch( Exception exception3 )
					{
						Trace.TraceError( exception3.Message );
						Trace.TraceError( "DirectSound の終了処理に失敗しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ パッドの終了処理 ]
				//---------------------
				if (Pad != null)
				{
					Trace.TraceInformation( "パッドの終了処理を行います。" );
					Trace.Indent();
					try
					{
						Pad = null;
						Trace.TraceInformation( "パッドの終了処理を完了しました。" );
					}
					catch( Exception exception4 )
					{
						Trace.TraceError( exception4.Message );
						Trace.TraceError( "パッドの終了処理に失敗しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ DirectInput, MIDI入力の終了処理 ]
				//---------------------
				if (Input管理 != null)
				{
					Trace.TraceInformation( "DirectInput, MIDI入力の終了処理を行います。" );
					Trace.Indent();
					try
					{
						Input管理.Dispose();
						Input管理 = null;
						Trace.TraceInformation( "DirectInput, MIDI入力の終了処理を完了しました。" );
					}
					catch( Exception exception5 )
					{
						Trace.TraceError( exception5.Message );
						Trace.TraceError( "DirectInput, MIDI入力の終了処理に失敗しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ 文字コンソールの終了処理 ]
				//---------------------
				if (act文字コンソール != null)
				{
					Trace.TraceInformation( "文字コンソールの終了処理を行います。" );
					Trace.Indent();
					try
					{
						act文字コンソール.On非活性化();
						act文字コンソール = null;
						Trace.TraceInformation( "文字コンソールの終了処理を完了しました。" );
					}
					catch( Exception exception6 )
					{
						Trace.TraceError( exception6.Message );
						Trace.TraceError( "文字コンソールの終了処理に失敗しました。" );
					}
					finally
					{
						Trace.Unindent();
					}
				}
				//---------------------
				#endregion
				#region [ FPSカウンタの終了処理 ]
				//---------------------
				Trace.TraceInformation("FPSカウンタの終了処理を行います。");
				Trace.Indent();
				try
				{
					if( FPS != null )
					{
						FPS = null;
					}
					Trace.TraceInformation( "FPSカウンタの終了処理を完了しました。" );
				}
				finally
				{
					Trace.Unindent();
				}
				//---------------------
				#endregion
				#region [ タイマの終了処理 ]
				//---------------------
				Trace.TraceInformation("タイマの終了処理を行います。");
				Trace.Indent();
				try
				{
					if( Timer != null )
					{
						Timer.Dispose();
						Timer = null;
						Trace.TraceInformation( "タイマの終了処理を完了しました。" );
					}
					else
					{
						Trace.TraceInformation( "タイマは使用されていません。" );
					}
				}
				finally
				{
					Trace.Unindent();
				}
                //---------------------
                #endregion
                #region [ Windows Animation Managerの終了処理 ]
				//---------------------
				Trace.TraceInformation("Windows Animation Managerの終了処理を行います。");
				Trace.Indent();
				try
                {
					AnimationManager?.Dispose();
					AnimationManager = null;
					Trace.TraceInformation("Windows Animation Managerの終了処理を完了しました。");
				}
				finally
                {
					Trace.Unindent();
                }
				//---------------------
                #endregion
                #region [ Config.iniの出力 ]
                //---------------------
                Trace.TraceInformation("Config.ini を出力します。");
//				if ( ConfigIni.bIsSwappedGuitarBass )			// #24063 2011.1.16 yyagi ギターベースがスワップしているときは元に戻す
				if ( ConfigIni.bIsSwappedGuitarBass_AutoFlagsAreSwapped )	// #24415 2011.2.21 yyagi FLIP中かつ演奏中にalt-f4で終了したときは、AUTOのフラグをswapして戻す
				{
				    ConfigIni.SwapGuitarBassInfos_AutoFlags();
				}
				if ( ConfigIni.bIsSwappedGuitarBass_PlaySettingsAreSwapped)	// #35417 2015/8/18 yyagi FLIP中かつ演奏中にalt-f4で終了したときは、演奏設定のフラグをswapして戻す
				{
					ConfigIni.SwapGuitarBassInfos_PlaySettings();
				}
				string str = strEXEのあるフォルダ + "Config.ini";
				Trace.Indent();
				try
				{
					if ( DTXVmode.Enabled )
					{
						DTXVmode.tUpdateConfigIni();
						Trace.TraceInformation( "DTXVモードの設定情報を、Config.iniに保存しました。" );
					}
					else
					{
						ConfigIni.t書き出し( str );
						Trace.TraceInformation( "保存しました。({0})", str );
					}
				}
				catch( Exception e )
				{
					Trace.TraceError( e.Message );
					Trace.TraceError( "Config.ini の出力に失敗しました。({0})", str );
				}
				finally
				{
					Trace.Unindent();
				}
				//---------------------
				#endregion
				#region [ DTXVmodeの終了処理 ]
				//---------------------
				//Trace.TraceInformation( "DTXVモードの終了処理を行います。" );
				//Trace.Indent();
				try
				{
					if ( DTXVmode != null )
					{
						DTXVmode = null;
						//Trace.TraceInformation( "DTXVモードの終了処理を完了しました。" );
					}
					else
					{
						//Trace.TraceInformation( "DTXVモードは使用されていません。" );
					}
				}
				finally
				{
					//Trace.Unindent();
				}
				//---------------------
				#endregion
				Trace.TraceInformation( "アプリケーションの終了処理を完了しました。" );


				this.b終了処理完了済み = true;
			}
		}
		private CScoreIni tScoreIniへBGMAdjustとHistoryとPlayCountを更新(string str新ヒストリ行)
		{
			bool bIsUpdatedDrums, bIsUpdatedGuitar, bIsUpdatedBass;
			string strFilename = DTX.strファイル名の絶対パス + ".score.ini";
			CScoreIni ini = new CScoreIni( strFilename );
			if( !File.Exists( strFilename ) )
			{
				ini.stファイル.Title = DTX.TITLE;
				ini.stファイル.Name = DTX.strファイル名;
				ini.stファイル.Hash = CScoreIni.tファイルのMD5を求めて返す( DTX.strファイル名の絶対パス );
				for( int i = 0; i < 6; i++ )
				{
					ini.stセクション[ i ].nPerfectになる範囲ms = nPerfect範囲ms;
					ini.stセクション[ i ].nGreatになる範囲ms = nGreat範囲ms;
					ini.stセクション[ i ].nGoodになる範囲ms = nGood範囲ms;
					ini.stセクション[ i ].nPoorになる範囲ms = nPoor範囲ms;
				}
			}
			ini.stファイル.BGMAdjust = DTX.nBGMAdjust;
			CScoreIni.t更新条件を取得する( out bIsUpdatedDrums, out bIsUpdatedGuitar, out bIsUpdatedBass );
			if( bIsUpdatedDrums || bIsUpdatedGuitar || bIsUpdatedBass )
			{
				if( bIsUpdatedDrums )
				{
					ini.stファイル.PlayCountDrums++;
				}
				if( bIsUpdatedGuitar )
				{
					ini.stファイル.PlayCountGuitar++;
				}
				if( bIsUpdatedBass )
				{
					ini.stファイル.PlayCountBass++;
				}
				ini.tヒストリを追加する( str新ヒストリ行 );
				if( !bコンパクトモード )
				{
                    if( CDTXMania.bXGRelease ) {
					    stage選曲XG.r現在選択中のスコア.譜面情報.演奏回数.Drums = ini.stファイル.PlayCountDrums;
					    stage選曲XG.r現在選択中のスコア.譜面情報.演奏回数.Guitar = ini.stファイル.PlayCountGuitar;
					    stage選曲XG.r現在選択中のスコア.譜面情報.演奏回数.Bass = ini.stファイル.PlayCountBass;
                    } else {
					    stage選曲GITADORA.r現在選択中のスコア.譜面情報.演奏回数.Drums = ini.stファイル.PlayCountDrums;
					    stage選曲GITADORA.r現在選択中のスコア.譜面情報.演奏回数.Guitar = ini.stファイル.PlayCountGuitar;
					    stage選曲GITADORA.r現在選択中のスコア.譜面情報.演奏回数.Bass = ini.stファイル.PlayCountBass;
                    }


					for( int j = 0; j < ini.stファイル.History.Length; j++ )
					{
                        if( CDTXMania.bXGRelease ) {
						    stage選曲XG.r現在選択中のスコア.譜面情報.演奏履歴[ j ] = ini.stファイル.History[ j ];
                        } else {
						    stage選曲GITADORA.r現在選択中のスコア.譜面情報.演奏履歴[ j ] = ini.stファイル.History[ j ];
                        }

					}
				}
			}
			if( ConfigIni.bScoreIniを出力する )
			{
				ini.t書き出し( strFilename );
			}

			return ini;
		}
		private void tガベージコレクションを実行する()
		{
			//LOHに対するコンパクションを要求
			GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
		
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			GC.WaitForPendingFinalizers();

            //通常通り、LOHへのGCを抑制
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.Default;
        }
		private void tプラグイン検索と生成()
		{
			this.listプラグイン = new List<STPlugin>();

			string strIPluginActivityの名前 = typeof( IPluginActivity ).FullName;
			string strプラグインフォルダパス = strEXEのあるフォルダ + "Plugins\\";

			this.t指定フォルダ内でのプラグイン検索と生成( strプラグインフォルダパス, strIPluginActivityの名前 );

			if( this.listプラグイン.Count > 0 )
				Trace.TraceInformation( this.listプラグイン.Count + " 個のプラグインを読み込みました。" );
		}
		#region [ Windowイベント処理 ]

		private System.Reflection.Assembly CurrentDomain_AssemblyResolve( object sender, ResolveEventArgs args )
		{
			var domain = (AppDomain) sender;


			foreach ( var assembly in domain.GetAssemblies() )
			{
				if ( assembly.FullName == args.Name )
					return assembly;
			}


			return null;



		}
		private void t指定フォルダ内でのプラグイン検索と生成( string strプラグインフォルダパス, string strプラグイン型名 )
		{
			// 指定されたパスが存在しないとエラー
			if( !Directory.Exists( strプラグインフォルダパス ) )
			{
				Trace.TraceWarning( "プラグインフォルダが存在しません。(" + strプラグインフォルダパス + ")" );
				return;
			}

			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;


			// (1) すべての *.dll について…
			string[] strDLLs = System.IO.Directory.GetFiles( strプラグインフォルダパス, "*.dll" );
			foreach( string dllName in strDLLs )
			{
				try
				{
					// (1-1) dll をアセンブリとして読み込む。
					System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom( dllName );

					// (1-2) アセンブリ内のすべての型について、プラグインとして有効か調べる
					foreach ( Type t in asm.GetTypes() )
					{
						//  (1-3) ↓クラスであり↓Publicであり↓抽象クラスでなく↓IPlugin型のインスタンスが作れる　型を持っていれば有効
						if ( t.IsClass && t.IsPublic && !t.IsAbstract && t.GetInterface( strプラグイン型名 ) != null )
						{
							// (1-4) クラス名からインスタンスを作成する
							var st = new STPlugin()
							{
								plugin = (IPluginActivity) asm.CreateInstance( t.FullName ),
								strプラグインフォルダ = Path.GetDirectoryName( dllName ),
								strアセンブリ簡易名 = asm.GetName().Name,
								Version = asm.GetName().Version,
							};

							// (1-5) プラグインリストへ登録
							this.listプラグイン.Add( st );
							Trace.TraceInformation( "プラグイン {0} ({1}, {2}, {3}) を読み込みました。", t.FullName, Path.GetFileName( dllName ), st.strアセンブリ簡易名, st.Version.ToString() );
						}
					}
				}
				catch ( System.Reflection.ReflectionTypeLoadException e )
				{
					Trace.TraceInformation( dllName + " からプラグインを生成することに失敗しました。スキップします。" );
					Trace.TraceInformation( e.ToString() );
					Trace.TraceInformation( e.Message );
{
    StringBuilder sb = new StringBuilder();
    foreach (Exception exSub in e.LoaderExceptions)
    {
        sb.AppendLine(exSub.Message);
        FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
        if (exFileNotFound != null)
        {                
            if(!string.IsNullOrEmpty(exFileNotFound.FusionLog))
            {
                sb.AppendLine("Fusion Log:");
                sb.AppendLine(exFileNotFound.FusionLog);
            }
        }
        sb.AppendLine();
    }
    string errorMessage = sb.ToString();
    //Display or log the error based on your application.
	Trace.TraceInformation( errorMessage );
}
				}
				catch ( Exception e )
				{
					Trace.TraceInformation( dllName + " からプラグインを生成することに失敗しました。スキップします。" );
					Trace.TraceInformation( e.ToString() );
					Trace.TraceInformation( e.Message );
				}
			}

			// (2) サブフォルダがあれば再帰する
			string[] strDirs = Directory.GetDirectories( strプラグインフォルダパス, "*" );
			foreach( string dir in strDirs )
				this.t指定フォルダ内でのプラグイン検索と生成( dir + "\\", strプラグイン型名 );
		}
		//-----------------
		private void Window_ApplicationActivated( object sender, EventArgs e )
		{
			this.bApplicationActive = true;
		}
		private void Window_ApplicationDeactivated( object sender, EventArgs e )
		{
			this.bApplicationActive = false;
		}
		private void Window_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Menu )
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
			else if ( ( e.KeyCode == Keys.Return ) && e.Alt )
			{
				if ( ConfigIni != null )
				{
					ConfigIni.bウィンドウモード = !ConfigIni.bウィンドウモード;
					this.t全画面_ウィンドウモード切り替え();
				}
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
			else
			{
				for ( int i = 0; i < 0x10; i++ )
				{
                    var captureCode = (SlimDX.DirectInput.Key)ConfigIni.KeyAssign.System[ (int)EKeyConfigPad.Capture ][ i ].コード;

					if( (int) captureCode > 0 &&
						e.KeyCode == DeviceConstantConverter.KeyToKeys( captureCode ) )
					{
						// Debug.WriteLine( "capture: " + string.Format( "{0:2x}", (int) e.KeyCode ) + " " + (int) e.KeyCode );
						string strFullPath =
						   Path.Combine( CDTXMania.strEXEのあるフォルダ, "Capture_img" );
						strFullPath = Path.Combine( strFullPath, DateTime.Now.ToString( "yyyyMMddHHmmss" ) + ".png" );
						SaveResultScreen( strFullPath );
					}
				}
			}
		}
		private void Window_MouseUp( object sender, MouseEventArgs e )
		{
			mb = e.Button;
		}

		private void Window_MouseDoubleClick( object sender, MouseEventArgs e)	// #23510 2010.11.13 yyagi: to go full screen mode
		{
			if ( mb.Equals(MouseButtons.Left) && ConfigIni.bIsAllowedDoubleClickFullscreen )	// #26752 2011.11.27 yyagi
			{
				ConfigIni.bウィンドウモード = false;
				this.t全画面_ウィンドウモード切り替え();
			}
		}
		private void Window_ResizeEnd(object sender, EventArgs e)				// #23510 2010.11.20 yyagi: to get resized window size
		{
			if ( ConfigIni.bウィンドウモード )
			{
				ConfigIni.n初期ウィンドウ開始位置X = base.Window.Location.X;	// #30675 2013.02.04 ikanick add
				ConfigIni.n初期ウィンドウ開始位置Y = base.Window.Location.Y;	//
			}

			ConfigIni.nウインドウwidth = (ConfigIni.bウィンドウモード) ? base.Window.ClientSize.Width : currentClientSize.Width;	// #23510 2010.10.31 yyagi add
			ConfigIni.nウインドウheight = (ConfigIni.bウィンドウモード) ? base.Window.ClientSize.Height : currentClientSize.Height;
		}
		#endregion

		//internal sealed class GCBeep	// GC発生の度にbeep
		//{
		//    ~GCBeep()
		//    {
		//        Console.Beep();
		//        if ( !AppDomain.CurrentDomain.IsFinalizingForUnload()
		//            && !Environment.HasShutdownStarted )
		//        {
		//            new GCBeep();
		//        }
		//    }
		//}
	
		//-----------------

		//Stopwatch sw = new Stopwatch();
		//List<int> swlist1, swlist2, swlist3, swlist4, swlist5;

		#endregion
	}
}
