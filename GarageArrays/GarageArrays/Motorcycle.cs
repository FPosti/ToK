namespace GarageArrays
{
    internal sealed class Motorcycle : Vehicle
    {
        public int CylinderVolumeCm3 { get; }

        protected override string VehicleTypeName => "Motorcykel";

        public Motorcycle(string registrationNumber, string color,
            int numberOfWheels, int cylinderVolumeCm3)
            : base(registrationNumber, color, numberOfWheels)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cylinderVolumeCm3);

            CylinderVolumeCm3 = cylinderVolumeCm3;
        }

        protected override string GetSpecificDetails()
        {
            return $"cylindervolym: {CylinderVolumeCm3} cm3";
        }
    }
}
