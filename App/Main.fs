module Main

open Browser
open Sutil
open Sutil.CoreElements
open Sutil.Bulma
open type CssBulma
open type CssFa
open Sutil.Router

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
    | Home -> "#/"
    | Counter -> "#/counter"
    | PeicResult -> "#/peic-result"

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
                Ev.onClick (fun _ ->
                    page <~ thePage
                    window.location.assign (formatUrl thePage)
                )
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

    let page =
        Router.getCurrentUrl window.location
        |> parseUrl
        |> Store.make

    let routerSub =
        Navigable.listenLocation (fun location ->
            Router.getCurrentUrl location
            |> parseUrl
            |> Store.set page
        )

    bulma.section [
        disposeOnUnmount [ page ]
        unsubscribeOnUnmount [ routerSub ]
        bulma.container [
            bulma.box (main page)
        ]
    ]

view () |> Program.mount
