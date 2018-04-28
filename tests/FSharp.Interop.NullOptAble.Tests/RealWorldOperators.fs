(**
Real World-ish examples using the Operators
===============
*)
module RealWorldOperatorsTests

open System
open System.Text
open FSharp.Interop.NullOptAble.Operators
(*** hide ***)
open Xunit
open FsUnit.Xunit

(**
    Binding operator means function isn't applied if a `None` parameter.
*)
[<Fact>]          
let ``Basic concat`` () =
    let x = "Hello "
    let y = "World"
    (x,y) ||>? ( + ) |> should equal (Some "Hello World")
[<Fact>]
let ``Basic concat none`` () =
    let x = "Hello "
    let y:string = null
    (x,y) ||>? ( + ) |> should equal (None)

(**
    Binding doesn't work if function returns a non-`_:null or Nullable<_> or Option<_>`. You can use map operator instead `|>?@` but becareful don't use it on something that could be null.
*)
[<Fact>]
let ``Basic nullable math`` () =
    let x = Nullable(3)
    let y = Nullable(3)
    (x,y) ||>?@ ( + ) |> should equal (Some 6)

(*** hide ***)
[<AllowNullLiteral>]
type Node (child:Node)=
    new() = new Node(null)
    member val child:Node = child with get,set
    

(**
Doing the things found in this [MSDN Blog Post did with the Safe Nav operator](https://blogs.msdn.microsoft.com/jerrynixon/2014/02/26/at-last-c-is-getting-sometimes-called-the-safe-navigation-operator/)

*)
[<Fact>]
let ``Safe Navigation Operator Example`` ()=
    let getChild (n:Node) = n.child
    let parent = Node()
    parent 
        |>? getChild 
        |>? getChild
        |>? getChild
        |> should equal None

[<Fact>] 
let ``Safe Navigation Operator Seq Example`` ()=
    let getChild (n:Node) = n.child
    let parents = [
                    Node() //parent = some
                    Node() |> Node //parent.child = some
                    Node() |> Node |> Node //parent.child.child = some
                    Node() |> Node |> Node |> Node //parent.child.child.child = some
                  ]
    seq {
        for parent in parents  do
            yield parent 
                |>? getChild 
                |>? getChild
                |>? getChild
    } 
        |> Seq.choose id
        |> Seq.length |> should equal 1


(** 
Solution using `|>?` for this [RNA Transcription problem from exercism](http://exercism.io/exercises/fsharp/rna-transcription/readme).

**Note**: `|>? fun (s:StringBuilder) ->` This is because `|>?` needs argument for lambda overloads to work.
*)
[<Fact>]
let ``RNA transcriptions `` () =
    let toRna (dna: string): string option = 
        let combine (sb:StringBuilder option) =
            let append (c:char) = 
                sb |>? (fun (s:StringBuilder) -> s.Append(c))       
            function
            | 'G' -> 'C' |> append
            | 'C' -> 'G' |> append
            | 'T' -> 'A' |> append
            | 'A' -> 'U' |> append
            | ___ -> None
        dna
           |>? Seq.fold combine (Some <| StringBuilder())
           |>? string

    toRna "ACGTGGTCTTAA" |> should equal (Some "UGCACCAGAAUU")
