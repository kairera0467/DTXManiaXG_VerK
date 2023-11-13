using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏GuitarステータスパネルGD : CAct演奏ステータスパネル共通
	{
        // コンストラクタ
        public override void On活性化()
        {
            this.ftGroupFont = new Font( CDTXMania.ConfigIni.str選曲リストフォント, 16f, FontStyle.Regular, GraphicsUnit.Pixel );

            this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 20, FontStyle.Bold );
            this.pfSongTitleFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 20, FontStyle.Regular );

            base.On活性化();
        }

        public override void On非活性化()
        {
            CDTXMania.t安全にDisposeする( ref this.ftGroupFont );
            CDTXMania.t安全にDisposeする( ref this.pfNameFont );
            CDTXMania.t安全にDisposeする( ref this.pfSongTitleFont );
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.strGroupName = new string[ 2 ];
                this.strPlayerName = new string[ 2 ];
                this.txパネル = new CTexture[ 2 ];

                // TODO: 未完成

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

                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B ) {
                    this.iDifficulty = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_XG.png" ) );
                    this.iDifficultyNumber = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_number_XG.png" ) );
                } else {
                    this.iDifficulty = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty.png" ) );
                    this.iDifficultyNumber = CDTXMania.tテクスチャをImageで読み込む( CSkin.Path( @"Graphics\7_Difficulty_number_XG2.png" ) );
                }

                Bitmap bSongPanel = new Bitmap( nSongPanel[ 0 ], nSongPanel[ 1 ] );
                Graphics gSongNamePlate = Graphics.FromImage( bSongPanel );
                gSongNamePlate.PageUnit = GraphicsUnit.Pixel;

                gSongNamePlate.DrawImage( CDTXMania.tテクスチャをImageで読み込む( strSongpanelFile ), 0, 0, nSongPanel[ 0 ], nSongPanel[ 1 ] );

                #region[ 曲名 ]
                if( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strTitle = CDTXMania.stage選曲GITADORA.r現在選択中の曲.strタイトル;
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
                    int x = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 16 : 250;
                    int y = CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A ? 70 : 18;
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
                    Bitmap bNamePlate = new Bitmap( 250, 266 );
                    Graphics gNamePlate = Graphics.FromImage( bNamePlate );
                    gNamePlate.PageUnit = GraphicsUnit.Pixel;

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
                        if( bUseGraph[ 0 ] || bUseGraph[ 1 ] )
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
                #region[ スコア表示 ]
                for (int i = 0; i < 2; i++)
                {
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

                                this.txScore.vc拡大縮小倍率.X = 0.7f;
                                if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A)
                                    this.txScore.t2D描画( CDTXMania.app.Device, nNamePlateX[ i ] + 65 + ( j * 18 ), nNamePlateY[ i ] + 185, rectangle);
                                else if (CDTXMania.ConfigIni.eNamePlateType == Eタイプ.B)
                                    this.txScore.t2D描画( CDTXMania.app.Device, nNamePlateX[ i ] + 65 + ( j * 18 ), nNamePlateY[ i ] + 170, rectangle );   
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
        private Font ftGroupFont;
        private string[] strGroupName;
        private string[] strPlayerName;
        private string strTitle;
        private Image iDifficulty;
        private Image iDifficultyNumber;
        //-----------------
        #endregion
    }
}
