using abm_productos.Models.DTOs.Requests;
using abm_productos.Models.DTOs.Responses;

namespace abm_productos.Services.Interfaces;

public interface IProductService
{
    List<ProductForReadDto> GetAllProducts();
    ProductForReadDto? GetProductById(int id);
    ProductForReadDto CreateProduct(ProductForCreateDto dto);
    void UpdateProduct(int id, ProductForUpdateDto dto);
    void DeleteProduct(int id);
}
