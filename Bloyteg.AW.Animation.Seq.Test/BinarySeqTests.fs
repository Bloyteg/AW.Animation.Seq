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

[<Test>]
let ``Maca.seq's first joint's first keyframe should be 0`` () =
    let animation = loadAnimationFromFile "maca.seq"
    let (firstKeyframe :: _) = animation.Joints.Head.Keyframes
    firstKeyframe.Keyframe |> should equal 0

[<Test>]
let ``Maca.seq's first joint's first keyframe should have rotation W = 1.0, X = 0, Y = 0, Z = 0`` () =
    let animation = loadAnimationFromFile "maca.seq"
    let (firstKeyframe :: _) = animation.Joints.Head.Keyframes
    firstKeyframe.Rotation |> should equal { W = 1.0f; X = 0.0f; Y = 0.0f; Z = 0.0f }

[<Test>]
let ``Maca.seq's first joint's first keyframe should be 2`` () =
    let animation = loadAnimationFromFile "maca.seq"
    let (_ :: secondKeyFrame :: _) = animation.Joints.Head.Keyframes
    secondKeyFrame.Keyframe |> should equal 2


[<Test>]
let ``Maca.seq's first joint's second keyframe should have rotation W = 0.99932152, X = 0, Y = 0.03683079, Z = 0`` () =
    let animation = loadAnimationFromFile "maca.seq"
    let (_ :: secondKeyFrame :: _) = animation.Joints.Head.Keyframes
    secondKeyFrame.Rotation.W |> should (equalWithin 0.00000001) 0.99932152;
    secondKeyFrame.Rotation.X |> should equal 0.0f
    secondKeyFrame.Rotation.Y |> should (equalWithin 0.00000001) 0.03683079f
    secondKeyFrame.Rotation.Z |> should equal 0.0f
