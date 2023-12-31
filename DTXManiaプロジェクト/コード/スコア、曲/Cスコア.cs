﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace DTXMania
{
	[Serializable]
	internal class Cスコア
	{
		// プロパティ

		public STScoreIni情報 ScoreIni情報;
		[Serializable]
		[StructLayout( LayoutKind.Sequential )]
		public struct STScoreIni情報
		{
			public DateTime 最終更新日時;
			public long ファイルサイズ;

			public STScoreIni情報( DateTime 最終更新日時, long ファイルサイズ )
			{
				this.最終更新日時 = 最終更新日時;
				this.ファイルサイズ = ファイルサイズ;
			}
		}

		public STファイル情報 ファイル情報;
		[Serializable]
		[StructLayout( LayoutKind.Sequential )]
		public struct STファイル情報
		{
			public string ファイルの絶対パス;
			public string フォルダの絶対パス;
			public DateTime 最終更新日時;
			public long ファイルサイズ;

			public STファイル情報( string ファイルの絶対パス, string フォルダの絶対パス, DateTime 最終更新日時, long ファイルサイズ )
			{
				this.ファイルの絶対パス = ファイルの絶対パス;
				this.フォルダの絶対パス = フォルダの絶対パス;
				this.最終更新日時 = 最終更新日時;
				this.ファイルサイズ = ファイルサイズ;
			}
		}

		public ST譜面情報 譜面情報;
		[Serializable]
		[StructLayout( LayoutKind.Sequential )]
		public struct ST譜面情報
		{
			public string タイトル;
			public string アーティスト名;
			public string コメント;
			public string ジャンル;
			public string Preimage;
			public string Premovie;
			public string Presound;
			public string Backgound;
			public STDGBVALUE<int> レベル;
            public STDGBVALUE<int> レベルDec;
			public STRANK 最大ランク;
			public STSKILL 最大スキル;
            public STMUSICSKILL 最大曲別スキル;
			public STDGBVALUE<bool> フルコンボ;
			public STDGBVALUE<int> 演奏回数;
			public STHISTORY 演奏履歴;
			public bool レベルを非表示にする;
			public CDTX.E種別 曲種別;
			public double Bpm;
			public int Duration;
            public STDGBVALUE<bool> b完全にCLASSIC譜面である;
            public STDGBVALUE<bool> b譜面がある;
            public int n可視チップ数_HH;
            public int n可視チップ数_SD;
            public int n可視チップ数_BD;
            public int n可視チップ数_HT;
            public int n可視チップ数_LT;
            public int n可視チップ数_CY;
            public int n可視チップ数_FT;
            public int n可視チップ数_HHO;
            public int n可視チップ数_RD;
            public int n可視チップ数_LC;
            public int n可視チップ数_LP;
            public int n可視チップ数_LBD;
            public int n可視チップ数_Guitar;
            public int n可視チップ数_Guitar_R;
            public int n可視チップ数_Guitar_G;
            public int n可視チップ数_Guitar_B;
            public int n可視チップ数_Guitar_Y;
            public int n可視チップ数_Guitar_P;
            public int n可視チップ数_Guitar_OP;
            public int n可視チップ数_Bass;
            public int n可視チップ数_Bass_R;
            public int n可視チップ数_Bass_G;
            public int n可視チップ数_Bass_B;
            public int n可視チップ数_Bass_Y;
            public int n可視チップ数_Bass_P;
            public int n可視チップ数_Bass_OP;
            public double 最低Bpm;
            public double 最大Bpm;

			[Serializable]
			[StructLayout( LayoutKind.Sequential )]
			public struct STHISTORY
			{
				public string 行1;
				public string 行2;
				public string 行3;
				public string 行4;
				public string 行5;
				public string this[ int index ]
				{
					get
					{
						switch( index )
						{
							case 0:
								return this.行1;

							case 1:
								return this.行2;

							case 2:
								return this.行3;

							case 3:
								return this.行4;

							case 4:
								return this.行5;
						}
						throw new IndexOutOfRangeException();
					}
					set
					{
						switch( index )
						{
							case 0:
								this.行1 = value;
								return;

							case 1:
								this.行2 = value;
								return;

							case 2:
								this.行3 = value;
								return;

							case 3:
								this.行4 = value;
								return;

							case 4:
								this.行5 = value;
								return;
						}
						throw new IndexOutOfRangeException();
					}
				}
			}

			[Serializable]
			[StructLayout( LayoutKind.Sequential )]
			public struct STRANK
			{
				public int Drums;
				public int Guitar;
				public int Bass;
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
						if ( ( value < (int)CScoreIni.ERANK.SS ) || ( ( value != (int)CScoreIni.ERANK.UNKNOWN ) && ( value > (int)CScoreIni.ERANK.E ) ) )
						{
							throw new ArgumentOutOfRangeException();
						}
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
			}

			[Serializable]
			[StructLayout( LayoutKind.Sequential )]
			public struct STSKILL
			{
				public double Drums;
				public double Guitar;
				public double Bass;
				public double this[ int index ]
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
						if( ( value < 0.0 ) || ( value > 100.0 ) )
						{
							throw new ArgumentOutOfRangeException();
						}
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
			}

			[Serializable]
			[StructLayout( LayoutKind.Sequential )]
			public struct STMUSICSKILL
			{
				public double Drums;
				public double Guitar;
				public double Bass;
				public double this[ int index ]
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
						if( ( value < 0.0 ) || ( value > 200.0 ) )
						{
							throw new ArgumentOutOfRangeException();
						}
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
			}

            public STDGBVALUE<string> strレベル小数点含
            {
                get
                {
                    STDGBVALUE<string> ret = new STDGBVALUE<string>();
                    for( int i = 0; i < 3; i++ )
                    {
                        int n整数 = this.レベル[ i ] / 10;
                        int n小数 = ( this.レベル[ i ] - ( n整数 * 10 ) ) * 10;
                        n小数 += this.レベルDec[ i ];

                        ret[ i ] += string.Format( "{0:0}", n整数 );
                        ret[ i ] += ".";
                        ret[ i ] += string.Format( "{0,2:00}", n小数 );
                    }
                    return ret;
                }
            }
		}

		public bool bSongDBにキャッシュがあった;
		public bool bスコアが有効である
		{
			get
			{
				return ( ( ( this.譜面情報.レベル[ 0 ] + this.譜面情報.レベル[ 1 ] ) + this.譜面情報.レベル[ 2 ] ) != 0 );
			}
		}


		// コンストラクタ

		public Cスコア()
		{
			this.ScoreIni情報 = new STScoreIni情報( DateTime.MinValue, 0L );
			this.bSongDBにキャッシュがあった = false;
			this.ファイル情報 = new STファイル情報( "", "", DateTime.MinValue, 0L );
			this.譜面情報 = new ST譜面情報();
			this.譜面情報.タイトル = "";
			this.譜面情報.アーティスト名 = "";
			this.譜面情報.コメント = "";
			this.譜面情報.ジャンル = "";
			this.譜面情報.Preimage = "";
			this.譜面情報.Premovie = "";
			this.譜面情報.Presound = "";
			this.譜面情報.Backgound = "";
			this.譜面情報.レベル = new STDGBVALUE<int>();
            this.譜面情報.レベルDec = new STDGBVALUE<int>();
			this.譜面情報.最大ランク = new ST譜面情報.STRANK();
			this.譜面情報.最大ランク.Drums =  (int)CScoreIni.ERANK.UNKNOWN;
			this.譜面情報.最大ランク.Guitar = (int)CScoreIni.ERANK.UNKNOWN;
			this.譜面情報.最大ランク.Bass =   (int)CScoreIni.ERANK.UNKNOWN;
			this.譜面情報.フルコンボ = new STDGBVALUE<bool>();
			this.譜面情報.演奏回数 = new STDGBVALUE<int>();
			this.譜面情報.演奏履歴 = new ST譜面情報.STHISTORY();
			this.譜面情報.演奏履歴.行1 = "";
			this.譜面情報.演奏履歴.行2 = "";
			this.譜面情報.演奏履歴.行3 = "";
			this.譜面情報.演奏履歴.行4 = "";
			this.譜面情報.演奏履歴.行5 = "";
			this.譜面情報.レベルを非表示にする = false;
			this.譜面情報.最大スキル = new ST譜面情報.STSKILL();
            this.譜面情報.最大曲別スキル = new ST譜面情報.STMUSICSKILL();
			this.譜面情報.曲種別 = CDTX.E種別.DTX;
			this.譜面情報.Bpm = 120.0;
			this.譜面情報.Duration = 0;
            this.譜面情報.b完全にCLASSIC譜面である.Drums = false;
            this.譜面情報.b完全にCLASSIC譜面である.Guitar = false;
            this.譜面情報.b完全にCLASSIC譜面である.Bass = false;
            this.譜面情報.b譜面がある.Drums = false;
            this.譜面情報.b譜面がある.Guitar = false;
            this.譜面情報.b譜面がある.Bass = false;
            this.譜面情報.n可視チップ数_HH = 0;
            this.譜面情報.n可視チップ数_SD = 0;
            this.譜面情報.n可視チップ数_BD = 0;
            this.譜面情報.最低Bpm = 120.0;
            this.譜面情報.最大Bpm = 120.0;
		}
	}
}
