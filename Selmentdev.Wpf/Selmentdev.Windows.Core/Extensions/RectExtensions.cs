#region License Information
//
//   Copyright 2016 Selmentdev
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
#endregion

using System.Windows;

namespace Selmentdev.Windows.Core.Extensions
{
    public static class RectExtensions
    {
        public static Rect InflateRect(this Rect @this, double left, double top, double right, double bottom)
        {
            //
            // Compute total dimensions.
            //
            var totalWidth = @this.Width + left + right;
            var totalHeight = @this.Height + top + bottom;

            //
            // And initial rectangle margins.
            //
            var marginLeft = @this.X - left;
            var marginTop = @this.Y - top;

            if (totalWidth < 0.0)
            {
                //
                // Shrink rectangle and reset width.
                //
                marginLeft += (totalWidth / 2.0);
                totalWidth = 0.0;
            }

            if (totalHeight < 0.0)
            {
                //
                // Shrink rectangle and reset width.
                //
                marginTop += (totalHeight / 2.0);
                totalHeight = 0.0;
            }

            //
            // Create new rectangle out of it.
            //
            return new Rect(marginLeft, marginTop, totalWidth, totalHeight);
        }

        public static Rect InflateRect(this Rect @this, Thickness padding)
        {
            return @this.InflateRect(
                padding.Left,
                padding.Top,
                padding.Right,
                padding.Top
                );
        }
        public static Rect DeflateRect(this Rect @this, Thickness padding)
        {
            return @this.InflateRect(
                -padding.Left,
                -padding.Top,
                -padding.Right,
                -padding.Top
                );
        }

        public static Point PivotPoint(this Rect @this, Point origin)
        {
            return new Point(
                @this.X + origin.X * @this.Width,
                @this.Y + origin.Y * @this.Height
                );
        }
    }
}
