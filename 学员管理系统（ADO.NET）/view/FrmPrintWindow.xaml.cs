using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace 学员管理系统_ADO.NET_.view
{
    /// <summary>
    /// FrmPrintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FrmPrintWindow : Window
    {
        delegate void LoadXpsMethod();
        FlowDocument doc;
        DispatcherOperation op = null;
        public FrmPrintWindow(string strTmpname, object data)
        {
            InitializeComponent();
            doc = (FlowDocument)Application.LoadComponent(new Uri("/common/" + strTmpname, UriKind.RelativeOrAbsolute));
            doc.PagePadding = new Thickness(50);
            doc.DataContext = data;
            op = Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
            op.Completed += Op_Completed;
        }

        private void Op_Completed(object sender, EventArgs e)
        {
            if (op.Status == DispatcherOperationStatus.Completed)
            {
                doc.DataContext = null;
                op.Abort();
            }
        }
        void LoadXps()
        {
            MemoryStream stream = new MemoryStream();
            Package package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);
            Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(DocumentUri);
            PackageStore.AddPackage(DocumentUri, package);
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator);
            docViewer.Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();
        }
    }
}
