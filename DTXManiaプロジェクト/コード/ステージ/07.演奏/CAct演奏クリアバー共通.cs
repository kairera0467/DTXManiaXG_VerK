using FDK;
using SharpDX.Animation;
using System;

namespace DTXMania
{
	internal class CAct演奏クリアバー共通 : CActivity
	{

		// コンストラクタ

		public CAct演奏クリアバー共通()
		{
			base.b活性化してない = true;
		}
		
		// メソッド

		// CActivity 実装

		public override void On活性化()
		{
            this.dbNowPos = 0;
            this.dbEndPos = 0;
            this.n現在の区間 = 0;
            this.db現在の曲進行割合 = 0;
            for( int i = 0; i < 3; i++ )
            {
                this.bClearBar[ i ] = new bool[ SECTION_COUNT ];
                this.b区間内でミスをした[ i ] = false;
            }
            this.db区間位置 = new double[ SECTION_COUNT ];
            this.dbEndPos = ( CDTXMania.DTX.listChip.Count > 0 ) ? CDTXMania.DTX.listChip[ CDTXMania.DTX.listChip.Count - 1 ].n発声時刻ms / 1000.0 : 0;
                    
            double db区間 = this.dbEndPos / (double)SECTION_COUNT;
            for( int i = 0; i < SECTION_COUNT; i++ )
            {
                db区間位置[ i ] = db区間 * i;
            }
            this._カーソル = new カーソル();
            base.On活性化();
		}
		public override void On非活性化()
		{
            for( int i = 0; i < 3; i++ )
            {
                this.bClearBar[ i ] = null;
                this.b区間内でミスをした[ i ] = false;
            }  
            this.db区間位置 = null;

            this._カーソル?.Dispose();
            this._カーソル = null;
            base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
            return 0;
		}

        /// <summary>
        /// クリアバーを塗る
        /// </summary>
        protected void tクリアゲージ判定()
        {
            for( int i = 0; i < 3; i++ )
            {
                if( !this.b区間内でミスをした[ i ] )
                {
                    if( this.n現在の区間 > 0 ) {
                        this.bClearBar[ i ][ this.n現在の区間 - 1 ] = true;
                    }
                }
                this.b区間内でミスをした[ i ] = false;
            }
        }
        
        public void t区間内ミス通知( E楽器パート ePart )
        {
            this.b区間内でミスをした[ (int)ePart ] = true;
        }

        public void カーソルアニメ実行()
        {
            this._カーソル.アニメーション開始(CDTXMania.AnimationManager);
        }

        protected const int SECTION_COUNT = 64;
        protected STDGBVALUE<bool[]> bClearBar; //各パートごとにフラグを用意する。
        protected double[] db区間位置;
        protected int n現在の区間;

        protected double dbNowPos;
        protected double dbEndPos;
        protected double db現在の曲進行割合;
        protected STDGBVALUE<bool> b区間内でミスをした;

        protected class カーソル : IDisposable
        {
            public Variable X座標差分;
            public Variable エフェクト拡大率;
            public Variable エフェクト不透明度;
            public Storyboard ストーリーボード;


            public virtual void Dispose()
            {
                this.ストーリーボード?.Abandon();
                this.ストーリーボード = null;

                this.X座標差分?.Dispose();
                this.X座標差分 = null;

                this.エフェクト不透明度?.Dispose();
                this.エフェクト不透明度 = null;

                this.エフェクト拡大率?.Dispose();
                this.エフェクト拡大率 = null;
            }

            public void アニメーション開始( CAnimationManager am )
            {
                this.Dispose();

                // 1～28 開く動作
                // それ移行 動かない
                // 1拍過ぎた時 1コマ目に戻す
                float f速度倍率 = 1.0f;
                double dコマ秒 = 0.016;
                double 秒(double v) => (v / f速度倍率);

                // 矢印開度
                X座標差分 = new Variable(am.Manager, 0);
                ストーリーボード = new Storyboard(am.Manager);

                using (var X移動 = am.TrasitionLibrary.Linear(秒(dコマ秒 * 28), 18))
                {
                    ストーリーボード.AddTransition(X座標差分, X移動);
                }

                this.ストーリーボード?.Schedule(am.Timer.Time);
            }
        }
        protected カーソル _カーソル = null;
    }
}
