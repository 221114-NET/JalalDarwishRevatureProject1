namespace ERSModelsLayer
{
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
        public int UserID {get; set;}
        public string ReimburseType {get; set;}
        public decimal DollarAmount {get; set;} //using decimal to match database data type
        public string? Description {get; set;}
        public ReimbursementStatus ReimburseStatus {get; set;}

        public Reimbursement(int userID = -1, string reimburseType = "default", decimal dollarAmount = 0.0M, string? description = "default",
         ReimbursementStatus reimburseStatus = ReimbursementStatus.PENDING)
        {
            UserID = userID;
            ReimburseType = reimburseType;
            DollarAmount = dollarAmount;
            Description = description;
            ReimburseStatus = reimburseStatus;

        }
    }
}