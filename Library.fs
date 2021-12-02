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
    let _ = readLines ""
    

