namespace day7

module Input =
    open System
    open System.IO
    open Xunit 

    let readInit (filePath: string): int list = 
        use sr = new StreamReader (filePath) 
        let line = sr.ReadLine()
        let numbers = line.Split(",")
        numbers |> Array.map(fun f -> Int32.Parse(f)) |> Array.toList


    let calcFuel (input: int list): int =
        let min = input |> List.min
        let max = input |> List.max
        let stops = [min .. max]
        let allFuelCombinations = stops 
                                    |> List.map (fun s -> (s,input |> List.map(fun i -> Math.Abs(s - i )) |> List.sum))
        let bestCombo = allFuelCombinations |> List.map (fun (_,f) -> f) |> List.min 
        bestCombo 


    [<Fact>]
    let calcFuelForinput () = 
        let input = readInit "input.txt" 
        let fuel = calcFuel input
        Assert.Equal(37, fuel) 

    [<Fact>]
    let calcFuelForRealinput () = 
        let input = readInit "input1.txt" 
        let fuel = calcFuel input
        Assert.Equal(349812, fuel) 
     
    [<Fact>]
    let test2 () = 
        let input = readInit "input.txt" 
        Assert.Equal(10, input.Length) 

