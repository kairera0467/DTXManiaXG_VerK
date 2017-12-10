using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.Threading;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActConfigListGD : CActConfigList共通
	{
        // メソッド
        #region [ t項目リストの設定_System() ]
        //public void t項目リストの設定_System()
        //{
        //}

        #endregion
        #region [ t項目リストの設定_Drums() ]
        //public void t項目リストの設定_Drums()
        //{
        //}
        #endregion
        #region [ t項目リストの設定_Guitar() ]
        //public void t項目リストの設定_Guitar()
        //{
        //}
        #endregion
        #region [ t項目リストの設定_Bass() ]
        //public void t項目リストの設定_Bass()
        //{
        //}
        #endregion

        #region [ 項目リストの設定 ( Exit, KeyAssignSystem/Drums/Guitar/Bass) ]
        //public void t項目リストの設定_Exit()
        //{
        //}
        //public void t項目リストの設定_KeyAssignSystem()
        //{
        //}
        //public void t項目リストの設定_KeyAssignDrums()
        //{
        //}
        //public void t項目リストの設定_KeyAssignGuitar()
        //{
        //}
        //public void t項目リストの設定_KeyAssignBass()
        //{
        //}
        #endregion

        public override void On活性化()
        {
            this.prvFont = new CPrivateFastFont( CSkin.Path( @"Graphics\fonts\mplus-1p-heavy.ttf" ), 15 );	// t項目リストの設定 の前に必要
            this.ftフォント = new Font("MS PGothic", 17f, FontStyle.Regular, GraphicsUnit.Pixel);			//
            base.On活性化();
        }

        public override void OnManagedリソースの作成()
        {
            this.tx矢印 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\4_Arrow.png" ) );
            this.tx説明文パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\4_Description Panel.png" ) );
            this.txカーソル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\4_itembox cursor.png" ) );
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.tx矢印 );
            CDTXMania.tテクスチャの解放( ref this.tx説明文パネル );
            CDTXMania.tテクスチャの解放( ref this.txカーソル );
            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(bool)のほうを使用してください。" );
		}
		public override int t進行描画( bool b項目リスト側にフォーカスがある )
		{
			if( this.b活性化してない )
				return 0;

			// 進行

			#region [ 初めての進行描画 ]
			//-----------------
			if( base.b初めての進行描画 )
			{
                this.nスクロール用タイマ値 = CSound管理.rc演奏用タイマ.n現在時刻;
				this.ct三角矢印アニメ.t開始( 0, 9, 50, CDTXMania.Timer );
			
				base.b初めての進行描画 = false;
			}
			//-----------------
			#endregion

			this.b項目リスト側にフォーカスがある = b項目リスト側にフォーカスがある;		// 記憶

			#region [ 項目スクロールの進行 ]
			//-----------------
			long n現在時刻 = CDTXMania.Timer.n現在時刻;
			if( n現在時刻 < this.nスクロール用タイマ値 ) this.nスクロール用タイマ値 = n現在時刻;

			const int INTERVAL = 2;	// [ms]
			while( ( n現在時刻 - this.nスクロール用タイマ値 ) >= INTERVAL )
			{
				int n目標項目までのスクロール量 = Math.Abs( (int) ( this.n目標のスクロールカウンタ - this.n現在のスクロールカウンタ ) );
				int n加速度 = 0;

				#region [ n加速度の決定；目標まで遠いほど加速する。]
				//-----------------
				if( n目標項目までのスクロール量 <= 100 )
				{
					n加速度 = 2;
				}
				else if( n目標項目までのスクロール量 <= 300 )
				{
					n加速度 = 3;
				}
				else if( n目標項目までのスクロール量 <= 500 )
				{
					n加速度 = 4;
				}
				else
				{
					n加速度 = 8;
				}
				//-----------------
				#endregion
				#region [ this.n現在のスクロールカウンタに n加速度 を加減算。]
				//-----------------
				if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )
				{
					this.n現在のスクロールカウンタ += n加速度;
					if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )
					{
						// 目標を超えたら目標値で停止。
						this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;
					}
				}
				else if( this.n現在のスクロールカウンタ > this.n目標のスクロールカウンタ )
				{
					this.n現在のスクロールカウンタ -= n加速度;
					if( this.n現在のスクロールカウンタ < this.n目標のスクロールカウンタ )
					{
						// 目標を超えたら目標値で停止。
						this.n現在のスクロールカウンタ = this.n目標のスクロールカウンタ;
					}
				}
				//-----------------
				#endregion
				#region [ 行超え処理、ならびに目標位置に到達したらスクロールを停止して項目変更通知を発行。]
				//-----------------
				if( this.n現在のスクロールカウンタ >= 100 )
				{
					this.n現在の選択項目 = this.t次の項目( this.n現在の選択項目 );
					this.n現在のスクロールカウンタ -= 100;
					this.n目標のスクロールカウンタ -= 100;
					if( this.n目標のスクロールカウンタ == 0 )
					{
						CDTXMania.stageコンフィグ.t項目変更通知();
					}
				}
				else if( this.n現在のスクロールカウンタ <= -100 )
				{
					this.n現在の選択項目 = this.t前の項目( this.n現在の選択項目 );
					this.n現在のスクロールカウンタ += 100;
					this.n目標のスクロールカウンタ += 100;
					if( this.n目標のスクロールカウンタ == 0 )
					{
						CDTXMania.stageコンフィグ.t項目変更通知();
					}
				}
				//-----------------
				#endregion

				this.nスクロール用タイマ値 += INTERVAL;
			}
			//-----------------
			#endregion
			
			#region [ ▲印アニメの進行 ]
			//-----------------
			if( this.b項目リスト側にフォーカスがある && ( this.n目標のスクロールカウンタ == 0 ) )
				this.ct三角矢印アニメ.t進行Loop();
			//-----------------
			#endregion


			// 描画

            this.ptパネルの基本座標[4].X = this.b項目リスト側にフォーカスがある ? 0x228 : 0x25a;		// メニューにフォーカスがあるなら、項目リストの中央は頭を出さない。

            //2014.04.25 kairera0467 GITADORAでは項目パネルが11個だが、選択中のカーソルは中央に無いので両方を同じにすると7×2+1=15個パネルが必要になる。
            //　　　　　　　　　　　 さらに画面に映らないがアニメーション中に見える箇所を含めると17個は必要とされる。
            //　　　　　　　　　　　 ただ、画面に表示させる分には上のほうを考慮しなくてもよさそうなので、上4個は必要なさげ。
            #region [ 計11個の項目パネルを描画する。]
            //-----------------
            int nItem = this.n現在の選択項目;
            for (int i = 0; i < 4; i++)
                nItem = this.t前の項目(nItem);

            for (int n行番号 = -4; n行番号 < 10; n行番号++)		// n行番号 == 0 がフォーカスされている項目パネル。
            {
                #region [ 今まさに画面外に飛びだそうとしている項目パネルは描画しない。]
                //-----------------
                if (((n行番号 == -4) && (this.n現在のスクロールカウンタ > 0)) ||		// 上に飛び出そうとしている
                    ((n行番号 == +9) && (this.n現在のスクロールカウンタ < 0)))		// 下に飛び出そうとしている
                {
                    nItem = this.t次の項目(nItem);
                    continue;
                }
                //-----------------
                #endregion

                int n移動元の行の基本位置 = n行番号 + 4;
                int n移動先の行の基本位置 = (this.n現在のスクロールカウンタ <= 0) ? ((n移動元の行の基本位置 + 1) % 14) : (((n移動元の行の基本位置 - 1) + 14) % 14);
                int x = this.pt新パネルの基本座標[n移動元の行の基本位置].X + ((int)((this.pt新パネルの基本座標[n移動先の行の基本位置].X - this.pt新パネルの基本座標[n移動元の行の基本位置].X) * (((double)Math.Abs(this.n現在のスクロールカウンタ)) / 100.0)));
                int y = this.pt新パネルの基本座標[n移動元の行の基本位置].Y + ((int)((this.pt新パネルの基本座標[n移動先の行の基本位置].Y - this.pt新パネルの基本座標[n移動元の行の基本位置].Y) * (((double)Math.Abs(this.n現在のスクロールカウンタ)) / 100.0)));
                int n新項目パネルX = 420;

                #region [ 現在の行の項目パネル枠を描画。]
                //-----------------
                switch (this.list項目リスト[nItem].eパネル種別)
                {
                    case CItemBase.Eパネル種別.通常:
                        if (this.tx通常項目行パネル != null)
                            this.tx通常項目行パネル.t2D描画(CDTXMania.app.Device, n新項目パネルX, y);
                        break;

                    case CItemBase.Eパネル種別.その他:
                        if (this.txその他項目行パネル != null)
                            this.txその他項目行パネル.t2D描画(CDTXMania.app.Device, n新項目パネルX, y);
                        break;
                }
                //-----------------
                #endregion
                #region [ 現在の行の項目名を描画。]
                //-----------------
				if ( listMenu[ nItem ].txMenuItemRight != null )	// 自前のキャッシュに含まれているようなら、再レンダリングせずキャッシュを使用
				{
					listMenu[ nItem ].txMenuItemRight.t2D描画( CDTXMania.app.Device, ( n新項目パネルX + 20 ), ( y + 24 ) );
				}
				else
				{
					Bitmap bmpItem = prvFont.DrawPrivateFont( this.list項目リスト[ nItem ].str項目名, Color.White, Color.Transparent );
					listMenu[ nItem ].txMenuItemRight = CDTXMania.tテクスチャの生成( bmpItem );
//					ctItem.t2D描画( CDTXMania.app.Device, ( x + 0x12 ) * Scale.X, ( y + 12 ) * Scale.Y - 20 );
//					CDTXMania.tテクスチャの解放( ref ctItem );
					CDTXMania.t安全にDisposeする( ref bmpItem );
				}
				//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 0x12, y + 12, this.list項目リスト[ nItem ].str項目名 );
                //-----------------
                #endregion
                #region [ 現在の行の項目の要素を描画。]
				//-----------------
				string strParam = null;
				bool b強調 = false;
				switch( this.list項目リスト[ nItem ].e種別 )
				{
					case CItemBase.E種別.ONorOFFトグル:
						#region [ *** ]
						//-----------------
						//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, ( (CItemToggle) this.list項目リスト[ nItem ] ).bON ? "ON" : "OFF" );
						strParam = ( (CItemToggle) this.list項目リスト[ nItem ] ).bON ? "ON" : "OFF";
						break;
						//-----------------
						#endregion

					case CItemBase.E種別.ONorOFFor不定スリーステート:
						#region [ *** ]
						//-----------------
						switch( ( (CItemThreeState) this.list項目リスト[ nItem ] ).e現在の状態 )
						{
							case CItemThreeState.E状態.ON:
								strParam = "ON";
								break;

							case CItemThreeState.E状態.不定:
								strParam = "- -";
								break;

							default:
								strParam = "OFF";
								break;
						}
						//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, "ON" );
						break;
						//-----------------
						#endregion

					case CItemBase.E種別.整数:		// #24789 2011.4.8 yyagi: add PlaySpeed supports (copied them from OPTION)
						#region [ *** ]
						//-----------------
						if( this.list項目リスト[ nItem ] == this.iCommonPlaySpeed )
						{
							double d = ( (double) ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値 ) / 20.0;
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, d.ToString( "0.000" ), ( n行番号 == 0 ) && this.b要素値にフォーカス中 );
							strParam = d.ToString( "0.000" );
						}
						else if( this.list項目リスト[ nItem ] == this.iDrumsScrollSpeed || this.list項目リスト[ nItem ] == this.iGuitarScrollSpeed || this.list項目リスト[ nItem ] == this.iBassScrollSpeed )
						{
							float f = ( ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値 + 1 ) * 0.5f;
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, f.ToString( "x0.0" ), ( n行番号 == 0 ) && this.b要素値にフォーカス中 );
							strParam = f.ToString( "x0.0" );
						}
						else
						{
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値.ToString(), ( n行番号 == 0 ) && this.b要素値にフォーカス中 );
							strParam = ( (CItemInteger) this.list項目リスト[ nItem ] ).n現在の値.ToString();
						}
						b強調 = ( n行番号 == 0 ) && this.b要素値にフォーカス中;
						break;
						//-----------------
						#endregion

					case CItemBase.E種別.リスト:	// #28195 2012.5.2 yyagi: add Skin supports
						#region [ *** ]
						//-----------------
						{
							CItemList list = (CItemList) this.list項目リスト[ nItem ];
							//CDTXMania.stageコンフィグ.actFont.t文字列描画( x + 210, y + 12, list.list項目値[ list.n現在選択されている項目番号 ] );
							strParam = list.list項目値[ list.n現在選択されている項目番号 ];

							#region [ 必要な場合に、Skinのサンプルを生成・描画する。#28195 2012.5.2 yyagi ]
							if ( this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemSkinSubfolder )
							{
								tGenerateSkinSample();		// 最初にSkinの選択肢にきたとき(Enterを押す前)に限り、サンプル生成が発生する。

							}
							#endregion
							break;
						}
						//-----------------
						#endregion
				}
				if ( b強調 )
				{
					Bitmap bmpStr = b強調 ?
						prvFont.DrawPrivateFont( strParam, Color.White, Color.Black, Color.Yellow, Color.OrangeRed ) :
						prvFont.DrawPrivateFont( strParam, Color.Black, Color.Transparent );
					CTexture txStr = CDTXMania.tテクスチャの生成( bmpStr, false );
					txStr.t2D描画( CDTXMania.app.Device, ( n新項目パネルX + 260 ) , ( y + 20 ) );
					CDTXMania.tテクスチャの解放( ref txStr );
					CDTXMania.t安全にDisposeする( ref bmpStr );
				}
				else
				{
					int nIndex = this.list項目リスト[ nItem ].GetIndex();
					if ( listMenu[ nItem ].nParam != nIndex || listMenu[ nItem ].txParam == null )
					{
						stMenuItemRight stm = listMenu[ nItem ];
						stm.nParam = nIndex;
						object o = this.list項目リスト[ nItem ].obj現在値();
						stm.strParam = ( o == null ) ? "" : o.ToString();

				        Bitmap bmpStr =
				            prvFont.DrawPrivateFont( strParam, Color.Black, Color.Transparent );
				        stm.txParam = CDTXMania.tテクスチャの生成( bmpStr, false );
				        CDTXMania.t安全にDisposeする( ref bmpStr );

				        listMenu[ nItem ] = stm;
				    }
				    listMenu[ nItem ].txParam.t2D描画( CDTXMania.app.Device, ( n新項目パネルX + 260 ) , ( y + 24 ) );
				}
				//-----------------
                #endregion

                nItem = this.t次の項目(nItem);
            }
            //-----------------
            #endregion

            #region[ カーソル ]
            if( this.b項目リスト側にフォーカスがある && this.txカーソル != null )
            {
                this.txカーソル.t2D描画( CDTXMania.app.Device, 413, 193 );
            }
            #endregion
			
            #region[ 説明文パネル ]
            if( this.b項目リスト側にフォーカスがある && this.n目標のスクロールカウンタ == 0 )
            {
                if( this.tx説明文パネル != null )
                    this.tx説明文パネル.t2D描画( CDTXMania.app.Device, 601, 252 );
                if ( txSkinSample1 != null && this.n目標のスクロールカウンタ == 0 && this.list項目リスト[ this.n現在の選択項目 ] == this.iSystemSkinSubfolder )
				{
					txSkinSample1.t2D描画( CDTXMania.app.Device, 615 - 60, 442 - 106 );
				}
            }

            if( this.tx説明文 != null && !CDTXMania.stageコンフィグ.bメニューにフォーカス中 && this.n目標のスクロールカウンタ == 0 )
                this.tx説明文.t2D描画(CDTXMania.app.Device, 620, 270);
            #endregion

            #region [ 項目リストにフォーカスがあって、かつスクロールが停止しているなら、パネルの上下に▲印を描画する。]
            //-----------------
            if( this.b項目リスト側にフォーカスがある )//&& (this.n目標のスクロールカウンタ == 0))
            {
                int x;
                int y_upper;
                int y_lower;

                int n新カーソルX = 394;
                int n新カーソル上Y = 174;
                int n新カーソル下Y = 240;

                // 位置決定。

                if (this.b要素値にフォーカス中)
                {
                    x = 552;	// 要素値の上下あたり。
                    y_upper = 0x117 - this.ct三角矢印アニメ.n現在の値;
                    y_lower = 0x17d + this.ct三角矢印アニメ.n現在の値;
                }
                else
                {
                    x = 552;	// 項目名の上下あたり。
                    y_upper = 0x129 - this.ct三角矢印アニメ.n現在の値;
                    y_lower = 0x16b + this.ct三角矢印アニメ.n現在の値;
                }

                //新矢印
                if( this.tx矢印 != null )
                {
                    this.tx矢印.t2D描画(CDTXMania.app.Device, n新カーソルX, n新カーソル上Y, new Rectangle(0, 0, 40, 40));
                    this.tx矢印.t2D描画(CDTXMania.app.Device, n新カーソルX, n新カーソル下Y, new Rectangle(0, 40, 40, 40));
                }
            }
            //-----------------
            #endregion
			return 0;
		}

		public override void t説明文パネルに現在選択されているメニューの説明を描画する( int n現在のメニュー番号 )
		{
			try
			{
				var image = new Bitmap( (int)(220 * 2 ), (int)(192 * 2 ) );		// 説明文領域サイズの縦横 2 倍。（描画時に 0.5 倍で表示する。）
				var graphics = Graphics.FromImage( image );
				graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				
				string[,] str = new string[ 2, 2 ];
				switch( CDTXMania.stageコンフィグ.n現在のメニュー番号 )
				{
					case 0:
						str[ 0, 0 ] = "システムに関係する項目を設定します。";
						str[ 0, 1 ] = "";
						str[ 1, 0 ] = "Settings for an overall systems.";
						break;

					//case 1:
					//    str[0, 0] = "ドラムのキー入力に関する項目を設";
					//    str[0, 1] = "定します。";
					//    str[1, 0] = "Settings for the drums key/pad inputs.";
					//    str[1, 1] = "";
					//    break;

					//case 2:
					//    str[0, 0] = "ギターのキー入力に関する項目を設";
					//    str[0, 1] = "定します。";
					//    str[1, 0] = "Settings for the guitar key/pad inputs.";
					//    str[1, 1] = "";
					//    break;

					//case 3:
					//    str[0, 0] = "ベースのキー入力に関する項目を設";
					//    str[0, 1] = "定します。";
					//    str[1, 0] = "Settings for the bass key/pad inputs.";
					//    str[1, 1] = "";
					//    break;
					case 1:
						str[ 0, 0 ] = "ドラムの演奏に関する項目を設定します。";
						str[ 0, 1 ] = "";
						str[ 1, 0 ] = "Settings to play the drums.";
						str[ 1, 1 ] = "";
						break;

					case 2:
						str[ 0, 0 ] = "ギターの演奏に関する項目を設定します。";
						str[ 0, 1 ] = "";
						str[ 1, 0 ] = "Settings to play the guitar.";
						str[ 1, 1 ] = "";
						break;

					case 3:
						str[ 0, 0 ] = "ベースの演奏に関する項目を設定します。";
						str[ 0, 1 ] = "";
						str[ 1, 0 ] = "Settings to play the bass.";
						str[ 1, 1 ] = "";
						break;

					case 4:
						str[ 0, 0 ] = "設定を保存し、コンフィグ画面を終了します。";
						str[ 0, 1 ] = "";
						str[ 1, 0 ] = "Save the settings and exit from\nCONFIGURATION menu.";
						str[ 1, 1 ] = "";
						break;
				}
				
				int c = (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ja") ? 0 : 1;
				for (int i = 0; i < 2; i++)
				{
					graphics.DrawString( str[ c, i ], this.ftフォント, Brushes.Black, new PointF( 4f , ( i * 30 ) ) );
				}
				graphics.Dispose();
				if( this.tx説明文 != null )
				{
					this.tx説明文.Dispose();
				}
				//this.tx説明文パネル = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );
				// this.tx説明文パネル.vc拡大縮小倍率.X = 0.5f;
				// this.tx説明文パネル.vc拡大縮小倍率.Y = 0.5f;
				image.Dispose();
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "説明文テクスチャの作成に失敗しました。" );
				this.tx説明文パネル = null;
			}
		}
		public override void t説明文パネルに現在選択されている項目の説明を描画する()
		{
			try
			{
				var image = new Bitmap( (int)(400), (int)(192) );		// 説明文領域サイズの縦横 2 倍。（描画時に 0.5 倍で表示する・・・のは中止。処理速度向上のため。）
				var graphics = Graphics.FromImage( image );
				graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

				CItemBase item = this.ib現在の選択項目;
				if( ( item.str説明文 != null ) && ( item.str説明文.Length > 0 ) )
				{
					//int num = 0;
					//foreach( string str in item.str説明文.Split( new char[] { '\n' } ) )
					//{
					//    graphics.DrawString( str, this.ftフォント, Brushes.White, new PointF( 4f * Scale.X, (float) num * Scale.Y ) );
					//    num += 30;
					//}
					graphics.DrawString( item.str説明文, this.ftフォント, Brushes.Black, new RectangleF( 4f, (float) 0, 230, 430 ) );
				}
				graphics.Dispose();
				if( this.tx説明文 != null )
				{
					this.tx説明文.Dispose();
				}
				this.tx説明文 = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat, false );
				//this.tx説明文パネル.vc拡大縮小倍率.X = 0.58f;
				//this.tx説明文パネル.vc拡大縮小倍率.Y = 0.58f;
				image.Dispose();
			}
			catch( CTextureCreateFailedException )
			{
				Trace.TraceError( "説明文パネルテクスチャの作成に失敗しました。" );
				this.tx説明文パネル = null;
			}
		}

        private CTexture tx矢印;
        private CTexture txカーソル;

        private Point[] ptパネルの基本座標 = new Point[] {
            new Point(0x25a, 4),
            new Point(0x25a, 0x4f),
            new Point(0x25a, 0x9a),
            new Point(0x25a, 0xe5),
            new Point(0x228, 0x130),
            new Point(0x25a, 0x17b),
            new Point(0x25a, 0x1c6),
            new Point(0x25a, 0x211),
            new Point(0x25a, 0x25c),
            new Point(0x25a, 0x2a7),
            new Point(0x25a, 0x2d0)
        };
		private Point[] pt新パネルの基本座標 = new Point[] {
            new Point(0x25a, -79),
            new Point(0x25a, -12),
            new Point(0x25a, 55),
            new Point(0x25a, 122),
            new Point(0x228, 189),
            new Point(0x25a, 256),
            new Point(0x25a, 323),
            new Point(0x25a, 390),
            new Point(0x25a, 457),
            new Point(0x25a, 524),
            new Point(0x25a, 591),
            new Point(0x25a, 658),
            new Point(0x25a, 725),
            new Point(0x25a, 792)
        };
	}
}
