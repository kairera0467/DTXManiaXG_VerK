using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using FDK;
using SharpDX;

using Rectangle = System.Drawing.Rectangle;
namespace DTXMania
{
	internal class CAct演奏Drums判定文字列GD : CAct演奏判定文字列共通
	{
		// コンストラクタ

		public CAct演奏Drums判定文字列GD()
		{
			this.stレーンサイズ = new STレーンサイズ[ 12 ]
			{
				new STレーンサイズ( 290, 80 ),
				new STレーンサイズ( 367, 46 ),
				new STレーンサイズ( 470, 54 ),
				new STレーンサイズ( 582, 60 ),
				new STレーンサイズ( 528, 46 ),
				new STレーンサイズ( 645, 46 ),
				new STレーンサイズ( 694, 46 ),
				new STレーンサイズ( 748, 64 ),
				new STレーンサイズ( 419, 46 ),
				new STレーンサイズ( 815, 80 ),
				new STレーンサイズ( 815, 80 ),
				new STレーンサイズ( 815, 80 )
			};
            //for ( int i = 0; i < 12; i++ )
            //{
            //	this.stレーンサイズ[i] = new STレーンサイズ();
            //	this.stレーンサイズ[i].x = sizeXW[i, 0];
            //	this.stレーンサイズ[i].w = sizeXW[i, 1];
            //}

            // 19.7.20 kairera0467
            // matixx風ではDTXManiaとは別の画像フォーマットを取っている。
            // ToDo:外部ファイルからの座標読み込み
            // ToDo:一旦matixx風限定として使っているが、別のタイプとして運用できるように構造を整える。
            for( int i = 0; i < 3; i++ )
            {
                this.stLag数値[ 0 + i * 12 ].rc = new Rectangle( 0, i * 18, 14, 18 );
                this.stLag数値[ 1 + i * 12 ].rc = new Rectangle( 14, i * 18, 14, 18 );
                this.stLag数値[ 2 + i * 12 ].rc = new Rectangle( 28, i * 18, 14, 18 );
                this.stLag数値[ 3 + i * 12 ].rc = new Rectangle( 42, i * 18, 14, 18 );
                this.stLag数値[ 4 + i * 12 ].rc = new Rectangle( 56, i * 18, 14, 18 );
                this.stLag数値[ 5 + i * 12 ].rc = new Rectangle( 70, i * 18, 14, 18 );
                this.stLag数値[ 6 + i * 12 ].rc = new Rectangle( 84, i * 18, 14, 18 );
                this.stLag数値[ 7 + i * 12 ].rc = new Rectangle( 98, i * 18, 14, 18 );
                this.stLag数値[ 8 + i * 12 ].rc = new Rectangle( 112, i * 18, 14, 18 );
                this.stLag数値[ 9 + i * 12 ].rc = new Rectangle( 126, i * 18, 14, 18 );
                this.stLag数値[ 10 + i * 12 ].rc = new Rectangle( 140, i * 18, 14, 18 );
                this.stLag数値[ 11 + i * 12 ].rc = new Rectangle( 154, i * 18, 14, 18 );
            }

            this.n最後に叩いたチップのラグms = 0;
            this.bラグ表示を描画する = false;

			base.b活性化してない = true;
		}
		
