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

    [<Fact>]
    let test2 () = 
        let input = readInit "input.txt" 
        Assert.Equal(5, input.Length) 
 
