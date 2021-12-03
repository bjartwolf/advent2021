

namespace day3



module Input =
    open System.IO
    open System.Collections
    open Xunit
    open System.Text
    open System

    let readLines (filePath:string): BitArray seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine () 
            let nr = Convert.ToInt32(line, 2)
            let bytes = BitConverter.GetBytes(nr);
            yield new BitArray(bytes) 
    }

    [<Fact>]
    let readAllLines ()= 
      Assert.Equal(12, readLines "input.txt" |> Seq.length)
      Assert.Equal(1000, readLines "input1.txt" |> Seq.length)

    [<Fact>]
    let checkBitesInParser ()= 
      // 00100
      // 11110
      // 0 is the LSB, can get bits up to nr 32 (index 31)
      let diagnostics = readLines "input.txt"
      let firstLine = diagnostics |> Seq.head
      Assert.False(firstLine.Get(0))
      Assert.False(firstLine.Get(1))
      Assert.True(firstLine.Get(2))
      Assert.False(firstLine.Get(3))
      Assert.False(firstLine.Get(4))
      Assert.False(firstLine.Get(31))
 
      let secondLine = diagnostics |> Seq.skip 1 |> Seq.head
      Assert.False(secondLine.Get(0))
      Assert.True(secondLine.Get(1))
      Assert.True(secondLine.Get(2))
      Assert.True(secondLine.Get(3))
      Assert.True(secondLine.Get(4))
      Assert.False(secondLine.Get(7))
 

    
 
module Main =
    open Input
    open Xunit

 
