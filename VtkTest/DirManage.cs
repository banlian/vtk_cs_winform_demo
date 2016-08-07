
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vtk
{
    class DirManage
    {
        private static string root;

        public static string Root
        {
            get
            {
                return string.IsNullOrEmpty(root)
                    ? @"E:\PCL\PclTest": Root;
            }
            //root = Directory.GetCurrentDirectory() : root; }
            set { root = value; }
        }

        public static string PointDataDir
        {
            get { return Path.Combine(Root,"Data"); }
        }

        public static string ConfigDataDir
        {
            get { return Path.Combine(Root, "Config"); }
        }

    }
}
