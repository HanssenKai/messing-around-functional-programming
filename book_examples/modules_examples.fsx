module Person =
    type T = {First:string; Last:string} with
        // member defined with type declaration
        member this.FullName =
            this.First + " " + this.Last

    // constructor
    let create first last =
        {First=first; Last=last}

    // another member added later
    let fullname {First=first; Last=last} =
        first + " " + last

    type T with 
        member this.Fullname = fullname this

    // standalone function
    let hasSameFirstAndLastName (person:T) otherFirst otherLast =
        person.First = otherFirst && person.Last = otherLast

        
    // attach preexisting function as a member
    type T with
        member this.HasSameFirstAndLastName = hasSameFirstAndLastName this

let person = Person.create "john" "doe"

//can be used in OO way, or FP way
let fullnameOO = person.FullName
let fullnameFP = Person.fullname person 

let resultOO = person.HasSameFirstAndLastName "john" "doopy"
let resultFP = Person.hasSameFirstAndLastName person "john" "doe"


//Adding functionality to types
type System.Int32 with 
    static member IsOdd x = x % 2 = 1

let test_result = System.Int32.IsOdd 20


//Interfacing with object oriented .net library
type Product = {ID:string; Price: float} with
    // curried style
    member this.CurriedTotal qty discount =     
        (this.Price * float qty) - discount 
    // tuple style
    member this.TupleTotal(qty,discount) =
        (this.Price * float qty) - discount

    //Optional, defaulta args
    member this.TupleTotalOptional(qty, ?discount) = 
        let extPrice = this.Price * float qty
        let discount = defaultArg discount 0.0 //Optional discount
        extPrice - discount

    //overloading (OO)
    member this.TupleTotalOverload(qty) = 
        this.Price * float qty

    member this.TupleTotalOverload(qty, discount) =
        (this.Price * float qty) - discount


let product = {ID="2045"; Price=2.0}
let totalFP = product.CurriedTotal 10 1.0
let totalOO = product.TupleTotal(10, 4.0)

//FP cannot do
    // Named parameters 
let totalOO1 = product.TupleTotal(qty=10,discount=1.9)
let totalOO2 = product.TupleTotal(discount=1.0, qty=10)
    // Optional parameters
    // Overloading



