using System;
namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {
            
        }
    }
}