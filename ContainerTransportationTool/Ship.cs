using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    internal class Ship
    {
        public int StackLength { get; private set; }
        public int StackWidth { get; private set; }
        public List<List<Stack>> Stacks { get; private set; }

        public Ship(int stackLength, int stackWidth)
        {
            StackLength = stackLength;
            StackWidth = stackWidth;
            InitializeStacks();
        }

        private void InitializeStacks()
        {
            Stacks = new List<List<Stack>>();
            for (int i = 0; i < StackLength; i++)
            {
                var row = new List<Stack>();
                for (int j = 0; j < StackWidth; j++)
                {
                    row.Add(new Stack());
                }
                Stacks.Add(row);
            }
        }

        private void ValidateStackIndex(List<Stack> dimension, int index)
        {
            if (index < 0 || index >= dimension.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range!");
            }
        }
    }
}
