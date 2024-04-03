namespace ContainerTransportationTool
{
    internal class Ship
    {
        public int stackLength { get; private set; }
        public int stackWidth { get; private set; }
        public List<Layer> layers;

        public Ship(int stackLength, int stackWidth)
        {
            this.stackLength = stackLength;
            this.stackWidth = stackWidth;
            layers = new List<Layer>();
        }

        public void AddLayer(Layer layer)
        {
            layers.Add(layer);
        }

        public void RemoveLayer(int index)
        {
            layers.RemoveAt(index);
        }

        public Layer GetLayer(int index)
        {
            return layers[index];
        }

        public void ClearLayers()
        {
            layers.Clear();
        }
    }
}