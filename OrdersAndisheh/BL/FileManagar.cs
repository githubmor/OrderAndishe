﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BL
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class FileManagar
	{
        
        //private List<ItemSefaresh> items;
        private List<ReportRow> ReportRows;
        private string Tarikh;
        //public FileManagar(List<ItemSefaresh> items)
        //{
        //    this.items = items;
        //}

        public FileManagar(List<ReportRow> reportRows,string Tarikh )
        {
            // TODO: Complete member initialization
            this.ReportRows = reportRows;
            this.Tarikh = Tarikh;
        }

        
		public virtual void CreatFile(string fileName)
		{

            foreach (var row in ReportRows)
            {
                
            }
		}


        
    }
}
