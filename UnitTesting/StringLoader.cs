using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UnitTesting
{

  public class StringLoader : IDisposable
  {
    [DllImport("kernel32", CharSet = CharSet.Auto)]
    private static extern IntPtr LoadLibrary(string filename);

    [DllImport("kernel32", CharSet = CharSet.Auto)]
    private static extern bool FreeLibrary(IntPtr lib);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int LoadString(IntPtr hInstance, int uId, out IntPtr buffer, int bufferMax);

    private readonly IntPtr _libPtr;

    public StringLoader(string filename)
    {
      _libPtr = LoadLibrary(filename);
      if (_libPtr == IntPtr.Zero)
        throw new Win32Exception();
    }

    public string Load(int id)
    {
      IntPtr resource;
      var length = LoadString(_libPtr, id, out resource, 0);
      return length == 0 ? null : Marshal.PtrToStringAuto(resource, length);
    }

    public void Dispose()
    {
      FreeLibrary(_libPtr);
      GC.SuppressFinalize(this);
    }

    ~StringLoader()
    {
      Dispose();
    }
  }

}