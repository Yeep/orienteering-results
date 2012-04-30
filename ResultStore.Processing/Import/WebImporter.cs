using System;
using HtmlAgilityPack;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;
using ResultStore.Model.Application;

namespace ResultStore.Processing.Import
{
    public abstract class WebImporter : IImporter
    {
        private HtmlWeb m_web = new HtmlWeb();
        private WebEvent m_event;

        //---------------------------------------------------------------------------------------------------

        public abstract string Name { get; }
        public abstract string Description { get; }

        public QueuedEvent Event {
            get { return m_event; }
            set {
                if (value is WebEvent) {
                    m_event = (WebEvent)value;
                } else {
                    throw new Exception("Event must be of type WebEvent");
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        protected WebEvent InternalEvent {
            get { return m_event; }
        }

        //---------------------------------------------------------------------------------------------------

        public abstract decimal Probability();
        public abstract Event Sample();
        public abstract Event Import(IImportRepository repository);

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
