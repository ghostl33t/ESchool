using EmailService.Interface;

namespace EmailService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IGradesEmail _gradesEmail ;
    public Worker(ILogger<Worker> logger, IGradesEmail gradesEmail)
    {
        _logger = logger;
        _gradesEmail = gradesEmail;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _gradesEmail.Execute();
            await Task.Delay(5000, stoppingToken);
        }
    }
}