using System;

namespace ParrotWings
{
    public class AuthenticateException : Exception
    {
        public AuthenticateException(string message) : base(message) { }
    }
}