namespace SingleView
open System
open System.Collections.Generic
open MonoTouch.Foundation
open MonoTouch.UIKit

// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
[<Register ("AppDelegate")>]
type AppDelegate() =
    inherit UIApplicationDelegate()
    
    let mutable window = Unchecked.defaultof<_>
    let mutable viewController = Unchecked.defaultof<_>

    // This method is invoked when the application has loaded and is ready to run. In this 
    // method you should instantiate the window, load the UI into it and then make the window visible.
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    override x.FinishedLaunching (app,  options) =
        window <- new UIWindow(UIScreen.MainScreen.Bounds)
        viewController <- new SingleViewController()
        window.RootViewController <- viewController
        window.MakeKeyAndVisible()
        true