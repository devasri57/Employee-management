namespace webApi1.Models
{
    public class EmployeeModels
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeSalary { get; set; }
        public int EmployeeAge { get; set; }

        public EmployeeModels() { }
        public EmployeeModels(int EmployeeId, string EmployeeName, int EmployeeSalary, int EmployeeAge)
        {
            this.EmployeeId = EmployeeId;
            this.EmployeeName = EmployeeName;
            this.EmployeeSalary = EmployeeSalary;
            this.EmployeeAge =  EmployeeAge;
        }
    }
}
