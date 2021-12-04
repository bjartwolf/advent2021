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

//    let parseLines (filePath: string): 
    let isWinnerRow (row: Row) : bool =
        row |> Array.forall ( fun f -> match f with
                                | Marked -> true
                                | Nr _ -> false)

    let isWinnerBoard (board: Board) : bool =
        board |> Array.exists ( fun r -> isWinnerRow r)

    [<Fact>]
    let RowdWithMarkedRowIsWinner ()= 
        let r1 = [| Marked; Marked ; Marked ; Marked; Marked|]
        Assert.Equal(true, isWinnerRow r1)        

    [<Fact>]
    let BlarddWithMarkedRowIsWinner ()= 
        let r1 = [| Marked; Nr 4 ; Marked ; Marked; Marked|]
        let r2 = [| Marked; Marked ; Marked ; Marked; Marked|]
        let b = [|r1;r2|]
        Assert.Equal(true, isWinnerBoard b)        

    [<Fact>]
    let RowdWithUndrawnNrIsNotWinner ()= 
        let r1 = [| Marked; Nr 4 ; Marked ; Marked; Marked|]
        Assert.Equal(false, isWinnerRow r1)        


    [<Fact>]
    let readAllLines ()= 
      Assert.Equal(19, readLines "input.txt" |> Seq.length)
      Assert.Equal(601, readLines "input1.txt" |> Seq.length)
