using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXCreator.譜面
{
	public class Cチップ : IComparable<Cチップ>
	{
		public bool bドラッグで選択中;
		public bool b移動済;
		public bool b確定選択中;
		public bool b裏;
		public float f値_浮動小数;
		public int n移動開始時のレーン番号0to;
		public int n移動開始時の小節内の位置grid;
		public int n移動開始時の小節番号0to;
		public int n読み込み時の解像度 = 0xc0;
		public int n枠外レーン数;
		public int nチャンネル番号00toFF
		{
			get
			{
				return this._nチャンネル番号00toFF;
			}
			set
			{
				if( ( value < 0 ) || ( value > 0xff ) )
				{
					throw new Exception( "値が範囲(0～255)を超えています。-->[" + value + "]" );
				}
				this._nチャンネル番号00toFF = value;
			}
		}
		public int nレーン番号0to
		{
			get
			{
				return this._nレーン番号0to;
			}
			set
			{
				if( value < 0 )
				{
					throw new Exception( "値が範囲を超えています。-->[" + value + "]" );
				}
				this._nレーン番号0to = value;
			}
		}
		public int n位置grid
		{
			get
			{
				return this._n位置grid;
			}
			set
			{
				this._n位置grid = value;
			}
		}
		public int n値_整数1to1295
		{
			get
			{
				return this._n値_整数1to1295;
			}
			set
			{
				if( ( value < 0 ) || ( value > 36 * 36 - 1 ) )
				{
					throw new Exception( "値が範囲(0～1295)を超えています。-->[" + value + "]" );
				}
				this._n値_整数1to1295 = value;
			}
		}
		public static readonly int nチップの高さdot = 9;
		
		public int CompareTo( Cチップ other )
		{
			return ( this.n位置grid - other.n位置grid );
		}
		public static void tOPENチップを描画する( Graphics g, Rectangle rcチップ描画領域 )
		{
			t表チップを描画する_本体( g, rcチップ描画領域, Color.White );
			string str = "O P E N";
			t表チップを描画する_番号( g, rcチップ描画領域, str );
		}
		public void tコピーfrom( Cチップ ccコピー元 )
		{
			this._nチャンネル番号00toFF = ccコピー元._nチャンネル番号00toFF;
			this._nレーン番号0to = ccコピー元._nレーン番号0to;
			this._n値_整数1to1295 = ccコピー元._n値_整数1to1295;
			this.f値_浮動小数 = ccコピー元.f値_浮動小数;
			this._n位置grid = ccコピー元._n位置grid;
			this.b裏 = ccコピー元.b裏;
			this.bドラッグで選択中 = ccコピー元.bドラッグで選択中;
			this.b確定選択中 = ccコピー元.b確定選択中;
			this.n読み込み時の解像度 = ccコピー元.n読み込み時の解像度;
			this.b移動済 = ccコピー元.b移動済;
			this.n枠外レーン数 = ccコピー元.n枠外レーン数;
			this.n移動開始時の小節番号0to = ccコピー元.n移動開始時の小節番号0to;
			this.n移動開始時のレーン番号0to = ccコピー元.n移動開始時のレーン番号0to;
			this.n移動開始時の小節内の位置grid = ccコピー元.n移動開始時の小節内の位置grid;
		}
		public static void tチップの周囲の太枠を描画する( Graphics g, Rectangle rcチップ描画領域 )
		{
			Pen pen = new Pen( Color.White, 2f );
			g.DrawRectangle( pen, rcチップ描画領域 );
			pen.Dispose();
		}
		public static void t表チップを描画する( Graphics g, Rectangle rcチップ描画領域, int nチップ番号, Color col色 )
		{
			t表チップを描画する( g, rcチップ描画領域, nチップ番号, col色, Cレーン.E種別.WAV );	// とにかくCレーン.E種別.BEAT以外を指定する
		}
		public static void t表チップを描画する( Graphics g, Rectangle rcチップ描画領域, int nチップ番号, Color col色, Cレーン.E種別 e種別 )
		{
			if ( e種別 == Cレーン.E種別.BEAT )
			{
				t表チップを描画する_本体( g, rcチップ描画領域, col色, e種別 );
			}
			else
			{
				t表チップを描画する_本体( g, rcチップ描画領域, col色 );
				if ( nチップ番号 >= 0 )
				{
					string str = C変換.str数値を36進数2桁に変換して返す( nチップ番号 );
					str = str[ 0 ] + " " + str[ 1 ];
					t表チップを描画する_番号( g, rcチップ描画領域, str );
				}
			}
		}
		public static void t表チップを描画する( Graphics g, Rectangle rcチップ描画領域, float fチップ数値, Color col色 )
		{
			t表チップを描画する_本体( g, rcチップ描画領域, col色 );
			if( fチップ数値 >= 0f )
			{
				string str = fチップ数値.ToString();
				t表チップを描画する_番号( g, rcチップ描画領域, str );
			}
		}
		public static void t裏チップを描画する( Graphics g, Rectangle rcチップ描画領域, int nチップ番号, Color col色 )
		{
			t裏チップを描画する( g, rcチップ描画領域, nチップ番号, col色, Cレーン.E種別.WAV );	// とにかくCレーン.E種別.BEAT以外を指定する
		}
		public static void t裏チップを描画する( Graphics g, Rectangle rcチップ描画領域, int nチップ番号, Color col色, Cレーン.E種別 e種別 )
		{
			t裏チップを描画する_本体( g, rcチップ描画領域, col色, e種別 );

			if ( e種別 == Cレーン.E種別.BEAT )
			{
				t裏チップを描画する_本体( g, rcチップ描画領域, col色, e種別 );
			}
			else
			{
				t裏チップを描画する_本体( g, rcチップ描画領域, col色 );
				if ( nチップ番号 >= 0 )
				{
					string str = C変換.str数値を36進数2桁に変換して返す( nチップ番号 );
					str = str[ 0 ] + " " + str[ 1 ];
					t裏チップを描画する_番号( g, rcチップ描画領域, str );
				}
			}
	
		}
		public static void t裏チップを描画する( Graphics g, Rectangle rcチップ描画領域, float fチップ数値, Color col色 )
		{
			t裏チップを描画する_本体( g, rcチップ描画領域, col色 );
			if( fチップ数値 >= 0f )
			{
				string str = fチップ数値.ToString();
				t裏チップを描画する_番号( g, rcチップ描画領域, str );
			}
		}
		protected static Font ftチップ文字用フォント = new Font( "MS Gothic", 8f, FontStyle.Bold );

		#region [ private ]
		//-----------------
		private int _nチャンネル番号00toFF;
		private int _nレーン番号0to;
		private int _n位置grid;
		private int _n値_整数1to1295;

		private static void t表チップを描画する_番号( Graphics g, Rectangle rcチップ描画領域, string str番号文字列 )
		{
			tチップを描画する_番号( g, rcチップ描画領域, str番号文字列, false );
		}

		// BEATレーン専用; ●を書く
		private static void t表チップを描画する_本体( Graphics g, Rectangle rcチップ描画領域, Color col色, Cレーン.E種別 e種別 )
		{
			if ( e種別 == Cレーン.E種別.BEAT )
			{
				tチップを描画する_本体( g, rcチップ描画領域, col色, false, true );
			}
			else
			{
				tチップを描画する_本体( g, rcチップ描画領域, col色, false, false );
			}
		}
		private static void t表チップを描画する_本体( Graphics g, Rectangle rcチップ描画領域, Color col色 )
		{
			tチップを描画する_本体( g, rcチップ描画領域, col色, false, false );
		}
		private static void t裏チップを描画する_番号( Graphics g, Rectangle rcチップ描画領域, string str番号文字列 )
		{
			tチップを描画する_番号( g, rcチップ描画領域, str番号文字列, true );
		}

		// BEATレーン専用; ●を書く
		private static void t裏チップを描画する_本体( Graphics g, Rectangle rcチップ描画領域, Color col色, Cレーン.E種別 e種別 )
		{
			if ( e種別 == Cレーン.E種別.BEAT )
			{
				tチップを描画する_本体( g, rcチップ描画領域, col色, true, true );
			}
			else
			{
				tチップを描画する_本体( g, rcチップ描画領域, col色, true, false );
			}
		}
		private static void t裏チップを描画する_本体( Graphics g, Rectangle rcチップ描画領域, Color col色 )
		{
			tチップを描画する_本体( g, rcチップ描画領域, col色, true, false );
		}

		private static void tチップを描画する_本体( Graphics g, Rectangle rcチップ描画領域, Color col色, bool b裏チップである, bool bBEATチップである )
		{
			int alphaBrush = 128;
			int alphaPen = 255;
			int alphaPen2 = 64;
			if ( b裏チップである )
			{
				rcチップ描画領域.Width -= 8;
				rcチップ描画領域.Height -= 2;
				rcチップ描画領域.X += 4;
				rcチップ描画領域.Y++;
				alphaBrush = 80;
				alphaPen = 180;
				alphaPen2 = 44;
			}
			SolidBrush brush = new SolidBrush( Color.FromArgb( alphaBrush, col色.R, col色.G, col色.B ) );
			Pen pen = new Pen( Color.FromArgb( alphaPen, col色.R, col色.G, col色.B ) );
			Pen pen2 = new Pen( Color.FromArgb( alphaPen2, col色.R, col色.G, col色.B ) );
			g.FillRectangle( brush, rcチップ描画領域 );
			g.DrawLine( pen, rcチップ描画領域.X, rcチップ描画領域.Y, rcチップ描画領域.Right, rcチップ描画領域.Y );
			g.DrawLine( pen, rcチップ描画領域.X, rcチップ描画領域.Y, rcチップ描画領域.X, rcチップ描画領域.Bottom );
			g.DrawLine( pen2, rcチップ描画領域.X, rcチップ描画領域.Bottom, rcチップ描画領域.Right, rcチップ描画領域.Bottom );
			g.DrawLine( pen2, rcチップ描画領域.Right, rcチップ描画領域.Bottom, rcチップ描画領域.Right, rcチップ描画領域.Y );
			if ( bBEATチップである )
			{
				SolidBrush brushWhite = new SolidBrush( Color.White );
				g.FillEllipse(
					brushWhite,
					( rcチップ描画領域.X + rcチップ描画領域.Width / 2.0f ) - ( rcチップ描画領域.Height / 2.0f ),
					( rcチップ描画領域.Y ),
					rcチップ描画領域.Height,
					rcチップ描画領域.Height
				);
				brushWhite.Dispose();

				if ( b裏チップである )
				{
					Pen pen3 = new Pen( Color.White );
					g.DrawLine( pen3, rcチップ描画領域.X, rcチップ描画領域.Y, rcチップ描画領域.X + rcチップ描画領域.Width, rcチップ描画領域.Y + rcチップ描画領域.Height );
					g.DrawLine( pen3, rcチップ描画領域.X, rcチップ描画領域.Y + rcチップ描画領域.Height, rcチップ描画領域.X + rcチップ描画領域.Width, rcチップ描画領域.Y );
					pen3.Dispose();
				}
			}
			brush.Dispose();
			pen.Dispose();
			pen2.Dispose();
		}
		private static void tチップを描画する_番号( Graphics g, Rectangle rcチップ描画領域, string str番号文字列, bool b裏チップである )
		{
			// 表裏に関係なく、番号描画は共通

			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Near;
			RectangleF layoutRectangle = new RectangleF();
			layoutRectangle.X = rcチップ描画領域.X;
			layoutRectangle.Y = rcチップ描画領域.Y + 1;
			layoutRectangle.Width = rcチップ描画領域.Width;
			layoutRectangle.Height = rcチップ描画領域.Height;
			g.DrawString( str番号文字列, ftチップ文字用フォント, Brushes.Black, layoutRectangle, format );
			layoutRectangle.X--;
			layoutRectangle.Y--;
			g.DrawString( str番号文字列, ftチップ文字用フォント, Brushes.White, layoutRectangle, format );
		}
		//-----------------
		#endregion
	}
}
