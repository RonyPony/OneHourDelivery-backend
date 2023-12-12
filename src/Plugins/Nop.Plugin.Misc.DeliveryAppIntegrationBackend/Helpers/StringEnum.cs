using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers
{
    /// <summary>
    /// Utility to 'use' a class as an enum of strings.
    /// </summary>
    public class StringEnum
    {
        protected StringEnum(string value) { Value = value; }
        public string Value { get; }
        public override string ToString() => Value;
    }
}
