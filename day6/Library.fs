namespace day6

module Input =
    open System.IO
    open Xunit
    open System

    type Newborn = int
    type LanternFish = int
    type Fish = Baby of Newborn | Adult of LanternFish 

    let createLanternFishFromDays (days: int) : Fish =
        Adult days 
 
    let createLanternFish () : LanternFish =
        let nrOfDay = 7
        nrOfDay - 1 
    
    let createNewbornFish () : Newborn =
        let nrOfDay = 2
        nrOfDay - 1 

    let passDay (fish: Fish) : (Fish * Newborn option) =
        match fish with 
            | Baby nb -> if nb = 0 then (Adult (createLanternFish ()), None) else (Baby (nb - 1), None)
            | Adult a -> if a = 0 then (Adult (createLanternFish ()), Some (createNewbornFish ())) else (Adult (a - 1), None)

    let readInit (filePath: string): int list = 
        use sr = new StreamReader (filePath) 
        let line = sr.ReadLine()
        let numbers = line.Split(",")
        numbers |> Array.map(fun f -> Int32.Parse(f)) |> Array.toList

    let getInitialFish (filePath: string): Fish list =
        let numbers = readInit filePath
        numbers |> List.map (fun f -> createLanternFishFromDays f)

    [<Fact>]
    let ReadlDataPart1CheckFish() =
        let fish  = getInitialFish "input.txt" 
        Assert.Equal(Adult 3, fish.Head)
        let fishes = fish |> List.map (fun f -> passDay f)
        Assert.Equal(fishes.Head, (Adult 2, None))

    [<Fact>]
    let ReadlDataPart1 () =
        let x  = readInit "input.txt"
        let fish  = getInitialFish "input.txt" 
        Assert.Equal(5, x |> List.length)
        Assert.Equal(5, fish |> List.length)
