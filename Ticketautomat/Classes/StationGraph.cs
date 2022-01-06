using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ticketautomat.Classes.EnumCollection;

namespace Ticketautomat.Classes
{
    public class StationGraph
    {
        private Graph graph = new Graph();

        public Graph Graph { get => graph; set => graph = value; }

        public StationGraph()
        {
            Graph.addStation("Rößle", EStationZone.ZONE_A);                   //0
            Graph.addStation("Rust", EStationZone.ZONE_A);                    //1
            Graph.addStation("Logos", EStationZone.ZONE_A);                   //2
            Graph.addStation("Grosfeld", EStationZone.ZONE_A);                //3
            Graph.addStation("Altstadt", EStationZone.ZONE_A);                //4
            Graph.addStation("Nier2", EStationZone.ZONE_A);                   //5
            Graph.addStation("Nier1", EStationZone.ZONE_B);                   //6
            Graph.addStation("Keinfurt", EStationZone.ZONE_B);                //7
            Graph.addStation("Trutztrum", EStationZone.ZONE_B);               //8
            Graph.addStation("Hollstein", EStationZone.ZONE_B);               //9
            Graph.addStation("Bierö", EStationZone.ZONE_B);                   //10
            Graph.addStation("Swefurt", EStationZone.ZONE_B);                 //11
            Graph.addStation("Ruste", EStationZone.ZONE_B);                   //12
            Graph.addStation("Wolkenstadt", EStationZone.ZONE_B);             //13
            Graph.addStation("Wissenstein", EStationZone.ZONE_B);             //14
            Graph.addStation("Echsenstadt", EStationZone.ZONE_B);             //15
            Graph.addStation("Rußstein", EStationZone.ZONE_B);                //16
            Graph.addStation("Stolze", EStationZone.ZONE_C);                  //17
            Graph.addStation("Wolfhain", EStationZone.ZONE_C);                //18
            Graph.addStation("Rissstein", EStationZone.ZONE_C);               //19
            Graph.addStation("Altschauerberg", EStationZone.ZONE_C);          //20
            Graph.addStation("Simmerstein", EStationZone.ZONE_C);             //21
            Graph.addStation("Bad Leer", EStationZone.ZONE_C);                //22
            Graph.addStation("Nebelburg", EStationZone.ZONE_C);               //23
            Graph.addStation("Obertal", EStationZone.ZONE_C);                 //24
            Graph.addStation("Neudorf", EStationZone.ZONE_C);                 //25
            Graph.addStation("Düstersteig", EStationZone.ZONE_C);             //26
            Graph.addStation("Tanntal", EStationZone.ZONE_C);                 //27
            Graph.addStation("Sems", EStationZone.ZONE_C);                    //28
            Graph.addStation("Rotburg", EStationZone.ZONE_C);                 //29
            Graph.addStation("Olde", EStationZone.ZONE_C);                    //30
            Graph.addStation("Untersee", EStationZone.ZONE_C);                //31
            Graph.addStation("Bruchdorf", EStationZone.ZONE_C);               //32


            //Edges Station 0 Roeßle
            Graph.AddEdge(Graph.GetStation(0), Graph.GetStation(1));
            Graph.AddEdge(Graph.GetStation(0), Graph.GetStation(2));
            Graph.AddEdge(Graph.GetStation(0), Graph.GetStation(3));
            Graph.AddEdge(Graph.GetStation(0), Graph.GetStation(4));
            Graph.AddEdge(Graph.GetStation(0), Graph.GetStation(5));
            Graph.AddEdge(Graph.GetStation(0), Graph.GetStation(8));

            //Edges Station 1 Rust
            Graph.AddEdge(Graph.GetStation(1), Graph.GetStation(2));
            Graph.AddEdge(Graph.GetStation(1), Graph.GetStation(3));
            Graph.AddEdge(Graph.GetStation(1), Graph.GetStation(10));
            Graph.AddEdge(Graph.GetStation(1), Graph.GetStation(11));
            Graph.AddEdge(Graph.GetStation(1), Graph.GetStation(12));

            //Edges Station 2 Logos
            Graph.AddEdge(Graph.GetStation(2), Graph.GetStation(8));
            Graph.AddEdge(Graph.GetStation(2), Graph.GetStation(9));
            Graph.AddEdge(Graph.GetStation(2), Graph.GetStation(10));

            //Edges Station 3 Grasfeld
            Graph.AddEdge(Graph.GetStation(3), Graph.GetStation(4));
            Graph.AddEdge(Graph.GetStation(3), Graph.GetStation(12));
            Graph.AddEdge(Graph.GetStation(3), Graph.GetStation(13));
            Graph.AddEdge(Graph.GetStation(3), Graph.GetStation(14));
            Graph.AddEdge(Graph.GetStation(3), Graph.GetStation(15));

            //Edges Station 4 Altstadt
            Graph.AddEdge(Graph.GetStation(4), Graph.GetStation(5));
            Graph.AddEdge(Graph.GetStation(4), Graph.GetStation(6));
            Graph.AddEdge(Graph.GetStation(4), Graph.GetStation(15));
            Graph.AddEdge(Graph.GetStation(4), Graph.GetStation(16));

            //Edges Station 5 Nier2
            Graph.AddEdge(Graph.GetStation(5), Graph.GetStation(6));
            Graph.AddEdge(Graph.GetStation(5), Graph.GetStation(7));
            Graph.AddEdge(Graph.GetStation(5), Graph.GetStation(8));

            //Edges Station 6 Nier1
            Graph.AddEdge(Graph.GetStation(6), Graph.GetStation(7));
            Graph.AddEdge(Graph.GetStation(6), Graph.GetStation(16));
            Graph.AddEdge(Graph.GetStation(6), Graph.GetStation(18));

            //Edges Station 7 Keinfurt
            Graph.AddEdge(Graph.GetStation(7), Graph.GetStation(8));
            Graph.AddEdge(Graph.GetStation(7), Graph.GetStation(18));
            Graph.AddEdge(Graph.GetStation(7), Graph.GetStation(19));

            //Edges Station 8 Trutztrum
            Graph.AddEdge(Graph.GetStation(8), Graph.GetStation(9));
            Graph.AddEdge(Graph.GetStation(8), Graph.GetStation(19));
            Graph.AddEdge(Graph.GetStation(8), Graph.GetStation(20));

            //Edges Station 9 Hollstein
            Graph.AddEdge(Graph.GetStation(9), Graph.GetStation(10));
            Graph.AddEdge(Graph.GetStation(9), Graph.GetStation(20));
            Graph.AddEdge(Graph.GetStation(9), Graph.GetStation(21));

            //Edges Station 10 Bierö
            Graph.AddEdge(Graph.GetStation(10), Graph.GetStation(11));
            Graph.AddEdge(Graph.GetStation(10), Graph.GetStation(21));
            Graph.AddEdge(Graph.GetStation(10), Graph.GetStation(22));

            //Edges Station 11 Swefurt
            Graph.AddEdge(Graph.GetStation(11), Graph.GetStation(12));
            Graph.AddEdge(Graph.GetStation(11), Graph.GetStation(22));
            Graph.AddEdge(Graph.GetStation(11), Graph.GetStation(23));
            Graph.AddEdge(Graph.GetStation(11), Graph.GetStation(23));

            //Edges Station 12 Ruste
            Graph.AddEdge(Graph.GetStation(12), Graph.GetStation(13));
            Graph.AddEdge(Graph.GetStation(12), Graph.GetStation(24));
            Graph.AddEdge(Graph.GetStation(12), Graph.GetStation(25));

            //Edges Station 13 Wolkenstadt
            Graph.AddEdge(Graph.GetStation(13), Graph.GetStation(14));
            Graph.AddEdge(Graph.GetStation(13), Graph.GetStation(25));
            Graph.AddEdge(Graph.GetStation(13), Graph.GetStation(26));
            Graph.AddEdge(Graph.GetStation(13), Graph.GetStation(27));

            //Edges Station 14 Wissstein
            Graph.AddEdge(Graph.GetStation(14), Graph.GetStation(15));
            Graph.AddEdge(Graph.GetStation(14), Graph.GetStation(27));
            Graph.AddEdge(Graph.GetStation(14), Graph.GetStation(28));
            Graph.AddEdge(Graph.GetStation(14), Graph.GetStation(29));
            Graph.AddEdge(Graph.GetStation(14), Graph.GetStation(30));

            //Edges Station 15 Echsenstadt
            Graph.AddEdge(Graph.GetStation(15), Graph.GetStation(16));
            Graph.AddEdge(Graph.GetStation(15), Graph.GetStation(30));
            Graph.AddEdge(Graph.GetStation(15), Graph.GetStation(31));

            //Edges Station 16 Rußstein
            Graph.AddEdge(Graph.GetStation(16), Graph.GetStation(17));
            Graph.AddEdge(Graph.GetStation(16), Graph.GetStation(31));
            Graph.AddEdge(Graph.GetStation(16), Graph.GetStation(32));

            //Edges Station 17 Stolze
            Graph.AddEdge(Graph.GetStation(17), Graph.GetStation(18));
            Graph.AddEdge(Graph.GetStation(17), Graph.GetStation(32));

            //Edges Station 18 Wolfhain
            Graph.AddEdge(Graph.GetStation(18), Graph.GetStation(19));
            Graph.AddEdge(Graph.GetStation(18), Graph.GetStation(32));

            //Edges Station 19 Rissstein
            Graph.AddEdge(Graph.GetStation(19), Graph.GetStation(20));

            //Edges Station 20 Altschauerberg
            Graph.AddEdge(Graph.GetStation(20), Graph.GetStation(21));

            //Edges Station 21 Simmerstein
            Graph.AddEdge(Graph.GetStation(21), Graph.GetStation(22));

            //Edges Station 22 Bad Leer
            Graph.AddEdge(Graph.GetStation(22), Graph.GetStation(23));

            //Edges Station 23 Nebelburg
            Graph.AddEdge(Graph.GetStation(23), Graph.GetStation(24));

            //Edges Station 24 Obertal
            Graph.AddEdge(Graph.GetStation(24), Graph.GetStation(25));

            //Edges Station 25 Neudorf
            Graph.AddEdge(Graph.GetStation(25), Graph.GetStation(26));

            //Edges Station 26 Düstersteig
            Graph.AddEdge(Graph.GetStation(26), Graph.GetStation(27));

            //Edges Station 27 Tanntal
            Graph.AddEdge(Graph.GetStation(27), Graph.GetStation(28));

            //Edges Station 28 Sems
            Graph.AddEdge(Graph.GetStation(28), Graph.GetStation(29));

            //Edges Station 29 Rotburg
            Graph.AddEdge(Graph.GetStation(29), Graph.GetStation(30));

            //Edges Station 30 Olde


            //Edges Station 31 Untersee
            Graph.AddEdge(Graph.GetStation(31), Graph.GetStation(32));
        }

