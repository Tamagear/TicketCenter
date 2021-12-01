namespace Ticketautomat.Classes
{
    class MoneyManager
    {

        private Dictionary<Money, int> m_moneyFillState = new Dictionary<Money, int>();

        private int m_ticketPaperLeft;

        private List<Money> m_currentlyInsertedMoney;

        /// <summary>
        /// Setter MoneyFillState
        /// </summary>
        /// <param name="p_moneyFillState"></param>
        public void SetMoneyFillState(Dictionary<Money, int> p_moneyFillState)
        {
            m_moneyFillState = p_moneyFillState;
        }

        /// <summary>
        /// Getter MoneyFillState
        /// </summary>
        public void GetMoneyFillState()
        {
            return m_moneyFillState;
        }

        /// <summary>
        ///  Getter & Setter ticketPaperLeft
        /// </summary>
        public int ticketPaperLeft { get => m_ticketPaperLeft; set => m_ticketPaperLeft = value; }

        /// <summary>
        /// Setter CurrentlyInsertedMoney
        /// </summary>
        /// <param name="p_currentlyInsertedMoney"></param>
        public void SetCurrentlyInsertedMoney(List<Money> p_currentlyInsertedMoney)
        {
            m_currentlyInsertedMoney = p_currentlyInsertedMoney;
        }

        /// <summary>
        /// Getter CurrentlyInsertedMoney
        /// </summary>
        public void GetCurrentlyInsertedMoney()
        {
            return m_currentlyInsertedMoney;
        }

        /// <summary>
        /// Methode zum Geld einzahlen
        /// </summary>
        /// <param name="p_insertMoney"></param>
        public InsertMoney(Dictionary<Money, int> p_insertMoney)
        {
            m_moneyFillState += p_insertMoney;
        }

        /// <summary>
        /// Methode 
        /// </summary>
        /// <returns></returns>
        public List<Money> GetChange()
        {
            //missing
        }

        /// <summary>
        /// currentlyInsertedMoney zurueck geben, dann zurücksetzen
        /// </summary>
        public void CancelMoneyInsertion()
        {
            //gebe m_currentlyInsertedMoney zurück
            this.m_currentlyInsertedMoney = null;
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
            this.m_moneyFillState.add(p_money, p_zahl);
        }

        /// <summary>
        /// Methode um Papier wieder aufzufüllen
        /// Mengengeruest Papierrolle, umfasst 1000 Tickets
        /// </summary>
        public void RefillTicketPaper()
        {
            this.ticketPaperLeft.set(1000);
        }
    }
}
