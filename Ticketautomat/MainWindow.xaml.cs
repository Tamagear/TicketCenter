using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using Ticketautomat.Classes;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Version version = new Version(0, 0, 3);

        private Profile currentProfile = null;
        private Manager manager = null;
        private EAgeType currentSelectedAgeType = EAgeType.ADULT;
        private EAgeType currentAdminChangePriceAgeType = EAgeType.ADULT;
        private ETariffLevel currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_A;
        private bool timerRuns = false;
        private bool ticketMapIsSelectingDestination = false;
        private Thickness ticketMapMarginStart = new Thickness(0f, -5f, 0f, 5f);
        private Thickness ticketMapMarginDestination = new Thickness(0, 50f, 0f, -50f);
        private Station startStation = null;
        private Station destinationStation = null;

        private ObservableCollection<Tuple<Ticket, int, string>> tickets = new ObservableCollection<Tuple<Ticket, int, string>>();
        private ObservableCollection<LogEntry> dynamicLogs = null;

        private object currentTicket { get; set; }

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

            dynamicLogs = new ObservableCollection<LogEntry>(manager.LogEntries);

            List_Logs.ItemsSource = dynamicLogs;
            List_ShoppingCart.ItemsSource = tickets;
            List_ShoppingCart.SelectedItem = currentTicket;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            HandleSystemData();
            LoadFile();
            UpdateTexts();
            SetStartStation(manager.StationGraph.Graph.GetStation(0));
            SetDestinationStation(manager.StationGraph.Graph.GetStation(1));
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
            Button_MainMenu_BuyButton_Adult.Content = $"Erwachsener\nAb {manager.PriceEntries[2, 0].Price.ToString("F2")}€";
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
            manager.ResetTimeUntilTimeout();
            GoTo_MainMenu();
        }

        private void Button_ErrorWindow_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
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

            Label_AdminChangePricesMenu_Table_Child_A.Content = $"{manager.PriceEntries[0, 0].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Child_B.Content = $"{manager.PriceEntries[0, 1].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Child_C.Content = $"{manager.PriceEntries[0, 2].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Reduced_A.Content = $"{manager.PriceEntries[1, 0].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Reduced_B.Content = $"{manager.PriceEntries[1, 1].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Reduced_C.Content = $"{manager.PriceEntries[1, 2].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Adult_A.Content = $"{manager.PriceEntries[2, 0].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Adult_B.Content = $"{manager.PriceEntries[2, 1].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Adult_C.Content = $"{manager.PriceEntries[2, 2].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Pensioner_A.Content = $"{manager.PriceEntries[3, 0].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Pensioner_B.Content = $"{manager.PriceEntries[3, 1].Price.ToString("F2")}€";
            Label_AdminChangePricesMenu_Table_Pensioner_C.Content = $"{manager.PriceEntries[3, 2].Price.ToString("F2")}€";
        }

        //temp => save
        private void EncryptFile(string inputFile, string outputFile)
        {
            string inputText = File.ReadAllText(inputFile);
            string outputText = string.Empty;
            int encryptionIndex = 0;
            foreach (char ch in inputText)
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

        private void AddLogEntry(string content)
        {
            DateTime dateTime = DateTime.Now;           
            string autor = manager.CurrentUser.Name;            
            manager.LogEntries.Add(new LogEntry(dateTime.ToString(), autor, content));
            dynamicLogs.Add(new LogEntry(dateTime.ToString(), autor, content));
            SaveFile();
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
            Button_MaintenanceLogin.Visibility = Visibility.Visible;
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
            Button_MaintenanceLogin.Visibility = Visibility.Collapsed;
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
            float finalPrice = manager.CurrentUser.GetFinalPrice();
            Button_MainMenu_GoToCart.Content = $"Warenkorb\nanzeigen ({manager.CurrentUser.ShoppingCart.Count})";
            Button_BuyMenu_GoToCart.Content = $"Warenkorb\nanzeigen ({manager.CurrentUser.ShoppingCart.Count})";
            Button_ShoppingCart_PayNowButton.IsEnabled = finalPrice > 0f;
            Button_ShoppingCart_Decrease.IsEnabled = finalPrice > 0f;
            Button_ShoppingCart_Increase.IsEnabled = finalPrice > 0f;
            Button_ShoppingCart_Remove.IsEnabled = finalPrice > 0f;
            if (currentTicket == null)
            {
                Button_ShoppingCart_Decrease.IsEnabled = false;
                Button_ShoppingCart_Increase.IsEnabled = false;
                Button_ShoppingCart_Remove.IsEnabled = false;
            }
            Label_ShoppingCart_Sum.Content = $"Preis insgesamt: {finalPrice:F2}€";
            Label_PayMenu_PaySum.Content = $"{manager.MoneyManager.SumLeft:F2}€";
        }

        private void Reset()
        {
            manager.CurrentUser.ResetShoppingCart();
            timerRuns = false;
            manager.ResetTimeUntilTimeout();
            UpdateTicketSpecifics();
            tickets.Clear();
            currentTicket = null;
        }

        private void Button_BuyMenu_GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_MainMenu();
        }

        private void Button_AdminMenu_Logout_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_MainMenu();

            //Unsicher ob richtig
            currentProfile = new Profile();
            manager.CurrentUser = currentProfile;
            manager.CurrentUser.Name = "Kunde";
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Adult_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = false;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = true;
            currentSelectedAgeType = EAgeType.ADULT;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Child_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = false;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = true;
            currentSelectedAgeType = EAgeType.CHILD;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Pensioner_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = false;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = true;
            currentSelectedAgeType = EAgeType.PENSIONER;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_TariffOption_Reduced_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            Button_BuyMenu_TicketOptions_TariffOption_Adult.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Child.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Pensioner.IsEnabled = true;
            Button_BuyMenu_TicketOptions_TariffOption_Reduced.IsEnabled = false;
            currentSelectedAgeType = EAgeType.REDUCED;
            //Aktualisiere Preise
        }

        private void Button_BuyMenu_TicketOptions_StartButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();

            if (BuyMenu_TicketOptions_TicketMap.Visibility == Visibility.Collapsed ||
                BuyMenu_TicketOptions_TicketMap.Margin == ticketMapMarginDestination)
            {
                BuyMenu_TicketOptions_TicketMap.Visibility = Visibility.Visible;
                BuyMenu_TicketOptions_TicketMap.Margin = ticketMapMarginStart;
                ticketMapIsSelectingDestination = false;
            }
            else
                BuyMenu_TicketOptions_TicketMap.Visibility = Visibility.Collapsed;
        }

        private void Button_BuyMenu_TicketOptions_DestinationButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();

            if (BuyMenu_TicketOptions_TicketMap.Visibility == Visibility.Collapsed ||
                BuyMenu_TicketOptions_TicketMap.Margin == ticketMapMarginStart)
            {
                BuyMenu_TicketOptions_TicketMap.Visibility = Visibility.Visible;
                BuyMenu_TicketOptions_TicketMap.Margin = ticketMapMarginDestination;
                ticketMapIsSelectingDestination = true;
            }
            else
                BuyMenu_TicketOptions_TicketMap.Visibility = Visibility.Collapsed;
        }

        private void Button_BuyMenu_TicketSelection_Cheapest_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            AddedTicket.Visibility = Visibility.Visible;

            int amount = 1; //Aus entsprechendem Knopf auslesen
            ETariffLevel tariffLevel = manager.StationGraph.GetRouteTariffLevel(manager.StationGraph.Graph.CheapestPath(startStation, destinationStation));
            PriceEntry usedPriceEntry = manager.PriceEntries[(int)currentSelectedAgeType, (int)tariffLevel];

            AddCurrentTicket(amount, usedPriceEntry);
        }

        private void Button_BuyMenu_TicketSelection_Fastest_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            AddedTicket.Visibility = Visibility.Visible;

            int amount = 1; //Aus entsprechendem Knopf auslesen
            ETariffLevel tariffLevel = manager.StationGraph.GetRouteTariffLevel(manager.StationGraph.Graph.ShortestPath(startStation, destinationStation));
            PriceEntry usedPriceEntry = manager.PriceEntries[(int)currentSelectedAgeType, (int)tariffLevel];

            AddCurrentTicket(amount, usedPriceEntry);
        }

        private void AddCurrentTicket(int amount, PriceEntry usedPriceEntry)
        {
            DateTime dateTime = DateTime.Now;

            Ticket addTicket = new Ticket(dateTime, manager.CurrentUser, startStation, destinationStation, usedPriceEntry);
            for (int i = 0; i < amount; i++)
                manager.CurrentUser.AddToShoppingCart(addTicket);

            #region ShoppingCart

            bool exists = false;
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].Item1 == addTicket)
                {
                    int menge = tickets[i].Item2;
                    tickets.RemoveAt(i);
                    tickets.Add(new Tuple<Ticket, int, string>(addTicket, amount + menge, "white"));
                    exists = true;
                    break;
                }
            }

            if (!exists)
                tickets.Add(new Tuple<Ticket, int, string>(addTicket, amount, "white"));

            #endregion

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
                manager.ResetTimeUntilTimeout();
                timerRuns = true;
                currentProfile = manager.CurrentUser;
                AddLogEntry($"Erfolgreiche Anmeldung. Benutzer: {TextBox_AdminLogin_AdminUsername.Text}");
            }
            else
            {
                Image_AdminLogin_WrongData.Visibility = Visibility.Visible;
            }
        }

        private void Button_AdminLogin_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            AdminLogin.Visibility = Visibility.Collapsed;
        }

        private void Button_AdminDashboard_AdminButtonOptions_Statistics_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_AdminStatistics();
        }

        private void Button_AdminDashboard_AdminButtonOptions_SavingsManagement_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_AdminSavingsManagement();
        }

        private void Button_AdminDashboard_AdminButtonOptions_DisableMachine_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            AdminDisableMachine.Visibility = Visibility.Visible;
        }

        private void Button_AdminDashboard_AdminButtonOptions_ChangePrices_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_AdminChangePricesMenu();
        }

        private void Button_AdminSavingsManagement_AdminButtonOptions_FillTicketPaper_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            manager.MoneyManager.RefillTicketPaper();
            //Bestätigungsfenster?
            AddLogEntry("Papierspeicher aufgefüllt");
        }

        private void Button_AdminSavingsManagement_AdminButtonOptions_FillCoins_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            manager.MoneyManager.Refill(EMoneyType.COIN, out _);
            //Bestätigungsfenster? Ausschuss anzeigen
            AddLogEntry("Maschine mit Münzen aufgefüllt");
        }

        private void Button_AdminSavingsManagement_AdminButtonOptions_FillBills_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            manager.MoneyManager.Refill(EMoneyType.BILL, out _);
            //Bestätigungsfenster? Ausschuss anzeigen
            AddLogEntry("Maschine mit Scheinen aufgefüllt");
        }

        private void Button_AdminSavingsManagement_GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_AdminDashboard();
        }

        private void Button_AdminChangePricesMenu_SaveAndGoBackButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_AdminDashboard();
        }

        private void Button_AdminDisableMachine_ShutDownButton_Click(object sender, RoutedEventArgs e)
        {
            GoTo_MainMenu();
            Reset();
            AdminDisableMachine.Visibility = Visibility.Collapsed;
            DisabledScreen.Visibility = Visibility.Visible;
            AddLogEntry("Maschine deaktiviert");
        }

        private void Button_AdminDisableMachine_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
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
                AddLogEntry("Maschine wieder aktiviert");
                currentProfile = new Profile();
                manager.CurrentUser = currentProfile;
                manager.CurrentUser.Name = "Kunde";
            }
            else
            {
                Image_DisableScreen_WrongData.Visibility = Visibility.Visible;
            }
        }

        private void Button_ShoppingCart_PayNowButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
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
            manager.ResetTimeUntilTimeout();
            AddedTicket.Visibility = Visibility.Collapsed;
            GoTo_ShoppingCart();
        }

        private void Button_AddedTicket_ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            AddedTicket.Visibility = Visibility.Collapsed;
        }

        private void PayMenu_PayButtonGrid_PayButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
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
            manager.ResetTimeUntilTimeout();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                String fileName = saveFileDialog.FileName;
                currentProfile.ExportToPDF(fileName);
            }
            AddLogEntry("Ticketpdf exportiert");
        }

        private void Button_PayMenu_GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_ShoppingCart();
            //Altes Geld auswerfen
        }

        private void Change_Selected_Ticket(object sender, SelectionChangedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            if (List_ShoppingCart.SelectedItem == null)
            {
            }
            else if (currentTicket != null)
            {
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                currentTicket = List_ShoppingCart.SelectedItem;
                Tuple<Ticket, int, string> item2 = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1 || tickets[i].Item1 == item2.Item1)
                    {
                        if (tickets[i].Item1 == item.Item1)
                        {
                            int menge = tickets[i].Item2;
                            tickets.RemoveAt(i);
                            tickets.Insert(i, new Tuple<Ticket, int, string>(item.Item1, menge, "White"));
                        }
                        else if (tickets[i].Item1 == item2.Item1)
                        {
                            int menge = tickets[i].Item2;
                            tickets.RemoveAt(i);
                            tickets.Insert(i, new Tuple<Ticket, int, string>(item2.Item1, menge, "AntiqueWhite"));
                        }
                    }
                }
            }
            else
            {
                currentTicket = List_ShoppingCart.SelectedItem;
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1)
                    {
                        if (tickets[i].Item1 == item.Item1)
                        {
                            int menge = tickets[i].Item2;
                            tickets.RemoveAt(i);
                            tickets.Insert(i, new Tuple<Ticket, int, string>(item.Item1, menge, "AntiqueWhite"));
                        }                        
                    }
                }
            }
            UpdateTicketSpecifics();
        }

        private void Button_ShoppingCart_Increase_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();

            if (List_ShoppingCart.SelectedItem != null)
            {
                currentTicket = List_ShoppingCart.SelectedItem;
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1)
                    {
                        manager.CurrentUser.IncreaseByOneFromShoppingCart(item.Item1);
                        int menge = tickets[i].Item2 + 1;
                        tickets.RemoveAt(i);
                        tickets.Insert(i, new Tuple<Ticket, int, string>(item.Item1, menge, "AntiqueWhite"));
                        break;
                    }
                }
            }
            else if (currentTicket != null)
            {
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1)
                    {
                        manager.CurrentUser.IncreaseByOneFromShoppingCart(item.Item1);
                        int menge = tickets[i].Item2 + 1;
                        tickets.RemoveAt(i);
                        tickets.Insert(i, new Tuple<Ticket, int, string>(item.Item1, menge, "AntiqueWhite"));
                        break;
                    }
                }
            }

            UpdateTicketSpecifics();
        }

        private void Button_ShoppingCart_Decrease_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();

            if (List_ShoppingCart.SelectedItem != null)
            {
                currentTicket = List_ShoppingCart.SelectedItem;
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1)
                    {
                        manager.CurrentUser.DecreaseByOneFromShoppingCart(item.Item1);
                        int menge = tickets[i].Item2 - 1;
                        tickets.RemoveAt(i);
                        if (menge > 0)
                        {
                            tickets.Insert(i, new Tuple<Ticket, int, string>(item.Item1, menge, "AntiqueWhite"));
                        }
                        else
                        {
                            currentTicket = null;
                        }
                        break;
                    }
                }
            }
            else if (currentTicket != null)
            {
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1)
                    {
                        manager.CurrentUser.DecreaseByOneFromShoppingCart(item.Item1);
                        int menge = tickets[i].Item2 - 1;
                        tickets.RemoveAt(i);
                        if (menge > 0)
                        {
                            tickets.Insert(i, new Tuple<Ticket, int, string>(item.Item1, menge, "AntiqueWhite"));
                        }
                        else
                        {
                            currentTicket = null;
                        }
                        break;
                    }
                }
            }

            UpdateTicketSpecifics();
        }

        private void Button_ShoppingCart_Remove_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();

            if (List_ShoppingCart.SelectedItem != null)
            {
                currentTicket = List_ShoppingCart.SelectedItem;
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1)
                    {
                        manager.CurrentUser.RemoveFromShoppingCart(item.Item1);
                        tickets.RemoveAt(i);
                        currentTicket = null;
                        break;
                    }
                }
            }
            else if (currentTicket != null)
            {
                Tuple<Ticket, int, string> item = (Tuple<Ticket, int, string>)currentTicket;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets[i].Item1 == item.Item1)
                    {
                        manager.CurrentUser.RemoveFromShoppingCart(item.Item1);
                        tickets.RemoveAt(i);
                        currentTicket = null;
                        break;
                    }
                }
            }

            UpdateTicketSpecifics();
        }

        private void Button_BuyMenu_TicketOptions_TicketMap_StationButton_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            BuyMenu_TicketOptions_TicketMap.Visibility = Visibility.Collapsed;
            string[] nameparts = ((Button)sender).Name.Split('_');
            if (int.TryParse(nameparts[nameparts.Length - 1], out int id))
            {
                Station selectedStation = manager.StationGraph.Graph.GetStation(id);

                Console.WriteLine($"Ausgewählt: {selectedStation.StationName}");
                if (!ticketMapIsSelectingDestination)
                    SetStartStation(selectedStation);
                else
                    SetDestinationStation(selectedStation);
            }
            else
                ShowError("Die ausgewählte Station ist ungültig.");
        }

        private void SetStartStation(Station selectedStation)
        {
            Button_BuyMenu_TicketOptions_StartButton.Content = selectedStation.StationName;
            Button_BuyMenu_TicketOptions_StartButton.Background = selectedStation.GetStationColor();
            startStation = selectedStation;
        }

        private void SetDestinationStation(Station selectedStation)
        {
            Button_BuyMenu_TicketOptions_DestinationButton.Content = selectedStation.StationName;
            Button_BuyMenu_TicketOptions_DestinationButton.Background = selectedStation.GetStationColor();
            destinationStation = selectedStation;
        }

        private void Button_AdminChangePricesMenu_Table_Child_A_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[0, 0].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.CHILD;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_A;
        }

        private void Button_AdminChangePricesMenu_Table_Child_B_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[0, 1].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.CHILD;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_B;
        }

        private void Button_AdminChangePricesMenu_Table_Child_C_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[0, 2].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.CHILD;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_C;
        }

        private void Button_AdminChangePricesMenu_Table_Reduced_A_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[1, 0].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.REDUCED;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_A;
        }

        private void Button_AdminChangePricesMenu_Table_Reduced_B_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[1, 1].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.REDUCED;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_B;
        }

        private void Button_AdminChangePricesMenu_Table_Reduced_C_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[1, 2].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.REDUCED;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_C;
        }

        private void Button_AdminChangePricesMenu_Table_Adult_A_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[2, 0].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.ADULT;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_A;
        }

        private void Button_AdminChangePricesMenu_Table_Adult_B_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[2, 1].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.ADULT;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_B;
        }

        private void Button_AdminChangePricesMenu_Table_Adult_C_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[2, 2].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.ADULT;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_C;
        }

        private void Button_AdminChangePricesMenu_Table_Pensioner_A_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[3, 0].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.PENSIONER;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_A;
        }

        private void Button_AdminChangePricesMenu_Table_Pensioner_B_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[3, 1].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.PENSIONER;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_B;
        }

        private void Button_AdminChangePricesMenu_Table_Pensioner_C_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Visible;
            TextBox_AdminChangePricesMenu_NewPrice.Text = manager.PriceEntries[3, 2].Price.ToString("F2");
            currentAdminChangePriceAgeType = EAgeType.PENSIONER;
            currentAdminChangePriceTariffLevel = ETariffLevel.TARIFF_C;
        }

        private void Button_AdminChangePricesMenu_ChangeDialogue_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(TextBox_AdminChangePricesMenu_NewPrice.Text, out float result))
            {
                manager.PriceEntries[(int)currentAdminChangePriceAgeType, (int)currentAdminChangePriceTariffLevel].Price = result;
                AdminChangePricesMenu_ChangeDialogue.Visibility = Visibility.Collapsed;
                AddLogEntry("Preise angepasst");
                UpdatePriceTableTexts();
            }           
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