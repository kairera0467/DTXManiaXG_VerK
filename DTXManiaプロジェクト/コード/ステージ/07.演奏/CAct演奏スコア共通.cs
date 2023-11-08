using System;
using System.Collections.Generic;
using System.Text;
using FDK;

namespace DTXMania
{
	internal class CAct演奏スコア共通 : CActivity
	{
		// プロパティ

		protected STDGBVALUE<long> nスコアの増分;
        public STDGBVALUE<int>[] x位置 = new STDGBVALUE<int>[ 10 ];
		public STDGBVALUE<double> n現在の本当のスコア;
		public STDGBVALUE<long> n現在表示中のスコア;
		protected long n進行用タイマ;

        protected STDGBVALUE<double> nスコア_ボーナス含まない;
        protected STDGBVALUE<double> nスコア_ボーナスのみ;

		
		// コンストラクタ

		public CAct演奏スコア共通()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public double Get( E楽器パート part )
		{
			return this.n現在の本当のスコア[ (int) part ];
		}
		public void Set( E楽器パート part, double nScore )
		{
			int nPart = (int) part;
			if( this.n現在の本当のスコア[ nPart ] != nScore )
			{
				this.n現在の本当のスコア[ nPart ] = nScore;
				this.nスコアの増分[ nPart ] = (long) ( ( (double) ( this.n現在の本当のスコア[ nPart ] - this.n現在表示中のスコア[ nPart ] ) ) / 20.0 );
				if( this.nスコアの増分[ nPart ] < 1L )
				{
					this.nスコアの増分[ nPart ] = 1L;
				}
			}
		}
		/// <summary>
		/// 点数を加える(各種AUTO補正つき)
		/// </summary>
		/// <param name="part"></param>
		/// <param name="bAutoPlay"></param>
		/// <param name="delta"></param>
		public void Add( E楽器パート part, STAUTOPLAY bAutoPlay, long delta )
		{
			double rev = 1.0;
			switch ( part )
			{
				#region [ Unknown ]
				case E楽器パート.UNKNOWN:
					throw new ArgumentException();
				#endregion
				#region [ Drums ]
				case E楽器パート.DRUMS:
					if ( !CDTXMania.ConfigIni.bドラムが全部オートプレイである )
					{
						#region [ Auto BD ]
						if ( bAutoPlay.BD == true )
						{
							rev /= 2;
						}
						#endregion
					}
					break;
				#endregion
				#region [ Gutiar ]
				case E楽器パート.GUITAR:
					if ( !CDTXMania.ConfigIni.bギターが全部オートプレイである )
					{
						#region [ Auto Wailing ]
						if ( bAutoPlay.GtW )
						{
							rev /= 2;
						}
						#endregion
						#region [ Auto Pick ]
						if ( bAutoPlay.GtPick )
						{
							rev /= 3;
						}
						#endregion
						#region [ Auto Neck ]
						if ( bAutoPlay.GtR || bAutoPlay.GtG || bAutoPlay.GtB || bAutoPlay.GtY || bAutoPlay.GtP )
						{
							rev /= 4;
						}
						#endregion
					}
					break;
				#endregion
				#region [ Bass ]
				case E楽器パート.BASS:
					if ( !CDTXMania.ConfigIni.bベースが全部オートプレイである )
					{
						#region [ Auto Wailing ]
						if ( bAutoPlay.BsW )
						{
							rev /= 2;
						}
						#endregion
						#region [ Auto Pick ]
						if ( bAutoPlay.BsPick )
						{
							rev /= 3;
						}
						#endregion
						#region [ Auto Neck ]
						if ( bAutoPlay.BsR || bAutoPlay.BsG || bAutoPlay.BsB || bAutoPlay.BsY || bAutoPlay.BsP )
						{
							rev /= 4;
						}
						#endregion
					}
					break;
				#endregion
			}
			this.Set( part, this.Get( part ) + delta * rev );
		}

        /// <summary>
        /// 全楽器パートにおいてスコアの情報をリセットする。(On活性化相当の処理)
        /// </summary>
        public void Reset()
        {
			this.n進行用タイマ = -1;
			for( int i = 0; i < 3; i++ )
			{
				this.n現在表示中のスコア[ i ] = 0L;
				this.n現在の本当のスコア[ i ] = 0L;
				this.nスコアの増分[ i ] = 0L;
			}
        }


		// CActivity 実装

		public override void On活性化()
		{
			this.n進行用タイマ = -1;
			for( int i = 0; i < 3; i++ )
			{
				this.n現在表示中のスコア[ i ] = 0L;
				this.n現在の本当のスコア[ i ] = 0L;
				this.nスコアの増分[ i ] = 0L;
			}
			base.On活性化();
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
	}
}
