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
using System.Windows.Data;
using System.Windows.Documents;
using System.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using AbiturientClient.JsonModels;
using System.Diagnostics;
using System.IO;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace AbiturientClient.MVVM.ViewModel
{
    class CreateCampaignDepartmentPage : ObservableObject
    {
        private Page pg;
        private int panelHeight = 54;
        private void OnPreviewMouseDown(object sender, EventArgs e)
        {
            (sender as TextBox).Text = null;
            (sender as TextBox).PreviewMouseDown -= OnPreviewMouseDown;
        }
        private void GotFocus(object sender, EventArgs e)
        {
            (sender as TextBox).Text = null;
            (sender as TextBox).GotFocus -= GotFocus;
        }

        private string orderLabelId = "orderLabel";
        private string nameTextBoxId = "nameTextBox";
        private string codeTextBoxId = "codeTextBox";
        private string departmentGridId = "departmentGrid";
        private string departmentStackPanelId = "StackPanelDepartment";

        private List<Department> departments = new List<Department>();

        private int value = 0;//номер текущей плитки
        private List<int> panelsId = new List<int>();//список с номерами всех плиток

        private StackPanel _stackPanel;
        private StackPanel StackPanel
        {
            get
            {
                if(_stackPanel == null)
                {
                    _stackPanel = pg.FindName(departmentStackPanelId) as StackPanel;
                }
                return _stackPanel;
            }
        }


        private RelayCommand _toMainWindow;
        public RelayCommand ToMainWindow //команда нажатия на кнопку входа
        {
            get
            {
                return _toMainWindow ??
                    (_toMainWindow = new RelayCommand(o =>
                    {
                        ServiceFunctions.SwitchWindow(new View.Navigator());
                        return;
                    }));
            }
        }

        private RelayCommand _addDepartament;
        public RelayCommand AddDepartament //команда нажатия на кнопку входа
        {
            get
            {
                return _addDepartament ??
                    (_addDepartament = new RelayCommand(o =>
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

        private RelayCommand _deleteDepartament;
        public RelayCommand DeleteDepartament //команда удаления плитки
        {
            get
            {
                return _deleteDepartament ??
                    (_deleteDepartament = new RelayCommand(num =>
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
                                MessageBox.Show("Не найдено ни одного структурного подразделения");
                                return;
                            }
                        }
                        for (int i = 0; i < panelsId.Count; i++)
                        {
                            if ((pg.FindName(nameTextBoxId + panelsId[i]) as TextBox).Text == "" || (pg.FindName(nameTextBoxId + panelsId[i]) as TextBox).Text == "Название")
                            {
                                validation = false;
                                MessageBox.Show("Не введено имя для структурного подразделения№" + (panelsId[i] + 1));
                                return;
                            }
                            if ((pg.FindName(codeTextBoxId + panelsId[i]) as TextBox).Text == "" || (pg.FindName(codeTextBoxId + panelsId[i]) as TextBox).Text == "Код")
                            {
                                validation = false;
                                MessageBox.Show("Не введен код для структурного подразделения№" + (panelsId[i] + 1));
                                return;
                            }
                        }
                        if (validation)
                        {
                            for (int i = 0; i < panelsId.Count; i++)
                            {
                                TextBox Name = pg.FindName(nameTextBoxId + panelsId[i]) as TextBox;
                                TextBox Code = pg.FindName(codeTextBoxId + panelsId[i]) as TextBox;
                                Label Order = pg.FindName(orderLabelId + panelsId[i]) as Label;
                                departments.Add(new Department(Code.Text, Name.Text, float.Parse(Order.Content.ToString())));
                            }
                            CreateCampaign.Departments = departments;
                            (pg.FindName("HyperLinkDepartment") as Hyperlink).DoClick();
                        }
                    }));
            }
        }

        private void DeleteGrid(int number)
        {
            Grid panel = pg.FindName(departmentGridId + number) as Grid;
            StackPanel.Children.Remove(panel);
            pg.UnregisterName(nameTextBoxId + number.ToString());
            pg.UnregisterName(codeTextBoxId + number.ToString());
            pg.UnregisterName(orderLabelId + number.ToString());
            pg.UnregisterName(departmentGridId + number.ToString());
            panelsId.Remove(number);
        }

        private Grid CreateGrid(int number)
        {
            string fullId = departmentGridId + number.ToString();
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Height = 50;
            grid.Width = 600;
            grid.Margin = new Thickness(0,0,0,4);
            grid.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF5F2F2");
            grid.Name = fullId;
            pg.RegisterName(fullId, grid);

            Label orderLabel = GetOrderLabel(number);
            TextBox nameTextBox = GetNameTextBox(number);
            TextBox codeTextBox = GetCodeTextBox(number);
            Button deleteButton = GetDeleteButton(number);

            grid.Children.Add(orderLabel);
            grid.Children.Add(nameTextBox);
            grid.Children.Add(codeTextBox);
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
            button.Margin = new Thickness(563, 10, 0, 10);
            button.BorderThickness = new Thickness(0, 0, 0, 0);
            button.Padding = new Thickness(0, 0, 0, 0);
            button.Height = 22;
            button.Width = 22;
            button.Command = DeleteDepartament;
            button.CommandParameter = number;
            button.Background = brush;

            return button;
        }

        private TextBox GetCodeTextBox(int number)
        {
            string fullId = codeTextBoxId + number.ToString();
            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Left;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.Margin = new Thickness(451, 10, 0, 10);
            textBox.Height = 26;
            textBox.Width = 100;
            textBox.Text = "Код";
            textBox.PreviewMouseDown += OnPreviewMouseDown;
            textBox.GotFocus += GotFocus;
            textBox.Name = fullId;
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
            textBox.Margin = new Thickness(40, 10, 0, 10);
            textBox.Height = 26;
            textBox.Width = 390;
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
            label.Margin = new Thickness(10, 0, 0, 10);
            label.Height = 26;
            label.Width = 50;
            label.Name = fullId;
            label.FontSize = 16;
            pg.RegisterName(fullId, label);
            return label;
        }

    }
}