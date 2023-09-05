using System.Collections.Concurrent;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Stub;

public static class UserDatabase
{
    private static ConcurrentBag<UserEntity> _db = new();

    public static ConcurrentBag<UserEntity> Data
    {
        get
        {
            if (_db.Any())
            {
                return _db;
            }

            IEnumerable<UserEntity> collection = Enumerable.Range(1, 25)
                .Select(item =>
                {
                    Random rnd = new();

                    UserEntity user = new(NameValueObject.Create($"User_{item}").Value!,
                        DateOnly.FromDateTime(DateTime.UtcNow), Gender.Male,
                        CpfValueObject.Create("000.000.000-00").Value!);

                    _ = Enumerable.Range(1, rnd.Next(2, 5))
                        .Select(i =>
                        {
                            EmailEntity email = new(user, EmailValueObject.Create($"User_{i}@mail.com").Value!);

                            user.AddEmail(email);

                            return email;
                        });

                    _ = Enumerable.Range(1, rnd.Next(2, 5))
                        .Select(i =>
                        {
                            PhoneEntity phone = new(PhoneValueObject.Create($"(49) 99999-999{i}").Value!);

                            //user.AddPhone(phone);

                            return phone;
                        });

                    return user;
                });

            _db = new ConcurrentBag<UserEntity>(collection);

            return _db;
        }
    }
}
