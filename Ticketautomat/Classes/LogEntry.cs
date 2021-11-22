using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    class LogEntry
    {
        private DateTime m_date;
        private string m_author;
        private string m_content; 

        LogEntry(DateTime p_date , string p_author , string p_content)
        {
            this.m_author = p_author;
            this.m_content = p_content;
            this.m_date = p_date;
        }
    }
}
