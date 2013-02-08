namespace SingleView

open System
open System.Drawing
open MonoTouch.Foundation
open MonoTouch.UIKit

[<Register ("SingleViewController")>]
type SingleViewController() =
    inherit UIViewController("SingleViewController", null)
        
    let ReleaseDesignerOutlets() = ( (* No outlets to release  *))

    override x.DidReceiveMemoryWarning() =
    // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning();
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad() =
        base.ViewDidLoad()
        // Perform any additional setup after loading the view, typically from a nib.

    override x.ViewDidUnload() =
        base.ViewDidUnload()
        // Clear any references to subviews of the main view in order to
        // allow the Garbage Collector to collect them sooner.
        // e.g. myOutlet.Dispose (); myOutlet = null;
        ReleaseDesignerOutlets()

    override x.ShouldAutorotateToInterfaceOrientation(toInterfaceOrientation) =
        // Return true for supported orientations
        toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown