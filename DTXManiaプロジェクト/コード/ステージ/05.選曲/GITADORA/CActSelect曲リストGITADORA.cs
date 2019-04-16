using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using SharpDX;
using FDK;

using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using SlimDXKey = SlimDX.DirectInput.Key;
namespace DTXMania
{
    internal class CActSelect曲リストGITADORA : CActSelect曲リスト共通
    {
        public CActSelect曲リストGITADORA()
        {
        }

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
                //this.tパネルの生成(i, song.strタイトル, this.stバー情報[ i ].strアーティスト名, song.col文字色);
                
                if( this.stバー情報[ i ].strPreimageのパス != null )
                {
                    if( !this.dicThumbnail.ContainsKey( this.stバー情報[ i ].strPreimageのパス ) )
				    {
                        this.tパスを指定してサムネイル画像を生成する( i, this.stバー情報[ i ].strPreimageのパス, this.stバー情報[ i ].eバー種別  );
		                this.dicThumbnail.Add( this.stバー情報[ i ].strPreimageのパス, this.txTumbnail[ i ] );
				    }
                    this.txTumbnail[ i ] = this.dicThumbnail[ this.stバー情報[ i ].strPreimageのパス ];
                }

                if( this.stバー情報[ i ].strタイトル文字列 != null )
                {
                    if( !this.dicMusicName.ContainsKey( this.stバー情報[ i ].strタイトル文字列 ) )
                    {
                        this.t指定された文字テクスチャを生成してバーに格納する( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].eバー種別  );
                        this.dicMusicName.Add( this.stバー情報[ i ].strタイトル文字列, this.txMusicName[ i ] );
                    }
                    this.txMusicName[ i ] = this.dicMusicName[ this.stバー情報[ i ].strタイトル文字列 ];
                }

				for( int j = 0; j < 3; j++ )
					this.stバー情報[ i ].nスキル値[ j ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ j ];

				song = this.r次の曲( song );
			}

			this.n現在の選択行 = 7;
		}

        public override void On活性化()
        {
            this.b初めての進行描画 = true; //ここで有効にしなおさなければ他の場面に行き来することができなくなる

            this.prvFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 24, FontStyle.Regular );
            this.prvFontSmall = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 15, FontStyle.Regular );
            base.On活性化();
        }

        public override void On非活性化()
        {
            CDTXMania.t安全にDisposeする( ref this.prvFont );
            CDTXMania.t安全にDisposeする( ref this.prvFontSmall );
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            if( !this.b活性化してない )
            {
                this.txバー背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_Bar Background.png" ) );
                this.txバー選択中 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_Bar selected.png" ) );
                this.txバー選択中枠 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_Bar selected cursor.png" ) );
                this.txバー_フォルダ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar box.png" ) );

                this.txジャケットパネル背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_JacketPanel Background.png" ) );
                this.txジャケットパネル枠 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_Jacket sensor.png" ) );

                this.tx水色 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile lightblue.png" ) );
                this.tx黒 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile black.png" ) );
                this.tx青色 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile blue.png" ) );
                this.tx群青 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile darkblue.png" ) );

                this.txクリアランプ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_Clearlamp.png" ) );

			    //this.tx曲名バー.Score = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar score.png" ), false );
			    //this.tx曲名バー.Box = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar box.png" ), false );
			    //this.tx曲名バー.Other = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other.png" ), false );
       //         this.tx曲名バー.Random = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other.png" ), false );
       //         this.tx曲名バー.BackBox = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other.png" ), false );
			    //this.tx選曲バー.Score = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar score selected.png" ), false );
			    //this.tx選曲バー.Box = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar box selected.png" ), false );
			    //this.tx選曲バー.Other = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other selected.png" ), false );
			    //this.tx選曲バー.Random = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other selected.png" ), false );
       //         this.tx選曲バー.BackBox = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_bar other selected.png" ), false );
                //this.txスキル数字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenSelect skill number on list.png"), false);

			    for( int i = 0; i < 15; i++ )
				    this.t曲名バーの生成( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].col文字色 );

                #region[ テクスチャの復元 ]
                int nKeys = this.dicThumbnail.Count;
                string[] keys = new string[ nKeys ];
                this.dicThumbnail.Keys.CopyTo( keys, 0 );
                foreach( var key in keys )
                    this.dicThumbnail[ key ] = this.tパスを指定してサムネイル画像を生成して返す( 0, key, this.stバー情報[ 0 ].eバー種別  );

                nKeys = this.dicThumbnail.Count;
                keys = new string[ nKeys ];
                this.dicMusicName.Keys.CopyTo( keys, 0 );
                foreach( var key in keys )
                    this.dicMusicName[ key ] = this.t指定された文字テクスチャを生成する( key );

                //ここは最初に表示される画像の復元に必要。
                for( int i = 0; i < 15; i++ )
                {
                    //this.tパネルの生成( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].strアーティスト名, this.stバー情報[ i ].col文字色 );
                    if( this.stバー情報[ i ].strPreimageのパス != null )
                    {
                        if( !this.dicThumbnail.ContainsKey( this.stバー情報[ i ].strPreimageのパス ) )
                        {
                            this.tパスを指定してサムネイル画像を生成する( i, this.stバー情報[ i ].strPreimageのパス, this.stバー情報[ i ].eバー種別  );
                            this.dicThumbnail.Add( this.stバー情報[ i ].strPreimageのパス, this.txTumbnail[ i ] );
                        }
                        this.txTumbnail[ i ] = this.dicThumbnail[ this.stバー情報[ i ].strPreimageのパス ];
                    }

                    if( this.stバー情報[ i ].strタイトル文字列 != null )
                    {
                        if( !this.dicMusicName.ContainsKey( this.stバー情報[ i ].strタイトル文字列 ) )
                        {
                            this.t指定された文字テクスチャを生成してバーに格納する( i, this.stバー情報[ i ].strタイトル文字列, this.stバー情報[ i ].eバー種別  );
                            this.dicMusicName.Add( this.stバー情報[ i ].strタイトル文字列, this.txMusicName[ i ] );
                        }
                        this.txMusicName[ i ] = this.dicMusicName[ this.stバー情報[ i ].strタイトル文字列 ];
                    }
                }
                #endregion

                base.OnManagedリソースの作成();
            }

        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txバー背景 );
            CDTXMania.tテクスチャの解放( ref this.txバー選択中 );
            CDTXMania.tテクスチャの解放( ref this.txバー選択中枠 );
            CDTXMania.tテクスチャの解放( ref this.txバー_フォルダ );

            CDTXMania.tテクスチャの解放( ref this.txジャケットパネル背景 );
            CDTXMania.tテクスチャの解放( ref this.txジャケットパネル枠 );
            CDTXMania.tテクスチャの解放( ref this.tx選択中のアーティスト名テクスチャ );

            CDTXMania.tテクスチャの解放( ref this.tx水色 );
            CDTXMania.tテクスチャの解放( ref this.tx黒 );
            CDTXMania.tテクスチャの解放( ref this.tx青色 );
            CDTXMania.tテクスチャの解放( ref this.tx群青 );

            CDTXMania.tテクスチャの解放( ref this.txクリアランプ );

            #region[ ジャケット、曲名テクスチャ画像の解放 ]
            // ジャケット
            int nKeys = this.dicThumbnail.Count;
            string[] keys = new string[ nKeys ];
            this.dicThumbnail.Keys.CopyTo( keys, 0 );
            foreach( var key in keys )
            {
                C共通.tDisposeする( this.dicThumbnail[ key ] );
                this.dicThumbnail[ key ] = null;
            }

            // 曲名
            nKeys = this.dicMusicName.Count;
            keys = new string[ nKeys ];
            this.dicMusicName.Keys.CopyTo( keys, 0 );
            foreach( var key in keys )
            {
                C共通.tDisposeする( this.dicMusicName[ key ] );
                this.dicMusicName[ key ] = null;
            }

            for( int i = 0; i < 15; i++ )
            {
                CDTXMania.tテクスチャの解放( ref this.txTumbnail[ i ] );
                CDTXMania.tテクスチャの解放( ref this.txMusicName[ i ] );
            }
            #endregion

            base.OnManagedリソースの解放();
        }

        //ここではGITADORAスキン専用の表示をする
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
				CDTXMania.stage選曲GITADORA.t選択曲変更通知();
				
				base.b初めての進行描画 = false;
			}
            //-----------------
            #endregion

		
			// まだ選択中の曲が決まってなければ、曲ツリールートの最初の曲にセットする。

			if( ( this.r現在選択中の曲 == null ) && ( CDTXMania.Songs管理.list曲ルート.Count > 0 ) )
				this.r現在選択中の曲 = CDTXMania.Songs管理.list曲ルート[ 0 ];


