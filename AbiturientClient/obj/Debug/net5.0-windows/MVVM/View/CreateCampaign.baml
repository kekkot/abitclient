   M S B A M L   `   `   `  ����K  GAbiturientClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:AbiturientClient.MVVM.ViewModelAbiturientClient.MVVM.ViewModel  5   	   G(clr-namespace:AbiturientClient.MVVM.ViewAbiturientClient.MVVM.View  5   	   /    )AbiturientClient.MVVM.View.CreateCampaign   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   C	viewModel-clr-namespace:AbiturientClient.MVVM.ViewModel     5   	   :local(clr-namespace:AbiturientClient.MVVM.View     5   	     � 
ResizeMode$  NoResize=�5	   	    � Title$8 2Создание приемной компании��5
   	   $	��760��6,   $	��715��69    � WindowStartupLocation$ CenterScreen=�6E    � DataContext 5      4  .AbiturientClient.MVVM.ViewModel.CreateCampaign  5   
   .��5      � .+�5   
    � -   5          �Name$ 	mainFrame��5        � Source$! CreateCampaignExamPage.xaml?�6#     � NavigationUIVisibility$ Hidden=�6H    J� InputBindings 5      �� 5      
 �� Key$
 Back��6   	 �� Command$	 NotACommand��6(   �� 5      $
 Next��6   $	 NotACommand��6(   �� 5      $ BrowserBack��6   $	 NotACommand��6/   �� 5      $ BrowserForward��6   $	 NotACommand��62   �� 5      $
 Left��6   
 �� 	Modifiers$	
 Altf�6(   $	 NotACommand��68   �� 5      $ Right��6   $	
 Altf�6)   $	 NotACommand��69   5      5      5      