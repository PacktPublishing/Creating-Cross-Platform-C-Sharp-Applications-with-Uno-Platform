using System.Collections.Generic;
using UnoBookRail.Common.Issues;

namespace ResourcePlanner.ViewModels
{
    public class IssueListViewModel
    {

        public readonly IList<Issue> Issues;

        public IssueListViewModel(IList<Issue> issues)
        {
            this.Issues = issues;
        }
    }
}
