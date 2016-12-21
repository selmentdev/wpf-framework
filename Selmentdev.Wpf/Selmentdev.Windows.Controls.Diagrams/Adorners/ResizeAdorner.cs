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
using System.Windows.Documents;
using System.Windows.Media;

namespace Selmentdev.Windows.Controls.Diagrams.Primitives
{
    public class ResizeAdorner : Adorner
    {
        private VisualCollection m_Visuals;
        private ResizeChrome m_Chrome;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.m_Visuals.Count;
            }
        }

        public ResizeAdorner(DiagramItem diagramItem)
            : base(diagramItem)
        {
            this.m_Chrome = new ResizeChrome();
            this.m_Visuals = new VisualCollection(this);
            this.m_Visuals.Add(this.m_Chrome);
            this.m_Chrome.DataContext = diagramItem;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.m_Chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.m_Visuals[index];
        }
    }
}
