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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Selmentdev.Windows.Core.Extensions;

namespace Selmentdev.Windows.Controls.Diagrams
{
    public class DiagramItem : ContentControl, INotifyPropertyChanged
    {
        #region Notify Property Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ActualSize Property
        public Size ActualSize
        {
            get
            {
                return new Size(this.ActualWidth, this.ActualHeight);
            }
        }
        #endregion

        #region Bounds Property
        public Rect Bounds
        {
            get
            {
                return new Rect(this.Position, this.ActualSize);
            }
        }
        #endregion

        #region ActualBounds Property
        public Rect ActualBounds
        {
            get
            {
                return this.Transform.GetRotateTransform().TransformBounds(this.Bounds);
            }
        }
        #endregion

        #region Position
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(nameof(DiagramItem.Position), typeof(Point), typeof(DiagramItem), new PropertyMetadata(DiagramItem.OnPositionPropertyChanged));
        public Point Position
        {
            get
            {
                return (Point)this.GetValue(PositionProperty);
            }
            set
            {
                this.SetValue(PositionProperty, value);
            }
        }
        private static void OnPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnPositionChanged((Point)e.NewValue, (Point)e.OldValue);
            }
        }
        protected virtual void OnPositionChanged(Point newValue, Point oldValue)
        {
            Canvas.SetLeft(this, newValue.X);
            Canvas.SetTop(this, newValue.Y);
            this.RaisePropertyChanged(nameof(this.Position));
        }
        #endregion

        #region ZIndex Property
        public static readonly DependencyProperty ZIndexProperty = DependencyProperty.Register(nameof(DiagramItem.ZIndex), typeof(int), typeof(DiagramItem), new PropertyMetadata(-1, DiagramItem.OnZIndexPropertyChanged));
        public int ZIndex
        {
            get
            {
                return (int)this.GetValue(ZIndexProperty);
            }
            set
            {
                this.SetValue(ZIndexProperty, value);
            }
        }
        private static void OnZIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnZIndexChanged((int)e.NewValue, (int)e.OldValue);
            }
        }
        protected virtual void OnZIndexChanged(int newValue, int oldValue)
        {
            Panel.SetZIndex(this, newValue);
            this.RaisePropertyChanged(nameof(this.ZIndex));
        }
        #endregion

        #region RotationAngle Property
        public static readonly DependencyProperty RotationAngleProperty = DependencyProperty.Register(nameof(DiagramItem.RotationAngle), typeof(double), typeof(DiagramItem), new PropertyMetadata(0.0, DiagramItem.OnRotationAnglePropertyChanged));
        public double RotationAngle
        {
            get
            {
                return (double)this.GetValue(RotationAngleProperty);
            }
            set
            {
                this.SetValue(RotationAngleProperty, value);
            }
        }
        private static void OnRotationAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnRotationAngleChanged((double)e.NewValue, (double)e.OldValue);
            }
        }
        protected virtual void OnRotationAngleChanged(double newValue, double oldValue)
        {
            var rotateTransform = this.Transform.GetRotateTransform();
            if (rotateTransform != null)
            {
                rotateTransform.Angle = newValue;
            }

            this.RaisePropertyChanged(nameof(RotationAngle));
        }
        #endregion

        #region IsSelected Property
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(DiagramItem.IsSelected), typeof(bool), typeof(DiagramItem), new PropertyMetadata(false, DiagramItem.OnIsSelectedPropertyChanged));
        public bool IsSelected
        {
            get
            {
                return (bool)this.GetValue(IsSelectedProperty);
            }
            set
            {
                this.SetValue(IsSelectedProperty, value);
            }
        }
        private static void OnIsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnIsSelectedChanged((bool)e.NewValue, (bool)e.OldValue);
            }
        }
        protected virtual void OnIsSelectedChanged(bool newValue, bool oldValue)
        {
            this.RaisePropertyChanged(nameof(this.IsSelected));
        }
        #endregion

        #region UniqueId Property
        public static readonly DependencyProperty UniqueIdProperty = DependencyProperty.Register(nameof(DiagramItem.UniqueId), typeof(bool), typeof(DiagramItem), new PropertyMetadata(DiagramItem.OnUniqueIdPropertyChanged));
        public Guid UniqueId
        {
            get
            {
                return (Guid)this.GetValue(UniqueIdProperty);
            }
            set
            {
                this.SetValue(UniqueIdProperty, value);
            }
        }
        private static void OnUniqueIdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnUniqueIdChanged((Guid)e.NewValue, (Guid)e.OldValue);
            }
        }
        protected virtual void OnUniqueIdChanged(Guid newValue, Guid oldValue)
        {
            this.RaisePropertyChanged(nameof(this.UniqueId));
        }
        #endregion

        #region Stroke Property
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof(DiagramItem.Stroke), typeof(Brush), typeof(DiagramItem), new PropertyMetadata(DiagramItem.OnStrokePropertyChanged));
        public Brush Stroke
        {
            get
            {
                return (Brush)this.GetValue(StrokeProperty);
            }
            set
            {
                this.SetValue(StrokeProperty, value);
            }
        }
        private static void OnStrokePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnStrokeChanged((Brush)e.NewValue, (Brush)e.OldValue);
            }
        }
        protected virtual void OnStrokeChanged(Brush newValue, Brush oldValue)
        {
            this.RaisePropertyChanged(nameof(this.Stroke));
        }
        #endregion

        #region StrokeThickness Property
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof(DiagramItem.StrokeThickness), typeof(double), typeof(DiagramItem), new PropertyMetadata(1.0, DiagramItem.OnStrokeThicknessPropertyChanged, DiagramItem.OnStrokeThicknessCoerce));
        public double StrokeThickness
        {
            get
            {
                return (double)this.GetValue(StrokeThicknessProperty);
            }
            set
            {
                this.SetValue(StrokeThicknessProperty, value);
            }
        }
        private static void OnStrokeThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnStrokeThicknessChanged((double)e.NewValue, (double)e.OldValue);
            }
        }
        private static object OnStrokeThicknessCoerce(DependencyObject element, object baseValue)
        {
            var result = (double)baseValue;
            return result;
        }
        protected virtual void OnStrokeThicknessChanged(double newValue, double oldValue)
        {
            this.RaisePropertyChanged(nameof(this.StrokeThickness));
        }
        #endregion

        #region StrokeDashArray Property
        public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(nameof(DiagramItem.StrokeDashArray), typeof(DoubleCollection), typeof(DiagramItem), new PropertyMetadata(new DoubleCollection(), DiagramItem.OnStrokeDashArrayPropertyChanged));
        public DoubleCollection StrokeDashArray
        {
            get
            {
                return (DoubleCollection)this.GetValue(StrokeDashArrayProperty);
            }
            set
            {
                this.SetValue(StrokeDashArrayProperty, value);
            }
        }
        private static void OnStrokeDashArrayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as DiagramItem;
            if (item != null)
            {
                item.OnStrokeDashArrayChanged((DoubleCollection)e.NewValue, (DoubleCollection)e.OldValue);
            }
        }
        protected virtual void OnStrokeDashArrayChanged(DoubleCollection newValue, DoubleCollection oldValue)
        {
            this.RaisePropertyChanged(nameof(this.StrokeDashArray));
        }
        #endregion

        #region X Property
        public double X
        {
            get
            {
                return this.Position.X;
            }
            set
            {
                this.Position = new Point(value, this.Position.Y);
            }
        }
        #endregion

        #region Y Property
        public double Y
        {
            get
            {
                return this.Position.Y;
            }
            set
            {
                this.Position = new Point(this.Position.X, value);
            }
        }
        #endregion

        #region Transform
        protected internal TransformGroup Transform
        {
            get
            {
                return this.RenderTransform as TransformGroup;
            }
        }
        #endregion

        #region Constructors
        public DiagramItem()
        {
            this.RenderTransform = new TransformGroup();
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Transform.EnsureStandardTransform();
        }
        #endregion

        #region Static Constructor
        static DiagramItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItem), new FrameworkPropertyMetadata(typeof(DiagramItem)));
        }
        #endregion
    }
}
