using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using CaseTaskManager.Interfaces;
using CaseTaskManager.Models.CaseWorker;


namespace CaseTaskManager.Services
{
    public class CaseWorkerService : ICaseWorkerService
    {
        private readonly IConfiguration _config;

        public CaseWorkerService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<CaseWorkerDto?> GetCaseWorkerByIdAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<CaseWorkerDto>(
                "sp_GetCaseWorkerById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<CaseWorkerDto>> GetAllCaseWorkersAsync()
        {
            using var connection = new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));

            var workers = await connection.QueryAsync<CaseWorkerDto>(
                "sp_GetAllCaseWorkers",
                commandType: CommandType.StoredProcedure);

            return workers;
        }

        public async Task<bool> UpdateCaseWorkerAsync(int id, UpdateDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                Id = id,
                FullName = dto.FullName,
                Email = dto.Email
            };

            var affectedRows = await connection.ExecuteAsync(
                "sp_UpdateCaseWorker",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }

        public async Task<bool> ActivateCaseWorkerAsync(int caseWorkerId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var affectedRows = await connection.ExecuteAsync(
                "sp_ActivateCaseWorker",
                new { CaseWorkerId = caseWorkerId },
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }

        public async Task<bool> DeactivateCaseWorkerAsync(int caseWorkerId)
        {
            using var connection =
                new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var rows = await connection.ExecuteAsync(
                "sp_DeactivateCaseWorker",
                new { CaseWorkerId = caseWorkerId },
                commandType: CommandType.StoredProcedure);

            return rows > 0;          // true if an active row was updated
        }
        public async Task<bool> DeleteCaseWorkerAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var affectedRows = await connection.ExecuteAsync(
                "sp_DeleteCaseWorker",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }
        public async Task<int> AddCaseWorkerAsync(CreateDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                FullName = dto.FullName,
                Email = dto.Email,
                IsActive = dto.IsActive
            };

            var newId = await connection.QuerySingleAsync<int>(
                "sp_AddCaseWorker",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return newId;
        }

    }
}
