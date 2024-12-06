[<AutoOpen>]
module Common

open Sutil

let mkIcon (name: string seq) = Html.i [ Attr.className name ]
