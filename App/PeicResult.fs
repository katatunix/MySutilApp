module PeicResult

open Sutil
open Sutil.Bulma

let view () =
    let hiddenInput name value =
        Html.input [
            Attr.typeHidden
            Attr.name name
            Attr.value (value:string)
        ]

    Html.form [
        Attr.action "http://dangkythi.ttngoaingutinhoc.hcm.edu.vn/tra-cuu-diem"
        Attr.method "POST"
        Attr.target "_blank"

        hiddenInput "name"          "BUI PHUONG CHI"
        hiddenInput "birthday"      "03-10-2014"
        hiddenInput "sbd"           "3429"
        hiddenInput "s_examdate"    "16-11-2024"
        hiddenInput "s_capdothi"    "QM"

        bulma.button.submit [
            prop.value "Get result 🚀"
            button.isLarge
            button.isFullwidth
        ]
    ]
