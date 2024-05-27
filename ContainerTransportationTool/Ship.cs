namespace ContainerTransportationTool
{
    internal class Ship
    {
        public int stackLength { get; private set; }
        public int stackWidth { get; private set; }
        public List<Stack> layers;

        public Ship(int stackLength, int stackWidth)
        {
            this.stackLength = stackLength;
            this.stackWidth = stackWidth;
            layers = new List<Stack>();
        }

        public void AddLayer(Stack layer)
        {
            layers.Add(layer);
        }

        public void RemoveLayer(int index)
        {
            layers.RemoveAt(index);
        }

        public Stack GetLayer(int index)
        {
            return layers[index];
        }

        public void ClearLayers()
        {
            layers.Clear();
        }
    }
}