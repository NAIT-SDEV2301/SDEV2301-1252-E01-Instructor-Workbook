using Microsoft.EntityFrameworkCore;
using WestwindSystem.DAL;
using WestwindSystem.Entities;

namespace WestwindSystem.BLL.SamW
{
    /// <summary>
    /// ShipperServices
    /// ----------------
    /// Purpose:
    /// Provides business logic and data-access coordination for Shipper records.
    ///
    /// Responsibilities:
    /// - Validates all Shipper business rules before database operations.
    /// - Calls EF Core through DbContext to perform CRUD operations.
    /// - Ensures only valid data is saved to the database.
    /// - Returns data to the UI in a safe and consistent manner.
    ///
    /// Notes:
    /// - This class is part of the reference architecture for the group project.
    /// - Blazor components must call this service instead of using DbContext directly.
    /// - Each method creates its own DbContext using IDbContextFactory.
    /// - Validation errors (ArgumentException) are allowed to pass to the UI.
    /// - Unexpected errors are wrapped in a user-friendly message.
    /// </summary>
    public class ShipperServices
    {
        /// <summary>
        /// Creates DbContext instances for each service method call.
        /// </summary>
        private readonly IDbContextFactory<WestWindContext> _dbContextFactory;

        /// <summary>
        /// Creates a new ShipperServices instance.
        /// </summary>
        /// <param name="dbContextFactory">Factory used to create WestWindContext objects.</param>
        public ShipperServices(IDbContextFactory<WestWindContext> dbContextFactory)
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
        /// Adds a new Shipper after validating business rules.
        /// </summary>
        /// <param name="newShipper">The shipper to add.</param>
        /// <returns>The saved shipper, including its generated key.</returns>
        public async Task<Shipper> AddShipperAsync(Shipper newShipper)
        {
            if (newShipper is null)
            {
                throw new ArgumentNullException(nameof(newShipper), "Shipper data is required.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                ValidateShipper(newShipper);

                await context.Shippers.AddAsync(newShipper);
                await context.SaveChangesAsync();

                return newShipper;
            }
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException(
                    "Unable to add shipper at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Finds one shipper by its primary key value.
        /// </summary>
        /// <param name="shipperId">The shipper ID to search for.</param>
        /// <returns>The matching shipper, or null if not found.</returns>
        public async Task<Shipper?> FindShipperById(int shipperId)
        {
            if (shipperId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(shipperId),
                    "Shipper ID must be greater than 0.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                return await context.Shippers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.ShipperID == shipperId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to retrieve shipper at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Updates an existing Shipper after validating business rules.
        /// </summary>
        /// <param name="updatedShipper">The shipper data to update.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the shipper does not exist or validation fails.
        /// </exception>
        public async Task UpdateShipperAsync(Shipper updatedShipper)
        {
            if (updatedShipper is null)
            {
                throw new ArgumentNullException(nameof(updatedShipper), "Shipper data is required.");
            }

            if (updatedShipper.ShipperID <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(updatedShipper.ShipperID),
                    "Shipper ID must be greater than 0.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                var existingShipper = await context.Shippers
                    .FirstOrDefaultAsync(s => s.ShipperID == updatedShipper.ShipperID)
                    ?? throw new ArgumentException(
                        $"Shipper {updatedShipper.ShipperID} does not exist.",
                        nameof(updatedShipper.ShipperID));

                ValidateShipper(updatedShipper);

                // Copy editable values into the tracked entity.
                existingShipper.CompanyName = updatedShipper.CompanyName;
                existingShipper.Phone = updatedShipper.Phone;

                await context.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException(
                    "Unable to update shipper at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Deletes an existing Shipper by its ID.
        /// </summary>
        /// <param name="shipperId">The ID of the shipper to delete.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the shipper does not exist.
        /// </exception>
        public async Task DeleteShipperAsync(int shipperId)
        {
            if (shipperId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(shipperId),
                    "Shipper ID must be greater than 0.");
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                var existingShipper = await context.Shippers.FindAsync(shipperId);

                if (existingShipper is null)
                {
                    throw new ArgumentException(
                        $"Shipper {shipperId} does not exist.",
                        nameof(shipperId));
                }

                context.Shippers.Remove(existingShipper);
                await context.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException(
                    "Unable to delete shipper at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Validates shipper business rules before saving.
        /// </summary>
        /// <param name="shipper">The shipper to validate.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when validation rules are violated.
        /// </exception>
        private static void ValidateShipper(Shipper shipper)
        {
            if (string.IsNullOrWhiteSpace(shipper.CompanyName) || shipper.CompanyName.Trim().Length < 3)
            {
                throw new ArgumentException(
                    "Company name is required and must contain 3 or more characters.",
                    nameof(shipper.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(shipper.Phone) && shipper.Phone.Length < 10)
            {
                throw new ArgumentException(
                    "Phone number appears to be invalid.",
                    nameof(shipper.Phone));
            }
        }

        /// <summary>
        /// Returns a list of all shippers.
        /// </summary>
        /// <returns>A list of all shippers ordered by company name.</returns>
        public async Task<List<Shipper>> GetShippersAsync()
        {
            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                return await context.Shippers
                    .OrderBy(s => s.CompanyName)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to retrieve shippers at this time.",
                    ex);
            }
        }

        /// <summary>
        /// Searches for shippers by partial company name or phone.
        /// </summary>
        /// <param name="searchTerm">The search value entered by the user.</param>
        /// <returns>A list of matching shippers.</returns>
        /// <exception cref="ArgumentException">Thrown when the search term is empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when error accesssing database.</exception>
        public async Task<List<Shipper>> SearchShippersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException(
                    "Enter a company name or phone value to search.",
                    nameof(searchTerm));
            }

            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                string term = searchTerm.Trim();

                return await context.Shippers
                    .Where(s =>
                        s.CompanyName.Contains(term) ||
                        (s.Phone != null && s.Phone.Contains(term)))
                    .OrderBy(s => s.CompanyName)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException(
                    "Unable to retrieve shippers at this time.",
                    ex);
            }
        }
    }
}