namespace day1

module Input =
    open System.IO
    let readLines (filePath:string) = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield System.Int32.Parse(sr.ReadLine ())
    }

module Main =
    open Input
    open Xunit

    let countIncreasing (inputPath: string): int =
       readLines inputPath 
             |> Seq.toList 
             |> List.windowed 3 
             |> List.windowed 2
             |> List.where (fun f -> f.Head |> List.sum < (f.Tail.Head |> List.sum)) 
             |> List.length

    [<Fact>]
    let checkTestdata()= 
      let count = countIncreasing "input.txt"
      Assert.Equal(5, count)
       
    [<Fact>]
    let checkAssignment()= 
       let count = countIncreasing "input1.txt"
       Assert.Equal(1618, count)
