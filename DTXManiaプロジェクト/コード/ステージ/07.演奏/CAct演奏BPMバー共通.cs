using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FDK;

namespace DTXMania
{
	internal class CAct演奏BPMバー共通 : CActivity
	{
		// プロパティ

        protected CTexture txBPMバー;
        public CCounter ctBPMバー;
        public double UnitTime;
        public bool bサビ区間;

        //2015.09.30 kairera0467 画像を統合してみる。

		// コンストラクタ

		public CAct演奏BPMバー共通()
		{
			base.b活性化してない = true;
		}
		
		
		// メソッド

		// CActivity 実装

		public override void On活性化()
		{
            this.ctBPMバー = null;
            this.UnitTime = 0.0;
            this.bサビ区間 = false;
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txBPMバー = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_BPMBar.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txBPMバー );
				base.OnManagedリソースの解放();
			}
		}
	}
}
