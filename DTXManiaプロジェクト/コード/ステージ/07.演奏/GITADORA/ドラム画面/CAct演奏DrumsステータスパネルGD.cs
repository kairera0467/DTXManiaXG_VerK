using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Diagnostics;
using SlimDX;
using FDK;

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
                string strSongName = "";
                string strArtistName = "";

                if ( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    strSongName = CDTXMania.stage選曲GITADORA.r確定された曲.strタイトル;
                else
                    strSongName = CDTXMania.DTX.TITLE;

                if ( string.IsNullOrEmpty( CDTXMania.DTX.ARTIST ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    strArtistName = CDTXMania.stage選曲GITADORA.r確定されたスコア.譜面情報.アーティスト名;
                else
                    strArtistName = CDTXMania.DTX.ARTIST;

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
                bmpCardName = this.pfPlayerNameFont.DrawPrivateFont( "GUEST", Color.White, Color.Transparent );
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
                this.txJacket = CDTXMania.tテクスチャの生成( path );

                this.txSongNamePlate = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums Songpanel.png" ) );

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

                CDTXMania.tテクスチャの解放( ref this.txPlayerName );

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
                    x = -524;
                    y = 167;
                    rot = 0;
                    fScaleX = 0;
                    fScaleY = 0;

                    this.b初めての進行描画 = false;
                }
                //if ( CDTXMania.ConfigIni.bShowMusicInfo )
                if( this.txNamePlate != null )
                {
                    //this.txNamePlate.t3D描画( CDTXMania.app.Device, identity );
                }
                if( this.txスキルパネル != null )
                {
                    SlimDX.Matrix matSkillPanel = SlimDX.Matrix.Identity;
                    matSkillPanel *= SlimDX.Matrix.Scaling( 0.6f, 1, 1 );
                    matSkillPanel *= SlimDX.Matrix.RotationY( C変換.DegreeToRadian( -38 ) );
                    matSkillPanel *= SlimDX.Matrix.Translation( -465, -25, 0 );
                    this.txスキルパネル.t3D描画( CDTXMania.app.Device, matSkillPanel );
                }
                if( this.txPlayerName != null )
                {
                    SlimDX.Matrix matPlayerName = SlimDX.Matrix.Identity;
                    matPlayerName *= SlimDX.Matrix.Scaling( 0.6f, 1, 1 );
                    matPlayerName *= SlimDX.Matrix.RotationY( C変換.DegreeToRadian( -38 ) );
                    matPlayerName *= SlimDX.Matrix.Translation( -527, 161, 0 );
                    this.txPlayerName.t3D描画( CDTXMania.app.Device, matPlayerName );
                }

                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F1 ) )
                //{
                //    x--;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F2 ) )
                //{
                //    x++;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F3 ) )
                //{
                //    y--;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F4 ) )
                //{
                //    y++;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F6 ) )
                //{
                //    rot--;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F7 ) )
                //{
                //    rot++;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F8 ) )
                //{
                //    fScaleX -= 0.01f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F9 ) )
                //{
                //    fScaleX += 0.01f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F10 ) )
                //{
                //    fScaleX -= 0.1f;
                //}
                //if( CDTXMania.Input管理.Keyboard.bキーが押された( (int)SlimDX.DirectInput.Key.F11 ) )
                //{
                //    fScaleX += 0.1f;
                //}


                //CDTXMania.act文字コンソール.tPrint( 0, 0, C文字コンソール.Eフォント種別.白, "RotY:" + rot.ToString() );
                //CDTXMania.act文字コンソール.tPrint( 0, 16, C文字コンソール.Eフォント種別.白, "PanelX:" + x.ToString() );
                //CDTXMania.act文字コンソール.tPrint( 0, 32, C文字コンソール.Eフォント種別.白, "PanelY:" + y.ToString() );
                //CDTXMania.act文字コンソール.tPrint( 0, 48, C文字コンソール.Eフォント種別.白, "ScaleX:" + fScaleX.ToString() );
                
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
                this.n表示スコア.Drums = (long)CDTXMania.stage演奏ドラム画面.actScore.n現在表示中のスコア.Drums;
                //if ( CDTXMania.ConfigIni.nSkillMode == 0 && CDTXMania.ConfigIni.bShowScore )
                if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                {
                    string str = this.n表示スコア.Drums.ToString("0000000000");
                    for (int i = 0; i < 10; i++)
                    {
                        Rectangle rectangle;
                        char ch = str[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(0, 0, 32, 36);
                        }
                        else
                        {
                            int num3 = int.Parse(str.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                        }
                        if (this.txScore != null)
                        {
                            SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                            //if ( !CDTXMania.ConfigIni.bShowMusicInfo )
                            //{
                            //    matScoreXG *= SlimDX.Matrix.Translation((-615f + (i * 21f)) / 0.7f, 280, 0);
                            //    matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 1f, 1f);
                            //}
                            //else if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-1220 + (i * 30), 120 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.34f, 0.62f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                            }
                            else if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-1370 + (i * 30), 50 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.3f, 0.62f, 1f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                            }
                            //this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                        }
                    }
                }
                //else if ( CDTXMania.ConfigIni.eSkillMode == ESkillType.XG && CDTXMania.ConfigIni.bShowScore )
                else if( CDTXMania.ConfigIni.eSkillMode == ESkillType.XG )
                {
                    string str = this.n表示スコア.Drums.ToString("0000000");
                    for( int i = 0; i < 7; i++ )
                    {
                        Rectangle rectangle;
                        char ch = str[i];
                        if (ch.Equals(' '))
                        {
                            rectangle = new Rectangle(0, 0, 32, 36);
                        }
                        else
                        {
                            int num3 = int.Parse(str.Substring(i, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 32), 0, 32, 36);
                            }
                        }
                        if( this.txScore != null )
                        {
                            SlimDX.Matrix matScoreXG = SlimDX.Matrix.Identity;
                            //if ( !CDTXMania.ConfigIni.bShowMusicInfo )
                            //{
                            //    matScoreXG *= SlimDX.Matrix.Translation(-610 + (i * 30), 280, 0);
                            //    matScoreXG *= SlimDX.Matrix.Scaling(1f, 1f, 1f);
                            //}
                            //else if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-870 + (i * 30), 114 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.47f, 0.65f, 1.0f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.60f);
                            }
                            else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                            {
                                matScoreXG *= SlimDX.Matrix.Translation(-974 + (i * 30), 50 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= SlimDX.Matrix.Scaling(0.42f, 0.62f, 1f);
                                matScoreXG *= SlimDX.Matrix.RotationY(-0.8f);
                                //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                            }
                            //this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
                        }
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

        private int x;
        private int y;
        private int rot;
        private float fScaleX;
        private float fScaleY;
        //-----------------
        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            Bitmap bmp;
            bmp = this.pfSongTitleFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White, true );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }
        private CTexture t指定された文字テクスチャを生成する_小( string str文字 )
        {
            Bitmap bmp;
            bmp = this.pfSongArtistFont.DrawPrivateFont( str文字, CPrivateFont.DrawMode.Edge, Color.Black, Color.White, Color.White, Color.White, true );
            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );
            bmp.Dispose();

            return tx文字テクスチャ;
        }

        #endregion
    }
}