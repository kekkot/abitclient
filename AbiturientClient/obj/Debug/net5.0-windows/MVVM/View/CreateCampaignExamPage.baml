   M S B A M L   `   `   `  ����K  GAbiturientClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:AbiturientClient.MVVM.ViewModelAbiturientClient.MVVM.ViewModel  5      3clr-namespace:AbiturientClientAbiturientClient  5      7    1AbiturientClient.MVVM.View.CreateCampaignExamPage   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      =	viewModel-clr-namespace:AbiturientClient.MVVM.ViewModel  5      *localclr-namespace:AbiturientClient  5        N� Title$)  #Создание предметов��5
      $	��760��6    $	��715��6-    N� 
Background
 ����� � DataContext 5      <  6AbiturientClient.MVVM.ViewModel.CreateCampaignExamPage  5   
   .��5      � -   5        $��Departmente��5      $	��760��6   $	��715��6+   .+�5   
   �� $��95��6   $��30��6   $	��Top=�6I   $
��Left=�6a   $��30, 20q�6|       ButtonMouseOverNonBackGround#��[  6�   
�������$��0q�6�   ��6(   �� *ToMainWindow+.��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $)��#/Resources/icons8-back-arrow-96.png��6   $��30��6E   $��30��6P     RoundImageOpasity#��[ 6\   �� 5      $��В меню��6   $��65��6)   $��0,0,0,0q�64   $��5,3,0,0q�6E   
����  �$��16(�6i   ${�False��6w   5      5      �� 5   
   $��610,680,0,0q�6   �� 5      -   5         �Name$ HyperLinkExam��5      $'��!CreateCampaignDepartmentPage.xaml?�61   
�������.�6t   .�5      �� $��
Далее��6    ! ButtonTargetNonOnlyBackground#��[ 6-   $��30��6}   $��80��6�   $��16(�6�   ���	#FF1D80E2
���������6d   �� *Test+6�   5      5      �� 5   
   $%��Введите предметы��6   $��Center=�6+   $��Center=�6O   $��0,10,0,0q�6l   $	��Top=�6~   $	��424��6�   $��49��6�   $��24(�6�   �� 5   
   -   5         ��Name$ btNewStruct��5        RoundButton#��[ 6B   $��570,0,0,685q�6g   $��50��6|   $��50��6�   $��0,0,0,0q�6�   $��0,0,0,0q�6�   ��6&   �� *	AddExam+.��5      �� -   5         ��Name$ button��5      $��/Resources/button.png��6$   #��[ 6C   $��Center=�6n   $��Center=�6�   $��60��6�   $��60��6�   5      �� 5   
   $��0,70,0,0q�6   $	��665��6)   $	��600��65   $��Center=�6B   $	��Top=�6_   $
��Auto=�6w   .��5      �� -   5         ��Name$ StackPanelExam��5      $��0��61   $	��600��6<   $��Visible=�6H   $	��Top=�6}   5       5!      5"      