   M S B A M L   `   `   `  ����  
VenixTools"    Venix.ODE.app.Pages.CriarRap   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFirstFloor.ModernUI, Version=1.0.6.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  5      -   5        $	��820��5      $	��510��6+   .��5	      � $��0,0,0,14q�6   .+�5
   
   �� $��	0,0,0,-22q�6   .��5      �� -   5          ��Name$  Panel��5      $	��150��6(   $	��512��67   .+�5      �� $��COLEÇÃO DE RAP��6      Heading2#��[  62   �� 5      $4��.Lista de todos os raps em nosso banco de dados��6   � 5      $��0,45,0,0q�6   $	��393��6)   .+�5      �� -   5         ��Name$ tbBusca��5      $��Right=�6/   $��	0,6,133,0q�6K    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��602��6z   $��25��6�   $	��Top=�6�   6�   &   System.Windows.Controls.DataGrid  5      -   5          Name$ gridRap��5      $��10,107,10,53q�60    �� IsTextSearchEnabled . 6a   �� 5      -   5         ��Name$ btnCriarRap��5      $��	Criar RAP��62   $��674,358,0,10q�6F   $
��Left=�6\   $	��136��6w   6�   �� 5      $��BUSCAR:��6   $
��Left=�6.   $��	22,10,0,0q�6I   $	��Top=�6\   �� 5      -   5        $
 tbID��5       �� 
IsReadOnly . $
��Left=�6>   $��23��6Y   $��	83,47,0,0q�6e   $
 Wrap=�6x   $�� ��6�   $	��Top=�6�   $	��307��6�   �� 5      -   5        $ tbNome��5       . $
��Left=�6@   $��23��6[   $��	83,75,0,0q�6g   $
 Wrap=�6z   $�� ��6�   $	��Top=�6�   $	��307��6�   �� 5      -   5        $ 	tbRapNome��5       . $
��Left=�6C   $��23��6^   $��
493,47,0,0q�6j   $
 Wrap=�6~   $�� ��6�   $	��Top=�6�   $	��315��6�   �� 5      -	   5        $ 	tbRapCode��5       . $
��Left=�6C   $��23��6^   $��
493,75,0,0q�6j   $
 Wrap=�6~   $�� ��6�   $	��Top=�6�   $	��315��6�   �� 5      $��NOME:��6   $
��Left=�6,   $��	31,82,0,0q�6G   $	��Top=�6Z   �� 5      $	��ID:��6   $
��Left=�6*   $��	31,54,0,0q�6E   $	��Top=�6X   �� 5      $��	RAP NOME:��6   $
��Left=�60   $��
410,54,0,0q�6K   $	��Top=�6_   �� 5      $��	RAP CODE:��6   $
��Left=�60   $��
410,82,0,0q�6K   $	��Top=�6_   �� 5      -
   5        $ 
btnAlterar��5      {�.  $��Alterar��6C   $
��Left=�6U   $��	707,6,0,0q�6p   $	��Top=�6�   $	��101��6�   $��25��6�   6�   �� 5      -   5        $ 	btnApagar��5      $��Apagar��60   $
��Left=�6A   $��
10,357,0,0q�6\   $	��Top=�6p   $��75��6�   {�.  6�   5      5       5!      5"      5#      