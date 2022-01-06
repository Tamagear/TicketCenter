using System;
using System.Collections.Generic;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat.Classes
{
    public class MoneyManager
    {
        private Dictionary<Money, int> m_moneyFillState = new Dictionary<Money, int>();
        private int m_ticketPaperLeft;
        private List<Money> m_currentlyInsertedMoney = new List<Money>();
        private List<Money> m_allMoneyTypes = new List<Money>();
        private float m_sumLeft = 0f;

        private const int TICKET_PAPER_PER_ROLL = 1000;

        public Dictionary<Money, int> MoneyFillState { get { return m_moneyFillState; } set { m_moneyFillState = value; } }
        public int TicketPaperLeft { get => m_ticketPaperLeft; set => m_ticketPaperLeft = value; }
        public List<Money> MoneyFillStateList { get { return m_currentlyInsertedMoney; } set { m_currentlyInsertedMoney = value; } }
        public float SumLeft { get => m_sumLeft; set => m_sumLeft = value; }
        public List<Money> AllMoneyTypes { get => m_allMoneyTypes; set => m_allMoneyTypes = value; }

        public MoneyManager(List<int> p_fillStates = null)
        {
            m_allMoneyTypes.Add(new Money(0.05f, EnumCollection.EMoneyType.COIN));
            m_allMoneyTypes.Add(new Money(0.10f, EnumCollection.EMoneyType.COIN));
            m_allMoneyTypes.Add(new Money(0.20f, EnumCollection.EMoneyType.COIN));
            m_allMoneyTypes.Add(new Money(0.50f, EnumCollection.EMoneyType.COIN));
            m_allMoneyTypes.Add(new Money(1.00f, EnumCollection.EMoneyType.COIN));
            m_allMoneyTypes.Add(new Money(2.00f, EnumCollection.EMoneyType.COIN));
            m_allMoneyTypes.Add(new Money(5.00f, EnumCollection.EMoneyType.BILL));
            m_allMoneyTypes.Add(new Money(10.00f, EnumCollection.EMoneyType.BILL));
            m_allMoneyTypes.Add(new Money(20.00f, EnumCollection.EMoneyType.BILL));
            m_allMoneyTypes.Add(new Money(50.00f, EnumCollection.EMoneyType.BILL));

            foreach (Money money in m_allMoneyTypes)
                m_moneyFillState.Add(money, 1);     //1 später durch geladenen Wert ersetzen
            //Für jeden Geldtypen einen Eintrag im Dictionary erstellen
            //p_fillStates nutzen, um die Menge an Geld reinzutun
        }

        public Money GetMoneyFromValue(float value)
        {
            switch(value)
            {
                case 0.05f: return m_allMoneyTypes[0];
                case 0.10f: return m_allMoneyTypes[1];
                case 0.20f: return m_allMoneyTypes[2];
                case 0.50f: return m_allMoneyTypes[3];
                case 1.00f: return m_allMoneyTypes[4];
                case 2.00f: return m_allMoneyTypes[5];
                case 5.00f: return m_allMoneyTypes[6];
                case 10.00f: return m_allMoneyTypes[7];
                case 20.00f: return m_allMoneyTypes[8];
                case 50.00f: return m_allMoneyTypes[9];
            }

            return null;
        }

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

        public void Refill(EMoneyType p_moneyType, out Dictionary<Money, int> refilledCount)
        {
            refilledCount = new Dictionary<Money, int>();
            //Automatisch alles Geld vom Typen p_moneyType auffüllen und die aufgefüllte Menge in refilledCount rein
        }

        /// <summary>
        /// Methode um Geldspeicher aufzufuellen
        /// Mengengerueste sind:
        /// 200 Scheine pro Scheinart
        /// 150 Stk. Muenzen pro Muenztyp(bis auf 1 und 2 ct)
        /// </summary>
        /// <param name="p_money">Geldtyp</param>
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
