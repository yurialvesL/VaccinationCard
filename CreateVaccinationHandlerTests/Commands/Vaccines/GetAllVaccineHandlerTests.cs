using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;
using VaccinationCard.Application.Common.Mappings.Vaccine;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCardHandlerTests.Commands.Vaccinations;

namespace VaccinationCardHandlerTests.Commands.Vaccines;

public class GetAllVaccineHandlerTests
{
    private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
    private readonly Mock<ILogger<GetAllVaccineHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public GetAllVaccineHandlerTests()
    {
        _vaccineRepositoryMock = new Mock<IVaccineRepository>();
        _loggerMock = new Mock<ILogger<GetAllVaccineHandler>>();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new VaccineProfile());
        }, loggerFactory);

        _mapper = configuration.CreateMapper();
    }

    private GetAllVaccineHandler SUT() => new(
      _vaccineRepositoryMock.Object,
      _loggerMock.Object,
      _mapper
      );

    [Fact]
    public async Task Should_return_mapped_list_on_success()
    {
        // arrange
        var vaccines = new List<Vaccine>
        {
            new() { Id = Guid.NewGuid(), Name = "Hep B" },
            new() { Id = Guid.NewGuid(), Name = "Influenza" }
        };
        _vaccineRepositoryMock.Setup(r => r.GetAllVaccinesAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(vaccines);

        var cmd = new GetAllVaccineCommand();

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.Should().NotBeNull();
        result.Vaccines.Should().HaveCount(2);
        result.Vaccines![0].Name.Should().Be("Hep B");
        result.Vaccines![1].Name.Should().Be("Influenza");

        // não registrou erro
        _loggerMock.VerifyNoLog(LogLevel.Error);
    }

    [Fact]
    public async Task Should_return_empty_list_and_log_error_when_repository_returns_null()
    {
        // arrange
        _vaccineRepositoryMock.Setup(r => r.GetAllVaccinesAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync((List<Vaccine>?)null);

        var cmd = new GetAllVaccineCommand();

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.Should().NotBeNull();
        result.Vaccines.Should().NotBeNull().And.BeEmpty();

        _loggerMock.VerifyLog(LogLevel.Error, "retrieving vaccines");
    }

    [Fact]
    public async Task Should_return_empty_list_and_log_error_when_repository_throws()
    {
        // arrange
        _vaccineRepositoryMock.Setup(r => r.GetAllVaccinesAsync(It.IsAny<CancellationToken>()))
             .ThrowsAsync(new Exception("boom"));

        var cmd = new GetAllVaccineCommand();

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.Should().NotBeNull();
        result.Vaccines.Should().NotBeNull().And.BeEmpty();

        _loggerMock.VerifyLog(LogLevel.Error, "retrieving vaccines");
    }
}
