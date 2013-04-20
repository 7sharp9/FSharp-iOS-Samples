namespace HttpClient

open System
open System.IO
open System.Drawing
open System.Xml
open System.Xml.Linq
open System.Xml.XPath

open MonoTouch.UIKit
open MonoTouch.Foundation
open UITableView

    [<MonoTouch.Foundation.Register("AppDelegate")>]
    type AppDelegate() =
        inherit UIApplicationDelegate()

        let mutable __mt_window = Unchecked.defaultof<_>
        let mutable __mt_button1 = Unchecked.defaultof<_>
        let mutable __mt_stack = Unchecked.defaultof<_>
        let mutable __mt_navigationController = Unchecked.defaultof<_>
        
        [<MonoTouch.Foundation.Connect("window")>]
        member x.window 
            with get() = __mt_window <- x.GetNativeField("window") :?> UIWindow
                         __mt_window
            and set(value) =  __mt_window <- value
                              x.SetNativeField("window", value)
        
        [<MonoTouch.Foundation.Connect("button1")>]
        member x.button1
            with get() = __mt_button1 <- x.GetNativeField("button1") :?> UIButton
                         __mt_button1
            and set(value) = __mt_button1 <- value
                             x.SetNativeField("button1", value)
        
        [<MonoTouch.Foundation.Connect("stack")>]
        member x.stack
            with get() = __mt_stack <- x.GetNativeField("stack") :?> UITableView
                         __mt_stack;
            and set(value) = __mt_stack <- value
                             x.SetNativeField("stack", value)
        
        [<MonoTouch.Foundation.Connect("navigationController")>]
        member x.navigationController
            with get() = __mt_navigationController <- x.GetNativeField("navigationController") :?> UINavigationController
                         __mt_navigationController;
            and set(value) = __mt_navigationController <- value
                             x.SetNativeField("navigationController", value)

        //Normal partial entries are here:

        // This method is invoked when the application has loaded its UI and its ready to run
        override x.FinishedLaunching (app:UIApplication, options:NSDictionary) =
            x.window.AddSubview (x.navigationController.View)
            x.button1.TouchDown.Add 
                (fun _ ->  if not UIApplication.SharedApplication.NetworkActivityIndicatorVisible then                    
                               match x.stack.SelectedRow() with                    
                               | 0 -> DotNet.HttpSample x.RenderRssStream                    
                               | 1 -> DotNet.HttpSecureSample x.RenderStream                    
                               | _ -> (new Cocoa(x.RenderRssStream)).HttpSample() |> ignore )         
            TableViewSelector.Configure (x.stack, [|"http  - WebRequest"
                                                    "https - WebRequest"
                                                    "http  - NSUrlConnection" |] )                    
            x.window.MakeKeyAndVisible()
            true

        member x.RenderRssStream (stream: Stream) =
            let doc = XDocument.Load(new XmlTextReader(stream))
            let items = doc.XPathSelectElements("./rss/channel/item/title")

            // Since this is invoked on a separated thread, make sure that
            // we call UIKit only from the main thread.
            x.InvokeOnMainThread (fun () -> 
                let table = new UITableViewController()
                x.navigationController.PushViewController (table, true)
                
                // Put the data on a string [] so we can use our existing 
                // UITableView renderer for strings.
                
                let entries = items |> Seq.map (fun i -> i.Value) |> Seq.toArray
                TableViewSelector.Configure(table.View :?> _, entries)
            )
        
        member x.RenderStream(stream:Stream) =
            use reader = new StreamReader (stream)

            x.InvokeOnMainThread ( fun () ->
                let view = new UIViewController ()
                let label = new UILabel (RectangleF(20.f, 20.f, 300.f, 80.f), Text = "The HTML returned by Google:")
                let tv = new UITextView (RectangleF(20.f, 100.f, 300.f, 400.f), Text = reader.ReadToEnd())
                view.Add(label)
                view.Add(tv)
                    
                x.navigationController.PushViewController (view, true) )
        
        // This method is required in iPhoneOS 3.0
        override x.OnActivated(application:UIApplication) = ()
    
    module Main =
        [<EntryPoint>]
        let main args =
            UIApplication.Main (args)
            0

