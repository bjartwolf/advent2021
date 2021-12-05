namespace day5

module Input =
    open System.IO
    open Xunit
    open System

    type Point = { X: int; Y: int } 
    type LineSegment = { S: Point; E: Point }
    type LineType = VerticalLine of LineSegment | HorizontalLine of LineSegment 

    let readLines (filePath:string): LineType option seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine()
            let p1 = { X = 0; Y = 0} 
            let p2 = { X = 10; Y = 0} 
            let s = {S = p1; E = p2}
            yield Some (VerticalLine s)
        }

    [<Fact>]
    let ReadlDataPart1() =
        let x  = readLines "input.txt"
        Assert.Equal(10, x |> Seq.length)


