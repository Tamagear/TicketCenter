   M S B A M L   `   `   `  ���� H  DTicketautomat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TicketautomatTicketautomat      Ticketautomat.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:Ticketautomat       � Icon$'  !Resources/Images/softwareIcon.png�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � Title$ TicketCenter��$	��900��$
��1600��.��� -    �Name$ 
ParentGrid��.+��� $%��Resources/Images/background.png��$
��1600��$	��875��$��0,25,0,0q�� -   $ Overlay��.+��� -    ��Name$  Rectangle_TopBarBackground�����#d1ff51$��25��$
��1600��$��Center=�$	��Top=��� -    ��Name$ Button_Close��$��X��$��25��$��25��$��Right=�$	��Top=�$��25, 0q� � Cursor$
 Hand���� -   $ Button_Version��$��i��$��25��$��25��$��Right=�$	��Top=�$��75, 0q�$
 Hand���� -   $ Button_MaintenanceLogin��$��Mitarbeiterlogin��$	��100��$��30��$��Right=�$��Bottom=�$��0q�$
 Hand���� -   	 ��Name$	 Label_TimeLeft��$$��Zeit bis zum Abbruch: Lädt...��$��25��$	��200��$��14(�$��Right=�$	��Top=�$��200, 0q��� -   $#	 Label_SoftwareTitleAndVersion��$��Lädt...��$��25��$��14(�$
��Left=�$	��Top=�$��25, 0q�$
��Bold"��� -	   $	 Label_DateTime��$��Lädt...��$��25��$
��Auto��$��12(�$
��Left=�$��Bottom=�$��0q�� -
   $ MainMenu��$y�Visible=�.+��� -   
 ��Name$
 Image_MainMenu_Map��$#��Resources/Images/map_full.jpg��$	��500��$	��500��$��Right=�$��Center=�$��
0,0,20,-30q��� -   $	 Label_MainMenu_MOTD��$��LÄDT...��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -   $$	 Label_MainMenu_TicketSpotlight��$��Lädt...��$��60��$	��500��$��36(�$��Right=�$	��Top=�$��
0,715,20,0q��� -   $ Button_MainMenu_GoToCart��$��Warenkorb
anzeigen��$��60��$	��200��$��20(�$��Right=�$	��Top=�$��40, 60q�$
 Hand���� -   $$ Button_MainMenu_ShowPriceTable��$��Preistabelle einsehen��$��60��$	��300��$��30(�$
��Left=�$��Bottom=�$��190,0,0,125q�$
 Hand��� -   $ MainMenu_BuyButtonsGrid��$��Center=�$
��Left=� � ColumnDefinitions �� $��350px ��� $
��Auto � � RowDefinitions �� $	��250 ��� $
��Auto �.+��� -   $% Button_MainMenu_BuyButton_Adult��$��Erwachsener
Lädt...��$	��300��$	��200��$��40(�$��0��$��0��$��0q�$
 Hand���� -   $% Button_MainMenu_BuyButton_Child��$��Kind
Lädt...��$	��300��$	��200��$��40(�$��1��$��0��$��0q�$
 Hand���� -   $) #Button_MainMenu_BuyButton_Pensioner��$��Senior
Lädt...��$	��300��$	��200��$��40(�$��0��$��1��$��0q�$
 Hand���� -   $' !Button_MainMenu_BuyButton_Reduced��$��Ermäßigt
Lädt...��$	��300��$	��200��$��40(�$��1��$��1��$��0q�$
 Hand��� -   $ 
PriceTable��$y�	Collapsed=�$��0,25,0,0q�.+��� $+��%Resources/Images/background_table.png��$��0,0,25,0q��� -   $% Rectangle_PriceTable_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -   $	 Label_PriceTable_Title��$��AKTUELLE TICKETPREISE��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -   $	 Label_PriceTable_Hint��$����Die Ticketpreise können sich innerhalb eines Tages mehrfach ändern.
Kontaktieren Sie bei weiteren Fragen unseren Kundenservice.��$��Center=�$��Bottom=�$��0, 135q�$��20(��� -   $ 	 Label_PriceTable_AsOfToday��$��Stand: Lädt...��$
��Left=�$	��Top=�$��100, 155q�$��20(��� -   $$ Button_PriceTable_GoBackButton��$��Zurück��$��50(�$��80��$	��400��$��Center=�$��Bottom=�$
��0,30q�$
 Hand��� -   $ PriceTable_Table��$��	25,0,25,0q� �� $��7* ��� $��7* ��� $��7* ��� $��7* ��� $��7* ��� $��7* ��� $��7* � �� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* �.+��� -   $(	 "Label_PriceTable_Table_TariffLevel��$��
Tarifstufe��$��1��$��2��$��30(�$
��Bold"��� -   $&	  Label_PriceTable_Table_Age_Child��$��Kinder��$��2��$��2��$��30(�$
��Bold"��� -   $(	 "Label_PriceTable_Table_Age_Reduced��$��
Ermäßigt��$��3��$��2��$��30(�$
��Bold"��� -   $&	  Label_PriceTable_Table_Age_Adult��$��
Erwachsene��$��4��$��2��$��30(�$
��Bold"��� -    $*	 $Label_PriceTable_Table_Age_Pensioner��$��Senioren��$��5��$��2��$��30(�$
��Bold"��� -!   $%	 Label_PriceTable_Table_Tariff_A��$��A��$��1��$��3��$��30(�$
��Bold"��� -"   $%	 Label_PriceTable_Table_Tariff_B��$��B��$��1��$��4��$��30(�$
��Bold"��� -#   $%	 Label_PriceTable_Table_Tariff_C��$��C��$��1��$��5��$��30(�$
��Bold"��� -$   $$	 Label_PriceTable_Table_Child_A��$��Lädt...��$��2��$��3��$��30(��� -%   $$	 Label_PriceTable_Table_Child_B��$��Lädt...��$��2��$��4��$��30(��� -&   $$	 Label_PriceTable_Table_Child_C��$��Lädt...��$��2��$��5��$��30(��� -'   $&	  Label_PriceTable_Table_Reduced_A��$��Lädt...��$��3��$��3��$��30(��� -(   $&	  Label_PriceTable_Table_Reduced_B��$��Lädt...��$��3��$��4��$��30(��� -)   $&	  Label_PriceTable_Table_Reduced_C��$��Lädt...��$��3��$��5��$��30(��� -*   $$	 Label_PriceTable_Table_Adult_A��$��Lädt...��$��4��$��3��$��30(��� -+   $$	 Label_PriceTable_Table_Adult_B��$��Lädt...��$��4��$��4��$��30(��� -,   $$	 Label_PriceTable_Table_Adult_C��$��Lädt...��$��4��$��5��$��30(��� --   $(	 "Label_PriceTable_Table_Pensioner_A��$��Lädt...��$��5��$��3��$��30(��� -.   $(	 "Label_PriceTable_Table_Pensioner_B��$��Lädt...��$��5��$��4��$��30(��� -/   $(	 "Label_PriceTable_Table_Pensioner_C��$��Lädt...��$��5��$��5��$��30(�� -0   $ BuyMenu��$y�	Collapsed=�.+��� -1   $" Rectangle_BuyMenu_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -2   $* $Rectangle_BuyMenu_BelowTicketOptions�����#d1ff51$��7��$	��950��$��Center=�$��Center=�$
��0, 0q��� -3   $	 Label_BuyMenu_Title��$��TICKET KAUFEN��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -4   $1	 +Label_BuyMenu_TicketSelection_CheapestTitle��$��GÜNSTIGSTE OPTION��$��30(�$
��Bold"�$
��Left=�$��Bottom=�$��100,0,0,360q��� -5   $0	 *Label_BuyMenu_TicketSelection_FastestTitle��$��WENIGSTE STATIONEN��$��30(�$
��Bold"�$��Right=�$��Bottom=�$��100,0,100,360q��� -6   $ Button_BuyMenu_GoToCart��$��Warenkorb
anzeigen��$��60��$	��200��$��20(�$��Right=�$	��Top=�$��
0,60,235,0q�$
 Hand���� -7   $! Button_BuyMenu_GoBackButton��$��Zurück��$��30��$	��200��$��20(�$��Right=�$	��Top=�$��	0,90,30,0q�$
 Hand��� -8   $ BuyMenu_TicketOptions��$��250,250q� �� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � �� $��1* ��� $��2* �.+��� -9   $3	 -Label_BuyMenu_TicketOptions_TariffOptionTitle��$��TARIFOPTION��$��0��$��0��$��30(�$
��Bold"��� -:   $,	 &Label_BuyMenu_TicketOptions_StartTitle��$��START��$��1��$��0��$��30(�$
��Bold"��� -;   $2	 ,Label_BuyMenu_TicketOptions_DestinationTitle��$
��ZIEL��$��2��$��0��$��30(�$
��Bold"�� -<   $) #BuyMenu_TicketOptions_TariffOptions��$��0��$��1�� �� $��4* ��� $��4* ��� $��4* ��� $��4* �.+��� -=   $5 /Button_BuyMenu_TicketOptions_TariffOption_Adult��$��ERWACHSENER��$��20(�$
 Hand��$��0���� ->   $5 /Button_BuyMenu_TicketOptions_TariffOption_Child��$
��KIND��$��20(�$
 Hand��$��1���� -?   $9 3Button_BuyMenu_TicketOptions_TariffOption_Pensioner��$��SENIOR��$��20(�$
 Hand��$��2���� -@   $7 1Button_BuyMenu_TicketOptions_TariffOption_Reduced��$��
ERMÄßIGT��$��20(�$
 Hand��$��3���� -A   $. (Button_BuyMenu_TicketOptions_StartButton��$��SIMMERSTEIN��
��� ���$��30(�$
 Hand��$��1��$��1���� -B   $4 .Button_BuyMenu_TicketOptions_DestinationButton��$��ECHSENSTADT��
����d�$��30(�$
 Hand��$��2��$��1��� -C   $ BuyMenu_TicketSelection��$��100,50q� �� $��2* ��� $��100px ��� $��2* �.+��� -D   $- 'Button_BuyMenu_TicketSelection_Cheapest��$"��GÜNSTIGSTE OPTION
BESTELLEN��$	��300��$��Bottom=�$��0��$
 Hand���� -E   $, &Button_BuyMenu_TicketSelection_Fastest��$"��WENIGSTE STATIONEN
BESTELLEN��$	��300��$��Bottom=�$��2��$
 Hand���� $��Bottom=�$
��Left=�$��0��$��60��$��10q�.+��  �� $��40 ��� $��20 �.+��� -F   $)	 #Label_BuyMenu_TicketAmount_Cheapest��$��1��$��Center=�$��Center=�$��40��$��40��$
��Left=�
�������$��Bottom=�$��0���� -G   $3 -Button_BuyMenu_TicketAmount_Cheapest_Increase��$��+��$��20��$��20��$	��Top=�$��Right=�$
 Hand��$��1���� -H   $3 -Button_BuyMenu_TicketAmount_Cheapest_Decrease��$��-��$��20��$��20��$��Bottom=�$��Right=�$
 Hand��$��1���� $��Bottom=�$
��Left=�$��2��$��60��$��10q�.+��  �� $��40 ��� $��20 �.+��� -I   $(	 "Label_BuyMenu_TicketAmount_Fastest��$��1��$��Center=�$��Center=�$��40��$��40��$
��Left=�
�������$��Bottom=�$��0���� -J   $2 ,Button_BuyMenu_TicketAmount_Fastest_Increase��$��+��$��20��$��20��$	��Top=�$��Right=�$
 Hand��$��1���� -K   $2 ,Button_BuyMenu_TicketAmount_Fastest_Decrease��$��-��$��20��$��20��$��Bottom=�$��Right=�$
 Hand��$��1��� -L   $% BuyMenu_TicketOptions_TicketMap��$��0,-5,0,5q�$y�	Collapsed=� � 	Resources    ButtonStyleNoHighlighting �� 
TargetType �� Property �� Value �� 
TargetType'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState��%�   &	        �� " ��.���  � �� �� " ��.R�� #��z��   CommonStates��.   
Normal��  	MouseOver��.+��� -M   $6
 0Image_BuyMenu_TicketOptions_TicketMap_Background��$#��Resources/Images/map_full.jpg��$	��500��$��619,117,481,-117q��� -N   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_0��
������ 
������ $
 Hand��$��830,564,719,315q�#��[  �� -O   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_1��
������ 
������ $
 Hand��$��876,590,684,295q�#��[  �� -P   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_2��
������ 
������ $
 Hand��$��849,614,711,269q�#��[  �� -Q   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_3��
������ 
������ $
 Hand��$��865,532,666,349q�#��[  �� -R   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_4��
������ 
������ $
 Hand��$��819,519,748,364q�#��[  �� -S   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_5��
������ 
������ $
 Hand��$��804,553,770,333q�#��[  �� -T   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_6��
������ 
������ $
 Hand��$��781,524,786,364q�#��[  �� -U   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_7��
������ 
������ $
 Hand��$��767,571,796,302q�#��[  �� -V   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_8��
������ 
������ $
 Hand��$��792,604,759,269q�#��[  �� -W   $6 0Button_BuyMenu_TicketOptions_TicketMap_Station_9��
������ 
������ $
 Hand��$��845,658,716,232q�#��[  �� -X   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_10��
������ 
������ $
 Hand��$��887,668,690,222q�#��[  �� -Y   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_11��
������ 
������ $
 Hand��$��915,614,649,240q�#��[  �� -Z   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_12��
������ 
������ $
 Hand��$��926,584,643,302q�#��[  �� -[   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_13��
������ 
������ $
 Hand��$��933,514,614,368q�#��[  �� -\   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_14��
������ 
������ $
 Hand��$��873,484,659,398q�#��[  �� -]   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_15��
������ 
������ $
 Hand��$��811,460,738,423q�#��[  �� -^   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_16��
������ 
������ $
 Hand��$��786,488,770,398q�#��[  �� -_   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_17��
������ 
������ $
 Hand��$��753,476,819,413q�#��[  �� -`   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_18��
