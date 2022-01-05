namespace Ticketautomat.Classes
{
    public class Money
    {
        /// <summary>
        /// Geldbetrag
        /// </summary>
        private float m_value;
        /// <summary>
        /// Art des Geldes (Scheine oder Münzen)
        /// </summary>
        private EnumCollection.EMoneyType m_moneyType;
        
        public EnumCollection.EMoneyType MoneyType { get => m_moneyType; set => m_moneyType = value; }
        public float Value { get => m_value; set => m_value = value; }

        public Money(float p_value, EnumCollection.EMoneyType p_moneyType)
        {
            m_value = p_value;
            m_moneyType = p_moneyType;
        }
    }
}
