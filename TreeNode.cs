namespace PartB_Scenario1
{
   
    public abstract class TreeNode
    {
        public string Text { get; set; }

        protected TreeNode(string text)
        {
            Text = text;
        }

        public abstract void Display(int indentLevel);
    }
}
