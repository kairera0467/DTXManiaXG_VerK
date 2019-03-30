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
            if( !base.b活性化してない )
            {
                if( base.b初めての進行描画 )
                {
                    base.n進行用タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
                    base.b初めての進行描画 = false;
                }
                long num = FDK.CSound管理.rc演奏用タイマ.n現在時刻;

                #region [ スコアを桁数ごとに n位の数[] に格納する。CAct演奏コンボ共通の使いまわし。 ]
			    //-----------------
			    //座標側の管理が複雑になるので、コンボとは違い、右から数値を入れていく。
                int n = (int)this.n現在表示中のスコア.Drums;
			    int n桁数 = 0;
                int[] n位の数 = new int[ 7 ];
			    while( ( n > 0 ) && ( n桁数 < 7 ) )
			    {
			    	n位の数[ 6 - n桁数 ] = n % 10;
			    	n = ( n - ( n % 10 ) ) / 10;
			    	n桁数++;
			    }

                int n2 = (int)this.n現在の本当のスコア.Drums;
                int n桁数2 = 0;
                int[] n位の数2 = new int[ 7 ];
                while ((n2 > 0) && (n桁数2 < 7))
                {
                    n位の数2[ 6 - n桁数2 ] = n2 % 10;
                    n2 = (n2 - (n2 % 10)) / 10;
                    n桁数2++;
                }
			    //-----------------
			    #endregion

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
                for (int s = 0; s < 7; s++)
                {
                    // 2019.1.13 kairera0467 とりあえず7桁の場合のみ実装
                    if( n位の数[s] == n位の数2[s] )
                    {
                        base.x位置[s].Drums = 0;
                    }
                    else
                    {
                        base.x位置[s].Drums = 4;
                    }
                }
            }
            return 0;
        }
	}
}
