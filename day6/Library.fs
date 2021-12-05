namespace day6

module Input =
    open System.IO
    open Xunit
    open System


    let readLines (filePath:string): string seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine()
            yield line 
        }

    [<Fact>]
    let ReadlDataPart1() =
        let x  = readLines "input.txt"
        Assert.Equal(10, x |> Seq.length)
