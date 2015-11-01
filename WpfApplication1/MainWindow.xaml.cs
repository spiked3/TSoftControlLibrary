using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Math;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string TurnDirection
        {
            get { return (string)GetValue(TurnDirectionProperty); }
            set { SetValue(TurnDirectionProperty, value); }
        }
        public static readonly DependencyProperty TurnDirectionProperty =
            DependencyProperty.Register("TurnDirection", typeof(string), typeof(MainWindow), new PropertyMetadata("CW"));

        public float DegreesToTurn
        {
            get { return (float)GetValue(DegreesToTurnProperty); }
            set { SetValue(DegreesToTurnProperty, value); }
        }
        public static readonly DependencyProperty DegreesToTurnProperty =
            DependencyProperty.Register("DegreesToTurn", typeof(float), typeof(MainWindow), new PropertyMetadata(0F));

        public float AngleBetween
        {
            get { return (float)GetValue(NormalizedAngleBetweenProperty); }
            set { SetValue(NormalizedAngleBetweenProperty, value); }
        }
        public static readonly DependencyProperty NormalizedAngleBetweenProperty =
            DependencyProperty.Register("AngleBetween", typeof(float), typeof(MainWindow), new PropertyMetadata(0.0F));

        public MainWindow()
        {
            InitializeComponent();
            FromAngle.PropertyChanged += FromAngle_PropertyChanged;
            ToAngle.PropertyChanged += FromAngle_PropertyChanged;
        }

        //const double TwoPI = Math.PI * 2;
        private void FromAngle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var a = FromAngle.Angle - ToAngle.Angle;
            while (a > 180)
                a -= 360;
            while (a < -180)
                a += 360;
            AngleBetween = (float)(-a);

            TurnDirection = AngleBetween < 0 ? "CCW" : "CW";
            DegreesToTurn = (float)Abs(AngleBetween);
         }
    }
}
