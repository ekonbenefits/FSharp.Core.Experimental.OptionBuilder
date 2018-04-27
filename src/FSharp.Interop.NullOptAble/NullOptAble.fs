namespace FSharp.Interop.NullOptAble
open System

type NullOptAble =
        (* DefaultWith overloads *)
        static member DefaultWith(a: 'a option, b: 'a Lazy) =
            a |> Option.defaultWith b.Force
        static member DefaultWith(a: 'a Nullable, b: 'a Lazy) = 
            a |> Option.ofNullable 
              |> Option.defaultWith b.Force
        static member DefaultWith(a: 'a when 'a:null, b: 'a Lazy) =
            a |> Option.ofObj 
              |> Option.defaultWith b.Force

        (* Map overloads *)
        static member Map(a: 'a option, f: 'a -> 't) =
            option { 
                let! a' = a 
                return f a'
            }
        static member Map(a: 'a Nullable, f: 'a -> 't) = 
            option { 
                let! a' = a 
                return f a'
            }
        static member Map(a: 'a when 'a:null, f: 'a -> 't) =
            option { 
                let! a' = a 
                return f a'
            }

        (* Map 2 overloads *)
        static member Map2((a: 'a option, b: 'b option), f: 'a -> 'b -> 't) =
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a option, b: 'b Nullable), f: 'a -> 'b -> 't) = 
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a Nullable, b: 'b option), f: 'a -> 'b -> 't) = 
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a option, b: 'b when 'b:null), f: 'a -> 'b -> 't) =
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a, b: 'b option when 'a:null), f: 'a -> 'b -> 't) =
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a Nullable, b: 'b Nullable), f: 'a -> 'b -> 't) = 
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a Nullable, b: 'b when 'b:null), f: 'a -> 'b -> 't) =
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a, b: 'b Nullable when 'a:null), f: 'a -> 'b -> 't) =
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }
        static member Map2((a: 'a, b: 'b when 'a:null and 'b:null), f: 'a -> 'b -> 't) =
            option { 
                let! a' = a 
                let! b' = b
                return f a' b'
            }

        (* Bind overloads *)
        static member Bind(a: 'a option, f: 'a -> 't option) = 
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a option, f: 'a -> 't Nullable) = 
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a option, f: 'a -> 't when 't:null) = 
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a Nullable, f: 'a -> 't option) =
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a Nullable, f: 'a -> 't Nullable) =
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a Nullable, f: 'a -> 't when 't:null) =
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a when 'a:null, f: 'a -> 't option) =
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a when 'a:null, f: 'a -> 't Nullable) =
            option {
                let! a' = a
                return! f a'
            }
        static member Bind(a: 'a when 'a:null, f: 'a -> 't when 't:null) =
            option {
                let! a' = a
                return! f a'
            }
        (* Bind2 overloads *)
        static member Bind2((a: 'a option , b: 'b option), f: 'a -> 'b -> 't option) = 
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option , b: 'b option), f: 'a -> 'b -> 't Nullable) = 
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option , b: 'b option), f: 'a -> 'b -> 't when 't:null) = 
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option , b: 'b Nullable), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option , b: 'b Nullable), f: 'a -> 'b ->  't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option , b: 'b Nullable), f: 'a -> 'b ->  't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b option), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b option), f: 'a -> 'b ->  't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b option), f: 'a -> 'b ->  't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option , b: 'b when 'b:null), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option  , b: 'b when 'b:null), f: 'a -> 'b -> 't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a option  , b: 'b when 'b:null), f: 'a -> 'b -> 't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b option when 'a:null), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b option when 'a:null), f: 'a -> 'b -> 't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b option when 'a:null), f: 'a -> 'b -> 't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b Nullable), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b Nullable), f: 'a -> 'b ->  't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b Nullable), f: 'a -> 'b ->  't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b when 'b:null), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b when 'b:null), f: 'a -> 'b -> 't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a Nullable , b: 'b when 'b:null), f: 'a -> 'b -> 't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b Nullable when 'a:null), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b Nullable when 'a:null), f: 'a -> 'b -> 't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b Nullable when 'a:null), f: 'a -> 'b -> 't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b when 'a:null and 'b:null), f: 'a -> 'b -> 't option) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b when 'a:null and 'b:null), f: 'a -> 'b -> 't Nullable) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            }
        static member Bind2((a: 'a , b: 'b when 'a:null and 'b:null), f: 'a -> 'b -> 't when 't:null) =
            option { 
                let! a' = a 
                let! b' = b
                return! f a' b'
            } 