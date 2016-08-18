using System;
using System.Collections.Generic;
using System.Text;
using FDK;

namespace DTXMania
{
	internal class CAct演奏レーンフラッシュGB共通 : CActivity
	{
		// プロパティ

		protected CCounter[] ct進行 = new CCounter[ 10 ];
        protected CTexture txFlush;
        protected CTextureAf txFlushLine;


		// コンストラクタ

		public CAct演奏レーンフラッシュGB共通()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void Start( int nLane )
		{
			if( ( nLane < 0 ) || ( nLane > 10 ) )
			{
				throw new IndexOutOfRangeException( "有効範囲は 0～10 です。" );
			}
			this.ct進行[ nLane ] = new CCounter( 0, 100, 1, CDTXMania.Timer );
		}


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
				this.txFlush = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Guitar LaneFlush.png" ) );
                this.txFlushLine = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_Guitar Laneflush_line.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txFlush );
                CDTXMania.tテクスチャの解放( ref this.txFlushLine );
				base.OnManagedリソースの解放();
			}
		}
	}
}
