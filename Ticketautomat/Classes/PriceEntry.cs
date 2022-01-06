using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat.Classes
{
    public class PriceEntry
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
        private string m_ageTypeString;
        private char m_tariffLevelChar;

        public EAgeType AgeType { get => m_ageType; set => m_ageType = value; }
        public ETariffLevel TariffLevel { get => m_tariffLevel; set => m_tariffLevel = value; }
        public float Price { get => m_price; set => m_price = value; }
        public string AgeTypeString { get => m_ageTypeString; }
        public char TariffLevelChar { get => m_tariffLevelChar; }

        public PriceEntry(EAgeType p_ageType, ETariffLevel p_tariffLevel, float p_Price)
        {
            m_ageType = p_ageType;
            m_tariffLevel = p_tariffLevel;
            m_price = p_Price;
            m_tariffLevelChar = (char)((int)m_tariffLevel + 65);
            switch (m_ageType)
            {
                case EAgeType.CHILD:
                    m_ageTypeString = "Kind";
                    break;
                case EAgeType.REDUCED:
                    m_ageTypeString = "Ermäßigt";
                    break;
                case EAgeType.ADULT:
                    m_ageTypeString = "Erwachsener";
                    break;
                case EAgeType.PENSIONER:
                    m_ageTypeString = "Senior";
                    break;
            }
        }

        public override string ToString()
        {
            return $"{m_ageTypeString} / Tarifstufe {m_tariffLevelChar} ({m_price:F2}€)";
        }

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
