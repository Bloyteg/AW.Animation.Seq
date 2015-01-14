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

module BinaryStream

let readInt16 (stream : System.IO.Stream) : int = (stream.ReadByte() <<< 8) ||| (stream.ReadByte())

let readInt32 (stream : System.IO.Stream) : int = 
    (stream.ReadByte() <<< 24) ||| (stream.ReadByte() <<< 16) ||| (stream.ReadByte() <<< 8) ||| (stream.ReadByte())

let tryReadInt32 (stream : System.IO.Stream) : int option =
    let firstByte = stream.ReadByte()
    let secondByte = stream.ReadByte()
    let thirdByte = stream.ReadByte()
    let fourthByte = stream.ReadByte()

    if firstByte = -1 || secondByte = -1 || thirdByte = -1 || fourthByte = -1 then
        None
    else
        Some((firstByte <<< 24) ||| (secondByte <<< 16) ||| (thirdByte <<< 8) ||| fourthByte)

let readFloat32 (stream : System.IO.Stream) : single =
    let bytes : byte array = Array.zeroCreate 4
    stream.Read(bytes, 0, bytes.Length) |> ignore
    System.BitConverter.ToSingle(Array.rev bytes, 0)

let readString (stream : System.IO.Stream) length = 
    let stringBytes : byte array = Array.zeroCreate length
    stream.Read(stringBytes, 0, length) |> ignore
    System.Text.ASCIIEncoding.ASCII.GetString(stringBytes).TrimEnd(char 0s)