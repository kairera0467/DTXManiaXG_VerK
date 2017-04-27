using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using SlimDX.Direct3D9;
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
                string strPath = CDTXMania.strEXEのあるフォルダ + @"System\Common\Shutter\";
                bool bBLACK = false; 
                int i;
                if( CDTXMania.Skin.listShutterImage.Count != 0 )
                {
                    //配列取得
                    string[] shutterNames = CDTXMania.Skin.arGetShutterName();

                    //リスト
                    for( i = 0; i < 3; i++ )
                    {
                        int shutterIndex = Array.BinarySearch( shutterNames, CDTXMania.ConfigIni.strShutterImageName[ i ] );
                        if( CDTXMania.ConfigIni.strShutterImageName[ i ] != "BLACK" )
                        {
                            if( i == 0 )
                                this.txShutter[ i ] = CDTXMania.tテクスチャの生成( strPath + CDTXMania.Skin.listShutterImage[ shutterIndex ].strFilePathD );
                            else
                                this.txShutter[ i ] = CDTXMania.tテクスチャの生成( strPath + CDTXMania.Skin.listShutterImage[ shutterIndex ].strFilePathGB );
                        }
                        else
                        {
                            if( i == 0 )
                                this.t黒シャッターの生成( E楽器パート.DRUMS );
                            else if( i == 1 )
                                this.t黒シャッターの生成( E楽器パート.GUITAR );
                            else if( i == 2 )
                                this.t黒シャッターの生成( E楽器パート.BASS );
                        }
                    }
                }
                else
                {
                    //画像リストが無い場合は全パート黒テクスチャ
                    this.t黒シャッターの生成( E楽器パート.DRUMS );
                    this.t黒シャッターの生成( E楽器パート.GUITAR );
                    this.t黒シャッターの生成( E楽器パート.BASS );
                }

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                for( int i = 0; i < 3; i++ )
                {
                    CDTXMania.t安全にDisposeする( ref this.txShutter );
                }

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                if( this.txShutter.Drums != null )
                {
                    //Drum
                    if( CDTXMania.ConfigIni.bDrums有効 )
                    {
                        //IN側
                        this.txShutter.Drums.t2D描画( CDTXMania.app.Device, 295, (int)( ( CDTXMania.ConfigIni.nShutterInSide.Drums / 100.0 ) * 720.0 ) - 720 );

                        //OUT側
                        this.txShutter.Drums.t2D描画( CDTXMania.app.Device, 295, 720 - (int)( ( CDTXMania.ConfigIni.nShutterOutSide.Drums / 100.0 ) * 720.0 ) );
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

        public void t黒シャッターの生成( E楽器パート ePart )
        {
            if( ePart == E楽器パート.DRUMS ){
                this.txShutter[ 0 ] = new CTexture( CDTXMania.app.Device, 558, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );
            } else {
                this.txShutter[ ePart == E楽器パート.GUITAR ? 1 : 2 ] = new CTexture( CDTXMania.app.Device, 252, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );
            }
        }

		// その他

		#region [ private ]
		//-----------------
        private STDGBVALUE<CTexture> txShutter = new STDGBVALUE<CTexture>();
		//-----------------
		#endregion
	}
}
