// See https://aka.ms/new-console-template for more information

using MMGuideTestProject.Questions;
using MMGuideTestProject;

#region RedirectInput
while (true) {
    Console.WriteLine("\nSelect a question to run by capturing the numerical value and pressing Enter:");
    Console.WriteLine("0 - Exit application");
    Console.WriteLine("1 - Question 1");
    Console.WriteLine("2 - Question 2");
    Console.WriteLine("3 - Question 3");
    Console.WriteLine("4 - Question 4");
    Console.WriteLine("5 - Question 5");
    Console.Write("Enter your choice: ");

    if (int.TryParse(Console.ReadLine(), out int choice)) {
        if (choice == 0) {
            Console.WriteLine("Exiting program...");
            break;
        }

        IQuestion? question = QuestionFactory.GetQuestion(choice);
        if (question != null) {
            question.Execute();
        } else {
            Console.WriteLine("Invalid choice. Try again.");
        }
    } else {
        Console.WriteLine("Invalid input. Please enter a number.");
    }
}
#endregion