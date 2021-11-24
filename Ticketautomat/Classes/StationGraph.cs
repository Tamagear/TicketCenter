using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class StationGraph
    {
        private IEnumerable<Graph> graph;

        public IEnumerable<Graph> Graph { get => graph; set => graph = value; }
    }
}
