using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool.Tests
{
    public class StackTests
    {
        [Fact]
        public void AddContainer_ShouldAddContainerWhenWithinWeightLimit()
        {
            // Arrange
            Stack stack = new Stack();
            Container container = new Container(ContainerType.Normal, 30);

            // Act
            stack.AddContainer(container);

            // Assert
            Assert.Contains(container, stack.GetContainers());
        }


        [Fact]
        public void AddContainer_ShouldThrowExceptionWhenExceedingContainerWeightLimit()
        {
            // Arrange
            Stack stack = new Stack();
            Container heavyContainer = new Container(ContainerType.Normal, 40);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.AddContainer(heavyContainer));
        }

        [Fact]
        public void AddContainer_ShouldThrowExceptionWhenExceedingStackWeightLimit()
        {
            // Arrange
            Stack stack = new Stack();
            stack.AddContainer(new Container(ContainerType.Normal, 30));
            stack.AddContainer(new Container(ContainerType.Normal, 30));
            stack.AddContainer(new Container(ContainerType.Normal, 30));
            stack.AddContainer(new Container(ContainerType.Normal, 30));
            stack.AddContainer(new Container(ContainerType.Normal, 30));

            Container heavyContainer = new Container(ContainerType.Normal, 30);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.AddContainer(heavyContainer));
        }
    }
}
