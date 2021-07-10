using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ResourcePlanner.Views
{

#if __WASM__
    [Uno.UI.Runtime.WebAssembly.HtmlElement("a")]
    public class WasmDownloadElement : ContentControl
    {
        public static readonly DependencyProperty MimeTypeProperty = DependencyProperty.Register(
            "MimeType", typeof(string), typeof(WasmDownloadElement), new PropertyMetadata("application/octet-stream", OnChanged));

        public string MimeType
        {
            get => (string)GetValue(MimeTypeProperty);
            set => SetValue(MimeTypeProperty, value);
        }

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
            "FileName", typeof(string), typeof(WasmDownloadElement), new PropertyMetadata("filename.bin", OnChanged));

        public string FileName
        {
            get => (string)GetValue(FileNameProperty);
            set => SetValue(FileNameProperty, value);
        }

        private string _base64Content;

        public void SetBase64Content(string content)
        {
            _base64Content = content;
            Update();
        }

        private static void OnChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyobject is WasmDownloadElement wd)
            {
                wd.Update();
            }
        }

        private void Update()
        {
            if (_base64Content?.Length == 0)
            {
                this.ClearHtmlAttribute("href");
            }
            else
            {
                var dataUrl = $"data:{MimeType};base64,{_base64Content}";
                this.SetHtmlAttribute("href", dataUrl);
                this.SetHtmlAttribute("download", FileName);
            }
        }
    }
#endif
}
