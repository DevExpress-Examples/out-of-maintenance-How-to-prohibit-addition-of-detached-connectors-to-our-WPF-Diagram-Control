using DevExpress.Diagram.Core;
using DevExpress.Xpf.Diagram;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AttachedConnectors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DiagramDesignerControl_AddingNewItem(object sender, DevExpress.Xpf.Diagram.DiagramAddingNewItemEventArgs e)
        {
            DiagramConnector connector = e.Item as DiagramConnector;
            if (connector != null)
            {
                connector.BeginPointRestrictions = ConnectorPointRestrictions.KeepConnected;
                connector.EndPointRestrictions = ConnectorPointRestrictions.KeepConnected;
                e.Cancel = connector.BeginItem == null || connector.EndItem == null;
            }
        }
        private void DiagramDesignerControl_BeforeItemsMoving(object sender, DiagramBeforeItemsMovingEventArgs e)
        {
            e.Cancel = e.IsCopying && ListContainsDetachedConnectors(e.Items);
        }
        private void DiagramDesignerControl_ItemsPasting(object sender, DiagramItemsPastingEventArgs e)
        {
            e.Cancel = ListContainsDetachedConnectors(e.Items.Select(i => i.Item).ToList());
        }
        bool ListContainsDetachedConnectors(IList<DiagramItem> items)
        {
            return items.OfType<DiagramConnector>().Any(c => !items.Contains(c.BeginItem) || !items.Contains(c.EndItem));
        }
    }
}
