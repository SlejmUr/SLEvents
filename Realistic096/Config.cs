using Exiled.API.Interfaces;
using System;

namespace Realistic096
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; }
        public bool Debug { get; set; }
        public int PlayableSCP096Count { get; set; }  = 1;
        public bool OtherSCPSCanPlay { get; set; }
        public bool SpawnLoot { get; set; }
        public bool AllSCPSToDClass { get; set; }
    }
}
