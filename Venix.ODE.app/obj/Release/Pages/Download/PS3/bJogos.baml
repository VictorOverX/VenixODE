   M S B A M L   `   `   `  ����   VenixGFW-    'Venix.ODE.app.Pages.Download.PS3.bJogos   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.6.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  $	��820��$	��510��.��� $��0,0,0,14q�.+��� $��	0,0,0,-22q�.���� -     ��Name$  Panel��$	��150��$	��512��.+��� $��DOWNLOAD DE JOGOS��   Heading2#��[  �� $��Baixe seus jogos aqui��� $��0,45,0,0q�$	��393��.+��� $��Busca:��$
��Left=�$��	10,13,0,0q�$	��Top=��� -    ��Name$ tbBusca��$��23��$��61,10,482,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��277���� $��Filtro:��$
��Left=�$��
501,13,0,0q�$	��Top=�&   System.Windows.Controls.DataGrid  -     Name$ 	gridLista��$
��Left=�$��	10,46,0,0q�$	��Top=�$	��602��$	��306�� �� IsTextSearchEnabled . �� -   $��Baixar Jogo��$
��Left=�$��630,357,0,10q�$	��180���� ���	#FF4D4D4D$
��Left=�$	��342��$��
630,10,0,0q����	#FF424141$	��Top=�$	��180�� �� RadiusX$ 5�� �� RadiusY$ 5���� -    ��Name$ tbTipo��$
��Left=�$��
546,10,0,0q�$	��Top=�$��66��.@��� $	��PSP���� $��PSONE���� $��PSVITA���� $	��PS2���� $	��PS3���� $	��PSN���� $	��DLC���� �� -    ��Name$ imgCapa��$
��Left=�$	��160��$��
649,31,0,0q�$	��Top=�$	��143���� -   	 ��Name$	 tbTitulo��$
��Left=�$��649,222,0,0q�$
��Wrap=�$�� ��$	��Top=�$	��143��$��32��
 �� TextAlignment$
 Center=��� -   $
	 tbID��$
��Left=�$��649,259,0,0q�$
��Wrap=�$�� ��$	��Top=�$	��143��$��20��$
 Center=��� -	   $	 labelDownload��$
��Left=�$��649,284,0,0q�$
��Wrap=�$�� ��$	��Top=�$	��143��$��23���� -
   $	 tbTotal��$
��Left=�$��369,363,0,0q�$
��Wrap=�$��Total��$	��Top=�$��19��$��	Helvetica)��� -   $	 veloDonwload��$
��Left=�$��649,312,0,0q�$
��Wrap=�$�� ��$	��Top=�$	��143��$��23���� ���	#FF4D4D4D$
��Left=�$��26��$��
10,357,0,0q����	#FF424141$	��Top=�$	��345��$ 5��$ 5���� -   $	 
tbBaixando��$
��Left=�$��
20,363,0,0q�$
��Wrap=�$�� ��$	��Top=�$��54��� -    �Name$ progressBar1��$
��Left=�$��26��$��
79,357,0,0q�$	��Top=�$	��276�� J� IsHitTestVisible .  �� -   $	 	labelPerc��$
��Left=�$��187,363,0,0q�$
��Wrap=�$�� ��$	��Top=�$��57���� -    ��Name$ btnCancelar��$��Cancelar��$
��Left=�$��501,357,0,0q�$	��Top=�$	��120��