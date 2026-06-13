namespace ApiColmadoSinDB.Entities
{
    public class MovementType
    {
        public int IdMovementType { get; set; }
        public string Description { get; set; }
        public char IsDelete { get; set; } = '0';
    }
}
