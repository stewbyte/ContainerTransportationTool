using System.ComponentModel.DataAnnotations;

namespace ContainerTransportationTool
{
    internal class Layer
    {
        private Container[,] containers;

        public Layer(int length, int width)
        {
            containers = new Container[length, width];
        }

        public void AddContainer(Container container, int x, int y)
        {
            if (containers[x, y] != null)
            {
                throw new InvalidOperationException("Position is already occupied!");
            }
            if (x >= containers.GetLength(0) || y >= containers.GetLength(1))
            {
                throw new ArgumentOutOfRangeException("x or y", "Index is out of bounds!");
            }

            containers[x, y] = container;
        }

        public void RemoveContainer(int x, int y)
        {
            if (x >= containers.GetLength(0) || y >= containers.GetLength(1))
            {
                throw new ArgumentOutOfRangeException("x or y", "Index is out of bounds!");
            }

            containers[x, y] = null;
        }
    }
}