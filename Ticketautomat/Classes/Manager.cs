using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class Manager
    {
        /// <summary>
        /// Das Profil welches mormentan an den Automaten ist.
        /// </summary>
        private Profile m_currentUser;
        /// <summary>
        /// Ob der Automat zur Verfügung steht
        /// </summary>
        private bool m_enabled = true;
        /// <summary>
        /// Zeit bis der Automat resettet
        /// </summary>
        private float m_timeUntilTimeout = TIMEOUT_THRESHOLD;
        private IEnumerable<LogEntry> m_logEntries;
        /// <summary>
        /// Wie lange der Automat braucht um zu resetten, nachdem eine Aktion gemacht wurde
        /// </summary>
        private const float TIMEOUT_THRESHOLD = 10f;
        /// <summary>
        /// Erstellt eine Managerklasse mit einen Nutzer
        /// </summary>
        /// <param name="p_currentUser">Der Nutzer welcher am Automaten ist</param>
        public Manager(Profile p_currentUser)
        {
            m_currentUser = p_currentUser;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {

        }
        /// <summary>
        /// Setzt die Zeit bis zum Timeout zum Maximum
        /// </summary>
        private void ResetTimeUntilTimeout()
        {
            m_timeUntilTimeout = TIMEOUT_THRESHOLD;
        }


        /// <summary>
        /// 
        /// </summary>
        public void LoadSavedData()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public void CancelAndResetTransactions()
        {
            m_currentUser.ResetShoppingCart();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_customer"></param>
        /// <param name="p_ticket"></param>
        /// <returns></returns>
        public bool ConfirmPurchase(Customer p_customer, Ticket p_ticket)
        {
            return true;
        }


        /// <summary>
        /// Erstellt ein Ticket und fügt es zum ShoppingCart hinzu
        /// </summary>
        /// <param name="p_ticket">Das Hinzuzufügende Ticket</param>
        public void CreateTicketInstance(Ticket p_ticket)
        {
            m_currentUser.AddToShoppingCart(p_ticket);
        }
        /// <summary>
        /// 
        /// </summary>
        public void GoToCheckout()
        {
            float preis = m_currentUser.GetFinalPrice();
        }
        /// <summary>
        /// 
        /// </summary>
        public void FinalizeTransaction()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public void ReturnToMain()
        {

        }
    }
}
