using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SharpDX.Animation;
using FDK;

namespace DTXMania
{
	internal class CAct演奏判定文字列共通 : CActivity
	{
		// プロパティ
        public int iP_A;
        public int iP_B;
        protected STSTATUS[] st状態 = new STSTATUS[15];
        [StructLayout(LayoutKind.Sequential)]
        protected struct STSTATUS
        {
            public CCounter ct進行;
            public E判定 judge;
            public float fZ軸回転度_棒;
            public float fX方向拡大率_棒;
            public float fY方向拡大率_棒;
            public int n相対X座標_棒;
            public int n相対Y座標_棒;


            public float fZ軸回転度;
            public float fX方向拡大率;
            public float fY方向拡大率;
            public int n相対X座標;
            public int n相対Y座標;

            public float fX方向拡大率B;
            public float fY方向拡大率B;
            public int n相対X座標B;
            public int n相対Y座標B;
            public int n透明度B;

            public int n透明度;
            public int nLag;								// #25370 2011.2.1 yyagi
            public int nRect;
        }

		protected readonly ST判定文字列[] st判定文字列;
		[StructLayout( LayoutKind.Sequential )]
		protected struct ST判定文字列
		{
			public int n画像番号;
			public Rectangle rc;
		}

		protected readonly STlag数値[] stLag数値;			// #25370 2011.2.1 yyagi
		[StructLayout( LayoutKind.Sequential )]
		protected struct STlag数値
		{
			public Rectangle rc;
		}

		
		protected CTextureAf[] tx判定文字列 = new CTextureAf[ 3 ];
		protected CTexture txlag数値 = new CTexture();		// #25370 2011.2.1 yyagi

		public int nShowLagType							// #25370 2011.6.3 yyagi
		{
			get;
			set;
		}

		[StructLayout( LayoutKind.Sequential )]
		protected struct STレーンサイズ
		{
			public int x;
			public int w;
			public STレーンサイズ( int x_, int w_ )
			{
				x = x_;
				w = w_;
			}

		}

        protected C判定文字[] _判定文字 = new C判定文字[ 15 ];

        protected class C判定文字 : IDisposable
        {
            public bool b表示;
            public E判定 e判定;
            public Variable var文字中心位置X;
            public Variable var文字中心位置Y;
            public Variable var文字拡大率X;
            public Variable var文字拡大率Y;
            public Variable var文字不透明度;
            public Variable var文字Z軸回転度;
            public Variable var文字オーバーレイ中心位置X;
            public Variable var文字オーバーレイ中心位置Y;
            public Variable var文字オーバーレイ拡大率X;
            public Variable var文字オーバーレイ拡大率Y;
            public Variable var文字オーバーレイ不透明度;
            public Variable var文字オーバーレイZ軸回転度;
            public Variable var棒拡大率X;
            public Variable var棒拡大率Y;
            public Variable var棒Z軸回転度;
            public Storyboard ストーリーボード;
            public Storyboard ストーリーボード_オーバーレイ;
            public Storyboard ストーリーボード_棒;
            public Rectangle rect画像範囲;
            public Rectangle rect棒範囲;

