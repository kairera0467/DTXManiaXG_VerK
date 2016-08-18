using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CActSelectPreimageパネル : CActivity
	{
		// メソッド

		public CActSelectPreimageパネル()
		{
			base.b活性化してない = true;
		}
		public void t選択曲が変更された()
		{
			this.b新しいプレビューファイルを読み込んだ = false;
		}

		// CActivity 実装

		public override void On活性化()
		{
			base.On活性化();
		}
		public override void On非活性化()
		{
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
					base.b初めての進行描画 = false;
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private bool b新しいプレビューファイルを読み込んだ;
		private bool b新しいプレビューファイルをまだ読み込んでいない
		{
			get
			{
				return !this.b新しいプレビューファイルを読み込んだ;
			}
			set
			{
				this.b新しいプレビューファイルを読み込んだ = !value;
			}
		}
		private void tプレビュー画像_動画の変更()
		{
		}
		private bool tプレビュー画像の指定があれば構築する()
		{
			return true;
		}
		private bool tプレビュー動画の指定があれば構築する()
		{
			return false;
		}
		private bool t背景画像があればその一部からプレビュー画像を構築する()
		{

			return true;
		}
		private void t描画処理_ジャンル文字列()
		{
		}
		private void t描画処理_センサ光()
		{
		}
		private void t描画処理_センサ本体()
		{
		}
		private void t描画処理_パネル本体()
		{
		}
		private unsafe void t描画処理_プレビュー画像()
		{
		}
		//-----------------
		#endregion
	}
}
