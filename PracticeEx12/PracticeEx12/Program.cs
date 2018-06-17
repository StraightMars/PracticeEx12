using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeEx12
{
    public class Node
    {
        public int number;
        public Node rightLeaf;
        public Node leftLeaf;
        public Node(int value)
        {
            number = value;
            rightLeaf = null;
            leftLeaf = null;
        }
        public bool isLeaf(ref Node node)
        {
            return (node.rightLeaf == null && node.leftLeaf == null);
        }
        public void insertData(ref Node node, int data)
        {
            if (node == null)
            {
                node = new Node(data);
            }
            else if (node.number < data)
            {
                insertData(ref node.rightLeaf, data);
            }
            else if (node.number > data)
            {
                insertData(ref node.leftLeaf, data);
            }
        }
        public bool search(Node node, int s)
        {
            if (node == null)
                return false;
            if (node.number == s)
            {
                return true;
            }
            else if (node.number < s)
            {
                return search(node.rightLeaf, s);
            }
            else if (node.number > s)
            {
                return search(node.leftLeaf, s);
            }
            return false;
        }
        public void display(Node n)
        {
            if (n == null)
                return;
            display(n.leftLeaf);
            Console.Write(n.number + " ");
            display(n.rightLeaf);
        }
    }
    public class BinaryTree
    {
        private Node root;
        public int count;
        public BinaryTree()
        {
            root = null;
            count = 0;
        }
        public bool isEmpty()
        {
            return root == null;
        }
        public void insert(int d)
        {
            if (isEmpty())
            {
                root = new Node(d);
            }
            else
            {
                root.insertData(ref root, d);
            }
            count++;
        }
        public bool search(int s)
        {
            return root.search(root, s);
        }
        public bool isLeaf()
        {
            if (!isEmpty())
                return root.isLeaf(ref root);

            return true;
        }
        public void display()
        {
            if (!isEmpty())
                root.display(root);
        }
        public int Count()
        {
            return count;
        }
    }
    class Program
    {
        static void ShakeSort(int[] arr, out int peresAmount, out int compareAmount)
        {
            int b = 0;
            peresAmount = 0;
            compareAmount = 0;
            int left = 0;//Левая граница
            int right = arr.Length - 1;//Правая граница
            while (left <= right)
            {
                for (int i = left; i < right; i++)//Слева направо...
                {
                    compareAmount++;
                    if (arr[i] > arr[i + 1])
                    {
                        b = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = b;
                        peresAmount++;
                    }
                }
                right--;//Сохраним последнюю перестановку как границу
                for (int i = right; i > left; i--)//Справа налево...
                {
                    compareAmount++;
                    if (arr[i - 1] > arr[i])
                    {
                        b = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = b;
                        peresAmount++;
                    }
                }
                left++;//Сохраним последнюю перестановку как границу
            }
        }
        static int[] CreateIncArr(int size, Random rnd)
        {
            int[] arr = new int[size];
            arr[0] = rnd.Next(-100, 101);
            for (int i = 1; i < size; i++)
            {
                arr[i] = rnd.Next(arr[i - 1] + 1, 100 + Math.Abs(arr[i - 1]) + 1);
            }
            return arr;
        }
        static int[] CreateDecArr(int size, Random rnd)
        {
            int[] arr = new int[size];
            arr[0] = rnd.Next(-100, 101);
            for (int i = 1; i < size; i++)
            {
                arr[i] = rnd.Next(-100 - Math.Abs(arr[i - 1]), arr[i - 1]);
            }
            return arr;
        }
        static int[] CreateDisordedArr(int size, Random rnd)
        {
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(-1000, 1001);
            }
            return arr;
        }
        static void ShowArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write("{0} ", arr[i]);
            Console.Write("\n");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте! Введите размер массивов (для чистоты эксперимента - все массивы одной размерности):");
            bool ok;
            int N;
            int movesShake, comparesShake;
            Random rnd = new Random();
            do
            {
                ok = Int32.TryParse(Console.ReadLine(), out N);
                if (!ok)
                    Console.WriteLine("Ошибка ввода! Введите натуральное число.");
                if (N <= 0)
                {
                    Console.WriteLine("Ошибка ввода! Введите натуральное число.");
                    ok = false;
                }
            } while (!ok);
            int[] inc = CreateIncArr(N, rnd);
            int[] dec = CreateDecArr(N, rnd);
            int[] disordered = CreateDisordedArr(N, rnd);
            BinaryTree incTree = new BinaryTree();
            BinaryTree decTree = new BinaryTree();
            BinaryTree disorderedTree = new BinaryTree();
            int permutations = N;
            Console.WriteLine("Начальный возрастающий массив: ");
            ShowArr(inc);
            ShakeSort(inc, out movesShake, out comparesShake);
            Console.WriteLine("Отсортированный возрастающий массив (shaker): ");
            ShowArr(inc);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.", movesShake, comparesShake);
            for (int i = 0; i < inc.Length; i++)
            {
                incTree.insert(inc[i]);
            }
            Console.WriteLine("Отсортированный возрастающий массив (двоичное дерево): ");
            incTree.display();
            Console.WriteLine("\nКоличество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", permutations, );
            Console.WriteLine("Начальный убывающий массив: ");
            ShowArr(dec);
            ShakeSort(dec, out movesShake, out comparesShake);
            Console.WriteLine("Отсортированный убывающий массив (shaker): ");
            ShowArr(dec);
            Console.WriteLine("Количество перестановок в убывающем массиве: {0},\n" +
                "Количество сравнений в убывающем массиве: {1}.", movesShake, comparesShake);
            for (int i = 0; i < dec.Length; i++)
            {
                decTree.insert(dec[i]);
            }
            Console.WriteLine("Отсортированный убывающий массив (двоичное дерево): ");
            decTree.display();
            Console.WriteLine("\nКоличество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", permutations, BinaryTree.);
            Console.WriteLine("Начальный неупорядоченный массив: ");
            ShowArr(disordered);
            ShakeSort(disordered, out movesShake, out comparesShake);
            Console.WriteLine("Отсортированный неупорядоченный массив (shaker): ");
            ShowArr(disordered);
            Console.WriteLine("Количество перестановок в неупорядоченном массиве: {0},\n" +
                "Количество сравнений в неупорядоченном массиве: {1}.", movesShake, comparesShake);
            for (int i = 0; i < disordered.Length; i++)
            {
                disorderedTree.insert(disordered[i]);
            }
            Console.WriteLine("Отсортированный неупорядоченный массив (двоичное дерево): ");
            disorderedTree.display();
            Console.WriteLine("\nКоличество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", permutations, compares);
        }
    }
}
