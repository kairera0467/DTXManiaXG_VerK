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
                        if( this.tx難易度パネル != null )
                        {
                            this.tx難易度パネル.t2D描画( CDTXMania.app.Device, 428, 352 );
                        }
                        this.t難易度カーソル描画( 426, base.n現在選択中の曲の難易度 );
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

		private int n現在の難易度ラベルが完全表示されているかを調べてスクロール方向を返す()
		{
			int num = 0;
			int length = 0;
			for( int i = 0; i < 5; i++ )
			{
				if( ( this.str難易度ラベル[ i ] != null ) && ( this.str難易度ラベル[ i ].Length > 0 ) )
				{
					length = this.str難易度ラベル[ i ].Length;
				}
				if( this.n現在選択中の曲の難易度 == i )
				{
					break;
				}
				if( ( this.str難易度ラベル[ i ] != null ) && ( this.str難易度ラベル.Length > 0 ) )
				{
					num += length + 2;
				}
			}
			if( num >= ( this.n難易度開始文字位置 + 55 ) )	// 0x24 -> 55
			{
				return 1;
			}
			if( ( num + length ) <= this.n難易度開始文字位置 )
			{
				return -1;
			}
			if( ( ( num + length ) - 1 ) >= ( this.n難易度開始文字位置 + 55 ) )	// 0x24 -> 55
			{
				return 1;
			}
			if( num < this.n難易度開始文字位置 )
			{
				return -1;
			}
			return 0;
		}
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
        private void t決定アニメ_難易度パネルを生成する()
        {
            //ギター・ベースの場合、1枚の画像を2回読み込むことになるので回避。
            Image imgPanel;
            imgPanel = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_base info panel.png" ) );
            int[] arPanelRectX = new int[] { 164, 326, 650, 488 }; //Drums(使用しない), Guitar, Bass, GuitarF, BassF

            #region[ ドラム ]
            if( CDTXMania.ConfigIni.bDrums有効 && imgPanel != null )
            {
                Bitmap bPanel = new Bitmap( 155, 362 );
                Graphics graphics = Graphics.FromImage( bPanel );

                Image imgLabel;
                Image imgLevelNum;

                graphics.PageUnit = GraphicsUnit.Pixel;
                graphics.DrawImage( imgPanel, new Rectangle( 0, 0, 155, 362 ), new Rectangle( 2, 2, 155, 362 ), GraphicsUnit.Pixel );

                imgLabel = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\6_difficulty panel.png" ) );
                graphics.DrawImage( imgLabel, new Rectangle( 14, 31, 130, 72 ), this.t指定したラベル名から難易度パネル画像の座標を取得する( CDTXMania.stage選曲.r確定された曲.ar難易度ラベル[ CDTXMania.stage選曲.n確定された曲の難易度 ] ), GraphicsUnit.Pixel );

                imgLevelNum = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_LevelNumber.png" ) );

                //XG譜面
                if( !CDTXMania.stage選曲.r確定されたスコア.譜面情報.b完全にCLASSIC譜面である.Drums )
                {
                    #region[ XG譜面 ]
                    int n難易度整数;
                    int n難易度小数;
                    string str難易度;
                    n難易度整数 = (int)this.n現在選択中の曲のレベル難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ 0 ] / 10;
                    n難易度小数 = ( this.n現在選択中の曲のレベル難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ 0 ] - ( n難易度整数 * 10 ) ) * 10;
                    n難易度小数 += this.n現在選択中の曲のレベル小数点難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ 0 ];
                    str難易度 = n難易度整数.ToString() + string.Format( "{0,2:00}", n難易度小数 );

                    if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE )
                    {
                        for( int j = 0; j < this.st大文字位置.Length; j++ )
                        {
                            if( this.st大文字位置[ j ].ch == str難易度[ 0 ] )
                            {
                                Rectangle rc画像内の描画領域 = new Rectangle( this.st大文字位置[ j ].pt.X, this.st大文字位置[ j ].pt.Y, 22, 40 );
                                graphics.DrawImage( imgLevelNum, new Rectangle( 84, 55, 22, 40 ), rc画像内の描画領域, GraphicsUnit.Pixel );
                                break;
                            }
                        }
                        graphics.DrawImage( imgLevelNum, new Rectangle( 105, 74, 9, 22 ), new Rectangle( 144, 40, 9, 22 ), GraphicsUnit.Pixel );
                        for( int k = 0; k < 2; k++ )
                        {
                            for( int j = 0; j < this.st小文字位置.Length; j++ )
                            {
                                if( this.st小文字位置[ j ].ch == str難易度[ k + 1 ] )
                                {
                                    Rectangle rc画像内の描画領域 = new Rectangle( this.st小文字位置[ j ].pt.X, this.st小文字位置[ j ].pt.Y, 12, 22 );
                                    graphics.DrawImage( imgLevelNum, new Rectangle( 113 + ( 12 * k ), 74, 12, 22 ), rc画像内の描画領域, GraphicsUnit.Pixel );
                                    break;
                                }
                            }
                        }
                    }
                    else if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM )
                    {
                        graphics.DrawImage( imgLevelNum, new Rectangle( 84, 55, 22, 40 ), new Rectangle( 0, 0, 22, 40 ), GraphicsUnit.Pixel );
                        graphics.DrawImage( imgLevelNum, new Rectangle( 105, 74, 9, 22 ), new Rectangle( 144, 40, 9, 22 ), GraphicsUnit.Pixel );
                        for( int k = 0; k < 2; k++ )
                        {
                            graphics.DrawImage( imgLevelNum, new Rectangle( 113 + ( 12 * k ), 74, 12, 22 ), new Rectangle( 0, 40, 12, 22 ), GraphicsUnit.Pixel );
                        }
                    }
                    #endregion
                }
                else
                {
                    #region[ CLASSIC譜面 ]
                    string str難易度;
                    str難易度 = string.Format( "{0,2:00}", (int)this.n現在選択中の曲のレベル難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ 0 ] );

                    if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE )
                    {
                        for( int j = 0; j < 2; j++ )
                        {
                            for( int k = 0; k < this.st大文字位置.Length; k++ )
                            {
                                if( this.st大文字位置[ k ].ch == str難易度[ j ] )
                                {
                                    Rectangle rc画像内の描画領域 = new Rectangle( this.st大文字位置[ k ].pt.X, this.st大文字位置[ k ].pt.Y, 22, 40 );
                                    graphics.DrawImage( imgLevelNum, new Rectangle( 94 + ( 22 * j ), 56, 22, 40 ), rc画像内の描画領域, GraphicsUnit.Pixel );
                                    break;
                                }
                            }
                        }
                    }
                    else if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM )
                    {
                        graphics.DrawImage( imgLevelNum, new Rectangle( 94, 55, 22, 40 ), new Rectangle( 0, 0, 22, 40 ), GraphicsUnit.Pixel );
                        graphics.DrawImage( imgLevelNum, new Rectangle( 116, 55, 22, 40 ), new Rectangle( 0, 0, 22, 40 ), GraphicsUnit.Pixel );
                    }
                    #endregion
                }

                this.tx決定後_難易度パネル1P = new CTexture( CDTXMania.app.Device, bPanel, CDTXMania.TextureFormat, false );

                CDTXMania.t安全にDisposeする( ref bPanel );
                CDTXMania.t安全にDisposeする( ref graphics );
                CDTXMania.t安全にDisposeする( ref imgPanel );
                CDTXMania.t安全にDisposeする( ref imgLabel );
                CDTXMania.t安全にDisposeする( ref imgLevelNum );
            }
            #endregion
            #region[ ギター・ベース ]
            else if( CDTXMania.ConfigIni.bGuitar有効 && imgPanel != null )
            {
                for( int i = 1; i <= 2; i++ )
                {
                    Bitmap bPanel = new Bitmap( 155, 362 );
                    Graphics gPanel = Graphics.FromImage( bPanel );

                    Image imgLabel;
                    Image imgLevelNum;

                    gPanel.PageUnit = GraphicsUnit.Pixel;
                    gPanel.DrawImage( imgPanel, new Rectangle( 0, 0, 155, 362 ), new Rectangle( arPanelRectX[ i + ( CDTXMania.ConfigIni.bIsSwappedGuitarBass ? 1 : -1 ) ], 2, 155, 362 ), GraphicsUnit.Pixel );

                    imgLabel = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\6_difficulty panel.png" ) );
                    gPanel.DrawImage( imgLabel, new Rectangle( 14, 32, 130, 72 ), this.t指定したラベル名から難易度パネル画像の座標を取得する( CDTXMania.stage選曲.r確定された曲.ar難易度ラベル[ CDTXMania.stage選曲.n確定された曲の難易度 ] ), GraphicsUnit.Pixel );

                    imgLevelNum = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_LevelNumber.png" ) );

                    //XG譜面
                    if( !CDTXMania.stage選曲.r確定されたスコア.譜面情報.b完全にCLASSIC譜面である[ i ] )
                    {
                        #region[ XG譜面 ]
                        int n難易度整数;
                        int n難易度小数;
                        string str難易度;
                        n難易度整数 = (int)this.n現在選択中の曲のレベル難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ i ] / 10;
                        n難易度小数 = ( this.n現在選択中の曲のレベル難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ i ] - ( n難易度整数 * 10 ) ) * 10;
                        n難易度小数 += this.n現在選択中の曲のレベル小数点難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ i ];
                        str難易度 = n難易度整数.ToString() + string.Format( "{0,2:00}", n難易度小数 );

                        if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE )
                        {
                            for( int j = 0; j < this.st大文字位置.Length; j++ )
                            {
                                if( this.st大文字位置[ j ].ch == str難易度[ 0 ] )
                                {
                                    Rectangle rc画像内の描画領域 = new Rectangle( this.st大文字位置[ j ].pt.X, this.st大文字位置[ j ].pt.Y, 22, 40 );
                                    gPanel.DrawImage( imgLevelNum, new Rectangle( 84, 56, 22, 40 ), rc画像内の描画領域, GraphicsUnit.Pixel );
                                    break;
                                }
                            }
                            gPanel.DrawImage( imgLevelNum, new Rectangle( 105, 74, 9, 22 ), new Rectangle( 144, 40, 9, 22 ), GraphicsUnit.Pixel );
                            for( int k = 0; k < 2; k++ )
                            {
                                for( int j = 0; j < this.st小文字位置.Length; j++ )
                                {
                                    if( this.st小文字位置[ j ].ch == str難易度[ k + 1 ] )
                                    {
                                        Rectangle rc画像内の描画領域 = new Rectangle( this.st小文字位置[ j ].pt.X, this.st小文字位置[ j ].pt.Y, 12, 22 );
                                        gPanel.DrawImage( imgLevelNum, new Rectangle( 113 + ( 12 * k ), 74, 12, 22 ), rc画像内の描画領域, GraphicsUnit.Pixel );
                                        break;
                                    }
                                }
                            }
                        }
                        else if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM )
                        {
                            gPanel.DrawImage( imgLevelNum, new Rectangle( 84, 55, 22, 40 ), new Rectangle( 0, 0, 22, 40 ), GraphicsUnit.Pixel );
                            gPanel.DrawImage( imgLevelNum, new Rectangle( 105, 74, 9, 22 ), new Rectangle( 144, 40, 9, 22 ), GraphicsUnit.Pixel );
                            for( int k = 0; k < 2; k++ )
                            {
                                gPanel.DrawImage( imgLevelNum, new Rectangle( 113 + ( 12 * k ), 74, 12, 22 ), new Rectangle( 0, 40, 12, 22 ), GraphicsUnit.Pixel );
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region[ CLASSIC譜面 ]
                        string str難易度;
                        str難易度 = string.Format( "{0,2:00}", (int)this.n現在選択中の曲のレベル難易度毎DGB[ CDTXMania.stage選曲.n確定された曲の難易度 ][ i ] );

                        if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE )
                        {
                            for( int j = 0; j < 2; j++ )
                            {
                                for( int k = 0; k < this.st大文字位置.Length; k++ )
                                {
                                    if( this.st大文字位置[ k ].ch == str難易度[ j ] )
                                    {
                                        Rectangle rc画像内の描画領域 = new Rectangle( this.st大文字位置[ k ].pt.X, this.st大文字位置[ k ].pt.Y, 22, 40 );
                                        gPanel.DrawImage( imgLevelNum, new Rectangle( 94 + ( 22 * j ), 56, 22, 40 ), rc画像内の描画領域, GraphicsUnit.Pixel );
                                        break;
                                    }
                                }
                            }
                        }
                        else if( CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM )
                        {
                            gPanel.DrawImage( imgLevelNum, new Rectangle( 94, 55, 22, 40 ), new Rectangle( 0, 0, 22, 40 ), GraphicsUnit.Pixel );
                            gPanel.DrawImage( imgLevelNum, new Rectangle( 116, 55, 22, 40 ), new Rectangle( 0, 0, 22, 40 ), GraphicsUnit.Pixel );
                        }
                        #endregion
                    }

                    if( i == 1 )
                        this.tx決定後_難易度パネル1P = new CTexture( CDTXMania.app.Device, bPanel, CDTXMania.TextureFormat, false );
                    else if( i == 2 )
                        this.tx決定後_難易度パネル2P = new CTexture( CDTXMania.app.Device, bPanel, CDTXMania.TextureFormat, false );

                    CDTXMania.t安全にDisposeする( ref bPanel );
                    CDTXMania.t安全にDisposeする( ref gPanel );
                    CDTXMania.t安全にDisposeする( ref imgLabel );
                    CDTXMania.t安全にDisposeする( ref imgLevelNum );
                }
            }
            #endregion
            CDTXMania.t安全にDisposeする( ref imgPanel );
        }
        private Rectangle t指定したラベル名から難易度パネル画像の座標を取得する( string strラベル名 )
        {
            string strRawScriptFile;

            Rectangle rect = new Rectangle( 0, 0, 130, 72 );

            //ファイルの存在チェック
            if( File.Exists( CSkin.Path( @"Script\difficult.dtxs" ) ) )
            {
                //スクリプトを開く
                StreamReader reader = new StreamReader( CSkin.Path( @"Script\difficult.dtxs" ), Encoding.GetEncoding( "Shift_JIS" ) );
                strRawScriptFile = reader.ReadToEnd();

                strRawScriptFile = strRawScriptFile.Replace( Environment.NewLine, "\n" );
                string[] delimiter = { "\n" };
                string[] strSingleLine = strRawScriptFile.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );

                for( int i = 0; i < strSingleLine.Length; i++ )
                {
                    if( strSingleLine[ i ].StartsWith( "//" ) )
                        continue; //コメント行の場合は無視

                    //まずSplit
                    string[] arScriptLine = strSingleLine[ i ].Split( ',' );

                    if( ( arScriptLine.Length >= 4 && arScriptLine.Length <= 5 ) == false )
                        continue; //引数が4つか5つじゃなければ無視。

                    if( arScriptLine[ 0 ] != "6" )
                        continue; //使用するシーンが違うなら無視。

                    if( arScriptLine.Length == 4 )
                    {
                        if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                            continue; //ラベル名が違うなら無視。大文字小文字区別しない
                    }
                    else if( arScriptLine.Length == 5 )
                    {
                        if( arScriptLine[ 4 ] == "1" )
                        {
                            if( arScriptLine[ 1 ] != strラベル名 )
                                continue; //ラベル名が違うなら無視。
                        }
                        else
                        {
                            if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                                continue; //ラベル名が違うなら無視。大文字小文字区別しない
                        }
                    }
                    rect.X = Convert.ToInt32( arScriptLine[ 2 ] );
                    rect.Y = Convert.ToInt32( arScriptLine[ 3 ] );

                    reader.Close();
                    break;
                }
            }

            return rect;
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
