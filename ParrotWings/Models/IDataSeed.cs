using ParrotWings.Models;

namespace ParrotWings.Models
{
    public interface IDataSeed
    {
        User[] GetUsers();
        Payment[] GetPayments();
    }
}