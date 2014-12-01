using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ModernApp4Me.WP8.SnSWidgets.SnSWidgetMarqueeTextBlock
{

    public partial class SnSMarqueeTextBlock
    {

        public string MarqueeText
        {
            get { return (string)GetValue(MarqueeContentProperty); }
            set { SetValue(MarqueeContentProperty, value); }
        }

        public static readonly DependencyProperty MarqueeContentProperty =
            DependencyProperty.Register("MarqueeText", typeof(string), typeof(SnSMarqueeTextBlock), new PropertyMetadata("", OnTheMarqueeTextChanged));

        public double MarqueeHeight
        {
            get { return (double)GetValue(MarqueeHeightProperty); }
            set { SetValue(MarqueeHeightProperty, value); }
        }

        public static readonly DependencyProperty MarqueeHeightProperty =
            DependencyProperty.Register("MarqueeHeight", typeof(double), typeof(SnSMarqueeTextBlock), new PropertyMetadata((double)10, OnTheMarqueeHeightChanged));

        public double MarqueeWidth
        {
            get { return (double)GetValue(MarqueeWidthProperty); }
            set { SetValue(MarqueeWidthProperty, value); }
        }

        public static readonly DependencyProperty MarqueeWidthProperty =
            DependencyProperty.Register("MarqueeWidth", typeof(double), typeof(SnSMarqueeTextBlock), new PropertyMetadata((double)10, OnTheMarqueeWidthChanged));

        public Brush MarqueeBackground
        {
            get { return (Brush)GetValue(MarqueeBackgroundProperty); }
            set { SetValue(MarqueeBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MarqueeBackgroundProperty =
            DependencyProperty.Register("MarqueeBackground", typeof(Brush), typeof(SnSMarqueeTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.White), OnTheMarqueeBackgroundChanged));


        /// <summary>
        /// Constructor.
        /// </summary>
        public SnSMarqueeTextBlock()
        {
            InitializeComponent();

            CanvasMarquee.Height = ActualHeight;
            CanvasMarquee.Width = ActualWidth;

            Loaded += SnSMarqueeTextBlock_Loaded;
        }

        /// <summary>
        /// Function automatically called when the property MarqueeText is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void OnTheMarqueeTextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ((SnSMarqueeTextBlock)sender).TxtMarquee.Text = Convert.ToString(args.NewValue);
        }

        /// <summary>
        /// Function automatically called when the property Marqueebackground is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void OnTheMarqueeBackgroundChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ((SnSMarqueeTextBlock)sender).Background = (Brush)args.NewValue;
            ((SnSMarqueeTextBlock)sender).CanvasMarquee.Background = (Brush)args.NewValue;
        }

        /// <summary>
        /// Function automatically called when the property MarqueeWidth is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void OnTheMarqueeWidthChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ((SnSMarqueeTextBlock)sender).Width = Convert.ToDouble(args.NewValue);
            ((SnSMarqueeTextBlock)sender).CanvasMarquee.Width = Convert.ToDouble(args.NewValue);
        }

        /// <summary>
        /// Function automatically called when the property MarqueeHeight is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void OnTheMarqueeHeightChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ((SnSMarqueeTextBlock)sender).Height = Convert.ToDouble(args.NewValue);
            ((SnSMarqueeTextBlock)sender).CanvasMarquee.Height = Convert.ToDouble(args.NewValue);
        }

        /// <summary>
        /// Function that is called when the widget is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnSMarqueeTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            var rectangleGeometry = new RectangleGeometry
            {
                Rect = new Rect(new Point(0, 0), new Size(CanvasMarquee.Width, CanvasMarquee.Height))
            };
            CanvasMarquee.Clip = rectangleGeometry;

            /*var height = CanvasMarquee.Height - TxtMarquee.ActualHeight;
            TxtMarquee.Margin = new Thickness(0, height / 2, 0, 0);*/

            var doubleAnimation = new DoubleAnimation
            {
                From = CanvasMarquee.Width,
                To = -(CanvasMarquee.Width + TxtMarquee.ActualWidth),
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(5))
            };

            var storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation, TxtMarquee);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Canvas.Left)"));
            storyboard.Begin();
        }
    }
}
