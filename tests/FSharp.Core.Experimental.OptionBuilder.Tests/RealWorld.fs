module RealWorldTests

open System
open Xunit
open FsUnit.Xunit
open FSharp.Core.Experimental.OptionBuilder
open System.Text

[<Fact>]
let ``Basic nullable math`` () =
    let x = Nullable(3)
    let y = Nullable(3)
    option {
        let! x' = x
        let! y' = y
        return (x' + y')
    } |> should equal (Some 6)

[<Fact>]
let ``RNA transcriptions `` () =
    //test is from exercism 
    //http://exercism.io/exercises/fsharp/rna-transcription/readme
    let toRna (dna: string): string option = 
        let combine (sb:StringBuilder option) =
            let append (c:char) = option {
                    let! sb' = sb
                    return sb'.Append(c)
                }
            function
                    | 'G' -> 'C' |> append
                    | 'C' -> 'G' |> append
                    | 'T' -> 'A' |> append
                    | 'A' -> 'U' |> append
                    | ___ -> None
        option {
            let! dna' = dna //handles if string is null
            let! sb' = dna' |> Seq.fold combine (Some <| StringBuilder())
            return sb'.ToString()
        }
    
    toRna "ACGTGGTCTTAA" |> should equal (Some "UGCACCAGAAUU")
