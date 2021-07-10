using ResourcePlanner.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ResourcePlanner.Views
{
    public sealed partial class CreateIssueView : UserControl
    {
        CreateIssueViewModel createIssueVM;

        public CreateIssueView(CreateIssueViewModel viewModel)
        {
            this.createIssueVM = viewModel;
            this.InitializeComponent();
        }

        private void FormInput_TextChanged(object sender, TextChangedEventArgs args)
        {
            EvaluateFieldsValid(sender);
        }

        private bool EvaluateFieldsValid(object sender)
        {
            bool allValid = true;
            if(sender == TitleTextBox || sender == null)
            {
                if (TitleTextBox.Text.Length == 0)
                {
                    allValid = false;
                    titleErrorNotification.Text = "Title must not be empty.";
                }
                else
                {
                    titleErrorNotification.Text = "";
                }
            }

            if (sender == TrainNumberTextBox || sender == null)
            {
                if (TrainNumberTextBox.Text.Length == 0)
                {
                    if (createIssueVM.IssueType == UnoBookRail.Common.Issues.IssueType.Train)
                    {
                        allValid = false;
                    }
                    trainNumberErrorNotification.Text = "Train number must not be empty.";
                }
                else
                {
                    trainNumberErrorNotification.Text = "";
                }
            }

            if (sender == StationNameTextBox || sender == null)
            {
                if (StationNameTextBox.Text.Length == 0)
                {
                    if (createIssueVM.IssueType == UnoBookRail.Common.Issues.IssueType.Station)
                    {
                        allValid = false;
                    }
                    stationNameErrorNotification.Text = "Station name must not be empty.";
                }
                else
                {
                    stationNameErrorNotification.Text = "";
                }
            }

            if (sender == LocationTextBox || sender == null)
            {
                if (LocationTextBox.Text.Length == 0)
                {
                    if (createIssueVM.IssueType == UnoBookRail.Common.Issues.IssueType.Other)
                    {
                        allValid = false;
                    }
                    locationErrorNotification.Text = "Location must not be empty.";
                }
                else
                {
                    locationErrorNotification.Text = "";
                }
            }
            return allValid;
        }

        private void CreateIssueButton_Click(object sender, RoutedEventArgs args)
        {
            if (EvaluateFieldsValid(null))
            {
                createIssueVM.CreateIssueCommand.Execute(null);
            }
        }
    }
}
