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

        [Fact]
        public void GetTopContainer_ShouldReturnTopContainer()
        {
            // Arrange
            Stack stack = new Stack();
            Container container1 = new Container(ContainerType.Normal, 10);
            Container container2 = new Container(ContainerType.Normal, 20);
            stack.AddContainer(container1);
            stack.AddContainer(container2);

            // Act
            Container topContainer = stack.GetTopContainer();

            // Assert
            Assert.Equal(container2, topContainer);
        }

        [Fact]
        public void GetTopContainer_ShouldThrowExceptionWhenStackIsEmpty()
        {
            // Arrange
            Stack stack = new Stack();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.GetTopContainer());
        }
    }
}
