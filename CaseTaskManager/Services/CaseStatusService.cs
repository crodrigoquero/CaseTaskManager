using CaseTaskManager.Interfaces;
using CaseTaskManager.Models.CaseStatus;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CaseTaskManager.Services
{
    public class CaseStatusService : ICaseStatusService
    {
        private readonly IConfiguration _config;

        public CaseStatusService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<CaseStatusDto>> GetAllCaseStatusesAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryAsync<CaseStatusDto>(
                "sp_GetAllCaseStatuses",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<CaseStatusDto?> GetCaseStatusByIdAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryFirstOrDefaultAsync<CaseStatusDto>(
                "sp_GetCaseStatusById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> AddCaseStatusAsync(CaseStatusDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QuerySingleAsync<int>(
                "sp_AddCaseStatus",
                new { StatusName = dto.StatusName },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateCaseStatusAsync(int id, CaseStatusDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var affectedRows = await connection.ExecuteAsync(
                "sp_UpdateCaseStatusDetails",
                new { Id = id, StatusName = dto.StatusName },
                commandType: CommandType.StoredProcedure
            );
            return affectedRows > 0;
        }

        public async Task<bool> DeleteCaseStatusAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var affectedRows = await connection.ExecuteAsync(
                "sp_DeleteCaseStatus",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
            return affectedRows > 0;
        }
    }
}
