﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using FDK;

namespace DTXMania
{
	// グローバル定数

	public enum Eシステムサウンド
	{
		BGMオプション画面 = 0,
		BGMコンフィグ画面,
		BGM起動画面,
		BGM選曲画面,
		SOUNDステージ失敗音,
		SOUNDカーソル移動音,
		SOUNDゲーム開始音,
		SOUNDゲーム終了音,
		SOUNDステージクリア音,
		SOUNDタイトル音,
		SOUNDフルコンボ音,
		SOUND歓声音,
		SOUND曲読込開始音,
		SOUND決定音,
		SOUND取消音,
		SOUND変更音,
        SOUND曲決定音,
		Count				// システムサウンド総数の計算用
	}

	internal class CSkin : IDisposable
	{
		// クラス

		public class Cシステムサウンド : IDisposable
		{
			// static フィールド

			public static CSkin.Cシステムサウンド r最後に再生した排他システムサウンド;

			// フィールド、プロパティ

			public bool bCompact対象;
			public bool bループ;
			public bool b読み込み未試行;
			public bool b読み込み成功;
			public bool b排他;
			public string strファイル名 = "";
			public bool b再生中
			{
				get
				{
					if( this.rSound[ 1 - this.n次に鳴るサウンド番号 ] == null )
						return false;

					return this.rSound[ 1 - this.n次に鳴るサウンド番号 ].b再生中;
				}
			}
			public int n位置_現在のサウンド
			{
				get
				{
					CSound sound = this.rSound[ 1 - this.n次に鳴るサウンド番号 ];
					if( sound == null )
						return 0;

					return sound.n位置;
				}
				set
				{
					CSound sound = this.rSound[ 1 - this.n次に鳴るサウンド番号 ];
					if( sound != null )
						sound.n位置 = value;
				}
			}
			public int n位置_次に鳴るサウンド
			{
				get
				{
					CSound sound = this.rSound[ this.n次に鳴るサウンド番号 ];
					if( sound == null )
						return 0;

					return sound.n位置;
				}
				set
				{
					CSound sound = this.rSound[ this.n次に鳴るサウンド番号 ];
					if( sound != null )
						sound.n位置 = value;
				}
			}
			public int n音量_現在のサウンド
			{
				get
				{
					CSound sound = this.rSound[ 1 - this.n次に鳴るサウンド番号 ];
					if( sound == null )
						return 0;

					return sound.n音量;
				}
				set
				{
					CSound sound = this.rSound[ 1 - this.n次に鳴るサウンド番号 ];
					if( sound != null )
						sound.n音量 = value;
				}
			}
			public int n音量_次に鳴るサウンド
			{
				get
				{
					CSound sound = this.rSound[ this.n次に鳴るサウンド番号 ];
					if( sound == null )
					{
						return 0;
					}
					return sound.n音量;
				}
				set
				{
					CSound sound = this.rSound[ this.n次に鳴るサウンド番号 ];
					if( sound != null )
					{
						sound.n音量 = value;
					}
				}
			}
			public int n長さ_現在のサウンド
			{
				get
				{
					CSound sound = this.rSound[ 1 - this.n次に鳴るサウンド番号 ];
					if( sound == null )
					{
						return 0;
					}
					return sound.n総演奏時間ms;
				}
			}
			public int n長さ_次に鳴るサウンド
			{
				get
				{
					CSound sound = this.rSound[ this.n次に鳴るサウンド番号 ];
					if( sound == null )
					{
						return 0;
					}
					return sound.n総演奏時間ms;
				}
			}


			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="strファイル名"></param>
			/// <param name="bループ"></param>
			/// <param name="b排他"></param>
			/// <param name="bCompact対象"></param>
			public Cシステムサウンド(string strファイル名, bool bループ, bool b排他, bool bCompact対象)
			{
				this.strファイル名 = strファイル名;
				this.bループ = bループ;
				this.b排他 = b排他;
				this.bCompact対象 = bCompact対象;
				this.b読み込み未試行 = true;
			}
			public Cシステムサウンド()
			{
				this.b読み込み未試行 = true;
			}
			

			// メソッド

