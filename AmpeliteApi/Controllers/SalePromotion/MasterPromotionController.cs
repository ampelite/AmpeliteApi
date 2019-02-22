using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AmpeliteApi.Models;
using AmpeliteApi.Data;
using AmpeliteApi.Services.SalePromotion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Route("api/SalePromotion/[controller]")]
    public class MasterPromotionController : Controller
    {
        private db_AmpeliteContext ctx;
        private ICodePromotionService iCodeProService;

        public MasterPromotionController (
            db_AmpeliteContext context,
            ICodePromotionService iCodePromotionService
        )
        {
            ctx = context;
            iCodeProService = iCodePromotionService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CodePromotion> Get()
        {
            return ctx.CodePromotion;
        }


        // GET api/values/5
        [HttpGet("GetByCon")]
        public IActionResult Get(string subId)
        {
            try
            {
                var promotion = ctx.CodePromotion.FirstOrDefault(x => x.SubId == subId);
                var mainProDropDowns = iCodeProService.MainPromotionDropDowns();
                var res = new MainProResponse
                {
                    Promotion = promotion,
                    MainProDropDowns = mainProDropDowns
                };
                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CodePromotion codePro)
        {
            try
            {
                var subIdItem = ctx.CodePromotion
                    .OrderByDescending(x => x.SubId)
                    .FirstOrDefault();
                var subId = "0001";
                if (subIdItem != null)
                {
                    subId = (int.Parse(subIdItem.SubId) + 1)
                        .ToString()
                        .PadLeft(4, '0');
                }

                codePro.SubId = subId;
                codePro.SubCodePro = GetSubCodePro((int)codePro.CodeMainPro);
                codePro.CreateDate = DateTime.Now;
                ctx.CodePromotion.Add(codePro);
                await ctx.SaveChangesAsync();

                var obj = new Dictionary<string, object> {
                    {"subId", subId}
                };
                return Ok(obj);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]CodePromotion codePro)
        {
           try
            {
                var mainProItem = ctx.CodePromotion.First(x => x.SubId == codePro.SubId);
                if (mainProItem == null)
                {
                    return BadRequest();
                }

                mainProItem.Status = codePro.Status;
                mainProItem.StartDate = codePro.StartDate;
                mainProItem.EndDate = codePro.EndDate;
                mainProItem.UpateDate = DateTime.Now;

                if (mainProItem.CodeMainPro != codePro.CodeMainPro)
                {
                    mainProItem.SubCodePro = GetSubCodePro((int)codePro.CodeMainPro);
                    mainProItem.CodeMainPro = codePro.CodeMainPro;
                    mainProItem.MainPro = codePro.MainPro;
                }

                await ctx.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        private string GetSubCodePro(int codeMainPro)
        {
            var mainProItem = ctx.CodePromotion
                    .OrderByDescending(x => x.SubId)
                    .FirstOrDefault(x => x.CodeMainPro == codeMainPro);
            var subCodePro = "";
            if (mainProItem != null)
            {
                var subCode = (int.Parse(mainProItem.SubCodePro.Substring(7, 3)) + 1)
                    .ToString()
                    .PadLeft(3, '0');
                subCodePro = $"SUB-{mainProItem.MainPro}{subCode}";
            }
            return subCodePro;
        }

        private bool CodePromotionExists(string subId)
        {
            return ctx.CodePromotion.Any(e => e.SubId == subId);
        }

        public class MainProResponse
        {
            public CodePromotion Promotion { get; set; }
            public List<DropDowns> MainProDropDowns { get; set; }
        }
    }
}
