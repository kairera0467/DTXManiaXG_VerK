using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FDK;

namespace DTXMania
{
	internal class CAct演奏DrumsコンボDGB_GD : CAct演奏Combo共通
	{
		// CAct演奏Combo共通 実装

        public override void On活性化()
        {
            for (int i = 0; i < 256; i++)
            {
                this.b爆発した[i] = false;
                base.bn00コンボに到達した[i].Drums = false;
            }
            base.nコンボカウント.Drums = 0;
            this.n火薬カウント = 0;
            base.On活性化();
        }

        public void Start( int nCombo値 )
        {
            this.n火薬カウント = nCombo値 / 100;

 
            for (int j = 0; j < 1; j++)
            {
                if (this.st爆発[j].b使用中)
                {
                    this.st爆発[j].ct進行.t停止();
                    this.st爆発[j].b使用中 = false;
                    this.b爆発した[ this.n火薬カウント ] = true;

                }
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (!this.st爆発[j].b使用中)
                    {
                        this.st爆発[j].b使用中 = true;
                        this.st爆発[j].ct進行 = new CCounter(0, 13, 20, CDTXMania.Timer);
                        break;
                    }
                }
            }
        }

		protected override void tコンボ表示_ギター( int nCombo値, int nジャンプインデックス )
		{
			int x, y;
			if( CDTXMania.DTX.bチップがある.Bass || CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Center )
			{
				x = (CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Left)? 1638 : 1567+5;
				//y = CDTXMania.ConfigIni.bReverse.Guitar ? 0xaf : 270;
				y = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.GUITAR, false, CDTXMania.ConfigIni.bReverse.Guitar );
				y += CDTXMania.ConfigIni.bReverse.Guitar ? (int) ( -134 * Scale.Y ) : (int) ( +174 * Scale.Y );
				if ( base.txCOMBOギター != null )
				{
					base.txCOMBOギター.n透明度 = 120;
				}
			}
			else
			{
				x = 1344;
				//y = CDTXMania.ConfigIni.bReverse.Guitar ? 0xee : 0xcf;
				y = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.GUITAR, false, CDTXMania.ConfigIni.bReverse.Guitar );
				y += CDTXMania.ConfigIni.bReverse.Guitar ? (int) ( -134 * Scale.Y ) : (int) ( +174 * Scale.Y );
				if ( base.txCOMBOギター != null )
				{
					base.txCOMBOギター.n透明度 = 0xff;
				}
			}
			base.tコンボ表示_ギター( nCombo値, x, y, nジャンプインデックス );
		}
		protected override void tコンボ表示_ドラム( int nCombo値, int nジャンプインデックス )
		{
			base.tコンボ表示_ドラム( nCombo値, nジャンプインデックス );

            this.n火薬カウント = (nCombo値 / 100);

            //if (nCombo値 % 100 == 0)
            if ((nCombo値 > (nCombo値 / 100) + 100) && this.b爆発した[n火薬カウント] == false)
            {
                this.Start(nCombo値);
            }

            int x = 1130 - 180;
            int y = 16 - 95;

            if (nCombo値 >= 100)
            {
                for (int i = 0; i < 1; i++)
                {
                    if (this.st爆発[i].b使用中)
                    {
                        int num1 = this.st爆発[i].ct進行.n現在の値;
                        this.st爆発[i].ct進行.t進行();
                        if (this.st爆発[i].ct進行.b終了値に達した)
                        {
                            this.st爆発[i].ct進行.t停止();
                            this.st爆発[i].b使用中 = false;
                            this.bn00コンボに到達した[this.nコンボカウント.Drums].Drums = true;
                        }
                        if (this.txComboBom != null )
                        {
                            this.txComboBom.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, (340 * num1), 360, 340));
                        }
                    }
                }
            }
		}
		protected override void tコンボ表示_ベース( int nCombo値, int nジャンプインデックス )
		{
			int x = ( CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Left ) ? 1311 : 1311-994+5;
			//int y = CDTXMania.ConfigIni.bReverse.Bass ? 0xaf : 270;
			int y = 演奏判定ライン座標.n判定ラインY座標( E楽器パート.BASS, false, CDTXMania.ConfigIni.bReverse.Bass );
			y += CDTXMania.ConfigIni.bReverse.Bass ? (int) ( -134 * Scale.Y ) : (int) ( +174 * Scale.Y );
			if ( base.txCOMBOギター != null )
			{
				base.txCOMBOギター.n透明度 = 120;
			}
			base.tコンボ表示_ベース( nCombo値, x, y, nジャンプインデックス );
		}
	}
}
