using abm_productos.Entities;
using abm_productos.Models.DTOs.Requests;
using abm_productos.Models.DTOs.Responses;
using abm_productos.Repositories.Implementations;

namespace abm_productos.Services.Implementations;

public class ProductService
{
    private ProductRepository _repository = new ProductRepository();

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

    public ProductForReadDto CreateProduct(ProductForCreateDto dto)
    {
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

    // mapeo a mano de entidad a DTO, se usa en varios metodos
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
