using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class StationGraph
    {
        private Graph graph;

        public Graph Graph { get => graph; set => graph = value; }

        public List<List<Station>> GetRoutes(Station p_start, Station p_ende)
        {
            List<List<Station>> ausgabe = new List<List<Station>>();
            ausgabe.Add(graph.CheapestPath(p_start, p_ende));
            ausgabe.Add(graph.ShortestPath(p_start, p_ende));
            return ausgabe;
        }
    }

    class Graph
    {
        private int m_anzahlStationen; // No. of vertices

        // Array of lists for
        // Adjacency List Representation
        private List<Station>[] m_verbindungen;

        // Constructor
        Graph(int p_anzahlStationen)
        {
            m_anzahlStationen = p_anzahlStationen;
            m_verbindungen = new List<Station>[p_anzahlStationen];
            for (int i = 0; i < p_anzahlStationen; ++i)
                m_verbindungen[i] = new List<Station>();
        }

        // Function to Add an edge into the graph
        void AddEdge(Station p_start, Station p_ende)
        {
            if (!m_verbindungen[p_start.StationIndex].Contains(p_ende))
            {
                m_verbindungen[p_start.StationIndex].Add(p_ende); // Add p_ende to p_start's list.
                m_verbindungen[p_ende.StationIndex].Add(p_start); // Add p_start to p_ende's list.
            }
        }

        public List<Station> ShortestPath(Station p_start, Station p_ende)
        {
            return null;
        }
        public List<Station> CheapestPath(Station p_start, Station p_ende)
        {
            return null;
        }
    }
}
