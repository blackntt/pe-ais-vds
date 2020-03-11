using System;
using System.Collections.Generic;
using VDS_New.Alg;

namespace VDS_New.DataLoader
{
    interface IDataLoader
    {
        List<VDSElement> LoadData(string path, double radius);
        String[] LoadFeatureTitle(string path);
        void LoadMinMaxData(string path, List<double> min_list, List<double> max_list);
    }
}
