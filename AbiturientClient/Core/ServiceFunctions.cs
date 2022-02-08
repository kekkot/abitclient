using AbiturientClient.JsonModels;
using AbiturientClient.Core;
using AbiturientClient.MVVM;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AbiturientClient.MVVM.ViewModel;

namespace AbiturientClient.Core
{
    public static class ServiceFunctions
    {
        public static string GetResource(string relUri)
        {
            string filepath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            filepath = Directory.GetParent(filepath).FullName;
            filepath = Directory.GetParent(filepath).FullName;
            filepath = Path.Combine(filepath, "Resources\\" + relUri);
            return filepath;
        }
        public static void SwitchWindow(Window targetWindow)
        {            
            Application.Current.MainWindow.Hide();
            targetWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = targetWindow;
        }
        public static string GetText(string ElementName)
        {
            return (Application.Current.MainWindow.FindName(ElementName) as TextBox).Text;
        }
        public static string FindTextBoxInGrid(Grid grid, string ElementName)
        {
            return (grid.FindName(ElementName) as TextBox).Text;
        }
        public static void SetEnabled<T>(string target, bool status) where T: UIElement
        {
            (Application.Current.MainWindow.FindName(target) as T).IsEnabled = status;
        }
        public static void SetVisible<T>(string target, bool status) where T : UIElement
        {
            if (status)
            {
                (Application.Current.MainWindow.FindName(target) as T).Visibility = Visibility.Visible;
            }
            else
            {
                (Application.Current.MainWindow.FindName(target) as T).Visibility = Visibility.Hidden;
            }
        }
        public static async Task<List<T>> GetEntityListAsync<T>(string url)
        {
            ApiRequest apiRequest = new ApiRequest(Settings.Host);
            IRestResponse response = await apiRequest.SendRequestAsync(url, RequestType.Get);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<T>>(response.Content);
            }
            else
            {
                throw new Exception("Сервер не отвечает!");
            }
        }
        public static string ReplaceMaskInTextBox(string MainString)
        {
            return MainString.Replace("_", "");
        }
    }
}
