namespace ContainerTransportationTool.Tests
{
    public class LayerTests
    {
        [Fact]
        public void AddContainerToLayer()
        {
            // Arrange
            Layer layer = new Layer(1, 1);
            Container container = new Container(Enums.ContainerType.Normal, 4000);

            // Act
            layer.AddContainer(container, 0, 0);

            // Assert
            Assert.Equal(container, layer.ReturnContainers()[0, 0]);
        }

        [Fact]
        public void AddContainerToOccupiedPosition()
        {
            // Arrange
            Layer layer = new Layer(1, 1);
            Container container1 = new Container(Enums.ContainerType.Normal, 4000);
            Container container2 = new Container(Enums.ContainerType.Normal, 4000);

            // Act
            layer.AddContainer(container1, 0, 0);

            // Assert
            Assert.Throws<InvalidOperationException>(() => layer.AddContainer(container2, 0, 0));
        }

        [Fact]
        public void AddContainerToInvalidPosition()
        {
            // Arrange
            Layer layer = new Layer(1, 1);
            Container container = new Container(Enums.ContainerType.Normal, 4000);

            // Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => layer.AddContainer(container, 1, 1));
        }
    }
}