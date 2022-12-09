namespace ERSModelsLayer
{
    /// <summary>
    /// Enum for reimbursment type: Travel=0, Food=1, Car Rental=2, Other=3
    /// </summary>
    public enum ReimbursementType:int
    {
        TRAVEL = 0,
        FOOD,
        CARRENTAL,
        OTHER
    }
    /// <summary>
    /// Enum for reimbursment status: Pending=1, Approved=2, Rejected=3
    /// </summary>
    public enum ReimbursementStatus:int
    {
        PENDING = 1,
        APPROVED,
        REJECTED
    }

    public class Reimbursement
    {
        public string? EmployeeMail {get; set;}
        public ReimbursementType ReimburseType {get; set;}
        public double DollarAmount {get; set;}
        public string? Description {get; set;}
        public ReimbursementStatus ReimburseStatus {get; set;}

        public Reimbursement(string? employeeMail = "none", ReimbursementType reimburseType = ReimbursementType.OTHER, double dollarAmount = 0.0, string? description = "default",
         ReimbursementStatus reimburseStatus = ReimbursementStatus.PENDING)
        {
            EmployeeMail = employeeMail;
            ReimburseType = reimburseType;
            DollarAmount = dollarAmount;
            Description = description;
            ReimburseStatus = reimburseStatus;
        }
    }
}