using TestedApplication._Internal;

namespace TestedApplication.HardToTest
{
    public class CountryDefenceController
    {
        private static readonly NuclerMissileLauncher Launcher = new NuclerMissileLauncher();
        public static void DestroyEnemy(string secretCode)
        {
            if (!IsAuthorized(secretCode))
            {
                throw new AuthorizationFailedException();
            }

            var succeed=Launcher.LaunchMissile();

            if (!succeed)
            {
                throw new MissileException();
            }
        }

        private static bool IsAuthorized(string secretCode)
        {
            return secretCode=="SuperSecretPassword";
        }
    }
}