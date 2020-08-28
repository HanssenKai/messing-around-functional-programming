//compose
let (>>) f g x = g (f (x))

let printInt (x: int) = printfn "printInt = %d" x
let add5 x = x + 5
let times2 x = x * 2
let add5Times2 x = (>>) add5 times2 x
let add5Times2Simple = add5 >> times2

add5Times2 6 |> printInt
add5Times2Simple 6 |> printInt

//partial composition, multiple functions
let add n1 n2 = n1 + n2
let times n1 n2 = n1 * n2

let add10Times2 = add 10 >> times 2 // preps both func add and func times
add10Times2 20 |> printInt

//any values can be used, as long as I/O match
let runFuncTwice func = func >> func
//^ note: 'a -> 'a and 'a -> 'a

let add4 = (+) 4
let add4Twice = runFuncTwice add4
// let printStr = printf "%s"
// let printStringTwice = runFuncTwice
//  ^ printStr i/o does not match (str->unit) (str->unit)
add4Twice 8 |> printInt

let addThenMultiply = (+)

// let add1ThenMultiplyX = (+)  >> (*) NOT ALLOWED I/O
// (int -> int) -> int
let add1ThenMultiplyX =
    (+) 1
    >> (*) //ALLOWED

add1ThenMultiplyX 10 2 |> printInt

//Reverse composition can make code easier to read, more like english
let myList = []
myList |> List.isEmpty |> not // straight pipeline
myList |> (not << List.isEmpty) // using reverse composition


//Composition or pipeline

let doCalc x y z = x / y + z
doCalc 100 2 4 |> printInt

4 |> doCalc 100 2 |> printInt //same, 4 piped in last as "z"
(*
    Composition f1 >> f2, pipe result of f1 as parameter to f2
                No   3 >> f2     3 is not a function
                Yes  add1 >> add2
    piping      x |> f1, pipe x as argument to y
*)
