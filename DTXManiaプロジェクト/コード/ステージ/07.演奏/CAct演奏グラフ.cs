using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SharpDX;
using FDK;

using Color = System.Drawing.Color;
using Rectangle = System.Drawing.Rectangle;
using Point = System.Drawing.Point;
namespace DTXMania
{
	internal class CAct演奏グラフ : CActivity
	{
        // グラフ仕様
        // ・ギターとベースで同時にグラフを出すことはない。
        //
        // ・目標のメーター画像
        //   →ゴーストがあった
        // 　　・ゴーストに基づいたグラフ(リアルタイム比較)
        // 　→なかった
        // 　　・ScoreIniの自己ベストのグラフ
        //

		private STDGBVALUE<int> nGraphBG_XPos = new STDGBVALUE<int>(); //ドラムにも座標指定があるためDGBVALUEとして扱う。
		private int nGraphBG_YPos = 200;
		private int DispHeight = 400;
		private int DispWidth = 60;
		private CCounter counterYposInImg = null;
		private readonly int slices = 10;
        private int nGraphUsePart = 0;
        private int[] nGraphGauge_XPos = new int[ 2 ];
        private int nPart = 0;

		// プロパティ

        public double dbグラフ値現在_渡
        {
            get
            {
                return this.dbグラフ値現在;
            }
            set
            {
                this.dbグラフ値現在 = value;
            }
        }
        public double dbグラフ値目標_渡
        {
            get
            {
                return this.dbグラフ値目標;
            }
            set
            {
                this.dbグラフ値目標 = value;
            }
        }
        public int[] n現在のAutoを含まない判定数_渡
        {
            get
            {
                return this.n現在のAutoを含まない判定数;
            }
            set
            {
                this.n現在のAutoを含まない判定数 = value;
            }
        }
		
		// コンストラクタ

		public CAct演奏グラフ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
        {
            this.dbグラフ値目標 = 0f;
            this.dbグラフ値現在 = 0f;

            this.n現在のAutoを含まない判定数 = new int[ 6 ];

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
                this.pfNameFont = new CPrivateFastFont( new FontFamily( "Arial" ), 16, FontStyle.Bold );
                this.txグラフ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Graph_Main.png" ) );
                this.txグラフ_ゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Graph_Gauge.png" ) );

                if( this.pfNameFont != null )
                {
                    if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.PERFECT )
                    {
                        this.txPlayerName = this.t指定された文字テクスチャを生成する( "DJ AUTO" );
                    }
                    else if( CDTXMania.ConfigIni.eTargetGhost.Drums == ETargetGhostData.LAST_PLAY )
                    {
                        this.txPlayerName = this.t指定された文字テクスチャを生成する( "LAST PLAY" );
                    }
                }
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				this.txグラフ?.Dispose();
                this.txグラフ_ゲージ?.Dispose();
                this.txグラフ値自己ベストライン?.Dispose();
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
                    //座標などの定義は初回だけにする。
                    //2016.03.29 kairera0467 非セッション譜面で、譜面が無いパートでグラフを有効にしている場合、譜面があるパートに一時的にグラフを切り替える。
                    //                       時間がなくて雑なコードになったため、後日最適化を行う。
                    if( CDTXMania.ConfigIni.bDrums有効 )
                    {
                        this.nPart = 0;
                        this.nGraphUsePart = 0;
                    }
                    else if( CDTXMania.ConfigIni.bGuitar有効 )
                    {
                        this.nGraphUsePart = ( CDTXMania.ConfigIni.bGraph.Guitar == true ) ? 1 : 2;
                        if( CDTXMania.DTX.bチップがある.Guitar )
                            this.nPart = CDTXMania.ConfigIni.bGraph.Guitar ? 0 : 1;
                        else if( !CDTXMania.DTX.bチップがある.Guitar && CDTXMania.ConfigIni.bGraph.Guitar )
                        {
                            this.nPart = 1;
                            this.nGraphUsePart = 2;
                        }

                        if( !CDTXMania.DTX.bチップがある.Bass && CDTXMania.ConfigIni.bGraph.Bass )
                            this.nPart = 0;
                    }


