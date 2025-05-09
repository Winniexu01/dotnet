// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
//

using System.Collections;
using System.Text;
using System.Windows.Media.Animation;

namespace System.Windows.Media
{
    /// <summary>
    /// The class definition for PathSegmentCollection
    /// </summary>
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public sealed partial class PathSegmentCollection : Animatable, IList, IList<PathSegment>
    {
        /// <summary>
        /// Can serialze "this" to a string.
        /// This is true iff every segment is stroked.
        /// </summary>
        internal bool CanSerializeToString()
        {
            bool canSerialize = true;

            for (int i=0; i<_collection.Count; i++)
            { 
                if (!_collection[i].IsStroked)
                {
                    canSerialize = false;
                    break;
                }
            }

            return canSerialize;
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string 
        /// and IFormatProvider passed in.  
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (_collection.Count == 0)
            {
                return String.Empty;
            }

            StringBuilder str = new StringBuilder();
            
            for (int i=0; i<_collection.Count; i++)
            { 
                str.Append(_collection[i].ConvertToString(format, provider));
            }

            return str.ToString();
        }
    }
}

