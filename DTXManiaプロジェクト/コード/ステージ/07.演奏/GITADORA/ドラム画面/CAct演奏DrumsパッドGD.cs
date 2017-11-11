using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏DrumsパッドGD : CActivity
	{
		// コンストラクタ

		public CAct演奏DrumsパッドGD()
		{
            this.st基本位置 = new ST基本位置[]{
                new ST基本位置( 263, 10, new Rectangle( 0, 0, 96, 96 ) ),
                new ST基本位置( 336, 10, new Rectangle( 96, 0, 96, 96 ) ),
                new ST基本位置( 446, 10, new Rectangle( 0, 96, 96, 96 ) ),
                new ST基本位置( 565, 10, new Rectangle( 0, 192, 96, 96 ) ),
                new ST基本位置( 510, 10, new Rectangle( 96, 96, 96, 96 ) ),
                new ST基本位置( 622, 10, new Rectangle( 192, 96, 96, 96 ) ),
                new ST基本位置( 672, 10, new Rectangle( 288, 96, 96, 96 ) ),
                new ST基本位置( 735, 10, new Rectangle( 192, 0, 96, 96 ) ),
                new ST基本位置( 791, 10, new Rectangle( 288, 0, 96, 96 ) ),
                new ST基本位置( 396, 10, new Rectangle( 96, 192, 96, 96 ) )
            };

			base.b活性化してない = true;
		}
		
		
		// メソッド

		public void Hit( int nLane )
		{
			this.stパッド状態[ nLane ].n明るさ = 6;
			this.stパッド状態[ nLane ].nY座標加速度dot = 2;
		}
        public void tボーナス演出( int nチャンネル番号 )
        {
            int nLane = this.tチャンネル番号toレーン番号( nチャンネル番号 );
            for( int j = 0; j < 4; j++ )
            {
                if( this.stボーナス[ j ].b使用中 )
                {
                    this.stボーナス[ j ].ct進行.t停止();
                    this.stボーナス[ j ].b使用中 = false;
                }
            }
            for( int i = 0; i < 1; i++ )
            {
                for( int j = 0; j < 4; j++ )
                {
                    if( !this.stボーナス[ j ].b使用中 )
                    {
                        this.stボーナス[ j ].b使用中 = true;
                        this.stボーナス[ j ].ct進行 = new CCounter( 0, 1020, 1, CDTXMania.Timer );
                        this.stボーナス[ j ].nLane = nLane;
                        this.stボーナス[ i ].x = -100;

                        if( this.stボーナス[ i ].nLane != -1 )
                        {
                            this.stボーナス[ i ].x = this.nチャンネルtoX座標XG[ this.stボーナス[ i ].nLane ] - 30;
                        }
                    }
                }
            }
        }

		// CActivity 実装

		public override void On活性化()
		{
			this.nフラッシュ制御タイマ = -1;
			this.nY座標制御タイマ = -1;
			for( int i = 0; i < 9; i++ )
			{
				STパッド状態 stパッド状態2 = new STパッド状態();
				STパッド状態 stパッド状態 = stパッド状態2;
				stパッド状態.nY座標オフセットdot = 0;
				stパッド状態.nY座標加速度dot = 0;
				stパッド状態.n明るさ = 0;
				this.stパッド状態[ i ] = stパッド状態;
			}
            for( int i = 0; i < 4; i++ )
            {
                this.stボーナス[ i ].x = -100;
                this.stボーナス[ i ].b使用中 = false;
                this.stボーナス[ i ].nLane = -1;
            }
            this.tレーンタイプからレーン位置を設定する( CDTXMania.ConfigIni.eLaneType, CDTXMania.ConfigIni.eRDPosition );
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパッド = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_pads.png" ) );
				this.tx光るパッド = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_pads flush.png" ) );
                this.txBonus = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Bonus.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパッド );
				CDTXMania.tテクスチャの解放( ref this.tx光るパッド );
                CDTXMania.tテクスチャの解放( ref this.txBonus );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
					this.nフラッシュ制御タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
					this.nY座標制御タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
					base.b初めての進行描画 = false;
				}
				long num = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
				if( num < this.nフラッシュ制御タイマ )
				{
					this.nフラッシュ制御タイマ = num;
				}
				while( ( num - this.nフラッシュ制御タイマ ) >= 15 )
				{
					for( int j = 0; j < 10; j++ )
					{
						if( this.stパッド状態[ j ].n明るさ > 0 )
						{
							this.stパッド状態[ j ].n明るさ--;
						}
					}
					this.nフラッシュ制御タイマ += 15;
				}
				long num3 = CSound管理.rc演奏用タイマ.n現在時刻;
				if( num3 < this.nY座標制御タイマ )
				{
					this.nY座標制御タイマ = num3;
				}
				while( ( num3 - this.nY座標制御タイマ ) >= 5 )
				{
					for( int k = 0; k < 10; k++ )
					{
						this.stパッド状態[ k ].nY座標オフセットdot += this.stパッド状態[ k ].nY座標加速度dot;
						if( this.stパッド状態[ k ].nY座標オフセットdot > 15 )
						{
							this.stパッド状態[ k ].nY座標オフセットdot = 15;
							this.stパッド状態[ k ].nY座標加速度dot = -1;
						}
						else if( this.stパッド状態[ k ].nY座標オフセットdot < 0 )
						{
							this.stパッド状態[ k ].nY座標オフセットdot = 0;
							this.stパッド状態[ k ].nY座標加速度dot = 0;
						}
					}
					this.nY座標制御タイマ += 5;
				}
				for( int i = 0; i < 10; i++ )
				{
					int index = this.n描画順[ i ];
					int x = this.st基本位置[ index ].x;
					int y = ( this.st基本位置[ index ].y + ( CDTXMania.ConfigIni.bReverse.Drums ? 48 : 560 ) ) + this.stパッド状態[ index ].nY座標オフセットdot;
                    #region[ レーン切り替え ]
                    if ((index == 2) && ((CDTXMania.ConfigIni.eLaneType == Eタイプ.B) || CDTXMania.ConfigIni.eLaneType == Eタイプ.D))
                    {
                        x = this.st基本位置[9].x - 4;
                    }
                    if (index == 3)
                    {
                        if ((CDTXMania.ConfigIni.eLaneType == Eタイプ.B) || (CDTXMania.ConfigIni.eLaneType == Eタイプ.C))
                        {
                            x = this.st基本位置[4].x + 7;
                        }
                    }
                    if (index == 4)
                    {
                        if ((CDTXMania.ConfigIni.eLaneType == Eタイプ.B) || (CDTXMania.ConfigIni.eLaneType == Eタイプ.C))
                        {
                            x = this.st基本位置[3].x + 15;
                        }
                        else if(CDTXMania.ConfigIni.eLaneType == Eタイプ.D)
                        {
                            x = this.st基本位置[3].x - 108;
                        }
                    }
                    if (index == 9)
                    {
                        if (CDTXMania.ConfigIni.eLaneType == Eタイプ.B)
                        {
                            x = this.st基本位置[2].x + 10;
                        }
                        else if (CDTXMania.ConfigIni.eLaneType == Eタイプ.D)
                        {
                            x = this.st基本位置[2].x + 50;
                        }
                    }
                    if ((index == 5) && (CDTXMania.ConfigIni.eLaneType == Eタイプ.B))
                    {
                        x = this.st基本位置[5].x + 2;
                    }
                    if ((index == 8) && (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RDRC))
                    {
                        x = this.st基本位置[7].x - 15;
                    }
                    if ((index == 7) && (CDTXMania.ConfigIni.eRDPosition == ERDPosition.RDRC))
                    {
                        x = this.st基本位置[8].x - 15;
                    }
                    #endregion
					if( this.txパッド != null )
					{
						this.txパッド.t2D描画( CDTXMania.app.Device, x, y, this.st基本位置[ index ].rc );
					}
					if( this.tx光るパッド != null )
					{
						this.tx光るパッド.n透明度 = ( this.stパッド状態[ index ].n明るさ * 40 ) + 15;
						this.tx光るパッド.t2D描画( CDTXMania.app.Device, x, y, this.st基本位置[ index ].rc );
					}
				}
                #region[ ボーナス表示 ]
                for (int i = 0; i < 4; i++)
                {
                    
                    //アニメーションは仮のもの。後から強化する予定。
                    
                    if (this.stボーナス[ i ].b使用中)
                    {
                        int numf = this.stボーナス[i].ct進行.n現在の値;
                        this.stボーナス[i].ct進行.t進行();
                        if (this.stボーナス[i].ct進行.b終了値に達した)
                        {
                            this.stボーナス[i].ct進行.t停止();
                            this.stボーナス[i].b使用中 = false;
                            this.stボーナス[i].x = -100;
                        }


                        if (this.txBonus != null)
                        {
                            this.txBonus.t2D描画(CDTXMania.app.Device, this.stボーナス[ i ].x, ( CDTXMania.ConfigIni.bReverse.Drums ? 60 : 570 ) );
                        }
                    }
                    
                    
                }
                #endregion
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct STパッド状態
		{
			public int n明るさ;
			public int nY座標オフセットdot;
			public int nY座標加速度dot;
		}
		[StructLayout( LayoutKind.Sequential )]
		private struct ST基本位置
		{
			public int x;
			public int y;
			public Rectangle rc;
            public ST基本位置( int x, int y, Rectangle rc )
            {
                this.x = x;
                this.y = y;
                this.rc = rc;
            }
		}
        [StructLayout(LayoutKind.Sequential)]
        public struct STボーナス
        {
            public bool b使用中;
            public CCounter ct進行;
            public int x;
            public int nLane;
        }
		private long nY座標制御タイマ;
		private long nフラッシュ制御タイマ;
        private readonly int[] n描画順 = new int[] { 9, 3, 2, 6, 5, 4, 8, 7, 1, 0 };
                                                  // LP BD SD FT HT LT RD CY HH LC
		private STパッド状態[] stパッド状態 = new STパッド状態[ 10 ];
		private ST基本位置[] st基本位置;
		private CTexture txパッド;
		private CTexture tx光るパッド;
        private CTexture txBonus;

        public bool[] bボーナス文字 = new bool[10];
        public STボーナス[] stボーナス = new STボーナス[4];
        private int[] nチャンネルtoX座標XG = new int[12];
        /// <summary>
        /// レーンのX座標をint配列に格納していく。
        /// </summary>
        /// <param name="eLaneType">レーンタイプ</param>
        private void tレーンタイプからレーン位置を設定する( Eタイプ eLaneType, ERDPosition eRDPosition )
        {
            #region[ 共通 ]
            this.nチャンネルtoX座標XG[ 0 ] = 370; //HHC
            this.nチャンネルtoX座標XG[ 4 ] = 645; //LT
            this.nチャンネルtoX座標XG[ 6 ] = 694; //FT
            this.nチャンネルtoX座標XG[ 7 ] = 373; //HHO
            this.nチャンネルtoX座標XG[ 9 ] = 298; //LC
            #endregion
            #region[ レーンタイプ別 ]
            switch( eLaneType )
            {
                case Eタイプ.A:
                    {
                        this.nチャンネルtoX座標XG[ 1 ] = 470; //SD
                        this.nチャンネルtoX座標XG[ 2 ] = 582; //BD
                        this.nチャンネルtoX座標XG[ 3 ] = 527; //HT
                        this.nチャンネルtoX座標XG[ 10 ] = 419; //LP
                        this.nチャンネルtoX座標XG[ 11 ] = 419; //LBD
                    }
                    break;
                case Eタイプ.B:
                    {
                        this.nチャンネルtoX座標XG[ 1 ] = 419; //SD
                        this.nチャンネルtoX座標XG[ 2 ] = 533; //BD
                        this.nチャンネルtoX座標XG[ 3 ] = 596; //HT
                        this.nチャンネルtoX座標XG[ 10 ] = 476; //LP
                        this.nチャンネルtoX座標XG[ 11 ] = 476; //LBD
                    }
                    break;
                case Eタイプ.C:
                    {
                        this.nチャンネルtoX座標XG[ 1 ] = 470; //SD
                        this.nチャンネルtoX座標XG[ 2 ] = 533; //BD
                        this.nチャンネルtoX座標XG[ 3 ] = 596; //HT
                        this.nチャンネルtoX座標XG[ 10 ] = 419; //LP
                        this.nチャンネルtoX座標XG[ 11 ] = 419; //LBD
                    }
                    break;
                case Eタイプ.D:
                    {
                        this.nチャンネルtoX座標XG[ 1 ] = 419; //SD
                        this.nチャンネルtoX座標XG[ 2 ] = 582; //BD
                        this.nチャンネルtoX座標XG[ 3 ] = 477; //HT
                        this.nチャンネルtoX座標XG[ 10 ] = 527; //LP
                        this.nチャンネルtoX座標XG[ 11 ] = 527; //LBD
                    }
                    break;
            }
            #endregion
            #region [ RC RD ]
            if( eRDPosition == ERDPosition.RCRD )
            {
                this.nチャンネルtoX座標XG[ 5 ] = 748; //RC
                this.nチャンネルtoX座標XG[ 8 ] = 815; //RD
            }
            else
            {
                this.nチャンネルtoX座標XG[ 5 ] = 786; //RC
                this.nチャンネルtoX座標XG[ 8 ] = 746; //RD
            }
            #endregion
        }

        private int tチャンネル番号toレーン番号( int pChipチャンネル番号 )
        {
            int ret = 0;
            switch( pChipチャンネル番号 )
            {
                case 0x1A: //LC
                    ret = 9;
                    break;

                case 0x11: //HH
                case 0x18:
                    ret = 0;
                    break;

                case 0x1B: //LP
                case 0x1C:
                    ret = 10;
                    break;

                case 0x12: //SD
                    ret = 1;
                    break;

                case 0x14: //HT
                    ret = 3;
                    break;

                case 0x13: //BD
                    ret = 2;
                    break;

                case 0x15: //LT
                    ret = 4;
                    break;

                case 0x17: //FT
                    ret = 6;
                    break;

                case 0x16: //CY
                    ret = 5;
                    break;

                case 0x19: //RD
                    ret = 8;
                    break;
                default:
                    break;
            }

            return ret;
        }

		//-----------------
		#endregion
	}
}