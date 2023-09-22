using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using SharpDX;
using SharpDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal abstract class CActResultImage共通 : CActivity
	{
		// コンストラクタ

		public CActResultImage共通()
		{


			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ct登場用.n現在の値 = this.ct登場用.n終了値;
		}

		// CActivity 実装

		public override void On活性化()
		{
            //if( CDTXMania.bXGRelease ) this.actResultImageXG.On活性化();
            //else this.actResultImageGD.On活性化();

			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ct登場用 != null )
			{
				this.ct登場用 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txリザルト画像がないときの画像 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ) );

                //if( CDTXMania.bXGRelease ) this.actResultImageXG.OnManagedリソースの作成();
                //else this.actResultImageGD.OnManagedリソースの作成();

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
				CDTXMania.tテクスチャの解放( ref this.txリザルト画像がないときの画像 );
                CDTXMania.tテクスチャの解放( ref this.tx中央パネル );
                CDTXMania.tテクスチャの解放( ref this.tx曲名 );

                CDTXMania.t安全にDisposeする( ref this.prvFont );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				this.ct登場用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct登場用.t進行();

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
		protected CTexture r表示するリザルト画像;
		//private Surface sfリザルトAVI画像;
        private CTexture tx中央パネル;
		protected CTexture txリザルト画像;
		protected CTexture txリザルト画像がないときの画像;
        private CTexture tx曲名;
        private CPrivateFastFont prvFont;
        private string strSongName;

        protected bool tプレビュー画像の指定があれば構築する()
		{
			if( string.IsNullOrEmpty( CDTXMania.DTX.PREIMAGE ) )
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PATH + CDTXMania.DTX.PREIMAGE;
			if( !File.Exists( path ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { path } );
				return false;
			}
			this.txリザルト画像 = CDTXMania.tテクスチャの生成( path );
			this.r表示するリザルト画像 = this.txリザルト画像;
			return ( this.r表示するリザルト画像 != null );
		}
		protected bool tリザルト画像の指定があれば構築する()
		{
			int rank = CScoreIni.t総合ランク値を計算して返す( CDTXMania.stage結果.st演奏記録.Drums, CDTXMania.stage結果.st演奏記録.Guitar, CDTXMania.stage結果.st演奏記録.Bass );
			if (rank == 99)	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
			{
				rank = 6;
			}
			if (string.IsNullOrEmpty(CDTXMania.DTX.RESULTIMAGE[rank]))
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PATH + CDTXMania.DTX.RESULTIMAGE[ rank ];
			if( !File.Exists( path ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { path } );
				return false;
			}
			this.txリザルト画像 = CDTXMania.tテクスチャの生成( path );
			this.r表示するリザルト画像 = this.txリザルト画像;
			return ( this.r表示するリザルト画像 != null );
		}
		//-----------------
		#endregion
	}
}
