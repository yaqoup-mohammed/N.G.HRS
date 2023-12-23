namespace N.G.HRS.Models
{
    public class Archives
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
    }
}
