// Copyright (c) 2019, Battelle Memorial Institute
// All rights reserved.
//
// 1. Battelle Memorial Institute (hereinafter Battelle) hereby grants permission
//    to any person or entity lawfully obtaining a copy of this software and
//    associated documentation files (hereinafter "the Software") to redistribute
//    and use the Software in source and binary forms, with or without
//    modification.  Such person or entity may use, copy, modify, merge, publish,
//    distribute, sublicense, and/or sell copies of the Software, and may permit
//    others to do so, subject to the following conditions:
//
//    * Redistributions of source code must retain the above copyright notice, this
//      list of conditions and the following disclaimers.
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation
//      and/or other materials provided with the distribution.
//    * Other than as used herein, neither the name Battelle Memorial Institute or
//      Battelle may be used in any form whatsoever without the express written
//      consent of Battelle.
//
// 2. THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL BATTELLE OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
// OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

namespace PNNL.BuildingId
{

    /// <summary>
    /// Represents a UBID code.
    /// </summary>
    [System.Serializable()]
    public class Code : System.Runtime.Serialization.ISerializable
    {

        /// <summary>
        /// The alphabet for digits in an OLC string.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Defined here because <cref name="Google.OpenLocationCode.OpenLocationCode.CodeAlphabet"/> is not visible.
        /// </para>
        /// </remarks>
        private const string OpenLocationCodeCodeAlphabet = "23456789CFGHJMPQRVWX";

        /// <summary>
        /// The padding character in OLC strings.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Defined here because <cref name="Google.OpenLocationCode.OpenLocationCode.PaddingCharacter"/> is not visible.
        /// </para>
        /// </remarks>
        private const char OpenLocationCodePaddingCharacter = '0';

        /// <summary>
        /// The separator character in OLC strings.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Defined here because <cref name="Google.OpenLocationCode.OpenLocationCode.SeparatorCharacter"/> is not visible.
        /// </para>
        /// </remarks>
        private const char OpenLocationCodeSeparatorCharacter = '+';

        /// <summary>
        /// The separator for segments of a UBID code.
        /// </summary>
        private const char SeparatorCharacter = '-';

        /// <summary>
        /// The regular expression for a UBID code.
        /// </summary>
        private static System.Text.RegularExpressions.Regex Regex = new System.Text.RegularExpressions.Regex(System.String.Join("", new string[] {
            @"^",
            System.String.Join("", new string[] {
                @"([",
                System.Text.RegularExpressions.Regex.Escape(OpenLocationCodeCodeAlphabet),
                @"]{4,8}",
                System.Text.RegularExpressions.Regex.Escape(System.Convert.ToString(OpenLocationCodeSeparatorCharacter)),
                @"[",
                System.Text.RegularExpressions.Regex.Escape(OpenLocationCodeCodeAlphabet),
                @"]*)",
            }),
            System.Text.RegularExpressions.Regex.Escape(System.Convert.ToString(SeparatorCharacter)),
            @"(0|[1-9][0-9]*)",
            System.Text.RegularExpressions.Regex.Escape(System.Convert.ToString(SeparatorCharacter)),
            @"(0|[1-9][0-9]*)",
            System.Text.RegularExpressions.Regex.Escape(System.Convert.ToString(SeparatorCharacter)),
            @"(0|[1-9][0-9]*)",
            System.Text.RegularExpressions.Regex.Escape(System.Convert.ToString(SeparatorCharacter)),
            @"(0|[1-9][0-9]*)",
            @"$",
        }), System.Text.RegularExpressions.RegexOptions.Compiled);

        /// <summary>
        /// The first group of a UBID code is the OLC for the center of mass.
        /// </summary>
        private const int GroupIndexOpenLocationCode = 1;

        /// <summary>
        /// The second group of the UBID code is the Chebyshev distance in OLC grid units
        /// from the OLC for the center of mass to the northern extent of the OLC bounding box.
        /// </summary>
        private const int GroupIndexNorth = 2;

        /// <summary>
        /// The third group of the UBID code is the Chebyshev distance in OLC grid units
        /// from the OLC for the center of mass to the eastern extent of the OLC bounding box.
        /// </summary>
        private const int GroupIndexEast = 3;

        /// <summary>
        /// The fourth group of the UBID code is the Chebyshev distance in OLC grid units
        /// from the OLC for the center of mass to the southern extent of the OLC bounding box.
        /// </summary>
        private const int GroupIndexSouth = 4;

