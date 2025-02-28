using System;

class Program {
    static void Main() {
        Console.WriteLine("Select Mode: (1) Producer or (2) Consumer");
        string? choice = Console.ReadLine();
        if (choice == "1") {
            Producer.Run();
        }
        else if (choice == "2") {
            Consumer.Run();
        }
        else {
            Console.WriteLine("Invalid choice. Exiting...");
        }
    }
}