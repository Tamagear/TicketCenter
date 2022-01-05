using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Ticketautomat.Classes;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Version version = new Version(0,0,3);

        private Profile currentProfile = null;
        private Manager manager = null;
        private EAgeType currentSelectedAgeType = EAgeType.ADULT;
        private bool timerRuns = false;

        public event EventHandler OnResetTimerTimeout;

        private const string ENCRYPTION_KEY = "DSXFCGBGFDVHGSVBNFHRRVGF";
        private const string SOFTWARE_NAME = "TicketCenter";
        private const string VERSION_STATUS = "SNAPSHOT";  //Später: RELEASE
        private const string VERSION_HINTS = "Ticketcenter: Versionshinweise\n\n(c)Software-Engineering Gruppe C2\nFH Aachen";
        private const string WELCOME_MESSAGE = "Willkommen beim TicketCenter! Systemdaten wurden erstellt. Ändern Sie die Textdateien im Unterverzeichnis '/data', um den Text in der Software anzupassen. Für den Inhalt der Textdateien übernimmt der Entwickler keine Garantie.";
        private const string dir_savedata = "data";
        private const string dir_messageOfTheDay = "motd.txt";
        private const string dir_ticketSpotlight = "spotlight.txt";
        private const string dir_saveFile = "savefile.txt";
        private const int TIME_DIVIDER = 60;

        public MainWindow()
        {
            InitializeComponent();

            currentProfile = new Profile();
            manager = new Manager(currentProfile);
            manager.ResetTimeUntilTimeout();
            UpdateTimeTexts();

            DispatcherTimer liveTimer = new DispatcherTimer();
            liveTimer.Interval = TimeSpan.FromSeconds(1);
            liveTimer.Tick += LiveTimer_Tick;
            liveTimer.Start();
        }
        
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            HandleSystemData();
            LoadFile();
            UpdateTexts();            
        }

        private void HandleSystemData()
        {
            string fileDirectory = $"{Directory.GetCurrentDirectory()}/{dir_savedata}";
            string motdPath = $"{fileDirectory}/{dir_messageOfTheDay}";
            string spotlightPath = $"{fileDirectory}/{dir_ticketSpotlight}";
            string saveFilePath = $"{fileDirectory}/{dir_saveFile}";
            bool hasDirectory = true;
            if (!Directory.Exists(fileDirectory))
            {
                hasDirectory = false;
                Directory.CreateDirectory(fileDirectory);
            }

            if (!File.Exists(motdPath))            
                File.WriteAllText(motdPath, "MESSAGE OF THE DAY");

            if (!File.Exists(spotlightPath))
                File.WriteAllText(spotlightPath, "TICKET-SPOTLIGHT");

            if (!File.Exists(saveFilePath))
            {
                manager.WriteDefaultPriceEntries();
                SaveFile();
            }

            if (!hasDirectory)
                MessageBox.Show(WELCOME_MESSAGE, "Willkommen beim TicketCenter!");
        }

        private void SaveFile()
        {
            string fileDirectory = $"{Directory.GetCurrentDirectory()}/{dir_savedata}";
            string tempFilePath = $"{fileDirectory}/temp_{RandomString(10)}.txt";
            string saveFilePath = $"{fileDirectory}/{dir_saveFile}";

            File.WriteAllText(tempFilePath, SerializeSaveFile());  
            EncryptFile(tempFilePath, saveFilePath);
            File.Delete(tempFilePath);
        }

        private void LoadFile()
        {
            string fileDirectory = $"{Directory.GetCurrentDirectory()}/{dir_savedata}";
            string tempFilePath = $"{fileDirectory}/temp_{RandomString(10)}.txt";
            string saveFilePath = $"{fileDirectory}/{dir_saveFile}";

            if (!File.Exists(saveFilePath)) return;

            DecryptFile(saveFilePath, tempFilePath);
            manager.LoadSavedData(File.ReadAllText(tempFilePath));
            File.Delete(tempFilePath);
        }

        private void LiveTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimeTexts();

            if (manager.TimeUntilTimeout > 0f && timerRuns)
            {
                manager.TimeUntilTimeout -= 1f;

                if (manager.TimeUntilTimeout <= 0f)
                {
                    manager.TimeUntilTimeout = 0f;
                    Reset();
                    GoTo_MainMenu();
                    OnResetTimerTimeout?.Invoke(null, null);
                }
            }
        }

        private void UpdateTexts()
        {
            Label_SoftwareTitleAndVersion.Content = $"{SOFTWARE_NAME} Version {version}-{VERSION_STATUS}";
            Button_MainMenu_BuyButton_Adult.Content = $"Erwachsener\nAb {manager.PriceEntries[2,0].Price.ToString("F2")}€";
            Button_MainMenu_BuyButton_Child.Content = $"Kind\nAb {manager.PriceEntries[0, 0].Price.ToString("F2")}€";
            Button_MainMenu_BuyButton_Pensioner.Content = $"Senior\nAb {manager.PriceEntries[3, 0].Price.ToString("F2")}€";
            Button_MainMenu_BuyButton_Reduced.Content = $"Ermäßigt\nAb {manager.PriceEntries[1, 0].Price.ToString("F2")}€";

            string fileDirectory = $"{Directory.GetCurrentDirectory()}/{dir_savedata}";
            string motdPath = $"{fileDirectory}/{dir_messageOfTheDay}";
            string spotlightPath = $"{fileDirectory}/{dir_ticketSpotlight}";

            if (!string.IsNullOrEmpty(motdPath) && File.Exists(motdPath))
                Label_MainMenu_MOTD.Content = File.ReadAllText(motdPath);

            if (!string.IsNullOrEmpty(spotlightPath) && File.Exists(spotlightPath))
                Label_MainMenu_TicketSpotlight.Content = File.ReadAllText(spotlightPath);

            UpdateTicketSpecifics();
            UpdatePriceTableTexts();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
            Application.Current.Shutdown();
        }

        private void Button_Version_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            MessageBox.Show(VERSION_HINTS, "Versionshinweise");
        }

        private void Button_MainMenu_GoToCart_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_ShoppingCart();
        }

        private void MainMenu_BuyButton_Adult_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Adult_Click(sender, e);
            GoTo_BuyMenu();
        }

        private void MainMenu_BuyButton_Child_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Child_Click(sender, e);
            GoTo_BuyMenu();
        }

        private void MainMenu_BuyButton_Pensioner_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner_Click(sender, e);
            GoTo_BuyMenu();
        }

        private void MainMenu_BuyButton_Reduced_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Reduced_Click(sender, e);
            GoTo_BuyMenu();
        }

        private void Button_MainMenu_ShowPriceTable_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_PriceTable();
        }

        private void Button_MaintenanceLogin_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();

            Image_AdminLogin_NotAllBoxesFilledOut.Visibility = Visibility.Collapsed;
            Image_AdminLogin_WrongData.Visibility = Visibility.Collapsed;
            TextBox_AdminLogin_AdminUsername.Text = string.Empty;
            PasswordBox_AdminLogin_AdminPassword.Password = string.Empty;

            AdminLogin.Visibility = Visibility.Visible;
        }

        private void Button_PriceTable_GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_MainMenu();
        }

        private void Button_ErrorWindow_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorWindow.Visibility = Visibility.Collapsed;
        }

        private void UpdateTimeTexts()
        {
            DateTime now = DateTime.Now;

            if (timerRuns)
            {
                int minutesUntilTimeout = (int)(manager.TimeUntilTimeout / TIME_DIVIDER);
                int secondsUntilTimeout = (int)(Math.Abs(manager.TimeUntilTimeout % TIME_DIVIDER));
                Label_TimeLeft.Content = $"Zeit bis zum Abbruch: {(minutesUntilTimeout < 10 ? "0" : string.Empty) + minutesUntilTimeout} : {(secondsUntilTimeout < 10 ? "0" : string.Empty) + secondsUntilTimeout}";
            }
            else
                Label_TimeLeft.Content = string.Empty;

            Label_DateTime.Content = $"{now.ToString("g")} Uhr";
        }

        private void UpdatePriceTableTexts()
        {
            Label_PriceTable_AsOfToday.Content = $"Stand: {Label_DateTime.Content}";
            Label_PriceTable_Table_Child_A.Content = $"{manager.PriceEntries[0, 0].Price.ToString("F2")}€";
            Label_PriceTable_Table_Child_B.Content = $"{manager.PriceEntries[0, 1].Price.ToString("F2")}€";
            Label_PriceTable_Table_Child_C.Content = $"{manager.PriceEntries[0, 2].Price.ToString("F2")}€";
            Label_PriceTable_Table_Reduced_A.Content = $"{manager.PriceEntries[1, 0].Price.ToString("F2")}€";
            Label_PriceTable_Table_Reduced_B.Content = $"{manager.PriceEntries[1, 1].Price.ToString("F2")}€";
            Label_PriceTable_Table_Reduced_C.Content = $"{manager.PriceEntries[1, 2].Price.ToString("F2")}€";
            Label_PriceTable_Table_Adult_A.Content = $"{manager.PriceEntries[2, 0].Price.ToString("F2")}€";
            Label_PriceTable_Table_Adult_B.Content = $"{manager.PriceEntries[2, 1].Price.ToString("F2")}€";
            Label_PriceTable_Table_Adult_C.Content = $"{manager.PriceEntries[2, 2].Price.ToString("F2")}€";
            Label_PriceTable_Table_Pensioner_A.Content = $"{manager.PriceEntries[3, 0].Price.ToString("F2")}€";
            Label_PriceTable_Table_Pensioner_B.Content = $"{manager.PriceEntries[3, 1].Price.ToString("F2")}€";
            Label_PriceTable_Table_Pensioner_C.Content = $"{manager.PriceEntries[3, 2].Price.ToString("F2")}€";
        }

        //temp => save
        private void EncryptFile(string inputFile, string outputFile)
        {
            string inputText = File.ReadAllText(inputFile);
            string outputText = string.Empty;
            int encryptionIndex = 0;
            foreach(char ch in inputText)
            {
                encryptionIndex = (encryptionIndex + 1) % ENCRYPTION_KEY.Length;
                outputText += (char)((int)ch + ENCRYPTION_KEY[encryptionIndex]);
            }

            File.WriteAllText(outputFile, outputText);
        }

        //save-> temp
        private void DecryptFile(string inputFile, string outputFile)
        {
            string inputText = File.ReadAllText(inputFile);
            string outputText = string.Empty;
            int encryptionIndex = 0;
            foreach (char ch in inputText)
            {
                encryptionIndex = (encryptionIndex + 1) % ENCRYPTION_KEY.Length;
                outputText += (char)((int)ch - ENCRYPTION_KEY[encryptionIndex]);
            }

            File.WriteAllText(outputFile, outputText);
        }

        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string SerializeSaveFile()
        {
            string result = string.Empty;
            //foreach (LogEntry log in manager.LogEntries)
            for (int i = 0; i < manager.LogEntries.Count; i++)
            {
                LogEntry log = manager.LogEntries[i];
                result += $"<log><date>{log.Date}</date><author>{log.Author}</author><content>{log.Content}</content></log>";
            }

            //foreach (PriceEntry priceEntry in manager.PriceEntries)
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    PriceEntry priceEntry = manager.PriceEntries[i, j];
                    result += $"<priceEntry><agetype>{(int)priceEntry.AgeType}</agetype><tarifflevel>{(int)priceEntry.TariffLevel}</tarifflevel><price>{priceEntry.Price}</price></priceEntry>";
                }
            }

            return result;
        }

        private void GoTo_MainMenu()
        {
            MainMenu.Visibility = Visibility.Visible;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_PriceTable()
        {            
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Visible;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }  
        
        private void GoTo_BuyMenu()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Visible;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_AdminDashboard()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Visible;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_AdminSavingsManagement()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Visible;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_AdminChangePricesMenu()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Visible;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_AdminStatistics()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Visible;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_ShoppingCart()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Visible;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_PayMenu()
        {
            manager.MoneyManager.SumLeft = manager.CurrentUser.GetFinalPrice();
            UpdateTicketSpecifics();

            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Visible;
            PDFExportMenu.Visibility = Visibility.Collapsed;
        }

        private void GoTo_PDFExportMenu()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Collapsed;
            BuyMenu.Visibility = Visibility.Collapsed;
            AdminDashboard.Visibility = Visibility.Collapsed;
            AdminSavingsManagement.Visibility = Visibility.Collapsed;
            AdminChangePricesMenu.Visibility = Visibility.Collapsed;
            AdminStatistics.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Collapsed;
            PayMenu.Visibility = Visibility.Collapsed;
            PDFExportMenu.Visibility = Visibility.Visible;
        }

        private void ShowError(string content, string caption = "FEHLER")
        {
            Label_ErrorWindow_Title.Content = caption;
            Label_ErrorWindow_Content.Content = content;
            ErrorWindow.Visibility = Visibility.Visible;
        }

        private void UpdateTicketSpecifics()
        {
            //Warenkorb: Sachen hinzufügen/löschen            
            Button_MainMenu_GoToCart.Content = $"Warenkorb\nanzeigen ({manager.CurrentUser.ShoppingCart.Count})";
            Button_BuyMenu_GoToCart.Content = $"Warenkorb\nanzeigen ({manager.CurrentUser.ShoppingCart.Count})";
            Label_ShoppingCart_Sum.Content = $"Preis insgesamt: {manager.CurrentUser.GetFinalPrice():F2}€";
            Label_PayMenu_PaySum.Content = $"{manager.MoneyManager.SumLeft:F2}€";
        }

        private void Reset()
        {
            manager.CurrentUser.ResetShoppingCart();
            timerRuns = false;
            manager.ResetTimeUntilTimeout();
        }

        private void Button_BuyMenu_GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_MainMenu();
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Adult_Click(object sender, RoutedEventArgs e)
        {
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = false;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = true;
            currentSelectedAgeType = EAgeType.ADULT;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Child_Click(object sender, RoutedEventArgs e)
        {
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = false;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = true;
            currentSelectedAgeType = EAgeType.CHILD;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Pensioner_Click(object sender, RoutedEventArgs e)
        {
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = false;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = true;
            currentSelectedAgeType = EAgeType.PENSIONER;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Reduced_Click(object sender, RoutedEventArgs e)
        {
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = false;
            currentSelectedAgeType = EAgeType.REDUCED;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_StartButton_Click(object sender, RoutedEventArgs e)
        {
            //Auswahl öffnen
        }

        private void Button_BuyMenu_TicketOptions_DestinationButton_Click(object sender, RoutedEventArgs e)
        {
            //Auswahl öffnen
        }

        private void Button_BuyMenu_TicketOptions_DisplayRoutes_Click(object sender, RoutedEventArgs e)
        {
            //Schrift von den beiden Ticket-Optionen setzen
        }

        private void Button_BuyMenu_TicketSelection_Cheapest_Click(object sender, RoutedEventArgs e)
        {
            //Ticket hinzufügen
            AddedTicket.Visibility = Visibility.Visible;
            //Texte von AddedTicket setzen
            UpdateTicketSpecifics();
        }

        private void Button_BuyMenu_TicketSelection_Fastest_Click(object sender, RoutedEventArgs e)
        {
            //Ticket hinzufügen
            AddedTicket.Visibility = Visibility.Visible;
            //Texte von AddedTicket setzen
            int amount = 1;
            DateTime dateTime = DateTime.Now;
            Station startStation = manager.StationGraph.Graph.GetStation(0); //Aus Buttons auslesen
            Station targetDestination = manager.StationGraph.Graph.GetStation(2); //Aus Buttons auslesen
            ETariffLevel tariffLevel = ETariffLevel.TARIFF_A; //Ausrechnen
            PriceEntry usedPriceEntry = manager.PriceEntries[(int)currentSelectedAgeType, (int)tariffLevel]; //Preis-Eintrag rausfinden
            //Testwerte
            for (int i = 0; i < amount; i++)
            {
                manager.CurrentUser.AddToShoppingCart(new Ticket(dateTime, manager.CurrentUser,
                    startStation, targetDestination, usedPriceEntry));
            }
            Label_AddedTicket_TicketCount.Content = $"Anzahl: {amount}";
            int splitIndex = usedPriceEntry.ToString().IndexOf('/');
            Label_AddedTicket_TicketType.Content = $"Ticket, {usedPriceEntry.ToString().Substring(0, splitIndex)}\n{usedPriceEntry.ToString().Substring(splitIndex)}";
            timerRuns = true;
            UpdateTicketSpecifics();
        }

        private void Button_AdminLogin_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_AdminLogin_AdminUsername.Text.Equals(string.Empty)
                || PasswordBox_AdminLogin_AdminPassword.Password.Equals(string.Empty))
            {
                Image_AdminLogin_NotAllBoxesFilledOut.Visibility = Visibility.Visible;
                return;
            }

            if (manager.TryLogin(TextBox_AdminLogin_AdminUsername.Text, PasswordBox_AdminLogin_AdminPassword.Password))
            {
                GoTo_AdminDashboard();
                AdminLogin.Visibility = Visibility.Collapsed;
            }
            else
            {
                Image_AdminLogin_WrongData.Visibility = Visibility.Visible;
            }
        }

        private void Button_AdminLogin_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin.Visibility = Visibility.Collapsed;
        }

        private void Button_AdminDashboard_AdminButtonOptions_Statistics_Click(object sender, RoutedEventArgs e)
        {
            GoTo_AdminStatistics();
        }

        private void Button_AdminDashboard_AdminButtonOptions_SavingsManagement_Click(object sender, RoutedEventArgs e)
        {
            GoTo_AdminSavingsManagement();
        }

        private void Button_AdminDashboard_AdminButtonOptions_DisableMachine_Click(object sender, RoutedEventArgs e)
        {
            AdminDisableMachine.Visibility = Visibility.Visible;
        }

        private void Button_AdminDashboard_AdminButtonOptions_ChangePrices_Click(object sender, RoutedEventArgs e)
        {
            GoTo_AdminChangePricesMenu();
        }

        private void Button_AdminSavingsManagement_AdminButtonOptions_FillTicketPaper_Click(object sender, RoutedEventArgs e)
        {
            manager.MoneyManager.RefillTicketPaper();
            //Bestätigungsfenster?
        }

        private void Button_AdminSavingsManagement_AdminButtonOptions_FillCoins_Click(object sender, RoutedEventArgs e)
        {
            manager.MoneyManager.Refill(EMoneyType.COIN, out _);
            //Bestätigungsfenster? Ausschuss anzeigen
        }

        private void Button_AdminSavingsManagement_AdminButtonOptions_FillBills_Click(object sender, RoutedEventArgs e)
        {
            manager.MoneyManager.Refill(EMoneyType.BILL, out _);
            //Bestätigungsfenster? Ausschuss anzeigen
        }

        private void Button_AdminSavingsManagement_GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_AdminDashboard();
        }

        private void Button_AdminChangePricesMenu_SaveAndGoBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_AdminDashboard();
        }

        private void Button_AdminDisableMachine_ShutDownButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_MainMenu();
            Reset();
            AdminDisableMachine.Visibility = Visibility.Collapsed;
            DisabledScreen.Visibility = Visibility.Visible;
        }

        private void Button_AdminDisableMachine_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminDisableMachine.Visibility = Visibility.Collapsed;
        }

        private void Button_DisableScreen_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_DisableScreen_AdminUsername.Text.Equals(string.Empty)
                || PasswordBox_DisableScreen_AdminPassword.Password.Equals(string.Empty))
            {
                Image_DisableScreen_NotAllBoxesFilledOut.Visibility = Visibility.Visible;
                return;
            }

            if (manager.TryLogin(TextBox_DisableScreen_AdminUsername.Text, PasswordBox_DisableScreen_AdminPassword.Password))
            {                
                DisabledScreen.Visibility = Visibility.Collapsed;
            }
            else
            {
                Image_DisableScreen_WrongData.Visibility = Visibility.Visible;
            }
        }

        private void Button_ShoppingCart_PayNowButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_PayMenu();
        }

        private void Button_AddedTicket_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddedTicket.Visibility = Visibility.Collapsed;
            Reset();
            GoTo_MainMenu();
        }

        private void Button_AddedTicket_ShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            AddedTicket.Visibility = Visibility.Collapsed;
            GoTo_ShoppingCart();
        }

        private void Button_AddedTicket_ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            AddedTicket.Visibility = Visibility.Collapsed;
        }

        private void PayMenu_PayButtonGrid_PayButton_Click(object sender, RoutedEventArgs e)
        {
            string[] moneyValues = ((Button)sender).Name.Split('_');
            string moneyValue = moneyValues[moneyValues.Length - 1];

            float addedMoney = int.Parse(moneyValue) / 100f;
            //Auf OverflowFehler achten!
            manager.MoneyManager.InsertMoney(manager.MoneyManager.GetMoneyFromValue(addedMoney), 1);
            manager.MoneyManager.SumLeft -= addedMoney;
            UpdateTicketSpecifics();

            if (manager.MoneyManager.SumLeft <= 0f)
            {
                //Entferne Wechselgeld
                //Reset();
                GoTo_PDFExportMenu();
            }
        }

        private void Button_PDFExportMenu_ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                String fileName = saveFileDialog.FileName;
                currentProfile.ExportToPDF(fileName);
            } 
        }

        private void Button_PayMenu_GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_ShoppingCart();
            //Altes Geld auswerfen
        }
    }

    public struct Version
    {
        public int major;
        public int minor;
        public int subMinor;

        public Version(int _major, int _minor, int _subMinor)
        {
            major = _major;
            minor = _minor;
            subMinor = _subMinor;
        }

        public static Version zero => new Version(0, 0, 0);

        public override string ToString()
        {
            return $"{major}.{minor}.{subMinor}";
        }
    }
}