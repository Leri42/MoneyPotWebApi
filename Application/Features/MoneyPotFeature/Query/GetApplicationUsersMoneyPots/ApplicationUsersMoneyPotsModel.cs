namespace Application.Features.MoneyPotFeature.Query.GetApplicationUsersMoneyPots
{
    public class ApplicationUsersMoneyPotsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueLink { get; set; }
        public decimal TargetAmount { get; set; }
        public string Deadline { get; set; }
        public decimal CurrentAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