        /// <summary>
        /// Die Methode gibt die 2 Möglichen Routen zwischen 2 Stationen aus.
        /// </summary>
        /// <param name="p_start">Startstation als Station-Objekt</param>
        /// <param name="p_ende">Endstation als Station-Objekt</param>
        /// <returns>Eine Liste, die 2 Listen von Stationen enthält. Die erste Liste hat den günstigsten Weg. Die Zweite hat den kürzesten</returns>
        public List<List<Station>> GetRoutes(Station p_start, Station p_ende)
        {
            List<List<Station>> ausgabe = new List<List<Station>>();
            ausgabe.Add(graph.CheapestPath(p_start, p_ende));
            ausgabe.Add(graph.ShortestPath(p_start, p_ende));
            return ausgabe;
        }

        public ETariffLevel GetRouteTariffLevel(List<Station> route)
        {
            List<EStationZone> zones = new List<EStationZone>();
            foreach (Station station in route)
            {
                if (!zones.Contains(station.StationZone))
                    zones.Add(station.StationZone);
            }

            return (ETariffLevel)zones.Count - 1;
        }
    }

    public class Graph
    {
        private int m_anzahlStationen;
        List<Station> m_stationen;

        private List<List<Station>> m_verbindungen;
        public List<List<Station>> getVerbindungen()
        {
            return m_verbindungen;
        }

