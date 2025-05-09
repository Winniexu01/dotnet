// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IO;
using System.Windows.Documents.Serialization;

//
// 
//
// Description: Implements a Plug-in document serializer factory for the XpsDocumentWriter
//
// 
//

namespace System.Windows.Xps.Serialization
{
    /// <summary>
    /// SerializerFactory is the factory class for the SerializerWriter that wraps an XPSDocumentSerializer
    /// </summary>
    public sealed class XpsSerializerFactory : ISerializerFactory
    {
        #region Constructors
        /// <summary>
        /// creates a SerializerFactory
        /// </summary>
        public XpsSerializerFactory()
        {
        }

        #endregion

        #region ISerializerFactory Implementation
        /// <summary>
        /// Create a SerializerWriter on the passed in stream
        /// </summary>
        public SerializerWriter CreateSerializerWriter(Stream stream)
        {
            return new XpsSerializerWriter(stream);
        }
        /// <summary>
        /// Return the DisplayName of the serializer.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return SR.XpsSerializerFactory_DisplayName;
            }
        }
        /// <summary>
        /// Return the ManufacturerName of the serializer.
        /// </summary>
        public string ManufacturerName
        {
            get
            {
                return SR.XpsSerializerFactory_ManufacturerName;
            }
        }
        /// <summary>
        /// Return the ManufacturerWebsite of the serializer.
        /// </summary>
        public Uri ManufacturerWebsite
        {
            get
            {
                return new Uri(SR.XpsSerializerFactory_ManufacturerWebsite);
            }
        }
        /// <summary>
        /// Return the DefaultFileExtension of the serializer.
        /// </summary>
        public string DefaultFileExtension
        {
            get
            {
                return ".xps";
            }
        }
        #endregion
    }
}
