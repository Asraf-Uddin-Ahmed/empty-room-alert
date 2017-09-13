using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Foundation.Core.Aggregates
{
    public abstract class Entity
    {
        [Required]
        public Guid ID { get; set; }

    }
}
