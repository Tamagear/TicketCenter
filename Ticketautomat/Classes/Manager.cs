using System;
using System.Collections.Generic;

namespace Ticketautomat.Classes
{
    class Manager
    {
        private Profile m_currentUser = null;
        private List<MaintenanceProfile> m_maintenanceProfiles = new List<MaintenanceProfile>();
        private bool m_enabled = true;
        private float m_timeUntilTimeout = 0;
        private List<LogEntry> m_logEntries = new List<LogEntry>();
        private PriceEntry[,] m_priceEntries = new PriceEntry[4, 3];
        private MoneyManager m_moneyManager = new MoneyManager();
        private StationGraph m_stationGraph = new StationGraph();

        private const float TIMEOUT_THRESHOLD = 180f;

        public Profile CurrentUser { get => m_currentUser; set => m_currentUser = value; }
        public bool Enabled { get => m_enabled; set => m_enabled = value; }
        public float TimeUntilTimeout { get => m_timeUntilTimeout; set => m_timeUntilTimeout = value; }
        public List<LogEntry> LogEntries { get => m_logEntries; set => m_logEntries = value; }
        public PriceEntry[,] PriceEntries { get => m_priceEntries; set => m_priceEntries = value; } //AgeType, TariffLevel
        public MoneyManager MoneyManager { get => m_moneyManager; }
        public StationGraph StationGraph { get => m_stationGraph; }

        /// <summary>
        /// Erstellt eine Managerklasse mit einen Nutzer
        /// </summary>
        public Manager()
        {
            CurrentUser = new Profile();
            Initialize();
        }

        /// <summary>
        /// Erstellt eine Managerklasse mit einen Nutzer
        /// </summary>
        /// <param name="p_currentUser">Der Nutzer welcher am Automaten ist</param>
        public Manager(Profile p_currentUser)
        {
            CurrentUser = p_currentUser;
            Initialize();
        }

        /// <summary>
        /// Platz für Initialisierungen
        /// </summary>
        /// 
        private void Initialize()
        {
            CurrentUser.Name = "Kunde";
            m_maintenanceProfiles.Add(new MaintenanceProfile("test", "test")); //ENTFERNEN!
            m_maintenanceProfiles.Add(new MaintenanceProfile("Niederhaeuser", "BAHNFAHRENISTTOLL"));
            m_maintenanceProfiles.Add(new MaintenanceProfile("Kueppers", "BAHNFAHRENISTSUPER"));
            m_maintenanceProfiles.Add(new MaintenanceProfile("Ochsendorf", "BAHNFAHRENISTMEGA"));
        }

        /// <summary>
        /// Setzt die Zeit bis zum Timeout zum Maximum
        /// </summary>
        public void ResetTimeUntilTimeout()
        {
            TimeUntilTimeout = TIMEOUT_THRESHOLD;
        }

        /// <summary>
        /// Lädt LogEntries und PriceEntries aus der Speicherdatei ein.
        /// </summary>
        public void LoadSavedData(string p_input)
        {
            //Lade Logs
            LogEntries.Clear();
            List<string> logs = StringHelpers.XML_Get(p_input, "log");            
            foreach (string line in logs)
            {
                if (!string.IsNullOrEmpty(line) && StringHelpers.XML_IsValid(line, "date"))
                {
                    Console.WriteLine(line);
                    string date = StringHelpers.XML_GetSingle2(line, "date");                    
                    string author = StringHelpers.XML_GetSingle2(line, "author");
                    string content = StringHelpers.XML_GetSingle2(line, "content");

                    LogEntry log = new LogEntry(date, author, content);
                    LogEntries.Add(log);
                }
            }

            //Lade PriceEntries
            PriceEntries = new PriceEntry[4,3];
            List<string> priceEntriesInput = StringHelpers.XML_Get(p_input, "priceEntry");

            if (priceEntriesInput.Count > 0)
            {
                foreach (string line in priceEntriesInput)
                {
                    if (!string.IsNullOrEmpty(line) &&
                        int.TryParse(StringHelpers.XML_Get(line, "agetype")[0], out int ageType) &&   //https://www.youtube.com/watch?v=q6FOh1N6V5A
                        int.TryParse(StringHelpers.XML_GetSingle(line, "tarifflevel"), out int tarifflevel) &&
                        float.TryParse(StringHelpers.XML_GetSingle(line, "price"), out float price))
                    {
                        PriceEntries[ageType, tarifflevel] = new PriceEntry((EnumCollection.EAgeType)ageType, (EnumCollection.ETariffLevel)tarifflevel, price);
                    }
                }
            }
            else
            {
                WriteDefaultPriceEntries();
            }
        }

