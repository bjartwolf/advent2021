namespace day8

module Input =
    open System
    open System.IO
    open Xunit 

    let one = 2
    let four = 4
    let seven = 3
    let eight = 7

    let readLines (filePath:string): string[] seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine() 
            let foo = line.Split("|").[1]
            yield foo.Split(" ") 
    }


    let readInit (filePath: string): string[] list = 
        let input = readLines filePath 
        input |> Seq.toList 

    let findNrOfOccurances (input: string[]) (targetLength: int) : int =
        input |> Array.filter(fun s -> s.Length = targetLength) |> Array.length

    let findNrOfOccurancesOfList (input: string[]) (targetLength: int list) : int =
        targetLength |> List.map (fun t -> findNrOfOccurances input t)
                     |> List.sum

    let findNrOfOccurancesTotal (input: string[] list) (targetLength: int list) : int =
        input |> List.map (fun i -> findNrOfOccurancesOfList i targetLength)
              |> List.sum

    [<Fact>]
    let findNr() = 
        let foo = [| "foo"; "foo"; "baaz"|]
        let nr = findNrOfOccurances foo 3
        Assert.Equal(2,nr)

    [<Fact>]
    let findNr2() = 
        let foo = [| "foo"; "foo"; "baaz"; "moooooff"|]
        let nr = findNrOfOccurancesOfList foo [3;4]
        Assert.Equal(3,nr)

    [<Fact>]
    let calcFuelForinput () = 
        let input = readInit "input.txt" 
        Assert.Equal(10, input.Length)
        Assert.Equal(26, findNrOfOccurancesTotal input [one;four;seven;eight])

        // a = A
        // b = E
        // c = B
        // d = G 
        // e = F
        // f = C
        // g = D 
    [<Fact>]
    let calcFuelForinput2 () = 
        let input = readInit "input1.txt" 
        Assert.Equal(272, findNrOfOccurancesTotal input [one;four;seven;eight])

 
