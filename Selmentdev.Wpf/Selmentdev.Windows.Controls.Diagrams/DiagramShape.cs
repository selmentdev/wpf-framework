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
using System.Windows.Media;

namespace Selmentdev.Windows.Controls.Diagrams
{
    public class DiagramShape : DiagramItem
    {
        public static readonly DependencyProperty GeometryProperty = DependencyProperty.Register(nameof(DiagramShape.Geometry), typeof(Geometry), typeof(DiagramShape), new PropertyMetadata(new PropertyChangedCallback(DiagramShape.OnGeometryPropertyChanged)));
        public Geometry Geometry
        {
            get
            {
                return (Geometry)this.GetValue(GeometryProperty);
            }
            set
            {
                this.SetValue(DiagramShape.GeometryProperty, value);
            }
        }
        private static void OnGeometryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ds = d as DiagramShape;
            if (ds != null)
            {
                ds.OnGeometryChanged((Geometry)e.NewValue, (Geometry)e.OldValue);
            }
        }
        protected virtual void OnGeometryChanged(Geometry newValue, Geometry oldValue)
        {
            this.RaisePropertyChanged(nameof(this.Geometry));
            // call event handler when done.
        }

        //public event EventHandler<EventArgs>

        static DiagramShape()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramShape), new FrameworkPropertyMetadata(typeof(DiagramShape)));
        }
    }
}
