using Marvel.Core.Domains;

namespace Marvel.Infra.Repository.Interface
{
    public interface IFavoriteMarvelCharacterRepository
    {
        Task SaveFavoriteCharacter(int marvelId);
        Task<List<FavoriteCharacter>> GetAll();
        Task<FavoriteCharacter> GetById(int marvelId);
        Task RemoveFavoriteCharacter(int marvelId);
    }
}
