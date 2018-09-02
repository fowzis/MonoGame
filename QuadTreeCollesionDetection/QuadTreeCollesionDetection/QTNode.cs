using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeCollesionDetection
{
    /// <summary>
    /// Quad Tree Node
    /// </summary>
    class QTNode
    {
        /// <summary>
        /// The rectangle represented by the node
        /// </summary>
        public Rectangle Boundaries { get; private set; }

        /// <summary>
        /// Max node capacity before splitting to quad rectangles
        /// </summary>
        public int Count { get { return Points.Count; } }

        /// <summary>
        /// Return the Node Capacity
        /// </summary>
        public int Capacity { get { return Points.Capacity; } }

        /// <summary>
        /// Indicate of the node is a leaf i.e. node having no children nodes.
        /// </summary>
        public bool IsMaxCapacity { get { return (Points.Count == Points.Capacity); } }

        /// <summary>
        /// Indicate if the node Is Leaf node, having no children nodes.
        /// </summary>
        public bool IsLeaf { get; private set; }

        public QTNode TopLeft { get; set; }
        public QTNode TopRight { get; set; }
        public QTNode BottomLeft { get; set; }
        public QTNode BottomRight { get; set; }

        private List<Point> Points;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capacity">Max point allowed in each leaf node, before splitting</param>
        public QTNode(Rectangle boundaries, int capacity)
        {
            Boundaries = boundaries;    // Set Node boudaries rectangle
            Points.Capacity = capacity; // Create a new Points array
            IsLeaf = true;              // Node is created as a leaf node
        }

        public bool Add(Point point)
        {
            QTNode tmpNode = this;

            // If node is at Max Capacity, then split the node to new four quadrant ad add the point to the right one.
            if (this.IsMaxCapacity)
            {
                this.Split();
                if (!FindChildNode(point, out tmpNode))
                    return false;
            }

            // Add the new point to the List of Points
            tmpNode.Points.Add(point);

            return true;
        }

        public bool Split()
        {
            // Don't split if max capacity not reached.
            if (!IsMaxCapacity)
                return false;

            Rectangle rec = this.Boundaries;
            int halfWidth = rec.Width / 2;
            int halfHeight = rec.Height / 2;

            TopLeft = new QTNode(new Rectangle(rec.Top, rec.Left, halfWidth, halfHeight), Capacity);
            TopRight = new QTNode(new Rectangle(rec.Top, rec.Left + halfWidth + 1, halfWidth, halfHeight), Capacity);
            BottomLeft = new QTNode(new Rectangle(rec.Top + halfHeight + 1, rec.Left, halfWidth, halfHeight-1), Capacity);
            BottomRight = new QTNode(new Rectangle(rec.Top + halfHeight + 1, rec.Left + halfWidth + 1, halfWidth-1, halfHeight-1), Capacity);

            foreach (var point in Points)
            {
                // Distribute the points on the corresponding quadrant
                if (FindChildNode(point, out QTNode tmpNode))
                    tmpNode.Add(point);
            }

            this.Points.Clear();
            //this.Points.TrimExcess(); // for better memory management
            this.IsLeaf = false;

            return true;
        }

        public bool FindChildNode(Point point, out QTNode node)
        {
            if (TopLeft.Boundaries.Contains(point))
                node = TopLeft;
            else if (TopRight.Boundaries.Contains(point))
                node = TopRight;
            else if (BottomLeft.Boundaries.Contains(point))
                node = BottomLeft;
            else if (BottomRight.Boundaries.Contains(point))
                node = BottomRight;
            else
            {
                node = null;
                return false;
            }

            return true;
        }
    }
}
