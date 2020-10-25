using System.ComponentModel.DataAnnotations;

namespace bs.LicensesManager.Core.ViewModel
{
    public class LicenseTokenFeatureViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Valore")]
        [MaxLength(32)]
        [Required]
        public string Value { get; set; }

        [Display(Name = "Attivo")]
        public bool Active { get; set; }

        [Display(Name = "Nome prodotto")]
        public bool ProductName { get; set; }

        [Display(Name = "Id prodotto")]
        [Required]
        public bool ProductId { get; set; }

        [Display(Name = "Id token licenza")]
        [Required]
        public bool LicenseTokenId { get; set; }
    }
}