            public void Dispose()
            {
                this.b表示 = false;

                this.ストーリーボード?.Abandon();
                this.ストーリーボード = null;

                this.ストーリーボード_オーバーレイ?.Abandon();
                this.ストーリーボード_オーバーレイ = null;

                this.ストーリーボード_棒?.Abandon();
                this.ストーリーボード_棒 = null;

                this.var文字中心位置X?.Dispose();
                this.var文字中心位置X = null;

                this.var文字中心位置Y?.Dispose();
                this.var文字中心位置Y = null;

                this.var文字拡大率X?.Dispose();
                this.var文字拡大率X = null;

                this.var文字拡大率Y?.Dispose();
                this.var文字拡大率Y = null;

                this.var文字不透明度?.Dispose();
                this.var文字不透明度 = null;

                this.var文字Z軸回転度?.Dispose();
                this.var文字Z軸回転度 = null;

                this.var文字オーバーレイ中心位置X?.Dispose();
                this.var文字オーバーレイ中心位置X = null;

                this.var文字オーバーレイ中心位置Y?.Dispose();
                this.var文字オーバーレイ中心位置Y = null;

                this.var文字オーバーレイ拡大率X?.Dispose();
                this.var文字オーバーレイ拡大率X = null;

                this.var文字オーバーレイ拡大率Y?.Dispose();
                this.var文字オーバーレイ拡大率Y = null;

                this.var文字オーバーレイZ軸回転度?.Dispose();
                this.var文字オーバーレイZ軸回転度 = null;

                this.var文字オーバーレイ不透明度?.Dispose();
                this.var文字オーバーレイ不透明度 = null;

                this.var棒拡大率X?.Dispose();
                this.var棒拡大率X = null;

                this.var棒拡大率Y?.Dispose();
                this.var棒拡大率Y = null;

                this.var棒Z軸回転度?.Dispose();
                this.var棒Z軸回転度 = null;
            }
        }

        protected bool bShow;

		// コンストラクタ

		public CAct演奏判定文字列共通()
		{
            //2016.02.16 kairera0467 X-Perfect判定追加により、7から8に増加。
			this.st判定文字列 = new ST判定文字列[ 8 ];
			Rectangle[] r = new Rectangle[] {
				new Rectangle( 0, 256 / 3 * 0, 256, 256 / 3 ),		// Perfect
				new Rectangle( 0, 256 / 3 * 1, 256, 256 / 3 ),		// Great
				new Rectangle( 0, 256 / 3 * 2, 256, 256 / 3 ),		// Good
				new Rectangle( 0, 256 / 3 * 0, 256, 256 / 3 ),		// Poor
				new Rectangle( 0, 256 / 3 * 1, 256, 256 / 3 ),		// Miss
				new Rectangle( 0, 256 / 3 * 2, 256, 256 / 3 ),		// Bad
				new Rectangle( 0, 256 / 3 * 0, 256, 256 / 3 ),		// X-Perfect
				new Rectangle( 0, 256 / 3 * 0, 256, 256 / 3 )		// Auto

			};
			for ( int i = 0; i < 8; i++ )
			{
				this.st判定文字列[ i ] = new ST判定文字列();
				this.st判定文字列[ i ].n画像番号 = i / 3;
				this.st判定文字列[ i ].rc = r[i];
			}
            int iP_A = 390;
            int iP_B = 584;
			this.stLag数値 = new STlag数値[ 12 * 3 ];       // #25370 2011.2.1 yyagi
            // #32093 2019.7.20 kairera0467 12 * 2 → 12 * 3

            for ( int i = 0; i < 12; i++ )
			{
				this.stLag数値[ i      ].rc = new Rectangle( ( i % 4 ) * 15     , ( i / 4 ) * 19     , 15, 19 );	// plus numbers
				this.stLag数値[ i + 12 ].rc = new Rectangle( ( i % 4 ) * 15 + 64, ( i / 4 ) * 19 + 64, 15, 19 );	// minus numbers
			}

			base.b活性化してない = true;
		}


		// メソッド
        public virtual void Start( int nLane, E判定 judge )
        {
            this.Start( nLane, judge, 0, true );
        }

        public virtual void Start( int nLane, E判定 judge, int lag )
        {
            this.Start( nLane, judge, lag, true );
        }

