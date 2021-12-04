namespace day4
module Input =
    open System.IO
    open System.Collections
    open Xunit
    open System

    let readLines (filePath:string): string seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield sr.ReadLine()
        }

    [<Fact>]
    let readAllLines ()= 
      Assert.Equal(19, readLines "input.txt" |> Seq.length)
      Assert.Equal(601, readLines "input1.txt" |> Seq.length)
