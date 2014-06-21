module public Bloyteg.AW.Animation.Seq.Test.BinarySeqTests

open Bloyteg.AW.Animation.Seq
open FsUnit
open NUnit.Framework
open System.IO

let loadAnimationFromFile fileName =
    use file = File.OpenRead(fileName)
    Loader().LoadFromStream(file)

[<Test>]
let ``Binary sequences with header 0x7F7F7F7A can be loaded`` () =
    loadAnimationFromFile "maca.seq" |> should not' (be Null)

[<Test>]
let ``Binary sequences with header 0x7F7F7F79 can be loaded`` () =
    loadAnimationFromFile "maca.seq" |> should not' (be Null)

[<Test>]
let ``Walk.seq should have total frame count of 34`` () =
    let animation = loadAnimationFromFile "walk.seq"
    animation.FrameCount |> should equal 34

[<Test>]
let ``Maca.seq's first joint should be pelvis`` () =
    let animation = loadAnimationFromFile "maca.seq"
    animation.Joints.Head.Name |> should equal "pelvis"

[<Test>]
let ``Maca.seq's first joint has 88 keyframes`` () =
    let animation = loadAnimationFromFile "maca.seq"
    animation.Joints.Head.Keyframes |> should haveLength 88