		public virtual void Start( int nLane, E判定 judge, int lag, bool bShow )
		{
			if( ( nLane < 0 ) || ( nLane > 14 ) )
			{
				throw new IndexOutOfRangeException( "有効範囲は 0～14 です。" );
			}
			if( ( ( nLane >= 10 ) || ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Drums ) != E判定文字表示位置.表示OFF ) ) && ( ( ( nLane != 13 ) || ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Guitar ) != E判定文字表示位置.表示OFF ) ) && ( ( nLane != 14 ) || ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Bass ) != E判定文字表示位置.表示OFF ) ) ) )
			{
                switch( CDTXMania.ConfigIni.eJudgeAnimeType )
                {
                    case Eタイプ.A:
                        this.st状態[ nLane ].ct進行 = new CCounter( 0, 300, 1, CDTXMania.Timer );
                        break;
                    case Eタイプ.B:
                        this.st状態[ nLane ].ct進行 = new CCounter( 0, CDTXMania.ConfigIni.nJudgeFrames - 1, CDTXMania.ConfigIni.nJudgeInterval, CDTXMania.Timer );
                        break;
                    case Eタイプ.C:
                        this._判定文字[ nLane ] = new C判定文字();
                        this.StartWAM( nLane, judge, 0 );
                        break;
                }

				this.st状態[ nLane ].judge = judge;
				this.st状態[ nLane ].fX方向拡大率 = 1f;
				this.st状態[ nLane ].fY方向拡大率 = 1f;
                this.st状態[ nLane ].fZ軸回転度 = 0f;
				this.st状態[ nLane ].n相対X座標 = 0;
				this.st状態[ nLane ].n相対Y座標 = 0;
                this.st状態[ nLane ].n透明度 = 0xff;

				this.st状態[ nLane ].fX方向拡大率B = 1f;
				this.st状態[ nLane ].fY方向拡大率B = 1f;
				this.st状態[ nLane ].n相対X座標B = 0;
				this.st状態[ nLane ].n相対Y座標B = 0;
                this.st状態[ nLane ].n透明度B = 0xff;
                
                this.st状態[ nLane ].fZ軸回転度_棒 = 0f;
                this.st状態[ nLane ].fX方向拡大率_棒 = 0;
				this.st状態[ nLane ].fY方向拡大率_棒 = 0;
				this.st状態[ nLane ].n相対X座標_棒 = 0;
				this.st状態[ nLane ].n相対Y座標_棒 = 0;                

				this.st状態[ nLane ].nLag = lag;
                
                this.bShow = bShow;
			}
		}

        /// <summary>
        /// WindowsAnimationManagerを使用した判定アニメーションで使用
        /// </summary>
        /// <param name="nLane">レーン</param>
        /// <param name="judge">判定</param>
        /// <param name="lag">ラグ時間ms</param>
        public virtual void StartWAM( int nLane, E判定 judge, int lag )
        {
            // WAMのテスト用
            // 最終形は演奏開始時(またはスキン準備時)に各判定のストーリーボードを生成、それを判定アニメ時に呼び出す形にしたい。
            #region[ Storyboardの構築 ]
            float f速度倍率 = 1.0f;
            double dコマ秒 = 0.016;
            double 秒(double v) => ( v / f速度倍率 );
            var animation = CDTXMania.AnimationManager;

            //this._判定文字[ nLane ].Dispose();

            var 文字 = this._判定文字[ nLane ];
            文字.e判定 = judge;
            文字.ストーリーボード = new Storyboard( animation.Manager );
            文字.var文字オーバーレイ拡大率X = new Variable( animation.Manager, 1.0 );
            文字.var文字オーバーレイ拡大率Y = new Variable( animation.Manager, 1.0 );
            文字.var文字オーバーレイ不透明度 = new Variable( animation.Manager, 0 );
            文字.rect棒範囲 = new Rectangle( 0, 0, 1, 1 );

            switch( judge )
            {
                case E判定.Perfect:
                    文字.var文字中心位置Y = new Variable( animation.Manager, 0 );
                    文字.var文字拡大率X = new Variable( animation.Manager, 1.66 );
                    文字.var文字拡大率Y = new Variable( animation.Manager, 1.66 );
                    文字.var文字Z軸回転度 = new Variable( animation.Manager, -C変換.DegreeToRadian( -15 ) );
                    文字.var文字不透明度 = new Variable( animation.Manager, 255 );

                    文字.var文字オーバーレイZ軸回転度 = new Variable( animation.Manager, -C変換.DegreeToRadian( -15 ) );

                    文字.var棒拡大率X = new Variable( animation.Manager, 0.63 );
                    文字.var棒拡大率Y = new Variable( animation.Manager, 1.0 );
                    文字.var棒Z軸回転度 = new Variable( animation.Manager, -C変換.DegreeToRadian(43.0) );

                    文字.ストーリーボード_棒 = new Storyboard( animation.Manager );
                    文字.ストーリーボード_オーバーレイ = new Storyboard( animation.Manager );

                    // オーバーレイ
                    using ( var 拡大率X = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    using ( var 拡大率Y = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    using ( var 不透明度 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率X, 拡大率X );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率Y, 拡大率Y );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }
                    using ( var 不透明度 = animation.TrasitionLibrary.Instantaneous( 255 ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }
                    using ( var 拡大率X = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 12 ), 1.66 ) )
                    using ( var 拡大率Y = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 12 ), 1.66 ) )
                    using ( var 不透明度 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 8 ) ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率X, 拡大率X );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率Y, 拡大率Y );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }
                    using ( var 不透明度 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 5 ), 0 ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }

                    // 文字
                    using ( var X拡大 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 1.0 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 1.0 ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 17 ) ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 17 ) ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 1.91, 0.6, 0.4 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 0.23, 0.6, 0.4 ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 4 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                    }

                    // 棒
                    using ( var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 3 ), 1.25, 0.6, 0.4 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( 0.013 * 3 ) ) )
                    using ( var Z回転 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 3 ), C変換.DegreeToRadian( 18.0 ), 0.6, 0.4 ) )
                    {
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率X, X拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率Y, Y拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 15 ) ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 15 ) ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 15 ), C変換.DegreeToRadian( 43.0 ) ) )
                    {
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率X, X拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率Y, Y拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 0.0, 0.6, 0.4 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 0.0, 0.6, 0.4 ) )
                    {
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率X, X拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率Y, Y拡大 );
                    }

                    break;
                case E判定.Great:
                    文字.var文字中心位置Y = new Variable( animation.Manager, 0 );
                    文字.var文字拡大率X = new Variable( animation.Manager, 1.66 );
                    文字.var文字拡大率Y = new Variable( animation.Manager, 1.66 );
                    文字.var文字Z軸回転度 = new Variable( animation.Manager, -C変換.DegreeToRadian( -15 ) );
                    文字.var文字不透明度 = new Variable( animation.Manager, 255 );

                    文字.var文字オーバーレイ中心位置Y = new Variable( animation.Manager, 0 );
                    文字.var文字オーバーレイ拡大率X = new Variable( animation.Manager, 0 );
                    文字.var文字オーバーレイ拡大率Y = new Variable( animation.Manager, 0 );
                    文字.var文字オーバーレイZ軸回転度 = new Variable( animation.Manager, -C変換.DegreeToRadian( -15 ) );
                    文字.var文字オーバーレイ不透明度 = new Variable( animation.Manager, 0 );

                    文字.var棒拡大率X = new Variable( animation.Manager, 0.63 );
                    文字.var棒拡大率Y = new Variable( animation.Manager, 1.0 );
                    文字.var棒Z軸回転度 = new Variable( animation.Manager, -C変換.DegreeToRadian(43.0) );

                    文字.ストーリーボード_棒 = new Storyboard( animation.Manager );
                    文字.ストーリーボード_オーバーレイ = new Storyboard( animation.Manager );

                    // オーバーレイ
                    using ( var 不透明度 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }
                    using ( var 不透明度 = animation.TrasitionLibrary.Instantaneous( 255 ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }
                    using ( var 不透明度 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 8 ) ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }
                    using ( var 不透明度 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 5 ), 0 ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }

                    // 文字
                    using ( var X拡大 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 1.0 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 1.0 ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 17 ) ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 17 ) ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 3 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 1.91, 0.6, 0.4 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 0.23, 0.6, 0.4 ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 4 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                    }

                    // 棒
                    using ( var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 3 ), 1.25, 0.6, 0.4 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( 0.013 * 3 ) ) )
                    using ( var Z回転 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 3 ), C変換.DegreeToRadian( 18.0 ), 0.6, 0.4 ) )
                    {
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率X, X拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率Y, Y拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 15 ) ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 15 ) ) )
                    using ( var Z回転 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 15 ), C変換.DegreeToRadian( 43.0 ) ) )
                    {
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率X, X拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率Y, Y拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒Z軸回転度, Z回転 );
                    }
                    using ( var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 0.0, 0.6, 0.4 ) )
                    using ( var Y拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 4 ), 0.0, 0.6, 0.4 ) )
                    {
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率X, X拡大 );
                        文字.ストーリーボード_棒.AddTransition( 文字.var棒拡大率Y, Y拡大 );
                    }


                    break;
                case E判定.Good:
                    文字.var文字中心位置Y = new Variable( animation.Manager, 0 );
                    文字.var文字拡大率X = new Variable( animation.Manager, 0.625 );
                    文字.var文字拡大率Y = new Variable( animation.Manager, 3.70 );
                    文字.var文字Z軸回転度 = new Variable( animation.Manager, 0.0 );
                    文字.var文字不透明度 = new Variable( animation.Manager, 255 );
                    文字.var文字オーバーレイ中心位置X = new Variable( animation.Manager, 0 );
                    文字.var文字オーバーレイ中心位置Y = new Variable( animation.Manager, 0 );
                    文字.var文字オーバーレイ拡大率X = new Variable( animation.Manager, 0.625 );
                    文字.var文字オーバーレイ拡大率Y = new Variable( animation.Manager, 3.70 );
                    文字.var文字オーバーレイ不透明度 = new Variable( animation.Manager, 255 );

                    文字.ストーリーボード_オーバーレイ = new Storyboard( animation.Manager );

                    // オーバーレイ不透明度
                    using ( var 不透明度 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 13 ), 255 ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }
                    using ( var 不透明度 = animation.TrasitionLibrary.Instantaneous( 0 ) )
                    {
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ不透明度, 不透明度 );
                    }

                    // 文字XY拡大
                    using (var X拡大 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 3), 1.375))
                    using ( var Y拡大 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 0.66 ) )
                    using ( var X拡大オーバーレイ = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 1.375 ) )
                    using ( var Y拡大オーバーレイ = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 0.66 ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率X, X拡大オーバーレイ );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率Y, Y拡大オーバーレイ );
                    }
                    using (var X拡大 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 2), 1.0))
                    using ( var Y拡大 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 2 ), 1.0 ) )
                    using ( var X拡大オーバーレイ = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 2 ), 1.0 ) )
                    using ( var Y拡大オーバーレイ = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 2 ), 1.0 ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率X, X拡大オーバーレイ );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率Y, Y拡大オーバーレイ );
                    }
                    using (var X拡大 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 14)))
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 14 ) ) )
                    using (var X拡大オーバーレイ = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 14)))
                    using ( var Y拡大オーバーレイ = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 14 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率X, X拡大オーバーレイ );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率Y, Y拡大オーバーレイ );
                    }
                    using (var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 5), 1.91, 0.6, 0.4))
                    using ( var Y拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 5 ), 0.23, 0.6, 0.4 ) )
                    using (var X拡大オーバーレイ = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 5), 1.91, 0.6, 0.4))
                    using ( var Y拡大オーバーレイ = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 5 ), 0.23, 0.6, 0.4 ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率X, X拡大オーバーレイ );
                        文字.ストーリーボード_オーバーレイ.AddTransition( 文字.var文字オーバーレイ拡大率Y, Y拡大オーバーレイ );
                    }

                    break;
                case E判定.Poor:
                case E判定.Miss:
                default:
                    文字.var文字拡大率X = new Variable( animation.Manager, 1.0 );
                    文字.var文字拡大率Y = new Variable( animation.Manager, 1.0 );
                    文字.var文字中心位置Y = new Variable( animation.Manager, -18 );
                    文字.var文字Z軸回転度 = new Variable( animation.Manager, 0 );
                    文字.var文字不透明度 = new Variable( animation.Manager, 100 );
                    文字.var文字オーバーレイ不透明度 = new Variable( animation.Manager, 0 );

                    double offset = 0;
                    double db文字落下時間1 = dコマ秒 * 3; // 3コマで落下
                    using (var Z回転 = animation.TrasitionLibrary.Constant(秒(db文字落下時間1)))
                    using (var Y移動 = animation.TrasitionLibrary.AccelerateDecelerate(秒(db文字落下時間1), 0, 0.3, 0.7))
                    using (var 不透明度 = animation.TrasitionLibrary.Linear(秒(db文字落下時間1), 255))
                    {
                        // Y:-18 → 0
                        // t:100 → 255
                        文字.ストーリーボード.AddTransition(文字.var文字Z軸回転度, Z回転);
                        文字.ストーリーボード.AddTransition(文字.var文字中心位置Y, Y移動);
                        文字.ストーリーボード.AddTransition(文字.var文字不透明度, 不透明度);
                    }

                    using (var Z回転 = animation.TrasitionLibrary.Constant(秒(dコマ秒)))
                    using (var Y移動 = animation.TrasitionLibrary.Linear(秒(dコマ秒), -3))
                    using (var 不透明度 = animation.TrasitionLibrary.Constant(秒(dコマ秒)))
                    {
                        文字.ストーリーボード.AddTransition(文字.var文字Z軸回転度, Z回転);
                        文字.ストーリーボード.AddTransition(文字.var文字中心位置Y, Y移動);
                        文字.ストーリーボード.AddTransition(文字.var文字不透明度, 不透明度);
                    }

                    using (var Z回転 = animation.TrasitionLibrary.Constant(秒(dコマ秒)))
                    using (var Y移動 = animation.TrasitionLibrary.Linear(秒(dコマ秒), 0))
                    using (var 不透明度 = animation.TrasitionLibrary.Constant(秒(dコマ秒)))
                    {
                        文字.ストーリーボード.AddTransition(文字.var文字Z軸回転度, Z回転);
                        文字.ストーリーボード.AddTransition(文字.var文字中心位置Y, Y移動);
                        文字.ストーリーボード.AddTransition(文字.var文字不透明度, 不透明度);
                    }

                    // 12コマ滞留
                    using (var Z回転 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 12)))
                    using (var Y移動 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 12), 0))
                    using (var 不透明度 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 12)))
                    {
                        文字.ストーリーボード.AddTransition(文字.var文字Z軸回転度, Z回転);
                        文字.ストーリーボード.AddTransition(文字.var文字中心位置Y, Y移動);
                        文字.ストーリーボード.AddTransition(文字.var文字不透明度, 不透明度);
                    }

                    // 2コマ 回転
                    // 6コマ 透明(100%→40%?)
                    using (var Z回転 = animation.TrasitionLibrary.Linear(秒((dコマ秒 * 2)), -C変換.DegreeToRadian(10)))
                    using (var 不透明度 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 6), 51))
                    using (var Y移動 = animation.TrasitionLibrary.Linear(秒((dコマ秒 * 6)), 18))
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字Z軸回転度, Z回転 );
                        文字.ストーリーボード.AddTransition( 文字.var文字不透明度, 不透明度 );
                        文字.ストーリーボード.AddTransition( 文字.var文字中心位置Y, Y移動 );
                    }

                    break;
                case E判定.Auto:
                    文字.var文字中心位置Y = new Variable( animation.Manager, 0 );
                    文字.var文字拡大率X = new Variable( animation.Manager, 0.625 );
                    文字.var文字拡大率Y = new Variable( animation.Manager, 3.70 );
                    文字.var文字Z軸回転度 = new Variable( animation.Manager, 0.0 );
                    文字.var文字不透明度 = new Variable( animation.Manager, 255 );
                    
                    // 文字XY拡大
                    using (var X拡大 = animation.TrasitionLibrary.Linear(秒(dコマ秒 * 3), 1.0))
                    using ( var Y拡大 = animation.TrasitionLibrary.Linear( 秒( dコマ秒 * 3 ), 1.0 ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                    }
                    using (var X拡大 = animation.TrasitionLibrary.Constant(秒(dコマ秒 * 16)))
                    using ( var Y拡大 = animation.TrasitionLibrary.Constant( 秒( dコマ秒 * 16 ) ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                    }
                    using (var X拡大 = animation.TrasitionLibrary.AccelerateDecelerate(秒(dコマ秒 * 5), 1.91, 0.6, 0.4))
                    using ( var Y拡大 = animation.TrasitionLibrary.AccelerateDecelerate( 秒( dコマ秒 * 5 ), 0.23, 0.6, 0.4 ) )
                    {
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率X, X拡大 );
                        文字.ストーリーボード.AddTransition( 文字.var文字拡大率Y, Y拡大 );
                    }

                    break;
            }
            #endregion

            #region[ rectangle ]
            switch( 文字.e判定 )
            {
                case E判定.XPerfect:
                case E判定.Perfect:
                    文字.rect画像範囲 = new Rectangle( 0, 0, 85, 35 );
                    文字.rect棒範囲 = new Rectangle( 0, 112, 210, 18 );
                    break;
                case E判定.Great:
                    文字.rect画像範囲 = new Rectangle( 90, 0, 85, 35 );
                    文字.rect棒範囲 = new Rectangle( 0, 132, 210, 18 );
                    break;
                case E判定.Good:
                    文字.rect画像範囲 = new Rectangle( 0, 35, 85, 35 );
                    break;
                case E判定.Poor:
                    文字.rect画像範囲 = new Rectangle( 90, 37, 85, 35 );
                    break;
                case E判定.Miss:
                    文字.rect画像範囲 = new Rectangle( 0, 74, 85, 35 );
                    break;
                case E判定.Auto:
                    文字.rect画像範囲 = new Rectangle( 175, 74, 72, 35 );
                    break;
                default:
                    文字.rect画像範囲 = new Rectangle( 0, 74, 85, 35 );
                    break;
            }
            #endregion

            if( this._判定文字[ nLane ] != null )
            {
                var basetime = animation.Timer.Time;
                var start = basetime;

                this._判定文字[ nLane ].ストーリーボード_オーバーレイ?.Schedule( start );
                this._判定文字[ nLane ].ストーリーボード?.Schedule( start );
                this._判定文字[ nLane ].ストーリーボード_棒?.Schedule( start );
                this._判定文字[ nLane ].b表示 = true;
            }
        }


        // CActivity 実装

        public override void On活性化()
		{
			for( int i = 0; i < 15; i++ )
			{
				this.st状態[ i ].ct進行 = new CCounter();
                this._判定文字[ i ] = new C判定文字();
			}
            
			base.On活性化();
			this.nShowLagType = CDTXMania.ConfigIni.nShowLagType;
		}
		public override void On非活性化()
		{
			for( int i = 0; i < 15; i++ )
			{
				this.st状態[ i ].ct進行 = null;
                this._判定文字[ i ]?.Dispose();
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                //2016.02.20 kairera0467 大して読み込みまでの時間が変わらないことがわかったので、ここで読み込むようにした。
                switch( CDTXMania.ConfigIni.eJudgeAnimeType )
                {
                    case Eタイプ.A:
				        this.tx判定文字列[ 0 ] = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_judge strings 1.png" ) );
				        this.tx判定文字列[ 1 ] = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_judge strings 2.png" ) );
				        this.tx判定文字列[ 2 ] = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_judge strings 3.png" ) );
                        break;
                    case Eタイプ.B:
				        this.tx判定文字列[ 0 ] = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_judge strings.png" ) );
                        break;
                    case Eタイプ.C:
                        //正直この使い方はなんとかしたい...
                        this.tx判定文字列[ 0 ] = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_JudgeStrings_XG.png" ) );
                        break;
                }

				this.txlag数値 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_lag numbers.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx判定文字列[ 0 ] );
				CDTXMania.tテクスチャの解放( ref this.tx判定文字列[ 1 ] );
				CDTXMania.tテクスチャの解放( ref this.tx判定文字列[ 2 ] );
				CDTXMania.tテクスチャの解放( ref this.txlag数値 );
				base.OnManagedリソースの解放();
			}
		}

		public virtual int t進行描画( C演奏判定ライン座標共通 演奏判定ライン座標共通 )
		{
			return 0;
		}
	}
}
