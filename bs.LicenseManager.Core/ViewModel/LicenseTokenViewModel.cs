using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bs.LicenseManager.Core.ViewModel
{
    public class LicenseTokenViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Versione")]
        public string VersionName { get; set; }

        [Display(Name = "Id versione")]
        [Required]
        public string VersionId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Id cliente")]
        [Required]
        public string CustomerId { get; set; }

        [Display(Name = "Features")]
        public IEnumerable<LicenseTokenFeatureViewModel> LicenseTokenFeatures { get; set; }

        [Display(Name = "Chiave Privata")]
        public string PrivateKey { get; set; }
        
        [Display(Name = "Chiave Pubblica")]
        public string PublicKey { get; set; }

        [Display(Name = "Frase segreta")]
        public string PassPhrase { get; set; }

        [Display(Name = "Contenuto file licenza")]
        public string LicFileContent { get; set; }
        
        [Display(Name = "Attivo")]
        public bool Active { get; set; }
        public LicenseTypeViewModel LicenseType { get; set; }
        public DateTime EmitDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
