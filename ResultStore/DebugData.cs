using ResultStore.Data;
using ResultStore.Data.Application;
using ResultStore.Processing.Import;
using ResultStore.Model.Orienteering;
using ResultStore.Data.Orienteering;

namespace ResultStore
{
    public class DebugData
    {
        static DebugData() {
            // This sets up the database. NOT FOR PRODUCTION!
            SessionProvider.Instance.RebuildSchema();

            var import_repo = new ImportRepository();
            var autodownload = new AutoDownloadWeb(@"http://www.esoc.org.uk/results-files/2011/1023-barrybuddon/index.html");
            autodownload.Import(import_repo);

            var club_repo = new ClubRepository();
            var event_repo = new EventRepository();

            club_repo.Save(new Club { 
                Name = "West Anglian Orienteering Club", 
                ShortName = "WAOC", 
                Url = "www.waoc.org.uk" 
            });
            club_repo.Save(new Club { 
                Name = "Edinburgh University Orienteering Club", 
                ShortName = "EUOC", 
                Url = "orienteering.eusu.ed.ac.uk" 
            });
            club_repo.Save(new Club { 
                Name = "Auld Reekie Orienteering Society", 
                ShortName = "AROS", 
                Url = "www.aroslegends.com" 
            });
        }
    }
}