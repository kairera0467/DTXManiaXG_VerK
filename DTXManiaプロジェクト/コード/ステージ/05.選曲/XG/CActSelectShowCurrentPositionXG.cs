using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActSelectShowCurrentPositionXG : CActSelectShowCurrentPosition共通
	{
		// メソッド

		public CActSelectShowCurrentPositionXG()
		{
			base.b活性化してない = true;
		}

		// CActivity 実装

		public override void On活性化()
		{
			if ( this.b活性化してる )
				return;
            this.n決定演出用X = 0;
			base.On活性化();
		}
		public override void On非活性化()
		{
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if ( !base.b活性化してない )
			{
				this.txScrollBar = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_scrollbar.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if ( !base.b活性化してない )
			{
				CDTXMania.t安全にDisposeする( ref this.txScrollBar );

				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			#region [ スクロールバーの描画 #27648 ]
			if ( this.txScrollBar != null )
			{
				this.txScrollBar.t2D描画( CDTXMania.app.Device, ( 1280 - ( ( 429.0f / 100.0f ) * CDTXMania.stage選曲XG.ct登場時アニメ用共通.n現在の値 ) ) + this.n決定演出用X, 164, new Rectangle( 0, 0, 352, 26 ) ); //移動後のxは851
			}
			#endregion
			#region [ スクロール地点の描画 (計算はCActSelect曲リストで行う。スクロール位置と選曲項目の同期のため。)#27648 ]
			if ( this.txScrollBar != null )
			{
				int py = CDTXMania.stage選曲XG.nスクロールバー相対y座標;
				if( py <= 336 && py >= 0 )
				{
					this.txScrollBar.t2D描画( CDTXMania.app.Device, ( 1280 - 4 - ( ( 424.0f / 100.0f ) * CDTXMania.stage選曲XG.ct登場時アニメ用共通.n現在の値 ) ) + py + this.n決定演出用X, 164, new Rectangle( 352, 0, 26, 26 ) );//856
				}
			}
			#endregion
            if( CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_決定演出 || CDTXMania.r現在のステージ.eフェーズID == CStage.Eフェーズ.選曲_NowLoading画面へのフェードアウト )
            {
                this.n決定演出用X = ( CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 <= 250 && CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 >= 0 ? (int)( 429 * ( ( CDTXMania.stage選曲XG.ct決定演出待機.n現在の値 ) / 250.0 ) ) : 429 );
            }
            else
            {
                this.tアイテム数の描画();
            }

			return 0;
		}

        /// <summary>
        /// アイテム数と現在の位置を描画する。
        /// (CActSelect曲リスト.csから移植)
        /// </summary>
        /// <param name="nCurrentPos"></param>
        /// <param name="nItemCount"></param>
        public void tアイテム数の描画()
        {
            string s = CDTXMania.stage選曲XG.act曲リスト.nCurrentPosition.ToString() + "/" + CDTXMania.stage選曲XG.act曲リスト.nNumOfItems.ToString();
            int x = 1150;
            int y = 200;

            for (int p = s.Length - 1; p >= 0; p--)
            {
                tアイテム数の描画_１桁描画(x, y, s[p]);
                x -= 16;
            }
        }
        private void tアイテム数の描画_１桁描画(int x, int y, char s数値)
        {
            int dx, dy;
            if( s数値 == '/' )
            {
                dx = 96;
                dy = 0;
            }
            else
            {
                int n = (int)s数値 - (int)'0';
                dx = (n % 6) * 16;
                dy = (n / 6) * 16;
            }
            if( this.txScrollBar != null )
            {
                this.txScrollBar.t2D描画(CDTXMania.app.Device, x, y, new Rectangle( dx, dy + 32, 16, 16 ) );
            }
        }

		// その他

		#region [ private ]
		//-----------------
		private CTexture txScrollBar;
        private int n決定演出用X;
		//-----------------
		#endregion
	}
}
