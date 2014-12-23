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
            RB tree = new RB();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(9);
            tree.Insert(-1);
            tree.Insert(11);
            Console.ReadLine();
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
        public class Node
        {
            public Color colour;
            public Node left;
            public Node right;
            public Node parent;
            public int data;

            public Node(int data) { this.data = data; }
            public Node(Color colour) { this.colour = colour; }
            public Node(int data, Color colour) { this.data = data; this.colour = colour; }
        }
        /// <summary>
        /// Root node of the tree (both reference & pointer)
        /// </summary>
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
            Node Y = X.right; // set Y
            X.right = Y.left;//turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            Y.parent = X.parent;//link X's parent to Y
            if (X.parent == null)
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
            Y.left = X; //put X on Y's left
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
            if (Y.parent == null)
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
            Node newItem = new Node(item);
            if (root == null)
            {
                root = newItem;
                root.colour = Color.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.data < X.data)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
                newItem.parent = Y;
                if (Y == null)
                {
                    root = newItem;
                }
                else if (newItem.data < Y.data)
                {
                    Y.left = newItem;
                }
                else
                {
                    Y.right = newItem;
                }
                newItem.left = null;
                newItem.right = null;
                newItem.colour = Color.Red;//colour the new node red
                InsertFixUp(newItem);//call method to check for violations and fix
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
        private void InsertFixUp(Node item)//problem lies here
        {
            //Checks Red-Black Tree properties
            while (item.parent.colour == Color.Red)
            {
                /*We have a violation*/
                if (item.parent == item.parent.parent.left)
                {
                    Node Y = item.parent.parent.right;
                    if (Y.colour == Color.Red)//Case 1
                    {
                        item.parent.colour = Color.Black;
                        Y.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        item = item.parent.parent;
                    }
                    else if (item == item.parent.right)//Case 2
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
                    Node X = null;

                    X = item.parent.parent.left;
                    if (X.colour == Color.Black)//Case 1
                    {
                         item.parent.colour = Color.Red;
                         X.colour = Color.Red;
                         item.parent.parent.colour = Color.Black;
                         item = item.parent.parent;
                    }
                    else if (item == item.parent.left)//Case 2
                    {
                         item = item.parent;
                         RightRotate(item);
                    }
                    //Case 3: recolour & rotate
                    item.parent.colour = Color.Red;
                    item.parent.parent.colour = Color.Black;
                    LeftRotate(item.parent.parent);
                    
                }
                root.colour = Color.Black;//re-colour the root black as necessary
            }
        }
        /// <summary>
        /// Deletes a specified value from the tree
        /// </summary>
        /// <param name="item"></param>
        public void Delete(Node item)
        {
            Node Y = item;
            Node X = null;
            Color original_y_colour = Y.colour;
            if (item.left == null)
            {
                X = item.right;
                RBTransplant(item, item.right);
            }
            else if (item.right == null)
            {
                X = item.left;
                RBTransplant(item, item.left);
            }
            else
            {
                Y = TreeMinimum(item.right);
                original_y_colour = Y.colour;
                X = Y.right;
                if (Y.parent == item)
                {
                    X.parent = Y;
                }
                else
                {
                    RBTransplant(Y, Y.right);
                    Y.right = item.right;
                    Y.right.parent = Y;
                }
                RBTransplant(item, Y);
                Y.left = item.left;
                Y.left.parent = Y;
                Y.colour = item.colour;
            }
            if (original_y_colour == Color.Black)
            {
                DeleteFixUp(X);
            }
        }
        private void RBTransplant(Node u, Node v)
        {
            if (u.parent == null)
            {
                root = v;
            }
            else if (u == u.parent.left)
            {
                u.parent.left = v;
            }
            else
            {
                u.parent.right = v;
            }
            v.parent = u.parent;
        }
        /// <summary>
        /// Checks the tree for any violations after deletion and performs a fix
        /// </summary>
        /// <param name="X"></param>
        private void DeleteFixUp(Node X)
        {
            Node W = null;
            while (X != root && X.colour == Color.Black)
            {
                if (X == X.parent.left)
                {
                    W = X.parent.right;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black; //case 1
                        X.parent.colour = Color.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.colour == Color.Black && W.right.colour == Color.Black)
                    {
                        W.colour = Color.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else if (W.right.colour == Color.Black)
                    {
                        W.left.colour = Color.Black; //case 3
                        W.colour = Color.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.parent.right; //case 3
                    }
                    W.colour = X.parent.colour; //case 4
                    X.parent.colour = Color.Black; //case 4
                    W.right.colour = Color.Black; //case 4
                    LeftRotate(X.parent); //case 4
                    X = root; //case 4
                }
                else //mirror code from above
                {
                    W = X.parent.left;
                    if (W.colour == Color.Black)
                    {
                        W.colour = Color.Red;
                        X.parent.colour = Color.Black;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.colour == Color.Red && W.left.colour == Color.Red)
                    {
                        W.colour = Color.Red;
                        X = X.parent;
                    }
                    else if (W.left.colour == Color.Red)
                    {
                        W.right.colour = Color.Red;
                        W.colour = Color.Black;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.colour = X.parent.colour;
                    X.parent.colour = Color.Black;
                    W.left.colour = Color.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            X.colour = Color.Black;
        }
        /// <summary>
        /// Returns the most minimum node in the tree
        /// </summary>
        /// <param name="X"></param>
        /// <returns>Node</returns>
        private Node TreeMinimum(Node X)
        {
            while (X.left != null)
            {
                X = X.left;
            }
            return X;
        }
    }
}
