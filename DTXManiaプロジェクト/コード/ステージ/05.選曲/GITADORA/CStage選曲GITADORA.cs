using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;
using FDK;

using SlimDXKey = SlimDX.DirectInput.Key;

namespace DTXMania
{
    internal class CStage選曲GITADORA : CStage選曲
    {
        public CStage選曲GITADORA()
        {
            base.list子Activities.Add( this.actステータスパネル = new CActSelectステータスパネルGITADORA() );
            base.list子Activities.Add( this.act曲リスト = new CActSelect曲リストGITADORA() );
            base.list子Activities.Add( this.actShowCurrentPosition = new CActSelectShowCurrentPositionGITADORA() );
            base.list子Activities.Add( this.actSelectFO決定 = new CActSelectFO曲決定() );
        }
        
        public override void OnManagedリソースの作成()
        {
			if( !base.b活性化してない )
			{
				this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_background.png" ) );
				this.tx背景_決定後 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_background decide.png" ) );
				this.tx上部パネル = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\5_header panel.png" ) );
				this.tx下部パネル = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\5_footer panel.png" ) );
				this.txFLIP = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenSelect skill number on gauge etc.png" ) );
				base.OnManagedリソースの作成();
			}
        }

        public override void OnManagedリソースの解放()
        {
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx背景 );
				CDTXMania.tテクスチャの解放( ref this.tx背景_決定後 );
				CDTXMania.tテクスチャの解放( ref this.tx上部パネル );
				CDTXMania.tテクスチャの解放( ref this.tx下部パネル );
				CDTXMania.tテクスチャの解放( ref this.txFLIP );
				base.OnManagedリソースの解放();
			}
        }

		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				#region [ 初めての進行描画 ]
				//---------------------
				if( base.b初めての進行描画 )
				{
					this.ct登場時アニメ用共通 = new CCounter( 0, 100, 3, CDTXMania.Timer );
					if( CDTXMania.r直前のステージ == CDTXMania.stage結果 )
					{
						this.actFIfrom結果画面.tフェードイン開始();
						base.eフェーズID = CStage.Eフェーズ.選曲_結果画面からのフェードイン;
					}
					else
					{
						this.actFIFO.tフェードイン開始();
						base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
					}
					this.t選択曲変更通知();
					base.b初めての進行描画 = false;
				}
				//---------------------
				#endregion

				this.ct登場時アニメ用共通.t進行();

				if( this.tx背景 != null )
					this.tx背景.t2D描画( CDTXMania.app.Device, 0, 0 );
                if( this.tx背景_決定後 != null )
                {
                    if( CDTXMania.stage選曲GITADORA.ct決定演出待機.n現在の値 < 1000 )
                    {
                        this.tx背景_決定後.n透明度 = (int)( 255 * ( ( CDTXMania.stage選曲GITADORA.ct決定演出待機.n現在の値 ) / 500.0 ) );
                    }
                    else if( CDTXMania.stage選曲GITADORA.ct決定演出待機.n現在の値 > 1000 )
                    {
                        this.tx背景_決定後.n透明度 = 255;
                    }
                    this.tx背景_決定後.t2D描画( CDTXMania.app.Device, 0, 0 );
                }
                
			//	this.bIsEnumeratingSongs = !this.actPreimageパネル.bIsPlayingPremovie;				// #27060 2011.3.2 yyagi: #PREMOVIE再生中は曲検索を中断する

				this.act曲リスト.On進行描画();
				int y = 0;
				if( this.ct登場時アニメ用共通.b進行中 && this.tx上部パネル != null )
				{
					double db登場割合 = ( (double) this.ct登場時アニメ用共通.n現在の値 ) / 100.0;	// 100が最終値
					double dbY表示割合 = Math.Sin( Math.PI / 2 * db登場割合 );
					y = ( (int) ( this.tx上部パネル.sz画像サイズ.Height * dbY表示割合 ) ) - this.tx上部パネル.sz画像サイズ.Height;
				}
				#region [ 上部パネル描画 ]
				if( this.tx上部パネル != null )
                {
                    this.tx上部パネル.t2D描画( CDTXMania.app.Device, 0, 0 );
                }
				#endregion
				//this.actInformation.On進行描画();
				#region [ 下部パネル描画 ]
				if ( this.tx下部パネル != null )
					this.tx下部パネル.t2D描画( CDTXMania.app.Device, 0, SampleFramework.GameWindowSize.Height - this.tx下部パネル.sz画像サイズ.Height );
				#endregion
				this.actステータスパネル.On進行描画();
        //        if( CDTXMania.ConfigIni.bDrums有効 )
				    //this.act演奏履歴パネル.On進行描画();
				//this.actShowCurrentPosition.On進行描画();								// #27648 2011.3.28 yyagi
                this.actPresound.On進行描画();
				#region [ フェーズ処理 ]
				switch ( base.eフェーズID )
				{
					case CStage.Eフェーズ.共通_フェードイン:
						if( this.actFIFO.On進行描画() != 0 )
						{
							base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
						}
						break;

					case CStage.Eフェーズ.共通_フェードアウト:
						if( this.actFIFO.On進行描画() == 0 )
						{
							break;
						}
						return (int) this.eフェードアウト完了時の戻り値;

					case CStage.Eフェーズ.選曲_結果画面からのフェードイン:
						if( this.actFIfrom結果画面.On進行描画() != 0 )
						{
							base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
						}
						break;

					case CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト:
#if animetest
						if( this.actFOtoNowLoading.On進行描画() == 0 )
						{
							break;
						}
#endif
						return (int) this.eフェードアウト完了時の戻り値;
				}
				#endregion

				if( !this.bBGM再生済み && ( base.eフェーズID == CStage.Eフェーズ.共通_通常状態 ) )
				{
					CDTXMania.Skin.bgm選曲画面.n音量_次に鳴るサウンド = 100;
					CDTXMania.Skin.bgm選曲画面.t再生する();
					this.bBGM再生済み = true;
				}


