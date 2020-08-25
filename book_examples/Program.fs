// Learn more about F# at http://fsharp.org


open System
open TypeExamples 
open somefunc


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let y = add1 20
    printfn "num is %d" y
    0 // return an integer exit code

