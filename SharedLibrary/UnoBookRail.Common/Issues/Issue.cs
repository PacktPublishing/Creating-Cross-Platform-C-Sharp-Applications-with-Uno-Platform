using System;
using System.Collections.Generic;
using System.Text;
using UnoBookRail.Common.Auth;

namespace UnoBookRail.Common.Issues
{
    public class Issue
    {
        public IssueType IssueType { get; set; }

        // Issue type 'Train'
        public string TrainNumber { get; set; }

        // Issue type 'Station'
        public string StationName { get; set; }

        // Issue type 'Other'
        public string Location { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public Urgency Urgency { get; set; }
        public bool IsOpen { get; set; }
        public DateTime OpenDate { get; set; }
        public User OpenedBy { get; set; }
        public DateTime CloseDate { get; set; }
        public string CloseDateReadable => IsOpen ? "-" : CloseDate.ToString();
        public User ClosedBy { get; set; }
    }
}
