using ContainerTransportationTool;

bool exit = false;

while (!exit)
{
    Console.WriteLine("// ContainerTransportationTool");
    Console.WriteLine("1. Create new ship");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.Write("Enter the LENGTH of the ship's container stack:");
            int length = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.Write("Enter the WIDTH of the ship's container stack:");
            int width = int.Parse(Console.ReadLine());
            break;

        case "0":
            exit = true;
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
}