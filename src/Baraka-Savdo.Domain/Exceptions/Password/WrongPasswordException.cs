using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Exceptions.Password;

public class WrongPasswordException : NotFoundException
{
    public WrongPasswordException()
    {
        TitleMessage = "Wrong password !";
    }
}
