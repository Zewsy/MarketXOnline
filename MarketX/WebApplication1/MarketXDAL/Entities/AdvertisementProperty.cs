namespace MarketX.DAL.Entities
{
    public class AdvertisementProperty
    {
        public AdvertisementProperty(string value)
        {
            Value = value;
        }
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public int PropertyId { get; set; }
        public virtual Advertisement Advertisement { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
        public string Value { get; set; }
    }
}