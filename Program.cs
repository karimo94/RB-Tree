using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace newrbtree
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    enum Color
    {
        Red,
        Black
    }
    class RB
    {
        /// <summary>
        /// New object of type Node
        /// </summary>
        class Node
        {
            public Color colour;
            public Node left;
            public Node right;
            public Node parent;
            public int data;
            public Node(int data)
            { this.data = data; }
            public Node(Color colour) { this.colour = colour; }
        }
        private Node root;
        /// <summary>
        /// New instance of a Red-Black tree object
        /// </summary>
        public RB() { }
        /// <summary>
        /// Left Rotate
        /// </summary>
        /// <param name="X"></param>
        /// <returns>void</returns>
        private void LeftRotate(Node X)
        {
            Node Y = X.right;
            X.right = Y.left;
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            Y.parent = X.parent;
            if (X.parent != null)
            {
                root = Y;
            }
            else if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X;
            X.parent = Y;
            
        }
        /// <summary>
        /// Rotate Right
        /// </summary>
        /// <param name="Y"></param>
        /// <returns>void</returns>
        private void RightRotate(Node Y)
        {
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            X.parent = Y.parent;
            if (Y.parent != null)
            {
                root = X;
            }
            else if (Y == Y.parent.right)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.right = X;
            }
            X.right = Y;
            Y.parent = X;
            
        }
        /// <summary>
        /// Display Tree
        /// </summary>
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
                return;
            }
            if (root != null)
            {
                InOrderDisplay(root);
            }
        }
        /// <summary>
        /// Find item in the tree
        /// </summary>
        /// <param name="key"></param>
        public void Find(int key)
        {
            bool isFound = false;
            Node temp = root;
            while (isFound)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.data)
                {
                    temp = temp.left;
                }
                if (key > temp.data)
                {
                    temp = temp.right;
                }
                if (key == temp.data)
                {
                    isFound = true;
                }
            }
            if (isFound == true)
            {
                Console.WriteLine("{0} was found", key);
            }
            else
            {
                Console.WriteLine("{0} not found", key);
            }
        }
        /// <summary>
        /// Insert a new object into the RB Tree
        /// </summary>
        /// <param name="item"></param>
        public void Insert(int item)
        {
            Node n = new Node(item);
            InsertFixUp(n);
        }
        private void InOrderDisplay(Node current)
        {
            if (current != null)
            {
                InOrderDisplay(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplay(current.right);
            }
        }
        private void InsertFixUp(Node item)
        {
            Node Y = null;
            while (item.parent.colour == Color.Red)
            {
                if (item.parent == item.parent.parent.left)
                {
                    Y = item.parent.parent.right;
                    if (Y.colour == Color.Red)//Case 1
                    {
                        item.parent.colour = Color.Black;
                        Y.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        item = item.parent.parent;
                    }
                    else if (item == item.parent.right)//Case 2:
                    {
                        item = item.parent;
                        LeftRotate(item);
                    }
                    //Case 3: recolour & rotate
                    item.parent.colour = Color.Black;
                    item.parent.parent.colour = Color.Red;
                    RightRotate(item.parent.parent);
                }
                else
                {
                    //mirror image of code above
                    
                }
            }
        }
        private void Delete(Node item)
        {

        }
    }
}
