using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsドラムセット : CActivity
    {
        /// <summary>
        /// ドラムを描画するクラス。
        /// TIPS
        /// ・シンバル動作のスイッチはドラムパッドに寄生しています。
        /// 
        /// 課題
        /// ・加速度の調整
        /// </summary>
        public CAct演奏Drumsドラムセット()
        {
            base.b活性化してない = true;
        }

        public void Start( int nlane )
        {
            this.stパッド状態[ nlane ].nY座標加速度dot = 3;
            if( nlane == 7 || nlane == 8 )
                this.ctRightCymbal.n現在の値 = 0;
            else if( nlane == 0 )
                this.ctLeftCymbal.n現在の値 = 0;
        }

        public override void On活性化()
        {
            this.nY座標制御タイマ = -1;
            for( int i = 0; i < 10; i++ )
            {
                STパッド状態 stパッド状態 = new STパッド状態();
                stパッド状態.nY座標オフセットdot = 0;
                stパッド状態.nY座標加速度dot = 0;
                this.stパッド状態[ i ] = stパッド状態;
            }

            //this.nLeftCymbalFrame = CDTXMania.ConfigIni.nLeftCymbalFrame;
            //this.nRightCymbalFrame = CDTXMania.ConfigIni.nRightCymbalFrame;
            //this.nLeftCymbalInterval = CDTXMania.ConfigIni.nLeftCymbalInterval;
            //this.nRightCymbalInterval = CDTXMania.ConfigIni.nRightCymbalInterval;
            this.nLeftCymbalFrame = 9;
            this.nLeftCymbalInterval = 35;
            this.nRightCymbalFrame = 9;
            this.nRightCymbalInterval = 35;
            this.nLeftCymbalX = 0;
            this.nLeftCymbalY = 0;
            this.nRightCymbalX = 900;
            this.nRightCymbalY = 0;

            this.ctLeftCymbal = new CCounter( 0, this.nLeftCymbalFrame - 1, this.nLeftCymbalInterval, CDTXMania.Timer );
            this.ctRightCymbal = new CCounter( 0, this.nRightCymbalFrame - 1, this.nRightCymbalInterval, CDTXMania.Timer );

            base.On活性化();
        }

        public override void On非活性化()
        {
            this.ctLeftCymbal = null;
            this.ctRightCymbal = null;

            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            this.txSnare = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_Snare.png" ) );
            this.txHitom = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_HiTom.png" ) );
            this.txLowTom = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_LowTom.png" ) );
            this.txFloorTom = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_FloorTom.png" ) );
            this.txBassDrum = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_BassDrum.png" ) );
            this.txLeftCymbal = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_LCymbal.png" ) );
            this.txRightCymbal = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_RCymbal.png" ) );

            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txSnare );
            CDTXMania.tテクスチャの解放( ref this.txHitom );
            CDTXMania.tテクスチャの解放( ref this.txLowTom );
            CDTXMania.tテクスチャの解放( ref this.txFloorTom );
            CDTXMania.tテクスチャの解放( ref this.txBassDrum );
            CDTXMania.tテクスチャの解放( ref this.txLeftCymbal );
            CDTXMania.tテクスチャの解放( ref this.txRightCymbal );

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( base.b初めての進行描画 )
			{
				this.nY座標制御タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
				base.b初めての進行描画 = false;
			}
			long num = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
            int nIndex = 0;
			if( num < this.nY座標制御タイマ )
			{
				this.nY座標制御タイマ = num;
			}
			while( ( num - this.nY座標制御タイマ ) >= 5 )
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
			    this.nY座標制御タイマ += 7;
            }
            #region[ 座標類 ]
            this.n座標Snare = 490 + this.stパッド状態[ 2 ].nY座標オフセットdot;
            this.n座標HiTom = 491 + this.stパッド状態[ 4 ].nY座標オフセットdot;
            this.n座標LowTom = 490 + this.stパッド状態[ 5 ].nY座標オフセットdot;
            this.n座標FloorTom = 490 + this.stパッド状態[ 6 ].nY座標オフセットdot;
            this.n座標BassDrum = 517 - this.stパッド状態[ 3 ].nY座標オフセットdot;
            int nCtLC = this.ctLeftCymbal.n現在の値;
            int nCtRC = this.ctRightCymbal.n現在の値;
            #endregion
            this.ctLeftCymbal.t進行();
            this.ctRightCymbal.t進行();

            #region[ スネア ]
            if( this.txSnare != null )
                this.txSnare.t2D描画( CDTXMania.app.Device, 0, this.n座標Snare );
            #endregion

            #region[ ハイタム ]
            if( this.txHitom != null )
                this.txHitom.t2D描画( CDTXMania.app.Device, 107, this.n座標HiTom );
            #endregion

            #region[ フロアタム ]
            if( this.txFloorTom != null )
                this.txFloorTom.t2D描画( CDTXMania.app.Device, 1049, this.n座標FloorTom );
            #endregion

            #region[ ロータム ]
            if( this.txLowTom != null )
                this.txLowTom.t2D描画( CDTXMania.app.Device, 870, this.n座標LowTom );
            #endregion

            #region[ バスドラ ]
            if( this.txBassDrum != null )
                this.txBassDrum.t2D描画( CDTXMania.app.Device, 310, this.n座標BassDrum );
            #endregion

            #region[ 左シンバル ]
            if( this.txLeftCymbal != null )
            {
                int nLCRectX = ( this.txLeftCymbal.szテクスチャサイズ.Width / this.nLeftCymbalFrame ) * this.ctLeftCymbal.n現在の値;
                this.txLeftCymbal.t2D描画( CDTXMania.app.Device, this.nLeftCymbalX, this.nLeftCymbalY, new Rectangle( nLCRectX, 0, ( this.txLeftCymbal.szテクスチャサイズ.Width / this.nLeftCymbalFrame ), this.txLeftCymbal.szテクスチャサイズ.Height ) );
            }
            #endregion
            #region[ 右シンバル ]

            if( this.txRightCymbal != null )
            {
                int nRCRectX = ( this.txRightCymbal.szテクスチャサイズ.Width / this.nRightCymbalFrame ) * this.ctRightCymbal.n現在の値;
                this.txRightCymbal.t2D描画( CDTXMania.app.Device, this.nRightCymbalX, this.nRightCymbalY, new Rectangle( nRCRectX, 0, ( this.txRightCymbal.szテクスチャサイズ.Width / this.nRightCymbalFrame ), this.txRightCymbal.szテクスチャサイズ.Height ) );
            }
            #endregion
            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        [StructLayout(LayoutKind.Sequential)]
        private struct STパッド状態
        {
            public int n明るさ;
            public int nY座標オフセットdot;
            public int nY座標加速度dot;
        }

        public CCounter ctLeftCymbal;
        public CCounter ctRightCymbal;
        
        private CTexture txLeftCymbal;
        private CTexture txSnare;
        private CTexture txHitom;
        private CTexture txBassDrum;
        private CTexture txLowTom;
        private CTexture txFloorTom;
        private CTexture txRightCymbal;

        private long nY座標制御タイマ;
        private STパッド状態[] stパッド状態 = new STパッド状態[ 19 ];

        private readonly int[] n描画順 = new int[] { 9, 3, 2, 6, 5, 4, 8, 7, 1, 0 };
        // LP BD SD FT HT LT RD CY HH LC

        private int n座標Snare;
        private int n座標HiTom;
        private int n座標LowTom;
        private int n座標FloorTom;
        private int n座標BassDrum;

        private int nLeftCymbalX;
        private int nLeftCymbalY;
        private int nLeftCymbalFrame;
        private int nLeftCymbalInterval;
        private int nLeftCymbalWidth;

        private int nRightCymbalX;
        private int nRightCymbalY;
        private int nRightCymbalFrame;
        private int nRightCymbalInterval;
        private int nRightCymbalWidth;

        //-----------------
        #endregion
    }
}
