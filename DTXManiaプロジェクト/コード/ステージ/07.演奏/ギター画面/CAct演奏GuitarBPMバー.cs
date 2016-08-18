using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
    internal class CAct演奏GuitarBPMバー : CAct演奏BPMバー共通
    {
        // CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {
                base.ctBPMバー.t進行Loop();
                int num1 = base.ctBPMバー.n現在の値;

                float fギター左X = 67;
                float fギター右X = 331;
                float fベース左X = 940;
                float fベース右X = 1201;
                float fバーY = 57;

                if( base.txBPMバー != null )
                {
                    base.txBPMバー.n透明度 = 255;
                    //穴
                    //ギター
                    if( CDTXMania.DTX.bチップがある.Guitar )
                    {
                        base.txBPMバー.t2D描画( CDTXMania.app.Device, 71, 45, new Rectangle( 0, 0, 14, 627 ) );
                        base.txBPMバー.t2D描画( CDTXMania.app.Device, 323, 45, new Rectangle( 14, 0, 14, 627 ) );
                    }
                    //ベース
                    if( CDTXMania.DTX.bチップがある.Bass )
                    {
                        base.txBPMバー.t2D描画( CDTXMania.app.Device, 945, 45, new Rectangle( 0, 0, 14, 627 ) );
                        base.txBPMバー.t2D描画( CDTXMania.app.Device, 1192, 45, new Rectangle( 14, 0, 14, 627 ) );
                    }
                }

                if ((base.txBPMバー != null))// && CDTXMania.stage演奏ギター画面.ct登場用.n現在の値 >= 11)
                {
                    //if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.A)
                    {

                        if( CDTXMania.DTX.bチップがある.Guitar )
                        {
                            base.txBPMバー.n透明度 = 255;
                            base.txBPMバー.t2D描画(CDTXMania.app.Device, fギター左X - (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 28, 0, 10, 600 ));

                            //if( CDTXMania.stage演奏ギター画面.bサビ区間 )
                            //{
                            //    base.txBPMバー.n透明度 = 255 - (int)(255 * num1 / 14);
                            //    base.txBPMバー.t2D描画(CDTXMania.app.Device, - 13 + fギター左X - (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 48, 0, 32, 600 ));
                            //}
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー.n透明度 = 255;
                            base.txBPMバー.t2D描画(CDTXMania.app.Device, fベース右X + (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 28, 0, 10, 600 ));

                            //if( CDTXMania.stage演奏ギター画面.bサビ区間 )
                            //{
                            //    base.txBPMバー.n透明度 = 255 - (int)(255 * num1 / 14);
                            //    base.txBPMバー.t2D描画(CDTXMania.app.Device, fベース右X + (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 80, 0, 32, 600 ));
                            //}
                        }

                    }
                    //if ( CDTXMania.ConfigIni.eBPMbar == Eタイプ.A || CDTXMania.ConfigIni.eBPMbar == Eタイプ.B )
                    {

                        if (CDTXMania.DTX.bチップがある.Guitar)
                        {
                            base.txBPMバー.n透明度 = 255;
                            base.txBPMバー.t2D描画(CDTXMania.app.Device, fギター右X + (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 38, 0, 10, 600 ));

                            //if( CDTXMania.stage演奏ギター画面.bサビ区間 )
                            //{
                            //    base.txBPMバー.n透明度 = 255 - (int)(255 * num1 / 14);
                            //    base.txBPMバー.t2D描画(CDTXMania.app.Device, fギター右X + (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 80, 0, 32, 600 ));
                            //}
                        }

                        if (CDTXMania.DTX.bチップがある.Bass)
                        {
                            base.txBPMバー.n透明度 = 255;
                            base.txBPMバー.t2D描画(CDTXMania.app.Device, fベース左X - (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 38, 0, 10, 600 ));

                            //if( CDTXMania.stage演奏ギター画面.bサビ区間 )
                            //{
                            //    base.txBPMバー.n透明度 = 255 - (int)(255 * num1 / 14);
                            //    base.txBPMバー.t2D描画(CDTXMania.app.Device, - 13 - fベース左X - (float)(6 * Math.Sin(Math.PI * num1 / 14)), fバーY, new Rectangle( 48, 0, 32, 600 ));
                            //}
                        }
                    }
                }
            }
            return 0;
        }
    }
}
