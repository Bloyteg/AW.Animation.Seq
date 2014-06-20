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

module internal SeqLoader =
    let loadBinarySeqFromStream stream: Animation = failwith "Not implemented."

    let loadTextSeqFromStream stream: Animation = failwith "Not implemented."

    let loadFromStream (stream: System.IO.Stream) = 
        match (stream.ReadByte(), stream.ReadByte(), stream.ReadByte(), stream.ReadByte()) with
        | (0x7F, 0x7F, 0x7F, 0x79) | (0x7F, 0x7F, 0x7F, 0x7A) -> loadBinarySeqFromStream(stream)
        | (_, _, _, _) -> loadTextSeqFromStream(stream)
        | _ -> failwith "File format not recognized."

type Loader() =
    member this.LoadFromStream(stream: System.IO.Stream) =
        SeqLoader.loadFromStream stream

