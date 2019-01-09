﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LoopMoth.Models
{
    public class BibExport
    {
        public BibExport()
        {
            ToBibtex();
        }

        public void ToBibtex()
        {
            var db = new Entities();
            var prace = db.Prace.ToList();
            string x = "";
            foreach (var i in prace)
            {
                x += "@" + i.rodzaj + "{";
                x += i.id_pracy.ToString() + "_" + i.rok_publikacji.ToString() + ", ";
                x += "title={" + i.tytul + "}, ";
                x += "author={";
                foreach (var j in i.Autorzy)
                {
                    x += j + ", ";
                }
                x = x.Substring(x.Length - 2);
                x += "},";
                x += "publisher ={ ";
                x += i.Wydawcy;
                x += "}, ";
                x += "year={" + i.rok_publikacji + "}},";

            }
            x = x.Substring(x.Length - 1);
            File.WriteAllText("test.bib", x);
        }
    }
}