using Marvel.Core.Domains;

namespace Marvel.Application.ViewModels
{
    public class CharacterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string? Thumbnail { get; set; }
        public bool IsFav { get; set; }


        public Character ToEntity()
        {
            var entity = new Character
            {
                Id = Id,
                Name = Name,
                Description = Description
            };

            return entity;
        }

        public static CharacterViewModel FromEntity(Character entity)
        {
            var viewModel = new CharacterViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                IsFav = entity.IsFav,
                Description = entity.Description,
                Thumbnail = entity.Thumbnail != null ? $"{entity.Thumbnail.Path}.{entity.Thumbnail.Extension}" : String.Empty
            };

            return viewModel;
        }

    }
}
