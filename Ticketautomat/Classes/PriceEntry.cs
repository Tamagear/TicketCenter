using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ChangePrice(MaintenanceProfile p_maintenanceProfile)
        {

        }
    }
}