//			// 本ステージは、(1)登場アニメフェーズ → (2)通常フェーズ　と二段階にわけて進む。
//			// ２つしかフェーズがないので CStage.eフェーズID を使ってないところがまた本末転倒。

			
//			// 進行。

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
				long n現在時刻 = CDTXMania.Timer.n現在時刻;
				
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
						this.t曲名バーの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].col文字色 );

                        //ジャケット画像
                        if( !this.dicThumbnail.ContainsKey( this.stバー情報[ index ].strPreimageのパス ) )
                        {
                            this.tパスを指定してサムネイル画像を生成する( index, this.stバー情報[ index ].strPreimageのパス, this.stバー情報[ index ].eバー種別  );
                            this.dicThumbnail.Add( this.stバー情報[ index ].strPreimageのパス, this.txTumbnail[ index ] );
                        }
                        this.txTumbnail[ index ] = this.dicThumbnail[ this.stバー情報[ index ].strPreimageのパス ];

                        if( !this.dicMusicName.ContainsKey( this.stバー情報[ index ].strタイトル文字列 ) )
                        {
                            this.t指定された文字テクスチャを生成してバーに格納する( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].eバー種別  );
                            this.dicMusicName.Add( this.stバー情報[ index ].strタイトル文字列, this.txMusicName[ index ] );
                        }
                        this.txMusicName[ index ] = this.dicMusicName[ this.stバー情報[ index ].strタイトル文字列 ];
                        
						// stバー情報[] の内容を1行ずつずらす。
						
						C曲リストノード song2 = this.r現在選択中の曲;
						for( int i = 0; i < 7; i++ )
							song2 = this.r前の曲( song2 );

						for( int i = 0; i < 15; i++ )
						{
							int n = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;
							this.stバー情報[ n ].eバー種別 = this.e曲のバー種別を返す( song2 );
                            CDTXMania.t安全にDisposeする( ref this.stバー情報[ n ].txタイトル名 );
                            CDTXMania.t安全にDisposeする( ref this.stバー情報[ n ].txアーティスト名 );
							song2 = this.r次の曲( song2 );
						}

						
						// 新しく最下部に表示されるパネル用のスキル値を取得。

						for( int i = 0; i < 3; i++ )
							this.stバー情報[ index ].nスキル値[ i ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ i ];


						// 1行(100カウント)移動完了。

						this.n現在のスクロールカウンタ -= 100;
						this.n目標のスクロールカウンタ -= 100;

                        t選択曲が変更された( false );				// スクロールバー用に今何番目を選択しているかを更新
                        CDTXMania.t安全にDisposeする( ref this.tx選択中のアーティスト名テクスチャ );

						if( this.n目標のスクロールカウンタ == 0 )
							CDTXMania.stage選曲GITADORA.t選択曲変更通知();		// スクロール完了＝選択曲変更！

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
						this.t曲名バーの生成( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].col文字色 );

                        //ジャケット画像
                        if( !this.dicThumbnail.ContainsKey( this.stバー情報[ index ].strPreimageのパス ) )
                        {
                            this.tパスを指定してサムネイル画像を生成する( index, this.stバー情報[ index ].strPreimageのパス, this.stバー情報[ index ].eバー種別  );
                            this.dicThumbnail.Add( this.stバー情報[ index ].strPreimageのパス, this.txTumbnail[ index ] );
                        }
                        this.txTumbnail[ index ] = this.dicThumbnail[ this.stバー情報[ index ].strPreimageのパス ];

                        if( !this.dicMusicName.ContainsKey( this.stバー情報[ index ].strタイトル文字列 ) )
                        {
                            this.t指定された文字テクスチャを生成してバーに格納する( index, this.stバー情報[ index ].strタイトル文字列, this.stバー情報[ index ].eバー種別  );
                            this.dicMusicName.Add( this.stバー情報[ index ].strタイトル文字列, this.txMusicName[ index ] );
                        }
                        this.txMusicName[ index ] = this.dicMusicName[ this.stバー情報[ index ].strタイトル文字列 ];

						// stバー情報[] の内容を1行ずつずらす。
						
						C曲リストノード song2 = this.r現在選択中の曲;
						for( int i = 0; i < 7; i++ )
							song2 = this.r前の曲( song2 );

						for( int i = 0; i < 15; i++ )
						{
							int n = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;
							this.stバー情報[ n ].eバー種別 = this.e曲のバー種別を返す( song2 );
                            CDTXMania.t安全にDisposeする( ref this.stバー情報[ n ].txタイトル名 );
                            CDTXMania.t安全にDisposeする( ref this.stバー情報[ n ].txアーティスト名 );
							song2 = this.r次の曲( song2 );
						}

		
						// 新しく最上部に表示されるパネル用のスキル値を取得。
						
						for( int i = 0; i < 3; i++ )
							this.stバー情報[ index ].nスキル値[ i ] = (int) song.arスコア[ this.n現在のアンカ難易度レベルに最も近い難易度レベルを返す( song ) ].譜面情報.最大スキル[ i ];


						// 1行(100カウント)移動完了。

						this.n現在のスクロールカウンタ += 100;
						this.n目標のスクロールカウンタ += 100;

						this.t選択曲が変更された( false );				// スクロールバー用に今何番目を選択しているかを更新
                        CDTXMania.t安全にDisposeする( ref this.tx選択中のアーティスト名テクスチャ );

						if( this.n目標のスクロールカウンタ == 0 )
							CDTXMania.stage選曲GITADORA.t選択曲変更通知();		// スクロール完了＝選択曲変更！
						//-----------------
						#endregion
					}

					this.nスクロールタイマ += nアニメ間隔;
				}
				//-----------------
				#endregion

                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F8 ) )
                {
                    this.tTextureCacheClear();
                }
			}


			// 描画。

			if( this.r現在選択中の曲 == null )
			{
                #region [ 曲が１つもないなら「Songs not found.」を表示してここで帰れ。]
                //-----------------
                //this.tSongNotFound();
				//-----------------
				#endregion

				return 0;
			}
            else
            {
                // 曲があればバー背景を描画
                if( this.txバー背景 != null )
                    this.txバー背景.t2D描画( CDTXMania.app.Device, 710, 0 );
            }

            int i選曲バーX座標 = 673; //選曲バーの座標用
            int i選択曲バーX座標 = 665; //選択曲バーの座標用


			#region [ バーテクスチャを描画。]
			//-----------------
            if( this.txバー選択中 != null && ( ( this.n現在のスクロールカウンタ != 0 ) ) )
            {
                this.txバー選択中.t2D描画( CDTXMania.app.Device, 660, 312 );
            }
            //-----------------
            #endregion

            #region[ ジャケットパネル描画 ]
            //-----------------
            if( this.txジャケットパネル背景 != null )
            {
                this.txジャケットパネル背景.t2D描画( CDTXMania.app.Device, 0, 34 );
            }
            //if( this.txTumbnail[ 7 ] != null )
            //{
            //    //縮小 64x64
            //    float fRetW = 297.0f / this.txTumbnail[ 7 ].szテクスチャサイズ.Width;
            //    float fRetH = 297.0f / this.txTumbnail[ 7 ].szテクスチャサイズ.Height;
            //    float fRet = ( fRetW <= fRetH )? fRetW : fRetH;

            //    this.txTumbnail[ 7 ].vc拡大縮小倍率 = new Vector3( fRet, fRet, 1.0f );
            //    this.txTumbnail[ 7 ].t2D描画( CDTXMania.app.Device, 314, 39 );
            //}
            //-----------------
            #endregion

            //登場アニメ自体はいらないはずなんだよな...

            if( !this.b登場アニメ全部完了 )
			{
				#region [ (1) 登場アニメフェーズの描画。]
				//-----------------
				for( int i = 0; i < 15; i++ )	// パネルは全13枚。
				{
					//if( this.ct登場アニメ用[ i ].n現在の値 >= 0 )
					{
						double db割合0to1 = ( (double) this.ct登場アニメ用[ i ].n現在の値 ) / 100.0;
						double db回転率 = Math.Sin( Math.PI * 3 / 5 * db割合0to1 );
						int nパネル番号 = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;
						
						if( i == 7 )
						{
							// (A) 選択曲パネルを描画。

							#region [ バーテクスチャを描画。]
							//-----------------
                            if( this.txバー選択中 != null )
                            {
                                this.txバー選択中.t2D描画( CDTXMania.app.Device, 660, 312 );
                            }
                            if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Box || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.BackBox ) && this.txバー_フォルダ != null )
                            {
                                this.txバー_フォルダ.t2D描画( CDTXMania.app.Device, 660, 315 );
                            }
							//-----------------
							#endregion
							#region [ タイトル名テクスチャを描画。]
							//-----------------
                            if( this.txMusicName[ nパネル番号 ] != null )
                            {
                                this.txMusicName[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 772, 334 );
                            }
                            //-----------------
                            #endregion
                            #region[ ジャケット画像とクリアマークを描画 ]
                            if( this.txTumbnail[ nパネル番号 ] != null && ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Random ) )
                            {
                                //縮小 64x64
                                float fRetW = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width;
                                float fRetH = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;
                                float fRet = ( fRetW <= fRetH )? fRetW : fRetH;

                                this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( fRet, fRet, 1.0f );
                                this.txTumbnail[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 712, 329 );
                            }
                            //クリアマーク
                            if( this.stバー情報[ nパネル番号 ].nスキル値.Drums > 0 )
                            {
                                this.txクリアランプ?.t2D描画( CDTXMania.app.Device, 707, 324, new Rectangle( 0, this.n現在選択中の曲の現在の難易度レベル * 36, 36, 36 ) );
                            }
                            #endregion
                            #region[ 左側ジャケット画像描画 ]
                            //-----------------
                            if( this.txTumbnail[ i ] != null )
                            {
                                //縮小 64x64
                                float fRetW = 297.0f / this.txTumbnail[ i ].szテクスチャサイズ.Width;
                                float fRetH = 297.0f / this.txTumbnail[ i ].szテクスチャサイズ.Height;
                                float fRet = ( fRetW <= fRetH )? fRetW : fRetH;

                                this.txTumbnail[ i ].vc拡大縮小倍率 = new Vector3( fRet, fRet, 1.0f );
                                this.txTumbnail[ i ].t2D描画( CDTXMania.app.Device, 314, 38 );
                            }
                            //-----------------
                            #endregion
                            #region [ スキル値を描画。]
                            //-----------------
                            //if( ( this.actSelectList.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score ) && ( this.e楽器パート != E楽器パート.UNKNOWN ) )
                            //this.actSelectList.tスキル値の描画( i選択曲バーX座標 + 25, y + 12, this.stバー情報[nパネル番号].nスキル値[(int)this.e楽器パート]);
                            //-----------------
                            #endregion
                        }
						else
						{
							// (B) その他のパネルの描画。

							#region [ バーテクスチャの描画。]
							//-----------------
//							int width = (int) ( ( (double) ( ( 720 - this.ptバーの基本座標[ i ].X ) + 1 ) ) / Math.Sin( Math.PI * 3 / 5 ) );
////							int x = 720 - ( (int) ( width * db回転率 ) );
//                            int x = i選曲バーX座標 + 500 - (int)(db割合0to1 * 500);
							int y = this.ptバーの基本座標[ i ].Y;
//							this.tバーの描画( x, y, this.stバー情報[ nパネル番号 ].eバー種別, false );
                            if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Box || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.BackBox ) && this.txバー_フォルダ != null )
                            {
                                this.txバー_フォルダ.t2D描画( CDTXMania.app.Device, 691, y - 12 );
                            }
							//-----------------
							#endregion
                            #region[ ジャケット画像とクリアマークを描画 ]
                            if( this.txTumbnail[ nパネル番号 ] != null && ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Random ) )
                            {
                                //縮小 64x64
                                float fRetW = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width;
                                float fRetH = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;
                                float fRet = ( fRetW <= fRetH )? fRetW : fRetH;

                                this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( fRet, fRet, 1.0f );
                                this.txTumbnail[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 750, y + 7 );
                            }
                            //クリアマーク
                            if( this.stバー情報[ nパネル番号 ].nスキル値.Drums > 0 )
                            {
                                this.txクリアランプ?.t2D描画( CDTXMania.app.Device, 707, y + 2, new Rectangle( 0, this.n現在選択中の曲の現在の難易度レベル * 36, 36, 36 ) );
                            }
                            #endregion
							#region [ タイトル名テクスチャを描画。]
							//-----------------
                            if( this.txMusicName[ nパネル番号 ] != null )
                            {
                                this.txMusicName[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 808, y + 12 );
                            }
							//-----------------
							#endregion
							#region [ スキル値を描画。]
							//-----------------
							//if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score ) && ( this.e楽器パート != E楽器パート.UNKNOWN ) )
								//this.tスキル値の描画( x + 34, y + 18, this.stバー情報[ nパネル番号 ].nスキル値[ (int) this.e楽器パート ] );
							//-----------------
							#endregion
						}
                    }
				}
				//-----------------
				#endregion
			}
			else
			{
				#region [ (2) 通常フェーズの描画。]
				//-----------------
				for( int i = 0; i < 15; i++ )	// パネルは全13枚。
				{
					//if( ( i == 0 && this.n現在のスクロールカウンタ > 0 ) ||		// 最上行は、上に移動中なら表示しない。
					//	( i == 15 && this.n現在のスクロールカウンタ < 0 ) )		// 最下行は、下に移動中なら表示しない。
					//	continue;
                    if( ( i <= 1 || i >= 14 ) && this.n現在のスクロールカウンタ != 0 ) continue;

                    //64px

					int nパネル番号 = ( ( ( this.n現在の選択行 - 7 ) + i ) + 15 ) % 15;
                    int n選択曲のパネル番号 = ( ( ( this.n現在の選択行 - 7 ) + 7 ) + 15 ) % 15;
					int n見た目の行番号 = i;
					int n次のパネル番号 = ( this.n現在のスクロールカウンタ <= 0 ) ? ( ( i + 1 ) % 15 ) : ( ( ( i - 1 ) + 15 ) % 15 );
//					int x = this.ptバーの基本座標[ n見た目の行番号 ].X + ( (int) ( ( this.ptバーの基本座標[ n次のパネル番号 ].X - this.ptバーの基本座標[ n見た目の行番号 ].X ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );
                    int x = i選曲バーX座標;
					int y = this.ptバーの基本座標[ n見た目の行番号 ].Y + ( (int) ( ( this.ptバーの基本座標[ n次のパネル番号 ].Y - this.ptバーの基本座標[ n見た目の行番号 ].Y ) * ( ( (double) Math.Abs( this.n現在のスクロールカウンタ ) ) / 100.0 ) ) );
                    
                    if( i == 7 )
					{
                        // (A) スクロールが停止しているときの選択曲バーの描画。

                        if( this.n現在のスクロールカウンタ == 0 )
                        {
                            //移動中はX座標は通常と同じ位置、止まったら左に突き出し、動き出したら右に戻す。
                            #region [ バーテクスチャを描画。]
                            //-----------------
                            if( this.txバー選択中 != null )
                            {
                                this.txバー選択中.t2D描画( CDTXMania.app.Device, 660, 312 );
                            }
                            if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Box || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.BackBox ) && this.txバー_フォルダ != null )
                            {
                                this.txバー_フォルダ.t2D描画( CDTXMania.app.Device, 660, y - 7 );
                            }
                            //-----------------
                            #endregion
                            #region[ ジャケット画像とクリアマークを描画 ]
                            if( this.txTumbnail[ nパネル番号 ] != null && ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Random ) )
                            {
                                //縮小 64x64
                                float fRetW = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width;
                                float fRetH = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;
                                float fRet = ( fRetW <= fRetH )? fRetW : fRetH;

                                this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( fRet, fRet, 1.0f );
                                this.txTumbnail[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 712, y + 7 );
                            }
                            //クリアマーク
                            if( this.stバー情報[ nパネル番号 ].nスキル値.Drums > 0 )
                            {
                                this.txクリアランプ?.t2D描画( CDTXMania.app.Device, 707, y + 2, new Rectangle( 0, this.n現在選択中の曲の現在の難易度レベル * 36, 36, 36 ) );
                            }
                            #endregion
                            #region [ タイトル名テクスチャを描画。]
                            //-----------------
                            if( this.txMusicName[ nパネル番号 ] != null )
                            {
                                this.txMusicName[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 772, y + 12 );
                            }

                            //CDTXMania.act文字コンソール.tPrint( 0, n見た目の行番号 * 16, C文字コンソール.Eフォント種別.赤, this.stバー情報[ nパネル番号 ].strタイトル文字列 );
                            //CDTXMania.act文字コンソール.tPrint( 200, n見た目の行番号 * 16, C文字コンソール.Eフォント種別.赤, this.stバー情報[ nパネル番号 ].strアーティスト名 );
                            //-----------------
                            #endregion
                            #region[ アーティスト名テクスチャを描画。 ]
                            //-----------------
                            if( this.tx選択中のアーティスト名テクスチャ == null )
                            {
                                this.tx選択中のアーティスト名テクスチャ = this.t指定された文字テクスチャを生成する_小( this.stバー情報[ nパネル番号 ].strアーティスト名 );
                            }

                            if( this.tx選択中のアーティスト名テクスチャ != null )
                            {
                                this.tx選択中のアーティスト名テクスチャ.t2D描画( CDTXMania.app.Device, 793, y + 48 );
                            }
                            //-----------------
                            #endregion
                            #region [ スキル値を描画。]
                            //						//-----------------
                            //						if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score ) && ( this.e楽器パート != E楽器パート.UNKNOWN ) )
                            //                            this.tスキル値の描画(i選択曲バーX座標 + 25, y選曲 + 12, this.stバー情報[nパネル番号].nスキル値[(int)this.e楽器パート]);
                            //						//-----------------
                            #endregion
                        }

                        #region[ ジャケット画像(左側)を描画 ]
                        if( this.txTumbnail[ nパネル番号 ] != null )
                        {
                            float fRetW_Left = 297.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width;
                            float fRetH_Left = 297.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;
                            float fRet_Left = ( fRetW_Left <= fRetH_Left )? fRetW_Left : fRetH_Left;

                            this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( fRet_Left, fRet_Left, 1.0f );
                            this.txTumbnail[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 314, 38 );
                        }
                        #endregion
                    }
					else
					{
						// (B) スクロール中の選択曲バー、またはその他のバーの描画。

						#region [ バーテクスチャを描画。]
						//-----------------
                        //if( this.txバー選択中 != null && ( ( i == 7 ) && ( this.n現在のスクロールカウンタ != 0 ) ) )
                        //{
                        //    this.txバー選択中.t2D描画( CDTXMania.app.Device, 659, 312 );
                        //}
                        if( ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Box || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.BackBox ) && this.txバー_フォルダ != null )
                        {
                            this.txバー_フォルダ.t2D描画( CDTXMania.app.Device, 691, y - 12 );
                        }
						//-----------------
						#endregion
                        #region[ ジャケット画像とクリアマークを描画 ]
                        if( this.txTumbnail[ nパネル番号 ] != null && ( this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Score || this.stバー情報[ nパネル番号 ].eバー種別 == Eバー種別.Random ) )
                        {
                            //縮小 64x64
                            float fRetW = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Width;
                            float fRetH = 64.0f / this.txTumbnail[ nパネル番号 ].szテクスチャサイズ.Height;
                            float fRet = ( fRetW <= fRetH )? fRetW : fRetH;

                            this.txTumbnail[ nパネル番号 ].vc拡大縮小倍率 = new Vector3( fRet, fRet, 1.0f );
                            this.txTumbnail[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 750, y + 7 );
                        }
                        //クリアマーク
                        if( this.stバー情報[ nパネル番号 ].nスキル値.Drums > 0 )
                        {
                            this.txクリアランプ?.t2D描画( CDTXMania.app.Device, 745, y + 2, new Rectangle( 0, this.n現在選択中の曲の現在の難易度レベル * 36, 36, 36 ) );
                        }
                        #endregion
						#region [ タイトル名テクスチャを描画。]
						//-----------------
						//if( this.stバー情報[ nパネル番号 ].txタイトル名 != null )
						//	this.stバー情報[ nパネル番号 ].txタイトル名.t2D描画( CDTXMania.app.Device, x + 0x58, y + 6 );
                        if( this.txMusicName[ nパネル番号 ] != null )
                        {
                            this.txMusicName[ nパネル番号 ].t2D描画( CDTXMania.app.Device, 808, y + 12 );
                        }

                        //CDTXMania.act文字コンソール.tPrint( 0, n見た目の行番号 * 16, C文字コンソール.Eフォント種別.白, this.stバー情報[ nパネル番号 ].strタイトル文字列 );
						//-----------------
						#endregion

						#region [ スキル値を描画。]
						//-----------------
						//if( ( this.actSelectList.stバー情報[ nパネル番号 ].eバー種別 == CActSelect曲リスト.Eバー種別.Score ) && ( this.actSelectList.e楽器パート != E楽器パート.UNKNOWN ) )
							//this.actSelectList.tスキル値の描画( x + 34, y + 18, this.actSelectList.stバー情報[ nパネル番号 ].nスキル値[ (int) this.actSelectList.e楽器パート ] );
						//-----------------
						#endregion
					}
				}

                #region[ カーソル描画 ]
                //-----------------
                if( this.txバー選択中枠 != null )
                {
                    this.txバー選択中枠.t2D描画( CDTXMania.app.Device, 660, 312 );
                    if( this.n現在のスクロールカウンタ == 0 )
                    {
                        this.txジャケットパネル枠.t2D描画( CDTXMania.app.Device, 302, 26 );
                    }
                }

                //-----------------
                #endregion

				//-----------------
				#endregion
			}

            if( CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_決定演出 || CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト )
            {

            }

			#region [ アイテム数の描画 #27648 ]
			tアイテム数の描画();
            #endregion

            //if( CDTXMania.Input管理.Keyboard.bキーが押されている( (int)SlimDXKey.F7 ) )
            {
                //this.tPuzzleFIFOTest();
            }

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



        #region[ リソース ]
        //private CTexture txスキル数字;
        private CTexture txバー背景;
        private CTexture txジャケットパネル背景;
        private CTexture txジャケットパネル枠;
        private CTexture txSongNotFound;
        private CTexture txEnumeratingSongs;
        private CTexture txアイテム数数字; //2017.9.14 kairera0467 XG版だとアイテム数表示はShowCurrentPositionに統合してあるので、こっちでテストして問題なければ消します
        private CTexture tx選択中のアーティスト名テクスチャ;
        private CTexture txバー選択中;
        private CTexture txバー選択中枠;
        private CTexture txバー_フォルダ;
        public STバー tx曲名バー;
        public ST選曲バー tx選曲バー;
        //private CTexture[] txTumbnail = new CTexture[ 15 ];
        private CTexture[] txMusicName = new CTexture[ 15 ];
        //private readonly Point[] ptバーの基本座標 = new Point[] {
        //    new Point(0x2c4, 5), new Point(0x272, 56),
        //    new Point(0x2c4, 5), new Point(0x272, 56),
        //    new Point(0x242, 107), new Point(0x222, 158),
        //    new Point(0x210, 209), new Point(0x1d0, 270), new Point(0x224, 362), new Point(0x242, 413), new Point(0x270, 464), new Point(0x2ae, 515), new Point(0x314, 566), new Point(0x3e4, 617), new Point(0x500, 668)
        //};
        private readonly Point[] ptバーの基本座標 = new Point[] {
            new Point(710, -134),
            new Point(710, -134),
            new Point(710, -58),
            new Point(710, 18),
            new Point(710, 94),
            new Point(710, 170),
            new Point(710, 246),
            new Point(677, 322), //選択
            new Point(710, 398),
            new Point(710, 474),
            new Point(710, 550),
            new Point(710, 626),
            new Point(710, 702),
            new Point(710, 778),
            new Point(0, 854),
            new Point(0, 854)
        };
        private CPrivateFastFont prvFont;
        private CPrivateFastFont prvFontSmall;
        //2014.04.05.kairera0467 GITADORAグラデーションの色。
        //本当は共通のクラスに設置してそれを参照する形にしたかったが、なかなかいいメソッドが無いため、とりあえず個別に設置。
        public Color clGITADORAgradationTopColor = Color.FromArgb( 0, 220, 200 );
        public Color clGITADORAgradationBottomColor = Color.FromArgb( 255, 250, 40 );

        //辞書はジャケット画像と曲名テクスチャの2種類。一応保持制限として80枚ずつにする。
        protected Dictionary<string, CTexture> dicThumbnail = new Dictionary<string, CTexture>();
        protected Dictionary<string, CTexture> dicMusicName = new Dictionary<string, CTexture>();
        #endregion

        #region[ 図形描画用 ]
        CTexture tx水色;
        CTexture tx青色;
        CTexture tx群青;
        CTexture tx黒;
        #endregion

        #region[ 描画サブ ]
        private void tPuzzleFIFOTest()
        {
            if( this.tx水色 != null && this.tx黒 != null )
            {
                //14 群青(右)
                this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                this.tx群青.t2D描画( CDTXMania.app.Device, 401, 206 );

                //13 青(左)
                this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                this.tx青色.t2D描画( CDTXMania.app.Device, 79, 206 );

                //12 水色(左上)
                this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                this.tx水色.t2D描画( CDTXMania.app.Device, 0, 48 );

                //11 黒(左下)
                this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                this.tx黒.t2D描画( CDTXMania.app.Device, -10, 422 );

                //10 青(上)
                this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 0 );
                this.tx青色.t2D描画( CDTXMania.app.Device, 0, -224 );

                //9 黒(下)
                this.tx黒.fZ軸中心回転 = 0;
                this.tx黒.t2D描画( CDTXMania.app.Device, 300, 609 );

                //8 黒(右上)
                this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                this.tx黒.t2D描画( CDTXMania.app.Device, 465, -23 );

                //7 水色(右下)
                this.tx水色.t2D描画( CDTXMania.app.Device, 646, 416 );

                //6 黒(右)
                this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                this.tx黒.t2D描画( CDTXMania.app.Device, 820, 206 );

                //5 群青(左)
                this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( 90 );
                this.tx群青.t2D描画( CDTXMania.app.Device, -223, 196 );

                //4 青(右下)
                this.tx青色.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                this.tx青色.t2D描画( CDTXMania.app.Device, 836, 422 );

                //3 黒(左上)
                this.tx黒.fZ軸中心回転 = C変換.DegreeToRadian( 45 );
                this.tx黒.t2D描画( CDTXMania.app.Device, -309, -23 );

                //2 群青(右上)
                this.tx群青.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                this.tx群青.t2D描画( CDTXMania.app.Device, 854, -24 );

                //1 水色(左下)
                this.tx水色.fZ軸中心回転 = C変換.DegreeToRadian( -45 );
                this.tx水色.t2D描画( CDTXMania.app.Device, -306, 416 );
            }
        }


        //なんとなくメソッド化してみる
        protected void tSongNotFound()
        {
			if( this.bIsEnumeratingSongs )
			{
				if( this.txEnumeratingSongs != null )
				{
					this.txEnumeratingSongs.t2D描画( CDTXMania.app.Device, 460, 300 );
				}
			}
			else
			{
				if( this.txSongNotFound != null )
					this.txSongNotFound.t2D描画( CDTXMania.app.Device, 440, 300 );
			}
        }

		//private void tスキル値の描画( int x, int y, int nスキル値 )
		//{
		//	if( nスキル値 <= 0 || nスキル値 > 100 )		// スキル値 0 ＝ 未プレイ なので表示しない。
		//		return;

		//	int color = ( nスキル値 == 100 ) ? 3 : ( nスキル値 / 25 );

		//	int n百の位 = nスキル値 / 100;
		//	int n十の位 = ( nスキル値 % 100 ) / 10;
		//	int n一の位 = ( nスキル値 % 100 ) % 10;


		//	// 百の位の描画。

		//	if( n百の位 > 0 )
		//		this.tスキル値の描画_１桁描画( x, y, n百の位, color );


		//	// 十の位の描画。

		//	if( n百の位 != 0 || n十の位 != 0 )
		//		this.tスキル値の描画_１桁描画( x + 14, y, n十の位, color );


		//	// 一の位の描画。

		//	this.tスキル値の描画_１桁描画( x + 0x1c, y, n一の位, color );
		//}
		//private void tスキル値の描画_１桁描画( int x, int y, int n数値, int color )
		//{
		//	int dx = ( n数値 % 5 ) * 9;
		//	int dy = ( n数値 / 5 ) * 12;
			
		//	switch( color )
		//	{
		//		case 0:
		//			if( this.txスキル数字 != null )
		//				this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 45 + dx, 24 + dy, 9, 12 ) );
		//			break;

		//		case 1:
		//			if( this.txスキル数字 != null )
		//				this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 45 + dx, dy, 9, 12 ) );
		//			break;

		//		case 2:
		//			if( this.txスキル数字 != null )
		//				this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( dx, 24 + dy, 9, 12 ) );
		//			break;

		//		case 3:
		//			if( this.txスキル数字 != null )
		//				this.txスキル数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( dx, dy, 9, 12 ) );
		//			break;
		//	}
		//}

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
        private CTexture t指定された文字テクスチャを生成してバーに格納する( int nバー番号, string str文字, Eバー種別 eType )
        {
            if( eType == Eバー種別.Random ) // 2017.12.24 ランダムの場合、固有の文字列を付与する
            {
                str文字 = "ランダムカテゴリー内";
            }

            Bitmap bmp = prvFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return this.txMusicName[ nバー番号 ] = tx文字テクスチャ;
        }
        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            //2013.09.05.kairera0467 中央にしか使用することはないので、色は黒固定。
            //現在は機能しない(面倒なので実装してない)が、そのうち使用する予定。
            //PrivateFontの試験運転も兼ねて。
            //CPrivateFastFont
            //if(prvFont != null)
            //    prvFont.Dispose();
            
            Bitmap bmp = prvFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }
        private CTexture t指定された文字テクスチャを生成する_小( string str文字 )
        {
            Bitmap bmp = prvFontSmall.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }
        private void t曲名バーの生成( int nバー番号, string str曲名, Color color )
		{
			if( nバー番号 < 0 || nバー番号 > 15 )
				return;

			try
			{
				SizeF sz曲名;
                
				#region [ 曲名表示に必要となるサイズを取得する。]
				//-----------------
				using( var bmpDummy = new Bitmap( 1, 1 ) )
				{
					var g = Graphics.FromImage( bmpDummy );
					g.PageUnit = GraphicsUnit.Pixel;
					sz曲名 = g.MeasureString( str曲名, this.ft曲リスト用フォント );

                    g.Dispose();
                    bmpDummy.Dispose();
				}
				//-----------------
				#endregion

				int n最大幅px = 0x310;
				int height = 0x25;
				int width = (int) ( ( sz曲名.Width + 2 ) * 0.5f );
				if( width > ( CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2 ) )
					width = CDTXMania.app.Device.Capabilities.MaxTextureWidth / 2;	// 右端断ち切れ仕方ないよね

				float f拡大率X = ( width <= n最大幅px ) ? 0.5f : ( ( (float) n最大幅px / (float) width ) * 0.5f );	// 長い文字列は横方向に圧縮。

				using( var bmp = new Bitmap( width * 2, height * 2, PixelFormat.Format32bppArgb ) )		// 2倍（面積4倍）のBitmapを確保。（0.5倍で表示する前提。）
				using( var g = Graphics.FromImage( bmp ) )
				{
					g.TextRenderingHint = TextRenderingHint.AntiAlias;
					float y = ( ( ( float ) bmp.Height ) / 2f ) - ( ( CDTXMania.ConfigIni.n選曲リストフォントのサイズdot * 2f ) / 2f );
					g.DrawString( str曲名, this.ft曲リスト用フォント, new SolidBrush( this.color文字影 ), (float) 2f, (float) ( y + 2f ) );
					g.DrawString( str曲名, this.ft曲リスト用フォント, new SolidBrush( color ), 0f, y );

					CDTXMania.tテクスチャの解放( ref this.stバー情報[ nバー番号 ].txタイトル名 );

					this.stバー情報[ nバー番号 ].txタイトル名 = new CTexture( CDTXMania.app.Device, bmp, CDTXMania.TextureFormat );
					this.stバー情報[ nバー番号 ].txタイトル名.vc拡大縮小倍率 = new Vector3( f拡大率X, 0.5f, 1f );

                    g.Dispose();
				}
                
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "曲名テクスチャの作成に失敗しました。[{0}]", str曲名 );
				this.stバー情報[ nバー番号 ].txタイトル名 = null;
			}
		}
        private void tバーの描画( int x, int y, Eバー種別 type, bool b選択曲 )
		{
			if( x >= SampleFramework.GameWindowSize.Width || y >= SampleFramework.GameWindowSize.Height )
				return;
            
            if( b選択曲 )
            {
                #region [ (A) 選択曲の場合 ]
                //-----------------
                if( this.tx選曲バー[ (int)type ] != null )
                    this.tx選曲バー[ (int)type ].t2D描画( CDTXMania.app.Device, x, y );	// ヘサキ
                //-----------------
                #endregion
            }
            else
            {
                #region [ (B) その他の場合 ]
                //-----------------
                if (this.tx曲名バー[ (int)type ] != null)
                    this.tx曲名バー[ (int)type ].t2D描画( CDTXMania.app.Device, x, y );		// ヘサキ
                //-----------------
                #endregion
            }
        }
        private void tアイテム数の描画()
        {
            string s = this.nCurrentPosition.ToString() + "/" + this.nNumOfItems.ToString();
            int x = 1260;
            int y = 620;

            for( int p = s.Length - 1; p >= 0; p-- )
            {
                tアイテム数の描画_１桁描画( x, y, s[ p ] );
                x -= 16;
            }
        }
        private void tアイテム数の描画_１桁描画( int x, int y, char s数値 )
        {
            int dx, dy;
            if( s数値 == '/' )
            {
                dx = 96;
                dy = 0;
            }
            else
            {
                int n = (int)s数値 - (int)'0';
                dx = (n % 6) * 16;
                dy = (n / 6) * 16;
            }
            if( this.txアイテム数数字 != null )
            {
                this.txアイテム数数字.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( dx, dy, 16, 16 ) );
            }
        }
        #endregion

        private void tTextureCacheClear()
        {
            if( this.dicMusicName != null )
                this.dicMusicName.Clear();
            
            for( int i = 0; i < this.txMusicName.Length; i++ )
            {
                CDTXMania.tテクスチャの解放( ref this.txMusicName[ i ] );
            }
        }
    }
}
