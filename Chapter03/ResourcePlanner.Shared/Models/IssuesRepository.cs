﻿using System.Collections.Generic;
using UnoBookRail.Common.Issues;

namespace ResourcePlanner.Models
{
    public class IssuesRepository
    {
        private static List<Issue> issues = new List<Issue>();

        public static List<Issue> GetAllIssues()
        {
            return issues;
        }

        public static void AddIssue(Issue issue)
        {
            issues.Add(issue);
        }
    }
}