using Aspose.OMR;
using Aspose.OMR.Api;
using Aspose.OMR.CorrectionUI;
using System;
using System.IO;
using System.Windows;


namespace OMR_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Template for testing
        /// </summary>
        private static readonly string TemplateFilePath = @"C:\Files\OMR\Sheet.omr";

        /// <summary>
        /// Path to the license Aspose.OMR.NET.lic file
        /// </summary>
        private static readonly string LicensePath = @"";

        private CorrectionControl control;

        public MainWindow()
        {
            InitializeComponent();

            // Set and show template file path
            txtTemplatePath.Text = TemplateFilePath;

            // Set license, provide License file Path and uncomment to test full results
            License lic = new License();
            lic.SetLicense(LicensePath);
        }

        public string UserImagePath { get; set; }

        public string DataFolderPath { get; set; }


        /// <summary>
        /// Loads and displays CorrectionControl
        /// </summary>
        private void GetButtonClicked(object sender, RoutedEventArgs e)
        {
            string path = txtTemplatePath.Text;
            
            try
            {
                OmrEngine engine = new OmrEngine();
                TemplateProcessor processor = engine.GetTemplateProcessor(path);

                control = engine.GetCorrectionControl(processor);
                CustomContentControl.Content = control;
                control.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception");
            }
        }

        /// <summary>
        /// Select and display image 
        /// </summary>
        private void SelectImageClicked(object sender, RoutedEventArgs e)
        {
            if (control == null)
            {
                return;
            }

            string imagePath = DialogHelper.ShowOpenImageDialog(this.DataFolderPath);
            if (string.IsNullOrEmpty(imagePath))
            {
                return;
            }

            this.UserImagePath = imagePath;

            control.LoadAndDisplayImage(imagePath);
        }

        /// <summary>
        /// Recognize loaded image
        /// </summary>
        private void RecognizeImageClicked(object sender, RoutedEventArgs e)
        {
            if (control == null)
            {
                return;
            }

            control.RecognizeImage();
        }

        /// <summary>
        /// Export results to CSV
        /// </summary>
        private void ExportResultsClicked(object sender, RoutedEventArgs e)
        {
            if (control == null)
            {
                return;
            }

            string imageName = Path.GetFileNameWithoutExtension(this.UserImagePath);

            string path = DialogHelper.ShowSaveDataDialog(imageName);
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            control.ExportResults(path);

            MessageBox.Show("The exported resultant CSV file can be found here : " + path, "Operation Successful");
        }

    }
}
