using Exiled.API.Interfaces;

namespace Tiny
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; }
        public bool Debug { get; set; }
    }
}
