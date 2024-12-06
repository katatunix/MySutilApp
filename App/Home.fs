module Home

open Sutil.Bulma
open Sutil.CoreElements

let view () =
    fragment [
        bulma.title.h1 "Home"
        bulma.subtitle.h1 "Welcome to My Sutil App"
    ]
