namespace day3


module Input =
    open System.IO

    let readLines (filePath:string)  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine () 
            yield line
    }

module Main =
    open Input
    open Xunit

    [<Fact>]
    let readAllLines ()= 
      Assert.Equal(12, readLines "input.txt" |> Seq.length)
      Assert.Equal(1000, readLines "input1.txt" |> Seq.length)
     
 
 
