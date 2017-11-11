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
	internal class CActSelect曲リストXG : CActSelect曲リスト共通
	{

		public CActSelect曲リストXG()
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
			if( this.b活性化してない )
				return 0;

			#region [ 初めての進行描画 ]
			//-----------------
			if( this.b初めての進行描画 )
			{
				for( int i = 0; i < 15; i++ )
					this.ct登場アニメ用[ i ] = new CCounter( -i * 10, 100, 3, CDTXMania.Timer );

				this.nスクロールタイマ = CSound管理.rc演奏用タイマ.n現在時刻;
				CDTXMania.stage選曲.t選択曲変更通知();
				
				base.b初めての進行描画 = false;
			}
			//-----------------
			#endregion

			
			// まだ選択中の曲が決まってなければ、曲ツリールートの最初の曲にセットする。

			if( ( this.r現在選択中の曲 == null ) && ( CDTXMania.Songs管理.list曲ルート.Count > 0 ) )
				this.r現在選択中の曲 = CDTXMania.Songs管理.list曲ルート[ 0 ];


			// 本ステージは、(1)登場アニメフェーズ → (2)通常フェーズ　と二段階にわけて進む。
			// ２つしかフェーズがないので CStage.eフェーズID を使ってないところがまた本末転倒。

			
			// 進行。

			if( !this.b登場アニメ全部完了 )
			{
				#region [ (1) 登場アニメフェーズの進行。]
				//-----------------
				for( int i = 0; i < 15; i++ )	// パネルは全13枚。
				{
					this.ct登場アニメ用[ i ].t進行();

					if( this.ct登場アニメ用[ i ].b終了値に達した )
						this.ct登場アニメ用[ i ].t停止();
				}

				// 全部の進行が終わったら、this.b登場アニメ全部完了 を true にする。

				this.b登場アニメ全部完了 = true;
				for( int i = 0; i < 15; i++ )	// パネルは全13枚。
				{
					if( this.ct登場アニメ用[ i ].b進行中 )
					{
						this.b登場アニメ全部完了 = false;	// まだ進行中のアニメがあるなら false のまま。
						break;
					}
				}
				//-----------------
				#endregion
			}
			else
			{
				#region [ (2) 通常フェーズの進行。]
				//-----------------
				long n現在時刻 = CSound管理.rc演奏用タイマ.n現在時刻;
				
				if( n現在時刻 < this.nスクロールタイマ )	// 念のため
					this.nスクロールタイマ = n現在時刻;

				const int nアニメ間隔 = 2;
				while( ( n現在時刻 - this.nスクロールタイマ ) >= nアニメ間隔 )
				{
					int n加速度 = 1;
					int n残距離 = Math.Abs( (int) ( this.n目標のスクロールカウンタ - this.n現在のスクロールカウンタ ) );

					#region [ 残距離が遠いほどスクロールを速くする（＝n加速度を多くする）。]
					//-----------------
					if( n残距離 <= 100 )
					{
						n加速度 = 2;
					}
					else if( n残距離 <= 300 )
					{
						n加速度 = 3;
					}
					else if( n残距離 <= 500 )
					{
						n加速度 = 4;
					}
					else
					{
						n加速度 = 8;
					}
					//-----------------
					#endregion

					#region [ 加速度を加算し、現在のスクロールカウンタを目標のスクロールカウンタまで近づける。 ]
					//-----------------
					if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )		// (A) 正の方向に未達の場合：
					{
						this.n現在のスクロールカウンタ += n加速度;								// カウンタを正方向に移動する。

						if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )
							this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;	// 到着！スクロール停止！
					}

					else if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )	// (B) 負の方向に未達の場合：
					{
						this.n現在のスクロールカウンタ -= n加速度;								// カウンタを負方向に移動する。

						if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )	// 到着！スクロール停止！
							this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;
					}
					//-----------------
					#endregion

					if( this.n現在のスクロールカウンタ >= 100 )		// １行＝100カウント。
					{
						#region [ パネルを１行上にシフトする。]
						//-----------------

						// 選択曲と選択行を１つ下の行に移動。

						this.r現在選択中の曲 = this.r次の曲( this.r現在選択中の曲 );
						this.n現在の選択行 = ( this.n現在の選択行 + 1 ) % 15;

						// 選択曲から７つ下のパネル（＝新しく最下部に表示されるパネル。消えてしまう一番上のパネルを再利用する）に、新しい曲の情報を記載する。

						C曲リストノード song = this.r現在選択中の曲;
						for( int i = 0; i < 7; i++ )
							song = this.r次の曲( song );

						int index = ( this.n現在の選択行 + 7 ) % 15;	// 新しく最下部に表示されるパネルのインデックス（0～12）。
						this.stバー情報[ index ].strタイトル文字列 = song.strタイトル;
                        this.stバー情報[ index ].strアーティスト名 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.アーティスト名;
						this.stバー情報[ index ].col文字色 = song.col文字色;
                        this.stバー情報[ index ].strDTXフォルダのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス;
                        this.stバー情報[ index ].strPreimageのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス + song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.Preimage;
                        this.tパネルの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].strアーティスト名, this.stバー情報[ index ].col文字色 );

                        if( !this.dicThumbnail.ContainsKey( this.stバー情報[ index ].strPreimageのパス ) )
                        {
                            this.tパスを指定してサムネイル画像を生成する( index, this.stバー情報[ index ].strPreimageのパス, this.stバー情報[ index ].eバー種別  );
                            this.dicThumbnail.Add( this.stバー情報[ index ].strPreimageのパス, this.txTumbnail[ index ] );
                        }
                        this.txTumbnail[ index ] = this.dicThumbnail[ this.stバー情報[ index ].strPreimageのパス ];

						// stバー情報[] の内容を1行ずつずらす。
						
						C曲リストノード song2 = this.r現在選択中の曲;
						for( int i = 0; i < 7; i++ )
							song2 = this.r前の曲( song2 );

						for( int i = 0; i < 15; i++ )
						{
							int n = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;
							this.stバー情報[ n ].eバー種別 = this.e曲のバー種別を返す( song2 );
							song2 = this.r次の曲( song2 );
						}

						
						// 新しく最下部に表示されるパネル用のスキル値を取得。

						for( int i = 0; i < 3; i++ )
							this.stバー情報[ index ].nスキル値[ i ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ i ];

						// 1行(100カウント)移動完了。

						this.n現在のスクロールカウンタ -= 100;
						this.n目標のスクロールカウンタ -= 100;

						t選択曲が変更された( false );				// スクロールバー用に今何番目を選択しているかを更新
                        CDTXMania.t安全にDisposeする( ref this.tx選択されている曲の曲名 );
                        CDTXMania.t安全にDisposeする( ref this.tx選択されている曲のアーティスト名 );
                        CDTXMania.t安全にDisposeする( ref this.tx選択されている曲のジャケット画像 );
						if( this.n目標のスクロールカウンタ == 0 )
							CDTXMania.stage選曲XG.t選択曲変更通知();		// スクロール完了＝選択曲変更！

						//-----------------
						#endregion
					}
					else if( this.n現在のスクロールカウンタ <= -100 )
					{
						#region [ パネルを１行下にシフトする。]
						//-----------------

						// 選択曲と選択行を１つ上の行に移動。

						this.r現在選択中の曲 = this.r前の曲( this.r現在選択中の曲 );
						this.n現在の選択行 = ( ( this.n現在の選択行 - 1 ) + 15 ) % 15;

						// 選択曲から５つ上のパネル（＝新しく最上部に表示されるパネル。消えてしまう一番下のパネルを再利用する）に、新しい曲の情報を記載する。

						C曲リストノード song = this.r現在選択中の曲;
						for( int i = 0; i < 7; i++ )
							song = this.r前の曲( song );

						int index = ( ( this.n現在の選択行 - 7 ) + 15 ) % 15;	// 新しく最上部に表示されるパネルのインデックス（0～12）。
						this.stバー情報[ index ].strタイトル文字列 = song.strタイトル;
                        this.stバー情報[ index ].strアーティスト名 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す(song) ].譜面情報.アーティスト名;
						this.stバー情報[ index ].col文字色 = song.col文字色;
                        this.stバー情報[ index ].strDTXフォルダのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス;
                        this.stバー情報[ index ].strPreimageのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス + song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.Preimage;
                        this.tパネルの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].strアーティスト名, this.stバー情報[ index ].col文字色 );

                        if( !this.dicThumbnail.ContainsKey( this.stバー情報[ index ].strPreimageのパス ) )
                        {
                            this.tパスを指定してサムネイル画像を生成する( index, this.stバー情報[ index ].strPreimageのパス, this.stバー情報[ index ].eバー種別  );
                            this.dicThumbnail.Add( this.stバー情報[ index ].strPreimageのパス, this.txTumbnail[ index ] );
                        }
                        this.txTumbnail[ index ] = this.dicThumbnail[ this.stバー情報[ index ].strPreimageのパス ];

						// stバー情報[] の内容を1行ずつずらす。
						
						C曲リストノード song2 = this.r現在選択中の曲;
						for( int i = 0; i < 7; i++ )
							song2 = this.r前の曲( song2 );

						for( int i = 0; i < 15; i++ )
						{
							int n = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;
							this.stバー情報[ n ].eバー種別 = this.e曲のバー種別を返す( song2 );
							song2 = this.r次の曲( song2 );
						}

		
						// 新しく最上部に表示されるパネル用のスキル値を取得。
						
						for( int i = 0; i < 3; i++ )
							this.stバー情報[ index ].nスキル値[ i ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ i ];


						// 1行(100カウント)移動完了。

						this.n現在のスクロールカウンタ += 100;
						this.n目標のスクロールカウンタ += 100;


					    t選択曲が変更された( false );				// スクロールバー用に今何番目を選択しているかを更新
                        CDTXMania.t安全にDisposeする( ref this.tx選択されている曲の曲名 );
                        CDTXMania.t安全にDisposeする( ref this.tx選択されている曲のアーティスト名 );
                        CDTXMania.t安全にDisposeする( ref this.tx選択されている曲のジャケット画像 );
						if( this.n目標のスクロールカウンタ == 0 )
							CDTXMania.stage選曲XG.t選択曲変更通知();		// スクロール完了＝選択曲変更！
						//-----------------
						#endregion
					}

					this.nスクロールタイマ += nアニメ間隔;
				}
				//-----------------
				#endregion
			}


			// 描画。

			if( this.r現在選択中の曲 == null )
			{
				#region [ 曲が１つもないなら「Songs not found.」を表示してここで帰れ。]
				//-----------------
				if ( bIsEnumeratingSongs )
				{
					if ( this.txEnumeratingSongs != null )
					{
						this.txEnumeratingSongs.t2D描画( CDTXMania.app.Device, 460, 300 );
					}
				}
				else
				{
					if ( this.txSongNotFound != null )
						this.txSongNotFound.t2D描画( CDTXMania.app.Device, 440, 300 );
				}
				//-----------------
				#endregion

				return 0;
			}
            var bar = SlimDX.Matrix.Identity;

            var barL = SlimDX.Matrix.Identity;

            Matrix[] barC = new Matrix[2];
            Matrix[] barLa = new Matrix[2];
            Matrix[] barU = new Matrix[2];

            #region[ビルボードの実験コード。「ここでするなよ...」とか言わないであげて...]
            //Z座標はプラス方向にすると動かなくなるので注意。
            
            //共通
            Vector3 VecCam = new Vector3( 0.0f, 0.0f, -1.0f ); //Zをマイナス方向にしないと描画されない(?)
            Vector3 VecTarg = new Vector3( 0.0f, 0.0f, 0.0f );
            Vector3 VecUp = new Vector3( 0.0f, 1.0f, 0.0f );

            bar *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
            barL *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );

            bar *= SlimDX.Matrix.RotationYawPitchRoll( -0.40f, 0.0f, 0.0f ); ////通常のRotationと何が違うのかわからない(Axisから倍率を抜いただけ?)
            barL *= SlimDX.Matrix.RotationYawPitchRoll( 0.40f, 0.0f, 0.0f );


            //bar *= SlimDX.Matrix.PerspectiveFovLH( C変換.DegreeToRadian(45), 1280.0f/720.0f, 1.0f, 500000.0f ); //カメラの視野角など調整
            
            //ワールド変換とか意味ワカンネ
            #endregion

            bar *= SlimDX.Matrix.Translation(600f, 121f, 190f);
            barL *= SlimDX.Matrix.Translation(-600f, 121f, 190f);


            for( int i = 0; i < 2; i++ )
            {
                float fRate = 1.0f;
                if( i == 0 ) fRate = -1.0f;

                barC[ i ] = Matrix.Identity;
                barC[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                barC[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( -0.41f * fRate, 0.0f, 0.0f );
                barC[ i ] *= SlimDX.Matrix.Translation( 592f * fRate, 148f, 186f);

                barLa[ i ] = Matrix.Identity;
                barLa[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                barLa[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( -0.41f * fRate, 0.0f, 0.0f );
                barLa[ i ] *= SlimDX.Matrix.Translation( 592f * fRate, 100f, 186f);

                barU[ i ] = Matrix.Identity;
                barU[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                barU[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( -0.41f * fRate, 0.0f, 0.0f );
                barU[ i ] *= SlimDX.Matrix.Translation( 592f * fRate, -106f, 186f);
            }

            if( this.txパネル帯 != null )
            {
                this.txパネル帯.t3D描画( CDTXMania.app.Device, barU[ 0 ], new Rectangle( 2, 74, 1000, 30 ) );
                this.txパネル帯.t3D描画( CDTXMania.app.Device, barU[ 1 ], new Rectangle( 2, 74, 1000, 30 ) );
                this.txパネル帯.t3D描画( CDTXMania.app.Device, bar, new Rectangle( 2, 38, 1000, 30 ) );   //右の帯。
                this.txパネル帯.t3D描画( CDTXMania.app.Device, barL, new Rectangle( 2, 38, 1000, 30 ) );   //左の帯。
                //this.tx帯.t3D描画(CDTXMania.app.Device, barL);   //左の帯。
                //this.txパネル帯.n透明度 = 155 + this.ct登場アニメ用[10].n現在の値;
                this.txパネル帯.t3D描画( CDTXMania.app.Device, barC[ 0 ], new Rectangle( 2, 2, 1000, 30 ) );
                this.txパネル帯.t3D描画( CDTXMania.app.Device, barC[ 1 ], new Rectangle( 2, 2, 1000, 30 ) );
            }

            #region[ ジャケット画像のMatrix配列 ]
            //パネル1枚1枚にMatrixを割り当ててみる。
            //とりあえず構造の最適化無しで地味に作ってみる。
            Matrix[] matSongPanel = new SlimDX.Matrix[ 15 ];
            Matrix[] matJacket = new SlimDX.Matrix[ 15 ];
            ST中心点[] st3D座標 = new ST中心点[] {
			#region [ 計算なんてしていない。 ]
		    	//-----------------
                //new ST中心点() { x = -1100.0000f, y = 8f, z = 400.0f, rotY = 0.410f },
                new ST中心点() { x = -2000.0000f, y = 8f, z = 400.0f, rotY = 0.410f },
                new ST中心点() { x = -990.0000f, y = 8f, z = 344.0f, rotY = 0.410f },
                new ST中心点() { x = -820.0000f, y = 8f, z = 264f, rotY = 0.410f },
    			new ST中心点() { x = -666.0000f, y = 8f, z = 198f, rotY = 0.410f },
                new ST中心点() { x = -506.0000f, y = 8f, z = 127f, rotY = 0.410f },
		    	new ST中心点() { x = -350.0000f, y = 8f, z = 60f, rotY = 0.410f },
	    		new ST中心点() { x = -194.0000f, y = 8f, z = -6f, rotY = 0.410f },
    			new ST中心点() { x = 6.00002622683f, y = 8f, z = 0f, rotY = 0.0f }, //7
			    new ST中心点() { x = 208.0000f, y = 8f, z = 0f, rotY = -0.410f },
                new ST中心点() { x = 362.0000f, y = 8f, z = 66f, rotY = -0.410f },
                new ST中心点() { x = 518.0000f, y = 8f, z = 132f, rotY = -0.410f },
                new ST中心点() { x = 676.0000f, y = 8f, z = 200.0f, rotY = -0.410f },
                new ST中心点() { x = 837.0000f, y = 8f, z = 270.0f, rotY = -0.410f },
                new ST中心点() { x = 1010.0000f, y = 8f, z = 350.0f, rotY = -0.410f },
                new ST中心点() { x = 2000.0000f, y = 8f, z = 450.0f, rotY = -0.410f }
                //new ST中心点() { x = 1190.0000f, y = 8f, z = 450.0f, rotY = -0.410f }

			    //-----------------
			#endregion
		    };

            for( int i = 0; i < 15; i++ )
            {
                matSongPanel[ i ] = Matrix.Identity;
                matSongPanel[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                matSongPanel[ i ] *= SlimDX.Matrix.Scaling(0.62f, 0.88f, 1.0f);
                //matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                //matSongPanel[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );


                matJacket[ i ] = Matrix.Identity;
                matJacket[ i ] *= SlimDX.Matrix.LookAtLH( VecCam, VecTarg, VecUp );
                //matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f ); //ジャケット画像は計算順序を変える。

                //matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, 0f, st3D座標[ i ].z );
            }
            #endregion

			if( !this.b登場アニメ全部完了 )
			{
				#region [ (1) 登場アニメフェーズの描画。]
				//-----------------
				for( int i = 0; i < 15; i++ )	// パネルは全13枚。
				{
					if( this.ct登場アニメ用[ i ].n現在の値 >= 0 )
					{
						int nパネル番号 = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;

                        if( i == 8 )
                        {
                            #region [ ジャケット画像の描画 ]
                            //-----------------
                            if( this.stバー情報[ nパネル番号 ].txパネル != null )
                            {
                                matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matSongPanel[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );
                                this.stバー情報[ nパネル番号 ].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                            }
                            if( this.txTumbnail[nパネル番号] != null )
                            {
                                float f拡大率 = (float)172.0 / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width;
                                float f拡大率2 = (float)172.0 / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;

                                matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                                matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y - 1.5f, st3D座標[ i ].z );
 
                                this.txTumbnail[ nパネル番号 ].t3D描画(CDTXMania.app.Device, matJacket[ i ] );
                            }
                            //-----------------
                            #endregion
                            if( this.tx選曲パネル != null )
                                this.tx選曲パネル.t2D描画( CDTXMania.app.Device, 761, 233, new Rectangle( 304, 70, 59, 242 ) );
                        }
                        else if( i == 7 )
						{
						}
						else
						{
							// (B) その他のパネルの描画。
                            #region [ ジャケット画像の描画 ]
                            //-----------------
                            if( this.stバー情報[ nパネル番号 ].txパネル != null )
                            {
                                matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matSongPanel[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y, st3D座標[ i ].z );
                                this.stバー情報[ nパネル番号 ].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                            }
                            if( this.txTumbnail[nパネル番号] != null )
                            {
                                float f拡大率 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Width ;
                                float f拡大率2 = (float)172.0 / this.txTumbnail[nパネル番号].szテクスチャサイズ.Height;

                                matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                                matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                                matJacket[ i ] *= SlimDX.Matrix.Translation( st3D座標[ i ].x, st3D座標[ i ].y - 1.5f, st3D座標[ i ].z );

                                this.txTumbnail[nパネル番号].t3D描画( CDTXMania.app.Device, matJacket[ i ] );
                            }
                            //-----------------
                            #endregion
						}
					}
				}

                int n選択曲のパネル番号 = ( ( ( this.n現在の選択行 - 7 ) + 7 ) + 15 ) % 15;
                #region[ ランプ帯 ]
                if( this.txパネル帯 != null )
                {
                    this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 0 ], new Rectangle( 2, 110, 1000, 20 ) );
                    this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 1 ], new Rectangle( 2, 110, 1000, 20 ) );
                }
                #endregion
                #region[ 中央パネル ]
                if( this.tx選曲パネル != null )
                    this.tx選曲パネル.t2D描画(CDTXMania.app.Device, 457, 163, new Rectangle( 0, 0, 363, 368 ) );
                #endregion
                #region[ クリアランプ ]
                for( int la = 0; la < 5 ; la++ )
                {
                    if( this.txクリアランプ != null && CDTXMania.stage選曲.r現在選択中の曲.ar難易度ラベル[ la ] != null && CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ] != null )
                        this.txクリアランプ.t2D描画( CDTXMania.app.Device, 506, 292 - la * 13, new Rectangle(( CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ].譜面情報.最大スキル.Drums != 0 ? 11 + la * 11 : 0), ( CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ].譜面情報.フルコンボ.Drums ? 10 : 0 ), 11, 10 ));
                }
                #endregion
                #region[ ジャケット画像 ]
                //if( this.tx選択されている曲のジャケット画像 == null )
                //{
                //    this.tx選択されている曲のジャケット画像 = this.tパスを指定してサムネイル画像を生成して返す( n選択曲のパネル番号, this.stバー情報[ n選択曲のパネル番号 ].strPreimageのパス, this.stバー情報[ n選択曲のパネル番号 ].eバー種別 );
                //}
                //else if( this.tx選択されている曲のジャケット画像 != null )
                //{
                //    float f拡大率 = (float)218.0 / this.tx選択されている曲のジャケット画像.szテクスチャサイズ.Width;
                //    float f拡大率2 = (float)218.0 / this.tx選択されている曲のジャケット画像.szテクスチャサイズ.Height;
                //    this.tx選択されている曲のジャケット画像.vc拡大縮小倍率 = new Vector3(f拡大率, f拡大率2, 1.0f);
                //    this.tx選択されている曲のジャケット画像.t2D描画( CDTXMania.app.Device, 537, 249 );
                //}
                if( this.txTumbnail[ n選択曲のパネル番号 ] != null )
                {
                    float f拡大率 = (float)218.0 / this.txTumbnail[ n選択曲のパネル番号 ].szテクスチャサイズ.Width;
                    float f拡大率2 = (float)218.0 / this.txTumbnail[ n選択曲のパネル番号 ].szテクスチャサイズ.Height;
                    this.txTumbnail[ n選択曲のパネル番号 ].vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率2, 1.0f );
                    this.txTumbnail[ n選択曲のパネル番号 ].t2D描画(CDTXMania.app.Device, 537, 249 );
                    this.txTumbnail[ n選択曲のパネル番号 ].vc拡大縮小倍率 = new Vector3( 1.0f, 1.0f, 1.0f );
                }
                #endregion
                #region[ タイトル・アーティスト名 ]
                //曲名テクスチャ 生成
                if( File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "TitleTexture.png" ) && this.tx選択されている曲の曲名 == null )
                {
                    this.tx選択されている曲の曲名 = this.tカスタム曲名の生成( n選択曲のパネル番号 );
                }
                else
                {
                    if( this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 != "" && this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 != null && this.tx選択されている曲の曲名 == null )
                        this.tx選択されている曲の曲名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 );
                }
                //曲名テクスチャ 描画
		        if( this.tx選択されている曲の曲名 != null )
				    this.tx選択されている曲の曲名.t2D描画( CDTXMania.app.Device, 552, 210 );

                //アーティスト名テクスチャ 生成と描画

                if( File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "ArtistTexture.png" ) )
                {
                    if( this.tx選択されている曲のアーティスト名 == null )
                    this.tx選択されている曲のアーティスト名 = this.tカスタムアーティスト名テクスチャの生成( n選択曲のパネル番号 );

                    if( this.tx選択されている曲のアーティスト名 != null )
                        this.tx選択されている曲のアーティスト名.t2D描画( CDTXMania.app.Device, 552, 470 );
                }
                else if( !File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "ArtistTexture.png" ) )
                {
                    if( this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 != "" && this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 != null && this.tx選択されている曲のアーティスト名 == null )
                        this.tx選択されている曲のアーティスト名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 );

                    if( this.tx選択されている曲のアーティスト名 != null )
                    {
                        int nアーティスト名X座標 = 763 - (int)( this.tx選択されている曲のアーティスト名.szテクスチャサイズ.Width * 0.75f );
                        this.tx選択されている曲のアーティスト名.t2D描画( CDTXMania.app.Device, nアーティスト名X座標, 470 );
                    }
                }
                #endregion
				//-----------------
				#endregion
			}
			else
			{
				#region [ (2) 通常フェーズの描画。]
				//-----------------
				for( int i = 0; i < 15; i++ )	// パネルは全13枚。
				{
					int nパネル番号 = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;
					int n見た目の行番号 = i;
					int n次のパネル番号 = ( this.n現在のスクロールカウンタ <= 0 ) ? ( ( i + 1 ) % 15 ) : ( ( ( i - 1 ) + 15 ) % 15 );
                    float fX = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].x + ( ( ( st3D座標[ n次のパネル番号 ].x - st3D座標[ n見た目の行番号 ].x ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) : 
                                                                     st3D座標[ n見た目の行番号 ].x + ( ( ( st3D座標[ n次のパネル番号 ].x - st3D座標[ n見た目の行番号 ].x ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );

                    float fY = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].y + ( ( ( st3D座標[ n次のパネル番号 ].y + st3D座標[ n見た目の行番号 ].y ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) :
                                                                     st3D座標[ n見た目の行番号 ].y + ( ( ( st3D座標[ n次のパネル番号 ].y + st3D座標[ n見た目の行番号 ].y ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );

                    float fZ = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].z + ( ( ( st3D座標[ n次のパネル番号 ].z - st3D座標[ n見た目の行番号 ].z ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) :
                                                                     st3D座標[ n見た目の行番号 ].z + ( ( ( st3D座標[ n次のパネル番号 ].z - st3D座標[ n見た目の行番号 ].z ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );

                    float fR = this.n現在のスクロールカウンタ <= 0 ? st3D座標[ n見た目の行番号 ].rotY + ( ( ( st3D座標[ n次のパネル番号 ].rotY - st3D座標[ n見た目の行番号 ].rotY ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) ) :
                                                                     st3D座標[ n見た目の行番号 ].rotY + ( ( ( st3D座標[ n次のパネル番号 ].rotY - st3D座標[ n見た目の行番号 ].rotY ) * ( Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0f ) );

                    //デバッグ用
                    //if( i == 7 ){
                    //    CDTXMania.act文字コンソール.tPrint( 0, 16 * i, C文字コンソール.Eフォント種別.赤, i.ToString() + " " + this.stバー情報[ nパネル番号 ].strタイトル文字列.ToString() );
                    //}else{
                    //    CDTXMania.act文字コンソール.tPrint( 0, 16 * i, C文字コンソール.Eフォント種別.白, i.ToString() + " " + this.stバー情報[ nパネル番号 ].strタイトル文字列.ToString() );
                    //}



                    if( i == 8 )
                    {
                        #region [ ジャケット画像の描画 ]
                        //-----------------
                        if( this.stバー情報[ nパネル番号 ].txパネル != null )
                        {
                            matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( fR, 0.0f, 0.0f );
                            matSongPanel[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y, fZ );
                            this.stバー情報[ nパネル番号 ].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                        }
                        if( this.txTumbnail[ nパネル番号 ] != null )
                        {
                            float f拡大率 = (float)172.0 / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width;
                            float f拡大率2 = (float)172.0 / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;

                            matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                            matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( fR, 0.0f, 0.0f );
                            matJacket[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y - 1.5f, fZ );

                            this.txTumbnail[ nパネル番号 ].t3D描画( CDTXMania.app.Device, matJacket[ i ] );
                        }
                        //-----------------
                        #endregion
                    }
                    else if( i == 7 || i == 0 || i == 14)
                    {
                        //選択曲、画面外の曲のジャケット画像は表示しない。
					}
                    else
                    {
                        #region [ ジャケット画像の描画 ]
                        //-----------------
                        if( this.stバー情報[ nパネル番号 ].txパネル != null )
                        {
                            matSongPanel[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                            matSongPanel[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y, fZ );
                            this.stバー情報[ nパネル番号 ].txパネル.t3D描画( CDTXMania.app.Device, matSongPanel[ i ] );
                        }
                        if( this.txTumbnail[ nパネル番号 ] != null )
                        {
                            float f拡大率 = (float)172.0 / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width ;
                            float f拡大率2 = (float)172.0 / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;

                            matJacket[ i ] *= SlimDX.Matrix.Scaling( f拡大率 * CTexture.f画面比率 - 0.084f, f拡大率2 * CTexture.f画面比率 + 0.05f, 1.0f );
                            matJacket[ i ] *= SlimDX.Matrix.RotationYawPitchRoll( st3D座標[ i ].rotY, 0.0f, 0.0f );
                            matJacket[ i ] *= SlimDX.Matrix.Translation( fX, st3D座標[ i ].y - 1.5f, fZ );

                            this.txTumbnail[ nパネル番号 ].t3D描画( CDTXMania.app.Device, matJacket[ i ] );
                        }
                        //-----------------
                        #endregion
                    }
				}

                //選択中の曲
                int n選択曲のパネル番号 = ( ( ( this.n現在の選択行 - 7 ) + 7 ) + 15 ) % 15;
                if( CDTXMania.r現在のステージ.eフェーズID != CStage.Eフェーズ.選曲_決定演出 && CDTXMania.r現在のステージ.eフェーズID != CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト )
                {
                    #region[ ランプ帯 ]
                    if( this.txパネル帯 != null )
                    {
                        this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 0 ], new Rectangle( 2, 110, 1000, 20 ) );
                        this.txパネル帯.t3D描画( CDTXMania.app.Device, barLa[ 1 ], new Rectangle( 2, 110, 1000, 20 ) );
                    }
                    #endregion
                    #region[ 中央パネル ]
                    if( this.tx選曲パネル != null )
                        this.tx選曲パネル.t2D描画( CDTXMania.app.Device, 457, 163, new Rectangle( 0, 0, 363, 368 ) );
                    #endregion
                    #region[ クリアランプ ]
                    for( int la = 0; la < 5 ; la++ )
                    {
                        if( this.txクリアランプ != null && CDTXMania.stage選曲.r現在選択中の曲.ar難易度ラベル[ la ] != null && CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ] != null )
                            this.txクリアランプ.t2D描画( CDTXMania.app.Device, 506, 292 - la * 13, new Rectangle(( CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ].譜面情報.最大スキル.Drums != 0 ? 11 + la * 11 : 0), ( CDTXMania.stage選曲.r現在選択中の曲.arスコア[ la ].譜面情報.フルコンボ.Drums ? 10 : 0 ), 11, 10 ));
                    }
                    #endregion
                    #region[ ジャケット画像 ]
                    if( this.txTumbnail[ n選択曲のパネル番号 ] != null )
                    {
                        float f拡大率 = (float)218.0 / this.txTumbnail[ n選択曲のパネル番号 ].szテクスチャサイズ.Width;
                        float f拡大率2 = (float)218.0 / this.txTumbnail[ n選択曲のパネル番号 ].szテクスチャサイズ.Height;
                        this.txTumbnail[ n選択曲のパネル番号 ].vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率2, 1.0f );
                        this.txTumbnail[ n選択曲のパネル番号 ].t2D描画(CDTXMania.app.Device, 537, 249 );
                        this.txTumbnail[ n選択曲のパネル番号 ].vc拡大縮小倍率 = new Vector3( 1.0f, 1.0f, 1.0f );
                    }
                    #endregion
                    #region[ タイトル・アーティスト名 ]
                    //曲名テクスチャ 生成
                    if( File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "TitleTexture.png" ) && this.tx選択されている曲の曲名 == null )
                    {
                        this.tx選択されている曲の曲名 = this.tカスタム曲名の生成( n選択曲のパネル番号 );
                    }
                    else
                    {
                        if( this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 != "" && this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 != null && this.tx選択されている曲の曲名 == null )
                            this.tx選択されている曲の曲名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ n選択曲のパネル番号 ].strタイトル文字列 );
                    }
                    //曲名テクスチャ 描画
		            if( this.tx選択されている曲の曲名 != null )
				        this.tx選択されている曲の曲名.t2D描画( CDTXMania.app.Device, 552, 210 );

                    //アーティスト名テクスチャ 生成と描画

                    if( File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "ArtistTexture.png" ) )
                    {
                        if( this.tx選択されている曲のアーティスト名 == null )
                        this.tx選択されている曲のアーティスト名 = this.tカスタムアーティスト名テクスチャの生成( n選択曲のパネル番号 );

                        if( this.tx選択されている曲のアーティスト名 != null )
                            this.tx選択されている曲のアーティスト名.t2D描画( CDTXMania.app.Device, 552, 470 );
                    }
                    else if( !File.Exists( this.stバー情報[ n選択曲のパネル番号 ].strDTXフォルダのパス + "ArtistTexture.png" ) )
                    {
                        if( this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 != "" && this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 != null && this.tx選択されている曲のアーティスト名 == null )
                            this.tx選択されている曲のアーティスト名 = this.t指定された文字テクスチャを生成する( this.stバー情報[ n選択曲のパネル番号 ].strアーティスト名 );

                        if( this.tx選択されている曲のアーティスト名 != null )
                        {
                            int nアーティスト名X座標 = 763 - (int)( this.tx選択されている曲のアーティスト名.szテクスチャサイズ.Width * 0.75f );
                            this.tx選択されている曲のアーティスト名.t2D描画( CDTXMania.app.Device, nアーティスト名X座標, 470 );
                        }
                    }
                    #endregion
                }
                //-----------------
				#endregion
            }

            if( CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_決定演出 || CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト )
            {
                #region[ 決定時の透明度操作 ]
                //とりあえず透明度操作でごまかす...
                for( int i = 0; i < 15; i++ )
                {
                    if( this.txTumbnail[ i ] != null )
                        this.txTumbnail[ i ].n透明度 = (int)( 255 - ( 255 * ( ( CDTXMania.stage選曲.ct決定演出待機.n現在の値 ) / 250.0 ) ) );
                    if( this.stバー情報[ i ].txパネル != null )
                        this.stバー情報[ i ].txパネル.n透明度 = (int)( 255 - ( 255 * ( ( CDTXMania.stage選曲.ct決定演出待機.n現在の値 ) / 250.0 ) ) );
                }
                this.txパネル帯.n透明度 = (int)( 255 - ( 255 * ( ( CDTXMania.stage選曲.ct決定演出待機.n現在の値 ) / 250.0 ) ) );
                #endregion
                #region[ 決定時のパネル表示 ]
                if( this.txベース曲パネル == null )
                    this.t決定時パネルの生成();
                if( this.txベース曲パネル != null )
                {
                    //拡大終了まで80ms
                    int nNowTime = CDTXMania.stage選曲.ct決定演出待機.n現在の値 - 100;

                    float fScale = nNowTime > -1 ? ( nNowTime < 100 ? 0.778f + ( 0.222f * ( nNowTime / 100.0f ) ) : 1.0f ) : 0.778f;
                    int nMusicPanelX = (int)( 648 - ( ( 290.0f / 2.0f ) * fScale ) );
                    int nMusicPanelY = (int)( 353 - ( ( 360.0f / 2.0f ) * fScale ) );
                    this.txベース曲パネル.vc拡大縮小倍率 = new Vector3( fScale, fScale, 1.0f );
                    this.txベース曲パネル.t2D描画( CDTXMania.app.Device, nMusicPanelX, nMusicPanelY );
                }
                #endregion
            }
            
			return 0;
		}
		
    	// その他

		#region [ private ]
		//-----------------

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
		protected STバー tx曲名バー;
		protected STバー情報[] stバー情報 = new STバー情報[ 15 ];
		protected ST選曲バー tx選曲バー;

        //選択した後の演出用
        //難易度ラベル、レベル、オプション →ステータスパネルクラスへ
        private CTexture txシンボル;
        private CTexture txベース曲パネル;

		public override void tバーの初期化()
		{
			C曲リストノード song = this.r現在選択中の曲;
			
			if( song == null )
				return;

			for( int i = 0; i < 7; i++ )
				song = this.r前の曲( song );

			for( int i = 0; i < 15; i++ )
			{
				this.stバー情報[ i ].strタイトル文字列 = song.strタイトル;
                this.stバー情報[ i ].strアーティスト名 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.アーティスト名;
				this.stバー情報[ i ].col文字色 = song.col文字色;
				this.stバー情報[ i ].eバー種別 = this.e曲のバー種別を返す( song );
                this.stバー情報[ i ].ar譜面情報 = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報;

                //for( int n = 0; n < 5; n++ )
                {
                    //this.stバー情報[ i ].ar難易度ラベル[ n ];

                    //if( this.stバー情報[ i ].ar難易度ラベル[ n ] != null )
                        //this.stバー情報[ i ].ar難易度ラベル[ n ] = song.ar難易度ラベル[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ];
                }
				
                this.stバー情報[ i ].strDTXフォルダのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス;
                this.stバー情報[ i ].strPreimageのパス = song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].ファイル情報.フォルダの絶対パス + song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.Preimage;
                this.tパネルの生成(i, song.strタイトル, this.stバー情報[ i ].strアーティスト名, song.col文字色);
                
                if( this.stバー情報[ i ].strPreimageのパス != null )
                {
                    if( !this.dicThumbnail.ContainsKey( this.stバー情報[ i ].strPreimageのパス ) )
				    {
                        this.tパスを指定してサムネイル画像を生成する( i, this.stバー情報[ i ].strPreimageのパス, this.stバー情報[ i ].eバー種別  );
		                this.dicThumbnail.Add( this.stバー情報[ i ].strPreimageのパス, this.txTumbnail[ i ] );
				    }
                    this.txTumbnail[ i ] = this.dicThumbnail[ this.stバー情報[ i ].strPreimageのパス ];
                }

				for( int j = 0; j < 3; j++ )
					this.stバー情報[ i ].nスキル値[ j ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ j ];

				song = this.r次の曲( song );
			}

			this.n現在の選択行 = 7;
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

                string strFolderPath = CDTXMania.stage選曲XG.r確定されたスコア.ファイル情報.フォルダの絶対パス;
                string str曲名 = CDTXMania.stage選曲XG.r確定された曲.strタイトル;
                string strアーティスト名 = CDTXMania.stage選曲XG.r確定されたスコア.譜面情報.アーティスト名;

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
                if( File.Exists( strFolderPath + CDTXMania.stage選曲XG.r確定されたスコア.譜面情報.Preimage ) )
                {
                    imgSongJacket = Image.FromFile( strFolderPath + CDTXMania.stage選曲XG.r確定されたスコア.譜面情報.Preimage );
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
