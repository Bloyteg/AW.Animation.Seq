module public Bloyteg.AW.Animation.Seq.Test.BinarySeqTests

open Bloyteg.AW.Animation.Seq
open FsUnit.Xunit
open Xunit
open System.IO

[<Fact>]
let ``Binary sequences with header 0x7F7F7F7A can be loaded`` () =
    use file = File.OpenRead("maca.seq")
    (Loader().LoadFromStream(file) |> ignore) |> should not' (throw typeof<System.Exception>)

[<Fact>]
let ``Binary sequences with header 0x7F7F7F79 can be loaded`` () =
    use file = File.OpenRead("walk.seq")
    (Loader().LoadFromStream(file) |> ignore) |> should not' (throw typeof<System.Exception>)


[<Fact>]
let ``Walk.seq should have total frame count of 34`` () =
    use file = File.OpenRead("walk.seq")
    let animation = Loader().LoadFromStream(file) 
    animation.FrameCount |> should equal 34