			public void t読み込み()
			{
				this.b読み込み未試行 = false;
				this.b読み込み成功 = false;
				if( string.IsNullOrEmpty( this.strファイル名 ) )
					throw new InvalidOperationException( "ファイル名が無効です。" );

				if( !File.Exists( CSkin.Path( this.strファイル名 ) ) )
				{
					throw new FileNotFoundException( this.strファイル名 );
				}
////				for( int i = 0; i < 2; i++ )		// #27790 2012.3.10 yyagi 2回読み出しを、1回読みだし＋1回メモリコピーに変更
////				{
//                    try
//                    {
//                        this.rSound[ 0 ] = CDTXMania.Sound管理.tサウンドを生成する( CSkin.Path( this.strファイル名 ) );
//                    }
//                    catch
//                    {
//                        this.rSound[ 0 ] = null;
//                        throw;
//                    }
//                    if ( this.rSound[ 0 ] == null )	// #28243 2012.5.3 yyagi "this.rSound[ 0 ].bストリーム再生する"時もCloneするようにし、rSound[1]がnullにならないよう修正→rSound[1]の再生正常化
//                    {
//                        this.rSound[ 1 ] = null;
//                    }
//                    else
//                    {
//                        this.rSound[ 1 ] = ( CSound ) this.rSound[ 0 ].Clone();	// #27790 2012.3.10 yyagi add: to accelerate loading chip sounds
//                        CDTXMania.Sound管理.tサウンドを登録する( this.rSound[ 1 ] );	// #28243 2012.5.3 yyagi add (登録漏れによりストリーム再生処理が発生していなかった)
//                    }

////				}

				for ( int i = 0; i < 2; i++ )		// 一旦Cloneを止めてASIO対応に専念
				{
					try
					{
						this.rSound[ i ] = CDTXMania.Sound管理.tサウンドを生成する( CSkin.Path( this.strファイル名 ) );
					}
					catch
					{
						this.rSound[ i ] = null;
						throw;
					}
				}
				this.b読み込み成功 = true;
			}
			public void t再生する()
			{
				if ( this.b読み込み未試行 )
				{
					try
					{
						t読み込み();
					}
					catch
					{
						this.b読み込み未試行 = false;
					}
				}
				if( this.b排他 )
				{
					if( r最後に再生した排他システムサウンド != null )
						r最後に再生した排他システムサウンド.t停止する();

					r最後に再生した排他システムサウンド = this;
				}
				CSound sound = this.rSound[ this.n次に鳴るサウンド番号 ];
				if( sound != null )
					sound.t再生を開始する( this.bループ );

				this.n次に鳴るサウンド番号 = 1 - this.n次に鳴るサウンド番号;
			}
			public void t停止する()
			{
				if( this.rSound[ 0 ] != null )
					this.rSound[ 0 ].t再生を停止する();

				if( this.rSound[ 1 ] != null )
					this.rSound[ 1 ].t再生を停止する();

				if( r最後に再生した排他システムサウンド == this )
					r最後に再生した排他システムサウンド = null;
			}

			public void tRemoveMixer()
			{
				if ( CDTXMania.Sound管理.GetCurrentSoundDeviceType() != "DirectSound" )
				{
					for ( int i = 0; i < 2; i++ )
					{
						if ( this.rSound[ i ] != null )
						{
							CDTXMania.Sound管理.RemoveMixer( this.rSound[ i ] );
						}
					}
				}
			}

			#region [ IDisposable 実装 ]
			//-----------------
			public void Dispose()
			{
				if( !this.bDisposed済み )
				{
					for( int i = 0; i < 2; i++ )
					{
						if( this.rSound[ i ] != null )
						{
							CDTXMania.Sound管理.tサウンドを破棄する( this.rSound[ i ] );
							this.rSound[ i ] = null;
						}
					}
					this.b読み込み成功 = false;
					this.bDisposed済み = true;
				}
			}
			//-----------------
			#endregion

			#region [ private ]
			//-----------------
			private bool bDisposed済み;
			private int n次に鳴るサウンド番号;
			private CSound[] rSound = new CSound[ 2 ];
			//-----------------
			#endregion
		}

	
		// プロパティ

