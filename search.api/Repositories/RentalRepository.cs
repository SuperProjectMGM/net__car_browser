using search.api.Interfaces;

namespace search.api.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly IEmailInterface _emailService;
    private readonly IConfiguration _configuration;

    public RentalRepository(IEmailInterface emailService, IConfiguration configuration)
    {
        _emailService = emailService;
        _configuration = configuration;
    }
    
    
}