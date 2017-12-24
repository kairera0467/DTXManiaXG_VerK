using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using FDK;

namespace DTXMania
{
	internal class CStageコンフィグ : CStage
	{
		// プロパティ

		//public CActDFPFont actFont { get; private set; }


		// コンストラクタ

		public CStageコンフィグ()
		{
			//CActDFPFont font;
			base.eステージID = CStage.Eステージ.コンフィグ;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			//this.actFont = font = new CActDFPFont();
			//base.list子Activities.Add( font );
			base.list子Activities.Add( this.actFIFO = new CActFIFOWhite() );
			base.list子Activities.Add( this.actKeyAssign = new CActConfigKeyAssign() );
			base.list子Activities.Add( this.actオプションパネル = new CActオプションパネル() );
			base.b活性化してない = true;
		}
		
		
		// メソッド

		public void tアサイン完了通知()															// CONFIGにのみ存在
		{																						//
			this.eItemPanelモード = EItemPanelモード.パッド一覧;								//
		}																						//
		public void tパッド選択通知( EKeyConfigPart part, EKeyConfigPad pad )							//
		{																						//
			this.actKeyAssign.t開始( part, pad, this.actList.ib現在の選択項目.str項目名 );		//
			this.eItemPanelモード = EItemPanelモード.キーコード一覧;							//
		}																						//
		public void t項目変更通知()																// OPTIONと共通
		{																						//
			this.actList.t説明文パネルに現在選択されている項目の説明を描画する();				//
		}																						//

		
		// CStage 実装

