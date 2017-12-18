using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using SlimDX;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using FDK;

namespace DTXMania
{
	internal class CStage曲読み込み : CStage
	{
		// コンストラクタ

		public CStage曲読み込み()
		{
			base.eステージID = CStage.Eステージ.曲読み込み;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.b活性化してない = true;
			base.list子Activities.Add( this.actFI = new CActFIFOBlack() );	// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
			base.list子Activities.Add( this.actFO = new CActFIFOBlack() );

            if( !CDTXMania.bXGRelease ) {
                this.actLoadMain = new CAct曲読み込みメイン画面GD();
            }
		}


		// CStage 実装

		public override void On活性化()
		{
			Trace.TraceInformation( "曲読み込みステージを活性化します。" );
			Trace.Indent();
			try
			{
				this.str曲タイトル = "";
				this.strSTAGEFILE = "";
				this.b音符を表示する = false;
				this.n音符の表示位置X = 0x308;
				this.ftタイトル表示用フォント = new Font( "MS PGothic", fFontSizeTitle * Scale.Y, GraphicsUnit.Pixel );
				this.nBGM再生開始時刻 = -1;
				this.nBGMの総再生時間ms = 0;
				if( this.sd読み込み音 != null )
				{
					CDTXMania.Sound管理.tサウンドを破棄する( this.sd読み込み音 );
					this.sd読み込み音 = null;
				}

                string strDTXファイルパス = ( CDTXMania.bコンパクトモード ) ? CDTXMania.strコンパクトモードファイル : "";

                if( CDTXMania.bXGRelease )
                {
                    strDTXファイルパス = CDTXMania.stage選曲XG.r確定されたスコア.ファイル情報.ファイルの絶対パス;
                }
                else
                {
                    strDTXファイルパス = CDTXMania.stage選曲GITADORA.r確定されたスコア.ファイル情報.ファイルの絶対パス;
                }
				
				CDTX cdtx = new CDTX( strDTXファイルパス, true );
				this.str曲タイトル = cdtx.TITLE;
				if( ( ( cdtx.STAGEFILE != null ) && ( cdtx.STAGEFILE.Length > 0 ) ) && ( File.Exists( cdtx.strフォルダ名 + cdtx.STAGEFILE ) && !CDTXMania.ConfigIni.bストイックモード ) )
				{
					this.strSTAGEFILE = cdtx.strフォルダ名 + cdtx.PATH + cdtx.STAGEFILE;
					this.b音符を表示する = false;
				}
				else
				{
                    if( !CDTXMania.bXGRelease ) {
                        if( CDTXMania.ConfigIni.bDrums有効 ) {
                            this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_background Drums.png" ) );
                        } else {
                            this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_background Guitar.png" ) );
                        }
                    }
                    else
                    {
					    this.strSTAGEFILE = CSkin.Path( @"Graphics\\6_background.png" );
                    }


					this.b音符を表示する = true;
				}
				if( ( ( cdtx.SOUND_NOWLOADING != null ) && ( cdtx.SOUND_NOWLOADING.Length > 0 ) ) && File.Exists( cdtx.strフォルダ名 + cdtx.SOUND_NOWLOADING ) )
				{
					string strNowLoadingサウンドファイルパス = cdtx.strフォルダ名 + cdtx.PATH + cdtx.SOUND_NOWLOADING;
					try
					{
						this.sd読み込み音 = CDTXMania.Sound管理.tサウンドを生成する( strNowLoadingサウンドファイルパス );
					}
					catch
					{
						Trace.TraceError( "#SOUND_NOWLOADING に指定されたサウンドファイルの読み込みに失敗しました。({0})", strNowLoadingサウンドファイルパス );
					}
				}

                // #35411 2015.08.19 chnmr0 add
                // Read ghost data by config
                // It does not exist a ghost file for 'perfect' actually
                string [] inst = {"dr", "gt", "bs"};
				if( CDTXMania.ConfigIni.bIsSwappedGuitarBass )
				{
					inst[1] = "bs";
					inst[2] = "gt";
				}
                
                for(int instIndex = 0; instIndex < inst.Length; ++instIndex)
                {
                    bool readAutoGhostCond = false;
                    readAutoGhostCond |= instIndex == 0 ? CDTXMania.ConfigIni.bドラムが全部オートプレイである : false;
                    readAutoGhostCond |= instIndex == 1 ? CDTXMania.ConfigIni.bギターが全部オートプレイである : false;
                    readAutoGhostCond |= instIndex == 2 ? CDTXMania.ConfigIni.bベースが全部オートプレイである : false;

                    CDTXMania.listTargetGhsotLag[instIndex] = null;
                    CDTXMania.listAutoGhostLag[instIndex] = null;
                    CDTXMania.listTargetGhostScoreData[instIndex] = null;
                    this.nCurrentInst = instIndex;

                    if ( readAutoGhostCond )
                    {
                        string[] prefix = { "perfect", "lastplay", "hiskill", "hiscore", "online" };
                        int indPrefix = (int)CDTXMania.ConfigIni.eAutoGhost[instIndex];
                        string filename = cdtx.strフォルダ名 + "\\" + cdtx.strファイル名 + "." + prefix[indPrefix] + "." + inst[instIndex] + ".ghost";
                        if ( File.Exists(filename) )
                        {
                            CDTXMania.listAutoGhostLag[instIndex] = new List<int>();
                            CDTXMania.listTargetGhostScoreData[ instIndex ] = new CScoreIni.C演奏記録();
                            ReadGhost(filename, CDTXMania.listAutoGhostLag[instIndex]);
                        }
                    }

                    if( CDTXMania.ConfigIni.eTargetGhost[instIndex] != ETargetGhostData.NONE )
                    {
                        string[] prefix = { "none", "perfect", "lastplay", "hiskill", "hiscore", "online" };
                        int indPrefix = (int)CDTXMania.ConfigIni.eTargetGhost[instIndex];
                        string filename = cdtx.strフォルダ名 + "\\" + cdtx.strファイル名 + "." + prefix[indPrefix] + "." + inst[instIndex] + ".ghost";
                        if (File.Exists(filename))
                        {
                            CDTXMania.listTargetGhsotLag[instIndex] = new List<int>();
                            CDTXMania.listTargetGhostScoreData[ instIndex ] = new CScoreIni.C演奏記録();
                            this.stGhostLag[instIndex] = new List<STGhostLag>();
                            ReadGhost(filename, CDTXMania.listTargetGhsotLag[instIndex]);
                        }
                        else if( CDTXMania.ConfigIni.eTargetGhost[instIndex] == ETargetGhostData.PERFECT )
                        {
                            // All perfect
                            CDTXMania.listTargetGhsotLag[instIndex] = new List<int>();
                        }
                    }
                }

				cdtx.On非活性化();
				base.On活性化();
			}
			finally
			{
				Trace.TraceInformation( "曲読み込みステージの活性化を完了しました。" );
				Trace.Unindent();
			}
		}

