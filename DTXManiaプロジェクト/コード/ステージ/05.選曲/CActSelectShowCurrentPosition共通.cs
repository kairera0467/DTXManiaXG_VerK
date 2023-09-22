using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CActSelectShowCurrentPosition共通 : CActivity
	{
		// メソッド

		public CActSelectShowCurrentPosition共通()
		{
			base.b活性化してない = true;
		}

		// CActivity 実装

		public override void On活性化()
		{
			if ( this.b活性化してる )
				return;
			base.On活性化();
		}
		public override void On非活性化()
		{
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if ( !base.b活性化してない )
			{
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if ( !base.b活性化してない )
			{
				base.OnManagedリソースの解放();
			}
		}

		// その他

		#region [ private ]
		//-----------------
        private int n決定演出用X;
		//-----------------
		#endregion
	}
}
