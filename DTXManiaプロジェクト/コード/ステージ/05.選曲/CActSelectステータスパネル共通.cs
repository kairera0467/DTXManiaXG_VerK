using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
using System.IO;
using FDK;

namespace DTXMania
{
	internal class CActSelectステータスパネル共通 : CActivity
	{
		// メソッド

		public CActSelectステータスパネル共通()
		{
			base.b活性化してない = true;
		}
		public virtual void t選択曲が変更された()
		{
		}


		// CActivity 実装

		public override void On活性化()
		{
			base.On活性化();
		}
		public override void On非活性化()
		{
            this.ct難易度変更カウンター = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				//this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_status panel.png" ) );
				this.txゲージ用数字他 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_skill icon.png" ), false );
                this.tx難易度パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty panel.png" ) );
                this.tx難易度数字XG = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_LevelNumber.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.txゲージ用数字他 );
                CDTXMania.tテクスチャの解放( ref this.tx難易度パネル );
                CDTXMania.tテクスチャの解放( ref this.tx難易度数字XG );

                CDTXMania.tテクスチャの解放( ref this.tx決定後_難易度パネル1P );
                CDTXMania.tテクスチャの解放( ref this.tx決定後_難易度パネル2P );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{

			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct ST数字
		{
			public char ch;
			public Rectangle rc;
			public ST数字( char ch, Rectangle rc )
			{
				this.ch = ch;
				this.rc = rc;
			}
		}
        
        protected STDGBVALUE<bool>[] b現在選択中の曲がFC難易度毎 = new STDGBVALUE<bool>[5];
        protected STDGBVALUE<bool>[] b現在選択中の曲に譜面がある = new STDGBVALUE<bool>[5];
        protected STDGBVALUE<bool>[] b現在選択中の曲がフルコンボ難易度毎 = new STDGBVALUE<bool>[5];
        protected STDGBVALUE<int>[] n現在選択中の曲のレベル難易度毎DGB = new STDGBVALUE<int>[5];
        protected STDGBVALUE<int>[] n現在選択中の曲のレベル小数点難易度毎DGB = new STDGBVALUE<int>[5];
        protected STDGBVALUE<double>[] db現在選択中の曲の最高スキル値難易度毎 = new STDGBVALUE<double>[5];
        protected STDGBVALUE<int>[] n現在選択中の曲の最高ランク難易度毎 = new STDGBVALUE<int>[5];
        protected CDTX.STLANEINT[] n選択中の曲のノート数_難易度毎 = new CDTX.STLANEINT[5];
        protected double[] db現在選択中の曲の曲別スキル値難易度毎 = new double[5];
        private int[] n選択中の曲のレベル難易度毎 = new int[5];

		protected int n現在選択中の曲の難易度;
		private int n難易度開始文字位置;
		private const int n難易度表示可能文字数 = 0x24;
		private string[] str難易度ラベル = new string[] { "", "", "", "", "" };
		private CTexture txゲージ用数字他;
		public CTexture txパネル本体;
        private CTexture tx難易度パネル;
        private CTexture tx難易度数字XG;
        public CCounter ct難易度変更カウンター;

        private CTexture tx決定後_難易度パネル1P;
        private CTexture tx決定後_難易度パネル2P;

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
        [StructLayout(LayoutKind.Sequential)]
        private struct ST達成率数字
        {
            public char ch;
            public Rectangle rc;
            public ST達成率数字( char ch, Rectangle rc )
            {
                this.ch = ch;
                this.rc = rc;
            }
        }
        //2016.02.23 kairera0467 初期化の行数を大幅短縮。
        private ST文字位置[] st小文字位置 = new ST文字位置[] {
            new ST文字位置( '0', new Point( 13, 40 ) ),
            new ST文字位置( '1', new Point( 26, 40 ) ),
            new ST文字位置( '2', new Point( 39, 40 ) ),
            new ST文字位置( '3', new Point( 52, 40 ) ),
            new ST文字位置( '4', new Point( 65, 40 ) ),
            new ST文字位置( '5', new Point( 78, 40 ) ),
            new ST文字位置( '6', new Point( 91, 40 ) ),
            new ST文字位置( '7', new Point( 105, 40 ) ),
            new ST文字位置( '8', new Point( 118, 40 ) ),
            new ST文字位置( '9', new Point( 131, 40 ) ),
            new ST文字位置( '-', new Point( 0, 40 ) )
        };

        private ST文字位置[] st大文字位置 = new ST文字位置[] {
            new ST文字位置( '.', new Point( 144, 0 ) ),
            new ST文字位置( '1', new Point( 22, 0 ) ),
            new ST文字位置( '2', new Point( 44, 0 ) ),
            new ST文字位置( '3', new Point( 66, 0 ) ),
            new ST文字位置( '4', new Point( 88, 0 ) ),
            new ST文字位置( '5', new Point( 110, 0 ) ),
            new ST文字位置( '6', new Point( 132, 0 ) ),
            new ST文字位置( '7', new Point( 153, 0 ) ),
            new ST文字位置( '8', new Point( 176, 0 ) ),
            new ST文字位置( '9', new Point( 198, 0 ) ),
            new ST文字位置( '0', new Point( 220, 0 ) ),
            new ST文字位置( '-', new Point( 0, 0 ) )
        };

        private ST達成率数字[] st達成率数字 = new ST達成率数字[]{
            new ST達成率数字( '0', new Rectangle( 0, 62, 7, 16 ) ),
            new ST達成率数字( '1', new Rectangle( 7, 62, 7, 16 ) ),
            new ST達成率数字( '2', new Rectangle( 14, 62, 7, 16 ) ),
            new ST達成率数字( '3', new Rectangle( 21, 62, 7, 16 ) ),
            new ST達成率数字( '4', new Rectangle( 28, 62, 7, 16 ) ),
            new ST達成率数字( '5', new Rectangle( 35, 62, 7, 16 ) ),
            new ST達成率数字( '6', new Rectangle( 42, 62, 7, 16 ) ),
            new ST達成率数字( '7', new Rectangle( 49, 62, 7, 16 ) ),
            new ST達成率数字( '8', new Rectangle( 56, 62, 7, 16 ) ),
            new ST達成率数字( '9', new Rectangle( 63, 62, 7, 16 ) ),
            new ST達成率数字( '%', new Rectangle( 70, 62, 9, 16 ) ),
            new ST達成率数字( '.', new Rectangle( 79, 62, 3, 16 ) )
        };

		protected int n現在の難易度ラベルが完全表示されているかを調べてスクロール方向を返す()
		{
			int num = 0;
			int length = 0;
			for( int i = 0; i < 5; i++ )
			{
				if( ( this.str難易度ラベル[ i ] != null ) && ( this.str難易度ラベル[ i ].Length > 0 ) )
				{
					length = this.str難易度ラベル[ i ].Length;
				}
				if( this.n現在選択中の曲の難易度 == i )
				{
					break;
				}
				if( ( this.str難易度ラベル[ i ] != null ) && ( this.str難易度ラベル.Length > 0 ) )
				{
					num += length + 2;
				}
			}
			if( num >= ( this.n難易度開始文字位置 + 55 ) )	// 0x24 -> 55
			{
				return 1;
			}
			if( ( num + length ) <= this.n難易度開始文字位置 )
			{
				return -1;
			}
			if( ( ( num + length ) - 1 ) >= ( this.n難易度開始文字位置 + 55 ) )	// 0x24 -> 55
			{
				return 1;
			}
			if( num < this.n難易度開始文字位置 )
			{
				return -1;
			}
			return 0;
		}
        private void t小文字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for( int i = 0; i < this.st小文字位置.Length; i++ )
                {
                    if (this.st小文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 13, 22);
                        if (this.tx難易度数字XG != null)
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += 12;
            }
        }
        private void t大文字表示(int x, int y, string str)
        {
            for( int i = 0; i < str.Length; i++ )
            {
                char c = str[ i ];
                for( int j = 0; j < this.st大文字位置.Length; j++ )
                {
                    if( this.st大文字位置[ j ].ch == c )
                    {
                        Rectangle rc画像内の描画領域 = new Rectangle( this.st大文字位置[ j ].pt.X, this.st大文字位置[ j ].pt.Y, 22, 40);
                        if( c == '.' )
                        {
                            rc画像内の描画領域.Width -= 2;
                            rc画像内の描画領域.Height -= 2;
                        }
                        if( this.tx難易度数字XG != null )
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rc画像内の描画領域);
                        }
                        break;
                    }
                }
                if( c == '.' ) x += 0;
                else x += 24;
            }
        }
        private void t達成率表示(int x, int y, string str)
        {
            for (int j = 0; j < str.Length; j++)
            {
                char c = str[j];
                for (int i = 0; i < this.st達成率数字.Length; i++)
                {
                    if (this.st達成率数字[i].ch == c)
                    {
                        Rectangle rectangle = new Rectangle(this.st達成率数字[i].rc.X, this.st達成率数字[i].rc.Y, 7, 16);

                        if( c == '.' )
                            rectangle.Width -= 2;
                        else if( c == '%' )
                            rectangle.Width += 2;
                        if (this.tx難易度数字XG != null)
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                if (c == '.')
                    x += 4;
                else
                    x += 8;
            }
        }

        protected Rectangle t指定したラベル名から難易度パネル画像の座標を取得する( string strラベル名 )
        {
            string strRawScriptFile;

            Rectangle rect = new Rectangle( 0, 0, 130, 72 );

            //ファイルの存在チェック
            if( File.Exists( CSkin.Path( @"Script\difficult.dtxs" ) ) )
            {
                //スクリプトを開く
                StreamReader reader = new StreamReader( CSkin.Path( @"Script\difficult.dtxs" ), Encoding.GetEncoding( "Shift_JIS" ) );
                strRawScriptFile = reader.ReadToEnd();

                strRawScriptFile = strRawScriptFile.Replace( Environment.NewLine, "\n" );
                string[] delimiter = { "\n" };
                string[] strSingleLine = strRawScriptFile.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );

                for( int i = 0; i < strSingleLine.Length; i++ )
                {
                    if( strSingleLine[ i ].StartsWith( "//" ) )
                        continue; //コメント行の場合は無視

                    //まずSplit
                    string[] arScriptLine = strSingleLine[ i ].Split( ',' );

                    if( ( arScriptLine.Length >= 4 && arScriptLine.Length <= 5 ) == false )
                        continue; //引数が4つか5つじゃなければ無視。

                    if( arScriptLine[ 0 ] != "6" )
                        continue; //使用するシーンが違うなら無視。

                    if( arScriptLine.Length == 4 )
                    {
                        if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                            continue; //ラベル名が違うなら無視。大文字小文字区別しない
                    }
                    else if( arScriptLine.Length == 5 )
                    {
                        if( arScriptLine[ 4 ] == "1" )
                        {
                            if( arScriptLine[ 1 ] != strラベル名 )
                                continue; //ラベル名が違うなら無視。
                        }
                        else
                        {
                            if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                                continue; //ラベル名が違うなら無視。大文字小文字区別しない
                        }
                    }
                    rect.X = Convert.ToInt32( arScriptLine[ 2 ] );
                    rect.Y = Convert.ToInt32( arScriptLine[ 3 ] );

                    reader.Close();
                    break;
                }
            }

            return rect;
        }

		//-----------------
		#endregion
	}
}
