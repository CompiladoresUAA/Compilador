   M S B A M L   `   `   `  ????>  :IDE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IDEIDE  5   	       IDE.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:IDE     5   	   ^ ZICSharpCode.AvalonEdit, Version=6.2.0.78, Culture=neutral, PublicKeyToken=9cc39be672370310?
avalonedit.http://icsharpcode.net/sharpdevelop/avalonedit  6)     ? Title$  Jucs=??5   	   $	??450??6   $	??800??6$    ? Icon$ /icon-512x512.png??61   .??5      ?  ? 	Resources 5   
      options ?? 
TargetType ?? Triggers P? Property P? Value ?? Property ?? Value??%-  &	        ?? 5      " m?6$    5      P? 5       ?@J?	IsFocused$
 True??63   . ?5      ??  ? ??
 ?????67   ?? 5       ? ??$
 Bold"?67   ?? 5       ? ??$ -2q?63   ?? 5       ? ??
 ?   ?68   ?? 5       ? ??$ 2q?6<   5      5      5      	 ? ColumnDefinitions	 5   
   ?? 5      
 ? RowDefinitions
 5   
   ?? 5      ?? 5      $	??15* ?6   .+?5"      ?? $
??Auto??6   
????ح?$??0??66   .+?5#   
   o? $	??Top=?6   $??20??5$   *   .@?5&      m? $??_Archivo??6   .@?5'      m? -   5        $??_Abrir??5'       m? Icon 5(      ?? 5)      $??/open-folder.png??6$   5+      m? 5,      -   5        $??Guardar??5,       5-      ?? 5.      $??	/save.png??6$   $??20??67   50      m? 51      -   5        $??Guardar como??51       52      ?? 53      $??/saveas.png??6$   $??20??69   $??20??6D   55      m? 56      -   5        $??Cerrar??56       57      ?? 58      $??
/close.png??6$   $??20??68   $??20??6C   5:      5<      m? 5=      $??Editar??6   m? 5>      $??Formato??6   m? 5?      $??Compilar??6   m? 5@      $??Ayuda??6   5D      5G      ? 5H   
   
???????$??2??6&   	 5I      ?? 5J      $
??1.5* ?6#   ?? 5K      
 5N      ?? 5O      $??2* ?6    ?? 5P      .+?5T      '  !ICSharpCode.AvalonEdit.TextEditor  -   5          Name$ codigo??5T   %   $??Consolas)?65   $
??20pt(?6K   $??0??6[      ShowLineNumbers$
 True??6k   $??0,10,0,0q?6?   ]? 5V      $??1??6   $??2??6%   
???????$??
10,0,10,10q?6L   .+?5Y      o? $	??Top=?6   .@?5Z      m? #??[  6   $??Errores??6@   m? 5[      #??[  6   $??
Resultados??6@   5\      ?? 5^      $??Bottom=?6    ?? 
IsReadOnly$
 True??62   $??5,5,5,10q?6D    ?? AcceptsReturn$
 True??5_      " ?? VerticalScrollBarVisibility$ Visible=?6/    ?? TextWrapping$
 Wrap=?5`      .??6.   aaa5b      5d      ]? 5f      $??0??6   $??1??6%   
???????$??5,5,5,10q?6H   .+?5i      o? $	??Top=?6   .@?5j      m? -   5        $??	mnulexico??5j   e   $??Lexico??6   #??[  6/   6e   m? 5k      -   5        $??mnusintactico??5k   T   $??
Sintactico??6   #??[  63   6i   m? 5m      -   5        $??mnuSemantico??5m   S   #??[  6   $??	Semantico??6@   6g   m? 5n      -	   5        $??mnuInter??5n   [   #??[  6   $??Codigo Intermedio??6@   6k   5o      ?? 5q      -
   5        $??trans??5s   .   $??Bottom=?5q      $
 True??62   $??5,5,5,10q?6D   $
 True??5r      $ Visible=?6/   $
 Wrap=?5s      .??6;   5u      5w      5y      5z      5|      