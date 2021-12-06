namespace day6

module Input =
    open System.IO
    open Xunit
    open System

    //let firstCycle = 2
    let firstCycle = 1
    
    type Newborn = FirstCycle of int

    //let nrOfDay = 7
    let nrOfDay = 6
    let counter = [0 .. nrOfDay]
    type LanternFish = DaysLeft of int

    let readInit (filePath: string): int list = 
        use sr = new StreamReader (filePath) 
        let line = sr.ReadLine()
        let numbers = line.Split(",")
        numbers |> Array.map(fun f -> Int32.Parse(f)) |> Array.toList

    [<Fact>]
    let ReadlDataPart1() =
        let x  = readInit "input.txt"
        Assert.Equal(5, x |> List.length)
