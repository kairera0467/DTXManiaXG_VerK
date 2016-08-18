using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DTXMania
{
	public class CPreviewMagnifier
	{
		#region [ プロパティ(拡大率等の取得) ]
		/// <summary>
		/// 拡大後のwidth
		/// </summary>
		public int width;
		/// <summary>
		/// 拡大後のheight
		/// </summary>
		public int height;
		/// <summary>
		/// 拡大後のX拡大率
		/// </summary>
		public float magX;
		/// <summary>
		/// 拡大後のY拡大率
		/// </summary>
		public float magY;

		/// <summary>
		/// プレビュー画像向けの拡大率か(それとも、演奏画面向けの拡大率か)
		/// </summary>
		public bool bIsPreview;
		#endregion

		#region [ 定数定義 ]
		// 配列の0,1要素はそれぞれ, Preview用, 演奏画面用
		private	int[] WIDTH_VGA_SET		= { 204, 278 };											// VGA版DTXManiaのプレビュー画像width値
		private	int[] HEIGHT_VGA_SET	= { 269, 355 };											// VGA版DTXManiaのプレビュー画像height値
		private	int[] WIDTH_HD_SET		= { 400, 400 };											// HD版DTXManiaのプレビュー画像width値
		private	int[] HEIGHT_HD_SET		= { 400, 600 }; // 600は仮								// HD版DTXManiaのプレビュー画像height値
		private	int[] WIDTH_FHD_LIMIT	= { 320, 320 };											// VGA版/FullHD版どちらのプレビュー画像とみなすかのwidth閾値
		private	int[] HEIGHT_FHD_LIMIT	= { 416, 416 };											// VGA版/FullHD版どちらのプレビュー画像とみなすかのwidth閾値
		private	int[] WIDTH_FHD_SET		= { (int) ( 204 * Scale.X ), (int) ( 278 * Scale.X ) };	// FHD版DTXManiaのプレビュー画像height値
		private	int[] HEIGHT_FHD_SET	= { (int) ( 269 * Scale.Y ), (int) ( 355 * Scale.Y ) };	// FHD版DTXManiaのプレビュー画像height値
		#endregion

		#region [ コンストラクタ ]
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CPreviewMagnifier()
		{
		}
		#endregion

		/// <summary>
		/// 拡大率の取得
		/// </summary>
		/// <param name="width_org">元の幅</param>
		/// <param name="height_org">元の高さ</param>
		/// <param name="magX_org">元の拡大率(幅)</param>
		/// <param name="magY_org">元の拡大率(高さ)</param>
		/// <param name="bIsPreview">選曲画面(preview)用か、演奏画面用か</param>
		/// <remarks>出力はプロパティで得てください。</remarks>
		public void GetMagnifier( int width_org, int height_org, float magX_org, float magY_org, bool bIsPreview)
		{
			this.bIsPreview = bIsPreview;

            // #35820 画像サイズに関係なく、プレビュー領域に合わせる add ikanick 15.12.08
            this.width = width_fhd_set;
            this.height = height_fhd_set;
            this.magX = magX_org * width_vga_set / width_org * Scale.X;
            this.magY = magY_org * height_vga_set / height_org * Scale.Y;
            return;

#if false	// FHD対応の名残
			#region [ HD版DTXManiaのプレビュー画像は特別扱いする ]
			if ( width_org == width_hd_set && height_org == height_hd_set )			// HD版DTXManiaのプレビュー画像は特別扱いする
			{
				this.width = width_org;
				this.height = height_org;
				this.magX = 1.5f * magX_org;
				this.magY = 1.5f * magY_org;
				return;
			}
			#endregion

			#region [ width ]
			if ( width_org <= width_vga_set )										// width <= 204 なら、拡大率だけ変更
			{
				this.width = width_org;
				this.magX = magX_org * Scale.X;
			}
			else if ( width_fhd_limit > width_org && width_org > width_vga_set )			// width >= 320 なら原寸表示
			{
				this.width = width_vga_set;
				this.magX = magX_org * Scale.X;
			}
			else if ( width_org >= width_fhd_limit )
			{
				this.width = (int) ( width_vga_set * Scale.X );
				this.magX = magX_org;	// / width_hd_set;
			}
			else
			{
				this.width = width_org;
				this.magX = magX_org * Scale.X;
			}
			#endregion

			#region [ height ]
			if ( height_org <= height_vga_set )											// height <= 269 なら、拡大率だけ変更
			{
				this.height = height_org;
				this.magY = magY_org * Scale.Y;
			}
			else if ( height_fhd_limit > height_org && height_org > height_vga_set )		// height >= 416 なら原寸表示
			{
				this.height = height_vga_set;
				this.magY = magY_org * Scale.Y;
			}
			else if ( width_org >= height_fhd_limit )
			{
				this.height = (int) ( height_vga_set * Scale.Y );
				this.magY = magY_org; // / height_fhd_set;
			}
			else
			{
				this.height = height_org;
				this.magY = magY_org * Scale.Y;
			}
			#endregion
#endif
		}

		#region [ bIsPreviewによる配列→定数読み替え ]
		private int width_vga_set
		{
			get
			{
				return bIsPreview? WIDTH_VGA_SET[ 0 ] : WIDTH_VGA_SET[ 1 ];
			}
		}
		private int height_vga_set
		{
			get
			{
				return bIsPreview? HEIGHT_VGA_SET[ 0 ] : HEIGHT_VGA_SET[ 1 ];
			}
		}
		private int width_hd_set
		{
			get
			{
				return bIsPreview? WIDTH_HD_SET[ 0 ] : WIDTH_HD_SET[1];
			}
		}
		private int height_hd_set
		{
			get
			{
				return bIsPreview? HEIGHT_HD_SET[ 0 ] : HEIGHT_HD_SET[ 1 ];
			}
		}
		private int width_fhd_limit
		{
			get
			{
				return  bIsPreview? WIDTH_FHD_LIMIT[ 0 ] : WIDTH_FHD_LIMIT[ 1 ];
			}
		}
		private int height_fhd_limit
		{
			get
			{
				return bIsPreview? HEIGHT_FHD_LIMIT[ 0 ] : HEIGHT_FHD_LIMIT[ 1 ];
			}
		}
		private int width_fhd_set
		{
			get
			{
				return bIsPreview ? WIDTH_FHD_SET[ 0 ] : WIDTH_FHD_SET[ 1 ];
			}
		}
		private int height_fhd_set
		{
			get
			{
				return bIsPreview ? HEIGHT_FHD_SET[ 0 ] : HEIGHT_FHD_SET[ 1 ];
			}
		}
		#endregion

	}
}
