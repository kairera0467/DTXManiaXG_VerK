using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using SharpDX;
using FDK;

using Color = System.Drawing.Color;
using Rectangle = System.Drawing.Rectangle;
using SlimDXKey = SlimDX.DirectInput.Key;

namespace DTXMania
{
    internal class CAct演奏DrumsステータスパネルGD : CAct演奏ステータスパネル共通
    {
        //2016.02.21 kairera0467 Imageの解放方法を変更。これでファイルが無かった時の例外処理の書き方が楽になるはず。

        public override void On活性化()
        {
            this.pfPlayerNameFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 18, FontStyle.Regular );
            this.pfSongTitleFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 14, FontStyle.Regular );
            this.pfSongArtistFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 9, FontStyle.Regular );
            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if( !base.b活性化してない )
            {
                string strSongName;
                string strArtistName;

                if ( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    strSongName = CDTXMania.stage選曲GITADORA.r確定された曲.strタイトル;
                else
                    strSongName = CDTXMania.DTX.TITLE;

                if ( string.IsNullOrEmpty( CDTXMania.DTX.ARTIST ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    strArtistName = CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.アーティスト名;
                else
                    strArtistName = CDTXMania.DTX.ARTIST;

                this.txSongTitle?.Dispose();
                this.txArtistName?.Dispose();
                this.txSongTitle = this.t指定された文字テクスチャを生成する( strSongName );
                this.txArtistName = this.t指定された文字テクスチャを生成する_小( strArtistName );

                Bitmap bmpCardName = new Bitmap(1, 1);
                #region[ ネームカラー ]
                //--------------------
                Color clNameColor = Color.White;
                Color clNameColorLower = Color.White;
                switch( CDTXMania.ConfigIni.nNameColor[ 0 ] )
                {
                    case 0:
                        clNameColor = Color.White;
                        break;
                    case 1:
                        clNameColor = Color.LightYellow;
                        break;
                    case 2:
                        clNameColor = Color.Yellow;
                        break;
                    case 3:
                        clNameColor = Color.Green;
                        break;
                    case 4:
                        clNameColor = Color.Blue;
                        break;
                    case 5:
                        clNameColor = Color.Purple;
                        break;
                    case 6:
                        clNameColor = Color.Red;
                        break;
                    case 7:
                        clNameColor = Color.Brown;
                        break;
                    case 8:
                        clNameColor = Color.Silver;
                        break;
                    case 9:
                        clNameColor = Color.Gold;
                        break;

                    case 10:
                        clNameColor = Color.White;
                        break;
                    case 11:
                        clNameColor = Color.LightYellow;
                        clNameColorLower = Color.White;
                        break;
                    case 12:
                        clNameColor = Color.Yellow;
                        clNameColorLower = Color.White;
                        break;
                    case 13:
                        clNameColor = Color.FromArgb( 0, 255, 33 );
                        clNameColorLower = Color.White;
                        break;
                    case 14:
                        clNameColor = Color.FromArgb( 0, 38, 255 );
                        clNameColorLower = Color.White;
                        break;
                    case 15:
                        clNameColor = Color.FromArgb( 72, 0, 255 );
                        clNameColorLower = Color.White;
                        break;
                    case 16:
                        clNameColor = Color.FromArgb( 255, 255, 0, 0 );
                        clNameColorLower = Color.White;
                        break;
                    case 17:
                        clNameColor = Color.FromArgb( 255, 232, 182, 149 );
                        clNameColorLower = Color.FromArgb( 255, 122, 69, 26 );
                        break;
                    case 18:
                        clNameColor = Color.FromArgb( 246, 245, 255 );
                        clNameColorLower = Color.FromArgb( 125, 128, 137 );
                        break;
                    case 19:
                        clNameColor = Color.FromArgb( 255, 238, 196, 85 );
                        clNameColorLower = Color.FromArgb(255, 255, 241, 200 );
                        break;
                }

                //if (CDTXMania.ConfigIni.nNameColor.Drums >= 11)
                //{
                //    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent, clNameColor, clNameColorLower);
                //}
                //else
                //{
                //    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent);
                //}
                //--------------------
                #endregion
                #region[ 名前、グループ名 ]
                bmpCardName = this.pfPlayerNameFont.DrawPrivateFont( CDTXMania.ConfigIni.strGetCardName( E楽器パート.DRUMS ), Color.White, Color.Transparent );
                this.txPlayerName = CDTXMania.tテクスチャの生成( bmpCardName, false );
                #endregion
                #region[ 難易度数値 ]
                string str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL.Drums) / 10f );
                str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL.Drums ) / 10.0f + ( CDTXMania.DTX.LEVELDEC.Drums != 0 ? CDTXMania.DTX.LEVELDEC.Drums / 100.0f : 0 ) );
                int[] nDigit = new int[]{ Convert.ToInt16( str[ 0 ].ToString() ), Convert.ToInt16( str[ 2 ].ToString() ),Convert.ToInt16( str[ 3 ].ToString() ) };

                if ( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする ? ( CDTXMania.DTX.bCLASSIC譜面である.Drums && CDTXMania.DTX.b強制的にXG譜面にする == false ) : false )
                {
                    str = string.Format( "{0:00}", CDTXMania.DTX.LEVEL.Drums );
                    nDigit = new int[]{ Convert.ToInt16( str[ 0 ].ToString() ), Convert.ToInt16( str[ 1 ].ToString() ) };
                }
                #endregion
                #region[ ジャケット画像 オプションアイコン ]
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PATH + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) ) {
                    this.iAlbum = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                } else {
                    this.iAlbum = CDTXMania.tテクスチャをImageで読み込む( path );
                }
                #endregion
                
                this.txスキルパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_SkillPanel.png" ) );
                if( File.Exists( path ) )
                {
                    this.txJacket = CDTXMania.tテクスチャの生成( path );
                }
                else
                {
                    this.txJacket = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                }

                this.txSongNamePlate = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums Songpanel.png" ) );
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_score numbersGD.png" ) );

                this.tx判定数数字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Ratenumber_s.png" ) );
                this.tx達成率数字_整数 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Ratenumber_l.png" ) );

                #region[ 難易度ラベル/パート表記 ]
                // 難易度ラベル/パート表記
                // TODO:パート表記のフォントが3D描画の都合で汚くなってしまう。ここでテクスチャを合成したほうがよさそうかも...
                Image diff = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty.png" ) );
                Image part = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Part.png" ) );
                Image number = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_number.png" ) );
                Bitmap bDiff = new Bitmap( 68, 68 );
                Graphics gDiff = Graphics.FromImage( bDiff );
                gDiff.PageUnit = GraphicsUnit.Pixel;
                gDiff.DrawImage( diff, 0, 0, new Rectangle(0, 68 * CDTXMania.stage選曲GITADORA.n確定された曲の難易度, 68, 68), GraphicsUnit.Pixel );
                gDiff.DrawImage( part, 0, 0, new Rectangle(0, 0, 68, 68), GraphicsUnit.Pixel );

                // 数値
                int num_x = 0;
                for ( int i = 0; i < str.Length; i++ )
                {
                    char ch = str[i];
                    
                    if (ch.Equals('.'))
                    {
                        gDiff.DrawImage( number, num_x - 1, 30, new Rectangle(240, 0, 8, 32), GraphicsUnit.Pixel );
                        num_x += 2;
                    }
                    else
                    {
                        int digit = int.Parse(str.Substring(i, 1));
                        gDiff.DrawImage( number, num_x, 30, new Rectangle(digit * 24, 0, 24, 32), GraphicsUnit.Pixel );
                        num_x += 21;
                    }
                }

                this.tx難易度ラベル = new CTexture( CDTXMania.app.Device, bDiff, CDTXMania.TextureFormat, false );
                //this.tx難易度ラベル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Difficulty.png" ) );
                //this.txパート = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Part.png" ) );

                gDiff?.Dispose();
                diff?.Dispose();
                part?.Dispose();
                number?.Dispose();
                bDiff?.Dispose();
                #endregion

                CDTXMania.t安全にDisposeする( ref bmpCardName );

                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if( !base.b活性化してない )
            {
                //テクスチャ5枚
                //イメージ 6枚(ジャケット画像はここで解放しない)
                //フォント 5個
                CDTXMania.tテクスチャの解放( ref this.txScore );
                CDTXMania.tテクスチャの解放( ref this.txSongNamePlate );
                CDTXMania.t安全にDisposeする( ref this.iRisky );
                CDTXMania.t安全にDisposeする( ref this.iDrumspeed );
                CDTXMania.t安全にDisposeする( ref this.pfPlayerNameFont );
                CDTXMania.t安全にDisposeする( ref this.pfSongTitleFont );
                CDTXMania.t安全にDisposeする( ref this.pfSongArtistFont );
                CDTXMania.tテクスチャの解放( ref this.txJacket );
                CDTXMania.tテクスチャの解放( ref this.txスキルパネル );

                CDTXMania.tテクスチャの解放( ref this.tx判定数数字 );
                CDTXMania.tテクスチャの解放( ref this.tx達成率数字_整数 );

                CDTXMania.tテクスチャの解放( ref this.txPlayerName );

                CDTXMania.tテクスチャの解放( ref this.tx難易度ラベル );
                CDTXMania.tテクスチャの解放( ref this.txパート );

                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if( !base.b活性化してない )
            {
                //if( true )
                //    return 0;
                if( this.b初めての進行描画 )
                {
#if DEBUG
                    fX = -465;
                    fY = -25;
                    fZ = 0;
                    rot = -38;
                    fScaleX = 0.6f;
                    fScaleY = 1f;
                    offset = 0;
#endif
                    this.b初めての進行描画 = false;
                }
                //if ( CDTXMania.ConfigIni.bShowMusicInfo )
                if( this.txNamePlate != null )
                {
                    //this.txNamePlate.t3D描画( CDTXMania.app.Device, identity );
                }
                if( this.txスキルパネル != null )
                {
                    Matrix matSkillPanel = Matrix.Identity;
                    matSkillPanel *= Matrix.Scaling( 0.6f, 1.0f, 1 );
                    matSkillPanel *= Matrix.RotationY( C変換.DegreeToRadian( -38 ) );
                    matSkillPanel *= Matrix.Translation( -465, -25, 0 );
                    this.txスキルパネル.t3D描画( CDTXMania.app.Device, matSkillPanel );
                }
                if( this.txPlayerName != null )
                {
                    Matrix matPlayerName = Matrix.Identity;
                    matPlayerName *= Matrix.Scaling( 0.6f, 1, 1 );
                    matPlayerName *= Matrix.RotationY( C変換.DegreeToRadian( -38 ) );
                    matPlayerName *= Matrix.Translation( -578 + ( this.txPlayerName.szテクスチャサイズ.Width / 2.0f ), 161, 0 );
                    this.txPlayerName.t3D描画( CDTXMania.app.Device, matPlayerName );
                }

#if DEBUG
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F1 ) )
                {
                    fX--;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F2 ) )
                {
                    fX++;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F3 ) )
                {
                    fY--;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F4 ) )
                {
                    fY++;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F6 ) )
                {
                    rot--;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F7 ) )
                {
                    rot++;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F8 ) )
                {
                    fScaleX -= 0.01f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F9 ) )
                {
                    fScaleX += 0.01f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F10 ) )
                {
                    fScaleX -= 0.1f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.F11 ) )
                {
                    fScaleX += 0.1f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D1 ) )
                {
                    fScaleY -= 0.01f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D2 ) )
                {
                    fScaleY += 0.01f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D3 ) )
                {
                    fScaleY -= 0.1f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D4 ) )
                {
                    fScaleY += 0.1f;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D5 ) )
                {
                    fOffsetX--;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D6 ) )
                {
                    fOffsetX++;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D7 ) )
                {
                    fX -= 10;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D8 ) )
                {
                    fX += 10;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D9 ) )
                {
                    fY -= 10;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.D0 ) )
                {
                    fY += 10;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.Q ) )
                {
                    fZ -= 1;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.W ) )
                {
                    fZ += 1;
                }
                if ( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.E ) )
                {
                    fZ -= 10;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.R ) )
                {
                    fZ += 10;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.T ) )
                {
                    fOffsetY--;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.Y ) )
                {
                    fOffsetY++;
                }
                if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.U ) )
                {
                    fOffsetZ--;
                }
                if(CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDXKey.I ))
                {
                    fOffsetZ++;
                }

                CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.白, "RotY:" + rot.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 16, C文字コンソール.Eフォント種別.白, "PanelX:" + fX.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 32, C文字コンソール.Eフォント種別.白, "PanelY:" + fY.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 48, C文字コンソール.Eフォント種別.白, "PanelZ:" + fZ.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 64, C文字コンソール.Eフォント種別.白, "ScaleX:" + fScaleX.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 80, C文字コンソール.Eフォント種別.白, "ScaleY:" + fScaleY.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 96, C文字コンソール.Eフォント種別.白, "OffsetX:" + fOffsetX.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 112, C文字コンソール.Eフォント種別.白, "OffsetY:" + fOffsetY.ToString() );
                CDTXMania.act文字コンソール.tPrint( 0, 128, C文字コンソール.Eフォント種別.白, "OffsetZ:" + fOffsetZ.ToString() );
