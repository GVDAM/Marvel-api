using Marvel.Core.Domains;

namespace Marvel.Application.ViewModels
{
    public class FavoriteCharacterViewModel
    {
        public int Id { get; set; }
        public int MarvelId { get; set; }

        public static FavoriteCharacterViewModel FromEntity(FavoriteCharacter entity)
        {
            return new FavoriteCharacterViewModel()
            {
                Id = entity.Id,
                MarvelId = entity.MarvelId,
            };
        }
    }
}
