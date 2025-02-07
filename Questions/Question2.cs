using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MMGuideTestProject.Questions {
    public class Question2 : IQuestion {
        #region Properties
        private readonly string filePath = @"InputFiles/Question2.txt";
        //private readonly string filePath = @"InputFiles/Question2 - Sample.txt";

        private List<List<int>> initialList = new List<List<int>>();
        private List<int> countedList       = new List<int>();
        private List<int> finalList         = new List<int>();
        #endregion

        #region Methods
        public void Execute() {
            try {
                initialList.Clear();
                countedList.Clear();
                finalList.Clear();

                foreach (string line in File.ReadLines(filePath)) {
                    if (!string.IsNullOrWhiteSpace(line)) {
                        List<int> readRow = line.Trim('[',']').Split(',').Select(int.Parse).ToList();
                        initialList.Add(readRow);
                    }
                }

                for (int i = 0; i < initialList.Count; i++) {
                    List<int> firstList = initialList[i];
                    countedList.Clear();

                    for (int j = 0; j < firstList.Count; j++) {
                        int counter = firstList.Skip(j + 1).Count(c => firstList[j] > c); // Count the number items smaller than the cuurent number
                        countedList.Add(counter);
                    }
                  
                    int median = GetMedian(countedList);
                    finalList.Add(median);
                }

                Console.WriteLine($"Question 2 Result: { finalList.Sum() }");
            } catch (Exception ex) {
                Console.WriteLine($"Error in Question 2: { ex.Message }");
            }
        }

        private static int GetMedian(List<int> numbers) {
            numbers.Sort();

            int count     = numbers.Count;
            int mid       = count / 2;
            double median = (count % 2 != 0)
                            ? numbers[mid]                              // Odd count: return middle element
                            : (numbers[mid - 1] + numbers[mid]) / 2.0;  // Even count: average of two middle elements

            return (int)Math.Round(median);
        }
        #endregion
    }
}

#region Question Description
/*
After a smooth landing with one of the USS MM Guide’s shuttles on what seems like a small landing pad, the crew leaves the safety of the craft...
A big flights of steps lead up to something approximating a doorway, a closed gate into the unknown structure…
As the crew surveys the gate, they notice a sequence of numeric notations on one of the walls, which seem to provide clues for opening the huge door. The crew quickly realizes they must translate this sequence into a new sequence adhering to a specific rule that seems to be the core of this puzzle. The rule for translating the sequence is as follows: For each number in the original sequence, the crew must determine how many numbers to the right of it are strictly smaller than it. So For each number in the sequence, the crew needs to compare it with every other number that comes after it. It is important to note that the count is exclusively concerned with numbers that are to the right of the current number. Numbers that have appeared before it in the sequence are not considered in this count.

Example:
Current sequence:
[50,20,60,10]

50: There are 2 numbers to its right that are smaller (20 and 10).
20: There is 1 number to its right that is smaller (10).
60: There is 1 number to its right that is smaller (10).
10: There are no numbers to its right that are smaller.

Array Result: [2,1,1,0]

Sum the medians of all arrays.

[5,2,6,1]
[3, 7, 2, 4, 4]
[55,277,166,123,88,3]

First line:
5: There are 2 numbers to its right that are smaller (2 and 1).
2: There is 1 number to its right that is smaller (1).
6: There is 1 number to its right that is smaller (1).
1: There are no numbers to its right that are smaller.
Array Result: [2,1,1,0]

Second Line:
3: There is 1 number to its right that is smaller (2).
7: There are 3 numbers to its right that are smaller (2, 4, 4).
2: There are no numbers to its right that are smaller.
4 (first instance): There are no numbers to its right that are smaller.
4 (second instance): There are no numbers to its right that are smaller.
Array Result: [1,3,0,0,0]

Third Line:
55: There is 1 number to its right that is smaller (3).
277: There are 4 numbers to its right that are smaller (166, 123, 88, 3).
166: There are 3 numbers to its right that are smaller (123, 88, 3).
123: There are 2 numbers to its right that are smaller (88, 3).
88: There is 1 number to its right that is smaller (3).
3: There are no numbers to its right that are smaller.
Array result: [1,4,3,2,1,0]

Final result: [rounded up median of first array result = 1] + [rounded up median of second array result = 0] + [rounded up median of third array result = 2] = 3

Explanation of final result:
To calculate the median of the first array [2, 1, 1, 0], we begin by sorting it in ascending order: [0, 1, 1, 2]. Since the array has an even number of elements (4), we find the median by taking the average of the two middle elements, which are both 1. Thus, the median is 1, and since it is an integer, there is no need to round it up.
For the second array [1, 3, 0, 0, 0], sorting it yields [0, 0, 0, 1, 3]. With an odd number of elements (5), the median is the middle element, which is 0. As 0 is an integer, rounding up is unnecessary.
Now, let’s consider the third array [1, 4, 3, 2, 1, 0]. Sorting it results in [0, 1, 1, 2, 3, 4]. Since the array has an even number of elements (6), we calculate the median by averaging the two middle elements, which are 1 and 2. The average is 1.5, and rounding up gives us 2.
Therefore, the final result is the sum of the medians: 1 (from the first array) + 0 (from the second array) + 2 (from the third array) = 3

3

Compute the sum of the rounded up medians from the array results and write the final result into the output text box.
*/
#endregion