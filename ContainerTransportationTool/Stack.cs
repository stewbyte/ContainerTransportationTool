using System;
using System.Collections.Generic;

namespace ContainerTransportationTool
{
    public class Stack
    {
        private List<Container> containers;
        public int maxStackWeight { get; private set; } = 120000;

        public Stack()
        {
            containers = new List<Container>();
        }

        public void AddContainer(Container container)
        {
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
    }
}
