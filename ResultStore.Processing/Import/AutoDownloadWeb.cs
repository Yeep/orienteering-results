using System;
using HtmlAgilityPack;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Processing.Import
{
    public class AutoDownloadWeb : WebImporter
    {
        public AutoDownloadWeb(string address) {
            WebAddress = address;
        }

        //---------------------------------------------------------------------------------------------------

        public override string Name {
            get { return Strings.ResultStore_Processing_Import_AutoDownloadWeb_Name; }
        }

        //---------------------------------------------------------------------------------------------------

        public override string Description {
            get { return Strings.ResultStore_Processing_Import_AutoDownloadWeb_Description; }
        }

        //---------------------------------------------------------------------------------------------------

        public override decimal Probability() {
            // This is obviously wrong but I don't care right now
            return 1;
        }

        //---------------------------------------------------------------------------------------------------

        public override Event Sample() {
            return GetEventSection(GetResultsPage());
        }

        //---------------------------------------------------------------------------------------------------

        public override void Import(IImportRepository repository) {
            var home_page = GetResultsPage();

            // Create and commit the event section
            Event ev = GetEventSection(home_page);
            repository.AddEvent(ref ev);

            // Select all TR nodes with TD children
            var course_nodes = home_page.DocumentNode.SelectNodes("//tr[td]");
            foreach (var course_node in course_nodes) {
                Course course = new Course();
                course.Event = ev;

                // Fetch all information from the results page
                var address_node = course_node.SelectSingleNode("td/td/a");
                string address = address_node.Attributes["href"].Value;

                // Load the individual course page
                var course_page = LoadUrl(WebAddress, address);

                // Course name
                var name_node = course_page.DocumentNode.SelectSingleNode("//div[@class=\"course-title\"]/h2");
                course.Name = name_node.InnerText;

                // Course details
                var details_node = course_page.DocumentNode.SelectSingleNode("//p[@class=\"course-info\"][2]");
                course.Length = Convert.ToDecimal(details_node.InnerText.Split(new char[] { ' ' })[0].TrimEnd(new char[] { 'k', 'm' }));
                course.Climb = Convert.ToDecimal(details_node.InnerText.Split(new char[] { ' ' })[1].TrimEnd(new char[] { 'm' }));

                // Commit the course
                repository.AddCourse(ref course);

                // Results
                var results_nodes = course_page.DocumentNode.SelectNodes("//tr[td]");
                foreach (var results_node in results_nodes) {
                    Result result = new Result();
                    result.Course = course;

                    // Position can be a number or null, if null then use the string as a code
                    var position_node = results_node.SelectSingleNode("td");
                    try {
                        result.Position = Convert.ToInt32(position_node.ChildNodes[0].InnerText.TrimEnd(new char[] { 'r', 's', 't', 'n', 'd', 'h' }));
                    } catch (FormatException) {
                        result.Position = null;
                        result.Code = position_node.ChildNodes[0].InnerText;
                    }

                    // Name
                    var results_name_node = results_node.SelectSingleNode("td/td");
                    result.Name = results_name_node.ChildNodes[0].InnerText;

                    // Club
                    var club_name_node = results_node.SelectSingleNode("td/td/td");
                    result.ClubName = club_name_node.ChildNodes[0].InnerText;
                    result.Club = repository.TryGetClubFromName(result.ClubName);

                    // Age class
                    var age_class_node = results_node.SelectSingleNode("td/td/td/td");
                    result.Age = age_class_node.ChildNodes[0].InnerText;

                    // Time
                    var time_node = results_node.SelectSingleNode("td/td/td/td/td");
                    try {
                        result.Time = new TimeSpan(
                            0,
                            Convert.ToInt32(time_node.ChildNodes[0].InnerText.Split(new char[] { ':' })[0]),
                            Convert.ToInt32(time_node.ChildNodes[0].InnerText.Split(new char[] { ':' })[1])
                        );
                    } catch (FormatException) {
                        result.Time = null;
                    }

                    // Commit the result
                    repository.AddResult(ref result);
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        private HtmlDocument GetResultsPage() {
            if (string.IsNullOrEmpty(WebAddress)) {
                throw new Exception(Strings.ResultStore_Processing_Import_WebAddressNotSet);
            }

            return LoadUrl(WebAddress);
        }

        //---------------------------------------------------------------------------------------------------

        private Event GetEventSection(HtmlDocument page) {
            Event ev = new Event();

            // The event name is in a div with id="title"
            var title_node = page.DocumentNode.SelectSingleNode("//div[@id=\"title\"]");
            ev.Name = title_node.InnerText.Trim();

            return ev;
        }
    }
}
