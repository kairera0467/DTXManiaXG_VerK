using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActResultRank : CActivity
	{
		// コンストラクタ

		public CActResultRank()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ctランク表示.n現在の値 = this.ctランク表示.n終了値;
		}


		// CActivity 実装

		public override void On活性化()
		{
            #region [ 本体位置 ]

            int n上X = 138;
            int n上Y = 8;

            int n下X = 850;
            int n下Y = 420;

            this.n本体X[0] = 0;
            this.n本体Y[0] = 0;

            this.n本体X[1] = 0;
            this.n本体Y[1] = 0;

            this.n本体X[2] = 0;
            this.n本体Y[2] = 0;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体X[0] = n上X;
                this.n本体Y[0] = n上Y;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[1] = n下X;
                        this.n本体Y[1] = n下Y;
                    }
                    else
                    {
                        this.n本体X[1] = n上X;
                        this.n本体Y[1] = n上Y;
                    }
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[2] = n上X;
                        this.n本体Y[2] = n上Y;
                    }
                    else
                    {
                        this.n本体X[2] = n下X;
                        this.n本体Y[2] = n下Y;
                    }
                }

            }
            #endregion

            this.b全オート.Drums = CDTXMania.ConfigIni.bドラムが全部オートプレイである;
            this.b全オート.Guitar = CDTXMania.ConfigIni.bギターが全部オートプレイである;
            this.b全オート.Bass = CDTXMania.ConfigIni.bベースが全部オートプレイである;
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ct表示用 != null )
			{
				this.ct表示用 = null;
			}
			if( this.ctランク表示 != null )
			{
				this.ctランク表示 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txFullCombo = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_fullcombo.png"));
                this.txExcellent = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_Excellent.png"));
                for (int j = 0; j < 3; j++)
                {
                    switch (CDTXMania.stage結果.nランク値[j])
                    {
                        case 0:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        case 1:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankS.png"));
                            break;

                        case 2:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankA.png"));
                            break;

                        case 3:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankB.png"));
                            break;

                        case 4:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankC.png"));
                            break;

                        case 5:
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankD.png"));
                            break;

                        case 6:
                        case 99:	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
                            this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankE.png"));
                            if (this.b全オート[j])
                                this.txランク文字[j] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\8_rankSS.png"));
                            break;

                        default:
                            this.txランク文字[j] = null;
                            break;
                    }
                }
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txFullCombo );
                CDTXMania.tテクスチャの解放( ref this.txExcellent );
                CDTXMania.t安全にDisposeする( ref this.txランク文字 );
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
                this.ctランク表示 = new CCounter(0, 0x3e8, 2, CDTXMania.Timer);
                this.ct表示用 = new CCounter(0, 1000, 3, CDTXMania.Timer);
				base.b初めての進行描画 = false;
			}
			this.ctランク表示.t進行();
            this.ct表示用.t進行();
            if (this.ctランク表示.n現在の値 >= 500)
			{
				double num2 = ( (double) ( this.ctランク表示.n現在の値 - 500 ) ) / 500.0;
                for (int j = 0; j < 3; j++)
                {
                    if (this.txランク文字[j] != null && this.n本体X[j] != 0)
                    {
                        this.txランク文字[j].t2D描画(CDTXMania.app.Device, this.n本体X[j], this.n本体Y[j], new Rectangle(0, 0, (int)((double)txランク文字[j].sz画像サイズ.Width * num2), this.txランク文字[j].sz画像サイズ.Height));
                    }
                }
			}
			if( !this.ctランク表示.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}
		

		// その他

		#region [ private ]
		//-----------------
		private CCounter ctランク表示;
        private CCounter ct表示用;
        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;
        private STDGBVALUE<bool> b全オート;
        private STDGBVALUE<CTexture> txランク文字;
        private CTexture txExcellent;
        private CTexture txFullCombo;
		//-----------------
		#endregion
	}
}
