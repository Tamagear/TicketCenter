using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat.Classes
{
    class PriceEntry
    {
        /// <summary>
        /// Preisklasse der Altersgruppe
        /// </summary>
        private EAgeType m_ageType;
        /// <summary>
        /// Preisklasse für die Zone
        /// </summary>
        private ETariffLevel m_tariffLevel;
        /// <summary>
        /// Ticketpreis für die bestimmte Altersgruppe und Zone
        /// </summary>
        private float m_price;

        public EAgeType AgeType { get => m_ageType; set => m_ageType = value; }
        public ETariffLevel TariffLevel { get => m_tariffLevel; set => m_tariffLevel = value; }
        public float Price { get => m_price; set => m_price = value; }

        /// <summary>
        /// Ändert den Preis eines Tickets
        /// </summary>
        /// <param name="p_maintenanceProfile">Maintenance Profile, das den Preis ändern soll</param>
        /// <param name="p_price">Neuer Preis</param>
        public void ChangePrice(MaintenanceProfile p_maintenanceProfile, float p_price)
        {
            if (p_price >= 0)
            {
                p_maintenanceProfile.ChangePrice(m_ageType, m_tariffLevel, p_price);
            }
        }
    }
}
