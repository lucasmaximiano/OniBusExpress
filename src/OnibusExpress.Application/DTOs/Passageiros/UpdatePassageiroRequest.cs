using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnibusExpress.Application.DTOs.Passageiros
{
    public record UpdatePassageiroRequest(
    string Nome,
    string Email,
    DateTime DataNascimento);
}
