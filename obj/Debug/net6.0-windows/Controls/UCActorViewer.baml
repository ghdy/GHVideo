   M S B A M L   `   `   `  ????E  AGHVideoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GHVideoApp.ControlsGHVideoApp.Controls  5      '    !GHVideoApp.Controls.UCActorViewer   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:GHVideoApp.Controls     5        ? 	Resources  5
         Info ?? 
TargetType ?? Property ?? Value??%?  &	        '?? ?     '?? ?    ?? 5   
   " ??6   .?5      ??  ? ??
 ?????6+   ?? 5       ? ??$ 20(?6)   ?? 5       ? ??$ 2q?6'   ?? 5       ? ??$ Black"?6+   5      ?? 5   
   " ??6   .?5      ??  ? ??$ 32??6'   ?? 5       ? ??$	 100??6&   ?? 5       ? ??$ 20(?6)   ?? 5       ? ??$ 2q?6'   ?? 5       ?@?Cursor$
 Hand??6'   5      ?? 5   
   " ??6   .?5      ??  ? ??$ 36??6'   ?? 5        ? ??$	 0,2q?6'   ?? 5!       ? ??$ Black"?6+   ?? 5"       ? ??$ 22(?6)   ?? 5#       ? ??
 ?????6+   5$      5%      .??5&      ?  ? ColumnDefinitions 5'   
   ?? 5(      $??* ?6   ?? 5)      $??* ?6   ?? 5*      $??* ?6   ?? 5+      $??* ?6   ?? 5,      $??* ?6   ?? 5-      $??* ?6    ? RowDefinitions 50   
   ?? 51      $??50 ?6   6*   ?? 52      $??46 ?6   6*   ?? 53      $??46 ?6   6*   ?? 54      $??* ?6   6)   ?? 55      $??46 ?6   6*   .+?58   
   ?? -   5         ??Name$ propertyTitle??58      $??0??59      $??6??5:      $??Actor??5;      $??Center=?5<      $??30(?5=      ?? 5>   
   $??1??6   $??0??6   #??[  5?      $??名字:??5@      ?? 5A   
   -   5         ??Name$ txtName??5A      $??1??5B      $??1??6   $??2??6/   ??5E      ?? *Current.GHName+5F      ?? 5H   
   $??1??6   $??3??6   #??[  5I      $??人种:??5J      ?? 5K   
   $??1??6   $??4??6    $??2??60   $??22(?5L      $	??0,5q?5M      ??5N      ?? *Races+ ?? SelectedItem 5O      ?? *Race+	 ?? Mode$	 TwoWay=?5R      ?? 5S   
   $??2??6   $??0??6   #??[  5T      $??特点:??5U      ?? 5V   
   -   5        
 ??Name$
 listTags??5V      $??2??6&   $??1??63   $??5??6D   $??22(?5W      $	??0,2q?5Y      ??5Z      ?? *TagCollection+??5[      ?? 5\      .R?5]      ?  ? Orientation$ 
Horizontal=?6    5^      5`      ?? 5h   
   $??3??6   $??0??6   #??[  5i      $??别名:??5j      ?? 5k   
   -   5         ??Name$ listName??5m      $??3??5k      $??1??6   $??2??6/   
?????????5n      ?? *NameCollection+5p      ]? 5q   
   $??3??6   $??3??6!   $??3??62   .+?5r      ]? $	??Top=?6   $
??True??6-   .+?5s      ?? $??新名:??6   $
??Left=?6&   #??[  6<   ?? 5t      -   5        $ txtCurrentName??5u      $	??Top=?5t      
???????$??2q?5w      $?? ??5x      6   5y      ? 5z      $??Center=?6   $??Center=?65   .+?5{      ?? -   5         ??Name$ 	buttonAdd??5{      $??添加??6,   $??ApplicationCommands.New??5|      ??5}      ??  ?? ElementName$ txtCurrentName?? ?? Path$
 Text?6Y   ?? 5~      -   5        $ 	buttonDel??5~      $??删除??6,   $ ??ApplicationCommands.Delete??5      ??5?      ?? $ listName??$ SelectedItem?6[   5?      5?      ?? 5?   
   -   5        $ buttonOK??5?      $??5??5?      $??1??6   $??2??6.   
???????$??确定??5?      5?      ?? 5?   
   -	   5        $ buttonNo??5?      $??5??5?      $??3??6   $??2??6.   
???????$??取消??5?      5?      5?      5?      