namespace day6

module Input =
    open System.IO
    open Xunit
    open System

    type Newborn = FirstCycle of int list
    type LanternFish = DaysLeft of int list
    type Fish = Baby of Newborn | Adult of LanternFish 

    let createLanternFishFromDays (days: int) : LanternFish =
        let counter = [0 .. days] // starts a bit off
        DaysLeft counter
 
    let createLanternFish () : LanternFish =
        let nrOfDay = 7
        let counter = [0 .. nrOfDay - 1]
        DaysLeft counter
    
    let createNewbornFish () : Newborn =
        let nrOfDay = 2
        let counter = [0 .. nrOfDay - 1]
        FirstCycle counter




    let readInit (filePath: string): int list = 
        use sr = new StreamReader (filePath) 
        let line = sr.ReadLine()
        let numbers = line.Split(",")
        numbers |> Array.map(fun f -> Int32.Parse(f)) |> Array.toList

    let getInitialFish (filePath: string): LanternFish list =
        let numbers = readInit filePath
        numbers |> List.map (fun f -> createLanternFishFromDays f)

    [<Fact>]
    let ReadlDataPart1() =
        let x  = readInit "input.txt"
        let fish  = getInitialFish "input.txt" 
        Assert.Equal(5, x |> List.length)
        Assert.Equal(5, fish |> List.length)
