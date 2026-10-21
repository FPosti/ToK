namespace GarageArrays
{
    internal sealed class Airplane : Vehicle
    {
        public int NumberOfEngines { get; }

        protected override string VehicleTypeName => "Flygplan";

        public Airplane(string registrationNumber, string color,
            int numberOfWheels, int numberOfEngines)
            : base(registrationNumber, color, numberOfWheels)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfEngines);

            NumberOfEngines = numberOfEngines;
        }

        protected override string GetSpecificDetails()
        {
            return $"antal motorer: {NumberOfEngines}";
        }
    }
}
