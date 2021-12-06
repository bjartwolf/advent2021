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

    let passDay (fish: Fish) : Fish list =
        match fish with 
            | Baby nb -> if nb = 0 then [Adult (createLanternFish ())] else [Baby (nb - 1)]
            | Adult a -> if a = 0 then [Adult (createLanternFish ()); Baby( createNewbornFish ())] else [Adult (a - 1)]

    let passDayForFishes (fishes: Fish list) : Fish list =
        fishes |> List.map (fun f -> passDay f) |> List.collect id

    let readInit (filePath: string): int list = 
        use sr = new StreamReader (filePath) 
        let line = sr.ReadLine()
        let numbers = line.Split(",")
        numbers |> Array.map(fun f -> Int32.Parse(f)) |> Array.toList

    let getInitialFish (filePath: string): Fish list =
        let numbers = readInit filePath
        numbers |> List.map (fun f -> createLanternFishFromDays f)

    let rec passNDays (fish: Fish list) (days: int) : Fish list = 
        if days = 0 then fish
        else 
            let nextDay = passDayForFishes fish
            passNDays nextDay (days - 1)
        
    [<Fact>]
    let CheckFullData () =
        let day0 = getInitialFish "input1.txt" 
        let day80 = passNDays day0 80 
        Assert.Equal(387413, day80.Length)


    let readFishes (input: int list) : int64 [] =
        let groupedInput = input |> List.groupBy id 
        let array = Array.create 8 0L 
        for (x,xs) in groupedInput do 
            array.[x] <- int64 xs.Length
        array 

    let nextDay (fishes: int64 []) (babies: int64 []) : (int64[] * int64 []) =
        let newFish = fishes.[0] + babies.[0]
        let fish0 = fishes.[0]
        babies.[0] <- babies.[1]
        babies.[1] <- fishes.[0] 
        fishes.[0] <- fishes.[1]
        fishes.[1] <- fishes.[2]
        fishes.[2] <- fishes.[3]
        fishes.[3] <- fishes.[4]
        fishes.[4] <- fishes.[5]
        fishes.[5] <- fishes.[6]
        fishes.[6] <- newFish 
        (fishes, babies)

    let rec moveDays (fishes: int64[]) (babies: int64[]) (daysToGo: int) : int64 =
        if daysToGo = 0 then fishes.[0] + fishes.[1] + fishes.[2] + fishes.[3] + fishes.[4] + fishes.[5] + fishes.[6] + babies.[0] + babies.[1]
        else 
           let (nextFish, nextBaby) = nextDay fishes babies
           moveDays nextFish nextBaby (daysToGo - 1) 

      
    [<Fact>]
    let test2 () = 
        let input = readInit "input.txt" 
        let foo = readFishes input
        Assert.Equal(2L, foo.[3])
        Assert.Equal(1L, foo.[1])
        let day18 = moveDays foo [|0L;0L|] 18
        Assert.Equal(26L, day18)

    [<Fact>]
    let test3 () = 
        let input = readInit "input.txt" 
        let foo = readFishes input
        let day80 = moveDays foo [|0L;0L|] 80 
        Assert.Equal(5934L, day80)

    [<Fact>]
    let test4 () = 
        let input = readInit "input1.txt" 
        let foo = readFishes input
        let day80 = moveDays foo [|0L;0L|] 80 
        Assert.Equal(387413L, day80)

    [<Fact>]
    let test5 () = 
        let input = readInit "input1.txt" 
        let foo = readFishes input
        let day80 = moveDays foo [|0L;0L|] 256 
        Assert.Equal(1738377086345L, day80)


    [<Fact>]
    let CheckTestData () =
        let day0 = getInitialFish "input.txt" 
        let day18 = passNDays day0 18
        Assert.Equal(26, day18.Length)
//        let day80 = passNDays day0 80 
//        Assert.Equal(5934, day80.Length)
//        let day256 = passNDays day0 256 
//        Assert.NotEmpty(day256)
 
    [<Fact>]
    let ReadlDataPart1CheckFish() =
        let day0 = getInitialFish "input.txt" 
        Assert.Equal(Adult 3, day0.Head)
        let day1 = passDayForFishes day0 
        Assert.Equal(day1.Head, Adult 2)
        Assert.Equal(day1.Tail.Head, Adult 3)
        let day2 = passDayForFishes day1 
        Assert.Equal(6, day2.Length)

        let day3 = passDayForFishes day2 
        Assert.Equal(7, day3.Length)

        Assert.True((day0 = passNDays day0 0))
        Assert.True((day3 = passNDays day0 3))

    [<Fact>]
    let ReadlDataPart1 () =
        let x  = readInit "input.txt"
        let fish  = getInitialFish "input.txt" 
        Assert.Equal(5, x |> List.length)
        Assert.Equal(5, fish |> List.length)
