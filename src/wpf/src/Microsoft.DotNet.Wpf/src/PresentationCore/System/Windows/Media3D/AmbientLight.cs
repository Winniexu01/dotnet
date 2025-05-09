// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
//
//
// Description: 3D ambient light implementation.
//
//              See spec at http://avalon/medialayer/Specifications/Avalon3D%20API%20Spec.mht
//
//

namespace System.Windows.Media.Media3D
{
    /// <summary>
    ///     Ambient lights light objects uniformly, regardless of their shape.
    /// </summary>
    public sealed partial class AmbientLight : Light
    {
        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        #region Constructors

        /// <summary>
        ///     Constructor that creates a new white ambient light.
        /// </summary>
        public AmbientLight() 
            : this( Colors.White )
        {
        }

        /// <summary>
        ///     Constructor that creates a new ambient light.
        /// </summary>
        /// <param name="ambientColor">Ambient color of the new light.</param>
        public AmbientLight(Color ambientColor)
        {
            Color = ambientColor;
        }

        #endregion Constructors

        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Public Events
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------
    }
}

