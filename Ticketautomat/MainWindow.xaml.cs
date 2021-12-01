using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

            //Erstelle Systemdaten: Existieren sie noch nicht, zeige eine Willkommensnachricht

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

            UpdateTexts();
            HandleSystemData();
            LoadFile();
        }

        private void HandleSystemData()
        {
            string fileDirectory = $"{Directory.GetCurrentDirectory()}/{dir_savedata}";
            string motdPath = $"{fileDirectory}/{dir_messageOfTheDay}";
            string spotlightPath = $"{fileDirectory}/{dir_ticketSpotlight}";
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

            if (!hasDirectory)
                MessageBox.Show(WELCOME_MESSAGE, "Willkommen beim TicketCenter!");
        }

        private void SaveFile()
        {
            string fileDirectory = $"{Directory.GetCurrentDirectory()}/{dir_savedata}";
            string tempFilePath = $"{fileDirectory}/temp_{RandomString(10)}.txt";
            string saveFilePath = $"{fileDirectory}/{dir_saveFile}";

            File.WriteAllText(tempFilePath, "Dies ist ein Test. Ob du's willst oder nicht.");  //Hier dann die Daten richtig serialisieren!          
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
            Button_MainMenu_BuyButton_Adult.Content = $"Erwachsener\nAb {manager.PriceEntries.ToArray()[0].Price.ToString("F2")}€";
            Button_MainMenu_BuyButton_Child.Content = $"Kind\nAb {manager.PriceEntries.ToArray()[1].Price.ToString("F2")}€";
            Button_MainMenu_BuyButton_Pensioner.Content = $"Senior\nAb {manager.PriceEntries.ToArray()[2].Price.ToString("F2")}€";
            Button_MainMenu_BuyButton_Reduced.Content = $"Ermäßigt\nAb {manager.PriceEntries.ToArray()[3].Price.ToString("F2")}€";

            string fileDirectory = $"{Directory.GetCurrentDirectory()}/{dir_savedata}";
            string motdPath = $"{fileDirectory}/{dir_messageOfTheDay}";
            string spotlightPath = $"{fileDirectory}/{dir_ticketSpotlight}";

            if (!string.IsNullOrEmpty(motdPath) && File.Exists(motdPath))
                Label_MainMenu_MOTD.Content = File.ReadAllText(motdPath);

            if (!string.IsNullOrEmpty(spotlightPath) && File.Exists(spotlightPath))
                Label_MainMenu_TicketSpotlight.Content = File.ReadAllText(spotlightPath);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void MainMenu_BuyButton_Adult_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
        }

        private void MainMenu_BuyButton_Child_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
        }

        private void MainMenu_BuyButton_Pensioner_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
        }

        private void MainMenu_BuyButton_Reduced_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
        }

        private void Button_MainMenu_ShowPriceTable_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
        }

        private void Button_MaintenanceLogin_Click(object sender, RoutedEventArgs e)
        {
            manager.ResetTimeUntilTimeout();
        }

        private void UpdateTimeTexts()
        {            
            DateTime now = DateTime.Now;
            int minutesUntilTimeout = (int)(manager.TimeUntilTimeout / TIME_DIVIDER);
            int secondsUntilTimeout = (int)(Math.Abs(manager.TimeUntilTimeout % TIME_DIVIDER));
            Label_TimeLeft.Content = $"Zeit bis zum Abbruch: {(minutesUntilTimeout < 10 ? "0" : string.Empty) + minutesUntilTimeout} : {(secondsUntilTimeout < 10 ? "0" : string.Empty) + secondsUntilTimeout}";
            Label_DateTime.Content = $"{now.ToString("g")} Uhr";
        }

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

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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
