namespace day3

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
      let diagnostics = readLines "input.txt"
      let firstLine = diagnostics |> Seq.head
      Assert.False(firstLine.Get(0))
      Assert.False(firstLine.Get(1))
      Assert.True(firstLine.Get(2))
      Assert.False(firstLine.Get(3))
      Assert.False(firstLine.Get(4))
 
      let secondLine = diagnostics |> Seq.skip 1 |> Seq.head
      Assert.True(secondLine.Get(0))
      Assert.True(secondLine.Get(1))
      Assert.True(secondLine.Get(2))
      Assert.True(secondLine.Get(3))
      Assert.False(secondLine.Get(4))

    [<Fact>]
    let checkBitesInParserWithOtherdataset ()= 
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

module Common = 
    open System
    open System.Collections
    open Input
    open Xunit

    let getMostCommonBit (report: BitArray list) (position: int) =
        let nrOfTrue = report |> List.map (fun f -> f.Get(position))
                              |> List.where id 
                              |> List.length 
        let nrOfFalse = report.Length - nrOfTrue 
        nrOfTrue >= nrOfFalse

    let bitArrayToInt (input: BitArray) : int =
        let bitArray = new BitArray(input.Length)
        for i in [0 .. (bitArray.Length - 1)] do
           bitArray.[bitArray.Length - i - 1] <- input.Get(i) 
        let resultBytes = Array.create<byte> 4 0uy
        bitArray.CopyTo(resultBytes, 0);
        BitConverter.ToInt32(resultBytes,0)

    [<Fact>]
    let checkMostCommonBit()= 
      let report = readLines "input.txt" |> Seq.toList
      Assert.Equal(true, getMostCommonBit report 0)
      Assert.Equal(false, getMostCommonBit report 1)
      Assert.Equal(true, getMostCommonBit report 2)
      Assert.Equal(true, getMostCommonBit report 3)
      Assert.Equal(false, getMostCommonBit report 4)
