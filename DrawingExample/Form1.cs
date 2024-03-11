using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
namespace DrawingExample
{
    public partial class Form1 : Form
    {
        string path = "kira.jpg";
        private Bitmap editedImage = new Bitmap("kira.jpg");
        List<CoordinateInfo> coordinatesList;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {


        }

        private void create()
        {
            coordinatesList = new List<CoordinateInfo>
            {
                new CoordinateInfo { X = 175, Y = 212, Text = rentingCustomerNameSurnameTxt.Text },
                new CoordinateInfo { X = 600, Y = 212, Text = rentingCustomerTCNo.Text },
                new CoordinateInfo { X = 135, Y = 238, Text = rentingCustomerAddress.Text },
                new CoordinateInfo { X = 140, Y = 267, Text = rentingCustomerPhone.Text },
                new CoordinateInfo { X = 580, Y = 267, Text = rentingCustomerGSM.Text },

                new CoordinateInfo { X = 175, Y = 325, Text = tenantCustomerNameSurnameTxt.Text },
                new CoordinateInfo { X = 600, Y = 325, Text = tenantCustomerTCN.Text },
                new CoordinateInfo { X = 135, Y = 352, Text = tenantCustomerAddress.Text },
                new CoordinateInfo { X = 140, Y = 380, Text = tenantCustomerPhone.Text },
                new CoordinateInfo { X = 580, Y = 380, Text = tenantCustomerGSM.Text },

                new CoordinateInfo { X = 135, Y = 432, Text = estateAddress.Text },
                new CoordinateInfo { X = 135, Y = 467, Text = estateType.Text },
                new CoordinateInfo { X = 470, Y = 467, Text = estateLand.Text },
                new CoordinateInfo { X = 820, Y = 467, Text = estateRoomCount.Text },
                new CoordinateInfo { X = 485, Y = 492, Text = estateConfig.Text },

                new CoordinateInfo { X = 175, Y = 580, Text = expertNameSurname.Text },
                new CoordinateInfo { X = 600, Y = 580, Text = expertTCNo.Text },
                new CoordinateInfo { X = 230, Y = 607, Text = expertServiceFee.Text },
                new CoordinateInfo { X = 650, Y = 607, Text = contractDate.Text },
            };
        }
        private void generate()
        {

            editedImage = new Bitmap("kira.jpg");
            using (Graphics g = Graphics.FromImage(editedImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (Font font = new Font("Arial", 12))
                {
                    foreach (CoordinateInfo info in coordinatesList)
                    {
                        g.DrawString(info.Text, font, Brushes.Black, new PointF(info.X, info.Y));
                    }
                }
            }
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            create();
            generate();
            Form preview = new Form() { Size = new Size(600, 800), Text = "Önizleme" };
            PictureBox previewImg = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                BackgroundImageLayout = ImageLayout.Zoom,
                Image = editedImage,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Dock = DockStyle.Fill,
                Size = new Size(600, 800)

            };

            preview.Controls.Add(previewImg);
            preview.ShowDialog();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "jpeg|*.jpg";
            var results = dialog.ShowDialog();
            if (results != DialogResult.OK) return;

            if (File.Exists(dialog.FileName))
                File.Delete(dialog.FileName);

            editedImage.Save(dialog.FileName, ImageFormat.Jpeg);
            var MessageDialog = MessageBox.Show("Görüntü baþarýyla kaydedildi. Dosyayý açmak ister misiniz?","Baþarýlý",MessageBoxButtons.YesNo);
            if(MessageDialog != DialogResult.Yes) return;
            System.Diagnostics.Process.Start("explorer.exe", dialog.FileName);

        }
    }
}
class CoordinateInfo
{
    public int X { get; set; }
    public int Y { get; set; }
    public required string Text { get; set; }
}