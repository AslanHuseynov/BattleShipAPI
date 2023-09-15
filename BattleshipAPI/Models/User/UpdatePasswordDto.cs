namespace BattleshipAPI.Models.User
{
    public class UpdatePasswordDto
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
