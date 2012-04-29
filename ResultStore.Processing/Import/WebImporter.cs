using System;
using HtmlAgilityPack;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Processing.Import
{
    public abstract class WebImporter : IImporter
    {
        private HtmlWeb m_web = new HtmlWeb();

        protected string WebAddress { get; set; }

        //---------------------------------------------------------------------------------------------------

        public abstract string Name { get; }
        public abstract string Description { get; }

        //---------------------------------------------------------------------------------------------------

        public abstract decimal Probability();
        public abstract Event Sample();
        public abstract void Import(IImportRepository repository);

        //---------------------------------------------------------------------------------------------------

        protected HtmlDocument LoadUrl(string base_url, string url) {
            Uri uri = new Uri(new Uri(base_url), url);
            return LoadUrl(uri.AbsoluteUri);
        }

        //---------------------------------------------------------------------------------------------------

        protected HtmlDocument LoadUrl(string url) {
            return m_web.Load(url);
        }
    }
}
