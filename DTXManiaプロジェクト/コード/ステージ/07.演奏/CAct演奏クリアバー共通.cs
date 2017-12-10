using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

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
                this.bClearBar[ i ] = new bool[ 30 ];
                this.b区間内でミスをした[ i ] = false;
            }
            this.db区間位置 = new double[ 30 ];
            this.dbEndPos = ( CDTXMania.DTX.listChip.Count > 0 ) ? CDTXMania.DTX.listChip[ CDTXMania.DTX.listChip.Count - 1 ].n発声時刻ms / 1000.0 : 0;
                    
            double db区間 = this.dbEndPos / 30.0;
            for( int i = 0; i < 30; i++ )
            {
                db区間位置[ i ] = db区間 * i;
            }
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
			if( !base.b活性化してない )
			{
                //double[] db区間位置 = new double[ 30 ];
                //if( this.b初めての進行描画 )
                //{
                //    this.dbNowPos = ( ( ( ( double ) CDTXMania.Timer.n現在時刻 ) / 1000.0 ) );
                //    this.dbEndPos = ( CDTXMania.DTX.listChip.Count > 0 ) ? CDTXMania.DTX.listChip[ CDTXMania.DTX.listChip.Count - 1 ].n発声時刻ms / 1000.0 : 0;
                    
                //    double db区間 = this.dbEndPos / 30.0;
                //    for( int i = 0; i < 30; i++ )
                //    {
                //        db区間位置[ i ] = db区間 * i;
                //    }

                //    base.b初めての進行描画 = false;
                //}
                //this.db現在の曲進行割合 = dbNowPos / dbEndPos;

                //for( int j = n現在の区間; j < 30; j++ )
                //{
                //    if( this.dbNowPos >= db区間位置[ j ] )
                //    {
                //        this.tクリアゲージ判定();
                //        this.n現在の区間++;
                //        break;
                //    }
                //}
            }
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

        protected STDGBVALUE<bool[]> bClearBar; //各パートごとにフラグを用意する。
        protected double[] db区間位置;
        protected int n現在の区間;

        protected double dbNowPos;
        protected double dbEndPos;
        protected double db現在の曲進行割合;
        protected STDGBVALUE<bool> b区間内でミスをした;
    }
}
