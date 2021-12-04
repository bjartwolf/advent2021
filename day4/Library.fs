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
    
    type NumbersToDraw = int list
    type Field = Marked | Nr of int
    type Row = Field []
    type Board = Row [] 
    type Boards = Board list 

    let parseInput (filePath: string): (Boards * NumbersToDraw) = 
        let getNumbersFromCsvLine (line: string) = line.Split(",") |> Array.map (fun x -> Int32.Parse(x)) |> Array.toList
        let lines = readLines filePath |> Seq.toList
        let numbersLine = lines |> List.head 
        let numbers = getNumbersFromCsvLine numbersLine 

        let boardsInput = lines.Tail.Tail |> List.filter (fun s -> String.IsNullOrEmpty(s) |> not)
        let nrOfBoards = boardsInput.Length / 5
        let boardGroups = boardsInput |> List.splitInto(nrOfBoards)
        let boards = boardGroups |> List.map (fun b -> 
            b |> List.toArray |> Array.map ( fun r ->
                r.Split(" ") |> Array.filter( fun s -> String.IsNullOrEmpty(s) |> not) |> Array.map (fun i -> Nr (Int32.Parse(i)))))

        ( boards, numbers)

    [<Fact>]
    let ParseingInputYields27NumbersAndThreeBoards()= 
        let (boards, numbers) = parseInput "input.txt" 
        Assert.Equal(27, numbers.Length)        
        Assert.Equal(3, boards.Length)        

//    let parseLines (filePath: string): 
    let isWinnerRow (row: Row) : bool =
        row |> Array.forall ( fun f -> match f with
                                | Marked -> true
                                | Nr _ -> false)

    let sumOfBoard (board: Board): int =
        let fields = board |> Array.collect (fun r -> r |> Array.map (fun f -> match f with 
                                                                | Nr x -> x
                                                                | Marked -> 0)) 
        Array.sum(fields)

    [<Fact>]
    let SumOfABoard () =
        let (b, nums) = parseInput "input.txt"
        let firstBoard = b.Head
        Assert.Equal(300, sumOfBoard firstBoard)

    let isWinnerColumn (board: Board) : bool =
        let lengthOfBoard = board.[0].Length
        let columnIndexes = [0 .. lengthOfBoard - 1] 
        columnIndexes |> List.exists (fun i ->
            let makeRow = board |> Array.map( fun x -> x.[i])
            isWinnerRow makeRow)

    let isWinnerBoard (board: Board) : bool =
        board |> Array.exists ( fun r -> isWinnerRow r) || isWinnerColumn board 

    let markBoard (board: Board) (nrToMark: int) : Board =
        board |> Array.map (fun r -> r|> Array.map (fun (f: Field) ->
            match f with 
                | Marked -> Marked
                | Nr x -> if x = nrToMark then Marked else Nr x
            )) 

    [<Fact>]
    let RowdWithMarkedRowIsWinner ()= 
        let r1 = [| Marked; Marked ; Marked ; Marked; Marked|]
        Assert.Equal(true, isWinnerRow r1)        

    [<Fact>]
    let BoardWithMarkedRowIsWinner ()= 
        let r1 = [| Marked; Nr 4 ; Marked ; Marked; Marked|]
        let r2 = [| Marked; Marked ; Marked ; Marked; Marked|]
        let b = [|r1;r2|]
        Assert.Equal(true, isWinnerBoard b)        

    [<Fact>]
    let BoardWithMarkedColumnIsWinner ()= 
        let r1 = [| Nr 3; Nr 4 ; Marked ; Marked; Marked|]
        let r2 = [| Nr 1; Nr 3; Nr 3; Marked; Nr 4|]
        let r3 = [| Nr 1; Nr 3; Marked ; Marked; Marked|]
        let b = [|r1;r2;r3|]
        Assert.Equal(true, isWinnerBoard b)        

    [<Fact>]
    let BoardWithNoCompleteMarkedRowIsNotWinner ()= 
        let r1 = [|  Nr 4 ; Marked ; Marked; Nr 4|]
        let r2 = [|  Marked ; Nr 3; Nr 3; Marked|]
        let b = [|r1;r2|]
        Assert.Equal(false, isWinnerBoard b)        

    [<Fact>]
    let MarkingBoardMakesItWinner()= 
        let r1 = [|  Nr 4 ; Marked ; Marked; Nr 4|]
        let r2 = [|  Marked ; Nr 3; Nr 3; Marked|]
        let b = [|r1;r2|]
        let markedBoard = markBoard b 3
        Assert.Equal(true, isWinnerBoard markedBoard)        
 
    [<Fact>]
    let RowdWithUndrawnNrIsNotWinner ()= 
        let r1 = [| Marked; Nr 4 ; Marked ; Marked; Marked|]
        Assert.Equal(false, isWinnerRow r1)        

    [<Fact>]
    let readAllLines ()= 
      Assert.Equal(19, readLines "input.txt" |> Seq.length)
      Assert.Equal(601, readLines "input1.txt" |> Seq.length)
