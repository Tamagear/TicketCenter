
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
    }
}
