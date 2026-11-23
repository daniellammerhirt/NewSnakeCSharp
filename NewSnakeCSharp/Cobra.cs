using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace NewSnakeCSharp;

public class Cobra
{
    public Rectangle Cabeca { get; set; }
    public List<Rectangle> Corpo { get; set; } = [];

    public Cobra(Canvas canvas)
    {
        Cabeca = CriaRetangulo(20, Brushes.Green);
        Canvas.SetLeft(Cabeca, 500);
        Canvas.SetTop(Cabeca, 480);
        canvas.Children.Add(Cabeca);

        for (int i = 0; i < 3; i++)
        {
            var parteCorpo = CriaRetangulo(20, Brushes.DarkGreen);
            Corpo.Add(parteCorpo);
            Canvas.SetLeft(parteCorpo, 520 + (i * 20));
            Canvas.SetTop(parteCorpo, 480);
            canvas.Children.Add(parteCorpo);
        }
    }

    public void MoveCorpo()
    {
        for (int i = Corpo.Count - 1; i > 0; i--)
        {
            Canvas.SetLeft(Corpo[i], Canvas.GetLeft(Corpo[i - 1]));
            Canvas.SetTop(Corpo[i], Canvas.GetTop(Corpo[i - 1]));
        }

        Canvas.SetLeft(Corpo[0], Canvas.GetLeft(Cabeca));
        Canvas.SetTop(Corpo[0], Canvas.GetTop(Cabeca));
    }

    public void AdicionaParte(Canvas canvas)
    {
        var novaParte = CriaRetangulo(20, Brushes.DarkGreen);
        Corpo.Add(novaParte);
        canvas.Children.Add(novaParte);
    }

    private Rectangle CriaRetangulo(double tamanho, IBrush cor)
    {
        return new Rectangle { Width = tamanho, Height = tamanho, Fill = cor };
    }
}