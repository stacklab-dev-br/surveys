using StackLab.Survey.Domain.Entities;

namespace StackLab.Survey.Application.Common.Interfaces;
public interface INotificationService
{
    public Task SendPasswordResetTokenMessage(string address, VerificationToken token);
    public Task SendVerificationTokenMessage(string address, VerificationToken token);
}
