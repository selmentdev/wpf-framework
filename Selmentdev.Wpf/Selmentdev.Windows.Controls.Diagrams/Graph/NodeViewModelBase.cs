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

using System.ComponentModel;
using System.Windows;

namespace Selmentdev.Windows.Controls.Diagrams
{
    public class NodeViewModelBase : INotifyPropertyChanged
    {
        #region Constructors
        public NodeViewModelBase()
        {
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Width Property
        private double m_Width;
        public double Width
        {
            get
            {
                return this.m_Width;
            }
            set
            {
                this.m_Width = value;
                this.RaisePropertyChanged(nameof(this.Width));
            }
        }
        #endregion

        #region Height Property
        private double m_Height;
        public double Height
        {
            get
            {
                return this.m_Height;
            }
            set
            {
                this.m_Height = value;
                this.RaisePropertyChanged(nameof(this.Height));
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
                this.m_Position = value;
                this.RaisePropertyChanged(nameof(this.Position));
            }
        }
        #endregion

        #region IsSelected Property
        private bool m_IsSelected;
        public bool IsSelected
        {
            get
            {
                return this.m_IsSelected;
            }
            set
            {
                this.m_IsSelected = value;
                this.RaisePropertyChanged(nameof(this.IsSelected));
            }
        }
        #endregion

        #region Content Property
        private object m_Content;
        public object Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
                this.RaisePropertyChanged(nameof(this.Content));
            }
        }
        #endregion

        #region Visibility Property
        private Visibility m_Visibility;
        public Visibility Visibility
        {
            get
            {
                return this.m_Visibility;
            }
            set
            {
                this.m_Visibility = value;
                this.RaisePropertyChanged(nameof(this.Visibility));
            }
        }
        #endregion
    }
}
