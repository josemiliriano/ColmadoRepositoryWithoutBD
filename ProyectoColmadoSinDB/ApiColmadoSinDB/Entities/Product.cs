namespace ApiColmadoSinDB.Entities
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string QualityPerUnit { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public char IsDelete { get; set; } = '0';
    }
}
