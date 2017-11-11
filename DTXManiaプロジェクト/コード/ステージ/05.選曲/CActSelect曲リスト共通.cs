using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;

using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActSelect曲リスト共通 : CActivity
	{
		// プロパティ

		public bool bIsEnumeratingSongs
		{
			get;
			set;
		}
		public bool bスクロール中
		{
			get
			{
				if( this.n目標のスクロールカウンタ == 0 )
				{
					return ( this.n現在のスクロールカウンタ != 0 );
				}
				return true;
			}
		}
		public int n現在のアンカ難易度レベル 
		{
			get;
			set;
		}
		public int n現在選択中の曲の現在の難易度レベル
		{
			get
			{
				return this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( this.r現在選択中の曲 );
			}
		}
		public Cスコア r現在選択中のスコア
		{
			get
			{
				if( this.r現在選択中の曲 != null )
				{
					return this.r現在選択中の曲.arスコア[ this.n現在選択中の曲の現在の難易度レベル ];
				}
				return null;
			}
		}
		public C曲リストノード r現在選択中の曲 
		{
			get;
			set;
		}

		public int nスクロールバー相対y座標
		{
			get;
			protected set;
		}

		// t選択曲が変更された()内で使う、直前の選曲の保持
		// (前と同じ曲なら選択曲変更に掛かる再計算を省略して高速化するため)
		public C曲リストノード song_last = null;

		
		// コンストラクタ

		public CActSelect曲リスト共通()
		{
			this.r現在選択中の曲 = null;
			this.n現在のアンカ難易度レベル = 0;
			base.b活性化してない = true;
			this.bIsEnumeratingSongs = false;

            this.stパネルマップ = null;
            this.stパネルマップ = new STATUSPANEL[ 12 ];		// yyagi: 以下、手抜きの初期化でスマン
            string[] labels = new string[ 12 ] {
            "DTXMANIA",     //0
            "DEBUT",        //1
            "NOVICE",       //2
            "REGULAR",      //3
            "EXPERT",       //4
            "MASTER",       //5
            "BASIC",        //6
            "ADVANCED",     //7
            "EXTREME",      //8
            "RAW",          //9
            "RWS",          //10
            "REAL"          //11
            };
            int[] status = new int[ 12 ] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            for( int i = 0; i < 12; i++ )
            {
                this.stパネルマップ[ i ] = default(STATUSPANEL);
                this.stパネルマップ[ i ].status = status[i];
                this.stパネルマップ[ i ].label = labels[i];
            }
		}


		// メソッド

		public int n現在のアンカ難易度レベルに最も近い難易度レベルを返す( C曲リストノード song )
		{
			// 事前チェック。

			if( song == null )
				return this.n現在のアンカ難易度レベル;	// 曲がまったくないよ

			if( song.arスコア[ this.n現在のアンカ難易度レベル ] != null )
				return this.n現在のアンカ難易度レベル;	// 難易度ぴったりの曲があったよ

			if( ( song.eノード種別 == C曲リストノード.Eノード種別.BOX ) || ( song.eノード種別 == C曲リストノード.Eノード種別.BACKBOX ) )
				return 0;								// BOX と BACKBOX は関係無いよ


			// 現在のアンカレベルから、難易度上向きに検索開始。

			int n最も近いレベル = this.n現在のアンカ難易度レベル;

			for( int i = 0; i < 5; i++ )
			{
				if( song.arスコア[ n最も近いレベル ] != null )
					break;	// 曲があった。

				n最も近いレベル = ( n最も近いレベル + 1 ) % 5;	// 曲がなかったので次の難易度レベルへGo。（5以上になったら0に戻る。）
			}


			// 見つかった曲がアンカより下のレベルだった場合……
			// アンカから下向きに検索すれば、もっとアンカに近い曲があるんじゃね？

			if( n最も近いレベル < this.n現在のアンカ難易度レベル )
			{
				// 現在のアンカレベルから、難易度下向きに検索開始。

				n最も近いレベル = this.n現在のアンカ難易度レベル;

				for( int i = 0; i < 5; i++ )
				{
					if( song.arスコア[ n最も近いレベル ] != null )
						break;	// 曲があった。

					n最も近いレベル = ( ( n最も近いレベル - 1 ) + 5 ) % 5;	// 曲がなかったので次の難易度レベルへGo。（0未満になったら4に戻る。）
				}
			}

			return n最も近いレベル;
		}
		public C曲リストノード r指定された曲が存在するリストの先頭の曲( C曲リストノード song )
		{
			List<C曲リストノード> songList = GetSongListWithinMe( song );
			return ( songList == null ) ? null : songList[ 0 ];
		}
		public C曲リストノード r指定された曲が存在するリストの末尾の曲( C曲リストノード song )
		{
			List<C曲リストノード> songList = GetSongListWithinMe( song );
			return ( songList == null ) ? null : songList[ songList.Count - 1 ];
		}

		private List<C曲リストノード> GetSongListWithinMe( C曲リストノード song )
		{
			if ( song.r親ノード == null )					// root階層のノートだったら
			{
				return CDTXMania.Songs管理.list曲ルート;	// rootのリストを返す
			}
			else
			{
				if ( ( song.r親ノード.list子リスト != null ) && ( song.r親ノード.list子リスト.Count > 0 ) )
				{
					return song.r親ノード.list子リスト;
				}
				else
				{
					return null;
				}
			}
		}


		public delegate void DGSortFunc( List<C曲リストノード> songList, E楽器パート eInst, int order, params object[] p);
		/// <summary>
		/// 主にCSong管理.cs内にあるソート機能を、delegateで呼び出す。
		/// </summary>
		/// <param name="sf">ソート用に呼び出すメソッド</param>
		/// <param name="eInst">ソート基準とする楽器</param>
		/// <param name="order">-1=降順, 1=昇順</param>
		public void t曲リストのソート( DGSortFunc sf, E楽器パート eInst, int order, params object[] p )
		{
			List<C曲リストノード> songList = GetSongListWithinMe( this.r現在選択中の曲 );
			if ( songList == null )
			{
				// 何もしない;
			}
			else
			{
//				CDTXMania.Songs管理.t曲リストのソート3_演奏回数の多い順( songList, eInst, order );
				sf( songList, eInst, order, p );
//				this.r現在選択中の曲 = CDTXMania
				this.tバーの初期化();
			}
		}

		public bool tBOXに入る()
		{
//Trace.TraceInformation( "box enter" );
//Trace.TraceInformation( "Skin現在Current : " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) );
//Trace.TraceInformation( "Skin現在System  : " + CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在BoxDef  : " + CSkin.strBoxDefSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) );
//Trace.TraceInformation( "Skin現pt: " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) );
//Trace.TraceInformation( "Skin指定: " + CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) );
//Trace.TraceInformation( "Skinpath: " + this.r現在選択中の曲.strSkinPath );
			bool ret = false;
			if ( CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) != CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath )
				&& CSkin.bUseBoxDefSkin )
			{
				ret = true;
				// BOXに入るときは、スキン変更発生時のみboxdefスキン設定の更新を行う
				CDTXMania.Skin.SetCurrentSkinSubfolderFullName( this.r現在選択中の曲.strSkinPath, false );
			}

