using System;

namespace DTXMania
{
	/// <summary>
	/// VGA座標系をFullHD座標系に変換するための、当座のしのぎ。
	/// </summary>
	public struct Scale
	{
		public const float X = SampleFramework.GameWindowSize.Width / 640f;
		public const float Y = SampleFramework.GameWindowSize.Height / 480f;
	}
}
