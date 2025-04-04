using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace SchoolManagementSystem
{
    public partial class ss : SplashScreen
    {
        CommonFunctions fun = new CommonFunctions();
        List<string> slider_img = new List<string>();
        string login_imgs = Path.GetDirectoryName(Application.ExecutablePath) + @"\slider_images\login";

        public ss()
        {
            InitializeComponent();
            load_next_image();
            lbl_year.Text = DateTime.Now.Year.ToString();
            if (Login.Logo != null)
            {
                Image mylogo = fun.Base64ToImage(Login.Logo);
                PicLogo.Image = mylogo;
            }
        }
        void load_next_image()
        {
            if (Directory.Exists(login_imgs))
                slider_img = Directory.GetFiles(login_imgs, "*.*", SearchOption.AllDirectories).ToList();
            foreach (string img in slider_img)
            {
                imageSlider1.Images.Add(Image.FromFile(img, true));
            }
        }
        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

      
    }
}
