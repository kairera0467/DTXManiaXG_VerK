using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CActオプションパネル : CActivity
	{
		// CActivity 実装

		public override void On非活性化()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txオプションパネル );
				base.On非活性化();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txオプションパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Screen option panels.png" ), false );
				base.OnManagedリソースの作成();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				Device device = CDTXMania.app.Device;
				CConfigIni configIni = CDTXMania.ConfigIni;
				if( this.txオプションパネル != null )
				{
					#region [ スクロール速度表示 ]
					int drums = configIni.n譜面スクロール速度.Drums;
					if( drums > 15 )
					{
						drums = 15;
					}
					this.txオプションパネル.t2D描画(
						device,
						0x171 * Scale.X,
						12 * Scale.Y - 4,
						this.rc譜面スピード[ drums ]
					);
					int guitar = configIni.n譜面スクロール速度.Guitar;
					if( guitar > 15 )
					{
						guitar = 15;
					}
					this.txオプションパネル.t2D描画(
						device,
						0x171 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rc譜面スピード[ guitar ]
					);
					int bass = configIni.n譜面スクロール速度.Bass;
					if( bass > 15 )
					{
						bass = 15;
					}
					this.txオプションパネル.t2D描画(
						device,
						0x171 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rc譜面スピード[ bass ]
					);
					#endregion
					#region [ Sudden/Hidden/Invisible ]
					this.txオプションパネル.t2D描画(
						device,
						0x189 * Scale.X,
						12 * Scale.Y - 4,
						this.rcHS[ ( configIni.bHidden.Drums ? 1 : 0 ) + ( configIni.bSudden.Drums ? 2 : 0 ) +
									( configIni.eInvisible.Drums == EInvisible.SEMI ? 4 : 0 ) +
									( configIni.eInvisible.Drums == EInvisible.FULL ? 5 : 0 ) ] );
					this.txオプションパネル.t2D描画(
						device,
						0x189 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rcHS[ ( configIni.bHidden.Guitar ? 1 : 0 ) + ( configIni.bSudden.Guitar ? 2 : 0 ) +
									( configIni.eInvisible.Guitar == EInvisible.SEMI ? 4 : 0 ) +
									( configIni.eInvisible.Guitar == EInvisible.FULL ? 5 : 0 ) ] );
					this.txオプションパネル.t2D描画(
						device,
						0x189 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rcHS[ ( configIni.bHidden.Bass ? 1 : 0 ) + ( configIni.bSudden.Bass ? 2 : 0 ) +
									( configIni.eInvisible.Bass == EInvisible.SEMI ? 4 : 0 ) +
									( configIni.eInvisible.Bass == EInvisible.FULL ? 5 : 0 ) ] );
					#endregion
					#region [ Dark ]
					this.txオプションパネル.t2D描画(
						device,
						0x1a1 * Scale.X,
						12 * Scale.Y - 4,
						this.rcDark[ (int) configIni.eDark ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x1a1 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rcDark[ (int) configIni.eDark ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x1a1 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rcDark[ (int) configIni.eDark ]
					);
					#endregion
					#region [ Reverse ]
					this.txオプションパネル.t2D描画(
						device,
						0x1b9 * Scale.X,
						12 * Scale.Y - 4,
						this.rcReverse[ configIni.bReverse.Drums ? 1 : 0 ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x1b9 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rcReverse[ configIni.bReverse.Guitar ? 1 : 0 ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x1b9 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rcReverse[ configIni.bReverse.Bass ? 1 : 0 ]
					);
					#endregion
					#region [ 判定文字表示位置 ]
					this.txオプションパネル.t2D描画(
						device,
						0x1d1 * Scale.X,
						12 * Scale.Y - 4,
						this.rcPosition[ (int) configIni.判定文字表示位置.Drums ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x1d1 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rcPosition[ (int) configIni.判定文字表示位置.Guitar ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x1d1 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rcPosition[ (int) configIni.判定文字表示位置.Bass ]
					);
					#endregion
					#region [ Tight ]
					this.txオプションパネル.t2D描画(
						device,
						0x1e9 * Scale.X,
						12 * Scale.Y - 4,
						this.rcTight[ configIni.bTight ? 1 : 0 ]
					);
					#endregion
					#region [ Random ]
					this.txオプションパネル.t2D描画(
						device,
						0x1e9 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rcRandom[ (int) configIni.eRandom.Guitar ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x1e9 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rcRandom[ (int) configIni.eRandom.Bass ]
					);
					#endregion
					#region [ ComboPosigion ]
					this.txオプションパネル.t2D描画(
						device,
						0x201 * Scale.X,
						12 * Scale.Y - 4,
						this.rcComboPos[ (int) configIni.ドラムコンボ文字の表示位置 ]
					);
					#endregion
					#region [ Light ]
					this.txオプションパネル.t2D描画(
						device,
						0x201 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rcLight[ configIni.bLight.Guitar ? 1 : 0 ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x201 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rcLight[ configIni.bLight.Bass ? 1 : 0 ] 
					);
					#endregion
					#region [ Left ]
					this.txオプションパネル.t2D描画(
						device,
						0x219 * Scale.X,
						0x18 * Scale.Y - 4,
						this.rcLeft[ configIni.bLeft.Guitar ? 1 : 0 ]
					);
					this.txオプションパネル.t2D描画(
						device,
						0x219 * Scale.X,
						0x24 * Scale.Y - 4,
						this.rcLeft[ configIni.bLeft.Bass ? 1 : 0 ]
					);
					#endregion
				}
			}
			return 0;
		}

		
		// その他

		#region [ private ]
		//-----------------
		private readonly Rectangle[] rcComboPos = new Rectangle[] {
			new Rectangle( (int)(0x30 * Scale.X), (int)(0x48 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x30 * Scale.X), (int)(60 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x30 * Scale.X), (int)(0x30 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x18 * Scale.X), (int)(0x48 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private readonly Rectangle[] rcDark = new Rectangle[] {
			new Rectangle( (int)(0x18 * Scale.X), (int)(0 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x18 * Scale.X), (int)(12 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x18 * Scale.X), (int)(0x54 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private readonly Rectangle[] rcHS = new Rectangle[] {
			new Rectangle( (int)(0 * Scale.X), (int)(0 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),		// OFF
			new Rectangle( (int)(0 * Scale.X), (int)(12 * Scale.Y),(int)( 0x18 * Scale.X), (int)(12 * Scale.Y) ),		// Hidden
			new Rectangle( (int)(0 * Scale.X), (int)(0x18 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),		// Sudden
			new Rectangle( (int)(0 * Scale.X), (int)(0x24 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),		// H/S
			new Rectangle( (int)(0x60 * Scale.X), (int)(0x54 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),	// Semi-Invisible
			new Rectangle( (int)(120 * Scale.X), (int)(0x54 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )	// Full-Invisible
		};
		private readonly Rectangle[] rcLeft = new Rectangle[] {
			new Rectangle( (int)(0x60 * Scale.X), (int)(0x48 * Scale.Y),(int)( 0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(120 * Scale.X),(int)( 0x48 * Scale.Y),(int)( 0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private readonly Rectangle[] rcLight = new Rectangle[] {
			new Rectangle( (int)(120 * Scale.X), (int)(0x30 * Scale.Y),(int)( 0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(120 * Scale.X), (int)(60 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private readonly Rectangle[] rcPosition = new Rectangle[] {
			new Rectangle( (int)( 0 * Scale.X), (int)( 48 * Scale.Y), (int)( 24 * Scale.X), (int)(12 * Scale.Y) ),		// P-A
			new Rectangle( (int)( 0 * Scale.X), (int)( 60 * Scale.Y), (int)( 24 * Scale.X), (int)(12 * Scale.Y) ),		// P-B
			new Rectangle( (int)( 0 * Scale.X), (int)( 72 * Scale.Y), (int)( 24 * Scale.X), (int)(12 * Scale.Y) ),		// P-C
			new Rectangle( (int)(24 * Scale.X), (int)( 72 * Scale.Y), (int)( 24 * Scale.X), (int)(12 * Scale.Y) )		// OFF
		};
		private readonly Rectangle[] rcRandom = new Rectangle[] {
			new Rectangle( (int)(0x48 * Scale.X), (int)(0x30 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x48 * Scale.X), (int)(60 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x48 * Scale.X), (int)(0x48 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x48 * Scale.X), (int)(0x54 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private readonly Rectangle[] rcReverse = new Rectangle[] {
			new Rectangle( (int)(0x18 * Scale.X), (int)(0x18 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x18 * Scale.X), (int)(0x24 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private readonly Rectangle[] rcTight = new Rectangle[] {
			new Rectangle( (int)(0x60 * Scale.X), (int)(0x30 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x60 * Scale.X), (int)(60 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private readonly Rectangle[] rc譜面スピード = new Rectangle[] {
			new Rectangle( (int)(0x30 * Scale.X), (int)(0 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x30 * Scale.X), (int)(12 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x30 * Scale.X), (int)(0x18 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x30 * Scale.X), (int)(0x24 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x48 * Scale.X), (int)(0 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x48 * Scale.X), (int)(12 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x48 * Scale.X), (int)(0x18 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x48 * Scale.X), (int)(0x24 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x60 * Scale.X), (int)(0 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x60 * Scale.X), (int)(12 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x60 * Scale.X), (int)(0x18 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(0x60 * Scale.X), (int)(0x24 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(120 * Scale.X), (int)(0 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(120 * Scale.X), (int)(12 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(120 * Scale.X), (int)(0x18 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) ),
			new Rectangle( (int)(120 * Scale.X), (int)(0x24 * Scale.Y), (int)(0x18 * Scale.X), (int)(12 * Scale.Y) )
		};
		private CTexture txオプションパネル;
		//-----------------
		#endregion
	}
}
