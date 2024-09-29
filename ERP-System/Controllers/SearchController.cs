using ERP_System.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ERP_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly string _connectionString;

        public SearchController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        [HttpGet("SearchVendorByCode/{vendorCode}")]
        public async Task<ActionResult<IEnumerable<VendorDescDto>>> SearchVendorByCodeAsync(string vendorCode)
        {
            var vendorDescriptions = new List<VendorDescDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT VendorId, VendorDescA, VendorCode, CurrencyId FROM MS_Vendor WHERE VendorCode LIKE @VendorCode", connection);
                command.Parameters.AddWithValue("@VendorCode", $"%{vendorCode}%");

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        vendorDescriptions.Add(new VendorDescDto
                        {
                            VendorId = reader.GetInt32(0),
                            VendorDescA = reader.GetString(1),
                            VendorCode = reader.GetString(2),
                            CurrencyId = reader.GetInt32(3)
                        });
                    }
                }
            }

            return Ok(vendorDescriptions);
        }

        [HttpGet("SearchVendorByName/{vendorName}")]
        public async Task<ActionResult<IEnumerable<VendorDescDto>>> SearchVendorByNameAsync(string vendorName)
        {
            var vendorDescriptions = new List<VendorDescDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT VendorId, VendorDescA, VendorCode, CurrencyId FROM MS_Vendor WHERE VendorDescA LIKE @VendorName", connection);
                command.Parameters.AddWithValue("@VendorName", $"%{vendorName}%");

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        vendorDescriptions.Add(new VendorDescDto
                        {
                            VendorId = reader.GetInt32(0),
                            VendorDescA = reader.GetString(1),
                            VendorCode = reader.GetString(2),
                            CurrencyId = reader.GetInt32(3)
                        });
                    }
                }
            }

            return Ok(vendorDescriptions);
        }
    }
}
