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
            Ship ship = new Ship(2, 2, 500000);
            List<Container> containers = new List<Container>
            {
                new Container(ContainerType.Normal, 15000),
                new Container(ContainerType.Valuable, 20000),
                new Container(ContainerType.Coolable, 10000),
                new Container(ContainerType.Normal, 30000),
                new Container(ContainerType.Valuable, 25000)
            };

            // Act
            var sortedContainers = ship.SortContainers(containers);

            // Assert
            Assert.Equal(ContainerType.Coolable, sortedContainers[0].ContainerType);
            Assert.Equal(ContainerType.Valuable, sortedContainers[1].ContainerType);
            Assert.Equal(ContainerType.Valuable, sortedContainers[2].ContainerType);
            Assert.Equal(ContainerType.Normal, sortedContainers[3].ContainerType);
            Assert.Equal(ContainerType.Normal, sortedContainers[4].ContainerType);
            Assert.Equal(25000, sortedContainers[1].Weight);
            Assert.Equal(20000, sortedContainers[2].Weight);
            Assert.Equal(30000, sortedContainers[3].Weight);
        }
    }
}
