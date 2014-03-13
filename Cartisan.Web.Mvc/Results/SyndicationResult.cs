using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace Cartisan.Web.Mvc.Results {
    public class SyndicationResult: ActionResult {
        public SyndicationFeed Feed { get; set; }
        public FeedType Type { get; set; }

        public SyndicationResult() {
            Type = FeedType.Rss;
        }

        public SyndicationResult(string title, string description, Uri uri, IEnumerable<SyndicationItem> items) {
            Type = FeedType.Rss;
            Feed = new SyndicationFeed(title, description, uri, items);
        }

        public SyndicationResult(SyndicationFeed feed) {
            Type = FeedType.Rss;
            Feed = feed;
        }

        public override void ExecuteResult(ControllerContext context) {
            context.HttpContext.Response.ContentType = this.GetContentType();

            SyndicationFeedFormatter feedFormatter = this.GetFeedFormatter();

            XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output);

            feedFormatter.WriteTo(writer);

            writer.Flush();
            writer.Close();
        }

        private string GetContentType() {
            if(Type==FeedType.Atom) {
                return "application/atom+xml";
            }
            return "application/rss+xml";
        }

        private SyndicationFeedFormatter GetFeedFormatter() {
            if(Type == FeedType.Atom) {
                return new Atom10FeedFormatter();
            }
            return new Rss20FeedFormatter();
        }
        public enum FeedType {
            Rss = 0,
            Atom = 1
        }
    }
}