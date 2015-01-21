// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System

// 試行回数
let max = 1000000

// ドアの数
let doorCount = 3

let rand = new Random()

let getDoor () = rand.Next doorCount

let getSelect () = rand.Next doorCount

let rec hostSelect door selection =
  let hostSelection = rand.Next doorCount
  if hostSelection = door || hostSelection = selection then
    hostSelect door selection
  else
    hostSelection

let logic1 () =
  let door = getDoor ()
  let selection = getSelect ()
  let hostSelection = hostSelect door
  selection = door

let logic2 () =
  let door = getDoor ()
  let selection = getSelect ()
  let hostSelection = hostSelect door selection
  let lastSelection = [0..doorCount - 1] |> Seq.filter (fun n -> n <> selection && n <> hostSelection) |> Seq.head
  lastSelection = door

[<EntryPoint>]
let main argv = 
    printfn "選び直さない場合"
    let hit = [for n in [1..max] -> logic1 ()]
              |> Seq.filter (fun n -> n)
              |> Seq.length
    printfn "HIT : %A" (double hit / double max)
    printfn "選び直す場合"
    let hit = [for n in [1..max] -> logic2 ()]
              |> Seq.filter (fun n -> n)
              |> Seq.length
    printfn "HIT : %A" (double hit / double max)
    
    printfn "[ENTER]"
    System.Console.ReadLine () |> ignore

    0 // return an integer exit code
