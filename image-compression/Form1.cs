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

				string[] sizes = { "B", "KB", "MB", "GB", "TB" };
				double len = new FileInfo(file).Length;
				int order = 0;
				while (len >= 1024 && order < sizes.Length - 1)
				{
					order++;
					len = len / 1024;
				}


				string result = String.Format("{0:0.##} {1}", len, sizes[order]);

				if(len < 500 && len > 125){
					i++;
					string ext = Path.GetExtension(file).ToUpper();
					if (ext == ".PNG" || ext == ".JPG")
					{
						CompressImage(file, textBox2.Text, 15);
						if (i < 95)
						{
							progressBar1.Value = i;

						}
					}

				}

				if (len < 1000 && len > 500)
				{
					i++;
					string ext = Path.GetExtension(file).ToUpper();
					if (ext == ".PNG" || ext == ".JPG")
					{
						CompressImage(file, textBox2.Text, 30);
						if (i < 95)
						{
							progressBar1.Value = i;

						}
					}

				}


				if (len < 2000 && len > 1000)
				{
					i++;
					string ext = Path.GetExtension(file).ToUpper();
					if (ext == ".PNG" || ext == ".JPG")
					{
						CompressImage(file, textBox2.Text, 50);
						if (i < 95)
						{
							progressBar1.Value = i;

						}
					}

				}



			}
			progressBar1.Value = 100;


			MessageBox.Show("resimler şuraya hazırlandı \n" + textBox2.Text);
			textBox2.Text = "";
			textBox1.Text = "";
			progressBar1.Value = 0;

		}

	}
}



