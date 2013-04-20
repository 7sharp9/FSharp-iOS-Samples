// This sample shows how to use the two Http stacks in MonoTouch:
// The System.Net.WebRequest.
// The MonoTouch.Foundation.NSMutableUrlRequest

namespace HttpClient
open System
open System.IO
open System.Linq
open System.Drawing
open MonoTouch.Foundation
open MonoTouch.UIKit

    type Application() =
    
        // URL where we fetch the wisdom from
        static member WisdomUrl = "http://api.twitter.com/1/statuses/user_timeline.rss?screen_name=7sharp9"
        static member Main(args: string[] ) = UIApplication.Main (args)
        static member Busy() = UIApplication.SharedApplication.NetworkActivityIndicatorVisible <- true
        static member Done() = UIApplication.SharedApplication.NetworkActivityIndicatorVisible <- false   