		public Cシステムサウンド bgmオプション画面 = null;
		public Cシステムサウンド bgmコンフィグ画面 = null;
		public Cシステムサウンド bgm起動画面 = null;
		public Cシステムサウンド bgm選曲画面 = null;
		public Cシステムサウンド soundSTAGEFAILED音 = null;
		public Cシステムサウンド soundカーソル移動音 = null;
		public Cシステムサウンド soundゲーム開始音 = null;
		public Cシステムサウンド soundゲーム終了音 = null;
		public Cシステムサウンド soundステージクリア音 = null;
		public Cシステムサウンド soundタイトル音 = null;
		public Cシステムサウンド soundフルコンボ音 = null;
		public Cシステムサウンド sound歓声音 = null;
		public Cシステムサウンド sound曲読込開始音 = null;
		public Cシステムサウンド sound決定音 = null;
		public Cシステムサウンド sound取消音 = null;
		public Cシステムサウンド sound変更音 = null;
        public Cシステムサウンド sound曲決定音 = null;
		public readonly int nシステムサウンド数 = (int)Eシステムサウンド.Count;
		public Cシステムサウンド this[ Eシステムサウンド sound ]
		{
			get
			{
				switch( sound )
				{
					case Eシステムサウンド.SOUNDカーソル移動音:
						return this.soundカーソル移動音;

					case Eシステムサウンド.SOUND決定音:
						return this.sound決定音;

					case Eシステムサウンド.SOUND変更音:
						return this.sound変更音;

					case Eシステムサウンド.SOUND取消音:
						return this.sound取消音;

					case Eシステムサウンド.SOUND歓声音:
						return this.sound歓声音;

					case Eシステムサウンド.SOUNDステージ失敗音:
						return this.soundSTAGEFAILED音;

					case Eシステムサウンド.SOUNDゲーム開始音:
						return this.soundゲーム開始音;

					case Eシステムサウンド.SOUNDゲーム終了音:
						return this.soundゲーム終了音;

					case Eシステムサウンド.SOUNDステージクリア音:
						return this.soundステージクリア音;

					case Eシステムサウンド.SOUNDフルコンボ音:
						return this.soundフルコンボ音;

					case Eシステムサウンド.SOUND曲読込開始音:
						return this.sound曲読込開始音;

					case Eシステムサウンド.SOUNDタイトル音:
						return this.soundタイトル音;

					case Eシステムサウンド.BGM起動画面:
						return this.bgm起動画面;

					case Eシステムサウンド.BGMオプション画面:
						return this.bgmオプション画面;

					case Eシステムサウンド.BGMコンフィグ画面:
						return this.bgmコンフィグ画面;

					case Eシステムサウンド.BGM選曲画面:
						return this.bgm選曲画面;

                    case Eシステムサウンド.SOUND曲決定音:
                        return this.sound曲決定音;
				}
				throw new IndexOutOfRangeException();
			}
		}
		public Cシステムサウンド this[ int index ]
		{
			get
			{
				switch( index )
				{
					case 0:
						return this.soundカーソル移動音;

					case 1:
						return this.sound決定音;

					case 2:
						return this.sound変更音;

					case 3:
						return this.sound取消音;

					case 4:
						return this.sound歓声音;

					case 5:
						return this.soundSTAGEFAILED音;

					case 6:
						return this.soundゲーム開始音;

					case 7:
						return this.soundゲーム終了音;

					case 8:
						return this.soundステージクリア音;

					case 9:
						return this.soundフルコンボ音;

					case 10:
						return this.sound曲読込開始音;

					case 11:
						return this.soundタイトル音;

					case 12:
						return this.bgm起動画面;

					case 13:
						return this.bgmオプション画面;

					case 14:
						return this.bgmコンフィグ画面;

					case 15:
						return this.bgm選曲画面;

                    case 16:
                        return this.sound曲決定音;
				}
				throw new IndexOutOfRangeException();
			}
		}


		// スキンの切り替えについて・・・
		//
		// ・スキンの種類は大きく分けて2種類。Systemスキンとboxdefスキン。
		// 　前者はSystem/フォルダにユーザーが自らインストールしておくスキン。
		// 　後者はbox.defで指定する、曲データ制作者が提示するスキン。
		//
		// ・Config画面で、2種のスキンを区別無く常時使用するよう設定することができる。
		// ・box.defの#SKINPATH 設定により、boxdefスキンを一時的に使用するよう設定する。
		// 　(box.defの効果の及ばない他のmuxic boxでは、当該boxdefスキンの有効性が無くなる)
		//
		// これを実現するために・・・
		// ・Systemスキンの設定情報と、boxdefスキンの設定情報は、分離して持つ。
		// 　(strSystem～～ と、strBoxDef～～～)
		// ・Config画面からは前者のみ書き換えできるようにし、
		// 　選曲画面からは後者のみ書き換えできるようにする。(SetCurrent...())
		// ・読み出しは両者から行えるようにすると共に
		// 　選曲画面用に二種の情報を区別しない読み出し方法も提供する(GetCurrent...)

		private object lockBoxDefSkin;
		public static bool bUseBoxDefSkin = true;						// box.defからのスキン変更を許容するか否か

		public string strSystemSkinRoot = null;
		public string[] strSystemSkinSubfolders = null;		// List<string>だとignoreCaseな検索が面倒なので、配列に逃げる :-)
		private string[] _strBoxDefSkinSubfolders = null;
		public string[] strBoxDefSkinSubfolders
		{
			get
			{
				lock ( lockBoxDefSkin )
				{
					return _strBoxDefSkinSubfolders;
				}
			}
			set
			{
				lock ( lockBoxDefSkin )
				{
					_strBoxDefSkinSubfolders = value;
				}
			}
		}			// 別スレッドからも書き込みアクセスされるため、スレッドセーフなアクセス法を提供

		private static string strSystemSkinSubfolderFullName;			// Config画面で設定されたスキン
		private static string strBoxDefSkinSubfolderFullName = "";		// box.defで指定されているスキン

