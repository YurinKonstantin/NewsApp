using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsApp
{
   public class ClassComande
    {
        public async Task<List<RSSFeedItem>> zagruzka1(ClassIstochnik classIstochnik, double xw)
        {
            List<RSSFeedItem> Col1 = new List<RSSFeedItem>();

            try
            {
                // await prog.ProgressTo(.1, 250, Easing.Linear);

                
                int x = 0;
                int poz = 1;
               
                HttpClient client = new HttpClient();
                
                    HttpResponseMessage response = await client.GetAsync(classIstochnik.Urr);
                    //  await prog.ProgressTo(.3, 250, Easing.Linear);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //   await prog.ProgressTo(.6, 250, Easing.Linear);
                    var xdoc = XDocument.Parse(responseBody);



                    foreach (var item in xdoc.Descendants("item"))
                    {

                        var id = 0;
                        string title = "title";
                        string description = String.Empty;
                        string link = "title";
                        string pubDate = "title";
                        string url1 = "title";
                        string category = "Category";
                        Boolean H = true;
                        Double w = 1;
                        try
                        {
                            title = item.Element("title").Value;
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            link = item.Element("link").Value;
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            string[] text = item.Element("description").Value.Split(' ');
                            if (text.Length > 50)
                            {
                                for (int i = 0; i < 50; i++)
                                {
                                    description += text[i] + " ";
                                }

                            }
                            else
                            {
                                description = item.Element("description").Value;
                            }

                        }
                        catch (Exception ex)
                        {
                            // await DisplayAlert("Получили ошибку description ", ex.Message, "OK");
                        }
                        try
                        {
                            pubDate = item.Element("pubDate").Value;
                        }
                        catch (Exception ex)
                        {
                            pubDate = "00.00.0000";

                        }

                        try
                        {
                            url1 = item.Element("enclosure").Attribute("url").Value;
                            H = true;
                        }
                        catch (Exception ex)
                        {
                           H = false;

                        }
                        try
                        {
                            category = item.Element("category").Value;
                        }
                        catch (Exception ex)
                        {

                         //   await DisplayAlert("Получили ошибку category ", ex.Message, "OK");
                        }
                        id++;
                    bool fig = ClassSetUpUser.FigShow;
                        if (H)
                        {
                            w = 0.35 * (xw);
                        }
                        else
                        {
                        w = 0.35 * (xw);
                        fig = false;
                    }
                        
                        Col1.Add(new RSSFeedItem
                            {
                                Title = title,
                                Description = description,
                                Link = link,
                                PublishDate = pubDate,
                                Id = id,
                                Enclosure = url1,
                                Category = category,
                                h = w,
                                istochnic = classIstochnik.Istochnik,
                            FigShow = fig,
                            FigDesc = ClassSetUpUser.FigDesc

                        });
                        
                        // save values to database
                    }
                  
                

            }
            catch (Exception ex)
            {

            }
            finally
            {
                
                //   await prog.ProgressTo(1, 250, Easing.Linear);
            }
            return Col1;
            // RSSFeedItem.h = 0.75 * phonesList.Width;
            //   await prog.ProgressTo(1, 250, Easing.Linear);
            // await prog.ProgressTo(0, 250, Easing.Linear);
        }

    }
}
