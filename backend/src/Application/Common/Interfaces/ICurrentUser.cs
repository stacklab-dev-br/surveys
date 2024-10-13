using StackLab.Survey.Domain.Entities;

namespace StackLab.Survey.Application.Common.Interfaces;
public interface ICurrentUser
{
    int? Id { get; }
    string? Email { get; }
    string? Name { get; }
}
