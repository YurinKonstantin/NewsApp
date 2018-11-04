using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp
{
    public class RSSFeedItem
    {
      
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PublishDate { get; set; }
        public int Id { get; set; }
        public string Enclosure { get; set; }
        public string Category { get; set; }
        public Double h { get; set; }
        public Double w { get; set; }
        public string collor { get; set; }
        string _istochnic = String.Empty;
        public string istochnic
        {
            get
            {
                return _istochnic;
            }
            set
            {
                _istochnic = value;
            }
        }

         bool figShow = true;
        public bool FigShow
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
        bool butShow = true;
        public bool ButShow
        {
            get
            {
                return butShow;
            }
            set
            {
                butShow = value;
            }
        }
        bool figDesc = true;
         public bool FigDesc
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


    }
}