		/// <summary>
		/// スキンパス名をフルパスで取得する
		/// </summary>
		/// <param name="bFromUserConfig">ユーザー設定用ならtrue, box.defからの設定ならfalse</param>
		/// <returns></returns>
		public string GetCurrentSkinSubfolderFullName( bool bFromUserConfig )
		{
			if ( !bUseBoxDefSkin || bFromUserConfig == true || strBoxDefSkinSubfolderFullName == "" )
			{
				return strSystemSkinSubfolderFullName;
			}
			else
			{
				return strBoxDefSkinSubfolderFullName;
			}
		}
		/// <summary>
		/// スキンパス名をフルパスで設定する
		/// </summary>
		/// <param name="value">スキンパス名</param>
		/// <param name="bFromUserConfig">ユーザー設定用ならtrue, box.defからの設定ならfalse</param>
		public void SetCurrentSkinSubfolderFullName( string value, bool bFromUserConfig )
		{
			if ( bFromUserConfig )
			{
				strSystemSkinSubfolderFullName = value;
			}
			else
			{
				strBoxDefSkinSubfolderFullName = value;
			}
		}


		// コンストラクタ
		public CSkin( string _strSkinSubfolderFullName, bool _bUseBoxDefSkin )
		{
			lockBoxDefSkin = new object();
			strSystemSkinSubfolderFullName = _strSkinSubfolderFullName;
			bUseBoxDefSkin = _bUseBoxDefSkin;
			InitializeSkinPathRoot();
			ReloadSkinPaths();
			PrepareReloadSkin();
            CreateShutterList(); //2017.04.13 kairera0467
		}
		public CSkin()
		{
			lockBoxDefSkin = new object();
			InitializeSkinPathRoot();
			bUseBoxDefSkin = true;
			ReloadSkinPaths();
			PrepareReloadSkin();
            CreateShutterList(); //2017.04.13 kairera0467
		}
		private string InitializeSkinPathRoot()
		{
			strSystemSkinRoot = System.IO.Path.Combine( CDTXMania.strEXEのあるフォルダ, "System" + System.IO.Path.DirectorySeparatorChar );
			return strSystemSkinRoot;
		}

		/// <summary>
		/// Skin(Sounds)を再読込する準備をする(再生停止,Dispose,ファイル名再設定)。
		/// あらかじめstrSkinSubfolderを適切に設定しておくこと。
		/// その後、ReloadSkinPaths()を実行し、strSkinSubfolderの正当性を確認した上で、本メソッドを呼び出すこと。
		/// 本メソッド呼び出し後に、ReloadSkin()を実行することで、システムサウンドを読み込み直す。
		/// ReloadSkin()の内容は本メソッド内に含めないこと。起動時はReloadSkin()相当の処理をCEnumSongsで行っているため。
		/// </summary>
		public void PrepareReloadSkin()
		{
			Trace.TraceInformation( "SkinPath設定: {0}",
				( strBoxDefSkinSubfolderFullName == "" ) ?
				strSystemSkinSubfolderFullName :
				strBoxDefSkinSubfolderFullName
			);

			for ( int i = 0; i < nシステムサウンド数; i++ )
			{
				if ( this[ i ] != null && this[i].b読み込み成功 )
				{
					this[ i ].t停止する();
					this[ i ].Dispose();
				}
			}
			this.soundカーソル移動音	= new Cシステムサウンド( @"Sounds\Move.ogg",			false, false, false );
			this.sound決定音			= new Cシステムサウンド( @"Sounds\Decide.ogg",			false, false, false );
			this.sound変更音			= new Cシステムサウンド( @"Sounds\Change.ogg",			false, false, false );
			this.sound取消音			= new Cシステムサウンド( @"Sounds\Cancel.ogg",			false, false, true  );
			this.sound歓声音			= new Cシステムサウンド( @"Sounds\Audience.ogg",		false, false, true  );
			this.soundSTAGEFAILED音		= new Cシステムサウンド( @"Sounds\Stage failed.ogg",	false, true,  true  );
			this.soundゲーム開始音		= new Cシステムサウンド( @"Sounds\Game start.ogg",		false, false, false );
			this.soundゲーム終了音		= new Cシステムサウンド( @"Sounds\Game end.ogg",		false, true,  false );
			this.soundステージクリア音	= new Cシステムサウンド( @"Sounds\Stage clear.ogg",		false, true,  true  );
			this.soundフルコンボ音		= new Cシステムサウンド( @"Sounds\Full combo.ogg",		false, false, true  );
			this.sound曲読込開始音		= new Cシステムサウンド( @"Sounds\Now loading.ogg",		false, true,  true  );
			this.soundタイトル音		= new Cシステムサウンド( @"Sounds\Title.ogg",			false, true,  false );
			this.bgm起動画面			= new Cシステムサウンド( @"Sounds\Setup BGM.ogg",		true,  true,  false );
			this.bgmオプション画面		= new Cシステムサウンド( @"Sounds\Option BGM.ogg",		true,  true,  false );
			this.bgmコンフィグ画面		= new Cシステムサウンド( @"Sounds\Config BGM.ogg",		true,  true,  false );
			this.bgm選曲画面			= new Cシステムサウンド( @"Sounds\Select BGM.ogg",		true,  true,  false );
            if( File.Exists( CSkin.Path( @"Sounds\MusicDecide.ogg" ) ) )
			    this.sound曲決定音 = new Cシステムサウンド( @"Sounds\MusicDecide.ogg",		false, false, false );
            else
			    this.sound曲決定音 = new Cシステムサウンド( @"Sounds\Decide.ogg",		    false, false, false );

            tReadSkinConfig();
		}

