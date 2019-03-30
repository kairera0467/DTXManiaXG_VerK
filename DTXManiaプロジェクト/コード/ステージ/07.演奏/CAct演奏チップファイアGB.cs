using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpDX;
using FDK;

using Point = System.Drawing.Point;
namespace DTXMania
{
	internal abstract class CAct演奏チップファイアGB : CActivity
	{
		// コンストラクタ

		public CAct演奏チップファイアGB()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public virtual void Start( int nLane, int n中央X, int n中央Y, C演奏判定ライン座標共通 演奏判定ライン座標 )
		{
			if( ( nLane >= 0 ) || ( nLane <= 9 ) )
			{
				this.pt中央位置[ nLane ].X = n中央X;
				this.pt中央位置[ nLane ].Y = n中央Y;
				this.ct進行[ nLane ].t開始( 28, 56, 8, CDTXMania.Timer );		// #24736 2011.2.17 yyagi: (0, 0x38, 4,..) -> (24, 0x38, 8) に変更 ギターチップの光り始めを早くするため
				//this.nJudgeLinePosY_delta = _nJudgeLinePosY_delta;				// #31602 2013.6.24 yyagi
				this._演奏判定ライン座標 = 演奏判定ライン座標;
				this.bReverse = CDTXMania.ConfigIni.bReverse;					//
			}
		}

		public abstract void Start( int nLane, C演奏判定ライン座標共通 演奏判定ライン座標 );
//		public abstract void Start( int nLane );

		// CActivity 実装

		public override void On活性化()
		{
			for( int i = 0; i < 10; i++ )
			{
				this.ct進行[ i ] = new CCounter();
			}
			base.On活性化();
		}
		public override void On非活性化()
		{
			for( int i = 0; i < 10; i++ )
			{
				this.ct進行[ i ] = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.tx火花[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay chip fire red.png" ) );
				this.tx火花[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay chip fire green.png" ) );
				this.tx火花[ 2 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay chip fire blue.png" ) );
                this.tx火花[ 3 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay chip fire yellow.png" ) );
                this.tx火花[ 4 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay chip fire purple.png" ) );

                for( int i = 0; i < 5; i++ )
                {
				    if( this.tx火花[ i ] != null )
				    {
					    this.tx火花[ i ].b加算合成 = true;
				    }
                }
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                for( int i = 0; i < 5; i++ )
                {
				    CDTXMania.tテクスチャの解放( ref this.tx火花[ i ] );
                }
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				for( int i = 0; i < 10; i++ )
				{
					this.ct進行[ i ].t進行();
					if( this.ct進行[ i ].b終了値に達した )
					{
						this.ct進行[ i ].t停止();
					}
				}
				for( int j = 0; j < 10; j++ )
				{
					if( ( this.ct進行[ j ].n現在の経過時間ms != -1 ) && ( this.tx火花[ j % 5 ] != null ) )
					{
						float scale = (float) ( 2.0 * Math.Cos( ( Math.PI * ( 90.0 - ( 90.0 * ( ( (double) this.ct進行[ j ].n現在の値 ) / 56.0 ) ) ) ) / 180.0 ) );
						int x = (int) ( this.pt中央位置[ j ].X ) - ( (int) ( ( this.tx火花[ j % 5 ].sz画像サイズ.Width * scale ) / 2f ) );
						int y = (int) ( this.pt中央位置[ j ].Y ) - ( (int) ( ( this.tx火花[ j % 5 ].sz画像サイズ.Height * scale ) / 2f ) );
						this.tx火花[ j % 5 ].n透明度 = ( this.ct進行[ j ].n現在の値 < 0x1c ) ? 0xff : ( 0xff - ( (int) ( 255.0 * Math.Cos( ( Math.PI * ( 90.0 - ( 90.0 * ( ( (double) ( this.ct進行[ j ].n現在の値 - 0x1c ) ) / 28.0 ) ) ) ) / 180.0 ) ) ) );
						this.tx火花[ j % 5 ].vc拡大縮小倍率 = new Vector3( scale, scale, 1f );

						E楽器パート e楽器パート = ( j < 3 ) ? E楽器パート.GUITAR : E楽器パート.BASS;	// BEGIN #31602 2013.6.24 yyagi
						int deltaY = _演奏判定ライン座標.nJudgeLinePosY_delta[ (int)e楽器パート ];
						if ( this.bReverse[ (int)e楽器パート ] )
						{
							deltaY = -deltaY;
						}																				// END   #31602
						
						this.tx火花[ j % 5 ].t2D描画( CDTXMania.app.Device, x, y - deltaY );
					}
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter[] ct進行 = new CCounter[ 10 ];
		private Point[] pt中央位置 = new Point[ 10 ];
		private CTexture[] tx火花 = new CTexture[ 5 ];
		//private STDGBVALUE<int> nJudgeLinePosY_delta = new STDGBVALUE<int>();
		C演奏判定ライン座標共通 _演奏判定ライン座標 = new C演奏判定ライン座標共通();
		private STDGBVALUE<bool> bReverse = new STDGBVALUE<bool>();
		//-----------------
		#endregion
	}
}
