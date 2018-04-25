namespace FSharp.Interop.NullOptAble

module Operators =
    open System

    //static generic inlining inspired by http://stackoverflow.com/a/2812306/637783
    type NullCoalesce =  
        static member Coalesce(a: 'a option, b: 'a Lazy) = a |> Option.defaultWith b.Force
        static member Coalesce(a: 'a Nullable, b: 'a Lazy) = a |> Option.ofNullable 
                                                               |> Option.defaultWith b.Force
        static member Coalesce(a: 'a when 'a:null, b: 'a Lazy) = a |> Option.ofObj 
                                                                   |> Option.defaultWith b.Force

    let inline nullCoalesceHelper< ^t, ^a, ^b, ^c when (^t or ^a) : (static member Coalesce : ^a * ^b -> ^c)> a b = 
                                                ((^t or ^a) : (static member Coalesce : ^a * ^b -> ^c) (a, b))

    let inline (|??) a b = nullCoalesceHelper<NullCoalesce, _, _, _> a b



    type NullPipe =  
        static member Into(a: 'a option, f: 'a -> 't option) = Option.bind f a
        static member Into(a: 'a option, f: 'a -> 't) = Option.map f a
        static member Into(a: 'a Nullable, f: 'a -> 't option) = a |> Option.ofNullable 
                                                                   |> Option.bind f 
        static member Into(a: 'a Nullable, f: 'a -> 't) = a |> Option.ofNullable
                                                            |> Option.map f 
        static member Into(a: 'a when 'a:null, f: 'a -> 't option) = a |> Option.ofObj 
                                                                       |> Option.bind f
        static member Into(a: 'a when 'a:null, f: 'a -> 't) = a |> Option.ofObj
                                                                |> Option.map f

    let inline nullPipeHelper< ^t, ^a, ^b, ^c when (^t or ^a) : (static member Into : ^a * ^b -> ^c)> a b = 
                                                ((^t or ^a) : (static member Into : ^a * ^b -> ^c) (a, b))

    let inline (|?|>) a b = nullPipeHelper<NullPipe, _, _, _> a b