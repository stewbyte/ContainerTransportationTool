using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    public class Ship
    {
        public int StackLength { get; private set; }
        public int StackWidth { get; private set; }
        public int MaximumWeight { get; private set; }
        public List<List<Stack>> Stacks { get; private set; }

        public Ship(int stackLength, int stackWidth, int maximumWeight)
        {
            StackLength = stackLength;
            StackWidth = stackWidth;
            MaximumWeight = maximumWeight;

            InitializeStacks();
        }

        public void PlaceContainers(List<Container> containers)
        {
            var sortedContainers = SortContainers(containers);

            foreach (Container container in sortedContainers)
            {
                bool placed = PlaceContainerOnLighterSide(container);

                if (!placed)
                {
                    throw new InvalidOperationException("Unable to place container due to constraints.");
                }
            }

            if (!IsShipBalanced() || !IsWeightUtilized())
            {
                throw new InvalidOperationException("Ship is not balanced and/or less than 50% of the weight is utilized.");
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

        public bool CanPlaceContainer(Container container, int lengthIndex, int widthIndex)
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

        private bool PlaceContainerOnLighterSide(Container container)
        {
            double leftWeight = CalculateWeight(0, StackWidth / 2);
            double rightWeight = CalculateWeight(StackWidth / 2, StackWidth);

            if (leftWeight <= rightWeight)
            {
                return TryPlaceContainer(container, 0, StackWidth / 2);
            }
            else
            {
                return TryPlaceContainer(container, StackWidth / 2, StackWidth);
            }
        }

        private bool TryPlaceContainer(Container container, int startColumn, int endColumn)
        {
            for (int i = 0; i < StackLength; i++)
            {
                for (int j = startColumn; j < endColumn; j++)
                {
                    if (CanPlaceContainer(container, i, j))
                    {
                        AddContainer(container, i, j);
                        return true;
                    }
                }
            }
            return false;
        }

        private double CalculateWeight(int startColumn, int endColumn)
        {
            double weight = 0;
            for (int i = 0; i < StackLength; i++)
            {
                for (int j = startColumn; j < endColumn; j++)
                {
                    weight += Stacks[i][j].GetTotalWeight();
                }
            }
            return weight;
        }

        public List<Container> SortContainers(List<Container> containers)
        {
            return containers.OrderByDescending(c => c.ContainerType == ContainerType.Coolable)
                             .ThenByDescending(c => c.ContainerType == ContainerType.Valuable)
                             .ThenByDescending(c => c.Weight)
                             .ToList();
        }

        public void ValidateStackIndex(int lengthIndex, int widthIndex)
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
            double leftWeight = CalculateWeight(0, StackWidth / 2);
            double rightWeight = CalculateWeight(StackWidth / 2, StackWidth);
            float maxAllowedDifference = 0.2f;

            double totalWeight = leftWeight + rightWeight;
            return Math.Abs(leftWeight - rightWeight) <= totalWeight * maxAllowedDifference;
        }

        public bool IsWeightUtilized()
        {
            double totalWeight = Stacks.Sum(row => row.Sum(stack => stack.GetTotalWeight()));
            return totalWeight >= MaximumWeight * 0.5;
        }
    }
}
