   M S B A M L   `   `   `  ����  
VenixTools.    (Venix.ODE.app.Pages.Download.PS3.edtJogo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFirstFloor.ModernUI, Version=1.0.6.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  5      $	��820��5      $	��510��6+   .��5	      � $��0,0,0,14q�6   .+�5
   
   �� $��	0,0,0,-22q�6   .��5      �� -   5          ��Name$  Panel��5      $	��150��6(   $	��512��67   .+�5      �� $��EDITANDO JOGO��6      Heading2#��[  61   �� 5      $Q��KVocê pode editar ou apagar seu jogo por aqui, basta utilizar o ID do jogo ��6   � 5      $��	0,82,0,21q�6    � ColumnDefinitions 5      �� 5      $	��59* �6+   �� 5      $
��146* �6+   .+�5      �� $��ID (BLES/BLUS/etc):��6   $
��Left=�6:   $��
10,-49,0,0q�6U   $	��Top=�6i   �� 5      -   5         ��Name$
 tbID��5      $
��Left=�6,   $��23��6G   $��127,-56,0,0q�6S    �� TextWrapping$
 Wrap=�6h   $�� ��6|   $	��Top=�6�   $	��115��6�   $��2��6�   �� 5      $��Titulo:��6   $
��Left=�6.   $��
10,-21,0,0q�6I   $	��Top=�6]   �� 5      -   5        $ tbTitulo��5      $
��Left=�60   $��23��6K   $��127,-28,0,0q�6W   $
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $	��373��6�   $��2��6�    �� 
IsReadOnly . 6�   �� 5      $��Tipo:��6   $
��Left=�6,   $��10,7,0,0q�6G   $	��Top=�6Y    J� RenderTransformOrigin$ -1.907,0.758&�6q   �� 5      $��Região:��6   $
��Left=�6.   $��	10,34,0,0q�6I   $	��Top=�6\   �� 5      $��PSN URL(1):��6   $
��Left=�62   $��	10,73,0,0q�6M   $	��Top=�6`   �� 5      -   5        $ tbLink1��5      $
��Left=�6/   $��23��6J   $��
127,66,0,0q�6V   $
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��373��6�   $��2��6�    . 6�   �� 5      -   5         ��Name$ tbRegiao��5      $
��Left=�61   $��
127,28,0,0q�6L   $	��Top=�6`   $	��373��6x   $��2��6�   {�.  .@�5      �� $��EU��6'   �� 5      $��US��6'   �� 5       $��JP��6'   �� 5!      $��AS��6'   �� 5"      $��HK��6'   �� 5#      5%      �� 5&      $��Capa:��6   $
��Left=�6,   $��
10,229,0,0q�6G   $	��Top=�6[   �� 5'      -   5        $ tbImagem��5'      $
��Left=�60   $��23��6K   $��127,222,0,0q�6W   $
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $	��293��6�   $��2��6�    . 6�   �� 5(      -   5        $ tbTipo��5(      $
��Left=�6/   $��	127,1,0,0q�6J   $	��Top=�6]   $	��373��6u   $��2��6�   {�.  .@�5)      �� $	��PSP��6'   �� 5*      $��PSONE��6'   �� 5+      $��PSVITA��6'   �� 5,      $	��PS2��6'   �� 5-      $	��PS3��6'   �� 5.      $	��PSN��6'   �� 5/      $	��DLC��6'   �� 50      51      �� 52      -   5         ��Name$ 	btnEnviar��52      $��Salvar��60   $
��Left=�6A   $��130,275,0,0q�6\   $	��Top=�6q   $	��134��6�   $��1��6�   {�.  6�   �� 53      $��PSN URL(2):��6   $
��Left=�62   $��
10,101,0,0q�6M   $	��Top=�6a   �� 54      -	   5        $ tbLink2��54      $
��Left=�6/   $��23��6J   $��
127,94,0,0q�6V   $
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��373��6�   $��2��6�    . 6�   �� 55      $��PSN URL(3):��6   $
��Left=�62   $��
10,129,0,0q�6M   $	��Top=�6a   �� 56      -
   5        $ tbLink3��56      $
��Left=�6/   $��23��6J   $��127,122,0,0q�6V   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $	��373��6�   $��2��6�    . 6�   �� 57      $��PSN URL(4):��6   $
��Left=�62   $��
10,157,0,0q�6M   $	��Top=�6a   �� 58      -   5        $ tbLink4��58      $
��Left=�6/   $��23��6J   $��127,150,0,0q�6V   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $	��373��6�   $��2��6�    . 6�   �� 59      -   5        $ btnAbrir��59      $��Abrir��6/   $
��Left=�6?   $��189,222,0,0q�6Z   $	��Top=�6o   $��75��6�   $��23��6�   $��	16,2,16,5q�6�   $��1��6�   {�.  6�   �� 5:      $��PSN URL(5):��6   $
��Left=�62   $��
10,185,0,0q�6M   $	��Top=�6a   �� 5;      -   5        $ tbLink5��5;      $
��Left=�6/   $��23��6J   $��127,178,0,0q�6V   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $	��373��6�   $��2��6�    . 6�   �� 5<      -   5        $ btnCancelar��5<      $��Cancelar��62   $��33,275,459,117q�6E   $��1��6w   {�.  6�   �� 5=      -   5        $ 	btnApagar��5=      $��Apagar��60   $��10,275,134,117q�6A   {�.  6q   �� 5>      -   5        $ 
btnAlterar��5>      $��Alterar��61   $��172,-59,320,451q�6C   $��1��6u   {�.  6�   �� 5?      -   5        $ btnBusca��5?      $��Busca��6/   $��23��6?   $��11,-56,501,451q�6K   $��1��6c   $��	16,2,16,5q�6s   6�   5@      5A      5B      5C      5D      