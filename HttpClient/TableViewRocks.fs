namespace HttpClient
open System
open MonoTouch.UIKit
open MonoTouch.Foundation
            
    type StringDataSource(tableView:UITableView, source:string []) =
        inherit UITableViewDataSource()
        let mutable source = source
        
        let kDefaultCell_ID = new NSString ("f")
        
        member x.Source
            with get () = source
            and set (value) = source <- value
                              tableView.ReloadData()

        override x.NumberOfSections(tableView) = 1
        
        override x.RowsInSection (tableView, section) = source.Length
        
        override x.GetCell (tableView, indexPath) =
            let cell = 
                match tableView.DequeueReusableCell(kDefaultCell_ID) with
                | null -> new UITableViewCell (UITableViewCellStyle.Default, 
                                               kDefaultCell_ID, 
                                               SelectionStyle = UITableViewCellSelectionStyle.Blue,
                                               IndentationWidth = 30.0f)
                | cell -> cell 
            cell.TextLabel.Text <- source.[indexPath.Row]
            cell
    
    [<AllowNullLiteralAttribute>]
    type StringDelegate() =
        inherit UITableViewDelegate()
        let mutable selected = 0
        
        override x.RowSelected (tableView, indexPath) =
            tableView.CellAt(NSIndexPath.FromRowSection(selected, 0)).Accessory <- UITableViewCellAccessory.None
            selected <- indexPath.Row
            tableView.CellAt(indexPath).Accessory <- UITableViewCellAccessory.Checkmark
            tableView.DeselectRow(indexPath, true)
        
        member x.Selected with get() = selected

    type TableViewSelector() =
        static member Configure(tableView:UITableView, choices:string[]) =
            tableView.DataSource <- new StringDataSource(tableView, choices)
            tableView.Delegate <- new StringDelegate ();
            tableView.SelectRow(NSIndexPath.FromRowSection(0, 0), true, UITableViewScrollPosition.None)
            
    module UITableView =
        type UITableView with    
            member x.SelectedRow() =
                match x.Delegate with
                | :? StringDelegate as sd -> sd.Selected
                | _ ->  0

