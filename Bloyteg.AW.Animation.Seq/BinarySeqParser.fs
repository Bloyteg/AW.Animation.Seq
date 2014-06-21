module BinarySeqParser

open Bloyteg.AW.Animation.Seq
open BinaryStream

let loadKeyframes keyframeCount stream = 
    seq { 
        for currentKeyframe = 0 to keyframeCount - 1 do
            yield { Keyframe = (readInt32 stream) - 1
                    Rotation = 
                        { W = readFloat32 stream
                          X = readFloat32 stream
                          Y = readFloat32 stream
                          Z = readFloat32 stream }
                    Translation = Vector.Zero }
    }

let loadJoints jointCount stream = 
    seq { 
        for currentJoint = 0 to jointCount - 1 do
            let jointName = readInt16 stream |> readString stream
            let dataLength = readInt32 stream
            let keyframeCount = readInt32 stream
            yield { Name = jointName
                    Keyframes = (loadKeyframes keyframeCount stream) |> List.ofSeq }
    }

let loadBinarySeqFromStream stream : Animation = 
    let frameCount = readInt16 stream
    let jointCount = readInt32 stream
    do readInt16 stream |> readString stream |> ignore
    do readInt16 stream |> readString stream |> ignore
    { FramesPerSecond = 30
      FrameCount = frameCount
      Joints = (loadJoints jointCount stream) |> List.ofSeq }