������ 
������ $
 Hand��$��702,501,833,380q�#��[  �� -a   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_19��
������ 
������ $
 Hand��$��717,644,845,244q�#��[  �� -b   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_20��
������ 
������ $
 Hand��$��760,654,760,222q�#��[  �� -c   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_21��
������ 
������ $
 Hand��$��842,696,696,187q�#��[  �� -d   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_22��
������ 
������ $
 Hand��$��912,668,641,187q�#��[  �� -e   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_23��
������ 
������ $
 Hand��$��964,663,593,206q�#��[  �� -f   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_24��
������ 
������ $
 Hand��$��956,611,610,273q�#��[  �� -g   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_25��
������ 
������ $
 Hand��$��982,579,572,294q�#��[  �� -h   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_26��
������ 
������ $
 Hand��$��1001,476,550,398q�#��[  �� -i   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_27��
������ 
������ $
 Hand��$��956,476,610,407q�#��[  �� -j   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_28��
������ 
������ $
 Hand��$��915,455,651,426q�#��[  �� -k   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_29��
������ 
������ $
 Hand��$��879,428,677,450q�#��[  �� -l   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_30��
������ 
������ $
 Hand��$��837,433,738,445q�#��[  �� -m   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_31��
������ 
������ $
 Hand��$��760,420,800,466q�#��[  �� -n   $7 1Button_BuyMenu_TicketOptions_TicketMap_Station_32��
