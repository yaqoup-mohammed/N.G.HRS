namespace N.G.HRS.Models
{
    public class StatementOfEmployeeFiles
    {
        //يرتبط بجدول ملفات الموظفين والموظفين
        public int ID { get; set; }
        public Boolean  FilesStatus { get; set; }
        public string Notes { get; set; }
    }
}
