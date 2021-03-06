using ceTe.DynamicPDF.Merger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketautomat.Classes;

namespace Ticketautomat.Classes
{
    public class Profile
    {
        protected string m_name = "";
        protected Dictionary<Ticket, int> m_shoppingCart = new Dictionary<Ticket, int>();
        protected bool m_isMaintenance = false;

        public String Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public Dictionary<Ticket, int> ShoppingCart
        {
            get { return this.m_shoppingCart; }
            set { this.m_shoppingCart = value; }
        }

        public bool IsMaintenance
        {
            get { return this.m_isMaintenance; }
            set { this.m_isMaintenance = value; }
        }

        /// <summary>
        /// Die Methode entfernt sämtliche Tickets im Warenkorb.
        /// </summary>
        public void ResetShoppingCart()
        {

            m_shoppingCart.Clear();

        }

        /// <summary>
        /// Die Methode fügt einen Ticket zum Warenkorb hinzu.
        /// </summary>
        /// <param name="p_ticket">Ticket, den man hinzufügen will</param>
        public void AddToShoppingCart(Ticket p_ticket)
        {
            if (m_shoppingCart.TryGetValue(p_ticket, out int value))
                m_shoppingCart[p_ticket]++;
            else
                m_shoppingCart.Add(p_ticket, 1);
        }

        /// <summary>
        /// Die Methode entfernt einen Ticket aus dem Warenkorb.
        /// </summary>
        /// <param name="p_ticket">Der zu entfernende Ticket</param>
        public void RemoveFromShoppingCart(Ticket p_ticket)
        {

            if (m_shoppingCart.ContainsKey(p_ticket))
            {
                m_shoppingCart.Remove(p_ticket);
            }

        }

        public void DecreaseByOneFromShoppingCart(Ticket p_ticket)
        {
            if (m_shoppingCart.ContainsKey(p_ticket))
            {
                int i = m_shoppingCart[p_ticket]-1;
                m_shoppingCart.Remove(p_ticket);
                if (i > 0)
                {
                    m_shoppingCart.Add(p_ticket, i);
                }
            }
        }
        public void IncreaseByOneFromShoppingCart(Ticket p_ticket)
        {
            if (m_shoppingCart.ContainsKey(p_ticket))
            {
                int i = m_shoppingCart[p_ticket] + 1;
                m_shoppingCart.Remove(p_ticket);
                if (i > 0)
                {
                    m_shoppingCart.Add(p_ticket, i);
                }
            }
        }

        /// <summary>
        /// Die Methode ändert die Anzahl eines bestimmten Tickets.
        /// </summary>
        /// <param name="p_ticket">Der gewählte Ticket</param>
        /// <param name="p_zahl">Die Anzahl an Tickets, die man möchte</param>
        public void ChangeTicketCountTo(Ticket p_ticket, int p_zahl)
        {
            if (m_shoppingCart.ContainsKey(p_ticket))
            {
                m_shoppingCart[p_ticket] = p_zahl;
            }
        }

        /// <summary>
        /// Die Methode rechnet den Gesamtpreis aller Tickets im Warenkorb aus.
        /// </summary>
        /// <returns>Gesamtpreis aller Tickets im Warenkorb</returns>
        public float GetFinalPrice()
        {
            float price = 0;
            
            foreach (KeyValuePair<Ticket, int> t in m_shoppingCart)
            {
                price += (t.Key.PriceEntry.Price * t.Value);
            }

            return price;
        }

        /// <summary>
        /// Hängt die Pdf der Tickets zu einer Pdf aneinander
        /// </summary>
        /// <param name="p_text">Name der PDF</param>
        public void ExportToPDF(String p_text)
        {
            MergeDocument mergeDocument = new MergeDocument();
            foreach (KeyValuePair<Ticket, int> item in ShoppingCart)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    item.Key.ToPDF("CreatePdf.pdf");
                    mergeDocument.Append("CreatePdf.pdf");
                }

                mergeDocument.Draw(p_text);
            }
        }
    }
}
