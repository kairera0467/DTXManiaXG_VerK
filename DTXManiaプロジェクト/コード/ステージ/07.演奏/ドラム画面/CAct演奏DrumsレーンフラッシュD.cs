using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏DrumsレーンフラッシュD : CActivity
	{
		// コンストラクタ

		public CAct演奏DrumsレーンフラッシュD()
		{
            this.rc画像位置[0] = new Rectangle( 0, 0, 64, 128 );
            this.rc画像位置[1] = new Rectangle( 64, 0, 46, 128 );
            this.rc画像位置[2] = new Rectangle( 218, 0, 54, 128 );
            this.rc画像位置[3] = new Rectangle( 158, 0, 60, 128 );
            this.rc画像位置[4] = new Rectangle( 272, 0, 46, 128 );
            this.rc画像位置[5] = new Rectangle( 318, 0, 46, 128 );
            this.rc画像位置[6] = new Rectangle( 364, 0, 46, 128 );
            this.rc画像位置[7] = new Rectangle( 410, 0, 64, 128 );
            this.rc画像位置[8] = new Rectangle( 110, 0, 48, 128 );
            this.rc画像位置[9] = new Rectangle( 426, 128, 36, 128 );
            this.rc画像位置[10] = new Rectangle( 110, 0, 48, 128 );
			base.b活性化してない = true;
		}


		// メソッド

		public void Start( Eレーン lane, float f強弱度合い )
		{
			int num = (int) ( ( 1f - f強弱度合い ) * 55f );
			this.ct進行[ (int) lane ] = new CCounter( 0, 11, 23, CDTXMania.Timer );
		}

        /// <summary>
        /// レーンのX座標をint配列に格納していく。
        /// </summary>
        /// <param name="eLaneType">レーンタイプ</param>
        private void tレーンタイプからレーン位置を設定する( Eタイプ eLaneType, ERDPosition eRDPosition )
        {
            #region[ 共通 ]
            this.nチャンネルtoX座標XG[ 0 ] = 298; //LC
            this.nチャンネルtoX座標XG[ 1 ] = 370; //HHC
            this.nチャンネルtoX座標XG[ 5 ] = 645; //LT
            this.nチャンネルtoX座標XG[ 6 ] = 694; //FT

            #endregion
            #region[ レーンタイプ別 ]
            switch( eLaneType )
            {
                case Eタイプ.A:
                    {
                        this.nチャンネルtoX座標XG[ 2 ] = 470; //SD
                        this.nチャンネルtoX座標XG[ 3 ] = 582; //BD
                        this.nチャンネルtoX座標XG[ 4 ] = 527; //HT
                        this.nチャンネルtoX座標XG[ 8 ] = 419; //LP
                        this.nチャンネルtoX座標XG[ 10 ] = 419; //LBD
                    }
                    break;
                case Eタイプ.B:
                    {
                        this.nチャンネルtoX座標XG[ 2 ] = 419; //SD
                        this.nチャンネルtoX座標XG[ 3 ] = 533; //BD
                        this.nチャンネルtoX座標XG[ 4 ] = 596; //HT
                        this.nチャンネルtoX座標XG[ 8 ] = 476; //LP
                        this.nチャンネルtoX座標XG[ 10 ] = 476; //LBD
                    }
                    break;
                case Eタイプ.C:
                    {
                        this.nチャンネルtoX座標XG[ 2 ] = 470; //SD
                        this.nチャンネルtoX座標XG[ 3 ] = 533; //BD
                        this.nチャンネルtoX座標XG[ 4 ] = 596; //HT
                        this.nチャンネルtoX座標XG[ 8 ] = 419; //LP
                        this.nチャンネルtoX座標XG[ 10 ] = 419; //LBD
                    }
                    break;
                case Eタイプ.D:
                    {
                        this.nチャンネルtoX座標XG[ 2 ] = 419; //SD
                        this.nチャンネルtoX座標XG[ 3 ] = 582; //BD
                        this.nチャンネルtoX座標XG[ 4 ] = 477; //HT
                        this.nチャンネルtoX座標XG[ 8 ] = 527; //LP
                        this.nチャンネルtoX座標XG[ 10 ] = 527; //LBD
                    }
                    break;
            }

            #endregion
            #region [ RC RD ]
            if( eRDPosition == ERDPosition.RCRD )
            {
                this.nチャンネルtoX座標XG[ 7 ] = 748; //RC
                this.nチャンネルtoX座標XG[ 9 ] = 815; //RD
            }
            else
            {
                this.nチャンネルtoX座標XG[ 7 ] = 786; //RC
                this.nチャンネルtoX座標XG[ 9 ] = 746; //RD
            }
            #endregion
        }


		// CActivity 実装

		public override void On活性化()
		{
			for ( int i = 0; i < 11; i++ )
			{
				this.ct進行[ i ] = new CCounter();
			}
            this.tレーンタイプからレーン位置を設定する( CDTXMania.ConfigIni.eLaneType, CDTXMania.ConfigIni.eRDPosition );
			base.On活性化();
		}
		public override void On非活性化()
		{
			for ( int i = 0; i < 11; i++ )
			{
				this.ct進行[ i ] = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if ( !base.b活性化してない )
			{
                this.txLaneFlush = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums LaneFlush.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if ( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txLaneFlush );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if ( !base.b活性化してない )
			{
				for ( int i = 0; i < 11; i++ )
				{
					if ( !this.ct進行[ i ].b停止中 )
					{
						this.ct進行[ i ].t進行();
						if ( this.ct進行[ i ].b終了値に達した )
						{
							this.ct進行[ i ].t停止();
						}
					}
				}
				for( int j = 0; j < 11; j++ )
				{
					if( !this.ct進行[ j ].b停止中 )
					{
                        int x = this.nチャンネルtoX座標XG[ j ];
                        for ( int k = 0; k < 3; k++ )
						{
							if( CDTXMania.ConfigIni.bReverse.Drums )
							{
								int nY = this.ct進行[ j ].n現在の値 * 46;
                                int n透明度 = 255 - ( this.ct進行[ j ].n現在の値 * 21 );
                                if( this.txLaneFlush != null )
                                {
                                    this.txLaneFlush.n透明度 = n透明度;
                                    this.txLaneFlush.t2D上下反転描画( CDTXMania.app.Device, x, ( 146 - 128 ) + nY, this.rc画像位置[ j ] );
								}
							}
							else
							{
								int nY = this.ct進行[ j ].n現在の値 * 46;
                                int n透明度 = 255 - ( this.ct進行[ j ].n現在の値 * 21 );
                                if( this.txLaneFlush != null )
                                {
                                    this.txLaneFlush.n透明度 = n透明度;
                                    this.txLaneFlush.t2D描画( CDTXMania.app.Device, x, 574 - nY, this.rc画像位置[ j ] );
								}
							}
						}
					}
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
        private Rectangle[] rc画像位置 = new Rectangle[ 11 ];
		private CCounter[] ct進行 = new CCounter[ 11 ];
        private CTexture txLaneFlush;
        private int[] nチャンネルtoX座標XG = new int[ 11 ];
		//-----------------
		#endregion
	}
}
