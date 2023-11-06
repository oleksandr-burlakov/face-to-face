using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2F.DLL.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime LastModifiedDate { get; set; }
}
