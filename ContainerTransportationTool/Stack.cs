namespace ContainerTransportationTool
{
    public class Stack
    {
        private List<Container> containers;
        public int MaxStackWeight { get; private set; } = 120;
        public int MaxContainerWeight { get; private set; } = 30;

        public Stack()
        {
            containers = new List<Container>();
        }

        public void AddContainer(Container container)
        {
            if (GetWeightAboveFirstContainer() + container.Weight > MaxStackWeight)
            {
                throw new InvalidOperationException("Adding this container exceeds the maximum allowed weight above the first container!");
            }

            if (container.Weight > MaxContainerWeight)
            {
                throw new InvalidOperationException("Container exceeds max container weight!");
            }

            containers.Insert(0, container);
        }

        public Container GetTopContainer()
        {
            if (containers.Count == 0)
            {
                throw new InvalidOperationException("No container at this position!");
            }

            return containers[0];
        }


        public void DeleteTopContainer()
        {
            if (containers.Count == 0)
            {
                throw new InvalidOperationException("No container to remove!");
            }

            containers.RemoveAt(containers.Count - 1);
        }

        public int GetWeightAboveFirstContainer()
        {
            int totalWeight = 0;

            for (int i = 1; i < containers.Count; i++)
            {
                totalWeight += containers[i].Weight;
            }
            return totalWeight;
        }

        public int GetTotalWeight()
        {
            return containers.Sum(container => container.Weight);
        }

        public List<Container> GetContainers()
        {
            return containers;
        }
    }
}
