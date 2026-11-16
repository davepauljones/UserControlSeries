using System.Windows;
using System.Windows.Media;

namespace UserControlSeries
{
    public partial class MainWindow : Window
    {
        private bool _isPowerMode = false;

        public MainWindow()
        {
            InitializeComponent();
            UpdateModeUI();
        }

        private void BtnTestAnim_Click(object sender, RoutedEventArgs e)
        {
            _isPowerMode = false;
            MyMeter.SignalMax = 100.0;
            MyMeter.StartSlidingAnimation();
            UpdateModeUI();
        }

        private void BtnSignalMode_Click(object sender, RoutedEventArgs e)
        {
            MyMeter.StopSlidingAnimation();
            _isPowerMode = false;
            MyMeter.SignalMax = 100.0;
            Slider100.Value = MyMeter.SignalStrength;
            MyMeter.SignalStrength = Slider100.Value;
            UpdateModeUI();
        }

        private void BtnPowerMode_Click(object sender, RoutedEventArgs e)
        {
            MyMeter.StopSlidingAnimation();
            _isPowerMode = true;
            MyMeter.PowerMax = 100.0;
            Slider100.Value = MyMeter.PowerValue;
            MyMeter.PowerValue = Slider100.Value;
            UpdateModeUI();
        }

        private void Slider100_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MyMeter == null || TxtSlider100 == null) return;

            MyMeter.StopSlidingAnimation();

            if (_isPowerMode)
            {
                MyMeter.PowerMax = 100.0;
                MyMeter.PowerValue = e.NewValue;
            }
            else
            {
                MyMeter.SignalMax = 100.0;
                MyMeter.SignalStrength = e.NewValue;
            }

            UpdateModeUI();
        }

        private void Slider255_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MyMeter == null || TxtSlider255 == null) return;

            MyMeter.StopSlidingAnimation();
            _isPowerMode = false;
            MyMeter.SignalMax = 255.0;
            MyMeter.SignalStrength = e.NewValue;

            TxtSlider255.Text = $"Signal Raw (0-255): {e.NewValue:F0}";
            UpdateModeUI();
        }

        private void Slider30_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MyMeter == null || TxtSlider30 == null) return;

            MyMeter.StopSlidingAnimation();
            _isPowerMode = true;
            MyMeter.PowerMax = 30.0;
            MyMeter.PowerValue = e.NewValue;

            TxtSlider30.Text = $"Power Scale A (0-30): {e.NewValue:F1}";
            UpdateModeUI();
        }

        private void Slider10_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MyMeter == null || TxtSlider10 == null) return;

            MyMeter.StopSlidingAnimation();
            _isPowerMode = true;
            MyMeter.PowerMax = 10.0;
            MyMeter.PowerValue = e.NewValue;

            TxtSlider10.Text = $"Power Scale B (0-10): {e.NewValue:F1}";
            UpdateModeUI();
        }

        private void UpdateModeUI()
        {
            if (TxtSlider100 == null || BtnSignalMode == null || BtnPowerMode == null) return;

            BtnSignalMode.Background = !_isPowerMode ? Brushes.LightGreen : SystemColors.ControlBrush;
            BtnPowerMode.Background = _isPowerMode ? Brushes.Salmon : SystemColors.ControlBrush;

            string modeLabel = _isPowerMode ? "Power" : "Signal";
            double activeVal = _isPowerMode ? MyMeter.PowerValue : MyMeter.SignalStrength;
            double activeMax = _isPowerMode ? MyMeter.PowerMax : MyMeter.SignalMax;
            double percent = (activeVal / activeMax) * 100.0;

            TxtSlider100.Text = $"Active Mode [{modeLabel}]: {percent:F0}%";
        }
    }
}