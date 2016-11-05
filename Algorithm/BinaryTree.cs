using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public enum ElementType {Data, LeftBracket, RightBracket};

    public struct TreeNode<T>
    {
        public T Value;
        public int Left, Right;
    }

    public class Element<T>
    {
        public ElementType Type;
        public T Value;
        public Element(ElementType t, T v)
        {
            Type = t;
            Value = v;
        }

        public bool IsNull
        {
            get { return Type == ElementType.Data && Value == null; }
        }
    }

    public class BinaryTree<T>
    {
        T Value { get; set; }
        BinaryTree<T> Left { get; set; }
        BinaryTree<T> Right { get; set; }

        public static BinaryTree<T> Construct(T[] values)
        {
            return Construct(values, 0);
        }

        private static BinaryTree<T> Construct(T[] values, int idx)
        {
	        if (idx >= values.Length || values[idx] == null) return null;
    	    BinaryTree<T> root = new BinaryTree<T>(values[idx]);
	        root.Left = Construct(values, 2*idx+1);
            root.Right = Construct(values, 2 * idx + 2);
	        return root;
        }

        public BinaryTree(T value) 
        {
            Value = value;
            Left = Right = null;
        }

        private static string _ToString(BinaryTree<T> root)
        {
            if (root == null) return "\\";
            StringBuilder sb = new StringBuilder();
            sb.Append(root.Value);
            if (root.Left == null && root.Right == null)
            {
                return sb.ToString();
            }
            sb.Append('(');
            sb.Append(_ToString(root.Left));
            sb.Append(',');
            sb.Append(_ToString(root.Right));
            sb.Append(')');
            return sb.ToString();
        }

        override public string ToString() 
        {
            return _ToString(this);
        }

        public static void WidthFirstTraverse(BinaryTree<T> root)
        {
            Queue<BinaryTree<T>> queue = new Queue<BinaryTree<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                BinaryTree<T> node = queue.Dequeue();
                if (node == null) continue;
                System.Console.Write(node.Value + " ");
                queue.Enqueue(node.Left);
                queue.Enqueue(node.Right);
            }
        }

        public void WidthFirstTraverse()
        {
            WidthFirstTraverse(this);
        }

        public int Height
        {
            get
            {
                return _height(this);
            }
        }

        private static int _height(BinaryTree<T> root)
        {
            if (root == null) return 0;
            return Math.Max(_height(root.Left), _height(root.Right)) + 1;
        }

        public static BinaryTree<T> Construct(List<Element<T>> input)
        {
            return Construct(input, 0);
        }

        private static BinaryTree<T> Construct(List<Element<T>> list, int idx)
        {
            if (idx >= list.Count || list[idx].IsNull) return null;
            
            if (list[idx].Type != ElementType.Data) 
                throw new Exception(String.Format("Invalid input: data wanted at position {0}", idx));
            BinaryTree<T> node = new BinaryTree<T>(list[idx].Value);
            if (idx + 1 >= list.Count) return node;

            switch (list[idx + 1].Type)
            {
                case ElementType.Data:
                case ElementType.RightBracket:
                    break;
                case ElementType.LeftBracket:
                    // syntax check for no input after (
                    node.Left = Construct(list, idx + 2);
                    int idx2 = LocateRightIdx(list, idx + 2);
                    node.Right = Construct(list, idx2);
                    break;
            }
            return node;
        }

        private static int LocateRightIdx(List<Element<T>> list, int idx)
        {
            int count = 0;
            int i;
            for (i = idx + 1; i < list.Count; i++)
            {
                switch (list[i].Type)
                {
                    case ElementType.LeftBracket:
                        count++;
                        break;
                    case ElementType.RightBracket:
                        count--;
                        if (count < 0) throw new Exception(String.Format("Invalid input: braket mismatch at position {0}", i));
                        break;
                    case ElementType.Data:
                        if (count == 0) return i;
                        break;
                }
            }
            throw new Exception(String.Format("Invalid input: braket mismatch at position {0}", i));
        }

        public BinaryTree<T> Copy()
        {
            BinaryTree<T> root = new BinaryTree<T>(Value);
            if (Left != null)
                root.Left = Left.Copy();
            if (Right != null)
                root.Right = Right.Copy();
            return root;
        }

        static public BinaryTree<T> FromList(List<TreeNode<T>> list)
        {
            return FromList(list, list.Count - 1);
        }

        static private BinaryTree<T> FromList(List<TreeNode<T>> list, int index)
        {
            if (index < 0) return null;
            BinaryTree<T> node = new BinaryTree<T>(list[index].Value);
            node.Left = FromList(list, list[index].Left);
            node.Right = FromList(list, list[index].Right);
            return node;
        }

        public List<TreeNode<T>> ToList()
        {
            var list = new List<TreeNode<T>>();
            ToList(this, list);
            return list;
        }

        private static int ToList(BinaryTree<T> root, List<TreeNode<T>> list)
        {
            if (root == null) return -1;
            int leftIdx, rightIdx;
            leftIdx = ToList(root.Left, list);
            rightIdx = ToList(root.Right,list);
            TreeNode<T> node = new TreeNode<T>();
            node.Value = root.Value;
            node.Left = leftIdx;
            node.Right = rightIdx;
            list.Add(node);
            return list.Count - 1;
        }

        /*public override bool Equals(BinaryTree<T> root)
        {
            return Equals(this, root);
        }

        private bool Equals(BinaryTree<T> root1, BinaryTree<T> root2)
        {
            if (root1 == null)
            {
                return root2 == null;
            }
            else
            {
                return (root2 != null) && (root1.Value == root2.Value);
            }
        }*/
        public static BinaryTree<T> Construct(T[] inOrder, T[] postOrder)
        {
            return construct(inOrder, 0, inOrder.Length - 1, postOrder, 0, postOrder.Length - 1);
        }

        private static BinaryTree<T> construct(T[] inOrder, int Sin, int Ein, T[] postOrder, int Spost, int Epost)
        {
            if (Sin > Ein) return null;
            BinaryTree<T> node = new BinaryTree<T>(postOrder[Epost]);
            int idx = Array.FindIndex<T>(inOrder, e => e.Equals(node.Value));
            int LSin = Sin, LEin = idx - 1, RSin = idx + 1, REin = Ein;
            int LSpost = Spost, LEpost = Spost + LEin - LSin, REpost = Epost - 1, RSpost = REpost - REin + RSin;
            node.Left = construct(inOrder, LSin, LEin, postOrder, LSpost, LEpost);
            node.Right = construct(inOrder, RSin, REin, postOrder, RSpost, REpost);
            return node;
        }
    }

}
