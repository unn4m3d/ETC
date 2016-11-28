/*
 * Created by SharpDevelop.
 * User: unn4m3d
 * Date: 27.11.2016
 * Time: 18:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using TeleSharp.TL;
using TeleSharp.TL.Account;
using TLSharp.Core;

namespace ETC
{
	
	public enum AuthState
	{
		PhoneRequest,
		CodeRequest,
		RegistrationRequest,
		PasswordRequest
	}
	
	/// <summary>
	/// Description of LoginForm.
	/// </summary>
	public partial class LoginForm : Form
	{
		internal TelegramClient Client;
		internal AuthState State = AuthState.PhoneRequest;
		internal string CodeHash;
		internal string Number;
		internal Task<TLPassword> PasswordData;
		internal TLPassword PasswordDataResult;
		internal MainForm TheMainForm;
		internal char pchar;
		
		internal void BackCallback(object s, EventArgs e)
		{
			switch(State)
			{
				case AuthState.PasswordRequest:
					State = AuthState.CodeRequest;
					(s as Button).Text = "Back";
					break;
				case AuthState.CodeRequest:
					State = AuthState.PhoneRequest;
					(s as Button).Text = "Quit";
					break;
				case AuthState.RegistrationRequest:
					State = AuthState.PhoneRequest;
					(s as Button).Text = "Quit";
					break;
				case AuthState.PhoneRequest:
					Application.Exit();
					break;
			}
			UpdateText();
		}
		
		internal async void NextCallback(object s, EventArgs e)
		{
			try{
				switch(State)
				{
					case AuthState.PhoneRequest:
						Number = InputText.Text;
						/*if(!Number.StartsWith("+")) 
							Number = "+"+Number;
						/**/
						InfoText.Text = "Wait...";
						InfoText.ForeColor = Color.Black;
						bool completed = false;
						bool reg = false;
						while(!completed){
							try
							{
								Debug.WriteLine("Try block entered");
								reg = await Client.IsPhoneRegisteredAsync(Number);
								completed = true;
							}
							catch(Exception ex)
							{
								reg = true;
								completed = true;
								InfoText.Text = "Exception catched during phone check";
								Debug.WriteLine(ex.GetType().Namespace);
							}
						}
						if(reg)
						{
							CodeHash = await Client.SendCodeRequestAsync(Number);
							State = AuthState.CodeRequest;
						}
						else
						{
							State = AuthState.RegistrationRequest;
							InfoText.Text = "You are not registered! Click Next to register";							
						}
						break;
					case AuthState.CodeRequest:
						var code = InputText.Text;
						if(code.Length != 5)
							throw new Exception("Wrong code");
						
						InfoText.Text = "Wait...";
						InfoText.ForeColor = Color.Black;
						
						try
						{
							Debug.WriteLine("Try block entered");
							var user = await Client.MakeAuthAsync(Number,CodeHash,code);
							this.Hide();
							TheMainForm = new MainForm(Client);
							TheMainForm.Show();
							Debug.WriteLine("Try block escape");
						}
						catch(CloudPasswordNeededException ex)
						{
							Debug.WriteLine("Catch block entered");
							PasswordData = Client.GetPasswordSetting();
							State = AuthState.PasswordRequest;
						}
						break;
					case AuthState.PasswordRequest:
						var _user = await Client.MakeAuthWithPasswordAsync(PasswordDataResult,InputText.Text);
						this.Hide();
						TheMainForm = new MainForm(Client);
						TheMainForm.Show();
						break;
						
					case AuthState.RegistrationRequest:
						MessageBox.Show("Registrations are not supported yet!");
						State = AuthState.PhoneRequest;
						break;
				}
				UpdateText();
			}
			catch(Exception _e)
			{
				InfoText.Text = _e.Message;
				InfoText.ForeColor = Color.Red;
				Debug.WriteLine(_e.Message);
				Debug.WriteLine(_e.GetType().Name);
				Debug.WriteLine(_e.StackTrace);
			}
		}
		
		internal void UpdateText()
		{
			InfoText.ForeColor = Color.Black;
			InputText.PasswordChar = pchar;
			switch(State)
			{
				case AuthState.PhoneRequest:
					InfoText.Text = "Please enter your phone number";
					break;
				case AuthState.CodeRequest:
					InfoText.Text = "Please enter confirmation code";
					break;
				case AuthState.PasswordRequest:
					InfoText.Text = "Retriveing password data...";
					BackButton.Enabled = false;
					NextButton.Enabled = false;
					InputText.PasswordChar = '*';
					var bw = new BackgroundWorker();
					bw.DoWork += (sender,args)=>{PasswordDataResult = PasswordData.Result;};
					bw.RunWorkerCompleted += (sender,args) => {
						BackButton.Enabled = true;
						NextButton.Enabled = true;
						InfoText.Text = "Enter password (Hint : " + PasswordDataResult.hint + ")";
					};
					bw.RunWorkerAsync();
					break;
			}
		}
		
		public LoginForm(TelegramClient client)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Client = client;
			BackButton.Click += BackCallback;
			NextButton.Click += NextCallback;
			pchar = InputText.PasswordChar;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	}
}
