using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeCollesionDetection
{
    /// <summary>
    /// Quad Tree Datastructure
    /// </summary>
    class QuadTree
    {
        /// <summary>
        /// Max leaf node capacity before splitting to quad rectangles
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// The external boundaries of the Quad Tree
        /// Usually screen current resolution
        /// </summary>
        public Rectangle Boundaries { get; set; }

        /// <summary>
        /// Quad Tree Root Node
        /// </summary>
        public QTNode QTRoot { get; private set; }

        /// <summary>
        /// Define and initialize a new Quad Tree
        /// </summary>
        /// <param name="boundaries"></param>
        /// <param name="capacity"></param>
        public QuadTree(Rectangle boundaries, int capacity)
        {
            this.Boundaries = boundaries;
            this.Capacity = capacity;

            // Instanciate the Quad Tree root node
            QTRoot = new QTNode(boundaries, capacity);
        }

        public bool Insert(Point point)
        {
            // Provided point is outside Tree external boundaries
            if (!Boundaries.Contains(point))
                return false;

            return Insert(QTRoot, point);
        }

        public bool Insert(QTNode node, Point point)
        {
            QTNode tmpNode;

            // Traverse the tree to find the smallest leaf node where the point is within the nodes boudaries and can be inserted
            tmpNode = FindLeafNode(node, point);

            return tmpNode.Add(point);
        }

        /// <summary>
        /// Find the node to which to insert the new point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public QTNode FindLeafNode(QTNode node, Point point)
        {
            QTNode tmpNode = node;

            while (!tmpNode.IsLeaf)
            {
                if (tmpNode.FindChildNode(point, out tmpNode) ==  false)
                {
                    // No child found to insert the point
                    // Should'nt reach this point ever.
                    return null;
                }
            }

            return tmpNode;
        }

        /// <summary>
        /// Split the node and insert the points to the new quadrants
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool Split(QTNode node)
        {
            if (!node.IsMaxCapacity)
                return false;

            Rectangle rec = node.Boundaries;
            int halfWidth = rec.Width / 2;
            int halfHeight = rec.Height / 2;

            node.TopLeft = new QTNode(new Rectangle(rec.Top, rec.Left, halfWidth, halfHeight), Capacity);
            node.TopRight = new QTNode(new Rectangle(rec.Top, rec.Left + halfWidth + 1, halfWidth, halfHeight), Capacity);
            node.BottomLeft = new QTNode(new Rectangle(rec.Top + halfHeight + 1, rec.Left, halfWidth, halfHeight), Capacity);
            node.BottomRight = new QTNode(new Rectangle(rec.Top + halfHeight + 1, rec.Left + halfWidth + 1, halfWidth, halfHeight), Capacity);

            return true;
        }

    }
}
