namespace day6

module Input =
    open System
    open System.IO
    open Xunit 

    let readInit (filePath: string): int list = 
        use sr = new StreamReader (filePath) 
        let line = sr.ReadLine()
        let numbers = line.Split(",")
        numbers |> Array.map(fun f -> Int32.Parse(f)) |> Array.toList

    let readFishes (input: int list) : int64 [] =
        let groupedInput = input |> List.groupBy id 
        let array = Array.create 8 0L 
        for (x,xs) in groupedInput do 
            array.[x] <- int64 xs.Length
        array 

    let nextDay (fishes: int64 []) (babies: int64 []) : (int64[] * int64 []) =
        let newFish = fishes.[0] + babies.[0]
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
