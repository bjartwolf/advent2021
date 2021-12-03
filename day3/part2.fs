namespace day3part2

module Main =
    open day3.Input
    open day3.Common
    open Xunit
    open System.Collections

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
