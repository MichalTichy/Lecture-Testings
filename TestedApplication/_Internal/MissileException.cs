using System;

namespace TestedApplication._Internal
{
    public class MissileException : Exception
    {
        public MissileException() : base("Missile launch failed!")
        {
        }
    }
    public class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException() : base("Provided passCode is not correct!")
        {
        }
    }
}