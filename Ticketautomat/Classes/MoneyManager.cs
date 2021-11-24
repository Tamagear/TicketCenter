namespace Ticketautomat.Classes
{
    class MoneyManager
    {
        /// <summary>
        /// Attribut moneyFillState, welches als Dictionary dargestellt wird
        /// </summary>
        private Dictionary<Money, int> m_moneyFillState = new Dictionary<Money, int>();

        private int m_ticketPaperLeft;

        private List<Money> m_currentlyInsertedMoney;

        public void SetMoneyFillState(Dictionary<Money, int> p_moneyFillState)
        {
            m_moneyFillState = p_moneyFillState;
        }
        public void GetMoneyFillState()
        {
            return m_moneyFillState;
        }
        public int ticketPaperLeft { get => m_ticketPaperLeft; set => m_ticketPaperLeft = value; }


        /// Methoden der Klasse MoneyManager
        /// </summary>
        public InsertMoney(Dictionary<Money, int> p_insertMoney)
        {

        }
        public GetChange()
        {

        }
        public void CancelMoneyInsertion()
        {

        }
    }
}
