using ResultStore.Data;
using ResultStore.Data.Application;
using ResultStore.Processing.Import;
using ResultStore.Model.Orienteering;
using ResultStore.Data.Orienteering;

namespace ResultStore
{
    public class DebugData
    {
        public static void Generate() {
            // This sets up the database. NOT FOR PRODUCTION!
            SessionProvider.Instance.RebuildSchema();

            //var import_repo = new ImportRepository();
            //var autodownload1 = new AutoDownloadWeb(@"http://www.esoc.org.uk/results-files/2011/1023-barrybuddon/");
            //autodownload1.Import(import_repo);
            //var autodownload2 = new AutoDownloadWeb(@"http://www.esoc.org.uk/results-files/2011/0115-mary-erskine/");
            //autodownload2.Import(import_repo);
            //var autodownload3 = new AutoDownloadWeb(@"http://www.esoc.org.uk/results-files/2011/0212-hopetoun/");
            //autodownload3.Import(import_repo);

            var club_repo = new ClubRepository();
            var event_repo = new EventRepository();

            club_repo.Save(new Club { 
                Name = "West Anglian Orienteering Club", 
                ShortName = "WAOC", 
                Url = "http://www.waoc.org.uk" 
            });
            club_repo.Save(new Club { 
                Name = "Edinburgh University Orienteering Club", 
                ShortName = "EUOC",
                Url = "http://orienteering.eusu.ed.ac.uk" 
            });
            club_repo.Save(new Club { 
                Name = "Auld Reekie Orienteering Society", 
                ShortName = "AROS",
                Url = "http://www.aroslegends.com"
            });
            club_repo.Save(new Club {
                Name = "Edinburgh Southern Orienteering Club",
                ShortName = "ESOC",
                Url = "http://www.esoc.org.uk"
            });
        }
    }
}