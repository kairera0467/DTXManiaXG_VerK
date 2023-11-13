using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SharpDX;
using FDK;

using Rectangle = System.Drawing.Rectangle;
using Point = SharpDX.Point;
using Size = System.Drawing.Size;
namespace DTXMania
{
	internal class CActResultParameterPanelGD : CActResultParameterPanel共通
	{
		// コンストラクタ

		public CActResultParameterPanelGD()
		{
            this.tスキル数字フォント初期化();
            this.t達成率数字フォント初期化();
            base.b活性化してない = true;
		}


		// メソッド

		// CActivity 実装

		public override void On活性化()
		{
            // TODO: DrumsかGuitarかではなく1人プレイか2人プレイかで使用するレイアウトを変更するようにしたい

            #region [ 本体位置 ]
            Point leftSide = new Point(0, 0);
            Point rightSide = new Point(831, 0);

            this.n本体X[ 0 ] = -1;
            this.n本体Y[ 0 ] = -1;

            this.n本体X[ 1 ] = -1;
            this.n本体Y[ 1 ] = -1;

            this.n本体X[ 2 ] = -1;
            this.n本体Y[ 2 ] = -1;

            if( CDTXMania.ConfigIni.bDrums有効 )
            {
                this.n本体X[ 0 ] = rightSide.X;
                this.n本体Y[ 0 ] = rightSide.Y;
            }
            else if( CDTXMania.ConfigIni.bGuitar有効 )
            {
                bool bSwap = CDTXMania.ConfigIni.bIsSwappedGuitarBass;
                if( CDTXMania.DTX.bチップがある.Guitar )
                {
                    this.n本体X[ 1 ] = bSwap ? rightSide.X : leftSide.X;
                    this.n本体Y[ 1 ] = bSwap ? rightSide.Y : leftSide.Y;
                }
                if( CDTXMania.DTX.bチップがある.Bass )
                {
                    this.n本体X[ 2 ] = bSwap ? leftSide.X : rightSide.X;
                    this.n本体Y[ 2 ] = bSwap ? leftSide.Y : rightSide.Y;
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
                
                this.txスキル数字_整数 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Skill Number Big.png" ) );
                this.txスキル数字_少数 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Skill Number Small.png" ) );
                this.txスキル数字_点 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Skill Number dot.png" ) );

                this.tx達成率数字_整数 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Rate Number Big.png" ) );
                this.tx達成率数字_少数 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Rate Number Small.png" ) );

                this.tx項目文字列 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_text.png" ) );

                this.tx判定項目文字列 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_JudgeString.png" ) );

                this.txNewRecord = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\8_NewRecord.png") );

                #region[ 難易度、達成率、スキル値の下の白線の生成 ]
                Bitmap b白線 = new Bitmap( 340, 2 );
                Graphics g白線 = Graphics.FromImage( b白線 );
                g白線.FillRectangle( Brushes.White, 0, 0, 340, 2 );

                this.tx白線 = CDTXMania.tテクスチャの生成( b白線, false );
                CDTXMania.t安全にDisposeする( ref b白線 );
                CDTXMania.t安全にDisposeする( ref g白線 );
                #endregion
                #region[ 達成率ゲージ中身の生成]
                Bitmap b = new Bitmap( 202, 14 );
                Graphics g = Graphics.FromImage( b );
                g.DrawImage( CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\8_Gauge.png" ) ),
                        new Rectangle(0, 0, 202, 11), new Rectangle(8, 18, 204, 11), GraphicsUnit.Pixel
                    );
                this.txゲージ中身 = new CTexture( CDTXMania.app.Device, b, CDTXMania.TextureFormat, false );
                b?.Dispose();
                g?.Dispose();
                #endregion
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

                CDTXMania.tテクスチャの解放( ref this.txスキル数字_整数 );
                CDTXMania.tテクスチャの解放( ref this.txスキル数字_少数 );
                CDTXMania.tテクスチャの解放( ref this.txスキル数字_点 );

                CDTXMania.tテクスチャの解放( ref this.tx達成率数字_整数 );
                CDTXMania.tテクスチャの解放( ref this.tx達成率数字_少数 );

                CDTXMania.tテクスチャの解放( ref this.tx項目文字列 );

                CDTXMania.tテクスチャの解放( ref this.tx判定項目文字列 );

                CDTXMania.tテクスチャの解放( ref this.txNewRecord );

                CDTXMania.tテクスチャの解放( ref this.tx白線 );
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
                this.ctNewRecord = new CCounter( 0, 1, 160, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct表示用.t進行();
            this.ctNewRecord.t進行Loop();
            int[] z = new int[3];
            bool bSwap = CDTXMania.ConfigIni.bIsSwappedGuitarBass;
            z[ 0 ] = 0;
            z[ 1 ] = bSwap ? 2 : 1;
            z[ 2 ] = bSwap ? 1 : 2;

			int num = this.ct表示用.n現在の値;
			for( int i = 0; i < 3; i++ )
			{
                if (!CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.b譜面がある[ i ])
                {
                    // 譜面が無いパートは非表示
                    continue;
                }

                if( this.n本体X[ i ] != -1 )
                {
                    CScoreIni.C演奏記録 result = CDTXMania.stage結果.st演奏記録[ i ];
                    double rate = result.db演奏型スキル値;
                    double bestRate = -1;
                    switch (i)
                    {
                        case 0:
                            bestRate = CDTXMania.stage結果.sc更新前Scoreini.stセクション.HiSkillDrums.db演奏型スキル値;
                            break;
                        case 1:
                            bestRate = CDTXMania.stage結果.sc更新前Scoreini.stセクション.HiSkillGuitar.db演奏型スキル値;
                            break;
                        case 2:
                            bestRate = CDTXMania.stage結果.sc更新前Scoreini.stセクション.HiSkillBass.db演奏型スキル値;
                            break;
                    }

                    if (i == 0)
                    {
                        // 1人プレイ用レイアウト
                        this.tレベル値の描画( 1078, 159, CDTXMania.DTX.LEVEL.Drums, CDTXMania.DTX.LEVELDEC.Drums );
                        this.tx白線?.t2D描画( CDTXMania.app.Device, 916, 215 );

                        //this.t特大文字表示( 1080, 260, string.Format("{0,-6:##0.00%}", CDTXMania.stage結果.st演奏記録[ i ].db演奏型スキル値 / 100.0 ) );
                        this.t達成率値の描画( 1040, 232, rate );
                        this.tx白線?.t2D描画( CDTXMania.app.Device, 890, 288 );

                        //this.t特大文字表示( 1020, 370, string.Format("{0,6:##0.00}", CDTXMania.stage結果.st演奏記録[i].dbゲーム型スキル値));
                        this.tスキル値の描画(976, 328, CDTXMania.stage結果.st演奏記録[i].dbゲーム型スキル値);
                        this.tx白線?.t2D描画(CDTXMania.app.Device, 842, 416);

                        this.txゲージ?.t2D描画(CDTXMania.app.Device, 977, 398, new Rectangle(0, 0, 220, 16));

                        // TODO: ゲージの先端の描画方法を改善する
                        //       ゲージ右端の先端をくっつける方法でも十分実現可能ではありそう(ゲージ本体テクスチャを塗り部分と枠部分で分ける必要あり)
                        this.txゲージ中身.t2D描画(CDTXMania.app.Device, 985, 400, new Rectangle(0, 0, (int)(203.0f * (rate / 100.0f)), 11));

                        // 目標ラインテスト
                        // TODO: 当分の間は自己ベストの達成率の部分を表示する機能として実装する予定
                        //this.txゲージ中身.n透明度 = 96;
                        //this.txゲージ中身.t2D描画( CDTXMania.app.Device, 985, 400, new Rectangle( 0, 0, (int)(203.0f * (bestRate / 100.0f) ), 11) );
                        //this.txゲージ中身.n透明度 = 255;


                        // 各項目の文字
                        // TODO: セッション時は0.7倍で表示される
                        this.tx項目文字列?.t2D描画(CDTXMania.app.Device, 847, 381, new Rectangle(0, 0, 128, 32));
                        this.tx項目文字列?.t2D描画(CDTXMania.app.Device, 895, 254, new Rectangle(0, 32, 96, 32));
                        this.tx項目文字列?.t2D描画(CDTXMania.app.Device, 917, 181, new Rectangle(0, 64, 96, 32));

                        this.tx項目文字列?.t2D描画(CDTXMania.app.Device, 1190, 397, new Rectangle(0, 96, 32, 14));

                        // 新記録
                        if ( CDTXMania.stage結果.b新記録スキル[ i ] )
                        {
                            this.txNewRecord?.t2D描画( CDTXMania.app.Device, 1041, 288, new Rectangle(0, this.ctNewRecord.n現在の値 * 24, 96, 24) );
                            this.txNewRecord?.t2D描画( CDTXMania.app.Device, this.n本体X[0] + 14, 436, new Rectangle(0, this.ctNewRecord.n現在の値 * 24, 96, 24) );
                        }
                    }
                    else
                    {
                        // セッション時レイアウト

                        if ( this.tx白線 != null )
                        {
                            this.tx白線.vc拡大縮小倍率.X = 0.6f;
                            this.tx白線.vc拡大縮小倍率.Y = 0.6f;
                        }
                        this.tx白線?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 206, 257 );
                        this.tx白線?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 206, 315 );
                        this.tx白線?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 177, 413 );

                        if ( this.tx項目文字列 != null )
                        {
                            this.tx項目文字列.vc拡大縮小倍率.X = 0.7f;
                            this.tx項目文字列.vc拡大縮小倍率.Y = 0.7f;
                        }
                        this.tx項目文字列?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 181, 388, new Rectangle(0, 0, 128, 32) );

                        if (this.tx項目文字列 != null)
                        {
                            this.tx項目文字列.vc拡大縮小倍率.X = 0.62f;
                            this.tx項目文字列.vc拡大縮小倍率.Y = 0.62f;
                        }
                        this.tx項目文字列?.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 208, 295, new Rectangle(0, 32, 96, 32));

                        this.tx項目文字列?.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 205, 237, new Rectangle(0, 64, 96, 32));

                        if (this.tx項目文字列 != null)
                        {
                            this.tx項目文字列.vc拡大縮小倍率.X = 0.7f;
                            this.tx項目文字列.vc拡大縮小倍率.Y = 0.7f;
                        }
                        this.tx項目文字列?.t2D描画(CDTXMania.app.Device, 420, 399, new Rectangle(0, 96, 32, 14));

                        // 新記録
                        if ( CDTXMania.stage結果.b新記録スキル[ i ] )
                        {
                            this.txNewRecord?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 233, 312, new Rectangle(0, this.ctNewRecord.n現在の値 * 24, 96, 24) );
                            this.txNewRecord?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 114, 418, new Rectangle(0, this.ctNewRecord.n現在の値 * 24, 96, 24) );
                        }
                    }

                    // 判定項目文字
                    this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 47, 476, new Rectangle( 0, 0, 128, 24 ) );
                    this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 47, 501, new Rectangle( 0, 24, 128, 24 ) );
                    this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 47, 525, new Rectangle( 0, 48, 128, 24 ) );
                    this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 47, 549, new Rectangle( 0, 72, 128, 24 ) );
                    this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 47, 572, new Rectangle( 0, 96, 128, 24 ) );
                    this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, this.n本体X[i] + 47, 596, new Rectangle( 0, 120, 128, 24 ) );

                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 46, 628, C文字コンソール.Eフォント種別.白, "Score");

                    //数値
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 155, 484, C文字コンソール.Eフォント種別.白, string.Format("{0,4:###0}", result.nPerfect数));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 155, 508, C文字コンソール.Eフォント種別.白, string.Format("{0,4:###0}", result.nGreat数));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 155, 532, C文字コンソール.Eフォント種別.白, string.Format("{0,4:###0}", result.nGood数));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 155, 556, C文字コンソール.Eフォント種別.白, string.Format("{0,4:###0}", result.nPoor数));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 155, 580, C文字コンソール.Eフォント種別.白, string.Format("{0,4:###0}", result.nMiss数));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 155, 604, C文字コンソール.Eフォント種別.白, string.Format("{0,4:###0}", result.n最大コンボ数));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 151, 628, C文字コンソール.Eフォント種別.白, string.Format("{0,7:######0}", result.nスコア));

                    //数値2
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 223, 484, C文字コンソール.Eフォント種別.白, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPerfect率[i]));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 223, 508, C文字コンソール.Eフォント種別.白, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGreat率[i]));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 223, 532, C文字コンソール.Eフォント種別.白, string.Format("{0,3:##0}%", CDTXMania.stage結果.fGood率[i]));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 223, 556, C文字コンソール.Eフォント種別.白, string.Format("{0,3:##0}%", CDTXMania.stage結果.fPoor率[i]));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 223, 580, C文字コンソール.Eフォント種別.白, string.Format("{0,3:##0}%", CDTXMania.stage結果.fMiss率[i]));
                    CDTXMania.act文字コンソール.tPrint(this.n本体X[i] + 223, 604, C文字コンソール.Eフォント種別.白, string.Format("{0,3:##0}%", (((float)result.n最大コンボ数) / ((float)result.n全チップ数)) * 100.0f));
                }
			}

            int num5 = this.ct表示用.n現在の値 / 100;
            double num6 = 1.0 - (((double)(this.ct表示用.n現在の値 % 100)) / 100.0);
            int height = 20;

            ////文字
            //// 左サイド x=48 右サイドx=878
            //this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, 878, 476, new Rectangle( 0, 0, 128, 24 ) );
            //this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, 878, 501, new Rectangle( 0, 24, 128, 24 ) );
            //this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, 878, 525, new Rectangle( 0, 48, 128, 24 ) );
            //this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, 878, 549, new Rectangle( 0, 72, 128, 24 ) );
            //this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, 878, 572, new Rectangle( 0, 96, 128, 24 ) );
            //this.tx判定項目文字列?.t2D描画( CDTXMania.app.Device, 878, 596, new Rectangle( 0, 120, 128, 24 ) );

            //CDTXMania.act文字コンソール.tPrint( 877, 628, C文字コンソール.Eフォント種別.白, "Score" );

            ////数値
            //CDTXMania.act文字コンソール.tPrint( 986, 484, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nPerfect数 ) );
            //CDTXMania.act文字コンソール.tPrint( 986, 508, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nGreat数 ) );
            //CDTXMania.act文字コンソール.tPrint( 986, 532, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nGood数 ) );
            //CDTXMania.act文字コンソール.tPrint( 986, 556, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nPoor数 ) );
            //CDTXMania.act文字コンソール.tPrint( 986, 580, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.nMiss数 ) );
            //CDTXMania.act文字コンソール.tPrint( 986, 604, C文字コンソール.Eフォント種別.白, string.Format( "{0,4:###0}", CDTXMania.stage結果.st演奏記録.Drums.n最大コンボ数 ) );
            //CDTXMania.act文字コンソール.tPrint( 982, 628, C文字コンソール.Eフォント種別.白, string.Format( "{0,7:######0}", CDTXMania.stage結果.st演奏記録.Drums.nスコア ) );

            ////数値2
            //CDTXMania.act文字コンソール.tPrint( 1054, 484, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fPerfect率.Drums ) );
            //CDTXMania.act文字コンソール.tPrint( 1054, 508, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fGreat率.Drums ) );
            //CDTXMania.act文字コンソール.tPrint( 1054, 532, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fGood率.Drums ) );
            //CDTXMania.act文字コンソール.tPrint( 1054, 556, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fPoor率.Drums ) );
            //CDTXMania.act文字コンソール.tPrint( 1054, 580, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", CDTXMania.stage結果.fMiss率.Drums ) );
            //CDTXMania.act文字コンソール.tPrint( 1054, 604, C文字コンソール.Eフォント種別.白, string.Format( "{0,3:##0}%", (((float)CDTXMania.stage結果.st演奏記録.Drums.n最大コンボ数) / ((float)CDTXMania.stage結果.st演奏記録.Drums.n全チップ数)) * 100.0f ) );


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

        private struct ST数字フォント
        {
            public char ch文字;
            public Rectangle rect;
        }

        private ST数字フォント[] STスキル数字_整数;
        private ST数字フォント[] STスキル数字_少数;
        private ST数字フォント[] ST達成率数字_整数;
        private ST数字フォント[] ST達成率数字_少数;

		private CCounter ct表示用;
        private CCounter ctNewRecord;
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
        private CTexture txゲージ中身;
        private CTexture txゲージ2;
		private CTexture[] tx文字 = new CTexture[ 3 ];
        private CTexture txスキル数字_整数;
        private CTexture txスキル数字_少数;
        private CTexture txスキル数字_点;
        private CTexture tx達成率数字_整数;
        private CTexture tx達成率数字_少数;
        private CTexture tx白線;
        private CTexture tx項目文字列;
        private CTexture tx判定項目文字列;

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

        private void tスキル数字フォント初期化()
        {
            this.STスキル数字_整数 = new ST数字フォント[] {
                new ST数字フォント{ ch文字 = '0', rect = new Rectangle( 0, 0, 70, 70 ) },
                new ST数字フォント{ ch文字 = '1', rect = new Rectangle( 70, 0, 70, 70 ) },
                new ST数字フォント{ ch文字 = '2', rect = new Rectangle( 140, 0, 70, 70 ) },
                new ST数字フォント{ ch文字 = '3', rect = new Rectangle( 210, 0, 70, 70 ) },
                new ST数字フォント{ ch文字 = '4', rect = new Rectangle( 280, 0, 70, 70 ) },
                new ST数字フォント{ ch文字 = '5', rect = new Rectangle( 0, 70, 70, 70 ) },
                new ST数字フォント{ ch文字 = '6', rect = new Rectangle( 70, 70, 70, 70 ) },
                new ST数字フォント{ ch文字 = '7', rect = new Rectangle( 140, 70, 70, 70 ) },
                new ST数字フォント{ ch文字 = '8', rect = new Rectangle( 210, 70, 70, 70 ) },
                new ST数字フォント{ ch文字 = '9', rect = new Rectangle( 280, 70, 70, 70 ) },
            };
            this.STスキル数字_少数 = new ST数字フォント[] {
                new ST数字フォント{ ch文字 = '0', rect = new Rectangle( 0, 0, 50, 50 ) },
                new ST数字フォント{ ch文字 = '1', rect = new Rectangle( 50, 0, 50, 50 ) },
                new ST数字フォント{ ch文字 = '2', rect = new Rectangle( 100, 0, 50, 50 ) },
                new ST数字フォント{ ch文字 = '3', rect = new Rectangle( 150, 0, 50, 50 ) },
                new ST数字フォント{ ch文字 = '4', rect = new Rectangle( 200, 0, 50, 50 ) },
                new ST数字フォント{ ch文字 = '5', rect = new Rectangle( 0, 50, 50, 50 ) },
                new ST数字フォント{ ch文字 = '6', rect = new Rectangle( 50, 50, 50, 50 ) },
                new ST数字フォント{ ch文字 = '7', rect = new Rectangle( 100, 50, 50, 50 ) },
                new ST数字フォント{ ch文字 = '8', rect = new Rectangle( 150, 50, 50, 50 ) },
                new ST数字フォント{ ch文字 = '9', rect = new Rectangle( 200, 50, 50, 50 ) },
            };
        }

        private void t達成率数字フォント初期化()
        {
            this.ST達成率数字_整数 = new ST数字フォント[] {
                new ST数字フォント{ ch文字 = '0', rect = new Rectangle( 0, 0, 38, 50 ) },
                new ST数字フォント{ ch文字 = '1', rect = new Rectangle( 38, 0, 38, 50 ) },
                new ST数字フォント{ ch文字 = '2', rect = new Rectangle( 76, 0, 38, 50 ) },
                new ST数字フォント{ ch文字 = '3', rect = new Rectangle( 114, 0, 38, 50 ) },
                new ST数字フォント{ ch文字 = '4', rect = new Rectangle( 152, 0, 38, 50 ) },
                new ST数字フォント{ ch文字 = '5', rect = new Rectangle( 0, 50, 38, 50 ) },
                new ST数字フォント{ ch文字 = '6', rect = new Rectangle( 38, 50, 38, 50 ) },
                new ST数字フォント{ ch文字 = '7', rect = new Rectangle( 76, 50, 38, 50 ) },
                new ST数字フォント{ ch文字 = '8', rect = new Rectangle( 114, 50, 38, 50 ) },
                new ST数字フォント{ ch文字 = '9', rect = new Rectangle( 152, 50, 38, 50 ) }
            };
            this.ST達成率数字_少数 = new ST数字フォント[] {
                new ST数字フォント{ ch文字 = '0', rect = new Rectangle( 0, 0, 32, 42 ) },
                new ST数字フォント{ ch文字 = '1', rect = new Rectangle( 32, 0, 32, 42 ) },
                new ST数字フォント{ ch文字 = '2', rect = new Rectangle( 64, 0, 32, 42 ) },
                new ST数字フォント{ ch文字 = '3', rect = new Rectangle( 96, 0, 32, 42 ) },
                new ST数字フォント{ ch文字 = '4', rect = new Rectangle( 128, 0, 32, 42 ) },
                new ST数字フォント{ ch文字 = '5', rect = new Rectangle( 0, 42, 32, 42 ) },
                new ST数字フォント{ ch文字 = '6', rect = new Rectangle( 32, 42, 32, 42 ) },
                new ST数字フォント{ ch文字 = '7', rect = new Rectangle( 64, 42, 32, 42 ) },
                new ST数字フォント{ ch文字 = '8', rect = new Rectangle( 96, 42, 32, 42 ) },
                new ST数字フォント{ ch文字 = '9', rect = new Rectangle( 128, 42, 32, 42 ) },
                new ST数字フォント{ ch文字 = '%', rect = new Rectangle( 0, 84, 42, 42 ) },
                new ST数字フォント{ ch文字 = '.', rect = new Rectangle( 42, 84, 32, 42 ) }
            };
        }

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
        private void tスキル値の描画( int x, int y, double dbスキル値 )
        {
            if( dbスキル値 < 0 || dbスキル値 > 200 )
                return;

            // 1文字あたりのマージン
            int n文字間隔_整数部 = 46;
            int n文字間隔_小数部 = 32;
            bool b先頭処理中 = true;
            bool b整数部処理中 = true;
            dbスキル値 = dbスキル値 * 100.0;
            dbスキル値 = Math.Floor( dbスキル値 );
            dbスキル値 = dbスキル値 / 100.0;
            string formatText = string.Format( "{0,6:000.00}", dbスキル値 );

            for( int i = 0; i < formatText.Length; i++ )
            {
                char c = formatText[ i ];

                if( c.Equals( '.' ) )
                {
                    // 小数点だったら小数点を描画してフラグ切り替えてcontinue
                    this.txスキル数字_点.t2D描画( CDTXMania.app.Device, x - 14, y );
                    b整数部処理中 = false;
                    x += 18;
                    continue;
                }
                else if( ( !c.Equals( '0' ) && b先頭処理中 ) || i == 2 )
                {
                    b先頭処理中 = false;
                }
                else if( c.Equals( ' ' ) )
                {
                    // 空白ならなにもせずcontinue
                    continue;
                }

                for( int j = 0; j < 10; j++ )
                {
                    if( c.Equals( this.STスキル数字_整数[ j ].ch文字 ) )
                    {
                        if( b整数部処理中 )
                        {
                            this.txスキル数字_整数.n透明度 = b先頭処理中 ? 128 : 255;
                            this.txスキル数字_整数.t2D描画( CDTXMania.app.Device, x, y, this.STスキル数字_整数[ j ].rect );
                            x += n文字間隔_整数部;
                        }
                        else
                        {
                            this.txスキル数字_少数.t2D描画( CDTXMania.app.Device, x, y + 20, this.STスキル数字_少数[ j ].rect );
                            x += n文字間隔_小数部;
                        }
                    }
                }
            }
        }

        private void t達成率値の描画( int x, int y, double dbスキル値 )
        {
            // 1文字あたりのマージン
            int n文字間隔_整数部 = 38;
            int n文字間隔_小数部 = 30;
            bool b整数部処理中 = true;
            string formatText = string.Format( "{0,5:#0.00}%", dbスキル値 );

            for( int i = 0; i < formatText.Length; i++ )
            {
                char c = formatText[ i ];

                if( c.Equals( '.' ) )
                {
                    // 小数点だったら小数点を描画してフラグ切り替えてcontinue
                    this.tx達成率数字_少数?.t2D描画( CDTXMania.app.Device, x - 12, y + 8, this.ST達成率数字_少数[ 11 ].rect );
                    b整数部処理中 = false;
                    x += 8;
                    continue;
                }
                else if( c.Equals( '%' ) )
                {
                    this.tx達成率数字_少数?.t2D描画( CDTXMania.app.Device, x, y + 8, this.ST達成率数字_少数[ 10 ].rect );
                    continue;
                }
                else if( c.Equals( ' ' ) )
                {
                    // 空白ならなにもせずcontinue
                    continue;
                }

                for( int j = 0; j < 10; j++ )
                {
                    if( c.Equals( this.ST達成率数字_整数[ j ].ch文字 ) )
                    {
                        if( b整数部処理中 )
                        {
                            this.tx達成率数字_整数?.t2D描画( CDTXMania.app.Device, x, y, this.ST達成率数字_整数[ j ].rect );
                            x += n文字間隔_整数部;
                        }
                        else
                        {
                            this.tx達成率数字_少数?.t2D描画( CDTXMania.app.Device, x, y + 9, this.ST達成率数字_少数[ j ].rect );
                            x += n文字間隔_小数部;
                        }
                    }
                }
            }
        }

        private void tレベル値の描画( int x, int y, int nレベル, int nレベルDec )
        {
            // 1文字あたりのマージン
            int n文字間隔_整数部 = 38;
            int n文字間隔_小数部 = 30;
            bool b整数部処理中 = true;
            decimal decLevel = 0.00M;
            decLevel += nレベル / 10.0M;
            decLevel += nレベルDec / 100.0M;
            string formatText = string.Format( "{0,4:0.00}", decLevel.ToString() );

            for( int i = 0; i < formatText.Length; i++ )
            {
                char c = formatText[ i ];

                if( c.Equals( '.' ) )
                {
                    // 小数点だったら小数点を描画してフラグ切り替えてcontinue
                    this.tx達成率数字_少数?.t2D描画( CDTXMania.app.Device, x - 12, y + 8, this.ST達成率数字_少数[ 11 ].rect );
                    b整数部処理中 = false;
                    x += 8;
                    continue;
                }
                else if( c.Equals( ' ' ) )
                {
                    // 空白ならなにもせずcontinue
                    continue;
                }

                for( int j = 0; j < 10; j++ )
                {
                    if( c.Equals( this.ST達成率数字_整数[ j ].ch文字 ) )
                    {
                        if( b整数部処理中 )
                        {
                            this.tx達成率数字_整数?.t2D描画( CDTXMania.app.Device, x, y, this.ST達成率数字_整数[ j ].rect );
                            x += n文字間隔_整数部;
                        }
                        else
                        {
                            this.tx達成率数字_少数?.t2D描画( CDTXMania.app.Device, x, y + 9, this.ST達成率数字_少数[ j ].rect );
                            x += n文字間隔_小数部;
                        }
                    }
                }
            }
        }
		//-----------------
		#endregion
	}
}
