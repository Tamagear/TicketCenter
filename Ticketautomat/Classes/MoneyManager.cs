using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class MoneyManager
    {
        /// <summary>
        /// Attribut moneyFillState, welches als Dictionary dargestellt wird
        /// </summary>
        private IEnumerable<Money> m_moneyFillState = new Dictionary<int, int>;

        private int m_ticketPaperLeft;

        private IEnumerable<Money> m_currentlyInsertedMoney;

        /// <summary>
        /// Methoden der Klasse MoneyManager
        /// </summary>
        public InsertMoney(p_money)
        {
            return m_currentlyInsterdMoney += p_money;
        }
        public GetChange()
        {
            return p_money;
        }
        public void CancelMoneyInsertion()
        {
            
        }
    }
}
