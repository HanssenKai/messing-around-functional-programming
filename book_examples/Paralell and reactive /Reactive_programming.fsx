open System
open System.Threading

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


//Task:
    // Tick every 500ms
    // for each tick, print num ticks and curr time

// create the timer and the corresponding observable
let basicTimer2 , timerEventStream = createTimerAndObservable 500

// Process event stream
timerEventStream
|> Observable.scan (fun count _ -> count + 1) 0 //"list fold" the event stream
|> Observable.subscribe (fun count -> printfn "Timer ticked, count = %i TimeNow = %A" count DateTime.Now) 

//test
Async.RunSynchronously basicTimer2

