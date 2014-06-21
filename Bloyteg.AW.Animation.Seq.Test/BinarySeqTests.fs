module public Bloyteg.AW.Animation.Seq.Test.BinarySeqTests

open Bloyteg.AW.Animation.Seq
open FsUnit.Xunit
open Xunit
open System.IO

[<Fact>]
let ``Binary sequences with header 0x7F7F7F7A can be loaded`` () =
    (Loader().LoadFromStream(File.OpenRead("maca.seq")) |> ignore) |> should not' (throw typeof<System.Exception>)

[<Fact>]
let ``Binary sequences with header 0x7F7F7F79 can be loaded`` () =
    (Loader().LoadFromStream(File.OpenRead("walk.seq")) |> ignore) |> should not' (throw typeof<System.Exception>)