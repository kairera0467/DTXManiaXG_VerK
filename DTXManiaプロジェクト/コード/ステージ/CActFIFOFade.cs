using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using SharpDX.Animation;
using FDK;

using SlimDXKey = SlimDX.DirectInput.Key;
using SharpDX;

namespace DTXMania
{
    // 未完成

	internal class CActFIFOFade : CActivity
	{
		// メソッド
        public void tフェードアウト開始WAM()
        {
            this.mode = EFIFOモード.フェードアウト;

            #region[ Storyboardの構築 ]
            float f速度倍率 = 0.3f;
            double dコマ秒 = 0.016;
            double 秒(double v) => ( v / f速度倍率 );
            var animation = CDTXMania.AnimationManager;
            var basetime = animation.Timer.Time;
            var start = basetime;

            C図形 図形 = this._図形[ 0 ];
            図形._ストーリーボード = new Storyboard( animation.Manager );
            図形.var画像Z軸回転度 = new Variable( animation.Manager, -135 );
            図形.var画像中心位置X = new Variable( animation.Manager, -1280 );
            図形.var画像中心位置Y = new Variable( animation.Manager, -180);

            // 初期50度 -> 180度

            using (var Z回転 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 20), 0))
            using (var X移動1 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 20), 0))
            {
                図形._ストーリーボード.AddTransition(図形.var画像Z軸回転度, Z回転);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動1);
            }
            this._図形[0]._ストーリーボード?.Schedule( start );

            start += 秒(dコマ秒 * 2);
            図形 = this._図形[ 1 ];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 568);
            図形.var画像中心位置Y = new Variable(animation.Manager, 662);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 226, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[1]._ストーリーボード?.Schedule(start);


            // タイトルロゴ
            start = basetime + 秒(dコマ秒 * 8);
            _タイトルロゴ._ストーリーボード = new Storyboard(animation.Manager);
            _タイトルロゴ.var画像不透明度 = new Variable(animation.Manager, 0);
            _タイトルロゴ.var画像中心位置X = new Variable(animation.Manager, 268);
            _タイトルロゴ.var画像中心位置Y = new Variable(animation.Manager, -240);

            // 8コマ待機
            // 不透明度変化 12コマ
            // X座標移動 20コマ 268 -> 388 (70%/30%)
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 20), 388, 0.1, 0.9))
            using (var 不透明度 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 12), 255))
            {
                _タイトルロゴ._ストーリーボード.AddTransition(_タイトルロゴ.var画像中心位置X, X移動);
                _タイトルロゴ._ストーリーボード.AddTransition(_タイトルロゴ.var画像不透明度, 不透明度);
            }
            _タイトルロゴ._ストーリーボード?.Schedule(start);

            #endregion
        }

        public void tフェードイン開始WAM()
        {
            this.mode = EFIFOモード.フェードイン;

            #region[ Storyboardの構築 ]
            float f速度倍率 = 1.0f;
            double dコマ秒 = 0.016;
            double 秒(double v) => ( v / f速度倍率 );
            var animation = CDTXMania.AnimationManager;
            var basetime = animation.Timer.Time;
            var start = basetime;

            // タイトルロゴ
            start = basetime + 秒(dコマ秒 * 8);
            _タイトルロゴ._ストーリーボード = new Storyboard(animation.Manager);
            _タイトルロゴ.var画像不透明度 = new Variable(animation.Manager, 255);
            _タイトルロゴ.var画像中心位置X = new Variable(animation.Manager, 390);
            _タイトルロゴ.var画像中心位置Y = new Variable(animation.Manager, -220);

            // 8コマ待機
            // 不透明度変化 12コマ
            // X座標移動 20コマ 268 -> 388 (70%/30%)
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 20), 388, 0.7, 0.3))
            using (var 不透明度 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 12), 0))
            {
                _タイトルロゴ._ストーリーボード.AddTransition(_タイトルロゴ.var画像中心位置X, X移動);
                _タイトルロゴ._ストーリーボード.AddTransition(_タイトルロゴ.var画像不透明度, 不透明度);
            }
            _タイトルロゴ._ストーリーボード?.Schedule(start);
            #endregion
        }

        // CActivity 実装
        public override void On活性化()
        {
            this._図形 = new C図形[2];
            for (int i = 0; i < this._図形.Length; i++)
            {
                this._図形[i] = new C図形();
            }
            this._タイトルロゴ = new Cタイトルロゴ();

            base.On活性化();
        }

        public override void On非活性化()
		{
			if( !base.b活性化してない )
			{
                foreach (C図形 s in this._図形)
                {
                    s?.Dispose();
                }
                this._図形 = null;

                this._タイトルロゴ?.Dispose();
                this._タイトルロゴ = null;

				base.On非活性化();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                // テクスチャ生成
                // 黒幕、マスクのテクスチャを生成する。
                // マスク
                int textureSizeW = SampleFramework.GameWindowSize.Width;
                int textureSizeH = SampleFramework.GameWindowSize.Height;

                Bitmap bmp = new Bitmap(textureSizeW, textureSizeH);
                Graphics graphic = Graphics.FromImage(bmp);
                graphic?.FillRectangle(Brushes.Black, 0, 0, textureSizeW, textureSizeH);
                graphic?.Dispose();

                this.txマスク = CDTXMania.tテクスチャの生成(bmp);
                bmp?.Dispose();

                // 黒幕
                textureSizeW = (int)(SampleFramework.GameWindowSize.Width * 1.5f);
                textureSizeH = (int)(SampleFramework.GameWindowSize.Height / 2.0f * 1.5f);

                bmp = new Bitmap(textureSizeW, textureSizeH);
                graphic = Graphics.FromImage(bmp);
                graphic?.FillRectangle(Brushes.Black, 0, 0, textureSizeW, textureSizeH);
                graphic?.Dispose();

                this.tx黒幕 = CDTXMania.tテクスチャの生成(bmp);
                bmp?.Dispose();

                this.txロゴ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO logo.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない || ( this._図形[ 0 ]._ストーリーボード == null ) )
			{
				return 0;
			}
            
            if( this.tx黒幕 != null )
            {
#if DEBUG
			    if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F3 ) )
			    {
                    ret--;
			    }
			    if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F4 ) )
			    {
                    ret++;
			    }
			    if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F5 ) )
			    {
                    ret = ret - 10;
			    }
			    if( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDXKey.F6 ) )
			    {
                    ret = ret + 10;
			    }

                if( this.b座標デバッグモード )
                {
                    this.tx黒幕.n透明度 = 128;
                }
