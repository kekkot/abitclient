using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows;
using System.Net.Mail;
using System.Net;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using AbiturientClient.Core;
using AbiturientClient.MVVM.View;
using AbiturientClient.MVVM.ViewModel;
using System.Windows.Data;
using System.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using AbiturientClient.JsonModels;
using System.Diagnostics;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using System.Windows.Markup;
using Xceed.Wpf.Toolkit;
using System.Threading.Tasks;
using MessageBox = System.Windows.MessageBox;

namespace AbiturientClient.MVVM.ViewModel
{

    delegate void AddChildren(int number, StackPanel stackPanel);
    class CreateCampaignSpecialtyPage : ObservableObject
    {
        private Page pg;
        private int panelHeight = 430;
        private void OnPreviewMouseDown(object sender, EventArgs e)
        {
            (sender as TextBox).Text = null;
            (sender as TextBox).PreviewMouseDown -= OnPreviewMouseDown;
        }
        private void GotFocus(object sender, EventArgs e)
        {
            (sender as TextBox).Text = null;
            (sender as TextBox).CaretIndex = 0;
            (sender as TextBox).GotFocus -= GotFocus;
        }
        private void GotMouseCapture(object sender, EventArgs e)
        {
            (sender as TextBox).CaretIndex = 0;
            //(sender as TextBox).GotMouseCapture -= GotMouseCapture;
        }
        private string nameTextBoxId = "nameTextBox";
        private string codeTextBoxId = "codeTextBox";
        private string cipherTextBoxId = "cipherTextBox";
        private string departmentComboBoxId = "departmentComboBox";
        private string budgetTextBoxId = "budgetTextBox";
        private string commerceTextBoxId = "commerceTextBox";
        private string levelComboBoxId = "levelComboBox";
        private string educationFormListViewId = "educationFormListView";
        private string contestTypeListViewId = "contestTypeListView";
        private string examComboBoxId = "examComboBox";
        private string examOrCheckBoxId = "examOrCheckBox";
        private string minTextBoxId = "minTextBox";

        private string specialtyGridId = "departmentGrid";
        private string specialtyStackPanelId = "StackPanelSpecialty";

        private int value = 0;//номер текущей плитки
        private List<int> panelsId = new List<int>();//список с номерами всех плиток

        private List<Specialty> specialties = new List<Specialty>();

        private StackPanel _stackPanel;
        private StackPanel StackPanel
        {
            get
            {
                if (_stackPanel == null)
                {
                    _stackPanel = pg.FindName(specialtyStackPanelId) as StackPanel;
                }
                return _stackPanel;
            }
        }

        private RelayCommand _toMainWindow;
        public RelayCommand ToMainWindow //команда нажатия на кнопку входа
        {
            get
            {
                return _toMainWindow ??= new RelayCommand(o =>
                {
                    ServiceFunctions.SwitchWindow(new View.Navigator());
                });
            }
        }

        private RelayCommand _addSpecialty;
        public RelayCommand AddSpecialty //команда нажатия на кнопку добавления
        {
            get
            {
                return _addSpecialty ??
                    (_addSpecialty = new RelayCommand(async o =>
                    {
                        if (pg == null)
                        {
                            pg = (Application.Current.MainWindow.FindName("mainFrame") as Frame).Content as Page;
                        }
                        StackPanel.Height += panelHeight;//изменить
                        Grid panel = CreateGrid(value);
                        panelsId.Add(value);
                        StackPanel.Children.Add(panel);
                        value++;
                    }));
            }
        }

        private RelayCommand _deleteSpecialty;
        public RelayCommand DeleteSpecialty //команда удаления плитки
        {
            get
            {
                return _deleteSpecialty ??
                    (_deleteSpecialty = new RelayCommand(num =>
                    {
                        int n = (int)num;
                        StackPanel.Height -= panelHeight;
                        DeleteGrid(n);
                    }));
            }
        }

