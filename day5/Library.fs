namespace day5

module Input =
    open System.IO
    open Xunit
    open System

    type Point = { X: int; Y: int } 
    type Points = Point list
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

    let getPointsFromHorizontalLine (ls: LS) : Points = 
        let a = ls.E.X - ls.S.X
        let inc = if a > 0 then 1 else - 1 
        let xs = [ls.S.X .. inc .. ls.E.X]
        let points = xs |> List.map (fun x -> { X = x; Y = ls.S.Y })
        points

    let getPointsFromVerticalLine (ls: LS) : Points = 
        let a = ls.E.Y - ls.S.Y
        let inc = if a > 0 then 1 else - 1 
        let ys = [ls.S.Y .. inc .. ls.E.Y]
        let points = ys |> List.map (fun y -> { X = ls.S.X; Y = y })
        points


    let getPointsFromLine (lt: LineType) : Points = 
        match lt with 
            | Vertical l -> getPointsFromVerticalLine l 
            | Horizontal l -> getPointsFromHorizontalLine l 
        
    [<Fact>]
    let GetPointsOnHorizontalLine () =
        let p_2_2 = { X = 2; Y = 2}
        let p_3_2 = { X = 3; Y = 2}
        let p_4_2 = { X = 4; Y = 2}
        let p_5_2  = { X = 5; Y = 2}

        let segment = {S = p_2_2; E = p_5_2 }
        let lt = getLineTypeFromLineSegment segment
        let points = getPointsFromLine lt.Value

        Assert.True(points |> List.contains p_2_2)
        Assert.True(points |> List.contains p_3_2)
        Assert.True(points |> List.contains p_4_2)
        Assert.True(points |> List.contains p_5_2)

    [<Fact>]
    let GetPointsOnVerticalLine () =
        let p_2_2 = { X = 2; Y = 2}
        let p_2_3 = { X = 2; Y = 3}
        let p_2_4 = { X = 2; Y = 4}
        let p_2_5  = { X = 2; Y = 5}
        let p_2_6  = { X = 2; Y = 6}

        let segment = {S = p_2_5; E = p_2_2 }
        let lt = getLineTypeFromLineSegment segment
        let points = getPointsFromLine lt.Value

        Assert.True(points |> List.contains p_2_2)
        Assert.True(points |> List.contains p_2_3)
        Assert.True(points |> List.contains p_2_4)
        Assert.True(points |> List.contains p_2_5)
        Assert.False(points |> List.contains p_2_6)
  
    [<Fact>]
    let ReadlDataPart1() =
        let x  = readLines "input.txt"
        let validLines = x |> Seq.choose id
        Assert.Equal(10, x |> Seq.length)
        Assert.Equal(6, validLines  |> Seq.length)


