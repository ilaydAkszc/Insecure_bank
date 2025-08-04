using Insecure_Bank.Models.Model;

namespace Insecure_Bank.Models.ViewModels
{
    public class DashBoardViewModel
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
