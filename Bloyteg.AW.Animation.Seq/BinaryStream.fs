module BinaryStream

let readInt16 (stream : System.IO.Stream) : int = (stream.ReadByte() <<< 8) ||| (stream.ReadByte())

let readInt32 (stream : System.IO.Stream) : int = 
    (stream.ReadByte() <<< 24) ||| (stream.ReadByte() <<< 16) ||| (stream.ReadByte() <<< 8) ||| (stream.ReadByte())

let readFloat32 (stream : System.IO.Stream) : single =
    let bytes : byte array = Array.zeroCreate 4
    stream.Read(bytes, 0, bytes.Length) |> ignore
    System.BitConverter.ToSingle(Array.rev bytes, 0)

let readString (stream : System.IO.Stream) length = 
    let stringBytes : byte array = Array.zeroCreate length
    stream.Read(stringBytes, 0, length) |> ignore
    System.Text.ASCIIEncoding.ASCII.GetString(stringBytes).TrimEnd(char 0s)