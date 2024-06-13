using static ContainerTransportationTool.Enums;

namespace ContainerTransportationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            int stackLength = 3;
            int stackWidth = 3;
            int maxWeight = 900;

            Ship ship = new Ship(stackLength, stackWidth, maxWeight);

            List<Container> containers = new List<Container>
            {
                new Container(ContainerType.Coolable, 30),
                new Container(ContainerType.Coolable, 30),
                new Container(ContainerType.Coolable, 30),
                new Container(ContainerType.Coolable, 25),
                new Container(ContainerType.Coolable, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Normal, 25),
                new Container(ContainerType.Valuable, 25),
                new Container(ContainerType.Valuable, 25),
                new Container(ContainerType.Valuable, 25),
                new Container(ContainerType.Valuable, 25),
            };

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"== SHIP INFO ==");
            Console.WriteLine($"Dimensions: {stackLength}x{stackWidth}");
            Console.WriteLine($"Allowed cargo weight: {ship.MaximumWeight * 0.5} - {ship.MaximumWeight}");
            Console.WriteLine($"Amount of containers to load: {containers.Count} \n");
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                ship.Load(containers);

                string visualizationLink = GenerateVisualizationLink(ship);
                Console.WriteLine("Visualization Link: " + visualizationLink);
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Error: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;

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
                        stackTypes.Add(((int)container.ContainerType + 1).ToString());
                        stackWeights.Add(container.Weight.ToString());
                    }

                    if (stackTypes.Count > 0)
                    {
                        rowStacks.Add(string.Join("-", stackTypes));
                        rowWeights.Add(string.Join("-", stackWeights));
                    }
                }

                if (rowStacks.Count > 0)
                {
                    stacks.Add(string.Join(",", rowStacks));
                }

                if (rowWeights.Count > 0)
                {
                    weights.Add(string.Join(",", rowWeights));
                }
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
