using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeEx12
{
    class TreeElement
    {
        public readonly int Data;
        public TreeElement Left;
        public TreeElement Right;
        public TreeElement(int data, TreeElement left = null, TreeElement right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }
    class Program
    {
        public static int countChange = 0;
        public static int countCompare = 0;
        private static TreeElement root;
        private static readonly List<int> result = new List<int>(); // отсортированный массив
        private static void AddToTreeElement(int value, ref TreeElement localRoot)
        {
            if (localRoot == null)
            {
                countChange++;
                localRoot = new TreeElement(value);
                return;
            }
            countCompare++;
            if (localRoot.Data < value)
            {
                countChange++;
                AddToTreeElement(value, ref localRoot.Right);
            }
            else
            {
                countChange++;
                AddToTreeElement(value, ref localRoot.Left);
            }
        }
        public static void FormTree(int[] arr)
        {
            foreach (int el in arr)
                AddToTreeElement(el, ref root);
        }
        private static void GetSortedNumRec(TreeElement node)
        {
            // обход дерева лево -> корень -> право
            if (node != null)
            {
                GetSortedNumRec(node.Left);
                result.Add(node.Data);
                GetSortedNumRec(node.Right);
            }
        }
        static public int[] TreeSort(int[] arr)
        {
            root = null;
            result.Clear();
            FormTree(arr);
            GetSortedNumRec(root);
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
            int[] inc1 = inc;
            int[] dec = CreateDecArr(N, rnd);
            int[] dec1 = dec;
            int[] disordered = CreateDisordedArr(N, rnd);
            int[] disordered1 = disordered;
            ShowArr(disordered);
            ShowArr(disordered1);
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
            TreeSort(inc1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный возрастающий массив (двоичное дерево): ");
            Console.ResetColor();
            ShowArr(inc1);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", countChange, countCompare);
            Console.ForegroundColor = ConsoleColor.Green;
            countChange = 0;
            countCompare = 0;
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
            TreeSort(dec1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный убывающий массив (двоичное дерево): ");
            Console.ResetColor();
            ShowArr(dec1);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", countChange, countCompare);
            Console.ForegroundColor = ConsoleColor.Green;
            countChange = 0;
            countCompare = 0;
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
            TreeSort(disordered1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Отсортированный неупорядоченный массив (двоичное дерево): ");
            Console.ResetColor();
            ShowArr(disordered1);
            Console.WriteLine("Количество перестановок в возрастающем массиве: {0},\n" +
                "Количество сравнений в возрастающем массиве: {1}.\n", countChange, countCompare);
            Console.ReadLine();
        }
    }
}
