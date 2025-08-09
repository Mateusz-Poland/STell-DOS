using CosmosKernel2;
using System.Globalization;
using Cosmos.System;

namespace CosmosKernel2
{
    public static class Power
    {

        public static void Reboot()
        {
            byte good = 0x02;
            while ((good & 0x02) != 0)
                good = IOPorts.Inb(0x64);
            IOPorts.Outb(0x64, 0xFE); //Pulse reset pin
            Cosmos.Core.CPU.Halt();
        }

        public static void ShutDown()
        {
            ACPI.Shutdown();
            ACPI.Disable();
            Cosmos.Core.CPU.Halt();
        }
    }
}