#endif

                #region[ ステータスパネルの文字 ]
                if( this.tx判定数数字 != null )
                {
                    int nowtotal = CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Perfect +
                        CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Great +
                        CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Good +
                        CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Poor +
                        CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Miss;
                    
                    //string str = nowtotal == 0 ? "   0" : string.Format("{0,3:##0}%", (CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Perfect / (float)nowtotal) * 100.0f );
                    //for( int i = 0; i < 4; i++ )
                    //{
                    //    Rectangle rectangle;
                    //    char ch = str[i];
                    //    if (ch.Equals(' '))
                    //    {
                    //        rectangle = new Rectangle(0, 0, 0, 0);
                    //    }
                    //    else if( ch.Equals('%') )
                    //    {
                    //        rectangle = new Rectangle( 200, 0, 20, 26 );
                    //    }
                    //    else
                    //    {
                    //        int num3 = int.Parse(str.Substring(i, 1));
                    //        rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                    //    }
                    //    Matrix matScoreXG = Matrix.Identity;
                    //    matScoreXG *= Matrix.Scaling(0.45f, 0.8f, 1f);
                    //    matScoreXG *= Matrix.RotationY(C変換.DegreeToRadian(-40));
                    //    matScoreXG *= Matrix.Translation( -435 + (i * 6), 107, 28 + i * 6);
                    //    this.tx判定数数字.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                    //}

                    this.t判定数文字描画( -476, 107, CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Perfect );
                    this.t判定数文字描画( -476, 77, CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Great );
                    this.t判定数文字描画( -476, 47, CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Good );
                    this.t判定数文字描画( -476, 17, CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Poor );
                    this.t判定数文字描画( -476, -13, CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Miss );
                    this.t判定数文字描画( -476, -43, CDTXMania.stage演奏ドラム画面GITADORA.actCombo.n現在のコンボ数.Drums最高値 );

                    this.t判定率文字描画( -435, 107, nowtotal == 0 ? "  0%" : string.Format("{0,3:##0}%", (CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Perfect / (float)nowtotal) * 100.0f )  );
                    this.t判定率文字描画( -435, 77,  nowtotal == 0 ? "  0%" : string.Format("{0,3:##0}%", (CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Great / (float)nowtotal) * 100.0f )  );
                    this.t判定率文字描画( -435, 47,  nowtotal == 0 ? "  0%" : string.Format("{0,3:##0}%", (CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Good / (float)nowtotal) * 100.0f )  );
                    this.t判定率文字描画( -435, 17,  nowtotal == 0 ? "  0%" : string.Format("{0,3:##0}%", (CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Poor / (float)nowtotal) * 100.0f )  );
                    this.t判定率文字描画( -435, -13, nowtotal == 0 ? "  0%" : string.Format("{0,3:##0}%", (CDTXMania.stage演奏ドラム画面GITADORA.nヒット数_Auto含む.Drums.Miss / (float)nowtotal) * 100.0f )  );
                    this.t判定率文字描画( -435, -43, nowtotal == 0 ? "  0%" : string.Format("{0,3:##0}%", (CDTXMania.stage演奏ドラム画面GITADORA.actCombo.n現在のコンボ数.Drums最高値 / (float)nowtotal) * 100.0f )  );

                    // 達成率

                }
