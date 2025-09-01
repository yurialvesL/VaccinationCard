using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;
using VaccinationCard.Application.Commands.Vaccines.CreateVaccine;
using VaccinationCard.Application.Common.Mappings.Vaccine;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCardHandlerTests.Commands.Vaccinations;

namespace VaccinationCardHandlerTests.Commands.Vaccines;

public class CreateVaccineHandlerTests
{
    private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
    private readonly Mock<ILogger<CreateVaccineHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public CreateVaccineHandlerTests()
    {
        _vaccineRepositoryMock = new Mock<IVaccineRepository>();
        _loggerMock = new Mock<ILogger<CreateVaccineHandler>>();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new VaccineProfile());
        }, loggerFactory);

        _mapper = configuration.CreateMapper();
    }

    private CreateVaccineHandler SUT() => new(
   _vaccineRepositoryMock.Object,
   _loggerMock.Object,
   _mapper
   );

    [Fact]
    public async Task Should_throw_Conflict_when_vaccine_with_same_name_exists()
    {
        // arrange
        var cmd = new CreateVaccineCommand { Name = "Covid"};
        _vaccineRepositoryMock.Setup(r => r.GetVaccineByNameAsync(cmd.Name, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new Vaccine { Id = Guid.NewGuid(), Name = cmd.Name });

        // act + assert
        await Assert.ThrowsAsync<ConflictException>(() => SUT().Handle(cmd, default));

        _vaccineRepositoryMock.Verify(r => r.CreateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Should_create_log_and_return_result_on_success()
    {
        // arrange
        var cmd = new CreateVaccineCommand { Name = "Influenza" };

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByNameAsync(cmd.Name, It.IsAny<CancellationToken>()))
             .ReturnsAsync((Vaccine?)null);

        var created = new Vaccine { Id = Guid.NewGuid(), Name = cmd.Name };
        _vaccineRepositoryMock.Setup(r => r.CreateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(created);

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.Should().NotBeNull();
        result.Id.Should().Be(created.Id);
        result.Name.Should().Be("Influenza");

        _vaccineRepositoryMock.Verify(r => r.CreateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()), Times.Once);
        _loggerMock.VerifyLog(LogLevel.Information, $"Vaccine {created.Id} created successfully");
    }
}
