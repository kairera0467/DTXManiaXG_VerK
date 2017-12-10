using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
using System.IO;
using FDK;

namespace DTXMania
{
	internal class CActSelectステータスパネルGITADORA : CActSelectステータスパネル共通
	{
		// メソッド

		public CActSelectステータスパネルGITADORA()
		{
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
                    if (CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania)
                        this.n現在選択中の曲の最高ランク[i] = cスコア.譜面情報.最大ランク[i];
                    else if (CDTXMania.ConfigIni.eSkillMode == ESkillType.XG )
                        this.n現在選択中の曲の最高ランク[i] = DTXMania.CScoreIni.tXGランク値を計算して返す( cスコア.譜面情報.最大スキル[i] );

					int nLevel = cスコア.譜面情報.レベル[ i ];
					if( nLevel < 0 )
					{
						nLevel = 0;
					}
					if( nLevel > 99 )
					{
						nLevel = 99;
					}
					this.n現在選択中の曲のレベル[ i ] = nLevel;
					this.n現在選択中の曲の最高ランク[ i ] = cスコア.譜面情報.最大ランク[ i ];
					this.b現在選択中の曲がフルコンボ[ i ] = cスコア.譜面情報.フルコンボ[ i ];
					this.db現在選択中の曲の最高スキル値[ i ] = cスコア.譜面情報.最大スキル[ i ];

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
                        }
                    }
				}
				for( int i = 0; i < 5; i++ )
				{
					this.str難易度ラベル[ i ] = c曲リストノード.ar難易度ラベル[ i ];
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
				this.n現在選択中の曲のレベル[ i ] = 0;
                this.db現在選択中の曲の曲別スキル値難易度毎[ i ] = 0.0;
				this.n現在選択中の曲の最高ランク[ i ] = (int)CScoreIni.ERANK.UNKNOWN;
				this.b現在選択中の曲がフルコンボ[ i ] = false;
				this.db現在選択中の曲の最高スキル値[ i ] = 0.0;
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
				this.str難易度ラベル[ j ] = "";
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
                this.tx難易度パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty panel.png" ) );
                this.tx難易度数字XG = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_LevelNumber.png" ) );
                this.tx難易度カーソル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty sensor.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.txゲージ用数字他 );
                CDTXMania.tテクスチャの解放( ref this.tx難易度パネル );
                CDTXMania.tテクスチャの解放( ref this.tx難易度数字XG );
                CDTXMania.tテクスチャの解放( ref this.tx難易度カーソル );

                CDTXMania.tテクスチャの解放( ref this.tx決定後_難易度パネル1P );
                CDTXMania.tテクスチャの解放( ref this.tx決定後_難易度パネル2P );
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
                if( this.txパネル本体 != null )
                {
                    this.txパネル本体.t2D描画( CDTXMania.app.Device, 428, 352 );

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

                            //if( this.str難易度ラベル[ i ] != null && this.b現在選択中の曲に譜面がある[ i ][ j ])
                            //{
                            //    this.t大文字表示(73 + this.n本体X[ j ] + (i * 143), 19 + this.n本体Y[j] - y差分[i], string.Format("{0:0}", n難易度整数[i]));
                            //    this.t小文字表示(102 + this.n本体X[ j ] + (i * 143), 37 + this.n本体Y[j] - y差分[i], string.Format("{0,2:00}", n難易度小数[i]));
                            //    this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                            //}
                            //else if ((this.str難易度ラベル[i] != null && !this.b現在選択中の曲に譜面がある[i][j]) || CDTXMania.stage選曲XG.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM)
                            //{
                            //    this.t大文字表示(73 + this.n本体X[j] + (i * 143), 19 + this.n本体Y[j] - y差分[i], ("-"));
                            //    this.t小文字表示(102 + this.n本体X[j] + (i * 143), 37 + this.n本体Y[j] - y差分[i], ("--"));
                            //    this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                            //}

                            if( this.str難易度ラベル[ i ] != null && this.b現在選択中の曲に譜面がある[ i ].Drums )
                            {
                                CDTXMania.act文字コンソール.tPrint( 570, 634 - ( 60 * i ), C文字コンソール.Eフォント種別.白, string.Format( "{0:0}", n難易度整数[i] ) + "." + string.Format("{0,2:00}", n難易度小数[i]) );
                            }
                        }

                        this.t難易度カーソル描画( 426, base.n現在選択中の曲の難易度 );

                        //ノート数グラフ
                        CDTXMania.act文字コンソール.tPrint( 380, 400, C文字コンソール.Eフォント種別.白, "LC:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.LC.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16, C文字コンソール.Eフォント種別.白, "HH:" + ( CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.HH + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.HHO ).ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 2, C文字コンソール.Eフォント種別.白, "LP:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.LP.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 3, C文字コンソール.Eフォント種別.白, "SD:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.SD.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 4, C文字コンソール.Eフォント種別.白, "HT:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.HT.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 5, C文字コンソール.Eフォント種別.白, "BD:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.BD.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 6, C文字コンソール.Eフォント種別.白, "LT:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.LT.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 7, C文字コンソール.Eフォント種別.白, "FT:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.FT.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 8, C文字コンソール.Eフォント種別.白, "CY:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.CY.ToString() );
                        CDTXMania.act文字コンソール.tPrint( 380, 400 + 16 * 9, C文字コンソール.Eフォント種別.白, "RD:" + CDTXMania.stage選曲GITADORA.r現在選択中のスコア.譜面情報.n可視チップ数.RD.ToString() );
                    }
                    #endregion
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

		private STDGBVALUE<bool> b現在選択中の曲がフルコンボ;
		private CCounter ct登場アニメ用;
		private CCounter ct難易度スクロール用;
		private CCounter ct難易度矢印用;
		private STDGBVALUE<double> db現在選択中の曲の最高スキル値;
		private STDGBVALUE<int> n現在選択中の曲のレベル;
		private STDGBVALUE<int> n現在選択中の曲の最高ランク;
        
        private int[] n選択中の曲のレベル難易度毎 = new int[5];
        
		private int n難易度開始文字位置;
		private const int n難易度表示可能文字数 = 0x24;
        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;
		private C曲リストノード r直前の曲;
		private string[] str難易度ラベル = new string[] { "", "", "", "", "" };
		private CTexture txゲージ用数字他;
        private CTexture tx難易度パネル;
        private CTexture tx難易度数字XG;

        private CTexture tx決定後_難易度パネル1P;
        private CTexture tx決定後_難易度パネル2P;
        private CTexture tx難易度カーソル;

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
        private void t難易度カーソル描画( int x, int current )
        {
            if( this.tx難易度カーソル != null )
            {
                this.tx難易度カーソル.t2D描画( CDTXMania.app.Device, x, 603 - ( 60 * current ) );
            }
        }
		//-----------------
		#endregion
	}
}
