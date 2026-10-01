using abm_productos.Entities;
using abm_productos.Models.DTOs.Requests;
using abm_productos.Models.DTOs.Responses;
using abm_productos.Repositories.Interfaces;
using abm_productos.Services.Interfaces;

namespace abm_productos.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<ProductForReadDto> GetAllProducts()
    {
        List<Product> products = _repository.GetAllProducts();
        List<ProductForReadDto> result = new List<ProductForReadDto>();

        foreach (Product product in products)
        {
            result.Add(MapToReadDto(product));
        }

        return result;
    }

    public ProductForReadDto? GetProductById(int id)
    {
        Product? product = _repository.GetProductById(id);

        if (product == null)
        {
            return null;
        }

        return MapToReadDto(product);
    }

    public ProductForReadDto? CreateProduct(ProductForCreateDto dto)
    {
        bool nameExists = _repository.GetAllProducts()
            .Any(p => string.Equals(p.Name, dto.Name, StringComparison.OrdinalIgnoreCase));

        if (nameExists)
        {
            return null;
        }

        Product product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        _repository.AddProduct(product);

        return MapToReadDto(product);
    }

    public void UpdateProduct(int id, ProductForUpdateDto dto)
    {
        Product? product = _repository.GetProductById(id);

        if (product == null)
        {
            return;
        }

        product.Name = dto.Name;
        product.Price = dto.Price;

        _repository.UpdateProduct(product);
    }

    public void DeleteProduct(int id)
    {
        Product? product = _repository.GetProductById(id);

        if (product == null)
        {
            return;
        }

        _repository.DeleteProduct(product);
    }

    public List<ProductForReadDto> SearchProductsByName(string name)
    {
        List<Product> products = _repository.SearchProductsByName(name);
        List<ProductForReadDto> result = new List<ProductForReadDto>();

        foreach (Product product in products)
        {
            result.Add(MapToReadDto(product));
        }

        return result;
    }

    public ProductStatsDto GetStats()
    {
        List<Product> products = _repository.GetAllProducts();

        if (products.Count == 0)
        {
            return new ProductStatsDto
            {
                Total = 0,
                AveragePrice = 0,
                MostExpensiveName = string.Empty
            };
        }

        return new ProductStatsDto
        {
            Total = products.Count,
            AveragePrice = products.Average(p => p.Price),
            MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name
        };
    }

    private ProductForReadDto MapToReadDto(Product product)
    {
        return new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
