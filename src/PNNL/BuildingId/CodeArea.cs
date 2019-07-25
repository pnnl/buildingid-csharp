// Copyright (c) 2019, Battelle Memorial Institute
// All rights reserved.
//
// See LICENSE.txt and WARRANTY.txt for details.

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
