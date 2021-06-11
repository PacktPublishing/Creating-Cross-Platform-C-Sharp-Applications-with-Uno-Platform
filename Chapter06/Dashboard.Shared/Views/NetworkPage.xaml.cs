using Windows.UI.Xaml.Controls;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using UnoBookRail.Common.Mapping;
using UnoBookRail.Common.Network;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Dashboard.Views
{
    public sealed partial class NetworkPage : Page
    {
        public NetworkPage()
        {
            this.InitializeComponent();
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = SetUpCanvas(e);
            DrawLines(canvas);
            DrawStations(canvas);
            DrawTrains(canvas);
        }

        private SKCanvas SetUpCanvas(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var relativeWidth = e.Info.Width / ImageMap.Width;
            var relativeHeight = e.Info.Height / ImageMap.Height;
            canvas.Scale(Math.Min(relativeWidth, relativeHeight));
            var x = 0f;
            var y = 0f;
            if (relativeWidth > relativeHeight)
            {
                x = (e.Info.Width - (ImageMap.Width * relativeHeight)) / 2f / relativeHeight;
            }
            else
            {
                y = (e.Info.Height - (ImageMap.Height * relativeWidth)) / 2f / relativeWidth;
            }
            canvas.Translate(x, y);
            canvas.Clear();
            return canvas;
        }

        void DrawLines(SKCanvas canvas)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                StrokeWidth = 1,
            };
            var northPnts = ImageMap.GetStations(Branch.NorthBranch);
            var mainPnts = ImageMap.GetStations(Branch.MainLine);
            var southPnts = ImageMap.GetStations(Branch.SouthBranch);

            SKPoint[] ToSKPointArray(List<(float X, float Y)> list)
                => list.Select(p => new SKPoint(p.X, p.Y)).ToArray();

            void DrawBranch(SKPoint[] stnPoints)
                => canvas.DrawPoints(SKPointMode.Polygon, stnPoints, paint);

            DrawBranch(ToSKPointArray(northPnts));
            DrawBranch(ToSKPointArray(mainPnts));
            DrawBranch(ToSKPointArray(southPnts));
        }

        void DrawStations(SKCanvas canvas)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Fill,
            };

            foreach (var (X, Y) in ImageMap.Stations)
            {
                canvas.DrawCircle(new SKPoint(X, Y), 2, paint);
            }
        }

        void DrawTrains(SKCanvas canvas)
        {
            var trainPaint = new SKPaint
            {
                Color = SKColors.Cyan,
                Style = SKPaintStyle.Fill,
            };

            foreach (var train in ImageMap.GetTrainsInNetwork())
            {
                canvas.DrawCircle(new SKPoint(train.MapPosition.X, train.MapPosition.Y), 1.8f, trainPaint);
            }
        }
    }
}
