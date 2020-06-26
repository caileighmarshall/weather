namespace weather.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PointModel
    {
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        public string gridId { get; set; }
        public string gridX { get; set; }
        public string gridY { get; set; }
        public string forecastUrl { get; set; }
        public string forecastHourlyUrl { get; set; }
    }
}
