using Marvel.Application.InputModel;
using Marvel.Application.ViewModels;
using Marvel.Core.Domains;
using Marvel.Infra;
using Marvel.Infra.Repository.Interface;

namespace Marvel.Application
{
    public class CharacterApplication : ICharacterApplication
    {
        private readonly IMarvelService _marvelService;
        private readonly IFavoriteMarvelCharacterRepository _favoriteMarvelCharacterRepository;

        public CharacterApplication(IMarvelService marvelService,
            IFavoriteMarvelCharacterRepository favoriteMarvelCharacterRepository)
        {
            _marvelService = marvelService;
            _favoriteMarvelCharacterRepository = favoriteMarvelCharacterRepository;
        }

        public async Task<DataViewModel> GetCharacter(FiltersCharactersInputModel filters)
        {
            try
            {
                var allFavorits = new List<FavoriteCharacter>();

                if (filters.Skip == 0)
                    allFavorits = await _favoriteMarvelCharacterRepository.GetAll();

                var result = new Data();

                foreach (var fav in allFavorits)
                {
                    var character = await _marvelService.GetCharacter(fav.MarvelId);
                    character.IsFav = true;
                    result.Results.Add(character);
                }

                var take = filters.Take;
                filters.Take -= result.Results.Count;

                var responseMarvel = await _marvelService.GetCharacters(filters.Skip, filters.Take, filters.OrderByNameAsc, filters.NameStartsWith);

                if (result.Results.Count < take)
                {
                    result.Results.AddRange(responseMarvel.Results);
                }
                result.Count = result.Results.Count;
                result.Limit = result.Results.Count;
                result.Total = responseMarvel.Total;
                result.CurrentPage = (int)Math.Floor((double)filters.Skip / filters.Take) + 1;

                result.Results = result.Results.OrderBy(x => x.IsFav).ThenBy(x => x.Name).ToList();

                return DataViewModel.FromEntity(result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro não esperado", ex);
            }            
        }

        public async Task SetFavorite(int marvelId)
        {
            try
            {
                var allFavorits = await _favoriteMarvelCharacterRepository.GetAll();

                if (allFavorits.Count() >= 5)
                    throw new ApplicationException("Não pode salvar mais do que 5 personagens como favorito");

                if (allFavorits.Select(x => x.MarvelId).Contains(marvelId))
                    throw new ApplicationException("Personagem já está salvo como favorito");

                await _favoriteMarvelCharacterRepository.SaveFavoriteCharacter(marvelId);
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro não esperado", ex);
            }            
        }

        public async Task<IEnumerable<FavoriteCharacterViewModel>> GetFavorite()
        {
            try
            {
                var allFavorits = await _favoriteMarvelCharacterRepository.GetAll();
                return allFavorits.Select(fav => FavoriteCharacterViewModel.FromEntity(fav));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro não esperado", ex);
            }            
        }

        public async Task RemoveFavorite(int marvelId)
        {
            try
            {
                var entity = await _favoriteMarvelCharacterRepository.GetById(marvelId);

                if (entity != null)
                {
                    await _favoriteMarvelCharacterRepository.RemoveFavoriteCharacter(marvelId);
                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro não esperado", ex);
            }
        }
    }
}
