using System;
using System.Collections.Generic;

namespace Nop.Plugin.DdpApi.Maps
{
    public interface IJsonPropertyMapper
    {
        Dictionary<string, Tuple<string, Type>> GetMap(Type type);
    }
}