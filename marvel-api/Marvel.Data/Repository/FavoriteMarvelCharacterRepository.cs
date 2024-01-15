using Marvel.Infra.Repository.Interface;
using Marvel.Infra.Context;
using System.Data;
using Dapper;
using Marvel.Core.Domains;

namespace Marvel.Infra.Repository
{
    public class FavoriteMarvelCharacterRepository : IFavoriteMarvelCharacterRepository
    {
        private readonly MarvelDbContext _marvelDbContext;

        public FavoriteMarvelCharacterRepository(MarvelDbContext marvelDbContext)
        {
            _marvelDbContext = marvelDbContext;
        }

        public async Task SaveFavoriteCharacter(int marvelId)
        {
            var query = "INSERT INTO dbo.FAVORITE_CHARACTER (MarvelId) values (@MarvelId)";

            using var connection = _marvelDbContext.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            var parameter = new DynamicParameters();
            parameter.Add("MarvelId", marvelId, DbType.Int64);

            await connection.ExecuteAsync(query, parameter, transaction);

            transaction.Commit();
            connection.Close();
        }

        public async Task<List<FavoriteCharacter>> GetAll()
        {
            var query = "SELECT * FROM [dbo].[FAVORITE_CHARACTER]";

            using var connection = _marvelDbContext.CreateConnection();
            var result = await connection.QueryAsync<FavoriteCharacter>(query);
            return result.ToList();
        }

        public async Task<FavoriteCharacter> GetById(int marvelId)
        {
            var query = "SELECT * FROM FAVORITE_CHARACTER WHERE Marvelid = @Marvelid";

            var parameter = new DynamicParameters();
            parameter.Add("MarvelId", marvelId, DbType.Int64);

            using var connection = _marvelDbContext.CreateConnection();
            var result = await connection.QuerySingleAsync<FavoriteCharacter>(query, parameter);
            return result;
        }

        public async Task RemoveFavoriteCharacter(int marvelId)
        {
            var query = "DELETE FROM FAVORITE_CHARACTER where MarvelId = @Marvelid";

            var parameter = new DynamicParameters();
            parameter.Add("MarvelId", marvelId, DbType.Int64);

            using var connection = _marvelDbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            
            await connection.ExecuteAsync(query, parameter, transaction);

            transaction.Commit();
            connection.Close();
        }
    }
}
