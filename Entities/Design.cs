namespace Backend_.Net.Entities
{
    public class Design
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Title { get; set; }
        public required string SubTitle { get; set; }
        public double SizeWidth { get; set; }
        public double SizeHeight { get; set; }

        public List<Image> Images { get; set; } = [];

        //public Guid LayoutImageId { get; set; } = Guid.Empty;
        //public LayoutImage LayoutImage { get; set; } = default!;


        public string LayoutImageUrl { get; set; } = string.Empty;

        public Guid StyleId { get; set; }
        public Style DesignStyle { get; set; } = default!;

        public Guid CategoryId { get; set; }
        public Category DesignCategory { get; set; } = default!;

        public Guid DesignerId { get; set; }
        public Designer Designer { get; set; } = default!;

        public int Likes { get; set; } = 0;

        public List<DesignComment> DesignComments { get; set; } = [];
        public List<DesignDescription> Descriptions { get; set; } = [];
        public List<KeyFeature> KeyFeatures { get; set; } = [];
        public List<Consideration> DesignConsiderations { get; set; } = [];
        public List<Material> DesignMaterials { get; set; } = [];
        public List<Color> Colors { get; set; } = [];
        public List<DesignConcept> Concepts { get; set; } = [];
    }

}
