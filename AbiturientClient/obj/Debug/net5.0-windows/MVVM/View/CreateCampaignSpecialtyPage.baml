   M S B A M L   `   `   `  ����K  GAbiturientClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:AbiturientClient.MVVM.ViewModelAbiturientClient.MVVM.ViewModel  5   	   G(clr-namespace:AbiturientClient.MVVM.ViewAbiturientClient.MVVM.View  5	   	   <    6AbiturientClient.MVVM.View.CreateCampaignSpecialtyPage   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   V RXamlAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=20a987d8023d9690<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  5   	   =	viewModel-clr-namespace:AbiturientClient.MVVM.ViewModel  5   	   4local(clr-namespace:AbiturientClient.MVVM.View  5	   	     N� Title$-  'Создание направлений��5   	   $	��760��6&   $	��715��63    N� 
Background
 ����� � DataContext 5      A  ;AbiturientClient.MVVM.ViewModel.CreateCampaignSpecialtyPage  5   
   .��5      � .+�5   
   �� $��95��6   $��30��6   $	��Top=�6I   $
��Left=�6a   $��30, 20q�6|       ButtonMouseOverNonBackGround#��[  6�   
�������$��0q�6�   ��6(   �� *ToMainWindow+.��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $)��#/Resources/icons8-back-arrow-96.png��6   $��30��6E   $��30��6P     RoundImageOpasity#��[ 6\   �� 5      $��В меню��6   $��65��6)   $��0,0,0,0q�64   $��5,3,0,0q�6E   
����  �$��16(�6i   ${�False��6w   5      5      �� 5   
   $E��?Введите информацию о направлениях��6   $��Center=�6<   $��0,10,0,0q�6Y   $	��Top=�6k   $	��440��6�   $��45��6�   $��24(�6�   �� 5   
   $	��660��6   $	��600��6#   $	��Top=�60   $��
22,70,23,0q�6H   .��5      �� -   5         ��Name$ StackPanelSpecialty��5      $��0��66   $	��630��6A   $��Visible=�6M   $	��Top=�6�   5      �� 5   
   -   5         ��Name$ btNewStruct��5        RoundButton#��[ 6G   $��570,0,0,685q�6l   $��50��6�   $��50��6�   $��0,0,0,0q�6�   $��0,0,0,0q�6�   ��6&   �� *AddSpecialty+.��5      �� -   5         ��Name$ button��5      $��/Resources/button.png��6$   #��[ 6C   $��Center=�6n   $��Center=�6�   $��60��6�   $��60��6�   5      �� 5   
   $��Отправить��6    ! ButtonTargetNonOnlyBackground#��[ 6%   $��30��6}   $��80��6�   $��Bottom=�6�   $��Right=�6�   $��25,50q�6�   $��16(�6�   ���	#FF1D80E2
���������6\   �� *FinishCreate+6   �� 5   
   -   5        $ 	animation��5      '  !XamlAnimatedGif.AnimationBehavior   	SourceUri$ /Resources/loading.gif?�6#   $��Center=�6\   $��Center=�6y   $	��100��6�   $	��100��6�   $y�Hidden=�6�   5      5       