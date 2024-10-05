using MediatR;
using Microsoft.EntityFrameworkCore;
using StackLab.Survey.Application.Common.Interfaces;
using StackLab.Survey.Application.Exceptions;

namespace StackLab.Survey.Application.Requests.Auth.Commands;
public class ResetPasswordCommand : IRequest<Unit>
{
    public string Password { get; set; }
    public string Token { get; set; }

    private string Login { get; set; }

    public void SetLogin(string login)
    {

        Login = login;
    }

    public string GetLogin()
    {
        return Login;
    }
}

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly INotificationService _emailService;
    public ResetPasswordCommandHandler(IApplicationDbContext context, INotificationService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.VerificationTokens)
            .FirstOrDefaultAsync(x => x.Login == request.GetLogin());

        if (user == null || !user.ValidateToken(request.Token))
        {
            throw new UnauthorizedException();
        }

        user.SetPassword(request.Password);

        user.ClearAllVerificationTokens();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

