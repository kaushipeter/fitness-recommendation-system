using System;
using System.Collections.Generic;
using System.Linq;

namespace PartB_Scenario1
{
    public class FitnessTree
    {
        public TreeNode Root { get; private set; }

        public FitnessTree()
        {
            InitializeSampleTree();
        }

        private void InitializeSampleTree()
        {
             
            var q1 = new QuestionNode("What is your primary fitness goal?");
            Root = q1;

            //  Weight Loss
            var q2 = new QuestionNode("What is your preferred activity style?");
            q1.AddOption("Weight Loss", q2);
            
            q2.AddOption("Cardio Intensive", new PlanNode("Plan C", "HIIT & Running Schedule"));
            q2.AddOption("Low-Impact", new PlanNode("Plan D", "Swimming & Cycling Routine"));
            q2.AddOption("Diet-Centric", new PlanNode("Plan E", "Calorie Deficit + Nutrition Guidance"));

            // Muscle Gain
            var q3 = new QuestionNode("Which training style suits you best?");
            q1.AddOption("Muscle Gain", q3);

            q3.AddOption("Strength Training", new PlanNode("Plan F", "Powerlifting Program"));
            q3.AddOption("Functional Fitness", new PlanNode("Plan G", "CrossFit/Calisthenics Plan"));
            q3.AddOption("Progressive Hypertrophy", new PlanNode("Plan H", "Bodybuilding Split Routine"));

            //  Flexibility
            q1.AddOption("Flexibility & Mindfulness", new PlanNode("Plan A", "Yoga & Meditation Routine"));

            // Overall Health
            q1.AddOption("Overall Health & Balance", new PlanNode("Plan B", "Balanced Lifestyle Plan"));
        }

        //  Fitness Quiz
        public void TakeQuiz()
        {
            if (Root == null)
            {
                Console.WriteLine("Tree is not initialized.");
                return;
            }

            TreeNode currentNode = Root;

            while (currentNode is QuestionNode questionNode)
            {
                Console.WriteLine($"\nQUSTION: {questionNode.Text}");
                var options = questionNode.Options.Keys.ToList();

                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                Console.Write("Choose an option number: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= options.Count)
                {
                    string selectedOption = options[choice - 1];
                    currentNode = questionNode.Options[selectedOption];
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }

            if (currentNode is PlanNode planNode)
            {
                Console.WriteLine($"\n>>> RECOMMENDED PLAN: {planNode.Text}");
                Console.WriteLine($"Details: {planNode.Description}");
            }
        }

        //  View All Fitness Plans
        public void DisplayAllPlans()
        {
            Console.WriteLine("\n--- All Fitness Plans ---");
            DisplayRecursive(Root);
        }

        private void DisplayRecursive(TreeNode node)
        {
            if (node == null) return;

            if (node is PlanNode plan)
            {
                Console.WriteLine($"- {plan.Text}: {plan.Description}");
            }
            else if (node is QuestionNode question)
            {
                foreach (var child in question.Options.Values)
                {
                    DisplayRecursive(child);
                }
            }
        }
        
        // Helper to visualize structure
        public void DisplayTreeStructure()
        {
            Console.WriteLine("\n--- Tree Structure ---");
            Root?.Display(0);
        }


        //  Search for Plan or Question
        public void Search(string query)
        {
            Console.WriteLine($"\nSearching for '{query}'...");
            bool found = SearchRecursive(Root, query);
            if (!found) Console.WriteLine("Not found.");
        }

        private bool SearchRecursive(TreeNode node, string query)
        {
            if (node == null) return false;

            if (node.Text.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Found: [{node.GetType().Name}] {node.Text}");
                return true;
            }

            if (node is QuestionNode question)
            {
                foreach (var child in question.Options.Values)
                {
                    if (SearchRecursive(child, query)) return true;
                }
            }
            return false;
        }

        // Add a new multi-option question at any leaf node
        public void AddQuestionToLeaf()
        {
            // Asks user for parent plan name to extend
            Console.Write("Enter the exact name of the Plan to extend (e.g., Plan A): ");
            string targetPlanName = Console.ReadLine();

            QuestionNode parentOfLeaf = null; 
            string optionKeyToLeaf = null;
            PlanNode leafNode = null;

            // Find the parent of the leaf node we want to replace
            FindParentOfPlan(Root, targetPlanName, ref parentOfLeaf, ref optionKeyToLeaf, ref leafNode);

            if (parentOfLeaf != null && leafNode != null)
            {
                Console.WriteLine($"Found Plan: {leafNode.Text}. It is an option under '{parentOfLeaf.Text}'");
                Console.Write("Enter new Question text: ");
                string newQuestionText = Console.ReadLine();
                
                var newQuestion = new QuestionNode(newQuestionText);
                bool addingOptions = true;
                while (addingOptions)
                {
                    Console.Write("Enter Option Text (e.g., High Intensity): ");
                    string optText = Console.ReadLine();
                    Console.Write("Enter Resulting Plan Name (e.g., Plan X): ");
                    string matchPlanName = Console.ReadLine();
                    Console.Write("Enter Plan Description: ");
                    string matchDesc = Console.ReadLine();

                    newQuestion.AddOption(optText, new PlanNode(matchPlanName, matchDesc));

                    Console.Write("Add another option? (Y/N): ");
                    if (Console.ReadLine().ToUpper() != "Y") addingOptions = false;
                }

                // Replace the old leaf with the new question node
                parentOfLeaf.Options[optionKeyToLeaf] = newQuestion;
                Console.WriteLine("Tree extended successfully.");
            }
            else
            {
                Console.WriteLine("Plan not found or it is the Root (cannot replace root in this impl).");
            }
        }

        // Helper to find parent of a specific leaf node
        private void FindParentOfPlan(TreeNode current, string planName, ref QuestionNode parent, ref string key, ref PlanNode foundNode)
        {
            if (foundNode != null) return; // Already found

            if (current is QuestionNode qNode)
            {
                foreach (var entry in qNode.Options)
                {
                    if (entry.Value is PlanNode pNode && pNode.Text.Equals(planName, StringComparison.OrdinalIgnoreCase))
                    {
                        parent = qNode;
                        key = entry.Key;
                        foundNode = pNode;
                        return;
                    }
                    FindParentOfPlan(entry.Value, planName, ref parent, ref key, ref foundNode);
                }
            }
        }

        //  Remove a Fitness Plan
        public void RemovePlan()
        {
            Console.Write("Enter the exact name of the Plan to remove (e.g., Plan C): ");
            string targetPlanName = Console.ReadLine();

            QuestionNode parent = null;
            string key = null;
            PlanNode found = null;

            FindParentOfPlan(Root, targetPlanName, ref parent, ref key, ref found);

            if (parent != null)
            {
                parent.RemoveOption(key);
                Console.WriteLine($"Plan '{targetPlanName}' removed from '{parent.Text}'.");
            }
            else
            {
                Console.WriteLine("Plan not found.");
            }
        }
    }
}
