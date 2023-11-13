using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpDX.Animation;
using FDK;

using SlimDXKey = SlimDX.DirectInput.Key;
using SharpDX;

namespace DTXMania
{
	internal class CActFIFOPuzzle : CActivity
	{
		// メソッド
        public void tフェードアウト開始WAM()
        {
            this.mode = EFIFOモード.フェードアウト;

            #region[ Storyboardの構築 ]
            float f速度倍率 = 1.0f;
            double dコマ秒 = 0.016;
            double 秒(double v) => ( v / f速度倍率 );
            var animation = CDTXMania.AnimationManager;
            var basetime = animation.Timer.Time;
            var start = basetime;

            C図形 図形 = this._図形[ 0 ];
            図形._ストーリーボード = new Storyboard( animation.Manager );
            図形.var画像Z軸回転度 = new Variable( animation.Manager, C変換.DegreeToRadian( -45 ) );
            図形.var画像中心位置X = new Variable( animation.Manager, -584 );
            図形.var画像中心位置Y = new Variable( animation.Manager, -645 );

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -219, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
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

            start += 秒(dコマ秒 * 2);
            図形 = this._図形[2];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, -531);
            図形.var画像中心位置Y = new Variable(animation.Manager, 682);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 270, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[2]._ストーリーボード?.Schedule(start);

            start += 秒(dコマ秒 * 2);
            図形 = this._図形[3];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 500);
            図形.var画像中心位置Y = new Variable(animation.Manager, -728);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -286, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[3]._ストーリーボード?.Schedule(start);

