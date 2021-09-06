using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTO;
using back_end.Extension;
using back_end.Models;
using back_end.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = ConstantValue.Role_Admin)]
	public class ProductController : ControllerBase
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;

		public ProductController(IUnitOfWork db, IMapper mapper, IWebHostEnvironment env)
		{
			_db = db;
			_mapper = mapper;
			_env = env;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IEnumerable<ProductDTO>> GetProducts()
		{
			IEnumerable<Product> prds = await _db.Products.GetAll(includeProperties: "Category");
			return prds.Select(p => _mapper.Map<ProductDTO>(p));
		}

		[HttpGet("{id}")]
		[AllowAnonymous]
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

		[HttpPost("SaveFile")]
		public async Task<IActionResult> SaveFile()
		{
			try
			{
				IFormCollection httpRequest = Request.Form;
				IFormFile postedFile = httpRequest.Files[0];
				string filename = postedFile.FileName;
				string physicalPath = _env.ContentRootPath + "/Photos/" + filename;

				using (var stream = new FileStream(physicalPath, FileMode.Create))
				{
					await postedFile.CopyToAsync(stream);
				}

				return Ok(filename);
			}
			catch (Exception)
			{
				return Ok("NewProduct.jpg");
			}
		}
	}
}