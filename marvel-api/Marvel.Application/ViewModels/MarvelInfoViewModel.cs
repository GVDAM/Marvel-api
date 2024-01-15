using Marvel.Core.Domains;
using Marvel.Infra.DTOs;

namespace Marvel.Application.ViewModels
{
    public class DataViewModel
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public int OffSet { get; set; }
        public int CurrentPage { get; set; }
        public List<CharacterViewModel> Results { get; set; }

        public static DataViewModel FromEntity(Data entity)
        {
            var data = new DataViewModel
            {
                Count = entity.Count,
                Limit = entity.Limit,
                OffSet = entity.OffSet,
                Total = entity.Total,
                CurrentPage = entity.CurrentPage,
            };

            data.Results = entity.Results.Select(result => CharacterViewModel.FromEntity(result)).ToList();

            return data;
        }
    }
}
