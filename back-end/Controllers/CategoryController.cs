using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTO;
using back_end.Extension;
using back_end.Models;
using back_end.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = ConstantValue.Role_Admin)]
	public class CategoryController : ControllerBase
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

		public CategoryController(IUnitOfWork db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IEnumerable<CategoryDTO>> GetCategories()
		{
			IEnumerable<Category> ctgs = await _db.Categories.GetAll();
			return ctgs.Select(p => _mapper.Map<CategoryDTO>(p));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
		{
			Category ctg = await _db.Categories.GetFirstOrDefault(c => c.Id == id);

			if (ctg == null)
			{
				return NotFound();
			}

			return _mapper.Map<CategoryDTO>(ctg);
		}

		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(CategoryUpsertDTO cuDTO)
		{
			if (ModelState.IsValid)
			{
				Category ctg = _mapper.Map<Category>(cuDTO);
				await _db.Categories.Add(ctg);
				await _db.SaveChanges();
				return CreatedAtAction("GetCategory", new { id = ctg.Id }, ctg);
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, CategoryUpsertDTO cuDTO)
		{
			if (ModelState.IsValid)
			{
				bool ctgExists = await _db.Categories.IsExists(id);
				if (!ctgExists)
				{
					return NotFound();
				}
				Category ctg = _mapper.Map<Category>(cuDTO);
				ctg.Id = id;
				await _db.Categories.Update(ctg);
				await _db.SaveChanges();
				return NoContent();
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			bool ctgExists = await _db.Categories.IsExists(id);
			if (!ctgExists)
			{
				return NotFound();
			}
			await _db.Categories.Remove(id);
			await _db.SaveChanges();
			return NoContent();
		}
	}
}