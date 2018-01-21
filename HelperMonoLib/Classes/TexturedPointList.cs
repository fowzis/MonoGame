using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shakkour.MonoGame.Helper
{
    public class VertexBufferTextured : IDisposable
    {
        IList<VertexPositionSizeColor> vertexList;
        int countSkipped = 0;

        Texture2D Texture { get; set; }

        public int Count
        {
            get { return vertexList.Count; }
        }

        public int CountSkipped
        {
            get
            {
                return countSkipped;
            }
        }

        public VertexBufferTextured()
        {
            // Instanciate the list of VertexPosition 
            vertexList = new List<VertexPositionSizeColor>();
        }

        public void Initialize(Texture2D texture)
        {
            Texture = texture;
        }

        // Add a textured point to the list
        public void Add(VertexPositionSizeColor vertex)
        {
            vertexList.Add(vertex);
        }

        // Instanciate and add a textured point to the list
        public void Add(int x, int y, int vertexSize, Color vertexColor)
        {
            VertexPositionSizeColor newVertex = new VertexPositionSizeColor(
                        new Vector3(
                            (float)(x - vertexSize / 2),
                            (float)(y - vertexSize / 2),
                            0.0F),
                        vertexSize,
                        vertexColor);

            // if exist a point in the same coordinate, skip adding it again
            if (!vertexList.Contains(newVertex))
            {
                vertexList.Add(newVertex);
            }
            else
            {
                countSkipped++;
            }
        }

        public void Remove(VertexPositionSizeColor vertex)
        {
            vertexList.Remove(vertex);
        }

        public void Clear()
        {
            vertexList.Clear();
        }

        public void Dispose()
        {
            Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Make sure spriteBatch.Begin(); has been called before

            foreach (var vertex in vertexList)
            {
                spriteBatch.Draw(
                    Texture,
                    new Rectangle(
                        (int)(vertex.Position.X),
                        (int)(vertex.Position.Y),
                        vertex.Size,
                        vertex.Size),
                    vertex.Color);
            }
            
            // spriteBatch.End();
        }
    }
}
