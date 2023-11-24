using System;

abstract class Storage
{
    protected string name;
    protected string model;

    public Storage(string name, string model)
    {
        this.name = name;
        this.model = model;
    }

    public abstract double GetMemory();

    public abstract void CopyData(double dataSize);

    public abstract double GetFreeSpace();

    public abstract void PrintDeviceInfo();
}

class Flash : Storage
{
    private double usbSpeed;
    private double memorySize;

    public Flash(string name, string model, double usbSpeed, double memorySize)
        : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.memorySize = memorySize;
    }

    public override double GetMemory()
    {
        return memorySize;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Copying {dataSize} GB to Flash Drive...");
    }

    public override double GetFreeSpace()
    {
        return memorySize - 0.1; // Assuming 100 MB reserved space for system files
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"Flash Drive: {name}, Model: {model}, USB Speed: {usbSpeed} GB/s, Memory Size: {memorySize} GB");
    }
}

class DVD : Storage
{
    private double readWriteSpeed;
    private string type;

    public DVD(string name, string model, double readWriteSpeed, string type)
        : base(name, model)
    {
        this.readWriteSpeed = readWriteSpeed;
        this.type = type;
    }

    public override double GetMemory()
    {
        if (type == "single-sided")
            return 4.7;
        else if (type == "double-sided")
            return 9;
        else
            return 0;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Copying {dataSize} GB to DVD...");
    }

    public override double GetFreeSpace()
    {
        return GetMemory() - 0.5; // Assuming 500 MB reserved space for system files
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"DVD: {name}, Model: {model}, Read/Write Speed: {readWriteSpeed} GB/s, Type: {type}, Memory Size: {GetMemory()} GB");
    }
}

class HDD : Storage
{
    private double usbSpeed;
    private int partitions;
    private double partitionSize;

    public HDD(string name, string model, double usbSpeed, int partitions, double partitionSize)
        : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.partitions = partitions;
        this.partitionSize = partitionSize;
    }

    public override double GetMemory()
    {
        return partitions * partitionSize;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Copying {dataSize} GB to HDD...");
    }

    public override double GetFreeSpace()
    {
        return GetMemory() - 1; // Assuming 1 GB reserved space for system files
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"HDD: {name}, Model: {model}, USB Speed: {usbSpeed} GB/s, Partitions: {partitions}, Partition Size: {partitionSize} GB, Memory Size: {GetMemory()} GB");
    }
}

class Program
{
    static void Main()
    {
        Storage[] devices = new Storage[3];
        devices[0] = new Flash("FlashDrive1", "Kingston", 3.0, 64);
        devices[1] = new DVD("DVD1", "Sony", 2.4, "single-sided");
        devices[2] = new HDD("HDD1", "Seagate", 2.0, 2, 500);

        double totalMemory = 0;
        foreach (var device in devices)
        {
            totalMemory += device.GetMemory();
            device.PrintDeviceInfo();
        }

        Console.WriteLine($"\nTotal Memory of all devices: {totalMemory} GB\n");

        double dataSize = 565;
        double totalFreeSpace = 0;

        foreach (var device in devices)
        {
            device.CopyData(dataSize);
            totalFreeSpace += device.GetFreeSpace();
        }

        double transferTime = totalFreeSpace / 10; // Assuming transfer speed is 10 GB/s
        Console.WriteLine($"\nTotal Free Space on all devices: {totalFreeSpace} GB");
        Console.WriteLine($"Time required for transfer: {transferTime} seconds");

        double numberOfDevicesNeeded = Math.Ceiling(dataSize / totalFreeSpace);
        Console.WriteLine($"Number of devices needed for transfer: {numberOfDevicesNeeded}");
    }
}
