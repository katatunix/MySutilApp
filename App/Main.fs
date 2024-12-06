module Main

open Sutil
open Sutil.CoreElements
open Sutil.Router2
open Sutil.Bulma
open type CssBulma
open type CssFa

type Page =
    | Home
    | Counter
    | PeicResult
    | RandomPicture

let parseUrl = function
    | [] -> Home
    | [ "counter" ] -> Counter
    | [ "peic-result" ] -> PeicResult
    | [ "random-picture" ] -> RandomPicture
    | _ -> Home

let formatUrl = function
    | Home -> Router.format()
    | Counter -> Router.format "counter"
    | PeicResult -> Router.format "peic-result"
    | RandomPicture -> Router.format "random-picture"

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
                mkItem RandomPicture [ Fas; FaImage ]
            ]
        ]

        Bind.el (page, function
            | Home -> Home.view ()
            | Counter -> Counter.view ()
            | PeicResult -> PeicResult.view ()
            | RandomPicture -> RandomPicture.create ()
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