        /// <summary>
        /// The fifth group of the UBID code is the Chebyshev distance in OLC grid units
        /// from the OLC for the center of mass to the western extent of the OLC bounding box.
        /// </summary>
        private const int GroupIndexWest = 5;

        /// <summary>
        /// Returns the UBID code for the specified latitude and longitude coordinates.
        /// </summary>
        /// <param name="southLatitude">The latitude coordinate of the minima (units: decimal degrees).</param>
        /// <param name="westLongitude>">The longitude coordinate of the minima (units: decimal degrees).</param>
        /// <param name="northLatitude">The latitude coordinate of the maxima (units: decimal degrees).</param>
        /// <param name="eastLongitude">The longitude coordinate of the maxima (units: decimal degrees).</param>
        /// <param name="centerLatitude">The latitude coordinate of the center of mass (units: decimal degrees).</param>
        /// <param name="centerLongitude">The longitude coordinate of the center of mass (units: decimal degrees).</param>
        /// <param name="codeLength">The number of digits in the OLC.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a latitude coordinate is outside of the range: -90 to 90.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a longitude coordinate is outside of the range: -180 to 180.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when an OLC code is syntactically invalid.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when the specified number of digits in the OLC code is invalid.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when a syntactically valid OLC code cannot be decoded.
        /// </exception>
        /// <returns>
        /// The UBID code for the specified latitude and longitude coordinates.
        /// </returns>
        public static Code Encode(double southLatitude, double westLongitude, double northLatitude, double eastLongitude, double centerLatitude, double centerLongitude, int codeLength = Google.OpenLocationCode.OpenLocationCode.CodePrecisionNormal)
        {
            // Encode OLCs for minima, maxima and center of mass.
            Google.OpenLocationCode.OpenLocationCode minOpenLocationCode = new Google.OpenLocationCode.OpenLocationCode(southLatitude, westLongitude, codeLength);
            Google.OpenLocationCode.OpenLocationCode maxOpenLocationCode = new Google.OpenLocationCode.OpenLocationCode(northLatitude, eastLongitude, codeLength);
            Google.OpenLocationCode.OpenLocationCode centerOpenLocationCode = new Google.OpenLocationCode.OpenLocationCode(centerLatitude, centerLongitude, codeLength);

            // Decode OLCs for minima, maxima and center of mass.
            Google.OpenLocationCode.CodeArea minCodeArea = minOpenLocationCode.Decode();
            Google.OpenLocationCode.CodeArea maxCodeArea = maxOpenLocationCode.Decode();
            Google.OpenLocationCode.CodeArea centerCodeArea = centerOpenLocationCode.Decode();

            // Calculate the Chebyshev distances in OLC grid units from the OLC for the center of mass
            // to the northern, eastern, southern and western extents of the OLC bounding box.
            double north = (maxCodeArea.NorthLatitude - centerCodeArea.NorthLatitude) / centerCodeArea.LatitudeHeight;
            double east = (maxCodeArea.EastLongitude - centerCodeArea.EastLongitude) / centerCodeArea.LongitudeWidth;
            double south = (centerCodeArea.SouthLatitude - minCodeArea.SouthLatitude) / centerCodeArea.LatitudeHeight;
            double west = (centerCodeArea.WestLongitude - minCodeArea.WestLongitude) / centerCodeArea.LongitudeWidth;

            // Build the UBID code string.
            string value = System.String.Join(System.Convert.ToString(SeparatorCharacter), new string[] {
                centerOpenLocationCode.Code,
                System.Convert.ToString(System.Math.Round(north)),
                System.Convert.ToString(System.Math.Round(east)),
                System.Convert.ToString(System.Math.Round(south)),
                System.Convert.ToString(System.Math.Round(west)),
            });

            // Build and return the UBID code.
            return new Code(value);
        }

        /// <summary>
        /// Returns the UBID code for the specified instances of the <cref name="Google.OpenLocationCode.GeoPoint"/> class.
        /// </summary>
        /// <param name="min">The minima.</param>
        /// <param name="max>">The maxima.</param>
        /// <param name="center">The center of mass.</param>
        /// <param name="codeLength">The number of digits in the OLC.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a latitude coordinate is outside of the range: -90 to 90.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a longitude coordinate is outside of the range: -180 to 180.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when an OLC code is syntactically invalid.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when the specified number of digits in the OLC code is invalid.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when a syntactically valid OLC code cannot be decoded.
        /// </exception>
        /// <returns>
        /// The UBID code for the specified instances of the <cref name="Google.OpenLocationCode.GeoPoint"/> class.
        /// </returns>
        public static Code Encode(Google.OpenLocationCode.GeoPoint min, Google.OpenLocationCode.GeoPoint max, Google.OpenLocationCode.GeoPoint center, int codeLength = Google.OpenLocationCode.OpenLocationCode.CodePrecisionNormal) => Encode(min.Latitude, min.Longitude, max.Latitude, max.Longitude, center.Latitude, center.Longitude, codeLength);