#endregion


                
                if ( this.txSongNamePlate != null )
                {
                    this.txSongNamePlate.t2D描画( CDTXMania.app.Device, 969, -2 );
                    if( this.txJacket != null )
                    {
                        this.txJacket.vc拡大縮小倍率 = new Vector3( 64.0f / this.txJacket.sz画像サイズ.Width, 64.0f / this.txJacket.sz画像サイズ.Height, 1.0f );
                        this.txJacket.t2D描画( CDTXMania.app.Device, 982, 10 );
                    }
                    if( this.txSongTitle != null )
                    {
                        this.txSongTitle.t2D描画( CDTXMania.app.Device, 1050, 28 );
                    }
                    if( this.txArtistName != null )
                    {
                        this.txArtistName.t2D描画( CDTXMania.app.Device, 1055, 52 );
                    }
                }

                #region[ 難易度ラベル ]
                if( /*this.txパート != null &&*/ this.tx難易度ラベル != null )
                {
                    Matrix matPart = Matrix.Identity;
                    matPart *= Matrix.Scaling( 0.6f, 1, 1 );
                    matPart *= Matrix.RotationY( C変換.DegreeToRadian( -38 ) );
                    matPart *= Matrix.Translation( -528, -110, 0 );
                    this.tx難易度ラベル.t3D描画( CDTXMania.app.Device, matPart );
                    //this.tx難易度ラベル.t3D描画( CDTXMania.app.Device, matPart, new Rectangle( 0, 68 * CDTXMania.stage選曲GITADORA.n確定された曲の難易度, 68, 68 ) );
                    //this.txパート.t3D描画( CDTXMania.app.Device, matPart, new Rectangle( 0, 0, 68, 68 ) ); // DrumsだけなのでRectangle.Xは0で固定
                }
                #endregion
                #region[ HSアイコン ]
                                //ハイスピはここで描画させる。
                                if( this.nCurrentDrumspeed != CDTXMania.ConfigIni.n譜面スクロール速度.Drums && this.iDrumspeed != null )
                                {
                                    Graphics gNamePlate = Graphics.FromImage( this.bNamePlate );
                                    this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;
                                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                                    {
                                        gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 209, 156, 42, 48 ), new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                                    }
                                    else if(CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                                    {
                                        gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 210, 141, 42, 48 ), new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                                    }
                                    gNamePlate.Dispose();
                                    this.txNamePlate.Dispose();
                                    this.txNamePlate = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );
                                }
                #endregion
                #region[ スコア表示 ]
                this.n表示スコア.Drums = (long)CDTXMania.stage演奏ドラム画面GITADORA.actScore.n現在表示中のスコア.Drums;
                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                {
                    //string str = this.n表示スコア.Drums.ToString("0000000000");
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    Rectangle rectangle;
                    //    char ch = str[i];
                    //    if (ch.Equals(' '))
                    //    {
                    //        rectangle = new Rectangle(0, 0, 32, 36);
                    //    }
                    //    else
                    //    {
                    //        int num3 = int.Parse(str.Substring(i, 1));
                    //        rectangle = new Rectangle((num3 * 36), 0, 36, 50);
                    //    }
                    //    if (this.txScore != null)
                    //    {
                    //        SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                    //        matScoreXG *= SlimDX.Matrix.Translation(-1370 + (i * 30), 50 + CDTXMania.stage演奏ドラム画面GITADORA.actScore.x位置[i].Drums, 0);
                    //        matScoreXG *= SlimDX.Matrix.Scaling(0.3f, 0.62f, 1f);
                    //        matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                    //        this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                    //    }
                    //}
                }
                else if( CDTXMania.ConfigIni.eSkillMode == ESkillType.XG )
                {
                    // 2019.1.12 kairera0467
                    // とりあえずXGスコア計算時のみ表示
                    if( this.txScore != null )
                    {
                        string str = string.Format("{0,7:######0}", this.n表示スコア.Drums);
                        for( int i = 0; i < 7; i++ )
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            if (ch.Equals(' '))
                            {
                                rectangle = new Rectangle(0, 0, 0, 0);
                            }
                            else
                            {
                                int num3 = int.Parse(str.Substring(i, 1));
                                rectangle = new Rectangle((num3 * 36), 0, 36, 50);
                            }
                            Matrix matScoreXG = Matrix.Identity;
                            matScoreXG *= Matrix.Scaling(0.6f, 1.1f, 1f);
                            matScoreXG *= Matrix.RotationY(C変換.DegreeToRadian(-40));
                            matScoreXG *= Matrix.Translation(-522 + (i * 14), 237 + CDTXMania.stage演奏ドラム画面GITADORA.actScore.x位置[i].Drums, i * 14);
                            this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                        }
                        Matrix matScoreTxt = Matrix.Identity;
                        matScoreTxt *= Matrix.Scaling(0.6f, 1.5f, 1f);
                        matScoreTxt *= Matrix.RotationY(C変換.DegreeToRadian(-40));
                        matScoreTxt *= Matrix.Translation(-494f, 282f, 0);
                        this.txScore.t3D描画(CDTXMania.app.Device, matScoreTxt, new Rectangle( 0, 50, 88, 28 ));
                    }
                }
                #endregion
            }
            return 0;

        }


        // その他

        #region [ private ]
        //-----------------
        private Bitmap b4font;
        private Bitmap bNamePlate;
        private Image iAlbum;
        private Image iDifficulty;
        private Image iDifficultyNumber;
        private Image iDrumspeed;
        private Image iNamePlate;
        private Image iPart;
        private Image iRisky;
        private int nCurrentDrumspeed;
        private string strGroupName;
        private string strPlayerName;
        private CTexture txNamePlate;
        private CTexture txScore;
        private CTexture txスキルパネル;
        private CPrivateFastFont pfSongArtistFont;
        private CPrivateFastFont pfSongTitleFont;
        private CPrivateFastFont pfPlayerNameFont;
        private CPrivateFastFont pfGroupNameFont;

        private CTexture txSongNamePlate;
        private CTexture txSongTitle;
        private CTexture txArtistName;
        private CTexture txJacket;
        private CTexture txPlayerName;
        private CTexture txTitleName;
        private CTexture tx判定数数字;
        private CTexture tx達成率数字_整数;

        private CTexture tx難易度ラベル;
        private CTexture txパート;

