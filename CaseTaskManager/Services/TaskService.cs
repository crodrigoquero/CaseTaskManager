using Dapper;
using CaseTaskManager.Interfaces;
using CaseTaskManager.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CaseTaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly IConfiguration _config;
        public TaskService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> AddTaskAsync(CreateTaskDto newTask)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                CaseId = newTask.CaseId,
                Title = newTask.Title,
                Description = newTask.Description,
                StatusId = newTask.StatusId,
                TaskTypeId = newTask.TaskTypeId,
                DueDate = newTask.DueDate
            };

            var result = await connection.QuerySingleAsync<int>(
                "sp_AddTask",
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
            );

            return result; // This is the new Task ID
        }
        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var tasks = await connection.QueryAsync<TaskItem>(
                "sp_GetAllTasks",
                commandType: System.Data.CommandType.StoredProcedure
            );

            return tasks;
        }
        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<TaskItem>(
                "sp_GetTaskById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<IEnumerable<TaskItem>> GetTasksByCaseIdAsync(int caseId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var results = await connection.QueryAsync<TaskItem>(
                "sp_GetTasksByCaseId",
                new { CaseId = caseId },
                commandType: CommandType.StoredProcedure
            );

            return results;
        }
        public async Task<TaskTypeDto?> GetTaskTypeByIdAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var result = await connection.QueryFirstOrDefaultAsync<TaskTypeDto>(
                "sp_GetTaskTypeById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<IEnumerable<TaskItem>> GetTasksByTypeIdAsync(int typeId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var tasks = await connection.QueryAsync<TaskItem>(
                "sp_GetTasksByTypeId",
                new { TaskTypeId = typeId },
                commandType: CommandType.StoredProcedure
            );

            return tasks;
        }
        public async Task<int> AddTaskTypeAsync(CreateTaskTypeDto taskTypeDto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                TypeName = taskTypeDto.TypeName,
                Description = taskTypeDto.Description
            };

            var result = await connection.QuerySingleAsync<int>(
                "sp_AddTaskType",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var rows = await connection.QuerySingleAsync<int>(
                "sp_DeleteTask",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;   // true => 204 NoContent, false => 404 NotFound
        }


        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto taskDto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var parameters = new
            {
                Id = id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                StatusId = taskDto.StatusId,
                TaskTypeId = taskDto.TaskTypeId,
                DueDate = taskDto.DueDate
            };

            var rows = await connection.QuerySingleAsync<int>(
                "sp_UpdateTask",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }

        public async Task<bool> UpdateTaskStatusAsync(int taskId, int statusId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                TaskId = taskId,
                StatusId = statusId
            };

            var affectedRows = await connection.ExecuteAsync(
                "sp_UpdateTaskStatus",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }
        public async Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var taskTypes = await connection.QueryAsync<TaskTypeDto>(
                "sp_GetAllTaskTypes",
                commandType: CommandType.StoredProcedure
            );

            return taskTypes;
        }

        public async Task<IEnumerable<TaskStatusDto>> GetAllTaskStatusesAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryAsync<TaskStatusDto>(
                "sp_GetAllTaskStatuses",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<TaskStatusDto?> AddTaskStatusAsync(string statusName)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var newId = await connection.QuerySingleAsync<int>(
                "sp_AddTaskStatus",
                new { StatusName = statusName },
                commandType: CommandType.StoredProcedure
            );

            return new TaskStatusDto { Id = newId, StatusName = statusName };
        }

        public async Task<bool> DeleteTaskStatusAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var rows = await connection.ExecuteAsync(
                "sp_DeleteTaskStatus",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }

        public async Task<TaskStatusDto?> GetTaskStatusByIdAsync(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<TaskStatusDto>(
                "sp_GetTaskStatusById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

    }
}
