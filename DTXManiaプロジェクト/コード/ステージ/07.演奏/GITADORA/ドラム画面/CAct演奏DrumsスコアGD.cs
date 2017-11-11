using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DTXMania
{
	internal class CAct演奏DrumsスコアGD : CAct演奏スコア共通
	{
		// CActivity 実装（共通クラスからの差分のみ）

		public override unsafe int On進行描画()
        {
            if (!base.b活性化してない)
            {
                if (base.b初めての進行描画)
                {
                    base.n進行用タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
                    base.b初めての進行描画 = false;
                }
                long num = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
                if (num < base.n進行用タイマ)
                {
                    base.n進行用タイマ = num;
                }
                while ((num - base.n進行用タイマ) >= 10)
                {
                    for (int j = 0; j < 3; j++)
                    {
					    this.n現在表示中のスコア[j] += this.nスコアの増分[j];

    					if (this.n現在表示中のスコア[j] > (long) this.n現在の本当のスコア[j])
                            this.n現在表示中のスコア[j] = (long) this.n現在の本当のスコア[j];
                    }
                    base.n進行用タイマ += 10;
                }
            }
            return 0;
        }
	}
}
