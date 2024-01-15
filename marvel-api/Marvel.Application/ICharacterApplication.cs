using Marvel.Application.InputModel;
using Marvel.Application.ViewModels;

namespace Marvel.Application
{
    public interface ICharacterApplication
    {
        Task<DataViewModel> GetCharacter(FiltersCharactersInputModel filters);
        Task SetFavorite(int marvelId);
        Task<IEnumerable<FavoriteCharacterViewModel>> GetFavorite();
        Task RemoveFavorite(int marvelId);
    }
}
