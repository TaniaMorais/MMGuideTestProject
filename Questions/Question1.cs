using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MMGuideTestProject.Questions {
    public class Question1 : IQuestion {
        #region Properties
        private readonly string filePath = @"InputFiles/Question1.txt";
        //private readonly string filePath = @"InputFiles/Question1 - Sample.txt";

        private List<List<int>> initialList = new List<List<int>>();
        private List<int> finalList         = new List<int>();
        private int rightCount              = 0;
        private int leftCount               = 0;
        private BigInteger finalResult      = 1;
        #endregion

        #region Methods
        public void Execute() {
            try {
                initialList.Clear();
                finalList.Clear();
                rightCount  = 0;
                leftCount   = 0;
                finalResult = 1;

                foreach (string line in File.ReadLines(filePath)) {
                    if (!string.IsNullOrWhiteSpace(line)) {
                        List<int> readRow = line.Select(s => int.Parse(s.ToString())).ToList();
                        initialList.Add(readRow);
                    }
                }

                for (int i = 0; i < initialList.Count; i++) {
                    int sumOfTicks       = 0;
                    List<int> firstList  = initialList[i];
                    List<int> secondList = initialList[i + 1];

                    for (int j = 0; j < firstList.Count; j++) {
                        int firstItem  = firstList[j];
                        int secondItem = secondList[j];
                        rightCount     = 0;
                        leftCount      = 0;

                        if (secondItem < firstItem) {
                            rightCount = firstItem - secondItem;
                            leftCount = ((9 - firstItem) + (1 + secondItem));
                        } else {
                            rightCount = firstItem + (10 - secondItem);
                            leftCount  = secondItem - firstItem;
                        }

                        sumOfTicks += Math.Min(rightCount, leftCount);
                    }

                    i += 1;
                    finalList.Add(sumOfTicks);
                }

                foreach (int item in finalList) {
                    if (item == 0) {
                        Console.WriteLine("Warning: A zero detected in final list, skipping multiplication.");
                        continue;
                    }

                    finalResult *= item;
                }

                Console.WriteLine($"Question 1 Result: { finalResult }");
            } catch (Exception ex) {
                Console.WriteLine($"Error in Question 1: { ex.Message }");
            }
        }
        #endregion
    }
}

#region Question Description
/*
The USS MM Guide is pursuing the exploration of the Iota Quadrant...

As the spaceship approaches an unknown system, the long-range scanners detect an unusual magnetic field emanating from the surface of the third planet in the Poseidon star system.

Standard research protocols dictate the crew has to investigate the origin of this anomaly...

As the spaceship approaches the planet, the central computer notices the unknown magnetic force is acting on the engines and a critical alert is emitted. The incessant beep is heard all over the cabine and the console shows a message with instructions to adjust the engines to support the new pressure on its propulsion. "Insufficient data for a safe atmospheric entry. Please provide additional information."

The spaceship has 4 engines that have their throttle adjusted by a rounded dial with numbers from 0 to 9 each. That dial can be turned left to increase the number or right to decrease the number, which gives less or more power to the engine and a perfect combination between the 4 engines allows the spaceship to support any atmosphere. Each tick on the dial changes one number.The dials are continuous, which means that turning them one tick right from 0 will set it to 9 and one tick left from 9 will set it back to 0. Due to the new magnetic force, the central computer requires the engines to be adjusted by a limited number of ticks to the dial, varying as they enter further into the atmosphere.


Determine the least number of ticks either to left or right to get to the expected number on each dial. A set of the current numbers on the dials will be given by the central computer as well as the expected numbers to be adjusted in each dial.

Example:
Current numbers on the dials:
4 5 2 8

Expected numbers:
2 4 9 1

For the first dial to result in number 2 it can be ticked 2 times to the right or 8 times to the left.
So, the expected result would be 2.
For the second dial to result in number 4 it can be ticked 1 time to the right or 9 times to the left.
So, the expected result would be 1.
For the third dial to result in number 9 it can be ticked 7 times to the left or 3 times to the right.
So, the expected result would be 3.
For the fourth dial to result in number 1 it can be ticked 3 times to the left or 7 times to the right to result in number 1.
So, the expected result would be 3.

After that, the ticks should be summed, to get the result for each set of dials:
2 + 1 + 3 + 3 = 9

At the end, multiply the results for all sets.

More example sets:
4 5 2 8
2 4 9 1
3 1 6 1
4 2 0 9
5 9 1 0
8 8 9 1

First set:
4 5 2 8
2 4 9 1
2 (ticks right) + 1 (tick right) + 3 (ticks right) + 3 (ticks left)
Result: 9

Second set:
3 1 6 1
4 2 0 9
1 (tick left) + 1 (tick left) + 4 (ticks left) + 2 (ticks right)
Result: 8

Third set:
5 9 1 0
8 8 9 1
3 (ticks left) + 1 (tick right) + 2 (ticks right) + 1 (tick left)
Result: 7

Final result: 9 * 8 * 7 = 504
504

Sum the numbers of least ticks to get the result per set of dials and multiple all the results from each set.
*/
#endregion