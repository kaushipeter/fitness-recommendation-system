using System;

namespace PartB_Scenario1
{
 
    public class PlanNode : TreeNode
    {
        public string Description { get; set; }

        public PlanNode(string planName, string description) : base(planName)
        {
            Description = description;
        }

        public override void Display(int indentLevel)
        {
            string indent = new string('-', indentLevel * 2);
            Console.WriteLine($"{indent} [PLAN]: {Text} - {Description}");
        }
    }
}
