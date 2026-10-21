namespace GarageArrays;

internal static class VehicleConsole
{
    private static readonly string[] VehicleTypeNames =
    [
        nameof(Motorcycle), nameof(Airplane), nameof(Car), nameof(Bus), nameof(Boat)
    ];

    public static void DisplayVehicles(Vehicle[] vehicles)
    {
        if (vehicles.Length == 0)
        {
            Console.WriteLine("Inga fordon att visa.");
            return;
        }

        foreach (Vehicle vehicle in vehicles)
        {
            Console.WriteLine(vehicle);
        }
    }

    public static void ListVehicleTypes(Garage garage)
    {
        if (garage.Count == 0)
        {
            Console.WriteLine("Garaget är tomt.");
            return;
        }

        foreach (string type in VehicleTypeNames)
        {
            int count = garage.Search(vehicleType: type).Length;

            if (count > 0)
            {
                Console.WriteLine($"{GetVehicleTypeDisplayName(type)}: {count}");
            }
        }
    }

    public static void ParkVehicle(Garage garage)
    {
        if (garage.Count == garage.Capacity)
        {
            Console.WriteLine("Garaget är fullt.");
            return;
        }

        string? type = ReadVehicleType(allowAny: false);
        string registration = ConsoleInput.ReadRegistrationNumber("Registreringsnummer: ");

        if (garage.FindByRegistration(registration) is not null)
        {
            Console.WriteLine("Ett fordon med det registreringsnumret är redan parkerat.");
            return;
        }

        string color = ConsoleInput.ReadRequiredText("Färg: ");
        int wheels = ConsoleInput.ReadInt("Antal hjul: ", 0);

        Vehicle vehicle;

        switch (type)
        {
            case nameof(Motorcycle):
                int cylinderVolumeCm3 = ConsoleInput.ReadInt("Cylindervolym (cm3): ", 1);
                vehicle = new Motorcycle(registration, color, wheels, cylinderVolumeCm3);
                break;
            case nameof(Airplane):
                int engines = ConsoleInput.ReadInt("Antal motorer: ", 1);
                vehicle = new Airplane(registration, color, wheels, engines);
                break;
            case nameof(Car):
                string fuelType = ConsoleInput.ReadRequiredText("Bränsletyp: ");
                vehicle = new Car(registration, color, wheels, fuelType);
                break;
            case nameof(Bus):
                int seats = ConsoleInput.ReadInt("Antal sittplatser: ", 1);
                vehicle = new Bus(registration, color, wheels, seats);
                break;
            case nameof(Boat):
                decimal length = ConsoleInput.ReadPositiveDecimal("Längd i meter: ");
                vehicle = new Boat(registration, color, wheels, length);
                break;
            default:
                Console.WriteLine("Okänd fordonstyp.");
                return;
        }

        if (garage.TryPark(vehicle, out string errorMessage))
        {
            Console.WriteLine($"Fordonet {vehicle.RegistrationNumber} har parkerats.");
        }
        else
        {
            Console.WriteLine(errorMessage);
        }
    }

    public static void RemoveVehicle(Garage garage)
    {
        string registration = ConsoleInput.ReadRegistrationNumber("Ange registreringsnummer att hämta ut: ");

        Console.WriteLine(garage.Remove(registration)
            ? "Fordonet har hämtats ut."
            : "Inget fordon med det registreringsnumret hittades.");
    }

    public static void FindVehicle(Garage garage)
    {
        string registration = ConsoleInput.ReadRegistrationNumber("Registreringsnummer att söka efter: ");
        Vehicle? vehicle = garage.FindByRegistration(registration);

        if (vehicle is null)
        {
            Console.WriteLine("Inget fordon med det registreringsnumret hittades.");
        }
        else
        {
            Console.WriteLine(vehicle);
        }
    }

    public static void SearchVehicles(Garage garage)
    {
        Console.WriteLine("Lämna fält tomma för valfria värden. Alla angivna sökvillkor måste stämma.");
        string? registration = ConsoleInput.ReadOptionalRegistrationNumber("Registreringsnummer (lämna tomt för alla): ");
        string? color = ConsoleInput.ReadOptionalText("Färg (lämna tomt för alla): ");
        int? wheels = ConsoleInput.ReadOptionalWheelCount();
        string? type = ReadVehicleType(allowAny: true);

        DisplayVehicles(garage.Search(registration, color, wheels, type));
    }

    public static void AddSampleVehicles(Garage garage)
    {
        Vehicle[] samples =
        [
            new Car("ABC123", "svart", 4, "Bensin"),
            new Motorcycle("MCT12A", "rosa", 3, 600),
            new Airplane("FLY123", "vit", 3, 2),
            new Bus("BUS123", "röd", 6, 40),
            new Boat("BAT123", "blå", 0, 5.5m)
        ];

        int added = 0;

        foreach (Vehicle vehicle in samples)
        {
            if (garage.TryPark(vehicle, out _))
            {
                added++;
            }
        }

        Console.WriteLine($"{added} exempelfordon parkerades. " +
            $"{samples.Length - added} fick inte plats.");
    }

    private static string GetVehicleTypeDisplayName(string type)
    {
        return type switch
        {
            nameof(Motorcycle) => "Motorcykel",
            nameof(Airplane) => "Flygplan",
            nameof(Car) => "Bil",
            nameof(Bus) => "Buss",
            nameof(Boat) => "Båt",
            _ => type,
        };
    }

    private static string? ReadVehicleType(bool allowAny)
    {
        for (int i = 0; i < VehicleTypeNames.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {GetVehicleTypeDisplayName(VehicleTypeNames[i])}");
        }

        if (allowAny)
        {
            Console.WriteLine("0. Alla fordonstyper");
        }

        int choice = ConsoleInput.ReadInt("Välj fordonstyp: ", allowAny ? 0 : 1, VehicleTypeNames.Length);
        return choice == 0 ? null : VehicleTypeNames[choice - 1];
    }
}
