using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XImgProc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = OpenCvSharp.Point;
using Rect = OpenCvSharp.Rect;
using OxyPlot;
using OxyPlot.Series;
using Window = System.Windows.Window;

namespace FillTube
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_LoadImageAxis1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.tif; *.bmp)|*.jpg; *.jpeg; *.tif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                axis1.Source = new BitmapImage(fileUri);
            }
        }

        private void Button_LoadImageAxis2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.tif; *.bmp)|*.jpg; *.jpeg; *.tif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                axis2.Source = new BitmapImage(fileUri);
            }
        }

        private void TextBox_TargetId_TextChanged(object sender, TextChangedEventArgs e)
        {
            //update TargetId
        }

        private void CppStyleMSER(Mat gray, Mat dst)
        {
            MSER mser = MSER.Create();
            Point[][] contours;
            Rect[] bboxes;
            mser.DetectRegions(gray, out contours, out bboxes);
            foreach (Point[] pts in contours)
            {
                Scalar color = Scalar.RandomColor();
                foreach (Point p in pts)
                {
                    dst.Circle(p, 1, color);
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Simmer_Checked(object sender, RoutedEventArgs e)
        {
            //enable simmer mode
        }

        private void Button_SetSourceVoltage_Click(object sender, RoutedEventArgs e)
        {
            //set source voltage
        }

        private void TextBox_SourceVoltage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_SetBeamCurrent_Click(object sender, RoutedEventArgs e)
        {
            //set beam current
        }

        private void TextBox_BeamCurrent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_CalcIceThickness_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.tif; *.bmp)|*.jpg; *.jpeg; *.tif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                axis1.Source = new BitmapImage(fileUri);

                Mat src = Cv2.ImRead(openFileDialog.FileName);
                Mat gray = src.CvtColor(ColorConversionCodes.BGR2GRAY);
                Mat binary = src.EmptyClone();
                Mat edges = src.EmptyClone();
                Mat regions = src.EmptyClone();
                Mat blobs = src.EmptyClone();

                CvXImgProc.NiblackThreshold(gray, binary, 255, ThresholdTypes.Binary, 51, -0.60, LocalBinarizationMethods.Niblack);
                Cv2.ImShow("Binarize1 - Niblack", binary);

                CvXImgProc.NiblackThreshold(gray, binary, 255, ThresholdTypes.Binary, 51, -0.07, LocalBinarizationMethods.Nick);
                Cv2.ImShow("Binarize3 - Nick", binary);

                CvXImgProc.NiblackThreshold(gray, binary, 255, ThresholdTypes.Binary, 51, 0.06, LocalBinarizationMethods.Sauvola);
                Cv2.ImShow("Binarize2 - Sauvola", binary);

                Cv2.Canny(binary, edges, 25, 150, 3, false);
                Cv2.ImShow("Canny Edges", edges);

                CppStyleMSER(binary, regions);
                Cv2.ImShow("MSER Detection", regions);

                ConnectedComponents cc = Cv2.ConnectedComponentsEx(binary);
                if (cc.LabelCount <= 1)
                    return;
                cc.RenderBlobs(blobs);
                Cv2.ImShow("Connected Components", blobs);
            }
        }

        private void TextBox_DesiredThickness_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_CalculatedThickness_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_DeltaThickness_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_StartFillLoop_Click(object sender, RoutedEventArgs e)
        {
            //start target fill process
        }

        private void Button_StopFillLoop_Click(object sender, RoutedEventArgs e)
        {
            //stop target fill process
        }

        private void TextBox_StartTemp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_EndTemp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_StepSize_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_NumSteps_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_SoakTime_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_FlashFreeze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_CaptureSeed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_PlotRamp_Click(object sender, RoutedEventArgs e)
        {
            //var plt = new PlotModel { Title = "Cosine Plot" };
            //plt.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }
    }
}
