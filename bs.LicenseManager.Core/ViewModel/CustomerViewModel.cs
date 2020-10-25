using System.ComponentModel.DataAnnotations;

namespace bs.LicenseManager.Core.ViewModel
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Email di contatto")]
        public string EmailContact { get; set; }
        [Display(Name = "Attivo")]
        public bool Active { get; set; }
    }
}
