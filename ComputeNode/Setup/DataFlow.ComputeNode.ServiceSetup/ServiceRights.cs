﻿using System;

namespace AISTek.DataFlow.ComputeNode.ServiceSetup
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ServiceRights
    {
        /// <summary>
        /// 
        /// </summary>
        QueryConfig = 0x1,
        /// <summary>
        /// 
        /// </summary>
        ChangeConfig = 0x2,
        /// <summary>
        /// 
        /// </summary>
        QueryStatus = 0x4,
        /// <summary>
        /// 
        /// </summary>
        EnumerateDependants = 0x8,
        /// <summary>
        /// 
        /// </summary>
        Start = 0x10,
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x20,
        /// <summary>
        /// 
        /// </summary>
        PauseContinue = 0x40,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x80,
        /// <summary>
        /// 
        /// </summary>
        UserDefinedControl = 0x100,
        /// <summary>
        /// 
        /// </summary>
        Delete = 0x00010000,
        /// <summary>
        /// 
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// 
        /// </summary>
        AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig |
        QueryStatus | EnumerateDependants | Start | Stop | PauseContinue |
        Interrogate | UserDefinedControl)
    }

}
