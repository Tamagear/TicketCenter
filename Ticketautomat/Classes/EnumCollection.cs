namespace Ticketautomat.Classes
{
    public class EnumCollection
    {
        public enum EAgeType
        {
            CHILD,
            REDUCED,
            ADULT,
            PENSIONER
        }
        public enum EMoneyType
        {
            COIN,
            BILL
        }
        public enum EStationZone
        {
            ZONE_A,
            ZONE_B,
            ZONE_C
        }
        public enum EStatisticDisplay
        {
            TABLE,
            GRAPH
        }
        public enum EStatisticTimeType
        {
            DAY,
            WEEK,
            MONTH,
            COMPLETE
        }
        public enum EStatisticTimespanType
        {
            TIMESPAN,
            PER_TIME_OF_DAY
        }
        public enum ETariffLevel
        {
            TARIFF_A,
            TARIFF_B,
            TARIFF_C
        }
    }
}
