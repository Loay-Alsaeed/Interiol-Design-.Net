namespace Backend_.Net.DTO
{
    public class DesignDTO
    {
        public required string Title { get; set; } 
        public required string SubTitle { get; set; }
        public required double SizeWidth { get; set; } 
        public required double SizeHeight { get; set; } 

        public Guid CategoryId { get; set; }
        public Guid StyleId { get; set; }
        public Guid DesignerId { get; set; }

        // صورة التخطيط
        public IFormFile LayoutImage { get; set; } = null!;

        // الصور الإضافية
        public List<IFormFile> Images { get; set; } = new();

        // ميزات التصميم
        public List<string> KeyFeatures { get; set; } = new();

        // المواد
        public List<MaterialDTO> Materials { get; set; } = new();

        public List<ColorDTO> Colors { get; set; } = new();

        public List<ConsiderationDTO> DesignConsiderations { get; set; } = new();

        public List<string> Descriptions { get; set; } = new();
    }
    
}
