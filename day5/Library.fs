namespace day5

module Input =
    open System.IO
    open Xunit
    open System

    let readLines (filePath:string): string seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield sr.ReadLine()
        }

    [<Fact>]
    let ReadlDataPart1() =
        let x  = readLines "input.txt"
        Assert.Equal(1, x |> Seq.length)


