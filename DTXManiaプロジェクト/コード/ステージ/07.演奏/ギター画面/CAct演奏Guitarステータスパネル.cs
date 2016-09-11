using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
	{
		// コンストラクタ
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.strGroupName = new string[ 2 ];
                this.strPlayerName = new string[ 2 ];
                this.txパネル = new CTexture[ 2 ];
                this.pfSongTitleFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 20, FontStyle.Regular );
                this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 24, FontStyle.Bold );
                this.ftGroupFont = new Font( "ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel );
                this.ftLevelFont = new Font( "Impact", 26f, FontStyle.Regular );
                this.ftDifficultyL = new Font( "Arial", 30f, FontStyle.Bold );
                this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );
                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                    this.ftDifficultyL = new Font( "Arial", 46f, FontStyle.Bold );

                #region[ 曲名パネル ]
                int[] nSongPanel = new int[] { 250, 112 };
                string strSongpanelFile = CSkin.Path( @"Graphics\7_Guitar Songpanel.png" );
                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                    nSongPanel = new int[] { 250, 112 };
                else
                {
                    nSongPanel = new int[] { 520, 70 };
                    strSongpanelFile = CSkin.Path( @"Graphics\7_Guitar Songpanel_XG.png" );
                }

                Bitmap bSongPanel = new Bitmap( nSongPanel[ 0 ], nSongPanel[ 1 ] );
                Graphics gSongNamePlate = Graphics.FromImage( bSongPanel );
                gSongNamePlate.PageUnit = GraphicsUnit.Pixel;

                gSongNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( strSongpanelFile ), 0, 0, nSongPanel[ 0 ], nSongPanel[ 1 ] );

                #region[ 曲名 ]
                if( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strTitle = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strTitle = CDTXMania.DTX.TITLE;

                if( File.Exists( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" ) )
                {
                    Image imgCustomSongNameTexture;
                    imgCustomSongNameTexture = CDTXMania.tテクスチャをImageで読み込む( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" );
                    //2014.08.11 kairera0467 XG1とXG2では座標が異なるため、変数を使って対処する。
                    int x = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 16 : 250;
                    int y = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 70 : 18;
                    gSongNamePlate.DrawImage( imgCustomSongNameTexture, x, y, 238, 30 );
                }
                else
                {
                    //PrivateFontのテスト
                    Bitmap bmpSongName = pfSongTitleFont.DrawPrivateFont( this.strTitle, Color.White );
                    int x = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 80 : 250;
                    int y = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 60 : 18;
                    if( ( bmpSongName.Size.Width / 1.25f ) > 240 )
                    {
                        gSongNamePlate.DrawImage( bmpSongName, x, y, ( bmpSongName.Size.Width / 1.25f ) * ( 240.0f / ( bmpSongName.Size.Width / 1.25f ) ), bmpSongName.Size.Height );
                    }
                    else
                    {
                        gSongNamePlate.DrawImage( bmpSongName, x, y, ( bmpSongName.Size.Width / 1.25f ), bmpSongName.Size.Height );
                    }
                    CDTXMania.t安全にDisposeする( ref bmpSongName );
                }
                #endregion
                #region[ アルバム ]
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PATH + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) )
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                        gSongNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_preimage default.png" ) ), 20, 10, 51, 51 );
                    else
                        gSongNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( path ), 172, 2, 64, 64 );
                }
                else
                {
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                        gSongNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\5_preimage default.png" ) ), 20, 10, 51, 51 );
                    else
                        gSongNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( path ), 172, 2, 64, 64 );
                }

                #endregion

                this.tx曲名パネル = new CTexture( CDTXMania.app.Device, bSongPanel, CDTXMania.TextureFormat, false );
                CDTXMania.t安全にDisposeする( ref bSongPanel );
                CDTXMania.t安全にDisposeする( ref gSongNamePlate );
                #endregion

                #region[ ステータスパネル ]
                for( int i = 0; i < 2; i++ )
                {
                    //XG2でのパネルの大きさは250x266、XG1は250x300(めやす)
                    Bitmap bNamePlate = new Bitmap( 250, 266 );
                    Bitmap bLevel = new Bitmap( 1, 1 );
                    Graphics gNamePlate = Graphics.FromImage( bNamePlate );
                    gNamePlate.PageUnit = GraphicsUnit.Pixel;

                    #region[ カードネーム ]
                    this.strPlayerName[ 0 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strCardName[ 1 ] ) ? "GUEST" : CDTXMania.ConfigIni.strCardName[ 1 ];
                    this.strPlayerName[ 1 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strCardName[ 2 ] ) ? "GUEST" : CDTXMania.ConfigIni.strCardName[ 2 ];
                    this.strGroupName[ 0 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGroupName[ 1 ] ) ? "" : CDTXMania.ConfigIni.strGroupName[ 1 ];
                    this.strGroupName[ 1 ] = string.IsNullOrEmpty( CDTXMania.ConfigIni.strGroupName[ 2 ] ) ? "" : CDTXMania.ConfigIni.strGroupName[ 2 ];

                    #endregion
                    #region[ ネームカラー ]
                    //-------------------
                    Color clNameColor;
                    Color clNameColorLower;

                    //初期化
                    clNameColor = new Color();
                    clNameColorLower = new Color();

                    switch( CDTXMania.ConfigIni.nNameColor[ i + 1 ] ) // 0はDrumなので+1する。
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
                            clNameColor = Color.FromArgb(0, 255, 33);
                            clNameColorLower = Color.White;
                            break;
                        case 14:
                            clNameColor = Color.FromArgb(0, 38, 255);
                            clNameColorLower = Color.White;
                            break;
                        case 15:
                            clNameColor = Color.FromArgb(72, 0, 255);
                            clNameColorLower = Color.White;
                            break;
                        case 16:
                            clNameColor = Color.FromArgb(255, 255, 0, 0);
                            clNameColorLower = Color.White;
                            break;
                        case 17:
                            clNameColor = Color.FromArgb(255, 232, 182, 149);
                            clNameColorLower = Color.FromArgb(255, 122, 69, 26);
                            break;
                        case 18:
                            clNameColor = Color.FromArgb(246, 245, 255);
                            clNameColorLower = Color.FromArgb(125, 128, 137);
                            break;
                        case 19:
                            clNameColor = Color.FromArgb(255, 238, 196, 85);
                            clNameColorLower = Color.FromArgb(255, 255, 241, 200);
                            break;
                    }

                    Bitmap bmpCardName = new Bitmap(1, 1);
                    if( CDTXMania.ConfigIni.nNameColor[ 1 ] >= 11 )
                        bmpCardName = this.pfNameFont.DrawPrivateFont(this.strPlayerName[ i ], clNameColor, Color.Transparent, clNameColor, clNameColorLower );
                    else
                        bmpCardName = this.pfNameFont.DrawPrivateFont( this.strPlayerName[ i ], clNameColor, Color.Transparent);
                    //--------------------
                    #endregion
                    #region[ ネームプレート、難易度、カードネーム ]
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                    {
                        Rectangle Rect1 = new Rectangle( 7, 91, 234, 38 );
                        Rectangle RectDifficulty = new Rectangle( base.rectDiffPanelPoint.X, base.rectDiffPanelPoint.Y, 234, 38 );

                        gNamePlate = Graphics.FromImage( bNamePlate );
                        gNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_nameplate_Guitar.png" ) ), 0, 0, 250, 266 );
                        gNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty.png" ) ), Rect1, RectDifficulty, GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( bmpCardName, 44f, 46f );
                        gNamePlate.DrawString( this.strGroupName[ i ], this.ftGroupFont, Brushes.White, 16f, 30f );
                    }
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                    {
                        Rectangle Rect1 = new Rectangle( 6, 50, 234, 60 );
                        Rectangle RectDifficulty = new Rectangle( base.rectDiffPanelPoint.X, base.rectDiffPanelPoint.Y, 234, 60 );

                        gNamePlate = Graphics.FromImage( bNamePlate );
                        gNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_nameplate_Guitar_XG.png" ) ), 0, 0, 250, 297 );
                        gNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_XG.png" ) ), Rect1, RectDifficulty, GraphicsUnit.Pixel );
                        gNamePlate.DrawImage( bmpCardName, 45f, 0f );
                    }
                    #endregion
                    #region[ レベル ]
                    //2016.04.04 kairera0467
                    //以前まではネームプレート外に描画していたが、今回からネームプレートに埋め込む形で描画していく。
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                        bLevel = new Bitmap( 250, 266 );
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                        bLevel = new Bitmap( 250, 297 );

                    string strLevel = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL[ i + 1 ]) / 10f);
                    strLevel = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL[ i + 1 ]) / 10.0f + (CDTXMania.DTX.LEVELDEC[ i + 1 ] != 0 ? CDTXMania.DTX.LEVELDEC[ i + 1 ] / 100.0f : 0));

                    if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                        (CDTXMania.DTX.bCLASSIC譜面である.Guitar == false) &&
                        (CDTXMania.DTX.b強制的にXG譜面にする == false))
                    {
                        strLevel = string.Format("{0:00}", CDTXMania.DTX.LEVEL[ i + 1 ]);
                    }

                    int widthG = (int)gNamePlate.MeasureString(this.stパネルマップ[ 0 ].label.Substring(0, 3) + "   ", this.ftLevelFont).Width;
                    //数字の描画部分。その左側。
                    if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                        (CDTXMania.DTX.bCLASSIC譜面である.Guitar == false) &&
                        (CDTXMania.DTX.b強制的にXG譜面にする == false))
                    {
                        if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A)
                            gNamePlate.DrawString(strLevel.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 91f + widthG, 89f);
                        else if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                            gNamePlate.DrawString(strLevel.Substring(0, 2), this.ftDifficultyL, Brushes.Black, 44f + widthG, 47f);
                    }
                    else
                    {
                        if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A)
                            gNamePlate.DrawString(strLevel.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 91f + widthG, 89f);
                        else if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                            gNamePlate.DrawString(strLevel.Substring(0, 1), this.ftDifficultyL, Brushes.Black, 44f + widthG, 47f);
                    }
                    widthG += (int)gNamePlate.MeasureString(strLevel.Substring(0, 1), this.ftDifficultyL).Width;

                    //数字の右。
                    if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする &&
                        (CDTXMania.DTX.bCLASSIC譜面である.Guitar == false) &&
                        (CDTXMania.DTX.b強制的にXG譜面にする == false))
                    {
                    }
                    else
                    {
                        if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A)
                            gNamePlate.DrawString(strLevel.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 88f + widthG, 100f);
                        else if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                            gNamePlate.DrawString(strLevel.Substring(1, 3), this.ftDifficultyS, Brushes.Black, 44f + widthG, 78f);
                    }
                    #endregion


                    this.txパネル[ i ] = new CTexture( CDTXMania.app.Device, bNamePlate, CDTXMania.TextureFormat, false );
                    CDTXMania.t安全にDisposeする( ref bNamePlate );
                    CDTXMania.t安全にDisposeする( ref gNamePlate );
                }
                #endregion

                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Score numbers_Guitar.png" ) );
                //this.txSpeed = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_panel_icons.jpg" ) );
                //this.txRisky = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_panel_icons2.jpg" ) );

                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                    this.txPart = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Part.png" ) );
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                    this.txPart = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Part_XG.png" ) );

                base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル[ 0 ] );
				CDTXMania.tテクスチャの解放( ref this.txパネル[ 1 ] );
                CDTXMania.tテクスチャの解放( ref this.tx曲名パネル );
                CDTXMania.t安全にDisposeする( ref this.pfSongTitleFont );
                CDTXMania.tテクスチャの解放( ref this.txScore );
                CDTXMania.tテクスチャの解放( ref this.txPart );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                bool bセッション譜面である = CDTXMania.DTX.bチップがある.Guitar && CDTXMania.DTX.bチップがある.Bass;
                bool[] bShowPanel = new bool[] { false, false };
                bool[] bUseGraph = new bool[] { false, false };
                if( CDTXMania.ConfigIni.bGraph.Guitar )
                {
                    if( CDTXMania.DTX.bチップがある.Guitar ) bUseGraph[ 0 ] = true;
                    else bUseGraph[ 1 ] = true;
                }
                else if( CDTXMania.ConfigIni.bGraph.Bass )
                {
                    if( CDTXMania.DTX.bチップがある.Bass ) bUseGraph[ 1 ] = true;
                    else bUseGraph[ 0 ] = true;
                }

                #region[ 曲名パネル ]
                if( this.tx曲名パネル != null )
                {
                    int nSongNamePanelX = 515;
                    int nSongNamePanelY = 521;
                    if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                    {
                        if( CDTXMania.ConfigIni.bGraph.Drums )
                            nSongNamePanelY = 293;

                        this.tx曲名パネル.t2D描画( CDTXMania.app.Device, nSongNamePanelX, nSongNamePanelY );
                    }
                    else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                    {
                        nSongNamePanelX = 381;
                        nSongNamePanelY = 317;

                        if( bUseGraph[ 0 ] )
                            nSongNamePanelX = 650;
                        else if( bUseGraph[ 1 ] )
                            nSongNamePanelX = 102;

                        if( !bセッション譜面である )
                            this.tx曲名パネル.t2D描画( CDTXMania.app.Device, nSongNamePanelX, nSongNamePanelY );
                    }
                }
                #endregion
                #region[ ステータスパネル ]
                int[] nNamePlateX = new int[] { 337, 694 };
                int[] nNamePlateY = new int[] { 211, 211 };
                if( CDTXMania.DTX.bチップがある.Guitar )
                    bShowPanel[ 0 ] = true;
                if( CDTXMania.DTX.bチップがある.Bass )
                    bShowPanel[ 1 ] = true;

                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    if( ( bUseGraph[ 0 ] || bUseGraph[ 1 ] ) && bセッション譜面である )
                        nNamePlateY = new int[] { 45, 424 };
                }
                else if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B )
                {
                    nNamePlateY = new int[] { 392, 392 };
                    if( ( bUseGraph[ 0 ] || bUseGraph[ 1 ] ) && bセッション譜面である )
                        nNamePlateY = new int[] { 45, 424 };
                }
                
                if( CDTXMania.ConfigIni.bGraph[ 1 ] || CDTXMania.ConfigIni.bGraph[ 2 ] )
                {
                    if( bUseGraph[ 0 ] )
                    {
                        nNamePlateX = new int[] { 650, 650 };
                    }
                    else if( bUseGraph[ 1 ] )
                    {
                        nNamePlateX = new int[] { 370, 370 };
                    }
                }

                if( this.txパネル[ 0 ] != null && bShowPanel[ 0 ] )
                {
                    this.txパネル[ 0 ].t2D描画( CDTXMania.app.Device, nNamePlateX[ 0 ], nNamePlateY[ 0 ] );
                }
                if( this.txパネル[ 1 ] != null && bShowPanel[ 1 ] )
                {
                    this.txパネル[ 1 ].t2D描画( CDTXMania.app.Device, nNamePlateX[ 1 ], nNamePlateY[ 1 ] );
                }
                #endregion
                #region[ スコア、パート表示 ]
                for (int i = 0; i < 2; i++)
                {
                    if( this.txPart != null && bShowPanel[ i ] )
                    {
                        if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                        {
                            int[] nPartRectY = CDTXMania.ConfigIni.bIsSwappedGuitarBass ? new int[] { 76, 38 } : new int[] { 38, 76 };
                            this.txPart.t2D描画(CDTXMania.app.Device, 7 + nNamePlateX[ i ], 91 + nNamePlateY[ i ], new Rectangle(0, nPartRectY[ i ], 234, 38));
                        }
                        else if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                        {
                            int[] nPartRectY = CDTXMania.ConfigIni.bIsSwappedGuitarBass ? new int[] { 120, 60 } : new int[] { 60, 120 };
                            this.txPart.t2D描画(CDTXMania.app.Device, 6 + nNamePlateX[ i ], 50 + nNamePlateY[ i ], new Rectangle(0, nPartRectY[ i ], 234, 60));
                        }
                    }
                    long nNowScore = (long)CDTXMania.stage演奏ギター画面.actScore.n現在表示中のスコア[ i + 1 ];
                    if( CDTXMania.ConfigIni.eSkillMode == ESkillType.DTXMania )
                    {
                        string str = nNowScore.ToString("0000000000");
                        for (int j = 0; j < 10; j++)
                        {
                            Rectangle rectangle;
                            char ch = str[ j ];
                            int num3 = int.Parse(str.Substring(j, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                            }
                            if( this.txScore != null && bShowPanel[ i ] )
                            {
                                this.txScore.t2D描画( CDTXMania.app.Device, nNamePlateX[ i ] + 65 + ( j * 18 ), nNamePlateY[ i ] + 170, rectangle );
                                this.txScore.vc拡大縮小倍率.X = 0.7f;
                            }
                        }
                    }
                    else if( CDTXMania.ConfigIni.eSkillMode == ESkillType.XG )
                    {
                        string str = nNowScore.ToString("0000000");
                        for (int j = 0; j < 7; j++)
                        {
                            Rectangle rectangle;
                            char ch = str[i];
                            int num3 = int.Parse(str.Substring(j, 1));
                            if (num3 < 5)
                            {
                                rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                            }
                            else
                            {
                                rectangle = new Rectangle((num3 * 24), 0, 24, 25);
                            }
                            if (this.txScore != null && bShowPanel[ i ] )
                            {
                                if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A)
                                    this.txScore.t2D描画(CDTXMania.app.Device, nNamePlateX[ i ] + 65 + (i * 25), nNamePlateY[ i ] + 185, rectangle);
                                else if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                                    this.txScore.t2D描画(CDTXMania.app.Device, nNamePlateX[ i ] + 65 + (i * 25), nNamePlateY[ i ] + 170, rectangle);
                            }
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
        private CTexture txPart;
        private CTexture txScore;
        private CTexture[] txパネル;
        private CTexture tx曲名パネル;
        private CPrivateFastFont pfSongTitleFont;
        private CPrivateFastFont pfNameFont;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private string[] strGroupName;
        private string[] strPlayerName;
        private string strTitle;

		//-----------------
		#endregion
	}
}
