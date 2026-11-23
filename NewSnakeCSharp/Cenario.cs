
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace NewSnakeCSharp;

public class Cenario
{
    public List<Rectangle> Obstaculos { get; set; } = [];

    public Cenario(Canvas canvas)
    {
        int[] x = [0, 0,0, 980, 250, 730];
        int[] y = [0, 0, 780, 0, 200, 200];
        int[] altura = [800, 20, 20, 800, 400, 400];
        int[] largura = [20, 1000, 1000, 20, 20, 20];

        for (int i = 0; i < 6; i++)
        {
            var obstaculo = new Rectangle
            {
                Width = largura[i],
                Height = altura[i],
                Fill = Brushes.Red
            };

            Canvas.SetLeft(obstaculo, x[i]);
            Canvas.SetTop(obstaculo, y[i]);
            canvas.Children.Add(obstaculo);
            Obstaculos.Add(obstaculo);
        }
    }
}