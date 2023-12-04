using DecisionSystem.Data;
using DecisionSystem.DTOs;
using DecisionSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DecisionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly DecisionSystemDbContext _context;

        public ManagerController(DecisionSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet("product-analys")]
        public async Task<ActionResult<IEnumerable<ProductAnalys>>> GetProductAnalys()
        {
            if (_context.ProductAnalys == null)
            {
                return NotFound();
            }
            return await _context.ProductAnalys.ToListAsync();
        }

        [HttpGet("reports")]
        public async Task<ActionResult<IEnumerable<ReportsDTO>>> GetReports()
        {
            var reportsQuery = from report in _context.AnalysReport
                               join analysis in _context.ArchiveProductsAnalys
                               on report.Id equals analysis.AnalysReportId into reportAnalyses
                               select new ReportsDTO
                               {
                                   Id = report.Id,
                                   Date = report.Date,
                                   Analyses = reportAnalyses.Select(ra => new Analyses
                                   {
                                       ProductId = ra.ProductId,
                                       Popularity = ra.Popularity
                                   }).ToList()
                               };

            var reports = await reportsQuery.ToListAsync();

            return Ok(reports);
        }


    }
}
