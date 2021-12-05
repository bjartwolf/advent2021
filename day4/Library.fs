namespace day4

module Input =
    open System.IO
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
                r.Split(" ") |> Array.filter( fun s -> not (String.IsNullOrEmpty(s))) |> Array.map (fun i -> Nr (Int32.Parse(i)))))

        ( boards, numbers)

    let isWinnerRow (row: Row) : bool =
        row |> Array.forall ( fun f -> match f with
                                        | Marked -> true
                                        | Nr _ -> false)

    let sumOfBoard (board: Board): int =
        let fields = board |> Array.collect (fun r -> r |> Array.map (fun f -> match f with 
                                                                                | Nr x -> x
                                                                                | Marked -> 0)) 
        Array.sum(fields)

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

    let rec findWinnerProduct (boards: Boards) (numbers: NumbersToDraw) =
        let number = numbers.Head
        let markedBoards = boards |> List.map (fun b -> markBoard b number)
        let winners = markedBoards |> List.filter isWinnerBoard
        if (winners.IsEmpty) then
            findWinnerProduct markedBoards numbers.Tail
        else
           sumOfBoard winners.Head * number

    let rec findLooserProduct (boards: Boards) (numbers: NumbersToDraw) =
        let number = numbers.Head
        let markedBoards = boards |> List.map (fun b -> markBoard b number)
        let loosers = markedBoards |> List.filter (isWinnerBoard >> not)
        if (loosers.IsEmpty) then
            sumOfBoard markedBoards.Head * number
        else
            findLooserProduct loosers numbers.Tail

    [<Fact>]
    let TestDataPart1 () =
        let (b, num) = parseInput "input.txt"
        Assert.Equal(4512, findWinnerProduct b num)

    [<Fact>]
    let TestDataPart2 () =
        let (b, num) = parseInput "input.txt"
        Assert.Equal(1924, findLooserProduct b num)

    [<Fact>]
    let ReadlDataPart1() =
        let (b, num) = parseInput "input1.txt"
        Assert.Equal(6592, findWinnerProduct b num)

    [<Fact>]
    let RealDataPart2 () =
        let (b, num) = parseInput "input1.txt"
        Assert.Equal(31755, findLooserProduct b num)
