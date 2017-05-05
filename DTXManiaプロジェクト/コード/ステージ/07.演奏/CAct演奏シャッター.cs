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
            this.nShutterCounter = 0;
            this.nShutterCounter2 = 0;
            this.nShutterStartTime = 0;
            this.bSinglePush = false;
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

        CCounter ctShutter;
        int nShutterCounter;
        int nShutterCounter2;
        int nShutterStartTime;
        bool bSinglePush;

        //今のところドラムのみ
        public void tShutterMove( IInputDevice bKeyboard )
        {
            #region[ ShutterInSide Up ]
            if ( bKeyboard.bキーが離されている( (int) SlimDX.DirectInput.Key.RightShift ) && bKeyboard.bキーが離されている( (int) SlimDX.DirectInput.Key.LeftShift ) )
            {
                if ( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.NumberPad8 ) )
                {
                    if( nShutterStartTime <= 0 )
                    {
                        nShutterStartTime = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                        nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) > 10 && (int)( CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 60 )
                    {
                        nShutterCounter = ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 );
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) < 50 && !this.bSinglePush ) // 単押し判定
                        {
                            this.tShutterMove( E楽器パート.DRUMS, 0, 0 );
                            this.bSinglePush = true; //短い時間で離したら単押しと判定。
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 1000 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 100 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 0, 0 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 50 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 0, 0 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) >= 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 30 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 0, 0 );
                        }
                    }
                }
            }
            else if( bKeyboard.bキーが離された( (int) SlimDX.DirectInput.Key.NumberPad8 ) )
            {
                if( nShutterStartTime > 0 )
                    nShutterStartTime = 0;
                this.bSinglePush = false;
            }
            #endregion
            #region[ ShutterInSide Down ]
            if ( bKeyboard.bキーが離されている( (int) SlimDX.DirectInput.Key.RightShift ) && bKeyboard.bキーが離されている( (int) SlimDX.DirectInput.Key.LeftShift ) )
            {
                if ( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.NumberPad2 ) )
                {
                    if( nShutterStartTime <= 0 )
                    {
                        nShutterStartTime = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                        nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) > 10 && (int)( CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 60 )
                    {
                        nShutterCounter = ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 );
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) < 50 && !this.bSinglePush ) // 単押し判定
                        {
                            this.tShutterMove( E楽器パート.DRUMS, 0, 1 );
                            this.bSinglePush = true; //短い時間で離したら単押しと判定。
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 1000 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 100 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 0, 1 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 50 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 0, 1 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) >= 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 30 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 0, 1 );
                        }
                    }
                }
            }
            else if( bKeyboard.bキーが離された( (int) SlimDX.DirectInput.Key.NumberPad2 ) )
            {
                if( nShutterStartTime > 0 )
                    nShutterStartTime = 0;
                this.bSinglePush = false;
            }
            #endregion
            #region[ ShutterOutSide Up ]
            if( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) || bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) )
            {
                if( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.NumberPad8 ) )
                {
                    if( nShutterStartTime <= 0 )
                    {
                        nShutterStartTime = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                        nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) > 10 && (int)( CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 60 )
                    {
                        nShutterCounter = ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 );
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) < 50 && !this.bSinglePush ) // 単押し判定
                        {
                            this.tShutterMove( E楽器パート.DRUMS, 1, 0 );
                            this.bSinglePush = true; //短い時間で離したら単押しと判定。
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 1000 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 100 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 1, 0 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 50 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 1, 0 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) >= 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 30 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 1, 0 );
                        }
                    }
                }
            }
            if( bKeyboard.bキーが離された( (int) SlimDX.DirectInput.Key.NumberPad8 ) )
            {
                if( nShutterStartTime > 0 )
                    nShutterStartTime = 0;
                this.bSinglePush = false;
            }
            #endregion
            #region[ ShutterOutSide Down ]
            if( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) || bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) )
            {
                if( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.NumberPad2 ) )
                {
                    if( nShutterStartTime <= 0 )
                    {
                        nShutterStartTime = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                        nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) > 10 && (int)( CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 60 )
                    {
                        nShutterCounter = ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 );
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) < 50 && !this.bSinglePush ) // 単押し判定
                        {
                            this.tShutterMove( E楽器パート.DRUMS, 1, 1 );
                            this.bSinglePush = true; //短い時間で離したら単押しと判定。
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 1000 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 100 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 1, 1 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) < 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 50 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 1, 1 );
                        }
                    }
                    else if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterStartTime ) >= 2500 )
                    {
                        if( ( (int)CSound管理.rc演奏用タイマ.nシステム時刻ms - nShutterCounter2 ) > 30 )
                        {
                            nShutterCounter2 = (int)CSound管理.rc演奏用タイマ.nシステム時刻ms;
                            this.tShutterMove( E楽器パート.DRUMS, 1, 1 );
                        }
                    }
                }
            }
            if( bKeyboard.bキーが離された( (int) SlimDX.DirectInput.Key.NumberPad2 ) )
            {
                if( nShutterStartTime > 0 )
                    nShutterStartTime = 0;
                this.bSinglePush = false;
            }
            #endregion

            //デバッグ用
            //if( bKeyboard.bキーが押された( (int) SlimDX.DirectInput.Key.D0 ) )
            //{
            //    CDTXMania.ConfigIni.nShutterInSide.Drums = 100;
            //}
            if ( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.NumberPad2 ) )
            {
                CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.白, "NUM2" );
            }
            if ( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.NumberPad8 ) )
            {
                CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.白, "NUM8" );
            }
            if ( bKeyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) )
            {
                CDTXMania.act文字コンソール.tPrint( 0, 16, C文字コンソール.Eフォント種別.白, "R SHIFT" );
            }
        }

        /// <summary>
        /// シャッターを1px動かす
        /// </summary>
        /// <param name="ePart">楽器パート(Guitar:ギター画面1P、Bass:ギター画面2P)</param>
        /// <param name="MoveSide">移動するシャッター(0:上側、1:下側)</param>
        /// <param name="Direction">移動する方向(0:上、1:下)</param>
        private void tShutterMove( E楽器パート ePart, int MoveSide, int nDirection )
        {
            if( MoveSide == 0 )
            {
                switch( ePart )
                {
                    case E楽器パート.DRUMS:
                        if( nDirection == 0 && CDTXMania.ConfigIni.nShutterInSide.Drums >= 0 )
                            CDTXMania.ConfigIni.nShutterInSide.Drums = CDTXMania.ConfigIni.nShutterInSide.Drums - 1;
                        else if( nDirection == 1 && CDTXMania.ConfigIni.nShutterInSide.Drums < 100 )
                            CDTXMania.ConfigIni.nShutterInSide.Drums = CDTXMania.ConfigIni.nShutterInSide.Drums + 1;
                        break;
                    case E楽器パート.GUITAR:
                        if( nDirection == 0 && CDTXMania.ConfigIni.nShutterInSide.Guitar >= 0 ) CDTXMania.ConfigIni.nShutterInSide.Guitar = CDTXMania.ConfigIni.nShutterInSide.Guitar - 1;
                        else if( nDirection == 1 && CDTXMania.ConfigIni.nShutterInSide.Guitar < 100 ) CDTXMania.ConfigIni.nShutterInSide.Guitar = CDTXMania.ConfigIni.nShutterInSide.Guitar + 1;
                        break;
                    case E楽器パート.BASS:
                        if( nDirection == 0 && CDTXMania.ConfigIni.nShutterInSide.Bass >= 0 ) CDTXMania.ConfigIni.nShutterInSide.Bass = CDTXMania.ConfigIni.nShutterInSide.Bass - 1;
                        else if( nDirection == 1 && CDTXMania.ConfigIni.nShutterInSide.Bass < 100 ) CDTXMania.ConfigIni.nShutterInSide.Bass = CDTXMania.ConfigIni.nShutterInSide.Bass + 1;
                        break;
                }
            }
            else
            {
                switch( ePart )
                {
                    case E楽器パート.DRUMS:
                        if( nDirection == 0 && CDTXMania.ConfigIni.nShutterOutSide.Drums >= 0 )
                            CDTXMania.ConfigIni.nShutterOutSide.Drums = CDTXMania.ConfigIni.nShutterOutSide.Drums + 1;
                        else if( nDirection == 1 && CDTXMania.ConfigIni.nShutterOutSide.Drums < 100 )
                            CDTXMania.ConfigIni.nShutterOutSide.Drums = CDTXMania.ConfigIni.nShutterOutSide.Drums - 1;
                        break;
                    case E楽器パート.GUITAR:
                        if( nDirection == 0 && CDTXMania.ConfigIni.nShutterOutSide.Guitar >= 0 ) CDTXMania.ConfigIni.nShutterOutSide.Guitar = CDTXMania.ConfigIni.nShutterOutSide.Guitar - 1;
                        else if( nDirection == 1 && CDTXMania.ConfigIni.nShutterOutSide.Guitar < 100 ) CDTXMania.ConfigIni.nShutterOutSide.Guitar = CDTXMania.ConfigIni.nShutterOutSide.Guitar + 1;
                        break;
                    case E楽器パート.BASS:
                        if( nDirection == 0 && CDTXMania.ConfigIni.nShutterOutSide.Bass >= 0 ) CDTXMania.ConfigIni.nShutterOutSide.Bass = CDTXMania.ConfigIni.nShutterOutSide.Bass - 1;
                        else if( nDirection == 1 && CDTXMania.ConfigIni.nShutterOutSide.Bass > 100 ) CDTXMania.ConfigIni.nShutterOutSide.Bass = CDTXMania.ConfigIni.nShutterOutSide.Bass + 1;
                        break;
                }
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