        private RelayCommand _orCommand;
        public RelayCommand OrCommand //команда активации/деактивации 2 экзамена
        {
            get
            {
                return _orCommand ??
                    (_orCommand = new RelayCommand(cb =>
                    {
                        ComboBox exam = (ComboBox)cb;
                        if (exam.IsEnabled)
                        {
                            exam.IsEnabled = false;
                        }
                        else
                        {
                            exam.IsEnabled = true;
                        }
                    }));
            }
        }

        private RelayCommand _finishCreate;
        public RelayCommand FinishCreate //команда взятия информации
        {
            get
            {
                return _finishCreate ??
                    (_finishCreate = new RelayCommand(async arg =>
                    {
                        bool validation = true;
                        if (validation)
                        {
                            if (panelsId.Count == 0)
                            {
                                validation = false;
                                MessageBox.Show("Не найдено ни одного направления");
                                return;
                            }
                        }
                        Specialty specialty = new Specialty();
                        for (int i = 0; i < panelsId.Count; i++)
                        {
                            if (validation)
                            {
                                if ((pg.FindName(nameTextBoxId + panelsId[i]) as TextBox).Text == "")
                                {
                                    validation = false;
                                    MessageBox.Show("Не введен имя для направления№" + (panelsId[i] + 1));
                                    return;
                                }
                                else if ((pg.FindName(codeTextBoxId + panelsId[i]) as TextBox).Text == "")
                                {
                                    validation = false;
                                    MessageBox.Show("Не введен код для направления№" + (panelsId[i] + 1));
                                    return;
                                }
                                else if ((pg.FindName(cipherTextBoxId + panelsId[i]) as TextBox).Text.Contains("_"))
                                {
                                    validation = false;
                                    MessageBox.Show("Не введен шифр для направления№" + (panelsId[i] + 1));
                                    return;
                                }
                                else if ((pg.FindName(budgetTextBoxId + panelsId[i]) as TextBox).Text == "___")
                                {
                                    validation = false;
                                    MessageBox.Show("Не введено количество бюджетных мест для направления№" + (panelsId[i] + 1));
                                    return;
                                }
                                else if ((pg.FindName(commerceTextBoxId + panelsId[i]) as TextBox).Text == "___")
                                {
                                    validation = false;
                                    MessageBox.Show("Не введено количество мест коммерции для направления№" + (panelsId[i] + 1));
                                    return;
                                }
                                ListView listViewEduc = pg.FindName(educationFormListViewId + panelsId[i]) as ListView;
                                for (int j = 0; j < listViewEduc.Items.Count; j++)
                                {
                                    CheckBox checkBox = listViewEduc.Items[j] as CheckBox;
                                    if (checkBox.IsChecked == true)
                                    {
                                        validation = true;
                                        break;
                                    }
                                    else
                                    {
                                        validation = false;
                                    }
                                }
                                if (validation == false)
                                {
                                    MessageBox.Show("Не выбраны формы обучения для направления№" + (panelsId[i] + 1));
                                    return;
                                }
                                ListView listViewCont = pg.FindName(contestTypeListViewId + panelsId[i]) as ListView;
                                for (int j = 0; j < listViewCont.Items.Count; j++)
                                {
                                    CheckBox checkBox = listViewCont.Items[j] as CheckBox;
                                    if (checkBox.IsChecked == true)
                                    {
                                        validation = true;
                                        break;
                                    }
                                    else
                                    {
                                        validation = false;
                                    }
                                }
                                if (validation == false)
                                {
                                    MessageBox.Show("Не выбраны типы конкурсов для направления№" + (panelsId[i] + 1));
                                    return;
                                }
                                for (int k = 0; k < 3; k++)
                                {

                                    if ((pg.FindName(minTextBoxId + panelsId[i].ToString() + "row" + k.ToString()) as TextBox).Text == "__")
                                    {
                                        validation = false;
                                        MessageBox.Show("Для экзамена/зкзаменов №" + (k + 1) + " поле с минимумом баллов не заполнено для направления№" + (panelsId[i] + 1));
                                        break;
                                    }
                                }
                            }
                        }
                        if (validation)
                        {
                            for (int i = 0; i < panelsId.Count; i++)
                            {
                                Grid panel = pg.FindName(specialtyGridId + panelsId[i]) as Grid;
                                specialty.Name = ServiceFunctions.FindTextBoxInGrid(panel, nameTextBoxId + panelsId[i]);
                                specialty.Code = ServiceFunctions.FindTextBoxInGrid(panel, codeTextBoxId + panelsId[i]);
                                specialty.Cypher = ServiceFunctions.FindTextBoxInGrid(panel, cipherTextBoxId + panelsId[i]);
                                specialty.DepartmentCode = CreateCampaign.Departments.Find(Dep => Dep.Name == ((panel.FindName(departmentComboBoxId + panelsId[i]) as ComboBox).SelectedItem as Label).Content.ToString()).Code;
                                specialty.BudgetPlaces = int.Parse(ServiceFunctions.ReplaceMaskInTextBox(ServiceFunctions.FindTextBoxInGrid(panel, budgetTextBoxId + panelsId[i])));
                                specialty.EducationLevelId = CreateCampaign.EducationLevels.Find(EdLevel => EdLevel.Name == ((panel.FindName(levelComboBoxId + panelsId[i]) as ComboBox).SelectedItem as Label).Content.ToString()).Id;
                                specialty.CommercialPlaces = int.Parse(ServiceFunctions.ReplaceMaskInTextBox(ServiceFunctions.FindTextBoxInGrid(panel, commerceTextBoxId + panelsId[i])));
                                ListView listViewForm = panel.FindName(educationFormListViewId + panelsId[i]) as ListView;
                                for (int j = 0; j < listViewForm.Items.Count; j++)
                                {
                                    CheckBox checkBox = listViewForm.Items[j] as CheckBox;
                                    if (checkBox.IsChecked == true)
                                    {
                                        specialty.SpecialtyFormOfEducationCodes.Add(CreateCampaign.FormsOfEducation.Find(FormEduc => FormEduc.Name == checkBox.Content.ToString()).Code);
                                    }
                                }
                                ListView listViewContest = panel.FindName(contestTypeListViewId + panelsId[i]) as ListView;
                                for (int j = 0; j < listViewContest.Items.Count; j++)
                                {
                                    CheckBox checkBox = listViewContest.Items[j] as CheckBox;
                                    if (checkBox.IsChecked == true)
                                    {
                                        specialty.SpecialtyContestCodes.Add(CreateCampaign.Contests.Find(contest => contest.Name == checkBox.Content.ToString()).Code);
                                    }
                                }
                                for (int k = 0; k < 3; k++)
                                {
                                    CheckBox checkBox = panel.FindName(examOrCheckBoxId + panelsId[i].ToString() + "row" + k.ToString()) as CheckBox;

                                    SpecialtyExam specialtyExam1 = new SpecialtyExam();
                                    specialtyExam1.ExamCode = CreateCampaign.Exams.Find(FormEduc => FormEduc.Name == ((panel.FindName(examComboBoxId + panelsId[i].ToString() + "row" + k.ToString() + "no" + 0) as ComboBox).SelectedItem as Label).Content.ToString()).Code;
                                    specialtyExam1.Priority = k + 1;
                                    specialtyExam1.MinimalScore = int.Parse(ServiceFunctions.ReplaceMaskInTextBox(ServiceFunctions.FindTextBoxInGrid(panel, minTextBoxId + panelsId[i].ToString() + "row" + k.ToString())));

                                    specialty.SpecialtyExams.Add(specialtyExam1);

                                    if (checkBox.IsChecked == true)
                                    {
                                        SpecialtyExam specialtyExam2 = new SpecialtyExam();
                                        specialtyExam2.ExamCode = CreateCampaign.Exams.Find(FormEduc => FormEduc.Name == ((panel.FindName(examComboBoxId + panelsId[i].ToString() + "row" + k.ToString() + "no" + 1) as ComboBox).SelectedItem as Label).Content.ToString()).Code;
                                        specialtyExam2.Priority = k + 1;
                                        specialtyExam2.MinimalScore = int.Parse(ServiceFunctions.ReplaceMaskInTextBox(ServiceFunctions.FindTextBoxInGrid(panel, minTextBoxId + panelsId[i].ToString() + "row" + k.ToString())));

                                        specialty.SpecialtyExams.Add(specialtyExam2);
                                    }
                                }
                                specialties.Add(specialty);
                            }

                            CreateCampaign.Specialties = specialties;
                            InitCampaignContainer container = new InitCampaignContainer(CreateCampaign.Departments, CreateCampaign.Exams, CreateCampaign.Specialties);

                            ApiRequest request = new ApiRequest(Settings.Host);
                            string json = JsonConvert.SerializeObject(container);
                            IRestResponse responce = await request.SendJsonRequestAsync("api/Environment/InitCampaign", RequestType.Post, json, Settings.Cookies);
                            if(responce.StatusCode == HttpStatusCode.OK)
                            {
                                MessageBox.Show(responce.Content);
                                ServiceFunctions.SwitchWindow(new View.Navigator());
                            }
                            else if(responce.StatusCode == HttpStatusCode.BadRequest)
                            {
                                MessageBox.Show(responce.Content);
                            }
                            else
                            {
                                MessageBox.Show("Сервер не отвечает");
                            }
                            }                        
                    }));
            }
        }

