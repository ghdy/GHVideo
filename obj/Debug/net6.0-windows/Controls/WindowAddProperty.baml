   M S B A M L   `   `   `  ????E  AGHVideoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GHVideoApp.ControlsGHVideoApp.Controls  5   	   +    %GHVideoApp.Controls.WindowAddProperty   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:GHVideoApp.Controls     5   	   -   5          ? Title$  WindowAddProperty??5   	   $	??320??5	   	   $	??480??5
   	    ? WindowStartupLocation$ CenterScreen=?5   	    ? 	Resources 5       ?? 
TargetType ?? Property ?? Value??%R   '??       ?? 5   
   " ??6   .?5      ??  ? ??$ 24(?6)   5      5      .??5      ]? $
??True??6   .+?5   
   ?? -   5         ??Name$ propertyTitle??5      $	??Top=?6'   $??Add Property??5      $??Center=?5      $??32(?5      ?? 5   
   -   5         ??Name$ propertyValue??5      $	??Top=?5      $??45??5      $??5q?5      $??Black"?5      $??32(?5      
?????????5       ?? *NewProperty+ ?? Mode$ OneWayToSource=?5!      ?? 5"   
   -   5        	 ??Name$	 buttonOK??5"      $??Bottom=?5#      $??确定??5%      $??48??5&      $	??120??5'      $??26(?5(      ?? 5)   
   -   5        
 ??Name$
 existsPropertyList??5)      $
??Left=?5*      
???????$??5q?5,      $??22(?5-       ?? SelectionMode$ Multiple=?5.      ??5/      ?? *
Properties+??50      ?? 51      .R?52      ?  ? Orientation$ 
Horizontal=?6    $	??440??53       54      ??57      ?? 58      " ??6    ?? Triggers 59      P? 5:       P? Property ?@??
IsSelected P? Value$
 True??68   . ?5;      ??  ? ??
 ?  ??5=      5>      5@      5B      5D      5E      