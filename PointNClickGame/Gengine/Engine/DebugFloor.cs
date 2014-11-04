using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Gengine
{
    class DebugFloor
    {
        private GraphicsDevice graphDev;
        private VertexPositionColor[] primitiveList;
        private short[] lineStripIndices;
        private int points = 8;
        public DebugFloor(GraphicsDevice graphicsdevice)
        {
            graphDev = graphicsdevice;
            primitiveList = new VertexPositionColor[points];

            for (int x = 0; x < points / 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    primitiveList[(x * 2) + y] = new VertexPositionColor(
                        new Vector3(x * 100, y * 100, 0), Color.White);
                }
            }
            // Initialize an array of indices of type short.
            lineStripIndices = new short[points];

            // Populate the array with references to indices in the vertex buffer.
            for (int i = 0; i < points; i++)
            {
                lineStripIndices[i] = (short)(i);
            }
        }

        public void Draw(){
            for (int i = 0; i < primitiveList.Length; i++)
                primitiveList[i].Color = Color.Red;
            
          /*  graphDev.DrawUserIndexedPrimitives<VertexPositionColor>(
                PrimitiveType.LineStrip,
                primitiveList,
                0,   // vertex buffer offset to add to each element of the index buffer
                8,   // number of vertices to draw
                lineStripIndices,
                0,   // first index element to read
                7    // number of primitives to draw
            );*/
            for (int i = 0; i < primitiveList.Length; i++)
                primitiveList[i].Color = Color.White;
        }
    }
}
