using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class Money
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
    }
}
