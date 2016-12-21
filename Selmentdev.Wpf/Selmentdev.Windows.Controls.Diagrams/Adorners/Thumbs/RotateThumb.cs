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

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Selmentdev.Windows.Controls.Diagrams.Primitives
{
    public class RotateThumb : Thumb
    {
        private double m_InitialAngle;
        private Vector m_StartVector;
        private Point m_CenterPoint;
        private DiagramItem m_DiagramItem;
        private DiagramControl m_DiagramControl;
        private RotateAdorner m_Adorner;

        public RotateThumb()
        {
            this.DragStarted += this.RotateThumb_DragStarted;
            this.DragDelta += this.RotateThumb_DragDelta;
            this.DragCompleted += this.RotateThumb_DragCompleted;
        }

        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.m_DiagramItem = this.DataContext as DiagramItem;

            if (this.m_DiagramItem != null)
            {
                this.m_DiagramControl = VisualTreeHelper.GetParent(this.m_DiagramItem) as DiagramControl;
                if (this.m_DiagramControl != null)
                {
                    this.m_CenterPoint = this.m_DiagramItem.TranslatePoint(
                        new Point(
                            this.m_DiagramItem.Width * this.m_DiagramItem.RenderTransformOrigin.X,
                            this.m_DiagramItem.Height * this.m_DiagramItem.RenderTransformOrigin.Y), this.m_DiagramControl);

                    var startPoint = Mouse.GetPosition(this.m_DiagramControl);
                    this.m_StartVector = Point.Subtract(startPoint, this.m_CenterPoint);

                    this.m_InitialAngle = this.m_DiagramItem.RotationAngle;

                    var adornerLayer = AdornerLayer.GetAdornerLayer(this.m_DiagramControl);
                    if (adornerLayer != null)
                    {
                        this.m_Adorner = new RotateAdorner(this.m_DiagramItem);
                        adornerLayer.Add(this.m_Adorner);
                    }
                }
            }
        }

        private double UnwindAngle(double value)
        {
            while (value > 180.0)
            {
                value -= 360.0;
            }

            while (value < -180.0)
            {
                value += 360.0;
            }

            return value;
        }

        private double DivideAngle(double value, double parts)
        {
            var unwinded = UnwindAngle(value);
            var result = (Math.Round(parts * (unwinded / 360.0) + parts) % (int)parts) * 360.0 / parts;
            return result;
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.m_DiagramItem != null && this.m_DiagramControl != null)
            {
                var currentPoint = Mouse.GetPosition(this.m_DiagramControl);
                var deltaVector = Point.Subtract(currentPoint, this.m_CenterPoint);

                var angle = this.m_InitialAngle + Vector.AngleBetween(this.m_StartVector, deltaVector);

                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    //
                    // Put angle into 24 ranges by 15*.
                    //
                    //  Circle is divided into 24 parts, rotated by 1/2 of part into positive angle.
                    //
                    //  When value of that angle is < than half, snap it left, otherwise, right.
                    //
                    angle = DivideAngle(angle, 24.0);
                }

                var roundedAngle = Math.Round(angle, 0);

                this.m_DiagramItem.RotationAngle = roundedAngle;
                this.m_DiagramItem.InvalidateMeasure();
            }
        }

        private void RotateThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this.m_Adorner != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(this.m_DiagramControl);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.m_Adorner);
                }

                this.m_Adorner = null;
            }
        }
    }
}
