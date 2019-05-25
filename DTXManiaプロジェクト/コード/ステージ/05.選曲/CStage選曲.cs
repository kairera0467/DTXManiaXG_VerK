using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using FDK;

namespace DTXMania
{
	internal class CStage選曲 : CStage
	{
		// プロパティ
		public int nスクロールバー相対y座標
		{
			get
			{
				if ( act曲リスト != null )
				{
					return act曲リスト.nスクロールバー相対y座標;
				}
				else
				{
					return 0;
				}
			}
		}
		public bool bIsEnumeratingSongs
		{
			get
			{
				return act曲リスト.bIsEnumeratingSongs;
			}
			set
			{
				act曲リスト.bIsEnumeratingSongs = value;
			}
		}
		public bool bスクロール中
		{
			get
			{
				return this.act曲リスト.bスクロール中;
			}
		}
		public int n確定された曲の難易度
		{
			get;
			private set;
		}
		public Cスコア r確定されたスコア
		{
			get;
			private set;
		}
		public C曲リストノード r確定された曲 
		{
			get;
			private set;
		}
		public int n現在選択中の曲の難易度
		{
			get
			{
				return this.act曲リスト.n現在選択中の曲の現在の難易度レベル;
			}
		}
		public Cスコア r現在選択中のスコア
		{
			get
			{
				return this.act曲リスト.r現在選択中のスコア;
			}
		}
		public C曲リストノード r現在選択中の曲
		{
			get
			{
				return this.act曲リスト.r現在選択中の曲;
			}
		}

		// コンストラクタ
		public CStage選曲()
		{
			base.eステージID = CStage.Eステージ.選曲;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.b活性化してない = true;
			base.list子Activities.Add( this.actオプションパネル = new CActオプションパネル() );
			base.list子Activities.Add( this.actFIFO = new CActFIFOBlack() );
			base.list子Activities.Add( this.actFIfrom結果画面 = new CActFIFOBlack() );
			base.list子Activities.Add( this.actFOtoNowLoading = new CActFIFOBlack() );	// #27787 2012.3.10 yyagi 曲決定時の画面フェードアウトの省略
			base.list子Activities.Add( this.act曲リスト = new CActSelect曲リスト共通() );
			base.list子Activities.Add( this.actステータスパネル = new CActSelectステータスパネル共通() );
			base.list子Activities.Add( this.act演奏履歴パネル = new CActSelect演奏履歴パネル() );
			base.list子Activities.Add( this.actPresound = new CActSelectPresound() );
			base.list子Activities.Add( this.actArtistComment = new CActSelectArtistComment() );
			base.list子Activities.Add( this.actInformation = new CActSelectInformation() );
			base.list子Activities.Add( this.actSortSongs = new CActSortSongs() );
			base.list子Activities.Add( this.actShowCurrentPosition = new CActSelectShowCurrentPosition共通() );
			base.list子Activities.Add( this.actQuickConfig = new CActSelectQuickConfig() );

			this.CommandHistory = new CCommandHistory();		// #24063 2011.1.16 yyagi
		}
		
		
		// メソッド

