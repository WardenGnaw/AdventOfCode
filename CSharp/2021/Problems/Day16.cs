using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Utilities;

namespace AdventOfCode2021.Day16
{
    public abstract class Packet
    {
        public byte Version;

        public byte TypeId { get; }

        public Packet(byte version, byte typeId)
        {
            Version = version;
            TypeId = typeId;
        }

        public static Packet CreatePacket(BitStreamReader reader)
        {
            Packet packet = null;
            byte version = reader.ReadByte(3);
            byte packetId = reader.ReadByte(3);
            switch (packetId)
            {
                case 0:
                    packet = new SumPacket(version, packetId, reader);
                    break;
                case 1:
                    packet = new ProductPacket(version, packetId, reader);
                    break;
                case 2:
                    packet = new MinimumPacket(version, packetId, reader);
                    break;
                case 3:
                    packet = new MaximumPacket(version, packetId, reader);
                    break;
                case 4:
                    packet = new LiteralValuePacket(version, packetId, reader);
                    break;
                case 5:
                    packet = new GreaterThanPacket(version, packetId, reader);
                    break;
                case 6:
                    packet = new LessThanPacket(version, packetId, reader);
                    break;
                case 7:
                    packet = new EqualToPacket(version, packetId, reader);
                    break;
                default:
                    throw new InvalidOperationException("Not a valid packet type");
            }

            return packet;
        }

        public abstract ulong Value { get; }
    }

    public class LiteralValuePacket : Packet
    {
        public override ulong Value { get; }

        public LiteralValuePacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId)
        {
            Value = 0;

            while (reader.ReadBit())
            {
                Value = Value << 4;
                Value |= reader.ReadByte(4);
            }
            Value = Value << 4;
            Value |= reader.ReadByte(4);
        }
    }

    public class OperatorPacket : Packet
    {
        public override ulong Value { get; }

        public List<Packet> Packets;

        public OperatorPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId)
        {
            Value = 0;

            Packets = new List<Packet>();

            bool lengthTypeId = reader.ReadBit();
            if (lengthTypeId)
            {
                int numPackets = reader.ReadByte(8) << 8 | reader.ReadByte(3);

                for (int i = 0; i < numPackets; i++)
                {
                    Packets.Add(CreatePacket(reader));
                }
            }
            else
            {
                int numBits = reader.ReadByte(8) << 7;
                    numBits |= reader.ReadByte(7);
                List<byte> bytes = new List<byte>();
                int i;
                for (i = numBits; i >= 8; i -= 8)
                {
                    bytes.Add(reader.ReadByte(8));
                }
                if (i > 0)
                {
                    bytes.Add((byte)((byte)reader.ReadByte(i) << (8 - i)));
                }
                BitStreamReader innerReader = new BitStreamReader(bytes.ToArray(), (uint)numBits);
                while (!innerReader.EndOfStream)
                {
                    Packets.Add(Packet.CreatePacket(innerReader));
                }
            }
        }
    }

    public class SumPacket : OperatorPacket
    {
        public override ulong Value { 
            get
            {
                ulong sum = 0;
                foreach (Packet p in Packets)
                {
                    sum += p.Value;
                }
                return sum;
            }
        }

        public SumPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId, reader)
        {
        }
    }

    public class ProductPacket : OperatorPacket
    {
        public override ulong Value
        {
            get
            {
                return Packets.Aggregate((ulong)1, (x, y) => x * y.Value);
            }
        }

        public ProductPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId, reader)
        {
        }
    }

    public class MinimumPacket : OperatorPacket
    {
        public override ulong Value
        {
            get
            {
                return Packets.Min(x => x.Value);
            }
        }

        public MinimumPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId, reader)
        {
        }
    }

    public class MaximumPacket : OperatorPacket
    {
        public override ulong Value
        {
            get
            {
                return Packets.Max(x => x.Value);
            }
        }

        public MaximumPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId, reader)
        {
        }
    }

    public class GreaterThanPacket : OperatorPacket
    {
        public override ulong Value
        {
            get
            {
                return Packets[0].Value > Packets[1].Value ? (ulong)1 : (ulong)0;
            }
        }

        public GreaterThanPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId, reader)
        {
        }
    }

    public class LessThanPacket : OperatorPacket
    {
        public override ulong Value
        {
            get
            {
                return Packets[0].Value < Packets[1].Value ? (ulong)1 : (ulong)0;
            }
        }

        public LessThanPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId, reader)
        {
        }
    }

    public class EqualToPacket : OperatorPacket
    {
        public override ulong Value
        {
            get
            {
                return Packets[0].Value == Packets[1].Value ? (ulong)1 : (ulong)0;
            }
        }

        public EqualToPacket(byte version, byte typeId, BitStreamReader reader) : base(version, typeId, reader)
        {
        }
    }

    public class Main
    {
        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));
            List<byte> packet = new List<byte>();

            for (int i = 1; i < data.Length; i += 2)
            {
                string byteStr = string.Concat(data[i - 1], data[i]);
                packet.Add(byte.Parse(byteStr, NumberStyles.HexNumber));
            }

            List<Packet> packets = new List<Packet>();

            BitStreamReader reader = new BitStreamReader(packet.ToArray());

            ulong sumVersion = 0;
            Packet top = Packet.CreatePacket(reader);
            Queue<Packet> queue = new Queue<Packet>();
            queue.Enqueue(top);
            while (queue.Count > 0)
            {
                Packet p = queue.Dequeue();
                sumVersion += p.Version;
                if (p is OperatorPacket op)
                {
                    foreach (Packet inner in op.Packets)
                    {
                        queue.Enqueue(inner);
                    }
                }

            }

            Assert.Equal((ulong)0, sumVersion);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));
            List<byte> packet = new List<byte>();

            for (int i = 1; i < data.Length; i += 2)
            {
                string byteStr = string.Concat(data[i - 1], data[i]);
                packet.Add(byte.Parse(byteStr, NumberStyles.HexNumber));
            }

            List<Packet> packets = new List<Packet>();

            BitStreamReader reader = new BitStreamReader(packet.ToArray());

            ulong sumVersion = 0;
            Packet top = Packet.CreatePacket(reader);


            Assert.Equal((ulong)0, top.Value);
        }
    }
}