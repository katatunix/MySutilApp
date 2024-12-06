module Main

open Sutil
open Sutil.CoreElements
open Sutil.Bulma
open type CssBulma
open type CssFa
open Sutil.Router2

type Page =
    | Home
    | Counter
    | PeicResult

let parseUrl = function
    | [] -> Home
    | [ "counter" ] -> Counter
    | [ "peic-result" ] -> PeicResult
    | _ -> Home

let formatUrl = function
    | Home -> Router.format()
    | Counter -> Router.format "counter"
    | PeicResult -> Router.format "peic-result"

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
                Attr.href (formatUrl thePage)
            ]
        ]

    fragment [
        bulma.tabs [
            Html.ul [
                mkItem Home [ Fas; FaHome ]
                mkItem Counter [ Fas; FaCalculator ]
                mkItem PeicResult [ Fas; FaRocket ]
            ]
        ]

        Bind.el (page, function
            | Home -> Home.view ()
            | Counter -> Counter.view ()
            | PeicResult -> PeicResult.view ()
        )
    ]

let view () =
    let page = Router.currentUrl() |> parseUrl |> Store.make

    let routerSub = Router.listen RouteMode.Hash (parseUrl >> Store.set page)

    bulma.section [
        disposeOnUnmount [ page ]
        unsubscribeOnUnmount [ routerSub ]
        bulma.container [
            bulma.box (main page)
        ]
    ]

view () |> Program.mount
