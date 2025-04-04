using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinFormCharpWebCam;

namespace SchoolManagementSystem.Students
{
    public partial class TakeImage : DevExpress.XtraEditors.XtraForm
    {
        AddStudent s = new AddStudent();
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<StudentInfo> studentinfo = new ObservableCollection<StudentInfo>();

        public TakeImage()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            string str = "";
            studentinfo = fun.GetAllStudentsWId_S_C_S(str);
            searchLookUpEdit1.Text = "";
            searchLookUpEdit1.Properties.DataSource = studentinfo;
            searchLookUpEdit1.Properties.DisplayMember = "ID";
            searchLookUpEdit1.Properties.ValueMember = "ID";
        }
        WebCam webcam;

        private void TakeImage_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            webcam.Continue();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            imgCapture.Image = imgVideo.Image;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            webcam.Stop();
            this.Close();
            //   Help.SaveImageCapture(imgCapture.Image);
        }

        private void btnVideoF_Click(object sender, EventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void btnVideoS_Click(object sender, EventArgs e)
        {
            webcam.AdvanceSetting();
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Students\"; // <---
            var name = searchLookUpEdit1.Text;

            if (Directory.Exists(appPath) == false)                                              // <---
            {                                                                                    // <---
                Directory.CreateDirectory(appPath);                                              // <---
            }
            if (name != "" && Directory.Exists(appPath) != false)
            {
                Image image = imgCapture.Image;
                string filename = appPath + name.ToString() + ".Jpeg";
                FileStream fstream = new FileStream(filename, FileMode.Create);
                image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();
            }
        }
    }
}