#endif

                if ( this.mode == EFIFOモード.フェードアウト )
                {
                    Matrix mat;

                    // 黒幕(右)
                    //mat = Matrix.Identity;
                    //mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    //mat *= Matrix.RotationZ((float)this._図形[0].var画像Z軸回転度.Value);
                    //mat *= Matrix.Translation((float)this._図形[1].var画像中心位置X.Value, (float)this._図形[1].var画像中心位置Y.Value, 0);
                    //this.tx黒幕.t3D描画( CDTXMania.app.Device, mat );

                    // 黒幕(左)
                    // 上辺中央座標
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ(C変換.DegreeToRadian((float)this._図形[0].var画像Z軸回転度.Value));
                    mat *= Matrix.Translation((float)this._図形[0].var画像中心位置X.Value, (float)this._図形[0].var画像中心位置Y.Value, 0);
                    //mat *= Matrix.RotationZ(C変換.DegreeToRadian(0));
                    //mat *= Matrix.Translation(0, 0, 0);
                    //this.tx黒幕.t3D描画(CDTXMania.app.Device, mat, new Vector3(0, -this.tx黒幕.szテクスチャサイズ.Height / 2f, 0));
                    this.tx黒幕.t3D描画(CDTXMania.app.Device, mat);

                    // マスク
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ(0);
                    mat *= Matrix.Translation(0, 0, 0);
                    this.txマスク.n透明度 = 0;
                    //this.txマスク.t3D描画(CDTXMania.app.Device, mat);

                    // タイトルロゴ
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.Translation((float)this._タイトルロゴ.var画像中心位置X.Value, (float)this._タイトルロゴ.var画像中心位置Y.Value, 0);
                    this.txロゴ.n透明度 = (int)this._タイトルロゴ.var画像不透明度.Value;
                    this.txロゴ.t3D描画(CDTXMania.app.Device, mat);
                }
                else
                {
                    Matrix mat;

                    // タイトルロゴ
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.Translation((float)this._タイトルロゴ.var画像中心位置X.Value, (float)this._タイトルロゴ.var画像中心位置Y.Value, 0);
                    this.txロゴ.n透明度 = (int)this._タイトルロゴ.var画像不透明度.Value;
                    this.txロゴ.t3D描画(CDTXMania.app.Device, mat);
                }
#if DEBUG
                if( this.b座標デバッグモード )
                    CDTXMania.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, $"ret:{ret.ToString()}");
#endif
            }

            if ( this._タイトルロゴ._ストーリーボード.Status != StoryboardStatus.Ready )
            {
                return 0;
            }

			return 1;
		}


		// その他
		#region [ private ]
		//-----------------
		private EFIFOモード mode;
        private CTexture txロゴ;
        private CTexture tx水色;
        private CTexture tx青色;
        private CTexture tx群青;

        private CTexture txマスク;
        private CTexture tx黒幕;
#if DEBUG
        private int ret;
        private readonly bool b座標デバッグモード = true;
#endif
        //-----------------
        #endregion

        #region[ 図形描画用 ]
        protected C図形[] _図形 = new C図形[ 2 ];
        protected Cマスク _マスク;
        protected Cタイトルロゴ _タイトルロゴ;
        protected class C図形 : IDisposable
        {
            public Variable var表示;
            public Variable var画像中心位置X;
            public Variable var画像中心位置Y;
            public Variable var画像Z軸回転度;
            public Storyboard _ストーリーボード;
            
            public void Dispose()
            {
                this._ストーリーボード?.Abandon();
                this._ストーリーボード = null;

                this.var表示?.Dispose();
                this.var表示 = null;

                this.var画像中心位置X?.Dispose();
                this.var画像中心位置X = null;

                this.var画像中心位置Y?.Dispose();
                this.var画像中心位置Y = null;

                this.var画像Z軸回転度?.Dispose();
                this.var画像Z軸回転度 = null;
            }
        }

        protected class Cマスク : IDisposable
        {
            public Variable var画像不透明度;
            public Storyboard _ストーリーボード;

            public void Dispose()
            {
                this._ストーリーボード?.Abandon();
                this._ストーリーボード = null;

                this.var画像不透明度?.Dispose();
                this.var画像不透明度 = null;
            }
        }

        protected class Cタイトルロゴ : IDisposable
        {
            public Variable var画像中心位置X;
            public Variable var画像中心位置Y;
            public Variable var画像不透明度;
            public Storyboard _ストーリーボード;

            public void Dispose()
            {
                this._ストーリーボード?.Abandon();
                this._ストーリーボード = null;

                this.var画像中心位置X?.Dispose();
                this.var画像中心位置X = null;

                this.var画像中心位置Y?.Dispose();
                this.var画像中心位置Y = null;

                this.var画像不透明度?.Dispose();
                this.var画像不透明度 = null;
            }
        }
        #endregion
    }
}
