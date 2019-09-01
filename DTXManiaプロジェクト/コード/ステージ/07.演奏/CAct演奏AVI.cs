using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpDX;
using SharpDX.Direct3D9;
using FDK;
using System.Diagnostics;

namespace DTXMania
{
	internal class CAct演奏AVI : CActivity
	{
		// コンストラクタ

		public CAct演奏AVI()
		{
			base.b活性化してない = true;
		}
		/// <summary>
		/// プレビュームービーかどうか
		/// </summary>
		/// <remarks>
		/// On活性化()の前にフラグ操作すること。(活性化中に、本フラグを見て動作を変える部分があるため)
		/// </remarks>
		public bool bIsPreviewMovie
		{
			get;
			set;
		}
		public bool bHasBGA
		{
			get;
			set;
		}
		public bool bFullScreenMovie
		{
			get;
			set;
		}

		public void PrepareProperSizeTexture(int width, int height)
		{
			try
			{
				if ( this.tx描画用 != null && ( this.tx描画用.szテクスチャサイズ.Width != width || this.tx描画用.szテクスチャサイズ.Height != height ) )
				{
//Debug.WriteLine( "orgW=" + this.tx描画用.szテクスチャサイズ.Width + ", W=" + width + ", orgH=" + this.tx描画用.szテクスチャサイズ.Height + ", H=" + height );
					this.tx描画用.Dispose();
					this.tx描画用 = null;
				}
				if ( this.tx描画用 == null )
				{
					this.tx描画用 = new CTexture(
						CDTXMania.app.Device, width, height,
						CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat,
						Pool.Managed);
				}
			}
			catch (CTextureCreateFailedException e)
			{
				Trace.TraceError("CActAVI: OnManagedリソースの作成(): " + e.Message);
				this.tx描画用 = null;
			}
		}

