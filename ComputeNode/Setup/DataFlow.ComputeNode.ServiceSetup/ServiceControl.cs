﻿namespace AISTek.DataFlow.ComputeNode.ServiceSetup
{
    /// <summary>
    /// 
    /// </summary>
    public enum ServiceControl
    {
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        Pause = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        Continue = 0x00000003,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        Shutdown = 0x00000005,
        /// <summary>
        /// 
        /// </summary>
        ParamChange = 0x00000006,
        /// <summary>
        /// 
        /// </summary>
        NetBindAdd = 0x00000007,
        /// <summary>
        /// 
        /// </summary>
        NetBindRemove = 0x00000008,
        /// <summary>
        /// 
        /// </summary>
        NetBindEnable = 0x00000009,
        /// <summary>
        /// 
        /// </summary>
        NetBindDisable = 0x0000000A
    }

}
