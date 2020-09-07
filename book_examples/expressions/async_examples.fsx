let childTask() =
// chew up some CPU.
    for i in [1..3000] do
        for i in [1..3000] do
            do "Hello".Contains("H") |> ignore

let asyncChildTask = async {return childTask}

let asyncParentTask = 
    asyncChildTask
    |> List.replicate 20 
    |> Async.Parallel

let parentTask = 
    childTask
    |> List.replicate 20 
    |> List.reduce (>>)            

#time
asyncParentTask
|> Async.RunSynchronously
#time

#time 
parentTask()
#time




let slowConsoleWrite msg =
    msg |> String.iter (fun ch->
        System.Threading.Thread.Sleep(1)
        System.Console.Write ch
        )

let makeTask logger taskId = async {
    let name = sprintf "Task%i" taskId
    for i in [1..3] do
        let msg = sprintf "-%s:Loop%i-" name i
        logger msg
    }

type SerializedLogger() =
    // create the mailbox processor
    let agent = MailboxProcessor.Start(fun inbox ->
        // the message processing function
        let rec messageLoop () = async{
            // read a message
            let! msg = inbox.Receive()
            // write it to the log
            slowConsoleWrite msg
            // loop to top
            return! messageLoop ()
            }
        // start the loop
        messageLoop ()
        )
    // public interface
    member this.Log msg = agent.Post msg


type UnserializedLogger() =
    // interface
    member this.Log msg = slowConsoleWrite msg

let unserializedLogger = UnserializedLogger()
unserializedLogger.Log "hello"


let unserializedExample =
    let logger = new UnserializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore


let serialExample =
    let logger = new SerializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore
