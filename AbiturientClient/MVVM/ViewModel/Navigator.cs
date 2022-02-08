using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AbiturientClient.Core;
using AbiturientClient.JsonModels;
using AbiturientClient.MVVM.View;
using RestSharp;

namespace AbiturientClient.MVVM.ViewModel
{
    class Navigator: ObservableObject
    {
        private RelayCommand _toAuthorisationWindow;
        public RelayCommand ToAuthorisationWindow //переход к блоку авторизации
        {
            get
            {
                return _toAuthorisationWindow ??
                    (_toAuthorisationWindow = new RelayCommand(o =>
                    {
                        Settings.Cookies = null;
                        ServiceFunctions.SwitchWindow(new View.Authorisation());
                    }));
            }
        }
        private RelayCommand _toCreateCampaign;
        public RelayCommand ToCreateCampaign //переход к созданию компании
        {
            get
            {
                return _toCreateCampaign ??
                    (_toCreateCampaign = new RelayCommand(async o =>
                    {
                        ServiceFunctions.SetVisible<Image>("animation", true);
                        ServiceFunctions.SetEnabled<Grid>("GridNavigator", false);
                        ApiRequest request = new ApiRequest(Settings.Host);
                        IRestResponse response = await request.SendRequestAsync("api/Environment/IsCampaignFormed", RequestType.Get, Settings.Cookies);
                        bool isFormed;
                        if(response.StatusCode == HttpStatusCode.OK)
                        {
                            isFormed = bool.Parse(response.Content);
                        }
                        else
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            ServiceFunctions.SetEnabled<Grid>("GridNavigator", true);
                            MessageBox.Show("Сервер не отвечает");
                            return;
                        }

                        if(isFormed)
                        {
                            ServiceFunctions.SetVisible<Image>("animation", false);
                            ServiceFunctions.SetEnabled<Grid>("GridNavigator", true);
                            MessageBox.Show("Приемная кампания уже сформирована");
                            return;
                        }

                        if (CreateCampaign.ExamForms == null)
                        {
                            try
                            {
                                CreateCampaign.ExamForms = await ServiceFunctions.GetEntityListAsync<ExamForm>("api/Environment/ExamForm");
                            }
                            catch (Exception e)
                            {
                                ServiceFunctions.SetVisible<Image>("animation", false);
                                ServiceFunctions.SetEnabled<Grid>("GridNavigator", true);
                                MessageBox.Show(e.Message);
                                return;
                            }
                        }

                        if (CreateCampaign.Contests == null)
                        {
                            try
                            {
                                CreateCampaign.Contests = await ServiceFunctions.GetEntityListAsync<Contest>("api/Environment/Contest");
                            }
                            catch (Exception e)
                            {
                                ServiceFunctions.SetVisible<Image>("animation", false);
                                ServiceFunctions.SetEnabled<Grid>("GridNavigator", true);
                                MessageBox.Show(e.Message);
                                return;
                            }
                        }

                        if (CreateCampaign.EducationLevels == null)
                        {
                            try
                            {
                                CreateCampaign.EducationLevels = await ServiceFunctions.GetEntityListAsync<EducationLevel>("api/Environment/EducationLevel");
                            }
                            catch (Exception e)
                            {
                                ServiceFunctions.SetVisible<Image>("animation", false);
                                ServiceFunctions.SetEnabled<Grid>("GridNavigator", true);
                                MessageBox.Show(e.Message);
                                return;
                            }
                        }

                        if (CreateCampaign.FormsOfEducation == null)
                        {
                            try
                            {
                                CreateCampaign.FormsOfEducation = await ServiceFunctions.GetEntityListAsync<FormOfEducation>("api/Environment/FormOfEducation");
                            }
                            catch (Exception e)
                            {
                                ServiceFunctions.SetVisible<Image>("animation", false);
                                ServiceFunctions.SetEnabled<Grid>("GridNavigator", true);
                                MessageBox.Show(e.Message);
                                return;
                            }
                        }
                        ServiceFunctions.SetVisible<Image>("animation", false);
                        ServiceFunctions.SwitchWindow(new View.CreateCampaign());
                    }));
            }
        }
    }
}