		public void t選択曲変更通知()
		{
			this.actPresound.t選択曲が変更された();
			this.act演奏履歴パネル.t選択曲が変更された();
			this.actステータスパネル.t選択曲が変更された();
			this.actArtistComment.t選択曲が変更された();

			#region [ プラグインにも通知する（BOX, RANDOM, BACK なら通知しない）]
			//---------------------
			if( CDTXMania.app != null )
			{
				var c曲リストノード = CDTXMania.stage選曲.r現在選択中の曲;
				var cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
                if( CDTXMania.bXGRelease ) {
                    c曲リストノード = CDTXMania.stage選曲XG.r現在選択中の曲;
                    cスコア = CDTXMania.stage選曲GITADORA.r現在選択中のスコア;
                } else {
                    c曲リストノード = CDTXMania.stage選曲GITADORA.r現在選択中の曲;
                    cスコア = CDTXMania.stage選曲GITADORA.r現在選択中のスコア;
                }

				if( c曲リストノード != null && cスコア != null && c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.SCORE )
				{
					string str選択曲ファイル名 = cスコア.ファイル情報.ファイルの絶対パス;
					CSetDef setDef = null;
					int nブロック番号inSetDef = -1;
					int n曲番号inブロック = -1;

					if( !string.IsNullOrEmpty( c曲リストノード.pathSetDefの絶対パス ) && File.Exists( c曲リストノード.pathSetDefの絶対パス ) )
					{
						setDef = new CSetDef( c曲リストノード.pathSetDefの絶対パス );
						nブロック番号inSetDef = c曲リストノード.SetDefのブロック番号;
                        if( CDTXMania.bXGRelease ) n曲番号inブロック = CDTXMania.stage選曲XG.act曲リスト.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( c曲リストノード );
                        else n曲番号inブロック = CDTXMania.stage選曲GITADORA.act曲リスト.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( c曲リストノード );
					}

					foreach( CDTXMania.STPlugin stPlugin in CDTXMania.app.listプラグイン )
					{
						Directory.SetCurrentDirectory( stPlugin.strプラグインフォルダ );
						stPlugin.plugin.On選択曲変更( str選択曲ファイル名, setDef, nブロック番号inSetDef, n曲番号inブロック );
						Directory.SetCurrentDirectory( CDTXMania.strEXEのあるフォルダ );
					}
				}
			}
			//---------------------
			#endregion
		}

		// CStage 実装

		/// <summary>
		/// 曲リストをリセットする
		/// </summary>
		/// <param name="cs"></param>
		public void Refresh( CSongs管理 cs, bool bRemakeSongTitleBar)
		{
			this.act曲リスト.Refresh( cs, bRemakeSongTitleBar );
		}

