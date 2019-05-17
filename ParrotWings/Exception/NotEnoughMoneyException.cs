using System;

namespace ParrotWings
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string message) : base(message) { }
    }
}