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
            // Max Allowed Difference 40 * 0.2 = 8 - Difference: 8
            // Arrange
            Ship ship = new Ship(2, 2, 500);

            ship.AddContainer(new Container(ContainerType.Normal, 8), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 8), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 12), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 12), 1, 1);

            // Act
            bool isBalanced = ship.IsShipBalanced();

            // Assert
            Assert.True(isBalanced);
        }

        [Fact]
        public void IsShipBalanced_ShouldReturnFalseWhenShipIsUnbalanced()
        {
            // Max Allowed Difference 40 * 0.2 = 8 - Difference: 10
            // Arrange
            Ship ship = new Ship(2, 2, 500);

            ship.AddContainer(new Container(ContainerType.Normal, 7), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 8), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 12), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 13), 1, 1);

            // Act
            bool isBalanced = ship.IsShipBalanced();

            // Assert
            Assert.False(isBalanced);
        }

        [Fact]
        public void IsWeightUtilized_ShouldReturnTrueWhenWeightUtilizationIsMet()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 150);

            ship.AddContainer(new Container(ContainerType.Normal, 20), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 20), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 20), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 20), 1, 1);

            // Act
            bool isWeightUtilized = ship.IsWeightUtilized();

            // Assert
            Assert.True(isWeightUtilized);
        }

        [Fact]
        public void IsWeightUtilized_ShouldReturnFalseWhenWeightUtilizationIsNotMet()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 150);

            ship.AddContainer(new Container(ContainerType.Normal, 15), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 15), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 20), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 20), 1, 1);

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
        public void CanPlaceContainer_ShouldReturnTrueForNormalContainer()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);
            Container normalContainer = new Container(ContainerType.Normal, 10);

            // Act
            bool canPlace = ship.CanPlaceContainer(normalContainer, 1, 1);

            // Assert
            Assert.True(canPlace);
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

        [Fact]
        public void CanPlaceContainer_ShouldReturnFalseForCoolableContainerNotInFirstRow()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);
            Container coolableContainer = new Container(ContainerType.Coolable, 10);

            // Act
            bool canPlace = ship.CanPlaceContainer(coolableContainer, 1, 0);

            // Assert
            Assert.False(canPlace);
        }

        [Fact]
        public void CanPlaceContainer_ShouldReturnFalseForValuableContainerOnOccupiedStack()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);
            Container valuableContainer = new Container(ContainerType.Valuable, 10);
            ship.AddContainer(new Container(ContainerType.Normal, 10), 0, 0);

            // Act
            bool canPlace = ship.CanPlaceContainer(valuableContainer, 0, 0);

            // Assert
            Assert.False(canPlace);
        }

        [Fact]
        public void CanPlaceContainer_ShouldReturnTrueForValuableContainerOnEmptyStack()
        {
            // Arrange
            Ship ship = new Ship(2, 2, 500);
            Container valuableContainer = new Container(ContainerType.Valuable, 10);

            // Act
            bool canPlace = ship.CanPlaceContainer(valuableContainer, 0, 0);

            // Assert
            Assert.True(canPlace);
        }
    }
}
