   M S B A M L   `   `   `  ����A  =WDCApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WDCApp.PagesWDCApp.Pages  5          WDCApp.Pages.OrdersPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:WDCApp.Pages     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5        N� Title$  
OrdersPage��5
      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ ordersListBox��5      $��0��6A   $��2��6N      ListBoxItemSeparatorStyle#��[  6b   ��6)   �� ��5      �� 5      .R�5      �� .+�5      �� �� 5      $#�Номер договора: ��6"   $
��Bold"�6;    �� 5      $
��Bold"�6B   $��16(�6T   �6"   �� *NumberContract+6T   5      �� 5      $��0,5,0,0q�6$   �� 5      $�Услуга:��6"   $
��Bold"�61    �� 5       �6"   �� *Service.NameService+5!      �� 5"      $��0,5,0,0q�6$   �� 5#      $�Клиент:��6"   $
��Bold"�61    �� 5$      �6"   �� *Client.Name+ �� 5%      �6"   �� *Client.Patronymic+ �� 5&      �6"   �� *Client.Surname+5'      �� 5(      $��0,5,0,0q�6$   �� 5)      $�	Дата:��6"   $
��Bold"�6/    �� 5*      �6"   �� *Date+ �� StringFormat$ yyyy-MM-dd HH:mm:ss��5+      �� 5,      $��0,5,0,0q�6$   �� 5-      $ �Статус заказа:��6"   $
��Bold"�68    �� 5.      �6"   �� *Status+5/      �� 50      -   5        $
��Bold"�52   !   $��35��63   $��35��6>   $
��Left=�6J   $��5,5,0,0q�6e   y�51   !   �� *IsCompleted+��53      "  FontAwesome.WPF.ImageAwesome  54   "      
Foreground
 �����$	��Top=�6E   $��15��6]   $��18��6h      Icon$
 Bomb=�6t   $��3q�6�   56      57      58      5:      �� 5;   
   $��1��6   $��1��6"   $��5q�62    �� Orientation$ 
Horizontal=�6=   .+�5<      �� -   5         ��Name$ searchTextBox��5<      $	��200��6*   $��25��66   $��5q�6B   �� 5=      -   5        $��5,0,0,0q�5=      ��5?        5@      -   5        $��SearchButton��5@   &   
 �����$	��Top=�6M   $��25��6e   $��15��6p   $ Search=�6|   $��3q�6�   5B      �� 5C      -   5        	 ��Name$	 statusComboBox��5C      $	��120��5D      $��25��6#   $��5q�6/   .@�5E      �� $��Все��6   �� 5F      $��Завершенные��6   �� 5G      $!��Не завершенные��6   5H      5I      �� 5J   
   -   5        
 ��Name$
 ExportToExcelButton��5L      $��25��5J      $	��150��6   $��1��5K      $��0��6   $��5q�6.   ��5M        5N      
 �����$	��Top=�65   $��35��6M   $��18��6X   $ FileExcelOutline=�6d   $��3q�6}   5P      5Q      5R      