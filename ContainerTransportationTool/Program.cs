using System;
using System.Collections.Generic;
using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            int stackLength = 5;
            int stackWidth = 4;
            Ship ship = new Ship(stackLength, stackWidth, 100000);

            List<Container> containers = new List<Container>
            {
                new Container(ContainerType.Normal, 10000),
            };

            try
            {
                ship.PlaceContainers(containers);

                string visualizationLink = GenerateVisualizationLink(ship);
                Console.WriteLine("Visualization Link: " + visualizationLink);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static string GenerateVisualizationLink(Ship ship)
        {
            List<string> stacks = new List<string>();
            List<string> weights = new List<string>();

            for (int i = 0; i < ship.StackLength; i++)
            {
                List<string> rowStacks = new List<string>();
                List<string> rowWeights = new List<string>();

                for (int j = 0; j < ship.StackWidth; j++)
                {
                    Stack stack = ship.GetStack(i, j);
                    List<string> stackTypes = new List<string>();
                    List<string> stackWeights = new List<string>();

                    foreach (var container in stack.GetContainers())
                    {
                        stackTypes.Add(((int)container.ContainerType).ToString());
                        stackWeights.Add(container.Weight.ToString());
                    }

                    rowStacks.Add(string.Join("-", stackTypes));
                    rowWeights.Add(string.Join("-", stackWeights));
                }

                stacks.Add(string.Join("/", rowStacks));
                weights.Add(string.Join("/", rowWeights));
            }

            string baseUrl = "https://i872272.luna.fhict.nl/ContainerVisualizer/index.html";
            string length = "length=" + ship.StackLength;
            string width = "width=" + ship.StackWidth;
            string stacksParam = "stacks=" + string.Join("/", stacks);
            string weightsParam = "weights=" + string.Join("/", weights);

            return $"{baseUrl}?{length}&{width}&{stacksParam}&{weightsParam}";
        }
    }
}
