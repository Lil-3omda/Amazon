namespace Amazon_API.Models.Entities.Products.Dtos
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }

        public List<CategoryResponseDto> SubCategories { get; set; }
    }
}
