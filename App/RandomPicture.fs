module RandomPicture

open Sutil
open Sutil.Core
open Sutil.Bulma
open Sutil.CoreElements

let create () =
    let url = "https://picsum.photos/2000/1200"
    let dummy = Store.make 0
    let isLoading = Store.make true

    fragment [
        disposeOnUnmount [ dummy; isLoading ]
        bulma.block [
            Bind.el (dummy, fun _ -> Components.Image.create url isLoading)
        ]
        bulma.block [
            bulma.buttons [
                buttons.isCentered
                bulma.button.button [
                    Html.text "Next"
                    Ev.onClick (fun _ -> dummy |> Store.modify ((+)1))
                    Bind.booleanAttr ("disabled", isLoading)
                    color.isLink
                ]
            ]
        ]
    ]
