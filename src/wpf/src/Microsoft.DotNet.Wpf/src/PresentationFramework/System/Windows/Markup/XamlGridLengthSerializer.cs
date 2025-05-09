﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
// Description:
//   XamlSerializer used to persist GridLength structures in Baml
//

using System.Globalization;
using System.IO;
using MS.Internal;

#if PBTCOMPILER
namespace MS.Internal.Markup
#else
namespace System.Windows.Markup
#endif
{
    /// <summary>
    ///     XamlGridLengthSerializer is used to persist a GridLength structure in Baml files
    /// </summary>
    internal class XamlGridLengthSerializer : XamlSerializer
    {
#region Construction

        /// <summary>
        ///     Constructor for XamlGridLengthSerializer
        /// </summary>
        /// <remarks>
        ///     This constructor will be used under 
        ///     the following two scenarios
        ///     1. Convert a string to a custom binary representation stored in BAML
        ///     2. Convert a custom binary representation back into a GridLength
        /// </remarks>
        private XamlGridLengthSerializer()
        {
        }


#endregion Construction

#region Conversions

        ///<summary>
        /// Serializes this object using the passed writer.
        ///</summary>
        /// <remarks>
        /// This is called ONLY from the Parser and is not a general public method. 
        /// </remarks>
        //
        //  Format of serialized data:
        //  first byte   other bytes      format
        //  0AAAAAAA     none             Amount [0 - 127] in AAAAAAA, Pixel GridUnitType
        //  100XXUUU     one byte         Amount in byte [0 - 255], GridUnitType in UUU
        //  110XXUUU     two bytes        Amount in int16 , GridUnitType in UUU
        //  101XXUUU     four bytes       Amount in int32 , GridUnitType in UUU
        //  111XXUUU     eight bytes      Amount in double, GridUnitType in UUU
        //
        public override bool ConvertStringToCustomBinary (
            BinaryWriter   writer,           // Writer into the baml stream
            string         stringValue)      // String to convert
        {
            ArgumentNullException.ThrowIfNull(writer);

            GridUnitType gridUnitType;
            double   value;
            FromString(stringValue, TypeConverterHelper.InvariantEnglishUS,
                        out value, out gridUnitType);

            byte unitAndFlags = (byte)gridUnitType;
            int intAmount = (int)value;

            if ((double)intAmount == value)
            {
                //
                //  0 - 127 and Pixel
                //
                if (    intAmount <= 127 
                    &&  intAmount >= 0
                    &&  gridUnitType == GridUnitType.Pixel  )
                {
                    writer.Write((byte)intAmount);
                }
                //
                //  unsigned byte
                //
                else if (   intAmount <= 255 
                        &&  intAmount >= 0  )
                {
                    writer.Write((byte)(0x80 | unitAndFlags));
                    writer.Write((byte)intAmount);
                }
                //
                //  signed short integer
                //
                else if (   intAmount <= 32767 
                        &&  intAmount >= -32768 )
                {
                    writer.Write((byte)(0xC0 | unitAndFlags));
                    writer.Write((Int16)intAmount);
                }
                //
                //  signed integer
                //
                else
                {
                    writer.Write((byte)(0xA0 | unitAndFlags));
                    writer.Write(intAmount);
                }
            }
            //
            //  double
            //
            else 
            {
                writer.Write((byte)(0xE0 | unitAndFlags));
                writer.Write(value);
            }

            return true;
        }
        
        /// <summary>
        ///   Convert a compact binary representation of a GridLength into and instance
        ///   of GridLength.  The reader must be left pointing immediately after the object 
        ///   data in the underlying stream.
        /// </summary>
        /// <remarks>
        /// This is called ONLY from the Parser and is not a general public method. 
        /// </remarks>
        public override object ConvertCustomBinaryToObject(
            BinaryReader reader)
        {
            ArgumentNullException.ThrowIfNull(reader);

            GridUnitType unitType;
            double unitValue;
            byte unitAndFlags = reader.ReadByte();

            if ((unitAndFlags & 0x80) == 0)
            {
                unitType = GridUnitType.Pixel;
                unitValue = (double)unitAndFlags;
            }
            else
            {
                unitType = (GridUnitType)(unitAndFlags & 0x1F);
                byte flags = (byte)(unitAndFlags & 0xE0);

                if (flags == 0x80)
                {
                    unitValue = (double)reader.ReadByte();
                }
                else if (flags == 0xC0)
                {
                    unitValue = (double)reader.ReadInt16();
                }
                else if (flags == 0xA0)
                {
                    unitValue = (double)reader.ReadInt32();
                }
                else 
                {
                    unitValue = (double)reader.ReadDouble();
                }
            }
            return new GridLength(unitValue, unitType);
        }



        // Parse a GridLength from a string given the CultureInfo.
        internal static void FromString(
                string       s, 
                CultureInfo  cultureInfo,
            out double       value,
            out GridUnitType unit)
        {
            ReadOnlySpan<char> valueSpan = s.AsSpan().Trim();

            value = 0.0;
            unit = GridUnitType.Pixel;

            int i;
            int strLenUnit = 0;
            double unitFactor = 1.0;

            //  this is where we would handle trailing whitespace on the input string.
            //  peel [unit] off the end of the string
            i = 0;

            if (valueSpan.Equals(UnitStrings[i], StringComparison.OrdinalIgnoreCase))
            {
                strLenUnit = UnitStrings[i].Length;
                unit = (GridUnitType)i;
            }
            else
            {
                for (i = 1; i < UnitStrings.Length; ++i)
                {
                    //  Note: this is NOT a culture specific comparison.
                    //  this is by design: we want the same unit string table to work across all cultures.
                    if (valueSpan.EndsWith(UnitStrings[i], StringComparison.OrdinalIgnoreCase))
                    {
                        strLenUnit = UnitStrings[i].Length;
                        unit = (GridUnitType)i;
                        break;
                    }
                }
            }

            //  we couldn't match a real unit from GridUnitTypes.
            //  try again with a converter-only unit (a pixel equivalent).
            if (i >= UnitStrings.Length)
            {
                PixelUnit pixelUnit;
                if (PixelUnit.TryParsePixelPerInch(valueSpan, out pixelUnit)
                    || PixelUnit.TryParsePixelPerCentimeter(valueSpan, out pixelUnit)
                    || PixelUnit.TryParsePixelPerPoint(valueSpan, out pixelUnit))
                {
                    strLenUnit = pixelUnit.Name.Length;
                    unitFactor = pixelUnit.Factor;
                }
            }

            //  this is where we would handle leading whitespace on the input string.
            //  this is also where we would handle whitespace between [value] and [unit].
            //  check if we don't have a [value].  This is acceptable for certain UnitTypes.
            if (valueSpan.Length == strLenUnit
                && unit is GridUnitType.Auto or GridUnitType.Star)
            {
                value = 1;
            }
            //  we have a value to parse.
            else
            {
                Debug.Assert(   unit == GridUnitType.Pixel 
                            ||  DoubleUtil.AreClose(unitFactor, 1.0)    );

                ReadOnlySpan<char> valueString = valueSpan.Slice(0, valueSpan.Length - strLenUnit);
                value = double.Parse(valueString, provider: cultureInfo) * unitFactor;
            }
        }


#endregion Conversions

#region Fields

        //  Note: keep this array in sync with the GridUnitType enum
        private static string[] UnitStrings = { "auto", "px", "*" };

#endregion Fields
    }
}
