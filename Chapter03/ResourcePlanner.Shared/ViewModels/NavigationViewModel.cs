using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace ResourcePlanner.ViewModels
{
    class NavigationViewModel : ObservableObject
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

        public ICommand Issues_OpenNewIssueViewCommand { get; private set; }
        public ICommand Issues_OpenEditIssueCommand { get; private set; }

        public ICommand Issues_OpenAllIssuesCommand { get; private set; }
        public ICommand Issues_OpenAllOpenIssuesCommand { get; private set; }
        public ICommand Issues_OpenAllClosedIssuesCommand { get; private set; }

        public NavigationViewModel()
        {
            Issues_OpenAllClosedIssuesCommand = new RelayCommand(() =>
            {

            });
            Issues_OpenEditIssueCommand = new RelayCommand(() =>
            {

            });
            Issues_OpenAllIssuesCommand = new RelayCommand(() =>
            {

            });
            Issues_OpenAllOpenIssuesCommand = new RelayCommand(() =>
            {

            });
            Issues_OpenAllClosedIssuesCommand = new RelayCommand(() =>
            {

            });
        }

    }
}
