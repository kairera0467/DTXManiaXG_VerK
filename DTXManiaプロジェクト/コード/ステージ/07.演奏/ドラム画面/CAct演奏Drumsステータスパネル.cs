﻿using System;
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
namespace DTXMania
{
    internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
    {
        //2016.02.21 kairera0467 Imageの解放方法を変更。これでファイルが無かった時の例外処理の書き方が楽になるはず。

        public override void On活性化()
        {
            //if( true )
            //    return;

            this.ftGroupFont = new Font( CDTXMania.ConfigIni.str選曲リストフォント, 16f, FontStyle.Regular, GraphicsUnit.Pixel );

            this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 20, FontStyle.Bold ); //2013.09.07.kairera0467 PrivateFontへの移行テスト。
            this.pfSongTitleFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 20, FontStyle.Regular );
            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if( !base.b活性化してない )
            {
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_score numbers.png" ) );
                this.iPart = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Part_XG.png" ) ); //2016.02.21 kairera0467 ダミーファイルを不要にするため、最初から読み込ませるよう変更。
                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B ) {
                    this.iDifficulty = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_XG.png" ) );
                    this.iDifficultyNumber = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_number_XG.png" ) );
                } else {
                    this.iDifficulty = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty.png" ) );
                    this.iDifficultyNumber = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_number_XG2.png" ) );
                }

