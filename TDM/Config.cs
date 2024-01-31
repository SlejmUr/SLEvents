using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace TDM
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; }
        public bool Debug { get; set; }
        public bool EventEnabled { get; set; }
        public List<ItemType> Team1Items { get; set; }
        public List<ItemType> Team2Items { get; set; }
    }
}
