using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    public class Container
    {
        public int Weight { get; private set; }
        public ContainerType ContainerType { get; private set; }

        public Container(ContainerType containerType, int weight)
        {
            Weight = weight;
            ContainerType = containerType;
        }
    }
}