namespace day1

module Input =
    open System.IO
    open Xunit
    let readLines (filePath:string) = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield System.Int32.Parse(sr.ReadLine ())
    }

module Main =
    open Input
    open Xunit
    let lines = readLines "input.txt" |> Seq.toList

    let rec makeTriples(list: int list) =
        match list with
            | head :: tail :: ttail :: rest -> (head,tail,ttail) :: makeTriples(tail:: ttail:: rest) 
            | _ -> [] 

    let rec makeTuples(list: (int*int*int) list) =
        match list with
            | head :: tail :: rest -> (head,tail) :: makeTuples(tail:: rest) 
            | _ -> [] 

    [<Fact>]
    let checkTestdata()= 
       let testdata = readLines "input.txt" |> Seq.toList
       let triples = makeTriples testdata
       let tuples = makeTuples triples 
       let count = tuples |> List.where ( fun ((a,b,c), (d,e,f)) ->  (a+b+c) < (d+e+f)) |> List.length
       Assert.Equal(5, count)

       let sums = [607;618; 618; 617; 647; 716; 769; 792];
       Assert.Equal<int list>(sums, (triples |> List.map (fun (a,b,c) -> a+b+c)))
       
    [<Fact>]
    let checkAssignment()= 
       let testdata = readLines "input1.txt" |> Seq.toList
       let triples = makeTriples testdata
       let tuples = makeTuples triples 
       let count = tuples |> List.where ( fun ((a,b,c), (d,e,f)) ->  (a+b+c) < (d+e+f)) |> List.length
       Assert.Equal(1618, count)

