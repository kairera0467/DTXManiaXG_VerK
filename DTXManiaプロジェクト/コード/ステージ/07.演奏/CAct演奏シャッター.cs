using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏シャッター : CActivity
	{

		// コンストラクタ

		public CAct演奏シャッター()
		{
			base.b活性化してない = true;
		}
		
		// メソッド

		// CActivity 実装

		public override void On活性化()
		{

			base.On活性化();
		}
		public override void On非活性化()
		{

			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                //シャッターテクスチャの生成
                //固定実装
                string strPath = @"..\Common\\Shutter\\" + "default_d.png";
                //if(  )
                //{
                    
                //}
                //else if( )
                //{
                //
                //}
                this.txShutter = CDTXMania.tテクスチャの生成( CSkin.Path( strPath ) );

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txShutter );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                if( this.txShutter != null )
                {
                    //Drum
                    if( CDTXMania.ConfigIni.bDrums有効 )
                    {
                        //IN側
                        this.txShutter.t2D描画( CDTXMania.app.Device, 295, (int)( ( CDTXMania.ConfigIni.nShutterInSide.Drums / 100.0 ) * 720.0 ) - 720 );

                        //OUT側
                        this.txShutter.t2D描画( CDTXMania.app.Device, 295, 720 - (int)( ( CDTXMania.ConfigIni.nShutterOutSide.Drums / 100.0 ) * 720.0 ) );
                    }
                    else
                    {
                        //int[] arLaneX = new int[] { 79, 949 }; //いずれはCSkinに置きたい...
                        //for( int i = 0; i < 2; i++ )
                        //{
                            //this.txShutter.t2D描画( CDTXMania.app.Device, arLaneX[ i ], 0 );
                        //}
                    }
                }

			}
            return base.On進行描画();
		}


		// その他

		#region [ private ]
		//-----------------
        private CTexture txShutter;
		//-----------------
		#endregion
	}
}
