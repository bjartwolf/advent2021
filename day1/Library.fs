namespace day1

module Input =
    open System.IO
    let readLines (filePath:string) = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield System.Int32.Parse(sr.ReadLine ())
    }

module Main =
    open Input
    open Xunit
    let rec makeTriples(list: int list) =
        list |> List.windowed 3
             |> List.map (fun (x) -> (x.Head, x.Tail.Head, x.Tail.Tail.Head))

    let rec makeTuples(list: (int*int*int) list) =
        list |> List.windowed 2
             |> List.map (fun (x) -> (x.Head, x.Tail.Head))

    let countIncreasing (inputPath: string): int =
       let input = readLines inputPath |> Seq.toList
       let triples = makeTriples input 
       let tuples = makeTuples triples 
       tuples |> List.where ( fun ((a,b,c), (d,e,f)) ->  (a+b+c) < (d+e+f))
              |> List.length

    [<Fact>]
    let checkTestdata()= 
      let count = countIncreasing "input.txt"
      Assert.Equal(5, count)
       
    [<Fact>]
    let checkAssignment()= 
       let count = countIncreasing "input1.txt"
       Assert.Equal(1618, count)