		public void ReloadSkin()
		{
			for ( int i = 0; i < nシステムサウンド数; i++ )
			{
				if ( !this[ i ].b排他 )	// BGM系以外のみ読み込む。(BGM系は必要になったときに読み込む)
				{
					Cシステムサウンド cシステムサウンド = this[ i ];
					if ( !CDTXMania.bコンパクトモード || cシステムサウンド.bCompact対象 )
					{
						try
						{
							cシステムサウンド.t読み込み();
							Trace.TraceInformation( "システムサウンドを読み込みました。({0})", cシステムサウンド.strファイル名 );
						}
						catch ( FileNotFoundException )
						{
							Trace.TraceWarning( "システムサウンドが存在しません。({0})", cシステムサウンド.strファイル名 );
						}
						catch ( Exception e )
						{
							Trace.TraceError( e.Message );
							Trace.TraceWarning( "システムサウンドの読み込みに失敗しました。({0})", cシステムサウンド.strファイル名 );
						}
					}
				}
			}
            CreateShutterList();
		}


		/// <summary>
		/// Skinの一覧を再取得する。
		/// System/*****/Graphics (やSounds/) というフォルダ構成を想定している。
		/// もし再取得の結果、現在使用中のSkinのパス(strSystemSkinSubfloderFullName)が消えていた場合は、
		/// 以下の優先順位で存在確認の上strSystemSkinSubfolderFullNameを再設定する。
		/// 1. System/Default/
		/// 2. System/*****/ で最初にenumerateされたもの
 		/// 3. System/ (従来互換)
		/// </summary>
		public void ReloadSkinPaths()
		{
			#region [ まず System/*** をenumerateする ]
			string[] tempSkinSubfolders = System.IO.Directory.GetDirectories( strSystemSkinRoot, "*" );
			strSystemSkinSubfolders = new string[ tempSkinSubfolders.Length ];
			int size = 0;
			for ( int i = 0; i < tempSkinSubfolders.Length; i++ )
			{
				#region [ 検出したフォルダがスキンフォルダかどうか確認する]
				if ( !bIsValid( tempSkinSubfolders[ i ] ) )
					continue;
				#endregion
				#region [ スキンフォルダと確認できたものを、strSkinSubfoldersに入れる ]
				// フォルダ名末尾に必ず\をつけておくこと。さもないとConfig読み出し側(必ず\をつける)とマッチできない
				if ( tempSkinSubfolders[ i ][ tempSkinSubfolders[ i ].Length - 1 ] != System.IO.Path.DirectorySeparatorChar )
				{
					tempSkinSubfolders[ i ] += System.IO.Path.DirectorySeparatorChar;
				}
				strSystemSkinSubfolders[ size ] = tempSkinSubfolders[ i ];
				Trace.TraceInformation( "SkinPath検出: {0}", strSystemSkinSubfolders[ size ] );
				size++;
				#endregion
			}
			Trace.TraceInformation( "SkinPath入力: {0}", strSystemSkinSubfolderFullName );
			Array.Resize( ref strSystemSkinSubfolders, size );
			Array.Sort( strSystemSkinSubfolders );	// BinarySearch実行前にSortが必要
			#endregion

			#region [ 現在のSkinパスがbox.defスキンをCONFIG指定していた場合のために、最初にこれが有効かチェックする。有効ならこれを使う。 ]
			if ( bIsValid( strSystemSkinSubfolderFullName ) &&
				Array.BinarySearch( strSystemSkinSubfolders, strSystemSkinSubfolderFullName,
				StringComparer.InvariantCultureIgnoreCase ) < 0 )
			{
				strBoxDefSkinSubfolders = new string[ 1 ]{ strSystemSkinSubfolderFullName };
				return;
			}
			#endregion

			#region [ 次に、現在のSkinパスが存在するか調べる。あれば終了。]
			if ( Array.BinarySearch( strSystemSkinSubfolders, strSystemSkinSubfolderFullName,
				StringComparer.InvariantCultureIgnoreCase ) >= 0 )
				return;
			#endregion
			#region [ カレントのSkinパスが消滅しているので、以下で再設定する。]
			/// 以下の優先順位で現在使用中のSkinパスを再設定する。
			/// 1. System/Default/
			/// 2. System/*****/ で最初にenumerateされたもの
			/// 3. System/ (従来互換)
			#region [ System/Default/ があるなら、そこにカレントSkinパスを設定する]
			string tempSkinPath_default = System.IO.Path.Combine( strSystemSkinRoot, "Default" + System.IO.Path.DirectorySeparatorChar );
			if ( Array.BinarySearch( strSystemSkinSubfolders, tempSkinPath_default, 
				StringComparer.InvariantCultureIgnoreCase ) >= 0 )
			{
				strSystemSkinSubfolderFullName = tempSkinPath_default;
				return;
			}
			#endregion
			#region [ System/SkinFiles.*****/ で最初にenumerateされたものを、カレントSkinパスに再設定する ]
			if ( strSystemSkinSubfolders.Length > 0 )
			{
				strSystemSkinSubfolderFullName = strSystemSkinSubfolders[ 0 ];
				return;
			}
			#endregion
			#region [ System/ に、カレントSkinパスを再設定する。]
			strSystemSkinSubfolderFullName = strSystemSkinRoot;
			strSystemSkinSubfolders = new string[ 1 ]{ strSystemSkinSubfolderFullName };
			#endregion
			#endregion
		}

