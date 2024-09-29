using ERP_System.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ERP_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : Controller
    {
        private readonly string _connectionString;

        public PurchaseOrderController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        [HttpGet("VendorsNames")]
        public async Task<ActionResult<IEnumerable<VendorDescDto>>> GetVendorDescriptionsAsync()
        {
            var vendorDescriptions = new List<VendorDescDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT CurrencyId, VendorId, VendorDescA FROM MS_Vendor", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        vendorDescriptions.Add(new VendorDescDto
                        {
                            CurrencyId = reader.GetInt32(0),
                            VendorId = reader.GetInt32(1),
                            VendorDescA = reader.GetString(2)
                        });
                    }
                }
            }

            return Ok(vendorDescriptions);
        }

        [HttpGet("Currencies")]
        public async Task<ActionResult<IEnumerable<CurrenctyDescDto>>> GetAllCurrenciesAsync()
        {
            var currencies = new List<CurrenctyDescDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT CurrencyId, CurrencyDescA FROM MS_Currency", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        currencies.Add(new CurrenctyDescDto
                        {
                            CurrencyId = reader.GetInt32(0),
                            CurrencyDescA = reader.GetString(1)
                        });
                    }
                }
            }

            return Ok(currencies);
        }

        [HttpGet("AnalaticCodes")]
        public async Task<ActionResult<IEnumerable<AnalaticCodeDescDto>>> GetAnalaticCodesAsync()
        {
            var codes = new List<AnalaticCodeDescDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT AId, Code, DescA FROM Sys_AnalyticalCodes", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        codes.Add(new AnalaticCodeDescDto
                        {
                            AId = reader.GetInt32(0),
                            Code = reader.GetString(1),
                            DescA = reader.GetString(2)
                        });
                    }
                }
            }

            return Ok(codes);
        }

        [HttpGet("BooksWithTermType18")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksWithTermType18Async()
        {
            var books = new List<BookDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT BookId, BookNameAR, PrefixCode FROM Sys_Books WHERE TermType = @TermType", connection);
                command.Parameters.AddWithValue("@TermType", 18);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        books.Add(new BookDto
                        {
                            BookId = reader.GetInt32(0),
                            BookNameAR = reader.GetString(1),
                            PrefixCode = reader.GetString(2)
                        });
                    }
                }
            }
            return Ok(books);
        }

        [HttpGet("Items")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItemsAsync()
        {
            var items = new List<ItemDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(@"
                    SELECT i.ItemCode, i.ItemCardId, i.ItemDescA, 
                           u.UnitNam, u.Price1 
                    FROM Ms_ItemCard i
                    LEFT JOIN Ms_ItemUnit u ON i.ItemCardId = u.ItemCardId
                    WHERE u.IsDefaultPurchas = 1", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new ItemDto
                        {
                            ItemCode = reader.GetString(0),
                            ItemCardId = reader.GetInt32(1),
                            ItemDescA = reader.GetString(2),
                            UnitName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Price1 = reader.IsDBNull(4) ? (decimal?)null : reader.GetDecimal(4)
                        });
                    }
                }
            }

            return Ok(items);
        }

        [HttpGet("LastPurchaseOrder")]
        public async Task<ActionResult<PurchaseOrderDto>> GetLastPurchaseOrder()
        {
            PurchaseOrderDto lastPurchaseOrder = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(@"
            SELECT TOP 1 
                BookId,
                TrDate,
                ArrivalDate,
                DeliveryPeriodDays,
                PayPeriodDays,
                ManualTrNo,
                InvoiceType,
                VendorId,
                CurrencyId,
                PurOrderId,
                AId
            FROM Ms_PurchasOrder
            ORDER BY CreatedAt DESC", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        lastPurchaseOrder = new PurchaseOrderDto
                        {
                            BookId = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0),
                            TrDate = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
                            ArrivalDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            DeliveryPeriodDays = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            PayPeriodDays = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                            ManualTrNo = reader.IsDBNull(5) ? null : reader.GetString(5),
                            InvoiceType = reader.IsDBNull(6) ? (int?)null : reader.GetByte(6),
                            VendorId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                            CurrencyId = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                            PurOrderId = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),
                            AId = reader.IsDBNull(10) ? (int?)null : reader.GetInt32(10)
                        };
                    }
                    else
                    {
                        return NotFound(new { message = "No purchase orders found." });
                    }
                }
            }

            return Ok(lastPurchaseOrder);
        }

        [HttpGet("GetPurchaseOrderDetailListByPurOrderId/{purOrderId}")]
        public async Task<ActionResult> GetPurchaseOrderDetailListByPurOrderId(int purOrderId)
        {
            if (purOrderId <= 0)
            {
                return BadRequest(new { message = "Invalid Purchase Order ID." });
            }

            List<PurchaseOrderDetailDto> purchaseOrderDetails = new List<PurchaseOrderDetailDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(@"
        SELECT 
            PurOrderId,
            ItemCardId,
            StoreId,
            UnitId,
            Quantity,
            Price,
            ItemCardDesc
        FROM MS_PurchOrderDetail
        WHERE PurOrderId = @PurOrderId", connection);

                command.Parameters.AddWithValue("@PurOrderId", purOrderId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var detail = new PurchaseOrderDetailDto
                        {
                            PurOrderId = reader.GetInt32(0),    // Assuming PurOrderId is always non-null
                            ItemCardId = reader.GetInt32(1),    // Assuming ItemCardId is always non-null
                            StoreId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            UnitId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            Quantity = reader.GetDecimal(4),   // Assuming Quantity is decimal
                            Price = reader.GetDecimal(5),      // Assuming Price is decimal
                            ItemCardDesc = reader.IsDBNull(6) ? null : reader.GetString(6)
                        };

                        purchaseOrderDetails.Add(detail);
                    }
                }
            }

            if (purchaseOrderDetails.Count == 0)
            {
                return NotFound(new { message = "No purchase order details found for the specified Purchase Order ID." });
            }

            return Ok(purchaseOrderDetails);
        }

        [HttpPost("CreatePurchaseOrder")]
        public async Task<ActionResult> CreatePurchaseOrder([FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            int bookId = 0;
            int? storeId = null; // Nullable StoreId

            // Get BookId and StoreId from Sys_Books based on the bookId in the DTO
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT BookId, StoreId FROM Sys_Books WHERE TermType = @TermType", connection);
                command.Parameters.AddWithValue("@TermType", purchaseOrderDto.TermType);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // Safely retrieve BookId
                        if (!reader.IsDBNull(0)) // Check if BookId is not NULL
                        {
                            bookId = reader.GetInt32(0); // Get the BookId
                        }
                        // Safely retrieve StoreId
                        if (!reader.IsDBNull(1)) // Check if StoreId is not NULL
                        {
                            storeId = reader.GetInt32(1); // Get the StoreId
                        }
                    }
                    else
                    {
                        // Handle case where no records are found
                        return NotFound(new { message = "Book not found." });
                    }
                }
            }

            // Increment the Counter and get the BookNumber
            int TrNo = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE Sys_Counter SET Counter = Counter + 1; SELECT Counter FROM Sys_Counter WHERE BookId = @BookId", connection);
                command.Parameters.AddWithValue("@BookId", bookId);

                // Execute command and safely handle result
                var result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    TrNo = (int)result; // Safely cast to int
                }
                else
                {
                    return BadRequest(new { message = "Failed to increment counter." });
                }
            }

            // Insert the new purchase order into Ms_PurchaseOrder and retrieve the new ID
            int newPurchaseOrderId;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var insertCommand = new SqlCommand(@"
        INSERT INTO Ms_PurchasOrder (
            TrNo,
            BookId,
            TrDate,
            ArrivalDate,
            ExpiryDate,
            DeliveryPeriodDays,
            PayPeriodDays,
            ManualTrNo,
            InvoiceType,
            Rate,
            VendorId,
            CurrencyId,
            StorId,
            AId,
            CreatedAt
        ) 
        OUTPUT INSERTED.PurOrderId
        VALUES (
            @TrNo,
            @BookId,
            @TrDate,
            @ArrivalDate,
            @ExpiryDate,
            @DeliveryPeriodDays,
            @PayPeriodDays,
            @ManualTrNo,
            @InvoiceType,
            @Rate,
            @VendorId,
            @CurrencyId,
            @StoreId,
            @AId,
            @CreatedAt
        )", connection);

                insertCommand.Parameters.AddWithValue("@TrNo", TrNo);
                insertCommand.Parameters.AddWithValue("@BookId", bookId);
                insertCommand.Parameters.AddWithValue("@TrDate", purchaseOrderDto.TrDate);
                insertCommand.Parameters.AddWithValue("@ArrivalDate", purchaseOrderDto.ArrivalDate);
                insertCommand.Parameters.AddWithValue("@ExpiryDate", (object)purchaseOrderDto.ExpiryDate ?? DBNull.Value);
                insertCommand.Parameters.AddWithValue("@DeliveryPeriodDays", purchaseOrderDto.DeliveryPeriodDays);
                insertCommand.Parameters.AddWithValue("@PayPeriodDays", purchaseOrderDto.PayPeriodDays);
                insertCommand.Parameters.AddWithValue("@ManualTrNo", purchaseOrderDto.ManualTrNo);
                insertCommand.Parameters.AddWithValue("@InvoiceType", purchaseOrderDto.InvoiceType);
                insertCommand.Parameters.AddWithValue("@Rate", (object)purchaseOrderDto.Rate ?? DBNull.Value);
                insertCommand.Parameters.AddWithValue("@VendorId", purchaseOrderDto.VendorId);
                insertCommand.Parameters.AddWithValue("@CurrencyId", purchaseOrderDto.CurrencyId);
                insertCommand.Parameters.AddWithValue("@StoreId", (object)storeId ?? DBNull.Value);
                insertCommand.Parameters.AddWithValue("@AId", purchaseOrderDto.AId);
                insertCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                // Execute the command and retrieve the newly inserted PurchaseOrderId
                newPurchaseOrderId = (int)await insertCommand.ExecuteScalarAsync();
            }

            return Ok(new { message = "Purchase Order created successfully.", PurchaseOrderId = newPurchaseOrderId });
        }

        [HttpPost("CreatePurchaseOrderDetails")]
        public async Task<ActionResult> CreatePurchaseOrderDetails([FromBody] List<PurchaseOrderDetailDto> purchaseOrderDetails)
        {
            if (purchaseOrderDetails == null || purchaseOrderDetails.Count == 0)
            {
                return BadRequest(new { message = "Invalid or empty data." });
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                foreach (var detail in purchaseOrderDetails)
                {
                    var insertCommand = new SqlCommand(@"
                INSERT INTO MS_PurchOrderDetail (
                    PurOrderId,
                    ItemCardId,
                    StoreId,
                    UnitId,
                    Quantity,
                    Price,
                    ItemCardDesc
                ) VALUES (
                    @PurOrderId,
                    @ItemCardId,
                    @StoreId,
                    @UnitId,
                    @Quantity,
                    @Price,
                    @ItemCardDesc
                )", connection);

                    insertCommand.Parameters.AddWithValue("@PurOrderId", (object)detail.PurOrderId ?? DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@ItemCardId", detail.ItemCardId);
                    insertCommand.Parameters.AddWithValue("@StoreId", detail.StoreId?? (object)DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@UnitId", detail.UnitId ?? (object)DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@Quantity", detail.Quantity);
                    insertCommand.Parameters.AddWithValue("@Price", detail.Price);
                    insertCommand.Parameters.AddWithValue("@ItemCardDesc", detail.ItemCardDesc ?? (object)DBNull.Value);

                    try
                    {
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new { message = $"Error inserting data: {ex.Message}" });
                    }
                }
            }

            return Ok(new { message = "Purchase Order Details created successfully." });
        }
    }
}