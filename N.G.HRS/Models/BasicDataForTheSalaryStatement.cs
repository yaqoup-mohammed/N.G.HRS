namespace N.G.HRS.Models
{
    public class BasicDataForTheSalaryStatement
    {
        public int Id { get; set; }
        public Boolean HealthInsuranceIncluded { get; set; }
        public Boolean RetirementInsuranceIncluded { get; set; }
        public Boolean IncludesTheWorkShareInRetirementInsurance { get; set; }
        public Boolean IncludesTaxCalculation { get; set; }
        public Boolean TaxFrom { get; set; }
        public Boolean AllowancesIncluded { get; set; }
        public Boolean IncludesAdditionalData { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string Notes { get; set; }
        public int Percentage { get; set; } 
        public int PercentageOnEmployee { get; set; }
        public int PercentageOnCompany { get; set; }



    }
}
