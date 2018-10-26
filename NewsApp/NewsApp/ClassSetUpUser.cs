using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
       static public void SaveSetUp()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SetUp.txt");
            bool doesExist = File.Exists(fileName);
            string text = null;
            text = FigShow.ToString() + "\n"+ FigDesc.ToString();
            File.WriteAllText(fileName, text);
        }
       static public void OpenSetUp()
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
                    }
                    catch
                    {

                    }
                }
                else
                {
                    SaveSetUp();
                }
            }
            catch
            {

            }
        }
    }
}
