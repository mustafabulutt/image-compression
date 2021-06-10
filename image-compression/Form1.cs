using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace image_compression
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			for (int i = 10; i <= 100; i = i + 10)
			{
				comboBox1.Items.Add(i);
			}
			comboBox1.SelectedIndex = 0;

			progressBar1.Visible = false;
		}

		private void OpenFolderDialog(TextBox Filepath)
		{
			FolderBrowserDialog folderDlg = new FolderBrowserDialog();
			folderDlg.ShowNewFolderButton = true;

			DialogResult result = folderDlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				Filepath.Text = folderDlg.SelectedPath;
				Environment.SpecialFolder root = folderDlg.RootFolder;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFolderDialog(textBox1);

		}

		private void button2_Click(object sender, EventArgs e)
		{
			OpenFolderDialog(textBox2);

		}







		public static void CompressImage(string SoucePath, string DestPath, int quality)
		{
			var FileName = Path.GetFileName(SoucePath);
			DestPath = DestPath + "\\" + FileName;

			using (Bitmap bmp1 = new Bitmap(SoucePath))
			{
				ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

				System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;

				EncoderParameters myEncoderParameters = new EncoderParameters(1);

				EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);

				myEncoderParameters.Param[0] = myEncoderParameter;
				bmp1.Save(DestPath, jpgEncoder, myEncoderParameters);

			}
		}

		private static ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo codec in codecs)
			{
				if (codec.FormatID == format.Guid)
				{
					return codec;
				}
			}
			return null;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			progressBar1.Visible = true;

			string[] files = Directory.GetFiles(textBox1.Text);
			int i = 0;
			foreach (var file in files)
			{
				i++;
				string ext = Path.GetExtension(file).ToUpper();
				if (ext == ".PNG" || ext == ".JPG"){ 
					CompressImage(file, textBox2.Text, (int)comboBox1.SelectedItem);
					if (i <95) {
					progressBar1.Value = i;

					}
				}

			}
			progressBar1.Value = 100;


			MessageBox.Show("Resimleri küçülttüm metin abi şu yola kayıt ettim bi bakarsın :D \n" + textBox2.Text);
			textBox2.Text = "";
			textBox1.Text = "";
			progressBar1.Value = 0;

		}

	}
}



