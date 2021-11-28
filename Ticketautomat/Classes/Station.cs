
namespace Ticketautomat.Classes
{
    public class Station
    {
        private string m_stationName;
        private EStationZone m_stationZone;

       public string StationName
        {
            get { return this.m_stationName; }
            set { this.m_stationName = value; }
        }
        public EStationZone StationZone
        {
            get { return this.m_stationZone; }
            set { this.m_stationZone = value; }
        }

    }
}
