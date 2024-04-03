using ContainerTransportationTool;

bool exit = false;
Validator validator = new Validator();

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
            Console.Write("Enter the length of the ship: ");
            int length = int.Parse(Console.ReadLine());
            validator.Validate(length, 1, 10);

            Console.Clear();
            Console.Write("Enter the width of the ship: ");
            int width = int.Parse(Console.ReadLine());
            validator.Validate(width, 1, 10);
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