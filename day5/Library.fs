namespace day5

module Input =
    open System.IO
    open Xunit
    open System

    type Point = { X: int; Y: int } 
    type LS = { S: Point; E: Point }
    type LineType = Vertical of LS | Horizontal of LS 

    let getLineTypeFromLineSegment (ls: LS ): LineType option =
        match ls with
            | { S = { X = x1; Y = y1};
                E = { X = x2; Y = y2}} when x1 = x2 && y1 <> y2 -> Some (Vertical ls)
            | { S = { X = x1; Y = y1};
                E = { X = x2; Y = y2}} when x1 <> x2 && y1 = y2 -> Some (Horizontal ls)
            | _ -> None

    let readLines (filePath:string): LineType option seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine()
            let lineparts = line.Split (" -> ")
            let p1raw = lineparts.[0]
            let p2raw = lineparts.[1]
            let x1y1raw = p1raw.Split(",")
            let x2y2raw = p2raw.Split(",")
            let p1 = { X = Int32.Parse(x1y1raw.[0]); Y = Int32.Parse(x1y1raw.[1])} 
            let p2 = { X = Int32.Parse(x2y2raw.[0]); Y = Int32.Parse(x2y2raw.[1])} 
            let s = {S = p1; E = p2}
            let lt = getLineTypeFromLineSegment s
            yield lt 
        }

    [<Fact>]
    let ReadlDataPart1() =
        let x  = readLines "input.txt"
        Assert.Equal(10, x |> Seq.length)