        public Station GetStation(int index)
        {
            return m_stationen[index];
        }
        public List<Station> GetStationen()
        {
            return m_stationen;
        }
        // Constructor
        public Graph()
        {
            m_anzahlStationen = 0;
            m_verbindungen = new List<List<Station>>();
            m_stationen = new List<Station>();
        }

        /// <summary>
        /// Fügt eine Verbindung zwischen 2 Stationen hinzu
        /// </summary>
        /// <param name="p_start">Startstation</param>
        /// <param name="p_ende">Endstation</param>
        public void AddEdge(Station p_start, Station p_ende)
        {
            if (!m_verbindungen[p_start.StationIndex].Contains(p_ende))
            {
                m_verbindungen[p_start.StationIndex].Add(p_ende); // Add p_ende to p_start's list.
                m_verbindungen[p_ende.StationIndex].Add(p_start); // Add p_start to p_ende's list.
            }
        }

        /// <summary>
        /// Fügt eine neue Station hinzu
        /// </summary>
        /// <param name="p_stationName">Name der Station</param>
        /// <param name="p_stationZone">Zone der Station</param>
        public void addStation(string p_stationName, EStationZone p_stationZone)
        {
            Station neu = new Station();
            neu.StationName = p_stationName;
            neu.StationZone = p_stationZone;
            neu.StationIndex = m_anzahlStationen;
            m_stationen.Add(neu);
            m_verbindungen.Add(new List<Station>());
            m_anzahlStationen++;
        }

