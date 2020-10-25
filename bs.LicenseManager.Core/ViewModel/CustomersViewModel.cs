using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bs.LicensesManager.Core.ViewModel
{
    public class CustomersViewModel
    {
        public IList<CustomerViewModel> CustomersList { get; set; }
       

    }

    public class CustomerViewModel
    {
        public string Id { get; set; }
       [Display(Name="Nome")]
        public string Name { get; set; }
       [Display(Name="Email di contatto")]
        public string EmailContact { get; set; }
       [Display(Name="Attivo")]
        public bool Active { get; set; }
    }
}
