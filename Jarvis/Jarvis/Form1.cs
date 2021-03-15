using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;

namespace Jarvis
{
	public partial class Form1 : Form
	{
		SpeechSynthesizer s = new SpeechSynthesizer();
		Choices list = new Choices();
		Boolean wake = true;

		public Form1()
		{
			SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
			

			
			list.Add(new string[] {"hello", "how are you", "what time is it", "what is today", "open amazon prime video", "sleep", "wake up", "restart", "update" });
			Grammar gr = new Grammar(new GrammarBuilder(list));

			try
			{
				rec.RequestRecognizerUpdate();
				rec.LoadGrammar(gr);
				rec.SpeechRecognized += Rec_SpeechRecognized;
				rec.SetInputToDefaultAudioDevice();
				rec.RecognizeAsync(RecognizeMode.Multiple);
			}
			catch 
			{
				return;
			}


			InitializeComponent();
		}


		public void Say(String h) 
		{
			s.Speak(h);
		}
		private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			String r = e.Result.Text;
			if (r == "wake up")
			{
				wake = true;
				Say("Up and running sir");
			}
			if (r == "sleep")
			{
				wake = false;
				Say("Alright. Wake me up when you need me");
			}

			//COMMANDS	
			if (wake == true)
			{
				if (r == "hello")
					Say("Hi");

				if (r == "how are you")
					Say("Perfect as usual. What about you?");

				if (r == "what time is it")
					Say(DateTime.Now.ToString("h:mm tt"));

				if (r == "what is today")
					Say(DateTime.Now.ToString("M/d/yyyy"));

				if (r == "open amazon prime video")
					Process.Start("https://www.primevideo.com/");
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
