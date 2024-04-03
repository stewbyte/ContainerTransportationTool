using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    public class Container
    {
        public ContainerType ContainerType { get; private set; }
        public int weight { get; private set; }
    }
}