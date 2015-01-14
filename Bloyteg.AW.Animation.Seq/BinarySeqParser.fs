// Copyright 2014 Joshua R. Rodgers
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

let loadPositionBlock stream = 
    (readInt32 stream) |> ignore
    let positionCount = (readInt32 stream) - 1
    seq { 
        for currentPosition = 0 to (positionCount) do
            yield { Keyframe = readInt32 stream
                    Value = (readFloat32 stream) / 10.0f }
    } |> List.ofSeq

let loadGlobalPositions (stream: System.IO.Stream) animation =
    match (tryReadInt32 stream) with
    | Some(totalBlocks) ->
        let rec loadDataBlocks currentBlock animation =
            if currentBlock < totalBlocks then
                match currentBlock with
                | 0 -> { animation with GlobalXPositions = loadPositionBlock stream } |> loadDataBlocks (currentBlock + 1)
                | 1 -> { animation with GlobalYPositions = loadPositionBlock stream } |> loadDataBlocks (currentBlock + 1)
                | 2 -> { animation with GlobalZPositions = loadPositionBlock stream } |> loadDataBlocks (currentBlock + 1)
                | _ -> animation
            else
                animation

        loadDataBlocks 0 animation
    | None -> animation

let loadBinarySeqFromStream stream : Animation = 
    let frameCount = readInt16 stream
    let jointCount = readInt32 stream
    do readInt16 stream |> readString stream |> ignore
    do readInt16 stream |> readString stream |> ignore
    { FramesPerSecond = 30
      FrameCount = frameCount
      Joints = (loadJoints jointCount stream) |> List.ofSeq
      GlobalXPositions = []
      GlobalYPositions = []
      GlobalZPositions = [] } |> (loadGlobalPositions stream)