using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;

namespace Alphabet.Controls
{
    using Batch = IEnumerable<Tuple<Point, Point>>;

    public static class Ext
    {
        private static bool IsValid(char c) => c >= 'A' && c <= 'Y';

        private static Point ToPoint(char c) =>
            new Point((c - 'A') % 5 * 3 + 1, (c - 'A') / 5 * 3 + 1);

        public static Batch Interpret(this string input)
            => input
                .Split(',')
                .Select(g => g.Where(IsValid).Select(ToPoint).ToList())
                .SelectMany(x => x.Zip(x.Skip(1), Tuple.Create));
    }

    public partial class LetterControl 
    {
        public LetterControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register(
            "Value", typeof(string), typeof(LetterControl), 
            new PropertyMetadata(default(string), Changed));

        public string Value
        {
            get { return (string) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LetterControl)d).Changed((string)e.NewValue);
        }
        private void Changed(string value)
        {
            Cnv.Children.Clear();
            foreach (var pr in value.Interpret())
                Cnv.Children.Add(new Line
                {
                    X1 = pr.Item1.X,
                    Y1 = pr.Item1.Y,
                    X2 = pr.Item2.X,
                    Y2 = pr.Item2.Y,
                });
        }
    }
}
