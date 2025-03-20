open System
open System.IO

let seedWeight (x: int, y: int) =
    let baseWeight: double = (1.0 / (abs (x - y) + 2 |> double))
    if x > y then baseWeight - 0.5 else (1.0 - baseWeight) - 0.5

let rnd = new Random()

let weightedRandomBool weight =
    rnd.Next(0, 100) |> double <= (1.0 + weight) * 50.0

let predictGame (team1: int * string, team2: int * string) =
    let (t1seed, t1name) = team1
    let (t2seed, t2name) = team2

    let result =
        if t1name.ToLower().Contains("duke") then
            weightedRandomBool (seedWeight (t1seed * 2, t2seed))
        else if t2name.ToLower().Contains("duke") then
            weightedRandomBool (seedWeight (t1seed, t2seed * 2))
        else
            weightedRandomBool (seedWeight (t1seed, t2seed))

    if result then team1 else team2

let predictRound (teams: (int * string) array) =
    (*for i in [ 0..2 .. teams.Length - 1 ] do
        let (t1seed, t1name) = teams[i]
        let (t2seed, t2name) = teams[i + 1]
        let result = predictGame (teams[i], teams[i + 1])*)
    //let (resultSeed, resultName) = result
    //printfn "%s vs %s: %s" t1name t2name resultName
    seq {
        for i in 0..2 .. teams.Length - 1 ->
            predictGame (teams[i], teams[i + 1])
    } |> Seq.toArray

let printTeams (teams: (int * string) array) =
    for (seed, name) in teams do
        printfn "%d: %s" seed name
    printfn ""

[<TailCall>]
let rec teamsLoop (teams: (int * string) array) =
    if teams.Length = 1 then
        printTeams teams
    else
        printTeams teams
        teamsLoop (predictRound teams)

[<EntryPoint>]
let main args =
    if args.Length <> 1 then
        printfn "Please supply only the file containing your bracket."
        1
    else
        let teams =
            File.ReadAllLines(args[0])
            |> Array.filter (fun x -> not (x.StartsWith("//")))
            |> Array.map (fun x -> x.Split(':'))
            |> Array.map (Array.map (fun x -> x.Trim()))
            |> Array.map (fun x -> (x[0] |> int, x[1]))

        teamsLoop teams
        0
