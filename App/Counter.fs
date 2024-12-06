module Counter

open System
open Sutil
open Sutil.CoreElements
open Sutil.Bulma

let view () =
    let count = Store.make DateTime.Now.Year
    let add by = count |> Store.modify ((+) by)
    let increase _ = add 1
    let decrease _ = add -1

    fragment [
        disposeOnUnmount [ count ]

        bulma.title.h1  [
            Bind.el (count, string >> Html.text)
        ]

        bulma.buttons [
            let btn text color onClick =
                bulma.button.button [
                    Html.text (text:string)
                    color
                    Ev.onClick onClick
                ]
            btn "Increase" color.isPrimary increase
            btn "Decrease" color.isDanger decrease
        ]
    ]
