using Exiled.API.Interfaces;
using System;

namespace BlackOut
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; }
        public bool Debug { get; set; }
        public bool EventEnabled { get; set; }
        public bool Lotus_Round { get; set; }
        public bool JDuff_Round { get; set; }
        public bool Fallout_Round { get; set; }
        public bool Skelle_Round { get; set; }
    }
}
