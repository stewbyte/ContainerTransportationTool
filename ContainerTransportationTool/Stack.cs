using System;
using System.Collections.Generic;

namespace ContainerTransportationTool
{
    public class Stack
    {
        private List<Container> containers;
        public int MaxStackWeight { get; private set; } = 120000; // Maximum weight of the stack in kg

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

            containers.Add(container);
        }

        public Container GetTopContainer()
        {
            if (containers.Count == 0)
            {
                throw new InvalidOperationException("No container at this position!");
            }

            return containers[containers.Count - 1];
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
            if (containers.Count <= 1)
            {
                return 0;
            }

            int totalWeight = 0;
            for (int i = 1; i < containers.Count; i++)
            {
                totalWeight += containers[i].Weight;
            }
            return totalWeight;
        }
    }
}
