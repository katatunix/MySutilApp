module Utils

open Browser
open Fable.SimpleHttp

let exnMsg (ex: exn) = ex.Message

let load (url: string) =
    async {
        let! res =
            Http.request url
            |> Http.overrideResponseType ResponseTypes.Blob
            |> Http.send

        let blob =
            match res.content with
            | ResponseContent.Blob blob -> blob
            | _ -> failwith "Expected binary response"

        let x = URL.createObjectURL(blob)
        #if DEBUG
        printfn "createObjectURL: %s" x
        #endif
        return x
    }

let revoke (url: string) =
    #if DEBUG
    printfn "revokeObjectURL: %s" url
    #endif
    URL.revokeObjectURL url
