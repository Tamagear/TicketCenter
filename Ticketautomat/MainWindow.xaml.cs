using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ticketautomat.Classes;

namespace Ticketautomat
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Version version = new Version(0,0,1);
        private string versionHints = string.Empty;

        private Profile currentProfile = null;
        private Manager manager = null;

        private static Timer timer = null;

        private const string softwareName = "TicketCenter";
        private const string versionStatus = "SNAPSHOT";  //Später: RELEASE
        private const string dir_versionHints = "";
        private const string dir_messageOfTheDay = "";
        private const string dir_ticketSpotlight = "";
        private const int timeDivider = 60;

        public MainWindow()
        {
            InitializeComponent();

            currentProfile = new Profile();
            manager = new Manager(currentProfile);
            OnTimerElapsed();

            timer = new Timer();
            timer.Elapsed += (sender, e) => { OnTimerElapsed(); };
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Start();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            Label_SoftwareTitleAndVersion.Content = $"{softwareName} Version {version}-{versionStatus}";
            Button_MainMenu_BuyButton_Adult.Content = $"Erwachsener\nAb {manager.PriceEntries.ToArray()[0].Price}€";
            Button_MainMenu_BuyButton_Child.Content = $"Kind\nAb {manager.PriceEntries.ToArray()[1].Price}€";
            Button_MainMenu_BuyButton_Pensioner.Content = $"Senior\nAb {manager.PriceEntries.ToArray()[2].Price}€";
            Button_MainMenu_BuyButton_Reduced.Content = $"Ermäßigt\nAb {manager.PriceEntries.ToArray()[3].Price}€";

            if (!string.IsNullOrEmpty(dir_versionHints) && File.Exists(dir_versionHints))
                versionHints = File.ReadAllText(dir_versionHints);

            if (!string.IsNullOrEmpty(dir_messageOfTheDay) && File.Exists(dir_messageOfTheDay))
                Label_MainMenu_MOTD.Content = File.ReadAllText(dir_messageOfTheDay);

            if (!string.IsNullOrEmpty(dir_ticketSpotlight) && File.Exists(dir_ticketSpotlight))
                Label_MainMenu_TicketSpotlight.Content = File.ReadAllText(dir_ticketSpotlight);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Version_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(versionHints + "Drücken Sie den Knopf, um Ihre Seele an Georg Hoever zu verkaufen.", "Rhetorische Frage", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void Button_MainMenu_GoToCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_BuyButton_Adult_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_BuyButton_Child_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_BuyButton_Pensioner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_BuyButton_Reduced_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_MainMenu_ShowPriceTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_MaintenanceLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnTimerElapsed()
        {
            DateTime now = DateTime.Now;
            //Label_TimeLeft.Content = $"Zeit bis zum Abbruch: {string.Format("{0}:{1:00}", manager.TimeUntilTimeout / timeDivider, Math.Abs(manager.TimeUntilTimeout % timeDivider))}";
            //Label_DateTime.Content = now.ToString("g");
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
