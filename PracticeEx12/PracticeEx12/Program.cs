using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeEx12
{
    public class TreeEl
    {
        public int data;
        public TreeEl left;
        public TreeEl right;
        public TreeEl(int value, TreeEl l = null, TreeEl r = null)
        {
            data = value;
            left = l;
            right = r;
        }
    }
    class Program
    {
        public static TreeEl root;
        public static List<int> result = new List<int>();
        public static void Add(int value, ref TreeEl staticRoot, ref int treeCompares, ref int treeChanges)
        {
            if (staticRoot == null)
            {
                treeChanges++;
                staticRoot = new TreeEl(value);
                return;
            }
            treeCompares++;
            if (staticRoot.data < value)
            {
                treeChanges++;
                Add(value, ref staticRoot.right, ref treeCompares, ref treeChanges);
            }
            else
            {
                treeChanges++;
                Add(value, ref staticRoot.left, ref treeCompares, ref treeChanges);
            }
        }
        public static void CreateTree(int[] arr, out int treeCompares, out int treeChanges)
        {
            treeCompares = 0;
            treeChanges = 0;
            foreach (int elem in arr)
            {
                Add(elem, ref root, ref treeCompares, ref treeChanges);
            }
        }
        public static void GetNumber(TreeEl node)
        {
            if (node != null)
            {
                GetNumber(node.left);
                result.Add(node.data);
                GetNumber(node.right);
            }
        }
        public static int[] TreeSort(int[] arr, out int treeCompares, out int treeChanges)
        {
            treeCompares = 0;
            treeChanges = 0;
            root = null;
            result.Clear();
            CreateTree(arr, out treeCompares, out treeChanges);
            GetNumber(root);
            return result.ToArray();
        }
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
            int movesShake, comparesShake, movesTree, comparesTree;
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
            int[] inc1 = inc;
            int[] dec = CreateDecArr(N, rnd);
            int[] dec1 = dec;
            int[] disordered = CreateDisordedArr(N, rnd);
            int[] disordered1 = disordered;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Начальный возрастающий массив: ");
            Console.ResetColor();
            ShowArr(inc);
            ShakeSort(inc, out movesShake, out comparesShake);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный возрастающий массив (shaker): ");
            Console.ResetColor();
            ShowArr(inc);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.", movesShake, comparesShake);
            TreeSort(inc1, out comparesTree, out movesTree);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный возрастающий массив (двоичное дерево): ");
            Console.ResetColor();
            ShowArr(inc1);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", movesTree, comparesTree);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Начальный убывающий массив: ");
            Console.ResetColor();
            ShowArr(dec);
            ShakeSort(dec, out movesShake, out comparesShake);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный убывающий массив (shaker): ");
            Console.ResetColor();
            ShowArr(dec);
            Console.WriteLine("Количество перестановок в убывающем массиве: {0},\n" +
                "Количество сравнений в убывающем массиве: {1}.", movesShake, comparesShake);
            TreeSort(dec1, out comparesTree, out movesTree);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный убывающий массив (двоичное дерево): ");
            Console.ResetColor();
            ShowArr(dec1);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", movesTree, comparesTree);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Начальный неупорядоченный массив: ");
            Console.ResetColor();
            ShowArr(disordered);
            ShakeSort(disordered, out movesShake, out comparesShake);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный неупорядоченный массив (shaker): ");
            Console.ResetColor();
            ShowArr(disordered);
            Console.WriteLine("Количество перестановок в неупорядоченном массиве: {0},\n" +
                "Количество сравнений в неупорядоченном массиве: {1}.", movesShake, comparesShake);
            TreeSort(disordered1, out comparesTree, out movesTree);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный неупорядоченный массив (двоичное дерево): ");
            Console.ResetColor();
            ShowArr(disordered1);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", movesTree, comparesTree);
            Console.ReadLine();
        }
    }
}
