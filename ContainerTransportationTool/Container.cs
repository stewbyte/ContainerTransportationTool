using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    public class Container
    {
        public int weight { get; private set; }
        public ContainerType ContainerType { get; private set; }

        public Container(ContainerType containerType, int weight)
        {
            this.weight = weight;
            ContainerType = containerType;
        }
    }
}