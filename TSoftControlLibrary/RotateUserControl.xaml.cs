//-----------------------------------------------------------------------
// <copyright>
//     Copyright (c) TommySoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;

namespace TSoftControlLibrary
{
    /// <summary>
    /// Interaction logic for RotateUserControl.xaml
    /// </summary>
    public partial class RotateUserControl : UserControl, INotifyPropertyChanged
    {
        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(RotateUserControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, coerceValueCallback));

        public Point arrowCenterPoint;

        bool isMouseRotating;

        double mouseDownAngle;

        private Vector mouseDownVector;

        public RotateUserControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String T = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(T));
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); OnPropertyChanged(); }        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            isMouseRotating = false;
            base.OnLostMouseCapture(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var mouseDownPoint = e.GetPosition(this);
            mouseDownVector = mouseDownPoint - arrowCenterPoint;
            mouseDownAngle = Angle;
            e.MouseDevice.Capture(this);
            isMouseRotating = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isMouseRotating)
            {
                Point curPos = e.GetPosition(this);
                Vector currentVector = curPos - arrowCenterPoint;
                Angle = Vector.AngleBetween(mouseDownVector, currentVector) + mouseDownAngle;
                Debug.WriteLine("Angle: " + Angle.ToString());
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (isMouseRotating)
            {
                e.MouseDevice.Capture(null);
                isMouseRotating = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            arrowCenterPoint = new Point(ActualWidth / 2, ActualHeight / 2);
        }

        static object coerceValueCallback(DependencyObject d, object baseValue)
        {
            var angle = (double)baseValue % 360.0;
            if (angle < 0)
                return angle + 360;
            else
                return angle;
        }

        //static double angleBetween(double angle1, double angle2)
        //{
        //    double diff = angle1 - angle2;
        //    double absDiff = Math.Abs(diff);

        //    if (absDiff <= Math.PI)
        //        return absDiff == Math.PI ? Math.PI : diff; // so -180 is 180 (in radians)

        //    else if (angle1 > angle2)
        //        return absDiff - (Math.PI * 2);

        //    return (Math.PI * 2) - absDiff;
        //}

    }
}