#if DEBUG
        private float fX;
        private float fY;
        private float fZ;
        private int rot;
        private int offset;
        private float fScaleX;
        private float fScaleY;
        private float fOffsetX;
        private float fOffsetY;
        private float fOffsetZ;
#endif
        //-----------------
        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            Bitmap bmp;
            bmp = this.pfSongTitleFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }
        private CTexture t指定された文字テクスチャを生成する_小( string str文字 )
        {
            Bitmap bmp;
            bmp = this.pfSongArtistFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }

        private void t判定数文字描画( float x, float y, int value )
        {
            string str = string.Format("{0,4:###0}", value );
            for( int i = 0; i < 4; i++ )
            {
                Rectangle rectangle;
                char ch = str[i];
                if (ch.Equals(' '))
                {
                    rectangle = new Rectangle(0, 0, 0, 0);
                }
                else
                {
                    int num3 = int.Parse(str.Substring(i, 1));
                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                }
                Matrix matScoreXG = Matrix.Identity;
                matScoreXG *= Matrix.Scaling(0.45f, 0.8f, 1f);
                matScoreXG *= Matrix.RotationY(C変換.DegreeToRadian(-40));
                matScoreXG *= Matrix.Translation( x + (i * 6), y, i * 6);
                this.tx判定数数字.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
            }
        }

        private void t判定率文字描画( float x, float y, string str )
        {
            for( int i = 0; i < 4; i++ )
            {
                Rectangle rectangle;
                char ch = str[i];
                if (ch.Equals(' '))
                {
                    rectangle = new Rectangle(0, 0, 0, 0);
                }
                else if( ch.Equals('%') )
                {
                    rectangle = new Rectangle( 200, 0, 20, 26 );
                }
                else
                {
                    int num3 = int.Parse(str.Substring(i, 1));
                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                }
                Matrix matScoreXG = Matrix.Identity;
                matScoreXG *= Matrix.Scaling(0.45f, 0.8f, 1f);
                matScoreXG *= Matrix.RotationY(C変換.DegreeToRadian(-40));
                matScoreXG *= Matrix.Translation( x + (i * 6), y, 28 + i * 6);
                this.tx判定数数字.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
            }
        }

        private void t達成率文字描画(float x, float y, string str)
        {
            // TODO: MAX表記
            for (int i = 0; i < 7; i++)
            {
                Rectangle rectangle;
                char ch = str[i];
                if (ch.Equals(' '))
                {
                    rectangle = new Rectangle(0, 0, 0, 0);
                }
                else if (ch.Equals('.'))
                {
                    rectangle = new Rectangle(0, 0, 0, 0);
                }
                else
                {
                    int num3 = int.Parse(str.Substring(i, 1));
                    rectangle = new Rectangle((num3 * 20), 0, 20, 26);
                }
                Matrix matScoreXG = Matrix.Identity;
                matScoreXG *= Matrix.Scaling(0.45f, 0.8f, 1f);
                matScoreXG *= Matrix.RotationY(C変換.DegreeToRadian(-40));
                matScoreXG *= Matrix.Translation(x + (i * 6), y, 28 + i * 6);
                this.tx判定数数字.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
            }
        }
        #endregion
    }
}