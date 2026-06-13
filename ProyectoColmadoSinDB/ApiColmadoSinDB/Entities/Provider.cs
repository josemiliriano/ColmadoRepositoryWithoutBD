namespace ApiColmadoSinDB.Entities
{
    public class Provider
    {
        public int IdProvider { get; set; }
        public string ProviderName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public char IsDelete { get; set; } = '0';
    }
}
