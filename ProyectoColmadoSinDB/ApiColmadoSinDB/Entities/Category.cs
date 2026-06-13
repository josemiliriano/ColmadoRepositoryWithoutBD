namespace ApiColmadoSinDB.Entities
{
    public class Category
    {
        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public char IsDelete { get; set; } = '0';
    }
}
