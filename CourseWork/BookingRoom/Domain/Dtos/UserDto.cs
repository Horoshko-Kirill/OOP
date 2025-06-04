using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Dtos
{
    public sealed record UserDto(
    int Id,
    string Name,
    string Email,
    string Password,
    UserRole Role);
}
