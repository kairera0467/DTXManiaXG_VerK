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
    internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
    {
        //2016.02.21 kairera0467 Imageの解放方法を変更。これでファイルが無かった時の例外処理の書き方が楽になるはず。

        public override void On活性化()
        {
            //if( true )
            //    return;

            this.ftGroupFont = new Font( CDTXMania.ConfigIni.str選曲リストフォント, 16f, FontStyle.Regular, GraphicsUnit.Pixel );
            this.ftLevelFont = new Font( "Impact", 26f, FontStyle.Regular );
            this.ftDifficultyL = new Font( "Arial", 30f, FontStyle.Bold );
            this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );

            this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 20, FontStyle.Bold ); //2013.09.07.kairera0467 PrivateFontへの移行テスト。
            this.pfSongTitleFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 20, FontStyle.Regular );
            if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
            {
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && 
                    ( CDTXMania.DTX.bCLASSIC譜面である.Drums &&
                    CDTXMania.DTX.b強制的にXG譜面にする == false ) )
                {
                    this.ftDifficultyL = new Font( "Arial", 30f, FontStyle.Bold );
                    this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );
                }
                else
                {
                    this.ftDifficultyL = new Font( "Arial", 48f, FontStyle.Bold );
                    this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );
                }
            }
            //this.nDifficulty = CDTXMania.nSongDifficulty;
            //CDTXMania.strSongDifficulyName = this.stパネルマップ[ this.nDifficulty ].label;
            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if( !base.b活性化してない )
            {
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_score numbers.png" ) );
                this.iPart = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Part_XG.png" ) ); //2016.02.21 kairera0467 ダミーファイルを不要にするため、最初から読み込ませるよう変更。
                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                    this.iDifficulty = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_XG.png" ) );
                else
                    this.iDifficulty = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty.png" ) );

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
                    this.strPanelString = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strPanelString = CDTXMania.DTX.TITLE;

                this.strPlayerName = string.IsNullOrEmpty( CDTXMania.ConfigIni.strCardName[0] ) ? "GUEST" : CDTXMania.ConfigIni.strCardName[0];
                this.strGroupName = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGroupName[0] ) ? "" : CDTXMania.ConfigIni.strGroupName[0];
                gNamePlate.Dispose();

                this.bNamePlate = new Bitmap( 0x170, 0x103 );

                gNamePlate = Graphics.FromImage( this.bNamePlate );
                gNamePlate.DrawImage( this.iNamePlate, 0, 0, 0x170, 0x103 );

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

                Bitmap bmpCardName = new Bitmap( 1, 1 );

                if (CDTXMania.ConfigIni.nNameColor.Drums >= 11)
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

                string str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL.Drums) / 10f );
                str = string.Format( "{0:0.00}", ( (float)CDTXMania.DTX.LEVEL.Drums ) / 10.0f + ( CDTXMania.DTX.LEVELDEC.Drums != 0 ? CDTXMania.DTX.LEVELDEC.Drums / 100.0f : 0 ) );
                
                if ( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする ? ( CDTXMania.DTX.bCLASSIC譜面である.Drums && CDTXMania.DTX.b強制的にXG譜面にする == false ) : false )
                {
                    str = string.Format( "{0:00}", CDTXMania.DTX.LEVEL.Drums );
                }


                int width = (int)gNamePlate.MeasureString( "DTX" + "   ", this.ftLevelFont).Width;
                //数字の描画部分。その左側。
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする ? ( CDTXMania.DTX.bCLASSIC譜面である.Drums && CDTXMania.DTX.b強制的にXG譜面にする == false ) : false )
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                        gNamePlate.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 18f + 64, 164f);
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                        gNamePlate.DrawString(str.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 24f + 64, 154f);
                }
                else
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                        gNamePlate.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 12.0f + 64, 164f);
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                        gNamePlate.DrawString(str.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 14.0f + 64, 130f);
                }
                width += ( int )gNamePlate.MeasureString( str.Substring( 0, 1 ), this.ftDifficultyL ).Width;

                //数字の右。
                if( CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする ? ( !CDTXMania.DTX.bCLASSIC譜面である.Drums || CDTXMania.DTX.b強制的にXG譜面にする ) : true )
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                        gNamePlate.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, width, 176f);
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                        gNamePlate.DrawString(str.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 2f + width, 166f);
                }

                //ジャケット画像描画部
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PATH + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) )
                {
                    this.iAlbum = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                }
                else
                {
                    this.iAlbum = CDTXMania.tテクスチャをImageで読み込む( path );
                }

                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    gNamePlate.DrawImage( this.iAlbum,     new Rectangle( 6, 0x11, 0x45, 0x4b ),  new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                    if( this.iDrumspeed != null )
                        gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 209, 156, 42, 48 ),     new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                    if( this.iRisky != null )
                        gNamePlate.DrawImage( this.iRisky,     new Rectangle( 260, 156, 42, 48 ),     new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                }
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                {
                    gNamePlate.DrawImage( this.iAlbum,     new Rectangle( 6, 9, 0x45, 0x4b ), new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 210, 141, 42, 48 ), new Rectangle( 0, ( ( this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed ) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                    gNamePlate.DrawImage( this.iRisky,     new Rectangle( 260, 141, 42, 48 ), new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                }
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
                CDTXMania.t安全にDisposeする( ref this.iPart );

                //ここで使用したフォント3つはここで開放。
                CDTXMania.t安全にDisposeする( ref this.ftLevelFont );
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

                CDTXMania.t安全にDisposeする( ref this.ftDifficultyS );
                CDTXMania.t安全にDisposeする( ref this.ftDifficultyL );
                CDTXMania.t安全にDisposeする( ref this.ftLevelFont );
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

                SlimDX.Matrix identity = SlimDX.Matrix.Identity;
                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                {
                    identity *= SlimDX.Matrix.Translation( -1135, 150, 0 );
                    identity *= SlimDX.Matrix.Scaling( 0.338f, 0.62f, 1f );
                    identity *= SlimDX.Matrix.RotationY( -0.8f );
                }
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    identity *= SlimDX.Matrix.Translation( -991, 225, 0 );
                    identity *= SlimDX.Matrix.Scaling( 0.385f, 0.61f, 1.0f );
                    identity *= SlimDX.Matrix.RotationY( -0.60f );
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
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private Image iAlbum;
        private Image iDrumspeed;
        private Image iRisky;
        private Image iNamePlate;
        private Image iDifficulty;
        private Image iPart;
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