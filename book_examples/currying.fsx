let printThreeParameters x y z=
    printfn "x=%i y=%i z=%d" x y z

let retFunc1 = printThreeParameters 10

let retFunc2 = retFunc1 20

retFunc2 30

let printThreeParametersCompiled x = 
    let sub1 y = 
        let sub2 z = 
            printfn "x=%i y=%i z=%d" x y z
        sub2 //return tempFunc2 with x & y baked in
    sub1 //return tempFunc with x baked in

printThreeParametersCompiled 30 40 50


//Printfn example

//normal:
let printResult = printfn "x=%i y=%i" 200 300

printResult

//after compile:
let sub1 = printfn "x=%i y=%s" 200
let result = sub1 "pans"


//simple addition
let add1Param x = (+) x 
let sub = add1Param 20
printfn "sub = %A" sub
let resultAdd = sub 10
printfn "result = %i" resultAdd

(*

int->int->int // two int parameters and returns an int

string->bool->int // first param is a string, second is a bool,
                    // returns an int

int->string->bool->unit // three params (int,string,bool)
                            // returns nothing (unit)
(int->string)->int   // has only one parameter, a function
                       // value (from int to string)
                       // and returns a int

(int->string)->(int->bool) // takes a function (int to string)
                            // returns a function (int to bool)
*)


//bugs
let printHello() = printfn "hello"

let addXY x y =
    printfn "x=%i y=%i" x 
    x + y

let test x y z = x + y + z 
let num = test 10 20 30


//list piping
let res =
    [1..10]
    |> List.map (fun i -> i+1)
    |> List.filter (fun i -> i>5)

let compositeOp = List.map (fun i -> i+1)
                >> List.filter (fun i -> i>5)
let res2 = compositeOp [1..10]    


let add1 x = 1 + x
let add2 x = 2 + x

let F x y z = x (y z) 
let F1 x y z = y z |> x
let F2 x y z = x <| y z

let f (x:int) = float x * 3.0
let g (x:float) = x > 3.2

let h (x:int) = g(f(x))






