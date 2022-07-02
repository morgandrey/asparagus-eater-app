namespace AsparagusEaterApp.DataAccess.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public DateTime UserEatLastDate { get; set; }
        public int UserTimesEat { get; set; }
    }
}
