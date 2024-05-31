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

            InitializeStacks();
        }

        private void InitializeStacks()
        {
            for (int i = 0; i < StackLength; i++)
            {
                Length.Add(new Stack());
            }
            for (int i = 0; i < StackWidth; i++)
            {
                Width.Add(new Stack());
            }
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
