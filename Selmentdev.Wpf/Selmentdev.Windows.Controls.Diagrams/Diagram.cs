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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Selmentdev.Windows.Controls.Diagrams
{
    [ContentProperty("Items")]
    [TemplatePart(Name = "ItemsHost", Type = typeof(Panel))]
    public class Diagram : Control
    {

        static Diagram()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Diagram), new FrameworkPropertyMetadata(typeof(Diagram)));
            Diagram.ItemsProperty = Diagram.ItemsPropertyKey.DependencyProperty;
        }

        public Diagram()
        {
            base.SetValue(Diagram.ItemsPropertyKey, new ObservableCollection<DiagramItem>());
        }
        

        public static readonly DependencyPropertyKey ItemsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Diagram.Items), typeof(ObservableCollection<DiagramItem>), typeof(Diagram), new PropertyMetadata(null));
        public static readonly DependencyProperty ItemsProperty;
        public ObservableCollection<DiagramItem> Items
        {
            get
            {
                return (ObservableCollection<DiagramItem>)this.GetValue(Diagram.ItemsProperty);
            }
            private set
            {
                base.SetValue(Diagram.ItemsPropertyKey, value);
            }
        }

        public Style ShapeStyle { get; set; }

        public static readonly DependencyProperty ShapeStyleSelectorProperty = DependencyProperty.Register(nameof(Diagram.ShapeStyleSelector), typeof(StyleSelector), typeof(Diagram), new PropertyMetadata(null, Diagram.OnTemplateRelatedPropertyChanged));

        private static void OnTemplateRelatedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var diagram = d as Diagram;
            if (diagram != null)
            {
                diagram.UpdateStylesAndTemplates();
            }
        }

        private void UpdateStylesAndTemplates()
        {
            //throw new NotImplementedException();
        }

        public StyleSelector ShapeStyleSelector
        {
            get
            {
                return (StyleSelector)base.GetValue(Diagram.ShapeStyleSelectorProperty);
            }
            set
            {
                base.SetValue(Diagram.ShapeStyleSelectorProperty, value);
            }
        }

        private Style GetContainerStyle(object item, DependencyObject view)
        {
            var style = null as Style;
            var styleSelector = null as StyleSelector;

            if (view is DiagramShape)
            {
                styleSelector = this.ShapeStyleSelector;
                if (view is DiagramShape)
                {
                    style = this.ShapeStyle;
                }
            }

            return Diagram.GetValidStyle(styleSelector, style, item, view);
        }

        private static Style GetValidStyle(StyleSelector styleSelector, Style style, object item, DependencyObject view)
        {
            if (style == null && styleSelector != null)
            {
                style = styleSelector.SelectStyle(item, view);
            }

            return style;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.m_ItemsHost != null)
            {
                this.m_ItemsHost.Children.Clear();
                this.m_ItemsHost.LayoutUpdated -= this.OnItemsHostLayoutUpdated;
            }

            this.m_ItemsHost = (base.GetTemplateChild("ItemsHost") as Canvas);

            if (this.m_ItemsHost != null)
            {
                this.m_ItemsHost.LayoutUpdated += this.OnItemsHostLayoutUpdated;
            }

            this.AddItemsToCanvas();

            this.ApplyTemplateForItems();
        }

        private void OnItemsHostLayoutUpdated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ApplyTemplateForItems() // update styles and templates
        {
            for (int i = 0; i < this.Items.Count; ++i)
            {
                var item = this.Items[i];
                var itemContainer = this.GetContainerForItem(item);
            }
        }

        private FrameworkElement GetContainerForItem(object item)
        {
            if (item != null)
            {
                //return this.m_ContainerGenerator.ContainerForItem(item);
            }

            return null;
        }

        private Canvas m_ItemsHost;

        private void AddItemsToCanvas()
        {
            if (this.m_ItemsHost != null)
            {
                this.m_ItemsHost.Children.Clear();

                foreach (var current in this.Items.ToList<object>())
                {
                    this.AddItemToCanvas(current as DiagramItem);
                    //this.AddItemToCanvas(this.containergenerator.ContainerForItem(current));
                }
            }
        }

        private void AddItemToCanvas(DiagramItem container)
        {
            var element = container as FrameworkElement;
            if (element != null)
            {
                if (this.m_ItemsHost != null && !this.m_ItemsHost.Children.Contains(element))
                {
                    this.m_ItemsHost.Children.Add(element);
                    this.OnVisualChildrenChanged();
                }
            }
        }

        private void RemoveItemFromCanvas(DiagramItem container)
        {
            if (this.m_ItemsHost != null)
            {
                var element = container as FrameworkElement;
                if (element != null && this.m_ItemsHost.Children.Contains(element))
                {
                    this.m_ItemsHost.Children.Remove(element); ;
                    this.OnVisualChildrenChanged();
                }
            }
        }

        public event EventHandler VisualChildrenChanged;

        private void OnVisualChildrenChanged()
        {
            this.VisualChildrenChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
