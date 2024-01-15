namespace Marvel.Application.InputModel
{
    public class FiltersCharactersInputModel
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 5;
        public string? Name { get; set; }
        public string? NameStartsWith { get; set; }
        public bool OrderByNameAsc { get; set; } = true;

    }
}
