﻿namespace day3

module Input =
    open System.IO
    open System.Collections
    open Xunit
    open System

    let readLines (filePath:string): BitArray seq  = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            let line = sr.ReadLine() 
            let chars = line.Length // could definitely to this just once, but the cost is maybe not massive so....
            let nr = Convert.ToInt32(line, 2)
            let ba = new BitArray(BitConverter.GetBytes(nr)) 
            let reversedArray = new BitArray(chars) 
            for i in [0 .. chars - 1] do
               reversedArray.[i] <- ba.[chars - i - 1] 
            yield reversedArray 
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
//      Assert.False(firstLine.Get(31))
 
      let secondLine = diagnostics |> Seq.skip 1 |> Seq.head
      Assert.True(secondLine.Get(0))
      Assert.True(secondLine.Get(1))
      Assert.True(secondLine.Get(2))
      Assert.True(secondLine.Get(3))
      Assert.False(secondLine.Get(4))
 //     Assert.False(secondLine.Get(7))

    [<Fact>]
    let checkBitesInParserWithOtherdataset ()= 
      //111100000101
      // 0 is the LSB, can get bits up to nr 32 (index 31)
      let diagnostics = readLines "input1.txt"
      let firstLine = diagnostics |> Seq.head
      Assert.True(firstLine.Get(0))
      Assert.True(firstLine.Get(1))
      Assert.True(firstLine.Get(2))
      Assert.True(firstLine.Get(3))
      Assert.False(firstLine.Get(4))
      Assert.False(firstLine.Get(5))
      Assert.False(firstLine.Get(6))
      Assert.False(firstLine.Get(7))
      Assert.False(firstLine.Get(8))
      Assert.True(firstLine.Get(9))
      Assert.False(firstLine.Get(10))
      Assert.True(firstLine.Get(11))

    
 
module Main =
    open Input
    open Xunit

    [<Fact>]
    let countFirstBit()= 
      Assert.Equal(12, readLines "input.txt" |> Seq.length)
 
