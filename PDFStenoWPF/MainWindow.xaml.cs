using PDFSteganoLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace PDFSteganoWPF
{
    public partial class MainWindow
    {
        private string outDir;

        public MainWindow()
        {
            InitializeComponent();
            try {
                if (string.IsNullOrEmpty(Properties.Settings.Default.OutDir)) {
                    string newDir =
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PdfSteganography";
                    Directory.CreateDirectory(newDir);
                    Properties.Settings.Default.OutDir = newDir;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            outDir = Properties.Settings.Default.OutDir;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string filePath;
            string outPath;
            string msg;
            dlg.Filter = "PDF Files(*.PDF)|*.PDF|All Files(*.*)|*.*";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    filePath = dlg.FileName.ToString();
                    outPath = outDir + '\\' + Path.GetFileName(filePath);
                    msg = new TextRange(InputText.Document.ContentStart, InputText.Document.ContentEnd).Text.Replace("\r\n", "");
                    PdfLineSpacingStegano.InsertMessage(filePath, outPath, msg);
                    Process.Start("explorer.exe", outDir);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonGet_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string filePath;
            string msg;
            dlg.Filter = "PDF Files(*.PDF)|*.PDF|All Files(*.*)|*.*";
            dlg.InitialDirectory = outDir;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    filePath = dlg.FileName.ToString();
                    msg = PdfLineSpacingStegano.GetMessage(filePath);

                    OutText.Document.Blocks.Clear();
                    OutText.Document.Blocks.Add(new Paragraph(new Run(msg)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    Properties.Settings.Default.OutDir = fbd.SelectedPath;
                    Properties.Settings.Default.Save();
                    outDir = Properties.Settings.Default.OutDir;
                }
            }
        }
    }
}