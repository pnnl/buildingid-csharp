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
    /// Represents a UBID code area.
    /// </summary>
    public class CodeArea : Google.OpenLocationCode.CodeArea
    {

        /// <summary>
        /// The OLC code area for the center of mass.
        /// </summary>
        public Google.OpenLocationCode.CodeArea CenterCodeArea { get; private set; }

        /// <summary>
        /// The number of digits in the OLC for the center of mass.
        /// </summary>
        public int CenterCodeLength { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PNNL.BuildingId.CodeArea"/> class.
        /// </summary>
        /// <param name="min">The point for the minima.</param>
        /// <param name="max">The point for the maxima.</param>
        /// <param name="centerCodeArea">The OLC code area for the center of mass.</param>
        /// <param name="centerCodeLength">The number of digits in the OLC for the center of mass.</param>
        public CodeArea(Google.OpenLocationCode.GeoPoint min, Google.OpenLocationCode.GeoPoint max, Google.OpenLocationCode.CodeArea centerCodeArea, int centerCodeLength) : base(min, max)
        {
            this.CenterCodeArea = centerCodeArea;
            this.CenterCodeLength = centerCodeLength;
        }

        /// <summary>
        /// Returns the UBID code for this UBID code area.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a latitude coordinate is outside of the range: -90 to 90.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a longitude coordinate is outside of the range: -180 to 180.
        /// </exception>
        /// <returns>
        /// The UBID code for this code area.
        /// </returns>
        public Code Encode() => Code.Encode(this.Min, this.Max, this.CenterCodeArea.Center, this.CenterCodeLength);

        /// <summary>
        /// Returns a resized version of this UBID code area, where the latitude and
        /// longitude of the lower left and upper right corners of the OLC bounding
        /// box are moved inwards by dimensions that correspond to half of the height
        /// and width of the OLC grid reference cell for the centroid.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The purpose of the resizing operation is to ensure that re-encoding a
        /// given UBID code area results in the same coordinates.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// Code code = new Code("849VQJH6+95J-51-58-42-50");
        /// 
        /// if (code.Value == code.Decode().Resize().Encode().Value)
        /// {
        ///     System.Console.WriteLine("Original and resized UBID codes match!");
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Original and resized UBID codes do not match...");
        /// }
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a latitude coordinate is outside of the range: -90 to 90.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when a longitude coordinate is outside of the range: -180 to 180.
        /// </exception>
        /// <returns>
        /// The resized version of this UBID code area.
        /// </returns>
        public CodeArea Resize()
        {
            // Calculate the (half-)dimensions of OLC code area for the center of mass.
            double halfHeight = this.CenterCodeArea.LatitudeHeight / 2D;
            double halfWidth = this.CenterCodeArea.LongitudeWidth / 2D;

            // Build the new minima by adding.
            Google.OpenLocationCode.GeoPoint min = new Google.OpenLocationCode.GeoPoint(
                this.SouthLatitude + halfHeight,
                this.WestLongitude + halfWidth
            );

            // Build the new maxima by subtracting.
            Google.OpenLocationCode.GeoPoint max = new Google.OpenLocationCode.GeoPoint(
                this.NorthLatitude - halfHeight,
                this.EastLongitude - halfWidth
            );

            // Build and return the resized version of this UBID code area.
            return new CodeArea(min, max, this.CenterCodeArea, this.CenterCodeLength);
        }

    }

}
