// using JacksonVeroneze.NET.MongoDB.Interfaces;
// using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
// using JacksonVeroneze.TemplateWebApi.Domain.Entities;
//
// namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Mongo;
//
// public class UserWriteRepository : IUserWriteRepository
// {
//     private readonly IBaseRepository<UserEntity, Guid> _repository;
//
//     public UserWriteRepository(
//         IBaseRepository<UserEntity, Guid> repository)
//     {
//         _repository = repository;
//     }
//
//     public Task CreateAsync(UserEntity entity,
//         CancellationToken cancellationToken = default)
//     {
//         return _repository.CreateAsync(
//             entity, cancellationToken);
//     }
//
//     public Task DeleteAsync(UserEntity entity,
//         CancellationToken cancellationToken = default)
//     {
//         return _repository.DeleteAsync(
//             entity, cancellationToken);
//     }
//
//     public Task UpdateAsync(UserEntity entity,
//         CancellationToken cancellationToken = default)
//     {
//         return _repository.UpdateAsync(
//             entity, cancellationToken);
//     }
// }



