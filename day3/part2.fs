namespace day3part2

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
    open System
    open System.Collections

    // In part two, it says 1 has priority over 0 when equal, so keeping it as it is 
    let getMostCommonBit (report: BitArray list) (position: int) =
        let bits = report |> List.map (fun f -> f.Get(position)) 
        let nrOfTrue = bits |> List.where id |> List.length 
        let nrOfFalse = bits |> List.map not |> List.where id |> List.length 
        nrOfTrue >= nrOfFalse

    let getLeastCommonBit (report: BitArray list) (position: int) =
        getMostCommonBit report position |> not

    let bitArrayToInt (input: BitArray) : int =
        let count = input.Length 
        let bitArray = new BitArray(count)
        for i in [0 .. (count - 1)] do
           bitArray.[count - (i+1)] <- input.Get(i) 
        let resultBytes = Array.create<byte> 16 0uy
        bitArray.CopyTo(resultBytes, 0);
        BitConverter.ToInt32(resultBytes,0)

    let rec getCore (report: BitArray list) (converter: bool -> bool) (position: int) =
        let mostCommonBit = getMostCommonBit report position
        let remaining = report |> List.where (fun f -> f.Get(position) = converter mostCommonBit)
        if (remaining.Length = 1) then 
            remaining.Head |> bitArrayToInt
        else 
            getCore remaining converter (position+1)

    let getOxygen (report: BitArray list) = 
        getCore report id 0

    let getScrubber (report: BitArray list) = 
        getCore report not 0 

    let getProduct (report: BitArray list) =
        getOxygen report * getScrubber report

    [<Fact>]
    let calcValueExample()= 
      let report = readLines "input.txt" |> Seq.toList
      Assert.Equal(23, getOxygen report)
      Assert.Equal(10, getScrubber report)
      Assert.Equal(230, getProduct report)

    [<Fact>]
    let calcValueFull()= 
      let report = readLines "input1.txt" |> Seq.toList
      Assert.Equal(5736383, getProduct report)


    [<Fact>]
    let checkMostCommonBit()= 
      let report = readLines "input.txt" |> Seq.toList
      Assert.Equal(true, getMostCommonBit report 0)
      Assert.Equal(false, getMostCommonBit report 1)
      Assert.Equal(true, getMostCommonBit report 2)
      Assert.Equal(true, getMostCommonBit report 3)
      Assert.Equal(false, getMostCommonBit report 4)

    [<Fact>]
    let checkLeastCommonBit()= 
      let report = readLines "input.txt" |> Seq.toList
      Assert.Equal(false, getLeastCommonBit report 0)
      Assert.Equal(true, getLeastCommonBit report 1)
      Assert.Equal(false, getLeastCommonBit report 2)
      Assert.Equal(false, getLeastCommonBit report 3)
      Assert.Equal(true, getLeastCommonBit report 4)


