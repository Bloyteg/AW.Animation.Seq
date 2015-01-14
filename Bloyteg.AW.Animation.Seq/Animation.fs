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
namespace Bloyteg.AW.Animation.Seq

open System.Runtime.Serialization

[<CLIMutable>]
[<DataContract(Namespace="")>]
type Quaternion = 
    { [<DataMember(Name="W")>] W : single
      [<DataMember(Name="X")>] X : single
      [<DataMember(Name="Y")>] Y : single
      [<DataMember(Name="Z")>] Z : single }

[<CLIMutable>]
[<DataContract(Namespace="")>]
type Vector = 
    { [<DataMember(Name="X")>] X : single
      [<DataMember(Name="Y")>] Y : single
      [<DataMember(Name="Z")>] Z : single }
    static member Zero = 
        { X = 0.0f
          Y = 0.0f
          Z = 0.0f }

[<CLIMutable>]
[<DataContract(Namespace="")>]
type Keyframe = 
    { [<DataMember(Name="Keyframe")>] Keyframe : int
      [<DataMember(Name="Rotation")>] Rotation : Quaternion
      [<DataMember(Name="Translation")>] Translation : Vector }

[<CLIMutable>]
[<DataContract(Namespace="")>]
type GlobalPositionKeyframe =
    { [<DataMember(Name="Keyframe")>] Keyframe : int
      [<DataMember(Name="Value")>] Value : single }

[<CLIMutable>]
[<DataContract(Namespace="")>]
type Joint = 
    { [<DataMember(Name="Name")>] Name : string
      [<DataMember(Name="Keyframes")>] Keyframes : Keyframe list }

[<CLIMutable>]
[<DataContract(Namespace="")>]
type Animation = 
    { [<DataMember(Name="FramesPerSecond")>] FramesPerSecond : int
      [<DataMember(Name="FrameCount")>] FrameCount : int
      [<DataMember(Name="Joints")>] Joints : Joint list 
      [<DataMember(Name="GlobalXPositions")>] GlobalXPositions: GlobalPositionKeyframe list
      [<DataMember(Name="GlobalYPositions")>] GlobalYPositions: GlobalPositionKeyframe list
      [<DataMember(Name="GlobalZPositions")>] GlobalZPositions: GlobalPositionKeyframe list }