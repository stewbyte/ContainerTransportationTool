using Microsoft.VisualStudio.CodeCoverage;
using System.Collections.Generic;
using Xunit;
using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool.Tests
{
    public class ShipTests
    {
        [Fact]
        public void SortContainers_ShouldPrioritizeCoolableThenValuableThenNormal_AndSortByWeight()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);
            List<Container> containers = new List<Container>
            {
                new Container(ContainerType.Normal, 15),
                new Container(ContainerType.Valuable, 20),
                new Container(ContainerType.Coolable, 10),
                new Container(ContainerType.Normal, 30),
                new Container(ContainerType.Valuable, 25)
            };

            // Act
            var sortedContainers = ship.SortContainers(containers);

            // Assert
            Assert.Equal(ContainerType.Coolable, sortedContainers[0].ContainerType);
            Assert.Equal(ContainerType.Valuable, sortedContainers[1].ContainerType);
            Assert.Equal(ContainerType.Valuable, sortedContainers[2].ContainerType);
            Assert.Equal(ContainerType.Normal, sortedContainers[3].ContainerType);
            Assert.Equal(ContainerType.Normal, sortedContainers[4].ContainerType);
            Assert.Equal(25, sortedContainers[1].Weight);
            Assert.Equal(20, sortedContainers[2].Weight);
            Assert.Equal(30, sortedContainers[3].Weight);
        }

        [Fact]
        public void IsShipBalanced_ShouldReturnTrueWhenShipIsBalanced()
        {
            // Max Allowed Difference 100 * 0.2 = 20 - Difference: 20
            // Arrange
            Ship ship = new Ship(2, 2, 500);

            ship.AddContainer(new Container(ContainerType.Normal, 20), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 20), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 30), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 30), 1, 1);

            // Act
            bool isBalanced = ship.IsShipBalanced();

            // Assert
            Assert.True(isBalanced);
        }

        [Fact]
        public void IsShipBalanced_ShouldReturnFalseWhenShipIsUnbalanced()
        {
            // Max Allowed Difference 100 * 0.2 = 20 - Difference: 22
            // Arrange
            Ship ship = new Ship(2, 2, 500);

            ship.AddContainer(new Container(ContainerType.Normal, 19), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 20), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 30), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 31), 1, 1);

            // Act
            bool isBalanced = ship.IsShipBalanced();

            // Assert
            Assert.False(isBalanced);
        }

        [Fact]
        public void IsWeightUtilized_ShouldReturnTrueWhenWeightUtilizationIsMet()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 300);

            ship.AddContainer(new Container(ContainerType.Normal, 40), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 40), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 40), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 40), 1, 1);

            // Act
            bool isWeightUtilized = ship.IsWeightUtilized();

            // Assert
            Assert.True(isWeightUtilized);
        }

        [Fact]
        public void IsWeightUtilized_ShouldReturnFalseWhenWeightUtilizationIsNotMet()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 300);

            ship.AddContainer(new Container(ContainerType.Normal, 35), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 35), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 35), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 35), 1, 1);

            // Act
            bool isWeightUtilized = ship.IsWeightUtilized();

            // Assert
            Assert.False(isWeightUtilized);
        }

        [Fact]
        public void GetStack_ShouldReturnsStack()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 100);

            // Act
            var stack = ship.GetStack(1, 1);

            // Assert
            Assert.NotNull(stack);
        }

        [Fact]
        public void GetStack_ShouldThrowExceptionForInvalidIndex()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 100);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.GetStack(1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.GetStack(1, 2));
        }

        [Fact]
        public void ValidateStackIndex_ShouldNotThrowExceptionForValidIndices()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);

            // Act & Assert
            Exception ex = Record.Exception(() => ship.ValidateStackIndex(1, 1));
            Assert.Null(ex);
        }

        [Fact]
        public void ValidateStackIndex_ShouldThrowExceptionForInvalidIndex()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.ValidateStackIndex(1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ship.ValidateStackIndex(1, 2));
        }

        [Fact]
        public void CanPlaceContainer_ShouldReturnTrueForCoolableContainerInFirstRow()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);
            Container coolableContainer = new Container(ContainerType.Coolable, 10);

            // Act
            bool canPlace = ship.CanPlaceContainer(coolableContainer, 0, 0);

            // Assert
            Assert.True(canPlace);
        }
    }
}
