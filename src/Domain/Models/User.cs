using System;
using NovelistsApi.Domain.Common;

namespace NovelistsApi.Domain.Models
{
    public class User : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
