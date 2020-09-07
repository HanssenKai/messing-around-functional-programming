open System
open System.Threading
(*
    Timers;
        1. 300 ms -> 3 -> Fizz
        2. 500 ms -> 5 -> Buzz 
        3. 1 & 2 -> FizzBuzz
*)

let createTimer timerInterval eventHandler =
    // setup a timer
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true
    // add an event handler
    timer.Elapsed.Add eventHandler
    // return an async task
    async {
        // start timer...
        timer.Start()
        // ...run for five seconds...
        do! Async.Sleep 5000
        // ... and stop
        timer.Stop()
    }

type FizzBuzzEvent = {label:int; time: DateTime} 

let areSimultaneous (earlierEvent, laterEvent) = 
    let {label=_;time=t1} = earlierEvent
    let {label=_;time=t2} = laterEvent 
    t2.Subtract(t1).Milliseconds < 2


type ImperativeFizzBuzz() = 
    let mutable previousEvent: FizzBuzzEvent option = None 

    let printEvent thisEvent = 
        let {label=id; time=t} = thisEvent 
        printf "[%i] %i.%03i" id t.Second t.Millisecond
        let simultaneous = previousEvent.IsSome && areSimultaneous (previousEvent.Value, thisEvent)

        if simultaneous then printfn "FizzBuzz" 
        elif id = 3 then printfn "Fizz" 
        elif id = 5 then printfn "Buzz"

    member this.handleEvent3 eventArgs = 
        let event = {label=3; time=DateTime.Now} 
        printEvent event 
        previousEvent <- Some event 

    member this.handleEvent5 eventArgs = 
        let event = {label=5; time=DateTime.Now}
        printEvent event 
        previousEvent <- Some event

let handler = new ImperativeFizzBuzz() 
let timer3 = createTimer 300 handler.handleEvent3 
let timer5 = createTimer 500 handler.handleEvent5


// [timer3;timer5]
// |> Async.Parallel
// |> Async.RunSynchronously

//Functional implementation:
let createTimerAndObservable timerInterval =
    // setup a timer
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true
    // events are automatically IObservable
    let observable = timer.Elapsed
    // return an async task
    let task = async {
        timer.Start()
        do! Async.Sleep 5000
        timer.Stop()
        }
    // return a async task and the observable
    (task,observable)

let timer3F, timerEventStream3 = createTimerAndObservable 300
let timer5F, timerEventStream5 = createTimerAndObservable 500

//Convert to FizzBuzz format
let eventStream3 = 
    timerEventStream3
    |> Observable.map (fun _ -> {label=3; time=DateTime.Now}) 

let eventStream5 = 
    timerEventStream5 
    |> Observable.map (fun _ -> {label=5; time=DateTime.Now})


//Combine
let combinedStream = 
    Observable.merge eventStream3 eventStream5 
    
// Merge events
let pairwiseStream = 
    combinedStream |> Observable.pairwise
 
//Split simultaneous events
let simultaneousStream, nonSimultaneousStream = 
    pairwiseStream |> Observable.partition areSimultaneous

//Sort the nonsimultaneous events
let fizzStream, buzzStream = 
    nonSimultaneousStream
    |>Observable.map (fun (ev1,_) -> ev1)
    |>Observable.partition (fun {label=id} -> id=3)
printfn "Functional approach : "
combinedStream
|> Observable.subscribe (fun {label=id; time=t} -> printf "[%i] %i.%03i " id t.Second t.Millisecond)

simultaneousStream
|> Observable.subscribe (fun _ -> printfn "FizzBuzz")

fizzStream
|> Observable.subscribe (fun _ -> printfn "Fizz") 

buzzStream 
|> Observable.subscribe (fun _ -> printfn "Buzz")

[timer3F; timer5F]
|> Async.Parallel 
|> Async.RunSynchronously