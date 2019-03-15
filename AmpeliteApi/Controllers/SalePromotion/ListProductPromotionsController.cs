using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Data;
using AmpeliteApi.Models;

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Produces("application/json")]
    [Route("api/SalePromotion/[controller]")]
    public class ListProductPromotionsController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public ListProductPromotionsController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/ListProductPromotions
        [HttpGet]
        public IEnumerable<ListProductPromotion> GetListProductPromotion()
        {
            return _context.ListProductPromotion;
        }

        // GET: api/ListProductPromotions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetListProductPromotion([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listProductPromotion = await _context.ListProductPromotion.SingleOrDefaultAsync(m => m.GoodId == id);

            if (listProductPromotion == null)
            {
                return NotFound();
            }

            return Ok(listProductPromotion);
        }

        // PUT: api/ListProductPromotions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListProductPromotion([FromRoute] string id, [FromBody] ListProductPromotion listProductPromotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listProductPromotion.GoodId)
            {
                return BadRequest();
            }

            _context.Entry(listProductPromotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListProductPromotionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ListProductPromotions
        [HttpPost]
        public async Task<IActionResult> PostListProductPromotion([FromBody] ListProductPromotion listProductPromotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ListProductPromotion.Add(listProductPromotion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ListProductPromotionExists(listProductPromotion.GoodId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetListProductPromotion", new { id = listProductPromotion.GoodId }, listProductPromotion);
        }

        // DELETE: api/ListProductPromotions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListProductPromotion([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listProductPromotion = await _context.ListProductPromotion.SingleOrDefaultAsync(m => m.GoodId == id);
            if (listProductPromotion == null)
            {
                return NotFound();
            }

            _context.ListProductPromotion.Remove(listProductPromotion);
            await _context.SaveChangesAsync();

            return Ok(listProductPromotion);
        }

        private bool ListProductPromotionExists(string id)
        {
            return _context.ListProductPromotion.Any(e => e.GoodId == id);
        }
    }
}