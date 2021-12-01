using System;
using System.Collections.Generic;


namespace Ticketautomat.Classes
{
    class MoneyManager
    {
        private Dictionary<Money, int> m_moneyFillState = new Dictionary<Money, int>();
        private int m_ticketPaperLeft;
        private List<Money> m_currentlyInsertedMoney;

        private const int TICKET_PAPER_PER_ROLL = 1000;

        public Dictionary<Money, int> MoneyFillState { get { return m_moneyFillState; } set { m_moneyFillState = value; } }
        public int TicketPaperLeft { get => m_ticketPaperLeft; set => m_ticketPaperLeft = value; }
        public List<Money> MoneyFillStateList { get { return m_currentlyInsertedMoney; } set { m_currentlyInsertedMoney = value; } }

        /// <summary>
        /// Methode zum Geld einzahlen
        /// </summary>
        /// <param name="p_insertMoney"></param>
        public void InsertMoney(Money p_money, int p_count)
        {
            if (m_moneyFillState.ContainsKey(p_money))
                m_moneyFillState[p_money] += p_count;
            else
                m_moneyFillState.Add(p_money, p_count);
        }

        /// <summary>
        /// Methode 
        /// </summary>
        /// <returns></returns>
        public List<Money> GetChange()
        {
            //missing
            //was fehlt: Wie viel Geld muss denn überhaupt eingeworfen werden?
            m_currentlyInsertedMoney.Clear();
            return null;
        }

        /// <summary>
        /// currentlyInsertedMoney zurueck geben, dann zurücksetzen
        /// </summary>
        public void CancelMoneyInsertion()
        {
            //Wie viel Geld muss eingeworfen werden => 0
            GetChange();
        }

        /// <summary>
        /// Methode um Geldspeicher aufzufuellen
        /// Mengengerueste sind:
        /// 200 Scheine pro Scheinart
        /// 150 Stk. Muenzen pro Muenztyp(bis auf 1 und 2 ct)
        /// </summary>
        /// /// <param name="p_money">Geldtyp</param>
        /// <param name="p_zahl">Anzahl zum auffüllen</param>
        public void Refill(Money p_money, int p_zahl)
        {
            m_moneyFillState.Add(p_money, p_zahl);
        }

        /// <summary>
        /// Methode um Papier wieder aufzufüllen
        /// Mengengeruest Papierrolle, umfasst 1000 Tickets
        /// </summary>
        public void RefillTicketPaper()
        {
            TicketPaperLeft = TICKET_PAPER_PER_ROLL;
        }
    }
}
