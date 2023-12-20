using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class DllFiles
    {
        [Key]
        public int DllID { get; set; }
        public string DllName { get; set; } = "";
        public string DllPath { get; set; } = "";
        public string DllType { get; set; } = "";
    }
}