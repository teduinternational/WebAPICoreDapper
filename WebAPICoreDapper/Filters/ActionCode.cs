using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICoreDapper.Filters
{
    public enum ActionCode
    {
        CREATE,
        UPDATE,
        DELETE,
        VIEW,
        IMPORT,
        EXPORT,
        APPROVE
    }
}
