namespace Ambev.DeveloperEvaluation.Application.Products.Dtos
{
    public class ProductDto
    {
        public ProductDto(int id, string? title, decimal price, string? description, string? category, string? image, RatingDto rating)
        {
            Id = id;
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
            Rating = rating;
        }

        public string? Category { get; set; }
        public string? Description { get; set; }
        public int Id { get; set; }

        public string? Image { get; set; }
        public decimal Price { get; set; }
        public RatingDto Rating { get; set; }
        public string? Title { get; set; }
    }
}
