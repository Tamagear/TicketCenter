using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class Manager
    {
        private Profile m_currentUser = null;
        private bool m_enabled = true;
        private float m_timeUntilTimeout = 0;
        private List<LogEntry> m_logEntries = new List<LogEntry>();
        private PriceEntry[,] m_priceEntries = new PriceEntry[4, 3];

        private const float TIMEOUT_THRESHOLD = 180f;

        public Profile CurrentUser { get => m_currentUser; set => m_currentUser = value; }
        public bool Enabled { get => m_enabled; set => m_enabled = value; }
        public float TimeUntilTimeout { get => m_timeUntilTimeout; set => m_timeUntilTimeout = value; }
        public List<LogEntry> LogEntries { get => m_logEntries; set => m_logEntries = value; }
        public PriceEntry[,] PriceEntries { get => m_priceEntries; set => m_priceEntries = value; }


        /// <summary>
        /// Erstellt eine Managerklasse mit einen Nutzer
        /// </summary>
        /// <param name="p_currentUser">Der Nutzer welcher am Automaten ist</param>
        public Manager(Profile p_currentUser)
        {
            CurrentUser = p_currentUser;
            PriceEntries.Add(new PriceEntry(EnumCollection.EAgeType.ADULT, EnumCollection.ETariffLevel.TARIFF_A, 2.5f));
            PriceEntries.Add(new PriceEntry(EnumCollection.EAgeType.CHILD, EnumCollection.ETariffLevel.TARIFF_A, 1.5f));
            PriceEntries.Add(new PriceEntry(EnumCollection.EAgeType.PENSIONER, EnumCollection.ETariffLevel.TARIFF_A, 1.0f));
            PriceEntries.Add(new PriceEntry(EnumCollection.EAgeType.REDUCED, EnumCollection.ETariffLevel.TARIFF_A, 7.3f));
            Initialize();
        }

        /// <summary>
        /// Lädt die gespeicherten Daten
        /// </summary>
        /// 
        private void Initialize()
        {
            LoadSavedData();
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
        public void LoadSavedData(string input)
        {
            //Lade Logs
            LogEntries.Clear();
            List<string> logs = StringHelpers.XML_Get(input, "log");
            foreach (string line in logs)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    DateTime dateTime = DateTime.ParseExact(StringHelpers.XML_GetSingle(line, "datetime"), "g", null);
                    string author = StringHelpers.XML_GetSingle(line, "author");
                    string content = StringHelpers.XML_GetSingle(line, "content");
                    LogEntry log = new LogEntry(dateTime, author, content);
                    LogEntries.Add(log);
                }
            }

            //Lade PriceEntries
            PriceEntries = new PriceEntry[4,3];
            List<string> priceEntriesInput = StringHelpers.XML_Get(input, "priceEntry");

            if (priceEntriesInput.Count > 0)
            {
                foreach (string line in priceEntriesInput)
                {
                    if (!string.IsNullOrEmpty(line) &&
                        int.TryParse(StringHelpers.XML_GetSingle(line, "agetype"), out int ageType) &&
                        int.TryParse(StringHelpers.XML_GetSingle(line, "tarifflevel"), out int tarifflevel) &&
                        float.TryParse(StringHelpers.XML_GetSingle(line, "price"), out float price))
                    {
                        PriceEntries[ageType, tarifflevel] = new PriceEntry((EnumCollection.EAgeType)ageType, (EnumCollection.ETariffLevel)tarifflevel, price);
                    }
                }
            }
            else
            {
                //Standardwerte
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
    }
}
