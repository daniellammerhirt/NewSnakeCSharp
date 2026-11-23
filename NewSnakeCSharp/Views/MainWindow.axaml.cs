using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Threading;
using MsBox.Avalonia;

namespace NewSnakeCSharp.Views;

public partial class MainWindow : Window
{
    private Cobra cobra;
    private Fruta  fruta;
    private Cenario cenario;
    private DispatcherTimer _timer;
    private string direcao = "Esquerda";
    private bool jogoRodando = true;
    
    public MainWindow()
    {
        InitializeComponent();
        KeyDown += OnKeyDown;
        
        Dispatcher.UIThread.Post(Inicializa, DispatcherPriority.Loaded);
    }

    private void Inicializa()
    {
        cenario = new Cenario(MyCanvas);
        cobra = new Cobra(MyCanvas);
        fruta = new Fruta(MyCanvas);
        direcao = "Esquerda";
        jogoRodando = true;
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
        _timer.Tick += Jogo;
        _timer.Start();
    }
    
    private void Jogo(object? sender, EventArgs e)
    {
        if (!jogoRodando) return;
        cobra.MoveCorpo();
        MoveCobra();

        VerificaColisaoFruta();
        if (VerificaColisaoComObstaculo())
        {
            GameOver();
        }
    }

    private void MoveCobra()
    {
        double x = Canvas.GetLeft(cobra.Cabeca);
        double y = Canvas.GetTop(cobra.Cabeca);

        switch (direcao)
        {
            case "Esquerda": x -= 20;break;
            case "Direita": x += 20;break;
            case "Cima": y -= 20;break;
            case "Baixo": y += 20;break;
        }
        Canvas.SetLeft(cobra.Cabeca, x);
        Canvas.SetTop(cobra.Cabeca, y);
        }

    private void VerificaColisaoFruta()
    {
        double x = Canvas.GetLeft(cobra.Cabeca);
        double y = Canvas.GetTop(cobra.Cabeca);

        if (Math.Abs(x - Canvas.GetLeft(fruta.Retangulo)) < 20 &&
            Math.Abs(y - Canvas.GetTop(fruta.Retangulo)) < 20)
        {
            cobra.AdicionaParte(MyCanvas);
            fruta.Posicionar(MyCanvas);
        }
    }


    private bool VerificaColisaoComObstaculo()
    {
        double x = Canvas.GetLeft(cobra.Cabeca);
        double y = Canvas.GetTop(cobra.Cabeca);

        foreach (Rectangle r in cenario.Obstaculos)
            if (x + 10 > Canvas.GetLeft(r) && x < Canvas.GetLeft(r) + r.Width &&
                y + 10 > Canvas.GetTop(r) && y < Canvas.GetTop(r) + r.Height)
            {
                return true;
            }

        foreach (Rectangle parte in cobra.Corpo)
            if (Math.Abs(x - Canvas.GetLeft(parte)) < 20 &&
                Math.Abs(y - Canvas.GetTop(parte)) < 20)
            {
                return true;
            }

        return false;
    }

    private async void GameOver()
    {
        jogoRodando = false;
        _timer.Stop();
        var box = MessageBoxManager.GetMessageBoxStandard("Game Over",
            "Click ok para reiniciar", MsBox.Avalonia.Enums.ButtonEnum.Ok);
        await box.ShowAsync();
        MyCanvas.Children.Clear();
        Inicializa();
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Left: direcao = "Esquerda"; break;
            case Key.Right: direcao = "Direita"; break;
            case Key.Up: direcao = "Cima"; break;
            case Key.Down: direcao = "Baixo"; break;
        }
    }
            
        
    }