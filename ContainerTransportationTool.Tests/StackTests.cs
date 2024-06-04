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

        [Fact]
        public void DeleteTopContainer_ShouldRemoveTopContainer()
        {
            // Arrange
            Stack stack = new Stack();
            Container container1 = new Container(ContainerType.Normal, 10);
            Container container2 = new Container(ContainerType.Normal, 20);
            stack.AddContainer(container1);
            stack.AddContainer(container2);

            // Act
            stack.DeleteTopContainer();

            // Assert
            Assert.DoesNotContain(container2, stack.GetContainers());
            Assert.Contains(container1, stack.GetContainers());
        }

        [Fact]
        public void DeleteTopContainer_ShouldThrowExceptionWhenStackIsEmpty()
        {
            // Arrange
            Stack stack = new Stack();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.DeleteTopContainer());
        }

        [Fact]
        public void GetWeightAboveFirstContainer_ShouldReturnCorrectWeight()
        {
            // Arrange
            Stack stack = new Stack();
            stack.AddContainer(new Container(ContainerType.Normal, 10));
            stack.AddContainer(new Container(ContainerType.Normal, 20));
            stack.AddContainer(new Container(ContainerType.Normal, 30));

            // Act
            int weightAboveFirst = stack.GetWeightAboveFirstContainer();

            // Assert
            Assert.Equal(50, weightAboveFirst); // 20 + 30
        }

        [Fact]
        public void GetWeightAboveFirstContainer_ShouldReturnZeroWhenOnlyOneContainer()
        {
            // Arrange
            Stack stack = new Stack();
            stack.AddContainer(new Container(ContainerType.Normal, 10));

            // Act
            int weightAboveFirst = stack.GetWeightAboveFirstContainer();

            // Assert
            Assert.Equal(0, weightAboveFirst);
        }
    }
}
