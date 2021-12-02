using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Ticketautomat.Classes;

namespace Ticketautomat
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Version version = new Version(0,0,1);

        private Profile currentProfile = null;
        private Manager manager = null;

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

            if (manager.TimeUntilTimeout > 0f)
            {
                manager.TimeUntilTimeout -= 1f;

                if (manager.TimeUntilTimeout <= 0f)
                {
                    manager.TimeUntilTimeout = 0f;
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
            ShowError("Diese Funktion ist noch nicht implementiert.");
        }

        private void MainMenu_BuyButton_Adult_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            ShowError("Diese Funktion ist noch nicht implementiert.");
        }

        private void MainMenu_BuyButton_Child_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            ShowError("Diese Funktion ist noch nicht implementiert.");
        }

        private void MainMenu_BuyButton_Pensioner_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            ShowError("Diese Funktion ist noch nicht implementiert.");
        }

        private void MainMenu_BuyButton_Reduced_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            ShowError("Diese Funktion ist noch nicht implementiert.");
        }

        private void Button_MainMenu_ShowPriceTable_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            GoTo_PriceTable();
        }

        private void Button_MaintenanceLogin_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
            ShowError("Diese Funktion ist noch nicht implementiert.");
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
            int minutesUntilTimeout = (int)(manager.TimeUntilTimeout / TIME_DIVIDER);
            int secondsUntilTimeout = (int)(Math.Abs(manager.TimeUntilTimeout % TIME_DIVIDER));
            Label_TimeLeft.Content = $"Zeit bis zum Abbruch: {(minutesUntilTimeout < 10 ? "0" : string.Empty) + minutesUntilTimeout} : {(secondsUntilTimeout < 10 ? "0" : string.Empty) + secondsUntilTimeout}";
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

        private void EncryptFile(string inputFile, string outputFile)
        {
            string inputText = File.ReadAllText(inputFile);
            Console.WriteLine(inputText);
            string outputText = string.Empty;
            int encryptionIndex = 0;
            foreach(char ch in inputText)
            {
                encryptionIndex = (encryptionIndex + 1) % ENCRYPTION_KEY.Length;
                outputText += (char)((int)ch + ENCRYPTION_KEY[encryptionIndex]);
            }

            File.WriteAllText(outputFile, outputText);
        }

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
            foreach (LogEntry log in manager.LogEntries)
                result += $"<log><date>{log.Date}</date><author>{log.Author}</author><content>{log.Content}</content></log>";

            foreach (PriceEntry priceEntry in manager.PriceEntries)
                result += $"<priceEntry><agetype>{(int)priceEntry.AgeType}</agetype><tarifflevel>{(int)priceEntry.TariffLevel}</tarifflevel><price>{priceEntry.Price}</price></priceEntry>";

            return result;
        }

        private void GoTo_MainMenu()
        {
            MainMenu.Visibility = Visibility.Visible;
            PriceTable.Visibility = Visibility.Collapsed;
        }

        private void GoTo_PriceTable()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            PriceTable.Visibility = Visibility.Visible;
        }

       

        private void ShowError(string content, string caption = "FEHLER")
        {
            Label_ErrorWindow_Title.Content = caption;
            Label_ErrorWindow_Content.Content = content;
            ErrorWindow.Visibility = Visibility.Visible;
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
