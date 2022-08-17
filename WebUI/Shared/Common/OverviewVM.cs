using System.Collections.Generic;

namespace WebUI.Shared.Common
{
    public class OverviewVM<T>
    {
        public IList<T> Records { get; set; }

        public MetaData MetaData { get; set; }

        public OverviewVM()
        {
            Records = new List<T>();
            MetaData = new MetaData();
        }

        public OverviewVM(IList<T> records, MetaData metaData)
        {
            if (records != null)
                Records = records;
            else
                Records = new List<T>();

            MetaData = metaData;
        }
    }
}