		// メソッド
		public void Start(int nチャンネル番号, CDTX.CAVI rAVI, int n開始サイズW, int n開始サイズH, int n終了サイズW, int n終了サイズH, int n画像側開始位置X, int n画像側開始位置Y, int n画像側終了位置X, int n画像側終了位置Y, int n表示側開始位置X, int n表示側開始位置Y, int n表示側終了位置X, int n表示側終了位置Y, int n総移動時間ms, int n移動開始時刻ms)
		{
			if( nチャンネル番号 == (int) Ech定義.Movie || nチャンネル番号 == (int) Ech定義.MovieFull )
			{
				this.rAVI = rAVI;
				this.n開始サイズW = n開始サイズW;
				this.n開始サイズH = n開始サイズH;
				this.n終了サイズW = n終了サイズW;
				this.n終了サイズH = n終了サイズH;
				this.n総移動時間ms = n総移動時間ms;
				this.PrepareProperSizeTexture((int)this.rAVI.avi.nフレーム幅, (int)this.rAVI.avi.nフレーム高さ);
				this.n移動開始時刻ms = ( n移動開始時刻ms != -1 ) ? n移動開始時刻ms : CSound管理.rc演奏用タイマ.n現在時刻;
				this.rAVI.avi.Run();
			}
		}
		public void SkipStart( int n移動開始時刻ms )
		{
			foreach( CDTX.CChip chip in CDTXMania.DTX.listChip )
			{
				if( chip.n発声時刻ms > n移動開始時刻ms )
				{
					break;
				}
				switch( chip.eAVI種別 )
				{
					case EAVI種別.AVI:
						{
							if( chip.rAVI != null )
							{
								if (this.rAVI == null )
								{
									this.rAVI = chip.rAVI;		// DTXVモードで、最初に途中再生で起動したときに、ここに来る
								}
								this.bFullScreenMovie = ( chip.nチャンネル番号 == (int) Ech定義.MovieFull || CDTXMania.ConfigIni.bForceAVIFullscreen );		// DTXVモードで、最初に途中再生で起動したときのために必要
								this.rAVI.avi.Seek( n移動開始時刻ms - chip.n発声時刻ms );
								this.Start( chip.nチャンネル番号, chip.rAVI, SampleFramework.GameWindowSize.Width, SampleFramework.GameWindowSize.Height, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chip.n発声時刻ms );
							}
							continue;
						}
					case EAVI種別.AVIPAN:
						{
							if( chip.rAVIPan != null )
							{
								if ( this.rAVI == null )
								{
									this.rAVI = chip.rAVI;		// DTXVモードで、最初に途中再生で起動したときに、ここに来る
								}
								this.bFullScreenMovie = ( chip.nチャンネル番号 == (int) Ech定義.MovieFull || CDTXMania.ConfigIni.bForceAVIFullscreen );		// DTXVモードで、最初に途中再生で起動したときのために必要
								this.rAVI.avi.Seek( n移動開始時刻ms - chip.n発声時刻ms );
								this.Start( chip.nチャンネル番号, chip.rAVI, chip.rAVIPan.sz開始サイズ.Width, chip.rAVIPan.sz開始サイズ.Height, chip.rAVIPan.sz終了サイズ.Width, chip.rAVIPan.sz終了サイズ.Height, chip.rAVIPan.pt動画側開始位置.X, chip.rAVIPan.pt動画側開始位置.Y, chip.rAVIPan.pt動画側終了位置.X, chip.rAVIPan.pt動画側終了位置.Y, chip.rAVIPan.pt表示側開始位置.X, chip.rAVIPan.pt表示側開始位置.Y, chip.rAVIPan.pt表示側終了位置.X, chip.rAVIPan.pt表示側終了位置.Y, chip.n総移動時間, chip.n発声時刻ms );
							}
							continue;
						}
				}
			}
		}
		public void Stop()
		{
			if( ( this.rAVI != null ) && ( this.rAVI.avi != null ) )
			{
				this.n移動開始時刻ms = -1;
			}
		}
		public void Cont( int n再開時刻ms )
		{
			if( ( this.rAVI != null ) && ( this.rAVI.avi != null ) )
			{
				this.n移動開始時刻ms = n再開時刻ms;
			}
		}
		public int t進行描画(int x, int y, int areaDrawingWidth, int areaDrawingHeight)
		{
			if( !base.b活性化してない )
			{
				// Rectangle rectangle;
				// Rectangle rectangle2;

				if ( ( ( this.n移動開始時刻ms == -1 ) || ( this.rAVI == null ) ) || ( this.rAVI.avi == null ) )
				{
					return 0;
				}
				if ( this.tx描画用 == null )
				{
					return 0;
				}
				this.nTime = (int)((CSound管理.rc演奏用タイマ.n現在時刻 - this.n移動開始時刻ms) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
				if( ( this.n総移動時間ms != 0 ) && ( this.n総移動時間ms < this.nTime ) )
				{
					this.n総移動時間ms = 0;
					this.n移動開始時刻ms = -1;
					return 0;
				}
				if( ( this.n総移動時間ms == 0 ) && this.nTime >= this.rAVI.avi.GetDuration() )
				{
					if ( !bIsPreviewMovie )
					{
						this.n移動開始時刻ms = -1;
						return 0;
					}
					// PREVIEW時はループ再生する。移動開始時刻msを現時刻にして(=AVIを最初に巻き戻して)、ここまでに行った計算をやり直す。
					this.n移動開始時刻ms = CSound管理.rc演奏用タイマ.n現在時刻;
					this.nTime = (int) ( ( CSound管理.rc演奏用タイマ.n現在時刻 - this.n移動開始時刻ms ) * ( ( (double) CDTXMania.ConfigIni.n演奏速度 ) / 20.0 ) );
					this.rAVI.avi.Seek(0);
				}

				if ((this.tx描画用 != null) && (this.n総移動時間ms != -1))
				{
					this.rAVI.avi.tGetBitmap( CDTXMania.app.Device, this.tx描画用, this.nTime);
					// 旧動画 (278x355以下)の場合と、それ以上の場合とで、拡大/表示位置補正ロジックを変えること。
					// 旧動画の場合は、「278x355の領域に表示される」ことを踏まえて扱う必要あり。
					// 例: 上半分だけ動画表示するような場合は・・・「上半分だけ」という表示意図を維持すべきか？それとも無視して全画面拡大すべきか？？
					// chnmr0 : プレビューの場合表示領域いっぱいにアス比保持で拡縮します。
					//          プレビューでない場合単純に縦横2倍、位置変更なしで表示します。
					// yyagi: BGAの有無を見ないで、単純にFullScreenMovieならアス比保持で拡縮、そうでないなら縦横2倍＋位置変更なし。
					// chnmr0 : 従来の大きさ以上のプレビュー動画で不都合が起きますのでここは常にアス比保持でフィッティングします。

					float magX = 2, magY = 2;
					int xx = x, yy = y;

					if( CDTXMania.DTX != null && CDTXMania.DTX.bUse556x710BGAAVI )
					{
						magX = magY = 1;
					}

					if ( bFullScreenMovie || bIsPreviewMovie )
					{
						#region [ アスペクト比を維持した拡大縮小 ]
						if (bFullScreenMovie)
						{
							xx = 0;
							yy = 0;
							areaDrawingWidth = SampleFramework.GameWindowSize.Width;
							areaDrawingHeight = SampleFramework.GameWindowSize.Height;
						}
						
						magX = (float)areaDrawingWidth / this.rAVI.avi.nフレーム幅;
						magY = (float)areaDrawingHeight / this.rAVI.avi.nフレーム高さ;
						if (magX > magY)
						{
							magX = magY;
							xx += (int)((areaDrawingWidth - (this.rAVI.avi.nフレーム幅 * magY)) / 2);
						}
						else
						{
							magY = magX;
							yy += (int)((areaDrawingHeight - (this.rAVI.avi.nフレーム高さ * magX)) / 2);
						}
						#endregion
					}

                    if( (float)this.rAVI.avi.nフレーム幅 / (float)this.rAVI.avi.nフレーム高さ > 1.77f )
                    {
                        //2016.02.20 kairera0467
                        //0x5Aじゃなくても16:9なら拡大倍率を変更する。
						xx = 0;
						yy = 0;
                        areaDrawingWidth = SampleFramework.GameWindowSize.Width;
						areaDrawingHeight = SampleFramework.GameWindowSize.Height;

						magX = (float)areaDrawingWidth / this.rAVI.avi.nフレーム幅;
						magY = (float)areaDrawingHeight / this.rAVI.avi.nフレーム高さ;
						if (magX > magY)
						{
							magX = magY;
							xx += (int)((areaDrawingWidth - (this.rAVI.avi.nフレーム幅 * magY)) / 2);
						}
						else
						{
							magY = magX;
							yy += (int)((areaDrawingHeight - (this.rAVI.avi.nフレーム高さ * magX)) / 2);
						}
                    }

					this.tx描画用.vc拡大縮小倍率.X = magX;
					this.tx描画用.vc拡大縮小倍率.Y = magY;
					this.tx描画用.vc拡大縮小倍率.Z = 1.0f;
					this.tx描画用.bFlipY = true;
                    if( CDTXMania.ConfigIni.eMovieClipMode != EMovieClipMode.OFF && CDTXMania.ConfigIni.eMovieClipMode != EMovieClipMode.Window )
					    this.tx描画用.t2D描画( CDTXMania.app.Device, xx, yy );
				}


                //if( CDTXMania.ConfigIni.ボーナス演出を表示する == true )
                {
                    if( this.bStageEffect演出中 )
                    {
                        int numf = this.ctStageEffect進行.n現在の値;
                        this.ctStageEffect進行.t進行();
                        if ( this.ctStageEffect進行.b終了値に達した )
                        {
                            this.ctStageEffect進行.t停止();
                            this.bStageEffect演出中 = false;
                        }
                        if( CDTXMania.ConfigIni.bDrums有効 )
                        {
                            if( this.txArフィルインエフェクト[ this.ctStageEffect進行.n現在の値 ] != null )
                            {
                                float fWidth = 1280.0f / this.txArフィルインエフェクト[ this.ctStageEffect進行.n現在の値 ].sz画像サイズ.Width;
                                float fHeight = 720.0f / this.txArフィルインエフェクト[ this.ctStageEffect進行.n現在の値 ].sz画像サイズ.Height;

                                Vector3 vcRatio = new Vector3( fWidth, fHeight, 1.0f );
                                this.txArフィルインエフェクト[ this.ctStageEffect進行.n現在の値 ].b加算合成 = true;
                                this.txArフィルインエフェクト[ this.ctStageEffect進行.n現在の値 ].vc拡大縮小倍率 = vcRatio;
                                this.txArフィルインエフェクト[ this.ctStageEffect進行.n現在の値 ].t2D描画( CDTXMania.app.Device, 0, 0 );
                            }
                        }
                    }
                }
			}
			return 0;
		}

        //ただレーンの位置にクリップを表示させるだけなんですけどね(白目)
        public void tクリップをレーン上に表示する()
        {
            if( this.rAVI == null )
                return;

            if( this.txクリップパネル != null )
            {
                //switch( CDTXMania.ConfigIni.eMovieClipMode )
                //{
                //    case EMovieClipMode.Window:
                //    case EMovieClipMode.Both:
                //        {
                            int[] nClipPos = new int[] { 298, 327, 0, 0 };
                            int[] nPanelPos = new int[] { 854, 142, 0, 0 };
                            float fClipRatio = ( (float)this.rAVI.avi.nフレーム幅 / (float)this.rAVI.avi.nフレーム高さ );
                            float fResizeRatio = 1.0f;
                            if( CDTXMania.ConfigIni.bDrums有効 )
                            {
                                //if( CDTXMania.ConfigIni.bGraph.Drums )
                                //{
                                //    nClipPos = new int[] { 2, 402, 0, 0 };
                                //    nPanelPos = new int[] { 2, 402, 0, 0 };
                                //    nClipPos[ 0 ] += 7;
                                //    nClipPos[ 1 ] += 156;
                                //    if( fClipRatio >= 1.77f ) //16:9
                                //        fResizeRatio = 260.0f / this.rAVI.avi.nフレーム幅;

                                //    nClipPos[ 0 ] += (int)( ( 260.0f - ( this.rAVI.avi.nフレーム幅 * fResizeRatio ) ) / 2.0f );
                                //    nClipPos[ 1 ] -= (int)( ( ( this.rAVI.avi.nフレーム高さ * fResizeRatio ) ) / 2.0f );
                                //}
                                //else
                                {
                                    //nClipPos[ 0 ] += 5;
                                    //nClipPos[ 1 ] += 30;
                                    
                                    if( fClipRatio >= 1.77f ) //16:9
                                        fResizeRatio = 553.0f / this.rAVI.avi.nフレーム幅;

                                    nClipPos[1] -= 180;
                                }
                            }
                            else
                            {

                            }

                            //this.txクリップパネル.t2D描画( CDTXMania.app.Device, nPanelPos[ 0 ], nPanelPos[ 1 ] );
                            this.tx描画用.vc拡大縮小倍率 = new Vector3( fResizeRatio, fResizeRatio, 1.0f );
                            this.tx描画用.t2D描画( CDTXMania.app.Device, nClipPos[ 0 ], nClipPos[ 1 ] );
                //        }
                //        break;
                //}

            }
        }

        public void tウィンドウクリップを表示する()
        {
            if( this.rAVI == null )
                return;

            if( this.txクリップパネル != null )
            {
                if( CDTXMania.ConfigIni.bWindowClipMode )
                {
                    int[] nClipPos = new int[] { 854, 142, 0, 0 };
                    int[] nPanelPos = new int[] { 854, 142, 0, 0 };
                    float fClipRatio = ( (float)this.rAVI.avi.nフレーム幅 / (float)this.rAVI.avi.nフレーム高さ );
                    float fResizeRatio = 1.0f;
                    if( CDTXMania.ConfigIni.bDrums有効 )
                    {
                        if( CDTXMania.ConfigIni.bGraph.Drums )
                        {
                            nClipPos = new int[] { 2, 402, 0, 0 };
                            nPanelPos = new int[] { 2, 402, 0, 0 };
                            nClipPos[ 0 ] += 7;
                            nClipPos[ 1 ] += 156;
                            if( fClipRatio >= 1.77f ) //16:9
                                fResizeRatio = 260.0f / this.rAVI.avi.nフレーム幅;

                            nClipPos[ 0 ] += (int)( ( 260.0f - ( this.rAVI.avi.nフレーム幅 * fResizeRatio ) ) / 2.0f );
                            nClipPos[ 1 ] -= (int)( ( ( this.rAVI.avi.nフレーム高さ * fResizeRatio ) ) / 2.0f );
                        }
                        else
                        {
                            nClipPos[ 0 ] += 5;
                            nClipPos[ 1 ] += 30;
                            if( fClipRatio >= 1.77f ) //16:9
                                fResizeRatio = 416.0f / this.rAVI.avi.nフレーム幅;
                        }
                    }
                    else
                    {

                    }

                    this.txクリップパネル.t2D描画( CDTXMania.app.Device, nPanelPos[ 0 ], nPanelPos[ 1 ] );
                    this.tx描画用.vc拡大縮小倍率 = new Vector3( fResizeRatio, fResizeRatio, 1.0f );
                    this.tx描画用.t2D描画( CDTXMania.app.Device, nClipPos[ 0 ], nClipPos[ 1 ] );
                }
                #region[ 古いの ]
                //switch( CDTXMania.ConfigIni.eMovieClipMode )
                //{
                //    case EMovieClipMode.Window:
                //    case EMovieClipMode.Both:
                //        {
                //            int[] nClipPos = new int[] { 854, 142, 0, 0 };
                //            int[] nPanelPos = new int[] { 854, 142, 0, 0 };
                //            float fClipRatio = ( (float)this.rAVI.avi.nフレーム幅 / (float)this.rAVI.avi.nフレーム高さ );
                //            float fResizeRatio = 1.0f;
                //            if( CDTXMania.ConfigIni.bDrums有効 )
                //            {
                //                if( CDTXMania.ConfigIni.bGraph.Drums )
                //                {
                //                    nClipPos = new int[] { 2, 402, 0, 0 };
                //                    nPanelPos = new int[] { 2, 402, 0, 0 };
                //                    nClipPos[ 0 ] += 7;
                //                    nClipPos[ 1 ] += 156;
                //                    if( fClipRatio >= 1.77f ) //16:9
                //                        fResizeRatio = 260.0f / this.rAVI.avi.nフレーム幅;

                //                    nClipPos[ 0 ] += (int)( ( 260.0f - ( this.rAVI.avi.nフレーム幅 * fResizeRatio ) ) / 2.0f );
                //                    nClipPos[ 1 ] -= (int)( ( ( this.rAVI.avi.nフレーム高さ * fResizeRatio ) ) / 2.0f );
                //                }
                //                else
                //                {
                //                    nClipPos[ 0 ] += 5;
                //                    nClipPos[ 1 ] += 30;
                //                    if( fClipRatio >= 1.77f ) //16:9
                //                        fResizeRatio = 416.0f / this.rAVI.avi.nフレーム幅;
                //                }
                //            }
                //            else
                //            {

                //            }

                //            this.txクリップパネル.t2D描画( CDTXMania.app.Device, nPanelPos[ 0 ], nPanelPos[ 1 ] );
                //            this.tx描画用.vc拡大縮小倍率 = new Vector3( fResizeRatio, fResizeRatio, 1.0f );
                //            this.tx描画用.t2D描画( CDTXMania.app.Device, nClipPos[ 0 ], nClipPos[ 1 ] );
                //        }
                //        break;
                //}
                #endregion
            }
        }

        public void tウィンドウクリップを3D表示する()
        {
            // 今のところmatixxのみ
            if( this.rAVI == null )
                return;

            if( CDTXMania.ConfigIni.bWindowClipMode )
            {
                int[] nClipPos = new int[] { 854, 142, 0, 0 };
                int[] nPanelPos = new int[] { 854, 142, 0, 0 };
                float fClipRatio = ( (float)this.rAVI.avi.nフレーム幅 / (float)this.rAVI.avi.nフレーム高さ );
                float fResizeRatio = 1.0f;
                float fResizeRatio2 = 1.0f;
                if( CDTXMania.ConfigIni.bDrums有効 )
                {
                    //if( CDTXMania.ConfigIni.bGraph.Drums )
                    //{
                    //    nClipPos = new int[] { 2, 402, 0, 0 };
                    //    nPanelPos = new int[] { 2, 402, 0, 0 };
                    //    nClipPos[ 0 ] += 7;
                    //    nClipPos[ 1 ] += 156;
                    //    if( fClipRatio >= 1.77f ) //16:9
                    //    {
                    //        fResizeRatio = 260.0f / this.rAVI.avi.nフレーム幅;
                    //        //fResizeRatio = 260.0f / this.rAVI.avi.nフレーム幅;
                    //    }

                    //    nClipPos[ 0 ] += (int)( ( 260.0f - ( this.rAVI.avi.nフレーム幅 * fResizeRatio ) ) / 2.0f );
                    //    nClipPos[ 1 ] -= (int)( ( ( this.rAVI.avi.nフレーム高さ * fResizeRatio ) ) / 2.0f );
                    //}
                    //else
                    {
                        nClipPos[ 0 ] += 5;
                        nClipPos[ 1 ] += 30;
                        if( fClipRatio >= 1.77f ) //16:9
                        {
                            fResizeRatio = 277.0f / this.rAVI.avi.nフレーム幅;
                            fResizeRatio2 = 155.0f / this.rAVI.avi.nフレーム高さ;
                        }
                    }
                }
                else
                {
                    // ギター側未実装
                }

                Matrix mat = Matrix.Identity;

                mat *= Matrix.Scaling( fResizeRatio, fResizeRatio2 + 0.08f, 0);
                mat *= Matrix.RotationY( C変換.DegreeToRadian( 30 ) );
                mat *= Matrix.Translation( 450, -80, 0 );

                this.txクリップパネル?.t2D描画( CDTXMania.app.Device, nPanelPos[ 0 ], nPanelPos[ 1 ] );
                //this.tx描画用.vc拡大縮小倍率 = new Vector3( fResizeRatio, fResizeRatio, 1.0f );
                //this.tx描画用.t2D描画( CDTXMania.app.Device, nClipPos[ 0 ], nClipPos[ 1 ] );
                this.tx描画用?.t3D上下反転描画( CDTXMania.app.Device, mat );
            }
        }

		
		// CActivity 実装

		public override void On活性化()
		{
			this.rAVI = null;
			this.n移動開始時刻ms = -1;
			this.bHasBGA = false;
			this.bFullScreenMovie = false;
            this.bStageEffect演出中 = false;
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
#if TEST_Direct3D9Ex
				this.tx描画用 = new CTexture( CDTXMania.app.Device,
					320,
					355,
					CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Default, Usage.Dynamic );
#else
				this.PrepareProperSizeTexture(
						(bIsPreviewMovie) ? 204 : SampleFramework.GameWindowSize.Width,
						(bIsPreviewMovie) ? 269 : SampleFramework.GameWindowSize.Height
						);
#endif
                this.txArフィルインエフェクト = new CTexture[ 31 ];
                for( int ar = 0; ar < 31; ar++ )
                {
                    if( this.txArフィルインエフェクト[ ar ] == null ) {
                        this.txArフィルインエフェクト[ ar ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\StageEffect\7_StageEffect_" + ar.ToString() + ".png" ) );
                    }
                    if( this.txArフィルインエフェクト[ ar ] == null )
                        continue; //テクスチャが欠けていた場合は、事故防止のためにループを1つスキップする。
                    if( this.txArフィルインエフェクト[ ar ] != null )
                    {
                        this.txArフィルインエフェクト[ ar ].b加算合成 = true;
                        this.txArフィルインエフェクト[ ar ].vc拡大縮小倍率 = new Vector3( 2.0f, 2.0f, 1.0f );
                    }
                }
                this.ctStageEffect進行 = new CCounter( 0, 30, 30, CDTXMania.Timer );

                if( CDTXMania.ConfigIni.bDrums有効 )
                {
                    if( CDTXMania.ConfigIni.bGraph.Drums )
                        this.txクリップパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_ClipPanelB.png" ) );
                    else
                        this.txクリップパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_ClipPanel.png" ) );
                }
                else
                {
                    this.txクリップパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_ClipPanelC.png" ) );
                }
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				this.tx描画用?.Dispose();
				this.tx描画用 = null;
                for( int ar = 0; ar < 31; ar++ )
                {
                    if( this.txArフィルインエフェクト != null )
                    {
                        this.txArフィルインエフェクト[ ar ]?.Dispose();
                    }
                }
                this.txArフィルインエフェクト = null;
                this.txクリップパネル?.Dispose();
                this.ctStageEffect進行 = null;

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(int,int)のほうを使用してください。" );
		}
        public void Start()
        {
            if( !this.bStageEffect演出中 )
            {
                this.bStageEffect演出中 = true;
                this.ctStageEffect進行 = new CCounter( 0, 30, 30, CDTXMania.Timer );
            }
            else
            {
                this.ctStageEffect進行.t停止();
                this.bStageEffect演出中 = false;
            }
        }

		// その他

		#region [ private ]
		//-----------------
		private long n移動開始時刻ms;
		private int n画像側開始位置X;
		private int n画像側開始位置Y;
		private int n画像側終了位置X;
		private int n画像側終了位置Y;
		private int n開始サイズH;
		private int n開始サイズW;
		private int n終了サイズH;
		private int n終了サイズW;
		private int n総移動時間ms;
		private int n表示側開始位置X;
		private int n表示側開始位置Y;
		private int n表示側終了位置X;
		private int n表示側終了位置Y;
		private CDTX.CAVI rAVI;
		private CTexture tx描画用;
        private CTexture txクリップパネル;

        private CTexture[] txArフィルインエフェクト;
        private CCounter ctStageEffect進行;
        private bool bStageEffect演出中;
        private int nTime;
		//-----------------
		#endregion
	}
}
