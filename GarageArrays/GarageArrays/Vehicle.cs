namespace GarageArrays
{
    internal abstract class Vehicle
    {
        public string RegistrationNumber { get; }
        public string Color { get; }
        public int NumberOfWheels { get; }

        protected abstract string VehicleTypeName { get; }

        protected Vehicle(string registrationNumber, string color, int numberOfWheels)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(color);
            ArgumentOutOfRangeException.ThrowIfNegative(numberOfWheels);

            if (!RegistrationNumberValidator.TryParse(registrationNumber, out string registration))
            {
                throw new ArgumentException(RegistrationNumberValidator.ErrorMessage, nameof(registrationNumber));
            }

            RegistrationNumber = registration;
            Color = color.Trim();
            NumberOfWheels = numberOfWheels;
        }

        protected abstract string GetSpecificDetails();

        public sealed override string ToString()
        {
            return $"{VehicleTypeName}: {RegistrationNumber}, färg: {Color}, " +
                $"antal hjul: {NumberOfWheels}, {GetSpecificDetails()}";
        }
    }
}
