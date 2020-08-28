let printInt (x: int) = printfn "printInt = %d" x

//Nested
let addThreeNumbers x y z =
    let add n = fun x -> x + n

    x |> add y |> add z
// add is helper func only within addThreeNumber scope

//Nested commonly used in recursion:
let sumNumbersUpTo max =

    let rec recursiveSum n sumSoFar =
        match n with
        | 0 -> sumSoFar
        | _ -> recursiveSum (n - 1) (n + sumSoFar)

    recursiveSum max 0

sumNumbersUpTo 20 |> printInt



//Modules
module StringFunctions =
    let concatenate (a: string) (x: string) = a + x

    let firstHalf (a: string) =
        let slice (str: string) stop = str.[0..stop]

        let stop = String.length a / 2 - 1
        slice a stop

    let lastHalf (a: string) =
        let slice (str: string) start = str.[start..]
        let start = String.length a / 2
        slice a start

    //nested module
    module ChildModule =
        let childAdd x y = x + y



//acessing other module:
module TestingAccess =

    let testString = "JhonnyMcLauren"

    StringFunctions.firstHalf testString
    |> printfn "first half = %s, second half = %s"
    <| StringFunctions.lastHalf testString

    StringFunctions.ChildModule.childAdd 2 4
    |> printInt