//Debug.WriteLine( "パンくず=" + this.r現在選択中の曲.strBreadcrumbs );


				// キー入力
				if( base.eフェーズID == CStage.Eフェーズ.共通_通常状態 
					&& CDTXMania.act現在入力を占有中のプラグイン == null )
				{
					#region [ 簡易CONFIGでMore、またはShift+F1: 詳細CONFIG呼び出し ]
					if (  actQuickConfig.bGotoDetailConfig )
					{	// 詳細CONFIG呼び出し
						actQuickConfig.tDeativatePopupMenu();
						this.actPresound.tサウンド停止();
						this.eフェードアウト完了時の戻り値 = E戻り値.コンフィグ呼び出し;	// #24525 2011.3.16 yyagi: [SHIFT]-[F1]でCONFIG呼び出し
						this.actFIFO.tフェードアウト開始();
						base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
						CDTXMania.Skin.sound取消音.t再生する();
						return 0;
					}
					#endregion
					if ( !this.actSortSongs.bIsActivePopupMenu && !this.actQuickConfig.bIsActivePopupMenu )
					{
						#region [ ESC ]
						if ( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.Escape ) || ( ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.LC ) || CDTXMania.Pad.b押されたGB( Eパッド.Cancel ) || CDTXMania.Pad.b押されたGB( Eパッド.Pick ) ) && ( ( this.act曲リスト.r現在選択中の曲 != null ) && ( this.act曲リスト.r現在選択中の曲.r親ノード == null ) ) ) )
						{	// [ESC]
							CDTXMania.Skin.sound取消音.t再生する();
							this.eフェードアウト完了時の戻り値 = E戻り値.タイトルに戻る;
							this.actFIFO.tフェードアウト開始();
							base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
							return 0;
						}
						#endregion
						#region [ Shift-F1: CONFIG画面 ]
						if ( ( CDTXMania.Input管理.Keyboard.bキーが押されている( (int) SlimDXKey.RightShift ) || CDTXMania.Input管理.Keyboard.bキーが押されている( (int) SlimDXKey.LeftShift ) ) &&
							CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F1 ) )
						{	// [SHIFT] + [F1] CONFIG
							this.actPresound.tサウンド停止();
							this.eフェードアウト完了時の戻り値 = E戻り値.コンフィグ呼び出し;	// #24525 2011.3.16 yyagi: [SHIFT]-[F1]でCONFIG呼び出し
							this.actFIFO.tフェードアウト開始();
							base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
							CDTXMania.Skin.sound取消音.t再生する();
							return 0;
						}
						#endregion
						#region [ Shift-F2: 未使用 ]
						// #24525 2011.3.16 yyagi: [SHIFT]+[F2]は廃止(将来発生するかもしれない別用途のためにキープ)
						/*
											if ( ( CDTXMania.Input管理.Keyboard.bキーが押されている( (int)SlimDXKey.RightShift ) || CDTXMania.Input管理.Keyboard.bキーが押されている( (int)SlimDXKey.LeftShift ) ) &&
												CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F2 ) )
											{	// [SHIFT] + [F2] CONFIGURATION
												this.actPresound.tサウンド停止();
												this.eフェードアウト完了時の戻り値 = E戻り値.コンフィグ呼び出し;
												this.actFIFO.tフェードアウト開始();
												base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
												CDTXMania.Skin.sound取消音.t再生する();
												return 0;
											}
						*/
						#endregion
						if ( this.act曲リスト.r現在選択中の曲 != null )
						{
							#region [ Right ]
                            //2016.02.20 kairera0467 XG風の場合は使わない。
							if ( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.RightArrow ) && !CDTXMania.bXGRelease )
							{
								if ( this.act曲リスト.r現在選択中の曲 != null )
								{
									switch ( this.act曲リスト.r現在選択中の曲.eノード種別 )
									{
										case C曲リストノード.Eノード種別.BOX:
											{
												CDTXMania.Skin.sound決定音.t再生する();
												bool bNeedChangeSkin = this.act曲リスト.tBOXに入る();
												if ( bNeedChangeSkin )
												{
													this.eフェードアウト完了時の戻り値 = E戻り値.スキン変更;
													base.eフェーズID = Eフェーズ.選曲_NowLoading画面へのフェードアウト;
												}
											}
											break;
									}
								}
							}
							#endregion
							#region [ Decide ]
							if (( CDTXMania.Pad.b押されたDGB( Eパッド.Decide ) || CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.RD ) ) ||
								( ( CDTXMania.ConfigIni.bEnterがキー割り当てのどこにも使用されていない && CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.Return ) ) ) )
							{
								if ( this.act曲リスト.r現在選択中の曲 != null )
								{
									switch ( this.act曲リスト.r現在選択中の曲.eノード種別 )
									{
										case C曲リストノード.Eノード種別.SCORE:
                                            CDTXMania.Skin.sound曲決定音.t再生する();
											this.t曲を選択する();
											break;

										case C曲リストノード.Eノード種別.SCORE_MIDI:
											this.t曲を選択する();
											break;

										case C曲リストノード.Eノード種別.BOX:
											{
                                                CDTXMania.Skin.sound決定音.t再生する();
												bool bNeedChangeSkin = this.act曲リスト.tBOXに入る();
												if ( bNeedChangeSkin )
												{
													this.eフェードアウト完了時の戻り値 = E戻り値.スキン変更; 
													base.eフェーズID = Eフェーズ.選曲_NowLoading画面へのフェードアウト;
												}
											}
											break;

										case C曲リストノード.Eノード種別.BACKBOX:
											{
                                                CDTXMania.Skin.sound決定音.t再生する();
												bool bNeedChangeSkin = this.act曲リスト.tBOXを出る();
												if ( bNeedChangeSkin )
												{
													this.eフェードアウト完了時の戻り値 = E戻り値.スキン変更; 
													base.eフェーズID = Eフェーズ.選曲_NowLoading画面へのフェードアウト;
												}
											}
											break;

										case C曲リストノード.Eノード種別.RANDOM:
                                            CDTXMania.Skin.sound曲決定音.t再生する();
											this.t曲をランダム選択する();
											break;
									}
								}
							}
							#endregion
							#region [ Up ]
							this.ctキー反復用.Up.tキー反復( CDTXMania.Input管理.Keyboard.bキーが押されている( (int) SlimDXKey.UpArrow ), new CCounter.DGキー処理( this.tカーソルを上へ移動する ) );
							this.ctキー反復用.R.tキー反復( CDTXMania.Pad.b押されているGB( Eパッド.R ), new CCounter.DGキー処理( this.tカーソルを上へ移動する ) );
							if ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.SD ) )
							{
								this.tカーソルを上へ移動する();
							}
							#endregion
							#region [ Down ]
							this.ctキー反復用.Down.tキー反復( CDTXMania.Input管理.Keyboard.bキーが押されている( (int) SlimDXKey.DownArrow ), new CCounter.DGキー処理( this.tカーソルを下へ移動する ) );
							this.ctキー反復用.B.tキー反復( CDTXMania.Pad.b押されているGB( Eパッド.G ), new CCounter.DGキー処理( this.tカーソルを下へ移動する ) );
							if ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.FT ) )
							{
								this.tカーソルを下へ移動する();
							}
							#endregion
							#region [ Upstairs / Left ]
							if ( ( ( this.act曲リスト.r現在選択中の曲 != null ) && ( this.act曲リスト.r現在選択中の曲.r親ノード != null ) ) &&
								( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.LC ) ||
								  CDTXMania.Pad.b押されたGB( Eパッド.Pick ) || CDTXMania.Pad.b押されたGB( Eパッド.Cancel ) ) )
							{
								this.actPresound.tサウンド停止();
								CDTXMania.Skin.sound取消音.t再生する();
								this.act曲リスト.tBOXを出る();
								this.t選択曲変更通知();
							}
							#endregion
							#region [ BDx2: 簡易CONFIG ]
							if ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.BD ) )
							{	// [BD]x2 スクロール速度変更
								CommandHistory.Add( E楽器パート.DRUMS, EパッドFlag.BD );
								EパッドFlag[] comChangeScrollSpeed = new EパッドFlag[] { EパッドFlag.BD, EパッドFlag.BD };
								if ( CommandHistory.CheckCommand( comChangeScrollSpeed, E楽器パート.DRUMS ) )
								{
									CDTXMania.Skin.sound変更音.t再生する();
									this.actQuickConfig.tActivatePopupMenu( E楽器パート.DRUMS );
								}
							}
							#endregion
							#region [ HHx2: 難易度変更 ]
							if ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.HH ) || CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.HHO ) )
							{	// [HH]x2 難易度変更
								CommandHistory.Add( E楽器パート.DRUMS, EパッドFlag.HH );
								EパッドFlag[] comChangeDifficulty = new EパッドFlag[] { EパッドFlag.HH, EパッドFlag.HH };
								if ( CommandHistory.CheckCommand( comChangeDifficulty, E楽器パート.DRUMS ) )
								{
									this.act曲リスト.t難易度レベルをひとつ進める();
								}
							}
							#endregion
							#region [ Bx2 Guitar: 難易度変更 ]
							if( CDTXMania.Pad.b押された( E楽器パート.GUITAR, Eパッド.B ) )
							{	// [B]x2 ギター難易度変更
								CommandHistory.Add( E楽器パート.GUITAR, EパッドFlag.B );
								EパッドFlag[] comChangeDifficultyG = new EパッドFlag[] { EパッドFlag.B, EパッドFlag.B };
								if ( CommandHistory.CheckCommand( comChangeDifficultyG, E楽器パート.GUITAR ) )
								{
									Debug.WriteLine( "ギター難易度変更" );
									this.act曲リスト.t難易度レベルをひとつ進める();
									//CDTXMania.Skin.sound変更音.t再生する();
								}
							}
							#endregion
							#region [ Bx2 Bass: 難易度変更 ]
							if( CDTXMania.Pad.b押された( E楽器パート.BASS, Eパッド.B ) )
							{	// [B]x2 ベース難易度変更
								CommandHistory.Add( E楽器パート.BASS, EパッドFlag.B );
								EパッドFlag[] comChangeDifficultyB = new EパッドFlag[] { EパッドFlag.B, EパッドFlag.B };
								if ( CommandHistory.CheckCommand( comChangeDifficultyB, E楽器パート.BASS ) )
								{
									Debug.WriteLine( "ベース難易度変更" );
									this.act曲リスト.t難易度レベルをひとつ進める();
								}
							}
							#endregion
							#region [ Yx2 Guitar: ギターとベースを入れ替え ]
							if ( CDTXMania.Pad.b押された( E楽器パート.GUITAR, Eパッド.Y ) )
							{	// Yx2 ギターとベースを入れ替え
								CommandHistory.Add( E楽器パート.GUITAR, EパッドFlag.Y );
								EパッドFlag[] comSwapGtBs1 = new EパッドFlag[] { EパッドFlag.Y, EパッドFlag.Y };
								if ( CommandHistory.CheckCommand( comSwapGtBs1, E楽器パート.GUITAR ) )
								{
									Debug.WriteLine( "ギターとベースの入れ替え1" );
									CDTXMania.Skin.sound変更音.t再生する();
									CDTXMania.ConfigIni.bIsSwappedGuitarBass = !CDTXMania.ConfigIni.bIsSwappedGuitarBass;
								}
							}
							#endregion
							#region [ Yx2 Bass: ギターとベースを入れ替え ]
                            if ( CDTXMania.Pad.b押された( E楽器パート.BASS, Eパッド.Y ) )
							{
								CommandHistory.Add( E楽器パート.BASS, EパッドFlag.Y );
								// Yx2 ギターとベースを入れ替え
								EパッドFlag[] comSwapGtBs1 = new EパッドFlag[] { EパッドFlag.Y, EパッドFlag.Y };
								if ( CommandHistory.CheckCommand( comSwapGtBs1, E楽器パート.BASS ) )
								{
									Debug.WriteLine( "ギターとベースの入れ替え2" );
									CDTXMania.Skin.sound変更音.t再生する();
									CDTXMania.ConfigIni.bIsSwappedGuitarBass = !CDTXMania.ConfigIni.bIsSwappedGuitarBass;
								}
							}
							#endregion
                            #region [ Px2 Guitar: 簡易CONFIG ]
							if ( CDTXMania.Pad.b押された( E楽器パート.GUITAR, Eパッド.P ) )
							{
								CommandHistory.Add( E楽器パート.GUITAR, EパッドFlag.P );
								EパッドFlag[] comChangeScrollSpeed = new EパッドFlag[] { EパッドFlag.P, EパッドFlag.P };
								if ( CommandHistory.CheckCommand( comChangeScrollSpeed, E楽器パート.GUITAR ) )
								{
									CDTXMania.Skin.sound変更音.t再生する();
									this.actQuickConfig.tActivatePopupMenu( E楽器パート.GUITAR );
								}
							}
							#endregion
                            #region [ Px2 Bass: 簡易CONFIG ]
							if ( CDTXMania.Pad.b押された( E楽器パート.BASS, Eパッド.P ) )
							{
								CommandHistory.Add( E楽器パート.BASS, EパッドFlag.P );
								EパッドFlag[] comChangeScrollSpeed = new EパッドFlag[] { EパッドFlag.P, EパッドFlag.P };
								if ( CommandHistory.CheckCommand( comChangeScrollSpeed, E楽器パート.BASS ) )
								{
									CDTXMania.Skin.sound変更音.t再生する();
									this.actQuickConfig.tActivatePopupMenu( E楽器パート.BASS );
								}
							}
							#endregion
							#region [ Y P Guitar: ソート画面 ]
							if ( CDTXMania.Pad.b押されている( E楽器パート.GUITAR, Eパッド.Y ) && CDTXMania.Pad.b押された( E楽器パート.GUITAR, Eパッド.P ) )
							{
                                    CDTXMania.Skin.sound変更音.t再生する();
                                    this.actSortSongs.tActivatePopupMenu( E楽器パート.GUITAR, ref this.act曲リスト );
 							}
							#endregion
							#region [ Y P Bass: ソート画面 ]
							if ( CDTXMania.Pad.b押されている( E楽器パート.BASS, Eパッド.Y ) && CDTXMania.Pad.b押された( E楽器パート.BASS, Eパッド.P ) )
							{
									CDTXMania.Skin.sound変更音.t再生する();
									this.actSortSongs.tActivatePopupMenu( E楽器パート.BASS, ref this.act曲リスト );
							}
							#endregion
							#region [ HTx2 Drums: ソート画面 ]
							if ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.HT ) )
							{	// [HT]x2 ソート画面        2013.12.31.kairera0467
    							CommandHistory.Add( E楽器パート.DRUMS, EパッドFlag.HT );
								EパッドFlag[] comSort = new EパッドFlag[] { EパッドFlag.HT, EパッドFlag.HT };
								if ( CommandHistory.CheckCommand( comSort, E楽器パート.DRUMS ) )
								{
									CDTXMania.Skin.sound変更音.t再生する();
									this.actSortSongs.tActivatePopupMenu( E楽器パート.DRUMS, ref this.act曲リスト );
								}
							}
							#endregion
						}
					}

                    this.actSelectFO決定.On進行描画();
					#region [ ESC ]
					if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F7 ) )
					{
                        this.actSelectFO決定.tフェードアウト開始();
					}
					#endregion
                    
                    {
                        //Debug
                        //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F5 ) )
                        //{
                        //        Debug.WriteLine( "ギターとベースの入れ替え1" );
                        //        CDTXMania.Skin.sound変更音.t再生する();
                        //        CDTXMania.ConfigIni.bIsSwappedGuitarBass = !CDTXMania.ConfigIni.bIsSwappedGuitarBass;
                        //}
                        //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F8 ) )
                        //{
                        //        Debug.WriteLine( "[Test]シャッター画像csvの読み込み" );
                        //        CDTXMania.Skin.sound変更音.t再生する();
                        //        CDTXMania.Skin.CreateShutterList();
                        //}
                        //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F9 ) )
                        //{
                        //        Debug.WriteLine( "[Test]シャッター画像のリスト生成＆呼び出し" );
                        //        CDTXMania.Skin.sound変更音.t再生する();
                        //        CDTXMania.Skin.arGetShutterName();
                        //}
                    }
					this.actSortSongs.t進行描画();
					this.actQuickConfig.t進行描画();
				}
			}
			return 0;
		}
		private CTextureAf tx下部パネル;
		private CTextureAf tx上部パネル;
		private CTexture tx背景;
        private CTexture tx背景_決定後;
		private CTexture txFLIP;
        protected CActSelectFO曲決定 actSelectFO決定;
    }
}
