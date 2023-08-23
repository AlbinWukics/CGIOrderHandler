namespace OrderHandler.DomainCommons.Services.Interfaces;

public interface IRepository<T> where T : class
{
    Task<ServiceResponse<T>> AddAsync(T dto);
    Task<ServiceResponse<T>> GetByIdAsync(Guid id);
    Task<ServiceResponse<IReadOnlyCollection<T>>> GetAllAsync();
    Task<ServiceResponse<T>> UpdateAsync(T dto);
    Task<ServiceResponse<T>> RemoveAsync(Guid id);
}