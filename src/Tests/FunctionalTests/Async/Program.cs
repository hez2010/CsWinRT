﻿using System;
using System.Threading.Tasks;
using TestComponentCSharp;

var instance = new Class();

instance.IntProperty = 12;
var async_get_int = instance.GetIntAsync();
int async_int = 0;
async_get_int.Completed = (info, status) => async_int = info.GetResults();
async_get_int.GetResults();

if (async_int != 12)
{
    return 101;
}

instance.StringProperty = "foo";
var async_get_string = instance.GetStringAsync();
string async_string = "";
async_get_string.Completed = (info, status) => async_string = info.GetResults();
int async_progress;
async_get_string.Progress = (info, progress) => async_progress = progress;
async_get_string.GetResults();

if (async_string != "foo")
{
    return 102;
}

var task = InvokeAddAsync(instance, 20, 10);
if (task.Wait(25))
{
    return 103;
}

instance.CompleteAsync();
if (!task.Wait(1000))
{
    return 104;
}

if (task.Status != TaskStatus.RanToCompletion || task.Result != 30)
{
    return 105;
}

var ports = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(
    Windows.Devices.SerialCommunication.SerialDevice.GetDeviceSelector(),
    new string[] { "System.ItemNameDisplay" });
foreach (var port in ports)
{
    object o = port.Properties["System.ItemNameDisplay"];
    if (o is null)
    {
        return 106;
    }
}

var folderPath = System.IO.Path.GetDirectoryName(AppContext.BaseDirectory);
Windows.Storage.StorageFile file = await Windows.Storage.StorageFile.GetFileFromPathAsync(folderPath + "\\Async.exe");
var handle = System.IO.WindowsRuntimeStorageExtensions.CreateSafeFileHandle(file, System.IO.FileAccess.Read);
if (handle is null)
{
    return 107;
}
handle.Close();

handle = null;
var getFolderFromPathAsync = Windows.Storage.StorageFolder.GetFolderFromPathAsync(folderPath);
getFolderFromPathAsync.Completed += (s, e) => 
{
    handle = System.IO.WindowsRuntimeStorageExtensions.CreateSafeFileHandle(s.GetResults(), "Async.pdb", System.IO.FileMode.Open, System.IO.FileAccess.Read);
};
await Task.Delay(1000);
if (handle is null)
{
    return 108;
}
handle.Close();

return 100;

static async Task<int> InvokeAddAsync(Class instance, int lhs, int rhs)
{
    return await instance.AddAsync(lhs, rhs);
}