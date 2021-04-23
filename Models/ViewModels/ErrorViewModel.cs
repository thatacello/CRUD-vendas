using System;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