		public override void On活性化()
		{
			Trace.TraceInformation( "選曲ステージを活性化します。" );
			Trace.Indent();
			try
			{
				this.eフェードアウト完了時の戻り値 = E戻り値.継続;
				this.bBGM再生済み = false;
				for( int i = 0; i < 4; i++ )
					this.ctキー反復用[ i ] = new CCounter( 0, 0, 0, CDTXMania.Timer );
                this.ct決定演出待機 = new CCounter();
                this.b決定演出カウンタ使用中 = false;
				base.On活性化();

				this.actステータスパネル.t選択曲が変更された();	// 最大ランクを更新
			}
			finally
			{
				Trace.TraceInformation( "選曲ステージの活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void On非活性化()
		{
			Trace.TraceInformation( "選曲ステージを非活性化します。" );
			Trace.Indent();
			try
			{
				for( int i = 0; i < 4; i++ )
				{
					this.ctキー反復用[ i ] = null;
				}
                this.ct決定演出待機 = null;
				base.On非活性化();
			}
			finally
			{
				Trace.TraceInformation( "選曲ステージの非活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void OnManagedリソースの作成()
		{

		}
		public override void OnManagedリソースの解放()
		{

		}

		public enum E戻り値 : int
		{
			継続,
			タイトルに戻る,
			選曲した,
			オプション呼び出し,
			コンフィグ呼び出し,
			スキン変更
		}
		

		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		protected struct STキー反復用カウンタ
		{
			public CCounter Up;
			public CCounter Down;
			public CCounter R;
			public CCounter B;
			public CCounter this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Up;

						case 1:
							return this.Down;

						case 2:
							return this.R;

						case 3:
							return this.B;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.Up = value;
							return;

						case 1:
							this.Down = value;
							return;

						case 2:
							this.R = value;
							return;

						case 3:
							this.B = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}
		protected CActSelectArtistComment actArtistComment;
		protected CActFIFOBlack actFIFO;
		protected CActFIFOBlack actFIfrom結果画面;
		protected CActFIFOBlack actFOtoNowLoading;	// #27787 2012.3.10 yyagi 曲決定時の画面フェードアウトの省略
		protected CActSelectInformation actInformation;
		protected CActSelectPresound actPresound;
		protected CActオプションパネル actオプションパネル;
		public  CActSelectステータスパネル共通 actステータスパネル;
		protected CActSelect演奏履歴パネル act演奏履歴パネル;
		public  CActSelect曲リスト共通 act曲リスト;
		protected CActSelectShowCurrentPosition共通 actShowCurrentPosition;

		protected CActSortSongs actSortSongs;
		protected CActSelectQuickConfig actQuickConfig;

		protected bool bBGM再生済み;
		protected STキー反復用カウンタ ctキー反復用;
		public CCounter ct登場時アニメ用共通;
        public CCounter ct決定演出待機; //テスト
        protected bool b決定演出カウンタ使用中;
		protected E戻り値 eフェードアウト完了時の戻り値;


		protected struct STCommandTime		// #24063 2011.1.16 yyagi コマンド入力時刻の記録用
		{
			public E楽器パート eInst;		// 使用楽器
			public EパッドFlag ePad;		// 押されたコマンド(同時押しはOR演算で列挙する)
			public long time;				// コマンド入力時刻
		}
		protected class CCommandHistory		// #24063 2011.1.16 yyagi コマンド入力履歴を保持・確認するクラス
		{
			readonly int buffersize = 16;
			private List<STCommandTime> stct;

			public CCommandHistory()		// コンストラクタ
			{
				stct = new List<STCommandTime>( buffersize );
			}

			/// <summary>
			/// コマンド入力履歴へのコマンド追加
			/// </summary>
			/// <param name="_eInst">楽器の種類</param>
			/// <param name="_ePad">入力コマンド(同時押しはOR演算で列挙すること)</param>
			public void Add( E楽器パート _eInst, EパッドFlag _ePad )
			{
				STCommandTime _stct = new STCommandTime {
					eInst = _eInst,
					ePad = _ePad,
					time = CDTXMania.Timer.n現在時刻
				};

				if ( stct.Count >= buffersize )
				{
					stct.RemoveAt( 0 );
				}
				stct.Add(_stct);
//Debug.WriteLine( "CMDHIS: 楽器=" + _stct.eInst + ", CMD=" + _stct.ePad + ", time=" + _stct.time );
			}
			public void RemoveAt( int index )
			{
				stct.RemoveAt( index );
			}

			/// <summary>
			/// コマンド入力に成功しているか調べる
			/// </summary>
			/// <param name="_ePad">入力が成功したか調べたいコマンド</param>
			/// <param name="_eInst">対象楽器</param>
			/// <returns>コマンド入力成功時true</returns>
			public bool CheckCommand( EパッドFlag[] _ePad, E楽器パート _eInst)
			{
				int targetCount = _ePad.Length;
				int stciCount = stct.Count;
				if ( stciCount < targetCount )
				{
//Debug.WriteLine("NOT start checking...stciCount=" + stciCount + ", targetCount=" + targetCount);
					return false;
				}

				long curTime = CDTXMania.Timer.n現在時刻;
//Debug.WriteLine("Start checking...targetCount=" + targetCount);
				for ( int i = targetCount - 1, j = stciCount - 1; i >= 0; i--, j-- )
				{
					if ( _ePad[ i ] != stct[ j ].ePad )
					{
//Debug.WriteLine( "CMD解析: false targetCount=" + targetCount + ", i=" + i + ", j=" + j + ": ePad[]=" + _ePad[i] + ", stci[j] = " + stct[j].ePad );
						return false;
					}
					if ( stct[ j ].eInst != _eInst )
					{
//Debug.WriteLine( "CMD解析: false " + i );
						return false;
					}
					if ( curTime - stct[ j ].time > 500 )
					{
//Debug.WriteLine( "CMD解析: false " + i + "; over 500ms" );
						return false;
					}
					curTime = stct[ j ].time;
				}

//Debug.Write( "CMD解析: 成功!(" + _ePad.Length + ") " );
//for ( int i = 0; i < _ePad.Length; i++ ) Debug.Write( _ePad[ i ] + ", " );
//Debug.WriteLine( "" );
				//stct.RemoveRange( 0, targetCount );			// #24396 2011.2.13 yyagi 
				stct.Clear();									// #24396 2011.2.13 yyagi Clear all command input history in case you succeeded inputting some command

				return true;
			}
		}
		protected CCommandHistory CommandHistory;

		protected void tカーソルを下へ移動する()
		{
			CDTXMania.Skin.soundカーソル移動音.t再生する();
			this.act曲リスト.t次に移動();
		}
		protected void tカーソルを上へ移動する()
		{
			CDTXMania.Skin.soundカーソル移動音.t再生する();
			this.act曲リスト.t前に移動();
		}
		protected void t曲をランダム選択する()
		{
			C曲リストノード song = this.act曲リスト.r現在選択中の曲;
			if( ( song.stackランダム演奏番号.Count == 0 ) || ( song.listランダム用ノードリスト == null ) )
			{
				if( song.listランダム用ノードリスト == null )
				{
					song.listランダム用ノードリスト = this.t指定された曲が存在する場所の曲を列挙する_子リスト含む( song );
				}
				int count = song.listランダム用ノードリスト.Count;
				if( count == 0 )
				{
					return;
				}
				int[] numArray = new int[ count ];
				for( int i = 0; i < count; i++ )
				{
					numArray[ i ] = i;
				}
				for( int j = 0; j < ( count * 1.5 ); j++ )
				{
					int index = CDTXMania.Random.Next( count );
					int num5 = CDTXMania.Random.Next( count );
					int num6 = numArray[ num5 ];
					numArray[ num5 ] = numArray[ index ];
					numArray[ index ] = num6;
				}
				for( int k = 0; k < count; k++ )
				{
					song.stackランダム演奏番号.Push( numArray[ k ] );
				}
				if( CDTXMania.ConfigIni.bLogDTX詳細ログ出力 )
				{
					StringBuilder builder = new StringBuilder( 1024 );
					builder.Append( string.Format( "ランダムインデックスリストを作成しました: {0}曲: ", song.stackランダム演奏番号.Count ) );
					for( int m = 0; m < count; m++ )
					{
						builder.Append( string.Format( "{0} ", numArray[ m ] ) );
					}
					Trace.TraceInformation( builder.ToString() );
				}
			}
			this.r確定された曲 = song.listランダム用ノードリスト[ song.stackランダム演奏番号.Pop() ];
			this.n確定された曲の難易度 = this.act曲リスト.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( this.r確定された曲 );
			this.r確定されたスコア = this.r確定された曲.arスコア[ this.n確定された曲の難易度 ];
			this.eフェードアウト完了時の戻り値 = E戻り値.選曲した;
		//	this.actFOtoNowLoading.tフェードアウト開始();					// #27787 2012.3.10 yyagi 曲決定時の画面フェードアウトの省略
#if animetest
            base.eフェーズID = CStage.Eフェーズ.選曲_決定演出;
#else
			base.eフェーズID = CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト;
#endif
			if( CDTXMania.ConfigIni.bLogDTX詳細ログ出力 )
			{
				int[] numArray2 = song.stackランダム演奏番号.ToArray();
				StringBuilder builder2 = new StringBuilder( 0x400 );
				builder2.Append( "ランダムインデックスリスト残り: " );
				if( numArray2.Length > 0 )
				{
					for( int n = 0; n < numArray2.Length; n++ )
					{
						builder2.Append( string.Format( "{0} ", numArray2[ n ] ) );
					}
				}
				else
				{
					builder2.Append( "(なし)" );
				}
				Trace.TraceInformation( builder2.ToString() );
			}
			CDTXMania.Skin.bgm選曲画面.t停止する();
		}
		protected void t曲を選択する()
		{
			this.r確定された曲 = this.act曲リスト.r現在選択中の曲;
			this.r確定されたスコア = this.act曲リスト.r現在選択中のスコア;
			this.n確定された曲の難易度 = this.act曲リスト.n現在選択中の曲の現在の難易度レベル;
			if( ( this.r確定された曲 != null ) && ( this.r確定されたスコア != null ) )
			{
				this.eフェードアウト完了時の戻り値 = E戻り値.選曲した;
			//	this.actFOtoNowLoading.tフェードアウト開始();				// #27787 2012.3.10 yyagi 曲決定時の画面フェードアウトの省略
                if( CDTXMania.bXGRelease )
                {
#if animetest
                    base.eフェーズID = CStage.Eフェーズ.選曲_決定演出;
#else
                    base.eフェーズID = CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト;
#endif
                }
                else
                {
			    	this.actFOtoNowLoading.tフェードアウト開始();
                    base.eフェーズID = CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト;
                }
			}
			CDTXMania.Skin.bgm選曲画面.t停止する();
		}
        protected void t決定アニメーション()
        {
            //決定からの演出
            if( this.b決定演出カウンタ使用中 )
            {
                this.ct決定演出待機.t進行();
                if( this.ct決定演出待機.b終了値に達した )
                {
                    this.ct決定演出待機.t停止();
                    this.b決定演出カウンタ使用中 = false;
                }
            }

            if( base.eフェーズID == Eフェーズ.選曲_決定演出 )
            {
                if( this.ct決定演出待機.b進行中 )
                {
                    this.actPresound.tサウンド停止();
                    //CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.白, "MUSIC DECIDED." );
                    //CDTXMania.act文字コンソール.tPrint( 0, 16, C文字コンソール.Eフォント種別.白, "PLEASE WAIT  " + this.ct決定演出待機.n現在の値.ToString() );
                }

                if( this.ct決定演出待機.b終了値に達した )
                {
                    this.t決定アニメーション終了();
                }
            }
        }
        protected void t決定アニメーション開始()
        {
            if( !this.b決定演出カウンタ使用中 )
            {
                this.b決定演出カウンタ使用中 = true;
                this.ct決定演出待機 = new CCounter( 0, 6000, 1, CDTXMania.Timer );
            }
        }
        protected void t決定アニメーション終了()
        {
            //アニメーション終了後の処理。
            this.actFOtoNowLoading.tフェードアウト開始();
            base.eフェーズID = CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト;
        }
		private List<C曲リストノード> t指定された曲が存在する場所の曲を列挙する_子リスト含む( C曲リストノード song )
		{
			List<C曲リストノード> list = new List<C曲リストノード>();
			song = song.r親ノード;
			if( ( song == null ) && ( CDTXMania.Songs管理.list曲ルート.Count > 0 ) )
			{
				foreach( C曲リストノード c曲リストノード in CDTXMania.Songs管理.list曲ルート )
				{
					if( ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.SCORE ) || ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.SCORE_MIDI ) )
					{
						list.Add( c曲リストノード );
					}
					if( ( c曲リストノード.list子リスト != null ) && CDTXMania.ConfigIni.bランダムセレクトで子BOXを検索対象とする )
					{
						this.t指定された曲の子リストの曲を列挙する_孫リスト含む( c曲リストノード, ref list );
					}
				}
				return list;
			}
			this.t指定された曲の子リストの曲を列挙する_孫リスト含む( song, ref list );
			return list;
		}
		private void t指定された曲の子リストの曲を列挙する_孫リスト含む( C曲リストノード r親, ref List<C曲リストノード> list )
		{
			if( ( r親 != null ) && ( r親.list子リスト != null ) )
			{
				foreach( C曲リストノード c曲リストノード in r親.list子リスト )
				{
					if( ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.SCORE ) || ( c曲リストノード.eノード種別 == C曲リストノード.Eノード種別.SCORE_MIDI ) )
					{
						list.Add( c曲リストノード );
					}
					if( ( c曲リストノード.list子リスト != null ) && CDTXMania.ConfigIni.bランダムセレクトで子BOXを検索対象とする )
					{
						this.t指定された曲の子リストの曲を列挙する_孫リスト含む( c曲リストノード, ref list );
					}
				}
			}
		}
		//-----------------
		#endregion
	}
}
