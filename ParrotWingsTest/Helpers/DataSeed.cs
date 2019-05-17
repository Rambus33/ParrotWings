using ParrotWings.Models;
using System;

namespace ParrotWingsTests.Helpers
{
    public class DataSeed : IDataSeed
    {
        public Payment[] GetPayments()
        {
            return new[]
            {
                new Payment {Amount = 500, CorrespondentUserId = 1, Date = new DateTime(2019, 4, 19, 12, 19, 1), PaymentId = 1, UserId = UserFactory.CurrentUserId},
                new Payment {Amount = 10, CorrespondentUserId = 4, Date = new DateTime(2019, 5, 1, 11, 5, 5), PaymentId = 2, UserId = UserFactory.CurrentUserId},
                new Payment {Amount = 20, CorrespondentUserId = 3, Date = new DateTime(2019, 5, 2, 23, 58, 56), PaymentId = 3, UserId = UserFactory.CurrentUserId},
                new Payment {Amount = 20, CorrespondentUserId = 3, Date = new DateTime(2019, 5, 3, 20, 4, 45), PaymentId = 4, UserId = UserFactory.CurrentUserId}
            };
        }

        public User[] GetUsers()
        {
            return new[]
            {
                new User {Email = null, Name = "System", UserId = 1, Password = null},
                new User {Email = UserFactory.CurrentUserEmail, Name = UserFactory.CurrentUserName, UserId = UserFactory.CurrentUserId, Password = UserFactory.CurrentUserPassword},
                new User {Email = "ralph.morrow@mail.com", Name = "Ralph Morrow", UserId = 3, Password = "asdfg"},
                new User {Email = "jeremy.hawkins@mail.com", Name = "Jeremy Hawkins", UserId = 4, Password = "zxcvb"}
            };
        }
    }
}