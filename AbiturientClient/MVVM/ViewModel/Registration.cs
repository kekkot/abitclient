using System.Windows;
using System.Net;
using AbiturientClient.Core;
using AbiturientClient.MVVM.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings;
using System.Collections.Generic;
using System.Collections.Specialized;
using RestSharp;
using Newtonsoft.Json;
using System.Windows.Controls;
using AbiturientClient.JsonModels;
using System.Windows.Media;

namespace AbiturientClient.MVVM.ViewModel
{
    class Registration : INotifyPropertyChanged
    {        
        private string _login;
        private string _email;
        private string _phonenumber;
        public string PasswordConfirm
        {
            get { return ((Application.Current.MainWindow.FindName("tbReturnPass")) as PasswordBox).Password; }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return ((Application.Current.MainWindow.FindName("tbPass")) as PasswordBox).Password; }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string PhoneNumber
        {
            get { return _phonenumber; }
            set
            {
                _phonenumber = value;
                OnPropertyChanged();
            }
        }


        private RelayCommand _sendRegistrationRequest;
        public RelayCommand SendRegistrationRequest //отправка кода доступа на почту 
        {

            get
            {
                
                return _sendRegistrationRequest ??
                    (_sendRegistrationRequest = new RelayCommand(async o =>
                    {
                        bool validation = true;
                        if (validation == true)
                        {
                            if (Login == "" || Login == null)
                            {
                                validation = false;
                                MessageBox.Show("Не введено имя пользователя");
                                return;
                            }
                            else if (Email == "" || Email == null)
                            {
                                validation = false;
                                MessageBox.Show("Не введен email");
                                return;
                            }
                            else if (Password == "" || Password == null)
                            {
                                validation = false;
                                MessageBox.Show("Не введен пароль");
                                return;
                            }
                            else if (PasswordConfirm == "" || PasswordConfirm == null)
                            {
                                validation = false;
                                MessageBox.Show("Не введен потверждающий пароль");
                                return;
                            }
                            else if (PhoneNumber == "_-(___)-___-__-__" || PhoneNumber == null)
                            {
                                validation = false;
                                MessageBox.Show("Не введен номер телефона");
                                return;
                            }
                        }
                        ServiceFunctions.SetVisible<Image>("animation", true);

                        ServiceFunctions.SetEnabled<Grid>("gridReg", false);

                        ApiRequest registrationRequest = new ApiRequest(Settings.Host);

                        Dictionary<string, string> args = new Dictionary<string, string>();
                        args.Add("Email", Email);
                        args.Add("Password", Password);
                        args.Add("UserName", Login);
                        args.Add("PhoneNumber", PhoneNumber);
                        args.Add("PasswordConfirmation", PasswordConfirm);


                        IRestResponse response = await registrationRequest.SendRequestAsync("api/Account/Register", RequestType.Post, args);


                        if(response.StatusCode == HttpStatusCode.OK)
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            MessageBox.Show(response.Content);
                            ServiceFunctions.SwitchWindow(new View.Authorisation());
                        }
                        else if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            List<string> errors = ErrorParser.GetErrorsList(response.Content);
                            MessageBox.Show(string.Join("\n", errors));
                            ServiceFunctions.SetEnabled<Grid>("gridReg", true);

                        }
                        else
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            MessageBox.Show("Сервер недоступен");
                            ServiceFunctions.SetEnabled<Grid>("gridReg", true);
                        }
                    }));

            }
        }


        private RelayCommand _toAuthorisationWindow;
        public RelayCommand ToAuthorisationWindow //переход к блоку авторизации
        {
            get
            {
                return _toAuthorisationWindow ??
                    (_toAuthorisationWindow = new RelayCommand(o =>
                    {
                        ServiceFunctions.SwitchWindow(new View.Authorisation());
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
