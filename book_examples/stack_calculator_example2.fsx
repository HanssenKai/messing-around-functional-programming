type Stack = StackContents of float list 


let push x (StackContents contents) =
    StackContents (x::contents)

/// Pop a value from the stack and return it
/// and the new stack as a tuple
let pop (StackContents contents) =
    match contents with
    | top::rest ->
        let newStack = StackContents rest
        (top,newStack)
    | [] ->
        failwith "Stack underflow"

let SHOW stack =
    let x,_ = pop stack
    printfn "The answer is %f" x
    stack
    // keep going with same stack

let EMPTY = StackContents []

let ONE = push 1.0 
let TWO = push 2.0 
let THREE = push 3.0 
let FOUR = push 4.0 
let FIVE = push 5.0 
let SIX = push 6.0 

let binary mathFn stack = 
    let y, stack' = pop stack 
    let x, stack'' = pop stack' 
    let result = mathFn x y 
    push result stack

let ADD = binary (+) 
let SUB = binary (-) 
let MUL = binary (*) 
let DIV = binary (/)


let unary f stack =
    let x,stack' = pop stack //pop the top of the stack
    push (f x) stack' //push the function value on the stack

let NEG = unary (fun x -> -x)
let SQUARE = unary (fun x -> x * x)



let stacko = EMPTY |> ONE
let stacko2 = stacko |> THREE
let stacko3 = stacko2 |> ADD
let x, stacko4 = stacko3 |> pop
let y, stacko5 = stacko4 |> pop
let z, stacko6 = stacko5 |> pop


