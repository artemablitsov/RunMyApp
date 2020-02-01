using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing;

namespace RunMyApp
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public MainWindowModel(bool isFirstRun = false)
        {
            if (isFirstRun)
            {
                string baseDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!File.Exists(Path.Combine(baseDir, "settings.json")))
                {
                    File.WriteAllText(Path.Combine(baseDir, "settings.json"), JsonConvert.SerializeObject(this, Formatting.Indented));
                }
                var settings = JsonConvert.DeserializeObject<MainWindowModel>(File.ReadAllText(Path.Combine(baseDir, "settings.json")));
                Image = settings.Image;
                AppToRun = settings.AppToRun;
                if (!File.Exists(Image))
                {
                    throw new Exception("No image file found at: " + Image);
                } else if (!Image.Contains("\\") && !Image.Contains("/"))
                {
                    Image = Path.Combine(baseDir, Image);
                }
                Bitmap img = new Bitmap(Image);
                Width = img.Width;
                Height = img.Height;
            }
        }

        private int m_Width { get; set; }
        public int Width
        {
            get => m_Width;
            set
            {
                m_Width = value;
                OnProperyChanged("Width");
            }
        }
        private int m_Height { get; set; }
        public int Height
        {
            get => m_Height;
            set
            {
                m_Height = value;
                OnProperyChanged("Height");
            }
        }

        private string m_Image { get; set; }
        public string Image {
            get => m_Image; 
            set
            {
                m_Image = value;
                OnProperyChanged("Image");
            } 
        }
        private string m_AppToRun { get; set; }
        public string AppToRun {
            get => m_AppToRun;
            set
            {
                m_AppToRun = value;
                OnProperyChanged("AppToRun");
            }
        }


        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnProperyChanged(string _name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_name));
            }
        }
        #endregion
    }
}
