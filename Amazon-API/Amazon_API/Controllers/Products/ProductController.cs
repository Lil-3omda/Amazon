using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon_API.Interfaces;
using AutoMapper;
using Amazon_API.Models.Entities.Products.Dtos;
using Amazon_API.Models.Entities.Products;
using Amazon_API.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using Amazon_API.Models.Entities.Reviews;
using Amazon_API.Services.Interfaces;
namespace Amazon_API.Controllers.Products
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IProductService _productService;
        IProductImageService _productimageservice;
        IReviewService _reviewService;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper,IProductService productService,IProductImageService productImageService,IReviewService reviewService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = productService;
            _productimageservice = productImageService;
            _reviewService = reviewService;
        }

        //  Get ALL Products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts( [FromQuery]int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var products = await _unitOfWork.Products.GetPagedProductsAsync(pageNumber, pageSize);
            var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(result);
        }
        // Get Product By ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetproductById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var result = _mapper.Map<ProductResponseDto>(product);
                 return Ok(result);
        }

        // Add Products
        [HttpPost]
        public async Task<IActionResult> AddProducts([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(dto);

            var sellerid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (sellerid == null)
                return Unauthorized("you must be logged in ! ");

            product.SellerId = sellerid;

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<ProductResponseDto>(product);
            return CreatedAtAction(nameof(GetproductById), new { id = product.Id }, result);
        }

        // Search Products by Title
        [HttpGet("search")]
        public async Task<IActionResult> Search(string title)
        {
            var products = await _unitOfWork.Products.SearchProductsByTitleAsync(title);
            var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(result);
        }
        //  Filter Products
        [HttpGet("filter")]
        public async Task<IActionResult> Filter(decimal?minprice,decimal?maxprice,string sortBy= "price_asc")
        {
            var products = await _unitOfWork.Products.FilterProductsAsync(minprice, maxprice, sortBy);
            var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(result);
        }
        //  Get by Category
        [HttpGet("category/{categoryid}")]
        public async Task<IActionResult> GetByCategory(int categoryid)
        {
            var products = await _unitOfWork.Products.GetProductsByCategoryIdAsync(categoryid);
            var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(result);
        }
        // Get by Seller
        [HttpGet("seller/{sellerid}")]
        public async Task<IActionResult> GetBySeller(string sellerid)
        {
            var products = await _unitOfWork.Products.GetProductsBySellerIdAsync(sellerid);
            var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(result);
        }
        //  Get Top Selling
        [HttpGet("TopSelling")]
        public async Task<IActionResult> GetTopSelling(int count=5)
        {
            var products = await _unitOfWork.Products.GetTopSellingProductsAsync(count);
            var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(result);
        }
        //Get Related Products
        [HttpGet("related/{productid}")]
        public async Task<IActionResult> GetRelated(int productid)
        {
            var products = await _unitOfWork.Products.GetRelatedProductsAsync(productid);
            var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(result);
        }

     

        //Add Images to product by id 
        [HttpPost("{productid}/images")]
        public async Task<IActionResult> AddImagesToProduct(int productid, ProductImageCreateDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productid);
            if (product == null)
                return NotFound("product not found ! ");

            foreach(var urlimg in dto.ImageUrls)
            {
                product.Images.Add(new ProductImage { ImageUrl = urlimg, ProductId = productid });
            }
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();
            return Ok("Images added successfull");
        }


        //delete Images from product by id
        [HttpDelete("{productid}/images/{imageid}")]
        public async Task<IActionResult> DeleteImage(int productid, int imageid)
        {
            var img = await _productimageservice.SoftDeleteImageAsync(productid, imageid);
            if (!img)
                return NotFound("image not found!");

            return Ok("image deleted Successfully");
        }

        // Update images
        [HttpPut("{productid}/images")]
        public async Task<IActionResult> UpdateImage(int productid, UpdateProductImageDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productid);
            if (product == null)
                return NotFound("product not found!");

            //clear old image 
            product.Images.Clear();
            //add new image
            foreach (var urlimg in dto.ImageUrls)
            {
                product.Images.Add(new ProductImage { ImageUrl = urlimg, ProductId = productid });
            }
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();

            return Ok("Images updated successfully");
        }


        // Add Reviews to product
        //[HttpPost("{productid}/reviews")]
        //public async Task<IActionResult> AddReviewToProduct(int productid, CreateReviewDto dto)
        //{
        //    var product = await _unitOfWork.Products.GetByIdAsync(productid);
        //    if (product == null)
        //        return NotFound("Product not found");

        //    var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userid == null)
        //        return Unauthorized();

        //    var result = new Review
        //    {
        //        ProductId = productid,
        //        UserId = userid,
        //        Rating = dto.Rating,
        //        Comment = dto.Comment,
        //        CreatedAt = DateTime.UtcNow

        //    };
        //    await _unitOfWork.Reviews.AddAsync(result);
        //    await _unitOfWork.SaveAsync();

        //    return Ok("Review added successfully.");
        //}


        // Update product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody]UpdateProductDto dto)
        {
            if (id != dto.Id)
                return BadRequest("id in url notequal dto id");

            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound("product not found!");

            var sellerid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (sellerid == null || product.SellerId != sellerid)
                return Unauthorized("You can only update your own products!");

            _mapper.Map(dto, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();

            return Ok("Product updated successfully");
        }

      

        // update reviews
        [HttpPut("{productid}/reviews/{reviewid}")]
        public async Task<IActionResult> UpdateReview(int productid,int reviewid,UpdateReviewDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productid);
            if (product == null)
                return NotFound("product not found! ");

            var review = product.Reviews.FirstOrDefault(p => p.Id == reviewid);
            if (review == null)
                return NotFound("Review not found!");

            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userid == null || review.UserId != userid)
                return Unauthorized("You can only update your own review");

            _mapper.Map(dto, review);

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();

            return Ok("Review updated successfully");
        }

        //delete product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.SoftDeleteProductAsync(id);
            if (!result)
                return NotFound("Product not found.");

            return Ok("Product deleted successfully.");
        }
      

        //delete review from peoductByid
        [HttpDelete("{productid}/reviews/{reviewid}")]
        public async Task<IActionResult> DeleteReview(int productid,int reviewid)
        {
            var review = await _reviewService.SoftDeleteReviewAsync(productid,reviewid);
            if (!review)
                return NotFound("review not found!");

            return Ok("review deleted successfully");
        }
    }
}
