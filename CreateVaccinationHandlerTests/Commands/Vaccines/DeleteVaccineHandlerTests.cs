using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Vaccines.DeleteVaccine;
using VaccinationCard.Application.Common.Mappings.Vaccine;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCardHandlerTests.Commands.Vaccinations;

namespace VaccinationCardHandlerTests.Commands.Vaccines;

public class DeleteVaccineHandlerTests
{
    private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
    private readonly Mock<ILogger<DeleteVaccineHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public DeleteVaccineHandlerTests()
    {
        _vaccineRepositoryMock = new Mock<IVaccineRepository>();
        _loggerMock = new Mock<ILogger<DeleteVaccineHandler>>();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new VaccineProfile());
        }, loggerFactory);

        _mapper = mapperConfig.CreateMapper();
    }
    private DeleteVaccineHandler SUT() => new(
   _vaccineRepositoryMock.Object,
   _loggerMock.Object,
   _mapper
   );


    [Fact]
    public async Task Should_throw_NotFound_when_vaccine_not_found()
    {
        //arrange
        var id = Guid.NewGuid();
        var cmd = new DeleteVaccineCommand { Id = id };

        // act & assert
        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Vaccine?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));

        _vaccineRepositoryMock.Verify(r => r.DeleteVaccineAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Should_return_false_and_log_error_when_delete_fails()
    {
        //arrange
        var id = Guid.NewGuid();

        var cmd = new DeleteVaccineCommand { Id = id };
        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new Vaccine { Id = id, Name= "BCG"  });
        _vaccineRepositoryMock.Setup(r => r.DeleteVaccineAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(false);

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.IsDeleted.Should().BeFalse();

        // log 
        _loggerMock.VerifyLog(LogLevel.Error, $"Failed to delete vaccine with ID {id}.");
        _vaccineRepositoryMock.Verify(r => r.DeleteVaccineAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_True_And_LogInformation_When_Delete_Succeeds()
    {
        // arrange
        var id = Guid.NewGuid();
        var cmd = new DeleteVaccineCommand { Id = id };
        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new Vaccine { Id = id, Name = "BCG" });
        _vaccineRepositoryMock.Setup(r => r.DeleteVaccineAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true);

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.IsDeleted.Should().BeTrue();
        _vaccineRepositoryMock.Verify(r => r.DeleteVaccineAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

}
