using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bs.LicensesManager.Core.ViewModel
{
    public class FeatureViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Chiave")]
        [MaxLength(32)]
        [Required]
        public string Key { get; set; }

        [Display(Name = "Attivo")]
        public bool Active { get; set; }

        [Display(Name = "Nome prodotto")]
        public bool ProductName { get; set; }

        [Display(Name = "Id prodotto")]
        [Required]
        public bool ProductId { get; set; }

        public IEnumerable<LicenseTokenFeatureViewModel> Values { get; set; }

    }
}
