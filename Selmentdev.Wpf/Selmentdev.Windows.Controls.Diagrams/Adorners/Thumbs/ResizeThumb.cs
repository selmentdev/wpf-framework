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
using System.Windows.Media;
using Selmentdev.Windows.Core.Extensions;

namespace Selmentdev.Windows.Controls.Diagrams.Primitives
{
    public class ResizeThumb : Thumb
    {
        private double m_Angle;
        private Adorner m_Adorner;
        private Point m_TransformOrigin;
        private DiagramItem m_DiagramItem;
        private DiagramControl m_DiagramControl;

        public ResizeThumb()
        {
            DragStarted += new DragStartedEventHandler(this.ResizeThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
            DragCompleted += new DragCompletedEventHandler(this.ResizeThumb_DragCompleted);
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.m_DiagramItem = this.DataContext as DiagramItem;

            if (this.m_DiagramItem != null)
            {
                this.m_DiagramControl = VisualTreeHelper.GetParent(this.m_DiagramItem) as DiagramControl;

                if (this.m_DiagramControl != null)
                {
                    this.m_TransformOrigin = this.m_DiagramItem.RenderTransformOrigin;

                    var rotateTransform = this.m_DiagramItem.Transform.GetRotateTransform();
                    if (rotateTransform != null)
                    {
                        this.m_Angle = rotateTransform.Angle * Math.PI / 180.0;
                    }
                    else
                    {
                        this.m_Angle = 0.0;
                    }

                    var adornerLayer = AdornerLayer.GetAdornerLayer(this.m_DiagramControl);
                    if (adornerLayer != null)
                    {
                        this.m_Adorner = new SizeAdorner(this.m_DiagramItem);
                        adornerLayer.Add(this.m_Adorner);
                    }
                }
            }
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.m_DiagramItem != null)
            {
                // TODO: as you can see, code below is somehow non-trivial to add snapping ;)

                double deltaVertical, deltaHorizontal;
                double y, x;

                switch (VerticalAlignment)
                {
                    case System.Windows.VerticalAlignment.Bottom:
                        {
                            deltaVertical = Math.Min(-e.VerticalChange, this.m_DiagramItem.ActualHeight - this.m_DiagramItem.MinHeight);

                            x = (this.m_DiagramItem.Position.X) - (deltaVertical * this.m_TransformOrigin.Y * Math.Sin(-this.m_Angle));
                            y = (this.m_DiagramItem.Position.Y) + (this.m_TransformOrigin.Y * deltaVertical * (1 - Math.Cos(-this.m_Angle)));

                            this.m_DiagramItem.Position = new Point(x, y);
                            this.m_DiagramItem.Height = Math.Max(this.m_DiagramItem.MinHeight, this.m_DiagramItem.Height - deltaVertical);
                            break;
                        }
                    case System.Windows.VerticalAlignment.Top:
                        {
                            deltaVertical = Math.Min(e.VerticalChange, this.m_DiagramItem.ActualHeight - this.m_DiagramItem.MinHeight);

                            x = (this.m_DiagramItem.Position.X) + deltaVertical * Math.Sin(-this.m_Angle) - (this.m_TransformOrigin.Y * deltaVertical * Math.Sin(-this.m_Angle));
                            y = (this.m_DiagramItem.Position.Y) + deltaVertical * Math.Cos(-this.m_Angle) + (this.m_TransformOrigin.Y * deltaVertical * (1 - Math.Cos(-this.m_Angle)));

                            this.m_DiagramItem.Position = new Point(x, y);
                            this.m_DiagramItem.Height = Math.Max(this.m_DiagramItem.MinHeight, this.m_DiagramItem.Height - deltaVertical);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                switch (HorizontalAlignment)
                {
                    case System.Windows.HorizontalAlignment.Left:
                        {
                            deltaHorizontal = Math.Min(e.HorizontalChange, this.m_DiagramItem.ActualWidth - this.m_DiagramItem.MinWidth);

                            x = (this.m_DiagramItem.Position.X) + deltaHorizontal * Math.Cos(this.m_Angle) + (this.m_TransformOrigin.X * deltaHorizontal * (1 - Math.Cos(this.m_Angle)));
                            y = (this.m_DiagramItem.Position.Y) + deltaHorizontal * Math.Sin(this.m_Angle) - this.m_TransformOrigin.X * deltaHorizontal * Math.Sin(this.m_Angle);

                            this.m_DiagramItem.Position = new Point(x, y);
                            this.m_DiagramItem.Width = Math.Max(this.m_DiagramItem.MinWidth, this.m_DiagramItem.Width - deltaHorizontal);
                            break;
                        }
                    case System.Windows.HorizontalAlignment.Right:
                        {
                            deltaHorizontal = Math.Min(-e.HorizontalChange, this.m_DiagramItem.ActualWidth - this.m_DiagramItem.MinWidth);

                            x = (this.m_DiagramItem.Position.X) + (deltaHorizontal * this.m_TransformOrigin.X * (1 - Math.Cos(this.m_Angle)));
                            y = (this.m_DiagramItem.Position.Y) - this.m_TransformOrigin.X * deltaHorizontal * Math.Sin(this.m_Angle);

                            this.m_DiagramItem.Position = new Point(x, y);
                            this.m_DiagramItem.Width = Math.Max(this.m_DiagramItem.MinWidth, this.m_DiagramItem.Width - deltaHorizontal);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            e.Handled = true;
        }

        private void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
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
