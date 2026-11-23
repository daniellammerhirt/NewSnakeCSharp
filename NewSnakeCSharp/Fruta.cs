using System;

using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace NewSnakeCSharp;

public class Fruta
{
    public Rectangle Retangulo { get; private set; }
    public readonly Random rand = new();

    public Fruta(Canvas canvas)
    {
        Retangulo = CriaFruta();
        Posicionar(canvas);
        canvas.Children.Add(Retangulo);
    }

    public void Posicionar(Canvas canvas)
    {
        double x, y;
        do
        {
            x =  rand.Next(0, (int)canvas.Bounds.Width / 20) * 20;
            y =  rand.Next(0, (int)canvas.Bounds.Height / 20) * 20;
        } while (ColideComCenarioOuCorpo(x, y, canvas));
        Canvas.SetLeft(Retangulo, x);
        Canvas.SetTop(Retangulo, y);
    }

    private Rectangle CriaFruta()
    {
        return new Rectangle{Width = 20, Height = 20, Fill = Brushes.Yellow};
    }

    private bool ColideComCenarioOuCorpo(double x, double y, Canvas canvas)
    {
        return canvas.Children.OfType<Rectangle>().Any(r =>
            Math.Abs(x - Canvas.GetLeft(r)) < 20 &&
            Math.Abs(y - Canvas.GetTop(r)) < 20);
    }
}