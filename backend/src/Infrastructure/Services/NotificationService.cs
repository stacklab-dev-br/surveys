using StackLab.Survey.Application.Common.Interfaces;
using StackLab.Survey.Domain.Entities;

namespace StackLab.Survey.Infrastructure.Services;
public class NotificationService : INotificationService
{
    public async Task SendPasswordResetTokenMessage(string address, VerificationToken token)
    {
        await Task.Delay(1000);
        return;
    }

    public async Task SendVerificationTokenMessage(string address, VerificationToken token)
    {
        await Task.Delay(1000);
        return;
    }
}