        private void ReadGhost(string filename, List<int> list) // #35411 2015.08.19 chnmr0 add
        {
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        try
                        {
                            int cnt = br.ReadInt32();
                            for (int i = 0; i < cnt; ++i)
                            {
                                short lag = br.ReadInt16();
                                list.Add(lag);
                            }
                        }
                        catch( EndOfStreamException )
                        {
                            Trace.TraceInformation("ゴーストデータは正しく読み込まれませんでした。");
                            list.Clear();
                        }
                    }
                }
            }

            //if( File.Exists( filename + ".score" ) )
            //{
            //    using( FileStream fs = new FileStream( filename + ".score", FileMode.Open, FileAccess.Read ) )
            //    {
            //        using( StreamReader sr = new StreamReader( fs ) )
            //        {
            //            try
            //            {
            //                string strScoreDataFile = sr.ReadToEnd();

            //                strScoreDataFile = strScoreDataFile.Replace( Environment.NewLine, "\n" );
            //                string[] delimiter = { "\n" };
            //                string[] strSingleLine = strScoreDataFile.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );

            //                for( int i = 0; i < strSingleLine.Length; i++ )
            //                {
            //                    string[] strA = strSingleLine[ i ].Split( '=' );
            //                    if (strA.Length != 2)
            //                        continue;

            //                    switch( strA[ 0 ] )
            //                    {
            //                        case "Score":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].nスコア = Convert.ToInt32( strA[ 1 ] );
            //                            continue;
            //                        case "PlaySkill":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].db演奏型スキル値 = Convert.ToDouble( strA[ 1 ] );
            //                            continue;
            //                        case "Skill":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].dbゲーム型スキル値 = Convert.ToDouble( strA[ 1 ] );
            //                            continue;
            //                        case "Perfect":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].nPerfect数_Auto含まない = Convert.ToInt32( strA[ 1 ] );
            //                            continue;
            //                        case "Great":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].nGreat数_Auto含まない = Convert.ToInt32( strA[ 1 ] );
            //                            continue;
            //                        case "Good":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].nGood数_Auto含まない = Convert.ToInt32( strA[ 1 ] );
            //                            continue;
            //                        case "Poor":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].nPoor数_Auto含まない = Convert.ToInt32( strA[ 1 ] );
            //                            continue;
            //                        case "Miss":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].nMiss数_Auto含まない = Convert.ToInt32( strA[ 1 ] );
            //                            continue;
            //                        case "MaxCombo":
            //                            CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ].n最大コンボ数 = Convert.ToInt32( strA[ 1 ] );
            //                            continue;
            //                        default:
            //                            continue;
            //                    }
            //                }
            //            }
            //            catch( NullReferenceException )
            //            {
            //                Trace.TraceInformation("ゴーストデータの記録が正しく読み込まれませんでした。");
            //            }
            //            catch( EndOfStreamException )
            //            {
            //                Trace.TraceInformation("ゴーストデータの記録が正しく読み込まれませんでした。");
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    CDTXMania.listTargetGhostScoreData[ (int)this.nCurrentInst ] = null;
            //}
        }

		public override void On非活性化()
		{
			Trace.TraceInformation( "曲読み込みステージを非活性化します。" );
			Trace.Indent();
			try
			{
				if( this.ftタイトル表示用フォント != null )
				{
					this.ftタイトル表示用フォント.Dispose();
					this.ftタイトル表示用フォント = null;
				}
				base.On非活性化();
			}
			finally
			{
				Trace.TraceInformation( "曲読み込みステージの非活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.tx背景 = CDTXMania.tテクスチャの生成( this.strSTAGEFILE, false );
                base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx背景 );

                if( !CDTXMania.bXGRelease )
                {
                    this.actLoadMain.OnManagedリソースの解放();
                }

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			string str;

			if( base.b活性化してない )
				return 0;

			#region [ 初めての進行描画 ]
			//-----------------------------
			if( base.b初めての進行描画 )
			{
				if( this.sd読み込み音 != null )
				{
					if( CDTXMania.Skin.sound曲読込開始音.b排他 && ( CSkin.Cシステムサウンド.r最後に再生した排他システムサウンド != null ) )
					{
						CSkin.Cシステムサウンド.r最後に再生した排他システムサウンド.t停止する();
					}
					this.sd読み込み音.t再生を開始する();
					this.nBGM再生開始時刻 = CSound管理.rc演奏用タイマ.n現在時刻;
					this.nBGMの総再生時間ms = this.sd読み込み音.n総演奏時間ms;
				}
				else
				{
					CDTXMania.Skin.sound曲読込開始音.t再生する();
					this.nBGM再生開始時刻 = CSound管理.rc演奏用タイマ.n現在時刻;
					this.nBGMの総再生時間ms = CDTXMania.Skin.sound曲読込開始音.n長さ_現在のサウンド;
				}
//				this.actFI.tフェードイン開始();							// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
				base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
				base.b初めての進行描画 = false;

				nWAVcount = 1;
				bitmapFilename = new Bitmap( SampleFramework.GameWindowSize.Width, (int)(fFontSizeFilename * Scale.X) );
				graphicsFilename = Graphics.FromImage( bitmapFilename );
				graphicsFilename.TextRenderingHint = TextRenderingHint.AntiAlias;
				ftFilename = new Font( "MS PGothic", fFontSizeFilename * Scale.X, FontStyle.Bold, GraphicsUnit.Pixel );
			}
			//-----------------------------
			#endregion

			#region [ ESC押下時は選曲画面に戻る ]
			if ( tキー入力() )
			{
				if ( this.sd読み込み音 != null )
				{
					this.sd読み込み音.tサウンドを停止する();
					this.sd読み込み音.t解放する();
				}
				return (int) E曲読込画面の戻り値.読込中止;
			}
			#endregion

			#region [ 背景表示 ]
			//-----------------------------
			if( this.tx背景 != null )
				this.tx背景.t2D描画( CDTXMania.app.Device, 0, 0 );
			//-----------------------------
			#endregion

            if( !CDTXMania.bXGRelease ) {
                if( !CDTXMania.bコンパクトモード ) { 
                    this.actLoadMain.t指定されたパスからジャケット画像を生成する( CDTXMania.stage選曲GITADORA.r確定されたスコア.ファイル情報.フォルダの絶対パス + CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.Preimage );
                    this.actLoadMain.t難易度パネルの描画( CDTXMania.stage選曲GITADORA.n確定された曲の難易度 );
                    this.actLoadMain.t曲名アーティスト名テクスチャの生成( CDTXMania.stage選曲GITADORA.r確定された曲.strタイトル, CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.アーティスト名 );
                }
                if( CDTXMania.ConfigIni.bDrums有効 ) {
                    this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_background Drums.png" ) );
                } else {
                    this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\6_background Guitar.png" ) );
                }

                this.actLoadMain.On進行描画();
            }

			switch( base.eフェーズID )
			{
				case CStage.Eフェーズ.共通_フェードイン:
					//if( this.actFI.On進行描画() != 0 )					// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
																		// 必ず一度「CStaeg.Eフェーズ.共通_フェードイン」フェーズを経由させること。
																		// さもないと、曲読み込みが完了するまで、曲読み込み画面が描画されない。
						base.eフェーズID = CStage.Eフェーズ.NOWLOADING_DTXファイルを読み込む;
					return (int) E曲読込画面の戻り値.継続;

				case CStage.Eフェーズ.NOWLOADING_DTXファイルを読み込む:
					{
						timeBeginLoad = DateTime.Now;
						TimeSpan span;
						str = null;
                        if( CDTXMania.bXGRelease )
                        {
						    if( !CDTXMania.bコンパクトモード )
							    str = CDTXMania.stage選曲XG.r確定されたスコア.ファイル情報.ファイルの絶対パス;
						    else
							    str = CDTXMania.strコンパクトモードファイル;
                        }
                        else
                        {
						    if( !CDTXMania.bコンパクトモード )
							    str = CDTXMania.stage選曲GITADORA.r確定されたスコア.ファイル情報.ファイルの絶対パス;
						    else
							    str = CDTXMania.strコンパクトモードファイル;
                        }

						CScoreIni ini = new CScoreIni( str + ".score.ini" );
						ini.t全演奏記録セクションの整合性をチェックし不整合があればリセットする();

						if( ( CDTXMania.DTX != null ) && CDTXMania.DTX.b活性化してる )
							CDTXMania.DTX.On非活性化();

						CDTXMania.DTX = new CDTX( str, false, ( (double) CDTXMania.ConfigIni.n演奏速度 ) / 20.0, ini.stファイル.BGMAdjust );
						Trace.TraceInformation( "----曲情報-----------------" );
						Trace.TraceInformation( "TITLE: {0}", CDTXMania.DTX.TITLE );
						Trace.TraceInformation( "FILE: {0}",  CDTXMania.DTX.strファイル名の絶対パス );
						Trace.TraceInformation( "---------------------------" );
                        
                        if( !CDTXMania.bコンパクトモード )
                        {
                            if( CDTXMania.ConfigIni.bSkillModeを自動切替えする && CDTXMania.ConfigIni.bDrums有効 )
                                this.tSkillModeを譜面に応じて切り替える( CDTXMania.DTX );
                        }

                        // #35411 2015.08.19 chnmr0 add ゴースト機能のためList chip 読み込み後楽器パート出現順インデックスを割り振る
                        try
                        {
                            int[] curCount = new int[(int)E楽器パート.UNKNOWN];
                            for (int i = 0; i < curCount.Length; ++i)
                            {
                                curCount[i] = 0;
                            }
                            foreach (CDTX.CChip chip in CDTXMania.DTX.listChip)
                            {
                                if (chip.e楽器パート != E楽器パート.UNKNOWN)
                                {
                                    chip.n楽器パートでの出現順 = curCount[(int)chip.e楽器パート]++;
                                    if( CDTXMania.listTargetGhsotLag[ (int)chip.e楽器パート ] != null )
                                    {
                                        var lag = new STGhostLag();
                                        lag.index = chip.n楽器パートでの出現順;
                                        lag.nJudgeTime = chip.n発声時刻ms + CDTXMania.listTargetGhsotLag[ (int)chip.e楽器パート ][ chip.n楽器パートでの出現順 ];
                                        lag.nLagTime = CDTXMania.listTargetGhsotLag[ (int)chip.e楽器パート ][ chip.n楽器パートでの出現順 ];

                                        this.stGhostLag[ (int)chip.e楽器パート ].Add( lag );
                                    }
                                }
                            }
                        
                            //演奏記録をゴーストから逆生成
                            for( int i = 0; i < 3; i++ )
                            {
                                int nNowCombo = 0;
                                int nMaxCombo = 0;
                                CDTXMania.listTargetGhostScoreData[ i ] = new CScoreIni.C演奏記録();
                                if( this.stGhostLag[ i ] == null )
                                    continue;
                                for( int j = 0; j < this.stGhostLag[ i ].Count; j++ )
                                {
                                    int ghostLag = 128;
                                    ghostLag = this.stGhostLag[ i ][ j ].nLagTime;
                                    // 上位８ビットが１ならコンボが途切れている（ギターBAD空打ちでコンボ数を再現するための措置）
                                    if (ghostLag > 255)
                                    {
                                        nNowCombo = 0;
                                    }
                                    ghostLag = (ghostLag & 255) - 128;

                                    if( ghostLag <= 127 )
                                    {
                                        E判定 eJudge = this.e指定時刻からChipのJUDGEを返す(ghostLag, 0);

                                        switch( eJudge )
                                        {
                                            case E判定.Perfect:
                                            case E判定.XPerfect:
                                                CDTXMania.listTargetGhostScoreData[ i ].nPerfect数++;
                                                break;
                                            case E判定.Great:
                                                CDTXMania.listTargetGhostScoreData[ i ].nGreat数++;
                                                break;
                                            case E判定.Good:
                                                CDTXMania.listTargetGhostScoreData[ i ].nGood数++;
                                                break;
                                            case E判定.Poor:
                                                CDTXMania.listTargetGhostScoreData[ i ].nPoor数++;
                                                break;
                                            case E判定.Miss:
                                            case E判定.Bad:
                                                CDTXMania.listTargetGhostScoreData[ i ].nMiss数++;
                                                break;
                                        }
                                        switch( eJudge )
                                        {
                                            case E判定.Perfect:
                                            case E判定.Great:
                                            case E判定.Good:
                                            case E判定.XPerfect:
                                                nNowCombo++;
                                                CDTXMania.listTargetGhostScoreData[ i ].n最大コンボ数 = Math.Max( nNowCombo, CDTXMania.listTargetGhostScoreData[ i ].n最大コンボ数 );
                                                break;
                                            case E判定.Poor:
                                            case E判定.Miss:
                                            case E判定.Bad:
                                                CDTXMania.listTargetGhostScoreData[ i ].n最大コンボ数 = Math.Max( nNowCombo, CDTXMania.listTargetGhostScoreData[ i ].n最大コンボ数 );
                                                nNowCombo = 0;
                                                break;
                                        }
                                        //Trace.WriteLine( eJudge.ToString() + " " + nNowCombo.ToString() + "Combo Max:" + nMaxCombo.ToString() + "Combo" );
                                    }
                                }
                                //CDTXMania.listTargetGhostScoreData[ i ].n最大コンボ数 = nMaxCombo;
                                int nTotal = CDTXMania.DTX.n可視チップ数.Drums;
                                if( i == 1 ) nTotal = CDTXMania.DTX.n可視チップ数.Guitar;
                                else if( i == 2 ) nTotal = CDTXMania.DTX.n可視チップ数.Bass;
                                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                                {
                                    CDTXMania.listTargetGhostScoreData[ i ].db演奏型スキル値 = CScoreIni.t演奏型スキルを計算して返す( nTotal, CDTXMania.listTargetGhostScoreData[ i ].nPerfect数, CDTXMania.listTargetGhostScoreData[ i ].nGreat数, CDTXMania.listTargetGhostScoreData[ i ].nGood数, CDTXMania.listTargetGhostScoreData[ i ].nPoor数, CDTXMania.listTargetGhostScoreData[ i ].nMiss数, (E楽器パート)i, CDTXMania.listTargetGhostScoreData[ i ].bAutoPlay );
                                }
                                else
                                {
                                    CDTXMania.listTargetGhostScoreData[ i ].db演奏型スキル値 = CScoreIni.tXG演奏型スキルを計算して返す( nTotal, CDTXMania.listTargetGhostScoreData[ i ].nPerfect数, CDTXMania.listTargetGhostScoreData[ i ].nGreat数, CDTXMania.listTargetGhostScoreData[ i ].nGood数, CDTXMania.listTargetGhostScoreData[ i ].nPoor数, CDTXMania.listTargetGhostScoreData[ i ].nMiss数, CDTXMania.listTargetGhostScoreData[ i ].n最大コンボ数, (E楽器パート)i, CDTXMania.listTargetGhostScoreData[ i ].bAutoPlay );
                                }
                            }
                        }
                        catch( Exception ex )
                        {
                            Trace.TraceError( "ゴーストデータの読み込みに失敗しました。" + ex.StackTrace );
                        }
 

						span = (TimeSpan) ( DateTime.Now - timeBeginLoad );
						Trace.TraceInformation( "DTX読込所要時間:           {0}", span.ToString() );

                        if( CDTXMania.bXGRelease )
                        {
						    if ( CDTXMania.bコンパクトモード )
							    CDTXMania.DTX.MIDIレベル = 1;
						    else
							    CDTXMania.DTX.MIDIレベル = ( CDTXMania.stage選曲XG.r確定された曲.eノード種別 == C曲リストノード.Eノード種別.SCORE_MIDI ) ? CDTXMania.stage選曲XG.n現在選択中の曲の難易度 : 0;
                        }
                        else
                        {
						    if ( CDTXMania.bコンパクトモード )
							    CDTXMania.DTX.MIDIレベル = 1;
						    else
							    CDTXMania.DTX.MIDIレベル = ( CDTXMania.stage選曲GITADORA.r確定された曲.eノード種別 == C曲リストノード.Eノード種別.SCORE_MIDI ) ? CDTXMania.stage選曲GITADORA.n現在選択中の曲の難易度 : 0;
                        }

						base.eフェーズID = CStage.Eフェーズ.NOWLOADING_WAVファイルを読み込む;
						timeBeginLoadWAV = DateTime.Now;
						return (int) E曲読込画面の戻り値.継続;
					}

				case CStage.Eフェーズ.NOWLOADING_WAVファイルを読み込む:
					{
						if ( nWAVcount == 1 && CDTXMania.DTX.listWAV.Count > 0 )			// #28934 2012.7.7 yyagi (added checking Count)
						{
							ShowProgressByFilename( CDTXMania.DTX.listWAV[ nWAVcount ].strファイル名 );
						}
						int looptime = (CDTXMania.ConfigIni.b垂直帰線待ちを行う)? 3 : 1;	// VSyncWait=ON時は1frame(1/60s)あたり3つ読むようにする
						for ( int i = 0; i < looptime && nWAVcount <= CDTXMania.DTX.listWAV.Count; i++ )
						{
							if ( CDTXMania.DTX.listWAV[ nWAVcount ].listこのWAVを使用するチャンネル番号の集合.Count > 0 )	// #28674 2012.5.8 yyagi
							{
								CDTXMania.DTX.tWAVの読み込み( CDTXMania.DTX.listWAV[ nWAVcount ] );
							}
							nWAVcount++;
						}
						if ( nWAVcount <= CDTXMania.DTX.listWAV.Count )
						{
							ShowProgressByFilename( CDTXMania.DTX.listWAV[ nWAVcount ].strファイル名 );
						}
						if ( nWAVcount > CDTXMania.DTX.listWAV.Count )
						{
							TimeSpan span = ( TimeSpan ) ( DateTime.Now - timeBeginLoadWAV );
							Trace.TraceInformation( "WAV読込所要時間({0,4}):     {1}", CDTXMania.DTX.listWAV.Count, span.ToString() );
							timeBeginLoadWAV = DateTime.Now;

							if ( CDTXMania.ConfigIni.bDynamicBassMixerManagement )
							{
								CDTXMania.DTX.PlanToAddMixerChannel();
							}
							CDTXMania.DTX.tギターとベースのランダム化( E楽器パート.GUITAR, CDTXMania.ConfigIni.eRandom.Guitar );
							CDTXMania.DTX.tギターとベースのランダム化( E楽器パート.BASS, CDTXMania.ConfigIni.eRandom.Bass );
                            CDTXMania.DTX.tドコドコ仕様変更( E楽器パート.DRUMS, CDTXMania.ConfigIni.eDkdkType.Drums );
                            CDTXMania.DTX.t旧仕様のドコドコチップを振り分ける( E楽器パート.DRUMS, CDTXMania.ConfigIni.bAssignToLBD.Drums );
                            //CDTXMania.DTX

                            #region[ 譜面に応じてSkillMode変更 ]
                            if( CDTXMania.ConfigIni.bSkillModeを自動切替えする )
                            {
                                if( CDTXMania.ConfigIni.bDrums有効 ? CDTXMania.DTX.bCLASSIC譜面である[ 0 ] : CDTXMania.DTX.bCLASSIC譜面である[ 1 ] | CDTXMania.DTX.bCLASSIC譜面である[ 2 ] )
                                {
                                    CDTXMania.ConfigIni.eSkillMode = ESkillType.DTXMania;
                                }
                                else
                                {
                                    CDTXMania.ConfigIni.eSkillMode = ESkillType.XG;
                                }
                            }
                            #endregion

                            if( CDTXMania.bXGRelease )
                            {
                                if ( CDTXMania.ConfigIni.bギタレボモード )
								    CDTXMania.stage演奏ギター画面.On活性化();
							    else
								    CDTXMania.stage演奏ドラム画面.On活性化();
                            }
                            else
                            {
                                if ( CDTXMania.ConfigIni.bギタレボモード )
								    CDTXMania.stage演奏ギター画面GITADORA.On活性化();
							    else
								    CDTXMania.stage演奏ドラム画面GITADORA.On活性化();
                            }
                            
							span = (TimeSpan) ( DateTime.Now - timeBeginLoadWAV );
							Trace.TraceInformation( "WAV/譜面後処理時間({0,4}):  {1}", ( CDTXMania.DTX.listBMP.Count + CDTXMania.DTX.listBMPTEX.Count + CDTXMania.DTX.listAVI.Count ), span.ToString() );

							base.eフェーズID = CStage.Eフェーズ.NOWLOADING_BMPファイルを読み込む;
						}
						return (int) E曲読込画面の戻り値.継続;
					}

				case CStage.Eフェーズ.NOWLOADING_BMPファイルを読み込む:
					{
						TimeSpan span;
						DateTime timeBeginLoadBMPAVI = DateTime.Now;
						if ( CDTXMania.ConfigIni.bBGA有効 )
							CDTXMania.DTX.tBMP_BMPTEXの読み込み();

						if ( CDTXMania.ConfigIni.bAVI有効 )
							CDTXMania.DTX.tAVIの読み込み();
						span = ( TimeSpan ) ( DateTime.Now - timeBeginLoadBMPAVI );
						Trace.TraceInformation( "BMP/AVI読込所要時間({0,4}): {1}", ( CDTXMania.DTX.listBMP.Count + CDTXMania.DTX.listBMPTEX.Count + CDTXMania.DTX.listAVI.Count ), span.ToString() );

						span = ( TimeSpan ) ( DateTime.Now - timeBeginLoad );
						Trace.TraceInformation( "総読込時間:                {0}", span.ToString() );

						if ( bitmapFilename != null )
						{
							bitmapFilename.Dispose();
							bitmapFilename = null;
						}
						if ( graphicsFilename != null )
						{
							graphicsFilename.Dispose();
							graphicsFilename = null;
						}
						if ( ftFilename != null )
						{
							ftFilename.Dispose();
							ftFilename = null;
						}
						CDTXMania.Timer.t更新();
						base.eフェーズID = CStage.Eフェーズ.NOWLOADING_システムサウンドBGMの完了を待つ;
						return (int) E曲読込画面の戻り値.継続;
					}

				case CStage.Eフェーズ.NOWLOADING_システムサウンドBGMの完了を待つ:
					{
						long nCurrentTime = CDTXMania.Timer.n現在時刻;
						if( nCurrentTime < this.nBGM再生開始時刻 )
							this.nBGM再生開始時刻 = nCurrentTime;

//						if ( ( nCurrentTime - this.nBGM再生開始時刻 ) > ( this.nBGMの総再生時間ms - 1000 ) )
						if ( ( nCurrentTime - this.nBGM再生開始時刻 ) >= ( this.nBGMの総再生時間ms ) )	// #27787 2012.3.10 yyagi 1000ms == フェードイン分の時間
						{
							if ( !CDTXMania.DTXVmode.Enabled )
							{
								this.actFO.tフェードアウト開始();
							}
							base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
						}
						return (int) E曲読込画面の戻り値.継続;
					}

				case CStage.Eフェーズ.共通_フェードアウト:
					if ( this.actFO.On進行描画() == 0 && !CDTXMania.DTXVmode.Enabled )		// DTXVモード時は、フェードアウト省略
						return 0;

					if ( txFilename != null )
					{
						txFilename.Dispose();
					}
					if ( this.sd読み込み音 != null )
					{
						this.sd読み込み音.t解放する();
					}
					return (int) E曲読込画面の戻り値.読込完了;
			}
    		return (int) E曲読込画面の戻り値.継続;
		}

		/// <summary>
		/// ESC押下時、trueを返す
		/// </summary>
		/// <returns></returns>
		protected bool tキー入力()
		{
			IInputDevice keyboard = CDTXMania.Input管理.Keyboard;
            if( base.eフェーズID != Eフェーズ.共通_フェードアウト )
            {
			    if 	( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.Escape ) )		// escape (exit)
			    {
				    return true;    //2017.06.17 kairera0467 フェードアウト中はキー操作ができないよう変更。
			    }
            }
			return false;
		}


		private void ShowProgressByFilename(string strファイル名 )
		{
			if ( graphicsFilename != null && ftFilename != null )
			{
				graphicsFilename.Clear( Color.Transparent );
				graphicsFilename.DrawString( strファイル名, ftFilename, Brushes.White, new RectangleF( 0, 0, SampleFramework.GameWindowSize.Width, fFontSizeFilename * Scale.X ) );
				if ( txFilename != null )
				{
					txFilename.Dispose();
				}
				txFilename = new CTexture( CDTXMania.app.Device, bitmapFilename, CDTXMania.TextureFormat );
				txFilename.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
				txFilename.t2D描画(
					CDTXMania.app.Device,
					0,
					( SampleFramework.GameWindowSize.Height - (int) ( txFilename.szテクスチャサイズ.Height * 0.5 ) )
				);
			}
		}

        private void tSkillModeを譜面に応じて切り替える( CDTX cdtx )
        {
            if( CDTXMania.ConfigIni.bDrums有効 ? ( cdtx.bCLASSIC譜面である.Drums ) :
                                                 ( cdtx.bCLASSIC譜面である.Guitar | cdtx.bCLASSIC譜面である.Bass ) &&
              !cdtx.b強制的にXG譜面にする )
                CDTXMania.ConfigIni.eSkillMode = ESkillType.DTXMania;
            else
                CDTXMania.ConfigIni.eSkillMode = ESkillType.XG;
        }

        // その他

        #region [ private ]
        //-----------------
        private CAct曲読み込みメイン画面GD actLoadMain;
		private CActFIFOBlack actFI;
		private CActFIFOBlack actFO;
		private bool b音符を表示する;
		private Font ftタイトル表示用フォント;
		private long nBGMの総再生時間ms;
		private long nBGM再生開始時刻;
        private int nCurrentInst;
		private int n音符の表示位置X;
		private CSound sd読み込み音;
		private string strSTAGEFILE;
		private string str曲タイトル;
		private CTexture tx背景;
		private DateTime timeBeginLoad;
		private DateTime timeBeginLoadWAV;
		private int nWAVcount;
		private CTexture txFilename;
		private Bitmap bitmapFilename;
		private Graphics graphicsFilename;
		private Font ftFilename;
		private const float fFontSizeFilename = 12.0f;
		private const float fFontSizeTitle = 48;

        private STDGBVALUE<List<STGhostLag>> stGhostLag;

        [StructLayout(LayoutKind.Sequential)]
        private struct STGhostLag
        {
            public int index;
            public int nJudgeTime;
            public int nLagTime;
            public STGhostLag( int index, int nJudgeTime, int nLagTime )
            {
                this.index = index;
                this.nJudgeTime = nJudgeTime;
                this.nLagTime = nLagTime;
            }
        }
        protected E判定 e指定時刻からChipのJUDGEを返す( long nTime, int nInputAdjustTime )
		{
			//if ( pChip != null )
			{
				int nDeltaTime = Math.Abs( (int)nTime );
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
		//-----------------
		#endregion
	}
}
