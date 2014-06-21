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

type Quaternion = { W: single; X: single; Y: single; Z: single }
type Vector = { X: single; Y: single; Z: single }
    with static member Zero = { X = 0.0f; Y = 0.0f; Z = 0.0f }

type Keyframe = {
    Keyframe: int
    Rotation: Quaternion
    Translation: Vector
}

type Joint = {
    Name: string
    Keyframes: Keyframe list
}

type Animation = {
    FramesPerSecond: int
    FrameCount: int
    Joints: Joint list
}