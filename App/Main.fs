module Main

open Sutil
open Sutil.CoreElements
open Sutil.Bulma
open type CssBulma
open type CssFa

type Page =
    | Home
    | Counter
    | PeicResult

let main page =
    let mkItem (thePage: Page) (iconName: string seq)  =
        Html.li [
            Bind.toggleClass (page .> (=)thePage, IsActive)
            Html.a [
                Html.span [
                    Attr.className [ Icon; IsSmall ]
                    mkIcon iconName
                ]
                Html.span (string thePage)
                Ev.onClick (fun _ -> page <~ thePage)
            ]
        ]

    fragment [
        bulma.tabs [
            Html.ul [
                mkItem Home [ Fas; FaHome ]
                mkItem Counter [ Fas; FaCalculator ]
                mkItem PeicResult [ Fas; FaLanguage ]
            ]
        ]

        Bind.el (page, function
            | Home -> Home.view ()
            | Counter -> Counter.view ()
            | PeicResult -> PeicResult.view ()
        )
    ]

let view () =
    let page = Store.make Home

    bulma.section [
        disposeOnUnmount [ page ]
        bulma.container [
            bulma.box (main page)
        ]
    ]

view () |> Program.mount
