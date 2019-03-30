using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FDK;
using SharpDX.Animation;

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

            this._BPMバー = new BPMバー[] { new BPMバー() };
			base.On活性化();
		}
        public override void On非活性化()
        {
            foreach( BPMバー s in this._BPMバー )
            {
                s?.Dispose();
            }
            this._BPMバー = null;

            base.On非活性化();
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
                this.txBPMバー?.Dispose();
				base.OnManagedリソースの解放();
			}
		}

        /// <summary>
        /// 1拍ごとにStoryboardを構築するのは効率が悪いため、BPMの変化が起こった場合のみ構築するようにしてみる。
        /// </summary>
        /// <param name="dbBPM"></param>
        public void tStoryboard構築( double dbBPM )
        {
            #region[ Storyboardの構築 ]
            float f速度倍率 = 1.0f;
            double 秒( double v ) => ( v / f速度倍率 );
            var animation = CDTXMania.AnimationManager;

            var バー = this._BPMバー[ 0 ];
            バー.Dispose();
            バー.左上位置X = new Variable( animation.Manager, 0 );
            バー.左上位置Y = new Variable( animation.Manager, 0 );
            バー.不透明度 = new Variable( animation.Manager, 0 );
            バー.ストーリーボード = new Storyboard( animation.Manager );
            
            // バー右側
            // BPM100を基準としている
            // 0.06 0から最大まで開く
            double db開く時間 = 0.06 / ( dbBPM / 100.0 );
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(db開く時間), 9, 0.3, 0.7))
            using (var 不透明度 = animation.TrasitionLibrary.AccelerateDecelerate(秒(db開く時間), 255, 0.3, 0.7))
            {
                バー.ストーリーボード.AddTransition(バー.左上位置X, X移動);
                バー.ストーリーボード.AddTransition( バー.不透明度, 不透明度 );
            }

            // 0.50 最大から0まで閉じる
            double db閉じる時間 = 0.5 / ( dbBPM / 100.0 );
            using ( var X移動 = animation.TrasitionLibrary.Linear( 秒( db閉じる時間 ), 0 ) )
            using ( var 不透明度 = animation.TrasitionLibrary.Linear( 秒( db閉じる時間 ), 0 ) )
            {
                バー.ストーリーボード.AddTransition( バー.左上位置X, X移動 );
                バー.ストーリーボード.AddTransition( バー.不透明度, 不透明度 );
            }

            #endregion
        }

        public void tStoryboard実行()
        {
            if( this._BPMバー[ 0 ] != null )
            {
                var animation = CDTXMania.AnimationManager;
                var basetime = animation.Timer.Time;
                var start = basetime;

                this._BPMバー[ 0 ].ストーリーボード.Schedule( start );
            }
        }

        public void tStoryboard消去()
        {
            this._BPMバー[ 0 ] = null;
        }

        protected class BPMバー : IDisposable
        {
            public Variable 左上位置X;
            public Variable 左上位置Y;
            public Variable 不透明度;
            public Storyboard ストーリーボード;

            public void Dispose()
            {
                this.ストーリーボード?.Abandon();
                this.ストーリーボード = null;

                this.左上位置X?.Dispose();
                this.左上位置X = null;

                this.左上位置Y?.Dispose();
                this.左上位置Y = null;

                this.不透明度?.Dispose();
                this.不透明度 = null;
            }
        }
        protected BPMバー[] _BPMバー = null;
    }
}
