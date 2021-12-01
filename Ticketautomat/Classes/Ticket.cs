using System;

namespace Ticketautomat.Classes
{
    public class Ticket
    {
        private DateTime m_date;
        private Profile m_customer;
        private Station m_startStation;
        private Station m_targetDestination;
        private PriceEntry m_priceEntry;

        public DateTime Date
        {
            get { return this.m_date; }
            set { m_date = value; }
        }
        public Profile Customer
        {
            get { return this.m_customer; }
            set { this.m_customer = value; }
        }
        public Station StartStation
        {
            get { return this.m_startStation; }
            set { this.m_startStation = value; }
        }
        public Station TargetDestination
        {
            get { return this.m_targetDestination; }
            set { this.m_targetDestination = value; }
        }
        public PriceEntry PriceEntry
        {
            get { return this.m_priceEntry; }
            set { this.m_priceEntry = value; }
        }




        public void ToPDF(string p_Text ) { }

    }
}
