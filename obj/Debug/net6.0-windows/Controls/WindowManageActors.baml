   M S B A M L   `   `   `  ????E  AGHVideoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GHVideoApp.ControlsGHVideoApp.Controls  5   	   ,    &GHVideoApp.Controls.WindowManageActors   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:GHVideoApp.Controls     5   	   -   5          ? Title$  WindowManageActors??5	   	   $	??640??6$   $	??800??61    ? 	Resources 5
       ?? 
TargetType ?? Property ?? Value   
titleStyle??%g  '??       &	  ?     '??     ?? 5   
   " ??6   .?5      ??  ? ??$ 22(?6)   ?? 5       ? ??$ 2q?6'   ?? 5       ? ??$
 Left=?6;   5      ?? 5   
   " ??6#   .?5      ??  ? ??$ 32(?6)   ?? 5       ? ??$ Center=?6;   5      ?? 5   
   " ??6   .?5      ??  ? ??$ 22(?6)   5      5      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??2* ?6    ? RowDefinitions 5   
   ?? 5       $??46 ?6   ?? 5!      $??* ?6   ?? 5"      $??2* ?6   ?? 5#      $??46 ?6   .+?5&   
   ?? $??0??6   $??2??6   $??管理Actor??5'      #??[  5(      ?? 5*   
   -   5         ??Name$ 
listActors??5*      $??1??5+      $??2??6    ??5,      ?? *ActorCollection+??5-      ?? 5.      .R?5/      ?? ??6   ?? *GHName+50      52      53      54      