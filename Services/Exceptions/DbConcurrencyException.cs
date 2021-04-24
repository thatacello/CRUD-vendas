using System;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message) : base(message)
        {

        }
    }
}