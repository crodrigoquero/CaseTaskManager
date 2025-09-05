using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using CaseTaskManager.Models.Case;
using CaseTaskManager.Interfaces;
using CaseTaskManager.Models;

namespace CaseTaskManager.Services
{
    public class CaseService : ICaseService
    {
        private readonly IConfiguration _config;

        public CaseService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<CaseDto>> GetAllCasesAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var results = await connection.QueryAsync<CaseDto>(
                "sp_GetAllCases",
                commandType: CommandType.StoredProcedure
            );

            return results;
        }

        public async Task<CaseDto?> GetCaseByIdAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<CaseDto>(
                "sp_GetCaseById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
            return result;
        }

        public async Task<bool> UpdateCaseDetailsAsync(int id, UpdateCaseDetailsDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description
            };

            var affectedRows = await connection.ExecuteAsync(
                "sp_UpdateCase",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }

        public async Task<bool> UpdateCaseStatusAsync(int caseId, int statusId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new { CaseId = caseId, StatusId = statusId };

            var affectedRows = await connection.ExecuteAsync(
                "sp_UpdateCaseStatus",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }

        public async Task<bool> DeleteCaseAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var affectedRows = await connection.ExecuteAsync(
                "sp_DeleteCase",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }

        public async Task<int> AddCaseAsync(CreateCaseDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                Title = dto.Title,
                Description = dto.Description
            };

            var newId = await connection.QuerySingleAsync<int>(
                "sp_AddCase",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return newId;
        }

        public async Task<int> AssignCaseWorkerAsync(CaseAssignmentDto assignmentDto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleAsync<int>(
                "sp_AddCaseAssignment",
                new
                {
                    CaseId = assignmentDto.CaseId,
                    CaseWorkerId = assignmentDto.CaseWorkerId
                },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<bool> RemoveCaseAssignmentAsync(CaseAssignmentDto assignmentDto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var affectedRows = await connection.ExecuteAsync(
                "sp_RemoveCaseAssignment",
                new { CaseId = assignmentDto.CaseId, CaseWorkerId = assignmentDto.CaseWorkerId },
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }

    }
}
