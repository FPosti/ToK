namespace GarageArrays
{
    internal sealed class Garage
    {
        public const int MaxCapacity = 1000;

        private readonly Vehicle?[] _vehicles;

        public int Capacity => _vehicles.Length;
        public int Count { get; private set; }

        public Garage(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(capacity, MaxCapacity);

            _vehicles = new Vehicle?[capacity];
        }

        public Vehicle[] GetVehicles()
        {
            return Search();
        }

        public Vehicle? FindByRegistration(string registrationNumber)
        {
            int index = FindIndexByRegistration(registrationNumber);

            if (index == -1)
            {
                return null;
            }

            return _vehicles[index];
        }

        public bool TryPark(Vehicle vehicle, out string errorMessage)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            if (FindByRegistration(vehicle.RegistrationNumber) is not null)
            {
                errorMessage = "Ett fordon med det registreringsnumret är redan parkerat.";
                return false;
            }

            for (int i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i] is null)
                {
                    _vehicles[i] = vehicle;
                    Count++;
                    errorMessage = string.Empty;
                    return true;
                }
            }

            errorMessage = "Garaget är fullt.";
            return false;
        }

        public bool Remove(string registrationNumber)
        {
            int index = FindIndexByRegistration(registrationNumber);

            if (index == -1)
            {
                return false;
            }

            _vehicles[index] = null;
            Count--;

            return true;
        }

        public Vehicle[] Search(
            string? registrationNumber = null,
            string? color = null,
            int? numberOfWheels = null,
            string? vehicleType = null)
        {
            registrationNumber = registrationNumber?.Trim();
            color = color?.Trim();
            vehicleType = vehicleType?.Trim();

            Vehicle[] matches = new Vehicle[Count];
            int matchCount = 0;

            foreach (Vehicle? vehicle in _vehicles)
            {
                if (vehicle is null)
                {
                    continue;
                }

                bool matchesRegistration = string.IsNullOrEmpty(registrationNumber) ||
                    string.Equals(vehicle.RegistrationNumber, registrationNumber,
                        StringComparison.OrdinalIgnoreCase);

                bool matchesColor = string.IsNullOrEmpty(color) ||
                    string.Equals(vehicle.Color, color,
                        StringComparison.OrdinalIgnoreCase);

                bool matchesWheels = numberOfWheels is null ||
                    vehicle.NumberOfWheels == numberOfWheels;

                bool matchesType = string.IsNullOrEmpty(vehicleType) ||
                    string.Equals(vehicle.GetType().Name, vehicleType,
                        StringComparison.OrdinalIgnoreCase);

                if (matchesRegistration && matchesColor && matchesWheels && matchesType)
                {
                    matches[matchCount] = vehicle;
                    matchCount++;
                }
            }

            return matches[..matchCount];
        }

        private int FindIndexByRegistration(string registrationNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);
            registrationNumber = registrationNumber.Trim();

            for (int i = 0; i < _vehicles.Length; i++)
            {
                Vehicle? vehicle = _vehicles[i];

                if (vehicle is not null &&
                    string.Equals(vehicle.RegistrationNumber, registrationNumber,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
