//basic function
let add5 x = x + 5

let eval5Add2 x = add5 x + 2

let times3 x = x * 3

// function input
let eval5Add2Func fn x = fn x + 2

let y = eval5Add2Func times3 8



// function output
let adderGenerator numberToAdd = (+) numberToAdd

let add1 = adderGenerator 1
let add2 = adderGenerator 2

let x = 2 + add1 2

let eval5AsInt (fn: int -> int) x = fn x + 5

let j = eval5AsInt add2 4
// j = 4 + 2 + 5

let onAStick x = x.ToString() + " on a stick!"

let p = onAStick 22


//test : create func to match signature:
// val testA = int -> int
let testA = (+) 1

// val testB = int -> int -> int
let testB a b = a + b + 2

// val testC = int -> (int -> int)
let testC x = (+) x

// val testD = (int -> int) -> int
let testD x = x 2 + 4

// val testE = int -> int -> int -> int
let testE x y z = x + y + z

// val testF = (int -> int) -> (int -> int)
let testF x = x 2 |> testB

// val testG = int -> (int -> int) -> int
let testG x f = f 2 + x

// val testH = (int -> int -> int) -> int
let testH f = f 2 3 + 4
