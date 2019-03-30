using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;
using SharpDX.Direct3D9;

namespace DTXMania
{
    internal class CAct演奏GuitarレーンGD : CActivity
    {
        /// <summary>
        /// レーンを描画するクラス。
        /// レーンの他にもクリップのウィンドウ表示も兼ねる。要はクラス作成が面倒だっただけ。
        /// 
        /// 課題
        /// ・レーンの透明度対応
        /// </summary>
        public CAct演奏GuitarレーンGD()
        {
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            for( int i = 0; i < 10; i++ )
            {
                this.ct進行[ i ] = new CCounter();
            }
            base.On活性化();
        }

        public override void On非活性化()
        {
            for( int i = 0; i < 10; i++ )
            { 
                this.ct進行[ i ] = null;
            }
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            if( !this.b活性化してない )
            {
                for( int i = 0; i < 10; i++ )
                {
                    this.txLane[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Paret.png" ) );
                }
                this.txLaneBackground = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Guitar_Lane_Back.png" ) );
                this.txLaneShadow = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Guitar LaneShadow.png" ) );
                this.txLaneBackground_Dark = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile black 64x64.png" ) );
                base.OnManagedリソースの作成();
            }
        }

        public override void OnManagedリソースの解放()
        {
            if( !this.b活性化してない )
            {
                for( int i = 0; i < 10; i++ )
                { 
                    CDTXMania.tテクスチャの解放( ref this.txLane[ i ] );
                }
                CDTXMania.tテクスチャの解放( ref this.txLaneBackground );
                CDTXMania.tテクスチャの解放( ref this.txLaneShadow );
                CDTXMania.tテクスチャの解放( ref this.txLaneBackground_Dark );
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if( CDTXMania.bXGRelease )
            {
			    for( int i = 0; i < 10; i++ )
    			{
	    			if( !this.ct進行[ i ].b停止中 )
		    		{
			    		this.ct進行[ i ].t進行();
				    	if ( this.ct進行[ i ].b終了値に達した )
					    {
						    this.ct進行[ i ].t停止();
    					}
	    			}
	            }
            }
            int[] n本体X = new int[] { 80, 948 };
            for( int i = 0; i < 2; i++ )
            {
                if( i == 0 ? CDTXMania.DTX.bチップがある.Guitar : CDTXMania.DTX.bチップがある.Bass )
                {
                    if( this.txLaneShadow != null )
                    {
                        this.txLaneShadow.t2D描画( CDTXMania.app.Device, ( n本体X[ i ] + 126 ) - this.txLaneShadow.sz画像サイズ.Width / 2, 0 );
                    }
                    if( this.txLaneBackground != null )
                    {
                        this.txLaneBackground.t2D描画( CDTXMania.app.Device, n本体X[ i ], 0 );
                    }
                    if( this.txLaneBackground_Dark != null && CDTXMania.ConfigIni.nLaneDispType[ i + 1 ] == 1 || CDTXMania.ConfigIni.nLaneDispType[ i + 1 ] == 3 )
                    {
                        this.txLaneBackground_Dark.t2D描画( CDTXMania.app.Device, n本体X[ i ] + 6, 0, new Rectangle( 0, 0, 197, 720 ) );
                    }
                }
            }

            return base.On進行描画();
        }

		public void Start( Eレーン lane )
		{
			this.ct進行[ (int) lane ] = new CCounter( 0, 8, 12, CDTXMania.Timer );
		}

        #region[ private ]
        //-----------------
        private CTexture[] txLane = new CTexture[ 10 ];
        private CCounter[] ct進行 = new CCounter[ 10 ];
        public CTexture txClip;
        private CTexture txLaneBackground;
        private CTexture txLaneBackground_Dark;
        private CTexture txLaneShadow;
        

        //-----------------
        #endregion
    }
}
