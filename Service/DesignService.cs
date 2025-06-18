using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class DesignService : IDesignService
    {
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;
        private readonly ILayoutImageService _layoutService;
        private readonly IKeyFeatureService _keyFeatureService;
        private readonly IMaterialService _materialService;
        private readonly IColorService _colorService;
        private readonly IConsiderationService _considerationService;
        private readonly IDescriptionService _descriptionService;
        private readonly IDesignConceptService _designConceptService;

        public DesignService
            (
            AppDbContext context,
            IImageService imageService,
            ILayoutImageService layoutService,
            IKeyFeatureService keyFeatureService,
            IMaterialService materialService,
            IColorService colorService,
            IConsiderationService considerationService,
            IDescriptionService descriptionService,
            IDesignConceptService designConceptService
            )
        {
            _context = context;
            _imageService = imageService;
            _layoutService = layoutService;
            _keyFeatureService = keyFeatureService;
            _materialService = materialService;
            _colorService = colorService;
            _considerationService = considerationService;
            _descriptionService = descriptionService;
            _designConceptService = designConceptService;
        }

        public async Task<Design?> CreateDesignWithDetailsAsync(AddDesignDTO dto)
        {
            // 1. تصميم أساسي
            var design = new Design
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                SubTitle = dto.SubTitle,
                SizeWidth = dto.SizeWidth,
                SizeHeight = dto.SizeHeight,
                CategoryId = dto.CategoryId,
                StyleId = dto.StyleId,
                DesignerId = dto.DesignerId,
                Likes = 0
            };

            await _context.Designs.AddAsync(design);
            await _context.SaveChangesAsync(); // حفظ أولي للحصول على Design.Id

            // 2. …
            // – LayoutImage
            if (dto.LayoutImage != null)
            { 
                var layoutDto = new LayoutImageDTO { DesignId = design.Id, Image = dto.LayoutImage };
                var layout = await _layoutService.AddlayoutImageAsync(layoutDto);
                //design.LayoutImageId = layout!.Id;
                _context.Designs.Update(design);
                await _context.SaveChangesAsync();
            }




            // – Images
            if (dto.Images != null)
            { 
                foreach (var img in dto.Images)
                {
                    var imgDto = new ImageDTO { DesignId = design.Id, Image = img };
                    await _imageService.AddImageAsync(imgDto);
                }
            }


            // – KeyFeatures
            if (dto.KeyFeatures != null)
            { 
                foreach (var content in dto.KeyFeatures)
                {
                    await _keyFeatureService.AddKeyFeatureAsync(new KeyFeatureDTO { Content = content, DesignId = design.Id });
                }
            }

            // – Materials
           
            
                foreach (var mat in dto.Materials)
                {
                    var matDto = new MaterialDTO
                    {
                        DesignId = design.Id,
                        Name = mat.Name,
                        Description = mat.Description,
                        Supplier = mat.Supplier,
                        Application = mat.Application,
                        Sustainability = mat.Sustainability,
                        Maintenance = mat.Maintenance,
                        Image = mat.Image
                    };
                    await _materialService.Create(matDto);
                }
            

            // - Colors
            
             
                foreach (var col in dto.Colors)
                {
                    var color = new ColorDTO
                    {
                        DesignId = design.Id,
                        Name = col.Name,
                        ColorNumber = col.ColorNumber,
                        application = col.application
                    };
                    await _colorService.AddColorAsync(color);
                }
            

            // - DesignConsiderations
           
                foreach (var con in dto.DesignConsiderations)
                {
                    var consideration = new ConsiderationDTO
                    {
                        DesignId = design.Id,
                        Title = con.Title,
                        Description = con.Description
                    };
                    await _considerationService.AddConsiderationAsync(consideration);
                }
            

            // - Descriptions
           
                foreach (var des in dto.Descriptions)
                {
                    var description = new DescriptionDTO
                    {
                        DesignId =design.Id,
                        Content = des,
                    };
                    await _descriptionService.AddDescriptionAsync(description);
                }
            

            return design;
        }

        public async Task<Design?> GetDesignByDesignId(Guid designId)
        {
            var design = await _context.Designs.FindAsync(designId);

            if (design is null) return null;

            var images = await _imageService.GetAllImagesAsync(designId);
            design.Images = images;

            var colors = await _colorService.GetColorsByDesignIdAsync(designId);
            design.Colors = colors;

            var MaterialList = await _materialService.GetAllByDesignId(designId);
            design.DesignMaterials = MaterialList;

            var descriptions = await _descriptionService.GetDescriptionBtDesignIdAsync(designId);
            design.Descriptions = descriptions;

            var keyFeature = await _keyFeatureService.GetAllKeyFeatureByDesignIdAsync(designId);
            design.KeyFeatures = keyFeature;

            var consideration = await _considerationService.GetAllConsiderationByDesignIdAsync(designId);
            design.DesignConsiderations = consideration;

            var concept = await _designConceptService.GetDesignConceptByDesignIdAsync(design.Id);
            design.Concepts = concept;

            return design;
        }

        public async Task<List<Design>> GetAllDesigns()
        {
            var designs = await _context.Designs.ToListAsync();
            if (designs != null)
            {
                foreach (var design in designs)
                {
                    var designImages = await _imageService.GetAllImagesAsync(design.Id);
                    design.Images = designImages!;

                    var colors = await _colorService.GetColorsByDesignIdAsync(design.Id);
                    design.Colors = colors;

                    var MaterialList = await _materialService.GetAllByDesignId(design.Id);
                    design.DesignMaterials = MaterialList!;

                    var descriptions = await _descriptionService.GetDescriptionBtDesignIdAsync(design.Id);
                    design.Descriptions = descriptions!;

                    var keyFeature = await _keyFeatureService.GetAllKeyFeatureByDesignIdAsync(design.Id);
                    design.KeyFeatures = keyFeature;

                    var consideration = await _considerationService.GetAllConsiderationByDesignIdAsync(design.Id);
                    design.DesignConsiderations = consideration;

                    var concept = await _designConceptService.GetDesignConceptByDesignIdAsync(design.Id);
                    design.Concepts = concept;
                }
            }
            return designs;
        }

        public async Task<bool> DeleteDesign(Guid designId)
        {
            // colors
            var colorList = await _colorService.GetColorsByDesignIdAsync(designId);
            if (colorList != null)
            {
                foreach (var col in colorList)
                {
                    await _colorService.DeleteColorAsync(col.Id);
                }
            }

            // image
            var imageList = await _imageService.GetAllImagesAsync(designId);
            if (imageList != null)
            {
                foreach (var img in imageList)
                {
                    await _imageService.DeleteImage(img.Id);
                }
            }

            // layout image

            await _layoutService.DeleteLayoutImage(designId);

            // key feature
            var keyList = await _keyFeatureService.GetAllKeyFeatureByDesignIdAsync(designId);
            if (keyList != null)
            { 
                foreach(var key in keyList)
                {
                    await _keyFeatureService.DeleteKeyFeatureAsync(key.Id);
                }
            }

            // description


            var descList = await _descriptionService.GetDescriptionBtDesignIdAsync(designId);
            if (descList != null)
            {
                foreach (var des in descList)
                {
                    await _descriptionService.DeleteDescriptionAsync(des.Id);
                }
            }

            // consideration

            var consideration = await _considerationService.GetAllConsiderationByDesignIdAsync(designId);
            if (consideration != null)
            {
                foreach (var con in consideration)
                {
                    await _considerationService.DeleteConsiderationAsync(con.Id);
                }
            }

            // Material

            var materrilalList = await _materialService.GetAllByDesignId(designId);
            if (materrilalList != null)
            {
                foreach (var material in materrilalList)
                {
                    await _materialService.Delete(material.Id);
                }
            }

            var concept = await _designConceptService.GetDesignConceptByDesignIdAsync(designId);
            if (concept != null)
            {
                foreach (var con in concept)
                {
                    await _designConceptService.DeleteDesignConceptAsync(con.Id);
                }
            }

            var design = await _context.Designs.FindAsync(designId);
            if (design != null)
            {
                _context.Designs.Remove(design);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}

