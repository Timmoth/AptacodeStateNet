﻿using System;

namespace Aptacode.StateNet.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ConnectionAttribute : Attribute
    {
        public ConnectionAttribute(string inputName, string targetName) : this(inputName, targetName, "1")
        {

        }
        public ConnectionAttribute(string inputName, string targetName, string connectionDescription)
        {
            TargetName = targetName;
            InputName = inputName;
            ConnectionDescription = connectionDescription;
        }

        public string InputName { get; set; }
        public string TargetName { get; set; }
        public string ConnectionDescription { get; set; }
    }
}