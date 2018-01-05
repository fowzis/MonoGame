using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;

namespace HelperMonoLib
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class VertexPositionSizeColor : IVertexType
    {
        public Vector3 Position;
        public int Size;
        public Color Color;

        public static readonly VertexDeclaration VertexDeclaration;

        public VertexPositionSizeColor(Vector3 position, int size, Color color)
        {
            this.Position = position;
            this.Size = size;
            this.Color = color;
        }

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get
            {
                return VertexDeclaration;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Size.GetHashCode();
                hashCode = (hashCode * 397) ^ Color.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return "{{Position:" + this.Position + " Size: " + this.Size + " Color:" + this.Color + "}}";
        }

        public static bool operator ==(VertexPositionSizeColor left, VertexPositionSizeColor right)
        {
            return ((left.Color == right.Color) && (left.Size == right.Size) && (left.Position == right.Position));
        }

        public static bool operator !=(VertexPositionSizeColor left, VertexPositionSizeColor right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != base.GetType())
            {
                return false;
            }
            return (this == ((VertexPositionSizeColor)obj));
        }

        static VertexPositionSizeColor()
        {
            VertexElement[] elements = new VertexElement[] {
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(12, VertexElementFormat.Single, VertexElementUsage.PointSize, 0),
                new VertexElement(0x10, VertexElementFormat.Color, VertexElementUsage.Color, 0)
            };
            VertexDeclaration declaration = new VertexDeclaration(elements);
            VertexDeclaration = declaration;
        }
    }
}
