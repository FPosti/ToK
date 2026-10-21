namespace GarageArrays
{
    internal sealed class Bus : Vehicle
    {
        public int NumberOfSeats { get; }

        protected override string VehicleTypeName => "Buss";

        public Bus(string registrationNumber, string color,
            int numberOfWheels, int numberOfSeats)
            : base(registrationNumber, color, numberOfWheels)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfSeats);

            NumberOfSeats = numberOfSeats;
        }

        protected override string GetSpecificDetails()
        {
            return $"antal sittplatser: {NumberOfSeats}";
        }
    }
}
