using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTO;
using back_end.Models;
using back_end.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

		public ProductController(IUnitOfWork db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<ProductDTO>> GetProducts()
		{
			IEnumerable<Product> prds = await _db.Products.GetAll(includeProperties: "Category");
			return prds.Select(p => _mapper.Map<ProductDTO>(p));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTO>> GetProduct(int id)
		{
			Product prd = await _db.Products.GetFirstOrDefault(c => c.Id == id,
												includeProperties: "Category");

			if (prd == null)
			{
				return NotFound();
			}

			return _mapper.Map<ProductDTO>(prd);
		}

		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(ProductUpsertDTO puDTO)
		{
			if (ModelState.IsValid)
			{
				Product prd = _mapper.Map<Product>(puDTO);
				await _db.Products.Add(prd);
				await _db.SaveChanges();
				return CreatedAtAction("GetProduct", new { id = prd.Id }, prd);
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, ProductUpsertDTO puDTO)
		{
			if (ModelState.IsValid)
			{
				bool prdExists = await _db.Products.IsExists(id);
				if (!prdExists)
				{
					return NotFound();
				}
				Product prd = _mapper.Map<Product>(puDTO);
				prd.Id = id;
				await _db.Products.Update(prd);
				await _db.SaveChanges();
				return NoContent();
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			bool prdExists = await _db.Products.IsExists(id);
			if (!prdExists)
			{
				return NotFound();
			}
			await _db.Products.Remove(id);
			await _db.SaveChanges();
			return NoContent();
		}
	}
}