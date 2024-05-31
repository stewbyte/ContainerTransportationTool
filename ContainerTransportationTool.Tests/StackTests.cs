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
            Container container = new Container(ContainerType.Normal, 100);

            // Act
            stack.AddContainer(container);

            // Assert
            Assert.Contains(container, stack.GetContainers());
        }
    }
}
