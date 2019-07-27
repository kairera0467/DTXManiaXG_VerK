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

        protected class C判定文字 : IDisposable
        {
            public Variable var文字中心位置X;
            public Variable var文字中心位置Y;
            public Variable var文字拡大率X;
            public Variable var文字拡大率Y;
            public Variable var文字不透明度;
            public Variable var文字Z軸回転度;
            public Variable var文字オーバーレイ中心位置X;
            public Variable var文字オーバーレイ中心位置Y;
            public Variable var棒拡大率X;
            public Variable var棒拡大率Y;
            public Variable var棒Z軸回転度;
            public Storyboard ストーリーボード;

            public void Dispose()
            {
                this.ストーリーボード?.Abandon();
                this.ストーリーボード = null;

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
                        this.st状態[ nLane ].ct進行 = new CCounter( 0, 23, 12, CDTXMania.Timer );
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


		// CActivity 実装

		public override void On活性化()
		{
			for( int i = 0; i < 15; i++ )
			{
				this.st状態[ i ].ct進行 = new CCounter();
			}
            
			base.On活性化();
			this.nShowLagType = CDTXMania.ConfigIni.nShowLagType;
		}
		public override void On非活性化()
		{
			for( int i = 0; i < 15; i++ )
			{
				this.st状態[ i ].ct進行 = null;
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
				        this.tx判定文字列[ 1 ] = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_JudgeStrings_XG.png" ) );
				        this.tx判定文字列[ 2 ] = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\7_JudgeStrings_XG.png" ) );
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
