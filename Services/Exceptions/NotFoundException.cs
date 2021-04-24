using System;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
            
        }
    }
}