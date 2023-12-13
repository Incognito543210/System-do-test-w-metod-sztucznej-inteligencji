using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class DllFiles
    {
        [Key]
        public int DllID { get; set; }
        public string DllPath { get; set; } = "";
    }
}