            start += 秒(dコマ秒 * 2);
            図形 = this._図形[4];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, -848);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -498, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[4]._ストーリーボード?.Schedule(start);

            start += 秒(dコマ秒 * 2);
            図形 = this._図形[5];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, 804);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 544, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[5]._ストーリーボード?.Schedule(start);

            start += 秒(dコマ秒 * 2);
            図形 = this._図形[6];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 1004);
            図形.var画像中心位置Y = new Variable(animation.Manager, -226);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 364, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[6]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 2);
            図形 = this._図形[7];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 568);
            図形.var画像中心位置Y = new Variable(animation.Manager, 662);

            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 10)))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 230, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 2)))
            using (var Y移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 2)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 172, 0.1, 0.9))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 234, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[7]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 18);
            図形 = this._図形[8];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, 0);
            図形.var画像中心位置X = new Variable(animation.Manager, 0);
            図形.var画像中心位置Y = new Variable(animation.Manager, -534);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 9), -413, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[8]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 20);
            図形 = this._図形[9];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, 0);
            図形.var画像中心位置X = new Variable(animation.Manager, 0);
            図形.var画像中心位置Y = new Variable(animation.Manager, 524);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 8), 419, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[9]._ストーリーボード?.Schedule(start);

            start = basetime;
            図形 = this._図形[10];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, -584);
            図形.var画像中心位置Y = new Variable(animation.Manager, -645);

            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 10)))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -219, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 12)))
            using (var Y移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 12)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 8), -398, 0.1, 0.9))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 8), -115, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[10]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 4);
            図形 = this._図形[11];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, -531);
            図形.var画像中心位置Y = new Variable(animation.Manager, 682);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -531, 0.1, 0.9))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 270, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 8)))
            using (var Y移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 8)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -369, 0.1, 0.9))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 71, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[11]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 8);
            図形 = this._図形[12];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, -848);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -498, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 5)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), -197, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[12]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 10);
            図形 = this._図形[13];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, 804);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 544, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 3)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 13), 125, 0.1, 0.9))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[13]._ストーリーボード?.Schedule(start);

            // タイトルロゴ
            _タイトルロゴ._ストーリーボード = new Storyboard(animation.Manager);
            _タイトルロゴ.var画像不透明度 = new Variable(animation.Manager, 0);
            _タイトルロゴ.var画像中心位置X = new Variable(animation.Manager, 390);
            _タイトルロゴ.var画像中心位置Y = new Variable(animation.Manager, -220);

            using (var 不透明度 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 38), 255))
            {
                _タイトルロゴ._ストーリーボード.AddTransition(_タイトルロゴ.var画像不透明度, 不透明度);
            }
            _タイトルロゴ._ストーリーボード?.Schedule(basetime);

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

            C図形 図形 = this._図形[13];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, 125);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 9), 544, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 6)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), 804, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[13]._ストーリーボード?.Schedule(start);


            start = basetime + 秒(dコマ秒 * 2);
            図形 = this._図形[12];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, -197);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), -498, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 5)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), -848, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[12]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 4);
            図形 = this._図形[11];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, -369);
            図形.var画像中心位置Y = new Variable(animation.Manager, 71);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -531, 0.9, 0.1))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 270, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 7)))
            using (var Y移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 7)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), -531, 0.9, 0.1))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), 720, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[11]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 6);
            図形 = this._図形[10];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, -398);
            図形.var画像中心位置Y = new Variable(animation.Manager, -115);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 8), -584, 0.9, 0.1))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 8), -219, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 14)))
            using (var Y移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 14)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 10)))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -645, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[10]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 8);
            図形 = this._図形[9];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, 0);
            図形.var画像中心位置X = new Variable(animation.Manager, 0);
            図形.var画像中心位置Y = new Variable(animation.Manager, 419);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 8), 524, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[9]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 10);
            図形 = this._図形[8];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, 0);
            図形.var画像中心位置X = new Variable(animation.Manager, 0);
            図形.var画像中心位置Y = new Variable(animation.Manager, -413);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 9), -534, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[8]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 12);
            図形 = this._図形[7];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 172);
            図形.var画像中心位置Y = new Variable(animation.Manager, 234);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), 568, 0.9, 0.1))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), 230, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 2)))
            using (var Y移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 2)))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            using (var X移動 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 11)))
            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), 662, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[7]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 14);
            図形 = this._図形[6];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 364);
            図形.var画像中心位置Y = new Variable(animation.Manager, -226);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 1004, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[6]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 16);
            図形 = this._図形[5];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, 544);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), 804, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[5]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 18);
            図形 = this._図形[4];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(90));
            図形.var画像中心位置X = new Variable(animation.Manager, -498);
            図形.var画像中心位置Y = new Variable(animation.Manager, 0);

            using (var X移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), -848, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置X, X移動);
            }
            this._図形[4]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 20);
            図形 = this._図形[3];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 500);
            図形.var画像中心位置Y = new Variable(animation.Manager, -286);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 11), -728, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[3]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 22);
            図形 = this._図形[2];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, -531);
            図形.var画像中心位置Y = new Variable(animation.Manager, 270);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 720, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[2]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 24);
            図形 = this._図形[1];
            図形._ストーリーボード = new Storyboard(animation.Manager);
            図形.var画像Z軸回転度 = new Variable(animation.Manager, C変換.DegreeToRadian(-45));
            図形.var画像中心位置X = new Variable(animation.Manager, 568);
            図形.var画像中心位置Y = new Variable(animation.Manager, 226);

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), 662, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[1]._ストーリーボード?.Schedule(start);

            start = basetime + 秒(dコマ秒 * 26);
            図形 = this._図形[ 0 ];
            図形._ストーリーボード = new Storyboard( animation.Manager );
            図形.var画像Z軸回転度 = new Variable( animation.Manager, C変換.DegreeToRadian( -45 ) );
            図形.var画像中心位置X = new Variable( animation.Manager, -584 );
            図形.var画像中心位置Y = new Variable( animation.Manager, -219 );

            using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 10), -645, 0.9, 0.1))
            {
                図形._ストーリーボード.AddTransition(図形.var画像中心位置Y, Y移動);
            }
            this._図形[0]._ストーリーボード?.Schedule( start );


            // タイトルロゴ
            _タイトルロゴ._ストーリーボード = new Storyboard(animation.Manager);
            _タイトルロゴ.var画像不透明度 = new Variable(animation.Manager, 255);
            _タイトルロゴ.var画像中心位置X = new Variable(animation.Manager, 390);
            _タイトルロゴ.var画像中心位置Y = new Variable(animation.Manager, -220);

            using (var 不透明度 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 38), 0))
            {
                _タイトルロゴ._ストーリーボード.AddTransition(_タイトルロゴ.var画像不透明度, 不透明度);
            }
            _タイトルロゴ._ストーリーボード?.Schedule(basetime);
            #endregion
        }

        // CActivity 実装
        public override void On活性化()
        {
            this._図形 = new C図形[14];
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
                this.tx水色 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile lightblue.png" ) );
                this.tx黒 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile black.png" ) );
                this.tx青色 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile blue.png" ) );
                this.tx群青 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO Tile darkblue.png" ) );

                this.txロゴ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\FIFO logo.png" ) );
				base.OnManagedリソースの作成();
			}
		}
        public override void OnManagedリソースの解放()
        {
            if( !base.b活性化してない )
            {
                CDTXMania.tテクスチャの解放( ref this.tx水色 );
                CDTXMania.tテクスチャの解放( ref this.tx黒 );
                CDTXMania.tテクスチャの解放( ref this.tx青色 );
                CDTXMania.tテクスチャの解放( ref this.tx群青 );

                CDTXMania.tテクスチャの解放( ref this.txロゴ );
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
		{
			if( base.b活性化してない || ( this._図形[ 0 ]._ストーリーボード == null ) )
			{
				return 0;
			}
            
            if( this.tx水色 != null && this.tx群青 != null && this.tx青色 != null && this.tx黒 != null )
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
                    this.tx水色.n透明度 = 128;
                    this.tx群青.n透明度 = 128;
                    this.tx青色.n透明度 = 128;
                    this.tx黒.n透明度 = 128;
                }
#endif

                if ( this.mode == EFIFOモード.フェードアウト )
                {
                    Matrix mat;

                    // 14 群青(右)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[13].var画像中心位置X.Value, (float)this._図形[13].var画像中心位置Y.Value, 0);
                    this.tx群青.t3D描画(CDTXMania.app.Device, mat);

                    // 13 青(左)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[12].var画像中心位置X.Value, (float)this._図形[12].var画像中心位置Y.Value, 0);
                    this.tx青色.t3D描画(CDTXMania.app.Device, mat);

                    // 12 水色(左上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[11].var画像中心位置X.Value, (float)this._図形[11].var画像中心位置Y.Value, 0);
                    this.tx水色.t3D描画( CDTXMania.app.Device, mat);

                    // 11 黒(左下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[10].var画像中心位置X.Value, (float)this._図形[10].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画( CDTXMania.app.Device, mat);

                    // 10 青(上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.Translation((float)this._図形[9].var画像中心位置X.Value, (float)this._図形[9].var画像中心位置Y.Value, 0);
                    this.tx青色.t3D描画( CDTXMania.app.Device, mat);

                    // 9 黒(下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.Translation((float)this._図形[8].var画像中心位置X.Value, (float)this._図形[8].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画( CDTXMania.app.Device, mat );

                    // 8 黒(右上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[7].var画像中心位置X.Value, (float)this._図形[7].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画( CDTXMania.app.Device, mat );

                    // 7 水色(右下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[6].var画像中心位置X.Value, (float)this._図形[6].var画像中心位置Y.Value, 0);
                    this.tx水色.t3D描画( CDTXMania.app.Device, mat );

                    // 6 黒(右)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[5].var画像中心位置X.Value, (float)this._図形[5].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画( CDTXMania.app.Device, mat );

                    // 5 群青(左)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[4].var画像中心位置X.Value, (float)this._図形[4].var画像中心位置Y.Value, 0);
                    this.tx群青.t3D描画( CDTXMania.app.Device, mat );

                    // 4 青(右下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[3].var画像中心位置X.Value, (float)this._図形[3].var画像中心位置Y.Value, 0);
                    this.tx青色.t3D描画( CDTXMania.app.Device, mat );

                    // 3 黒(左上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[2].var画像中心位置X.Value, (float)this._図形[2].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画( CDTXMania.app.Device, mat );

                    // 2 群青(右上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[1].var画像中心位置X.Value, (float)this._図形[1].var画像中心位置Y.Value, 0);
                    this.tx群青.t3D描画( CDTXMania.app.Device, mat );

                    // 1 水色(左下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[0].var画像中心位置X.Value, (float)this._図形[0].var画像中心位置Y.Value, 0);
                    this.tx水色.t3D描画( CDTXMania.app.Device, mat );

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

                    // 14 群青(右)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[13].var画像中心位置X.Value, (float)this._図形[13].var画像中心位置Y.Value, 0);
                    this.tx群青.t3D描画(CDTXMania.app.Device, mat);

                    // 13 青(左)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[12].var画像中心位置X.Value, (float)this._図形[12].var画像中心位置Y.Value, 0);
                    this.tx青色.t3D描画(CDTXMania.app.Device, mat);

                    // 12 水色(左上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[11].var画像中心位置X.Value, (float)this._図形[11].var画像中心位置Y.Value, 0);
                    this.tx水色.t3D描画(CDTXMania.app.Device, mat);

                    // 11 黒(左下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[10].var画像中心位置X.Value, (float)this._図形[10].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画(CDTXMania.app.Device, mat);

                    // 10 青(上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.Translation((float)this._図形[9].var画像中心位置X.Value, (float)this._図形[9].var画像中心位置Y.Value, 0);
                    this.tx青色.t3D描画(CDTXMania.app.Device, mat);

                    // 9 黒(下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.Translation((float)this._図形[8].var画像中心位置X.Value, (float)this._図形[8].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画(CDTXMania.app.Device, mat);

                    // 8 黒(右上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[7].var画像中心位置X.Value, (float)this._図形[7].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画(CDTXMania.app.Device, mat);

                    // 7 水色(右下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[6].var画像中心位置X.Value, (float)this._図形[6].var画像中心位置Y.Value, 0);
                    this.tx水色.t3D描画(CDTXMania.app.Device, mat);

                    // 6 黒(右)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[5].var画像中心位置X.Value, (float)this._図形[5].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画(CDTXMania.app.Device, mat);

                    // 5 群青(左)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(90));
                    mat *= Matrix.Translation((float)this._図形[4].var画像中心位置X.Value, (float)this._図形[4].var画像中心位置Y.Value, 0);
                    this.tx群青.t3D描画(CDTXMania.app.Device, mat);

                    // 4 青(右下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[3].var画像中心位置X.Value, (float)this._図形[3].var画像中心位置Y.Value, 0);
                    this.tx青色.t3D描画(CDTXMania.app.Device, mat);

                    // 3 黒(左上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(45));
                    mat *= Matrix.Translation((float)this._図形[2].var画像中心位置X.Value, (float)this._図形[2].var画像中心位置Y.Value, 0);
                    this.tx黒.t3D描画(CDTXMania.app.Device, mat);

                    // 2 群青(右上)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[1].var画像中心位置X.Value, (float)this._図形[1].var画像中心位置Y.Value, 0);
                    this.tx群青.t3D描画(CDTXMania.app.Device, mat);

                    // 1 水色(左下)
                    mat = Matrix.Identity;
                    mat *= Matrix.Scaling(1.0f, 1.0f, 1.0f);
                    mat *= Matrix.RotationZ((float)C変換.DegreeToRadian(-45));
                    mat *= Matrix.Translation((float)this._図形[0].var画像中心位置X.Value, (float)this._図形[0].var画像中心位置Y.Value, 0);
                    this.tx水色.t3D描画(CDTXMania.app.Device, mat);

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
        private CTexture tx黒;
#if DEBUG
        private int ret;
        private readonly bool b座標デバッグモード = false;
#endif
        //-----------------
        #endregion

        #region[ 図形描画用 ]
        protected C図形[] _図形 = new C図形[ 14 ];
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
