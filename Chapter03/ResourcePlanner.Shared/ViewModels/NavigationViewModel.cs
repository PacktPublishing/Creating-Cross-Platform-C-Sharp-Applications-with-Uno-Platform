using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ResourcePlanner.Models;
using ResourcePlanner.Views;
using System.Linq;
using System.Windows.Input;
using UnoBookRail.Common.Issues;
using Windows.UI.Xaml;

namespace ResourcePlanner.ViewModels
{
    public class NavigationViewModel : ObservableObject
    {

        private FrameworkElement content;
        public FrameworkElement Content
        {
            get
            {
                return content;
            }
            set
            {
                SetProperty(ref content, value);
            }
        }

        private Issue selectedIssue;
        private bool hasSelectedIssue;
        public bool HasSelectedIssue
        {
            get
            {
                return hasSelectedIssue;
            }
            set
            {
                SetProperty(ref hasSelectedIssue, value);
            }
        }

        public ICommand Issues_OpenNewIssueViewCommand { get; private set; }
        public ICommand Issues_ExportIssueViewCommand { get; private set; }

        public ICommand Issues_OpenAllIssuesCommand { get; private set; }
        public ICommand Issues_OpenTrainIssuesCommand { get; private set; }
        public ICommand Issues_OpenStationIssuesCommand { get; private set; }
        public ICommand Issues_OpenOtherIssuesCommand { get; private set; }

        public NavigationViewModel()
        {
            Issues_OpenNewIssueViewCommand = new RelayCommand(() =>
            {
                Content = new CreateIssueView(new CreateIssueViewModel(this));
            });
            Issues_ExportIssueViewCommand = new RelayCommand(() =>
            {
                Content = new ExportIssueView(new ExportIssueViewModel(this.selectedIssue));
            });
            Issues_OpenAllIssuesCommand = new RelayCommand(() =>
            {
                Content = new IssueListView(new IssueListViewModel(IssuesModel.GetAllIssues()), this);
            });
            Issues_OpenTrainIssuesCommand = new RelayCommand(() =>
            {
                Content = new IssueListView(new IssueListViewModel(IssuesModel.GetAllIssues().Where(issue => issue.IssueType == IssueType.Train).ToList()), this);
            });
            Issues_OpenTrainIssuesCommand = new RelayCommand(() =>
            {
                Content = new IssueListView(new IssueListViewModel(IssuesModel.GetAllIssues().Where(issue => issue.IssueType == IssueType.Station).ToList()), this);
            });
            Issues_OpenTrainIssuesCommand = new RelayCommand(() =>
            {
                Content = new IssueListView(new IssueListViewModel(IssuesModel.GetAllIssues().Where(issue => issue.IssueType == IssueType.Other).ToList()), this);
            });
        }
        internal void SetSelectedIssue(Issue issue)
        {
            selectedIssue = issue;
            HasSelectedIssue = issue != null;
        }
    }
}
