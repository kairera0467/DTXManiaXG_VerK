using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Combo共通 : CActivity
	{
		// プロパティ

		public STCOMBO n現在のコンボ数;
		public STCOMBO n現在のコンボ数Ghost; //2016.03.08 kairera0467 add
		public struct STCOMBO
		{
			public CAct演奏Combo共通 act;

			public int this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Drums;

						case 1:
							return this.Guitar;

						case 2:
							return this.Bass;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.Drums = value;
							return;

						case 1:
							this.Guitar = value;
							return;

						case 2:
							this.Bass = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
			public int Drums
			{
				get
				{
					return this.drums;
				}
				set
				{
					this.drums = value;
					if( this.drums > this.Drums最高値 )
					{
						this.Drums最高値 = this.drums;
					}
					this.act.status.Drums.nCOMBO値 = this.drums;
					this.act.status.Drums.n最高COMBO値 = this.Drums最高値;
				}
			}
			public int Guitar
			{
				get
				{
					return this.guitar;
				}
				set
				{
					this.guitar = value;
					if( this.guitar > this.Guitar最高値 )
					{
						this.Guitar最高値 = this.guitar;
					}
					this.act.status.Guitar.nCOMBO値 = this.guitar;
					this.act.status.Guitar.n最高COMBO値 = this.Guitar最高値;
				}
			}
			public int Bass
			{
				get
				{
					return this.bass;
				}
				set
				{
					this.bass = value;
					if( this.bass > this.Bass最高値 )
					{
						this.Bass最高値 = this.bass;
					}
					this.act.status.Bass.nCOMBO値 = this.bass;
					this.act.status.Bass.n最高COMBO値 = this.Bass最高値;
				}
			}
			public int Drums最高値 { get; private set; }
			public int Guitar最高値 { get; private set; }
			public int Bass最高値 { get; private set; }

			private int drums;
			private int guitar;
			private int bass;
		}
		public C演奏判定ライン座標共通 演奏判定ライン座標
		{
			get;
			set;
		}

		protected enum EEvent { 非表示, 数値更新, 同一数値, ミス通知 }
		protected enum EMode { 非表示中, 進行表示中, 残像表示中 }
        protected const int nギターコンボのCOMBO文字の高さ = 32;
        protected const int nギターコンボのCOMBO文字の幅 = 90;
        protected const int nギターコンボの高さ = 92;
        protected const int nギターコンボの幅 = 72;
        protected const int nギターコンボの文字間隔 = -6;
        protected const int nドラムコンボのCOMBO文字の高さ = 32;
        protected const int nドラムコンボのCOMBO文字の幅 = 90;
        protected const int nドラムコンボの高さ = 115;
        protected const int nドラムコンボの幅 = 90;
        protected const int nドラムコンボの文字間隔 = -6;
        protected int[] nジャンプ差分値 = new int[ 180 ];
		protected CSTATUS status;
		protected CTexture txCOMBOギター;
		protected CTexture txCOMBOドラム;
        protected CTexture txComboBom;
        public float nUnitTime;
        public CCounter ctコンボ;
        public CCounter ctコンボアニメ;
        public CCounter ctコンボアニメ_2P;
        public CCounter ctコンボ動作タイマ;
        public int nY1の位座標差分値 = 0;
        public int nY1の位座標差分値_2P = 0;

        [StructLayout(LayoutKind.Sequential)]
        public struct ST爆発
        {
            public bool b使用中;
            public CCounter ct進行;
        }
        public ST爆発[] st爆発 = new ST爆発[2];
        public bool[] b爆発した = new bool[256];
        public int n火薬カウント;
        public STDGBVALUE<bool>[] bn00コンボに到達した = new STDGBVALUE<bool>[256];
        public STDGBVALUE<int> nコンボカウント = new STDGBVALUE<int>();

		// 内部クラス

		protected class CSTATUS
		{
			public CSTAT Bass = new CSTAT();
			public CSTAT Drums = new CSTAT();
			public CSTAT Guitar = new CSTAT();
			public CSTAT this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Drums;

						case 1:
							return this.Guitar;

						case 2:
							return this.Bass;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.Drums = value;
							return;

						case 1:
							this.Guitar = value;
							return;

						case 2:
							this.Bass = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}

			public class CSTAT
			{
				public CAct演奏Combo共通.EMode e現在のモード;
				public int nCOMBO値;
				public long nコンボが切れた時刻;
				public int nジャンプインデックス値;
				public int n現在表示中のCOMBO値;
				public int n最高COMBO値;
				public int n残像表示中のCOMBO値;
				public long n前回の時刻_ジャンプ用;
			}
		}


		// コンストラクタ

		public CAct演奏Combo共通()
		{
			this.b活性化してない = true;

			// 180度分のジャンプY座標差分を取得。(0度: 0 → 90度:-15 → 180度: 0)
			for( int i = 0; i < 180; i++ )
				this.nジャンプ差分値[ i ] = (int) ( -15.0 * Math.Sin( ( Math.PI * i ) / 180.0 ) * Scale.Y );
			演奏判定ライン座標 = new C演奏判定ライン座標共通();
		}

        public void tコンボリセット処理()
        {
            for (int i = 0; i < 256; i++)
            {
                this.b爆発した[i] = false;
                this.bn00コンボに到達した[i].Drums = false;
            }
        }

		// メソッド

		protected virtual void tコンボ表示_ドラム( int nCombo値, int nジャンプインデックス )
		{
			#region [ 事前チェック。]
			//-----------------
			if( CDTXMania.ConfigIni.ドラムコンボ文字の表示位置 == Eドラムコンボ文字の表示位置.OFF )
				return;		// 表示OFF。

			if( nCombo値 == 0 )
				return;		// コンボゼロは表示しない。
			//-----------------
			#endregion

			int[] n位の数 = new int[ 10 ];	// 表示は10桁もあれば足りるだろう

			#region [ nCombo値を桁数ごとに n位の数[] に格納する。（例：nCombo値=125 のとき n位の数 = { 5,2,1,0,0,0,0,0,0,0 } ） ]
			//-----------------
			int n = nCombo値;
			int n桁数 = 0;
			while( ( n > 0 ) && ( n桁数 < 10 ) )
			{
				n位の数[ n桁数 ] = n % 10;		// 1の位を格納
				n = ( n - ( n % 10 ) ) / 10;	// 右へシフト（例: 12345 → 1234 ）
				n桁数++;
			}
			//-----------------
			#endregion

			bool guitar = CDTXMania.DTX.bチップがある.Guitar;
			bool bass = CDTXMania.DTX.bチップがある.Bass;
			var e表示位置 = CDTXMania.ConfigIni.ドラムコンボ文字の表示位置;
            int y動作差分 = 0;

			#region [ e表示位置 の調整 ]
			//-----------------
			if( CDTXMania.ConfigIni.bGuitar有効 )
			{
				if( bass )
				{
					// ベースがあるときは問答無用で LEFT 表示のみ。
					e表示位置 = Eドラムコンボ文字の表示位置.LEFT;
				}
				else if( guitar && ( e表示位置 == Eドラムコンボ文字の表示位置.RIGHT ) )
				{
					// ベースがなくてもギターがあるなら、RIGHT は CENTER に強制変更。
					e表示位置 = Eドラムコンボ文字の表示位置.CENTER;
				}
			}
			//-----------------
			#endregion

			#region [ n位の数[] を、"COMBO" → 1の位 → 10の位 … の順に、右から左へ向かって順番に表示する。]
			//-----------------
			const int n1桁ごとのジャンプの遅れ = 30;	// 1桁につき 50 インデックス遅れる

			int nX中央位置px = 1130;

            if( n桁数 >= 4 )
            {
                nX中央位置px = nX中央位置px - 40;
            }
            if( n桁数 == 2 && CDTXMania.ConfigIni.eNamePlateType == Eタイプ.A )
            {
                nX中央位置px = 1180;
            }

            int n数字とCOMBOを合わせた画像の全長px = ( ( nドラムコンボの幅 + nドラムコンボの文字間隔 ) * n桁数 );
			int nY上辺位置px = 16;
			int n数字の全長px = ((nドラムコンボの幅 + nドラムコンボの文字間隔) * n桁数);// +nドラムコンボのCOMBO文字の幅;
			int x = ( nX中央位置px + ( n数字とCOMBOを合わせた画像の全長px / 2 ) );
			int y = ( nY上辺位置px + nドラムコンボの高さ ) - nドラムコンボのCOMBO文字の高さ;
            int y2 = (nY上辺位置px) - nドラムコンボのCOMBO文字の高さ;
            int nJump = nジャンプインデックス - ( n桁数 );
            Eタイプ eNamePlateType = CDTXMania.ConfigIni.eNamePlateType;

            if ((nJump >= 0) && (nJump < 180))
            {
                y += this.nジャンプ差分値[nJump];
            }
            if ( (int)(this.ctコンボ動作タイマ.n現在の値 / 4) != 0 )
            {
                y動作差分 = 2;
            }
            else if ( (int)(this.ctコンボ動作タイマ.n現在の値 / 16) != 1 )
            {
                y動作差分 = 8;
            }
            if( this.nY1の位座標差分値 > 0 )
            {
                this.nY1の位座標差分値 = this.nY1の位座標差分値 - this.ctコンボアニメ.n現在の値;
            }
            else
            {
                this.nY1の位座標差分値 = 0;
            }

            #region[ COMBO文字の表示 ]
            if( this.txCOMBOドラム != null )
            {
                this.nコンボカウント.Drums = this.n現在のコンボ数.Drums / 100;
                #region [ "COMBO" の拡大率を設定。]
                //-----------------
                float f拡大率 = 1.0f;
                if (nジャンプインデックス >= 0 && nジャンプインデックス < 180)
                    f拡大率 = 1.0f - (((float)this.nジャンプ差分値[nジャンプインデックス]) / 180.0f);		// f拡大率 = 1.0 → 1.3333... → 1.0

                if (eNamePlateType == Eタイプ.B)
                {
                    if ((this.n現在のコンボ数.Drums > (this.n現在のコンボ数.Drums / 100) + 100) && this.bn00コンボに到達した[nコンボカウント.Drums].Drums == false && (nジャンプインデックス >= 0 && nジャンプインデックス < 180))
                    {
                        f拡大率 = 1.22f - (((float)this.nジャンプ差分値[nジャンプインデックス]) / 180.0f);		// f拡大率 = 1.0 → 1.3333... → 1.0
                    }

                    if (this.txCOMBOドラム != null)
                        this.txCOMBOドラム.vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率, 1.0f );
                }
                //-----------------
                #endregion
                #region [ "COMBO" 文字を表示。]
                //-----------------
                int nコンボx = nX中央位置px - 8 - ((int)((nドラムコンボのCOMBO文字の幅 * f拡大率) / 1.3f));
                int nコンボy = CDTXMania.ConfigIni.bReverse.Drums ? 442 : 132;

                if ((this.n現在のコンボ数.Drums > (this.n現在のコンボ数.Drums / 100 * 100) && (this.n現在のコンボ数.Drums >= 100 ? this.bn00コンボに到達した[nコンボカウント.Drums].Drums == false : false) && eNamePlateType == Eタイプ.B))
                {
                    nコンボx += nX中央位置px - 8 - ((int)((nドラムコンボのCOMBO文字の幅 * f拡大率) / 1.3f));
                    nコンボy += 30;
                }
                //-----------------
                #endregion

                if (n桁数 <= 2)//3ケタ未満の場合
                {
                    if( eNamePlateType == Eタイプ.B )
                    {
                        this.txCOMBOドラム.t2D描画(CDTXMania.app.Device, nコンボx, nコンボy + y動作差分, new Rectangle(0, 230, 280, 64));
                    }
                    else
                    {
                        this.txCOMBOドラム.t2D描画(CDTXMania.app.Device, x - 162, y + 36 + y動作差分, new Rectangle(0, 230, 160, 64));
                    }
                }
                else if (n桁数 == 3)//3ケタの場合
                {
                    if( eNamePlateType == Eタイプ.B)
                    {
                        nコンボx = nX中央位置px - 8 - ((int)((280.0f * f拡大率) / 1.8f));
                        this.txCOMBOドラム.t2D描画(CDTXMania.app.Device, nコンボx + 22, nコンボy + y動作差分, new Rectangle(0, 294, 280, 64));
                    }
                    else
                    {
                        this.txCOMBOドラム.t2D描画(CDTXMania.app.Device, x - 264, y + 36 + y動作差分, new Rectangle(0, 294, 280, 64));
                    }
                }
                else if (n桁数 >= 4)//4ケタの場合
                {
                    if( eNamePlateType == Eタイプ.B)
                    {
                        nコンボx = nX中央位置px - 8 - ((int)((318.0f * f拡大率) / 3.0f));
                        this.txCOMBOドラム.t2D描画(CDTXMania.app.Device, nコンボx - 35, nコンボy + y動作差分, new Rectangle(0, 357, 318, 64));
                    }
                    else
                    {
                        this.txCOMBOドラム.t2D描画(CDTXMania.app.Device, x - 320, y + 36 + y動作差分, new Rectangle(0, 357, 318, 63));
                    }
                }
            }
            #endregion

			// COMBO値を1の位から順に表示。
			for( int i = 0; i < n桁数; i++ )
			{
                if (this.txCOMBOドラム != null)
                {
                    if( eNamePlateType == Eタイプ.B )
                    {
                        if ((this.n現在のコンボ数.Drums > (this.n現在のコンボ数.Drums / 100 * 100) && (this.n現在のコンボ数.Drums >= 100 ? this.bn00コンボに到達した[nコンボカウント.Drums].Drums == false : false) && (nジャンプインデックス >= 0 && nジャンプインデックス < 180)))
                        {
                            x -= nドラムコンボの幅 + nドラムコンボの文字間隔 + 14;
                        }
                        else
                        {
                            x -= nドラムコンボの幅 + nドラムコンボの文字間隔;
                            this.txCOMBOドラム.vc拡大縮小倍率 = new Vector3(1.0f, 1.0f, 1.0f);
                        }
                    }
                    else
                    {
                        this.txCOMBOドラム.vc拡大縮小倍率 = new Vector3(1.0f, 1.0f, 1.0f);
                        x -= nドラムコンボの幅 + nドラムコンボの文字間隔;
                    }
                    y = nY上辺位置px;

                    nJump = nジャンプインデックス - (((n桁数 - i) - 1));
                    if( ( nJump >= 0 ) && ( nJump < 180 ) )
                    {
                        y += this.nジャンプ差分値[ nJump ];
                    }
                    this.txCOMBOドラム.t2D描画( CDTXMania.app.Device, x, y + y動作差分 - ( i == 0 ? this.nY1の位座標差分値 : 0 ),
                       new Rectangle( ( n位の数[ i ] % 5 ) * nドラムコンボの幅, ( n位の数[ i ] / 5 ) * nドラムコンボの高さ, nドラムコンボの幅, nドラムコンボの高さ ) );
                }
			}
			//-----------------
			#endregion
		}
		protected virtual void tコンボ表示_ギター( int nCombo値, int nジャンプインデックス )
		{
		}
		protected virtual void tコンボ表示_ベース( int nCombo値, int nジャンプインデックス )
		{
		}
		protected void tコンボ表示_ギター( int nCombo値, int n表示中央X, int n表示中央Y, int nジャンプインデックス )
		{
			#region [ 事前チェック。]
			//-----------------
			if ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Guitar ) == E判定文字表示位置.表示OFF )
				return;		// 表示OFF。

			if ( nCombo値 == 0 )
				return;     // コンボゼロは表示しない。
							//-----------------
			#endregion
			tコンボ表示_ギターベース( nCombo値, n表示中央X, n表示中央Y, nジャンプインデックス );
		}
		protected void tコンボ表示_ベース( int nCombo値, int n表示中央X, int n表示中央Y, int nジャンプインデックス )
		{
			#region [ 事前チェック。]
			//-----------------
			if ( ( (E判定文字表示位置) CDTXMania.ConfigIni.判定文字表示位置.Bass ) == E判定文字表示位置.表示OFF )
				return;		// 表示OFF。

			if ( nCombo値 == 0 )
				return;     // コンボゼロは表示しない。
							//-----------------
			#endregion
			tコンボ表示_ギターベース( nCombo値, n表示中央X, n表示中央Y, nジャンプインデックス );
		}
		protected void tコンボ表示_ギターベース( int nCombo値, int n表示中央X, int n表示中央Y, int nジャンプインデックス )
		{
			int[] n位の数 = new int[ 10 ];	// 表示は10桁もあれば足りるだろう

			#region [ nCombo値を桁数ごとに n位の数[] に格納する。（例：nCombo値=125 のとき n位の数 = { 5,2,1,0,0,0,0,0,0,0 } ） ]
			//-----------------
			int n = nCombo値;
			int n桁数 = 0;
			while( ( n > 0 ) && ( n桁数 < 10 ) )
			{
				n位の数[ n桁数 ] = n % 10;		// 1の位を格納
				n = ( n - ( n % 10 ) ) / 10;	// 右へシフト（例: 12345 → 1234 ）
				n桁数++;
			}
			//-----------------
			#endregion

			int n全桁の合計幅 = nギターコンボの幅 * n桁数;

			#region [ "COMBO" の拡大率を設定。]
			//-----------------
			float f拡大率 = 1.0f;
			if( nジャンプインデックス >= 0 && nジャンプインデックス < 180 )
				f拡大率 = 1.0f - ( ( (float) this.nジャンプ差分値[ nジャンプインデックス ] ) / 45.0f );		// f拡大率 = 1.0 → 1.3333... → 1.0

			if( this.txCOMBOギター != null )
				this.txCOMBOギター.vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率, 1.0f );
			//-----------------
			#endregion
			#region [ "COMBO" 文字を表示。]
			//-----------------
			int x = (int) ( n表示中央X ) - ( (int) ( ( nギターコンボのCOMBO文字の幅 * f拡大率 ) / 2.0f ) );
			int y = (int) ( n表示中央Y );

			if ( this.txCOMBOギター != null )
				this.txCOMBOギター.t2D描画(
					CDTXMania.app.Device,
					x,
					y,
					new Rectangle(
						128,
						(int) ( 158 ),
						(int) ( 124 ),
						(int) ( 16 * Scale.Y )
					)
				); 
			//-----------------
			#endregion

			//x = (int) ( n表示中央X * Scale.X ) + ( n全桁の合計幅 / 2 );
			x = n表示中央X + ( n全桁の合計幅 / 2 );
			for ( int i = 0; i < n桁数; i++ )
			{
				#region [ 数字の拡大率を設定。]
				//-----------------
				f拡大率 = 1.0f;
				if( nジャンプインデックス >= 0 && nジャンプインデックス < 180 )
					f拡大率 = 1.0f - ( ( (float) this.nジャンプ差分値[ nジャンプインデックス ] ) / 45f );		// f拡大率 = 1.0 → 1.3333... → 1.0

				if( this.txCOMBOギター != null )
					this.txCOMBOギター.vc拡大縮小倍率 = new Vector3( f拡大率, f拡大率, 1.0f );
				//-----------------
				#endregion
				#region [ 数字を1桁表示。]
				//-----------------
				x -= nギターコンボの幅 + nギターコンボの文字間隔;
				//y = (int) ( n表示中央Y * Scale.Y ) - nギターコンボの高さ;
				y = n表示中央Y - nギターコンボの高さ;

				if( this.txCOMBOギター != null )
				{
					this.txCOMBOギター.t2D描画(
						CDTXMania.app.Device,
						x - ( (int) ( ( ( f拡大率 - 1.0f ) * nギターコンボの幅 ) / 2.0f ) ),
						y - ( (int) ( ( ( f拡大率 - 1.0f ) * nギターコンボの高さ ) / 2.0f ) ),
						new Rectangle(
							( n位の数[ i ] % 4 ) * nギターコンボの幅,
							( n位の数[ i ] / 4 ) * nギターコンボの高さ,
							nギターコンボの幅,
							nギターコンボの高さ
						)
					);
				}
				//-----------------
				#endregion
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n現在のコンボ数 = new STCOMBO() { act = this };
            this.n現在のコンボ数Ghost = new STCOMBO() { act = this };
			this.status = new CSTATUS();
			for( int i = 0; i < 3; i++ )
			{
				this.status[ i ].e現在のモード = EMode.非表示中;
				this.status[ i ].nCOMBO値 = 0;
				this.status[ i ].n最高COMBO値 = 0;
				this.status[ i ].n現在表示中のCOMBO値 = 0;
				this.status[ i ].n残像表示中のCOMBO値 = 0;
				this.status[ i ].nジャンプインデックス値 = 99999;
				this.status[ i ].n前回の時刻_ジャンプ用 = -1;
				this.status[ i ].nコンボが切れた時刻 = -1;
			}
            this.ctコンボアニメ = new CCounter( 0, 130, 1, CDTXMania.Timer );
            this.ctコンボアニメ_2P = new CCounter( 0, 130, 1, CDTXMania.Timer );
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.status != null )
				this.status = null;

			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( this.b活性化してない )
				return;

			this.txCOMBOドラム = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_combo drums.png" ) );
			this.txCOMBOギター = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_combo guitar.png" ) );

			base.OnManagedリソースの作成();
		}
		public override void OnManagedリソースの解放()
		{
			if( this.b活性化してない )
				return;

			CDTXMania.tテクスチャの解放( ref this.txCOMBOドラム );
			CDTXMania.tテクスチャの解放( ref this.txCOMBOギター );

			base.OnManagedリソースの解放();
		}
		public override int On進行描画()
		{
			if( this.b活性化してない )
				return 0;

            if( this.ctコンボアニメ != null )
            {
                this.ctコンボアニメ.t進行();
            }


			for( int i = 2; i >= 0; i-- )
			{
				EEvent e今回の状態遷移イベント;

				#region [ 前回と今回の COMBO 値から、e今回の状態遷移イベントを決定する。]
				//-----------------
				if( this.status[ i ].n現在表示中のCOMBO値 == this.status[ i ].nCOMBO値 )
				{
					e今回の状態遷移イベント = EEvent.同一数値;
				}
				else if( this.status[ i ].n現在表示中のCOMBO値 > this.status[ i ].nCOMBO値 )
				{
					e今回の状態遷移イベント = EEvent.ミス通知;
				}
				else if( ( this.status[ i ].n現在表示中のCOMBO値 < CDTXMania.ConfigIni.n表示可能な最小コンボ数[ i ] ) && ( this.status[ i ].nCOMBO値 < CDTXMania.ConfigIni.n表示可能な最小コンボ数[ i ] ) )
				{
					e今回の状態遷移イベント = EEvent.非表示;
				}
				else
				{
					e今回の状態遷移イベント = EEvent.数値更新;
				}
				//-----------------
				#endregion

				#region [ nジャンプインデックス値 の進行。]
				//-----------------
				if( this.status[ i ].nジャンプインデックス値 < 360 )
				{
					if( ( this.status[ i ].n前回の時刻_ジャンプ用 == -1 ) || ( CDTXMania.Timer.n現在時刻 < this.status[ i ].n前回の時刻_ジャンプ用) )
						this.status[ i ].n前回の時刻_ジャンプ用 = CDTXMania.Timer.n現在時刻;

					const long INTERVAL = 2;
					while( ( CDTXMania.Timer.n現在時刻 - this.status[ i ].n前回の時刻_ジャンプ用) >= INTERVAL )
					{
						if( this.status[ i ].nジャンプインデックス値 < 2000 )
							this.status[ i ].nジャンプインデックス値 += 3;

						this.status[ i ].n前回の時刻_ジャンプ用 += INTERVAL;
					}
				}
			//-----------------
				#endregion


			Retry:	// モードが変化した場合はここからリトライする。

				switch( this.status[ i ].e現在のモード )
				{
					case EMode.非表示中:
						#region [ *** ]
						//-----------------

						if( e今回の状態遷移イベント == EEvent.数値更新 )
						{
							// モード変更
							this.status[ i ].e現在のモード = EMode.進行表示中;
							this.status[ i ].nジャンプインデックス値 = 0;
							this.status[ i ].n前回の時刻_ジャンプ用 = CDTXMania.Timer.n現在時刻;
							goto Retry;
						}

						this.status[ i ].n現在表示中のCOMBO値 = this.status[ i ].nCOMBO値;
						break;
					//-----------------
						#endregion

					case EMode.進行表示中:
						#region [ *** ]
						//-----------------

						if( ( e今回の状態遷移イベント == EEvent.非表示 ) || ( e今回の状態遷移イベント == EEvent.ミス通知 ) )
						{
							// モード変更
							this.status[ i ].e現在のモード = EMode.残像表示中;
							this.status[ i ].n残像表示中のCOMBO値 = this.status[ i ].n現在表示中のCOMBO値;
							this.status[ i ].nコンボが切れた時刻 = CDTXMania.Timer.n現在時刻;
							goto Retry;
						}

						if( e今回の状態遷移イベント == EEvent.数値更新 )
						{
							this.status[ i ].nジャンプインデックス値 = 0;
							this.status[ i ].n前回の時刻_ジャンプ用 = CDTXMania.Timer.n現在時刻;
						}

						this.status[ i ].n現在表示中のCOMBO値 = this.status[ i ].nCOMBO値;
						switch( i )
						{
							case 0:
								this.tコンボ表示_ドラム( this.status[ i ].nCOMBO値, this.status[ i ].nジャンプインデックス値 );
								break;

							case 1:
								this.tコンボ表示_ギター( this.status[ i ].nCOMBO値, this.status[ i ].nジャンプインデックス値 );
								break;

							case 2:
								this.tコンボ表示_ベース( this.status[ i ].nCOMBO値, this.status[ i ].nジャンプインデックス値 );
								break;
						}
						break;
					//-----------------
						#endregion

					case EMode.残像表示中:
						#region [ *** ]
						//-----------------
						if( e今回の状態遷移イベント == EEvent.数値更新 )
						{
							// モード変更１
							this.status[ i ].e現在のモード = EMode.進行表示中;
							goto Retry;
						}
						if( ( CDTXMania.Timer.n現在時刻 - this.status[ i ].nコンボが切れた時刻 ) > 1000 )
						{
							// モード変更２
							this.status[ i ].e現在のモード = EMode.非表示中;
							goto Retry;
						}
						this.status[ i ].n現在表示中のCOMBO値 = this.status[ i ].nCOMBO値;
						break;
						//-----------------
						#endregion
				}
			}

			return 0;
		}

        public void tComboAnime( E楽器パート ePart )
        {
            if( ePart == E楽器パート.DRUMS || ePart == E楽器パート.GUITAR )
            {
                this.ctコンボアニメ.n現在の値 = 0;
                this.nY1の位座標差分値 = 160;
            }
            else if( ePart == E楽器パート.BASS )
            {
                this.ctコンボアニメ_2P.n現在の値 = 0;
                this.nY1の位座標差分値_2P = 160;
            }
        }
	}
}
