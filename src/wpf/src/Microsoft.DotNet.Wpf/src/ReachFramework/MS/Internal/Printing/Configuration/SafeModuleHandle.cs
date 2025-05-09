// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/*++


 * Abstract:

    SafeHandle for HMODULE
 
--*/

namespace MS.Internal.Printing.Configuration
{
    /// <summary>
    ///     Represents a module handle (HMODULE) used in API's like LoadLibrary
    /// </summary>
    internal class SafeModuleHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeModuleHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return UnsafeNativeMethods.FreeLibrary(this.handle);
        }
    }
}
