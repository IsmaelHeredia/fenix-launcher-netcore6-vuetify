namespace FenixLauncher.Models
{
    public class ChangeUsernameModel
    {
        public int Id { get; set; }
        public string CurrentUser { get; set; }
        public string NewUsername { get; set; }
        public string CurrentPassword { get; set; }
    }
}
