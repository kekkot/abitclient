using System.Windows;
using AbiturientClient.Core;
using AbiturientClient.MVVM.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using RestSharp;
using System.Net;
using System.Windows.Controls;
using AbiturientClient.JsonModels;

namespace AbiturientClient.MVVM.ViewModel
{
    class Recovery : INotifyPropertyChanged
    {
        public string Email { get; set; } // электронная почта, которую вводит пользователь       
 
        private RelayCommand _sendRecoveryRequest;
        public RelayCommand SendRecoveryRequest //команда нажатия на кнопку посылки кода доступа на почту для меню восстановления
        {
            get
            {
                return _sendRecoveryRequest ??
                    (_sendRecoveryRequest = new RelayCommand(async o =>
                    {
                        if (Email == null)
                        {
                            MessageBox.Show("Значение почты, не должно быть пустым");
                            return;
                        }
                        ServiceFunctions.SetVisible<Image>("animation", true);

                        ServiceFunctions.SetEnabled<Grid>("gridRecovery",false);

                        ApiRequest recoveryRequest = new ApiRequest(Settings.Host);

                        Dictionary<string, string> args = new Dictionary<string, string>();
                        args.Add("Email", Email);
                        IRestResponse response = await recoveryRequest.SendRequestAsync("api/Account/ForgotPassword", RequestType.Post, args);

                        if(response.StatusCode == HttpStatusCode.OK)
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            MessageBox.Show(response.Content);
                            ServiceFunctions.SwitchWindow(new View.Authorisation());
                        }
                        else
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            List<string> errors = ErrorParser.GetErrorsList(response.Content);
                            MessageBox.Show(string.Join("\n", errors));
                            ServiceFunctions.SetEnabled<Grid>("gridRecovery", true);
                        }
                    }));
            }
        }

        

        private RelayCommand _toAuthorisationWindow;
        public RelayCommand ToAuthorisationWindow //команда нажатия на кнопку перехода к регистрации
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
