using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class SortingAlgorithms
    {
        static void Main()
        {
            int[] data = { 50, 23, 9, 18, 61, 32, 17, 45, 11, 36 };
            int[] originalData = (int[])data.Clone();

            // Define sorting algorithms with their performance measurement
            RunSortingAlgorithm("Bubble Sort", BubbleSort, data);
            data = (int[])originalData.Clone(); // Reset data

            RunSortingAlgorithm("Insertion Sort", InsertionSort, data);
            data = (int[])originalData.Clone(); // Reset data

            RunSortingAlgorithm("Selection Sort", SelectionSort, data);
            data = (int[])originalData.Clone(); // Reset data

            RunSortingAlgorithm("Heap Sort", HeapSort, data);
            data = (int[])originalData.Clone(); // Reset data

            RunSortingAlgorithm("Quick Sort", QuickSort, data);
            data = (int[])originalData.Clone(); // Reset data

            RunSortingAlgorithm("Merge Sort", MergeSort, data);

            // Summary
            Console.WriteLine("\nSummary: Each algorithm was run on the same dataset to compare their performance.");
        }

        // Performance wrapper
        static void RunSortingAlgorithm(string name, Action<int[]> sortAlgorithm, int[] data)
        {
            Console.WriteLine($"\n{name}");
            DescribeAlgorithm(name);
            int[] dataClone = (int[])data.Clone();
            Stopwatch timer = Stopwatch.StartNew();
            sortAlgorithm(dataClone);
            timer.Stop();
            Console.WriteLine($"Sorted Data: {string.Join(", ", dataClone)}");
            Console.WriteLine($"Execution Time: {timer.Elapsed.TotalMilliseconds} ms");
        }

        // Algorithm Descriptions
        static void DescribeAlgorithm(string name)
        {
            switch (name)
            {
                case "Bubble Sort":
                    Console.WriteLine("Description: Repeatedly swaps adjacent elements if they are in the wrong order.");
                    Console.WriteLine("Best Case: O(n), Worst Case: O(n^2)");
                    Console.WriteLine("Pseudocode:\nfor i in 0 to n-1\n  for j in 0 to n-i-2\n    if arr[j] > arr[j+1]\n      swap(arr[j], arr[j+1])");
                    break;
                case "Insertion Sort":
                    Console.WriteLine("Description: Builds the sorted array one element at a time by inserting unsorted elements in the correct position.");
                    Console.WriteLine("Best Case: O(n), Worst Case: O(n^2)");
                    Console.WriteLine("Pseudocode:\nfor i in 1 to n\n  key = arr[i]\n  while j >= 0 and arr[j] > key\n    shift arr[j]\n  insert key");
                    break;
                case "Selection Sort":
                    Console.WriteLine("Description: Finds the minimum element and swaps it with the first unsorted element.");
                    Console.WriteLine("Best Case: O(n^2), Worst Case: O(n^2)");
                    Console.WriteLine("Pseudocode:\nfor i in 0 to n-1\n  minIdx = i\n  for j in i+1 to n\n    if arr[j] < arr[minIdx]\n      minIdx = j\n  swap(arr[i], arr[minIdx])");
                    break;
                case "Heap Sort":
                    Console.WriteLine("Description: Builds a heap and repeatedly extracts the maximum/minimum element.");
                    Console.WriteLine("Best Case: O(n log n), Worst Case: O(n log n)");
                    Console.WriteLine("Pseudocode:\nbuildHeap(arr)\nfor i in n-1 to 0\n  swap(arr[0], arr[i])\n  heapify(arr, 0, i)");
                    break;
                case "Quick Sort":
                    Console.WriteLine("Description: Divides the array into partitions based on a pivot and recursively sorts them.");
                    Console.WriteLine("Best Case: O(n log n), Worst Case: O(n^2)");
                    Console.WriteLine("Pseudocode:\nif low < high\n  pivot = partition(arr, low, high)\n  quickSort(arr, low, pivot-1)\n  quickSort(arr, pivot+1, high)");
                    break;
                case "Merge Sort":
                    Console.WriteLine("Description: Divides the array into halves, sorts them, and merges them back together.");
                    Console.WriteLine("Best Case: O(n log n), Worst Case: O(n log n)");
                    Console.WriteLine("Pseudocode:\nif size > 1\n  mid = n/2\n  left = mergeSort(arr[0..mid])\n  right = mergeSort(arr[mid..n])\n  merge(left, right)");
                    break;
            }
        }

        // Sorting Algorithms Implementation
        static void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
                for (int j = 0; j < arr.Length - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                        Swap(arr, j, j + 1);
        }

        static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < arr.Length; j++)
                    if (arr[j] < arr[minIdx])
                        minIdx = j;
                Swap(arr, i, minIdx);
            }
        }

        static void HeapSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                Swap(arr, 0, i);
                Heapify(arr, i, 0);
            }
        }

        static void Heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && arr[left] > arr[largest])
                largest = left;

            if (right < n && arr[right] > arr[largest])
                largest = right;

            if (largest != i)
            {
                Swap(arr, i, largest);
                Heapify(arr, n, largest);
            }
        }

        static void QuickSort(int[] arr) => QuickSortHelper(arr, 0, arr.Length - 1);

        static void QuickSortHelper(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivot = Partition(arr, low, high);
                QuickSortHelper(arr, low, pivot - 1);
                QuickSortHelper(arr, pivot + 1, high);
            }
        }

        static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }

        static void MergeSort(int[] arr) => MergeSortHelper(arr, 0, arr.Length - 1);

        static void MergeSortHelper(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSortHelper(arr, left, mid);
                MergeSortHelper(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
        }

        static void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] L = new int[n1];
            int[] R = new int[n2];

            Array.Copy(arr, left, L, 0, n1);
            Array.Copy(arr, mid + 1, R, 0, n2);

            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                    arr[k++] = L[i++];
                else
                    arr[k++] = R[j++];
            }

            while (i < n1)
                arr[k++] = L[i++];
            while (j < n2)
                arr[k++] = R[j++];
        }

        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
