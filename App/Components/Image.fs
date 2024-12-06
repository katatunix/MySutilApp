module Components.Image

open Sutil
open Sutil.Bulma
open Sutil.CoreElements

type State =
    | Loading
    | Done of Result<string,string>

let private disposeResult = function
    | Ok blob -> Utils.revoke blob
    | Error _ -> ()

type Model =
    { State: State }
    static member dispose m =
        match m.State with
        | Done result -> disposeResult result
        | _ -> ()

    member this.IsLoading =
        match this.State with
        | Loading -> true
        | _ -> false

type Msg =
    | EndLoad of Result<string,string>

let init (url: string) =
    { State = Loading },
    Cmd.OfAsync.either
        Utils.load
        url
        (Ok >> EndLoad)
        (Utils.exnMsg >> Error >> EndLoad)

let update msg (model: Model) =
    if isNull (box model) then
        match msg with
        | EndLoad result -> disposeResult result
        model, Cmd.none
    else
        match msg, model.State with
        | EndLoad result, Loading ->
            { model with State = Done result }, Cmd.none
        | EndLoad result, _ ->
            disposeResult result
            model, Cmd.none

let create (url: string) (isLoading: IStore<bool>) =
    let model, _dispatch = url |> Store.makeElmish init update Model.dispose

    let sub = model |> Store.subscribe (fun m -> isLoading <~ m.IsLoading)

    fragment [
        disposeOnUnmount [ model; sub ]
        Bind.el (model .> _.State, function
            | Loading ->
                bulma.progress [ progress.isSmall ]
            | Done (Ok blob) ->
                bulma.image [
                    Html.img [
                        Attr.src blob
                        Ev.onLoad (fun _ -> Utils.revoke blob)
                    ]
                ]
            | Done (Error str) ->
                Html.p str // TODO
        )
    ]
