<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/234332401/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T852866)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to prohibit addition of detached connectors to our WPF Diagram Control

This example demonstrates how to prevent creating detached connectors:

![](./demo.gif)


There are several scenarios when a user may create connector whose [BeginItem](https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramConnector.BeginItem.property)/[EndItem](https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramConnector.EndItem.property) properties are empty:

1. Drawing a new connector using a tool. To cover this scenario, handle the [AddingNewItem](https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl.AddingNewItem.event) event and set e.Cancel to true if the BeginItem or EndItem properties are null:
```cs
private void DiagramDesignerControl_AddingNewItem(object sender, DevExpress.Xpf.Diagram.DiagramAddingNewItemEventArgs e)
{
    DiagramConnector connector = e.Item as DiagramConnector;
    if (connector != null)
    {
        //...
        e.Cancel = connector.BeginItem == null || connector.EndItem == null;
    }
}
```
2. Detaching a connector from an item. To prevent detaching connectors, set the [BeginPointRestrictions](https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramConnector.BeginPointRestrictions.property) and [EndPointRestrictions](https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramConnector.EndPointRestrictions.property) properties to ConnectorPointRestrictions.KeepConnected.
3. Copying and pasting a connector. Handle the [ItemsPasting](https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl.ItemsPasting.event) event and set e.Cancel if pasted items contain detached connectors.
4. The use of the Move&Copy operation (pressing the Ctrl key and moving an item). Handle the [BeforeItemsMoving](https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl.BeforeItemsMoving.event) event and set e.Cancel similarly to the previous point.