        private void DeleteGrid(int number)
        {
            Grid panel = pg.FindName(specialtyGridId + number) as Grid;
            StackPanel.Children.Remove(panel);

            pg.UnregisterName(specialtyGridId + number);
            pg.UnregisterName(cipherTextBoxId + number);
            pg.UnregisterName(codeTextBoxId + number);
            pg.UnregisterName(nameTextBoxId + number);
            pg.UnregisterName(departmentComboBoxId + number);
            pg.UnregisterName(budgetTextBoxId + number);
            pg.UnregisterName(commerceTextBoxId + number);
            pg.UnregisterName(levelComboBoxId + number);
            pg.UnregisterName(contestTypeListViewId + number);
            pg.UnregisterName(educationFormListViewId + number);

            for(int row = 0; row < 3; row++)
            {
                pg.UnregisterName(minTextBoxId + number.ToString() + "row" + row.ToString());
                pg.UnregisterName(examOrCheckBoxId + number.ToString() + "row" + row.ToString());
                for(int no = 0; no < 2; no++)
                {
                    pg.UnregisterName(examComboBoxId + number.ToString() + "row" + row.ToString() + "no" + no.ToString());
                }
            }

            panelsId.Remove(number);
        }
        private Grid CreateGrid(int number)
        {
            string fullId = specialtyGridId + number.ToString();
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Margin = new Thickness(0, 0, 0, 15);
            grid.Height = 425;
            grid.Width = 715;
            grid.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF5F2F2");
            grid.Name = fullId;

            int[] height = { 55, 55, 55, 95, 55, 55, 55 };
            AddChildren[] childrens = { StackPanel1AddChildren, StackPanel2AddChildren, StackPanel3AddChildren, StackPanel4AddChildren};

            for(int i = 0; i < height.Length; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(height[i]);
                grid.RowDefinitions.Add(rd);

                StackPanel stackPanel = GetStackPanel(i, height[i], grid.Width);
                if (i <= 3)
                {
                    childrens[i](number, stackPanel);
                }
                else
                {
                    StackPanel5AddChildren(number, i - 4, stackPanel);
                }

                grid.Children.Add(stackPanel);
            }

            pg.RegisterName(fullId, grid);
            return grid;
        }
        private StackPanel GetStackPanel(int gridRowPosition, double height, double width)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.VerticalAlignment = VerticalAlignment.Center;
            stackPanel.Margin = new Thickness(0, 0, 0, 0);
            stackPanel.Height = height;
            stackPanel.Width = width;
            stackPanel.Orientation = Orientation.Horizontal;
            Grid.SetRow(stackPanel, gridRowPosition);
            return stackPanel;
        }

