   M S B A M L   `   `   `  ����K  GAbiturientClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:AbiturientClient.MVVM.ViewModelAbiturientClient.MVVM.ViewModel  5      3clr-namespace:AbiturientClientAbiturientClient  5      )    #AbiturientClient.MVVM.View.Recovery   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      V RXamlAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=20a987d8023d9690<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  5      =	viewModel-clr-namespace:AbiturientClient.MVVM.ViewModel  5      *localclr-namespace:AbiturientClient  5        � 
ResizeMode$  NoResize=�5
       � Title$3 -Восстановление аккаунта��5      $	��234��6!   $	��563��6.    � WindowStartupLocation$ CenterScreen=�6:    � DataContext 5      .  (AbiturientClient.MVVM.ViewModel.Recovery  5   
   .��5      � -   5        $��gridRecovery��5      $��Center=�6   $	��190��6<   $	��Top=�6I   $	��543��6a   $
{�True��6m   $y�Visible=�6~   .+�5   
   �� $[��UУкажите почту, использованную при регистрации��6   $��Center=�6H   $	��Top=�6e   $��0,10,0,0q�6}   $��22(�6�   
�������6�   �� 5   
   -   5         ��Name$ 	tbRecMail��5   N   $��Center=�6a   $��39��6~   $��0,77,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��514��6�   $��22(�6�   ���	#FFFFFDFD$��Arial)�6�   $��0,0,0,0q�6  ���	#FFD1D1D1$��Center=�66  ��6   �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=�66  �� 5   
    !  ButtonTargetNonOnlyBackground#��[  69   $��Получить код��6p   $
��Left=�6�   $��379,144,0,0q�6�   $	��Top=�6�   $	��150��6�   $��20(�6�   $��30��6�   ���	#FF1D80E2
���   �
���������6   �� *SendRecoveryRequest+6   �� 5   
   -   5         ��Name$ 	animation��5      '  !XamlAnimatedGif.AnimationBehavior   	SourceUri$ /Resources/loading.gif?�6#   $��Center=�6\   $��Center=�6y   $	��100��6�   $	��100��6�   $y�Hidden=�6�   �� 5   
   $��Вернуться��6;   $
��Left=�6O   $��245,144,0,0q�6j   $	��Top=�6   $	��115��6�   $��30��6�   $��20(�6�   
�������$��0,0,0,0q�6�   ���	#FF1F1F1F��6   �� *ToAuthorisationWindow+6�   5      5      