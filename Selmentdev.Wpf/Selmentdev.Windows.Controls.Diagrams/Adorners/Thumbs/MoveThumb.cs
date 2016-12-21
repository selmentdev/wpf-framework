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
using System.Windows.Input;
using System.Windows.Media;
using Selmentdev.Windows.Core.Extensions;

namespace Selmentdev.Windows.Controls.Diagrams.Primitives
{
    public class MoveThumb : Thumb
    {
        private DiagramItem m_DiagramItem;
        private DiagramControl m_DiagramControl;

        public MoveThumb()
        {
            this.DragStarted += this.MoveThumb_DragStarted;
            this.DragDelta += this.MoveThumb_DragDelta;
            this.DragCompleted += this.MoveThumb_DragCompleted;
            this.MouseDoubleClick += MoveThumb_MouseDoubleClick;
        }

        private void MoveThumb_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var diagramItem = this.DataContext as DiagramItem;
            if (diagramItem != null)
            {
                diagramItem.IsSelected = !diagramItem.IsSelected;
            }
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.m_DiagramItem = this.DataContext as DiagramItem;
            if (this.m_DiagramItem != null)
            {
                this.m_DiagramControl = VisualTreeHelper.GetParent(this.m_DiagramItem) as DiagramControl;
            }
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.m_DiagramItem != null && this.m_DiagramControl != null && this.m_DiagramItem.IsSelected)
            {
                var dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                var rotateTransform = this.m_DiagramItem.Transform.GetRotateTransform();
                if (rotateTransform != null)
                {
                    dragDelta = rotateTransform.Transform(dragDelta);
                }

                var oldPosition = this.m_DiagramItem.Position;
                var newPosition = new Point(oldPosition.X + dragDelta.X, oldPosition.Y + dragDelta.Y);

                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    newPosition = new Point(
                        ScalarExtensions.SnapToGrid(newPosition.X, 10.0),
                        ScalarExtensions.SnapToGrid(newPosition.Y, 10.0)
                        );
                }

                this.m_DiagramItem.Position = newPosition;

                e.Handled = true;
            }
        }

        private void MoveThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
        }
    }
}