        /// <summary>
        /// Logt den Maintanance benutzer aus und resettet den ShoppingCart
        /// </summary>
        public void CancelAndResetTransactions()
        {
            if (CurrentUser.IsMaintenance)
            {
                ((MaintenanceProfile)CurrentUser).Logout();
            }
            CurrentUser.ResetShoppingCart();
        }

        /// <summary>
        /// W.I.P
        /// </summary>
        /// <param name="p_customer"></param>
        /// <param name="p_ticket"></param>
        /// <returns></returns>
        public bool ConfirmPurchase(Profile p_customer, Ticket p_ticket)
        {
            return true;
        }

        /// <summary>
        /// W.I.P
        /// </summary>
        /// <param name="p_ticket">Das Hinzuzufügende Ticket</param>
        public void CreateTicketInstance(Ticket p_ticket)
        {

        }

        /// <summary>
        /// W.I.P
        /// </summary>
        public void FinalizeTransaction()
        {

        }

        /// <summary>
        /// Schreibt die Standardwerte in die Preistabelle.
        /// </summary>
        public void WriteDefaultPriceEntries()
        {
            //Standardwerte, siehe Tabelle vom Kunden
            PriceEntries[0, 0] = new PriceEntry((EnumCollection.EAgeType)0, (EnumCollection.ETariffLevel)0, 1.3f);
            PriceEntries[1, 0] = new PriceEntry((EnumCollection.EAgeType)1, (EnumCollection.ETariffLevel)0, 1.5f);
            PriceEntries[2, 0] = new PriceEntry((EnumCollection.EAgeType)2, (EnumCollection.ETariffLevel)0, 2f);
            PriceEntries[3, 0] = new PriceEntry((EnumCollection.EAgeType)3, (EnumCollection.ETariffLevel)0, 1.8f);
            PriceEntries[0, 1] = new PriceEntry((EnumCollection.EAgeType)0, (EnumCollection.ETariffLevel)1, 1.6f);
            PriceEntries[1, 1] = new PriceEntry((EnumCollection.EAgeType)1, (EnumCollection.ETariffLevel)1, 1.9f);
            PriceEntries[2, 1] = new PriceEntry((EnumCollection.EAgeType)2, (EnumCollection.ETariffLevel)1, 2.5f);
            PriceEntries[3, 1] = new PriceEntry((EnumCollection.EAgeType)3, (EnumCollection.ETariffLevel)1, 2.2f);
            PriceEntries[0, 2] = new PriceEntry((EnumCollection.EAgeType)0, (EnumCollection.ETariffLevel)2, 1.9f);
            PriceEntries[1, 2] = new PriceEntry((EnumCollection.EAgeType)1, (EnumCollection.ETariffLevel)2, 2.3f);
            PriceEntries[2, 2] = new PriceEntry((EnumCollection.EAgeType)2, (EnumCollection.ETariffLevel)2, 3f);
            PriceEntries[3, 2] = new PriceEntry((EnumCollection.EAgeType)3, (EnumCollection.ETariffLevel)2, 2.6f);
        }

        public bool TryLogin(string p_username, string p_password)
        {
            //ONLOGIN!
            for (int i = 0; i<m_maintenanceProfiles.Count; i++)
            {
                if (m_maintenanceProfiles[i].Login(p_username, p_password))
                {
                    m_currentUser = m_maintenanceProfiles[i];
                    return true;
                }
            }

            return false;
        }
    }
}
