
using System.Windows.Media;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat.Classes

{
    public class Station
    {
        private string m_stationName;
        private EStationZone m_stationZone;
        private int m_stationIndex;

        public string StationName
        {
            get { return this.m_stationName; }
            set { this.m_stationName = value; }
        }


        public int StationIndex { get => m_stationIndex; set => m_stationIndex = value; }
        internal EStationZone StationZone { get => m_stationZone; set => m_stationZone = value; }

        public Brush GetStationColor()
        {
            switch (m_stationZone)
            {
                case EStationZone.ZONE_A:
                    return new SolidColorBrush(Color.FromRgb(104, 180, 45));
                case EStationZone.ZONE_B:
                    return new SolidColorBrush(Color.FromRgb(0, 177, 178));
                case EStationZone.ZONE_C:
                    return new SolidColorBrush(Color.FromRgb(250, 187, 22));
            }

            return new SolidColorBrush(Color.FromRgb(180, 45, 45));
        }
    }
}
