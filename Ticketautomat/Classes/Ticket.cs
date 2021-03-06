using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.BarCoding;
using System;

namespace Ticketautomat.Classes
{
    public class Ticket
    {
        private DateTime m_date;
        private Profile m_customer;
        private Station m_startStation;
        private Station m_targetDestination;
        private PriceEntry m_priceEntry = null;

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

        public Ticket(DateTime p_date, Profile p_customer, Station p_startStation, Station p_targetDestination,
            PriceEntry p_priceEntry)
        {
            m_date = p_date;
            m_customer = p_customer;
            m_startStation = p_startStation;
            m_targetDestination = p_targetDestination;
            m_priceEntry = p_priceEntry;
        }

        public PriceEntry PriceEntry { get { return this.m_priceEntry; } set { this.m_priceEntry = value; } }
        /// <summary>
        /// Erstellt eine Pdf mit den Informationen des Tickets
        /// </summary>
        /// <param name="p_Text">Der Name der Datei</param>
        public void ToPDF(string p_Text)
        {

            Document document = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);
            Label datumtext = new Label("Datum: " + Date, 0, 200, 504, 100, Font.Helvetica, 18, TextAlign.Left);
            page.Elements.Add(datumtext);
            Label Namenstext = new Label("Name: " + Customer.Name, 300, 200, 504, 100, Font.Helvetica, 18, TextAlign.Left);
            page.Elements.Add(Namenstext);
            Label Starttext = new Label("Startstation: " + StartStation.StationName, 0, 250, 504, 100, Font.Helvetica, 18, TextAlign.Left);
            page.Elements.Add(Starttext);
            Label Endtext = new Label("Endstation: " + TargetDestination.StationName, 300, 250, 504, 100, Font.Helvetica, 18, TextAlign.Left);
            page.Elements.Add(Endtext);
            Label Preisstufe = new Label("Preisstufe: " + PriceEntry, 0, 300, 504, 100, Font.Helvetica, 18, TextAlign.Left);
            page.Elements.Add(Preisstufe);
            Label Ticketcenter = new Label("TicketCenter", 200, 680, 504, 100, Font.Helvetica, 18, TextAlign.Left);
            page.Elements.Add(Ticketcenter);
            Image image = new Image("Resources/softwareIcon.png", 0, 0, 0.2f);
            page.Elements.Add(image);
            document.Draw(p_Text);
        }

        //public bool Equals(Ticket other)
        //{
        //    if (other is null)
        //        return false;

        //    if (ReferenceEquals(this, other))
        //        return true;

        //    if (GetType() != other.GetType())
        //        return false;

        //    return m_customer.Name.Equals(other.m_customer.Name)
        //        && other.StartStation.Equals(StartStation) && other.TargetDestination.Equals(TargetDestination)
        //        && other.PriceEntry.Equals(PriceEntry);
        //}

        //public static bool operator ==(Ticket left, Ticket right)
        //{
        //    if (left is null)
        //    {
        //        if (right is null)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }

        //    return left.Equals(right);
        //}

        //public static bool operator !=(Ticket left, Ticket right) => !(left == right);
    }
}
