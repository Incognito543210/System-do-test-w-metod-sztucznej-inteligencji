using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ParamInfo
    {
        [Key]
        public string Name { get;  set; }
        public string Description { get;  set; }
        public double UpperBoundary { get; set; }
        public double Step { get; set; }
        public double LowerBoundary { get; set; }
    }
}
