using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaSync.Models.StaticParameters
{
    public class State
    {
        public static readonly string IndexingRemoteRepository = "Indexing Remote Repository";
        public static readonly string IndexingLocalRepository = "Indexing Local Repository";
        public static readonly string SyncronizationInProgress = "Syncronization";
        public static readonly string SyncronizationDone = String.Empty;
    }
}
