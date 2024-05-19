namespace FenixLauncher.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        public string CurrentUser { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
    }
}
