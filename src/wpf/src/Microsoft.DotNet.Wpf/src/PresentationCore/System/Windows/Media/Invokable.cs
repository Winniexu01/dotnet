// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//

namespace System.Windows.Media
{
    #region IInvokable

    /// <summary>
    /// Any class can implement this interface to get events from an external source
    /// Used in Unmanaged -> Managed event relaying
    /// </summary>
    internal interface IInvokable
    {
        void RaiseEvent(byte[] buffer, int cb);
    }

    #endregion
}
