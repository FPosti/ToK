namespace GarageArrays
{
    internal sealed class Car : Vehicle
    {
        public string FuelType { get; }

        protected override string VehicleTypeName => "Bil";

        public Car(string registrationNumber, string color,
            int numberOfWheels, string fuelType)
            : base(registrationNumber, color, numberOfWheels)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fuelType);

            FuelType = fuelType.Trim();
        }

        protected override string GetSpecificDetails()
        {
            return $"bränsle: {FuelType}";
        }
    }
}
