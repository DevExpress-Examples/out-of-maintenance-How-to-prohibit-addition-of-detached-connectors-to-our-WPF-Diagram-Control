Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace AttachedConnectors
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub DiagramDesignerControl_AddingNewItem(ByVal sender As Object, ByVal e As DevExpress.Xpf.Diagram.DiagramAddingNewItemEventArgs)
			Dim connector As DiagramConnector = TryCast(e.Item, DiagramConnector)
			If connector IsNot Nothing Then
				connector.BeginPointRestrictions = ConnectorPointRestrictions.KeepConnected
				connector.EndPointRestrictions = ConnectorPointRestrictions.KeepConnected
				e.Cancel = connector.BeginItem Is Nothing OrElse connector.EndItem Is Nothing
			End If
		End Sub
		Private Sub DiagramDesignerControl_BeforeItemsMoving(ByVal sender As Object, ByVal e As DiagramBeforeItemsMovingEventArgs)
			e.Cancel = e.IsCopying AndAlso ListContainsDetachedConnectors(e.Items)
		End Sub
		Private Sub DiagramDesignerControl_ItemsPasting(ByVal sender As Object, ByVal e As DiagramItemsPastingEventArgs)
			e.Cancel = ListContainsDetachedConnectors(e.Items.Select(Function(i) i.Item).ToList())
		End Sub
		Private Function ListContainsDetachedConnectors(ByVal items As IList(Of DiagramItem)) As Boolean
			Return items.OfType(Of DiagramConnector)().Any(Function(c) Not items.Contains(c.BeginItem) OrElse Not items.Contains(c.EndItem))
		End Function
	End Class
End Namespace
