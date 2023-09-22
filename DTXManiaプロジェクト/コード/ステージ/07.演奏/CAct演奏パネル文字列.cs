using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SharpDX;
using FDK;

using Rectangle = System.Drawing.Rectangle;
namespace DTXMania
{
	internal class CAct演奏パネル文字列 : CActivity
	{

		// コンストラクタ

		public CAct演奏パネル文字列()
		{
			base.b活性化してない = true;
			this.strパネル文字列 = "";
			this.Start();
		}
		
		
		// メソッド

		public void SetPanelString( string str )
		{
			this.strパネル文字列 = str;
			if( base.b活性化してる )
			{
				CDTXMania.tテクスチャの解放( ref this.txPanel );
				if( ( this.strパネル文字列 != null ) && ( this.strパネル文字列.Length > 0 ) )
				{
					Bitmap image = new Bitmap( 1, 1 );
					Graphics graphics = Graphics.FromImage( image );
					graphics.PageUnit = GraphicsUnit.Pixel;
					this.n文字列の長さdot = (int) graphics.MeasureString( this.strパネル文字列, this.ft表示用フォント ).Width;
					graphics.Dispose();
					try
					{
						Bitmap bitmap2 = new Bitmap( this.n文字列の長さdot, (int) this.ft表示用フォント.Size );
						graphics = Graphics.FromImage( bitmap2 );
						graphics.DrawString( this.strパネル文字列, this.ft表示用フォント, Brushes.Red, (float) 0f, (float) 0f );
						graphics.Dispose();
						this.txPanel = new CTexture( CDTXMania.app.Device, bitmap2, CDTXMania.TextureFormat );
						this.txPanel.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
						bitmap2.Dispose();
					}
					catch( CTextureCreateFailedException )
					{
						Trace.TraceError( "パネル文字列テクスチャの生成に失敗しました。" );
						this.txPanel = null;
					}
					this.ct進行用 = new CCounter( (int) ( -278 * Scale.X ), this.n文字列の長さdot / 2, unchecked( (int) ( 12.0f / Scale.X ) ), CDTXMania.Timer );
				}
				this.Start();
			}
		}

		public void Stop()
		{
			this.bMute = true;
		}
		public void Start()
		{
			this.bMute = false;
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.ft表示用フォント = new Font( "MS PGothic", 48f * Scale.Y, FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Pixel );
			this.n文字列の長さdot = 0;
			this.txPanel = null;
			this.ct進行用 = new CCounter();
			this.Start();
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ft表示用フォント != null )
			{
				this.ft表示用フォント.Dispose();
				this.ft表示用フォント = null;
			}
			CDTXMania.tテクスチャの解放( ref this.txPanel );
			this.ct進行用 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.SetPanelString( this.strパネル文字列 );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txPanel );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(x,y)のほうを使用してください。" );
		}
		public int t進行描画( int x, int y )
		{
			if( !base.b活性化してない && !this.bMute )
			{
				this.ct進行用.t進行Loop();
				if( ( string.IsNullOrEmpty( this.strパネル文字列 ) || ( this.txPanel == null ) ) || ( this.ct進行用 == null ) )
				{
					return 0;
				}
				float magX = this.txPanel.vc拡大縮小倍率.X;
				Rectangle rectangle = new Rectangle(
					(int) ( ( (float) this.ct進行用.n現在の値 ) / magX ),
					0,
					(CDTXMania.ConfigIni.eドラムレーン表示位置 == Eドラムレーン表示位置.Left)?
						(int) ( 278f / magX * Scale.X ) :
						(int) ( 180f / magX * Scale.X ),
					(int) this.ft表示用フォント.Size
					);
				if ( rectangle.X < 0 )
				{
					x -= (int) ( rectangle.X * magX / Scale.X );
					rectangle.Width += rectangle.X;
					rectangle.X = 0;
				}
				if( rectangle.Right >= this.n文字列の長さdot )
				{
					rectangle.Width -= rectangle.Right - this.n文字列の長さdot;
				}
				this.txPanel.t2D描画( CDTXMania.app.Device, x * Scale.X, y * Scale.Y, rectangle );
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter ct進行用;
		private Font ft表示用フォント;
		private int n文字列の長さdot;
		private string strパネル文字列;
		private CTexture txPanel;
		private bool bMute;
		//-----------------
		#endregion
	}
}
