using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SharpDX;
using FDK;

using Rectangle = System.Drawing.Rectangle;
using Point = System.Drawing.Point;
namespace DTXMania
{
	internal class CActResultParameterPanelXG : CActResultParameterPanel共通
	{
		// コンストラクタ

		public CActResultParameterPanelXG()
		{
			base.b活性化してない = true;
		}

		// CActivity 実装

		public override void On活性化()
		{
            #region [ 本体位置 ]
            int n上X = 453;
            int n上Y = 11;
            int n下X = 106;
            int n下Y = 430;

            this.n本体X[ 0 ] = 0;
            this.n本体Y[ 0 ] = 0;

            this.n本体X[ 1 ] = 0;
            this.n本体Y[ 1 ] = 0;

            this.n本体X[ 2 ] = 0;
            this.n本体Y[ 2 ] = 0;

            if( CDTXMania.ConfigIni.bDrums有効 )
            {
                this.n本体X[ 0 ] = n上X;
                this.n本体Y[ 0 ] = n上Y;
            }
            else if( CDTXMania.ConfigIni.bGuitar有効 )
            {
                bool bSwap = CDTXMania.ConfigIni.bIsSwappedGuitarBass;
                if( CDTXMania.DTX.bチップがある.Guitar )
                {
                    this.n本体X[ 1 ] = bSwap ? n下X : n上X;
                    this.n本体Y[ 1 ] = bSwap ? n下Y : n上Y;
                }
                if( CDTXMania.DTX.bチップがある.Bass )
                {
                    this.n本体X[ 2 ] = bSwap ? n上X : n下X;
                    this.n本体Y[ 2 ] = bSwap ? n上Y : n下Y;
                }

            }
            #endregion
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ct表示用 != null )
			{
				this.ct表示用 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_result panel.png" ) );
				this.tx文字[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_numbers.png" ) );
				this.tx文字[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_numbers em.png" ) );
                this.tx文字[ 2 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_numbers_large.png" ) );
                this.tx難易度パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Difficulty.png" ) );
                this.txレベル数字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_LevelNumber.png" ) );
                this.txゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Gauge.png" ) );
                this.txゲージ2 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Gauge2.png" ) );
				this.txWhite = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.tx文字[ 0 ] );
				CDTXMania.tテクスチャの解放( ref this.tx文字[ 1 ] );
				CDTXMania.tテクスチャの解放( ref this.tx文字[ 2 ] );
                CDTXMania.tテクスチャの解放( ref this.tx難易度パネル );
                CDTXMania.tテクスチャの解放( ref this.txレベル数字 );
                CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txゲージ2 );
				CDTXMania.tテクスチャの解放( ref this.txWhite );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				this.ct表示用 = new CCounter( 0, 1000, 3, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct表示用.t進行();
            int[] z = new int[3];
            bool bSwap = CDTXMania.ConfigIni.bIsSwappedGuitarBass;
            z[ 0 ] = 0;
            z[ 1 ] = bSwap ? 2 : 1;
            z[ 2 ] = bSwap ? 1 : 2;

			int num = this.ct表示用.n現在の値;
			for( int i = 0; i < 3; i++ )
			{
                if( this.n本体X[ i ] != 0 )
                {
                    if( this.txパネル本体 != null )
                    {
                        this.txパネル本体.t2D描画( CDTXMania.app.Device, this.n本体X[ i ], this.n本体Y[ i ] );
                    }

                    //ゲージ
                    //X拡大縮小で描画する。
                    if( this.txゲージ != null )
                    {
                        //達成率
                        this.txゲージ.vc拡大縮小倍率.X = (float)CDTXMania.stage結果.st演奏記録[ i ].db演奏型スキル値 / 100.0f;
                        this.txゲージ.t2D描画( CDTXMania.app.Device, this.n本体X[ i ] + 12, this.n本体Y[ i ] + 71, new Rectangle( 0, 0, 350, 54 ) );

                        //自己ベスト
                        this.txゲージ2.vc拡大縮小倍率.X = (float)CDTXMania.stage結果.sc更新前Scoreini.stセクション[ ( i + 1 ) * 2 ].db演奏型スキル値 / 100.0f;
                        this.txゲージ2.t2D描画( CDTXMania.app.Device, this.n本体X[ i ] + 12, this.n本体Y[ i ] + 150, new Rectangle( 0, 0, 348, 12 ) );

                        //ゴースト
                        //this.txゲージ2.vc拡大縮小倍率.X = (float)CDTXMania.stage結果.st演奏記録[ i ].db演奏型スキル値 / 100.0f;
                        //this.txゲージ2.t2D描画( CDTXMania.app.Device, this.n本体X[ i ] + 12, this.n本体Y[ i ] + 182, new Rectangle( 0, 13, 348, 12 ) );
                    }

                    if( CDTXMania.stage結果.st演奏記録[ i ].nPerfect数 == CDTXMania.stage結果.st演奏記録[ i ].n全チップ数 )
                    {
                        if( this.txNewRecord != null )
                            this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 79, this.n本体Y[i] + 29, new Rectangle(0, 12, 72, 26));
                        else //画像が無かった時用として
                            this.t特大文字表示( this.n本体X[ i ] + 69, this.n本体Y[ i ] + 31, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録[ i ].db演奏型スキル値 / 100.0 ) );
                    }
                    else
                    {
                        this.t特大文字表示( this.n本体X[ i ] + 69, this.n本体Y[ i ] + 31, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録[ i ].db演奏型スキル値 / 100.0 ) );
                    }
                    this.t特大文字表示(this.n本体X[i] + 259, this.n本体Y[i] + 32, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録[i].dbゲーム型スキル値));

                    if( CDTXMania.stage結果.b新記録スキル[ i ] )
                    {
                        if( this.txNewRecord != null )
                            this.txNewRecord.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 16, this.n本体Y[i] + 56, new Rectangle(0, 0, 111, 12));
                    }

