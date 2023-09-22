using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using SharpDX;
using FDK;

using Rectangle = System.Drawing.Rectangle;
namespace DTXMania
{
	internal class CAct演奏DrumsチップファイアD : CActivity
	{
		// コンストラクタ

		public CAct演奏DrumsチップファイアD()
		{
			base.b活性化してない = true;
		}
		
		
		// メソッド

		public void Start( Eレーン lane )
		{
			this.Start( lane, false, false, false, 0 );
		}
		public void Start( Eレーン lane, bool bフィルイン )
		{
			this.Start( lane, bフィルイン, false, false, 0 );
		}
		public void Start( Eレーン lane, bool bフィルイン, bool b大波 )
		{
			this.Start( lane, bフィルイン, b大波, false, 0 );
		}
		public void Start( Eレーン lane, bool bフィルイン, bool b大波, bool b細波 )
		{
			this.Start( lane, bフィルイン, b大波, b細波, 0 );
		}
		public void Start( Eレーン lane, bool bフィルイン, bool b大波, bool b細波, int _nJudgeLinePosY_delta_Drums )
		{
			//nJudgeLinePosY_delta_Drums = _nJudgeLinePosY_delta_Drums;
            int iYpos = CDTXMania.stage演奏ドラム画面.演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );
            float fPos = CDTXMania.ConfigIni.bReverse.Drums ? iYpos - 183 : iYpos - 186;

