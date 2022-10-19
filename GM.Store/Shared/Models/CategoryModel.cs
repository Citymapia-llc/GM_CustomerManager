using System.Collections.Generic;

namespace GM.Store.Shared.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public int? OrderIndex { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? MenuItem { get; set; }
        public HashSet<CategoryModel>? children { get;set;}
        public bool IsActive { get; set; } = false;
        public bool IsExpanded { get; set; } = false;
    }


    public class FeaturedCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? OrderIndex { get; set; }
        public List<ProductModel> ItemsList { get; set; }
        public bool IsVisible { get; set; } = true;
    }
    public class BaseCategory
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? MenuItem { get; set; }
        public int? OrderIndex { get; set; }
    }
}
