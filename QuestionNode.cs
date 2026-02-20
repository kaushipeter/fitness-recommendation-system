using System;
using System.Collections.Generic;

namespace PartB_Scenario1
{
    
    public class QuestionNode : TreeNode
    {
        // Dictionary mapping Option Text -> Next Node (Child)
        public Dictionary<string, TreeNode> Options { get; private set; }

        public QuestionNode(string questionText) : base(questionText)
        {
            Options = new Dictionary<string, TreeNode>();
        }

        public void AddOption(string option, TreeNode node)
        {
            if (!Options.ContainsKey(option))
            {
                Options.Add(option, node);
            }
        }

        public void RemoveOption(string option)
        {
            if (Options.ContainsKey(option))
            {
                Options.Remove(option);
            }
        }

        public override void Display(int indentLevel)
        {
            Console.WriteLine(new string(' ', indentLevel * 2) + $"[Question]: {Text}");
            foreach (var option in Options)
            {
                Console.WriteLine(new string(' ', (indentLevel + 1) * 2) + $"- Option: {option.Key}");
                option.Value.Display(indentLevel + 2);
            }
        }
    }
}