//Trace.TraceInformation( "Skin変更: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) );
//Trace.TraceInformation( "Skin変更Current : " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) );
//Trace.TraceInformation( "Skin変更System  : " + CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "Skin変更BoxDef  : " + CSkin.strBoxDefSkinSubfolderFullName );

			if( ( this.r現在選択中の曲.list子リスト != null ) && ( this.r現在選択中の曲.list子リスト.Count > 0 ) )
			{
				this.r現在選択中の曲 = this.r現在選択中の曲.list子リスト[ 0 ];
                this.tバーの初期化();
				this.t選択曲が変更された(false);									// #27648 項目数変更を反映させる
			}
			return ret;
		}
		public bool tBOXを出る()
		{
//Trace.TraceInformation( "box exit" );
//Trace.TraceInformation( "Skin現在Current : " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "Skin現在System  : " + CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在BoxDef  : " + CSkin.strBoxDefSkinSubfolderFullName );
//Trace.TraceInformation( "Skin現在: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) ) );
//Trace.TraceInformation( "Skin現pt: " + CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "Skin指定: " + CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) );
//Trace.TraceInformation( "Skinpath: " + this.r現在選択中の曲.strSkinPath );
			bool ret = false;
			if ( CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName( false ) ) != CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath )
				&& CSkin.bUseBoxDefSkin )
			{
				ret = true;
			}
			// スキン変更が発生しなくても、boxdef圏外に出る場合は、boxdefスキン設定の更新が必要
			// (ユーザーがboxdefスキンをConfig指定している場合への対応のために必要)
			// tBoxに入る()とは処理が微妙に異なるので注意
			CDTXMania.Skin.SetCurrentSkinSubfolderFullName(
				( this.r現在選択中の曲.strSkinPath == "" ) ? "" : CDTXMania.Skin.GetSkinSubfolderFullNameFromSkinName( CSkin.GetSkinName( this.r現在選択中の曲.strSkinPath ) ), false );