        private void StackPanel1AddChildren(int number, StackPanel stackPanel)
        {
            TextBox name = GetNameTextBox(number);
            TextBox code = GetCodeTextBox(number);
            MaskedTextBox cipher = GetCipherTextBox(number);
            Button delete = GetDeleteButton(number);
            stackPanel.Children.Add(GetLabel("Название"));
            stackPanel.Children.Add(name);
            stackPanel.Children.Add(GetLabel("Код"));
            stackPanel.Children.Add(code);
            stackPanel.Children.Add(GetLabel("Шифр"));
            stackPanel.Children.Add(cipher);
            stackPanel.Children.Add(delete);
        }

        private void StackPanel2AddChildren(int number, StackPanel stackPanel)
        {
            ComboBox department = GetDepartmentComboBox(number);
            MaskedTextBox budget = GetBudgetTextBox(number);

            stackPanel.Children.Add(GetLabel("Подразделение"));
            stackPanel.Children.Add(department);
            stackPanel.Children.Add(GetLabel("Бюджет"));
            stackPanel.Children.Add(budget);
        }

        private void StackPanel3AddChildren(int number, StackPanel stackPanel)
        {
            ComboBox level = GetLevelComboBox(number);
            MaskedTextBox commerce = GetCommerceTextBox(number);
            stackPanel.Children.Add(GetLabel("Уровень"));
            stackPanel.Children.Add(level);
            stackPanel.Children.Add(GetLabel("Коммерция"));
            stackPanel.Children.Add(commerce);
        }

