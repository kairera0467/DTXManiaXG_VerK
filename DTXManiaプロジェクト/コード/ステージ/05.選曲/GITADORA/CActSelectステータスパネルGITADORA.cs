using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
using System.IO;
using System.Linq;
using FDK;

namespace DTXMania
{
	internal class CActSelectステータスパネルGITADORA : CActSelectステータスパネル共通
	{
		// メソッド

		public CActSelectステータスパネルGITADORA()
		{
            this.tレベル数値フォント初期化();
            this.tスキル数値フォント初期化();
            this.tBPM数値フォント初期化();
            base.b活性化してない = true;
		}
		public override void t選択曲が変更された()
		{
			C曲リストノード c曲リストノード = CDTXMania.stage選曲GITADORA.r現在選択中の曲;
			Cスコア cスコア = CDTXMania.stage選曲GITADORA.r現在選択中のスコア;
			if( ( c曲リストノード != null ) && ( cスコア != null ) )
			{
				this.n現在選択中の曲の難易度 = CDTXMania.stage選曲GITADORA.n現在選択中の曲の難易度;
				for( int i = 0; i < 3; i++ )
				{
                    for( int j = 0; j < 5; j++ )
                    {
                        if( c曲リストノード.arスコア[ j ] != null )
                        {
                            this.n現在選択中の曲のレベル難易度毎DGB[j][i] = c曲リストノード.arスコア[j].譜面情報.レベル[i];
                            this.n現在選択中の曲のレベル小数点難易度毎DGB[j][i] = c曲リストノード.arスコア[j].譜面情報.レベルDec[i];
                            //this.n現在選択中の曲の最高ランク難易度毎[j][i] = c曲リストノード.arスコア[j].譜面情報.最大ランク[i];
                            if ( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                                this.n現在選択中の曲の最高ランク難易度毎[ j ][ i ] = c曲リストノード.arスコア[ j ].譜面情報.最大ランク[ i ];
                            else if ( CDTXMania.ConfigIni.eSkillMode == ESkillType.XG )
                                this.n現在選択中の曲の最高ランク難易度毎[ j ][ i ] = ( DTXMania.CScoreIni.tXGランク値を計算して返す( c曲リストノード.arスコア[ j ].譜面情報.最大スキル[ i ] ) == (int)DTXMania.CScoreIni.ERANK.S && DTXMania.CScoreIni.tXGランク値を計算して返す( c曲リストノード.arスコア[ j ].譜面情報.最大スキル[ i ] ) >= 95 ? DTXMania.CScoreIni.tXGランク値を計算して返す( cスコア.譜面情報.最大スキル[ i ] ) : c曲リストノード.arスコア[ j ].譜面情報.最大ランク[ i ]);
                            this.db現在選択中の曲の最高スキル値難易度毎[ j ][ i ] = c曲リストノード.arスコア[ j ].譜面情報.最大スキル[i];
                            this.b現在選択中の曲がフルコンボ難易度毎[j][i] = c曲リストノード.arスコア[j].譜面情報.フルコンボ[i];
                            this.b現在選択中の曲に譜面がある[j][i] = c曲リストノード.arスコア[j].譜面情報.b譜面がある[i];
                            this.db現在選択中の曲の曲別スキル値難易度毎[ j ] = c曲リストノード.arスコア[ j ].譜面情報.最大曲別スキル[ i ];
                        }
                        else //2018.03.18 kairera0467 #38056
                        {
                            this.n現在選択中の曲のレベル難易度毎DGB[j][i] = 0;
                            this.n現在選択中の曲のレベル小数点難易度毎DGB[j][i] = 0;
                            this.n現在選択中の曲の最高ランク難易度毎[j][i] = 99;
                            this.db現在選択中の曲の最高スキル値難易度毎[j][i] = 0; // 2019.04.24 kairera0467 #39152
                            this.b現在選択中の曲がフルコンボ難易度毎[j][i] = false;
                            this.b現在選択中の曲に譜面がある[j][i] = false;
                        }
                    }
                    this.db現在選択中の曲の曲別スキル値[ i ] = this.db現在選択中の曲の曲別スキル値難易度毎.Max();
				}
				for( int i = 0; i < 5; i++ )
				{
                    if( c曲リストノード.arスコア[ i ] != null )
                    {
                        this.n選択中の曲のノート数_難易度毎[ i ].HH = cスコア.譜面情報.n可視チップ数_HH;
                        this.n選択中の曲のノート数_難易度毎[ i ].SD = cスコア.譜面情報.n可視チップ数_SD;
                        this.n選択中の曲のノート数_難易度毎[ i ].BD = cスコア.譜面情報.n可視チップ数_BD;
                        this.n選択中の曲のノート数_難易度毎[ i ].HT = cスコア.譜面情報.n可視チップ数_HT;
                        this.n選択中の曲のノート数_難易度毎[ i ].LT = cスコア.譜面情報.n可視チップ数_LT;
                        this.n選択中の曲のノート数_難易度毎[ i ].CY = cスコア.譜面情報.n可視チップ数_CY;
                        this.n選択中の曲のノート数_難易度毎[ i ].FT = cスコア.譜面情報.n可視チップ数_FT;
                        this.n選択中の曲のノート数_難易度毎[ i ].HHO = cスコア.譜面情報.n可視チップ数_HHO;
                        this.n選択中の曲のノート数_難易度毎[ i ].RD = cスコア.譜面情報.n可視チップ数_RD;
                        this.n選択中の曲のノート数_難易度毎[ i ].LC = cスコア.譜面情報.n可視チップ数_LC;
                        this.n選択中の曲のノート数_難易度毎[ i ].LP = cスコア.譜面情報.n可視チップ数_LP;
                        this.n選択中の曲のノート数_難易度毎[ i ].LBD = cスコア.譜面情報.n可視チップ数_LBD;
                    }
                }
				if( this.r直前の曲 != c曲リストノード )
				{
					this.n難易度開始文字位置 = 0;
				}
				this.r直前の曲 = c曲リストノード;
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n現在選択中の曲の難易度 = 0;
			for( int i = 0; i < 3; i++ )
			{
                this.db現在選択中の曲の曲別スキル値[ i ] = 0.0;
                for (int j = 0; j < 5; j++)
                {
                    this.n現在選択中の曲のレベル難易度毎DGB[j][i] = 0;
                    this.n現在選択中の曲のレベル小数点難易度毎DGB[j][i] = 0;
                    this.db現在選択中の曲の最高スキル値難易度毎[j][i] = 0.0;
                    this.n現在選択中の曲の最高ランク難易度毎[j][i] = (int)CScoreIni.ERANK.UNKNOWN;
                    this.b現在選択中の曲がフルコンボ難易度毎[j][i] = false;
                }
			}
			for( int j = 0; j < 5; j++ )
			{
                this.n選択中の曲のレベル難易度毎[ j ] = 0;

                this.db現在選択中の曲の曲別スキル値難易度毎[j] = 0.0;
			}
			this.n難易度開始文字位置 = 0;
			this.r直前の曲 = null;
			base.On活性化();
		}
		public override void On非活性化()
		{
			this.ct登場アニメ用 = null;
			this.ct難易度スクロール用 = null;
			this.ct難易度矢印用 = null;
            this.ct難易度変更カウンター = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty background.png" ) );
				this.txゲージ用数字他 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_skill icon.png" ), false );
                this.txRank = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_Rank icon.png" ) );
                this.tx難易度パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty panel.png" ) );
                this.tx難易度数字XG = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_LevelNumber.png" ) );
                this.tx難易度カーソル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty sensor.png" ) );

                this.txNotesData背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_NotesData background.png" ) );
                this.txNotesDataゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_NotesData gauge.png" ) );
                this.txTotalNotes数字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_TotalNotes_Number.png" ) );

                this.txレベル数字_中_整数部 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_LevelNumber Medium Int.png") );
                this.txレベル数字_中_小数部 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_LevelNumber Medium Decimal.png") );
                this.txレベル数字_中_小数点 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_LevelNumber Medium Dot.png") );

                this.txスキル数字_大_整数部 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_Skill number Large Int.png") );
                this.txスキル数字_大_小数部 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_Skill number Large Decimal.png") );
                this.txスキル数字_大_小数点 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_Skill number Large Dot.png") );

                this.txBPM数字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_BPM Number.png") );

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.txゲージ用数字他 );
                CDTXMania.tテクスチャの解放( ref this.txRank );
                CDTXMania.tテクスチャの解放( ref this.tx難易度パネル );
                CDTXMania.tテクスチャの解放( ref this.tx難易度数字XG );
                CDTXMania.tテクスチャの解放( ref this.tx難易度カーソル );

                CDTXMania.tテクスチャの解放( ref this.txNotesData背景 );
                CDTXMania.tテクスチャの解放( ref this.txNotesDataゲージ );
                CDTXMania.tテクスチャの解放( ref this.txTotalNotes数字 );

                CDTXMania.tテクスチャの解放( ref this.txレベル数字_中_整数部 );
                CDTXMania.tテクスチャの解放( ref this.txレベル数字_中_小数部 );
                CDTXMania.tテクスチャの解放( ref this.txレベル数字_中_小数点 );

                CDTXMania.tテクスチャの解放( ref this.txスキル数字_大_整数部 );
                CDTXMania.tテクスチャの解放( ref this.txスキル数字_大_小数部 );
                CDTXMania.tテクスチャの解放( ref this.txスキル数字_大_小数点 );

                CDTXMania.tテクスチャの解放( ref this.txBPM数字 );

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				#region [ 初めての進行描画 ]
				//-----------------
				if( base.b初めての進行描画 )
				{
					this.ct登場アニメ用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
					this.ct難易度スクロール用 = new CCounter( 0, 20, 1, CDTXMania.Timer );
					this.ct難易度矢印用 = new CCounter( 0, 5, 80, CDTXMania.Timer );
                    this.ct難易度変更カウンター = new CCounter( 1, 10, 10, CDTXMania.Timer );
					base.b初めての進行描画 = false;
				}
				//-----------------
				#endregion

				// 進行

				this.ct登場アニメ用.t進行();
                this.ct難易度変更カウンター.t進行();

				this.ct難易度スクロール用.t進行();
				if( this.ct難易度スクロール用.b終了値に達した )
				{
					int num = this.n現在の難易度ラベルが完全表示されているかを調べてスクロール方向を返す();
					if( num < 0 )
					{
						this.n難易度開始文字位置--;
					}
					else if( num > 0 )
					{
						this.n難易度開始文字位置++;
					}
					this.ct難易度スクロール用.n現在の値 = 0;
				}
	
				this.ct難易度矢印用.t進行Loop();

                // 描画
                if( CDTXMania.stage選曲GITADORA.r現在選択中のスコア != null )
                {
                    if( this.txパネル本体 != null )
                    {
                        this.txパネル本体.t2D描画( CDTXMania.app.Device, 428, 352 );
                    }
                    #region[ ドラム ]
                    if( CDTXMania.ConfigIni.bDrums有効 )
                    {
                        if ( this.tx難易度パネル != null )
                        {
                            this.tx難易度パネル.t2D描画( CDTXMania.app.Device, 428, 352 );
                        }
                        for( int i = 0; i < 5; i++ )
                        {
                            int[] n難易度整数 = new int[5];
                            int[] n難易度小数 = new int[5];
                            n難易度整数[ i ] = (int)this.n現在選択中の曲のレベル難易度毎DGB[ i ].Drums / 10;
                            n難易度小数[ i ] = ( this.n現在選択中の曲のレベル難易度毎DGB[ i ].Drums - (n難易度整数[ i ] * 10 ) ) * 10;
                            n難易度小数[ i ] += this.n現在選択中の曲のレベル小数点難易度毎DGB[ i ].Drums;

                            if( /* this.str難易度ラベル[ i ] != null && */ this.b現在選択中の曲に譜面がある[ i ].Drums )
                            {
                                //this.t大文字表示(73 + this.n本体X[ j ] + (i * 143), 19 + this.n本体Y[j] - y差分[i], string.Format("{0:0}", n難易度整数[i]));
                                //this.t小文字表示(102 + this.n本体X[ j ] + (i * 143), 37 + this.n本体Y[j] - y差分[i], string.Format("{0,2:00}", n難易度小数[i]));
                                //this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                                this.tレベル値の描画_中( 547, 626 - ( i * 60 ), string.Format("{0:0}", n難易度整数[i]) + "." + string.Format("{0,2:00}", n難易度小数[i]) );
                            }
                            //else if ((this.str難易度ラベル[i] != null && !this.b現在選択中の曲に譜面がある[i][j]) || CDTXMania.stage選曲XG.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM)
                            //{
                            //    this.t大文字表示(73 + this.n本体X[j] + (i * 143), 19 + this.n本体Y[j] - y差分[i], ("-"));
                            //    this.t小文字表示(102 + this.n本体X[j] + (i * 143), 37 + this.n本体Y[j] - y差分[i], ("--"));
                            //    this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                            //}

                            //if( this.b現在選択中の曲に譜面がある[ i ].Drums )
                            //{
                            //    CDTXMania.act文字コンソール.tPrint( 570, 634 - ( 60 * i ), C文字コンソール.Eフォント種別.白, string.Format( "{0:0}", n難易度整数[i] ) + "." + string.Format("{0,2:00}", n難易度小数[i]) );
                            //}
                            #region[ ランク画像 ]
                            int rank = this.n現在選択中の曲の最高ランク難易度毎[ i ].Drums;
                            if( rank != 99 )
                            {
                                if( rank < 0 ) rank = 0;
                                else if( rank > 6 ) rank = 6;

                                this.txRank?.t2D描画( CDTXMania.app.Device, 453, 612 - ( i * 60 ), this.rectRank文字[ rank ] );
                            }
                            #endregion
                            #region[ FC/EXC ]
                            if( this.db現在選択中の曲の最高スキル値難易度毎[ i ].Drums >= 100.0 )
                            {
                                this.txRank?.t2D描画( CDTXMania.app.Device, 487, 612 - ( i * 60 ), new Rectangle( 0, 56, 28, 28 ) );
                            }
                            else if( this.b現在選択中の曲がフルコンボ難易度毎[ i ].Drums )
                            {
                                this.txRank?.t2D描画( CDTXMania.app.Device, 487, 612 - ( i * 60 ), new Rectangle( 28, 56, 28, 28 ) );
                            }
                            #endregion
                        }

                        #region [ 選択曲の 最高スキル値の描画 ]
                        //-----------------
                        //for (int j = 0; j < 3; j++)
                        //{
                            for (int i = 0; i < 5; i++)
                            {
                                //if( j == 0 )
                                {
                                    if( this.db現在選択中の曲の最高スキル値難易度毎[ i ].Drums != 0.00 )
                                    {
                                        // ToDo:エクセはどう表示される?
                                        CDTXMania.act文字コンソール.tPrint( 450, 645 - ( i * 60 ), C文字コンソール.Eフォント種別.白, string.Format( "{0,6:##0.00}%", this.db現在選択中の曲の最高スキル値難易度毎[ i ].Drums ) );
                                    }
                                }
                            }
                        //}
                        //-----------------
                        #endregion
                        this.t難易度カーソル描画( 426, base.n現在選択中の曲の難易度 );

                        if( CDTXMania.stage選曲GITADORA.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE )
                        {
                            #region[ 曲別スキル値(左側)を描画 ]
                            this.tスキル値の描画_大( 79, 216, this.db現在選択中の曲の曲別スキル値.Drums );
                            #endregion
                            #region[ BPM値を描画 ]
                            // ToDo:速度変化への対応(DB側もいじらないとダメ)
                            this.tBPM値の描画( 120, 302, CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.最低Bpm, CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.最大Bpm );
                            #endregion
                        }
                    }
                    #endregion
                    #region[ ギター ]
                    if( CDTXMania.ConfigIni.bGuitar有効 )
                    {
                        if( this.txパネル本体 != null )
                        {
                            this.txパネル本体.t2D描画( CDTXMania.app.Device, 428 - 236, 352 );
                        }

                        for( int j = 1; j < 3; j++ )
                        {
                            if ( this.tx難易度パネル != null )
                            {
                                this.tx難易度パネル.t2D描画( CDTXMania.app.Device, j == 1 ? 428 - 236 : 428, 352 );
                            }
                            for( int i = 0; i < 5; i++ )
                            {
                                int[] n難易度整数 = new int[5];
                                int[] n難易度小数 = new int[5];
                                n難易度整数[ i ] = (int)this.n現在選択中の曲のレベル難易度毎DGB[ i ][ j ] / 10;
                                n難易度小数[ i ] = ( this.n現在選択中の曲のレベル難易度毎DGB[ i ][ j ] - (n難易度整数[ i ] * 10 ) ) * 10;
                                n難易度小数[ i ] += this.n現在選択中の曲のレベル小数点難易度毎DGB[ i ][ j ];

                                if( /* this.str難易度ラベル[ i ] != null && */ this.b現在選択中の曲に譜面がある[ i ][ j ] )
                                {
                                    //this.t大文字表示(73 + this.n本体X[ j ] + (i * 143), 19 + this.n本体Y[j] - y差分[i], string.Format("{0:0}", n難易度整数[i]));
                                    //this.t小文字表示(102 + this.n本体X[ j ] + (i * 143), 37 + this.n本体Y[j] - y差分[i], string.Format("{0,2:00}", n難易度小数[i]));
                                    //this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                                    this.tレベル値の描画_中( 547 - (j == 1 ? 236 : 0), 626 - ( i * 60 ), string.Format("{0:0}", n難易度整数[i]) + "." + string.Format("{0,2:00}", n難易度小数[i]) );
                                }
                                //else if ((this.str難易度ラベル[i] != null && !this.b現在選択中の曲に譜面がある[i][j]) || CDTXMania.stage選曲XG.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM)
                                //{
                                //    this.t大文字表示(73 + this.n本体X[j] + (i * 143), 19 + this.n本体Y[j] - y差分[i], ("-"));
                                //    this.t小文字表示(102 + this.n本体X[j] + (i * 143), 37 + this.n本体Y[j] - y差分[i], ("--"));
                                //    this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                                //}

                                //if( this.b現在選択中の曲に譜面がある[ i ].Drums )
                                //{
                                //    CDTXMania.act文字コンソール.tPrint( 570, 634 - ( 60 * i ), C文字コンソール.Eフォント種別.白, string.Format( "{0:0}", n難易度整数[i] ) + "." + string.Format("{0,2:00}", n難易度小数[i]) );
                                //}
                                #region[ ランク画像 ]
                                int rank = this.n現在選択中の曲の最高ランク難易度毎[ i ][ j ];
                                if( rank != 99 )
                                {
                                    if( rank < 0 ) rank = 0;
                                    else if( rank > 6 ) rank = 6;

                                    this.txRank?.t2D描画( CDTXMania.app.Device, 453, 612 - ( i * 60 ), this.rectRank文字[ rank ] );
                                }
                                #endregion
                                #region[ FC/EXC ]
                                if( this.db現在選択中の曲の最高スキル値難易度毎[ i ][ j ] >= 100.0 )
                                {
                                    this.txRank?.t2D描画( CDTXMania.app.Device, 487, 612 - ( i * 60 ), new Rectangle( 0, 56, 28, 28 ) );
                                }
                                else if( this.b現在選択中の曲がフルコンボ難易度毎[ i ][ j ] )
                                {
                                    this.txRank?.t2D描画( CDTXMania.app.Device, 487, 612 - ( i * 60 ), new Rectangle( 28, 56, 28, 28 ) );
                                }
                                #endregion
                            }

                            #region [ 選択曲の 最高スキル値の描画 ]
                            //-----------------
                            //for (int j = 0; j < 3; j++)
                            //{
                                for (int i = 0; i < 5; i++)
                                {
                                    //if( j == 0 )
                                    {
                                        if( this.db現在選択中の曲の最高スキル値難易度毎[ i ].Drums != 0.00 )
                                        {
                                            // ToDo:エクセはどう表示される?
                                            CDTXMania.act文字コンソール.tPrint( 450, 645 - ( i * 60 ), C文字コンソール.Eフォント種別.白, string.Format( "{0,6:##0.00}%", this.db現在選択中の曲の最高スキル値難易度毎[ i ].Drums ) );
                                        }
                                    }
                                }
                            //}
                            //-----------------
                            #endregion
                            this.t難易度カーソル描画( 426 - (j == 1 ? 236 : 0), base.n現在選択中の曲の難易度 );

                            if( CDTXMania.stage選曲GITADORA.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE )
                            {
                                #region[ 曲別スキル値(左側)を描画 ]
                                this.tスキル値の描画_大( 79, 216, this.db現在選択中の曲の曲別スキル値.Drums );
                                #endregion
                                #region[ BPM値を描画 ]
                                // ToDo:速度変化への対応(DB側もいじらないとダメ)
                                this.tBPM値の描画( 120, 302, CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.最低Bpm, CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.最大Bpm );
                                #endregion
                            }
                        }


                    }
                    #endregion
                }


                //ノート数グラフ
                // Todo:ギター・ベースモード時の表示
                if( CDTXMania.stage選曲GITADORA.r現在選択中のスコア != null )
                {
                    if( CDTXMania.ConfigIni.bDrums有効 )
                    {
                        if( this.txNotesData背景 != null )
                        {
                            this.txNotesData背景.t2D描画( CDTXMania.app.Device, 213, 353 );
                            if( this.txNotesDataゲージ != null )
                            {
                                //グラフ背景
                                for( int i = 0; i < 10; i++ )
                                {
                                    this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 260 + i * 12, 355, new Rectangle( 0, 0, 4, 270 ) );
                                }

                                double ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].LC );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 260, 625 - (int)(270 * ret), new Rectangle( 4, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].HH + this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].HHO );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 272, 625 - (int)(270 * ret), new Rectangle( 8, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].LP + this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].LBD );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 284, 625 - (int)(270 * ret), new Rectangle( 12, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].SD );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 296, 625 - (int)(270 * ret), new Rectangle( 16, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].HT );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 308, 625 - (int)(270 * ret), new Rectangle( 20, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].BD );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 320, 625 - (int)(270 * ret), new Rectangle( 24, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].LT );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 332, 625 - (int)(270 * ret), new Rectangle( 28, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].FT );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 344, 625 - (int)(270 * ret), new Rectangle( 32, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].CY );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 356, 625 - (int)(270 * ret), new Rectangle( 36, 0, 4, (int)(270 * ret) ) );
                                ret = this.dbノーツグラフゲージ割合計算( this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].RD );
                                this.txNotesDataゲージ.t2D描画( CDTXMania.app.Device, 368, 625 - (int)(270 * ret), new Rectangle( 40, 0, 4, (int)(270 * ret) ) );
                            }

                            // TotalNotes
                            this.tTotalNotes数字表示( 280, 658, String.Format( "{0,5:####0}", this.n選択中の曲のノート数_難易度毎[ base.n現在選択中の曲の難易度 ].Drums ) );
                        }
                    }

                    //CDTXMania.act文字コンソール.tPrint( 380, 400, C文字コンソール.Eフォント種別.白, "LC:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.LC.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16, C文字コンソール.Eフォント種別.白, "HH:" + ( CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.HH + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.HHO ).ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 2, C文字コンソール.Eフォント種別.白, "LP:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.LP.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 3, C文字コンソール.Eフォント種別.白, "SD:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.SD.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 4, C文字コンソール.Eフォント種別.白, "HT:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.HT.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 5, C文字コンソール.Eフォント種別.白, "BD:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.BD.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 6, C文字コンソール.Eフォント種別.白, "LT:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.LT.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 7, C文字コンソール.Eフォント種別.白, "FT:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.FT.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 8, C文字コンソール.Eフォント種別.白, "CY:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.CY.ToString() );
                    //CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 9, C文字コンソール.Eフォント種別.白, "RD:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.RD.ToString() );
                }
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct ST数字
		{
			public char ch;
			public Rectangle rc;
			public ST数字( char ch, Rectangle rc )
			{
				this.ch = ch;
				this.rc = rc;
			}
		}

		private CCounter ct登場アニメ用;
		private CCounter ct難易度スクロール用;
		private CCounter ct難易度矢印用;
        
        private int[] n選択中の曲のレベル難易度毎 = new int[5];
        
		private int n難易度開始文字位置;
		private const int n難易度表示可能文字数 = 0x24;
		private C曲リストノード r直前の曲;
		private CTexture txゲージ用数字他;
        private CTexture tx難易度パネル;
        private CTexture tx難易度数字XG;
        
        private CTexture tx難易度カーソル;

        private CTexture txNotesData背景;
        private CTexture txNotesDataゲージ; //クリアバーだけはソフトウェア側で生成する
        private CTexture txTotalNotes数字;

        private CTexture txレベル数字_中_整数部;
        private CTexture txレベル数字_中_小数部;
        private CTexture txレベル数字_中_小数点;
        private CTexture txスキル数字_大_整数部;
        private CTexture txスキル数字_大_小数部;
        private CTexture txスキル数字_大_小数点;
        private CTexture txBPM数字; // 2019.04.30 kairera0467

        private CTexture txRank;

        private struct ST数字フォント
        {
            public char ch文字;
            public Rectangle rect;
        }

        private ST数字フォント[] STレベル数字_中_整数;
        private ST数字フォント[] STレベル数字_中_小数;
        private ST数字フォント[] STスキル数字_大_整数;
        private ST数字フォント[] STスキル数字_大_小数;
        private ST数字フォント[] STBPM数字;

        private void tレベル数値フォント初期化()
        {
            this.STレベル数字_中_整数 = new ST数字フォント[]{
                new ST数字フォント(){ ch文字 = '0', rect = new Rectangle( 0, 0, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '1', rect = new Rectangle( 28, 0, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '2', rect = new Rectangle( 56, 0, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '3', rect = new Rectangle( 84, 0, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '4', rect = new Rectangle( 112, 0, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '5', rect = new Rectangle( 0, 38, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '6', rect = new Rectangle( 28, 38, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '7', rect = new Rectangle( 56, 38, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '8', rect = new Rectangle( 84, 38, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '9', rect = new Rectangle( 112, 38, 28, 38 ) },
                new ST数字フォント(){ ch文字 = '-', rect = new Rectangle( 140, 0, 28, 38 ) }
            };
            this.STレベル数字_中_小数 = new ST数字フォント[]{
                new ST数字フォント(){ ch文字 = '0', rect = new Rectangle( 0, 0, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '1', rect = new Rectangle( 20, 0, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '2', rect = new Rectangle( 40, 0, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '3', rect = new Rectangle( 60, 0, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '4', rect = new Rectangle( 80, 0, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '5', rect = new Rectangle( 0, 28, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '6', rect = new Rectangle( 20, 28, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '7', rect = new Rectangle( 40, 28, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '8', rect = new Rectangle( 60, 28, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '9', rect = new Rectangle( 80, 28, 20, 28 ) },
                new ST数字フォント(){ ch文字 = '-', rect = new Rectangle( 100, 0, 20, 28 ) }
            };
        }

        private void tスキル数値フォント初期化()
        {
            this.STスキル数字_大_整数 = new ST数字フォント[ 10 ];
            this.STスキル数字_大_整数[ 0 ] = new ST数字フォント() { ch文字 = '0', rect = new Rectangle( 0, 0, 64, 64 ) };
            this.STスキル数字_大_整数[ 1 ] = new ST数字フォント() { ch文字 = '1', rect = new Rectangle( 64, 0, 64, 64 ) };
            this.STスキル数字_大_整数[ 2 ] = new ST数字フォント() { ch文字 = '2', rect = new Rectangle( 128, 0, 64, 64 ) };
            this.STスキル数字_大_整数[ 3 ] = new ST数字フォント() { ch文字 = '3', rect = new Rectangle( 192, 0, 64, 64 ) };
            this.STスキル数字_大_整数[ 4 ] = new ST数字フォント() { ch文字 = '4', rect = new Rectangle( 256, 0, 64, 64 ) };
            this.STスキル数字_大_整数[ 5 ] = new ST数字フォント() { ch文字 = '5', rect = new Rectangle( 0, 64, 64, 64 ) };
            this.STスキル数字_大_整数[ 6 ] = new ST数字フォント() { ch文字 = '6', rect = new Rectangle( 64, 64, 64, 64 ) };
            this.STスキル数字_大_整数[ 7 ] = new ST数字フォント() { ch文字 = '7', rect = new Rectangle( 128, 64, 64, 64 ) };
            this.STスキル数字_大_整数[ 8 ] = new ST数字フォント() { ch文字 = '8', rect = new Rectangle( 192, 64, 64, 64 ) };
            this.STスキル数字_大_整数[ 9 ] = new ST数字フォント() { ch文字 = '9', rect = new Rectangle( 256, 64, 64, 64 ) };
            this.STスキル数字_大_小数 = new ST数字フォント[ 10 ];
            this.STスキル数字_大_小数[ 0 ] = new ST数字フォント() { ch文字 = '0', rect = new Rectangle( 0, 0, 46, 46 ) };
            this.STスキル数字_大_小数[ 1 ] = new ST数字フォント() { ch文字 = '1', rect = new Rectangle( 46, 0, 46, 46 ) };
            this.STスキル数字_大_小数[ 2 ] = new ST数字フォント() { ch文字 = '2', rect = new Rectangle( 92, 0, 46, 46 ) };
            this.STスキル数字_大_小数[ 3 ] = new ST数字フォント() { ch文字 = '3', rect = new Rectangle( 138, 0, 46, 46 ) };
            this.STスキル数字_大_小数[ 4 ] = new ST数字フォント() { ch文字 = '4', rect = new Rectangle( 184, 0, 46, 46 ) };
            this.STスキル数字_大_小数[ 5 ] = new ST数字フォント() { ch文字 = '5', rect = new Rectangle( 0, 46, 46, 46 ) };
            this.STスキル数字_大_小数[ 6 ] = new ST数字フォント() { ch文字 = '6', rect = new Rectangle( 46, 46, 46, 46 ) };
            this.STスキル数字_大_小数[ 7 ] = new ST数字フォント() { ch文字 = '7', rect = new Rectangle( 92, 46, 46, 46 ) };
            this.STスキル数字_大_小数[ 8 ] = new ST数字フォント() { ch文字 = '8', rect = new Rectangle( 138, 46, 46, 46 ) };
            this.STスキル数字_大_小数[ 9 ] = new ST数字フォント() { ch文字 = '9', rect = new Rectangle( 184, 46, 46, 46 ) };
        }

        private void tBPM数値フォント初期化()
        {
            this.STBPM数字 = new ST数字フォント[] {
                new ST数字フォント(){ ch文字 = '0', rect = new Rectangle( 0, 0, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '1', rect = new Rectangle( 28, 0, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '2', rect = new Rectangle( 56, 0, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '3', rect = new Rectangle( 84, 0, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '4', rect = new Rectangle( 112, 0, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '5', rect = new Rectangle( 0, 28, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '6', rect = new Rectangle( 28, 28, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '7', rect = new Rectangle( 56, 28, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '8', rect = new Rectangle( 84, 28, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '9', rect = new Rectangle( 112, 28, 28, 28 ) },
                new ST数字フォント(){ ch文字 = '~', rect = new Rectangle( 0, 56, 28, 28 ) }
            };

        }

        // 2019.04.21 kairera0467
        private void tレベル値の描画_中( int x, int y, string strレベル値 )
        {
            //if( dbレベル値 < 0 || dbレベル値 > 10 )
            //    return;

            // 1文字あたりのマージン
            int n文字間隔_整数部 = 28;
            int n文字間隔_小数部 = 17;
            bool b整数部処理中 = true;
            string formatText = strレベル値;

            for( int i = 0; i < formatText.Length; i++ )
            {
                char c = formatText[ i ];

                if( c.Equals( '.' ) )
                {
                    // 小数点だったら小数点を描画してフラグ切り替えてcontinue
                    this.txレベル数字_中_小数点?.t2D描画( CDTXMania.app.Device, x, y + 28 );
                    b整数部処理中 = false;
                    x += 7;
                    continue;
                }
                else if( c.Equals( ' ' ) )
                {
                    // 空白ならなにもせずcontinue
                    continue;
                }

                for( int j = 0; j < 11; j++ )
                {
                    if( c.Equals( this.STレベル数字_中_整数[ j ].ch文字 ) )
                    {
                        if( b整数部処理中 )
                        {
                            this.txレベル数字_中_整数部?.t2D描画( CDTXMania.app.Device, x, y, this.STレベル数字_中_整数[ j ].rect );
                            x += n文字間隔_整数部;
                        }
                        else
                        {
                            this.txレベル数字_中_小数部?.t2D描画( CDTXMania.app.Device, x, y + 9, this.STレベル数字_中_小数[ j ].rect );
                            x += n文字間隔_小数部;
                        }
                    }
                }
            }
        }

        // 2019.04.20 kairera0467
        private void tスキル値の描画_大( int x, int y, double dbスキル値 )
        {
            if( dbスキル値 <= 0 || dbスキル値 > 200 )
                return;

            // 1文字あたりのマージン
            int n文字間隔_整数部 = 41;
            int n文字間隔_小数部 = 30;
            bool b整数部処理中 = true;
            dbスキル値 = dbスキル値 * 100.0;
            dbスキル値 = Math.Floor( dbスキル値 );
            dbスキル値 = dbスキル値 / 100.0;
            //string formatText = string.Format( "{0,6:##0.00}", dbスキル値.ToString() );
            string formatText = dbスキル値.ToString( "##0.00" ); //string.Format( "{0,6:##0.00}", dbスキル値.ToString() );

            for( int i = 0; i < formatText.Length; i++ )
            {
                char c = formatText[ i ];

                if( c.Equals( '.' ) )
                {
                    // 小数点だったら小数点を描画してフラグ切り替えてcontinue
                    this.txスキル数字_大_小数点?.t2D描画( CDTXMania.app.Device, x, y + 54 );
                    b整数部処理中 = false;
                    x += 10;
                    continue;
                }
                else if( c.Equals( ' ' ) )
                {
                    // 空白ならなにもせずcontinue
                    continue;
                }

                for( int j = 0; j < 10; j++ )
                {
                    if( c.Equals( this.STスキル数字_大_整数[ j ].ch文字 ) )
                    {
                        if( b整数部処理中 )
                        {
                            this.txスキル数字_大_整数部?.t2D描画( CDTXMania.app.Device, x, y, this.STスキル数字_大_整数[ j ].rect );
                            x += n文字間隔_整数部;
                        }
                        else
                        {
                            this.txスキル数字_大_小数部?.t2D描画( CDTXMania.app.Device, x, y + 18, this.STスキル数字_大_小数[ j ].rect );
                            x += n文字間隔_小数部;
                        }
                    }
                }
            }
        }

        // 2019.04.20 kairera0467
        /// <summary>
        /// BPM値を画像フォントを用いて描画する
        /// ※速度変化しない場合は最小、最大のどちらか片方を-1にすること。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dbBPM最小">最小速度</param>
        /// <param name="dbBPM最大">最大速度</param>
        private void tBPM値の描画( int x, int y, double dbBPM最小, double dbBPM最大 )
        {
            if( dbBPM最小 <= 0 || dbBPM最小 > 9999 )
                return;

            // 1文字あたりのマージン
            int n文字間隔 = 18;
            string formatText = string.Format( "{0,4:###0}", ((int)dbBPM最小).ToString() );
            if( ( dbBPM最小 != -1 && dbBPM最大 != -1 ) && ( dbBPM最小 == dbBPM最大 ) ) {
                formatText = string.Format( "{0,4:###0}", ((int)dbBPM最大).ToString() );
                x += 34;
            }
            else if( dbBPM最小 != -1 && dbBPM最大 != -1 )
                formatText = string.Format( "{0,4:###0}" + "~" + "{1,4:###0}", ((int)dbBPM最小).ToString(), ((int)dbBPM最大).ToString() );

            for( int i = 0; i < formatText.Length; i++ )
            {
                char c = formatText[ i ];
                for( int j = 0; j < 11; j++ )
                {
                    if( c.Equals( this.STBPM数字[ j ].ch文字 ) )
                    {
                        this.txBPM数字?.t2D描画( CDTXMania.app.Device, x, y, this.STBPM数字[ j ].rect );
                        x += n文字間隔;
                    }
                }
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
            public ST文字位置( char ch, Point pt )
            {
                this.ch = ch;
                this.pt = pt;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct ST達成率数字
        {
            public char ch;
            public Rectangle rc;
            public ST達成率数字( char ch, Rectangle rc )
            {
                this.ch = ch;
                this.rc = rc;
            }
        }
        //2016.02.23 kairera0467 初期化の行数を大幅短縮。
        private ST文字位置[] st小文字位置 = new ST文字位置[] {
            new ST文字位置( '0', new Point( 13, 40 ) ),
            new ST文字位置( '1', new Point( 26, 40 ) ),
            new ST文字位置( '2', new Point( 39, 40 ) ),
            new ST文字位置( '3', new Point( 52, 40 ) ),
            new ST文字位置( '4', new Point( 65, 40 ) ),
            new ST文字位置( '5', new Point( 78, 40 ) ),
            new ST文字位置( '6', new Point( 91, 40 ) ),
            new ST文字位置( '7', new Point( 105, 40 ) ),
            new ST文字位置( '8', new Point( 118, 40 ) ),
            new ST文字位置( '9', new Point( 131, 40 ) ),
            new ST文字位置( '-', new Point( 0, 40 ) )
        };

        private ST文字位置[] st大文字位置 = new ST文字位置[] {
            new ST文字位置( '.', new Point( 144, 0 ) ),
            new ST文字位置( '1', new Point( 22, 0 ) ),
            new ST文字位置( '2', new Point( 44, 0 ) ),
            new ST文字位置( '3', new Point( 66, 0 ) ),
            new ST文字位置( '4', new Point( 88, 0 ) ),
            new ST文字位置( '5', new Point( 110, 0 ) ),
            new ST文字位置( '6', new Point( 132, 0 ) ),
            new ST文字位置( '7', new Point( 153, 0 ) ),
            new ST文字位置( '8', new Point( 176, 0 ) ),
            new ST文字位置( '9', new Point( 198, 0 ) ),
            new ST文字位置( '0', new Point( 220, 0 ) ),
            new ST文字位置( '-', new Point( 0, 0 ) )
        };

        private ST達成率数字[] st達成率数字 = new ST達成率数字[]{
            new ST達成率数字( '0', new Rectangle( 0, 62, 7, 16 ) ),
            new ST達成率数字( '1', new Rectangle( 7, 62, 7, 16 ) ),
            new ST達成率数字( '2', new Rectangle( 14, 62, 7, 16 ) ),
            new ST達成率数字( '3', new Rectangle( 21, 62, 7, 16 ) ),
            new ST達成率数字( '4', new Rectangle( 28, 62, 7, 16 ) ),
            new ST達成率数字( '5', new Rectangle( 35, 62, 7, 16 ) ),
            new ST達成率数字( '6', new Rectangle( 42, 62, 7, 16 ) ),
            new ST達成率数字( '7', new Rectangle( 49, 62, 7, 16 ) ),
            new ST達成率数字( '8', new Rectangle( 56, 62, 7, 16 ) ),
            new ST達成率数字( '9', new Rectangle( 63, 62, 7, 16 ) ),
            new ST達成率数字( '%', new Rectangle( 70, 62, 9, 16 ) ),
            new ST達成率数字( '.', new Rectangle( 79, 62, 3, 16 ) )
        };

        private ST文字位置[] stノート数数字 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 16, 0 ) ),
            new ST文字位置( '2', new Point( 32, 0 ) ),
            new ST文字位置( '3', new Point( 48, 0 ) ),
            new ST文字位置( '4', new Point( 64, 0 ) ),
            new ST文字位置( '5', new Point( 0, 24 ) ),
            new ST文字位置( '6', new Point( 16, 24 ) ),
            new ST文字位置( '7', new Point( 32, 24 ) ),
            new ST文字位置( '8', new Point( 48, 24 ) ),
            new ST文字位置( '9', new Point( 64, 24 ) )
        };

        private Rectangle[] rectRank文字 = new Rectangle[]
        {
            new Rectangle( 0, 0, 28, 28 ),
            new Rectangle( 28, 0, 28, 28 ),
            new Rectangle( 56, 0, 28, 28 ),
            new Rectangle( 84, 0, 28, 28 ),
            new Rectangle( 112, 0, 28, 28 ),
            new Rectangle( 140, 0, 28, 28 ),
            new Rectangle( 168, 0, 28, 28 ),
            new Rectangle( 0, 28, 28, 28 )
        };

        private void t小文字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for( int i = 0; i < this.st小文字位置.Length; i++ )
                {
                    if (this.st小文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 13, 22);
                        if (this.tx難易度数字XG != null)
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += 12;
            }
        }
        private void t大文字表示(int x, int y, string str)
        {
            for( int i = 0; i < str.Length; i++ )
            {
                char c = str[ i ];
                for( int j = 0; j < this.st大文字位置.Length; j++ )
                {
                    if( this.st大文字位置[ j ].ch == c )
                    {
                        Rectangle rc画像内の描画領域 = new Rectangle( this.st大文字位置[ j ].pt.X, this.st大文字位置[ j ].pt.Y, 22, 40);
                        if( c == '.' )
                        {
                            rc画像内の描画領域.Width -= 2;
                            rc画像内の描画領域.Height -= 2;
                        }
                        if( this.tx難易度数字XG != null )
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rc画像内の描画領域);
                        }
                        break;
                    }
                }
                if( c == '.' ) x += 0;
                else x += 24;
            }
        }
        private void t達成率表示(int x, int y, string str)
        {
            for (int j = 0; j < str.Length; j++)
            {
                char c = str[j];
                for (int i = 0; i < this.st達成率数字.Length; i++)
                {
                    if (this.st達成率数字[i].ch == c)
                    {
                        Rectangle rectangle = new Rectangle(this.st達成率数字[i].rc.X, this.st達成率数字[i].rc.Y, 7, 16);

                        if( c == '.' )
                            rectangle.Width -= 2;
                        else if( c == '%' )
                            rectangle.Width += 2;
                        if (this.tx難易度数字XG != null)
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                if (c == '.')
                    x += 4;
                else
                    x += 8;
            }
        }

        private void tTotalNotes数字表示( int x, int y, string str )
        {
            foreach( char ch in str )
            {
                for( int i = 0; i < this.stノート数数字.Length; i++ )
                {
                    if( this.st小文字位置[i].ch == ch )
                    {
                        Rectangle rectangle = new Rectangle( this.stノート数数字[ i ].pt.X, this.stノート数数字[ i ].pt.Y, 16, 24 );
                        if( this.txTotalNotes数字 != null )
                        {
                            this.txTotalNotes数字.t2D描画( CDTXMania.app.Device, x, y, rectangle );
                        }
                        break;
                    }
                }
                x += 14;
            }
        }
        private void t難易度カーソル描画( int x, int current )
        {
            if( this.tx難易度カーソル != null )
            {
                this.tx難易度カーソル.t2D描画( CDTXMania.app.Device, x, 603 - ( 60 * current ) );
            }
        }
        private double dbノーツグラフゲージ割合計算( int nチップ数 )
        {
            //ゲージ上限である250を超える場合は無視
            if( nチップ数 < 250 )
            {
                return nチップ数 / 250.0;
            }
            return 1.0;
        }
		//-----------------
		#endregion
	}
}
