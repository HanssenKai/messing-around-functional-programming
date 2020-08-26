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

            