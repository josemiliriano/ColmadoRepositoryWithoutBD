namespace ApiColmadoSinDB.Entities
{
    public class InventoryMovement
    {
        public int IdInventoryMovement { get; set; }
        public int IdProduct { get; set; }
        public int MovementTypediD { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public int? ProviderId { get; set; }
        public char IsDelete { get; set; } = '0';
    }
}
