using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;
using VaccinationCard.Application.Commands.Vaccinations.DeleteVaccinationById;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCardHandlerTests.Commands.Vaccinations;

public class DeleteVaccinationHandlerTests
{
    private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
    private readonly Mock<ILogger<DeleteVaccinationByIdHandler>> _loggerMock;

    public DeleteVaccinationHandlerTests()
    {
        _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
        _loggerMock = new Mock<ILogger<DeleteVaccinationByIdHandler>>();
    }

    private DeleteVaccinationByIdHandler SUT() => new(
   _vaccinationRepositoryMock.Object,
   _loggerMock.Object
   );

    [Fact]
    public async Task Should_throw_NotFound_when_vaccination_not_found()
    {
        //arrange
        var id = Guid.NewGuid();
        var cmd = new DeleteVaccinationByIdCommand { VaccinationId = id };

        // act & assert
        _vaccinationRepositoryMock.Setup(r => r.GetVaccinationByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Vaccination?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));

        _vaccinationRepositoryMock.Verify(r => r.DeleteVaccinationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Should_return_false_and_log_error_when_delete_fails()
    {
        //arrange
        var id = Guid.NewGuid();
        _vaccinationRepositoryMock.Setup(r => r.GetVaccinationByIdAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new Vaccination { Id = id });
        _vaccinationRepositoryMock.Setup(r => r.DeleteVaccinationAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(false);

        var cmd = new DeleteVaccinationByIdCommand { VaccinationId = id };

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.IsDeleted.Should().BeFalse();

        // log 
        _loggerMock.VerifyLog(LogLevel.Error, $"Failed to delete vaccine with ID {id}.");
        _vaccinationRepositoryMock.Verify(r => r.DeleteVaccinationAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_True_And_LogInformation_When_Delete_Succeeds()
    {
        // arrange
        var id = Guid.NewGuid();
        _vaccinationRepositoryMock.Setup(r => r.GetVaccinationByIdAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new Vaccination { Id = id });
        _vaccinationRepositoryMock.Setup(r => r.DeleteVaccinationAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true);

        var cmd = new DeleteVaccinationByIdCommand { VaccinationId = id};

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.IsDeleted.Should().BeTrue();
        _vaccinationRepositoryMock.Verify(r => r.DeleteVaccinationAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}






/// <summary>
/// Helper para verificar logs com Moq (match por substring, case-insensitive)
/// </summary>
public static class LoggerMoqExtensions
{
    public static void VerifyLog<T>(
        this Mock<ILogger<T>> logger,
        LogLevel level,
        string containsSubstring,
        Times? times = null)
    {
        times ??= Times.Once();

        logger.Verify(x => x.Log(
            It.Is<LogLevel>(l => l == level),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) =>
                v.ToString()!.Contains(containsSubstring, StringComparison.OrdinalIgnoreCase)),
            It.IsAny<Exception?>(),
            It.Is<Func<It.IsAnyType, Exception?, string>>((_, _) => true)
        ), times.Value);
    }
}