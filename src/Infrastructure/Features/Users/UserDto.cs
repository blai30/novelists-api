using System;

namespace NovelistsApi.Infrastructure.Features.Users
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = default;
        public string DisplayName { get; init; } = default;
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