			if( this.tx火花 != null ) // 表示しない設定の分岐は未実装
			{
				for ( int j = 0; j < FIRE_MAX; j++ )
				{
					if ( this.st火花[ j ].b使用中 && this.st火花[ j ].nLane == (int) lane )		// yyagi 負荷軽減のつもり・・・だが、あまり効果なさげ
					{
						this.st火花[ j ].ct進行.t停止();
						this.st火花[ j ].b使用中 = false;
					}
				}
				float n回転初期値 = CDTXMania.Random.Next( 360 );
				for( int i = 0; i < 1; i++ )
				{
					for( int j = 0; j < FIRE_MAX; j++ )
					{
						if( !this.st火花[ j ].b使用中 )
						{
							this.st火花[ j ].b使用中 = true;
							this.st火花[ j ].nLane = (int) lane;
                            if( CDTXMania.ConfigIni.nExplosionFrames == 1 )
                            {
                                this.st火花[ j ].ct進行 = new CCounter( 0, 70, 3, CDTXMania.Timer );
                            }
                            else
                            {
                                this.st火花[ j ].ct進行 = new CCounter( 0, CDTXMania.ConfigIni.nExplosionFrames - 1, CDTXMania.ConfigIni.nExplosionInterval, CDTXMania.Timer );
                            }
							this.st火花[ j ].f回転単位 = C変換.DegreeToRadian( (float)( n回転初期値 + ( i * 90.0f ) ) );
							this.st火花[ j ].fサイズ = ( i < 4 ) ? 1f : 0.5f;
							break;
						}
					}
				}
			}
            //if ((this.tx青い星 != null) && (CDTXMania.ConfigIni.eAttackEffect.Drums == Eタイプ.A || CDTXMania.ConfigIni.eAttackEffect.Drums == Eタイプ.B))
            if( ( this.tx青い星 != null ) )
            {
                for( int i = 0; i < 12; i++ )
                {
                    for( int j = 0; j < STAR_MAX; j++ )
                    {
                        if( !this.st青い星[ j ].b使用中 )
                        {
                            this.st青い星[ j ].b使用中 = true;
                            int n回転初期値 = CDTXMania.Random.Next( 360 );
                            double num7 = 0.89 + ( 1 / 100.0 ); // 拡散の大きさ
                            this.st青い星[ j ].nLane = (int)lane;
                            this.st青い星[ j ].ct進行 = new CCounter( 0, 40, 7, CDTXMania.Timer ); // カウンタ
                            this.st青い星[ j ].fX = this.nレーンの中央X座標[ (int)lane ] + 320; //X座標
                            this.st青い星[ j ].fY = ( ( fPos + 350.0f + ( ( (float)Math.Sin( this.st青い星[ j ].f半径 ) ) * this.st青い星[ j ].f半径 ) ) - 170.0f ); //Y座標
                            this.st青い星[ j ].f加速度X = (float)( num7 * Math.Cos( ( Math.PI * 2 * n回転初期値 ) / 360.0 ) );
                            this.st青い星[ j ].f加速度Y = (float)( num7 * ( Math.Sin( ( Math.PI * 2 * n回転初期値 ) / 360.0f ) ) - 0.1f );
                            this.st青い星[ j ].f加速度の加速度X = 1.000f;
                            this.st青い星[ j ].f加速度の加速度Y = 1.010f;
                            this.st青い星[ j ].f重力加速度 = 0.02040f;
                            this.st青い星[ j ].f半径 = (float)( 0.3f + ( ( (double)CDTXMania.Random.Next( 30 ) ) / 100.0f ) );
                            break;
                        }
                    }
                }
            }
			if( bフィルイン && ( this.tx青い星 != null ) )
			{
				for( int i = 0; i < 0x10; i++ )
				{
					for( int j = 0; j < STAR_MAX; j++ )
					{
						if( !this.st青い星[ j ].b使用中 )
						{
                            this.st青い星[ j ].b使用中 = true;
                            int n回転初期値 = CDTXMania.Random.Next( 360 );
                            double num7 = 0.89 + ( 1 / 100.0 ); // 拡散の大きさ
                            this.st青い星[ j ].nLane = (int)lane;
                            this.st青い星[ j ].ct進行 = new CCounter( 0, 40, 7, CDTXMania.Timer ); // カウンタ
                            this.st青い星[ j ].fX = this.nレーンの中央X座標[ (int)lane ] + 320; //X座標
                            this.st青い星[ j ].fY = ( ( fPos + 350.0f + ( ( (float)Math.Sin( this.st青い星[ j ].f半径 ) ) * this.st青い星[ j ].f半径 ) ) - 170.0f ); //Y座標
                            this.st青い星[ j ].f加速度X = (float)( num7 * Math.Cos( ( Math.PI * 2 * n回転初期値 ) / 360.0 ) );
                            this.st青い星[ j ].f加速度Y = (float)( num7 * ( Math.Sin( ( Math.PI * 2 * n回転初期値 ) / 360.0f ) ) - 0.1f );
                            this.st青い星[ j ].f加速度の加速度X = 1.000f;
                            this.st青い星[ j ].f加速度の加速度Y = 1.010f;
                            this.st青い星[ j ].f重力加速度 = 0.02040f;
                            this.st青い星[ j ].f半径 = (float)( 0.3f + ( ( (double)CDTXMania.Random.Next( 30 ) ) / 100.0f ) );
							break;
						}
					}
				}
			}
            //if (this.txNotes != null && b表示 && CDTXMania.ConfigIni.eAttackEffect.Drums == Eタイプ.A)
            if( this.txNotes != null )
            {
                    for( int j = 0; j < 8; j++ )
                    {
                        if( !this.st飛び散るチップ[ j ].b使用中 )
                        {
                            this.st飛び散るチップ[ j ].b使用中 = true;
                            int n回転初期値 = 1;
                            double num7 = 0.9 + ( 1 / 100.0 ); // 拡散の大きさ
                            this.st飛び散るチップ[ j ].nLane = (int)lane;
                            this.st飛び散るチップ[ j ].ct進行 = new CCounter( 0, 44, 10, CDTXMania.Timer ); // カウンタ

                            this.st飛び散るチップ[ j ].fXL = this.nレーンの中央X座標[ (int)lane ] + this.nノーツの幅[ (int)lane ] + 312; //X座標
                            this.st飛び散るチップ[ j ].fXR = this.nレーンの中央X座標[ (int)lane ] + this.nノーツの幅[ (int)lane ] + 312; //X座標
                            this.st飛び散るチップ[ j ].fY = ( (fPos + 359 + ( ( (float)Math.Sin( this.st青い星[ j ].f半径 ) ) * this.st青い星[ j ].f半径 ) ) - 170f );
                            this.st飛び散るチップ[ j ].f加速度X = (float)( num7 * Math.Cos( ( Math.PI * 2 * n回転初期値 ) / 360.0 ) + 0.3 );
                            this.st飛び散るチップ[ j ].f加速度Y = (float)( num7 * ( Math.Sin ( (Math.PI * 2 * n回転初期値 ) / 360.0 ) - 0.8 ) );
                            this.st飛び散るチップ[ j ].f加速度の加速度X = 0.995f;
                            this.st飛び散るチップ[ j ].f加速度の加速度Y = 1.000f;
                            this.st飛び散るチップ[ j ].f重力加速度 = 0.03100f;
                            this.st飛び散るチップ[ j ].f回転単位 = C変換.DegreeToRadian( (float)( n回転初期値 + ( 1.0f * 90.0f ) ) );
                            this.st飛び散るチップ[ j ].f回転方向 = ( 1 < 4 ) ? 1f : -2f;
                            this.st飛び散るチップ[ j ].f半径 = (float)( 0.5 + ( CDTXMania.Random.Next( 30 ) / 100.0 ) );
                            if( this.st飛び散るチップ[ j ].nLane == 0 || this.st飛び散るチップ[ j ].nLane == 3 || this.st飛び散るチップ[ j ].nLane == 7 )
                            {

                            }
                            else if( this.st飛び散るチップ[ j ].nLane == 1 )
                            {
                                this.st飛び散るチップ[ j ].fXL += 20f;
                            }
                            else
                            {
                                this.st飛び散るチップ[ j ].fXL += 10f;
                            }
                            break;
                        }
                    }
            }
			if( b大波 && ( this.tx大波 != null ) )
			{
				for( int i = 0; i < 4; i++ )
				{
					for( int j = 0; j < BIGWAVE_MAX; j++ )
					{
						if( !this.st大波[ j ].b使用中 )
						{
							this.st大波[ j ].b使用中 = true;
							this.st大波[ j ].nLane = (int) lane;
							this.st大波[ j ].f半径 = ( (float) ( ( 20 - CDTXMania.Random.Next( 40 ) ) + 100 ) ) / 100f;
							this.st大波[ j ].n進行速度ms = 10;
							this.st大波[ j ].ct進行 = new CCounter( 0, 100, this.st大波[ j ].n進行速度ms, CDTXMania.Timer );
							this.st大波[ j ].ct進行.n現在の値 = i * 10;
							this.st大波[ j ].f角度X = C変換.DegreeToRadian( (float) ( ( ( (double) ( CDTXMania.Random.Next( 100 ) * 50 ) ) / 100.0 ) + 30.0 ) );
							this.st大波[ j ].f角度Y = C変換.DegreeToRadian( this.b大波Balance ? ( this.fY波の最小仰角[ (int) lane ] + CDTXMania.Random.Next( 30 ) ) : ( this.fY波の最大仰角[ (int) lane ] - CDTXMania.Random.Next( 30 ) ) );
							this.st大波[ j ].f回転単位 = C変換.DegreeToRadian( (float) 0f );
							this.st大波[ j ].f回転方向 = 1f;
							this.b大波Balance = !this.b大波Balance;
							break;
						}
					}
				}
			}
			if( b細波 && ( this.tx細波 != null ) )
			{
				for( int i = 0; i < 1; i++ )
				{
					for( int j = 0; j < BIGWAVE_MAX; j++ )
					{
						if( !this.st細波[ j ].b使用中 )
						{
							this.st細波[ j ].b使用中 = true;
							this.st細波[ j ].nLane = (int) lane;
							this.st細波[ j ].f半径 = ( (float) ( ( 20 - CDTXMania.Random.Next( 40 ) ) + 100 ) ) / 100f;
							this.st細波[ j ].n進行速度ms = 8;
							this.st細波[ j ].ct進行 = new CCounter( 0, 100, this.st細波[ j ].n進行速度ms, CDTXMania.Timer );
							this.st細波[ j ].ct進行.n現在の値 = 0;
							this.st細波[ j ].f角度X = C変換.DegreeToRadian( (float) ( ( ( (double) ( CDTXMania.Random.Next( 100 ) * 50 ) ) / 100.0 ) + 30.0 ) );
							this.st細波[ j ].f角度Y = C変換.DegreeToRadian( this.b細波Balance ? ( this.fY波の最小仰角[ (int) lane ] + CDTXMania.Random.Next( 30 ) ) : ( this.fY波の最大仰角[ (int) lane ] - CDTXMania.Random.Next( 30 ) ) );
							this.b細波Balance = !this.b細波Balance;
							break;
						}
					}
				}
			}		}


