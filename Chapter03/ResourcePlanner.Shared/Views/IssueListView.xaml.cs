using Windows.UI.Xaml.Controls;
using ResourcePlanner.ViewModels;
using Microsoft.Toolkit.Uwp.UI.Controls;
using UnoBookRail.Common.Issues;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ResourcePlanner.Views
{
    public sealed partial class IssueListView : UserControl
    {
        private IssueListViewModel issueListVM;
        private NavigationViewModel navigationVM;
        public IssueListView(IssueListViewModel viewModel, NavigationViewModel navigationViewModel)
        {
            this.issueListVM = viewModel;
            this.navigationVM = navigationViewModel;
            this.InitializeComponent();
        }

        private void IssueList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            navigationVM.SetSelectedIssue((sender as DataGrid).SelectedItem as Issue);
        }
    }
}
