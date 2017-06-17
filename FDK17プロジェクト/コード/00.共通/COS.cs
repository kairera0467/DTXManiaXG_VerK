using System;
using System.Collections.Generic;
using System.Text;

namespace FDK
{
	public static class COS
	{
		/// <summary>
		/// OSがXP以前ならfalse, Vista以降ならtrueを返す
		/// </summary>
		/// <returns></returns>
		public static bool bIsVistaOrLater
		{
			get
			{
				return bCheckOSVersion(6, 0);
			}
		}
		/// <summary>
		/// OSがVista以前ならfalse, Win7以降ならtrueを返す
		/// </summary>
		/// <returns></returns>
		public static bool bIsWin7OrLater
		{
			get
			{
				return bCheckOSVersion(6, 1);
			}
		}
		/// <summary>
		/// OSがWin7以前ならfalse, Win8以降ならtrueを返す
		/// </summary>
		/// <returns></returns>
		public static bool bIsWin8OrLater
		{
			get
			{
				return bCheckOSVersion(6, 2);
			}
		}
		/// <summary>
		/// OSがWin8.1以前ならfalse, Win10以降ならtrueを返す
		/// </summary>
		/// <returns></returns>
		public static bool bIsWin10OrLater
		{
			get
			{
				return bCheckOSVersion(10, 0);
			}
		}


		/// <summary>
		/// 指定のOSバージョン以上であればtrueを返す
		/// </summary>
		private static bool bCheckOSVersion(int major, int minor)
		{
			//プラットフォームの取得
			System.OperatingSystem os = System.Environment.OSVersion;
			if (os.Platform != PlatformID.Win32NT)      // NT系でなければ、XP以前か、PC Windows系以外のOS。
			{
				return false;
			}

			if (os.Version.Major >= major && os.Version.Minor >= minor)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