        private void StackPanel4AddChildren(int number, StackPanel stackPanel)
        {
            ListView educationForm = GetEducationFormListView(number);
            ListView contestType = GetContestTypeListView(number);
            stackPanel.Children.Add(GetLabel("Формы"));
            stackPanel.Children.Add(educationForm);
            stackPanel.Children.Add(GetLabel("Конкурсы"));
            stackPanel.Children.Add(contestType);
        }

        private void StackPanel5AddChildren(int number, int row, StackPanel stackPanel)
        {
            ComboBox exam1 = GetExamComboBox(number, row, 0);
            ComboBox exam2 = GetExamComboBox(number, row, 1);
            CheckBox or = GetOrCheckBox(number, row, exam2);
            MaskedTextBox minExam = GetMinScoreTextBox(number, row);

            stackPanel.Children.Add(GetLabel("Экзамен " + (row + 1).ToString()));
            stackPanel.Children.Add(exam1);
            stackPanel.Children.Add(or);
            stackPanel.Children.Add(GetLabel(" или"));
            stackPanel.Children.Add(exam2);
            stackPanel.Children.Add(GetLabel("минимум"));
            stackPanel.Children.Add(minExam);
        }

        private Label GetLabel(string value)
        {
            Label label = new Label();
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Content = value;
            label.FontSize = 16;
            return label;
        }

        private Button GetDeleteButton(int number)
        {
            Button button = new Button();
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(ServiceFunctions.GetResource("deleteButton.png")));
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Margin = new Thickness(15, 0, 0, 0);
            button.BorderThickness = new Thickness(0, 0, 0, 0);
            button.Padding = new Thickness(0, 0, 0, 0);
            button.Height = 22;
            button.Width = 22;
            button.Command = DeleteSpecialty;
            button.CommandParameter = number;
            button.Background = brush;