        	        //if( CDTXMania.bXGRelease )
		        	{
        			    this.nGraphBG_XPos.Drums = 966;
                        this.nGraphBG_XPos.Guitar = 356;
                        this.nGraphBG_XPos.Bass = 647;
                        this.nGraphBG_YPos = 50;
	                }




                    if( CDTXMania.ConfigIni.eTargetGhost[ this.nGraphUsePart ] != ETargetGhostData.NONE )
                    {
                        if( CDTXMania.listTargetGhostScoreData[ this.nGraphUsePart ] != null )
                        {
                            //this.dbグラフ値目標 = CDTXMania.listTargetGhostScoreData[ this.nGraphUsePart ].db演奏型スキル値;
                            this.dbグラフ値目標_表示 = CDTXMania.listTargetGhostScoreData[ this.nGraphUsePart ].db演奏型スキル値;
                        }
                    }

                    this.nGraphGauge_XPos = new int[] { 3, 205 };

					base.b初めての進行描画 = false;
                }

				int stYposInImg = 0;



                if( this.txグラフ != null )
                {
                    //背景
					this.txグラフ.vc拡大縮小倍率 = new Vector3( 1f, 1f, 1f );
                    this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ], nGraphBG_YPos, new Rectangle( 2, 2, 280, 640 ) );

                    //ターゲット名
                    #region[ ターゲット名画像座標 ]
                    int nTargetNameRectY = 0;
                    switch( CDTXMania.ConfigIni.eTargetGhost.Drums )
                    {
                        case ETargetGhostData.NONE:
                            nTargetNameRectY = 556;
                            break;
                        case ETargetGhostData.HI_SKILL:
                            nTargetNameRectY = 588;
                            break;
                        case ETargetGhostData.PERFECT:
                            nTargetNameRectY = 572;
                            break;
                        case ETargetGhostData.LAST_PLAY:
                            nTargetNameRectY = 636;
                            break;
                        case ETargetGhostData.HI_SCORE:
                            nTargetNameRectY = 604;
                            break;
                        case ETargetGhostData.ONLINE:
                            nTargetNameRectY = 620;
                            break;
                    }
                    #endregion
                    this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + ( nPart == 1 ? 189 : 2), nGraphBG_YPos + 611, new Rectangle( 288, 652, 48, 16 ) );
                    this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + ( nPart == 1 ? 218 : 30 ), nGraphBG_YPos + 611, new Rectangle( 288, nTargetNameRectY, 48, 16 ) );

                    this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + ( this.nPart == 1 ? 172 : 90 ), nGraphBG_YPos + 600, new Rectangle( ( this.nPart == 1 ? 452 : 434), 556, 18, 28 ) );
                    this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + ( this.nPart == 1 ? 6 : 115 ), nGraphBG_YPos + 600, new Rectangle( 342, 556, 86, 28 ) );


                    //現在の達成率
                    //2Pはおおよその目分量です。
                    this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + ( this.nPart == 1 ? 96 : 207 ), nGraphBG_YPos + 606, string.Format( "{0,6:##0.00}", this.dbグラフ値現在 ) );
                }

                //ゲージ現在
				if( this.txグラフ_ゲージ != null )
                {
                    //ゲージ背景
                    //ドラムとギター1Pでは左端、ギター2Pでは右端。
                    this.txグラフ_ゲージ.n透明度 = 255;
                    this.txグラフ_ゲージ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + this.nGraphGauge_XPos[ nPart ], nGraphBG_YPos + 40, new Rectangle( 418, 2, 72, 560 ) );

                    //ゲージ本体
                    int nGaugeSize = (int)( 560.0f * (float)this.dbグラフ値現在 / 100.0f );
                    int nPosY = 650 - nGaugeSize;
                    this.txグラフ_ゲージ.n透明度 = 255;
                    this.txグラフ_ゲージ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + this.nGraphGauge_XPos[ nPart ] + 7, nPosY, new Rectangle( 628, 2, 60, nGaugeSize ) );

                    //ゲージ矢印
                    this.txグラフ_ゲージ.n透明度 = 32;
                    this.txグラフ_ゲージ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + this.nGraphGauge_XPos[ nPart ] + 3, nGraphBG_YPos + 40, new Rectangle( 562, 2, 60, 560 ) );

                    //ゲージ比較
                    int nTargetGaugeSize = (int)( 560.0f * ( (float)this.dbグラフ値目標 / 100.0f ) );
                    int nTargetGaugePosY = 650 - nTargetGaugeSize;
                    int nTargetGaugeRectX = this.dbグラフ値現在 > this.dbグラフ値目標 ? 2 : 210;
                    this.txグラフ_ゲージ.n透明度 = 255;
                    this.txグラフ_ゲージ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + ( nPart == 1 ? 3 : 75 ), nTargetGaugePosY, new Rectangle( nTargetGaugeRectX, 2, 201, nTargetGaugeSize ) );

                    string strPlus = this.dbグラフ値現在 >= this.dbグラフ値目標 ? "+" : "-";
                    this.t比較文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + this.nGraphGauge_XPos[ nPart ] + 7, nPosY, string.Format( strPlus + "{0,5:#0.00}", Math.Abs( this.dbグラフ値現在 - this.dbグラフ値目標 ) ) );
                }

                //2016.03.07 kairera0467 とりあえず自己ベストの表示だけ。
                if( this.txグラフ != null )
                {
                    if( CDTXMania.ConfigIni.bJudgeCountDisp == false )
                    {
                        int nBoardPos = 180;
                        int[] nBoardX = new int[] { 102, 16 };
                        int[,] nBoardY = new int[,] { { 180, 265 }, { 240, 180 } };
                        int nBoard = 0;
                        
                        if( this.dbグラフ値自己ベスト > this.dbグラフ値目標_表示 )
                            nBoard = 1;
                        else
                            nBoard = 0;

                        //ゴーストが存在するか
                        if( CDTXMania.listTargetGhsotLag[ this.nGraphUsePart ] != null )
                        {
                            if( CDTXMania.listTargetGhostScoreData[ this.nGraphUsePart ] != null )
                            {
                                switch( CDTXMania.ConfigIni.eTargetGhost[ this.nGraphUsePart ] )
                                {
                                    case ETargetGhostData.PERFECT:
                                        {
                                            this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ], nGraphBG_YPos + nBoardY[ nBoard, 0 ], new Rectangle( 288, 2, 162, 85 ) );
                                            this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ] + 90, nGraphBG_YPos + nBoardY[ nBoard, 0 ] + 50, string.Format( "{0,6:##0.00}%", this.dbグラフ値目標_表示 ) );
                                            if( this.txPlayerName != null )
                                            {
                                                this.txPlayerName.t2D描画( CDTXMania.app.Device, 96 + nGraphBG_XPos[ this.nGraphUsePart ], nGraphBG_YPos + nBoardY[ nBoard, 0 ] + 14 );
                                            }
                                        }
                                        break;
                                    case ETargetGhostData.LAST_PLAY:
                                    case ETargetGhostData.HI_SCORE:
                                    case ETargetGhostData.HI_SKILL:
                                        {
                                            this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ], nGraphBG_YPos + nBoardY[ nBoard, 0 ], new Rectangle( 288, 2, 162, 85 ) );
                                            this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ] + 90, nGraphBG_YPos + nBoardY[ nBoard, 0 ] + 50, string.Format( "{0,6:##0.00}%", this.dbグラフ値目標_表示 ) );
                                            if( CDTXMania.ConfigIni.eTargetGhost[ this.nGraphUsePart ] == ETargetGhostData.LAST_PLAY )
                                            {
                                                if( this.txPlayerName != null )
                                                {
                                                    this.txPlayerName.t2D描画( CDTXMania.app.Device, 96 + nGraphBG_XPos[ this.nGraphUsePart ], nGraphBG_YPos + nBoardY[ nBoard, 0 ] + 14 );
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                        this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ], nGraphBG_YPos + nBoardY[ nBoard, 1 ], new Rectangle( 288, 160, 162, 60 ) );
                        this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ] + 90, nGraphBG_YPos + nBoardY[ nBoard, 1 ] + 24, string.Format( "{0,6:##0.00}%", this.dbグラフ値自己ベスト ) );

                        if( this.dbグラフ値自己ベスト > this.dbグラフ値目標_表示 )
                            this.t折れ線を描画する( nBoardY[ 0, 0 ], nBoardY[ 1, 0 ] );
                        else
                            this.t折れ線を描画する( nBoardY[ 0, 1 ], nBoardY[ 1, 1 ] );
                    }
                    if( CDTXMania.ConfigIni.bJudgeCountDisp )
                    {
                        int nBoardPos = 50;
                        int[] nBoardX = new int[] { 102, 16 };
                        int[] arBoardPos = new int[ 2 ];
                        if( this.dbグラフ値自己ベスト < this.dbグラフ値目標_表示 ) nBoardPos += 54;

                        this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + 102, 240, new Rectangle( 288, 226, 162, 60 ) );
                        this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + 102, 320, new Rectangle( 288, 292, 162, 60 ) );
                        this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + 102, 400, new Rectangle( 288, 358, 162, 60 ) );
                        this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + 102, 480, new Rectangle( 288, 424, 162, 60 ) );
                        this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + 102, 560, new Rectangle( 288, 490, 162, 60 ) );

                        this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + 186, 264, string.Format("{0,6:###0}", this.n現在のAutoを含まない判定数[ 0 ]));
                        this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + 186, 344, string.Format("{0,6:###0}", this.n現在のAutoを含まない判定数[ 1 ]));
                        this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + 186, 424, string.Format("{0,6:###0}", this.n現在のAutoを含まない判定数[ 2 ]));
                        this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + 186, 504, string.Format("{0,6:###0}", this.n現在のAutoを含まない判定数[ 3 ]));
                        this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + 186, 584, string.Format("{0,6:###0}", this.n現在のAutoを含まない判定数[ 4 ]));

                        //ゴーストが存在するか
                        if( CDTXMania.listTargetGhsotLag[ this.nGraphUsePart ] != null )
                        {
                            if( CDTXMania.listTargetGhostScoreData[ this.nGraphUsePart ] != null )
                            {
                                switch( CDTXMania.ConfigIni.eTargetGhost[ this.nGraphUsePart ] )
                                {
                                    case ETargetGhostData.PERFECT:
                                        {
                                            this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ], nGraphBG_YPos + nBoardPos, new Rectangle( 288, 2, 162, 85 ) );
                                            this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ] + 90, nGraphBG_YPos + nBoardPos + 50, string.Format( "{0,6:##0.00}%", this.dbグラフ値目標_表示 ) );
                                            if( this.txPlayerName != null )
                                            {
                                                this.txPlayerName.t2D描画( CDTXMania.app.Device, 96 + nGraphBG_XPos[ this.nGraphUsePart ], nGraphBG_YPos + nBoardPos + 14 );
                                            }
                                        }
                                        break;
                                    case ETargetGhostData.LAST_PLAY:
                                    case ETargetGhostData.HI_SCORE:
                                    case ETargetGhostData.HI_SKILL:
                                        {
                                            this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ], nGraphBG_YPos + nBoardPos, new Rectangle( 288, 2, 162, 85 ) );
                                            //this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ], nGraphBG_YPos + 50, new Rectangle( 288, 2, 162, 85 ) );
                                            this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ] + 90, nGraphBG_YPos + nBoardPos + 50, string.Format( "{0,6:##0.00}%", this.dbグラフ値目標_表示 ) );
                                            if( CDTXMania.ConfigIni.eTargetGhost[ this.nGraphUsePart ] == ETargetGhostData.LAST_PLAY )
                                            {
                                                if( this.txPlayerName != null )
                                                {
                                                    this.txPlayerName.t2D描画( CDTXMania.app.Device, 96 + nGraphBG_XPos[ this.nGraphUsePart ], nGraphBG_YPos + nBoardPos + 14 );
                                                }
                                            }
                                        }
                                        break;
                                }
                                arBoardPos[ 1 ] = nBoardPos;
                            }
                        }
                        if( this.dbグラフ値自己ベスト < this.dbグラフ値目標_表示 ) nBoardPos -= 54;
                        else  nBoardPos += 85;
                        arBoardPos[ 0 ] = nBoardPos;

                        this.txグラフ.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ], nGraphBG_YPos + nBoardPos, new Rectangle( 288, 160, 162, 60 ) );
                        this.t達成率文字表示( nGraphBG_XPos[ this.nGraphUsePart ] + nBoardX[ nPart ] + 90, nGraphBG_YPos + nBoardPos + 24, string.Format( "{0,6:##0.00}%", this.dbグラフ値自己ベスト ) );

                        this.t折れ線を描画する( arBoardPos[ 0 ], arBoardPos[ 1 ] );
                    }
                }
			}
			return 0;
		}


		// その他

		#region [ private ]
		//----------------
        private double dbグラフ値目標;
        private double dbグラフ値目標_表示;
        private double dbグラフ値現在;
        private double dbグラフ値現在_表示;
        public double dbグラフ値自己ベスト;
        private int[] n現在のAutoを含まない判定数;

        private CTexture txPlayerName;
		private CTexture txグラフ;
        private CTexture txグラフ_ゲージ;
        private CTexture txグラフ値自己ベストライン;

        private CPrivateFastFont pfNameFont;

        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
            public ST文字位置( char ch, Point pt )
            {
                this.ch = ch;
                this.pt = pt;
            }
        }

        private ST文字位置[] st比較数字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 10, 0 ) ),
            new ST文字位置( '2', new Point( 20, 0 ) ),
            new ST文字位置( '3', new Point( 30, 0 ) ),
            new ST文字位置( '4', new Point( 40, 0 ) ),
            new ST文字位置( '5', new Point( 50, 0 ) ),
            new ST文字位置( '6', new Point( 60, 0 ) ),
            new ST文字位置( '7', new Point( 70, 0 ) ),
            new ST文字位置( '8', new Point( 80, 0 ) ),
            new ST文字位置( '9', new Point( 90, 0 ) ),
            new ST文字位置( '.', new Point( 100, 0 ) ),
            new ST文字位置( '-', new Point( 110, 0 ) ),
            new ST文字位置( '+', new Point( 120, 0 ) )
        };
        private ST文字位置[] st達成率数字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 12, 0 ) ),
            new ST文字位置( '2', new Point( 24, 0 ) ),
            new ST文字位置( '3', new Point( 36, 0 ) ),
            new ST文字位置( '4', new Point( 48, 0 ) ),
            new ST文字位置( '5', new Point( 60, 0 ) ),
            new ST文字位置( '6', new Point( 72, 0 ) ),
            new ST文字位置( '7', new Point( 84, 0 ) ),
            new ST文字位置( '8', new Point( 96, 0 ) ),
            new ST文字位置( '9', new Point( 108, 0 ) ),
            new ST文字位置( '.', new Point( 120, 0 ) ),
        };


		private void t比較文字表示( int x, int y, string str )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st比較数字位置.Length; i++ )
				{
					if( this.st比較数字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( 342 + this.st比較数字位置[ i ].pt.X, 612, 10, 14 );
						if( this.txグラフ != null )
						{
                            this.txグラフ.n透明度 = 255;
							this.txグラフ.t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
				x += 10;
			}
		}
		private void t達成率文字表示( int x, int y, string str )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st達成率数字位置.Length; i++ )
				{
					if( this.st達成率数字位置[ i ].ch == ch )
					{
						Rectangle rectangle = new Rectangle( 342 + this.st達成率数字位置[ i ].pt.X, 590, 12, 16 );
						if( this.txグラフ != null )
						{
                            this.txグラフ.n透明度 = 255;
							this.txグラフ.t2D描画( CDTXMania.app.Device, x, y, rectangle );
						}
						break;
					}
				}
                if( ch == '.' ) x += 6;
				else x += 12;
			}
		}
        private CTexture t指定された文字テクスチャを生成する( string str文字 )
        {
            Bitmap bmp;
            bmp = this.pfNameFont.DrawPrivateFont( str文字, Color.White, Color.Transparent );

            CTexture tx文字テクスチャ = CDTXMania.tテクスチャの生成( bmp, false );

            if( tx文字テクスチャ != null )
                tx文字テクスチャ.vc拡大縮小倍率 = new Vector3( 1.0f, 1.0f, 1f );

            bmp.Dispose();

            return tx文字テクスチャ;
        }
        private void t折れ線を描画する( int nBoardPosA, int nBoardPosB )
        {
            //やる気がまるでない線
            //2016.03.28 kairera0467 ギター画面では1Pと2Pで向きが変わるが、そこは残念ながら未対応。
            //参考 http://dobon.net/vb/dotnet/graphics/drawline.html
            if( this.txグラフ値自己ベストライン == null )
            {
                Bitmap canvas = new Bitmap( 280, 720 );

                Graphics g = Graphics.FromImage( canvas );
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                int nMybestGaugeSize = (int)( 560.0f * (float)this.dbグラフ値自己ベスト / 100.0f );
                int nMybestGaugePosY = 600 - nMybestGaugeSize;

                int nTargetGaugeSize = (int)( 560.0f * (float)this.dbグラフ値目標_表示 / 100.0f );
                int nTargetGaugePosY = 600 - nTargetGaugeSize;

                Point[] posMybest = {
                    new Point( 3, nMybestGaugePosY ),
                    new Point( 75, nMybestGaugePosY ),
                    new Point( 94, nBoardPosA + 31 ),
                    new Point( 102, nBoardPosA + 31 )
                };

                Point[] posTarget = {
                    new Point( 3, nTargetGaugePosY ),
                    new Point( 75, nTargetGaugePosY ),
                    new Point( 94, nBoardPosB + 59 ),
                    new Point( 102, nBoardPosB + 59 )
                };

                if( this.nGraphUsePart == 2 )
                {
                    posMybest = new Point[]{
                        new Point( 271, nMybestGaugePosY ),
                        new Point( 206, nMybestGaugePosY ),
                        new Point( 187, nBoardPosA + 31 ),
                        new Point( 178, nBoardPosA + 31 )
                    };

                    posTarget = new Point[]{
                        new Point( 271, nTargetGaugePosY ),
                        new Point( 206, nTargetGaugePosY ),
                        new Point( 187, nBoardPosB + 59 ),
                        new Point( 178, nBoardPosB + 59 )
                    };
                }

                Pen penMybest = new Pen( Color.Pink, 2 );
                g.DrawLines( penMybest, posMybest );

                if( CDTXMania.listTargetGhsotLag[ this.nGraphUsePart ] != null && CDTXMania.listTargetGhostScoreData[ this.nGraphUsePart ] != null )
                {
                    Pen penTarget = new Pen( Color.Orange, 2 );
                    g.DrawLines( penTarget, posTarget );
                }

                g.Dispose();

                this.txグラフ値自己ベストライン = new CTexture( CDTXMania.app.Device, canvas, CDTXMania.TextureFormat, false );
            }
            if( this.txグラフ値自己ベストライン != null )
                this.txグラフ値自己ベストライン.t2D描画( CDTXMania.app.Device, nGraphBG_XPos[ this.nGraphUsePart ], nGraphBG_YPos );
        }
		//-----------------
		#endregion
	}
}
