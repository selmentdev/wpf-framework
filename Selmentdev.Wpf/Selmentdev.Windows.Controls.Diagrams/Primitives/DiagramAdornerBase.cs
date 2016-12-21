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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Selmentdev.Windows.Core.Extensions;

namespace Selmentdev.Windows.Controls.Diagrams.Primitives
{
    public abstract class DiagramAdornerBase : Control
    {
        public virtual Diagram Diagram { get; set; }

        #region RotationTransform Property
        private readonly RotateTransform m_Rotation;
        public RotateTransform Rotation
        {
            get
            {
                return this.m_Rotation;
            }
        }
        #endregion

        #region Position Property
        private Point m_Position;
        public Point Position
        {
            get
            {
                return this.m_Position;
            }
            set
            {
                if (value != this.m_Position)
                {
                    this.m_Position = value;
                    this.SetLocation(this.m_Position);
                }
            }
        }
        #endregion

        #region Constructor
        protected DiagramAdornerBase()
        {
            var transformGroup = new TransformGroup();
            transformGroup.EnsureStandardTransform();
            this.m_Rotation = (transformGroup.Children[2] as RotateTransform);
            base.RenderTransform = new TransformGroup
            {
                Children =
                {
                    transformGroup,
                    new SkewTransform(),
                    new RotateTransform(),
                    new TranslateTransform(),
                }
            };
        }
        #endregion

        #region Manipulation Methods
        protected internal void Rotate(double angle)
        {
            if (this.m_Rotation.Angle != angle)
            {
                this.m_Rotation.Angle = angle;
            }
        }

        protected internal void Move(Point newPosition)
        {
            if (!Double.IsInfinity(newPosition.X) && !Double.IsInfinity(newPosition.Y))
            {
                this.Position = newPosition;
            }
        }

        protected internal void Resize(double width, double height)
        {
            if (!Double.IsInfinity(width) && !Double.IsInfinity(height) && (base.Width != width || base.Height != height))
            {
                base.Width = width;
                base.Height = height;
            }
        }
        #endregion
    }
}
