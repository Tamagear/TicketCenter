using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class EnumCollection
    {
        enum EAgeType
        {
            CHILD,
            REDUCED,
            ADULT,
            PENSIONER
        }
        enum EMoneyType
        {
            COIN,
            BILL
        }
        enum EStationZone
        {
            ZONE_A,
            ZONE_B,
            ZONE_C
        }
        enum EStatisticDisplay
        {
            TABLE,
            GRAPH
        }
        enum EStatisticTimeType
        {
            DAY,
            WEEK,
            MONTH,
            COMPLETE
        }
        enum ETariffLevel
        {
            TARIFF_A,
            TARIFF_B,
            TARIFF_C
        }
    }
}