		public override void On活性化()
		{
            if( CDTXMania.bXGRelease )
			    base.list子Activities.Add( this.actList = new CActConfigListXG() );
            else
			    base.list子Activities.Add( this.actList = new CActConfigListGD() );
			Trace.TraceInformation( "コンフィグステージを活性化します。" );
			Trace.Indent();
			try
			{
				this.n現在のメニュー番号 = 0;													//
				for ( int i = 0; i < 4; i++ )													//
				{																				//
					this.ctキー反復用[ i ] = new CCounter( 0, 0, 0, CDTXMania.Timer );			//
				}																				//
				this.bメニューにフォーカス中 = true;											// ここまでOPTIONと共通
				this.eItemPanelモード = EItemPanelモード.パッド一覧;
			}
			finally
			{
				Trace.TraceInformation( "コンフィグステージの活性化を完了しました。" );
				Trace.Unindent();
			}
			base.On活性化();		// 2011.3.14 yyagi: On活性化()をtryの中から外に移動
		}
		public override void On非活性化()
		{
			Trace.TraceInformation( "コンフィグステージを非活性化します。" );
			Trace.Indent();
			try
			{
				CDTXMania.ConfigIni.t書き出し( CDTXMania.strEXEのあるフォルダ + "Config.ini" );	// CONFIGだけ
				for( int i = 0; i < 4; i++ )
				{
					this.ctキー反復用[ i ] = null;
				}
				base.On非活性化();
			}
			catch ( UnauthorizedAccessException e )
			{
				Trace.TraceError( e.Message + "ファイルが読み取り専用になっていないか、管理者権限がないと書き込めなくなっていないか等を確認して下さい" );
			}
			catch ( Exception e )
			{
				Trace.TraceError( e.Message );
			}
			finally
			{
                base.list子Activities.Remove( this.actList );
				Trace.TraceInformation( "コンフィグステージの非活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void OnManagedリソースの作成()											// OPTIONと画像以外共通
		{
			if( !base.b活性化してない )
			{
				this.tx背景 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\4_background.png" ), false );
				this.tx上部パネル = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\4_header panel.png" ), true );
				this.tx下部パネル = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\4_footer panel.png" ), true );
				this.txMenuカーソル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\4_menu cursor.png" ), false );

				prvFont = new CPrivateFastFont( CSkin.Path( @"Graphics\fonts\mplus-1p-heavy.ttf" ), 20 );
				string[] strMenuItem = { "System", "Drums", "Guitar", "Bass", "Exit" };
				txMenuItemLeft = new CTexture[ strMenuItem.Length, 2 ];
				for ( int i = 0; i < strMenuItem.Length; i++ )
				{
					Bitmap bmpStr;
					bmpStr = prvFont.DrawPrivateFont( strMenuItem[ i ], Color.White, Color.Black );
					txMenuItemLeft[ i, 0 ] = CDTXMania.tテクスチャの生成( bmpStr, false );
					bmpStr.Dispose();
					bmpStr = prvFont.DrawPrivateFont( strMenuItem[ i ], Color.White, Color.Black, Color.Yellow, Color.OrangeRed );
					txMenuItemLeft[ i, 1 ] = CDTXMania.tテクスチャの生成( bmpStr, false );
					bmpStr.Dispose();
				}
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()											// OPTIONと同じ(COnfig.iniの書き出しタイミングのみ異なるが、無視して良い)
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx背景 );
				CDTXMania.tテクスチャの解放( ref this.tx上部パネル );
				CDTXMania.tテクスチャの解放( ref this.tx下部パネル );
				CDTXMania.tテクスチャの解放( ref this.txMenuカーソル );
				prvFont.Dispose();
				for ( int i = 0; i < txMenuItemLeft.GetLength( 0 ); i++ )
				{
					txMenuItemLeft[ i, 0 ].Dispose();
					txMenuItemLeft[ i, 0 ] = null;
					txMenuItemLeft[ i, 1 ].Dispose();
					txMenuItemLeft[ i, 1 ] = null;
				}
				txMenuItemLeft = null;
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない )
				return 0;

			if( base.b初めての進行描画 )
			{
				base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
				this.actFIFO.tフェードイン開始();
				base.b初めての進行描画 = false;
			}

			// 描画

			#region [ 背景 ]
			//---------------------
			if( this.tx背景 != null )
				this.tx背景.t2D描画( CDTXMania.app.Device, 0, 0 );
			//---------------------
			#endregion
			#region [ メニューカーソル ]
			//---------------------
			if ( this.txMenuカーソル != null )
			{
				Rectangle rectangle;
				this.txMenuカーソル.n透明度 = this.bメニューにフォーカス中 ? 0xff : 0x80;
				int x = 111;
				int y = 144 + ( this.n現在のメニュー番号 * 38 );
				int num3 = 340;
				this.txMenuカーソル.t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 0, 0, 32, 48 ) );
				this.txMenuカーソル.t2D描画( CDTXMania.app.Device, ( x + num3 ) - 32, y, new Rectangle( 20, 0, 32, 48 ) );
				x += 32;
				for ( num3 -= (int) ( 0x20 * Scale.X ); num3 > 0; num3 -= rectangle.Width )
				{
					rectangle = new Rectangle( 16, 0, 32, 48 );
					if ( num3 < 32 )
					{
						rectangle.Width -= 32 - num3;
					}
					this.txMenuカーソル.t2D描画( CDTXMania.app.Device, x, y, rectangle );
					x += rectangle.Width;
				}
			}
			//---------------------
			#endregion
			#region [ メニュー ]
			//---------------------
			int menuY = 162 - 22;
			int stepY = 39;
			for ( int i = 0; i < txMenuItemLeft.GetLength( 0 ); i++ )
			{
				//Bitmap bmpStr = (this.n現在のメニュー番号 == i) ?
				//      prvFont.DrawPrivateFont( strMenuItem[ i ], Color.White, Color.Black, Color.Yellow, Color.OrangeRed ) :
				//      prvFont.DrawPrivateFont( strMenuItem[ i ], Color.White, Color.Black );
				//txMenuItemLeft = CDTXMania.tテクスチャの生成( bmpStr, false );
				int flag = ( this.n現在のメニュー番号 == i ) ? 1 : 0;
				int num4 = txMenuItemLeft[ i, flag ].sz画像サイズ.Width;
				txMenuItemLeft[ i, flag ].t2D描画( CDTXMania.app.Device, 282 - ( num4 / 2 ), menuY ); //55
				//txMenuItem.Dispose();
				menuY += stepY;
			}
			//---------------------
			#endregion
			#region [ アイテム ]
			//---------------------
			switch( this.eItemPanelモード )
			{
				case EItemPanelモード.パッド一覧:
					this.actList.t進行描画( !this.bメニューにフォーカス中 );
					break;

				case EItemPanelモード.キーコード一覧:
					this.actKeyAssign.On進行描画();
					break;
			}
			//---------------------
			#endregion
			#region [ 上部パネル ]
			//---------------------
			if( this.tx上部パネル != null )
				this.tx上部パネル.t2D描画( CDTXMania.app.Device, 0, 0 );
			//---------------------
			#endregion
			#region [ 下部パネル ]
			//---------------------
			if( this.tx下部パネル != null )
				this.tx下部パネル.t2D描画( CDTXMania.app.Device, 0, SampleFramework.GameWindowSize.Height - this.tx下部パネル.sz画像サイズ.Height );
			//---------------------
			#endregion
			#region [ オプションパネル ]
			//---------------------
			this.actオプションパネル.On進行描画();
			//---------------------
			#endregion
			#region [ フェードイン・アウト ]
			//---------------------
			switch( base.eフェーズID )
			{
				case CStage.Eフェーズ.共通_フェードイン:
					if( this.actFIFO.On進行描画() != 0 )
					{
						CDTXMania.Skin.bgmコンフィグ画面.t再生する();
						base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
					}
					break;

				case CStage.Eフェーズ.共通_フェードアウト:
					if( this.actFIFO.On進行描画() == 0 )
					{
						break;
					}
					return 1;
			}
			//---------------------
			#endregion

			#region [ Enumerating Songs ]
			// CActEnumSongs側で表示する
			#endregion

			// キー入力
			#region [ キー入力 ]
			if ( ( base.eフェーズID != CStage.Eフェーズ.共通_通常状態 )
				|| this.actKeyAssign.bキー入力待ちの最中である
				|| CDTXMania.act現在入力を占有中のプラグイン != null )
				return 0;

			// 曲データの一覧取得中は、キー入力を無効化する
			if ( !CDTXMania.EnumSongs.IsEnumerating || CDTXMania.actEnumSongs.bコマンドでの曲データ取得 != true )
			{
				if ( ( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.Escape ) || CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.LC ) ) || CDTXMania.Pad.b押されたGB( Eパッド.FT ) )
				{
					CDTXMania.Skin.sound取消音.t再生する();
					if ( !this.bメニューにフォーカス中 )
					{
						if ( this.eItemPanelモード == EItemPanelモード.キーコード一覧 )
						{
							CDTXMania.stageコンフィグ.tアサイン完了通知();
							return 0;
						}
						if ( !this.actList.bIsKeyAssignSelected && !this.actList.bIsFocusingParameter )	// #24525 2011.3.15 yyagi, #32059 2013.9.17 yyagi
						{
							this.bメニューにフォーカス中 = true;
						}
						this.actList.t説明文パネルに現在選択されているメニューの説明を描画する( this.n現在のメニュー番号 );
						this.actList.tEsc押下();								// #24525 2011.3.15 yyagi ESC押下時の右メニュー描画用
					}
					else
					{
						this.actFIFO.tフェードアウト開始();
						base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
					}
				}
				#region [ ← ]
				else if ( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.LeftArrow ) )	// 左カーソルキー
				{
					if ( !this.bメニューにフォーカス中 )
					{
						if ( this.eItemPanelモード == EItemPanelモード.キーコード一覧 )
						{
							//キーコンフィグ画面中は、[←]押下に反応させない
							return 0;
						}
						if ( this.actList.bIsFocusingParameter )
						{
							// パラメータを増減している最中も、[←]押下に反応させない
							return 0;
						}
						if ( !this.actList.bIsKeyAssignSelected && !this.actList.bIsFocusingParameter )	// #24525 2011.3.15 yyagi, #32059 2013.9.17 yyagi
						{
							this.bメニューにフォーカス中 = true;
						}
						CDTXMania.Skin.sound取消音.t再生する();
						this.actList.t説明文パネルに現在選択されているメニューの説明を描画する( this.n現在のメニュー番号 );
						this.actList.tEsc押下();								// #24525 2011.3.15 yyagi ESC押下時の右メニュー描画用
					}
				}
				#endregion
				else if ( ( CDTXMania.Pad.b押されたDGB( Eパッド.CY ) || CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.RD ) ) || ( ( CDTXMania.ConfigIni.bEnterがキー割り当てのどこにも使用されていない && CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.Return ) ) ) )
				{
					#region [ EXIT ]
					if ( this.n現在のメニュー番号 == 4 )
					{
						CDTXMania.Skin.sound決定音.t再生する();
						this.actFIFO.tフェードアウト開始();
						base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
					}
					#endregion
					else if ( this.bメニューにフォーカス中 )
					{
						CDTXMania.Skin.sound決定音.t再生する();
						this.bメニューにフォーカス中 = false;
						this.actList.t説明文パネルに現在選択されている項目の説明を描画する();
					}
					else
					{
						switch ( this.eItemPanelモード )
						{
							case EItemPanelモード.パッド一覧:
								bool bIsKeyAssignSelectedBeforeHitEnter = this.actList.bIsKeyAssignSelected;	// #24525 2011.3.15 yyagi
								this.actList.tEnter押下();
								if ( this.actList.b現在選択されている項目はReturnToMenuである )
								{
									this.actList.t説明文パネルに現在選択されているメニューの説明を描画する( this.n現在のメニュー番号 );
									if ( bIsKeyAssignSelectedBeforeHitEnter == false )							// #24525 2011.3.15 yyagi
									{
										this.bメニューにフォーカス中 = true;
									}
								}
								break;

							case EItemPanelモード.キーコード一覧:
								this.actKeyAssign.tEnter押下();
								break;
						}
					}
				}
				#region [ → ]
				else if ( CDTXMania.Input管理.Keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.RightArrow ) )	// 右カーソルキー
				{
					#region [ EXIT ]
					if ( this.n現在のメニュー番号 == 4 )
					{
						// 何もしない
					}
					#endregion
					else if ( this.bメニューにフォーカス中 )
					{
						CDTXMania.Skin.sound決定音.t再生する();
						this.bメニューにフォーカス中 = false;
						this.actList.t説明文パネルに現在選択されている項目の説明を描画する();
					}
				}
				#endregion
				this.ctキー反復用.Up.tキー反復( CDTXMania.Input管理.Keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.UpArrow ), new CCounter.DGキー処理( this.tカーソルを上へ移動する ) );
				this.ctキー反復用.R.tキー反復( CDTXMania.Pad.b押されているGB( Eパッド.HH ), new CCounter.DGキー処理( this.tカーソルを上へ移動する ) );
				if ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.SD ) )
				{
					this.tカーソルを上へ移動する();
				}
				this.ctキー反復用.Down.tキー反復( CDTXMania.Input管理.Keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.DownArrow ), new CCounter.DGキー処理( this.tカーソルを下へ移動する ) );
				this.ctキー反復用.B.tキー反復( CDTXMania.Pad.b押されているGB( Eパッド.BD ), new CCounter.DGキー処理( this.tカーソルを下へ移動する ) );
				if ( CDTXMania.Pad.b押された( E楽器パート.DRUMS, Eパッド.FT ) )
				{
					this.tカーソルを下へ移動する();
				}
			}
			#endregion
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private enum EItemPanelモード
		{
			パッド一覧,
			キーコード一覧
		}

		[StructLayout( LayoutKind.Sequential )]
		private struct STキー反復用カウンタ
		{
			public CCounter Up;
			public CCounter Down;
			public CCounter R;
			public CCounter B;
			public CCounter this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.Up;

						case 1:
							return this.Down;

						case 2:
							return this.R;

						case 3:
							return this.B;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.Up = value;
							return;

						case 1:
							this.Down = value;
							return;

						case 2:
							this.R = value;
							return;

						case 3:
							this.B = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}

		private CActFIFOWhite actFIFO;
		private CActConfigKeyAssign actKeyAssign;
        private CActConfigList共通 actList;
		private CActオプションパネル actオプションパネル;
		public bool bメニューにフォーカス中;
		private STキー反復用カウンタ ctキー反復用;
		private const int DESC_H = 0x80;
		private const int DESC_W = 220;
		private EItemPanelモード eItemPanelモード;
		public int n現在のメニュー番号;
		private CTexture txMenuカーソル;
		private CTextureAf tx下部パネル;
		private CTextureAf tx上部パネル;
		private CTexture tx背景;
		private CPrivateFastFont prvFont;
		private CTexture[ , ] txMenuItemLeft;

		private void tカーソルを下へ移動する()
		{
			if( !this.bメニューにフォーカス中 )
			{
				switch( this.eItemPanelモード )
				{
					case EItemPanelモード.パッド一覧:
						this.actList.t次に移動();
						return;

					case EItemPanelモード.キーコード一覧:
						this.actKeyAssign.t次に移動();
						return;
				}
			}
			else
			{
				CDTXMania.Skin.soundカーソル移動音.t再生する();
				this.n現在のメニュー番号 = ( this.n現在のメニュー番号 + 1 ) % 5;
				switch( this.n現在のメニュー番号 )
				{
					case 0:
						this.actList.t項目リストの設定_System();
						break;

					//case 1:
					//    this.actList.t項目リストの設定・KeyAssignDrums();
					//    break;

					//case 2:
					//    this.actList.t項目リストの設定・KeyAssignGuitar();
					//    break;

					//case 3:
					//    this.actList.t項目リストの設定・KeyAssignBass();
					//    break;

					case 1:
						this.actList.t項目リストの設定_Drums();
						break;

					case 2:
						this.actList.t項目リストの設定_Guitar();
						break;

					case 3:
						this.actList.t項目リストの設定_Bass();
						break;

					case 4:
						this.actList.t項目リストの設定_Exit();
						break;
				}
				this.actList.t説明文パネルに現在選択されているメニューの説明を描画する( this.n現在のメニュー番号 );
			}
		}
		private void tカーソルを上へ移動する()
		{
			if( !this.bメニューにフォーカス中 )
			{
				switch( this.eItemPanelモード )
				{
					case EItemPanelモード.パッド一覧:
						this.actList.t前に移動();
						return;

					case EItemPanelモード.キーコード一覧:
						this.actKeyAssign.t前に移動();
						return;
				}
			}
			else
			{
				CDTXMania.Skin.soundカーソル移動音.t再生する();
				this.n現在のメニュー番号 = ( ( this.n現在のメニュー番号 - 1 ) + 5 ) % 5;
				switch( this.n現在のメニュー番号 )
				{
					case 0:
						this.actList.t項目リストの設定_System();
						break;

					//case 1:
					//    this.actList.t項目リストの設定・KeyAssignDrums();
					//    break;

					//case 2:
					//    this.actList.t項目リストの設定・KeyAssignGuitar();
					//    break;

					//case 3:
					//    this.actList.t項目リストの設定・KeyAssignBass();
					//    break;
					case 1:
						this.actList.t項目リストの設定_Drums();
						break;

					case 2:
						this.actList.t項目リストの設定_Guitar();
						break;

					case 3:
						this.actList.t項目リストの設定_Bass();
						break;

					case 4:
						this.actList.t項目リストの設定_Exit();
						break;
				}
				this.actList.t説明文パネルに現在選択されているメニューの説明を描画する( this.n現在のメニュー番号 );
			}
		}
		//-----------------
		#endregion
	}
}
