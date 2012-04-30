using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResultStore.Model.Orienteering;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ResultStore.Processing.Import
{
    public class WinSplitsWeb : WebImporter
    {
        public override string Name {
            get { return Strings.Import_WinSplitsWeb_Name; }
        }

        //---------------------------------------------------------------------------------------------------

        public override string Description {
            get { return Strings.Import_WinSplitsWeb_Description; }
        }

        //---------------------------------------------------------------------------------------------------

        public override decimal Probability() {
            // This is obviously wrong but I don't care right now
            return 1;
        }

        //---------------------------------------------------------------------------------------------------

        public override Event Sample() {
            var page = LoadUrl(InternalEvent.Url);
            return GetEventSection(page);
        }

        //---------------------------------------------------------------------------------------------------

        public override Event Import(Repository.IImportRepository repository) {
            var index_page = LoadUrl(InternalEvent.Url);
            Event ev = GetEventSection(index_page);

            repository.Event(ref ev);

            var courses_node = index_page.DocumentNode.SelectSingleNode("//frame[@name=\"main\"]");
            if (courses_node == null) {
                throw new ImportFormatNotSupportedException("Could not find the event main frame");
            }

            var courses_address = courses_node.GetAttributeValue("src", String.Empty);
            if (string.IsNullOrWhiteSpace(courses_address)) {
                throw new ImportFormatNotSupportedException("Could not find the event classes address");
            }

            var top_page = LoadUrl(InternalEvent.Url, courses_address);
            
            // Loop over classes
            var courses_nodes = top_page.DocumentNode.SelectNodes("//a[starts-with(@href, \"default.asp?page=table\")]");
            foreach (var course_node in courses_nodes) {
                var course = new Course {
                    Event = ev,
                    Name = course_node.InnerText
                };

                repository.Course(ref course);

                // The course page address needs massaging
                var course_url = course_node.Attributes["href"].Value.Replace("default.asp?page=table&", "table.asp?");
                var course_page = LoadUrl(InternalEvent.Url, course_url);

                var result_nodes = course_page.DocumentNode.SelectNodes("//tr[starts-with(@onmouseover, \"highlight\")]");

                // Two rows per result
                for (int i = 0; i < result_nodes.Count; i += 2) {
                    var result = new Result {
                        Course = course
                    };

                    // Position can be a number or null, if null then use the string as a code
                    var position_node = result_nodes[i].SelectSingleNode("td[1]");
                    try {
                        result.Position = Convert.ToInt32(position_node.ChildNodes[0].InnerText);
                    } catch (FormatException) {
                        result.Position = null;
                    }

                    // Name
                    var results_name_node = result_nodes[i].SelectSingleNode("td[2]");
                    result.Name = results_name_node.ChildNodes[0].InnerText;

                    // Time
                    var time_node = result_nodes[i].SelectSingleNode("td[3]/a");
                    try {
                        result.Time = new TimeSpan(
                            0,
                            Convert.ToInt32(time_node.ChildNodes[0].InnerText.Split(new char[] { '.' })[0]),
                            Convert.ToInt32(time_node.ChildNodes[0].InnerText.Split(new char[] { '.' })[1])
                        );
                    } catch (FormatException) {
                        result.Time = null;
                    }

                    // Club
                    var club_name_node = result_nodes[i + 1].SelectSingleNode("td[1]");
                    result.ClubName = club_name_node.ChildNodes[0].InnerText;

                    repository.Result(ref result);
                }
            }

            return ev;
        }

        //---------------------------------------------------------------------------------------------------

        private Event GetEventSection(HtmlDocument index_page) {
            var top_node = index_page.DocumentNode.SelectSingleNode("//frame[@name=\"top\"]");
            if (top_node == null) {
                throw new ImportFormatNotSupportedException("Could not find the event header frame");
            }

            var top_address = top_node.GetAttributeValue("src", String.Empty);
            if (string.IsNullOrWhiteSpace(top_address)) {
                throw new ImportFormatNotSupportedException("Could not find the event header address");
            }

            var top_page = LoadUrl(InternalEvent.Url, top_address);

            var details_node = top_page.DocumentNode.SelectSingleNode("//a[@class=\"menubar\" and starts-with(@href, \"classes.asp\")]");
            if (details_node == null) {
                throw new ImportFormatNotSupportedException("Could not find the event details section");
            }

            var details_string = details_node.InnerText;

            try {
                var reg = new Regex(@"(.*), (.*) \[(.*)\]");
                var match = reg.Match(details_string);

                return new Event {
                    Name = match.Groups[1].Value,
                    ClubName = match.Groups[2].Value,
                    Date = DateTime.Parse(match.Groups[3].Value),
                    Url = InternalEvent.Url
                };
            } catch (Exception e) {
                throw new ImportFormatNotSupportedException("Error parsing event details section", e);
            }

            throw new ImportFormatNotSupportedException("This should never get reached");
        }
    }
}
