using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace MidiInChecker2
{
	public partial class Form_Main : Form
	{
		CInputManager InputManager;
		System.Threading.Timer timer;
		object lockobj = new object();

		public Form_Main()
		{
			InitializeComponent();
			InputManager = new CInputManager();

			LogTextBox.AppendText( "Number of MIDI devices: " + InputManager.nInputMidiDevices + "\r\n" );
			foreach ( string s in InputManager.listStrMidiDevices )
			{
				LogTextBox.AppendText( s + "\r\n" );
			}
			LogTextBox.AppendText( "\r\nHit any MIDI Pad to show the signal info.\r\n\r\n\n" );

			#region [タイマーで0.1秒ごとにログ画面を更新するように初期化する]
			TimerCallback timerDelegate = new TimerCallback(mainloop);
			timer = new System.Threading.Timer(timerDelegate, null , 0, 100);
			#endregion
		}


		private void mainloop( object o )
		{
			lock ( lockobj )
			{
				InputManager.tポーリング( true, true );
				List<STInputEvent> list = new List<STInputEvent>();

				// すべての入力デバイスについて…
				foreach ( IInputDevice device in InputManager.list入力デバイス )
				{
					if ( ( device.list入力イベント != null ) && ( device.list入力イベント.Count != 0 ) && ( device.e入力デバイス種別 == E入力デバイス種別.MidiIn ) )
					{
						foreach ( STInputEvent ev in device.list入力イベント )
						{
							int nMIDIevent = ev.nKey & 0xF0;
							int nNote = ( ev.nKey >> 8 ) & 0xFF;	// note#
							// int nVelo = ( ev.nKey >> 16 ) & 0xFF;	// velocity

							string s = DateTime.Now.ToString( "hh:mm:ss.fff" ) +
									": Device=" + device.ID +
									", MIDIevent=0x" + nMIDIevent.ToString( "X2" ) +
									", Note#=0x" + nNote.ToString( "X2" ) +
									", Velocity=" + ev.nVelocity.ToString( "D3" ) +

									"\r\n";
							Invoke( new AppendTextDelegate( appendLogText ), s );
						}
					}
				}
			}
		}

		delegate void AppendTextDelegate( string text );
		private void appendLogText( string text )
		{
			LogTextBox.AppendText( text );
		}

		// ダサい。後日改善予定。
		private void Form_Main_FormClosing( object sender, FormClosingEventArgs e )
		{
			// mainloop処理中なら、待つ
			lock ( lockobj )
			{ };

			// mainloop処理中でないことを確認して、(mainloopを呼び出している)timerを止める
			timer.Dispose();

			// timerを止めてmainloop処理が発生しないことを担保してから、InputMangerを開放する
			InputManager.Dispose();
			InputManager = null;
		}

		private void button1_Click( object sender, EventArgs e )
		{
			Application.Exit();
		}

		private void exitToolStripMenuItem1_Click( object sender, EventArgs e )
		{
			Application.Exit();
		}

		/// <summary>
		/// textboxで、Ctrl-Aでの全選択ができるようにする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogTextBox_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == System.Windows.Forms.Keys.A & e.Control == true )
			{
				LogTextBox.SelectAll();
			} 
		}
	}
}
