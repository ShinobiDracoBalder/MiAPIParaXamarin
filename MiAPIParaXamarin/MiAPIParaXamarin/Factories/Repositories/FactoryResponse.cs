using Dapper;
using MiAPIParaXamarin.Common.Responses;
using MiAPIParaXamarin.Factories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MiAPIParaXamarin.Factories.Repositories
{
    public class FactoryResponse : IFactoryResponse
    {
        private readonly IConfiguration _configuration;

        public FactoryResponse(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GenericResponse<T>> AddAsync<T>(string Quiery, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection")))
            {
                try
                {
                    connection.Open();
                    using var tran = connection.BeginTransaction();
                    try
                    {
                        var result = await connection.ExecuteAsync(Quiery, parms, commandType: commandType);
                        return new GenericResponse<T>
                        {
                            IsSuccess = result == 1 ? true : false,
                        };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return new GenericResponse<T>
                        {
                            IsSuccess = false,
                            Message = ex.Message,
                        };
                    }
                    finally { connection?.Close(); }
                }
                catch (Exception exception)
                {
                    return new GenericResponse<T>
                    {
                        IsSuccess = false,
                        Message = exception.Message,
                    };
                }
                finally
                {
                    connection?.Close();
                }
            }
        }

        public async Task<GenericResponse<T>> DeleteAsync<T>(string Quiery, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection")))
            {
                try
                {
                    connection.Open();
                    using var tran = connection.BeginTransaction();
                    try
                    {
                        var result = await connection.ExecuteAsync(Quiery, parms, commandType: commandType, transaction: tran);
                        tran.Commit();
                        return new GenericResponse<T>
                        {
                            IsSuccess = result == 1 ? true : false,
                        };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return new GenericResponse<T>
                        {
                            IsSuccess = false,
                            Message = ex.Message,
                        };
                    }
                }
                catch (Exception exception)
                {
                    return new GenericResponse<T>
                    {
                        IsSuccess = false,
                        Message = exception.Message,
                    };
                }
                finally
                {
                    connection?.Close();
                }
            }
        }

        public void Dispose(){}

        public async Task<GenericResponse<T>> Execute<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection")))
            {
                try
                {
                    connection.Open();
                    using var tran = connection.BeginTransaction();
                    try
                    {
                        var result = await connection.ExecuteAsync(sp, parms, commandType: commandType);
                        return new GenericResponse<T>
                        {
                            IsSuccess = result == 1 ? true : false,
                        };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return new GenericResponse<T>
                        {
                            IsSuccess = false,
                            Message = ex.Message,
                        };
                    }
                    finally { connection?.Close(); }
                }
                catch (Exception exception)
                {
                    return new GenericResponse<T>
                    {
                        IsSuccess = false,
                        Message = exception.Message,
                    };
                }
                finally
                {
                    connection?.Close();
                }
            }
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection"));
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection"));
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public async Task<GenericResponse<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                var list = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                return new GenericResponse<T>
                {
                    IsSuccess = true,
                    ListResults = list.ToList(),
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection"));
        }

        public async Task<GenericResponse<T>> GetOnly<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection"));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                result = await db.QueryFirstAsync<T>(sp, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                return new GenericResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return new GenericResponse<T>
            {
                IsSuccess = true,
                Result = result,
            };
        }

        public async Task<GenericResponse<T>> GetOnlyAvatar<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection"));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                result = await db.QueryFirstAsync<T>(sp, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                return new GenericResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return new GenericResponse<T>
            {
                IsSuccess = true,
                Result = result,
            };
        }

        public async Task<GenericResponse<T>> InsertAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection"));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return new GenericResponse<T>
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return new GenericResponse<T>
            {
                IsSuccess = true,
                Result = result,
            };
        }

        public async Task<GenericResponse<T>> UpdateAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection"));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return new GenericResponse<T>
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return new GenericResponse<T>
            {
                IsSuccess = true,
                Result = result,
            };
        }

        public async Task<GenericResponse<T>> UpdatesAsync<T>(string Quiery, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultPetroConnection")))
            {
                try
                {
                    connection.Open();
                    using var tran = connection.BeginTransaction();
                    try
                    {
                        var result = await connection.ExecuteAsync(Quiery, parms, commandType: commandType);
                        return new GenericResponse<T>
                        {
                            IsSuccess = result == 1 ? true : false,
                        };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return new GenericResponse<T>
                        {
                            IsSuccess = false,
                            Message = ex.Message,
                        };
                    }
                    finally { connection?.Close(); }
                }
                catch (Exception exception)
                {
                    return new GenericResponse<T>
                    {
                        IsSuccess = false,
                        Message = exception.Message,
                    };
                }
                finally
                {
                    connection?.Close();
                }
            }
        }
    }
}
