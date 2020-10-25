using System.ComponentModel.DataAnnotations;

namespace bs.LicensesManager.Core.ViewModel
{
    public class LicenseTypeViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Valore")]
        [MaxLength(50)]
        public string Value { get; set; }
    }
}
