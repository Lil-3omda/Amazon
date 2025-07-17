using Amazon_API.Data;
using Amazon_API.Models.Entities.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amazon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var category = _context.Categories.Include(c => c.ParentCategory).Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ParentCategoryId = c.ParentCategoryId != null ? c.ParentCategoryId : null,
                ParentCategoryName = c.ParentCategory != null ? c.ParentCategory.Name : null
            }).ToListAsync();
            if (category == null) {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CategoryDTO>> GetById(int id) {
            var category = _context.Categories.Include(c => c.ParentCategory).FirstOrDefault(c=>c.Id==id);

            if (category == null) { 
            return NotFound();
            }
            var dtos = new List<CategoryDTO>();

            return Ok(dtos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateCategoryDTO dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            category.Name = dto.Name;
            category.ParentCategoryId = dto.ParentCategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                ParentCategoryId = categoryDTO.ParentCategoryId,
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Created(); //will be CaretedAtAciton()
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var category=await  _context.Categories.Include(c=> c.SubCategories).FirstOrDefaultAsync(category=>category.Id==id);

            if (category != null) {
                return NotFound();
            }

            if (category.SubCategories.Any())
            {
                return BadRequest("You Cant Delete Category With Subcategories.");
            }
            _context.Categories.Remove(category);
           
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}