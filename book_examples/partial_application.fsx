// use of half-done functions

//printer bot:
let printer = printfn "will print parameter %i"
printer 2
printer 5

//list manipulation
let add1 = (+) 1
let add1ToEach = List.map add1

add1ToEach [ 1; 2; 3; 4; 5; 5 ]

let filterEvens = List.filter (fun i -> i % 2 = 0)

filterEvens [ 1; 2; 3; 4; 5; 6 ]
