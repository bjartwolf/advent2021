namespace day1

module Input =
    open System.IO
    open Xunit
    let readLines (filePath:string) = seq {
        use sr = new StreamReader (filePath)
        while not sr.EndOfStream do
            yield System.Int32.Parse(sr.ReadLine ())
    }

    [<Fact>]
    let readTestData ()= 
       let testdata = readLines "input.txt" 
       Assert.Equal(10,testdata |> Seq.length)

    [<Fact>]
    let readTestDataAsInt ()= 
       let testdata = readLines "input.txt" 
       let answer = [ 199; 200; 208; 210;200; 207; 240; 269; 260; 263]
       Assert.Equal(answer, testdata)

module Main =
    open Input
    open Xunit
    let lines = readLines "input.txt" |> Seq.toList

    let rec makeTuples (list: int list) =
        match list with
            | head :: tail :: rest -> (head,tail) :: makeTuples (tail:: rest) 
            | _ -> [] 


    [<Fact>]
    let checkTestdata()= 
       let testdata = readLines "input.txt" |> Seq.toList
       let tuples = makeTuples testdata
       let count = tuples |> List.where ( fun (a,b) -> b > a) |> List.length
       Assert.Equal(7, count)
       
    [<Fact>]
    let checkAssignment()= 
       let testdata = readLines "input1.txt" |> Seq.toList
       let tuples = makeTuples testdata
       let count = tuples |> List.where ( fun (a,b) -> b > a) |> List.length
       Assert.Equal(1581, count)