		// メソッド

		public static string Path( string strファイルの相対パス )
		{
			if ( strBoxDefSkinSubfolderFullName == "" || !bUseBoxDefSkin )
			{
				return System.IO.Path.Combine( strSystemSkinSubfolderFullName, strファイルの相対パス );
			}
			else
			{
				return System.IO.Path.Combine( strBoxDefSkinSubfolderFullName, strファイルの相対パス );
			}
		}

		/// <summary>
		/// フルパス名を与えると、スキン名として、ディレクトリ名末尾の要素を返す
		/// 例: C:\foo\bar\ なら、barを返す
		/// </summary>
		/// <param name="skinpath">スキンが格納されたパス名(フルパス)</param>
		/// <returns>スキン名</returns>
		public static string GetSkinName( string skinPathFullName )
		{
			if ( skinPathFullName != null )
			{
				if ( skinPathFullName == "" )		// 「box.defで未定義」用
					skinPathFullName = strSystemSkinSubfolderFullName;
				string[] tmp = skinPathFullName.Split( System.IO.Path.DirectorySeparatorChar );
				return tmp[ tmp.Length - 2 ];		// ディレクトリ名の最後から2番目の要素がスキン名(最後の要素はnull。元stringの末尾が\なので。)
			}
			return null;
		}
		public static string[] GetSkinName( string[] skinPathFullNames )
		{
			string[] ret = new string[ skinPathFullNames.Length ];
			for ( int i = 0; i < skinPathFullNames.Length; i++ )
			{
				ret[ i ] = GetSkinName( skinPathFullNames[ i ] );
			}
			return ret;
		}


		public string GetSkinSubfolderFullNameFromSkinName( string skinName )
		{
			foreach ( string s in strSystemSkinSubfolders )
			{
				if ( GetSkinName( s ) == skinName )
					return s;
			}
			foreach ( string b in strBoxDefSkinSubfolders )
			{
				if ( GetSkinName( b ) == skinName )
					return b;
			}
			return null;
		}

		/// <summary>
		/// スキンパス名が妥当かどうか
		/// (タイトル画像にアクセスできるかどうかで判定する)
		/// </summary>
		/// <param name="skinPathFullName">妥当性を確認するスキンパス(フルパス)</param>
		/// <returns>妥当ならtrue</returns>
		public bool bIsValid( string skinPathFullName )
		{
			string filePathTitle;
			filePathTitle = System.IO.Path.Combine( skinPathFullName, @"Graphics\2_background.png" );
			return ( File.Exists( filePathTitle ) );
		}


		public void tRemoveMixerAll()
		{
			for ( int i = 0; i < nシステムサウンド数; i++ )
			{
				if ( this[ i ] != null && this[ i ].b読み込み成功 )
				{
					this[ i ].t停止する();
					this[ i ].tRemoveMixer();
				}
			}

		}
		#region [ IDisposable 実装 ]
		//-----------------
		public void Dispose()
		{
			if( !this.bDisposed済み )
			{
				for( int i = 0; i < this.nシステムサウンド数; i++ )
					this[ i ].Dispose();

				this.bDisposed済み = true;
			}
		}
		//-----------------
		#endregion
        
        public void tReadSkinConfig()
        {
            if( File.Exists( CSkin.Path( @"SkinConfig.ini" ) ) )
            {
                string str;
				//this.tキーアサインを全部クリアする();
				using ( StreamReader reader = new StreamReader( CSkin.Path( @"SkinConfig.ini" ), Encoding.GetEncoding( "Shift_JIS" ) ) )
                {
				    str = reader.ReadToEnd();
                }
                this.t文字列から読み込み( str );
            }
        }

