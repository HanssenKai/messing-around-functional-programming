namespace TypeExamples

module somefunc = 
    //basic function
    let add5 x = x + 5

    let eval5Add2  x = add5 x  + 2

    let times3 x = x * 3

    // function input
    let eval5Add2Func fn x = fn x + 2

    let y = eval5Add2Func times3 8



    // function output
    let adderGenerator numberToAdd = (+) numberToAdd

    let add1 = adderGenerator 1
    let add2 = adderGenerator 2 

    let x = 2 + add1 2

    let eval5AsInt (fn:int->int) x = fn x + 5

    let j = eval5AsInt add2 4 
    // j = 4 + 2 + 5

    let onAStick x = x.ToString() + " on a stick!"
    let p = onAStick 22 

    