                #region[ ネームプレート本体 ]
                this.iNamePlate = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_nameplate.png" ) );
                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                        ( CDTXMania.DTX.bCLASSIC譜面である.Drums && CDTXMania.DTX.b強制的にXG譜面にする == false ) )
                    {
                        if( this.iNamePlate != null )
                            this.iNamePlate = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_nameplate_cls.png" ) );
                    }
                }
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                {
                    this.iNamePlate = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_nameplate_XG.png" ) );
                    if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                        ( CDTXMania.DTX.bCLASSIC譜面である.Drums && CDTXMania.DTX.b強制的にXG譜面にする == false ) )
                    {
                        if( this.iNamePlate != null && File.Exists( CSkin.Path( @"Graphics\7_nameplate_XG_ccls.png" ) ) )
                            this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_nameplate_XG_cls.png" ) );
                    }
                }
                #endregion
                this.iDrumspeed = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_panel_icons.jpg" ) );
                this.iRisky = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_panel_icons2.jpg" ) );

                this.b4font = new Bitmap( 1, 1 );
                Graphics gNamePlate = Graphics.FromImage( this.b4font );
                gNamePlate.PageUnit = GraphicsUnit.Pixel;

                if ( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strPanelString = CDTXMania.bXGRelease ? CDTXMania.stage選曲XG.r現在選択中の曲.strタイトル : CDTXMania.stage選曲GITADORA.r現在選択中の曲.strタイトル;
                else
                    this.strPanelString = CDTXMania.DTX.TITLE;

                this.strPlayerName = CDTXMania.ConfigIni.strGetCardName( E楽器パート.DRUMS );
                this.strGroupName = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGetGroupName( E楽器パート.DRUMS ) ) ? "" : CDTXMania.ConfigIni.strGetGroupName( E楽器パート.DRUMS );
                gNamePlate.Dispose();

                this.bNamePlate = new Bitmap( 368, 304 );

                gNamePlate = Graphics.FromImage( this.bNamePlate );
                gNamePlate.DrawImage( this.iNamePlate, 0, 0, 368, 304 );

                if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A)
                {
                    Rectangle Rect1 = new Rectangle(7, 167, 150, 38);
                    Rectangle Rect2 = new Rectangle( base.rectDiffPanelPoint.X, base.rectDiffPanelPoint.Y, 150, 38);
                    gNamePlate.DrawImage(this.iDifficulty, Rect1, Rect2, GraphicsUnit.Pixel);
                }
                else if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                {
                    Rectangle Rect1 = new Rectangle(7, 138, 194, 60);
                    Rectangle Rect2 = new Rectangle( base.rectDiffPanelPoint.X, base.rectDiffPanelPoint.Y, 194, 60);
                    gNamePlate.DrawImage(this.iDifficulty, Rect1, Rect2, GraphicsUnit.Pixel);
                    if (this.iPart != null)
                    {
                        Rectangle RectP = new Rectangle(0, 0, 194, 60);
                        gNamePlate.DrawImage(this.iPart, 7, 138, RectP, GraphicsUnit.Pixel);
                    }
                }

                this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;

                #region[ ネームカラー ]
                //--------------------
                Color clNameColor = Color.White;
                Color clNameColorLower = Color.White;
                switch( CDTXMania.ConfigIni.nNameColor[ 0 ] )
                {
                    default:
                        clNameColor = Color.White;
                        clNameColorLower = Color.White;
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
                        clNameColor = Color.FromArgb( 255, 232, 182, 149 );
                        clNameColorLower = Color.FromArgb( 255, 122, 69, 26 );
                        break;
                    case 8:
                        clNameColor = Color.FromArgb( 246, 245, 255 );
                        clNameColorLower = Color.FromArgb( 125, 128, 137 );
                        break;
                    case 9:
                        clNameColor = Color.FromArgb( 255, 238, 196, 85 );
                        clNameColorLower = Color.FromArgb( 255, 255, 241, 200 );
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

                Bitmap bmpCardName = new Bitmap( 1, 1 );

                if (CDTXMania.ConfigIni.nNameColor.Drums >= 7)
                {
                    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent, clNameColor, clNameColorLower);
                }
                else
                {
                    bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName, clNameColor, Color.Transparent);
                }
                //--------------------
                #endregion
                #region[ 名前、グループ名 ]
                //2013.09.07.kairera0467 できればこの辺のメンテナンスが楽にできるよう、コードを簡略にしたいが・・・・
                Bitmap bmpSongTitle = new Bitmap( 1, 1 );
                #region[ 曲名 ]
                if( File.Exists( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" ) )
                {
                    Image imgCustomSongNameTexture;
                    imgCustomSongNameTexture = CDTXMania.tテクスチャをImageで読み込む( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" );
                    //2014.08.11 kairera0467 XG1とXG2では座標が異なるため、変数を使って対処する。
                    int x = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 78 : 80;
                    int y = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 59 : 50;
                    gNamePlate.DrawImage( imgCustomSongNameTexture, x, y, 238, 30 );
                }
                else
                {
                    //PrivateFontのテスト
                    Bitmap bmpSongName = pfSongTitleFont.DrawPrivateFont( this.strPanelString, Color.White );
                    int y = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 60 : 52;
                    if( ( bmpSongName.Size.Width / 1.25f ) > 240 )
                    {
                        gNamePlate.DrawImage( bmpSongName, 80f, y, ( bmpSongName.Size.Width / 1.25f ) * ( 240.0f / ( bmpSongName.Size.Width / 1.25f ) ), bmpSongName.Size.Height );
                    }
                    else
                    {
                        gNamePlate.DrawImage( bmpSongName, 80f, y, ( bmpSongName.Size.Width / 1.25f ), bmpSongName.Size.Height );
                    }
                    CDTXMania.t安全にDisposeする( ref bmpSongName );
                }
                #endregion

                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    gNamePlate.DrawImage( bmpCardName, 42f, 126f );
                    gNamePlate.DrawString( this.strGroupName, this.ftGroupFont, Brushes.White, 54f, 105f );
                }
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                {
                    gNamePlate.DrawImage( bmpCardName, 46f, 92f );
                }
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
                //左
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする ? ( CDTXMania.DTX.bCLASSIC譜面である.Drums && CDTXMania.DTX.b強制的にXG譜面にする == false ) : false )
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ){
                        gNamePlate.DrawImage( this.iDifficultyNumber, 94, 170, new Rectangle( nDigit[ 0 ] * 22, 0, 22, 32 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iDifficultyNumber, 116, 170, new Rectangle( nDigit[ 1 ] * 22, 0, 22, 32 ), GraphicsUnit.Pixel );
                    } else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B ) {
                        gNamePlate.DrawImage( this.iDifficultyNumber, new Rectangle( 88, 148, 25, 42), new Rectangle( nDigit[ 0 ] * 34, 0, 34, 48 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iDifficultyNumber, new Rectangle( 113, 148, 25, 42), new Rectangle( nDigit[ 1 ] * 34, 0, 34, 48 ), GraphicsUnit.Pixel );
                    }
                }
                else
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ) {
                        gNamePlate.DrawImage( this.iDifficultyNumber, 83, 170, new Rectangle( nDigit[ 0 ] * 22, 0, 22, 32 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iDifficultyNumber, 106, 197, new Rectangle( 0, 54, 4, 4 ), GraphicsUnit.Pixel );
                    } else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B ) {
                        gNamePlate.DrawImage( this.iDifficultyNumber, 94, 145, new Rectangle( nDigit[ 0 ] * 34, 0, 34, 48 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iDifficultyNumber, 130, 185, new Rectangle( 0, 70, 4, 4 ), GraphicsUnit.Pixel );
                    }
                }

                //右
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする ? ( !CDTXMania.DTX.bCLASSIC譜面である.Drums || CDTXMania.DTX.b強制的にXG譜面にする ) : true )
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ) {
                        gNamePlate.DrawImage( this.iDifficultyNumber, 112, 180, new Rectangle( 16 * nDigit[ 1 ], 32, 16, 22 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iDifficultyNumber, 128, 180, new Rectangle( 16 * nDigit[ 2 ], 32, 16, 22 ), GraphicsUnit.Pixel );
                    } else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B ) {
                        gNamePlate.DrawImage( this.iDifficultyNumber, 136, 170, new Rectangle( 16 * nDigit[ 1 ], 48, 16, 22 ), GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( this.iDifficultyNumber, 152, 170, new Rectangle( 16 * nDigit[ 2 ], 48, 16, 22 ), GraphicsUnit.Pixel );
                    }
                }
                #endregion
                #region[ ジャケット画像 オプションアイコン ]
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PATH + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) ) {
                    this.iAlbum = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                } else {
                    this.iAlbum = CDTXMania.tテクスチャをImageで読み込む( path );
                }

                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    gNamePlate.DrawImage( this.iAlbum,     new Rectangle( 6, 0x11, 0x45, 0x4b ),  new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                    if( this.iDrumspeed != null )
                        gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 209, 156, 42, 48 ),     new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                    if( this.iRisky != null )
                        gNamePlate.DrawImage( this.iRisky,     new Rectangle( 258, 156, 42, 48 ),     new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                }
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                {
                    gNamePlate.DrawImage( this.iAlbum,     new Rectangle( 6, 9, 0x45, 0x4b ), new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 210, 141, 42, 48 ), new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iRisky,     new Rectangle( 258, 141, 42, 48 ), new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                }
                #endregion

                CDTXMania.t安全にDisposeする( ref gNamePlate );
                CDTXMania.t安全にDisposeする( ref bmpCardName );
                CDTXMania.t安全にDisposeする( ref bmpSongTitle );
                CDTXMania.t安全にDisposeする( ref b4font );
                //テクスチャ変換
                this.txNamePlate = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );

                //ハイスピ画像の描画で使うので、ここでbNamePlateをDisposeしてはいけない。
                CDTXMania.t安全にDisposeする( ref this.iAlbum );
                CDTXMania.t安全にDisposeする( ref this.iNamePlate );
                CDTXMania.t安全にDisposeする( ref this.iDifficulty );
                CDTXMania.t安全にDisposeする( ref this.iDifficultyNumber );
                CDTXMania.t安全にDisposeする( ref this.iPart );

                //ここで使用したフォント3つはここで開放。
                CDTXMania.t安全にDisposeする( ref this.pfNameFont );
                CDTXMania.t安全にDisposeする( ref this.pfSongTitleFont );

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
                CDTXMania.tテクスチャの解放( ref this.txNamePlate );
                CDTXMania.tテクスチャの解放( ref this.txScore );
                CDTXMania.t安全にDisposeする( ref this.iRisky );
                CDTXMania.t安全にDisposeする( ref this.iDrumspeed );
                CDTXMania.t安全にDisposeする( ref this.pfNameFont );
                CDTXMania.t安全にDisposeする( ref this.pfSongTitleFont );

                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if( !base.b活性化してない )
            {
                //if( true )
                //    return 0;

                Matrix identity = Matrix.Identity;
                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                {
                    identity *= Matrix.Translation( -1135, 150, 0 );
                    identity *= Matrix.Scaling( 0.338f, 0.62f, 1f );
                    identity *= Matrix.RotationY( -0.8f );
                }
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    identity *= Matrix.Translation( -991, 225, 0 );
                    identity *= Matrix.Scaling( 0.385f, 0.61f, 1.0f );
                    identity *= Matrix.RotationY( -0.60f );
                }

                //if ( CDTXMania.ConfigIni.bShowMusicInfo )
                if( this.txNamePlate != null )
                {
                    this.txNamePlate.t3D描画( CDTXMania.app.Device, identity );
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
                            Matrix matScoreXG = Matrix.Identity;
                            //if ( !CDTXMania.ConfigIni.bShowMusicInfo )
                            //{
                            //    matScoreXG *= SlimDX.Matrix.Translation((-615f + (i * 21f)) / 0.7f, 280, 0);
                            //    matScoreXG *= SlimDX.Matrix.Scaling(0.7f, 1f, 1f);
                            //}
                            //else if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            {
                                matScoreXG *= Matrix.Translation(-1220 + (i * 30), 120 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= Matrix.Scaling(0.34f, 0.62f, 1.0f);
                                matScoreXG *= Matrix.RotationY(-0.60f);
                            }
                            else if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                            {
                                matScoreXG *= Matrix.Translation(-1370 + (i * 30), 50 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= Matrix.Scaling(0.3f, 0.62f, 1f);
                                matScoreXG *= Matrix.RotationY(-0.8f);
                                //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                            }
                            this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
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
                            Matrix matScoreXG = Matrix.Identity;
                            //if ( !CDTXMania.ConfigIni.bShowMusicInfo )
                            //{
                            //    matScoreXG *= SlimDX.Matrix.Translation(-610 + (i * 30), 280, 0);
                            //    matScoreXG *= SlimDX.Matrix.Scaling(1f, 1f, 1f);
                            //}
                            //else if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            if ( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                            {
                                matScoreXG *= Matrix.Translation(-870 + (i * 30), 114 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= Matrix.Scaling(0.47f, 0.65f, 1.0f);
                                matScoreXG *= Matrix.RotationY(-0.60f);
                            }
                            else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                            {
                                matScoreXG *= Matrix.Translation(-974 + (i * 30), 50 + CDTXMania.stage演奏ドラム画面.actScore.x位置[i].Drums, 0);
                                matScoreXG *= Matrix.Scaling(0.42f, 0.62f, 1f);
                                matScoreXG *= Matrix.RotationY(-0.8f);
                                //matScoreXG *= SlimDX.Matrix.RotationZ(-0.01f);
                            }
                            this.txScore.t3D描画(CDTXMania.app.Device, matScoreXG, rectangle);
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
        private Font ftGroupFont;
        private Image iAlbum;
        private Image iDifficulty;
        private Image iDifficultyNumber;
        private Image iDrumspeed;
        private Image iNamePlate;
        private Image iPart;
        private Image iRisky;
        private int nCurrentDrumspeed;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private CTexture txNamePlate;
        private CTexture txScore;
        private CPrivateFastFont pfNameFont;
        private CPrivateFastFont pfSongTitleFont;
        private CPrivateFastFont pfPlayerNameFont;
        private CPrivateFastFont pfGroupNameFont;
        //-----------------
        #endregion
    }
}