using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MMGuideTestProject.Questions {
    public class Question4 : IQuestion {
        #region Properties
        private readonly string filePath = @"InputFiles/Question4.txt";
        //private readonly string filePath = @"InputFiles/Question4 - Sample.txt";
        #endregion

        #region Methods
        public void Execute() {
            try {
                string[] lines           = File.ReadAllLines(filePath);
                List<BigInteger> results = new List<BigInteger>();

                foreach (string line in lines) {
                    string[] parts          = line.Split(' ', 2);
                    BigInteger n            = BigInteger.Parse(parts[0]);
                    HashSet<BigInteger> set = parts[1].Trim('[', ']')
                                                      .Split(',')
                                                      .Select(BigInteger.Parse)
                                                      .ToHashSet();

                    results.Add(GetMaxSplits(n, set));
                }

                Console.WriteLine($"Question 4 Result: { string.Concat(results) }");
                //Sample outputs 35 but it should be 34 -> NEED TO INVESTIGATE!
            } catch (Exception ex) {
                Console.WriteLine($"Error in Question 4: {ex.Message}");
            }
        }

        // Recursive function to compute max splits
        private static BigInteger GetMaxSplits(BigInteger n, HashSet<BigInteger> set) {
            Dictionary<BigInteger, BigInteger> memo = new Dictionary<BigInteger, BigInteger>();
            Queue<(BigInteger pile, BigInteger splits)> queue = new Queue<(BigInteger, BigInteger)>();
            
            queue.Enqueue((n, 0));
            BigInteger maxSplits = 0;
            
            while (queue.Count > 0) {
                var (currentPile, currentSplits) = queue.Dequeue();

                foreach (BigInteger x in set) {
                    if (x != currentPile && currentPile % x == 0) {
                        BigInteger numNewPiles = currentPile / x;
                        BigInteger newSplits   = numNewPiles - 1;
                        BigInteger totalSplits = currentSplits + newSplits;

                        if (!memo.ContainsKey(x) || totalSplits > memo[x]) {
                            memo[x] = totalSplits;
                            queue.Enqueue((x, totalSplits));

                            if (totalSplits > maxSplits)
                                maxSplits = totalSplits;
                        }

                    }
                }
            }

            return maxSplits;
        }
        #endregion
    }
}

#region Question Description
/*
The crew faces a new challenge linked to the mysterious object. 
You are provided with a list, where each row represents a pile of a number of stones 
that you aim to divide into multiple piles, using a set of distinct integers. 
Here's how the splitting process works:

1. Begin by searching for a number x in the set where x != n and n is divisible by x (i.e. x is a factor of n).
2. If such an x exists, divide the pile into n/x smaller piles.
3. Repeat this procedure for every new pile generated, one at a time, until further division is no longer feasible. 
Your task is to find the maximum number of splits possible for each row in the list using numbers from the set S, 
and then concatenate all the individual results to obtain the final result.

SAMPLE:
12 [3,6,5]
18 [9,6,3]

In the first row, with an initial pile size of 12 stones and the set S=[3,6,5]:
1. If we select 3 from S: We can split it into 12/3=4 equal piles of size 3 to get: (3,3,3,3). However, since there are no other suitable numbers in the set to further divide the piles, only one split occurs.
2. If we select 6 from S: We can split it into 12/6=2 equal piles of size 6 to get: (6,6). Then, selecting x=2 from S, we can split a pile of size 6 into 6/2=3 equal piles of size 3 to get: (3,3,6).
    Repeating the previous move again on another pile of size 6 gives: (3,3,3,3). There are no other suitable numbers in the set to further divide the piles. This scenario allows for a total of 3 splits.
3. If we select 5 from S, the pile cannot be evenly divided by 5. 
=>Therefore, the maximum number of splits achievable is 3.

In the second row, with an initial pile size of 18 stones and the set S=[3,6,9]:
1. If we select 3 from S: We can split it into 18/3=6 equal piles of size 3 to get: (3,3,3,3,3,3). However, since there are no other suitable numbers in the set to further divide the piles, only one split occurs.
2. If we select 6 from S: We can split it into 18/6=3 equal piles of size 6 to get: (6,6,6). Then, selecting x=3 from S, we can split a pile of size 6 into 6/3=2 equal piles of size 3 to get: (3,3,6,6).
    Repeating the previous move again on another pile of size 6 gives: (3,3,3,3,6)
    Repeating the previous move again on another pile of size 6 gives: (3,3,3,3,3,3).
    Thearere  no other suitable numbers in the set to further divide the piles. This scenario allows for a total of 4 splits.
3. If we select 9 from S: We can split it into 18/9=2 equal piles of size 6 to get: (9,9). Then, selecting x=3 from S, we can split a pile of size 9 into 9/3=3 equal piles of size 3 to get: (3,3,3,9).
    Repeating the previous move again on another pile of size 6 gives: (3,3,3,3,3,3). 
    There are no other suitable numbers in the set to further divide the piles.
    This scenario allows for a total of 3 splits. 
=>Therefore, the maximum number of splits achievable is 4.

The results of two rows are connected together, and a final output is generated: 34

34 (concat the maximum splits from each pile) 

You are provided with a list containing several piles of stones. 
Your objective is to determine the highest number of splits (divisions) achievable for each pile following the previously explained rules. 
Once you have found the maximum number of splits for each pile, concatenate all the resulting numbers into a single number.
*/
#endregion