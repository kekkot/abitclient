using AbiturientClient.Core;
using AbiturientClient.MVVM.View;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using RestSharp;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;
using AbiturientClient.JsonModels;
using System.Linq;

namespace AbiturientClient.MVVM.ViewModel
{
    class Authorisation: INotifyPropertyChanged
    {
        private string _login;

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
            get { return ((Application.Current.MainWindow.FindName("tbAuthPass")) as PasswordBox).Password; }
        }

        private RelayCommand _sendAuthorisationRequest;
        public RelayCommand SendAuthorisationRequest //вход в основную программу
        {
            get
            {

                return _sendAuthorisationRequest ??
                    (_sendAuthorisationRequest = new RelayCommand(async o =>
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
                            else if (Password == "" || Password == null)
                            {
                                validation = false;
                                MessageBox.Show("Не введен пароль");
                                return;
                            }
                        }
                        ServiceFunctions.SetVisible<Image>("animation", true);
                        ServiceFunctions.SetEnabled<Grid>("gridAuth", false);

                        ApiRequest registrationRequest = new ApiRequest(Settings.Host);

                        Dictionary<string, string> args = new Dictionary<string, string>();
                        args.Add("UserName", Login);
                        args.Add("Password", Password);


                        IRestResponse response = await registrationRequest.SendRequestAsync("api/Account/Login", RequestType.Post, args);
                        
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            //MessageBox.Show(response.Content);
                            //MessageBox.Show(response.Cookies.First().Value, response.Cookies.First().Name);
                            Settings.Cookies = response.Cookies;
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            ServiceFunctions.SwitchWindow(new View.Navigator());
                        }
                        else if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            List<string> errors = ErrorParser.GetErrorsList(response.Content);
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            MessageBox.Show(string.Join("\n", errors));
                            ServiceFunctions.SetEnabled<Grid>("gridAuth", true);
                        }
                        else
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            MessageBox.Show("Сервер недоступен");
                            ServiceFunctions.SetEnabled<Grid>("gridAuth", true);
                        }                  
                    }));
            }
        }
        
        private RelayCommand _toRegistrationWindow;
        public RelayCommand ToRegistrationWindow //переход к блоку регистрации
        {
            get
            {               
                return _toRegistrationWindow ??
                    (_toRegistrationWindow = new RelayCommand(o =>
                    {
                        ServiceFunctions.SwitchWindow(new View.Registration());
                    }));
            }
        }
        
        private RelayCommand _toRecoveryWindow;
        public RelayCommand ToRecoveryWindow //выход в окно восстановления
        {
            get
            {
                return _toRecoveryWindow ??
                    (_toRecoveryWindow = new RelayCommand(o =>
                    {
                        ServiceFunctions.SwitchWindow(new View.Recovery());
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
