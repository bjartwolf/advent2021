namespace day3part1

module Main =
    open day3.Input
    open day3.Common
    open Xunit
    open System.Collections

    let conversionCore (report: BitArray list) (converter: bool -> bool) =
        let count = (report |> List.head).Length
        let bitArray = new BitArray(count)
        for i in [0 .. (count - 1)] do
           bitArray.[i] <- converter (getMostCommonBit report i)
        bitArrayToInt bitArray
 
    let getEpsilonFromReport (report: BitArray list) =
        conversionCore report not
   
    let getGammaFromReport (report: BitArray list) =
        conversionCore report id 

    let getProductFrom (report: BitArray list) =
        getGammaFromReport report * getEpsilonFromReport report

    [<Fact>]
    let calcValueExample()= 
      let report = readLines "input.txt" |> Seq.toList
      Assert.Equal(22, getGammaFromReport report)
      Assert.Equal(9, getEpsilonFromReport report)
      Assert.Equal(198, getProductFrom report)

    [<Fact>]
    let calcValueFull()= 
      let report = readLines "input1.txt" |> Seq.toList
      Assert.Equal(3277364, getProductFrom report)


    [<Fact>]
    let checkMostCommonBit()= 
      let report = readLines "input.txt" |> Seq.toList
      Assert.Equal(true, getMostCommonBit report 0)
      Assert.Equal(false, getMostCommonBit report 1)
      Assert.Equal(true, getMostCommonBit report 2)
      Assert.Equal(true, getMostCommonBit report 3)
      Assert.Equal(false, getMostCommonBit report 4)

