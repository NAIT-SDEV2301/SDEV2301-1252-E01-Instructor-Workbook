using Microsoft.EntityFrameworkCore;
using WestwindSystem.DAL;
using WestwindSystem.Entities;

namespace WestwindSystem.BLL.DonW
{
    /// <summary>
    /// Provides business logic and data-access coordination for Shipment records.
    /// </summary>
    public class ShipmentServices
    {
        /// <summary>
        /// Creates DbContext instances for each service method call.
        /// </summary>
        private readonly IDbContextFactory<WestWindContext> _dbContextFactory;

        /// <summary>
        /// Creates a new ShipmentServices instance.
        /// </summary>
        /// <param name="dbContextFactory">Factory used to create WestWindContext objects.</param>
        public ShipmentServices(IDbContextFactory<WestWindContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /*
         * Every CRUD method follows this pattern:
         * Guard -> Context -> Query -> Action -> Save
         *
         * The 5-Line CRUD Service Template:
         *
         * public async Task MethodName(Type input)
         * {
         *     Guard(input);
         *     await using var context = await _dbContextFactory.CreateDbContextAsync();
         *     var entity = await Query(context, input);
         *     Action(context, entity, input);
         *     await context.SaveChangesAsync();
         * }
         */

        /// <summary>
        /// Returns all shipments for the specified year and month.
        /// </summary>
        /// <param name="year">The shipment year to search.</param>
        /// <param name="month">The shipment month to search.</param>
        /// <returns>A list of matching shipments.</returns>
        public async Task<List<Shipment>> FindShipmentsByYearAndMonth(int year, int month)
        {
            ValidateYearAndMonth(year, month);

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                return await context.Shipments
                    .Include(s => s.ShipViaNavigation)
                    .Where(s => s.ShippedDate.Year == year && s.ShippedDate.Month == month)
                    .OrderBy(s => s.ShippedDate)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to retrieve shipments at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Returns the total number of shipments for the specified year and month.
        /// </summary>
        /// <param name="year">The shipment year to search.</param>
        /// <param name="month">The shipment month to search.</param>
        /// <returns>The total number of matching shipments.</returns>
        public async Task<int> CountShipmentsByYearAndMonth(int year, int month)
        {
            ValidateYearAndMonth(year, month);

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                return await context.Shipments
                    .Where(s => s.ShippedDate.Year == year && s.ShippedDate.Month == month)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to count shipments at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Returns one page of shipments for the specified year and month.
        /// </summary>
        /// <param name="year">The shipment year to search.</param>
        /// <param name="month">The shipment month to search.</param>
        /// <param name="currentPageNumber">The page number to return.</param>
        /// <param name="itemsPerPage">The number of items per page.</param>
        /// <returns>A paged list of matching shipments.</returns>
        public async Task<List<Shipment>> FindShipmentsByYearAndMonthPaging(
            int year,
            int month,
            int currentPageNumber,
            int itemsPerPage)
        {
            ValidateYearAndMonth(year, month);

            if (currentPageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(currentPageNumber),
                    "Current page number must be 1 or greater.");
            }

            if (itemsPerPage < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(itemsPerPage),
                    "Items per page must be 1 or greater.");
            }

            int recordsSkipped = itemsPerPage * (currentPageNumber - 1);

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                return await context.Shipments
                    .Include(s => s.ShipViaNavigation)
                    .Where(s => s.ShippedDate.Year == year && s.ShippedDate.Month == month)
                    .OrderBy(s => s.ShippedDate)
                    .Skip(recordsSkipped)
                    .Take(itemsPerPage)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to retrieve paged shipments at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Adds a new shipment after validating business rules.
        /// </summary>
        /// <param name="newShipment">The shipment to add.</param>
        /// <returns>The saved shipment, including its generated key.</returns>
        public async Task<Shipment> AddShipmentAsync(Shipment newShipment)
        {
            if (newShipment is null)
            {
                throw new ArgumentNullException(nameof(newShipment), "Shipment data is required.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                await ValidateShipmentAsync(context, newShipment);

                // Generate a new tracking code before saving the shipment.
                newShipment.TrackingCode = Guid.NewGuid().ToString();

                await context.Shipments.AddAsync(newShipment);
                await context.SaveChangesAsync();

                return newShipment;
            }
            // The service layer lets validation errors pass through but wraps unexpected system errors in a user-friendly message.
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException(
                    "Unable to add the shipment at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Finds one shipment by its primary key value.
        /// </summary>
        /// <param name="shipmentId">The shipment ID to search for.</param>
        /// <returns>The matching shipment, or null if not found.</returns>
        public async Task<Shipment?> FindShipmentByShipmentId(int shipmentId)
        {
            if (shipmentId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(shipmentId),
                    "Shipment ID must be greater than 0.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                return await context.Shipments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.ShipmentID == shipmentId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to retrieve the shipment at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Updates an existing shipment after validating business rules.
        /// </summary>
        /// <param name="updatedShipment">The shipment data to update.</param>
        public async Task UpdateShipmentAsync(Shipment updatedShipment)
        {
            if (updatedShipment is null)
            {
                throw new ArgumentNullException(nameof(updatedShipment), "Shipment data is required.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                var existingShipment = await context.Shipments
                    .FirstOrDefaultAsync(s => s.ShipmentID == updatedShipment.ShipmentID)
                    ?? throw new ArgumentException(
                        $"Shipment {updatedShipment.ShipmentID} does not exist.",
                        nameof(updatedShipment.ShipmentID));

                await ValidateShipmentAsync(context, updatedShipment);

                // Copy editable values into the tracked entity.
                existingShipment.OrderID = updatedShipment.OrderID;
                existingShipment.ShipVia = updatedShipment.ShipVia;
                existingShipment.ShippedDate = updatedShipment.ShippedDate;
                existingShipment.TrackingCode = updatedShipment.TrackingCode;

                await context.SaveChangesAsync();
            }
            // The service layer lets validation errors pass through but wraps unexpected system errors in a user-friendly message.
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException(
                    "Unable to update the shipment at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Deletes an existing shipment by its primary key value.
        /// </summary>
        /// <param name="shipmentId">The shipment ID to delete.</param>
        public async Task DeleteShipmentAsync(int shipmentId)
        {
            if (shipmentId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(shipmentId),
                    "Shipment ID must be greater than 0.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                var existingShipment = await context.Shipments.FindAsync(shipmentId);

                if (existingShipment is null)
                {
                    throw new ArgumentException(
                        $"Shipment {shipmentId} does not exist.",
                        nameof(shipmentId));
                }

                context.Shipments.Remove(existingShipment);
                await context.SaveChangesAsync();
            }
            // The service layer lets validation errors pass through but wraps unexpected system errors in a user-friendly message.
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException(
                    "Operation failed. Please try again.",
                    ex);
            }
        }

        /// <summary>
        /// Validates the year and month values used in shipment searches.
        /// </summary>
        /// <param name="year">The year to validate.</param>
        /// <param name="month">The month to validate.</param>
        private static void ValidateYearAndMonth(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(month),
                    $"Invalid month {month}. Month must be between 1 and 12.");
            }

            if (year < 1990 || year > DateTime.Today.Year)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(year),
                    $"Invalid year {year}. Year must be between 1990 and {DateTime.Today.Year}.");
            }
        }

        /// <summary>
        /// Validates shipment business rules and foreign key values before save.
        /// </summary>
        /// <param name="context">The DbContext used for validation queries.</param>
        /// <param name="shipment">The shipment to validate.</param>
        private static async Task ValidateShipmentAsync(WestWindContext context, Shipment shipment)
        {
            if (shipment.ShippedDate < DateTime.Today)
            {
                throw new ArgumentException(
                    "Shipped date must be today or in the future.",
                    nameof(shipment.ShippedDate));
            }

            bool orderExists = await context.Orders
                .AnyAsync(o => o.OrderID == shipment.OrderID);

            if (!orderExists)
            {
                throw new ArgumentException(
                    $"Order ID {shipment.OrderID} does not exist.",
                    nameof(shipment.OrderID));
            }

            bool shipperExists = await context.Shippers
                .AnyAsync(s => s.ShipperID == shipment.ShipVia);

            if (!shipperExists)
            {
                throw new ArgumentException(
                    $"Shipper ID {shipment.ShipVia} does not exist.",
                    nameof(shipment.ShipVia));
            }
        }
    }
}