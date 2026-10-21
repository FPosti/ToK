namespace GarageArrays;

internal static class GarageConsole
{
    public static void Run()
    {
        try
        {
            RunApplication();
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nInmatningen avslutades. Programmet stängs.");
        }
    }

    private static void RunApplication()
    {
        Console.WriteLine("välkommen till garaget");
        int capacity = ConsoleInput.ReadInt($"Ange garagets kapacitet (1-{Garage.MaxCapacity}): ", 1, Garage.MaxCapacity);
        Garage garage = new(capacity);

        int addSamples = ConsoleInput.ReadInt("Vill du fylla garaget med exempelfordon? 1 = ja, 0 = nej: ", 0, 1);
        if (addSamples == 1)
        {
            VehicleConsole.AddSampleVehicles(garage);
        }

        while (true)
        {
            ShowMainMenu(garage);

            int choice = ConsoleInput.ReadInt("Välj ett alternativ: ", 0, 5);

            switch (choice)
            {
                case 1:
                    VehicleConsole.DisplayVehicles(garage.GetVehicles());
                    break;
                case 2:
                    VehicleConsole.ListVehicleTypes(garage);
                    break;
                case 3:
                    VehicleConsole.ParkVehicle(garage);
                    break;
                case 4:
                    VehicleConsole.RemoveVehicle(garage);
                    break;
                case 5:
                    SearchMenu(garage);
                    break;
                case 0:
                    Console.WriteLine("Programmet avslutas.");
                    return;
            }
        }
    }

    private static void ShowMainMenu(Garage garage)
    {
        Console.WriteLine($"\nGarage: {garage.Count}/{garage.Capacity} platser upptagna");
        Console.WriteLine("1. Lista parkerade fordon");
        Console.WriteLine("2. Visa antal fordon per typ");
        Console.WriteLine("3. Parkera ett fordon");
        Console.WriteLine("4. Hämta ut ett fordon");
        Console.WriteLine("5. Sök efter fordon");
        Console.WriteLine("0. Avsluta");
    }

    private static void SearchMenu(Garage garage)
    {
        while (true)
        {
            Console.WriteLine("\nSökmeny");
            Console.WriteLine("1. Sök via registreringsnummer");
            Console.WriteLine("2. Sök med flera filter");
            Console.WriteLine("0. Tillbaka");

            int choice = ConsoleInput.ReadInt("Välj ett alternativ: ", 0, 2);

            switch (choice)
            {
                case 1:
                    VehicleConsole.FindVehicle(garage);
                    break;
                case 2:
                    VehicleConsole.SearchVehicles(garage);
                    break;
                case 0:
                    return;
            }
        }
    }
}