                    if (num >= 0)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nPerfect数));
                        //this.tx達成率ゲージ.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 12, this.n本体Y[i] + 70, new Rectangle(0, 0, (int)dbメーター[i], 56));
                        this.t小文字表示(this.n本体X[i] + 507 + 0x40, this.n本体Y[i] + 35, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPerfect率[i]));
                    }
                    if (num >= 100)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 25, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nGreat数));
                        this.t小文字表示(this.n本体X[i] + 507 + 64, this.n本体Y[i] + 35 + 0x19, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGreat率[i]));
                    }
                    if (num >= 200)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 50, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nGood数));
                        this.t小文字表示(this.n本体X[i] + 507 + 64, this.n本体Y[i] + 35 + 50, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGood率[i]));
                    }
                    if (num >= 300)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 75, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nPoor数));
                        this.t小文字表示(this.n本体X[i] + 507 + 64, this.n本体Y[i] + 35 + 0x4b, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPoor率[i]));
                    }
                    if (num >= 400)
                    {
                        this.t大文字表示(this.n本体X[i] + 507, this.n本体Y[i] + 35 + 100, string.Format("{0,5:####0}%", CDTXMania.stage結果.st演奏記録[i].nMiss数));
                        this.t小文字表示(this.n本体X[i] + 507 + 64, this.n本体Y[i] + 35 + 100, string.Format("{0,3:##0}%", CDTXMania.stage結果.fMiss率[i]));
                    }
                    if (num >= 500)
                    {
                        this.t大文字表示(this.n本体X[i] + 507 - 44, this.n本体Y[i] + 35 + 125, string.Format("{0,9:########0}", CDTXMania.stage結果.st演奏記録[i].n最大コンボ数));
                        this.t小文字表示(this.n本体X[i] + 507 + 64, this.n本体Y[i] + 35 + 125, string.Format("{0,3:##0}%", (((float)CDTXMania.stage結果.st演奏記録[i].n最大コンボ数) / ((float)CDTXMania.stage結果.st演奏記録[i].n全チップ数)) * 100f));
                    }
                    if (num >= 600)
                    {
                        if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                            this.t特大文字表示(this.n本体X[i] + 507 - 126, this.n本体Y[i] + 35 + 173, string.Format("{0,10:#########0}", CDTXMania.stage結果.st演奏記録[i].nスコア), true);
                        else
                            this.t特大文字表示(this.n本体X[i] + 507 - 58, this.n本体Y[i] + 35 + 173, string.Format("{0,7:######0}", CDTXMania.stage結果.st演奏記録[i].nスコア), true);
                    }
                }
			}


            int num5 = this.ct表示用.n現在の値 / 100;
            double num6 = 1.0 - (((double)(this.ct表示用.n現在の値 % 100)) / 100.0);
            int height = 20;

            for( int i = 0; i < 3; i++ )
            {
                this.n白X[i] = this.n本体X[i] + 393;
                this.n白Y[i] = this.n本体Y[i] + 35 + (num5 * 24);

                if( this.n本体X[i] != 0 )
                {
                    STDGBVALUE<double> n表記するLEVEL = new STDGBVALUE<double>();
                    n表記するLEVEL[i] = CDTXMania.DTX.LEVEL[i] / 10.0;
                    n表記するLEVEL[i] += (CDTXMania.DTX.LEVELDEC[i] != 0 ? CDTXMania.DTX.LEVELDEC[i] / 100.0 : 0);
                    int DTXLevel = CDTXMania.DTX.LEVEL[i];
                    double DTXLevelDeci = (DTXLevel * 10 - CDTXMania.DTX.LEVEL[i]);

                    Cスコア CScore = CDTXMania.stage選曲XG.r確定されたスコア;
                    C曲リストノード CSongNode = CDTXMania.stage選曲XG.r確定された曲;
                    int n表示Level = 0;
                    string n表示LevelDec = "0";

                    if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && CDTXMania.DTX.bCLASSIC譜面である[ i ] )
                    {
                        n表示Level = CScore.譜面情報.レベル[i];
                    }
                    else
                    {
                        n表示Level = CScore.譜面情報.レベル[ i ] / 10;
                        if( CScore.譜面情報.レベル[ i ].ToString().Length > 1 )
                            n表示LevelDec = CScore.譜面情報.レベル[ i ].ToString().Substring( 1, 1 );
                        else
                            n表示LevelDec = CScore.譜面情報.レベル[ i ].ToString();

                        if( CScore.譜面情報.レベルDec[ i ] != 0 )
                            n表示LevelDec += CScore.譜面情報.レベルDec[i].ToString();
                        else
                            n表示LevelDec += "0";
                    }

                    if (this.ct表示用.n現在の値 < 700)
                    {
                        if (this.txWhite != null)
                        {
                            this.txWhite.n透明度 = (int)(255.0 * num6);
                        }
                        Rectangle rectangle = new Rectangle(0, 0, 222, height);
                        if (num5 >= 2)
                        {
                            if (num5 < 3)
                            {
                                this.n白Y[i]++;
                            }
                            else if (num5 < 4)
                            {
                                this.n白Y[i] += 2;
                            }
                            else if (num5 < 5)
                            {
                                this.n白Y[i] += 3;
                            }
                            else if (num5 < 6)
                            {
                                this.n白Y[i] += 4;
                            }
                            else if (num5 < 7)
                            {
                                this.n白Y[i] += 5;
                                rectangle.Height = 56;
                            }
                        }
                        this.txWhite.t2D描画( CDTXMania.app.Device, this.n白X[ i ], this.n白Y[ i ], rectangle );
                    }
                    this.t難易度パネルを描画する( CSongNode.ar難易度ラベル[ CDTXMania.stage選曲XG.n確定された曲の難易度 ], 941, 19 );
                    this.tレベル大文字表示( 1104, 22, n表示Level.ToString() );
                    this.tレベル小文字表示( 1126, 23, "." + n表示LevelDec );
                }
            }

            return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
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

		private CCounter ct表示用;
        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;
        private STDGBVALUE<int> n白X;
        private STDGBVALUE<int> n白Y;
		private CTexture txFullCombo;
        private CTexture txNewRecord;
		private CTexture txWhite;
		private CTexture txパネル本体;
        private CTexture tx難易度パネル;
        private CTexture txレベル数字;
        private CTexture txゲージ;
        private CTexture txゲージ2;
		private CTexture[] tx文字 = new CTexture[ 3 ];

        private ST文字位置[] st小文字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 36 ) ),
            new ST文字位置( '1', new Point( 14, 36 ) ),
            new ST文字位置( '2', new Point( 28, 36 ) ),
            new ST文字位置( '3', new Point( 42, 36 ) ),
            new ST文字位置( '4', new Point( 56, 36 ) ),
            new ST文字位置( '5', new Point( 0, 54 ) ),
            new ST文字位置( '6', new Point( 14, 54 ) ),
            new ST文字位置( '7', new Point( 28, 54 ) ),
            new ST文字位置( '8', new Point( 42, 54 ) ),
            new ST文字位置( '9', new Point( 56, 54 ) ),
            new ST文字位置( '%', new Point( 70, 54 ) )
        };

        private ST文字位置[] st大文字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 14, 0 ) ),
            new ST文字位置( '2', new Point( 28, 0 ) ),
            new ST文字位置( '3', new Point( 42, 0 ) ),
            new ST文字位置( '4', new Point( 56, 0 ) ),
            new ST文字位置( '5', new Point( 0, 18 ) ),
            new ST文字位置( '6', new Point( 14, 18 ) ),
            new ST文字位置( '7', new Point( 28, 18 ) ),
            new ST文字位置( '8', new Point( 42, 18 ) ),
            new ST文字位置( '9', new Point( 56, 18 ) ),
            new ST文字位置( '.', new Point( 70, 18 ) )
        };

        private ST文字位置[] st特大文字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 18, 0 ) ),
            new ST文字位置( '2', new Point( 36, 0 ) ),
            new ST文字位置( '3', new Point( 54, 0 ) ),
            new ST文字位置( '4', new Point( 72, 0 ) ),
            new ST文字位置( '5', new Point( 0, 24 ) ),
            new ST文字位置( '6', new Point( 18, 24 ) ),
            new ST文字位置( '7', new Point( 36, 24 ) ),
            new ST文字位置( '8', new Point( 54, 24 ) ),
            new ST文字位置( '9', new Point( 72, 24 ) ),
            new ST文字位置( '.', new Point( 90, 24 ) ),
            new ST文字位置( '%', new Point( 90, 0 ) )
        };

        private ST文字位置[] stレベル小文字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 16 ) ),
            new ST文字位置( '1', new Point( 16, 16 ) ),
            new ST文字位置( '2', new Point( 32, 16 ) ),
            new ST文字位置( '3', new Point( 48, 16 ) ),
            new ST文字位置( '4', new Point( 64, 16 ) ),
            new ST文字位置( '5', new Point( 80, 16 ) ),
            new ST文字位置( '6', new Point( 96, 16 ) ),
            new ST文字位置( '7', new Point( 112, 16 ) ),
            new ST文字位置( '8', new Point( 128, 16 ) ),
            new ST文字位置( '9', new Point( 144, 16 ) ),
            new ST文字位置( '.', new Point( 160, 16 ) )
        };

        private ST文字位置[] stレベル大文字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 20, 0 ) ),
            new ST文字位置( '2', new Point( 40, 0 ) ),
            new ST文字位置( '3', new Point( 60, 0 ) ),
            new ST文字位置( '4', new Point( 80, 0 ) ),
            new ST文字位置( '5', new Point( 100, 0 ) ),
            new ST文字位置( '6', new Point( 120, 0 ) ),
            new ST文字位置( '7', new Point( 140, 0 ) ),
            new ST文字位置( '8', new Point( 160, 0 ) ),
            new ST文字位置( '9', new Point( 180, 0 ) ),
        };

		private void t小文字表示( int x, int y, string str )
		{
			this.t小文字表示( x, y, str, false );
		}
		private void t小文字表示( int x, int y, string str, bool b強調 )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st小文字位置.Length; i++ )
				{
					if( this.st小文字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( this.st小文字位置[ i ].pt.X, this.st小文字位置[ i ].pt.Y, 14, 18 );
						if( ch == '%' )
						{
							rectangle.Width -= 2;
							rectangle.Height -= 2;
						}
						if( this.tx文字[ b強調 ? 1 : 0 ] != null )
						{
							this.tx文字[ b強調 ? 1 : 0 ].t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
				x += 11;
			}
		}
		private void t大文字表示( int x, int y, string str )
		{
			this.t大文字表示( x, y, str, false );
		}
		private void t大文字表示( int x, int y, string str, bool b強調 )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st大文字位置.Length; i++ )
				{
					if( this.st大文字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( this.st大文字位置[ i ].pt.X, this.st大文字位置[ i ].pt.Y, 14, 18 );
						if( ch == '.' )
						{
							rectangle.Width -= 2;
							rectangle.Height -= 2;
						}
						if( this.tx文字[ b強調 ? 1 : 0 ] != null )
						{
							this.tx文字[ b強調 ? 1 : 0 ].t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
				x += 11;
			}
		}
        private void t特大文字表示( int x, int y, string str )
        {
            this.t特大文字表示( x, y, str, false );
        }
        private void t特大文字表示( int x, int y, string str, bool bExtraLarge )
        {
            foreach( char c in str )
            {
                for( int j = 0; j < this.st特大文字位置.Length; j++ )
                {
                    if( this.st特大文字位置[ j ].ch == c )
                    {
                        int num = 0;
                        int num2 = 0;
                        if( bExtraLarge )
                        {
                            if( j < 5 )
                            {
                                num = 6 * j;
                                num2 = 48;
                            }
                            else if( j < 11 )
                            {
                                num = 6 * ( j - 5 );
                                num2 = 56;
                            }
                            else
                            {
                                num = 24;
                                num2 = 48;
                            }
                        }
                        Rectangle rc画像内の描画領域 = new Rectangle( this.st特大文字位置[ j ].pt.X + num, this.st特大文字位置[ j ].pt.Y + num2, bExtraLarge ? 24 : 18, bExtraLarge ? 32 : 24 );
                        if( c == '.' )
                        {
                            rc画像内の描画領域.Width -= 2;
                            rc画像内の描画領域.Height -= 2;
                        }
                        if (this.tx文字[ 2 ] != null)
                        {
                            this.tx文字[ 2 ].t2D描画( CDTXMania.app.Device, x, y, rc画像内の描画領域 );
                        }
                        break;
                    }
                }
                if (c == '.')
                {
                    x += bExtraLarge ? 20 : 14;
                }
                else
                {
                    x += bExtraLarge ? 23 : 17;
                }
            }
        }
		private void tレベル小文字表示( int x, int y, string str )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.stレベル小文字位置.Length; i++ )
				{
					if( this.stレベル小文字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( this.stレベル小文字位置[ i ].pt.X, this.stレベル小文字位置[ i ].pt.Y, 16, 16 );
						if( ch == '.' ) rectangle.Width -= 10;
						if( this.txレベル数字 != null )
						{
							this.txレベル数字.t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
                if( ch == '.' ) x += 6;
                else x += 16;
			}
		}
		private void tレベル大文字表示( int x, int y, string str )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.stレベル大文字位置.Length; i++ )
				{
					if( this.stレベル大文字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( this.stレベル大文字位置[ i ].pt.X, this.stレベル大文字位置[ i ].pt.Y, 20, 16 );
						if( this.txレベル数字 != null )
						{
							this.txレベル数字.t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
                x += 16;
			}
		}
        private void t難易度パネルを描画する( string strラベル名, int nX, int nY )
        {
            string strRawScriptFile;

            Rectangle rect = new Rectangle( 0, 0, 120, 20 );

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

                    if( arScriptLine[ 0 ] != "8" )
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

                    break;
                }
            }

            if( this.tx難易度パネル != null )
                this.tx難易度パネル.t2D描画( CDTXMania.app.Device, nX, nY, rect );
        }
		//-----------------
		#endregion
	}
}
