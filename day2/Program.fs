namespace day2

type Command = Up of int | Down of int | Forward of int

module Input =
    open System
    open System.IO

    let readLines (filePath:string) : Command seq = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let chars = sr.ReadLine().ToCharArray() 
            let value = (chars.[chars.Length - 1] |> int) - 48
            match chars.[0] with
                | 'f' -> yield Forward value 
                | 'd' -> yield Down value 
                | 'u' -> yield Up value 
   }

module Main =
    open Input
    open Xunit

    let rec move ((h,d): int*int) (aim:int) (commands: Command list)  =
        match commands with 
            | Forward x :: tail -> move (h + x, d + x * aim) aim tail 
            | Up x :: tail -> move (h, d) (aim - x) tail 
            | Down x :: tail -> move (h, d) (aim + x) tail 
            | _ -> (h, d)

    let multPos (h,d) = h * d

    [<Fact>]
    let moveGoesTo15and10()= 
      let commands = readLines "input.txt" |> Seq.toList
      let pos = move (0,0) 0 commands
      Assert.Equal((15,60), pos)
      Assert.Equal(900, multPos pos)
 
    [<Fact>]
    let checkTestdata()= 
      let count = readLines "input.txt" |> Seq.length
      Assert.Equal(6, count)
       
    [<Fact>]
    let checkAssignment()= 
      let commands = readLines "input1.txt" |> Seq.toList
      Assert.Equal(1000, List.length commands)
      let pos = move (0,0) 0 commands
      Assert.Equal((1965,1071386), pos)
      Assert.Equal(2105273490, multPos pos)
