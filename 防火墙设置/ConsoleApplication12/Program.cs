using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication12
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //关防火墙测试可以
                FireWallHelp.CloseFileWall();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
