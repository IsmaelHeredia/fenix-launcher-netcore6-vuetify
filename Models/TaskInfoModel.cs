namespace FenixLauncher.Models
{
    public class TaskInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string Command { get; set; }
        public int WindowStyle { get; set; }
        public int UAC { get; set; }
        public int IdProcess { get; set; }
    }
}
