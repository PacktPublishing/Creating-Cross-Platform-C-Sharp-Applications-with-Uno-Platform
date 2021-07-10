using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using UnoBookRail.Common.Issues;

namespace ResourcePlanner.ViewModels
{
    public class ExportIssueViewModel
    {

        public readonly Issue Issue;

        public ICommand SavePDFClickedCommand;

        public ExportIssueViewModel(Issue issue)
        {
            Issue = issue;

            SavePDFClickedCommand = new RelayCommand(async () =>
            {
#if !__WASM__
                var bytes = GeneratePDF();
                var tempFileName = $"{Path.GetFileNameWithoutExtension(Path.GetTempFileName())}.pdf";
                var folder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                await folder.CreateFileAsync(tempFileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var file = await folder.GetFileAsync(tempFileName);
                await Windows.Storage.FileIO.WriteBufferAsync(file, bytes.AsBuffer());
                await Windows.System.Launcher.LaunchFileAsync(file);
#endif
            });
        }

        public byte[] GeneratePDF()
        {
            byte[] bytes;

            using (var memoryStream = new MemoryStream())
            {
                var pdfWriter = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(pdfWriter);
                var document = new Document(pdfDocument);
                var header = new Paragraph("Issue export: " + Issue.Title)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20);
                document.Add(header);

                var issueType = new Paragraph("Type: " + Issue.IssueType);
                document.Add(issueType);

                switch (Issue.IssueType)
                {
                    case IssueType.Train:
                        var trainNumber = new Paragraph("Train number: " + Issue.TrainNumber);
                        document.Add(trainNumber);
                        break;
                    case IssueType.Station:
                        var stationName = new Paragraph("Station name: " + Issue.StationName);
                        document.Add(stationName);
                        break;
                    case IssueType.Other:
                        var location = new Paragraph("Location: " + Issue.Location);
                        document.Add(issueType);
                        break;
                }

                var description = new Paragraph("Description: " + Issue.Description);
                document.Add(description);

                document.Close();
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }

    }
}