		// CActivity 実装

		public override void On活性化()
		{
			for( int i = 0; i < FIRE_MAX; i++ )
			{
				this.st火花[ i ] = new ST火花();
				this.st火花[ i ].b使用中 = false;
				this.st火花[ i ].ct進行 = new CCounter();
                this.st火花[ i ].ctフレーム = new CCounter();
			}
			for( int i = 0; i < STAR_MAX; i++ )
			{
				this.st青い星[ i ] = new ST青い星();
				this.st青い星[ i ].b使用中 = false;
				this.st青い星[ i ].ct進行 = new CCounter();
			}
            for( int i = 0; i < 8; i++ )
            {
                this.st飛び散るチップ[ i ] = new ST飛び散るチップ();
                this.st飛び散るチップ[ i ].b使用中 = false;
                this.st飛び散るチップ[ i ].ct進行 = new CCounter();
            }
			for( int i = 0; i < BIGWAVE_MAX; i++ )
			{
				this.st大波[ i ] = new ST大波();
				this.st大波[ i ].b使用中 = false;
				this.st大波[ i ].ct進行 = new CCounter();
				this.st細波[ i ] = new ST細波();
				this.st細波[ i ].b使用中 = false;
				this.st細波[ i ].ct進行 = new CCounter();
			}

            this.tレーンタイプからレーン位置を設定する( CDTXMania.ConfigIni.eLaneType, CDTXMania.ConfigIni.eRDPosition );
			base.On活性化();
		}
		public override void On非活性化()
		{
			for( int i = 0; i < FIRE_MAX; i++ )
			{
				this.st火花[ i ].ct進行 = null;
                this.st火花[ i ].ctフレーム = null;
			}
			for( int i = 0; i < STAR_MAX; i++ )
			{
				this.st青い星[ i ].ct進行 = null;
			}
            for( int i = 0; i < 8; i++ )
            {
                this.st飛び散るチップ[ i ].ct進行 = null;
            }
			for( int i = 0; i < BIGWAVE_MAX; i++ )
			{
				this.st大波[ i ].ct進行 = null;
				this.st細波[ i ].ct進行 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.tx火花[0] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_LC.png" ) );
				this.tx火花[1] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_HH.png" ) );
                this.tx火花[2] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_SD.png" ) );
                this.tx火花[3] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_BD.png" ) );
                this.tx火花[4] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_HT.png" ) );
                this.tx火花[5] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_LT.png" ) );
                this.tx火花[6] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_FT.png" ) );
                this.tx火花[7] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_CY.png" ) );
                this.tx火花[8] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_LP.png" ) );
                this.tx火花[9] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_RD.png" ) );

				this.tx青い星[0] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_LC.png" ) );
                this.tx青い星[1] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_HH.png" ) );
                this.tx青い星[2] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_SD.png" ) );
                this.tx青い星[3] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_BD.png" ) );
                this.tx青い星[4] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_HT.png" ) );
                this.tx青い星[5] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_LT.png" ) );
                this.tx青い星[6] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_FT.png" ) );
                this.tx青い星[7] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_CY.png" ) );
                this.tx青い星[8] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_LP.png" ) );
                this.tx青い星[9] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip star_RD.png" ) );

                if( CDTXMania.ConfigIni.nExplosionFrames >= 2 )
                {
                    this.tx火花2 = CDTXMania.tテクスチャの生成Af( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire.png" ) );
                    if( this.tx火花2 != null )
                    {
                        this.tx火花2.b加算合成 = true;
                    }
                }

                for( int i = 0; i < 10; i++ )
                {
                    if( this.tx火花[ i ] != null )
                    {
                        this.tx火花[ i ].b加算合成 = true;
                    }
                    if( this.tx青い星[ i ] != null )
                    {
                        this.tx青い星[ i ].b加算合成 = true;
                    }
                }

				this.tx大波 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip wave.png" ) );
				if( this.tx大波 != null )
				{
					this.tx大波.b加算合成 = true;
				}
				this.tx細波 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip wave2.png" ) );
				if( this.tx細波 != null )
				{
					this.tx細波.b加算合成 = true;
				}
                this.txNotes = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Chips_drums.png" ) );
                if( this.txNotes != null )
                {
                    this.txNotes.n透明度 = 120;
                    this.txNotes.b加算合成 = true;
                }
                this.txボーナス花火 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums chip fire_Bonus.png" ) );
                if( this.txボーナス花火 != null )
                {
                    this.txボーナス花火.b加算合成 = true;
                }

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                for( int i = 0; i < 10; i++ )
                {
    				CDTXMania.tテクスチャの解放( ref this.tx火花[ i ] );
	    			CDTXMania.tテクスチャの解放( ref this.tx青い星[ i ] );
                }
				CDTXMania.tテクスチャの解放( ref this.tx大波 );
				CDTXMania.tテクスチャの解放( ref this.tx細波 );
                CDTXMania.tテクスチャの解放( ref this.txNotes );
                CDTXMania.tテクスチャの解放( ref this.tx火花2 );
                CDTXMania.tテクスチャの解放( ref this.txボーナス花火 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                int iYpos = CDTXMania.stage演奏ドラム画面.演奏判定ライン座標.n判定ラインY座標( E楽器パート.DRUMS, false, CDTXMania.ConfigIni.bReverse.Drums, false, true );
                float fPos = CDTXMania.ConfigIni.bReverse.Drums ? iYpos - 183 : iYpos - 186;
                float fPosTest = ( iYpos - ( SampleFramework.GameWindowSize.Height / 2.0f ) ) * -1.0f;

                for( int i = 0; i < STAR_MAX; i++ )
                {
                    if( this.st青い星[ i ].b使用中 )
                    {
                        this.st青い星[ i ].n前回のValue = this.st青い星[ i ].ct進行.n現在の値;
                        this.st青い星[ i ].ct進行.t進行();
                        if (this.st青い星[ i ].ct進行.b終了値に達した)
                        {
                            this.st青い星[ i ].ct進行.t停止();
                            this.st青い星[ i ].b使用中 = false;
                        }
                        for (int n = this.st青い星[ i ].n前回のValue; n < this.st青い星[ i ].ct進行.n現在の値; n++)
                        {
                            this.st青い星[ i ].fX += this.st青い星[ i ].f加速度X;
                            this.st青い星[ i ].fY -= this.st青い星[ i ].f加速度Y;
                            this.st青い星[ i ].f加速度X *= this.st青い星[ i ].f加速度の加速度X;
                            this.st青い星[ i ].f加速度Y *= this.st青い星[ i ].f加速度の加速度Y;
                            this.st青い星[ i ].f加速度Y -= this.st青い星[ i ].f重力加速度;
                        }
                        Matrix mat = Matrix.Identity;

                        float x = (float)( this.st青い星[ i ].f半径 * Math.Cos( ( Math.PI / 2.0f * this.st青い星[ i ].ct進行.n現在の値 ) / 100.0f ) );
                        mat *= Matrix.Scaling(x, x, 1f);
                        mat *= Matrix.Translation( this.st青い星[ i ].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st青い星[i].fY - SampleFramework.GameWindowSize.Height / 2 ), 0f );

                        if (this.tx青い星[ this.st青い星[ i ].nLane ] != null)
                        {
                            this.tx青い星[ this.st青い星[ i ].nLane ].t3D描画( CDTXMania.app.Device, mat );
                        }
                    }
                }

                //if (CDTXMania.ConfigIni.eAttackEffect.Drums == Eタイプ.A)
                {
                    for( int i = 0; i < 8; i++ )
                    {
                        if( this.st飛び散るチップ[ i ].b使用中 )
                        {
                            this.st飛び散るチップ[ i ].n前回のValue = this.st飛び散るチップ[ i ].ct進行.n現在の値;
                            this.st飛び散るチップ[ i ].ct進行.t進行();
                            if( this.st飛び散るチップ[ i ].ct進行.b終了値に達した )
                            {
                                this.st飛び散るチップ[ i ].ct進行.t停止();
                                this.st飛び散るチップ[ i ].b使用中 = false;
                            }
                            for( int n = this.st飛び散るチップ[ i ].n前回のValue; n < this.st飛び散るチップ[ i ].ct進行.n現在の値; n++ )
                            {
                                //これは物理放物線を利用する。
                                //θ=角度　角度は大体60度ぐらいかな。
                                //(注:ここでMath.Cosで使用する値はラジアンなので、角度 * Math.PI / 180をする必要がある。)
                                //X方向は加速度を加算しない。
                                //Y方向はY = (初速度 * sin(θ) * 定数 - ((重力加速度 * 定数)2乗) / 2)
                                //Y座標の加速度は重力加速度を使って加算していく。

                                this.st飛び散るチップ[ i ].fXL += (float)( ( this.st飛び散るチップ[ i ].f加速度X * Math.Cos( ( 120.0 * Math.PI / 180.0 ) ) ) * 5.0f );
                                this.st飛び散るチップ[ i ].fXR += (float)( ( this.st飛び散るチップ[ i ].f加速度X * Math.Cos( ( 60.0 * Math.PI / 180.0 ) ) ) * 5.0f );

                                this.st飛び散るチップ[ i ].fY += (float)( ( this.st飛び散るチップ[ i ].f加速度Y * Math.Sin( ( 60.0 * Math.PI / 180.0 ) ) ) * 10.0f - Math.Exp( this.st飛び散るチップ[ i ].f重力加速度 * 2.0f ) / 2.0f );
                                this.st飛び散るチップ[ i ].f加速度X *= this.st飛び散るチップ[ i ].f加速度の加速度X;
                                //this.st飛び散るチップ[i].fY *= this.st飛び散るチップ[i].f加速度Y;
                                this.st飛び散るチップ[ i ].f加速度Y += this.st飛び散るチップ[ i ].f重力加速度;
                            }



                            Matrix mat = Matrix.Identity;
                            Matrix mat2 = Matrix.Identity;

                            mat *= Matrix.RotationZ( 0.09f * this.st飛び散るチップ[ i ].ct進行.n現在の値 );
                            mat2 *= Matrix.RotationZ( -0.09f * this.st飛び散るチップ[ i ].ct進行.n現在の値 );

                            mat *= Matrix.Translation( ( this.st飛び散るチップ[ i ].fXL - 50f ) - SampleFramework.GameWindowSize.Width / 2, -( this.st飛び散るチップ[i].fY - SampleFramework.GameWindowSize.Height / 2 ), 0f );
                            mat2 *= Matrix.Translation( ( this.st飛び散るチップ[ i ].fXR - 50f ) - SampleFramework.GameWindowSize.Width / 2, -( this.st飛び散るチップ[i].fY - SampleFramework.GameWindowSize.Height / 2 ), 0f );
                            //mat *= Matrix.Translation(this.st飛び散るチップ[i].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st青い星[i].fY - SampleFramework.GameWindowSize.Height / 2), 0f);

                            if( this.txNotes != null )
                            {
                                this.txNotes.t3D描画( CDTXMania.app.Device, mat, new Rectangle( ( nノーツの左上X座標[ this.st飛び散るチップ[ i ].nLane ] ), 640, ( nノーツの幅[ this.st飛び散るチップ[ i ].nLane ] + 10 ) / 2, 64 ) );
                                this.txNotes.t3D描画( CDTXMania.app.Device, mat2, new Rectangle( ( nノーツの左上X座標[ this.st飛び散るチップ[ i ].nLane ] ), 640, ( nノーツの幅[ this.st飛び散るチップ[ i ].nLane ] + 10 ) / 2, 64 ) );
                            }
                        }

                    }
                }

				for( int i = 0; i < FIRE_MAX; i++ )
				{
					if( this.st火花[ i ].b使用中 )
					{
						this.st火花[ i ].ct進行.t進行();
						if( this.st火花[ i ].ct進行.b終了値に達した )
						{
							this.st火花[ i ].ct進行.t停止();
							this.st火花[ i ].b使用中 = false;
						}
                        if (CDTXMania.ConfigIni.nExplosionFrames <= 1)
                        {
                            Matrix identity = Matrix.Identity;
                            float num2 = ((float)this.st火花[i].ct進行.n現在の値) / 70f;
                            float num3 = this.st火花[i].f回転単位 + (this.st火花[i].f回転方向 * C変換.DegreeToRadian((float)(60f * num2)));
                            float num4 = ((float)(0.2 + (0.8 * Math.Cos((((double)this.st火花[i].ct進行.n現在の値) / 50.0) * 1.5707963267948966)))) * this.st火花[i].fサイズ;
                            identity *= Matrix.Scaling(0.2f + num4, 0.2f + num4, 1f);
                            //identity *= Matrix.RotationZ( num3 + ( (float) Math.PI / 2 ) );
                            float num5 = ((float)(0.8 * Math.Sin(num2 * 1.5707963267948966))) * this.st火花[i].fサイズ;
                            

                            identity *= Matrix.Translation( ( this.nレーンの中央X座標[ this.st火花[i].nLane ] ) - 320.0f, fPosTest, 0f);
                            if( this.tx火花[ this.st火花[ i ].nLane ] != null )
                            {
                                this.tx火花[ this.st火花[ i ].nLane ].t3D描画(CDTXMania.app.Device, identity );
                                if( CDTXMania.stage演奏ドラム画面.actBPMBar.bサビ区間 == true && this.txボーナス花火 != null )
                                    this.txボーナス花火.t3D描画( CDTXMania.app.Device, identity );
                            }
                        }
                        else
                        {
                            Matrix identity = Matrix.Identity;
                            identity *= Matrix.Translation( this.nレーンの中央X座標[ this.st火花[ i ].nLane ] - 320f, fPosTest, 0f );
                            if( this.tx火花2 != null )
                            {
                                int n幅 = CDTXMania.ConfigIni.nExplosionWidth;
                                int n高さ = CDTXMania.ConfigIni.nExplosionHeight;

                                this.tx火花2.t3D描画( CDTXMania.app.Device, identity, new Rectangle( n幅 * this.st火花[ i ].ct進行.n現在の値, this.st火花[ i ].nLane * n高さ, n幅, n高さ ) );
                                if( CDTXMania.stage演奏ドラム画面.actBPMBar.bサビ区間 == true && this.txボーナス花火 != null )
                                    this.tx火花2.t3D描画( CDTXMania.app.Device, identity, new Rectangle( this.st火花[ i ].ct進行.n現在の値 * n幅, 10 * n高さ, n幅, n高さ ) );
                            }
                        }
					}
				}
				for( int i = 0; i < BIGWAVE_MAX; i++ )
				{
					if( this.st大波[ i ].b使用中 )
					{
						this.st大波[ i ].ct進行.t進行();
						if( this.st大波[ i ].ct進行.b終了値に達した )
						{
							this.st大波[ i ].ct進行.t停止();
							this.st大波[ i ].b使用中 = false;
						}
						if( this.st大波[ i ].ct進行.n現在の値 >= 0 )
						{
							Matrix matrix3 = Matrix.Identity;
							float num10 = ( (float) this.st大波[ i ].ct進行.n現在の値 ) / 100f;
							float angle = this.st大波[ i ].f回転単位 + ( this.st大波[ i ].f回転方向 * C変換.DegreeToRadian( (float) ( 60f * num10 ) ) );
							float num12 = 1f;
							if( num10 < 0.4f )
							{
								num12 = 2.5f * num10;
							}
							else if( num10 < 0.8f )
							{
								num12 = (float) ( 1.0 + ( 10.1 * ( 1.0 - Math.Cos( ( Math.PI / 2 * ( num10 - 0.4 ) ) * 2.5 ) ) ) );
							}
							else
							{
								num12 = 11.1f + ( 12.5f * ( num10 - 0.8f ) );
							}
							int num13 = 0xff;
							if( num10 < 0.75f )
							{
								num13 = 0x37;
							}
							else
							{
								num13 = (int) ( ( 55f * ( 1f - num10 ) ) / 0.25f );
							}
							matrix3 *= Matrix.Scaling( num12 * this.st大波[ i ].f半径, num12 * this.st大波[ i ].f半径, 1f );
							matrix3 *= Matrix.RotationZ( angle );
							matrix3 *= Matrix.RotationX( this.st大波[ i ].f角度X );
							matrix3 *= Matrix.RotationY( this.st大波[ i ].f角度Y );
                            matrix3 *= Matrix.Translation(this.nレーンの中央X座標[this.st大波[i].nLane] + 280 - SampleFramework.GameWindowSize.Width / 2, -(200 - SampleFramework.GameWindowSize.Height / 2), 0f);
							if( this.tx大波 != null )
							{
								this.tx大波.n透明度 = num13;
								this.tx大波.t3D描画( CDTXMania.app.Device, matrix3 );
							}
						}
					}
				}
				for( int i = 0; i < BIGWAVE_MAX; i++ )
				{
					if( this.st細波[ i ].b使用中 )
					{
						this.st細波[ i ].ct進行.t進行();
						if( this.st細波[ i ].ct進行.b終了値に達した )
						{
							this.st細波[ i ].ct進行.t停止();
							this.st細波[ i ].b使用中 = false;
						}
						if( this.st細波[ i ].ct進行.n現在の値 >= 0 )
						{
							Matrix matrix4 = Matrix.Identity;
							float num15 = ( (float) this.st細波[ i ].ct進行.n現在の値 ) / 100f;
							float num16 = 14f * num15;
							int num17 = ( num15 < 0.5f ) ? 155 : ( (int) ( ( 155f * ( 1f - num15 ) ) / 1f ) );
							matrix4 *= Matrix.Scaling(
											num16 * this.st細波[ i ].f半径,
											num16 * this.st細波[ i ].f半径,
											1f
							);
							matrix4 *= Matrix.RotationX( this.st細波[ i ].f角度X );
							matrix4 *= Matrix.RotationY( this.st細波[ i ].f角度Y );
                            matrix4 *= Matrix.Translation(this.nレーンの中央X座標_改[this.st細波[i].nLane] + 280 - SampleFramework.GameWindowSize.Width / 2, -(200 - SampleFramework.GameWindowSize.Height / 2), 0f);
							if (this.tx細波 != null)
							{
								this.tx細波.n透明度 = num17;
								this.tx細波.t3D描画( CDTXMania.app.Device, matrix4 );
							}
						}
					}
				}
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct ST火花
		{
			public int nLane;
			public bool b使用中;
			public CCounter ct進行;
            public CCounter ctフレーム;
			public float f回転単位;
			public float f回転方向;
			public float fサイズ;
		}
		[StructLayout( LayoutKind.Sequential )]
		private struct ST細波
		{
			public int nLane;
			public bool b使用中;
			public CCounter ct進行;
			public float f角度X;
			public float f角度Y;
			public float f半径;
			public int n進行速度ms;
		}
		[StructLayout( LayoutKind.Sequential )]
		private struct ST青い星
		{
			public int nLane;
			public bool b使用中;
			public CCounter ct進行;
			public int n前回のValue;
			public float fX;
			public float fY;
			public float f加速度X;
			public float f加速度Y;
			public float f加速度の加速度X;
			public float f加速度の加速度Y;
			public float f重力加速度;
			public float f半径;
		}
		[StructLayout( LayoutKind.Sequential )]
		private struct ST大波
		{
			public int nLane;
			public bool b使用中;
			public CCounter ct進行;
			public float f角度X;
			public float f角度Y;
			public float f半径;
			public int n進行速度ms;
			public float f回転単位;
			public float f回転方向;
		}
        [StructLayout( LayoutKind.Sequential )]
        private struct STエフェクト
        {
            public int nLane;
            public bool b使用中;
            public CCounter ct進行;
            public int n前回のValue;
            public float fX;
            public float fY;
            public float f加速度X;
            public float f加速度Y;
            public float f加速度の加速度X;
            public float f加速度の加速度Y;
            public float f重力加速度;
            public float f半径;
        }
        [StructLayout( LayoutKind.Sequential )]
        private struct ST飛び散るチップ
        {
            public int nLane;
            public bool b使用中;
            public CCounter ct進行;
            public int n前回のValue;
            public float fXL;
            public float fXR;
            public float fY;
            public float fチップの質量;
            public float f初速度X;
            public float f初速度Y;
            public float f加速度X;
            public float f加速度Y;
            public float f加速度の加速度X;
            public float f加速度の加速度Y;
            public float f重力加速度;
            public float f半径;
            public float f回転単位;
            public float f回転方向;
        }

		private const int BIGWAVE_MAX = 20;
		private bool b細波Balance;
		private bool b大波Balance;
        private const int FIRE_MAX = 64;
        private readonly float[] fY波の最小仰角 = new float[] { -130f, -126f, -120f, -118f, -110f, -108f, -103f, -97f, -85f, -91f, -91f };
                                                            //   LC      HH     SD     BD     HT     LT    FT     CY    RD    LP   LP
        private readonly float[] fY波の最大仰角 = new float[] { 70f, 72f, 77f, 84f, 89f, 91f, 99f, 107f, 117f, 112f, 112f };
                                                            //   LC  HH   SD   BD  HT   LT   FT    CY   RD   LP   LP
        //private readonly int[] nレーンの中央X座標 = new int[] { 7, 71, 176, 293, 230, 349, 398, 464, 124, 514, 124 };
        private readonly int[] nチップエフェクト用X座標 = new int[] { 7, 71, 176, 293, 230, 349, 398, 464, 124, 514, 124 };
                                                       //  LC HH  SD   BD   HT    LT   FT   CY   LP   RD   LP
        private int[] nレーンの中央X座標_改 =  new int[] { 7, 71, 176, 293, 230, 349, 398, 498, 124, 448, 124 };
        private int[] nレーンの中央X座標B =    new int[] { 7, 71, 124, 240, 297, 349, 398, 464, 180, 514, 180 };
        private int[] nレーンの中央X座標B_改 = new int[] { 7, 71, 124, 240, 297, 349, 398, 500, 180, 448, 180 };
        private int[] nレーンの中央X座標C =    new int[] { 7, 71, 176, 242, 297, 349, 398, 464, 124, 508, 124 };
        private int[] nレーンの中央X座標C_改 = new int[] { 7, 71, 176, 242, 297, 349, 398, 500, 124, 448, 124 };
        private int[] nレーンの中央X座標D =    new int[] { 7, 71, 124, 294, 182, 349, 398, 464, 230, 514, 230 };
        private int[] nレーンの中央X座標D_改 = new int[] { 7, 71, 124, 294, 182, 349, 398, 500, 230, 448, 230 };
        private readonly int[] nノーツの左上X座標 = new int[] { 448 + 90, 60 + 10, 106 + 20, 0, 160 + 30, 206 + 40, 252 + 50, 298 + 60, 550 + 110, 362 + 70, 400 + 80 };
        private readonly int[] nノーツの幅 = new int[] { 64, 46, 54, 60, 46, 46, 46, 60, 48, 48, 48 };
		private const int STAR_MAX = 240;
		private ST火花[] st火花 = new ST火花[ FIRE_MAX ];
		private ST大波[] st大波 = new ST大波[ BIGWAVE_MAX ];
		private ST細波[] st細波 = new ST細波[ BIGWAVE_MAX ];
		private ST青い星[] st青い星 = new ST青い星[ STAR_MAX ];
        private ST飛び散るチップ[] st飛び散るチップ = new ST飛び散るチップ[ 8 ];
		private CTexture[] tx火花 = new CTexture[ 10 ];
        private CTextureAf tx火花2;
        private CTexture txボーナス花火;
		private CTexture tx細波;
		private CTexture[] tx青い星 = new CTexture[ 10 ];
		private CTexture tx大波;
        private CTexture txNotes;

        private int[] nレーンの中央X座標 = new int[ 11 ];

        /// <summary>
        /// レーンのX座標をint配列に格納していく。
        /// </summary>
        /// <param name="eLaneType">レーンタイプ</param>
        private void tレーンタイプからレーン位置を設定する( Eタイプ eLaneType, ERDPosition eRDPosition )
        {
            #region[ 共通 ]
            this.nレーンの中央X座標[ 0 ] = 7;  //LC
            this.nレーンの中央X座標[ 1 ] = 71; //HH
            this.nレーンの中央X座標[ 5 ] = 349; //LT
            this.nレーンの中央X座標[ 6 ] = 398; //FT

            #endregion
            #region[ レーンタイプ別 ]
            switch( eLaneType )
            {
                case Eタイプ.A:
                    {
                        this.nレーンの中央X座標[ 2 ] = 176; //SD
                        this.nレーンの中央X座標[ 3 ] = 293; //BD
                        this.nレーンの中央X座標[ 4 ] = 230; //HT
                        this.nレーンの中央X座標[ 8 ] = 124; //LP
                        this.nレーンの中央X座標[ 10 ] = 124; //LBD
                    }
                    break;
                case Eタイプ.B:
                    {
                        this.nレーンの中央X座標[ 2 ] = 124; //SD
                        this.nレーンの中央X座標[ 3 ] = 240; //BD
                        this.nレーンの中央X座標[ 4 ] = 297; //HT
                        this.nレーンの中央X座標[ 8 ] = 180; //LP
                        this.nレーンの中央X座標[ 10 ] = 180; //LBD
                    }
                    break;
                case Eタイプ.C:
                    {
                        this.nレーンの中央X座標[ 2 ] = 176; //SD
                        this.nレーンの中央X座標[ 3 ] = 242; //BD
                        this.nレーンの中央X座標[ 4 ] = 297; //HT
                        this.nレーンの中央X座標[ 8 ] = 124; //LP
                        this.nレーンの中央X座標[ 10 ] = 124; //LBD
                    }
                    break;
                case Eタイプ.D:
                    {
                        this.nレーンの中央X座標[ 2 ] = 124; //SD
                        this.nレーンの中央X座標[ 3 ] = 293; //BD
                        this.nレーンの中央X座標[ 4 ] = 182; //HT
                        this.nレーンの中央X座標[ 8 ] = 230; //LP
                        this.nレーンの中央X座標[ 10 ] = 230; //LBD
                    }
                    break;

            }

            #endregion
            #region [ RC RD ]
            if( eRDPosition == ERDPosition.RCRD )
            {
                this.nレーンの中央X座標[ 7 ] = 460; //RC
                this.nレーンの中央X座標[ 9 ] = 514; //RD
            }
            else
            {
                this.nレーンの中央X座標[ 7 ] = 498; //RC
                this.nレーンの中央X座標[ 9 ] = 448; //RD
            }
            #endregion

        }
		//-----------------
		#endregion
	}
}
