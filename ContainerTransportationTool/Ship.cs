namespace ContainerTransportationTool
{
    internal class Ship
    {
        public int StackLength { get; private set; }
        public int StackWidth { get; private set; }
        public List<Stack> Stacks { get; private set; }

        public Ship(int stackLength, int stackWidth)
        {
            StackLength = stackLength;
            StackWidth = stackWidth;
            Stacks = new List<Stack>();
        }

        public void AddStack()
        {
            Stacks.Add(new Stack());
        }

        private void ValidateStackIndex(int index)
        {
            if (index < 0 || index >= Stacks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range!");
            }
        }

        public void RemoveStack(int index)
        {
            ValidateStackIndex(index);
            Stacks.RemoveAt(index);
        }

        public Stack GetStack(int index)
        {
            ValidateStackIndex(index);
            return Stacks[index];
        }

        public void ClearStacks()
        {
            Stacks.Clear();
        }
    }
}
