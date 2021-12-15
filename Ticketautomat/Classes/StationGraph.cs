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

        public List<List<Station>> GetRoutes(Station p_start, Station p_ende)
        {
            List<List<Station>> ausgabe = new List<List<Station>>();
            ausgabe.Add(graph.CheapestPath(p_start, p_ende));
            //ausgabe.Add(graph.ShortestPath(p_start, p_ende, graph.getVerbindungen()));
            return ausgabe;
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

        public void AddEdge(Station p_start, Station p_ende)
        {
            if (!m_verbindungen[p_start.StationIndex].Contains(p_ende))
            {
                m_verbindungen[p_start.StationIndex].Add(p_ende); // Add p_ende to p_start's list.
                m_verbindungen[p_ende.StationIndex].Add(p_start); // Add p_start to p_ende's list.
            }
        }

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

        public List<Station> ShortestPath(Station p_start, Station p_ende, List<List<Station>> adj, List<Station> stationen)
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
            for (int i = path.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(path[i].StationName + " ");
            }
            return path;
        }
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
