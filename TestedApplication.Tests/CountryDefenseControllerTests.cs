using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TestedApplication._Internal;
using TestedApplication.HardToTest;
using Xunit;

namespace TestedApplication.Tests
{
    public class CountryDefenseControllerTests
    {
        private Mock<IMissileLauncher> _mock;
        private const string authenticationCode = "Password";

        public CountryDefenseControllerTests()
        {
            _mock = new Mock<IMissileLauncher>();

        }
        [Fact]
        public void DestroyEnemy_With_Correct_Code_Should_Launch_Missile()
        {
            //arrange
            _mock.Setup(l => l.LaunchMissile()).Returns(true);
            var launcher = _mock.Object;
            
            var defenseController = new CountryDefenseController(launcher,authenticationCode);
            //act

            defenseController.DestroyEnemy(authenticationCode);

            //assert

            _mock.Verify(missileLauncher => missileLauncher.LaunchMissile(),Times.Once);

        }
        [Fact]
        public void DestroyEnemy_With_Incorrect_Code_Should_Throw_AuthorizationFailedException()
        {
            //arrange
            _mock.Setup(l => l.LaunchMissile()).Returns(true);
            var launcher = _mock.Object;

            var defenseController = new CountryDefenseController(launcher,authenticationCode);

            //act & assert
            Assert.Throws<AuthorizationFailedException>(() => defenseController.DestroyEnemy("BadCode"));

            _mock.Verify(missileLauncher => missileLauncher.LaunchMissile(),Times.Never);

        }
        [Fact]
        public void DestroyEnemy_Should_Throw_MissileException_When_Launch_Fails()
        {
            //arrange
            _mock.Setup(l => l.LaunchMissile()).Returns(false);
            var launcher = _mock.Object;

            var defenseController = new CountryDefenseController(launcher,authenticationCode);

            //act & assert
            Assert.Throws<MissileException>(() => defenseController.DestroyEnemy(authenticationCode));

            _mock.Verify(missileLauncher => missileLauncher.LaunchMissile(),Times.Once);

        }
    }
}
