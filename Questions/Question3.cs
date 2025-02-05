using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MMGuideTestProject.Questions {
    public class Question3 : IQuestion {
        #region Properties
        private readonly string filePath = @"InputFiles/Question3.txt";
        //private readonly string filePath = @"InputFiles/Question3 - Sample.txt";

        private List<List<(char, char)>> initialList = new List<List<(char, char)>>();
        private List<int> finalList                  = new List<int>();
        #endregion

        #region Methods
        public void Execute() {
            try {
                initialList.Clear();
                finalList.Clear();

                foreach (string line in File.ReadLines(filePath)) {
                    if (!string.IsNullOrWhiteSpace(line)) {
                        var matches = Regex.Matches(line, @"\[(.),(.)\]");
                        List<(char, char)> readRow = new List<(char, char)>();

                        foreach (Match match in matches)
                            readRow.Add((match.Groups[1].Value[0], match.Groups[2].Value[0]));

                        if (readRow.Count > 0)
                            initialList.Add(readRow);
                    }
                }

                foreach (var dependencies in initialList) {
                    int result = CanPressAllSymbols(dependencies);
                    finalList.Add(result);
                }

                Console.WriteLine($"Question 3 Result: { finalList.Sum() }");
            } catch (Exception ex) {
                Console.WriteLine($"Error in Question 3: { ex.Message }");
            }
        }

        private static int CanPressAllSymbols(List<(char, char)> dependencies) {
            // Step 1: Build graph and track in-degrees
            Dictionary<char, List<char>> graph = new Dictionary<char, List<char>>();
            Dictionary<char, int> inDegree     = new Dictionary<char, int>();

            foreach (var (toPress, prerequisite) in dependencies) {
                if (!graph.ContainsKey(prerequisite)) 
                    graph[prerequisite] = new List<char>();
                
                if (!graph.ContainsKey(toPress)) 
                    graph[toPress] = new List<char>();

                graph[prerequisite].Add(toPress);

                if (!inDegree.ContainsKey(toPress)) 
                    inDegree[toPress] = 0;

                if (!inDegree.ContainsKey(prerequisite)) 
                    inDegree[prerequisite] = 0;

                inDegree[toPress]++; // Increase dependency count for `toPress`
            }

            // Step 2: Find all symbols with no prerequisites (in-degree = 0)
            Queue<char> queue = new Queue<char>();
            foreach (var symbol in inDegree.Where(w => w.Value == 0).Select(s => s.Key)) {
                queue.Enqueue(symbol);
            }

            // Step 3: Process symbols in order
            int processedCount = 0;
            while (queue.Count > 0) {
                char current = queue.Dequeue();
                processedCount++;

                foreach (char dependent in graph[current]) {
                    inDegree[dependent]--;

                    if (inDegree[dependent] == 0) {
                        queue.Enqueue(dependent);
                    }
                }
            }

            // Step 4: If we processed all symbols, it's possible (1), else cycle exists (0)
            return processedCount == inDegree.Count ? 1 : 0;
        }
        #endregion
    }
}

#region Question Description
/*
Success! The gate opens up and the crew steps inside. Once inside, the gate behind them closes. The dimly lit room shows another panel on the wall.

The panel shows connections between symbols. Each connection, shown as an array [a, b], means they need to activate symbol 'b' before symbol 'a'. 
So the crew required to assess the entire set of dependencies and decide if there exists a sequence that allows for the pressing of all symbols 
without violating any dependency rules. This involves ensuring that each symbol a is pressed only after its corresponding prerequisite symbol b has been pressed. 
Determine the feasibility of pressing all symbols in the array while respecting these dependencies. 
If it is possible to press all symbols according to the given dependencies, the solution should be 1; 
otherwise, if it is not possible, the solution should be 0.

Example:
Current dependency:
[.,,],[,,.]
The first array indicates that to press ',' you should first press '.' 
The second element indicates that to press '.' you should first press ','
So it is impossible.
Result: 0

Sum the results.

Sample:
[*,<],[<,*]
[+,~],[',+],[-,']
[+,~],[',+],[/,=],[~,']

First dependency: [*,<],[<,*]
The first array indicates that to press < you should first press *
The second array indicates that to press * you should first press < 
So it is impossible (i.e. not a valid sequence because a cicular dependancy exists [* then < then *])
Impossible (Cycle exists: '*' → '<' → '*' )
Result of first line : 0

Second dependency: [+,~],[',+],[-,']
The first array indicates that to press ~ you should first press + 
The second array indicates that to press + you should first press ' 
The third array indicates that to press ' you should first press -
Possible (Valid sequence: '-' → ' → + → ~)
Result of second line: 1

Third dependency: [+,~],[',+],[/,=],[~,']
The first array indicates that to press ~ you should first press + 
The second array indicates that to press + you should first press ' 
The third array indicates that to press = you should first press / 
The fourth array indicates that to press ' you should press ~ 
Impossible (Cycle exists: `'` → `+` → `~` → `'`)
Result of third line: 0

Final result: Sum of results of each line = 0 (from the first line) + 1 (from the second line) + 0 (from the third line) = 1

Calculate the sum of the outputs from each line in the input file and provide the final result in the output text box.
*/
#endregion