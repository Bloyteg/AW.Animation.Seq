module Bloyteg.AW.Animation.Seq.Test.BinarySeqTests
open Bloyteg.AW.Animation.Seq
open Xunit
open FsUnit.Xunit

[<Fact>]
let ``Binary sequences with header 0x7F7F7F7A can be loaded`` =
    let loader = Loader()
    loader.LoadFromStream(null)

[<Fact>]
let ``Binary sequences with header 0x7F7F7F79 can be loaded`` =
    Assert.True(false)