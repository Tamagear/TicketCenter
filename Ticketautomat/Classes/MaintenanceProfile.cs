using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat.Classes
{
    public class MaintenanceProfile : Profile
    {
        private String m_password = "BAHNFAHRENISTTOLL";
        private Manager m_manager = null;
        private MoneyManager m_moneyManager = null;

        public MaintenanceProfile(string p_name, string p_password)
        {
            m_name = p_name;
            m_password = p_password;
        }

        /// <summary>
        /// Versucht einen MaintenanceProfile einzuloggen.
        /// </summary>
        /// <returns></returns>
        public bool Login(string p_username, string p_password)
        {
            return m_name.Equals(p_username) && m_password.Equals(p_password);
        }
        /// <summary>
        /// Loggt den aktuellen Mitarbeiter aus.
        /// </summary>
        public void Logout()
        {
            //Tim
        }

        /// <summary>
        /// Ändert den Preis eines Preiseintrages.
        /// </summary>
        /// <param name="p_eAgeType"></param>
        /// <param name="p_eTariffLevel"></param>
        /// <param name="p_zahl"></param>
        public void ChangePrice(EAgeType p_eAgeType, ETariffLevel p_eTariffLevel, float p_zahl)
        {
            foreach(PriceEntry priceEntry in m_manager.PriceEntries)
            {
                if (priceEntry.AgeType == p_eAgeType && priceEntry.TariffLevel == p_eTariffLevel)
                {
                    priceEntry.Price = p_zahl;
                    break;
                }
            }
        }

        /// <summary>
        /// Aktiviert den Wartungsmodus.
        /// </summary>
        public void EnableMaster()
        {
            m_manager.Enabled = true;
        }

        /// <summary>
        /// Deaktiviert den Wartungsmodus.
        /// </summary>
        public void DisableMaster()
        {
            m_manager.Enabled = false;
        }

        /// <summary>
        /// Füllt einen Geldspeicher auf.
        /// </summary>
        /// <param name="p_money">Geldtyp</param>
        /// <param name="p_zahl">Anzahl, die nachzufüllen ist</param>
        public void RefillMoney(Money p_money, int p_zahl)
        {
            m_moneyManager.Refill(p_money, p_zahl);
        }
        /// <summary>
        /// Erneuert die Rolle an Ticketpapier.
        /// </summary>
        public void RefillTicketPaper()
        {
            m_moneyManager.RefillTicketPaper();
        }
        /// <summary>
        /// Speichert den aktuellen Zustand auf der Festplatte.
        /// </summary>
        public void SaveChanges()
        {

        }

        /// <summary>
        /// Löscht den Log.
        /// </summary>
        public void ClearLog()
        {
            m_manager.LogEntries.Clear();
        }
    }
}
