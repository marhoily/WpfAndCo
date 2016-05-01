using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Alphabet.Controls
{
    using Batch = IEnumerable<Tuple<Point, Point>>;


    public partial class LetterControl
    {
        public LetterControl()
        {
            InitializeComponent();
        }


        public bool ShowHints { get; set; }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
            "Value", typeof(string), typeof(LetterControl),
            new PropertyMetadata(default(string), Changed));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LetterControl)d).Changed((string)e.NewValue);
        }
        private void Changed(string value)
        {
            Cnv.Children.Clear();
            if (ShowHints)
                for (var c = 'A'; c < 'Z'; c++)
                {
                    var textBlock = new TextBlock
                    {
                        Text = c.ToString(),
                        FontSize = 10,
                    };
                    var p = ToPoint(c);
                    Canvas.SetLeft(textBlock, p.X - 5);
                    Canvas.SetTop(textBlock, p.Y - 7);
                    Panel.SetZIndex(textBlock, 1);
                    Cnv.Children.Add(textBlock);
                }

            if (value == null) return;
            foreach (var pr in Interpret(value))
                Cnv.Children.Add(new Line
                {
                    X1 = pr.Item1.X,
                    Y1 = pr.Item1.Y,
                    X2 = pr.Item2.X,
                    Y2 = pr.Item2.Y,
                });
        }
        private static bool IsValid(char c) => c >= 'A' && c <= 'Y';

        private static Point ToPoint(char c) =>
            new Point((c - 'A') % 5 * 30 + 10, (c - 'A') / 5 * 30 + 10);

        private static Batch Interpret(string input)
            => input
                .Split(',')
                .Select(g => g.Where(IsValid).Select(ToPoint).ToList())
                .SelectMany(x => x.Zip(x.Skip(1), Tuple.Create));

    }
}
