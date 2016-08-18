using System;
using System.Collections.Generic;
using System.Text;
using FDK;

namespace DTXMania
{
	internal class CAct演奏RGB共通 : CActivity
	{
		// プロパティ

		protected bool[] b押下状態 = new bool[ 10 ];
		protected CTexture txRGB;


		// コンストラクタ

		public CAct演奏RGB共通()
		{
			base.b活性化してない = true;
		}
		
		
		// メソッド

		public void Push( int nLane )
		{
			this.b押下状態[ nLane ] = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
			for( int i = 0; i < 10; i++ )
			{
				this.b押下状態[ i ] = false;
			}
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txRGB = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_RGB buttons.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txRGB );
				base.OnManagedリソースの解放();
			}
		}

		public virtual int t進行描画( C演奏判定ライン座標共通 演奏判定ライン座標共通 )
		{
			return 0;
		}
	}
}
