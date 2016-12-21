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
using System.Windows.Media;

namespace Selmentdev.Windows.Controls.Diagrams.Primitives
{
    public class DiagramItemDecorator : Control
    {
        private Adorner m_Adorner;

        public bool ShowDecorator
        {
            get
            {
                return (bool)GetValue(ShowDecoratorProperty);
            }
            set
            {
                SetValue(ShowDecoratorProperty, value);
            }
        }

        public static readonly DependencyProperty ShowDecoratorProperty = DependencyProperty.Register(
            "ShowDecorator",
            typeof(bool),
            typeof(DiagramItemDecorator),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ShowDecoratorProperty_Changed)));

        public DiagramItemDecorator()
        {
            this.Unloaded += new RoutedEventHandler(this.DesignerItemDecorator_Unloaded);
        }

        private void HideAdorner()
        {
            if (this.m_Adorner != null)
            {
                this.m_Adorner.Visibility = Visibility.Hidden;
            }
        }

        private void ShowAdorner()
        {
            if (this.m_Adorner == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(this);

                if (adornerLayer != null)
                {
                    var diagramItem = this.DataContext as DiagramItem;
                    var diagramControl = VisualTreeHelper.GetParent(diagramItem) as DiagramControl;
                    this.m_Adorner = new ManipulateAdorner(diagramItem);
                    adornerLayer.Add(this.m_Adorner);

                    if (this.ShowDecorator)
                    {
                        this.m_Adorner.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.m_Adorner.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                this.m_Adorner.Visibility = Visibility.Visible;
            }
        }

        private void DesignerItemDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.m_Adorner != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.m_Adorner);
                }

                this.m_Adorner = null;
            }
        }

        private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var decorator = (DiagramItemDecorator)d;
            bool showDecorator = (bool)e.NewValue;

            if (showDecorator)
            {
                decorator.ShowAdorner();
            }
            else
            {
                decorator.HideAdorner();
            }
        }
    }
}
