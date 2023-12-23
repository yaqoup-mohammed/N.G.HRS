namespace N.G.HRS.Models
{
    public class AllowancesAndDiscounts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Boolean Taxable { get; set; }
        public Boolean AddedToAllEmployees { get; set; }
        public Boolean CumulativeAllowance { get; set; }
        public Boolean SubjectToInsurance { get; set; }
        public Decimal Amount { get; set; }
        public string Notes { get; set; }

    }
}
