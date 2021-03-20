using TestedApplication._Internal;

namespace TestedApplication.HardToTest
{
    public class CountryDefenceController
    {
        private readonly IMissileLauncher Launcher;
        private readonly string _passCode;

        public CountryDefenceController(IMissileLauncher launcher,string passCode)
        {
            Launcher = launcher;
            _passCode = passCode;
        }
        public void DestroyEnemy(string secretCode)
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

        private bool IsAuthorized(string secretCode)
        {
            return secretCode==_passCode;
        }
    }
}