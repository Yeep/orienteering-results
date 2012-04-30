using System;
using System.Collections.Generic;
using ResultStore.Data.Application;
using ResultStore.Model.Application;
using ResultStore.Processing.Import;
using ResultStore.Processing.Postprocess;
using ResultStore.Repository;
using ResultStore.Model.Orienteering;

namespace ResultStore.Processing
{
    public class ImportQueue : IImportQueue
    {
        private IList<IPostprocess> m_postprocesses;
        private static IImportRepository s_repository;

        //---------------------------------------------------------------------------------------------------

        static ImportQueue() {
            s_repository = new MockImportRepository();
        }

        //---------------------------------------------------------------------------------------------------

        public ImportQueue() {
            m_postprocesses = new List<IPostprocess>();
        }

        //---------------------------------------------------------------------------------------------------

        public void Process(QueuedEvent e) {
            var importer = CreateImporter(e);

            var ev = importer.Import(s_repository);

            // Post processing
            foreach (var process in m_postprocesses) {
                process.Execute(s_repository, ev);
            }
        }

        //---------------------------------------------------------------------------------------------------

        public Event Summary(QueuedEvent e) {
            var importer = CreateImporter(e);
            return importer.Sample();
        }

        //---------------------------------------------------------------------------------------------------

        public void ProcessQueue() {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public void RegisterPostprocess(IPostprocess process) {
            m_postprocesses.Add(process);
        }

        //---------------------------------------------------------------------------------------------------

        public void UnregisterPostprocess(IPostprocess process) {
            m_postprocesses.Remove(process);
        }

        //---------------------------------------------------------------------------------------------------

        private IImporter CreateImporter(QueuedEvent e) {
            IImporter importer = Activator.CreateInstance(Type.GetType(e.Processor)) as IImporter;
            if (importer == null) {
                throw new Exception(String.Format("Could not create importer {0}", e.Processor));
            }
            importer.Event = e;
            return importer;
        }
    }
}
