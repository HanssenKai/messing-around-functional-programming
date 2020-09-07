(*
    for loop: good for list/seq generators
        let mylist = [for x in 0..100 do if x*x < 100 then yield x ]
    if else: good for elegant oneliners for pass into func
        let posNeg x = if x > 0 then "+" elif x < 0 then "-" else "0"
        [-5..5] |> List.map posNeg
*)



//avoid if - else 
// bad
let f x =
    if x = 1
    then "a"
    else "b"
// not much better
let f1 x =
    match x=1 with
    | true -> "a"
    | false -> "b"
// best
let f2 x =
    match x with
    | 1 -> "a"
    | _ -> "b"

// bad
let f3 list =
    if List.isEmpty list
        then printfn "is empty"
        elif (List.head list) > 0
            then printfn "first element is > 0"
            else printfn "first element is <= 0"
// much better
let f4 list =
    match list with
    | [] -> printfn "is empty"
    | x::_ when x > 0 -> printfn "first element is > 0"
    | x::_ -> printfn "first element is <= 0"

let divide x y=
    try
        (x+1) / y
    with
        | :? System.DivideByZeroException as ex ->
        printfn "%s" ex.Message
        reraise()
//test
divide 1 1
divide 1 0


//Matching
type Choices = A | B | C | D 
let x = 
    match A with 
    | A | B | C -> "a, b or c" //and/or logic 
    | D -> "d"
    //| a % d -> 23   <- must be same types, as match returns 

//list example
let y = 
    match [1;2;3] with 
    | [1;x;y] -> printfn "x =%A y=%A" x y 
    | 1::tail -> printfn "tail = %A" tail 
    | [] -> printfn "Empty"
    | _ -> printfn "happy?"


let rec printAllAndSum aList sum = 
    match aList with 
    | [] -> sum
    | tail::next -> printfn "element=%A" tail
                    let sum = sum + tail
                    printAllAndSum next sum

let sumNum = printAllAndSum [1..20] 0 |> printfn "num is %d" 

//Tuples 
let aTuple = (0,0)
match aTuple with 
| (1,_) -> "first is 1"
| (_,2) -> "second is 2"
| (x,y) when x > y -> "first is greater than second"
| (x,y) when x < y -> "first is less than second"
| (x,y) when x = y -> "first and second are equal"
| (_,_) as someBindedValue -> "0,0 is covered, you silly compiler you"

//Grouping
let matchOnTwoTuples x y =
    match (x,y) with
        | (1,_),(1,_) -> "both start with 1"
        | (_,2),(_,2) -> "both end with 2"
        | _ -> "something else"
matchOnTwoTuples (1,3) (1,2)
matchOnTwoTuples (3,2) (1,2)





let doSomething printerFn x y =
    let result = x + y
    printerFn "result is" result
let callback = printfn "%s %i"
do doSomething callback 3 4