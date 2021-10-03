using System;
using NovelistsApi.Infrastructure.Features.Users;

namespace NovelistsApi.Infrastructure.Features.Publications;

public record PublicationDto
{
    public Guid Id { get; init; }
    public UserDto User { get; init; }
    public string Title { get; init; } = default;
    public string? Synopsis { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}