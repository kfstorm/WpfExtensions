using System;

namespace Kfstorm.WpfExtensions.Interop
{
    // hook method called by system
    internal delegate IntPtr HOOKPROC(int code, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
}
