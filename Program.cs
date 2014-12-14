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
        public RB() { }
        /// <summary>
        /// Left Rotate
        /// </summary>
        private Node LeftRotate(Node X)
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
            return Y;
        }
        /// <summary>
        /// Rotate Right
        /// </summary>
        /// <param name="Y"></param>
        /// <returns>Node</returns>
        private Node RightRotate(Node Y)
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
                //YOU ARE HERE
            }
            
        }
        /// <summary>
        /// DisplayTree and Find
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
        private void InOrderDisplay(Node current)
        {
            if (current != null)
            {
                InOrderDisplay(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplay(current.right);
            }
        }
    }
}
