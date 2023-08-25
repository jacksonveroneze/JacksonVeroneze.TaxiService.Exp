using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Stub;

public static class UserDatabase
{
    private static readonly List<UserEntity> Db = new();

    public static IList<UserEntity> Data
    {
        get
        {
            if (Db.Any())
            {
                return Db;
            }

            Db.AddRange(Enumerable.Range(1, 25)
                .Select(item =>
                {
                    Random rnd = new();

                    UserEntity user = new(new NameValueObject($"User_{item}"), DateTime.Now, Gender.Male);

                    _ = Enumerable.Range(1, rnd.Next(2, 5))
                        .Select(i =>
                        {
                            EmailEntity email = new(new EmailValueObject($"User_{i}@mail.com"));

                            user.AddEmail(email);

                            return email;
                        });

                    _ = Enumerable.Range(1, rnd.Next(2, 5))
                        .Select(i =>
                        {
                            PhoneEntity phone = new(new PhoneValueObject($"(49) 99999-999{item}"));

                            user.AddPhone(phone);

                            return phone;
                        });

                    return user;
                }));

            return Db;
        }
    }
}
