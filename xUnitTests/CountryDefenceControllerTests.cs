using System;
using Moq;
using TestedApplication._Internal;
using TestedApplication.HardToTest;
using Xunit;

namespace xUnitTests
{
    public class CountryDefenceControllerTests
    {
        private Mock<IMissileLauncher> _rocketLauncherMock;

        public CountryDefenceControllerTests()
        {
           _rocketLauncherMock =  new Mock<IMissileLauncher>();
        }

        [Fact]
        public void DestroyEnemy_With_Correct_Code_Launches_Missile()
        {
            //arrange
            _rocketLauncherMock.Setup(missileLauncher => missileLauncher.LaunchMissile()).Returns(true);
            var launcher = _rocketLauncherMock.Object;

            const string passCode = "Password";

            var defenceController = new CountryDefenceController(launcher, passCode);

            //act
            defenceController.DestroyEnemy(passCode);

            //Check if LaunchMissile method was called exactly once.
            _rocketLauncherMock.Verify(missileLauncher => missileLauncher.LaunchMissile(),() => Times.Once());

        }

        [Fact]
        public void DestroyEnemy_With_Incorrect_Code_Throws_AuthorizationFailedException()
        {
            //arrange
            _rocketLauncherMock.Setup(missileLauncher => missileLauncher.LaunchMissile()).Returns(true);
            var launcher = _rocketLauncherMock.Object;

            const string passCode = "Password";

            var defenceController = new CountryDefenceController(launcher, passCode);

            //act
            Assert.Throws<AuthorizationFailedException>(() => defenceController.DestroyEnemy("1234"));

            //Check if LaunchMissile method was never called.
            _rocketLauncherMock.Verify(missileLauncher => missileLauncher.LaunchMissile(),() => Times.Never());
        }

        [Fact]
        public void DestroyEnemy_Throws_MissileException_When_Launch_Fails()
        {
            //arrange
            _rocketLauncherMock.Setup(missileLauncher => missileLauncher.LaunchMissile()).Returns(false);
            var launcher = _rocketLauncherMock.Object;

            const string passCode = "Password";

            var defenceController = new CountryDefenceController(launcher, passCode);

            //act
            Assert.Throws<MissileException>(() => defenceController.DestroyEnemy(passCode));

            //Check if LaunchMissile method was called once.
            _rocketLauncherMock.Verify(missileLauncher => missileLauncher.LaunchMissile(),() => Times.Once());
        }
    }
}