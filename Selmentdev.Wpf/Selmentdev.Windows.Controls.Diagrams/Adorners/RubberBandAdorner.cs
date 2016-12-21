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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Selmentdev.Windows.Controls.Diagrams.Primitives
{
    public class RubberBandAdorner : Adorner
    {
        private Point? m_StartPoint;
        private Point? m_EndPoint;
        private Pen m_Pen;

        private DiagramControl m_DiagramControl;

        public RubberBandAdorner(DiagramControl diagramControl, Point? startPoint)
            : base(diagramControl)
        {
            this.m_DiagramControl = diagramControl;
            this.m_StartPoint = startPoint;
            this.m_Pen = new Pen(SystemColors.HighlightBrush, 1);
            this.m_Pen.DashStyle = new DashStyle(new double[] { 2 }, 1);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                {
                    this.CaptureMouse();
                }

                this.m_EndPoint = e.GetPosition(this);
                this.UpdateSelection();
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured)
                {
                    this.ReleaseMouseCapture();
                }
            }

            e.Handled = true;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(this.m_DiagramControl);
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }

            e.Handled = true;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(this.RenderSize));

            if (this.m_StartPoint.HasValue && this.m_EndPoint.HasValue)
            {
                drawingContext.DrawRectangle(Brushes.Transparent, this.m_Pen, new Rect(this.m_StartPoint.Value, this.m_EndPoint.Value));
            }
        }

        private void UpdateSelection()
        {
            foreach (Control item in this.m_DiagramControl.Children)
            {
                var diagramItem = item as DiagramItem;
                if (diagramItem != null)
                {
                    diagramItem.IsSelected = false;
                }
            }

            var band = new Rect(this.m_StartPoint.Value, this.m_EndPoint.Value);
            foreach (Control item in this.m_DiagramControl.Children)
            {
                var itemRect = VisualTreeHelper.GetDescendantBounds(item);
                var itemBounds = item.TransformToAncestor(m_DiagramControl).TransformBounds(itemRect);

                if (band.Contains(itemBounds))
                {
                    var di = item as DiagramItem;
                    di.IsSelected = true;
                }
            }
        }
    }
}
