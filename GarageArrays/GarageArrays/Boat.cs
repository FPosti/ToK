namespace GarageArrays
{
    internal sealed class Boat : Vehicle
    {
        public decimal LengthInMeters { get; }

        protected override string VehicleTypeName => "Båt";

        public Boat(string registrationNumber, string color,
            int numberOfWheels, decimal lengthInMeters)
            : base(registrationNumber, color, numberOfWheels)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(lengthInMeters);

            LengthInMeters = lengthInMeters;
        }

        protected override string GetSpecificDetails()
        {
            return $"längd: {LengthInMeters} m";
        }
    }
}
