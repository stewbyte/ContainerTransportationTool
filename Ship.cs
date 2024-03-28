namespace ContainerTransportationTool
{
    internal class Ship
    {
        public int stackLength { get; private set; }
        public int stackWidth { get; private set; }
        public List<Container> containers { get; private set; }
        
        public Ship(int stackLength, int stackWidth)
        {
            this.stackLength = stackLength;
            this.stackWidth = stackWidth;
            containers = new List<Container>();
        }
    }
}
