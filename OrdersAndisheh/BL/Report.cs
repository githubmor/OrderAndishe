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

	public class Report
	{
        private Sefaresh sefaresh;
        public Report(Sefaresh sefaresh)
        {
            this.sefaresh = sefaresh;
            foreach (var item in sefaresh.Items)
            {
                ListKala.Add(item.Kala);
            }
        }

        public string Tarikh 
        {
            get { return sefaresh.Tarikh; }
        }

        public string Description
        {
            get { return sefaresh.Description; }
        }

        public string RozHafte
        {
            get { return ""; } //برای محاسبه نام روز هفته - شنبه و یکشنبه
        }

        public List<string> ListKala { get; private set; }
        
    }
}

