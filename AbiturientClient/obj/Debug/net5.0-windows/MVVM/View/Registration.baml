   M S B A M L   `   `   `  ����K  GAbiturientClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:AbiturientClient.MVVM.ViewModelAbiturientClient.MVVM.ViewModel  5   	   3clr-namespace:AbiturientClientAbiturientClient  5   	   -    'AbiturientClient.MVVM.View.Registration   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   C	viewModel-clr-namespace:AbiturientClient.MVVM.ViewModel     5   	   0localclr-namespace:AbiturientClient     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   V RXamlAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=20a987d8023d9690<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  5	   	   :
extensions)using:Microsoft.Toolkit.Uwp.UI.Extensions  5
   	     � 
ResizeMode$  NoResize=�5   	    � Title$- 'Регистрация аккаунта��5   	   $	��590��6&   $	��488��63    � WindowStartupLocation$ CenterScreen=�6?    � DataContext 5      2  ,AbiturientClient.MVVM.ViewModel.Registration  5   
   .��5      � -   5         �Name$ gridReg��5      $��Center=�6   $��0,10,0,0q�69   $	��Top=�6K   $y�Visible=�6c   .+�5   
   �� $��Регистрация��6   $
��Left=�6&   $��	19,12,0,0q�6A   $	��Top=�6T   $��47��6l   $	��229��6x   $��36(�6�   $��Arial)�6�   �� 5   
   $%��Имя пользователя��6   $
��Left=�6+   $��	24,74,0,0q�6F   $	��Top=�6Y   $	��299��6q   $��16(�6}   $��32��6�   $��0,0,0,0q�6�   $��Segoe UI Light)�6�   ���	#FF6868686�   �� 5   
   $!��Номер телефона��6   $
��Left=�6)   $��
25,374,0,0q�6D   $	��Top=�6X   $��16(�6p   $��0,0,0,0q�6~   $��26��6�   $��Segoe UI Light)�6�   ���	#FF6868686�   �� 5   
   $��Пароль��6   $
��Left=�6!   $��
24,224,0,0q�6<   $	��Top=�6P    J� RenderTransformOrigin$ 0.487,5.019&�6h   $��16(�6�   $��Segoe UI Light)�6�   $��0,0,0,0q�6�   ���	#FF6868686�   �� 5   
   $)��#Подтвердите пароль��6   $
��Left=�6-   $��
25,297,0,0q�6H   $	��Top=�6\   $��Segoe UI Light)�6t   $��16(�6�   $��0,0,0,0q�6�   ���	#FF6868686�   �� 5   
   $2��,Адрес электронной почты��6   $
��Left=�62   $��
20,143,0,0q�6M   $	��Top=�6a   $��16(�6y   $��Segoe UI Light)�6�   ���	#FF6868686�   �� 5   
   -   5         ��Name$ tbLogin��5      $
��Left=�6_   $��38��6z   $��	25,97,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��415��6�   $��24(�6�   ���	#FFF0F0F0��6#   �� *Login+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5        $ tbEmail��5      $
��Left=�6_   $��38��6z   $��
25,172,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��415��6�   $��24(�6�   ���	#FFF3F3F3��6#   �� *Email+$ PropertyChanged=�6�   G� 5   
   -   5        	 G�Name$	 tbPass��5      $
��Left=�6&   $��38��6A   $��
25,247,0,0q�6M   $	��Top=�6a   $	��415��6y   $��24(�6�   
�������6�   G� 5   
   -   5        $	 tbReturnPass��5      $
��Left=�6,   $��
25,323,0,0q�6G   $	��Top=�6[   $	��415��6s   $��38��6   $��24(�6�   
�������6�   %  Xceed.Wpf.Toolkit.MaskedTextBox  5   
   -   5        
  Name$
 tbPhoneNumber��5   7      Mask$ +0-(000)-000-00-00��6   $
��Left=�6�   $��38��6�   $��
25,400,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��415��6�   $��24(�6  
���������6N   �� *PhoneNumber+$ PropertyChanged=�6  �� 5   
   -   5         ��Name$ btEnter��5      $*��$Зарегистрироваться��6N    !  ButtonTargetNonOnlyBackground#��[  6k   $��Center=�6�   $��	0,455,0,0q�6�   $	��Top=�6�   $	��376��6�   $��50��6�   $��Normal"�6  $��22(�6  
�������
�������$��0,0,0,0q�6K  ���	#FF1D80E2��6"   �� *SendRegistrationRequest+6]  �� 5   
   $;��5Вернуться к окну авторизации��6;     ButtonTargetNonBackground#��[ 6b   $��Center=�6�   $	��Top=�6�   $��Center=�6�   ���	#FFFFFCFC���	#FF5F5E5E
�������$��0q�6.  $��	0,510,0,0q�6B  $��14(�6U  ��6   �� *ToAuthorisationWindow+6U  �� 5   
   $��347,10,48,477q�6   $��/Resources/2.png��6'   �� 5    
   -   5         ��Name$ 	animation��5       '  !XamlAnimatedGif.AnimationBehavior   	SourceUri$ /Resources/loading.gif?�6#   $��Center=�6]   $��Center=�6z   $	��100��6�   $	��100��6�   $y�Hidden=�6�   5"      5#      