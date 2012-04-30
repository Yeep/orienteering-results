using System;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;
using ResultStore.Data.Orienteering;

namespace ResultStore.Processing.Postprocess
{
    public class ClubRelationships : IPostprocess
    {
        private static IEventRepository s_event_repository;
        private static IClubRepository s_club_repository;
        private static ICourseRepository s_course_repository;

        private IImportRepository m_repository;

        //---------------------------------------------------------------------------------------------------

        static ClubRelationships() {
            s_event_repository = new EventRepository();
            s_club_repository = new ClubRepository();
            s_course_repository = new CourseRepository();
        }

        //---------------------------------------------------------------------------------------------------

        public void Execute(IImportRepository repository, Event e) {
            m_repository = repository;

            ProcessEvent(e);
            ProcessResults(e);
        }

        //---------------------------------------------------------------------------------------------------

        /// <summary>
        /// Try to relate the event club to an actual club
        /// </summary>
        /// <param name="e"></param>
        private void ProcessEvent(Event e) {
            // Only process once and don't bother if club name is empty
            if (e.Club == null && !String.IsNullOrWhiteSpace(e.ClubName)) {
                e.Club = FindOrCreateClub(e.ClubName);
                m_repository.Event(ref e);
            }
        }

        //---------------------------------------------------------------------------------------------------

        /// <summary>
        /// Try to relate the club for each result to an actual club
        /// </summary>
        /// <param name="e"></param>
        private void ProcessResults(Event e) {
            foreach (var course in s_course_repository.ForEvent(e)) {
                foreach (var result in s_course_repository.Results(course)) {
                    var res = result;
                    // Only process once and don't bother if club name is empty
                    if (res.Club == null && !String.IsNullOrWhiteSpace(res.ClubName)) {
                        res.Club = FindOrCreateClub(res.ClubName);
                        m_repository.Result(ref res);
                    }
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        private Club FindOrCreateClub(string name) {
            // Check the short name first
            var club = s_club_repository.GetByShortName(name);

            // If that fails try the full name
            if (club == null) {
                club = s_club_repository.GetByName(name);
            }

            if (club != null) {
                // A club was found, return it
                return club;
            } else {
                // No club was found, make a new one with the provided name
                club = new Club {
                    Name = name,
                    ShortName = name
                };
                s_club_repository.Save(club);
                return club;
            }
        }
    }
}
