using System;

namespace Ticketautomat.Classes
{
    public class LogEntry
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

        public DateTime Date
        {
            get { return this.m_date; }
            set { this.m_date = value; }
        }
        public string Author
        {
            get { return this.m_author; }
            set { this.m_author = value; }
        }
        public string Content
        {
            get { return this.m_content; }
            set { this.m_content = value; }
        }

    }
}
