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
        public List<Container> Containers { get; private set; }
        public int NormalContainers { get; private set; }
        public int CoolableContainers { get; private set; }
        public int ValuableContainers { get; private set; }

        public Ship(int stackLength, int stackWidth, int maximumWeight)
        {
            StackLength = stackLength;
            StackWidth = stackWidth;
            MaximumWeight = maximumWeight;

            Containers = new List<Container>();
            NormalContainers = 0;
            ValuableContainers = 0;
            CoolableContainers = 0;

            InitializeStacks();
        }

        public void Load(List<Container> containers)
        {
            if (containers.Count == 0)
            {
                throw new InvalidOperationException($"There are no containers to load.");
            }

            if (GetWeightOfContainers(containers) > MaximumWeight)
            {
                throw new InvalidOperationException($"The weight of the load ({GetWeightOfContainers(containers)} tons) is too heavy for this ship. The maximum allowed load weight should be at most {MaximumWeight} tons.");
            }

            if (GetWeightOfContainers(containers) < MaximumWeight * 0.5)
            {
                throw new InvalidOperationException($"The weight of the load ({GetWeightOfContainers(containers)} tons) is too light for this ship. The minimum allowed load weight should be at least {MaximumWeight * 0.5} tons.");
            }

            AddContainersToLists(containers);
            List<Container> sortedContainers = SortContainers(containers);

            //if (CoolableContainers > StackWidth)
            //{
            //    throw new InvalidOperationException($"There is not enough space to accommodate all {CoolableContainers} coolable containers, there is only space for {StackWidth}.");
            //}

            TryPlaceContainers(sortedContainers);
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

        public void TryPlaceContainers(List<Container> containers)
        {
            foreach (Container container in containers)
            {
                bool placed = false;

                for (int layer = 0; !placed; layer++)
                {
                    double leftWeight = CalculateWeight(0, StackWidth / 2);
                    double rightWeight = CalculateWeight(StackWidth / 2, StackWidth);

                    if (leftWeight <= rightWeight || leftWeight + rightWeight == 0)
                    {
                        placed = TryPlaceContainerOnSide(container, layer, 0, StackWidth / 2);
                    }
                    else
                    {
                        placed = TryPlaceContainerOnSide(container, layer, StackWidth / 2, StackWidth);
                    }

                    if (!placed)
                    {
                        if (leftWeight <= rightWeight || leftWeight + rightWeight == 0)
                        {
                            placed = TryPlaceContainerOnSide(container, layer, StackWidth / 2, StackWidth);
                        }
                        else
                        {
                            placed = TryPlaceContainerOnSide(container, layer, 0, StackWidth / 2);
                        }
                    }
                }

                if (!placed)
                {
                    throw new InvalidOperationException("Unable to place container due to constraints.");
                }
            }
        }

        private bool TryPlaceContainerOnSide(Container container, int layer, int startColumn, int endColumn)
        {
            for (int widthIndex = startColumn; widthIndex < endColumn; widthIndex++)
            {
                for (int lengthIndex = 0; lengthIndex < StackLength; lengthIndex++)
                {
                    if (layer > 0 && Stacks[lengthIndex][widthIndex].GetContainers().Count < layer)
                    {
                        continue;
                    }

                    if (CanPlaceContainer(container, lengthIndex, widthIndex, layer))
                    {
                        AddContainer(container, lengthIndex, widthIndex);
                        Console.WriteLine($"[{lengthIndex + 1}x{widthIndex + 1}] < {container.ContainerType}: {container.Weight}t");
                        return true;
                    }
                }
            }
            return false;
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
        public bool CanPlaceContainer(Container container, int lengthIndex, int widthIndex, int layer)
        {
            Stack stackTarget = GetStack(lengthIndex, widthIndex);

            if (CheckIfOccupied(lengthIndex, widthIndex, layer))
            {
                return false;
            }

            if (container.ContainerType == ContainerType.Coolable)
            {
                if (widthIndex != 0)
                {
                    return false;
                }
            }

            if (container.ContainerType == ContainerType.Valuable)
            {
                bool isFrontAccessible = lengthIndex == 0 || Stacks[lengthIndex - 1][widthIndex].GetContainers().Count <= layer;
                bool isBackAccessible = lengthIndex == StackLength - 1 || Stacks[lengthIndex + 1][widthIndex].GetContainers().Count <= layer;

                if (!isFrontAccessible && !isBackAccessible)
                {
                    return false;
                }

                if (lengthIndex > 0 && Stacks[lengthIndex - 1][widthIndex].GetContainers().Count > layer)
                {
                    return false;
                }

                if (lengthIndex < StackLength - 1 && Stacks[lengthIndex + 1][widthIndex].GetContainers().Count > layer)
                {
                    return false;
                }
            }

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
                    if (CanPlaceContainer(container, i, j, 0))
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

        public bool CheckIfOccupied(int lengthIndex, int widthIndex, int layer)
        {
            ValidateStackIndex(lengthIndex, widthIndex);

            if (Stacks[lengthIndex][widthIndex].GetContainers().Count <= layer)
            {
                return false;
            }

            return true;
        }


        public void AddContainersToLists(List<Container> containers)
        {
            foreach (var container in containers)
            {
                switch (container.ContainerType)
                {
                    case ContainerType.Normal:
                        NormalContainers++;
                        break;
                    case ContainerType.Valuable:
                        ValuableContainers++;
                        break;
                    case ContainerType.Coolable:
                        CoolableContainers++;
                        break;
                }
            }
        }

        public List<Container> SortContainers(List<Container> containers)
        {
            return containers.OrderByDescending(c => c.ContainerType == ContainerType.Valuable)
                             .ThenBy(c => c.ContainerType == ContainerType.Coolable)
                             .ThenBy(c => c.Weight)
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
