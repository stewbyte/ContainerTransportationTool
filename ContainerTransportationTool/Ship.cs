using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    internal class Ship
    {
        public int StackLength { get; private set; }
        public int StackWidth { get; private set; }
        public List<Stack> Length { get; private set; }
        public List<Stack> Width { get; private set; }

        public Ship(int stackLength, int stackWidth)
        {
            Length = new List<Stack>();
            Width = new List<Stack>();

            Length = new List<Stack>(stackLength);
            Width = new List<Stack>(stackWidth);
        }

        private void ValidateStackIndex(List<Stack> dimension, int index)
        {
            if (index < 0 || index >= dimension.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range!");
            }
        }
    }
}
