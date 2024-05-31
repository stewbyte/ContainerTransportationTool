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
            // Max Allowed Difference 500 * 0.2 = 100 - Difference: 100
            // Arrange
            Ship ship = new Ship(2, 2, 1000);

            ship.AddContainer(new Container(ContainerType.Normal, 100), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 100), 1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 150), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 150), 1, 1);

            // Act
            bool isBalanced = ship.IsShipBalanced();

            // Assert
            Assert.True(isBalanced);
        }

        [Fact]
        public void IsShipBalanced_ShouldReturnFalseWhenShipIsUnbalanced()
        {
            // Max Allowed Difference 500 * 0.2 = 100 - Difference: 102
            // Arrange
            Ship ship = new Ship(2, 2, 1000);

            ship.AddContainer(new Container(ContainerType.Normal, 100), 0, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 99),  1, 0);
            ship.AddContainer(new Container(ContainerType.Normal, 150), 0, 1);
            ship.AddContainer(new Container(ContainerType.Normal, 151), 1, 1);

            // Act
            bool isBalanced = ship.IsShipBalanced();

            // Assert
            Assert.False(isBalanced);
        }
    }
}
