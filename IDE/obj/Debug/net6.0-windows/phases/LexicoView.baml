   M S B A M L   `   `   `  ����>  :IDE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:IDE.phases
IDE.phases  5          IDE.phases.LexicoView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:IDE.phases     5        N� Title$  
LexicoView��5	       � 	Resources 5
       �� 
TargetType �� 
ResourceId �� TypeInTargetAssembly&   System.Windows.Controls.DataGrid �� Property �� Value �� 
TargetType ��Name	 J� SnapsToDevicePixels
 �Name J� Opacity � Points �� Stretch �� Triggers P� Property P� Value �� 
TargetName   DataGridStyle1 �� Padding ��Name � ColumnDefinitions � RowDefinitions   SelectAllCommand7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!   HeadersVisibilityConverter �� 	Converter �� RelativeSource �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name  �� CanContentScroll! ��Name" �� ScrollableHeight# � Maximum$ �� Orientation% �� ViewportHeight& �� ViewportSize*' �� #ComputedVerticalScrollBarVisibility( � Value) �� Mode* �� ScrollableWidth+ �� ViewportWidth,, �� %ComputedHorizontalScrollBarVisibility- �� Triggers. ]� 
Conditions/ �� Property0 �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell��%  (��       5      $" DataGridSelectAllButtonStyle��"  )&	  �    '  �    '  !    �� 6
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ Border��6(   #��� ��68   $
	 True��6y   � 5      $
 Arrow��6&   
���   �$��Right=�6B   $��8,8,3,3q�6^   $
 0.15Q�6o    �   ��� ��� ��� ��� $ Uniform=�6�   $��Bottom=�6�   5       5      P� 5       �@J�IsMouseOver$
 True��6=   . �5   "   �� $ Border��6;   # � ��6O    � ��5      P� 5       � ��$
 True��6;   . �5   "   �� $ Border��69   # � ��6M    � ��5      P� 5       � {�$ False��6;   . �5   "   �� $ Arrow��6?    � y�$ 	Collapsed=�6R   5      5      5       5!      �� 5"   
   "  6'   .�5#      ��  � �� �#2e2e2e6+   �� 5$       � ��
 �����6+   �� 5%       � ��
 �   �6,   �� 5&       � ��$ 5q�60   �� 5'        �@ RowDetailsVisibilityMode$ VisibleWhenSelected=�69   �� 5(       � ��$
 true��6>   �� 5)       �@��PanningMode$
 Both=�69   �� 5*       �@��IsFlicksEnabled$ False��67   �� 5+       � ��$ 18(�6)   �� 5,       � ��$
 Bold"�6+   �� 5-       � �� 5.      �� 5/      "  6&   .R�50      �� #��z��6!   #��z��6K   #��z��6w   # z��6�   $
	 True��6�   ._�51      �� $ DG_ScrollViewer��6+   $|�false��6D   ��52   "   �� 53   &   " ��66   .R�54   *   �  55   .   �� 56   2   $
��Auto �6C   �� 57   2   $��* �6C   �� 58   2   $
��Auto �6C    5:   .   �� 5;   2   $
��Auto �6@   �� 5<   2   $��* �6@   �� 5=   2   $
��Auto �6@   .+�5?   .   �� #��Z 65   $|�false��6d   ��6v   C� *�� $" DataGridSelectAllButtonStyle��"  +y�6�   �� *HeadersVisibility+# Z # Z  �� "  ��6�  �� *CellsPanelHorizontalOffset+ �� "    5@   .   $! PART_ColumnHeadersPresenter��6M   $��1��6r   y�6�   �� *HeadersVisibility+# Z # Z  �� "  �� 5A   .   $! PART_ScrollContentPresenter��6E   #  z��6j   $��2��6�   $��1��6�   �� 5B   .   $! PART_VerticalScrollBar��68   $��2��6X   ## z" 6h   $$ Vertical=�6�   $��1��6�   #& z% 6  #y�z' 6H  ( 6�   �� *VerticalOffset+$) OneWay=� �� *TemplatedParent+6H  � 5C   .   $��1��63   $��2��6C    5D   2   �� 5E   6   ��6G   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "  �� 5F   6   $��* �6G   .+�5H   2   �� $! PART_HorizontalScrollBar��6<   $��1��6^   ## z* 6n   $$ 
Horizontal=�6�   #& z+ 6  #y�z, 6C  ( 6�   �� *HorizontalOffset+$) OneWay=� �� *TemplatedParent+6C  5I   /   5J   +   5K   '   .��5M   "   �� #	 z	 61   5N      5O      5P      5R      - 5S      ]� 5T      . 5U      �� 5V      / �@��
IsGrouping$
0 true��6:   �� 5W      "/ �@%�IsVirtualizingWhenGrouping$0 false��6\   ./�5Y      ��  � ��$ false��6F   5Z      5\      �� 5]   
   "  6   .�5^      ��  � �� �	#FF688CAF6+   �� 5_       � ��$ 5q�6(   �� 5`       � ��
 �   �6,   �� 5a       � ��$ 0 0 1 0q�60   �� 5b       � ��
 �����6+   �� 5c       � ��$ SemiBold"�6+   �� 5d       � ��$ 25(�6)   �� 5e       � ��$ Robot)�6+   �� 5f       � ��$ Center=�6;   5g      �� 5h   
   "  6   .�5i      ��  � �� �#2e2e2e6+   �� 5j       � �� �#d4cfcf6+   - 5l      P� 5n       �@ 
IsSelected$
 True��60   . �5o      ��  � �� �#69696963   �� 5p       � ��
 ���� 64   5q      P� 5s       �@J�IsMouseOver$
 True��61   . �5t      ��  � �� �#69696963   �� 5u       � ��
 ���� 64   5v      5{      5|      .��5~      � .+�5   
     -   5        1  Name$1 dataGrid��5   <   #���   6   2   CanUserAddRows$2 False��5�      3   AutoGenerateColumns$3 False��5�      4   
IsReadOnly$
4 True��5�      5   Columns5 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn6   Header$6 Token��6%   7   Width5  /System.Windows.Controls.DataGridLengthConverter$7 * 6N   1	  +System.Windows.Controls.DataGridBoundColumn8 	  Binding8 64   �� *Token+6N     5�      $6 Lexema��6%   $7 * 6P   8 65   �� *Lexema+6P   5�      5�      5�      