using Microsoft.AspNetCore.Authorization;
using back_end.Repository.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using back_end.Models;
using back_end.DTO;
using System.Linq;
using AutoMapper;

namespace back_end.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;

    public CartController(IUnitOfWork db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet("{UserId}")]
    public async Task<IEnumerable<ShoppingCartDTO>> GetCarts(string UserId)
    {
        IEnumerable<ShoppingCart> sc = await _db.ShoppingCarts.GetAll(c => c.UserId == UserId,
                                                    includeProperties: "Product");
        return sc.Select(s => _mapper.Map<ShoppingCartDTO>(s));
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(ShoppingCartAddDTO scaDTO)
    {
        if (ModelState.IsValid)
        {
            ShoppingCart scFromDB = await _db.ShoppingCarts.GetFirstOrDefault(
                c => c.UserId == scaDTO.UserId && c.ProductId == scaDTO.ProductId,
                includeProperties: "Product");

            if (scFromDB == null)
            {
                ShoppingCart newSC = _mapper.Map<ShoppingCart>(scaDTO);
                await _db.ShoppingCarts.Add(newSC);
            }
            else
            {
                scFromDB.Count += scaDTO.Count;
            }
            await _db.SaveChanges();
            return StatusCode(201);
        }
        return BadRequest();
    }

    [HttpPut("plus/{cartId}")]
    public async Task<IActionResult> Plus(int cartId)
    {
        ShoppingCart cart = await _db
            .ShoppingCarts.GetFirstOrDefault(c => c.Id == cartId);
        if (cart == null) return NotFound();
        cart.Count++;
        await _db.SaveChanges();
        return NoContent();
    }

    [HttpPut("minus/{cartId}")]
    public async Task<IActionResult> Minus(int cartId)
    {
        ShoppingCart cart = await _db
            .ShoppingCarts.GetFirstOrDefault(c => c.Id == cartId);
        if (cart == null) return NotFound();
        if (cart.Count == 1)
        {
            await _db.ShoppingCarts.Remove(cart);
        }
        else
        {
            cart.Count--;
        }

        await _db.SaveChanges();
        return NoContent();
    }

    [HttpDelete("remove/{cartId}")]
    public async Task<IActionResult> Remove(int cartId)
    {
        ShoppingCart cart = await _db
            .ShoppingCarts.GetFirstOrDefault(c => c.Id == cartId);
        if (cart == null) return NotFound();
        await _db.ShoppingCarts.Remove(cart);
        await _db.SaveChanges();
        return NoContent();
    }
}
