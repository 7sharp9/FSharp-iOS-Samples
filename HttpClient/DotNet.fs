// This file contains the sample code to use System.Net.WebRequest
// on the iPhone to communicate with HTTP and HTTPS servers
//
// Original Author: Miguel de Icaza
// Converted to F# by Dave Thomas

namespace HttpClient
open System
open System.IO
open System.Net
open MonoTouch.Foundation
open System.Security.Cryptography.X509Certificates

    module DotNet =
        // Asynchronous HTTP request
        let HttpSample(ad) =
                Application.Busy() 
                let request = WebRequest.Create(Application.WisdomUrl )
                
                //F# async version
                async {try let! response = request.AsyncGetResponse()
                           Application.Done()
                           ad(response.GetResponseStream())
                       with ex -> () } |> Async.Start
                       
        // Asynchornous HTTPS request
        let HttpSecureSample(ad) =
            Application.Busy() 
            let https = WebRequest.Create("https://gmail.com") :?> HttpWebRequest

            // To not depend on the root certficates, we will
            // accept any certificates:

            async {try let! response = https.AsyncGetResponse()
                       Application.Done() 
                       ad(response.GetResponseStream())
                   with ex -> () } |> Async.Start
                       
//We dont actually need a type definition we can just use a module                       
//    type DotNet(ad: Stream -> unit) =
//
//        // Asynchronous HTTP request
//        member x.HttpSample() =
//            Application.Busy() 
//            let request = WebRequest.Create(Application.WisdomUrl )
//            
//            //F# async version
//            async {try let! response = request.AsyncGetResponse()
//                       Application.Done()
//                       ad(response.GetResponseStream())
//                   with ex -> () } |> Async.Start
//
//        
//        // Asynchornous HTTPS request
//        member x.HttpSecureSample () =
//            Application.Busy() 
//            let https = WebRequest.Create("https://gmail.com") :?> HttpWebRequest
//
//            // To not depend on the root certficates, we will
//            // accept any certificates:
//
//            async {try let! response = https.AsyncGetResponse()
//                       Application.Done() 
//                       ad(response.GetResponseStream())
//                   with ex -> () } |> Async.Start
       
        //WebProxy
        type AcceptingPolicy() =
            interface ICertificatePolicy with
                member x.CheckValidationResult(sp, cert, req, error) = true

