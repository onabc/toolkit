using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Toolkit.Widget.Controls
{
    public class UniformPanel : UniformGrid
    {
        static UniformPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UniformPanel), new FrameworkPropertyMetadata(typeof(UniformPanel)));
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (Rows > double.Epsilon && Columns > double.Epsilon)
            {
                UIElementCollection children = InternalChildren;

                double devideWidth = constraint.Width / Columns;
                double devideHeight = constraint.Height / Rows;

                double rozmiar = Math.Min(devideWidth, devideHeight);

                for (int i = 0, count = children.Count; i < count; ++i)
                {
                    var child = children[i];
                    if (child == null) { continue; }

                    Size childConstraint = new Size(rozmiar, rozmiar);

                    child.Measure(childConstraint);
                }

                return new Size(rozmiar * Columns, rozmiar * Rows);
            }

            return constraint;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (Rows > double.Epsilon && Columns > double.Epsilon)
            {
                UIElementCollection children = InternalChildren;

                double devideWidth = arrangeSize.Width / Columns;
                double devideHeight = arrangeSize.Height / Rows;

                double rozmiar = Math.Min(devideWidth, devideHeight);

                Rect childBounds = new Rect(0, 0, rozmiar, rozmiar);
                double xStep = childBounds.Width;
                double xBound = arrangeSize.Width - 1.0;

                childBounds.X += childBounds.Width * FirstColumn;

                foreach (UIElement child in children)
                {
                    child.Arrange(childBounds);

                    if (child.Visibility != Visibility.Collapsed)
                    {
                        childBounds.X += xStep;
                        if (childBounds.X >= xBound)
                        {
                            childBounds.Y += childBounds.Height;
                            childBounds.X = 0;
                        }
                    }
                }
            }

            return arrangeSize;
        }
    }
}
