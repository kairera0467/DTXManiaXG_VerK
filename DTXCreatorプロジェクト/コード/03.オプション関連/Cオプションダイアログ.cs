using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DTXCreator.オプション関連
{
	public partial class Cオプションダイアログ : Form
	{
		public bool bレーンリストの内訳が生成済みである
		{
			get; private set;
		}

		public Cオプションダイアログ()
		{
			bレーンリストの内訳が生成済みである = false;
			InitializeComponent();
		}

		public void tレーンリストの内訳を生成する( List<DTXCreator.譜面.Cレーン> listCLane )
		{
			DTXCreator.譜面.Cレーン.ELaneType eLastLaneType = DTXCreator.譜面.Cレーン.ELaneType.END;

			this.checkedListBoxLaneSelectList.BeginUpdate();
			foreach ( DTXCreator.譜面.Cレーン c in listCLane)
			{
				if ( eLastLaneType != c.eLaneType )
				{
					eLastLaneType = c.eLaneType;
					this.checkedListBoxLaneSelectList.Items.Add( eLastLaneType.ToString(), c.bIsVisible );
				}
			}
			this.checkedListBoxLaneSelectList.EndUpdate();
			bレーンリストの内訳が生成済みである = true;
		}

		public int tASIOデバイスリストの内訳を生成する()
		{
			this.comboBox_ASIOdevices.Items.Clear();
			string[] asiodevs = FDK.CEnumerateAllAsioDevices.GetAllASIODevices();
			this.comboBox_ASIOdevices.Items.AddRange( asiodevs );

			return asiodevs.Length;
		}

		private void Cオプションダイアログ_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Return )
			{
				this.buttonOK.PerformClick();
			}
			else if ( e.KeyCode == Keys.Escape )
			{
				this.button1.PerformClick();
			}
		}

		private void tabControlオプション_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Escape )
			{
				this.button1.PerformClick();
			}
		}

		private void radioButton_UseDTXViewer_CheckedChanged( object sender, EventArgs e )
		{
			this.radioButton_DirectSound.Enabled = false;
			this.radioButton_WASAPI.Enabled = false;
			this.radioButton_ASIO.Enabled = false;
			this.comboBox_ASIOdevices.Enabled = false;
			this.groupBox_SoundDeviceSettings.Enabled = false;
		}

		private void radioButton_UseDTXManiaGR_CheckedChanged( object sender, EventArgs e )
		{
			this.radioButton_DirectSound.Enabled = true;
			this.radioButton_WASAPI.Enabled = true;
			this.radioButton_ASIO.Enabled = true;
			this.comboBox_ASIOdevices.Enabled = true;
			this.groupBox_SoundDeviceSettings.Enabled = true;
		}

		private void radioButton_DirectSound_CheckedChanged( object sender, EventArgs e )
		{
			this.comboBox_ASIOdevices.Enabled = false;
		}

		private void radioButton_WASAPI_CheckedChanged( object sender, EventArgs e )
		{
			this.comboBox_ASIOdevices.Enabled = false;
		}

		private void radioButton_ASIO_CheckedChanged( object sender, EventArgs e )
		{
			this.comboBox_ASIOdevices.Enabled = true;
		}

		private void radioButtonSelectMode_CheckedChanged( object sender, EventArgs e )
		{

		}

		private void radioButtonEditMove_CheckedChanged( object sender, EventArgs e )
		{

		}
	}
}
