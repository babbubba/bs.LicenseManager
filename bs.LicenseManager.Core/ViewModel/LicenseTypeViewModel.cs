using System.ComponentModel.DataAnnotations;

namespace bs.LicenseManager.Core.ViewModel
{
    public class LicenseTypeViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Valore")]
        [MaxLength(50)]
        public string Value { get; set; }
    }
}
