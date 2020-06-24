namespace RESTfulApi.Api.Services
{
    public interface IPropertyCheckerService
    {
        bool TypeHasProperites<T>(string fields);
    }
}