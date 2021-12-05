namespace day5

module Input =
    open System.IO
    open Xunit
    open System

    type Point = { X: int; Y: int } 
    type LS = { S: Point; E: Point }
    type LineType = Vertical of LS | Horizontal of LS 

    let getLineTypeFromLineSegment (ls: LS ): LineType =
        match ls with
            | { S = { X = x1; Y = y1}; E = { X = x2; Y = y2}} -> Vertical ls

    let readLines (filePath:string): LineType option seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine()
            let p1 = { X = 0; Y = 0} 
            let p2 = { X = 10; Y = 0} 
            let s = {S = p1; E = p2}
            yield Some (Vertical s)
        }

    [<Fact>]
    let ReadlDataPart1() =
        let x  = readLines "input.txt"
        Assert.Equal(10, x |> Seq.length)


