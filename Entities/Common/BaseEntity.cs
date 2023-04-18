using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{
    public interface IEntity
    {
    }
    public class BaseEntity<TKey>:IEntity
    {
        [Required]
        [Key]
        public TKey Id { get; set; }
    }
    public class BaseEntity:BaseEntity<long>
    {

    }
}
