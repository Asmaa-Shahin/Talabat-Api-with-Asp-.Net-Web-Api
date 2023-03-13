using AutoMapper;
using BLL.Interface;
using BLL.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Errors;
using Talabat.API.Helpers;
using Talabat.BLL.Specifications;
using Talabat.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepo<Product> genericRepo;
        private readonly IGenericRepo<ProductBrand> _productBrandsRepo;
        private readonly IGenericRepo<ProductType> _productTypesRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepo<Product> genericRepo, IGenericRepo<ProductBrand> productBrandRepo, IGenericRepo<ProductType> productTypesRepo, IMapper mapper )
        {
            this.genericRepo = genericRepo;
            _productBrandsRepo = productBrandRepo;
            _productTypesRepo = productTypesRepo;
            this.mapper = mapper;
        } 


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await genericRepo.GetCountAsync(countSpec);

            var products = await genericRepo.GetAllWithSpecAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);

            return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await genericRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(mapper.Map<Product,ProductDTO>(product));


        }
   

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _productBrandsRepo.GetAllAsync());
        }
     
        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _productTypesRepo.GetAllAsync());
        }
    }
}