        /// <summary>
        /// レーンのX座標をint配列に格納していく。
        /// </summary>
        /// <param name="eLaneType">レーンタイプ</param>
        private void tレーンタイプからレーン位置を設定する( Eタイプ eLaneType, ERDPosition eRDPosition )
        {
            switch( eLaneType )
            {
                case Eタイプ.A:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 470, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 582, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 528, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 419, 46 );
                    break;
                case Eタイプ.B:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 419, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 534, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 590, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 478, 46 );
                    break;
                case Eタイプ.C:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 470, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 534, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 590, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 419, 46 );
                    break;
                case Eタイプ.D:
                    this.stレーンサイズ[ 2 ] = new STレーンサイズ( 419, 54 );
                    this.stレーンサイズ[ 3 ] = new STレーンサイズ( 582, 60 );
                    this.stレーンサイズ[ 4 ] = new STレーンサイズ( 476, 46 );
                    this.stレーンサイズ[ 8 ] = new STレーンサイズ( 528, 46 );
                    break;
            }

            if( eRDPosition == ERDPosition.RCRD )
            {
                this.stレーンサイズ[ 7 ] = new STレーンサイズ( 748, 64 );
                this.stレーンサイズ[ 9 ] = new STレーンサイズ( 815, 64 );
            }
            else
            {
                this.stレーンサイズ[ 7 ] = new STレーンサイズ( 818, 64 );
                this.stレーンサイズ[ 9 ] = new STレーンサイズ( 768, 64 );
            }
        }
		
		// CActivity 実装（共通クラスからの差分のみ）
		public override int On進行描画()
		{
			throw new InvalidOperationException( "t進行描画(C演奏判定ライン座標共通 演奏判定ライン共通 ) のほうを使用してください。" );
		}
		public override int t進行描画( C演奏判定ライン座標共通 演奏判定ライン座標 )
		{
            if( base.b初めての進行描画 )
            {
                this.tレーンタイプからレーン位置を設定する( CDTXMania.ConfigIni.eLaneType, CDTXMania.ConfigIni.eRDPosition );
                this.n最後に叩いたチップのラグms = 0;
                this.bラグ表示を描画する = false;

                base.b初めての進行描画 = false;
            }

			if( !base.b活性化してない )
			{
                #region[ 座標など定義(タイプA、B) ]
                if( CDTXMania.ConfigIni.eJudgeAnimeType == Eタイプ.A )
                {
                    #region[ むかしの ]
                    for (int i = 0; i < 12; i++)
                    {
                        if (!base.st状態[i].ct進行.b停止中)
                        {
                            base.st状態[i].ct進行.t進行();
                            if (base.st状態[i].ct進行.b終了値に達した)
                            {
                                base.st状態[i].ct進行.t停止();
                            }
                            int num2 = base.st状態[i].ct進行.n現在の値;
                            if ((base.st状態[i].judge != E判定.Miss) && (base.st状態[i].judge != E判定.Bad))
                            {
                                if (num2 < 50)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f + (1f * (1f - (((float)num2) / 50f)));
                                    base.st状態[i].fY方向拡大率 = ((float)num2) / 50f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else if (num2 < 130)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = ((num2 % 6) == 0) ? (CDTXMania.Random.Next(6) - 3) : base.st状態[i].n相対Y座標;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else if (num2 >= 240)
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f - ((1f * (num2 - 240)) / 60f);
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                                else
                                {
                                    base.st状態[i].fX方向拡大率 = 1f;
                                    base.st状態[i].fY方向拡大率 = 1f;
                                    base.st状態[i].n相対X座標 = 0;
                                    base.st状態[i].n相対Y座標 = 0;
                                    base.st状態[i].n透明度 = 0xff;
                                }
                            }
                            else if (num2 < 50)
                            {
                                base.st状態[i].fX方向拡大率 = 1f;
                                base.st状態[i].fY方向拡大率 = ((float)num2) / 50f;
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                            else if (num2 >= 200)
                            {
                                base.st状態[i].fX方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                                base.st状態[i].fY方向拡大率 = 1f - (((float)(num2 - 200)) / 100f);
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                            else
                            {
                                base.st状態[i].fX方向拡大率 = 1f;
                                base.st状態[i].fY方向拡大率 = 1f;
                                base.st状態[i].n相対X座標 = 0;
                                base.st状態[i].n相対Y座標 = 0;
                                base.st状態[i].n透明度 = 0xff;
                            }
                        }
                    }
                    #endregion
                }
                else if( CDTXMania.ConfigIni.eJudgeAnimeType == Eタイプ.B )
                {
                    #region[ コマ方式 ]
                    for (int i = 0; i < 12; i++)
                    {
                        if (!base.st状態[i].ct進行.b停止中)
                        {
                            base.st状態[i].ct進行.t進行();
                            if (base.st状態[i].ct進行.b終了値に達した)
                            {
                                base.st状態[i].ct進行.t停止();
                            }
                            base.st状態[i].nRect = base.st状態[i].ct進行.n現在の値;
                        }
                    }
                    #endregion
                }
                #endregion

                // 描画
                if( CDTXMania.ConfigIni.eJudgeAnimeType <= Eタイプ.B )
                {
                    for( int j = 0; j < 12; j++ )
                    {
                        //CDTXMania.act文字コンソール.tPrint( this.stレーンサイズ[ j ].x, 0, C文字コンソール.Eフォント種別.白, j.ToString() );
                        if( !base.st状態[ j ].ct進行.b停止中 )
                        {
                            #region[ 以前まで ]
                            // 2016.02.16 kairera0467 104の仕様にあわせて従来のコードに加筆修正。
                            //                        現時点ではドラム画面でのギタープレイはできないため、この辺は適当。

                            int num4 = CDTXMania.ConfigIni.nJudgeFrames > 1 ? 0 : base.st判定文字列[ (int)base.st状態[ j ].judge ].n画像番号;
                            int num5 = 0;
                            int num6 = 0;
                            int nJudgePosY = CDTXMania.stage演奏ドラム画面GITADORA.演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );
                            base.iP_A = nJudgePosY - 211;
                            base.iP_B = nJudgePosY + 23; //これは固定値にしてもいいのではないか?
                            if( j < 10 )
                            {
                                //縦は5pxずつを1ブロックとして分割。これでだいぶ本家に近づいたはず。
                                if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.表示OFF )
                                    continue;

                                num5 = this.stレーンサイズ[ j ].x;
                                if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.レーン上 )
                                    num6 = CDTXMania.ConfigIni.bReverse.Drums ? ( ( nJudgePosY + 211 ) - this.n文字の縦表示位置[ j ] * 5 ) : ( ( nJudgePosY - 211 ) + this.n文字の縦表示位置[ j ] * 5 );
                                else if( CDTXMania.ConfigIni.判定文字表示位置.Drums == E判定文字表示位置.判定ライン上 )
                                    num6 = 0;
                            }

                            int nRectX = CDTXMania.ConfigIni.nJudgeWidth;
                            int nRectY = CDTXMania.ConfigIni.nJudgeHeight;

                            int xc = ( num5 + base.st状態[ j ].n相対X座標 ) + ( this.stレーンサイズ[ j ].w / 2 );
                            int x = ( xc - ( (int)( ( 110f * base.st状態[ j ].fX方向拡大率 ) ) ) ) - ( ( nRectX - 225 ) / 2 );
                            int y = ( ( num6 + base.st状態[ j ].n相対Y座標 ) - ( (int)( ( ( 140f * base.st状態[ j ].fY方向拡大率 ) ) / 2.0 ) ) ) - ( ( nRectY - 135 ) / 2 );

                            if( base.tx判定文字列[ 0 ] != null )
                            {
                                //if (CDTXMania.ConfigIni.nJudgeFrames > 1 && CDTXMania.stage演奏ドラム画面.tx判定画像anime != null)
                                if( CDTXMania.ConfigIni.nJudgeFrames > 1 )
                                {
                                    if( !base.bShow ) x = 1280;

                                    if( base.st状態[ j ].judge == E判定.Perfect || base.st状態[ j ].judge == E判定.XPerfect )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( 0, nRectY * base.st状態[ j ].nRect, nRectX, nRectY ) );
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if( base.st状態[ j ].judge == E判定.Great )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX * 1, nRectY * base.st状態[ j ].nRect, nRectX, nRectY ) );
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 1, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if( base.st状態[ j ].judge == E判定.Good )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX * 2, nRectY * base.st状態[ j ].nRect, nRectX, nRectY ) );
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 2, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if( base.st状態[ j ].judge == E判定.Poor )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX * 3, nRectY * base.st状態[ j ].nRect, nRectX, nRectY ) );
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 3, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if( base.st状態[ j ].judge == E判定.Miss )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX * 4, nRectY * base.st状態[ j ].nRect, nRectX, nRectY ) );
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 4, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                    if( base.st状態[ j ].judge == E判定.Auto )
                                    {
                                        base.tx判定文字列[ 0 ].t2D描画( CDTXMania.app.Device, x, y, new Rectangle( nRectX * 5, nRectY * base.st状態[ j ].nRect, nRectX, nRectY ) );
                                        //CDTXMania.stage演奏ドラム画面.tx判定画像anime.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(nRectX * 5, nRectY * base.st状態[j].nRect, nRectX, nRectY));
                                    }
                                }
                                else if( base.tx判定文字列[ num4 ] != null )
                                {
                                    x = xc - ( (int)( ( 64f * base.st状態[ j ].fX方向拡大率 ) ) );
                                    y = ( num6 + base.st状態[ j ].n相対Y座標 ) - ( (int)( ( ( 43f * base.st状態[ j ].fY方向拡大率 ) ) / 2.0 ) );

                                    base.tx判定文字列[ num4 ].n透明度 = base.st状態[ j ].n透明度;
                                    base.tx判定文字列[ num4 ].vc拡大縮小倍率 = new Vector3( base.st状態[ j ].fX方向拡大率, base.st状態[ j ].fY方向拡大率, 1f );
                                    base.tx判定文字列[ num4 ].t2D描画( CDTXMania.app.Device, x, y, base.st判定文字列[ (int)base.st状態[ j ].judge ].rc );
                                }


                                if( base.nShowLagType >= (int)EShowLagType.ON )
                                {
                                    if( base.st状態[ j ].judge != E判定.Auto && base.st状態[ j ].judge != E判定.Miss )		// #25370 2011.2.1 yyagi
                                    {
                                        this.n最後に叩いたチップのラグms = base.st状態[ j ].nLag;
                                        this.bラグ表示を描画する = true;
                                    }
                                    else if( base.st状態[ j ].judge == E判定.Miss )
                                    {
                                        // Auto時更新されないだけなのでミスの場合だけ表示フラグを外す(暫定)
                                        this.bラグ表示を描画する = false;
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                else if( CDTXMania.ConfigIni.eJudgeAnimeType >= Eタイプ.C )
                {
                    int nRectX = 85;
                    int nRectY = 35;
                    int nJudgePosY = CDTXMania.stage演奏ドラム画面GITADORA.演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );

                    for( int i = 0; i < 12; i++ )
                    {
                        if( base._判定文字[ i ].b表示 )
                        {
                            if( base.tx判定文字列[ 0 ] != null )
                            {
                                if( base._判定文字[ i ].var棒Z軸回転度 != null &&
                                    base._判定文字[ i ].e判定 == E判定.Perfect || base._判定文字[ i ].e判定 == E判定.Great )
                                {
                                    Matrix matbar = Matrix.Identity;
                                    matbar *= Matrix.Scaling( (float)base._判定文字[ i ].var棒拡大率X.Value,
                                        (float)base._判定文字[ i ].var棒拡大率Y.Value, 0 );
                                    matbar *= Matrix.RotationZ( (float)base._判定文字[ i ].var棒Z軸回転度.Value );
                                    matbar *= Matrix.Translation( ( this.stレーンサイズ[ i ].x - 640 ) - 2 + this.stレーンサイズ[ i ].w / 2,
                                        -( (float)base._判定文字[ i ].var文字中心位置Y.Value ) - ( ( nJudgePosY - 360 ) - ( !CDTXMania.ConfigIni.bReverse.Drums ? 211 : -23 ) - -this.n文字の縦表示位置[ i ] * 5 ),
                                        0 );

                                    base.tx判定文字列[ 0 ].n透明度 = 255;
                                    base.tx判定文字列[ 0 ].b加算合成 = true;
                                    base.tx判定文字列[ 0 ].t3D描画( CDTXMania.app.Device, matbar, base._判定文字[ i ].rect棒範囲 );
                                }

                                Matrix mat = Matrix.Identity;
                                mat *= Matrix.Scaling( (float)base._判定文字[ i ].var文字拡大率X.Value,
                                    (float)base._判定文字[ i ].var文字拡大率Y.Value, 0 );
                                mat *= Matrix.RotationZ( (float)base._判定文字[ i ].var文字Z軸回転度.Value );
                                mat *= Matrix.Translation( ( this.stレーンサイズ[ i ].x - 640 ) + this.stレーンサイズ[ i ].w / 2,
                                    -( (float)base._判定文字[ i ].var文字中心位置Y.Value ) - ( ( nJudgePosY - 360 ) - ( !CDTXMania.ConfigIni.bReverse.Drums ? 211 : -23 ) - -this.n文字の縦表示位置[ i ] * 5 ),
                                    0 );

                                base.tx判定文字列[ 0 ].n透明度 = (int)base._判定文字[ i ].var文字不透明度.Value;
                                base.tx判定文字列[ 0 ].b加算合成 = false;
                                base.tx判定文字列[ 0 ].t3D描画( CDTXMania.app.Device, mat, base._判定文字[ i ].rect画像範囲 );

                                if( base._判定文字[ i ].ストーリーボード_オーバーレイ != null )
                                {
                                    Matrix mato = Matrix.Identity;
                                    mato *= Matrix.Scaling( (float)base._判定文字[ i ].var文字オーバーレイ拡大率X.Value,
                                        (float)base._判定文字[ i ].var文字オーバーレイ拡大率Y.Value, 0 );
                                    mato *= Matrix.RotationZ( (float)base._判定文字[ i ].var文字Z軸回転度.Value );
                                    mato *= Matrix.Translation( ( this.stレーンサイズ[ i ].x - 640 ) + this.stレーンサイズ[ i ].w / 2,
                                        -( (float)base._判定文字[ i ].var文字中心位置Y.Value ) - ( ( nJudgePosY - 360 ) - ( !CDTXMania.ConfigIni.bReverse.Drums ? 211 : -23 ) - -this.n文字の縦表示位置[ i ] * 5 ),
                                        0 );

                                    base.tx判定文字列[ 0 ].n透明度 = (int)base._判定文字[ i ].var文字オーバーレイ不透明度.Value;
                                    base.tx判定文字列[ 0 ].b加算合成 = true;
                                    base.tx判定文字列[ 0 ].t3D描画( CDTXMania.app.Device, mato, base._判定文字[ i ].rect画像範囲 );
                                }
                            }

                            if( base.nShowLagType == (int)EShowLagType.ON )
                            {
                                if( base.st状態[ i ].judge != E判定.Auto && base.st状態[ i ].judge != E判定.Miss )		// #25370 2011.2.1 yyagi
                                {
                                    this.n最後に叩いたチップのラグms = base.st状態[ i ].nLag;
                                    this.bラグ表示を描画する = true;
                                }
                                else if( base.st状態[ i ].judge == E判定.Miss )
                                {
                                    // Auto時更新されないだけなのでミスの場合だけ表示フラグを外す(暫定)
                                    this.bラグ表示を描画する = false;
                                }
                            }

                            if( base._判定文字[ i ].ストーリーボード.Status == SharpDX.Animation.StoryboardStatus.Ready )
                            {
                                base._判定文字[ i ].b表示 = false;
                            }
                        }
                    }
                }

                // 2019.07.20 kairera0467 GITADORA風
                // GITADORA風の判定ラグ表示は、一番最後に叩いたチップのラグ時間に依存する(っぽい)
                // なので判定画像表示のforループからは分離して、こちらでは外部の変数に対応した表示をするようにする。
                if( this.bラグ表示を描画する )
                {
                    string str符号 = this.n最後に叩いたチップのラグms < 0 ? "+" : "-";
                    int p = this.n最後に叩いたチップのラグms == 0 ? 2 : this.n最後に叩いたチップのラグms < 0 ? 1 : 0;
                    int offsetX = 0;

                    //CDTXMania.act文字コンソール.tPrint( 420, 444, C文字コンソール.Eフォント種別.白, "+0.000" );
                    //CDTXMania.act文字コンソール.tPrint(420, 444, C文字コンソール.Eフォント種別.白, str符号 + (Math.Abs(this.n最後に叩いたチップのラグms / 1000.0)).ToString("0.000"));
                    foreach( char c in str符号 + (Math.Abs(this.n最後に叩いたチップのラグms / 1000.0)).ToString("0.000") )
                    {
                        int index = 0;
                        if( c == '.' )
                        {
                            index = 10;
                        }
                        else if( c == '+' || c == '-' )
                        {
                            index = 11;
                        }
                        else
                        {
                            index = Convert.ToInt32( c.ToString() );
                        }
                        this.txlag数値.t2D描画( CDTXMania.app.Device, 412 + offsetX, 446, base.stLag数値[ index + 12 * p ].rc );
                        offsetX += (c == '.') ? 10 : 12;
                    }

                }
            }
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
        //HH SD BD HT LT FT CY HHO RD LC LP LBD
        //private readonly int[] n文字の縦表示位置 = new int[] { -1, 1, 1, 2, 0, 0, 1, -1, 2, 1, 2, -1, -1, 0, 0 };
        private readonly int[] n文字の縦表示位置 = new int[] { 0, 9, 9, 14, 4, 4, 9, 0, 9, 4, 0, 0, 0, 0, 0 };
		private STレーンサイズ[] stレーンサイズ;
        private int n最後に叩いたチップのラグms;
        private bool bラグ表示を描画する;
		//-----------------
		#endregion
	}
}
