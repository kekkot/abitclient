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
using System.Windows.Documents;
using AbiturientClient.Core;
using AbiturientClient.MVVM.View;
using System.Windows.Data;
using System.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using AbiturientClient.JsonModels;
using System.Diagnostics;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace AbiturientClient.MVVM.ViewModel
{
    class CreateCampaignExamPage : ObservableObject
    {
        Page pg;
        private int panelHeight = 109;
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
        private string orderLabelId = "orderLabel";
        private string nameTextBoxId = "nameTextBox";
        private string codeTextBoxId = "codeTextBox";
        private string examFormListViewId = "examFormListView";
        private string examFormCheckBoxId = "examFormCheckBox";
        private string examGridId = "examGrid";
        private string examStackPanelId = "StackPanelExam";

        private List<Exam> exams = new List<Exam>();


        private int value = 0;//номер текущей плитки
        private List<int> panelsId = new List<int>();//список с номерами всех плиток

        //private List<ExamForm> _examForm;


        private StackPanel _stackPanel;
        private StackPanel StackPanel
        {
            get
            {
                if (_stackPanel == null)
                {
                    _stackPanel = pg.FindName(examStackPanelId) as StackPanel;
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

        private RelayCommand _addExam;
        public RelayCommand AddExam //команда нажатия на кнопку входа
        {
            get
            {
                return _addExam ??
                    (_addExam = new RelayCommand(o =>
                    {
                        if (pg == null)
                        {
                            pg = (Application.Current.MainWindow.FindName("mainFrame") as Frame).Content as Page;
                        }
                        StackPanel.Height += panelHeight;
                        Grid panel = CreateGrid(value);
                        panelsId.Add(value);
                        StackPanel.Children.Add(panel);
                        value++;
                    }));
            }
        }

        private RelayCommand _deleteExam;
        public RelayCommand DeleteExam //команда удаления плитки
        {
            get
            {
                return _deleteExam ??
                    (_deleteExam = new RelayCommand(num =>
                    {
                        int n = (int)num;
                        StackPanel.Height -= panelHeight;
                        DeleteGrid(n);
                    }));
            }
        }
        private RelayCommand _test;
        public RelayCommand Test //команда удаления плитки
        {
            get
            {
                return _test ??
                    (_test = new RelayCommand(arg =>
                    {
                        bool validation = true;
                        if (validation)
                        {
                            if (panelsId.Count == 0)
                            {
                                validation = false;
                                MessageBox.Show("Не найдено ни одного предмета");
                                return;
                            }
                        }
                        if (validation)
                        {
                            for (int i = 0; i < panelsId.Count; i++)
                            {
                                if ((pg.FindName(nameTextBoxId + panelsId[i]) as TextBox).Text == "" || (pg.FindName(nameTextBoxId + panelsId[i]) as TextBox).Text == "Название")
                                {
                                    validation = false;
                                    MessageBox.Show("Не введено значение названия для предмета№" + (panelsId[i] + 1));
                                    return;
                                }
                                else if ((pg.FindName(codeTextBoxId + panelsId[i]) as TextBox).Text.Contains("_"))
                                {
                                    validation = false;
                                    MessageBox.Show("Не введено значение кода для предмета№" + (panelsId[i] + 1));
                                    return;
                                }
                                ListView ExamFormList = pg.FindName(examFormListViewId + panelsId[i]) as ListView;
                                for (int j = 0; j < ExamFormList.Items.Count; j++)
                                {
                                    CheckBox cb = (CheckBox)ExamFormList.Items[j];
                                    if (cb.IsChecked == true)
                                    {
                                        validation = true;
                                        break;
                                    }
                                    else
                                    {
                                        validation = false;
                                    }
                                }
                                if (!validation)
                                {
                                    MessageBox.Show("Не выбрана форма экзамена в предмете№" + (panelsId[i] + 1));
                                    return;
                                }
                            }
                        }
                        if (validation)
                        {
                            for (int i = 0; i < panelsId.Count; i++)
                            {
                                TextBox Name = pg.FindName(nameTextBoxId + panelsId[i]) as TextBox;
                                TextBox Code = pg.FindName(codeTextBoxId + panelsId[i]) as TextBox;
                                ListView ExamFormList = pg.FindName(examFormListViewId + panelsId[i]) as ListView;

                                Exam exam = new Exam();
                                exam.Name = Name.Text;
                                exam.Code = int.Parse(Code.Text);

                                for (int j = 0; j < ExamFormList.Items.Count; j++)
                                {
                                    CheckBox cb = (CheckBox)ExamFormList.Items[j];
                                    if ((bool)cb.IsChecked)
                                    {
                                        exam.ExamFormIds.Add(CreateCampaign.ExamForms[j].Id);
                                    }
                                }
                                exams.Add(exam);
                            }
                            CreateCampaign.Exams = exams;
                            (pg.FindName("HyperLinkExam") as Hyperlink).DoClick();
                        }
                    }));
            }
        }

        private RelayCommand _switchGridOnDepart;
        public RelayCommand SwitchGridOnDepart //переключение на окно создания предметов
        {
            get
            {
                return _switchGridOnDepart ??
                    (_switchGridOnDepart = new RelayCommand(myobject =>
                    {
                        return;
                    }));
            }
        }


        private void DeleteGrid(int number)
        {
            Grid panel = pg.FindName(examGridId + number) as Grid;
            StackPanel.Children.Remove(panel);
            pg.UnregisterName(orderLabelId + number.ToString());
            pg.UnregisterName(nameTextBoxId + number.ToString());
            pg.UnregisterName(codeTextBoxId + number.ToString());
            pg.UnregisterName(examGridId + number.ToString());
            pg.UnregisterName(examFormListViewId + number.ToString());
            for(int i = 0; i < CreateCampaign.ExamForms.Count; i++)
            {
                pg.UnregisterName($"{examFormListViewId}{number}{examFormCheckBoxId}{i}");
            }
            panelsId.Remove(number);
        }

        private Grid CreateGrid(int number)
        {
            string fullId = examGridId + number.ToString();
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Height = 105;
            grid.Width = 600;
            grid.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF5F2F2");
            grid.Name = fullId;
            grid.Margin = new Thickness(0,0,0,4);
            pg.RegisterName(fullId, grid);

            Label orderLabel = GetOrderLabel(number);
            TextBox nameTextBox = GetNameTextBox(number);
            MaskedTextBox codeTextBox = GetCodeTextBox(number);
            Button deleteButton = GetDeleteButton(number);
            ListBox examFormListBox = GetExamFormListView(number);

            grid.Children.Add(orderLabel);
            grid.Children.Add(nameTextBox);
            grid.Children.Add(codeTextBox);
            grid.Children.Add(examFormListBox);
            grid.Children.Add(deleteButton);

            return grid;
        }

        private Button GetDeleteButton(int number)
        {
            Button button = new Button();
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(ServiceFunctions.GetResource("deleteButton.png")));
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Margin = new Thickness(555, 10, 0, 10);
            button.BorderThickness = new Thickness(0, 0, 0, 0);
            button.Padding = new Thickness(0, 0, 0, 0);
            button.Height = 22;
            button.Width = 22;
            button.Command = DeleteExam;
            button.CommandParameter = number;
            button.Background = brush;

            return button;
        }
        
        private MaskedTextBox GetCodeTextBox(int number)
        {
            string fullId = codeTextBoxId + number.ToString();
            MaskedTextBox textBox = new MaskedTextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Left;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.Margin = new Thickness(220, 10, 0, 10);
            textBox.Height = 30;
            textBox.Width = 150;
            textBox.PreviewMouseDown += OnPreviewMouseDown;
            textBox.GotFocus += GotFocus;
            textBox.GotMouseCapture += GotMouseCapture;
            textBox.Name = fullId;
            textBox.Mask = "000";
            textBox.CaretIndex = 0;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }        

        private TextBox GetNameTextBox(int number)
        {
            string fullId = nameTextBoxId + number.ToString();
            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.Margin = new Thickness(40, 0, 0, 0);
            textBox.Height = 30;
            textBox.Width = 155;
            textBox.Text = "Название";
            textBox.PreviewMouseDown += OnPreviewMouseDown;
            textBox.GotFocus += GotFocus;
            textBox.Name = fullId;
            pg.RegisterName(fullId, textBox);
            return textBox;
        }

        private Label GetOrderLabel(int number)
        {
            string fullId = orderLabelId + number.ToString();
            Label label = new Label();
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Content = (number + 1).ToString();
            label.Margin = new Thickness(10, 0, 0, 0);
            label.Height = 30;
            label.Width = 50;
            label.Name = fullId;
            label.FontSize = 16;
            pg.RegisterName(fullId, label);
            return label;
        }
        private ListView GetExamFormListView(int number)
        {
            string fullId = examFormListViewId + number.ToString();
            ListView listBox = new ListView();
            listBox.HorizontalAlignment = HorizontalAlignment.Left;
            listBox.VerticalAlignment = VerticalAlignment.Center;
            listBox.HorizontalContentAlignment = HorizontalAlignment.Left;
            listBox.VerticalContentAlignment = VerticalAlignment.Center;
            listBox.Margin = new Thickness(390, 10, 0, 10);
            listBox.Height = 85;
            listBox.Width = 150;
            listBox.MaxWidth = 150;
            listBox.MaxHeight = 85;
            listBox.MinWidth = 150;
            listBox.MinHeight = 85;
            listBox.Name = fullId;
            pg.RegisterName(fullId, listBox);
            for(int i = 0; i < CreateCampaign.ExamForms.Count; i++)
            {
                CheckBox cb = GetExamFormCheckBox(number, i, CreateCampaign.ExamForms[i].Name);
                listBox.Items.Add(cb);
            }
            listBox.Height += 200;
            return listBox;
        }
        private CheckBox GetExamFormCheckBox(int numberListView, int numberCheckBox, string contentValue)
        {
            string fullId = $"{examFormListViewId}{numberListView}{examFormCheckBoxId}{numberCheckBox}";
            Debug.WriteLine(fullId);
            CheckBox checkBox = new CheckBox();
            checkBox.HorizontalAlignment = HorizontalAlignment.Left;
            checkBox.VerticalAlignment = VerticalAlignment.Center;
            checkBox.Margin = new Thickness(0, 3, 0, 0);
            checkBox.Height = 20;
            checkBox.Width = 130;
            checkBox.Name = fullId;
            checkBox.Tag = contentValue;
            checkBox.Content = contentValue;
            pg.RegisterName(fullId, checkBox);
            return checkBox;
        }
    }
}
