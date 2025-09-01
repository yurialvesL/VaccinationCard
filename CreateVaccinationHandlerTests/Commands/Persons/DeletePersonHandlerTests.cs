using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Person.DeletePerson;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCardHandlerTests.Commands.Vaccinations;

namespace VaccinationCardHandlerTests.Commands.Persons;

public class DeletePersonHandlerTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<ILogger<DeletePersonHandler>> _loggerMock;


    public DeletePersonHandlerTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _loggerMock = new Mock<ILogger<DeletePersonHandler>>();
    }

    private DeletePersonHandler SUT() => new(_personRepositoryMock.Object, _loggerMock.Object);

    [Fact]
    public async Task Should_throw_NotFound_when_person_not_exists()
    {
        // arrange
        var id = Guid.NewGuid();
        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(id, It.IsAny<CancellationToken>()))
               .ReturnsAsync((Person?)null);

        var cmd = new DeletePersonCommand { PersonId = id};

        // act + assert
        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));

        _personRepositoryMock.Verify(r => r.DeletePersonAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Should_return_false_and_log_error_when_delete_fails()
    {
        // arrange
        var id = Guid.NewGuid();
        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(id, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Person { Id = id });
        _personRepositoryMock.Setup(r => r.DeletePersonAsync(id, It.IsAny<CancellationToken>()))
               .ReturnsAsync(false);

        var cmd = new DeletePersonCommand { PersonId = id };

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.IsDelete.Should().BeFalse();
        _loggerMock.VerifyLog(LogLevel.Error, "Failed to delete person with ID");
        _personRepositoryMock.Verify(r => r.DeletePersonAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_return_true_and_log_information_when_delete_succeeds()
    {
        // arrange
        var id = Guid.NewGuid();
        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(id, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Person { Id = id });
        _personRepositoryMock.Setup(r => r.DeletePersonAsync(id, It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);

        var cmd = new DeletePersonCommand { PersonId = id};

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.IsDelete.Should().BeTrue();
        _loggerMock.VerifyLog(LogLevel.Information, "deleted successfully");
        _personRepositoryMock.Verify(r => r.DeletePersonAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}
