   M S B A M L   `   `   `  ����K  GAbiturientClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:AbiturientClient.MVVM.ViewModelAbiturientClient.MVVM.ViewModel  5   	   3clr-namespace:AbiturientClientAbiturientClient  5   	   ] YMicrosoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\Bclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasicMicrosoft.VisualBasic 66   .    (AbiturientClient.MVVM.View.Authorisation   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   C	viewModel-clr-namespace:AbiturientClient.MVVM.ViewModel     5   	   V RXamlAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=20a987d8023d9690<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  5   	   *localclr-namespace:AbiturientClient  5   	   JiBclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasic  66     � 
ResizeMode$  NoResize=�5
   	    � Title$% Меню авторизации��5   	   $	��420��6"   $	��585��6/    � WindowStartupLocation$ CenterScreen=�6;    � DataContext 5      3  -AbiturientClient.MVVM.ViewModel.Authorisation  5   
   .��5      � -   5         �Name$ gridAuth��5      .+�5   
   �� $��430,10,56,0q�6   $��/Resources/2.png��6%   $	��Top=�6?   $��62��6W   �� 5   
   $��Авторизация��6   $
��Left=�6&   $��	57,22,0,0q�6A   $	��Top=�6T   $��40(�6l   $��50��6z   $	��252��6�   $��Arial)�6�   ���	#FF686868$��7,0,0,0q�6�   �� 5   
   -   5         ��Name$ tbAuthLogin��5      $
��Left=�6c   $��64��6~   $��	57,77,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��469��6�   ���	#FFFFFDFD$��Arial)�6�   $��Center=�6�   $��15,0,0,0q�6!  $��24(�64  ���	#FFD1D1D1$��2��6Z  ��6'   �� *Login+ �� UpdateSourceTrigger$ PropertyChanged=�6Z  G� 5   
   -   5         G�Name$ 
tbAuthPass��5      $
��Left=�6*   $��64��6E   $��
57,144,0,0q�6Q   $	��Top=�6e   $	��469��6}   $��24(�6�   ���	#FFFFFDFD$��Arial)�6�   $��Center=�6�   $��15,0,0,0q�6�   $��2��6�   �� 5   
   -   5        	 ��Name$	 btEnter��5      $��
Войти��6O    !  ButtonTargetNonOnlyBackground#��[  6_   $
��Left=�6�   $��
57,229,0,0q�6�   $	��Top=�6�   $	��469��6�   
 J� RenderTransformOrigin$
 -1.667,-1.864&�5      $��20(�6'   $��58��65   ���	#FF1D80E2
���   �
�������$��2��6   ��5   "   �� *SendAuthorisationRequest+5      �� 5   
   -   5        $	 btPassRecovery��5      $/��)Восстановление пароля��6N     ButtonTargetNonBackground#��[ 6n   $
��Left=�6�   $��
57,302,0,0q�6�   $	��Top=�6�   $	��200��6�   $��25��6�   $��18(�6  $��Center=�6  ���	#FFFFFCFC���	#FF5F5E5E
�������$��0q�6s  ��6)   �� *ToRecoveryWindow+6s  �� 5   
   -   5         ��Name$ 	animation��5      '  !XamlAnimatedGif.AnimationBehavior   	SourceUri$ /Resources/loading.gif?�6#   $��Center=�6\   $��Center=�6y   $	��100��6�   $	��100��6�   $y�Hidden=�6�   �� 5   
   -   5        $	 btGoReg��5      $��Регистрация��6K   #��[ 6a   $
��Left=�6�   $��
57,342,0,0q�6�   $	��Top=�6�   $	��109��6�   $��25��6�   $��18(�6�   $��Center=�6  $��Center=�6%  $��0q�6G  
�������
����������	#FF615F5F��6"   �� *ToRegistrationWindow+6�  5      5      