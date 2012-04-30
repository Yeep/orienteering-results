using System;
using HtmlAgilityPack;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Processing.Import
{
    public class AutoDownloadWeb : WebImporter
    {
        public AutoDownloadWeb() { }

        //---------------------------------------------------------------------------------------------------

        public override string Name {
            get { return Strings.Import_AutoDownloadWeb_Name; }
        }

        //---------------------------------------------------------------------------------------------------

        public override string Description {
            get { return Strings.Import_AutoDownloadWeb_Description; }
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

        public override Event Import(IImportRepository repository) {
            var home_page = GetResultsPage();

            // Create and commit the event section
            Event ev = GetEventSection(home_page);
            repository.Event(ref ev);

            // If there are links in the data table this is a multi-page event
            if (home_page.DocumentNode.SelectSingleNode("//table[@class=\"data\"]//a[text()=\"Results\"]") != null) {
                // If there is a link with the text "Splits" the event probably has splits
                if (home_page.DocumentNode.SelectSingleNode("//table[@class=\"data\"]//a[text()=\"Splits\"]") != null) {
                    ParseMultiplePageWithSplits(repository, home_page, ev);
                    return ev;
                } else {
                    // This seems to only appear when there are multiple runs per class which I can't currently handle
                    throw new Exception("Import of this type of event is not currently supported");
                    //ParseMutiplePage(repository, home_page, ev);
                }
            }

            // If there is a course title then this is a single-page event
            if (home_page.DocumentNode.SelectSingleNode("//div[@class=\"course-title\"]") != null) {
                ParseSinglePage(repository, home_page, ev);
                return ev;
            }

            throw new Exception("Import of this type of event is not currently supported");
        }

        //---------------------------------------------------------------------------------------------------

        private HtmlDocument GetResultsPage() {
            if (string.IsNullOrEmpty(InternalEvent.Url)) {
                throw new Exception(Strings.Import_WebAddressNotSet);
            }

            return LoadUrl(InternalEvent.Url);
        }

        //---------------------------------------------------------------------------------------------------

        private Event GetEventSection(HtmlDocument page) {
            Event ev = new Event();

            // The event name is in a div with id="title"
            var title_node = page.DocumentNode.SelectSingleNode("//div[@id=\"title\"]");
            var title = title_node.InnerText.Trim();

            try {
                ev.Name = title.Remove(title.LastIndexOf(" on")).Remove(0, "Results for".Length);
                ev.Date = DateTime.Parse(title.Substring(title.LastIndexOf(" on ") + " on ".Length));
            } catch (Exception) {
                // If anything goes wrong just use the entire title as the name
                ev.Name = title;
            }

            ev.Url = InternalEvent.Url;

            return ev;
        }

        //---------------------------------------------------------------------------------------------------

        private void ParseMultiplePage(
            IImportRepository repository, 
            HtmlDocument document, 
            Event ev,
            string course_selector,
            string address_selector,
            string course_name_selector,
            string course_details_selector,
            string results_selector,
            string results_position_selector,
            string results_name_selector,
            string results_club_selector,
            string results_age_selector,
            string results_time_selector
        ) {
            // Courses
            var course_nodes = document.DocumentNode.SelectNodes(course_selector);
            foreach (var course_node in course_nodes) {
                Course course = new Course();
                course.Event = ev;

                // Fetch all information from the results page
                var address_node = course_node.SelectSingleNode(address_selector);
                string address = address_node.Attributes["href"].Value;

                // Load the individual course page
                var course_page = LoadUrl(InternalEvent.Url, address);

                // Course name
                var name_node = course_page.DocumentNode.SelectSingleNode(course_name_selector);
                course.Name = name_node.InnerText;

                // Course details
                var details_node = course_page.DocumentNode.SelectSingleNode(course_details_selector);
                course.Length = Convert.ToDecimal(details_node.InnerText.Split(new char[] { ' ' })[0].TrimEnd(new char[] { 'k', 'm' }));
                course.Climb = Convert.ToDecimal(details_node.InnerText.Split(new char[] { ' ' })[1].TrimEnd(new char[] { 'm' }));

                // Commit the course
                repository.Course(ref course);

                // Results
                var results_nodes = course_page.DocumentNode.SelectNodes(results_selector);
                foreach (var results_node in results_nodes) {
                    Result result = new Result();
                    result.Course = course;

                    // Position can be a number or null, if null then use the string as a code
                    var position_node = results_node.SelectSingleNode(results_position_selector);
                    try {
                        result.Position = Convert.ToInt32(position_node.ChildNodes[0].InnerText.TrimEnd(new char[] { 'r', 's', 't', 'n', 'd', 'h' }));
                    } catch (FormatException) {
                        result.Position = null;
                        result.Code = position_node.ChildNodes[0].InnerText;
                    }

                    // Name
                    var results_name_node = results_node.SelectSingleNode(results_name_selector);
                    result.Name = results_name_node.ChildNodes[0].InnerText;

                    // Club
                    var club_name_node = results_node.SelectSingleNode(results_club_selector);
                    result.ClubName = club_name_node.ChildNodes[0].InnerText;

                    // Age class
                    var age_class_node = results_node.SelectSingleNode(results_age_selector);
                    result.Age = age_class_node.ChildNodes[0].InnerText;

                    // Time
                    var time_node = results_node.SelectSingleNode(results_time_selector);
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
                    repository.Result(ref result);
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void ParseSinglePage(
            IImportRepository repository,
            HtmlDocument document,
            Event ev,
            string course_selector,
            string course_name_selector,
            string course_details_selector,
            string results_selector,
            string results_position_selector,
            string results_name_selector,
            string results_club_selector,
            string results_age_selector,
            string results_time_selector
        ) {
            // Courses
            var course_nodes = document.DocumentNode.SelectNodes(course_selector);
            foreach (var course_node in course_nodes) {
                Course course = new Course();
                course.Event = ev;

                // Course name
                var name_node = course_node.SelectSingleNode(course_name_selector);
                course.Name = name_node.InnerText;

                // Course details
                var details_node = course_node.SelectSingleNode(course_details_selector);
                if (details_node != null) {
                    course.Length = Convert.ToDecimal(details_node.InnerText.Split(new char[] { ' ' })[0].TrimEnd(new char[] { 'k', 'm' }));
                    course.Climb = Convert.ToDecimal(details_node.InnerText.Split(new char[] { ' ' })[1].TrimEnd(new char[] { 'm' }));
                }

                // Commit the course
                repository.Course(ref course);

                // Results
                var results_nodes = course_node.SelectNodes(results_selector);
                foreach (var results_node in results_nodes) {
                    Result result = new Result();
                    result.Course = course;

                    // Position can be a number or null, if null then use the string as a code
                    var position_node = results_node.SelectSingleNode(results_position_selector);
                    try {
                        result.Position = Convert.ToInt32(position_node.ChildNodes[0].InnerText.TrimEnd(new char[] { 'r', 's', 't', 'n', 'd', 'h' }));
                    } catch (FormatException) {
                        result.Position = null;
                        result.Code = position_node.ChildNodes[0].InnerText;
                    }

                    // Name
                    var results_name_node = results_node.SelectSingleNode(results_name_selector);
                    result.Name = results_name_node.ChildNodes[0].InnerText;

                    // Club
                    var club_name_node = results_node.SelectSingleNode(results_club_selector);
                    result.ClubName = club_name_node.ChildNodes[0].InnerText;

                    // Age class
                    var age_class_node = results_node.SelectSingleNode(results_age_selector);
                    result.Age = age_class_node.ChildNodes[0].InnerText;

                    // Time
                    var time_node = results_node.SelectSingleNode(results_time_selector);
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
                    repository.Result(ref result);
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        /// <summary>
        /// AutoDownload results split over multiple pages with splits
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="document"></param>
        /// <param name="ev"></param>
        private void ParseMultiplePageWithSplits(IImportRepository repository, HtmlDocument document, Event ev) {
            ParseMultiplePage(
                repository,
                document,
                ev,
                "//tr[td]",                             // Courses
                "td/td/a",                              // Course page address
                "//div[@class=\"course-title\"]/h2",    // Course name
                "//p[@class=\"course-info\"][2]",       // Course details
                "//tr[td]",                             // Results
                "td",                                   // Result position
                "td/td",                                // Result name
                "td/td/td",                             // Result club
                "td/td/td/td",                          // Result age
                "td/td/td/td/td"                        // Result time
            );
        }

        //---------------------------------------------------------------------------------------------------

        private void ParseSinglePageWithSplits(IImportRepository repository, HtmlDocument document, Event ev) {

        }

        //---------------------------------------------------------------------------------------------------

        private void ParseMutiplePage(IImportRepository repository, HtmlDocument document, Event ev) {
            ParseMultiplePage(
                repository,
                document,
                ev,
                "//tr[td]",                             // Courses
                "td/a",                                 // Course page address
                "//div[@class=\"course-title\"]/h2",    // Course name
                "//p[@class=\"course-info\"][2]",       // Course details
                "//tr[td]",                             // Results
                "td",                                   // Result position
                "td/td",                                // Result name
                "td/td/td",                             // Result club
                "td/td/td/td",                          // Result age
                "td/td/td/td/td"                        // Result time
            );
        }

        //---------------------------------------------------------------------------------------------------

        private void ParseSinglePage(IImportRepository repository, HtmlDocument document, Event ev) {
            ParseSinglePage(
                repository,
                document,
                ev,
                "//div[@class=\"course\"]",             // Courses
                "div[@class=\"course-title\"]/h2",      // Course name
                "p[@class=\"course-info\"][2]",         // Course details
                "table//tr[td]",                        // Results
                "td",                                   // Result position
                "td/td",                                // Result name
                "td/td/td",                             // Result club
                "td/td/td/td",                          // Result age
                "td/td/td/td/td"                        // Result time
            );
        }
    }
}
