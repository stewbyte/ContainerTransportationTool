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
            else if (container == null)
            {
                throw new InvalidOperationException("Container is null!");
            }

            containers[x, y] = container;
        }
    }
}