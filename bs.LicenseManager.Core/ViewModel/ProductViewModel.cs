using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bs.LicenseManager.Core.ViewModel
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Descrizione")]
        [MaxLength(500)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Attivo")]
        public bool Active { get; set; }

        [Display(Name = "Versioni")]
        public IEnumerable<VersionViewModel> Versions { get; set; }

        [Display(Name = "Features")]
        public IEnumerable<FeatureViewModel> Features { get; set; }

        [Display(Name = "Licenze emesse")]
        public IEnumerable<LicenseTokenViewModel> LicenseTokens { get; set; }
    }
}
