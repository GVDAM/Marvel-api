using Marvel.Core.Domains;

namespace Marvel.Infra
{
    public interface IMarvelService
    {
        Task<Data> GetCharacters(int skip, int take, bool orderByName, string? nameStartsWith);
        Task<Character> GetCharacter(int idCharacter);
    }
}
