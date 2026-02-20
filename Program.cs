using System;

namespace PartB_Scenario1
{
    class Program
    {
        static FitnessTree tree = new FitnessTree();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine("  Health & Fitness Recommendation System (N-ary) ");
                Console.WriteLine("=================================================");
                Console.WriteLine("A. Take the Fitness Quiz");
                Console.WriteLine("B. Build or Extend the Decision Tree");
                Console.WriteLine("C. View All Fitness Plans");
                Console.WriteLine("D. Search for a Fitness Plan or Question");
                Console.WriteLine("E. Remove a Fitness Plan");
                Console.WriteLine("V. View Tree Structure (Debug)");
                Console.WriteLine("Q. Quit");
                Console.WriteLine("=================================================");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "A":
                        Console.Clear();
                        tree.TakeQuiz();
                        Pause();
                        break;
                    case "B":
                        Console.Clear();
                        tree.AddQuestionToLeaf();
                        Pause();
                        break;
                    case "C":
                        Console.Clear();
                        tree.DisplayAllPlans();
                        Pause();
                        break;
                    case "D":
                        Console.Clear();
                        Console.Write("Enter search query: ");
                        string query = Console.ReadLine();
                        tree.Search(query);
                        Pause();
                        break;
                    case "E":
                        Console.Clear();
                        tree.RemovePlan();
                        Pause();
                        break;
                    case "V":
                        Console.Clear();
                        tree.DisplayTreeStructure();
                        Pause();
                        break;
                    case "Q":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadLine();
        }
    }
}
