using Dapper;
using MiAPIParaXamarin.Common.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace MiAPIParaXamarin.Factories.Interfaces
{
    public interface IFactoryResponse : IDisposable
    {
        DbConnection GetDbconnection();
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<GenericResponse<T>> Execute<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<GenericResponse<T>> InsertAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<GenericResponse<T>> UpdateAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<GenericResponse<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
        Task<GenericResponse<T>> GetOnly<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<GenericResponse<T>> GetOnlyAvatar<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
        Task<GenericResponse<T>> AddAsync<T>(string Quiery, DynamicParameters parms, CommandType commandType = CommandType.Text);
        Task<GenericResponse<T>> UpdatesAsync<T>(string Quiery, DynamicParameters parms, CommandType commandType = CommandType.Text);
        Task<GenericResponse<T>> DeleteAsync<T>(string Quiery, DynamicParameters parms, CommandType commandType = CommandType.Text);
    }
}
