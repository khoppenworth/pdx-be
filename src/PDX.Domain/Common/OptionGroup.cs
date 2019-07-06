using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Catalog;
using PDX.Domain.Helpers;

namespace PDX.Domain.Common
{
    [Table("option_group", Schema = "common")]
    public class OptionGroup : LookUpEntity
    {        
        [NavigationProperty]
        public virtual IEnumerable<Option> PossibleOptions { get; set; }

    }
}