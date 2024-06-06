using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    public class Ship
    {
        public int StackLength { get; private set; }
        public int StackWidth { get; private set; }
        public int MaximumWeight { get; private set; }
        public List<List<Stack>> Stacks { get; private set; }
        
        // Sorted Containers
        public List<Container> NormalContainers { get; private set; }
        public List<Container> ValuableContainers { get; private set; }
        public List<Container> CoolableContainers { get; private set; }

        public Ship(int stackLength, int stackWidth, int maximumWeight)
        {
            StackLength = stackLength;
            StackWidth = stackWidth;
            MaximumWeight = maximumWeight;

            NormalContainers = new List<Container>();
            ValuableContainers = new List<Container>();
            CoolableContainers = new List<Container>();

            InitializeStacks();
        }

        public void Load(List<Container> containers)
        {
            if (GetWeightOfContainers(containers) > MaximumWeight)
            {
                throw new InvalidOperationException($"The weight of the load ({GetWeightOfContainers(containers)} tons) is too heavy for this ship ({MaximumWeight} tons).");
            }
            
            AddContainersToLists(containers);
            SortContainersLists();

            IsWeightUtilized();
        }

        public void PlaceContainers(List<Container> containers)
        {
            //foreach (Container container in sortedContainers)
            //{
            //    bool placed = PlaceContainerOnLighterSide(container);

            //    if (!placed)
            //    {
            //        throw new InvalidOperationException("Unable to place container due to constraints.");
            //    }
            //}

            //if (!IsShipBalanced() || !IsWeightUtilized())
            //{
            //    throw new InvalidOperationException("Ship is not balanced and/or less than 50% of the weight is utilized.");
            //}
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
            Stack stackTarget = GetStack(lengthIndex, widthIndex);

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

            Console.WriteLine(stackTarget.GetWeightAboveFirstContainer() + container.Weight >= stackTarget.GetWeightAboveFirstContainer());

            if (stackTarget.GetWeightAboveFirstContainer() + container.Weight >= stackTarget.MaxStackWeight)
            {
                return false;
            }

            return true;
        }

        public bool PlaceContainerOnLighterSide(Container container)
        {
            double leftWeight = CalculateWeight(0, StackWidth / 2);
            double rightWeight = CalculateWeight(StackWidth / 2, StackWidth);

            if (leftWeight <= rightWeight || leftWeight + rightWeight == 0)
            {
                return TryPlaceContainer(container, 0, StackWidth / 2);
            }
            else
            {
                return TryPlaceContainer(container, StackWidth / 2, StackWidth);
            }
        }

        public bool TryPlaceContainer(Container container, int startColumn, int endColumn)
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

        public double CalculateWeight(int startColumn, int endColumn)
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

        public void AddContainersToLists(List<Container> containers)
        {
            foreach (var container in containers)
            {
                switch (container.ContainerType)
                {
                    case ContainerType.Normal:
                        NormalContainers.Add(container);
                        break;
                    case ContainerType.Valuable:
                        ValuableContainers.Add(container);
                        break;
                    case ContainerType.Coolable:
                        CoolableContainers.Add(container);
                        break;
                }
            }
        }

        public void SortContainersLists()
        {
            NormalContainers = NormalContainers.OrderByDescending(c => c.Weight).ToList();
            ValuableContainers = ValuableContainers.OrderByDescending(c => c.Weight).ToList();
            CoolableContainers = CoolableContainers.OrderByDescending(c => c.Weight).ToList();
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

        public int GetWeightOfContainers(List<Container> containers)
        {
            int count = 0;

            foreach (Container container in containers)
            {
                count += container.Weight;
            }

            return count;
        }
    }
}
