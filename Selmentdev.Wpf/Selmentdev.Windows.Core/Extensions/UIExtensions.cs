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
using System.Windows.Controls;

namespace Selmentdev.Windows.Core.Extensions
{
    public static class UIExtensions
    {
        public static void SetLocation(this UIElement @this, double x, double y)
        {
            if (!x.IsNanOrInfinity())
            {
                Canvas.SetLeft(@this, x);
            }

            if (!y.IsNanOrInfinity())
            {
                Canvas.SetTop(@this, y);
            }
        }
        public static void SetLocation(this UIElement @this, Point location)
        {
            @this.SetLocation(location.X, location.Y);
        }
        public static void SetLayout(this FrameworkElement @this, Rect rect)
        {
            @this.SetLocation(rect.Left, rect.Top);
            @this.Width = rect.Width;
            @this.Height = rect.Height;
        }
    }
}
