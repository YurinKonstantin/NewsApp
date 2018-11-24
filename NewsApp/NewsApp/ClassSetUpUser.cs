using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp
{
    class ClassSetUpUser
    {
       static bool figShow = true;
       static public bool FigShow
        {
            get
            {
                return figShow;
            }
            set
            {
                figShow = value;
            }
        }
        
      static  bool figDesc = true;
      static  public bool FigDesc
        {
            get
            {
                return figDesc;
            }
            set
            {
                figDesc = value;
            }
        }
        static bool myWebShow = true;
        static public bool MyWebShow
        {
            get
            {
                return myWebShow;
            }
            set
            {
                myWebShow = value;
            }
        }
        static async public Task SaveSetUp()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SetUp.txt");
            bool doesExist = File.Exists(fileName);
            string text = null;
            text = FigShow.ToString() + "\n"+ FigDesc.ToString() + "\n" + MyWebShow.ToString();
            File.WriteAllText(fileName, text);
        }
       static async public Task OpenSetUp()
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SetUp.txt");
                bool doesExist = File.Exists(fileName);
                if (doesExist == true)
                {
                    try
                    {
                        string text = File.ReadAllText(fileName);
                        string[] line = text.Split('\n');
                        FigShow = Convert.ToBoolean(line[0]);
                        figDesc = Convert.ToBoolean(line[1]);
                        if(line.Length>2)
                        {
                            MyWebShow = Convert.ToBoolean(line[2]);
                        }
                        else
                        {
                           await SaveSetUp();
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                   await SaveSetUp();
                }
            }
            catch
            {

            }
        }
    }
}
