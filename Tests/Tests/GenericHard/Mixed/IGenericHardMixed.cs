using RegistrationByAttributes.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.GenericHard.Mixed
{
    [TypeRegistrationAttribute(LifetimeManagementType.PerResolve)]
    internal interface IGenericHardMixed<T>
    {
    }
}