            return button;
        }

        private MaskedTextBox GetCipherTextBox(int number)
        {
            string fullId = cipherTextBoxId + number.ToString();
            MaskedTextBox textBox = new MaskedTextBox();
            textBox.Height = 30;
            textBox.Width = 100;
            textBox.Name = fullId;
            textBox.Mask = "00.00.00";
            textBox.GotFocus += GotFocus;
            textBox.GotMouseCapture += GotMouseCapture;
            textBox.Margin = new Thickness(5, 0, 0, 0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }

        private TextBox GetCodeTextBox(int number)
        {
            string fullId = codeTextBoxId + number.ToString();
            TextBox textBox = new TextBox();
            textBox.Height = 30;
            textBox.Width = 100;
            textBox.Name = fullId;
            textBox.GotFocus += GotFocus;
            textBox.GotMouseCapture += GotMouseCapture;
            textBox.Margin = new Thickness(5, 0, 0, 0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }

        private TextBox GetNameTextBox(int number)
        {
            string fullId = nameTextBoxId + number.ToString();
            TextBox textBox = new TextBox();
            textBox.Height = 30;
            textBox.Width = 193;
            textBox.Name = fullId;
            textBox.Margin = new Thickness(0, 0, 5, 0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }

        private ComboBox GetDepartmentComboBox(int number)
        {
            string fullId = departmentComboBoxId + number.ToString();
            ComboBox comboBox = new ComboBox();
            comboBox.VerticalAlignment = VerticalAlignment.Center;
            comboBox.Height = 25;
            comboBox.Width = 310;
            comboBox.MaxHeight = comboBox.Height;
            comboBox.MaxWidth = comboBox.Width;
            comboBox.MinHeight = comboBox.Height;
            comboBox.MinWidth = comboBox.Width;
            comboBox.Name = fullId;
            comboBox.Margin = new Thickness(5, 0, 5, 0);
            comboBox.Height += 200;
            pg.RegisterName(fullId, comboBox);

            if (CreateCampaign.Departments != null)
            {
                for (int i = 0; i < CreateCampaign.Departments.Count; i++)
                {
                    Label label = GetLabel(CreateCampaign.Departments[i].Name);
                    comboBox.Items.Add(label);
                }
                comboBox.SelectedIndex = 0;
            }

            return comboBox;
        }

        private MaskedTextBox GetBudgetTextBox(int number)
        {
            string fullId = budgetTextBoxId + number.ToString();
            MaskedTextBox textBox = new MaskedTextBox();
            textBox.Height = 30;
            textBox.Width = 100;
            textBox.Name = fullId;
            textBox.Mask = "000";
            textBox.CaretIndex = 0;
            textBox.GotFocus += GotFocus;
            textBox.GotMouseCapture += GotMouseCapture;
            textBox.Margin = new Thickness(5, 0, 0, 0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }

        private MaskedTextBox GetCommerceTextBox(int number)
        {
            string fullId = commerceTextBoxId + number.ToString();
            MaskedTextBox textBox = new MaskedTextBox();
            textBox.Height = 30;
            textBox.Width = 95;
            textBox.Name = fullId;
            textBox.Mask = "000";
            textBox.CaretIndex = 0;
            textBox.GotFocus += GotFocus;
            textBox.GotMouseCapture += GotMouseCapture;
            textBox.Margin = new Thickness(5, 0, 0, 0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }

        private ComboBox GetLevelComboBox(int number)
        {
            string fullId = levelComboBoxId + number.ToString();
            ComboBox comboBox = new ComboBox();
            comboBox.VerticalAlignment = VerticalAlignment.Center;
            comboBox.Height = 25;
            comboBox.Width = 345;
            comboBox.MaxHeight = comboBox.Height;
            comboBox.MaxWidth = comboBox.Width;
            comboBox.MinHeight = comboBox.Height;
            comboBox.MinWidth = comboBox.Width;
            comboBox.Name = fullId;
            comboBox.Margin = new Thickness(5, 0, 0, 0);
            comboBox.Height += 200;
            pg.RegisterName(fullId, comboBox);

            for (int i = 0; i < CreateCampaign.EducationLevels.Count; i++)
            {
                Label label = GetLabel(CreateCampaign.EducationLevels[i].Name);
                comboBox.Items.Add(label);
            }
            comboBox.SelectedIndex = 0;

            return comboBox;
        }

        private ListView GetContestTypeListView(int number)
        {
            string fullId = contestTypeListViewId + number.ToString();
            ListView listView = new ListView();
            listView.VerticalAlignment = VerticalAlignment.Center;
            listView.Height = 85;
            listView.Width = 183;
            listView.MaxHeight = listView.Height;
            listView.MaxWidth = listView.Width;
            listView.MinHeight = listView.Height;
            listView.MinWidth = listView.Width;
            listView.Margin = new Thickness(10, 0, 10, 0);
            listView.Name = fullId;
            listView.Height += 200;


            for (int i = 0; i < CreateCampaign.Contests.Count; i++)
            {
                CheckBox cb = GetListViewsCheckBox(CreateCampaign.Contests[i].Name);
                listView.Items.Add(cb);
            }

            pg.RegisterName(fullId, listView);

            return listView;
        }

        private ListView GetEducationFormListView(int number)
        {
            string fullId = educationFormListViewId + number.ToString();
            ListView listView = new ListView();
            listView.VerticalAlignment = VerticalAlignment.Center;
            listView.Height = 85;
            listView.Width = 260;
            listView.MaxHeight = listView.Height;
            listView.MaxWidth = listView.Width;
            listView.MinHeight = listView.Height;
            listView.MinWidth = listView.Width;
            listView.Margin = new Thickness(10, 0, 10, 0);
            listView.Name = fullId;
            listView.Height += 200;


            for (int i = 0; i < CreateCampaign.FormsOfEducation.Count; i++)
            {
                CheckBox cb = GetListViewsCheckBox(CreateCampaign.FormsOfEducation[i].Name);
                listView.Items.Add(cb);
            }

            pg.RegisterName(fullId, listView);

            return listView;
        }

        private CheckBox GetListViewsCheckBox(string contentValue)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Margin = new Thickness(0, 3, 0, 0);
            checkBox.Height = 20;
            checkBox.Tag = contentValue;
            checkBox.Content = contentValue;
            return checkBox;
        }

        private MaskedTextBox GetMinScoreTextBox(int number, int row)
        {
            string fullId = minTextBoxId + number.ToString() + "row" + row.ToString();
            MaskedTextBox textBox = new MaskedTextBox();
            textBox.Height = 30;
            textBox.Width = 63;
            textBox.Name = fullId;
            textBox.Mask = "00";
            textBox.CaretIndex = 0;
            textBox.GotFocus += GotFocus;
            textBox.GotMouseCapture += GotMouseCapture;
            textBox.Margin = new Thickness(5, 0, 0, 0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }

        private ComboBox GetExamComboBox(int number, int row, int n)
        {
            string fullId = examComboBoxId + number.ToString() + "row" + row.ToString() + "no" + n.ToString();
            ComboBox comboBox = new ComboBox();
            comboBox.VerticalAlignment = VerticalAlignment.Center;
            comboBox.Height = 25;
            comboBox.Width = 140;
            comboBox.MaxHeight = comboBox.Height;
            comboBox.MaxWidth = comboBox.Width;
            comboBox.MinHeight = comboBox.Height;
            comboBox.MinWidth = comboBox.Width;
            comboBox.Name = fullId;
            if (n > 0)
            {
                comboBox.IsEnabled = false;
            }
            comboBox.Margin = new Thickness(13, 4, 13, 0);
            pg.RegisterName(fullId, comboBox);

            if (CreateCampaign.Exams != null)
            {
                for (int i = 0; i < CreateCampaign.Exams.Count; i++)
                {
                    Label label = GetLabel(CreateCampaign.Exams[i].Name);
                    comboBox.Items.Add(label);
                }
                comboBox.SelectedIndex = 0;
            }


            comboBox.Height += 200;
            return comboBox;
        }

        private CheckBox GetOrCheckBox(int number, int row, ComboBox exam2)
        {
            string fullId = examOrCheckBoxId + number.ToString() + "row" + row.ToString();
            CheckBox checkBox = new CheckBox();
            checkBox.Name = fullId;
            checkBox.Margin = new Thickness(0, 8, 0, 0);
            checkBox.Height = 20;
            checkBox.Tag = "или";
            checkBox.Command = OrCommand;
            checkBox.CommandParameter = exam2;
            pg.RegisterName(fullId, checkBox);
            return checkBox;
        }
    }
}