        /// <summary>
        /// The UBID code string.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PNNL.BuildingId.Code"/> class
        /// with a specified UBID code string.
        /// <summary>
        /// <param name="value">The UBID code string.</param>
        public Code(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PNNL.BuildingId.Code"/> class
        /// with serialized data.
        /// <summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected Code(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            this.Value = (string) info.GetValue("Value", typeof(string));
        }

        /// <summary>
        /// Returns the UBID code area for this UBID code.
        /// </summary>
        /// <exception cref="InvalidCodeException">
        /// Thrown when this UBID code is invalid.
        /// </exception>
        /// <returns>
        /// The UBID code area for this UBID code.
        /// </returns>
        public CodeArea Decode()
        {
            if (this.Value is null)
            {
                throw new InvalidCodeException(this, "Invalid UBID: null object reference");
            }

            System.Text.RegularExpressions.MatchCollection matches = Regex.Matches(this.Value);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                System.Text.RegularExpressions.GroupCollection groups = match.Groups;

                try
                {
                    // Build the OLC for the center of mass.
                    Google.OpenLocationCode.OpenLocationCode centerOpenLocationCode = new Google.OpenLocationCode.OpenLocationCode(groups[GroupIndexOpenLocationCode].Value);

                    // Calculate the number of digits in the OLC for the center of mass.
                    int centerCodeLength = centerOpenLocationCode.Code.Replace(System.Convert.ToString(OpenLocationCodePaddingCharacter), "").Length - System.Convert.ToString(OpenLocationCodeSeparatorCharacter).Length;

                    // Decode the OLC for the center of mass.
                    Google.OpenLocationCode.CodeArea centerCodeArea = centerOpenLocationCode.Decode();

                    // Build the point for the minima by subtracting.
                    Google.OpenLocationCode.GeoPoint min = new Google.OpenLocationCode.GeoPoint(
                        centerCodeArea.SouthLatitude - (System.Convert.ToDouble(groups[GroupIndexSouth].Value) * centerCodeArea.LatitudeHeight),
                        centerCodeArea.WestLongitude - (System.Convert.ToDouble(groups[GroupIndexWest].Value) * centerCodeArea.LongitudeWidth)
                    );

                    // Build the point for the maxima by adding.
                    Google.OpenLocationCode.GeoPoint max = new Google.OpenLocationCode.GeoPoint(
                        centerCodeArea.NorthLatitude + (System.Convert.ToDouble(groups[GroupIndexNorth].Value) * centerCodeArea.LatitudeHeight),
                        centerCodeArea.EastLongitude + (System.Convert.ToDouble(groups[GroupIndexEast].Value) * centerCodeArea.LongitudeWidth)
                    );

                    // Build the UBID code area.
                    return new CodeArea(min, max, centerCodeArea, centerCodeLength);
                }
                catch (System.ArgumentException inner)
                {
                    throw new InvalidCodeException(this, "Invalid UBID: latitude and/or longitude coordinates are outside of range", inner);
                }
                catch (System.InvalidOperationException inner)
                {
                    throw new InvalidCodeException(this, "Invalid UBID: decode failed", inner);
                }
            }

            throw new InvalidCodeException(this, "Invalid UBID: regular expression failed to match");
        }

        /// <summary>
        /// Populates a <cref name="System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("Value", this.Value, typeof(string));
        }

        /// <summary>
        /// Is this UBID code valid?
        /// </summary>
        /// <returns>
        /// True if this UBID code is valid. Otherwise, false.
        /// <returns>
        public bool IsValid()
        {
            if (this.Value is null)
            {
                return false;
            }

            System.Text.RegularExpressions.MatchCollection matches = Regex.Matches(this.Value);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                System.Text.RegularExpressions.GroupCollection groups = match.Groups;

                try
                {
                    Google.OpenLocationCode.OpenLocationCode centerOpenLocationCode = new Google.OpenLocationCode.OpenLocationCode(groups[GroupIndexOpenLocationCode].Value);

                    return true;
                }
                catch (System.ArgumentException)
                {
                    return false;
                }
            }

            return false;
        }

    }

}
