// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

namespace UnitTests
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Activities.Commands;

    using Domain.Repositories;

    using NSubstitute;

    [TestFixture]
    public class CreateCommandHandlerTest
    {
        private CreateCommandHandler _handler;
        private IActivitiesRepository _activitiesRepository;

        public CreateCommandHandlerTest()
        {
            _activitiesRepository = Substitute.For<IActivitiesRepository>();
            _handler = new CreateCommandHandler(_activitiesRepository);
        }

        [Test]
        public async Task EmptyCommand_ThrowException_Test()
        {
            //Arrange
            var command = new CreateCommand();

            //Assert
            Assert.That(async () => await _handler.Handle(command, CancellationToken.None), Throws.Exception);
        }
    }
}
