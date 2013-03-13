using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace wp4me.SnSWidgets.SnSWidgetMarqueeTextBlock
{
    public sealed partial class SnSMarqueeTextBlock : UserControl
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private RectangleGeometry _rectangleGeometry;
        private DoubleAnimation _doubleAnimation;
        private Storyboard _storyboard;


        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public string MarqueeText
        {
            get { return (string)GetValue(MarqueeContentProperty); }
            set { SetValue(MarqueeContentProperty, value); }
        }

        public static readonly DependencyProperty MarqueeContentProperty =
            DependencyProperty.Register("MarqueeText", typeof(string), typeof(SnSMarqueeTextBlock), new PropertyMetadata("",
                                  new PropertyChangedCallback(OnTheMarqueeTextChanged)));

        public double MarqueeHeight
        {
            get { return (double)GetValue(MarqueeHeightProperty); }
            set { SetValue(MarqueeHeightProperty, value); }
        }

        public static readonly DependencyProperty MarqueeHeightProperty =
            DependencyProperty.Register("MarqueeHeight", typeof(double), typeof(SnSMarqueeTextBlock), new PropertyMetadata((double)10,
                              new PropertyChangedCallback(
                                OnTheMarqueeHeightChanged)));

        public double MarqueeWidth
        {
            get { return (double)GetValue(MarqueeWidthProperty); }
            set { SetValue(MarqueeWidthProperty, value); }
        }

        public static readonly DependencyProperty MarqueeWidthProperty =
            DependencyProperty.Register("MarqueeWidth", typeof(double), typeof(SnSMarqueeTextBlock), new PropertyMetadata((double)10,
                              new PropertyChangedCallback(
                                OnTheMarqueeWidthChanged)));

        public Brush MarqueeBackground
        {
            get { return (Brush)GetValue(MarqueeBackgroundProperty); }
            set { SetValue(MarqueeBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MarqueeBackgroundProperty =
            DependencyProperty.Register("MarqueeBackground", typeof(Brush), typeof(SnSMarqueeTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.Red),
                            new PropertyChangedCallback(
                              OnTheMarqueeBackgroundChanged)));
        

        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Constructor.
        /// </summary>
        public SnSMarqueeTextBlock()
        {
            InitializeComponent();

            CanvasMarquee.Height = Height;
            CanvasMarquee.Width = Width;

            Loaded += new RoutedEventHandler(SnSMarqueeTextBlock_Loaded);
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
            //Geometric properties
            _rectangleGeometry = new RectangleGeometry();
            _rectangleGeometry.Rect = new Rect(new Point(0, 0), new Size(CanvasMarquee.ActualWidth, CanvasMarquee.ActualHeight));
            CanvasMarquee.Clip = _rectangleGeometry;
            var height = CanvasMarquee.ActualHeight - TxtMarquee.ActualHeight;
            TxtMarquee.Margin = new Thickness(0, height / 2, 0, 0);

            //Set the animation
            _doubleAnimation = new DoubleAnimation();
            _doubleAnimation.From = CanvasMarquee.ActualWidth;
            _doubleAnimation.To = -TxtMarquee.ActualWidth;
            _doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            _doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));

            //Set the storyboard
            _storyboard = new Storyboard();
            _storyboard.Children.Add(_doubleAnimation);
            Storyboard.SetTarget(_doubleAnimation, TxtMarquee);
            Storyboard.SetTargetProperty(_doubleAnimation, new PropertyPath("(Canvas.Left)"));

            //Begin the storyboard
            _storyboard.Begin();
        }
    }
}