//Trace.TraceInformation( "SKIN変更: " + CSkin.GetSkinName( CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) ) );
//Trace.TraceInformation( "SKIN変更Current : "+  CDTXMania.Skin.GetCurrentSkinSubfolderFullName(false) );
//Trace.TraceInformation( "SKIN変更System  : "+  CSkin.strSystemSkinSubfolderFullName );
//Trace.TraceInformation( "SKIN変更BoxDef  : "+  CSkin.strBoxDefSkinSubfolderFullName );
			if ( this.r現在選択中の曲.r親ノード != null )
			{
				this.r現在選択中の曲 = this.r現在選択中の曲.r親ノード;
                this.tバーの初期化();
				this.t選択曲が変更された(false);									// #27648 項目数変更を反映させる
			}
			return ret;
		}
		public void t次に移動()
		{
			if( this.r現在選択中の曲 != null )
			{
				this.n目標のスクロールカウンタ += 100;
			}
		}
		public void t前に移動()
		{
			if( this.r現在選択中の曲 != null )
			{
				this.n目標のスクロールカウンタ -= 100;
			}
		}
		public void t難易度レベルをひとつ進める()
		{
			if( ( this.r現在選択中の曲 == null ) || ( this.r現在選択中の曲.nスコア数 <= 1 ) )
				return;		// 曲にスコアが０～１個しかないなら進める意味なし。
			

			// 難易度レベルを＋１し、現在選曲中のスコアを変更する。

			this.n現在のアンカ難易度レベル = this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( this.r現在選択中の曲 );

			for( int i = 0; i < 5; i++ )
			{
				this.n現在のアンカ難易度レベル = ( this.n現在のアンカ難易度レベル + 1 ) % 5;	// ５以上になったら０に戻る。
				if( this.r現在選択中の曲.arスコア[ this.n現在のアンカ難易度レベル ] != null )	// 曲が存在してるならここで終了。存在してないなら次のレベルへGo。
					break;
			}


			// 曲毎に表示しているスキル値を、新しい難易度レベルに合わせて取得し直す。（表示されている13曲全部。）

			C曲リストノード song = this.r現在選択中の曲;
			for( int i = 0; i < 5; i++ )
				song = this.r前の曲( song );

			for( int i = this.n現在の選択行 - 5; i < ( ( this.n現在の選択行 - 5 ) + 15 ); i++ )
			{
				int index = ( i + 15 ) % 15;
				for( int m = 0; m < 3; m++ )
				{
					this.stバー情報[ index ].nスキル値[ m ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ m ];
				}
				song = this.r次の曲( song );
			}

            this.tラベル名からステータスパネルを決定する( this.r現在選択中の曲.ar難易度ラベル[ this.n現在選択中の曲の現在の難易度レベル ] );
            this.tラベル名に応じてサウンドを再生する( 0 );
            //CDTXMania.stage選曲.actステータスパネル.ct難易度変更カウンター.n現在の値 = 1;

			// 選曲ステージに変更通知を発出し、関係Activityの対応を行ってもらう。
            
            if( CDTXMania.bXGRelease ) CDTXMania.stage選曲XG.t選択曲変更通知();
            else CDTXMania.stage選曲GITADORA.t選択曲変更通知();
			//CDTXMania.stage選曲.t選択曲変更通知();
		}

        public void tラベル名からステータスパネルを決定する( string strラベル名 )
        {
            if (string.IsNullOrEmpty(strラベル名))
            {
                this.nIndex = 0;
            }
            else
            {
                STATUSPANEL[] array = this.stパネルマップ;
                for (int i = 0; i < array.Length; i++)
                {
                    STATUSPANEL sTATUSPANEL = array[i];
                    if (strラベル名.Equals(sTATUSPANEL.label, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.nIndex = sTATUSPANEL.status;
                        return;
                    }
                    this.nIndex++;
                }
            }
        }

        public void tラベル名に応じてサウンドを再生する( int nIndex )
        {
            switch( this.nIndex )
            {
                //case 2:
                //    CDTXMania.Skin.soundNovice.t再生する();
                //    string strnov = CSkin.Path( @"Sounds\Novice.ogg" );
                //    if( !File.Exists( strnov ) )
                //        CDTXMania.Skin.sound変更音.t再生する();
                //    break;

                //case 3:
                //    CDTXMania.Skin.soundRegular.t再生する();
                //    string strreg = CSkin.Path( @"Sounds\Regular.ogg" );
                //    if( !File.Exists( strreg ) )
                //        CDTXMania.Skin.sound変更音.t再生する();
                //    break;

                //case 4:
                //    CDTXMania.Skin.soundExpert.t再生する();
                //    string strexp = CSkin.Path( @"Sounds\Expert.ogg" );
                //    if( !File.Exists( strexp ) )
                //        CDTXMania.Skin.sound変更音.t再生する();
                //    break;

                //case 5:
                //    CDTXMania.Skin.soundMaster.t再生する();
                //    string strmas = CSkin.Path( @"Sounds\Master.ogg" );
                //    if( !File.Exists( strmas ) )
                //        CDTXMania.Skin.sound変更音.t再生する();
                //    break;
                
                //case 6:
                //    CDTXMania.Skin.soundBasic.t再生する();
                //    string strbsc = CSkin.Path( @"Sounds\Basic.ogg" );
                //    if( !File.Exists( strbsc ) )
                //        CDTXMania.Skin.sound変更音.t再生する();
                //    break;

                //case 7:
                //    CDTXMania.Skin.soundAdvanced.t再生する();
                //    string stradv = CSkin.Path( @"Sounds\Advanced.ogg" );
                //    if( !File.Exists( stradv ) )
                //        CDTXMania.Skin.sound変更音.t再生する();
                //    break;

                //case 8:
                //    CDTXMania.Skin.soundExtreme.t再生する();
                //    string strext = CSkin.Path( @"Sounds\Extreme.ogg" );
                //    if( !File.Exists( strext ) )
                //        CDTXMania.Skin.sound変更音.t再生する();
                //    break;

                default:
                    CDTXMania.Skin.sound変更音.t再生する();
                    break;
            }
        }

		/// <summary>
		/// 曲リストをリセットする
		/// </summary>
		/// <param name="cs"></param>
		public void Refresh(CSongs管理 cs, bool bRemakeSongTitleBar )		// #26070 2012.2.28 yyagi
		{
//			this.On非活性化();

			if ( cs != null && cs.list曲ルート.Count > 0 )	// 新しい曲リストを検索して、1曲以上あった
			{
				CDTXMania.Songs管理 = cs;

				if ( this.r現在選択中の曲 != null )			// r現在選択中の曲==null とは、「最初songlist.dbが無かった or 検索したが1曲もない」
				{
					this.r現在選択中の曲 = searchCurrentBreadcrumbsPosition( CDTXMania.Songs管理.list曲ルート, this.r現在選択中の曲.strBreadcrumbs );
					if ( bRemakeSongTitleBar )					// 選曲画面以外に居るときには再構成しない (非活性化しているときに実行すると例外となる)
					{
						this.tバーの初期化();
					}
#if false			// list子リストの中まではmatchしてくれないので、検索ロジックは手書きで実装 (searchCurrentBreadcrumbs())
					string bc = this.r現在選択中の曲.strBreadcrumbs;
					Predicate<C曲リストノード> match = delegate( C曲リストノード c )
					{
						return ( c.strBreadcrumbs.Equals( bc ) );
					};
					int nMatched = CDTXMania.Songs管理.list曲ルート.FindIndex( match );

					this.r現在選択中の曲 = ( nMatched == -1 ) ? null : CDTXMania.Songs管理.list曲ルート[ nMatched ];
					this.t現在選択中の曲を元に曲バーを再構成する();
#endif
					return;
				}
			}
			this.On非活性化();
			this.r現在選択中の曲 = null;
			this.On活性化();
		}


		/// <summary>
		/// 現在選曲している位置を検索する
		/// (曲一覧クラスを新しいものに入れ替える際に用いる)
		/// </summary>
		/// <param name="ln">検索対象のList</param>
		/// <param name="bc">検索するパンくずリスト(文字列)</param>
		/// <returns></returns>
		private C曲リストノード searchCurrentBreadcrumbsPosition( List<C曲リストノード> ln, string bc )
		{
			foreach (C曲リストノード n in ln)
			{
				if ( n.strBreadcrumbs == bc )
				{
					return n;
				}
				else if ( n.list子リスト != null && n.list子リスト.Count > 0 )	// 子リストが存在するなら、再帰で探す
				{
					C曲リストノード r = searchCurrentBreadcrumbsPosition( n.list子リスト, bc );
					if ( r != null ) return r;
				}
			}
			return null;
		}

		/// <summary>
		/// BOXのアイテム数と、今何番目を選択しているかをセットする
		/// </summary>
		public void t選択曲が変更された( bool bForce )	// #27648
		{
			C曲リストノード song = CDTXMania.stage選曲.r現在選択中の曲;
			if ( song == null )
				return;
			if ( song == song_last && bForce == false )
				return;
				
			song_last = song;
			List<C曲リストノード> list = ( song.r親ノード != null ) ? song.r親ノード.list子リスト : CDTXMania.Songs管理.list曲ルート;
			int index = list.IndexOf( song ) + 1;
			if ( index <= 0 )
			{
				nCurrentPosition = nNumOfItems = 0;
			}
			else
			{
				nCurrentPosition = index;
				nNumOfItems = list.Count;
			}

            if( this.tx選択されている曲の曲名 != null )
            {
                this.tx選択されている曲の曲名.Dispose();
                this.tx選択されている曲の曲名 = null;
            }
            if( this.tx選択されている曲のアーティスト名 != null )
            {
                this.tx選択されている曲のアーティスト名.Dispose();
                this.tx選択されている曲のアーティスト名 = null;
            }
		}

		// CActivity 実装

		public override void On活性化()
		{
			if( this.b活性化してる )
				return;
            
			this.e楽器パート = E楽器パート.DRUMS;
			this.b登場アニメ全部完了 = false;
			this.n目標のスクロールカウンタ = 0;
			this.n現在のスクロールカウンタ = 0;
			this.nスクロールタイマ = -1;

			// フォント作成。
			// 曲リスト文字は２倍（面積４倍）でテクスチャに描画してから縮小表示するので、フォントサイズは２倍とする。

			FontStyle regular = FontStyle.Regular;
			if( CDTXMania.ConfigIni.b選曲リストフォントを斜体にする ) regular |= FontStyle.Italic;
			if( CDTXMania.ConfigIni.b選曲リストフォントを太字にする ) regular |= FontStyle.Bold;
			this.ft曲リスト用フォント = new Font(
				CDTXMania.ConfigIni.str選曲リストフォント,
				(float) ( CDTXMania.ConfigIni.n選曲リストフォントのサイズdot * 2 * Scale.Y ),		// 後でScale.Yを掛けないように直すこと(Config.ini初期値変更)
				regular,
				GraphicsUnit.Pixel
			);
            FontStyle fStyle = CDTXMania.ConfigIni.b選曲リストフォントを太字にする ? FontStyle.Bold : FontStyle.Regular;
            this.prvPanelFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 18, fStyle );

			// 現在選択中の曲がない（＝はじめての活性化）なら、現在選択中の曲をルートの先頭ノードに設定する。

			if( ( this.r現在選択中の曲 == null ) && ( CDTXMania.Songs管理.list曲ルート.Count > 0 ) )
				this.r現在選択中の曲 = CDTXMania.Songs管理.list曲ルート[ 0 ];


			// バー情報を初期化する。

			this.tバーの初期化();

			base.On活性化();

			this.t選択曲が変更された(true);		// #27648 2012.3.31 yyagi 選曲画面に入った直後の 現在位置/全アイテム数 の表示を正しく行うため
		}
		public override void On非活性化()
		{
			if( this.b活性化してない )
				return;

			CDTXMania.t安全にDisposeする( ref this.ft曲リスト用フォント );

			for( int i = 0; i < 15; i++ )
				this.ct登場アニメ用[ i ] = null;

			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( this.b活性化してない )
				return;
            
            this.tx選曲パネル = null;
            if( CDTXMania.ConfigIni.bDrums有効 )
                this.tx選曲パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_image_panel.png" ) );
            else
                this.tx選曲パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_image_panel_guitar.png" ) );

            this.txパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_music panel.png") );
            this.txパネル帯 = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\5_Backbar.png" ) );
            this.txクリアランプ = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_Clearlamp.png") );
            #region[ テクスチャの復元 ]
            int nKeys = this.dicThumbnail.Count;
            string[] keys = new string[ nKeys ];
            this.dicThumbnail.Keys.CopyTo( keys, 0 );
            foreach (var key in keys)
                this.dicThumbnail[ key ] = this.tパスを指定してサムネイル画像を生成して返す( 0, key, this.stバー情報[ 0 ].eバー種別  );;

            //ここは最初に表示される画像の復元に必要。
            for( int i = 0; i < 15; i++ )
            {
                this.tパネルの生成( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].strアーティスト名, this.stバー情報[ i ].col文字色 );
                if( this.stバー情報[ i ].strPreimageのパス != null )
                {
                    if( !this.dicThumbnail.ContainsKey( this.stバー情報[ i ].strPreimageのパス ) )
                    {
                        this.tパスを指定してサムネイル画像を生成する( i, this.stバー情報[ i ].strPreimageのパス, this.stバー情報[ i ].eバー種別  );
                        this.dicThumbnail.Add( this.stバー情報[ i ].strPreimageのパス, this.txTumbnail[ i ] );
                    }
                    this.txTumbnail[ i ] = this.dicThumbnail[ this.stバー情報[ i ].strPreimageのパス ];
                }
            }
            #endregion

			int c = ( CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja" ) ? 0 : 1;
			#region [ Songs not found画像 ]
			try
			{
				using( Bitmap image = new Bitmap( 1024, 512 ) ) //2016.03.05 kairera0467 サイズを2の乗数に変更。(わざわざ横1280にするのは無駄なので。)
				using( Graphics graphics = Graphics.FromImage( image ) )
				{
					string[] s1 = { "曲データが見つかりません。", "Songs not found." };
					string[] s2 = { "曲データをDTXManiaGR.exe以下の", "You need to install songs." };
					string[] s3 = { "フォルダにインストールして下さい。", "" };
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) (2f * Scale.X), (float) (2f * Scale.Y) );
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) 0f );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) (2f * Scale.X), (float) (44f * Scale.Y) );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) (42f * Scale.Y) );
					graphics.DrawString( s3[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) (2f * Scale.X), (float) (86f * Scale.Y) );
					graphics.DrawString( s3[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) (84f * Scale.Y) );

					this.txSongNotFound = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );

					this.txSongNotFound.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );	// 半分のサイズで表示する。
				}
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "SoungNotFoundテクスチャの作成に失敗しました。" );
				this.txSongNotFound = null;
			}
			#endregion
			#region [ "曲データを検索しています"画像 ]
			try
			{
				using ( Bitmap image = new Bitmap( SampleFramework.GameWindowSize.Width, (int)(96 * Scale.Y) ) )
				using ( Graphics graphics = Graphics.FromImage( image ) )
				{
					string[] s1 = { "曲データを検索しています。", "Now enumerating songs." };
					string[] s2 = { "そのまましばらくお待ち下さい。", "Please wait..." };
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) (2f * Scale.X), (float) (2f * Scale.Y) );
					graphics.DrawString( s1[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) 0f );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.DarkGray, (float) (2f * Scale.X), (float) (44f * Scale.Y) );
					graphics.DrawString( s2[c], this.ft曲リスト用フォント, Brushes.White, (float) 0f, (float) (42f * Scale.Y) );

					this.txEnumeratingSongs = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );

					this.txEnumeratingSongs.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );	// 半分のサイズで表示する。
				}
			}
			catch ( CTextureCreateFailedException )
			{
				Trace.TraceError( "txEnumeratingSongsテクスチャの作成に失敗しました。" );
				this.txEnumeratingSongs = null;
			}
			#endregion
			base.OnManagedリソースの作成();
		}
		public override void OnManagedリソースの解放()
		{
			if( this.b活性化してない )
				return;

            for ( int i = 0; i < 15; i++ )
            {
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txタイトル名 );
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txアーティスト名 );
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ i ].txパネル );
            }
            #region[ ジャケット画像の解放 ]
            int nKeys = this.dicThumbnail.Count;
            string[] keys = new string[ nKeys ];
            this.dicThumbnail.Keys.CopyTo( keys, 0 );
            foreach( var key in keys )
            {
                C共通.tDisposeする( this.dicThumbnail[ key ] );
                this.dicThumbnail[ key ] = null;
            }
            #endregion

            CDTXMania.t安全にDisposeする( ref this.tx選択されている曲の曲名 );
            CDTXMania.t安全にDisposeする( ref this.tx選択されている曲のアーティスト名 );
            CDTXMania.t安全にDisposeする( ref this.tx選択されている曲のジャケット画像 );

			CDTXMania.t安全にDisposeする( ref this.txEnumeratingSongs );
			CDTXMania.t安全にDisposeする( ref this.txSongNotFound );
            CDTXMania.t安全にDisposeする( ref this.tx選曲パネル );
            CDTXMania.t安全にDisposeする( ref this.txパネル );
            CDTXMania.t安全にDisposeする( ref this.txクリアランプ );
            CDTXMania.t安全にDisposeする( ref this.txパネル帯 );
            CDTXMania.t安全にDisposeする( ref this.txベース曲パネル );
            CDTXMania.t安全にDisposeする( ref this.prvPanelFont );

			base.OnManagedリソースの解放();
		}
		public override int On進行描画()
		{
            #region [ スクロール地点の計算(描画はCActSelectShowCurrentPositionにて行う) #27648 ]
            int py;
			double d = 0;
			if ( nNumOfItems > 1 )
			{
				d = ( 336 - 8 ) / (double) ( nNumOfItems - 1 );
				py = (int) ( d * ( nCurrentPosition - 1 ) );
			}
			else
			{
				d = 0;
				py = 0;
			}
			int delta = (int) ( d * this.n現在のスクロールカウンタ / 100 );
			if ( py + delta <= 336 - 8 )
			{
				this.nスクロールバー相対y座標 = py + delta;
			}
			#endregion
			return 0;
		}
		
    	// その他

		#region [ private ]
		//-----------------
        public enum Eバー種別 { Score, Box, Other, Random, BackBox }

        public struct STバー
        {
            public CTexture Score;
            public CTexture Box;
            public CTexture Other;
            public CTexture Random;
            public CTexture BackBox;
            public CTexture this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0:
                            return this.Score;

                        case 1:
                            return this.Box;

                        case 2:
                            return this.Other;

                        case 3:
                            return this.Random;

                        case 4:
                            return this.BackBox;
                    }
                    throw new IndexOutOfRangeException();
                }
                set
                {
                    switch (index)
                    {
                        case 0:
                            this.Score = value;
                            return;

                        case 1:
                            this.Box = value;
                            return;

                        case 2:
                            this.Other = value;
                            return;

                        case 3:
                            this.Random = value;
                            return;

                        case 4:
                            this.BackBox = value;
                            return;
                    }
                    throw new IndexOutOfRangeException();
                }
            }
        }

		public struct STバー情報
		{
            public CActSelect曲リスト共通.Eバー種別 eバー種別;
            public string strタイトル文字列;
            public string strアーティスト名;
            public CTexture txタイトル名;
            public CTexture txアーティスト名;
            public CTexture txパネル;
            public int nアーティスト名テクスチャの長さdot;
            public int nタイトル名テクスチャの長さdot;
            public STDGBVALUE<int> nスキル値;
            public Color col文字色;
            public string strDTXフォルダのパス;
            public string strPreimageのパス;
            public Cスコア.ST譜面情報 ar譜面情報;
		}

		public struct ST選曲バー
		{
			public CTexture Score;
			public CTexture Box;
			public CTexture Other;
            public CTexture Random;
            public CTexture BackBox;
			public CTexture this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Score;

						case 1:
							return this.Box;

						case 2:
							return this.Other;

                        case 3:
                            return this.Random;

                        case 4:
                            return this.BackBox;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.Score = value;
							return;

						case 1:
							this.Box = value;
							return;

						case 2:
							this.Other = value;
							return;

                        case 3:
                            this.Random = value;
                            return;

                        case 4:
                            this.BackBox = value;
                            return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}
        [StructLayout(LayoutKind.Sequential)]
        public struct STATUSPANEL
        {
            public string label;
            public int status;
        }
        public int nIndex;
        public STATUSPANEL[] stパネルマップ;
        protected struct ST中心点
        {
            public float x;
            public float y;
            public float z;
            public float rotY;
        }
        /// <summary>
		/// <para>SSTFファイル絶対パス(key)とサムネイル画像(value)との辞書。</para>
		/// <para>アプリの起動から終了まで単純に増加を続け、要素が減ることはない。</para>
        /// <para>正直この方法は好ましくないような気がする。</para>
		/// </summary>
		protected Dictionary<string, CTexture> dicThumbnail = new Dictionary<string, CTexture>();
        protected ST中心点[] stマトリックス座標 = new ST中心点[] {
			#region [ 実は円弧配置になってない。射影行列間違ってるよスターレインボウ見せる気かよ… ]
			//-----------------        
            new ST中心点() { x = -940.0000f, y = 4f, z = 320f, rotY = 0.4150f },
			new ST中心点() { x = -740.0000f, y = 4f, z = 230f, rotY = 0.4150f },
            new ST中心点() { x = -550.0000f, y = 4f, z = 150f, rotY = 0.4150f },
			new ST中心点() { x = -370.0000f, y = 4f, z = 70f, rotY = 0.4150f },
			new ST中心点() { x = -194.0000f,     y = 4f, z = -6f, rotY = 0.4150f },
			new ST中心点() { x = 6.00002622683f, y = 2f, z = 0f, rotY = 0f }, 
			new ST中心点() { x = 204.0000f, y = 4f, z = 0f, rotY = -0.4150f },
            new ST中心点() { x = 362.0000f, y = 4f, z = 70f, rotY = -0.4150f },
            new ST中心点() { x = 528.0000f, y = 4f, z = 146f, rotY = -0.4150f },
            new ST中心点() { x = 686.0000f, y = 4f, z = 212f, rotY = -0.4150f },
            new ST中心点() { x = 848.0000f, y = 4f, z = 282f, rotY = -0.4150f },
            new ST中心点() { x = 1200.0000f, y = 4f, z = 450f, rotY = -0.4150f },
            new ST中心点() { x = 1500.0000f, y = 4f, z = -289.5575f, rotY = -0.9279888f },
            new ST中心点() { x = 1500.0000f, y = 4f, z = -289.5575f, rotY = -0.9279888f },
			//-----------------
			#endregion
		};

		public bool b登場アニメ全部完了;
		public Color color文字影 = Color.FromArgb( 0x40, 10, 10, 10 );
		public CCounter[] ct登場アニメ用 = new CCounter[ 15 ];
		public E楽器パート e楽器パート;
		public Font ft曲リスト用フォント;
		public long nスクロールタイマ;
		public int n現在のスクロールカウンタ;
	    public int n現在の選択行;
		public int n目標のスクロールカウンタ;
		private CTexture txSongNotFound, txEnumeratingSongs;
        private CTexture[] txTumbnail = new CTexture[ 15 ];
        private CTexture txクリアランプ;
        private CTexture tx選曲パネル;
        private CTexture tx選択されている曲の曲名;
        private CTexture tx選択されている曲のアーティスト名;
        private CTexture tx選択されている曲のジャケット画像;
        private CTexture txパネル;
        private CTexture txパネル帯;
        private CPrivateFastFont prvPanelFont;
		public STバー tx曲名バー;
		public STバー情報[] stバー情報 = new STバー情報[ 15 ];
		public ST選曲バー tx選曲バー;
		public int nCurrentPosition = 0;
		public int nNumOfItems = 0;

        //選択した後の演出用
        //難易度ラベル、レベル、オプション →ステータスパネルクラスへ
        private CTexture txシンボル;
        private CTexture txベース曲パネル;
        
		//private string strBoxDefSkinPath = "";
		public Eバー種別 e曲のバー種別を返す( C曲リストノード song )
		{
			if( song != null )
			{
				switch( song.eノード種別 )
				{
					case C曲リストノード.Eノード種別.SCORE:
					case C曲リストノード.Eノード種別.SCORE_MIDI:
						return Eバー種別.Score;

					case C曲リストノード.Eノード種別.BOX:
						return Eバー種別.Box;
                    
                    case C曲リストノード.Eノード種別.BACKBOX:
                        return Eバー種別.BackBox;

                    case C曲リストノード.Eノード種別.RANDOM:
                        return Eバー種別.Random;
				}
			}
			return Eバー種別.Other;
		}
		public C曲リストノード r次の曲( C曲リストノード song )
		{
			if( song == null )
				return null;

			List<C曲リストノード> list = ( song.r親ノード != null ) ? song.r親ノード.list子リスト : CDTXMania.Songs管理.list曲ルート;
	
			int index = list.IndexOf( song );

			if( index < 0 )
				return null;

			if( index == ( list.Count - 1 ) )
				return list[ 0 ];

			return list[ index + 1 ];
		}
		public C曲リストノード r前の曲( C曲リストノード song )
		{
			if( song == null )
				return null;

			List<C曲リストノード> list = ( song.r親ノード != null ) ? song.r親ノード.list子リスト : CDTXMania.Songs管理.list曲ルート;

			int index = list.IndexOf( song );
	
			if( index < 0 )
				return null;

			if( index == 0 )
				return list[ list.Count - 1 ];

			return list[ index - 1 ];
		}
        public virtual void tバーの初期化()
        {

        }

        private void tパスを指定してサムネイル画像を生成する( int nバー番号, string strDTXPath, Eバー種別 eType )
        {
            if( nバー番号 < 0 || nバー番号 > 15 )
                return;

            //if( true )
            //    return;

            if( !File.Exists( strDTXPath ) )
            {
                this.txTumbnail[ nバー番号 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ) );
            }
            else
            {
                this.txTumbnail[ nバー番号 ] = CDTXMania.tテクスチャの生成( strDTXPath );
            }
        }
        /// <summary>
        /// 正直このメソッド消したい。
        /// </summary>
        /// <param name="nバー番号">バー番号</param>
        /// <param name="strDTXPath">preimageのパス</param>
        /// <param name="eType">バーの種類(未使用)</param>
        /// <returns>サムネイル画像</returns>
        private CTexture tパスを指定してサムネイル画像を生成して返す( int nバー番号, string strDTXPath, Eバー種別 eType )
        {
            //if( true )
            //    return null;

            if( nバー番号 < 0 || nバー番号 > 15 )
                return this.txTumbnail[ nバー番号 ] = null; //2016.03.12 kairera0467 僅かながら高速化

            if( !File.Exists( strDTXPath ) )
            {
                return this.txTumbnail[ nバー番号 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ) );
            }
            else
            {
                return this.txTumbnail[ nバー番号 ] = CDTXMania.tテクスチャの生成( strDTXPath );
            }
        }
        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            //2013.09.05.kairera0467 中央にしか使用することはないので、色は黒固定。
            //現在は機能しない(面倒なので実装してない)が、そのうち使用する予定。
            //PrivateFontの試験運転も兼ねて。
            //CPrivateFastFont
            CPrivateFastFont prvFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 14, FontStyle.Regular );
            Bitmap bmp;
            
            bmp = prvFont.DrawPrivateFont( str文字, Color.Black, Color.Transparent );

            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );

            if( tx文字テクスチャ != null )
                tx文字テクスチャ.vc拡大縮小倍率 = new Vector3( 0.75f, 1f, 1f );

            int n最大幅 = 290;
            if( tx文字テクスチャ.szテクスチャサイズ.Width > n最大幅 )
            {
                tx文字テクスチャ.vc拡大縮小倍率 = new Vector3( ( n最大幅 / ( tx文字テクスチャ.szテクスチャサイズ.Width / 0.75f ) ), 1f, 1f );
            }

            CDTXMania.t安全にDisposeする( ref bmp );
            CDTXMania.t安全にDisposeする( ref prvFont );

            return tx文字テクスチャ;
        }
        private void tパネルの生成( int nバー番号, string str曲名, string strアーティスト名, Color color )
        {
            //t3D描画の仕様上左詰や右詰が面倒になってしまうので、
            //パネルにあらかじめ曲名とアーティスト名を埋め込んでおく。

            //2016.02.21 kairera0467 ここの最適化が足りないかも。
            
            if( nバー番号 < 0 || nバー番号 > 15 )
				return;

            if( !File.Exists( CSkin.Path( @"Graphics\5_music panel.png" ) ) )
            {
                Trace.TraceError( "5_music panel.png が存在しないため、パネルの生成を中止しました。" );
                return;
            }

            try
            {
                CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txパネル ); //2016.3.12 kairera0467 パネル生成時に既にあるパネルを解放するようにした。

                Bitmap bSongPanel = new Bitmap( 223, 279 );
                Graphics graphics = Graphics.FromImage( bSongPanel );

                Image imgSongPanel;
                Image imgSongJacket;
                Image imgCustomSongNameTexture;
                Image imgCuttomArtistNameTexture;
                bool bFoundTitleTexture = false;
                bool bFoundArtistTexture = false;

                graphics.PageUnit = GraphicsUnit.Pixel;

                imgSongPanel = Image.FromFile( CSkin.Path( @"Graphics\5_music panel.png" ) );
                graphics.DrawImage( imgSongPanel, 0, 0, 223, 279 );

                string strPassBefore = "";
                string strPassAfter = "";
                try
                {
                    strPassBefore = this.stバー情報[ nバー番号 ].strDTXフォルダのパス;
                    strPassAfter = strPassBefore.Replace( this.stバー情報[ nバー番号 ].ar譜面情報.Preimage, "" );
                }
                catch
                {
                    //Replaceでエラーが出たらここで適切な処理ができるようにしたい。
                    strPassBefore = "";
                    strPassAfter = "";
                }

                #region[ 曲名とアーティスト名 ]
                string strPath = ( strPassAfter + "TitleTexture.png" );
                if( File.Exists( ( strPath ) ) )
                {
                    imgCustomSongNameTexture = Image.FromFile( strPath );
                    graphics.DrawImage( imgCustomSongNameTexture, 4, -1, 223, 33 );
                    CDTXMania.t安全にDisposeする( ref imgCustomSongNameTexture );
                    bFoundTitleTexture = true;
                }
                if( File.Exists( ( strPassAfter + "ArtistTexture.png" ) ) )
                {
                    imgCuttomArtistNameTexture = Image.FromFile( strPassAfter + "ArtistTexture.png" );
                    graphics.DrawImage(imgCuttomArtistNameTexture, 0, 252, 223, 26);
                    CDTXMania.t安全にDisposeする( ref imgCuttomArtistNameTexture );
                    bFoundArtistTexture = true;
                }

                if( bFoundTitleTexture == false || bFoundArtistTexture == false )
                {
                    //FontStyle fStyle = CDTXMania.ConfigIni.b選曲リストフォントを太字にする ? FontStyle.Bold : FontStyle.Regular;
                    //CPrivateFastFont prvFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 18, fStyle );
                    Bitmap bmpSongName = new Bitmap( 1, 1 );
                    if( bFoundTitleTexture == false )
                    {
                        bmpSongName = this.prvPanelFont.DrawPrivateFont( str曲名, Color.White );
                        if( ( bmpSongName.Size.Width / 1.25f ) > 240 )
                        {
                            graphics.DrawImage( bmpSongName, 4f, 2f, ( ( bmpSongName.Size.Width / 1.25f ) ), bmpSongName.Size.Height );
                        }
                        else
                        {
                            graphics.DrawImage( bmpSongName, 4f, 2f, ( bmpSongName.Size.Width / 1.25f ),bmpSongName.Size.Height );
                        }
                    }
                    if( bFoundArtistTexture == false )
                    {
                        bmpSongName = this.prvPanelFont.DrawPrivateFont( strアーティスト名, Color.White );
                        if( ( bmpSongName.Size.Width / 1.25f ) > 240 )
                        {
                            graphics.DrawImage( bmpSongName, 220f - ( ( bmpSongName.Size.Width / 1.25f ) ), 252f, ( ( bmpSongName.Size.Width / 1.25f ) ), bmpSongName.Size.Height );
                        }
                        else
                        {
                            graphics.DrawImage( bmpSongName, 220f - ( bmpSongName.Size.Width / 1.25f ), 252f, ( bmpSongName.Size.Width / 1.25f ), bmpSongName.Size.Height );
                        }
                    }
                    CDTXMania.t安全にDisposeする( ref bmpSongName );
                }
                #endregion
                #region[ ジャケット画像 ]
                //2016.02.22 kairera0467 残念ながらバグがあるため封印。

                //if( File.Exists( this.stバー情報[ nバー番号 ].strDTXフォルダのパス + this.stバー情報[ nバー番号 ].ar譜面情報.Preimage ) )
                //{
                //    imgSongJacket = Image.FromFile( this.stバー情報[ nバー番号 ].strDTXフォルダのパス + this.stバー情報[ nバー番号 ].ar譜面情報.Preimage );
                //    graphics.DrawImage( imgSongJacket, 3, 35, 218, 218 );
                    
                //    CDTXMania.t安全にDisposeする( ref imgSongJacket );
                //}
                //else if( File.Exists( CSkin.Path( @"Graphics\5_preimage default.png" ) ) )
                //{
                //    imgSongJacket = Image.FromFile( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                //    graphics.DrawImage( imgSongJacket, 3, 35, 218, 218 );
                    
                //    CDTXMania.t安全にDisposeする( ref imgSongJacket );
                //}
                #endregion

                CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txパネル );
                this.stバー情報[ nバー番号 ].txパネル = new CTexture( CDTXMania.app.Device, bSongPanel, CDTXMania.TextureFormat, false );

                CDTXMania.t安全にDisposeする( ref bSongPanel );
                CDTXMania.t安全にDisposeする( ref imgSongPanel );
                CDTXMania.t安全にDisposeする( ref graphics );
            }
            catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "曲名テクスチャの作成に失敗しました。[{0}]", str曲名 );
				this.stバー情報[ nバー番号 ].txパネル = null;
			}
            
        }
        private void t決定時パネルの生成()
        {
            try
            {
                Bitmap bSongPanel = new Bitmap( 290, 360 );
                Graphics graphics = Graphics.FromImage( bSongPanel );

                Image imgSongPanel;
                Image imgSongJacket;
                Image imgCustomSongNameTexture;
                Image imgCuttomArtistNameTexture;
                bool bFoundTitleTexture = false;
                bool bFoundArtistTexture = false;

                graphics.PageUnit = GraphicsUnit.Pixel;
                //graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                imgSongPanel = Image.FromFile( CSkin.Path( @"Graphics\6_base music panel.png" ) );
                graphics.DrawImage( imgSongPanel, 0, 0, 290, 360 );

                string strFolderPath = CDTXMania.stage選曲.r確定されたスコア.ファイル情報.フォルダの絶対パス;
                string str曲名 = CDTXMania.stage選曲.r確定された曲.strタイトル;
                string strアーティスト名 = CDTXMania.stage選曲.r確定されたスコア.譜面情報.アーティスト名;

                #region[ 曲名とアーティスト名 ]
                string strPath = ( strFolderPath + "TitleTexture.png" );
                if( File.Exists( ( strPath ) ) )
                {
                    imgCustomSongNameTexture = Image.FromFile( strPath );
                    graphics.DrawImage( imgCustomSongNameTexture, 0, 0, 290, 40 );
                    CDTXMania.t安全にDisposeする( ref imgCustomSongNameTexture );
                    bFoundTitleTexture = true;
                }
                if( File.Exists( ( strFolderPath + "ArtistTexture.png" ) ) )
                {
                    imgCuttomArtistNameTexture = Image.FromFile( strFolderPath + "ArtistTexture.png" );
                    graphics.DrawImage( imgCuttomArtistNameTexture, 0, 327, 290, 34 );
                    CDTXMania.t安全にDisposeする( ref imgCuttomArtistNameTexture );
                    bFoundArtistTexture = true;
                }

                if( bFoundTitleTexture == false || bFoundArtistTexture == false )
                {
                    FontStyle fStyle = CDTXMania.ConfigIni.b選曲リストフォントを太字にする ? FontStyle.Bold : FontStyle.Regular;
                    CPrivateFastFont prvFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 19, fStyle );
                    Bitmap bmpSongName = new Bitmap( 1, 1 );
                    if( bFoundTitleTexture == false )
                    {
                        bmpSongName = prvFont.DrawPrivateFont( str曲名, Color.White );
                        //bmpSongName.Save( "test.png" );
                        if( ( bmpSongName.Size.Width / 1.25f ) > 290 )
                        {
                            graphics.DrawImage( bmpSongName, 3f, 8f, ( ( bmpSongName.Size.Width / 1.25f ) * ( 285.0f / ( bmpSongName.Size.Width / 1.25f ) ) ), bmpSongName.Size.Height );
                        }
                        else
                        {
                            graphics.DrawImage( bmpSongName, 3f, 8f, ( bmpSongName.Size.Width / 1.25f ), bmpSongName.Size.Height );
                        }
                    }
                    if( bFoundArtistTexture == false )
                    {
                        bmpSongName = prvFont.DrawPrivateFont( strアーティスト名, Color.White );
                        if( ( bmpSongName.Size.Width / 1.25f ) > 290 )
                        {
                            graphics.DrawImage( bmpSongName, 290f - ( ( bmpSongName.Size.Width / 1.25f ) ), 328f, ( ( bmpSongName.Size.Width / 1.25f ) * ( 285.0f / ( bmpSongName.Size.Width / 1.25f ) ) ), bmpSongName.Size.Height );
                        }
                        else
                        {
                            graphics.DrawImage( bmpSongName, 290f - ( bmpSongName.Size.Width / 1.25f ), 328f, ( bmpSongName.Size.Width / 1.25f ), bmpSongName.Size.Height );
                        }
                    }
                    CDTXMania.t安全にDisposeする( ref bmpSongName );
                    //CDTXMania.t安全にDisposeする( ref prvFont );
                }
                #endregion
                #region[ ジャケット画像 ]
                if( File.Exists( strFolderPath + CDTXMania.stage選曲.r確定されたスコア.譜面情報.Preimage ) )
                {
                    imgSongJacket = Image.FromFile( strFolderPath + CDTXMania.stage選曲.r確定されたスコア.譜面情報.Preimage );
                    graphics.DrawImage( imgSongJacket, 4, 45, 280, 280 );
                    CDTXMania.t安全にDisposeする( ref imgSongJacket );
                }
                else if( File.Exists( CSkin.Path( @"Graphics\5_preimage default.png" ) ) )
                {
                    imgSongJacket = Image.FromFile( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                    graphics.DrawImage( imgSongJacket, 4, 45, 280, 280 );
                    CDTXMania.t安全にDisposeする( ref imgSongJacket );
                }
                #endregion

                this.txベース曲パネル = new CTexture( CDTXMania.app.Device, bSongPanel, CDTXMania.TextureFormat, false );

                CDTXMania.t安全にDisposeする( ref bSongPanel );
                CDTXMania.t安全にDisposeする( ref imgSongPanel );
                CDTXMania.t安全にDisposeする( ref graphics );
            }
            catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "曲名テクスチャの作成に失敗しました。" );
				this.txベース曲パネル = null;
			}
            
        }
        
        private CTexture tカスタム曲名の生成( int nバー番号 )
        {
            //t3D描画の仕様上左詰や右詰が面倒になってしまうので、
            //パネルにあらかじめ曲名とアーティスト名を埋め込んでおく。

            Bitmap bCustomSongNameTexture;
            Image imgCustomSongNameTexture;

            Graphics graphics = Graphics.FromImage( new Bitmap( 1, 1 ) );

            graphics.PageUnit = GraphicsUnit.Pixel;
            bCustomSongNameTexture = new Bitmap( 240, 40 );

            graphics = Graphics.FromImage( bCustomSongNameTexture );
            graphics.DrawImage( bCustomSongNameTexture, 0, 0, 180, 25 );


            string strPassAfter = "";
            try
            {
                strPassAfter = this.stバー情報[ nバー番号 ].strDTXフォルダのパス;
            }
            catch
            {
                strPassAfter = "";
            }

            string strPath = ( strPassAfter + "TitleTexture.png" );
            if( File.Exists( ( strPath ) ) )
            {
                imgCustomSongNameTexture = Image.FromFile( strPath );
                imgCustomSongNameTexture = this.CreateNegativeImage( imgCustomSongNameTexture );
                graphics.DrawImage( imgCustomSongNameTexture, 6, 1, 180, 25 );
                imgCustomSongNameTexture.Dispose();
            }
            
			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタム曲名テクスチャ );
			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタムアーティスト名テクスチャ );
            CTexture txTex = new CTexture( CDTXMania.app.Device, bCustomSongNameTexture, CDTXMania.TextureFormat, false );

            graphics.Dispose();
            bCustomSongNameTexture.Dispose();

            return txTex;
        }
        private CTexture tカスタムアーティスト名テクスチャの生成( int nバー番号 )
        {
            //t3D描画の仕様上左詰や右詰が面倒になってしまうので、
            //パネルにあらかじめ曲名とアーティスト名を埋め込んでおく。

            Bitmap bCustomArtistNameTexture;
            Image imgCustomArtistNameTexture;

            Graphics graphics = Graphics.FromImage( new Bitmap( 1, 1 ) );

            graphics.PageUnit = GraphicsUnit.Pixel;
            bCustomArtistNameTexture = new Bitmap( 210, 27 );

            graphics = Graphics.FromImage( bCustomArtistNameTexture );
            graphics.DrawImage( bCustomArtistNameTexture, 0, 0, 196, 27 );


            string strPassAfter = "";
            try
            {
                strPassAfter = this.stバー情報[ nバー番号 ].strDTXフォルダのパス;
            }
            catch
            {
                strPassAfter = "";
            }

            string strPath = (strPassAfter + "ArtistTexture.png");
            if (File.Exists((strPath)))
            {
                imgCustomArtistNameTexture = Image.FromFile(strPath);
                imgCustomArtistNameTexture = this.CreateNegativeImage( imgCustomArtistNameTexture );
                graphics.DrawImage(imgCustomArtistNameTexture, 16, 2, 196, 27);
            }

			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタム曲名テクスチャ );
			//CDTXMania.t安全にDisposeする( ref this.stバー情報[ nバー番号 ].txカスタムアーティスト名テクスチャ );
            this.stバー情報[nバー番号].nアーティスト名テクスチャの長さdot = 210;
            CTexture txTex = new CTexture( CDTXMania.app.Device, bCustomArtistNameTexture, CDTXMania.TextureFormat, false );

            CDTXMania.t安全にDisposeする( ref graphics );
            return txTex;
        }

        //DOBON.NETから拝借。
        //http://dobon.net/vb/dotnet/graphics/drawnegativeimage.html
        /// <summary>
        /// 指定された画像からネガティブイメージを作成する
        /// </summary>
        /// <param name="img">基の画像</param>
        /// <returns>作成されたネガティブイメージ</returns>
        public Image CreateNegativeImage( Image img )
        {
            //ネガティブイメージの描画先となるImageオブジェクトを作成
            Bitmap negaImg = new Bitmap( img.Width, img.Height );
            //negaImgのGraphicsオブジェクトを取得
            Graphics g = Graphics.FromImage( negaImg );

            //ColorMatrixオブジェクトの作成
            System.Drawing.Imaging.ColorMatrix cm =
                new System.Drawing.Imaging.ColorMatrix();
            //ColorMatrixの行列の値を変更して、色が反転されるようにする
            cm.Matrix00 = -1;
            cm.Matrix11 = -1;
            cm.Matrix22 = -1;
            cm.Matrix33 = 1;
            cm.Matrix40 = cm.Matrix41 = cm.Matrix42 = cm.Matrix44 = 1;

            //ImageAttributesオブジェクトの作成
            System.Drawing.Imaging.ImageAttributes ia =
                new System.Drawing.Imaging.ImageAttributes();
            //ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            //ImageAttributesを使用して色が反転した画像を描画
            g.DrawImage( img,
                new Rectangle( 0, 0, img.Width, img.Height ),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia );

            //リソースを解放する
            g.Dispose();

            return negaImg;
        }
		//-----------------
		#endregion
	}
}
