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

using System.Windows.Media;

namespace Selmentdev.Windows.Core.Extensions
{
    public static class TransformExtensions
    {
        public static void EnsureStandardTransform(this TransformGroup @this)
        {
            @this.Children.Clear();
            @this.Children.Add(new ScaleTransform());
            @this.Children.Add(new SkewTransform());
            @this.Children.Add(new RotateTransform());
            @this.Children.Add(new TranslateTransform());
        }

        public static ScaleTransform GetScaleTransform(this TransformGroup @this)
        {
            return (ScaleTransform)@this.Children[0];
        }

        public static SkewTransform GetSkewTransform(this TransformGroup @this)
        {
            return (SkewTransform)@this.Children[1];
        }

        public static RotateTransform GetRotateTransform(this TransformGroup @this)
        {
            return (RotateTransform)@this.Children[2];
        }

        public static TranslateTransform GetTranslateTransform(this TransformGroup @this)
        {
            return (TranslateTransform)@this.Children[3];
        }
    }
}
