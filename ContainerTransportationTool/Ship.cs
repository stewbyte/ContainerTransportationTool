using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    internal class Ship
    {
        public int StackLength { get; private set; }
        public int StackWidth { get; private set; }
        public int MaximumWeight { get; private set; }
        public List<List<Stack>> Stacks { get; private set; }

        public Ship(int stackLength, int stackWidth)
        {
            StackLength = stackLength;
            StackWidth = stackWidth;
            InitializeStacks();
        }

        public void PlaceContainers(List<Container> containers)
        {
            foreach (Container container in containers)
            {

            }
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

        private bool CanPlaceContainer(Container container, int lengthIndex, int widthIndex)
        {
            if (container.ContainerType == ContainerType.Coolable)
            {
                if (lengthIndex != 0)
                {
                    return false;
                }
            }

            if (container.ContainerType == ContainerType.Valuable)
            {
                if (Stacks[lengthIndex][widthIndex].GetContainers().Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public List<Container> SortContainers(List<Container> containers)
        {
            return containers.OrderByDescending(c => c.ContainerType == ContainerType.Coolable)
                             .ThenByDescending(c => c.ContainerType == ContainerType.Valuable)
                             .ThenByDescending(c => c.Weight)
                             .ToList();
        }

        private void ValidateStackIndex(int lengthIndex, int widthIndex)
        {
            if (lengthIndex < 0 || lengthIndex >= StackLength || widthIndex < 0 || widthIndex >= StackWidth)
            {
                throw new ArgumentOutOfRangeException(nameof(lengthIndex), "Index is out of range!");
            }
        }

        public Stack GetStack(int lengthIndex, int widthIndex)
        {
            ValidateStackIndex(lengthIndex, widthIndex);
            return Stacks[lengthIndex][widthIndex];
        }

        public bool AddContainer(Container container, int lengthIndex, int widthIndex)
        {
            ValidateStackIndex(lengthIndex, widthIndex);

            Stack stack = Stacks[lengthIndex][widthIndex];

            stack.AddContainer(container);

            return true;
        }

        public bool IsShipBalanced()
        {
            double leftWeight = 0;
            double rightWeight = 0;
            int half = StackWidth / 2;

            for (int i = 0; i < StackLength; i++)
            {
                leftWeight += Stacks[i].GetRange(0, half).Sum(stack => stack.GetTotalWeight());
                rightWeight += Stacks[i].GetRange(half, StackWidth - half).Sum(stack => stack.GetTotalWeight());
            }

            double totalWeight = leftWeight + rightWeight;
            return Math.Abs(leftWeight - rightWeight) <= totalWeight * 0.2;
        }

        public bool IsWeightUtilized()
        {
            double totalWeight = Stacks.Sum(row => row.Sum(stack => stack.GetTotalWeight()));
            return totalWeight >= MaximumWeight * 0.5;
        }
    }
}
