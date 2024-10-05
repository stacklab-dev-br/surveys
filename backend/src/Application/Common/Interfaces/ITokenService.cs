using StackLab.Survey.Domain.Entities;

namespace StackLab.Survey.Application.Common.Interfaces;

public interface ITokenService
{
    string GetToken(User user);
}
