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
        private List<PriceEntry> m_priceEntries = new List<PriceEntry>();

        private const float TIMEOUT_THRESHOLD = 10f;

        public Profile CurrentUser { get => m_currentUser; set => m_currentUser = value; }
        public bool Enabled { get => m_enabled; set => m_enabled = value; }
        public float TimeUntilTimeout { get => m_timeUntilTimeout; set => m_timeUntilTimeout = value; }
        public List<LogEntry> LogEntries { get => m_logEntries; set => m_logEntries = value; }
        public List<PriceEntry> PriceEntries { get => m_priceEntries; set => m_priceEntries = value; }


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
        private void ResetTimeUntilTimeout()
        {
            TimeUntilTimeout = TIMEOUT_THRESHOLD;
        }


        /// <summary>
        /// W.I.P
        /// </summary>
        public void LoadSavedData()
        {

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