        private void t文字列から読み込み(string strAllSettings)	// 2011.4.13 yyagi; refactored to make initial KeyConfig easier.
        {
            string[] delimiter = { "\n" };
            string[] strSingleLine = strAllSettings.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strSingleLine)
            {
                string str = s.Replace('\t', ' ').TrimStart(new char[] { '\t', ' ' });
                if ((str.Length != 0) && (str[0] != ';'))
                {
                    try
                    {
                        string str3;
                        string str4;
                        string[] strArray = str.Split(new char[] { '=' });
                        if (strArray.Length == 2)
                        {
                            str3 = strArray[0].Trim();
                            str4 = strArray[1].Trim();
                            //-----------------------------
                            if (str3.Equals("SkinType"))
                            {
                                CDTXMania.bXGRelease = C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 1, 0 ) == 0 ? true : false;
                            }
                            if (str3.Equals("NamePlateType"))
                            {
                                CDTXMania.ConfigIni.eNamePlateType = (Eタイプ)C変換.n値を文字列から取得して範囲内に丸めて返す( str4, 0, 3, (int)CDTXMania.ConfigIni.eNamePlateType );
                            }
                            //else if (str3.Equals("DrumSetMoves"))
                            //{
                            //    CDTXMania.ConfigIni.eドラムセットを動かす = (Eタイプ)C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 2, (int)CDTXMania.ConfigIni.eドラムセットを動かす);
                            //}
                            //else if (str3.Equals("BPMBar"))
                            //{
                            //    CDTXMania.ConfigIni.eBPMbar = (Eタイプ)C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 3, (int)CDTXMania.ConfigIni.eBPMbar);
                            //}
                            //else if (str3.Equals("LivePoint"))
                            //{
                            //    CDTXMania.ConfigIni.bLivePoint = C変換.bONorOFF(str4[0]);
                            //}
                            //else if (str3.Equals("Speaker"))
                            //{
                            //    CDTXMania.ConfigIni.bSpeaker = C変換.bONorOFF(str4[0]);
                            //}
                            else if (str3.Equals("JudgeAnimeType"))
                            {
                                CDTXMania.ConfigIni.eJudgeAnimeType = ( Eタイプ )C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, 2, (int)CDTXMania.ConfigIni.eJudgeAnimeType );
                            }
                            else if (str3.Equals("JudgeFrames"))
                            {
                                CDTXMania.ConfigIni.nJudgeFrames = C変換.n値を文字列から取得して返す(str4, CDTXMania.ConfigIni.nJudgeFrames);
                            }
                            else if (str3.Equals("JudgeInterval"))
                            {
                                CDTXMania.ConfigIni.nJudgeInterval = C変換.n値を文字列から取得して返す(str4, CDTXMania.ConfigIni.nJudgeInterval);
                            }
                            else if (str3.Equals("JudgeWidgh"))
                            {
                                CDTXMania.ConfigIni.nJudgeWidth = C変換.n値を文字列から取得して返す(str4, CDTXMania.ConfigIni.nJudgeWidth);
                            }
                            else if (str3.Equals("JudgeHeight"))
                            {
                                CDTXMania.ConfigIni.nJudgeHeight = C変換.n値を文字列から取得して返す(str4, CDTXMania.ConfigIni.nJudgeHeight);
                            }
                            else if (str3.Equals("JudgeStringBarX"))
                            {
                                string[] ar = str4.Split( ',' );
                                for( int i = 0; i < 3; i++ )
                                {
                                    CDTXMania.ConfigIni.nJudgeStringBarX[ i ] = int.Parse( ar[ i ] );
                                }
                            }
                            else if (str3.Equals("JudgeStringBarY"))
                            {
                                string[] ar = str4.Split( ',' );
                                for( int i = 0; i < 3; i++ )
                                {
                                    CDTXMania.ConfigIni.nJudgeStringBarY[ i ] = int.Parse( ar[ i ] );
                                }
                            }
                            else if (str3.Equals("JudgeStringBarWidth"))
                            {
                                string[] ar = str4.Split( ',' );
                                for( int i = 0; i < 3; i++ )
                                {
                                    CDTXMania.ConfigIni.nJudgeStringBarWidth[ i ] = int.Parse( ar[ i ] );
                                }
                            }
                            else if (str3.Equals("JudgeStringBarHeight"))
                            {
                                string[] ar = str4.Split( ',' );
                                for( int i = 0; i < 3; i++ )
                                {
                                    CDTXMania.ConfigIni.nJudgeStringBarHeight[ i ] = int.Parse( ar[ i ] );
                                }
                            }
                            else if (str3.Equals("ExplosionFrames"))
                            {
                                CDTXMania.ConfigIni.nExplosionFrames = C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, int.MaxValue, (int)CDTXMania.ConfigIni.nExplosionFrames);
                            }
                            else if (str3.Equals("ExplosionInterval"))
                            {
                                CDTXMania.ConfigIni.nExplosionInterval = C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, int.MaxValue, (int)CDTXMania.ConfigIni.nExplosionInterval);
                            }
                            else if (str3.Equals("ExplosionWidth"))
                            {
                                CDTXMania.ConfigIni.nExplosionWidth = C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, int.MaxValue, (int)CDTXMania.ConfigIni.nExplosionWidth);
                            }
                            else if (str3.Equals("ExplosionHeight"))
                            {
                                CDTXMania.ConfigIni.nExplosionHeight = C変換.n値を文字列から取得して範囲内に丸めて返す(str4, 0, int.MaxValue, (int)CDTXMania.ConfigIni.nExplosionHeight);
                            }
                            //else if (str3.Equals("WailingFireFrames"))
                            //{
                            //}
                            //else if (str3.Equals("WailingFireInterval"))
                            //{
                            //}
                            //else if (str3.Equals("WailingFireWidgh"))
                            //{
                            //}
                            //else if (str3.Equals("WailingFireHeight"))
                            //{
                            //}
                            //else if (str3.Equals("WailingFirePositionXGuitar"))
                            //{
                            //}
                            //else if (str3.Equals("WailingFirePositionXBass"))
                            //{
                            //}
                            //-----------------------------
                        }
                        continue;
                    }
                    catch (Exception exception)
                    {
                        Trace.TraceError(exception.Message);
                        continue;
                    }
                }
            }
        }

        private void tXMLから設定を読み込む()
        {
            if( File.Exists( CSkin.Path( "SkinConfig.xml" ) ) )
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.LoadXml( CSkin.Path( "SkinConfig.xml" ) );
                XmlElement xmleConfig = ( XmlElement )xmlConfig.SelectSingleNode( "" );
            }
        }

        // 2017.03.05 kairera0467
        // スキンを再読込する度に作り直す。
        public void CreateShutterList()
        {
            string strListCSV;
            bool bFileFound = false;
            this.listShutterImage = new List<CShutterImage>();

            //csvを読み込む
            if( File.Exists( CSkin.Path( @"..\\Common\\Shutter\\list.csv" ) ) )
            {
                StreamReader reader = new StreamReader( CSkin.Path( @"..\\Common\\Shutter\\list.csv" ) );
                strListCSV = reader.ReadToEnd();

                //設定していく。
                string[] delimiter = { "\n" };
                string[] strSingleLine = strListCSV.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );
                foreach( string s in strSingleLine )
                {
                    if( s.IndexOf( ";" ) != -1 ) //先頭文字が;の場合は無視
                        continue;
                    string str = s.Replace( '\r', ' ' );

                    //正常なら3個になる。
                    if( str.IndexOf(' ') != -1 )
                        str = str.Remove( str.IndexOf(' '), 1 );
                    string[] strArray = str.Split( ',' );

                    if( strArray.Length != 3 )
                        continue;
                    if( strArray[ 0 ] == "BLACK" ) //「BLACK」はシステム側で追加する名前なので無視する
                        continue;

                    var shutter = new CShutterImage();
                    shutter.strName = strArray[ 0 ];
                    shutter.strFilePathD = strArray[ 1 ];
                    shutter.strFilePathGB = strArray[ 2 ];

                    this.listShutterImage.Add( shutter );
                }
                reader.Close();

                //BLACK追加
                this.listShutterImage.Add( new CShutterImage() { strName = "BLACK" } );
            }
        }

        // 2017.03.30 kairera0467
        public string[] arGetShutterName()
        {
            //変数の用意
            string[] arNameList = new string[] { "BLACK" }; //csvファイルが存在しなかった時用

            //原始的だが仕方がない
            if( this.listShutterImage.Count != 0 )
            {
                string strList = "";
                for( int i = 0; i < this.listShutterImage.Count; i++ )
                {
                    strList += this.listShutterImage[i].strName;
                    
                    if( i < this.listShutterImage.Count - 1 )
                        strList += ",";
                }
                arNameList = strList.Split( ',' );
            }

            return arNameList;
        }

		// その他
        public bool b曲決定後に背景画像を変更する;
        public List<CShutterImage> listShutterImage;
		#region [ private ]
		//-----------------
		private bool bDisposed済み;
		//-----------------
		#endregion

	}

    public class CShutterImage
    {
        public bool bFileFound;
        public string strName;
        public string strFilePathD;
        public string strFilePathGB;

        public CShutterImage()
        {
            bFileFound = false;
            strName = "";
            strFilePathD = "";
            strFilePathGB = "";
        }
    }

    public class CAttackEffectImage
    {
        public bool bFileFound;
        public string strName;
        public string strFilePathD;
        public string strFilePathGB;

        public CAttackEffectImage()
        {
            bFileFound = false;
            strName = "";
            strFilePathD = "";
            strFilePathGB = "";
        }
    }

    public class CComboImage
    {
        public bool bFileFound;
        public string strName;
        public string strFilePathD;
        public string strFilePathGB;

        public CComboImage()
        {
            bFileFound = false;
            strName = "";
            strFilePathD = "";
            strFilePathGB = "";
        }
    }

    public class CJudgeStringImage
    {
        public bool bFileFound;
        public string strName;
        public string strFilePath;

        public CJudgeStringImage()
        {
            bFileFound = false;
            strName = "";
            strFilePath = "";
        }
    }
}