������ 
������ $
 Hand��$��727,439,829,447q�#��[  � -o   $ ShoppingCart��$y�	Collapsed=�.+��� -p   $' !Rectangle_ShoppingCart_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -q   $	 Label_ShoppingCart_Title��$��	WARENKORB��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -r   $	 Label_ShoppingCart_Sum��$��Preis insgesamt: Lädt...��$
��Bold"�$��50(�$��Right=�$	��Top=�$��
0,538,75,0q��� -s   $&  Button_ShoppingCart_GoBackButton��$��Zurück��$��30��$	��200��$��20(�$��Right=�$	��Top=�$��	0,90,30,0q�$
 Hand���� -t   $&  Button_ShoppingCart_PayNowButton��$��Jetzt bezahlen��$��60��$	��250��$��20(�$��Right=�$	��Top=�$��0,634,235,0q�$
 Hand���� -u    ��Name$. (ScrollViewer_ShoppingCart_TicketOverview��$��Disabled=�$	��900��$	��725��$��Bottom=�$
��Left=�$��20, 0q�.���� .+���  �� Orientation$ 
Horizontal=�.+��� -v   $" Button_ShoppingCart_Decrease��$��-��$��33��$��33��$��	400,0,0,0q�{�.  �� -w   $" Button_ShoppingCart_Increase��$��+��$��33��$��33��{�.  �� -x   $  Button_ShoppingCart_Remove��$��X��$��33��$��33��{�.  �� -y   $��List_ShoppingCart��$	��800��$��20q�$
��Left=����� .R��� $ 
Horizontal=�$	��785������ *Item3+.+��� $��
15,10,5,10q�$��Startstation:���� $��	0,10,0,10q����� * Item1.StartStation.StationName+�� $��
20,10,5,10q�$��Endstation:���� $��	0,10,0,10q����� *%#Item1.TargetDestination.StationName+�� $��
20,10,5,10q�$��Altersgruppe:���� $��	0,10,0,10q����� * Item1.PriceEntry.AgeTypeString+�� $��
20,10,5,10q�$��Tarifstufe:���� $��	0,10,0,10q����� *" Item1.PriceEntry.TariffLevelChar+�� $��
20,10,5,10q�$��Preis:���� $��	0,10,0,10q����� *Item1.PriceEntry.Price+ �� StringFormat$ {0:F2}���� $��	0,10,0,10q�$	��€���� $��
20,10,5,10q�$��Menge:���� $��	0,10,0,10q����� *Item2+� -z   $ AddedTicket��$y�	Collapsed=�.+��� -{   $&  Rectangle_AddedTicket_Background�� J� Opacity$
 0.95Q�
���   �$	��875��$��Stretch=�$��Bottom=�$��0q��� -|   $&  Rectangle_AddedTicket_Foreground��
�������$	��835��$	��500��$��Right=�$��Bottom=�$��20,20q��� -}   $&  Rectangle_AddedTicket_BelowTitle��
���   �$��3��$	��450��$��Right=�$	��Top=�$��50, 100q��� -~   $	 Label_AddedTicket_Title��$��TICKET HINZUGEFÜGT��$��40(�$
��Bold"�$��Right=�$	��Top=�$��50, 40q��� -   $"	 Label_AddedTicket_TicketType��$5��/Ticket, Tarifstufe Lädt...,
Lädt..., Lädt...��$��20(�$	��300��$	��250��$��Right=�$	��Top=�$��0,128,309,0q��� -�   $#	 Label_AddedTicket_TicketCount��$��Anzahl: Lädt..��$��20(�$	��300��$��Right=�$	��Top=�$��
0,128,50,0q��� -�   $% Button_AddedTicket_CancelButton��$��	Abbrechen��$��Bottom=�$��Right=�$	��300��$��50��$��30(�$��0,0,125,292q�$
 Hand���� -�   $+ %Button_AddedTicket_ShoppingCartButton��$��Zum Warenkorb��$��Bottom=�$��Right=�$	��480��$��70��$��40(�$��
0,0,26,194q�$
 Hand���� -�   $' !Button_AddedTicket_ContinueButton��$��Weiter Einkaufen��$��Bottom=�$��Right=�$	��480��$��70��$��40(�$��	0,0,30,98q�$
 Hand��� -�   $ PayMenu��$y�	Collapsed=�.+��� -�   $" Rectangle_PayMenu_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -�   $# Rectangle_PayMenu_BelowPaySum��
���   �$��7��$
��1050��$��Center=�$	��Top=�$��	0,512,0,0q��� -�   $	 Label_PayMenu_Title��$��BEZAHLEN��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -�   $	 Label_PayMenu_PayTitle��$��NOCH ZU ZAHLEN:��$��60(�$
��Bold"�$��Center=�$	��Top=�$��	0,252,0,0q��� -�   $	 Label_PayMenu_PaySum��$��Lädt...��$	��120(�$
��Bold"�$��Center=�$	��Top=�$��	0,342,0,0q��� -�   $	 Label_PayMenu_PayHint��$+��%Bitte werfen Sie die obige Summe ein.��$��30(�$
��Bold"�$��Center=�$	��Top=�$��	0,524,0,0q��� -�   $"	 Label_PayMenu_PayMethodTitle��$��Einwurf:��$��30(�$
��Bold"�$
��Left=�$	��Top=�$��
75,634,0,0q��� -�   $! Button_PayMenu_GoBackButton��$��Zurück��$��30��$	��200��$��20(�$��Right=�$	��Top=�$��	0,90,30,0q�$
 Hand��� -�   $ PayMenu_PayButtonGrid��$��50,689,50,71q� �� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* � �� $��2* ��� $��2* �.+��� -�   $* $PayMenu_PayButtonGrid_PayButton_0005��$��0,05€��$��40(�$��0��$��0��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_0010��$��0,10€��$��40(�$��0��$��1��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_0020��$��0,20€��$��40(�$��0��$��2��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_0050��$��0,50€��$��40(�$��0��$��3��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_0100��$��1,00€��$��40(�$��0��$��4��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_0200��$��2,00€��$��40(�$��0��$��5��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_0500��$��5,00€��$��40(�$��1��$��1��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_1000��$��10,00€��$��40(�$��1��$��2��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_2000��$��20,00€��$��40(�$��1��$��3��$
 Hand���� -�   $* $PayMenu_PayButtonGrid_PayButton_5000��$��50,00€��$��40(�$��1��$��4��$
 Hand��� -�   $ PDFExportMenu��$y�	Collapsed=�.+��� -�   $( "Rectangle_PDFExportMenu_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -�   $/ )Rectangle_PDFExportMenu_BelowExportDialog��
���   �$��7��$
��1050��$��Center=�$	��Top=�$��	0,410,0,0q��� -�   $	 Label_PDFExportMenu_Title��$$��VIELEN DANK FÜR IHREN EINKAUF��$	��130��$
��1325��$��70(�$
��Bold"�$
��Left=�$	��Top=�$��	40,56,0,0q��� -�   $(	 "Label_PDFExportMenu_PleaseWaitText��$:��4Bitte warten Sie, während ihr Ticket gedruckt wird.��$��60(�$
��Bold"�$��Center=�$	��Top=�$��	0,252,0,0q��� -�   $*	 $Label_PDFExportMenu_ExportDialogText��$>��8Möchten Sie Ihre Bestellung als .PDF-Datei exportieren?��$��40(�$
��Bold"�$��Center=�$	��Top=�$��	0,342,0,0q��� -�   $' !Button_PDFExportMenu_GoBackButton��$��Zum Hauptmenü��$��30��$	��200��$��20(�$��Right=�$	��Top=�$��	0,90,30,0q�$
 Hand���� -�   $' !Button_PDFExportMenu_ExportButton��$��Exportieren��$��Center=�$	��Top=�$��50(�$	��400��$��70��$��	0,496,0,0q�$
 Hand��� -�   $ AdminDashboard��$y�	Collapsed=�.+��� -�   $) #Rectangle_AdminDashboard_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -�   $/ )Rectangle_AdminDashboard_BehindScrollView��
�������$	��725��$	��700��$��Right=�$��Bottom=��� -�   $ 	 Label_AdminDashboard_Title��$��MITARBEITER-DASHBOARD��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -�   $#	 Label_AdminDashboard_LogTitle��$	��LOG��$��50(�$
��Bold"�$
��Left=�$	��Top=�$��792,162,0,0q��� -�   $( "Button_AdminDashboard_GoBackButton��$��Abmelden��$��30��$	��200��$��20(�$��Right=�$	��Top=�$��	0,90,30,0q�$
 Hand��� -�   $' !AdminDashboard_AdminButtonOptions��$��100,230,900,170q� �� $��4* ��� $��4* ��� $��4* ��� $��4* �.+��� -�   $9 3Button_AdminDashboard_AdminButtonOptions_Statistics��$��Statistiken��$��60(�$��0���� -�   $@ :Button_AdminDashboard_AdminButtonOptions_SavingsManagement��$��Speicherverwaltung��$��60(�$��1���� -�   $= 7Button_AdminDashboard_AdminButtonOptions_DisableMachine��$��Automat abschalten��$��60(�$��2���� -�   $; 5Button_AdminDashboard_AdminButtonOptions_ChangePrices��$��Preise ändern��$��60(�$��3���� -�   $&  ScrollViewer_AdminDashboard_Logs��$��Disabled=�$	��700��$	��725��$��Bottom=�$��Right=�.���� $	��Top=�$
��Left=�$��0q�
�������.+��� -�   $��	List_Logs��$��20q�$
��Left=�$	��640��
����������� .R��� $ 
Horizontal=�.+��� $��	0,10,0,10q����� *Date+�� $��
20,10,0,10q����� *Author+�� $��
20,10,0,10q����� *	Content+� -�   $ AdminSavingsManagement��$y�	Collapsed=�.+��� -�   $1 +Rectangle_AdminSavingsManagement_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -�   $= 7Rectangle_AdminSavingsManagement_BehindCurrentFillState��
�������$	��725��$	��700��$��Right=�$��Bottom=��� -�   $(	 "Label_AdminSavingsManagement_Title��$��SPEICHERVERWALTUNG��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -�   $3	 -Label_AdminSavingsManagement_CurrentFillState��$	��700��$	��725��$K��ETickets übrig: 1000 Stück
0,05€: 76 Stück
0,10€: 35 Stück
...��$��40(�$��Right=�$��Bottom=��� -�   $0 *Button_AdminSavingsManagement_GoBackButton��$��Zurück��$��30��$	��200��$��20(�$��Right=�$	��Top=�$��	0,90,30,0q�$
 Hand��� -�   $/ )AdminSavingsManagement_AdminButtonOptions��$��100,280,900,90q� �� $��4* ��� $��4* ��� $��4* ��� $��4* �.+��� -�   $F @Button_AdminSavingsManagement_AdminButtonOptions_FillTicketPaper��$��	0,20,0,20q�$��Papierspeicher auffüllen��$��50(�$��0��]� $��Center=�$��0q�$��1��.+��� -�   $@ :Button_AdminSavingsManagement_AdminButtonOptions_FillCoins��$��0,5,0,10q�$��Münzen auffüllen��$��50(�$��2��$	��Top=��� $��	20,0,0,20q�$��Right=�$	��Top=�$��Right=�$��40��$��60������  �� ElementName$ slValue_coin�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=��� -�    ��Name$ slValue_coin�� � Maximum$	 150Q� �� TickPlacement$ BottomRight=� �� TickFrequency$ 5Q� �� IsSnapToTickEnabled .   � Value$  75Q�]� $��Center=�$��0q�$��2��.+��� -�   $@ :Button_AdminSavingsManagement_AdminButtonOptions_FillBills��$��0,5,0,10q�$��Scheine auffüllen��$��50(�$��2��$	��Top=��� $��	20,0,0,20q�$��Right=�$	��Top=�$��Right=�$��40��$��60������ $ slValue_bill��$ Value�$ PropertyChanged=��� -�   $ slValue_bill��$	 200Q�$ BottomRight=�$ 5Q� . $  75Q�� -�   $ AdminChangePricesMenu��$y�	Collapsed=�$��0, 25, 0, 0q�.+��� $+��%Resources/Images/background_table.png��$��0, 0, 25, 0q��� -�   $0 *Rectangle_AdminChangePricesMenu_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -�   $'	 !Label_AdminChangePricesMenu_Title��$��TICKETPREISE ANPASSEN��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -�   $6 0Button_AdminChangePricesMenu_SaveAndGoBackButton��$��Speichern und zurück��$��40(�$��80��$	��400��$��Center=�$��Bottom=�$
��0,30q�$
 Hand��� -�   $! AdminChangePricesMenu_Table��$��	25,0,25,0q� �� $��7* ��� $��7* ��� $��7* ��� $��7* ��� $��7* ��� $��7* ��� $��7* � �� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* ��� $��8* �.+��� -�   $3	 -Label_AdminChangePricesMenu_Table_TariffLevel��$��
Tarifstufe��$��1��$��2��$��30(�$
��Bold"��� -�   $1	 +Label_AdminChangePricesMenu_Table_Age_Child��$��Kinder��$��2��$��2��$��30(�$
��Bold"��� -�   $3	 -Label_AdminChangePricesMenu_Table_Age_Reduced��$��
Ermäßigt��$��3��$��2��$��30(�$
��Bold"��� -�   $1	 +Label_AdminChangePricesMenu_Table_Age_Adult��$��
Erwachsene��$��4��$��2��$��30(�$
��Bold"��� -�   $5	 /Label_AdminChangePricesMenu_Table_Age_Pensioner��$��Senioren��$��5��$��2��$��30(�$
��Bold"��� -�   $0	 *Label_AdminChangePricesMenu_Table_Tariff_A��$��A��$��1��$��3��$��30(�$
��Bold"��� -�   $0	 *Label_AdminChangePricesMenu_Table_Tariff_B��$��B��$��1��$��4��$��30(�$
��Bold"��� -�   $0	 *Label_AdminChangePricesMenu_Table_Tariff_C��$��C��$��1��$��5��$��30(�$
��Bold"��� -�   $/	 )Label_AdminChangePricesMenu_Table_Child_A��$��Lädt...��$��2��$��3��$��30(��� -�   $/	 )Label_AdminChangePricesMenu_Table_Child_B��$��Lädt...��$��2��$��4��$��30(��� -�   $/	 )Label_AdminChangePricesMenu_Table_Child_C��$��Lädt...��$��2��$��5��$��30(��� -�   $1	 +Label_AdminChangePricesMenu_Table_Reduced_A��$��Lädt...��$��3��$��3��$��30(��� -�   $1	 +Label_AdminChangePricesMenu_Table_Reduced_B��$��Lädt...��$��3��$��4��$��30(��� -�   $1	 +Label_AdminChangePricesMenu_Table_Reduced_C��$��Lädt...��$��3��$��5��$��30(��� -�   $/	 )Label_AdminChangePricesMenu_Table_Adult_A��$��Lädt...��$��4��$��3��$��30(��� -�   $/	 )Label_AdminChangePricesMenu_Table_Adult_B��$��Lädt...��$��4��$��4��$��30(��� -�   $/	 )Label_AdminChangePricesMenu_Table_Adult_C��$��Lädt...��$��4��$��5��$��30(��� -�   $3	 -Label_AdminChangePricesMenu_Table_Pensioner_A��$��Lädt...��$��5��$��3��$��30(��� -�   $3	 -Label_AdminChangePricesMenu_Table_Pensioner_B��$��Lädt...��$��5��$��4��$��30(��� -�   $3	 -Label_AdminChangePricesMenu_Table_Pensioner_C��$��Lädt...��$��5��$��5��$��30(��� -�   $0 *Button_AdminChangePricesMenu_Table_Child_A��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��2��$��3���� -�   $0 *Button_AdminChangePricesMenu_Table_Child_B��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��2��$��4���� -�   $0 *Button_AdminChangePricesMenu_Table_Child_C��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��2��$��5���� -�   $2 ,Button_AdminChangePricesMenu_Table_Reduced_A��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��3��$��3���� -�   $2 ,Button_AdminChangePricesMenu_Table_Reduced_B��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��3��$��4���� -�   $2 ,Button_AdminChangePricesMenu_Table_Reduced_C��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��3��$��5���� -�   $0 *Button_AdminChangePricesMenu_Table_Adult_A��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��4��$��3���� -�   $0 *Button_AdminChangePricesMenu_Table_Adult_B��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��4��$��4���� -�   $0 *Button_AdminChangePricesMenu_Table_Adult_C��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��4��$��5���� -�   $4 .Button_AdminChangePricesMenu_Table_Pensioner_A��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��5��$��3���� -�   $4 .Button_AdminChangePricesMenu_Table_Pensioner_B��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��5��$��4���� -�   $4 .Button_AdminChangePricesMenu_Table_Pensioner_C��$��Ändern��$��50��$��50��$	��Top=�$��Right=�$
��25,0q�$��5��$��5��� -�   $* $AdminChangePricesMenu_ChangeDialogue��$y�	Collapsed=�.+��� -�   $? 9Rectangle_AdminChangePricesMenu_ChangeDialogue_Background��$	 0.5Q�
���   �$	��875��$��Stretch=�$��Bottom=�$��0q��� -�   $A ;Rectangle_AdminChangePricesMenu_ChangeDialogue_Middleground��$
 0.95Q�
���   �$	��300��$	��900��$��Stretch=�$��Center=�$��0q��� -�   $? 9Rectangle_AdminChangePricesMenu_ChangeDialogue_Foreground��
�������$	��100��$��28��$��Stretch=�$��Center=�$��0q��� -�   $6	 0Label_AdminChangePricesMenu_ChangeDialogue_Title��$��NEUEN PREIS EINGEBEN��
�������$	��130��$
��1325��$��50(�$
��Bold"�$
��Left=�$	��Top=�$��518,339,0,0q��� -�   $8	 2Label_AdminChangePricesMenu_ChangeDialogue_Content��$	��€��
�������$��30(�$
��Bold"�$
��Left=�$	��Top=�$��855,423,0,0q��� -�   $= 7Button_AdminChangePricesMenu_ChangeDialogue_CloseButton��$��OK��$	��250��$��30��$	��Top=�$��675,469,675,0q�$��20(�$
 Hand���� -�   ! ��Name$,! &TextBox_AdminChangePricesMenu_NewPrice��$	��100��$��28��$��19(�$
��1,50��� -�   $ AdminDisableMachine��$y�	Collapsed=�.+��� -�   $. (Rectangle_AdminDisableMachine_Background��$	 0.5Q�
���   �$	��875��$��Stretch=�$��Bottom=�$��0q��� -�   $0 *Rectangle_AdminDisableMachine_Middleground��$
 0.95Q�
���   �$	��300��$	��900��$��Stretch=�$��Center=�$��0q��� -�   $%	 Label_AdminDisableMachine_Title��$��AUTOMATEN
ABSCHALTEN?��
�������$	��130��$
��1325��$��50(�$
��Bold"�$
��Left=�$	��Top=�$��638,300,0,0q��� -�   $/ )Button_AdminDisableMachine_ShutDownButton��$��
Abschalten��$	��250��$��40��$	��Top=�$��675,469,675,0q�$��20(�$
 Hand���� -�   $- 'Button_AdminDisableMachine_CancelButton��$��Zurück��$	��250��$��40��$	��Top=�$��675,514,675,0q�$��20(�$
 Hand��� -�   $ AdminStatistics��$y�	Collapsed=�.+��� -�   $* $Rectangle_AdminStatistics_BelowTitle�����#d1ff51$��7��$
��1450��$��Center=�$	��Top=�$��0, 150q��� -�   $!	 Label_AdminStatistics_Title��$��STATISTIKEN��$	��130��$
��1325��$��90(�$
��Bold"�$
��Left=�$	��Top=�$��	40,40,0,0q��� -�   $-	 'Label_AdminStatistics_CurrentStatistics��$��Alle / Zeitspanne��$��40(�$
��Left=�$	��Top=�$��
40,722,0,0q��� -�   $) #Button_AdminStatistics_GoBackButton��$��Zurück��$��30��$	��200��$��20(�$��Right=�$	��Top=�$��	0,90,30,0q�$
 Hand���� -�   $+ %Button_AdminStatistics_TimespanButton��$��Zeitraum��$��50��$	��200��$��25(�$
 Hand��$��223,790,1177,60q��� -�   $/ )Button_AdminStatistics_PerTimeOfDayButton��$��Pro Tageszeit��$��50��$	��200��$��25(�$
 Hand��$��428,790,972,60q��� -�   $( "Button_AdminStatistics_GraphButton��$��Graph��$��50��$	��200��$��25(�$
 Hand��$��633,790,767,60q��� -�   $( "Button_AdminStatistics_TableButton��$��Tabelle��$��50��$	��200��$��25(�$
 Hand��$��838,790,562,60q�� -�   $ AdminStatistics_BarChart��$��Center=�$��Center=�$y�	Collapsed=�.+��� -�   " ��Name$+" %Canvas_AdminStatistics_BarChartCanvas��� -�   $ AdminStatistics_Table��$y�	Collapsed=�.+��� $��5��$	��500��$��Center=�$��Center=�
���"�"��� -�   # ��Name$3# -TextBlock_AdminStatistics_Table_LeftTextBlock��$
��Left=�$��Center=�$	��700��$	��500��$��15(�$��50, 0q�$ �� TextAlignment$$ Right=��� -�   $4# .TextBlock_AdminStatistics_Table_RightTextBlock��$��Right=�$��Center=�$	��700��$	��500��$��15(�$��50, 0q�$
$ Left=�� -�   $) #AdminStatistics_TimeTypeButtonsGrid��$��58,790,1382,60q� �� $��4* ��� $
��10px ��� $��4* ��� $
��10px ��� $��4* ��� $
��10px ��� $��4* ��� $
��10px �.+��� -�   $/ )Button_AdminStatistics_TimeTypeButton_Day��$��0��$��D��$
 Hand���� -�   $0 *Button_AdminStatistics_TimeTypeButton_Week��$��2��$��W��$
 Hand���� -�   $1 +Button_AdminStatistics_TimeTypeButton_Month��$��4��$��M��$
 Hand���� -�   $4 .Button_AdminStatistics_TimeTypeButton_Complete��$��6��$��G��$
 Hand��� -�   $ DisabledScreen��$y�	Collapsed=�.+��� -   $( "Rectangle_DisableScreen_Background��$
 0.95Q�
���   �$	��875��$��Stretch=�$��Bottom=�$��0q��� -  $
 Image_DisableScreen_Lock��$#��Resources/Images/lockIcon.png��$	��310��$	��303��$��645,226,645,370q��� -  $.
 (Image_DisableScreen_NotAllBoxesFilledOut��$:��4Resources/Images/adminLogin_notAllBoxesFilledOut.png��$y�	Collapsed=�$	��250��$��1105,311,245,-311q��� -  $#
 Image_DisableScreen_WrongData��$/��)Resources/Images/adminLogin_wrongData.png��$y�	Collapsed=�$	��250��$��1105,259,245,-259q��� -  $	 Label_DisableScreen_Title��$[��UDer Automat ist aktuell gesperrt.
Bitte wenden Sie sich an einen unserer Mitarbeiter.��
�������$
��Bold"�$��Bottom=�$��Center=�$��45(�$��0, 250q��� -  $"	 Label_DisableScreen_Username��$��Benutzername��
�������$��15(�$
��Bold"�$
��Left=�$	��Top=�$��372,680,0,0q��� -  $"	 Label_DisableScreen_Password��$��Passwort��
�������$��15(�$
��Bold"�$
��Left=�$	��Top=�$��411,710,0,0q��� -  $&  Button_DisableScreen_LoginButton��$��
Entsperren��$��Right=�$��Bottom=�$��0,0,500,123q�$��32��$��97��$��20(�$
 Hand��% �� 	IsDefault% . �� -  $)! #TextBox_DisableScreen_AdminUsername��$	��600��$��28��$��14(�$��Center=�$��500,682,500,190q�G� -	  & G�Name$-& 'PasswordBox_DisableScreen_AdminPassword��$	��600��$��28��$��14(�$��Center=�$��500,712,500,160q�� -
  $ ErrorWindow��$y�	Collapsed=�.+��� -  $&  Rectangle_ErrorWindow_Background��$
 0.95Q�
���   �$	��875��$��Stretch=�$��Bottom=�$��0q��� -  $&  Rectangle_ErrorWindow_Foreground��
�������$	��675��$
��1300���� -  $&  Rectangle_ErrorWindow_BelowTitle��
���OO/�$��7��$
��1250��$��Center=�$	��Top=�$��0, 200q��� -  $	 Label_ErrorWindow_Title��$��FEHLER��$	��130��$
��1325��$��70(�$
��Bold"�$
��Left=�$	��Top=�$��170,100,0,0q��� -  $	 Label_ErrorWindow_Content��$��
Fehlertext��$	��500��$
��1245��$��15(�$
��Bold"�$��Center=�$	��Top=�$��0, 230q��� -  $$ Button_ErrorWindow_CloseButton��$��OK��$
��1250��$��50��$��Stretch=�$��Bottom=�$��0, 90q�$��40(�$
 Hand��� -  $ 
AdminLogin��$y�	Collapsed=�.+��� -  $% Rectangle_AdminLogin_Background��$
 0.95Q�
���   �$	��875��$��Stretch=�$��Bottom=�$��0q��� -  $	 Label_AdminLogin_Title��$?��9BITTE MELDEN SIE SICH MIT
IHREN MITARBEITER-KENNDATEN AN.��
�������$��Center=�$��Center=�$
��auto��$
��auto��$��40(�$
��Bold"�$��Center=�$��Center=�$��
0,-300,0,0q��� -  $	 Label_AdminLogin_Username��$��Benutzername��
�������$��15(�$
��Bold"�$
��Left=�$	��Top=�$��369,420,0,0q��� -  $	 Label_AdminLogin_Password��$��Passwort��
�������$��15(�$
��Bold"�$
��Left=�$	��Top=�$��408,448,0,0q��� -  $# Button_AdminLogin_LoginButton��$��Anmelden��$	��150��$��28��$��Center=�$��Center=�$��445,120,0,0q�$
 Hand��% . �� -  $$ Button_AdminLogin_CancelButton��$��	Abbrechen��$	��150��$��28��$��Center=�$��Center=�$��100,120,0,0q�$
 Hand��' �� IsCancel' . �� -  $&!  TextBox_AdminLogin_AdminUsername��$	��600��$��28��$��14(�$��Center=�$��	0,-30,0,0q�G� -  $*& $PasswordBox_AdminLogin_AdminPassword��$	��600��$��28��$��14(�$��Center=�$��0,30,0,0q��� -  $+
 %Image_AdminLogin_NotAllBoxesFilledOut��$:��4Resources/Images/adminLogin_notAllBoxesFilledOut.png��$y�	Collapsed=�$	��250��$��1102,60,248,-60q��� -  $ 
 Image_AdminLogin_WrongData��$/��)Resources/Images/adminLogin_wrongData.png��$y�	Collapsed=�$	��250��$��1102,60,248,-60q