        public List<Station> ShortestPath(Station p_start, Station p_ende)
        {
            return ShortestPath(p_start, p_ende, m_verbindungen, m_stationen);
        }

        /// <summary>
        /// Die Methode berechnet den kürzesten Weg zwischen zwei Stationen
        /// </summary>
        /// <param name="p_start">Startstation</param>
        /// <param name="p_ende">Endstation</param>
        /// <param name="adj">Stationsverbindungen</param>
        /// <param name="stationen">Liste aller Stationen</param>
        /// <returns></returns>
        private List<Station> ShortestPath(Station p_start, Station p_ende, List<List<Station>> adj, List<Station> stationen)
        {
            List<Station> path = new List<Station>();
            int[] pred = new int[m_anzahlStationen];
            int[] dist = new int[m_anzahlStationen];
            BFS(adj, p_start, p_ende, m_anzahlStationen, pred, dist);
            path.Add(p_ende);
            int current = p_ende.StationIndex;
            while (pred[current] != -1)
            {
                path.Add(stationen[pred[current]]);
                current = pred[current];
            }
            
            return path;
        }

        /// <summary>
        /// Die Methode berechnet den günstigsten Weg zwischen zwei Stationen
        /// </summary>
        /// <param name="p_start">Startstation</param>
        /// <param name="p_ende">Endstation</param>
        /// <returns></returns>
        public List<Station> CheapestPath(Station p_start, Station p_ende)
        {
            EStationZone start = p_start.StationZone;
            EStationZone end = p_ende.StationZone;
            if ((start == EStationZone.ZONE_A && end == EStationZone.ZONE_C) || (start == EStationZone.ZONE_C && end == EStationZone.ZONE_A))
            {
                return ShortestPath(p_start, p_ende, m_verbindungen, m_stationen);
            }
            if (start == end)
            {
                List<List<Station>> verbindungenNeu = new List<List<Station>>();
                List<Station> stationenNeu = new List<Station>();
                int count = 0;
                for (int i = 0; i < m_stationen.Count(); i++)
                {
                    if (m_stationen[i].StationZone == start)
                    {
                        Station neueStation = new Station();
                        neueStation.StationIndex = count;
                        count++;
                        neueStation.StationName = m_stationen[i].StationName;
                        neueStation.StationZone = start;
                        stationenNeu.Add(neueStation);
                        if (m_stationen[i].StationName == p_start.StationName)
                        {
                            p_start = neueStation;
                        }
                        if (m_stationen[i].StationName == p_ende.StationName)
                        {
                            p_ende = neueStation;
                        }
                        List<Station> edgesAlt = new List<Station>();
                        for (int j = 0; j < m_verbindungen[i].Count; j++)
                        {
                            Station neueStation2 = new Station();
                            neueStation2.StationIndex = m_verbindungen[i][j].StationIndex;
                            neueStation2.StationName = m_verbindungen[i][j].StationName;
                            neueStation2.StationZone = m_verbindungen[i][j].StationZone;
                            edgesAlt.Add(neueStation2);
                        }
                        List<Station> toRemove = new List<Station>();
                        for (int j = 0; j < edgesAlt.Count(); j++)
                        {
                            if (edgesAlt[j].StationZone != start)
                            {
                                toRemove.Add(edgesAlt[j]);
                            }
                        }
                        for (int j = 0; j < toRemove.Count(); j++)
                        {
                            edgesAlt.Remove(toRemove[j]);
                        }


                        verbindungenNeu.Add(edgesAlt);
                    }
                }
                for (int k = 0; k < verbindungenNeu.Count; k++)
                {
                    List<Station> edgesNeu = verbindungenNeu[k];
                    for (int l = 0; l < edgesNeu.Count; l++)
                    {
                        string name = edgesNeu[l].StationName;
                        for (int i = 0; i < stationenNeu.Count; i++)
                        {
                            if (stationenNeu[i].StationName == name)
                            {
                                edgesNeu[l].StationIndex = stationenNeu[i].StationIndex;
                            }
                        }
                    }
                    verbindungenNeu[k] = edgesNeu;
                }
                return ShortestPath(p_start, p_ende, verbindungenNeu, stationenNeu);
            }
            else
            {
                List<List<Station>> verbindungenNeu = new List<List<Station>>();
                List<Station> stationenNeu = new List<Station>();
                int count = 0;
                for (int i = 0; i < m_stationen.Count(); i++)
                {
                    if (m_stationen[i].StationZone == start || m_stationen[i].StationZone == end)
                    {
                        Station neueStation = new Station();
                        neueStation.StationIndex = count;
                        count++;
                        neueStation.StationName = m_stationen[i].StationName;
                        neueStation.StationZone = start;
                        stationenNeu.Add(neueStation);
                        if (m_stationen[i].StationName == p_start.StationName)
                        {
                            p_start = neueStation;
                        }
                        if (m_stationen[i].StationName == p_ende.StationName)
                        {
                            p_ende = neueStation;
                        }
                        List<Station> edgesAlt = new List<Station>();
                        for (int j = 0; j < m_verbindungen[i].Count; j++)
                        {
                            Station neueStation2 = new Station();
                            neueStation2.StationIndex = m_verbindungen[i][j].StationIndex;
                            neueStation2.StationName = m_verbindungen[i][j].StationName;
                            neueStation2.StationZone = m_verbindungen[i][j].StationZone;
                            edgesAlt.Add(neueStation2);
                        }
                        List<Station> toRemove = new List<Station>();
                        for (int j = 0; j < edgesAlt.Count(); j++)
                        {
                            if (edgesAlt[j].StationZone != start && edgesAlt[j].StationZone != end)
                            {
                                toRemove.Add(edgesAlt[j]);
                            }
                        }
                        for (int j = 0; j < toRemove.Count(); j++)
                        {
                            edgesAlt.Remove(toRemove[j]);
                        }


                        verbindungenNeu.Add(edgesAlt);
                    }
                }
                for (int k = 0; k < verbindungenNeu.Count; k++)
                {
                    List<Station> edgesNeu = verbindungenNeu[k];
                    for (int l = 0; l < edgesNeu.Count; l++)
                    {
                        string name = edgesNeu[l].StationName;
                        for (int i = 0; i < stationenNeu.Count; i++)
                        {
                            if (stationenNeu[i].StationName == name)
                            {
                                edgesNeu[l].StationIndex = stationenNeu[i].StationIndex;
                            }
                        }
                    }
                    verbindungenNeu[k] = edgesNeu;
                }
                return ShortestPath(p_start, p_ende, verbindungenNeu, stationenNeu);
            }
        }
        private static bool BFS(List<List<Station>> adj,
                        Station p_start, Station p_ende,
                        int v, int[] pred,
                        int[] dist)
        {

            List<Station> queue = new List<Station>();

            bool[] visited = new bool[v];

            for (int i = 0; i < v; i++)
            {
                visited[i] = false;
                dist[i] = int.MaxValue;
                pred[i] = -1;
            }

            visited[p_start.StationIndex] = true;
            dist[p_start.StationIndex] = 0;
            queue.Add(p_start);
            int a = adj[p_start.StationIndex].Count;

            // bfs Algorithm
            while (queue.Count != 0)
            {
                Station u = queue[0];
                queue.RemoveAt(0);

                for (int i = 0; i < adj[u.StationIndex].Count; i++)
                {
                    if (visited[adj[u.StationIndex][i].StationIndex] == false)
                    {
                        visited[adj[u.StationIndex][i].StationIndex] = true;
                        dist[adj[u.StationIndex][i].StationIndex] = dist[u.StationIndex] + 1;
                        pred[adj[u.StationIndex][i].StationIndex] = u.StationIndex;
                        queue.Add(adj[u.StationIndex][i]);

                        if (adj[u.StationIndex][i] == p_ende)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
