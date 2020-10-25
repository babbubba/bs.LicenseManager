using System.ComponentModel.DataAnnotations;

namespace bs.LicensesManager.Core.ViewModel
{
    public class VersionViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Versione")]
        [MaxLength(16)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Attivo")]
        public bool Active { get; set; }

        [Display(Name = "Nome prodotto")]
        public bool ProductName { get; set; }
       
        [Display(Name = "Id prodotto")]
        [Required]
        public bool ProductId { get; set; }
    }
}
