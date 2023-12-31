﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using SharpDX;
using SharpDX.Direct3D9;
using FDK;

using Color = System.Drawing.Color;
using Rectangle = System.Drawing.Rectangle;
namespace DTXMania
{
	internal class CActResultImageXG : CActResultImage共通
	{
		// コンストラクタ

		public CActResultImageXG()
		{
			base.b活性化してない = true;
		}

        
		// CActivity 実装

		public override void On活性化()
		{
            if( !CDTXMania.bXGRelease ) return;
            if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
            {
                this.nAlbumWidth = 102;
                this.nAlbumHeight = 102;
            }
            else
            {
                this.nAlbumWidth = 128;
                this.nAlbumHeight = 128;
            }

            #region [ 本体位置 ]

            int n上X = 453;
            int n上Y = 11;

            int n下X = 106;
            int n下Y = 430;

            this.n本体X[0] = 0;
            this.n本体Y[0] = 0;

            this.n本体X[1] = 0;
            this.n本体Y[1] = 0;

            this.n本体X[2] = 0;
            this.n本体Y[2] = 0;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体X[0] = n上X;
                this.n本体Y[0] = n上Y;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[1] = n下X;
                        this.n本体Y[1] = n下Y;
                    }
                    else
                    {
                        this.n本体X[1] = n上X;
                        this.n本体Y[1] = n上Y;
                    }
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[2] = n上X;
                        this.n本体Y[2] = n上Y;
                    }
                    else
                    {
                        this.n本体X[2] = n下X;
                        this.n本体Y[2] = n下Y;
                    }
                }

            }
            #endregion
			base.On活性化();
		}
		public override void On非活性化()
		{
            if( !CDTXMania.bXGRelease ) return;

			if( this.ct登場用 != null )
			{
				this.ct登場用 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
            if( !CDTXMania.bXGRelease ) return;
			if( !base.b活性化してない )
			{
                this.tx中央パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_Center Panel.png" ) );

                if( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strSongName = CDTXMania.stage選曲XG.r現在選択中の曲.strタイトル;
                else
                    this.strSongName = CDTXMania.DTX.TITLE;

                if( File.Exists( CDTXMania.stage選曲XG.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png" ) )
                    this.tx曲名 = CDTXMania.tテクスチャの生成( CDTXMania.stage選曲XG.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png" );
                else
                {
                    this.prvFont = new CPrivateFastFont( new FontFamily( CDTXMania.ConfigIni.str選曲リストフォント ), 18 );
                    Bitmap bmpSongName = this.prvFont.DrawPrivateFont( this.strSongName, Color.White );
                    this.tx曲名 = CDTXMania.tテクスチャの生成( bmpSongName, false );

                    CDTXMania.t安全にDisposeする( ref bmpSongName );
                }

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
            if( !CDTXMania.bXGRelease ) return;
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.tx中央パネル );
                CDTXMania.tテクスチャの解放( ref this.tx曲名 );

                CDTXMania.t安全にDisposeする( ref this.prvFont );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
            if( !CDTXMania.bXGRelease ) return 1;
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				if ( CDTXMania.ConfigIni.bストイックモード )
				{
					this.r表示するリザルト画像 = this.txリザルト画像がないときの画像;
				}
				else if ( ( ( !this.tリザルト画像の指定があれば構築する() ) && ( !this.tプレビュー画像の指定があれば構築する() ) ) )
				{
					this.r表示するリザルト画像 = this.txリザルト画像がないときの画像;
				}

				this.ct登場用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct登場用.t進行();

			#region [ プレビュー画像表示 ]
            this.tx中央パネル.t2D描画( CDTXMania.app.Device, 0, 267 );
            if( this.r表示するリザルト画像 != null )
            {
                int width = this.r表示するリザルト画像.szテクスチャサイズ.Width;
                int height = this.r表示するリザルト画像.szテクスチャサイズ.Height;


                this.r表示するリザルト画像.vc拡大縮小倍率.X = ((float)this.nAlbumWidth) / ((float)width);
                this.r表示するリザルト画像.vc拡大縮小倍率.Y = ((float)this.nAlbumHeight) / ((float)height);

                int nアルバムX = 436;
                int nアルバムY = 271;

                if( CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
                {
                    nアルバムX = 449;
                    nアルバムY = 284;
                }

                this.r表示するリザルト画像.t2D描画(CDTXMania.app.Device, nアルバムX, nアルバムY, new Rectangle(0, 0, width, height));
            }
            #endregion
            if( CDTXMania.bXGRelease )
            {
                
                if ( File.Exists( CDTXMania.stage選曲XG.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png" ) )
                {
                    if( this.tx曲名 != null )
                    {
                        this.tx曲名.vc拡大縮小倍率 = new Vector3( 0.75f, 0.75f, 1f );
                        this.tx曲名.t2D描画( CDTXMania.app.Device, 576, 345 );
                    }
                }
                else
                {
                    if( this.tx曲名 != null )
                    {
                        this.tx曲名.vc拡大縮小倍率.X = 0.75f;
                        this.tx曲名.t2D描画( CDTXMania.app.Device, 576, 346 );

                    }
                }
            }

			if( !this.ct登場用.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		//private CAvi avi;
		//private bool b動画フレームを作成した;
		private CCounter ct登場用;
		//private long nAVI再生開始時刻;
		//private int n前回描画したフレーム番号;
        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;
        private int nAlbumHeight;
        private int nAlbumWidth;
		//private IntPtr pAVIBmp;
		//private Surface sfリザルトAVI画像;
        private CTexture tx中央パネル;
        private CTexture tx曲名;
        private CPrivateFastFont prvFont;
        private string strSongName;
        //-----------------
		#endregion
	}
}
