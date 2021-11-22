using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class Ticket
    {
        private DateTime m_date;
        private Profile m_customer;
        private Station m_startStation;
        private Station m_targetDestination;
        private PriceEntry m_priceEntry;

        public void ToPDF(string p_Text ) { }

    }
}
