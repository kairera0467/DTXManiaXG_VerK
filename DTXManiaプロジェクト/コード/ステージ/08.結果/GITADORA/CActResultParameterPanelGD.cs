using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActResultParameterPanelGD : CActResultParameterPanel共通
	{
		// コンストラクタ

		public CActResultParameterPanelGD()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ct表示用.n現在の値 = this.ct表示用.n終了値;
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
                    this.t特大文字表示( 1080, 260, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録[ i ].db演奏型スキル値 / 100.0 ) );
                    this.t特大文字表示( 1020, 370, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録[i].dbゲーム型スキル値));
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

                    Cスコア CScore = CDTXMania.bXGRelease ? CDTXMania.stage選曲XG.r確定されたスコア : CDTXMania.stage選曲GITADORA.r確定されたスコア;
                    C曲リストノード CSongNode = CDTXMania.bXGRelease ? CDTXMania.stage選曲XG.r確定された曲 : CDTXMania.stage選曲GITADORA.r確定された曲;
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
                    }
                }
            }
            //文字
            CDTXMania.act文字コンソール.tPrint( 877, 484, C文字コンソール.Eフォント種別.白, "PERFECT" );
            CDTXMania.act文字コンソール.tPrint( 877, 508, C文字コンソール.Eフォント種別.白, "GREAT" );
            CDTXMania.act文字コンソール.tPrint( 877, 532, C文字コンソール.Eフォント種別.白, "GOOD" );
            CDTXMania.act文字コンソール.tPrint( 877, 556, C文字コンソール.Eフォント種別.白, "OK" );
            CDTXMania.act文字コンソール.tPrint( 877, 580, C文字コンソール.Eフォント種別.白, "MISS" );
            CDTXMania.act文字コンソール.tPrint( 877, 604, C文字コンソール.Eフォント種別.白, "MAX COMBO" );
            CDTXMania.act文字コンソール.tPrint( 877, 628, C文字コンソール.Eフォント種別.白, "Score" );

            //数値
            CDTXMania.act文字コンソール.tPrint( 986, 484, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nPerfect数 ) );
            CDTXMania.act文字コンソール.tPrint( 986, 508, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nGreat数 ) );
            CDTXMania.act文字コンソール.tPrint( 986, 532, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nGood数 ) );
            CDTXMania.act文字コンソール.tPrint( 986, 556, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nPoor数 ) );
            CDTXMania.act文字コンソール.tPrint( 986, 580, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nMiss数 ) );
            CDTXMania.act文字コンソール.tPrint( 986, 604, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.n最大コンボ数 ) );
            CDTXMania.act文字コンソール.tPrint( 982, 628, C文字コンソール.Eフォント種別.白, string.Format( "{0,7:######0}", CDTXMania.stage結果.st演奏記録.Drums.nスコア ) );

            //数値2
            CDTXMania.act文字コンソール.tPrint( 1054, 484, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fPerfect率.Drums ) );
            CDTXMania.act文字コンソール.tPrint( 1054, 508, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fGreat率.Drums ) );
            CDTXMania.act文字コンソール.tPrint( 1054, 532, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fGood率.Drums ) );
            CDTXMania.act文字コンソール.tPrint( 1054, 556, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fPoor率.Drums ) );
            CDTXMania.act文字コンソール.tPrint( 1054, 580, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fMiss率.Drums ) );
            CDTXMania.act文字コンソール.tPrint( 1054, 604, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", (((float)CDTXMania.stage結果.st演奏記録.Drums.n最大コンボ数) / ((float)CDTXMania.stage結果.st演奏記録.Drums.n全チップ数)) * 100.0f ) );

            //string test = string.Format("{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nPerfect数);
			if( !this.ct表示用.b終了値に達した )
			{
				return 0;
			}
			return 1;
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
		//-----------------
		#endregion
	}
}
