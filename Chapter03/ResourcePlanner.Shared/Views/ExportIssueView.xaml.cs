using ResourcePlanner.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ResourcePlanner.Views
{
    public sealed partial class ExportIssueView : UserControl
    {

        private ExportIssueViewModel exportIssueVM;

        public ExportIssueView(ExportIssueViewModel viewModel)
        {
            this.exportIssueVM = viewModel;
            this.InitializeComponent();

            #if __WASM__
                this.WASMDownloadLink.MimeType = "application/pdf";
                var bytes = exportIssueVM.GeneratePDF();
                var b64 = Convert.ToBase64String(bytes);
                this.WASMDownloadLink.SetBase64Content(b64);
            #endif
        }
    }

}
